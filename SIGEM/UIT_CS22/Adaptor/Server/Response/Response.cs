// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

using System.Data;
using System.Data.Common;

using SIGEM.Client.Adaptor.Exceptions;
using SIGEM.Client.Adaptor.DataFormats;

namespace SIGEM.Client.Adaptor
{
	#region <Query.Response>.
	internal class QueryResponse
	{
		public QueryResponse() { }

		private string mClassName = string.Empty;
		public string ClassName
		{
			get
			{
				return mClassName;
			}
			set
			{
				mClassName = value;
			}
		}

		private uint mRows = 0;
		public uint Rows
		{
			get
			{
				return mRows;
			}
			set
			{
				mRows = value;
			}
		}

		private bool mLastBlock = false;
		public bool Lastblock
		{
			get
			{
				return mLastBlock;
			}
			set
			{
				mLastBlock = value;
			}
		}

		private DataTable mResult = null;
		public virtual DataTable Data
		{
			get
			{
				return mResult;
			}
			set
			{
				mResult = value;
			}
		}
	}
	#endregion <Query.Response>.

	#region <Response>.
	internal class Response
	{
		public Response() { }

		#region XML -> Attributes.
		private uint mSequence = 0;
		public uint Sequence
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

		private string mVersionDTD = string.Empty;
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
		#endregion XML -> Attributes.

		private string mTicket = string.Empty;
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

		#region <Statistics>
		private Statistics mStatistics = null;
		public Statistics Statistics
		{
			get
			{
				return mStatistics;
			}
			set
			{
				mStatistics = value;
				if (mStatistics != null)
				{
					mTimes = new StatisticsTime(mStatistics);
				}
				else
				{
					mTimes = null;
				}
			}
		}

		private StatisticsTime mTimes = null;
		public StatisticsTime Times
		{
			get
			{
				return mTimes;
			}
		}
		#endregion <Statistics>

		#region <Service.Response> Or <Query.Response>.
		private object mServerResponse = null;
		public object ServerResponse
		{
			get
			{
				return mServerResponse;
			}
			set
			{
				mServerResponse = value;
			}
		}

		public ServiceResponse Service{get{return this.ServerResponse as ServiceResponse;}}
		public bool IsService { get { return (this.Service != null ? true : false );} }

		public QueryResponse Query{get{return this.ServerResponse as QueryResponse;}}
		public bool IsQuery{ get { return (this.Query != null ? true : false); } }

		#endregion <Service.Request> Or <Query.Request>
	}
	#endregion <Response>.
}

