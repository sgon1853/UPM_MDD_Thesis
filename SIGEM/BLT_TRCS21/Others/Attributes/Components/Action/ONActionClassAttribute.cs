// 3.4.4.5

using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// Represents the attribute for the Action Class.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	internal class ONActionClassAttribute: Attribute
	{
		#region Members
		//Variable that has the value of the class 
		public string ClassName;
		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="className">Name of the class</param>
		public ONActionClassAttribute(string className)
		{
			ClassName = className;
		}
		#endregion
	}
}

