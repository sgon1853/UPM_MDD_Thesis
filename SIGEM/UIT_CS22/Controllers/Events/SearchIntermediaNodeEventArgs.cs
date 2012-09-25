// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Data;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// SearchIntermediaNodeDataEventArgs class.
	/// </summary>
	public class SearchIntermediaNodeDataEventArgs : EventArgs
	{
		#region Members
		/// <summary>
		/// Parent node Oid.
		/// </summary>
		private Oid mParentNodeOid = null;
		/// <summary>
		/// Node identifier.
		/// </summary>
		private string mNodeId = "";
		/// <summary>
		/// Oids list and its Id of the complete path for a node.
		/// </summary>
		private List<KeyValuePair<string, Oid>> mCompleteOidPath;
		/// <summary>
		/// Node data.
		/// </summary>
		private DataTable mData = null;
		/// <summary>
		/// LastBlock flag.
		/// </summary>
		private bool mLastBlock;
		/// <summary>
		/// Represents the information of the Display Set that is showed in the current Node.
		/// </summary>
		private DisplaySetInformation mDisplaySetInfo;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'SearchIntermediaNodeDataEventArgs' class.
		/// </summary>
		/// <param name="parentNodeOid">Parent node Oid.</param>
		/// <param name="nodeId">Node identifier.</param>
		/// <param name="completeOidPath">Oids list and its Ids of the complete path for a node.</param>
		public SearchIntermediaNodeDataEventArgs(Oid parentNodeOid, string nodeId, List<KeyValuePair<string, Oid>> completeOidPath)
		{
			mParentNodeOid = parentNodeOid;
			mNodeId = nodeId;
			mCompleteOidPath = completeOidPath;
			mLastBlock = true;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets the Parent node Oid.
		/// </summary>
		public Oid ParentNodeOid
		{
			get
			{
				return mParentNodeOid;
			}
		}
		/// <summary>
		/// Gets the node identifier.
		/// </summary>
		public string NodeId
		{
			get
			{
				return mNodeId;
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
		/// <summary>
		/// Gets or Sets value to LastBlock flag.
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
		#endregion Properties

	}
}


