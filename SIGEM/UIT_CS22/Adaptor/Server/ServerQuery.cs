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


using SIGEM.Client.Oids;
using SIGEM.Client.Adaptor.DataFormats;
using SIGEM.Client.Adaptor.Exceptions;

namespace SIGEM.Client.Adaptor
{
	#region ServerConnection
	public partial class ServerConnection
	{
		#region Query Methods

		#region Execute Query Instance
		/// <summary>
		/// Executes a query related to an specific Oid.
		/// </summary>
		/// <param name="agent">Agent who initiates the query.</param>
		/// <param name="className">Class the query is executed on.</param>
		/// <param name="oid">Oid involved.</param>
		/// <param name="displaySet">DisplaySet where the query is executed.</param>
		/// <returns>DataTable with the result query.</returns>
		public DataTable ExecuteQueryInstance(Oid agent, string className, Oid oid, string displaySet)
		{
			DataTable lResult = null;
			// Build Query Instance.
			if (oid != null)
			{
				QueryInstance lQueryInstance = new QueryInstance(oid);
				lResult = ExecuteQuery(agent, className, displaySet, lQueryInstance, null, null);
			}
			else
			{
				throw new ArgumentNullException("ExecuteQueryInstance ->Oid is null");
			}
			return lResult;
		}
		/// <summary>
		/// Executes a query related to an specific Oid.
		/// </summary>
		/// <param name="agent">Agent who initiates the query.</param>
		/// <param name="className">Class the query is executed on.</param>
		/// <param name="alternateKeyName">Indicates the name of the alternate key that is desired to use.</param>
		/// <param name="oid">Oid involved.</param>
		/// <param name="displaySet">DisplaySet where the query is executed.</param>
		/// <returns>DataTable with the result query.</returns>
		public DataTable ExecuteQueryInstance(Oid agent, string className, string alternateKeyName, Oid oid, string displaySet)
		{
			DataTable lResult = null;
			// Build Query Instance.
			if (oid != null)
			{
				QueryInstance lQueryInstance = new QueryInstance(oid);
				lResult = ExecuteQuery(agent, className, alternateKeyName, displaySet, lQueryInstance, null, null);
			}
			else
			{
				throw new ArgumentNullException("ExecuteQueryInstance ->Oid is null");
			}
			return lResult;
		}

		#endregion Execute Query Instance

		#region Query Filter Methods

		#region Query Filter Methods without variables.
		/// <summary>
		/// Executes a query and applies a filter on a population.
		/// There are no filter variables and related Oids.
		/// </summary>
		/// <param name="agent">Agent</param>
		/// <param name="className">Class Name.</param>
		/// <param name="filterName">Filter Name.</param>
		/// <param name="displaySet">Display Set in format "Atribute1, attribute2, ...".</param>
		/// <param name="orderCriteria">Order Criteria.</param>
		/// <param name="lastOid">Last Oid.</param>
		/// <param name="blockSize">Block Size 0 = All.</param>
		/// <returns>DataTable with rows of result Query.</returns>
		public DataTable ExecuteQueryFilter(
			Oid agent,
			string className,
			string filterName,
			string displaySet,
			string orderCriteria,
			Oid lastOid,
			int blockSize)
		{
			return ExecuteFilter(agent, className, filterName, (FilterVariables)null,(Dictionary<string, Oid>)null, displaySet, orderCriteria, lastOid, blockSize);
		}
		/// <summary>
		/// Executes a query and applies a filter on a population.
		/// Related Oids are indicated. There are no filter variables.
		/// </summary>
		/// <param name="agent">Agent.</param>
		/// <param name="className">Class Name.</param>
		/// <param name="filterName">Filter Name.</param>
		/// <param name="linkItems">Related Oids.</param>
		/// <param name="displaySet">Display Set in format "Atribute1, attribute2, ...".</param>
		/// <param name="orderCriteria">Order Criteria.</param>
		/// <param name="lastOid">Last Oid.</param>
		/// <param name="blockSize">Block Size 0 = All.</param>
		/// <returns>DataTable with rows of result Query.</returns>
		public DataTable ExecuteQueryFilter(
			Oid agent,
			string className,
			string filterName,
			Dictionary<string, Oid> linkItems,
			string displaySet,
			string orderCriteria,
			Oid lastOid,
			int blockSize)
		{
				return ExecuteFilter(agent, className, filterName, (FilterVariables)null, linkItems, displaySet, orderCriteria, lastOid, blockSize);
		}
		#endregion Query Filter Methods without variables.

