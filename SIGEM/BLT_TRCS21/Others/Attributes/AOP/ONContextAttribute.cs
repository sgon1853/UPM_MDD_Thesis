// 3.4.4.5

using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// ONContextAdder
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ONContextAttribute: ContextAttribute
	{
		#region Constructors
		public ONContextAttribute() : base("ONContextAttribute")
		{
		}
		#endregion

		#region ContextAttribute Methods
		public override void GetPropertiesForNewContext(IConstructionCallMessage ccm)
		{
			ccm.ContextProperties.Add(new ONContextAdder(this as IONContextClassAttribute));
		}
		public override bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
		{
			return (false);
		}
		#endregion
	}
}

