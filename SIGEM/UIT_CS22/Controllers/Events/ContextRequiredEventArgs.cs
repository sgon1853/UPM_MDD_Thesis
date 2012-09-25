// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Stores information related to Service Result event.
	/// </summary>
	public class ContextRequiredEventArgs : EventArgs
	{
		#region Members
		/// <summary>
		/// Context information.
		/// </summary>
		private IUContext mContext;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ContextRequiredEventArgs' class.
		/// </summary>
		public ContextRequiredEventArgs()
		{
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or Sets the Context information.
		/// </summary>
		public IUContext Context
		{
			get
			{
				return mContext;
			}
			set
			{
				mContext = value;
			}
		}
		#endregion Properties
	}
}



