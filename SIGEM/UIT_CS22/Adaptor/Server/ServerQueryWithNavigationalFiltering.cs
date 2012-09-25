// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Data;
using System.Data.Common;

using SIGEM.Client;
using SIGEM.Client.Oids;
using SIGEM.Client.Adaptor;
using SIGEM.Client.Adaptor.Exceptions;

namespace SIGEM.Client.Adaptor
{
	public partial class ServerConnection
	{
		#region ExecuteFilterWithNavigationalFiltering

		#region ExecuteFilter With Navigational Filtering - Arguments - With Filter Variables & Link Items
		/// <summary>
		/// Executes a query and applies a filter on a population related to a navigational filtering.
		/// The filter variables are two collections.
		/// </summary>
		/// <param name="agent">Agent</param>
		/// <param name="className">Class Name.</param>
		/// <param name="filterName">Filter Name.</param>
		/// <param name="variableTypes">Filter variables types.</param>
		/// <param name="variableValues">Filter variables values.</param>
		/// <param name="variableDomains">Filter variables domains.</param>
		/// <param name="linkItems">Related Oids.</param>
		/// <param name="displaySet">Display Set in format "Atribute1, attribute2, ...".</param>
		/// <param name="orderCriteria">Order Criteria.</param>
		/// <param name="navigationalFiltering">Navigational Filter.</param>
		/// <param name="lastOid">Last Oid</param>
		/// <param name="blockSize">Block Size 0 = All.</param>
		/// <returns>DataTable with rows of result Query.</returns>
		public DataTable ExecuteQueryFilter(
			Oid agent,
			string className,
			string filterName,
			Dictionary<string, ModelType> variableTypes,
			Dictionary<string, object> variableValues,
			Dictionary<string, string> variableDomains,
			Dictionary<string, Oid> linkItems,
			string displaySet,
			string orderCriteria,
			NavigationalFiltering navigationalFiltering,
			Oid lastOid,
			int blockSize)
		{
			// Create Filter Variables.
			FilterVariables lFilterVariables = new FilterVariables(variableTypes, variableValues, variableDomains);
			return ExecuteFilter(agent,className,filterName,lFilterVariables,linkItems,displaySet, orderCriteria,navigationalFiltering,lastOid,blockSize);
		}
		#endregion ExecuteFilter With Navigational Filtering - Arguments - With Filter Variables & Link Items

		#region ExecuteFilter With Navigational Filtering - Arguments - With Filter Variables & Link Items
		/// <summary>
		/// Executes a query and applies a filter on a population related to a navigational filtering.
		/// The filter variables are ArgumentsList.
		/// </summary>
		/// <param name="agent">Agent</param>
		/// <param name="className">Class Name.</param>
		/// <param name="filterName">Filter Name.</param>
		/// <param name="filterVariables">Filter variables.</param>
		/// <param name="linkItems">Related Oids.</param>
		/// <param name="displaySet">Display Set in format "Atribute1, attribute2, ...".</param>
		/// <param name="orderCriteria">Order Criteria.</param>
		/// <param name="navigationalFiltering">Navigational Filtering.</param>
		/// <param name="lastOid">Last Oid</param>
		/// <param name="blockSize">Block Size 0 = All.</param>
		/// <returns>DataTable with rows of result Query.</returns>
		public DataTable ExecuteQueryFilter(
			Oid agent,
			string className,
			string filterName,
			ArgumentsList filterVariables,
			Dictionary<string, Oid> linkItems,
			string displaySet,
			string orderCriteria,
			NavigationalFiltering navigationalFiltering,
			Oid lastOid,
			int blockSize)
		{
			// Create Filter Variables.
			FilterVariables lFilterVariables = null;
			if (filterVariables!= null)
			{
				lFilterVariables = new FilterVariables();
				foreach (ArgumentInfo lArgumentInfo in filterVariables)
				{
					lFilterVariables.Add(lArgumentInfo.Name, lArgumentInfo.Type, lArgumentInfo.Value, lArgumentInfo.ClassName);
				}
			}
			return ExecuteFilter(agent,className, filterName, lFilterVariables, linkItems, displaySet, orderCriteria, navigationalFiltering, lastOid, blockSize);
		}
		#endregion ExecuteFilter With Navigational Filtering - Arguments - With Filter Variables & Link Items

