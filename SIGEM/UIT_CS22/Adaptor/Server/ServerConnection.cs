// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SIGEM.Client.Adaptor.Exceptions;
using SIGEM.Client.Adaptor.Connection;
using SIGEM.Client.Adaptor.DataFormats;
using SIGEM.Client.Adaptor.Serializer;

namespace SIGEM.Client.Adaptor
{
	#region ServerConnection.
	/// <summary>
	/// Allows server connection management.
	/// </summary>
	public partial class ServerConnection : IDisposable
	{

		public const string VALIDATION_SERVICE_NAME = "MVAgentValidation";
		public const string CHANGE_PWD_SERVICE_NAME = "MVChangePassWord";

		#region Constructors
		/// <summary>
		/// Creates a new server connection with an empty connection string.
		/// </summary>
		public ServerConnection() : this(string.Empty) { }
		/// <summary>
		/// Creates a new server connection.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		public ServerConnection(string connectionString)
		{
			this.ConnectionString = connectionString;
		}
		#endregion Constructors

		#region Destructors
		/// <summary>
		/// Use C# destructor syntax for finalization code.
		/// This destructor will run only if the Dispose method
		/// does not get called.
		/// It gives your base class the opportunity to finalize.
		/// Do not provide destructors in types derived from this class.
		/// </summary>
		~ServerConnection()
		{
			// Do not re-create Dispose clean-up code here.
			// Calling Dispose(false) is optimal in terms of
			// readability and maintainability.
			Dispose(false);
		}
		#endregion Destructors

		#region IDisposable Members

		// Implement IDisposable.
		// Do not make this method virtual.
		// A derived class should not be able to override this method.
		/// <summary>
		/// This object will be cleaned up by the Dispose method.
		/// Therefore, this object will be taken off the finalization queue
		/// and prevent finalization code for this object
		/// from executing a second time.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			// This object will be cleaned up by the Dispose method.
			// Therefore, you should call GC.SupressFinalize to
			// take this object off the finalization queue
			// and prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}
		private bool disposed = false;
		/// <summary>
		/// Dispose(bool disposing) executes in two distinct scenarios.
		/// If disposing equals true, the method has been called directly
		/// or indirectly by a user's code. Managed and unmanaged resources
		/// can be disposed.
		/// If disposing equals false, the method has been called by the
		/// runtime from inside the finalizer and you should not reference
		/// other objects. Only unmanaged resources can be disposed.
		/// </summary>
		/// <param name="disposing">Disposing condition.</param>
		public virtual void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if (!this.disposed)
			{
				// If disposing equals true, dispose all managed
				// and unmanaged resources.
				if (disposing)
				{
					// Dispose managed resources.
					// Sample: -> component.Dispose();
				}

				this.Close();

				// Call the appropriate methods to clean up
				// unmanaged resources here.
				// If disposing is false, only the following
				// code is executed.
			}
			disposed = true;
		}
		#endregion

		#region XML Serializers
		private XMLErrorSerializer mXMLError = new XMLErrorSerializer();
		/// <summary>
		/// Gets the serialized error from an XML.
		/// </summary>
		private XMLErrorSerializer XMLError { get { return mXMLError; } }
		#endregion XML Serializers

		#region Connection

		#region Connection Object
		private AbstractConnection mConnection = null;
		/// <summary>
		/// Gets the connection object.
		/// </summary>
		protected AbstractConnection Connection { get { return mConnection; } }
		#endregion Connection Object

		#region Connection String
		private string mConnectionString = null;
		/// <summary>
		/// Gets or sets the connection string.
		/// </summary>
		public string ConnectionString
		{
			get
			{
				return mConnectionString;
			}
			set
			{
				if (!this.IsOpen())
				{
					mConnectionString = value;
				}
			}
		}
		#endregion Connection String

		#region Get the Name of Server
		/// <summary>
		/// Gets the server name.
		/// </summary>
		/// <returns>String.</returns>
		protected string GetServerName()
		{
			string[] lvalues2 = this.ConnectionString.Split(':');
			if (lvalues2.Length > 1)
			{
				this.Protocol = lvalues2[0];

				lvalues2[1] = lvalues2[1].Substring(2);
				if (lvalues2.Length == 3)
				{
					this.Application = lvalues2[2];
				}
				else
				{
					this.Application = lvalues2[1];
				}
			}
			else
			{
				//throw;
			}

			return this.Application;
		}
		#endregion Get the Name of Server

