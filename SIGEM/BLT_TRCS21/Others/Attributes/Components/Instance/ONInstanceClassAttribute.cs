// 3.4.4.5

using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using SIGEM.Business.Attributes;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// ONInstanceClassAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	internal class ONInstanceClassAttribute: Attribute, IONAttribute
	{
		#region Members
		public string ClassName;
		public string RootClassName;
		public string ParentClassName;
		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="className">Name of the class</param>
		public ONInstanceClassAttribute(string className)
		{
			ClassName = className;
		}
		#endregion
	}
}

