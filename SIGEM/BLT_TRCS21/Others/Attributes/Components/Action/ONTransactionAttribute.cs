// 3.4.4.5

using System;
using System.Reflection;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using SIGEM.Business.Action;
using SIGEM.Business.Data;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// Attribute for the Transactions.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	internal class ONTransactionAttribute : ONServiceAttribute, IONContextServiceAttribute
	{
		#region Members
		private bool mInStack;
		private ONContext mThisOnContext = null;
		#endregion

		#region Constructors
		public ONTransactionAttribute(string className, string serviceName)
			: base(className, serviceName)
		{
			mInStack = false;
		}
		#endregion

		#region Interface IONContextServiceAttribute Methods
		public void Preprocess(MarshalByRefObject inst, IMessage msg)
		{
			if (mServiceCacheItem == null)
				mServiceCacheItem = ONServiceCacheItem.Get("Action", ClassName, ServiceName);

			IMethodCallMessage lMsg = msg as IMethodCallMessage;

			// Extract Action
			ONAction lAction = inst as ONAction;

			// Take the active Instance
			bool lFind = false;
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
			if (( (object) lAction.Instance != null) && (!lFind))
				mServiceCacheItem.InvoqueSTD(lAction, lMsg.Args);

			// Check Precondition
			mServiceCacheItem.InvoquePrecondition(lAction, lMsg.Args);
		}
		public void Postprocess(MarshalByRefObject inst, IMessage msg, ref IMessage msgReturn)
		{
			IMethodCallMessage lMsgIn = msg as IMethodCallMessage;
			IMethodReturnMessage lMsgOut = msgReturn as IMethodReturnMessage;

			// Extract Action
			ONAction lAction = inst as ONAction;

			if ((lAction.Instance != null) && (lAction.Instance.ModifiedInTransaction))
			{
				foreach (string lActiveFacet in lAction.Instance.LeafActiveFacets())
				{
					ONInstance lInstanceToModify;
					if(lActiveFacet == lAction.ClassName)
						lInstanceToModify = lAction.Instance;
					else
						lInstanceToModify = lAction.Instance.GetFacet(lActiveFacet);
					// Update Instance
					if (lInstanceToModify.ModifiedInTransaction)
					{
						ONData lData = ONContext.GetComponent_Data(lInstanceToModify.ClassName, lInstanceToModify.OnContext);
						lData.UpdateEdited(lInstanceToModify);
					}
				}
				foreach (ONInstance lInstance in lAction.Instance.GetFacets())
					if(lInstance != null)
						lInstance.ModifiedInTransaction = false;
				
				lAction.Instance.ModifiedInTransaction = false;
			}

			// Calculate OutputArgumets
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

