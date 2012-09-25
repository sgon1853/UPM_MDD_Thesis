// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the ChangeEnableEventArgs.
	/// </summary>
	public class CheckForPendingChangesEventArgs: EventArgs
	{
		#region Members
		/// <summary>
		/// Cancel the action
		/// </summary>
		private bool mCancel;
		#endregion Members

		#region Properties
		/// <summary>
		/// Cancel the action
		/// </summary>
		public bool Cancel
		{
			get
			{
				return mCancel;
			}
			set
			{
				mCancel = value;
			}
		}
		#endregion Properties

		#region Constructor
		public CheckForPendingChangesEventArgs()
		{
			mCancel = false;
		}
		#endregion Constructor
	}
}




