// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// IntermediaNodeSelectedEventArgs class.
	/// </summary>
	public class IntermediaNodeSelectedEventArgs: EventArgs
	{
		#region Members
		/// <summary>
		/// Node identifier.
		/// </summary>
		private string mNodeId = "";
		/// <summary>
		/// Oids list and its Id of the complete path for a node.
		/// </summary>
		private List<KeyValuePair<string, Oid>> mCompleteOidPath;
		#endregion Members

		#region Properties
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
		#endregion Properties

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the 'IntermediaNodeSelectedEventArgs' class.
		/// </summary>
		/// <param name="nodeId">Node identifier.</param>
		/// <param name="completeOidPath">Oids list and its Ids of the complete path for a node.</param>
		public IntermediaNodeSelectedEventArgs(string nodeId, List<KeyValuePair<string, Oid>> completeOidPath)
		{
			mNodeId = nodeId;
			mCompleteOidPath = completeOidPath;
		}
		#endregion Constructor
	}
}


