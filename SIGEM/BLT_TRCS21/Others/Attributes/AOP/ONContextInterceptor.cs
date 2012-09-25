// 3.4.4.5

using SIGEM.Business.Types;
using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Reflection;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// ONContextInterceptor.
	/// </summary>
	public class ONContextInterceptor: IMessageSink
	{
		#region Atributes
		private IMessageSink mReceiver;
		private MarshalByRefObject mInstance;
		private IONContextClassAttribute mOnContextClass;
		#endregion

		#region Gets / Sets
		public IMessageSink NextSink 
		{
			get
			{
				return mReceiver;
			}
		}
    
		public IMessageSink Receiver
		{
			get
			{
				return mReceiver;
			}
		}
		public MarshalByRefObject Instance
		{
			get
			{
				return mInstance;
			}
		}
		#endregion

		#region Constructors
		public ONContextInterceptor(MarshalByRefObject inst, IMessageSink rec, IONContextClassAttribute onContextClass)
		{
			mInstance = inst;
			mReceiver = rec;
			mOnContextClass = onContextClass;
		}
		#endregion

		#region IMessageSink Methods
		public IMessage SyncProcessMessage(IMessage msg)
		{
			IMessage lMessage = null;

			// Extract AOP Service
			IONContextServiceAttribute lONContextService = GetONContextServiceAttribute(msg);

			// Extract AOP Arguments
			ArrayList lONContextArguments = GetONContextArgumentAttribute(msg);

			try
			{
				// Execute Preprocess AOP if exist
				if (mOnContextClass != null)
					mOnContextClass.Preprocess(mInstance, msg);
				if (lONContextService != null)
					lONContextService.Preprocess(mInstance, msg);
				
				//Execute Preprocess of the Inbound Arguments
				foreach (IONContextArgumentAttribute lArgument in lONContextArguments)
				{
					if (lArgument is ONInboundArgumentAttribute)
						lArgument.Preprocess(mInstance, msg);
				}

				lMessage = mReceiver.SyncProcessMessage(msg);

				ReturnMessage lReturnMessage = lMessage as ReturnMessage;
				if ((lReturnMessage != null) && (lReturnMessage.Exception != null))
					throw lReturnMessage.Exception;	

				// Execute Postprocess AOP if exist
				if (lONContextService != null)
					lONContextService.Postprocess(mInstance, msg, ref lMessage);
				if (mOnContextClass != null)
					mOnContextClass.Postprocess(mInstance, msg, ref lMessage);
				
				//Execute Postprocess of the Outbound Arguments
				foreach (IONContextArgumentAttribute lArgument in lONContextArguments)
				{
					if (lArgument is ONOutboundArgumentAttribute)
						lArgument.Postprocess(mInstance, msg, ref lMessage);
				}
			}
			catch (Exception e)
			{
				if (lONContextService != null)
					lONContextService.Exceptionprocess(mInstance, msg, e);
				if (mOnContextClass != null)
					mOnContextClass.Exceptionprocess(mInstance, msg, e);

				throw e;
			}

			return (lMessage);
		}
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new InvalidOperationException();
		}
		#endregion

		#region ContextServiceAttribute
		private IONContextServiceAttribute GetONContextServiceAttribute(IMessage msg)
		{
			if (!(msg is IMethodCallMessage))
				return null;

			IMethodCallMessage lMsg = msg as IMethodCallMessage;
			MethodInfo lMethodInfo = lMsg.MethodBase as MethodInfo;

			object[] lAttributes = lMethodInfo.GetCustomAttributes(typeof(IONContextServiceAttribute), true);
			if (lAttributes.Length > 0)
				return lAttributes[0] as IONContextServiceAttribute;

			return null;
		}
		#endregion

		#region ContextArgumentAttribute
		private ArrayList GetONContextArgumentAttribute(IMessage msg)
		{
			int lIndex = 0;
			ArrayList lArguments = new ArrayList();
			if (!(msg is IMethodCallMessage))
				return null;

			IMethodCallMessage lMsg = msg as IMethodCallMessage;
			MethodInfo lMethodInfo = lMsg.MethodBase as MethodInfo;
			ParameterInfo[] lArgumentInfo = lMethodInfo.GetParameters();
			object[] lAttributes = null;
			
			foreach (ParameterInfo lArgument in lArgumentInfo)
			{
				lAttributes = lArgument.GetCustomAttributes(typeof(IONContextArgumentAttribute), true);
				
				if (lAttributes.Length > 0)
				{
					IONContextArgumentAttribute lArgumentAttribute = lAttributes[0] as IONContextArgumentAttribute;
					lArgumentAttribute.IndexArgument = lIndex;
					lArguments.Add(lArgumentAttribute);
					lIndex++;
				}
			}
			return lArguments;
		}
		#endregion
	}
}

