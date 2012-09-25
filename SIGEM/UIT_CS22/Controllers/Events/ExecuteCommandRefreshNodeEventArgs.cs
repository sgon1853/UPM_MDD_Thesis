// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Base class, event arguments for ExecuteCommand event.
	/// </summary>
	public class ExecuteCommandRefreshNodeEventArgs: ExecuteCommandEventArgs
	{
		#region Members
		/// <summary>
		/// Node identifier.
		/// </summary>
		private string mNodeId;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ExecuteCommandRefreshNodeEventArgs' class.
		/// </summary>
		/// <param name="nodeId">Node identifier.</param>
		public ExecuteCommandRefreshNodeEventArgs(string nodeId)
			: base(ExecuteCommandType.ExecuteRefreshNode)
		{
			mNodeId = nodeId;
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
		#endregion Properties
	}
}

