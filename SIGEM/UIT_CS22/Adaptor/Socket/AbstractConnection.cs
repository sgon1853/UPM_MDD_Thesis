// v3.8.4.5.b
using System;
using System.Reflection;

namespace SIGEM.Client.Adaptor.Connection
{

	/// <summary>
	/// Class that manages the business logic connection methods.
	/// </summary>
	/// This class encapsulates the different methods used to communicate with the
	/// business logic.
	public abstract class AbstractConnection
	{
		/// <summary>
		/// Creates a new instance of the 'AbstractConnection' class.
		/// </summary>
		public AbstractConnection()
		{
		}
		/// <summary>
		/// Creates a server connection to be able to communicate wiht business logic.
		/// </summary>
		/// <param name="connectionString">Server connection string.</param>
		/// <returns>AbstractConnection class.</returns>
		public static AbstractConnection CreateServerConnection(string connectionString)
		{
			//Analyze connection string and return the corresponding subclass
			if ((connectionString == null) || (connectionString == string.Empty))
			{
				return null;
			}
			else if (connectionString.StartsWith("com://"))
			{
				// ActiveX connection
				// String connection format for COM protocol -> com://server_name
				return new ActiveXConnection(connectionString);
			}
			else if (connectionString.StartsWith("net://"))
			{
				// Native connection
				// String connection format for Native .NET -> net://server_name:server_namespace
				return new NativeConnection(connectionString);
			}
			else if (connectionString.StartsWith("http://") == true && connectionString.EndsWith(".wsdl") == false)
			{
				// HTTP connection.
				// String connection format for HTTP -> http://host:port/server_name/invxml
				return new HTTPConnection(connectionString);
			}
			/*else if ( connectionString.StartsWith("http://") == true && connectionString.EndsWith(".wsdl") == true )
			{
				// SOAPConnection
				return new SOAPConnection(connectionString);
			}*/
			else
			{
				// Default connection method (Native .NET).
				return new NativeConnection(connectionString);
			}
		}

		/// <summary>
		/// Sends data to the business logic.
		/// </summary>
		/// <param name="data">Data to send.</param>
		/// <returns>Retrieves a response or an exception if an error occurs.</returns>
		public abstract string Send(string data);//Sends a request to a bussines server and retrieves a response	(or an exception if an error ocurrs)
	}

}

