// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Data;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// FinalNodeSelectedEventArgs class.
	/// </summary>
	public class FinalNodeSelectedEventArgs : EventArgs
	{
		#region Members
		/// <summary>
		/// Parent node Oid.
		/// </summary>
		private Oid mParentNodeOid = null;
		/// <summary>
		/// Parent node identifier.
		/// </summary>
		private string mParentNodeId = "";
		/// <summary>
		/// Oids list and its Id of the complete path for a node.
		/// </summary>
		private List<KeyValuePair<string, Oid>> mCompleteOidPath;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'FinalNodeSelectedEventArgs' class.
		/// </summary>
		/// <param name="nodeId">Node identifier.</param>
		/// <param name="parentNodeOid">Parent node Oid.</param>
		/// <param name="completeOidPath">Oids list and its Ids of the complete path for a node.</param>
		public FinalNodeSelectedEventArgs(string nodeId, Oid parentNodeOid, List<KeyValuePair<string, Oid>> completeOidPath)
		{
			mParentNodeId = nodeId;
			mParentNodeOid = parentNodeOid;
			mCompleteOidPath = completeOidPath;
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
		/// Gets the parent node identifier.
		/// </summary>
		public string ParentNodeId
		{
			get
			{
				return mParentNodeId;
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
		#endregion Properties
	}
}

