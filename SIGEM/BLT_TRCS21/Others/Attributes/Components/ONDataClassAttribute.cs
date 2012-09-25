// 3.4.4.5

using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// ONDataClassAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	internal class ONDataClassAttribute : Attribute
	{
		#region Members
		public string ClassName;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		public ONDataClassAttribute(string className)
		{
			ClassName = className;
		}
		#endregion
	}
}

