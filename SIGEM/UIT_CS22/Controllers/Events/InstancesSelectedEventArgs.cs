// v3.8.4.5.b
using System;
using SIGEM.Client.Logics;
using SIGEM.Client.Oids;
using System.Collections.Generic;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Stores information related to Instances Selected event.
	/// </summary>
	public class InstancesSelectedEventArgs: EventArgs
	{
		#region Members
		/// <summary>
		/// List containing the selected Oids.
		/// </summary>
		public List<Oid> OidList;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'InstancesSelectedEventArgs'.
		/// </summary>
		/// <param name="oidList">List containing the selected Oids.</param>
		public InstancesSelectedEventArgs(List<Oid> oidList)
			:base()
		{
			OidList = oidList;
		}
		#endregion Constructors
	}
}


