// 3.4.4.5

using System;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// Attribute for the Integrity Constraints.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	internal class ONIntegrityConstraintAttribute: Attribute, IONAttribute
	{
		#region Members
		public string ClassName;
		#endregion

		#region Constructors
		public ONIntegrityConstraintAttribute(string className)
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
