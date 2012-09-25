// v3.8.4.5.b

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using SIGEM.Client.Adaptor;
using SIGEM.Client.Adaptor.Exceptions;
using SIGEM.Client.Oids;
using SIGEM.Client.Presentation;

namespace SIGEM.Client.Logics.Administrador
{
	/// <summary>
	/// Class that solves the class logic
	/// </summary>
	public static class ClassLogic
	{

		#region Execute Query Instance
		/// <summary>
		/// Execute a query to retrieve an instance.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <returns>A DataTable with the instance searched.</returns>
		public static DataTable ExecuteQueryInstance(IUQueryContext context)
		{
			AdministradorOid lOid = null;
			if (context.ExchangeInformation != null && context.ExchangeInformation.SelectedOids.Count > 0)
			{
				lOid = new AdministradorOid(context.ExchangeInformation.SelectedOids[0]);
			}
			return ExecuteQueryInstance(context.Agent, lOid, context.DisplaySetAttributes);

		}
		/// <summary>
		/// Execute a query to retrieve an instance.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="oid">Oid of the instance to be searched.</param>
		/// <param name="displaySet">Display set that will be retrieved.</param>
		/// <returns>A DataTable with the instance searched.</returns>
		public static DataTable ExecuteQueryInstance(Oid agent, Oid oid, string displaySet)
		{
			try
			{
				return ExecuteQueryInstance(agent, new AdministradorOid(oid), displaySet);
			}
			catch
			{
				return null;
			}
		}
		/// <summary>
		/// Execute a query to retrieve an instance.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="oid">Specific 'AdministradorOid' Oid of the instance to be searched.</param>
		/// <param name="displaySet">Display set that will be retrieved.</param>
		/// <returns>A DataTable with the instance searched.</returns>
		public static DataTable ExecuteQueryInstance(Oid agent, AdministradorOid oid, string displaySet)
		{
			return Logic.Adaptor.ExecuteQueryInstance(agent, "Administrador", string.Empty, oid, displaySet);
		}
		#endregion Execute query instance

		#region Execute query population
		/// <summary>
		/// Execute a query to retrieve a set of instances, without any condition.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <returns>A DataTable with the instances searched.</returns>
		public static DataTable ExecuteQueryPopulation(IUPopulationContext context)
		{
			// Last Oid
			AdministradorOid lLastOid = null;
			if (context.LastOids.Count > 0)
			{
				lLastOid = new AdministradorOid(context.LastOids.Peek());
			}

			// Last Block
			bool lLastBlock = true;
			NavigationalFiltering navigationalFiltering = NavigationalFiltering.GetNavigationalFiltering(context);
			DataTable lDataTable = ExecuteQueryRelated(context.Agent, new Dictionary<string, Oid>(), context.DisplaySetAttributes, context.OrderCriteriaNameSelected, navigationalFiltering, lLastOid, context.BlockSize, ref lLastBlock);
			context.LastBlock = lLastBlock;

			return lDataTable;
		}
		/// <summary>
		/// Execute a query to retrieve a set of instances, without any condition.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="displaySet">List of attributes to return.</param>
		/// <param name="orderCriteria">Order criteria name.</param>
		/// <param name="lastOid">Oid from which to search (not included).</param>
		/// <param name="blockSize">Number of instances to return (0 for all population).</param>
		/// <returns>A DataTable with the instances searched.</returns>
		public static DataTable ExecuteQueryPopulation(Oid agent, string displaySet, string orderCriteria, NavigationalFiltering navigationalFiltering, Oid lastOid, int blockSize, ref bool lastBlock)
		{
			return ExecuteQueryRelated(agent, new Dictionary<string, Oid>(), displaySet, orderCriteria, navigationalFiltering, lastOid, blockSize, ref lastBlock);
		}
		/// <summary>
		/// Execute a query to retrieve a set of instances, without any condition.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="displaySet">List of attributes to return.</param>
		/// <param name="orderCriteria">Order criteria name.</param>
		/// <param name="lastOid">Oid from which to search (not included).</param>
		/// <param name="blockSize">Number of instances to return (0 for all population).</param>
		/// <returns>A DataTable with the instances searched.</returns>
		public static DataTable ExecuteQueryPopulation(Oid agent, string displaySet, string orderCriteria, AdministradorOid lastOid, int blockSize, ref bool lastBlock)
		{
			return ExecuteQueryRelated(agent, new Dictionary<string, Oid>(), displaySet, orderCriteria, lastOid, blockSize, ref lastBlock);
		}
		#endregion Execute query population

