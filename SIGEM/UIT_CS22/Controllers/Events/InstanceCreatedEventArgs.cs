// v3.8.4.5.b
using System;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Stores information related to Instance Created event.
	/// </summary>
	public class InstanceCreatedEventArgs
	{
		#region Members
		/// <summary>
		/// Indicates the Oid of the new instance.
		/// </summary>
		public Oid mOid;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'InstanceCreatedEventArgs'.
		/// </summary>
		/// <param name="oid">Oid of the new instance.</param>
		public InstanceCreatedEventArgs(Oid oid)
		{
			mOid = oid;
		}
		#endregion Constructors
	}
}


