// v3.8.4.5.b
using System;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Stores information related to Instance Modified event.
	/// </summary>
	public class InstanceModifiedEventArgs
	{
		#region Members
		/// <summary>
		/// Indicates the Oid of the modified instance.
		/// </summary>
		public Oid mOid;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'InstanceModifiedEventArgs'.
		/// </summary>
		/// <param name="oid">Oid of the modified instance.</param>
		public InstanceModifiedEventArgs(Oid oid)
		{
			mOid = oid;
		}
		#endregion Constructors
	}
}


