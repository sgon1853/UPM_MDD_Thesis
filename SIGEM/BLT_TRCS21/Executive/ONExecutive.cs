// 3.4.4.5

using System;
using System.Collections;
using System.EnterpriseServices;
using SIGEM.Business;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Attributes;
using SIGEM.Business.Data;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Executive
{
	/// <summary>
	/// Superclass of Executives
	/// </summary>
	public class ONExecutive : ServicedComponent
	{
		#region Members
		private ONServiceContext mOnContext;
		private ONInstance mInstance;
		#endregion

		#region Properties
		/// <summary>
		/// Represents THIS object
		/// </summary>
		internal ONInstance Instance
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
		internal ONServiceContext OnContext
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
	}
}