		#region Query Filter Methods variables are Dictionary<string,ModelType> types and Dictionary<string,object> values.
		/// <summary>
		/// Executes a query and applies a filter on a population.
		/// The filter variables are two collections. Related Oids are indicated.
		/// </summary>
		/// <param name="agent">Agent.</param>
		/// <param name="className">Class Name.</param>
		/// <param name="filterName">Filter Name.</param>
		/// <param name="variableTypes">Variable types.</param>
		/// <param name="variableValues">Variable values.</param>
		/// <param name="variableDomains">Variable domains.</param>
		/// <param name="linkItems">Related Oids.</param>
		/// <param name="displaySet">Display Set in format "Atribute1, attribute2, ...".</param>
		/// <param name="orderCriteria">Order Criteria.</param>
		/// <param name="lastOid">Last Oid.</param>
		/// <param name="blockSize">Block Size 0 = All.</param>
		/// <returns>DataTable with rows of result Query.</returns>
		public DataTable ExecuteQueryFilter(
			Oid agent,
			string className,
			string filterName,
			Dictionary<string,ModelType> variableTypes,
			Dictionary<string,object> variableValues,
			Dictionary<string, string> variableDomains,
			Dictionary<string, Oid> linkItems,
			string displaySet,
			string orderCriteria,
			Oid lastOid,
			int blockSize)
		{
				// Create Filter Variables.
				FilterVariables lFilterVariables = new FilterVariables(variableTypes, variableValues, variableDomains);
				return ExecuteFilter(agent, className, filterName, lFilterVariables, linkItems, displaySet, orderCriteria, lastOid, blockSize);
		}
		/// <summary>
		/// Executes a query and applies a filter on a population.
		/// The filter variables are two collections.
		/// </summary>
		/// <param name="agent">Agent.</param>
		/// <param name="className">Class Name.</param>
		/// <param name="filterName">Filter Name.</param>
		/// <param name="variableTypes">Variable types.</param>
		/// <param name="variableValues">Variable values.</param>
		/// <param name="variableDomains">Variable domains.</param>
		/// <param name="displaySet">Display Set in format "Atribute1, attribute2, ...".</param>
		/// <param name="orderCriteria">Order Criteria.</param>
		/// <param name="lastOid">Last Oid.</param>
		/// <param name="blockSize">Block Size 0 = All.</param>
		/// <returns>DataTable with rows of result Query.</returns>
		public DataTable ExecuteQueryFilter(
			Oid agent,
			string className,
			string filterName,
			Dictionary<string, ModelType> variableTypes,
			Dictionary<string, object> variableValues,
			Dictionary<string, string> variableDomains,
			string displaySet,
			string orderCriteria,
			Oid lastOid,
			int blockSize)
		{
				// Create Filter Variables.
				FilterVariables lFilterVariables = new FilterVariables(variableTypes, variableValues, variableDomains);
				return ExecuteFilter(agent, className, filterName, lFilterVariables, (Dictionary<string, Oid>)null, displaySet, orderCriteria, lastOid, blockSize);
		}
		#endregion Query Filter Methods variables are Dictionary<string,ModelType> types and Dictionary<string,object> values.

		#region [Private] Query Filter -> Create QueryFilter to call ExecuteQuery(..).
		/// <summary>
		/// Executes Query Filter.
		/// </summary>
		/// <param name="agent">Agent.</param>
		/// <param name="className">Class Name.</param>
		/// <param name="filterName">Filter Name.</param>
		/// <param name="variables">Variables.</param>
		/// <param name="linkItems"></param>
		/// <param name="displaySet">Display Set in format "Atribute1, attribute2, ...".</param>
		/// <param name="orderCriteria">Order Criteria.</param>
		/// <param name="lastOid">Last Oid.</param>
		/// <param name="blockSize">Block Size 0 = All.</param>
		/// <returns>DataTable with rows of result Query.</returns>
		private DataTable ExecuteFilter(
			Oid agent,
			string className,
			string filterName,
			FilterVariables variables,
			Dictionary<string, Oid> linkItems,
			string displaySet,
			string orderCriteria,
			Oid lastOid,
			int blockSize)
		{
			// Create the Query Filter.
			QueryFilter lQueryFilter = new QueryFilter(filterName, variables, linkItems, lastOid, blockSize);
			DataTable lResult = ExecuteQuery(agent, className, displaySet, lQueryFilter, orderCriteria, null);
			// Add the name of Query Filter.
			lResult.ExtendedProperties.Add("QueryFilterName", filterName);
			return lResult;
		}
		#endregion  [Private] Query Filter -> Create QueryFilter to call ExecuteQuery(..).

		#endregion Query Filter Methods

		#region Execute Query Related

		#region Execute Query Related without all
		/// <summary>
		/// Executes a query related with an Oid.
		/// </summary>
		/// <param name="agent">Agent.</param>
		/// <param name="className">Class name.</param>
		/// <param name="displaySet">DisplaySet.</param>
		/// <param name="blockSize">Block size.</param>
		/// <returns>DataTable.</returns>
		public DataTable ExecuteQueryRelated(
			Oid agent,
			string className,
			string displaySet,
			int blockSize)
		{
				QueryRelated lQueryRelated = new QueryRelated(blockSize);
				return ExecuteQuery(agent, className, displaySet, lQueryRelated, string.Empty, null);
		}
		#endregion Execute Query Related without all

