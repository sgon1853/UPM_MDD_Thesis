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
	/// ONSharedEventAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	internal class ONSharedEventAttribute : ONServiceAttribute, IONContextServiceAttribute
	{
		#region Members
		private List<KeyValuePair<ONAction, object[]>> mSharedClasses = new List<KeyValuePair<ONAction, object[]>>();
		private bool mSharedPrincipal;
		private bool mInStack;
		private ONContext mThisOnContext = null;

		private List<ONServiceCacheItem> mServiceCacheItems = null;
		#endregion

		#region Constructors
		public ONSharedEventAttribute(string className, string serviceName)
			: base (className, serviceName)
		{
			mSharedPrincipal = false;
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
			mSharedPrincipal = true;

			// Shared Event Arguments (in Shared Event order)
			HybridDictionary lSharedEventArguments = new HybridDictionary(true);
			HybridDictionary lSharedEventArgumentsInfo = new HybridDictionary(true);
			int i = 0;
			foreach (ParameterInfo lArgument in lMsg.MethodBase.GetParameters())
			{
				ONSharedArgumentAttribute[] lSharedArgumentsInfo = lArgument.GetCustomAttributes(typeof(ONSharedArgumentAttribute),false) as ONSharedArgumentAttribute[];
				foreach (ONSharedArgumentAttribute lSharedArgumentInfo in lSharedArgumentsInfo)
				{
					lSharedEventArgumentsInfo[lArgument.Name] = lSharedArgumentInfo;
					break;
				}

				lSharedEventArguments[lArgument.Name] = lMsg.Args[i];
				i++;
			}

			// Shared Event Arguments for each Class (in Shared Event order)
			mServiceCacheItems = new List<ONServiceCacheItem>();
			foreach (DictionaryEntry lEntry in lSharedEventArgumentsInfo)
			{
				string lSharedArgumentName = lEntry.Key as String;
				ONSharedArgumentAttribute lSharedArgumentInfo = lEntry.Value as ONSharedArgumentAttribute;

				// Create Instance
				ONInstance lInstance = (lSharedEventArguments[lSharedArgumentName] as ONOid).GetInstance(lAction.OnContext);

				if (lInstance == null)
					throw new ONInstanceNotExistException(null, "", (lSharedEventArguments[lSharedArgumentName] as ONOid).ClassName);

				// Create Action
				ONAction lSharedAction = ONContext.GetComponent_Action(lSharedArgumentInfo.ClassName, lAction.OnContext);
				lSharedAction.Instance = lInstance;

				// Reorder arguments
				lIndex = 0;
				object[] lArguments = new object[lSharedEventArguments.Count];
				foreach (MethodInfo lMethodInfo in ONContext.GetMethods(lSharedAction.GetType(), typeof(ONServiceAttribute), ToString(lSharedAction.ClassName)))
					foreach (ParameterInfo lParameterInfo in lMethodInfo.GetParameters())
					{
						if (lSharedEventArguments.Contains(lParameterInfo.Name))
							// Normal Argument
							lArguments[lIndex++] = lSharedEventArguments[lParameterInfo.Name];
						else
						{
							ONSharedArgumentAttribute[] lSharedArgumentsInfo = lParameterInfo.GetCustomAttributes(typeof(ONSharedArgumentAttribute),false) as ONSharedArgumentAttribute[];
							if (lSharedArgumentsInfo.Length == 0)
								// Target Oid
								lArguments[lIndex++] = lInstance.Oid;
							else if(string.Compare(lSharedArgumentsInfo[0].ClassName, lAction.ClassName, true) == 0)
								// Source Oid
								lArguments[lIndex++] = lAction.Instance.Oid;
							else
								lArguments[lIndex++] = null;
						}
					}

				mSharedClasses.Add(new KeyValuePair<ONAction, object[]>(lSharedAction, lArguments));
				mServiceCacheItems.Add(ONServiceCacheItem.Get("Action", lSharedArgumentInfo.ClassName, ServiceName));
			}

			// Check State Changes
			if (lAction.OnContext.NeedsVerification)
			{
				mServiceCacheItem.InvoqueCheckState(lAction, lMsg.Args);
				lAction.OnContext.NeedsVerification = false;
			}
			
			// Check Shared STD
			lIndex = 0;
			foreach (KeyValuePair<ONAction, object[]> lShared in mSharedClasses)
			{
				// Take the active Instance
				bool lSharedFind = false;
				foreach (ONInstance lInstance in lAction.OnContext.TransactionStack)
				{
					if (lInstance == lShared.Key.Instance)
					{
						lShared.Key.Instance = lInstance.GetFacet(lShared.Key.Instance.ClassName);
						lSharedFind = true;
						break;
					}
				}
				if (!lSharedFind)
				{
					foreach (ONInstance lInstance in lAction.OnContext.OperationStack)
					{
						if (lInstance == lShared.Key.Instance)
						{
							lShared.Key.Instance = lInstance.GetFacet(lShared.Key.Instance.ClassName);
							lShared.Key.Instance.OnContext = lShared.Key.OnContext;

							break;
						}
					}
				}				

				// Push OID to Class Stack
				lShared.Key.OnContext.TransactionStack.Push(lShared.Key.Instance);

				// Check STD
				if (!lSharedFind)
					mServiceCacheItems[lIndex++].InvoqueSTD(lShared.Key, lShared.Value);
			}

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
			foreach (KeyValuePair<ONAction, object[]> lShared in mSharedClasses)
				mServiceCacheItems[lIndex++].InvoquePrecondition(lShared.Key, lShared.Value);

			// Check Precondition
			mServiceCacheItem.InvoquePrecondition(lAction, lMsg.Args);

            // Get Initial Values of the instances
            lAction.OnContext.GetInitialValues(lAction, lMsg.Args);

			// Throw Check Shared Partials
			lIndex = 0;
			foreach (KeyValuePair<ONAction, object[]> lShared in mSharedClasses)
                if ((string.Compare(lAction.ClassName, lShared.Key.ClassName, true) != 0) || (lAction.Instance != lShared.Key.Instance))
					mServiceCacheItems[lIndex++].InvoqueService(lShared.Key, lShared.Value);
		}
        public void Postprocess(MarshalByRefObject inst, IMessage msg, ref IMessage msgReturn)
		{
			IMethodCallMessage lMsgIn = msg as IMethodCallMessage;
			IMethodReturnMessage lMsgOut = msgReturn as IMethodReturnMessage;
			int lIndex = 0;

			// Extract Action
			ONAction lAction = inst as ONAction;

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
					if (((object) lNetInstance != null) && (lNetInstance.ClassName != lAction.ClassName) && lNetInstance.Modified)
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

				if (lAction.OnContext.ModifiedClass.ContainsKey(lAction.Instance.Oid))
				{
					ONAction lNetAction = ONContext.GetComponent_Action(lAction.Instance.Root().ClassName, lAction.Instance.Root().OnContext);
					lNetAction.Instance = lAction.Instance.Root();

					lAction.OnContext.AddUnmodifiedClass(lNetAction);
				}
			}

            // Shared Event Control
			if (mSharedPrincipal == false) // No-Principal Shared Event
				return;
			lAction.OnContext.InSharedContext = false;

			// Calculate Shared OutboundArguments
			lIndex = 0;
			object[] lArgs = lMsgOut.Args;
			foreach (KeyValuePair<ONAction, object[]> lShared in mSharedClasses)
			{
				mServiceCacheItems[lIndex++].InvoqueOutboundArguments(lShared.Key, lShared.Value);

				// Copy outbound arguments (only not initialized)
				for (int i = lMsgOut.ArgCount - lMsgOut.OutArgCount; i < lMsgOut.ArgCount; i++)
					if (new ONBool(lShared.Value[i] as ONSimpleType != null) || lShared.Value[i] as ONOid != null)
						lArgs[i] = lShared.Value[i];

				// Pop the Shared OID from Class Stack
				lAction.OnContext.TransactionStack.Pop();
			}

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