		#region [Private] Query Filter With Navigational Filtering -> Create QueryFilter to call ExecuteQuery(..)
		/// <summary>
		/// Creates an instance of QueryFilter & call ExecuteQuery.
		/// </summary>
		/// <param name="agent">Agent authenticate.</param>
		/// <param name="className">Class name.</param>
		/// <param name="filterName">Filter name.</param>
		/// <param name="filterVariables">Filter variables.</param>
		/// <param name="linkItems">Related Oids.</param>
		/// <param name="displaySet">Display set to retrive.</param>
		/// <param name="orderCriteria">Order criteria name.</param>
		/// <param name="navigationalFiltering">Navegational Filtering.</param>
		/// <param name="lastOid">Last Oid</param>
		/// <param name="blockSize">Block Size.</param>
		/// <returns>Datable with result of query.</returns>
		private DataTable ExecuteFilter(
			Oid agent,
			string className,
			string filterName,
			FilterVariables filterVariables,
			Dictionary<string, Oid> linkItems,
			string displaySet,
			string orderCriteria,
			NavigationalFiltering navigationalFiltering,
			Oid lastOid,
			int blockSize)
		{
			// Create the Query Filter.
			QueryFilter lQueryFilter = new QueryFilter(filterName, filterVariables, linkItems, lastOid, blockSize);
			DataTable lResult = ExecuteQuery(agent, className, displaySet, lQueryFilter, orderCriteria, navigationalFiltering);
			if (lResult != null)
			{
				// Add the name of Query Filter.
				lResult.ExtendedProperties.Add("QueryFilterName", filterName);
			}
			return lResult;
		}
		#endregion  [Private] Query Filter With Navigational Filtering -> Create QueryFilter to call ExecuteQuery(..)

		#endregion ExecuteFilterWithNavigationalFiltering

		#region Execute Query Related's With Navigational Filtering

		#region Execute Query Related with (Navigational Filtering)
		/// <summary>
		/// Executes a query related with an Oid and Navigational Filtering.
		/// </summary>
		/// <param name="agent">Agent.</param>
		/// <param name="className">Class name.</param>
		/// <param name="displaySet">DisplaySet.</param>
		/// <param name="navigationalFiltering">Navigational filtering.</param>
		/// <param name="blockSize">Block size.</param>
		/// <returns>DataTable.</returns>
		public DataTable ExecuteQueryRelated(
			Oid agent,
			string className,
			string displaySet,
			NavigationalFiltering navigationalFiltering,
			int blockSize)
		{
			QueryRelated lQueryRelated = new QueryRelated(blockSize);

			return ExecuteQuery(agent, className, displaySet, lQueryRelated, string.Empty, navigationalFiltering);
		}
		#endregion Execute Query Related with (Navigational Filtering)

		#region  Execute Query Related with All (LinkItems, Order Criteria, Navigational Filtering, Last Oid).
		/// <summary>
		/// Executes a query related with an Oid in a Navigational Filtering. Related Oids are indicated.
		/// </summary>
		/// <param name="agent">Agent for Query.</param>
		/// <param name="className">Class Name to Query.</param>
		/// <param name="linkItems">Related Items to Query.</param>
		/// <param name="displaySet">Display set of attributes to show</param>
		/// <param name="orderCriteria">Sort or order Criteria.</param>
		/// <param name="NavigationalFiltering">Navigational Filtering.</param>
		/// <param name="lastOid">Last Oid.</param>
		/// <param name="blockSize">Block Size.</param>
		/// <returns>DataTable with Block Size of Rows.</returns>
		public DataTable ExecuteQueryRelated(
			Oid agent,
			string className,
			Dictionary<string, Oid> linkItems,
			string displaySet,
			string orderCriteria,
			NavigationalFiltering navigationalFiltering,
			Oid lastOid,
			int blockSize)
		{
			QueryRelated lQueryRelated = new QueryRelated(linkItems, lastOid, blockSize);

			return ExecuteQuery(agent, className, displaySet, lQueryRelated, orderCriteria, navigationalFiltering);
		}
		#endregion  Execute Query Related with All (LinkItems, Order Criteria, Navigational Filtering, Last Oid).

		#endregion Execute Query Related With Navigational Filtering
	}
}

