// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// InstanceNodeSelectedEventArgs class.
	/// </summary>
	public class InstanceNodeSelectedEventArgs : SelectedInstanceChangedEventArgs
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

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'InstanceNodeSelectedEventArgs' class.
		/// </summary>
		/// <param name="selectedInstances">Selected instances Oid list.</param>
		/// <param name="nodeId">Node identifier.</param>
		/// <param name="completeOidPath">Oids list and its Ids of the complete path for a node.</param>
		public InstanceNodeSelectedEventArgs(List<Oid> selectedInstances, string nodeId, List<KeyValuePair<string, Oid>> completeOidPath)
			:base(selectedInstances)
		{
			mNodeId = nodeId;
			mCompleteOidPath = completeOidPath;
		}
		#endregion Constructors

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

	}
}


