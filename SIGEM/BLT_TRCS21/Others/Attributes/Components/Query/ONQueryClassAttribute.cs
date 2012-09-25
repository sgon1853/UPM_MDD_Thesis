// 3.4.4.5

using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using SIGEM.Business.Attributes;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// ONQueryClassAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	internal class ONQueryClassAttribute : Attribute
	{
		#region Members
		public string ClassName;
		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="className">Name of the class</param>
		public ONQueryClassAttribute(string className)
		{
			ClassName = className;
		}
		#endregion
	}
}