		#region Open Connection
		/// <summary>
		/// Indicates the connection string and initiates the connection with the server.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <returns>True if success, false if fail.</returns>
		public bool Open(string connectionString)
		{
			this.ConnectionString = connectionString;
			return Open();
		}
		/// <summary>
		/// Initiates the connection with de server.
		/// </summary>
		/// <returns>True if success, false if fail.</returns>
		public bool Open()
		{
			bool lRslt = false;
			if (!this.IsOpen())
			{
				// Open channel.
				mConnection = AbstractConnection.CreateServerConnection(this.ConnectionString);
				if (this.Connection != null)
				{
					lRslt = true;
				}

				this.Application = this.GetServerName();

				this.VersionDTD = "CARE.Request v. 4.00";
			}
			return lRslt;
		}
		#endregion Open Connection

		#region Close Connection
		/// <summary>
		/// Closes the connection
		/// </summary>
		public void Close()
		{
			mConnection = null;
			this.ConnectionString = string.Empty;
			this.Application = string.Empty;
			this.Protocol = string.Empty;
			this.VersionDTD = string.Empty;
		}
		#endregion Close Connection

		#region Is the connection Open
		/// <summary>
		/// Checks if the connection is open.
		/// </summary>
		/// <returns>True if connected, false if disconnected.</returns>
		public bool IsOpen()
		{
			return this.Connection == null ? false : true;
		}
		#endregion Is the connection Open

		#endregion Connection

		#region State of Comunication

		#region Members
		private string Protocol = string.Empty;
		private string Application = string.Empty;
		private string VersionDTD = string.Empty;
		#endregion Members

		#region Check the request data
		/// <summary>
		/// Checks and complete the request.
		/// </summary>
		/// <param name="request">Request to check.</param>
		/// <returns>Request checked.</returns>
		private Request CheckRequest(Request request)
		{
			Request lResult = null;

			// Get the sequence and ticket from the connected agent.
			if (request.Agent != null)
			{
				// Sequence.
				request.Sequence = request.Agent.Sequence;
				request.Agent.Sequence++;

				// Ticket.
				request.Ticket = request.Agent.Ticket;	
			}
			else
			{
				// Exception: 'MVAgentValidation' service does not need Agent.
				if ((request.IsQuery) || ((request.IsService) && (request.Service.Name != VALIDATION_SERVICE_NAME)))
				{
					string lMessage = CultureManager.TranslateString(LanguageConstantKeys.L_LOGIN_ERROR, LanguageConstantValues.L_LOGIN_ERROR);
					throw new RequestException(lMessage);
				}
			}

			// Application.
			if ((request.Application == null) || request.Application.Length == 0)
			{
				request.Application = this.Application;
			}

			// VersionDTD.
			if ((request.VersionDTD == null) || request.VersionDTD.Length == 0)
			{
				request.VersionDTD = this.VersionDTD;
			}

			// ConnectionString.
			request.ConnectionString = this.ConnectionString;
			if ((request.ConnectionString == null) || request.ConnectionString.Length == 0)
			{
				request.ConnectionString = this.ConnectionString;
			}

			lResult = request;
			return lResult;
		}
		#endregion Check the request data

		#endregion State of Comunication

		#region Send and Receive data
		/// <summary>
		/// Sends and receives data.
		/// </summary>
		/// <param name="request">Data to send.</param>
		/// <returns>Data received.</returns>
		private Response Send(Request request)
		{
			// Hourglass
			System.Windows.Forms.Cursor lCurrentCursor = System.Windows.Forms.Cursor.Current;
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

			Response lResult = null;
			if ((this.Connection != null) && (request != null))
			{
				lResult = new Response();

				// Check the info. of Request.
				CheckRequest(request);

				// Data To Send ...
				StringBuilder lSendData = XMLRequestSerializer.SerializeRoot(null as StringBuilder, request);

				// Send & Received Data ...
				StringBuilder lReceivedData = new StringBuilder(this.Connection.Send(lSendData.ToString()));

				// Throw a ResponseException if have error.
				lResult = XMLResponseSerializer.Deserialize(lReceivedData.ToString());

				// Save Ticket ...
				if (lResult.Ticket != null)
				{
					request.Agent.Ticket = lResult.Ticket;
				}
			}
			else
			{
				// Hourglass
				System.Windows.Forms.Cursor.Current = lCurrentCursor;

				string lMessageError = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR_REQUEST, LanguageConstantValues.L_ERROR_REQUEST);
				RequestException lrequestException = new RequestException(lMessageError);
				lrequestException.Number = 999;
				throw lrequestException;
			}

			// Hourglass
			System.Windows.Forms.Cursor.Current = lCurrentCursor;
			
			return lResult;
		}
		#endregion	Send and Receive data
	}
	#endregion ServerConnection
}

