// v3.8.4.5.b

using System;
using System.Reflection;

namespace SIGEM.Client.Adaptor.Connection
{
	/// <summary>
	/// Manages the SOAP connection.
	/// </summary>
	internal class SOAPConnection : AbstractConnection
	{
		/// <summary>
		/// Creates a new instance of SOAPConnection.
		/// </summary>
		/// <param name="connectionString">The SOAP connection string</param>
		public SOAPConnection(string connectionString)
		{
			//
			// All: Add Logic Contructor
			//
		}

		/// <summary>
		/// Sends data to the business logic.
		/// </summary>
		/// <param name="data">Data to send.</param>
		/// <returns>Retrieve a response.</returns>
		public override string Send(string data)
		{	// ALL: ...
			return string.Empty;
		}

	}
}

