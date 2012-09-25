// v3.8.4.5.b
using System;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Stores information related to Instance Deleted event.
	/// </summary>
	public class InstanceDeletedEventArgs
	{		
		#region Members
		/// <summary>
		/// Indicates the Oid of the deleted instance.
		/// </summary>
		public Oid mOid;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'InstanceDeletedEventArgs'.
		/// </summary>
		/// <param name="oid">Oid of the deleted instance.</param>
		public InstanceDeletedEventArgs(Oid oid)
		{
			mOid = oid;
		}
		#endregion Constructors
	}
}


