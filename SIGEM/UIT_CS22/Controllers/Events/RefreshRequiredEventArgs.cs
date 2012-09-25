// v3.8.4.5.b
using System;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Declares and Initializes RefreshRequiredType enum.
	/// </summary>
	public enum RefreshRequiredType
	{
		/// <summary>
		/// Refresh population
		/// </summary>
		Default = 0,
		/// <summary>
		/// Refresh the specified intances
		/// </summary>
		RefreshInstances = 10,
		/// <summary>
		/// Refresh Master instance
		/// </summary>
		RefreshMaster = 20
	}

	/// <summary>
	/// Stores information related to Refresh event.
	/// </summary>
	public class RefreshRequiredEventArgs: EventArgs
	{
		#region Members
		/// <summary>
		/// Refresh typè
		/// </summary>
		private RefreshRequiredType mRefreshType;
		/// <summary>
		/// Exchange info received
		/// </summary>
		private ExchangeInfo mReceivedExchangeInfo;
        /// <summary>
        /// If true, close the current scenario
        /// </summary>
        private bool mCloseScenario = false;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'RefreshRequiredEventArgs'.
		/// Used to request a refresh from the contained elements, an action item for example.
		/// </summary>
		public RefreshRequiredEventArgs(ExchangeInfo receivedExchangeInfo)
		{
			mRefreshType = RefreshRequiredType.Default;
			mReceivedExchangeInfo = receivedExchangeInfo;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Refresh type
		/// </summary>
		public RefreshRequiredType RefreshType
		{
			get
			{
				return mRefreshType;
			}
			set
			{
				mRefreshType = value;
			}
		}
		/// <summary>
		/// Received exchange info
		/// </summary>
		public ExchangeInfo ReceivedExchangeInfo
		{
			get
			{
				return mReceivedExchangeInfo;
			}
			set
			{
				mReceivedExchangeInfo = value;
			}
		}
        /// <summary>
        /// Close current scenario
        /// </summary>
        public bool CloseScenario
        {
            get
            {
                return mCloseScenario;
            }
            set
            {
                mCloseScenario = value;
            }
        }
		#endregion Properties
	}
}


