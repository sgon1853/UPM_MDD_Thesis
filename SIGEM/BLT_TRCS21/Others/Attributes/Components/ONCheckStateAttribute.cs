// 3.4.4.5
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Business.Attributes
{
    /// <summary>
	/// Attribute for the Preconditions.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	internal class ONCheckStateAttribute: Attribute, IONAttribute
	{
		#region Members
		public string ClassName;
		public string ServiceName;
		#endregion

		#region Constructors
		public ONCheckStateAttribute(string className, string serviceName)
		{
			ClassName = className;
			ServiceName = serviceName;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return "<Class>" + ClassName + "</Class><Service>" + ServiceName + "</Service>";
		}
		#endregion
	}
}
