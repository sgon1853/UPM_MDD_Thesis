// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Stores information related to Service Result event.
	/// </summary>
	public class ServiceResultEventArgs : EventArgs
	{
		/// <summary>
		/// Indicates if the service has success or not.
		/// </summary>
		public bool Success;
		/// <summary>
		/// The received Exchange information in the Service
		/// </summary>
		public ExchangeInfo ExchangeInfoReceived;
		/// Initializes a new instance of 'ServiceResultEventArgs'.
		/// </summary>
		/// <param name="success">Indicates if the service has success or not.</param>
		public ServiceResultEventArgs(bool success, ExchangeInfo exchangeInfoReceived)
			: base()
		{
			Success = success;
			ExchangeInfoReceived = exchangeInfoReceived;
		}
	}
}

