// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Stores information related to Refresh event.
	/// </summary>
	public class RefreshNodeRequiredEventArgs: RefreshRequiredEventArgs
	{
		#region Members
		/// <summary>
		/// Node identifier.
		/// </summary>
		private string mNodeId;
		/// <summary>
		/// RefreshRequired event reference.
		/// </summary>
		private RefreshRequiredEventArgs mRefreshRequiredArgs;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'RefreshNodeRequiredEventArgs'.
		/// Used to request a refresh from one Tree node
		/// </summary>
		public RefreshNodeRequiredEventArgs(string nodeId, RefreshRequiredEventArgs refreshRequiredArgs)
			:base(refreshRequiredArgs.ReceivedExchangeInfo)
		{
			RefreshType = refreshRequiredArgs.RefreshType;
			mNodeId = nodeId;
			mRefreshRequiredArgs = refreshRequiredArgs;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// NodeId of the node to be refreshed
		/// </summary>
		public string NodeId
		{
			get
			{
				return mNodeId;
			}
		}
		/// <summary>
		/// Refresh required arguments
		/// </summary>
		public RefreshRequiredEventArgs RefreshRequiredArgs
		{
			get
			{
				return mRefreshRequiredArgs;
			}
		}
		#endregion Properties
	}
}

