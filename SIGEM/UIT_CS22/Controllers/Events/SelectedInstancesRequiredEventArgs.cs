// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// SelectedInstancesRequiredEventArgs class.
	/// </summary>
	public class SelectedInstancesRequiredEventArgs : EventArgs
	{
		#region Members
		/// <summary>
		/// Oid list of selected instances.
		/// </summary>
		private List<Oid> mSelectedInstances;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'SelectedInstancesRequiredEventArgs'.
		/// </summary>
		public SelectedInstancesRequiredEventArgs()
		{
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or Sets the Oid list of selected instances
		/// </summary>
		public List<Oid> SelectedInstances
		{
			get
			{
				return mSelectedInstances;
			}
			set
			{
				mSelectedInstances = value;
			}
		}
		#endregion Properties
	}
}



