// 3.4.4.5

using System;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// Attribute for the STD.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	internal class ONSTDAttribute: Attribute, IONAttribute
	{
		#region Members
		public string ClassName;
		public string ServiceName;
		#endregion

		#region Constructors
		public ONSTDAttribute(string className, string serviceName)
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

