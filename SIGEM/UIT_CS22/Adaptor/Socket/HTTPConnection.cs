// v3.8.4.5.b

using System;
using System.Reflection;
using System.Net;
using System.IO;
using System.Text;

namespace SIGEM.Client.Adaptor.Connection
{
	/// <summary>
	/// Manages the HTTP connection.
	/// </summary>
	internal class HTTPConnection : AbstractConnection
	{
		#region VARIABLES
		/// <summary>
		/// Connection String.
		/// </summary>
		String lConnection;

		/// <summary>
		/// Request object.
		/// </summary>
		HttpWebRequest lRequest;

		/// <summary>
		/// Writing buffer.
		/// </summary>
		Stream lWriteStream;

		/// <summary>
		/// Data to send.
		/// </summary>
		UTF8Encoding lEncoding;
		byte[] lBytes;

		/// <summary>
		/// Response object.
		/// </summary>
		HttpWebResponse lResponse;

		/// <summary>
		/// Response buffer.
		/// </summary>
		Stream lResponseStream;
		StreamReader lReadStream;
		#endregion

		/// <summary>
		/// Creates a new instance of HTTPConnection.
		/// </summary>
		/// <param name="connectionString">The HTTP connection string</param>
		public HTTPConnection(string connectionString)
		{
			lConnection = connectionString;
		}
		/// <summary>
		/// Sends data to the business logic.
		/// </summary>
		/// <param name="data">Data to send.</param>
		/// <returns>Retrieves a response.</returns>
		public override string Send(string data)
		{
			try {
				// Request value.
				string lReturnedValue;

				// Create the request.
				lRequest = (HttpWebRequest) WebRequest.Create(lConnection);
				// Unlimited timeout.
				lRequest.Timeout = -1;
				// POST
				lRequest.Method = "POST";
				// Write buffer.
				lWriteStream = lRequest.GetRequestStream();
				// Data to send.
				lEncoding = new UTF8Encoding();
				lBytes = lEncoding.GetBytes(data);
				// Send data.
				lWriteStream.Write(lBytes, 0, lBytes.Length);
				// Close the write buffer.
				lWriteStream.Close();
				// Close the request.
				lResponse = (HttpWebResponse) lRequest.GetResponse();
				// Get the answer.
				lResponseStream = lResponse.GetResponseStream();
				// Read the answer.
				lReadStream = new StreamReader (lResponseStream, Encoding.UTF8);
				lReturnedValue = lReadStream.ReadToEnd();

				return lReturnedValue;
			}
			catch ( Exception e)
			{
				throw new Exception("Can't connect with server ",e);
			}
		}
	}
}

