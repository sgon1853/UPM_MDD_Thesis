// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Data;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// GetNextNodeDataBlockEventArgs class.
	/// </summary>
	public class GetNextNodeDataBlockEventArgs : EventArgs
	{
		#region Members
		/// <summary>
		/// Node identifier.
		/// </summary>
		private string mNodeId = "";
		/// <summary>
		/// Node data table.
		/// </summary>
		private DataTable mData;
		/// <summary>
		/// Flag to determine if it is the last data block.
		/// </summary>
		private bool mLastBlock;
		/// <summary>
		/// Last Oid.
		/// </summary>
		private Oid mLastOid;
		/// <summary>
		/// Oids list and its Id of the complete path for a node.
		/// </summary>
		private List<KeyValuePair<string, Oid>> mCompleteOidPath;
		/// <summary>
		/// Represents the information of the Display Set that is showed in the current Node.
		/// </summary>
		private DisplaySetInformation mDisplaySetInfo;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'GetNextNodeDataBlockEventArgs' class.
		/// </summary>
		/// <param name="nodeId">Node identifier.</param>
		/// <param name="lastOid">Last Oid.</param>
		/// <param name="completeOidPath">Oids list and its Ids of the complete path for a node.</param>
		public GetNextNodeDataBlockEventArgs(string nodeId, Oid lastOid, List<KeyValuePair<string, Oid>> completeOidPath)
		{
			NodeId = nodeId;
			mLastOid = lastOid;
			mCompleteOidPath = completeOidPath;
			mLastBlock = true;
		}
		#endregion Constructors

		#region Properties
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
		/// <summary>
		/// Gets or Sets the node data table values.
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
		/// Gets or Sets the LastBlock value.
		/// </summary>
		public bool LastBlock
		{
			get
			{
				return mLastBlock;
			}
			set
			{
				mLastBlock = value;
			}
		}
		/// <summary>
		/// Gets the last Oid.
		/// </summary>
		public Oid LastOid
		{
			get
			{
				return mLastOid;
			}
		}
		/// <summary>
		/// Gets the Oids and Ids complete path list.
		/// </summary>
		public List<KeyValuePair<string, Oid>> CompleteOidPath
		{
			get
			{
				return mCompleteOidPath;
			}
		}
		/// <summary>
		/// Represents the information of the Display Set that is showed in the current Node.
		/// </summary>
		public DisplaySetInformation DisplaySetInfo
		{
			get
			{
				return mDisplaySetInfo;
			}
			set
			{
				mDisplaySetInfo = value;
			}
		}
		#endregion Properties
	}
}

