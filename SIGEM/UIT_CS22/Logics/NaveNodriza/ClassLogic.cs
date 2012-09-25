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

namespace SIGEM.Client.Logics.NaveNodriza
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
			NaveNodrizaOid lOid = null;
			if (context.ExchangeInformation != null && context.ExchangeInformation.SelectedOids.Count > 0)
			{
				lOid = new NaveNodrizaOid(context.ExchangeInformation.SelectedOids[0]);
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
				return ExecuteQueryInstance(agent, new NaveNodrizaOid(oid), displaySet);
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
		/// <param name="oid">Specific 'NaveNodrizaOid' Oid of the instance to be searched.</param>
		/// <param name="displaySet">Display set that will be retrieved.</param>
		/// <returns>A DataTable with the instance searched.</returns>
		public static DataTable ExecuteQueryInstance(Oid agent, NaveNodrizaOid oid, string displaySet)
		{
			return Logic.Adaptor.ExecuteQueryInstance(agent, "NaveNodriza", string.Empty, oid, displaySet);
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
			NaveNodrizaOid lLastOid = null;
			if (context.LastOids.Count > 0)
			{
				lLastOid = new NaveNodrizaOid(context.LastOids.Peek());
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
		public static DataTable ExecuteQueryPopulation(Oid agent, string displaySet, string orderCriteria, NaveNodrizaOid lastOid, int blockSize, ref bool lastBlock)
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
			DataTable lDataTable = Logic.Adaptor.ExecuteQueryRelated(agent, "NaveNodriza", linkItems, displaySet, orderCriteria, navigationalFiltering, lastOid, blockSize);

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

					NaveNodrizaOid lOidInstance = new NaveNodrizaOid(lNavInfo.SelectedOids[0]);
					return ExecuteQueryInstance(context.Agent, lOidInstance, context.DisplaySetAttributes);
				}

				// Get link items.
				Oid lOid = lNavInfo.SelectedOids[0];
				Dictionary<string, Oid> lLinkItems = new Dictionary<string, Oid>(StringComparer.CurrentCultureIgnoreCase);
				lLinkItems.Add(lNavInfo.RolePath, lOid);

				bool lLastBlock = true;
				NaveNodrizaOid lLastOid = null;
				string lOrderCriteria = string.Empty;

				// Get population members.
				if (lIUContext != null)
				{
					if (lIUContext.LastOid != null)
					{
						lLastOid = new NaveNodrizaOid(lIUContext.LastOid);
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
		public static DataTable ExecuteQueryRelated(Oid agent, Dictionary<string, Oid> linkItems, string displaySet, string orderCriteria, NaveNodrizaOid lastOid, int blockSize, ref bool lastBlock)
		{
			DataTable lDataTable = Logic.Adaptor.ExecuteQueryRelated(agent, "NaveNodriza", linkItems, displaySet, orderCriteria, lastOid, blockSize);

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
					case "Clas_1347649273856884AccOfer_AutoElemAcc_1_Alias":
						{
							return Action_NaveNodriza_Crear_NaveNodriza(context, row, actionKey);
						}
					case "Clas_1347649273856884AccOfer_AutoElemAcc_2_Alias":
						{
							return Action_NaveNodriza_SIU_delete_instance(context, row, actionKey);
						}
					case "Clas_1347649273856884AccOfer_AutoElemAcc_3_Alias":
						{
							return Action_NaveNodriza_SIU_edit_instance(context, row, actionKey);
						}
					case "Clas_1347649273856884AccOfer_AutoElemAcc_4_Alias":
						{
							return Action_NaveNodriza__Auto_(context, row, actionKey);
						}
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

		/// <summary>
		/// Decides if the action Crear_NaveNodriza of NaveNodriza has to be enabled or disabled ("1" / "0").
		/// </summary>
		/// <param name="context">IUQueryContext object received.</param>
		/// <param name="row">DataRow that contains the data of the instance selected.</param>
		/// <param name="actionItemIdXML">XML identificator of the action element.</param>
		/// <returns>A string indicating if the action element has to be enabled "1" or disabled "0".</returns>
		private static string Action_NaveNodriza_Crear_NaveNodriza(IUQueryContext context, DataRow row, string actionItemIdXML)
		{
			return "1";
		}

		/// <summary>
		/// Decides if the action SIU_delete_instance of NaveNodriza has to be enabled or disabled ("1" / "0").
		/// </summary>
		/// <param name="context">IUQueryContext object received.</param>
		/// <param name="row">DataRow that contains the data of the instance selected.</param>
		/// <param name="actionItemIdXML">XML identificator of the action element.</param>
		/// <returns>A string indicating if the action element has to be enabled "1" or disabled "0".</returns>
		private static string Action_NaveNodriza_SIU_delete_instance(IUQueryContext context, DataRow row, string actionItemIdXML)
		{
			return "1";
		}

		/// <summary>
		/// Decides if the action SIU_edit_instance of NaveNodriza has to be enabled or disabled ("1" / "0").
		/// </summary>
		/// <param name="context">IUQueryContext object received.</param>
		/// <param name="row">DataRow that contains the data of the instance selected.</param>
		/// <param name="actionItemIdXML">XML identificator of the action element.</param>
		/// <returns>A string indicating if the action element has to be enabled "1" or disabled "0".</returns>
		private static string Action_NaveNodriza_SIU_edit_instance(IUQueryContext context, DataRow row, string actionItemIdXML)
		{
			return "1";
		}

		/// <summary>
		/// Decides if the action _Auto_ of NaveNodriza has to be enabled or disabled ("1" / "0").
		/// </summary>
		/// <param name="context">IUQueryContext object received.</param>
		/// <param name="row">DataRow that contains the data of the instance selected.</param>
		/// <param name="actionItemIdXML">XML identificator of the action element.</param>
		/// <returns>A string indicating if the action element has to be enabled "1" or disabled "0".</returns>
		private static string Action_NaveNodriza__Auto_(IUQueryContext context, DataRow row, string actionItemIdXML)
		{
			return "1";
		}
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
			lFields.Add("id_NaveNodriza"); //Attribute Name is id_NaveNodriza

			return lFields;
		}
		#endregion Get identification function fields

	}
}
