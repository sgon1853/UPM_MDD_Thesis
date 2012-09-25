// 3.4.4.5

using System;
using SIGEM.Business.Attributes;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// ONServerClassAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	internal class ONServerClassAttribute: Attribute
	{
		#region Members
		public string ClassName;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="className">Name of the class</param>
		public ONServerClassAttribute(string className)
		{
			ClassName = className;
		}
		#endregion
	}
}

