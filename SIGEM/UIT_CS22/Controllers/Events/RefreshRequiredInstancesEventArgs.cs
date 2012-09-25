// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Stores information related to Refresh event.
	/// </summary>
	public class RefreshRequiredInstancesEventArgs: RefreshRequiredEventArgs
	{
		#region Members
		/// <summary>
		/// Instances to be refreshed
		/// </summary>
		private List<Oid> mInstances;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'RefreshRequiredInstancesEventArgs'.
		/// Used to request a refresh from the contained elements, an action item for example.
		/// </summary>
		public RefreshRequiredInstancesEventArgs(List<Oid> instances, ExchangeInfo receivedExchangeInfo)
			:base(receivedExchangeInfo)
		{
			RefreshType = RefreshRequiredType.RefreshInstances;
			mInstances = instances;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Instances to be refreshed
		/// </summary>
		public List<Oid> Instances
		{
			get
			{
				return mInstances;
			}
			set
			{
				mInstances = value;
			}
		}
		#endregion Properties
	}
}


