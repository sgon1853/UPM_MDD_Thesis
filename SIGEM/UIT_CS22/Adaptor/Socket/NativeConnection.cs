// v3.8.4.5.b

using System;
using System.Reflection;
using System.Security;

namespace SIGEM.Client.Adaptor.Connection
 {
	/// <summary>
	/// Manages the Native connection.
	/// </summary>
	internal class NativeConnection : AbstractConnection
	{
		#region Variables
		Type lObject;
		Assembly lAssembly;
		Object csServer;
		MethodInfo lMethod;
		#endregion Variables

		/// <summary>
		/// Create a new instance of HTTPConnection.
		/// </summary>
		/// <param name="connectionString">The native connection string.</param>
		public NativeConnection(string connectionString)
		{
			String l_sNameSpace;
			String l_sServer;

			try
			{
				// Remove net://
				connectionString = connectionString.Replace("net://","");
				// Server connection.
				l_sServer = connectionString.Substring(0,connectionString.IndexOf(":"));
				// Get the namespace.
				l_sNameSpace =connectionString.Substring(connectionString.IndexOf(":")+1,connectionString.Length-connectionString.IndexOf(":")-1);

				// Load the assembly.
				lAssembly = Assembly.Load(l_sServer);
				// Get the XML_Listener
				lObject = lAssembly.GetType(l_sNameSpace + ".XML.XML_Listener");
				// Create the XML_Listener object.
				csServer = Activator.CreateInstance(lObject);
				// Get the method.
				lMethod = lObject.GetMethod("XMLRequest");
			}

			catch (Exception e)
			{
				throw new Exception("Can't connect with server ",e);
			}
		}
		/// <summary>
		/// Sends data to the business logic.
		/// </summary>
		/// <param name="data">Data to send.</param>
		/// <returns>Retrieves a response.</returns>
		public override string Send(string data)
		{	// TODO: Ziping/Unziping?  ...
			try
			{
				String res = (String)lMethod.Invoke(csServer,new object[]{data});
				return (res);
			}
			catch (Exception e)
			{
				throw new Exception("Can't connect with server ",e);
			}
		}
	}
}

