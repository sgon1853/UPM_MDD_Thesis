// 3.4.4.5

using System;
using System.Xml;
using System.Collections;
using System.Reflection;
using SIGEM.Business;
using SIGEM.Business.Attributes;
using SIGEM.Business.OID;
using SIGEM.Business.Instance;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business.Server
{
	/// <summary>
	/// Superclass of Server
	/// </summary>
	internal abstract class ONServer: ContextBoundObject, IDisposable
	{
		#region Members
		private string mClassName;
		private ONServiceContext mOnContext;
		private ONInstance mInstance;
		#endregion

		#region Properties
		/// <summary>
		/// Class name of the Action class
		/// </summary>
		public string ClassName
		{
			get
			{
				return mClassName;
			}
			set
			{
				mClassName = value;
			}
		}
		/// <summary>
		/// Represents THIS object
		/// </summary>
		public ONInstance Instance
		{
			get
			{
				return mInstance;
			}
			set
			{
				mInstance = value;
			}
		}
		/// <summary>
		/// Context necessary to execute a service
		/// </summary>
		public ONServiceContext OnContext
		{
			get
			{
				return mOnContext;
			}
			set
			{
				mOnContext = value;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="onContext">Context of the execution of the request</param>
		/// <param name="instance">THIS Instance</param>
		/// <param name="className">Name of the class that has the services</param>
		public ONServer(ONServiceContext onContext, ONInstance instance, string className)
		{
			ClassName = className;
			OnContext = onContext;
			Instance = instance;
		}
		#endregion

		#region IDisposable Methods
		/// <summary>
		/// Destroy all the resources that are associated with the object
		/// </summary>
		public void Dispose()
		{
			if (OnContext != null)
			{
				OnContext.Dispose();
				OnContext = null;
			}
		}
		#endregion
	}
}

