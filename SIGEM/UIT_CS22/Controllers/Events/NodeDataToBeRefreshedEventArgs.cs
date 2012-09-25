// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// NodeDataToBeRefreshedEventArgs class.
	/// </summary>
	public class NodeDataToBeRefreshedEventArgs : EventArgs
	{
		#region Members
		/// <summary>
		/// Node data table.
		/// </summary>
		private DataTable mData;
		/// <summary>
		/// Oid member.
		/// </summary>
		private Oid mOid;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'NodeDataToBeRefreshedEventArgs' class.
		/// </summary>
		/// <param name="lastOid">Node Oid.</param>
		/// <param name="completeOidPath">Node data table.</param>
		public NodeDataToBeRefreshedEventArgs(Oid oid, DataTable data)
		{
			mData = data;
			mOid = oid;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets the node DataTable values.
		/// </summary>
		public DataTable Data
		{
			get
			{
				return mData;
			}
		}
		/// <summary>
		/// Gets the node Oid.
		/// </summary>
		public Oid Oid  
		{
			get
			{
				return mOid;
			}
		}
		#endregion Properties
	}
}

