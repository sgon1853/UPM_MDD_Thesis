// 3.4.4.5

using System;
using System.Reflection;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using SIGEM.Business.Action;
using SIGEM.Business.Data;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;
using SIGEM.Business.Types;
using SIGEM.Business.OID;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// Represents the attribute for an event.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	internal class ONEventAttribute : ONServiceAttribute, IONContextServiceAttribute
	{
		#region Members
		private bool mInStack;
		private ONContext mThisOnContext = null;
		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="className">Name of the class</param>
		/// <param name="serviceName">Name of the service</param>
		public ONEventAttribute(string className, string serviceName)
			: base (className, serviceName)
		{
			mInStack = false;
		}
		#endregion

		#region Interface IONContextServiceAttribute Methods
		/// <summary>
		/// PreProcess method of the AOP
		/// </summary>
		/// <param name="inst">Instance</param>
		/// <param name="msg">Message</param>
		public void Preprocess(MarshalByRefObject inst, IMessage msg)
		{
			if (mServiceCacheItem == null)
				mServiceCacheItem = ONServiceCacheItem.Get("Action", ClassName, ServiceName);

			IMethodCallMessage lMsg = msg as IMethodCallMessage;

			// Extract Action
			ONAction lAction = inst as ONAction;

			// Take the active Instance
			bool lFind = false;
			if (ServiceType != ServiceTypeEnumeration.Carrier)
			{
				foreach (ONInstance lInstance in lAction.OnContext.TransactionStack)
				{
					if (((object) lInstance != null) && (lInstance == lAction.Instance))
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
						if (((object)lInstance != null) && (lInstance == lAction.Instance))
						{
							// Saving the old context of the This instance
							lAction.Instance = lInstance.GetFacet(lAction.Instance.ClassName);
							mThisOnContext = lAction.Instance.OnContext;
							lAction.Instance.OnContext = lAction.OnContext;

							break;
						}
					}
				}
			}

			// Push OID to Class Stack
			lAction.OnContext.TransactionStack.Push(lAction.Instance);
			mInStack = true;

			// Check State Changes
			if (lAction.OnContext.NeedsVerification)
			{
				mServiceCacheItem.InvoqueCheckState(lAction, lMsg.Args);
				lAction.OnContext.NeedsVerification = false;
			}
			
			// Check STD
			if (!lFind)
				mServiceCacheItem.InvoqueSTD(lAction, lMsg.Args);

			// Check Precondition
			mServiceCacheItem.InvoquePrecondition(lAction, lMsg.Args);
		}
		/// <summary>
		/// PostProcess method of the AOP
		/// </summary>
		/// <param name="inst">Instance</param>
		/// <param name="msg">Message</param>
		/// <param name="msgReturn">Return Message</param>
		public void Postprocess(MarshalByRefObject inst, IMessage msg, ref IMessage msgReturn)
		{
			IMethodCallMessage lMsgIn = msg as IMethodCallMessage;
			IMethodReturnMessage lMsgOut = msgReturn as IMethodReturnMessage;

			// Extract Action
			ONAction lAction = inst as ONAction;

			// Update Data
			if ((ServiceType == ServiceTypeEnumeration.New) || (ServiceType == ServiceTypeEnumeration.Destroy))
				// Add ModifiedClass
				lAction.OnContext.AddModifiedClass(lAction);
			else
			{
				if ((ServiceType == ServiceTypeEnumeration.Carrier) || (ServiceType == ServiceTypeEnumeration.Liberator))
					// Add ModifiedClass
					lAction.OnContext.AddModifiedClass(lAction);
				else if (lAction.Instance.Modified)
				{
					// Update Instance
					ONData lData = ONContext.GetComponent_Data(lAction.ClassName, lAction.OnContext);
					lData.UpdateEdited(lAction.Instance);

					// Add ModifiedClass
					lAction.OnContext.AddModifiedClass(lAction);
				}
				lAction.Instance.ModifiedInTransaction = false;

				// Update Inheritance net data
				foreach (ONInstance lNetInstance in lAction.Instance.GetFacets())
				{
					if (((object) lNetInstance != null) && (lNetInstance.ClassName != lAction.Instance.ClassName) && lNetInstance.Modified)
					{
						// Update inheritance net Instance
						ONData lData = ONContext.GetComponent_Data(lNetInstance.ClassName, lNetInstance.OnContext);
						lData.UpdateEdited(lNetInstance);
 
						// Create action class
						ONAction lNetAction = ONContext.GetComponent_Action(lNetInstance.ClassName, lNetInstance.OnContext);
						lNetAction.Instance = lNetInstance;

						// Add inheritance net ModifiedClass
						lAction.OnContext.AddModifiedClass(lNetAction);
					}

					if ((object) lNetInstance != null)
						lNetInstance.ModifiedInTransaction = false;
				}
				
				if (lAction.OnContext.ModifiedClass.ContainsKey(lAction.Instance.Oid))
				{
					ONAction lNetAction = ONContext.GetComponent_Action(lAction.Instance.Root().ClassName, lAction.Instance.Root().OnContext);
					lNetAction.Instance = lAction.Instance.Root();

					lAction.OnContext.AddUnmodifiedClass(lNetAction);
				}
			}

			lAction.Instance.CleanDerivationCache();

			// Calculate OutboundArgumets
			object[] lArgs = lMsgOut.Args;
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
		}
		/// <summary>
		/// Represent the handdle for the exceptions
		/// </summary>
		/// <param name="inst">Instance</param>
		/// <param name="msg">Message</param>
		/// <param name="exception">Possible exception</param>
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

