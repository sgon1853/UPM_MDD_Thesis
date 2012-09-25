// 3.4.4.5

using System;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// Attribute related with each of the services given in the model object.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	internal class ONServiceAttribute : Attribute, IONAttribute
	{
		#region Members
		public string ClassName;
		public string ServiceName;
		public ServiceTypeEnumeration ServiceType;
		protected ONServiceCacheItem mServiceCacheItem;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="className">Name of the class</param>
		/// <param name="serviceName">Name of the service</param>
		public ONServiceAttribute(string className, string serviceName)
		{
			ClassName = className;
			ServiceName = serviceName;

			ServiceType = ServiceTypeEnumeration.Normal;
			mServiceCacheItem = null;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return "<Class>" + ClassName + "</Class><Service>" + ServiceName + "</Service>";
		}
		public string ToString(string className)
		{
			return "<Class>" + className + "</Class><Service>" + ServiceName + "</Service>";
		}
		
		public string ToStringPartial()
		{
			return "_" + ServiceName + "_Partial";
		}
		#endregion
	}
}

