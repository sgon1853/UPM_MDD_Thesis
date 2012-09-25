// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Stores information related to RefreshRequiredMaster event.
	/// </summary>
	public class RefreshRequiredMasterEventArgs: RefreshRequiredEventArgs
	{
		#region Members
		/// <summary>
		/// Indicates if the Master has been succesfully refreshed
		/// </summary>
		private bool mRefreshDone;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'RefreshRequiredMasterEventArgs'.
		/// Used to request a refresh from the contained elements, an action item for example.
		/// </summary>
		public RefreshRequiredMasterEventArgs(ExchangeInfo receivedExchangeInfo)
			:base(receivedExchangeInfo)
		{
			RefreshType = RefreshRequiredType.RefreshMaster;
			mRefreshDone = false;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Indicates if the Master has been succesfully refreshed
		/// </summary>
		public bool RefreshDone
		{
			get
			{
				return mRefreshDone;
			}
			set
			{
				mRefreshDone = value;
			}
		}
		#endregion Properties
	}
}


