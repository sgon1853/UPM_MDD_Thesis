// 3.4.4.5

using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Services;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// This class solve the AOP algorithm
	/// </summary>
	public class ONRealProxy : RealProxy
	{
		#region Members
		private MarshalByRefObject mTarget;
		private Type mTargetType;
		private object mReturnValue;
		private MethodBase mMethod;
		private object[] mArgs;

		List<IONContextClassAttribute> mONContextClass = null;
		List<IONContextServiceAttribute> mONContextService = null;
		List<List<IONContextArgumentAttribute>> mONContextArguments = null;
		#endregion

		#region Constructors
		public ONRealProxy(MarshalByRefObject target, Type targetType) : base (targetType)
		{
			mTarget = target;
		}
		#endregion

		#region Invoke
		public override IMessage Invoke(IMessage msg)
		{
			IMethodCallMessage lMethodMsg = msg as IMethodCallMessage;
			IMessage lMsgOut = null;

			if (lMethodMsg is IConstructionCallMessage)
			{
				mTargetType = lMethodMsg.MethodBase.DeclaringType;
				mMethod = lMethodMsg.MethodBase;
				mArgs = lMethodMsg.Args;

				RealProxy lRealProxy = RemotingServices.GetRealProxy(mTarget);
				lRealProxy.InitializeServerObject((IConstructionCallMessage) lMethodMsg);

				MarshalByRefObject lTransparentProxy = (MarshalByRefObject) GetTransparentProxy();
				IMessage lReturnMessage = EnterpriseServicesHelper.CreateConstructionReturnMessage((IConstructionCallMessage) lMethodMsg, lTransparentProxy);

				return (lReturnMessage);
			}
			else
			{
				mMethod = lMethodMsg.MethodBase;
				mArgs = lMethodMsg.Args;

				// Initialize variables AOP
				Initprocess(msg);

				try
				{
					// Execute Preprocess AOP
					Preprocess(msg);

					try
					{
						if (string.Compare(mMethod.Name, "FieldGetter", true) == 0)
						{
							FieldInfo lFieldInfo = mTarget.GetType().GetField(lMethodMsg.Args[1] as string);
							mReturnValue = lFieldInfo.GetValue(mTarget);
						}
						else
						{
							MethodInfo lMethodInfo = mTarget.GetType().GetMethod(mMethod.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
							if (lMethodInfo == null)
								lMethodInfo = mTarget.GetType().GetMethod(mMethod.Name, BindingFlags.Instance | BindingFlags.Public);
							mReturnValue = lMethodInfo.Invoke(mTarget, mArgs);
						}
						lMsgOut = new ReturnMessage(mReturnValue, mArgs, lMethodMsg.ArgCount, lMethodMsg.LogicalCallContext, lMethodMsg);
					}
					catch (Exception e)
					{
						throw e.InnerException;
					}

					// Execute Preprocess AOP
					Postprocess(msg, ref lMsgOut);
				}
				catch (Exception e)
				{
					// Execute Preprocess AOP
					Exceptionprocess(msg, e);

					throw;
				}

				return (lMsgOut);
			}
		}
		private void Preprocess(IMessage msg)
		{
			// Execute Preprocess AOP
			foreach (IONContextClassAttribute lAttribute in mONContextClass)
				lAttribute.Preprocess(mTarget, msg);

			foreach (List<IONContextArgumentAttribute> lAttributes in mONContextArguments)
				foreach (IONContextArgumentAttribute lAttribute in lAttributes)
					lAttribute.Preprocess(mTarget, msg);

			foreach (IONContextServiceAttribute lAttribute in mONContextService)
				lAttribute.Preprocess(mTarget, msg);
		}
		private void Postprocess(IMessage msg, ref IMessage lMsgOut)
		{
			// Execute Postprocess AOP
			foreach (IONContextServiceAttribute lAttribute in mONContextService)
				lAttribute.Postprocess(mTarget, msg, ref lMsgOut);

			foreach (List<IONContextArgumentAttribute> lAttributes in mONContextArguments)
				foreach (IONContextArgumentAttribute lAttribute in lAttributes)
					lAttribute.Postprocess(mTarget, msg, ref lMsgOut);

			foreach (IONContextClassAttribute lAttribute in mONContextClass)
				lAttribute.Postprocess(mTarget, msg, ref lMsgOut);
		}
		private void Exceptionprocess(IMessage msg, Exception exception)
		{
			// Execute Exceptionprocess AOP
			foreach (IONContextServiceAttribute lAttribute in mONContextService)
				lAttribute.Exceptionprocess(mTarget, msg, exception);

			foreach (List<IONContextArgumentAttribute> lAttributes in mONContextArguments)
				foreach (IONContextArgumentAttribute lAttribute in lAttributes)
					lAttribute.Exceptionprocess(mTarget, msg, exception);

			foreach (IONContextClassAttribute lAttribute in mONContextClass)
				lAttribute.Exceptionprocess(mTarget, msg, exception);
		}
		#endregion

		#region Get context attributes
		private void Initprocess(IMessage msg)
		{
			// Extract AOP Service
			mONContextClass = InitprocessClass(msg);

			// Extract AOP Service
			mONContextService = InitprocessService(msg);

			// Extract AOP Arguments
			mONContextArguments = InitprocessArgument(msg);
		}
		private List<IONContextClassAttribute> InitprocessClass(IMessage msg)
		{
			IMethodCallMessage lMsg = msg as IMethodCallMessage;
			if (lMsg == null)
				return null;

			List<IONContextClassAttribute> lReturn = new List<IONContextClassAttribute>();
			foreach (IONContextClassAttribute lAttribute in mTargetType.GetCustomAttributes(typeof(IONContextClassAttribute), true))
				lReturn.Add(lAttribute);

			return lReturn;
		}
		private List<IONContextServiceAttribute> InitprocessService(IMessage msg)
		{
			IMethodCallMessage lMsg = msg as IMethodCallMessage;
			if (lMsg == null)
				return null;

			List<IONContextServiceAttribute> lReturn = new List<IONContextServiceAttribute>();
			foreach (IONContextServiceAttribute lAttribute in (lMsg.MethodBase as MethodInfo).GetCustomAttributes(typeof(IONContextServiceAttribute), true))
				lReturn.Add(lAttribute);

			return lReturn;
		}
		private List<List<IONContextArgumentAttribute>> InitprocessArgument(IMessage msg)
		{
			IMethodCallMessage lMsg = msg as IMethodCallMessage;
			if (lMsg == null)
				return null;

			int lIndex = 0;

			List<List<IONContextArgumentAttribute>> lReturn = new List<List<IONContextArgumentAttribute>>();
			foreach (ParameterInfo lArgument in (lMsg.MethodBase as MethodInfo).GetParameters())
			{
				List<IONContextArgumentAttribute> lReturnArgument = new List<IONContextArgumentAttribute>();
				foreach (IONContextArgumentAttribute lAttribute in lArgument.GetCustomAttributes(typeof(IONContextArgumentAttribute), true))
				{
					lAttribute.IndexArgument = lIndex;
					lReturnArgument.Add(lAttribute);
				}

				lReturn.Add(lReturnArgument);

				lIndex++;
			}
			return lReturn;
		}
		#endregion
	}
}

