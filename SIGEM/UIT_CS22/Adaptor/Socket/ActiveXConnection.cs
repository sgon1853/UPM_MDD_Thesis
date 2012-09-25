// v3.8.4.5.b

using System;
using System.Reflection;

namespace SIGEM.Client.Adaptor.Connection
 {
	/// <summary>
	/// Manages the ActiveX connection.
	/// </summary>
	internal class ActiveXConnection : AbstractConnection
	{
		public string COMObject;
		protected Type COMType = null;
		protected object VBServer = null;

		/// <summary>
		/// Creates a new instance of ActiveXConnection
		/// </summary>
		/// <param name="connectionString">The ActiveX connection string</param>
		/**
		 * Searches and loads by reflection an instance of the specified ActiveX object.
		 */
		public ActiveXConnection(string connectionString)
		{
			try
			{
				if ( connectionString.StartsWith("com://") )
				{
					COMObject = connectionString.Replace("com://", "");
				}
				else
				{
					COMObject = connectionString;
				}

				COMType = Type.GetTypeFromProgID(COMObject + ".XML_Listener", true);
				VBServer = Activator.CreateInstance(COMType);
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
		{
			try
			{
				return COMType.InvokeMember("XMLRequest", BindingFlags.InvokeMethod, null, VBServer, new object[] {data}) as string;
			}
			catch (Exception e)
			{
				throw new Exception("Can't connect with server ",e);
			}
		}
	}
}

