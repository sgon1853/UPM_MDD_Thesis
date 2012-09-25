// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// ShowDataInSubNodeEventArgs class.
	/// </summary>
	public class ShowDataInSubNodeEventArgs: EventArgs
	{
		#region Members
		/// <summary>
		/// Node data table.
		/// </summary>
		private DataTable mData;
		/// <summary>
		/// Node identifier.
		/// </summary>
		private string mNodeId;
		#endregion Members

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the 'ShowDataInSubNodeEventArgs' class.
		/// </summary>
		/// <param name="nodeId">Node identifier.</param>
		/// <param name="data">Node data table.</param>
		public ShowDataInSubNodeEventArgs(string nodeId, DataTable data)
		{
			mData = data;
			mNodeId = nodeId;
		}
		#endregion Constructor

		#region Properties
		/// <summary>
		/// Gets or Sets the node DataTable values.
		/// </summary>
		public DataTable Data
		{
			get
			{
				return mData;
			}
			set
			{
				mData = value;
			}
		}
		/// <summary>
		/// Gets or Sets the node identifier.
		/// </summary>
		public string NodeId
		{
			get
			{
				return mNodeId;
			}
			set
			{
				mNodeId = value;
			}
		}
		#endregion Properties
	}
}


