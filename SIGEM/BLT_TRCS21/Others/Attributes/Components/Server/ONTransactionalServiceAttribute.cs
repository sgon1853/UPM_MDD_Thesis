// 3.4.4.5

using System;
using System.Runtime.Remoting.Messaging;
using SIGEM.Business.Server;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.Exceptions;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// ONTransactionalServiceAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	internal class ONTransactionalServiceAttribute : ONServiceAttribute, IONContextServiceAttribute
	{
		#region Members
		private bool mInStack;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="className">Name of the class</param>
		/// <param name="serviceName">Name of the service</param>
		public ONTransactionalServiceAttribute(string className, string serviceName)
			: base (className, serviceName)
		{
			mInStack = false;
		}
		#endregion

		#region Interface IONContextServiceAttribute Methods
		public void Preprocess(MarshalByRefObject inst, IMessage msg)
		{
			IMethodCallMessage lMsgIn = msg as IMethodCallMessage;

			// Extract Server
			ONServer lServer = inst as ONServer;

			// Take the active Instance
			bool lFind = false;
			foreach (ONInstance lInstance in lServer.OnContext.TransactionStack)
			{
				if (((object)lInstance != null) && (lInstance == lServer.Instance))
				{
					lServer.Instance = lInstance.GetFacet(lServer.Instance.ClassName);
					lFind = true;

					break;
				}
			}
			if (!lFind)
			{
				foreach (ONInstance lInstance in lServer.OnContext.OperationStack)
				{
					if (((object)lInstance != null) && (lInstance == lServer.Instance))
					{
						lServer.Instance = lInstance.GetFacet(lServer.Instance.ClassName);
						lFind = true;

						break;
					}
				}
			}

			// Push OID to Class Stack
			lServer.OnContext.OperationStack.Push(lServer.Instance);
			mInStack = true;

			// Search all facets
			foreach (IONType lArgument in lMsgIn.InArgs)
			{
				ONOid lOidArgument = lArgument as ONOid;
				if (lOidArgument != null)
					if (!lOidArgument.Exist(lServer.OnContext, null))
						throw new ONInstanceNotExistException(null, ONContext.GetComponent_Instance(lOidArgument.ClassName, lServer.OnContext).IdClass, lOidArgument.ClassName);
			}
		}
		public void Postprocess(MarshalByRefObject inst, IMessage msg, ref IMessage msgReturn)
		{
			IMethodCallMessage lMsgIn = msg as IMethodCallMessage;
			IMethodReturnMessage lMsgOut = msgReturn as IMethodReturnMessage;

			// Extract Server
			ONServer lServer = inst as ONServer;

			// Throw triggers
			lServer.OnContext.ThrowAcceptedTriggers();

			// Pop the OID from Class Stack
			lServer.OnContext.OperationStack.Pop();
			mInStack = false;
		}
		public void Exceptionprocess(MarshalByRefObject inst, IMessage msg, Exception exception)
		{
			// Extract Server
			ONServer lServer = inst as ONServer;

			// Pop the OID from Class Stack
			if (mInStack)
				lServer.OnContext.OperationStack.Pop();
		}
		#endregion
	}
}