		#region Execute Query Related with (Order Criteria, Last Oid)
		/// <summary>
		/// Executes a query related with an Oid. The order criteria is indicated.
		/// </summary>
		/// <param name="agent">Agent.</param>
		/// <param name="className">Class name.</param>
		/// <param name="displaySet">DisplaySet.</param>
		/// <param name="orderCriteria">Order criteria.</param>
		/// <param name="lastOid">Last Oid.</param>
		/// <param name="blockSize">Block size.</param>
		/// <returns>DataTable.</returns>
		public DataTable ExecuteQueryRelated(
			Oid agent,
			string className,
			string displaySet,
			string orderCriteria,
			Oid lastOid,
			int blockSize)
		{
			QueryRelated lQueryRelated = new QueryRelated(lastOid, blockSize);
			return ExecuteQuery(agent, className, displaySet, lQueryRelated, orderCriteria, null);
		}
		#endregion Execute Query Related with (Order Criteria, Last Oid)

		#region  Execute Query Related with All (LinkItems, Order Criteria, Last Oid)
		/// <summary>
		/// Executes a query related with an Oid. Related Oids are indicated.
		/// </summary>
		/// <param name="agent">Agent.</param>
		/// <param name="className">Class name.</param>
		/// <param name="linkItems">Related Oids.</param>
		/// <param name="displaySet">DisplaySet.</param>
		/// <param name="orderCriteria">Order criteria.</param>
		/// <param name="lastOid">Last Oid.</param>
		/// <param name="blockSize">Block size.</param>
		/// <returns>DataTable.</returns>
		public DataTable ExecuteQueryRelated(
			Oid agent,
			string className,
			Dictionary<string, Oid> linkItems,
			string displaySet,
			string orderCriteria,
			Oid lastOid,
			int blockSize)
			{
				QueryRelated lQueryRelated = new QueryRelated(linkItems, lastOid, blockSize);
				return ExecuteQuery(agent, className, displaySet, lQueryRelated, orderCriteria, null);
			}
		#endregion  Execute Query Related with All (LinkItems, Order Criteria, Last Oid)

		#endregion Execute Query Related

		#region [Private] ExecuteQuery -> Create and Send the Request to Server
		/// <summary>
		/// Executes a query.
		/// </summary>
		/// <param name="agent">Agent who executes the query.</param>
		/// <param name="className">Class name to query.</param>
		/// <param name="displaySet">DisplaySet</param>
		/// <param name="queryInstance">Query to execute.</param>
		/// <param name="orderCriteria">Order criteria.</param>
		/// <param name="navigationalFiltering">Navigational Filtering object.</param>
		/// <returns>DataTable with rows of result Query.</returns>
		private DataTable ExecuteQuery(
				Oid agent,
				string className,
				string displaySet,
				QueryInstance queryInstance,
				string orderCriteria,
				NavigationalFiltering navigationalFiltering)
		{
			return this.ExecuteQuery(agent, className, string.Empty, displaySet, queryInstance, orderCriteria, navigationalFiltering);
		}
		/// <summary>
		/// Executes a query.
		/// </summary>
		/// <param name="agent">Agent who executes the query.</param>
		/// <param name="className">Class name to query.</param>
		/// <param name="alternateKeyName">Indicates the alternate key name of the .</param>
		/// <param name="displaySet">DisplaySet</param>
		/// <param name="queryInstance">Query to execute.</param>
		/// <param name="orderCriteria">Order criteria.</param>
		/// <param name="navigationalFiltering">Navigational Filtering object.</param>
		/// <returns>DataTable with rows of result Query.</returns>
		private DataTable ExecuteQuery(
			Oid agent,
			string className,
			string alternateKeyName,
			string displaySet,
			QueryInstance queryInstance,
			string orderCriteria,
			NavigationalFiltering navigationalFiltering)
		{
			//Create the Query Request.
			QueryRequest lQueryRequest = new QueryRequest(
							className, displaySet,
							queryInstance, orderCriteria,
							navigationalFiltering);

			lQueryRequest.AlternateKeyName = alternateKeyName;

			// Create the Request.
			Request lRequest = new Request(lQueryRequest, agent);
			// Send Request to Server.
			Response lResponse = this.Send(lRequest);
			if ((lResponse != null) && (lResponse.Query != null))
			{
				return lResponse.Query.Data;
			}
			return null;
		}
		#endregion [Private] ExecuteQuery -> Create and Send the Request to Server
		
		private bool HasAlternateKeysToProcess(DataTable datatable, string alternateKeyName)
		{
			if (datatable != null && datatable.Rows.Count == 1)
			{
				// Get the Oid associated to the row and check if it has alternate key.
				Oid oid = ServerConnection.GetOid(datatable, datatable.Rows[0], alternateKeyName);
				Oid alternateKey = (Oid)oid.GetAlternateKey(alternateKeyName);
				if (alternateKey != null)
				{
					foreach (IOidField oidField in alternateKey.Fields)
					{
						if (datatable.Columns[oidField.Name] == null)
						{
							// If any field is null, it is considered that there is not alternate key.
							return false;
						}
					}
					return true;
				}
			}

			return false;
		}
 
		#endregion Query Methods
	}
	#endregion ServerConnection
}

