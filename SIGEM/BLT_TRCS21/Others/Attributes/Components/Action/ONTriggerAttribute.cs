// 3.4.4.5

using System;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// Attribute for the Triggers.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	internal class ONTriggerAttribute: Attribute, IONAttribute
	{
		#region Members
		public string ClassName;
		#endregion

		#region Constructors
		public ONTriggerAttribute(string className)
		{
			ClassName = className;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return "<Class>" + ClassName + "</Class>";
		}
		#endregion
	}
}

