// 3.4.4.5

using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Contexts;
using System.EnterpriseServices;
using System.EnterpriseServices.Internal;
using System.EnterpriseServices.CompensatingResourceManager;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// Descripción breve de OnTransaction.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class ONInterceptionAttribute : ProxyAttribute, ICustomFactory
	{
		private ProxyAttribute mBase;
		private bool mIsServiceComponent;

		public ONInterceptionAttribute()
		{
			ProxyAttribute[] lProxyAttributes = (ProxyAttribute[]) typeof(ServicedComponent).GetCustomAttributes(typeof(ProxyAttribute), false);

			mBase = lProxyAttributes[0];
		}

		// Create an instance of ServicedComponentProxy
		public override MarshalByRefObject CreateInstance(Type serverType)
		{  
			mIsServiceComponent = serverType.IsSubclassOf(typeof(ServicedComponent));

			MarshalByRefObject lTarget = null;
			if (mIsServiceComponent)
				lTarget = mBase.CreateInstance(serverType);
			else
				lTarget = base.CreateInstance(serverType);
			RealProxy lRealProxy = new ONRealProxy(lTarget, serverType);

			return lRealProxy.GetTransparentProxy() as MarshalByRefObject;
		}

		MarshalByRefObject ICustomFactory.CreateInstance(Type serverType)
		{
			if (mIsServiceComponent)
				return (MarshalByRefObject) ((ICustomFactory) mBase).CreateInstance(serverType);
			else
				return (MarshalByRefObject) base.CreateInstance(serverType);
		}
	}
}

