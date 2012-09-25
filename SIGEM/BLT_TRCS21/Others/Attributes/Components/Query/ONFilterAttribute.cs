// 3.4.4.5

using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using SIGEM.Business.Types;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// Attribute for the filters.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	internal class ONFilterAttribute : Attribute, IONAttribute
	{
		#region Attributes
		public string ClassName;
		public string FilterName;
		#endregion

		#region Constructors
		public ONFilterAttribute(string className, string filterName)
		{
			ClassName = className;
			FilterName = filterName;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return "<Class>" + ClassName + "</Class><Filter>" + FilterName + "</Filter>";
		}
		#endregion
	}
}

