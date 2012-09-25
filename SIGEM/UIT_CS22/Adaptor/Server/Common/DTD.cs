// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Adaptor
{
	#region Tags for DTD version 3.10
	/// <summary>
	/// Class that stores the tags information for DTD v3.10.
	/// </summary>
	public static class DTD
	{
		#region Tags for <Error>
		/// <summary>
		/// Class that stores the Error tags for DTD.
		/// </summary>
		public static class Error
		{
			/// <summary>
			/// Error tag.
			/// </summary>
			public const string TagError= "Error";
			/// <summary>
			/// Type tag.
			/// </summary>
			public const string TagType = "Type";
			/// <summary>
			/// Number tag.
			/// </summary>
			public const string TagNumber   = "Number";
			/// <summary>
			/// Error message tag.
			/// </summary>
			public const string TagErrorMessage = "Error.Message";

			#region Tags for <Error.Params>
			/// <summary>
			/// Error Params tag.
			/// </summary>
			public const string TagErrorParams  = "Error.Params";
			/// <summary>
			/// Class that stores the Error Params tags for DTD.
			/// </summary>
			public static class ErrorParams
			{
				/// <summary>
				/// Error Param tag.
				/// </summary>
				public const string TagErrorParam = "Error.Param";
				/// <summary>
				/// Key tag.
				/// </summary>
				public const string TagKey = "Key";
				/// <summary>
				/// Type tag.
				/// </summary>
				public const string TagType= "Type";
			}
			#endregion Tags for <Error.Params>

			#region Tags for <Error.Trace>
			/// <summary>
			/// Error Trace tag.
			/// </summary>
			public const string TagErrorTrace = "Error.Trace";
			/// <summary>
			/// Class that stores the Error Trace tags for DTD.
			/// </summary>
			public static class ErrorTrace
			{
				/// <summary>
				/// Error Trace Item tag.
				/// </summary>
				public const string TagErrorTraceItem = "Error.TraceItem";
				/// <summary>
				/// Type tag.
				/// </summary>
				public const string TagType = "Type";
				/// <summary>
				/// Number tag.
				/// </summary>
				public const string TagNumber = "Number";
			}
			#endregion Tags for <Error.Trace>
			#region Tags for <ChangedItems>
			/// <summary>
			/// Arguments tag.
			/// </summary>
			public const string TagChangedItems = "ChangedItems";
			/// <summary>
			/// Class that stores the ChangeDetectionItems tags for DTD.
			/// </summary>
			public static class ChangedItems
			{
				#region Tags for <ChangedItem>
				/// <summary>
				/// Argument tag.
				/// </summary>
				public const string TagChangedItem = "ChangedItem";
				/// <summary>
				/// Class that stores the Argument tags for DTD.
				/// </summary>
				public static class ChangedItem
				{
					/// <summary>
					/// Name tag.
					/// </summary>
					public const string TagName = "Name";
					public const string TagChangedItemNewValue = "ChangedItemNewValue";
					public const string TagChangedItemOldValue = "ChangedItemOldValue";
					/// <summary>
					/// Value tag.
					/// </summary>
					public const string TagValue = "Value";
					/// <summary>
					/// Type tag.
					/// </summary>
					public const string TagType = "Type";
					/// <summary>
					/// Literal tag.
					/// </summary>
					public const string TagLiteral = "Literal";
					/// <summary>
					/// Null tag.
					/// </summary>
					public const string TagNull = "Null";
					/// <summary>
					/// OID tag.
					/// </summary>
					public const string TagOID = "OID";

				   
				}
				#endregion Tags for <ChangedItem>
			}
			#endregion Tags for <ChangedItems>
		}
		#endregion Tags for <Error>

		#region Tags for <OID>
		/// <summary>
		/// OID tag.
		/// </summary>
		public const string TagOID= "OID";
		/// <summary>
		/// Class that stores the OID tags for DTD.
		/// </summary>
		public static class OID
		{
			/// <summary>
			/// Class tag.
			/// </summary>
			public const string TagClass = "Class";
			/// <summary>
			/// OID Field tag.
			/// </summary>
			public const string TagOIDField = "OID.Field";
			/// <summary>
			/// Type tag.
			/// </summary>
			public const string TagType = "Type";
		}
		#endregion Tags for <OID>

		#region Tags for <Alternate Key>
		/// <summary>
		/// OID tag.
		/// </summary>
		public const string TagAlternateKey = "AlternateKey";
		/// <summary>
		/// Class that stores the alternate key tags for DTD.
		/// </summary>
		public static class AlternateKey
		{
			/// <summary>
			/// Class tag.
			/// </summary>
			public const string TagName = "Name";
			/// <summary>
			/// OID Field tag.
			/// </summary>
			public const string TagAlternateKeyField = "AlternateKey.Field";
			/// <summary>
			/// Type tag.
			/// </summary>
			public const string TagType = "Type";
		}
		#endregion Tags for <Alternate Key>

		#region Tags for <Response>
		/// <summary>
		/// Response tag.
		/// </summary>
		public const string TagResponse = "Response";
		/// <summary>
		/// Class that stores the Response tags for DTD.
		/// </summary>
		public static class Response
		{
			/// <summary>
			/// Sequence tag.
			/// </summary>
			public const string TagSequence = "Sequence";
			/// <summary>
			/// Version DTD tag.
			/// </summary>
			public const string TagVersionDTD   = "VersionDTD";

			#region Tag of <Statistics>
			/// <summary>
			/// Statistics tag.
			/// </summary>
			public const string TagStatistics   = "Statistics";
			/// <summary>
			/// Class that stores the Statistics tags for DTD.
			/// </summary>
			public static class Statistics
			{
				/// <summary>
				/// Star Time tag.
				/// </summary>
				public const string TagStarTime = "StarTime";
				/// <summary>
				/// End Time tag.
				/// </summary>
				public const string TagEndTime = "EndTime";
				/// <summary>
				/// Elapsed Time tag.
				/// </summary>
				public const string TagElapsedTime = "ElapsedTime";
			}
			#endregion Tag of <Statistics>

			/// <summary>
			/// Ticket tag.
			/// </summary>
			public const string TagTicket   = "Ticket";

			#region Tags for <Service.Response>
			/// <summary>
			/// Service Response tag.
			/// </summary>
			public const string TagServiceResponse = "Service.Response";
			/// <summary>
			/// Class that stores the Service Response tags for DTD.
			/// </summary>
			public static class ServiceResponse
			{
				/// <summary>
				/// Error tag.
				/// </summary>
				public const string TagError = "Error";
				/// <summary>
				/// OID tag.
				/// </summary>
				public const string TagOID = "OID";

				#region Tags for <Arguments>
				/// <summary>
				/// Arguments tag.
				/// </summary>
				public const string TagArguments = "Arguments";
				// Sub-elments from Service Request Arguments.
				#endregion Tags for <Argument>
			}
			#endregion Tags for <Service.Response>

			#region Tag of <Query.Response>
			/// <summary>
			/// Query Response tag.
			/// </summary>
			public const string TagQueryResponse = "Query.Response";
			/// <summary>
			/// Class that stores the Query Response tags for DTD.
			/// </summary>
			public static class QueryResponse
			{
				#region Tag of <Head>
				/// <summary>
				/// Head tag.
				/// </summary>
				public const string TagHead = "Head";
				/// <summary>
				/// Class that stores the Head tags for DTD.
				/// </summary>
				public static class Head
				{
					/// <summary>
					/// Head OID tag.
					/// </summary>
					public const string TagHeadOID = "Head.OID";
					/// <summary>
					/// Class tag.
					/// </summary>
					public const string TagClass = "Class";
					/// <summary>
					/// Head OID Field tag.
					/// </summary>
					public const string TagHeadOIDField = "Head.OID.Field";
					/// <summary>
					/// Head Columns tag.
					/// </summary>
					public const string TagHeadCols = "Head.Cols";
					/// <summary>
					/// Head Column tag.
					/// </summary>
					public const string TagHeadCol = "Head.Col";
					/// <summary>
					/// Name tag.
					/// </summary>
					public const string TagName = "Name";
					/// <summary>
					/// Type tag.
					/// </summary>
					public const string TagType = "Type";
				}
				#endregion Tag of <Head>

				#region Tag of <Data>
				/// <summary>
				/// Data tag.
				/// </summary>
				public const string TagData = "Data";
				/// <summary>
				/// Class that stores the Data tags for DTD.
				/// </summary>
				public static class Data
				{
					public const string TagRows = "Rows";
					public const string TagLastBlock = "LastBlock";

					public const string TagTotalRows = "TotalRows";

					#region Tag of <R>
					/// <summary>
					/// R tag.
					/// </summary>
					public const string TagR= "R";
					/// <summary>
					/// Class that stores the R tags for DTD.
					/// </summary>
					public static class R
					{
						/// <summary>
						/// O tag.
						/// </summary>
						public const string TagO = "O";
						/// <summary>
						/// V tag.
						/// </summary>
						public const string TagV = "V";
					}
					#endregion Tag of <R>
				}
				#endregion Tag of <Data>

				/// <summary>
				/// Error tag.
				/// </summary>
				public const string TagError = "Error";
			}
			#endregion Tag of <Query.Response>
		}
		#endregion Tags for <Response>

		#region for <Request>
		/// <summary>
		/// Request tag.
		/// </summary>
		public const string TagRequest = "Request";
		/// <summary>
		/// Class that stores the Request tags for DTD.
		/// </summary>
		public static class Request
		{
			/// <summary>
			/// Application tag.
			/// </summary>
			public const string TagApplication  = "Application";
			/// <summary>
			/// IdCnx tag.
			/// </summary>
			public const string TagIdCnx= "IdCnx";
			/// <summary>
			/// Sequence tag.
			/// </summary>
			public const string TagSequence = "Sequence";
			/// <summary>
			/// Version DTD tag.
			/// </summary>
			public const string TagVersionDTD   = "VersionDTD";
			/// <summary>
			/// Agent tag.
			/// </summary>
			public const string TagAgent = "Agent";
			/// <summary>
			/// Ticket tag.
			/// </summary>
			public const string TagTicket = "Ticket";

			#region Tags for <Service.Request>
			/// <summary>
			/// Service Request tag.
			/// </summary>
			public const string TagServiceRequest = "Service.Request";
			/// <summary>
			/// Class that stores the Service Request tags for DTD.
			/// </summary>
			public static class ServiceRequest
			{
				/// <summary>
				/// Class tag.
				/// </summary>
				public const string TagClass = "Class";
				/// <summary>
				/// Service tag.
				/// </summary>
				public const string TagService = "Service";

				#region Tags for <Arguments>
				/// <summary>
				/// Arguments tag.
				/// </summary>
				public const string TagArguments = "Arguments";
				/// <summary>
				/// Class that stores the Arguments tags for DTD.
				/// </summary>
				public static class Arguments
				{
					#region Tags for <Argument>
					/// <summary>
					/// Argument tag.
					/// </summary>
					public const string TagArgument = "Argument";
					/// <summary>
					/// Class that stores the Argument tags for DTD.
					/// </summary>
					public static class Argument
					{
						/// <summary>
						/// Name tag.
						/// </summary>
						public const string TagName = "Name";
						/// <summary>
						/// Value tag.
						/// </summary>
						public const string TagValue = "Value";
						/// <summary>
						/// Type tag.
						/// </summary>
						public const string TagType = "Type";
						/// <summary>
						/// Literal tag.
						/// </summary>
						public const string TagLiteral = "Literal";
						/// <summary>
						/// Null tag.
						/// </summary>
						public const string TagNull = "Null";
						/// <summary>
						/// OID tag.
						/// </summary>
						public const string TagOID = "OID";
					}
					#endregion Tags for <Argument>
				}
				#endregion Tags for <Arguments>
				#region Tags for <ChangeDetectionItems>
				/// <summary>
				/// Arguments tag.
				/// </summary>
				public const string TagChangeDetectionItems = "ChangeDetectionItems";
				/// <summary>
				/// Class that stores the ChangeDetectionItems tags for DTD.
				/// </summary>
				public static class ChangeDetectionItems
				{
					#region Tags for <ChangeDetectionItem>
					/// <summary>
					/// Argument tag.
					/// </summary>
					public const string TagChangeDetectionItem = "ChangeDetectionItem";
					/// <summary>
					/// Class that stores the Argument tags for DTD.
					/// </summary>
					public static class ChangeDetectionItem
					{
						/// <summary>
						/// Name tag.
						/// </summary>
						public const string TagName = "Name";
						/// <summary>
						/// Value tag.
						/// </summary>
						public const string TagValue = "Value";
						/// <summary>
						/// Type tag.
						/// </summary>
						public const string TagType = "Type";
						/// <summary>
						/// Literal tag.
						/// </summary>
						public const string TagLiteral = "Literal";
						/// <summary>
						/// Null tag.
						/// </summary>
						public const string TagNull = "Null";
						/// <summary>
						/// OID tag.
						/// </summary>
						public const string TagOID = "OID";
					}
					#endregion Tags for <ChangeDetectionItem>
				}
				#endregion Tags for <ChangeDetectionItems>
			}
			#endregion Tags for <Service.Request>

			#region Tags for <Query.Request>
			/// <summary>
			/// Query Request tag.
			/// </summary>
			public const string TagQueryRequest = "Query.Request";
			/// <summary>
			/// Class that stores the Query Request tags for DTD.
			/// </summary>
			public static class QueryRequest
			{
				/// <summary>
				/// Class tag.
				/// </summary>
				public const string TagClass = "Class";
				/// <summary>
				/// DisplaySet tag.
				/// </summary>
				public const string TagDisplaySet = "DisplaySet";
				/// <summary>
				/// Sort tag.
				/// </summary>
				public const string TagSort = "Sort";
				/// <summary>
				/// Criterium tag.
				/// </summary>
				public const string TagSortCriterium = "Criterium";

				#region Tags for <NavFilt>
				/// <summary>
				/// Navigational Filtering tag.
				/// </summary>
				public const string TagNavFilt = "NavFilt";
				/// <summary>
				/// Class that stores the Navigational Filtering tags for DTD.
				/// </summary>
				public static class NavFilt
				{
					/// <summary>
					/// UIElement tag.
					/// </summary>
					public const string TagNavFilterID = "NavFilterID";
					/// <summary>
					/// Navigational Filtering Argument tag.
					/// </summary>
					public const string TagNavFiltArgument = "NavFilt.Argument";
					/// <summary>
					/// Navigational Filtering Argument tag.
					/// </summary>
					public const string TagNavFiltVariable = "NavFilt.Variable";
					/// <summary>
					/// Class that stores the Navigational Filtering Argument tags for DTD.
					/// </summary>
					public static class NaviFiltArgument
					{
						/// <summary>
						/// Class tag.
						/// </summary>
						public const string TagClass = "Class";
						/// <summary>
						/// Service tag.
						/// </summary>
						public const string TagService = "Service";
						/// <summary>
						/// Argument tag.
						/// </summary>
						public const string TagArgument = "Argument";
					}
					/// <summary>
					/// Class that stores the Navigational Filtering filter variable tags for DTD.
					/// </summary>
					public static class NaviFiltVariable
					{
						/// <summary>
						/// Class tag.
						/// </summary>
						public const string TagClass = "Class";
						/// <summary>
						/// Filter tag.
						/// </summary>
						public const string TagFilter = "Filter";
						/// <summary>
						/// Argument tag.
						/// </summary>
						public const string TagVariable = "Variable";
					}
					/// <summary>
					/// Navigational Filtering Selected Object tag.
					/// </summary>
					public const string TagNavFiltSelectedObject = "NavFilt.SelectedObject";
					/// <summary>
					/// Navigational Filtering ServiceIU tag.
					/// </summary>
					public const string TagNavFiltServiceIU = "NavFilt.ServiceIU";
				}
				#endregion Tags for <NavFilt>

				/// <summary>
				/// Query Instance tag.
				/// </summary>
				public const string TagQueryInstance = "Query.Instance";

				#region Tags for <Query.Related>
				/// <summary>
				/// Query Related tag.
				/// </summary>
				public const string TagQueryRelated = "Query.Related";
				/// <summary>
				/// Class that stores the Query Related tags for DTD.
				/// </summary>
				public static class QueryRelated
				{
					/// <summary>
					/// Block Size tag.
					/// </summary>
					public const string TagBlockSize = "BlockSize";
					/// <summary>
					/// Start Row tag.
					/// </summary>
					public const string TagStartRow = "StartRow";
					/// <summary>
					/// Index tag.
					/// </summary>
					public const string TagIndex = "Index";
					/// <summary>
					/// OID tag.
					/// </summary>
					public const string TagOID = "OID";

					#region Tags for <LinkedTo>
					/// <summary>
					/// Linked To tag.
					/// </summary>
					public const string TagLinkedTo = "LinkedTo";
					/// <summary>
					/// Class that stores the Linked To tags for DTD.
					/// </summary>
					public static class LinkedTo
					{
						/// <summary>
						/// Link Item tag.
						/// </summary>
						public const string TagLinkItem = "Link.Item";
						/// <summary>
						/// Role tag.
						/// </summary>
						public const string TagRole = "Role";
						/// <summary>
						/// OID tag.
						/// </summary>
						public const string TagOID = "OID";
					}
					#endregion Tags for <LinkedTo>
				}
				#endregion Tags for <Query.Related>

				#region Tags for <Query.Filter>
				/// <summary>
				/// Query Filter tag.
				/// </summary>
				public const string TagQueryFilter = "Query.Filter";
				/// <summary>
				/// Class that stores the Query Filter tags for DTD.
				/// </summary>
				public static class QueryFilter
				{
					/// <summary>
					/// Name tag.
					/// </summary>
					public const string TagName = "Name";
					/// <summary>
					/// Block Size tag.
					/// </summary>
					public const string TagBlockSize = "BlockSize";
					/// <summary>
					/// Start Row tag.
					/// </summary>
					public const string TagStartRow = "StartRow";
					/// <summary>
					/// Index tag.
					/// </summary>
					public const string TagIndex = "Index";
					/// <summary>
					/// OID tag.
					/// </summary>
					public const string TagOID = "OID";

					#region Tags for <LinkedTo>
					/// <summary>
					/// Linked To tag.
					/// </summary>
					public const string TagLinkedTo = "LinkedTo";
					/// <summary>
					/// Class that stores the Linked To tags for DTD.
					/// </summary>
					public static class LinkedTo
					{
						/// <summary>
						/// Link Item tag.
						/// </summary>
						public const string TagLinkItem = "Link.Item";
						/// <summary>
						/// Role tag.
						/// </summary>
						public const string TagRole = "Role";
						/// <summary>
						/// OID tag.
						/// </summary>
						public const string TagOID = "OID";
					}
					#endregion Tags for <LinkedTo>

					#region Tags for <Filter.Variables>
					/// <summary>
					/// Filter Variables tag.
					/// </summary>
					public const string TagFilterVariables = "Filter.Variables";
					/// <summary>
					/// Class that stores the Filter Variables tags for DTD.
					/// </summary>
					public static class FilterVariables
					{
						#region Tags for <FilterVariable>
						/// <summary>
						/// Filter Variable tag.
						/// </summary>
						public const string TagFilterVariable = "Filter.Variable";
						/// <summary>
						/// Class that stores the Filter Variable tags for DTD.
						/// </summary>
						public static class FilterVariable
						{
							/// <summary>
							/// Name tag.
							/// </summary>
							public const string TagName = "Name";
							/// <summary>
							/// Type tag.
							/// </summary>
							public const string TagType = "Type";
							/// <summary>
							/// Literal tag.
							/// </summary>
							public const string TagLiteral = "Literal";
							/// <summary>
							/// Null tag.
							/// </summary>
							public const string TagNull = "Null";
							/// <summary>
							/// OID tag.
							/// </summary>
							public const string TagOID = "OID";
						}
						#endregion Tags for <FilterVariable>
					}
					#endregion Tags for <Filter.Variables>
				}
				#endregion Tags for <Query.Filter>
			}
			#endregion Tags for <Query.Request>
		}
		#endregion for <Request>
	}
	#endregion Tags for DTD version 3.10
}

