// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

using SIGEM.Client.Adaptor.DataFormats;

namespace SIGEM.Client.Adaptor
{
	#region <Request>.
	/// <summary>
	/// Class Request stores about service and query request.
	/// </summary>
	internal class Request
	{
		/// <summary>
		/// Initializes a new empty instance of Request.
		/// </summary>
		public Request() { }
		/// <summary>
		/// Initializes a new instance of Request.
		/// </summary>
		/// <param name="agent">Agent.</param>
		public Request(Oids.Oid agent)
		{
			this.Agent = agent as Oids.AgentInfo;
		}
		/// <summary>
		/// Initializes a new instance of Request.
		/// </summary>
		/// <param name="agent">Agent.</param>
		public Request(Oids.AgentInfo agent)
		{
			this.Agent = agent;
		}
		/// <summary>
		/// Initializes a new instance of Request.
		/// </summary>
		/// <param name="service">Service.</param>
		public Request(ServiceRequest service)
			: this(service, (Oids.Oid)null) { }
		/// <summary>
		/// Initializes a new instance of Request.
		/// </summary>
		/// <param name="service">Service.</param>
		/// <param name="agent">Agent.</param>
		public Request(ServiceRequest service, Oids.Oid agent)
			:this(agent)
		{
			this.ServerRequest = service;
		}
		/// <summary>
		/// Initializes a new instance of Request.
		/// </summary>
		/// <param name="query">Query.</param>
		public Request(QueryRequest query)
			: this(query, (Oids.Oid)null) { }
		/// <summary>
		/// Initializes a new instance of Request.
		/// </summary>
		/// <param name="query">Query.</param>
		/// <param name="agent">Agent.</param>
		public Request(QueryRequest query, Oids.Oid agent)
			:this(agent)
		{
			this.ServerRequest = query;
		}

		#region Attributes <Request Application ConnectionString Sequence VerstionDTD>.
		#region Application =
		private string mApplication = string.Empty;
		/// <summary>
		/// Gets or sets Application name.
		/// </summary>
		public string Application
		{
			get
			{
				return mApplication;
			}
			set
			{
				mApplication = value;
			}
		}
		#endregion Application =

		#region ConnectionString =
		private string mConnectionString = string.Empty;
		/// <summary>
		/// Gets or sets connection string.
		/// </summary>
		public string ConnectionString
		{
			get
			{
				return mConnectionString;
			}
			set
			{
				mConnectionString = value;
			}
		}
		#endregion ConnectionString =

		#region Sequence =
		private int mSequence = 0;
		/// <summary>
		/// Gets or sets sequence.
		/// </summary>
		public int Sequence
		{
			get
			{
				return mSequence;
			}
			set
			{
				mSequence = value;
			}
		}
		#endregion Sequence =

		#region VersionDTD =
		private string mVersionDTD = string.Empty;
		/// <summary>
		/// Gets or sets version of DTD.
		/// </summary>
		public string VersionDTD
		{
			get
			{
				return mVersionDTD;
			}
			set
			{
				mVersionDTD = value;
			}
		}
		#endregion VersionDTD =
		#endregion Attributes.

		#region <Ticket>
		private string mTicket = string.Empty;
		/// <summary>
		/// Gets or sets ticket.
		/// </summary>
		public string Ticket
		{
			get
			{
				return mTicket;
			}
			set
			{
				mTicket = value;
			}
		}
		#endregion <Ticket>

		#region <Agent>
		private Oids.AgentInfo mAgent = null;
		/// <summary>
		/// Gets or sets agent.
		/// </summary>
		internal Oids.AgentInfo Agent
		{
			get
			{
				return mAgent;
			}
			set
			{
				mAgent = value;
			}
		}
		#endregion <Agent>

		#region <Service.Request> Or <Query.Request>.
		private object mServerRequest = null;
		/// <summary>
		/// Gets or sets server request.
		/// </summary>
		public object ServerRequest
		{
			get
			{
				return mServerRequest;
			}
			set
			{
				mServerRequest = value;
			}
		}
		/// <summary>
		/// Gets or sets service.
		/// </summary>
		public ServiceRequest Service
		{
			get
			{
				return this.ServerRequest as ServiceRequest;
			}
		}
		/// <summary>
		/// Gets or sets query.
		/// </summary>
		public QueryRequest Query
		{
			get
			{
				return this.ServerRequest as QueryRequest;
			}
		}
		/// <summary>
		/// Determines if it's service.
		/// </summary>
		public bool IsService
		{
			get
			{
				return (Service != null ? true : false);
			}
		}
		/// <summary>
		/// Determines if it's query.
		/// </summary>
		public bool IsQuery
		{
			get
			{
				return (Query != null ? true : false);
			}
		}
		#endregion <Service.Request> Or <Query.Request>
	}
	#endregion <Request>
}

