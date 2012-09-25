// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Reflection;
using SIGEM.Business.Action;
using SIGEM.Business.Instance;
using SIGEM.Business.OID;
using SIGEM.Business.Data;
using SIGEM.Business.Attributes;
using SIGEM.Business.Exceptions;
using SIGEM.Business.Types;

namespace SIGEM.Business.Attributes
{
    /// <summary>
    /// ONReflexiveEventAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal class ONReflexiveEventAttribute : ONServiceAttribute, IONContextServiceAttribute
    {
        #region Members
        private KeyValuePair<ONAction, object[]> mReflexiveClass = new KeyValuePair<ONAction, object[]>();
        private bool mReflexivePrincipal;
        private bool mInStack;
        private ONContext mThisOnContext = null;

        private List<ONServiceCacheItem> mServiceCacheItems = null; 
        #endregion

        #region Constructors
        public ONReflexiveEventAttribute(string className, string serviceName)
            : base(className, serviceName)
        {
            mReflexivePrincipal = false;
            mInStack = false;
        }
        #endregion

        #region Interface IONContextServiceAttribute Methods
        public void Preprocess(MarshalByRefObject inst, IMessage msg)
        {
            if (mServiceCacheItem == null)
      	    mServiceCacheItem = ONServiceCacheItem.Get("Action", ClassName, ServiceName);

            IMethodCallMessage lMsg = msg as IMethodCallMessage;
            int lIndex = 0;

            // Extract Action
            ONAction lAction = inst as ONAction;

            // Shared Event Control
            if (lAction.OnContext.InSharedContext == true) // No-Principal Shared Event  
                return;
            lAction.OnContext.InSharedContext = true;
            mReflexivePrincipal = true;

            // Shared Event Arguments (in Shared Event order)
            HybridDictionary lReflexiveEventArguments = new HybridDictionary(true);
            HybridDictionary lReflexiveEventArgumentsInfo = new HybridDictionary(true);
            int i = 0;
            foreach (ParameterInfo lArgument in lMsg.MethodBase.GetParameters())
            {
                ONSharedArgumentAttribute[] lReflexiveArgumentsInfo = lArgument.GetCustomAttributes(typeof(ONSharedArgumentAttribute), false) as ONSharedArgumentAttribute[];
                foreach (ONSharedArgumentAttribute lReflexiveArgumentInfo in lReflexiveArgumentsInfo)
                {
                    lReflexiveEventArgumentsInfo[lArgument.Name] = lReflexiveArgumentInfo;
                    break;
                }

                lReflexiveEventArguments[lArgument.Name] = lMsg.Args[i];
                i++;
            }

            // Shared Event Arguments for each Class (in Shared Event order)
            mServiceCacheItems = new List<ONServiceCacheItem>();
            foreach (DictionaryEntry lEntry in lReflexiveEventArgumentsInfo)
            {
                string lReflexiveArgumentName = lEntry.Key as String;
                ONSharedArgumentAttribute lReflexiveArgumentInfo = lEntry.Value as ONSharedArgumentAttribute;

                // Create Instance
                ONInstance lInstance = (lReflexiveEventArguments[lReflexiveArgumentName] as ONOid).GetInstance(lAction.OnContext);

                if (lInstance == null)
                    throw new ONInstanceNotExistException(null, "", (lReflexiveEventArguments[lReflexiveArgumentName] as ONOid).ClassName);

                // Create Action 
                ONAction lReflexiveAction = ONContext.GetComponent_Action(lReflexiveArgumentInfo.ClassName, lAction.OnContext);
                //lSharedAction.Instance
                lReflexiveAction.Instance = lInstance;

                // Copy arguments
                MethodInfo lMethodInfo = ONContext.GetMethods(lReflexiveAction.GetType(), ToStringPartial());
                lIndex = 0;
                object[] lArguments = new object[lReflexiveEventArguments.Count];
                foreach (ParameterInfo lParameterInfo in lMethodInfo.GetParameters())
                {
                		// Normal Argument
                        lArguments[lIndex++] = lReflexiveEventArguments[lParameterInfo.Name];
                }
                mReflexiveClass = new KeyValuePair<ONAction, object[]>(lReflexiveAction, lArguments);
                mServiceCacheItems.Add(ONServiceCacheItem.Get("Action", lReflexiveArgumentInfo.ClassName, ServiceName + "_Partial"));
                object[] lObject = new object[mReflexiveClass.Value.Length];
                lObject[0] = mReflexiveClass.Value[1];
                lObject[1] = mReflexiveClass.Value[0];
                mReflexiveClass.Value[0] = lObject[0];
                mReflexiveClass.Value[1] = lObject[1];
            }

            // Take the active Instance
            lIndex = 0;
            bool lSharedFind = false;
            foreach (ONInstance lInstance in lAction.OnContext.TransactionStack)
            {
                  if (lInstance == mReflexiveClass.Key.Instance)
                  {
                        mReflexiveClass.Key.Instance = lInstance.GetFacet(mReflexiveClass.Key.Instance.ClassName);
                        lSharedFind = true;
                        break;
                  }
            }
            if (!lSharedFind)
            {
                foreach (ONInstance lInstance in lAction.OnContext.OperationStack)
                {
                    if (lInstance == mReflexiveClass.Key.Instance)
                    {
                        mReflexiveClass.Key.Instance = lInstance.GetFacet(mReflexiveClass.Key.Instance.ClassName);
                        mReflexiveClass.Key.Instance.OnContext = mReflexiveClass.Key.OnContext;

                        break;
                    }
                }
        	}

            // Check State Changes
			if (lAction.OnContext.NeedsVerification)
			{
				mServiceCacheItem.InvoqueCheckState(lAction, lMsg.Args);
				lAction.OnContext.NeedsVerification = false;
			}

			
            // Push OID to Class Stack
            mReflexiveClass.Key.OnContext.TransactionStack.Push(mReflexiveClass.Key.Instance);

            // Check Shared STD
            if (!lSharedFind)
                    mServiceCacheItem.InvoqueSTD(mReflexiveClass.Key, mReflexiveClass.Value);

            // Take the active Instance
            bool lFind = false;
            foreach (ONInstance lInstance in lAction.OnContext.TransactionStack)
            {
                if (lInstance == lAction.Instance)
                {
                    lAction.Instance = lInstance.GetFacet(lAction.Instance.ClassName);
                    lFind = true;
                    break;
                }
            }
            if (!lFind)
            {
                foreach (ONInstance lInstance in lAction.OnContext.OperationStack)
                {
                    if (lInstance == lAction.Instance)
                    {
                        // Saving the old context of the This instance
                        lAction.Instance = lInstance.GetFacet(lAction.Instance.ClassName);
                        mThisOnContext = lAction.Instance.OnContext;
                        lAction.Instance.OnContext = lAction.OnContext;

                        break;
                    }
                }
            }

            // Push OID to Class Stack
            lAction.OnContext.TransactionStack.Push(lAction.Instance);
            mInStack = true;

            // Check STD
            if (!lFind)
                mServiceCacheItem.InvoqueSTD(lAction, lMsg.Args);

            // Check Shared Precondition
            lIndex = 0;
            mServiceCacheItem.InvoquePrecondition(mReflexiveClass.Key, mReflexiveClass.Value);
            
            // Check Precondition
            mServiceCacheItem.InvoquePrecondition(lAction, lMsg.Args);
            
            // Get Initial Values of the instances
            lAction.OnContext.GetInitialValues(lAction, lMsg.Args);

            // Throw Check Shared Partials
            lIndex = 0;
            if (mReflexiveClass.Key.Instance != lAction.Instance)
                {
                    ONContext.InvoqueMethod(mReflexiveClass.Key, "_" + ServiceName + "_Partial", mReflexiveClass.Value);
                }
        }
      
         public void Postprocess(MarshalByRefObject inst, IMessage msg, ref IMessage msgReturn)
        {
            IMethodCallMessage lMsgIn = msg as IMethodCallMessage;
            IMethodReturnMessage lMsgOut = msgReturn as IMethodReturnMessage;
            int lIndex = 0;

            // Extract Action
            ONAction lAction = inst as ONAction;

            // Shared Event Control
            if (mReflexivePrincipal == false) // No-Principal Shared Event
                return;
            lAction.OnContext.InSharedContext = false;

                if ((ServiceType != ServiceTypeEnumeration.Destroy) && (ServiceType != ServiceTypeEnumeration.New))
                {
                    if (mReflexiveClass.Key.Instance.Modified)
                    {
                        // Update Instance
                        ONData lData = ONContext.GetComponent_Data(mReflexiveClass.Key.ClassName, lAction.OnContext);
                        lData.UpdateEdited(mReflexiveClass.Key.Instance);

                        // Add ModifiedClass
                        lAction.OnContext.AddModifiedClass(mReflexiveClass.Key);
                    }
                    else if (ServiceType == ServiceTypeEnumeration.Insertion || ServiceType == ServiceTypeEnumeration.Deletion)
                    {
                        // Add ModifiedClass
                        lAction.OnContext.AddUnmodifiedClass(mReflexiveClass.Key);
                    }

                    // Update Inheritance net data
                    foreach (ONInstance lNetInstance in mReflexiveClass.Key.Instance.GetFacets())
                    {
                        if (((object)lNetInstance != null) && (lNetInstance.ClassName != mReflexiveClass.Key.ClassName) && lNetInstance.Modified)
                        {
                            // Update inheritance net Instance
                            ONData lData = ONContext.GetComponent_Data(lNetInstance.ClassName, lNetInstance.OnContext);
                            lData.UpdateEdited(lNetInstance);

                            // Create action class
                            ONAction lNetAction = ONContext.GetComponent_Action(lNetInstance.ClassName, lNetInstance.OnContext);
                            lNetAction.Instance = lNetInstance;

                            // Add inheritance net ModifiedClass
                            lAction.OnContext.AddModifiedClass(mReflexiveClass.Key);
                        }
                    }
            }

            if ((ServiceType != ServiceTypeEnumeration.Destroy) && (ServiceType != ServiceTypeEnumeration.New))
            {
                if (lAction.Instance.Modified)
                {
                    // Update Instance
                    ONData lData = ONContext.GetComponent_Data(lAction.ClassName, lAction.OnContext);
                    lData.UpdateEdited(lAction.Instance);

                    // Add ModifiedClass
                    lAction.OnContext.AddModifiedClass(lAction);
                }
                else
                {
                    // Add ModifiedClass
                    lAction.OnContext.AddUnmodifiedClass(lAction);
                }

                // Update Inheritance net data
                foreach (ONInstance lNetInstance in lAction.Instance.GetFacets())
                {
                    if (((object)lNetInstance != null) && (lNetInstance.ClassName != lAction.ClassName) && lNetInstance.Modified)
                    {
                        // Update inheritance net Instance
                        ONData lData = ONContext.GetComponent_Data(lNetInstance.ClassName, lNetInstance.OnContext);
                        lData.UpdateEdited(lNetInstance);

                        // Create action class
                        ONAction lNetAction = ONContext.GetComponent_Action(lNetInstance.ClassName, lNetInstance.OnContext);
                        lNetAction.Instance = lNetInstance;

                        // Add inheritance net ModifiedClass
                        lAction.OnContext.AddModifiedClass(lAction);
                    }
                }
            }

            // Calculate Shared OutboundArguments
            lIndex = 0;
            object[] lArgs = lMsgOut.Args;
            mServiceCacheItems[lIndex++].InvoqueOutboundArguments(mReflexiveClass.Key, mReflexiveClass.Value);

            // Copy outbound arguments (only not initialized)
            for (int i = lMsgOut.ArgCount - lMsgOut.OutArgCount; i < lMsgOut.ArgCount; i++)
            if (new ONBool(mReflexiveClass.Value[i] as ONSimpleType != null) || mReflexiveClass.Value[i] as ONOid != null)
      	            lArgs[i] = mReflexiveClass.Value[i];

            // Pop the Shared OID from Class Stack
            lAction.OnContext.TransactionStack.Pop();

            // Calculate OutboundArgumets
            mServiceCacheItem.InvoqueOutboundArguments(lAction, lArgs);

            // Pop the OID from Class Stack
            lAction.OnContext.TransactionStack.Pop();
            mInStack = false;

            // Restoing the old context of the This instance
            if (mThisOnContext != null)
                lAction.Instance.OnContext = mThisOnContext;

            msgReturn = new ReturnMessage(lMsgOut.ReturnValue, lArgs, lArgs.Length, lMsgOut.LogicalCallContext, lMsgIn);
            if (lAction.OnContext.TransactionStack.Count == 0)
            {
                // Check triggers
                lAction.OnContext.CheckTriggers();

                // Check integrity constraints
                lAction.OnContext.CheckIntegrityConstraints();
            }
            
            lAction.OnContext.InstancesInitialValues.Clear();
        }
        public void Exceptionprocess(MarshalByRefObject inst, IMessage msg, Exception exception)
        {
            // Extract Action
            ONAction lAction = inst as ONAction;

            // Pop the OID from Class Stack
            if (mInStack)
                lAction.OnContext.TransactionStack.Pop();
        }
        #endregion
    }
}

