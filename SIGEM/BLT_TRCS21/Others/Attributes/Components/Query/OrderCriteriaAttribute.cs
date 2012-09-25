// 3.4.4.5

using System;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// Attribute for the OrderCriteria.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	internal class ONOrderCriteriaAttribute : Attribute, IONAttribute
	{
		#region Attributes
		public string ClassName;
		public string OrderCriteria;
		#endregion

		#region Gets / Sets
		#endregion

		#region Constructors
		public ONOrderCriteriaAttribute(string className, string orderCriteria)
		{
			ClassName = className;
			OrderCriteria = orderCriteria;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return "<OrderCriteria>" + "<Class>" + ClassName + "</Class>" + OrderCriteria + "</OrderCriteria>";
		}
		#endregion
	}
}