		#region Execute query related
		/// <summary>
		/// Execute a query related with other instance.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="linkItems">List of related instance oids (path - role).</param>
		/// <param name="displaySet">List of attributes to return.</param>
		/// <param name="orderCriteria">Order criteria name.</param>
		/// <param name="lastOid">Oid from which to search (not included).</param>
		/// <param name="blockSize">Number of instances to return (0 for all population).</param>
		/// <param name="lastBlock">Indicates if it is last block.</param>
		/// <returns>A DataTable with the instances searched.</returns>
		public static DataTable ExecuteQueryRelated(Oid agent, Dictionary<string, Oid> linkItems, string displaySet, string orderCriteria, NavigationalFiltering navigationalFiltering, Oid lastOid, int blockSize, ref bool lastBlock)
		{
			DataTable lDataTable = Logic.Adaptor.ExecuteQueryRelated(agent, "Administrador", linkItems, displaySet, orderCriteria, navigationalFiltering, lastOid, blockSize);

			// Last block
			if (lDataTable.ExtendedProperties.Contains("LastBlock"))
			{
				lastBlock = (bool)lDataTable.ExtendedProperties["LastBlock"];
			}
			else
			{
				lastBlock = false;
			}

			return lDataTable;
		}
		/// <summary>
		/// Execute a query related with other instance.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <returns>A DataTable with the instances searched.</returns>
		public static DataTable ExecuteQueryRelated(IUQueryContext context)
		{
			try
			{
				ExchangeInfo lExchangeInfo = context.ExchangeInformation;

				if (lExchangeInfo.ExchangeType != ExchangeType.Navigation || lExchangeInfo.SelectedOids.Count == 0)
				{
					return null;
				}

				IUPopulationContext lIUContext = context as IUPopulationContext;
				int blockSize=1;
				if (lIUContext != null)
				{
					blockSize = lIUContext.BlockSize;
				}
				ExchangeInfoNavigation lNavInfo = lExchangeInfo as ExchangeInfoNavigation;
				// Specific case. No role name indicates Query by Instance.
				if (lNavInfo.RolePath == "")
				{
					if (lIUContext != null)
					{
						lIUContext.LastBlock = true;
					}

					AdministradorOid lOidInstance = new AdministradorOid(lNavInfo.SelectedOids[0]);
					return ExecuteQueryInstance(context.Agent, lOidInstance, context.DisplaySetAttributes);
				}

				// Get link items.
				Oid lOid = lNavInfo.SelectedOids[0];
				Dictionary<string, Oid> lLinkItems = new Dictionary<string, Oid>(StringComparer.CurrentCultureIgnoreCase);
				lLinkItems.Add(lNavInfo.RolePath, lOid);

				bool lLastBlock = true;
				AdministradorOid lLastOid = null;
				string lOrderCriteria = string.Empty;

				// Get population members.
				if (lIUContext != null)
				{
					if (lIUContext.LastOid != null)
					{
						lLastOid = new AdministradorOid(lIUContext.LastOid);
					}
					lOrderCriteria = lIUContext.OrderCriteriaNameSelected;
				}
				NavigationalFiltering navigationalFiltering = NavigationalFiltering.GetNavigationalFiltering(context);
				DataTable lDataTable = ExecuteQueryRelated(context.Agent, lLinkItems, context.DisplaySetAttributes, lOrderCriteria, navigationalFiltering, lLastOid, blockSize, ref lLastBlock);

				if (lIUContext != null)
				{
					lIUContext.LastBlock = lLastBlock;
				}

				return lDataTable;
			}
			catch (Exception e)
			{
				ScenarioManager.LaunchErrorScenario(e);
				return null;
			}
		}
		/// <summary>
		/// Execute a query related with other instance.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="linkItems">List of related instance oids (path - role).</param>
		/// <param name="displaySet">List of attributes to return.</param>
		/// <param name="orderCriteria">Order criteria name.</param>
		/// <param name="lastOid">Oid from which to search (not included).</param>
		/// <param name="blockSize">Number of instances to return (0 for all population).</param>
		/// <param name="lastBlock">Return it is last block.</param>
		/// <returns>A DataTable with the instances searched.</returns>
		public static DataTable ExecuteQueryRelated(Oid agent, Dictionary<string, Oid> linkItems, string displaySet, string orderCriteria, AdministradorOid lastOid, int blockSize, ref bool lastBlock)
		{
			DataTable lDataTable = Logic.Adaptor.ExecuteQueryRelated(agent, "Administrador", linkItems, displaySet, orderCriteria, lastOid, blockSize);

			// Last block.
			if (lDataTable.ExtendedProperties.Contains("LastBlock"))
				lastBlock = (bool) lDataTable.ExtendedProperties["LastBlock"];
			else
				lastBlock = false;

			return lDataTable;
		}
		#endregion Execute query related

		#region Enabled Actions & Navigations
		public static void GetActionsNavigationsEnabledState(IUQueryContext context, DataTable data, List<string> actionList, List<string> navigationList)
		{
			if (data == null)
			{
				return;
			}

			// Add control columns to the DataTable
			if (!data.Columns.Contains(Constants.ACTIONS_ACTIVATION_COLUMN_NAME))
			{
				data.Columns.Add(Constants.ACTIONS_ACTIVATION_COLUMN_NAME, typeof(string));
			}

			if (!data.Columns.Contains(Constants.NAVIGATIONS_ACTIVATION_COLUMN_NAME))
			{
				data.Columns.Add(Constants.NAVIGATIONS_ACTIVATION_COLUMN_NAME, typeof(string));
			}

			string activationActionsValue;
			string activationNavigationsValue;
			foreach (System.Data.DataRow lRow in data.Rows)
			{
				activationActionsValue = "";
				if (actionList != null)
				{
					foreach (string actionKey in actionList)
					{
						activationActionsValue += GetEnabledAction(context, lRow, actionKey);
					}
				}
				lRow[Constants.ACTIONS_ACTIVATION_COLUMN_NAME] = activationActionsValue;

				activationNavigationsValue = "";
				if (navigationList != null)
				{
					foreach (string navigationKey in navigationList)
					{
						activationNavigationsValue += GetEnabledNavigation(context, lRow, navigationKey);
					}
				}
				lRow[Constants.NAVIGATIONS_ACTIVATION_COLUMN_NAME] = activationNavigationsValue;
			}
		}

		#region Enabled Actions

		/// <summary>
		/// Get the value to Enable/Disable the action idientified by the Key
		/// </summary>
		/// <param name="context"></param>
		/// <param name="row">Instance values</param>
		/// <param name="actionKey">Action Identifier</param>
		/// <returns></returns>
		private static string GetEnabledAction(IUQueryContext context, DataRow row, string actionKey)
		{
			try
			{
				switch (actionKey)
				{
					default:
						return "1";
				}
			}
			catch
			{
				return "1";
			}
		}

		#region Enable/Disable Actions
		#endregion Enable/Disable Actions

		#region Evaluate Preconditions in Advance

		#endregion Evaluate Preconditions in Advance

		#endregion Enabled Actions

		#region Enabled Navigations
		private static string GetEnabledNavigation(IUQueryContext context, DataRow row, string navigationKey)
		{
			try
			{
				switch (navigationKey)
				{
					default:
						return "1";
				}
			}
			catch
			{
				return "1";
			}
		}
		#endregion Enabled Navigations

		#endregion Enabled Actions & Navigations

		#region Get identification function fields
		/// <summary>
		/// Returns a list with the name of the identification function attributes.
		/// </summary>
		/// <returns>List with the name of the identification function attributes.</returns>
		public static List<string> GetIdentificationFunctionFields()
		{
			List<string> lFields = new List<string>();
			lFields.Add("id_Administrador"); //Attribute Name is id_Administrador

			return lFields;
		}
		#endregion Get identification function fields

	}
}
