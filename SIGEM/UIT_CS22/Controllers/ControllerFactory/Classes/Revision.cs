  // v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client;
using SIGEM.Client.Logics;
using SIGEM.Client.Controllers;
using SIGEM.Client.Presentation;
using SIGEM.Client.Logics.Preferences;

namespace SIGEM.Client.ControllerFactory
{
	/// <summary>
	/// Class that manages the 'Revision' class defined in the model.
	/// </summary>
	public static class Revision
	{
		#region Actions
		#region _Auto_
		/// <summary>
		/// Creates the specific ActionController of the '_Auto_' Action pattern.
		/// </summary>
		/// <param name="parentController">Parent controller.</param>
		/// <returns>Specific ActionController of the '_Auto_' Action pattern.</returns>
		public static ActionController Action__Auto_(IUController parentController)
		{
			// Action _Auto_.
			ActionController lActionController = new ActionController("_Auto_", parentController, Properties.Settings.Default.ConjunctionPolicy);
			string[] lAgentsActionItem0 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem0 = new List<KeyValuePair<string, string[]>>();

			lActionController.Add(
				0,
				"New",
				"Clas_1348178542592347AccOfer_AutoElemAcc_1_Alias",
				lAgentsActionItem0,
				"Revision",
				typeof(InteractionToolkit.Revision.IUServices.SIU_create_instanceInboundIT).FullName, "",
				ActionItemType.Creation,
				false,
				true,
				lAttrActivationItem0);

			string[] lAgentsActionItem1 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem1 = new List<KeyValuePair<string, string[]>>();

			lActionController.Add(
				1,
				"Destroy",
				"Clas_1348178542592347AccOfer_AutoElemAcc_2_Alias",
				lAgentsActionItem1,
				"Revision",
				typeof(InteractionToolkit.Revision.IUServices.SIU_delete_instanceInboundIT).FullName,
				"",
				ActionItemType.Destruction,
				false,
				true,
				lAttrActivationItem1);

			string[] lAgentsActionItem2 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem2 = new List<KeyValuePair<string, string[]>>();

			lActionController.Add(
				2,
				"Edit",
				"Clas_1348178542592347AccOfer_AutoElemAcc_3_Alias",
				lAgentsActionItem2,
				"Revision",
				typeof(InteractionToolkit.Revision.IUServices.SIU_edit_instanceInboundIT).FullName,
				"",
				ActionItemType.Normal,
				false,
				true,
				lAttrActivationItem2);

			string[] lAgentsActionItem3 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem3 = new List<KeyValuePair<string, string[]>>();
			lActionController.Add(
				3,
				"Revision",
				"Clas_1348178542592347AccOfer_AutoElemAcc_4_Alias",
				lAgentsActionItem3,
				"Revision",
				typeof(InteractionToolkit.Revision.IUInstances.IIU_RevisionIT).FullName,
				"",
				ActionItemType.Other,
				false,
				false,
				lAttrActivationItem3);

			string[] lAgentsActionItem4 = Logic.InstanceReportsList.GetAgentsForClass("Revision");
			List<KeyValuePair<string, string[]>> lAttrActivationItem4 = new List<KeyValuePair<string, string[]>>();
			lActionController.Add(
				4,
				LanguageConstantValues.L_PRINT,
				LanguageConstantKeys.L_PRINT,
				lAgentsActionItem4,
				"Revision",
				typeof(InteractionToolkit.PrintForm).FullName,
				"",
				ActionItemType.Print,
				true,
				true,
				lAttrActivationItem4);

			return lActionController;
		}
		#endregion _Auto_
		#endregion Actions

		#region Navigations
		#region _Auto_
		/// <summary>
		/// Creates the specific NavigationController of the '_Auto_' Navigation pattern.
		/// </summary>
		/// <param name="parentController">Parent controller.</param>
		/// <returns>Specific NavigationController of the '_Auto_' Navigation pattern.</returns>
		public static NavigationController Navigation__Auto_(IUController parentController)
		{
			// Navigation _Auto_.
			NavigationController lNavigationController = new NavigationController("_Auto_", parentController);

			string[] lAgentsNavigationItem0 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem0 = new List<KeyValuePair<string, string[]>>();
			lNavigationController.Add(
				0,
				"RevisionPasajero",
				"Clas_1348178542592347NavOfer_AutoElemNav_1_Alias",
				lAgentsNavigationItem0,
				"RevisionPasajero",
				typeof(InteractionToolkit.RevisionPasajero.IUInstances._Auto_IT).FullName,
				"Revision",
				"",
				"RevisionPasajero",
				"Clas_1348178673664478UIInst_1",
				"Revision",
				"Clas_1348178542592347",
				"",
				lAttrActivationItem0,
				"");


			return lNavigationController;
		}
		#endregion _Auto_
		#endregion Navigations

		#region Order Criterias
		#endregion Order Criterias

		#region DisplaySets
		#region _Auto_
		/// <summary>
		/// Creates the specific DisplaySetController of the '_Auto_' DisplaySet pattern.
		/// </summary>
		/// <param name="parentController">Parent controller.</param>
		/// <returns>Specific DisplaySetController of the '_Auto_' DisplaySet pattern.</returns>
		public static DisplaySetController DisplaySet__Auto_(SIGEM.Client.Controllers.Controller parentController)
		{
			DisplaySetController lDisplaySetController = new DisplaySetController(parentController);
			DisplaySetInformation lDisplaySetInfo = new DisplaySetInformation("_Auto_");
			string[] lAgentsDSItem0 = { Agents.Administrador };
			lDisplaySetInfo.Add("id_RevisarAeronave", "id_RevisarAeronave", "Clas_1348178542592347CjtoVis_AutoICtjoVis_1_Alias", ModelType.Autonumeric, lAgentsDSItem0, DefaultFormats.ColWidthAutonumeric);
			string[] lAgentsDSItem1 = { Agents.Administrador };
			lDisplaySetInfo.Add("FechaRevision", "FechaRevision", "Clas_1348178542592347CjtoVis_AutoICtjoVis_2_Alias", ModelType.Date, lAgentsDSItem1, DefaultFormats.ColWidthDate);
			string[] lAgentsDSItem2 = { Agents.Administrador };
			lDisplaySetInfo.Add("NombreRevisor", "NombreRevisor", "Clas_1348178542592347CjtoVis_AutoICtjoVis_3_Alias", ModelType.String, lAgentsDSItem2, DefaultFormats.ColWidthString20);
			string[] lAgentsDSItem3 = { Agents.Administrador };
			lDisplaySetInfo.Add("Id_Aeronave", "Id_Aeronave", "Clas_1348178542592347CjtoVis_AutoICtjoVis_4_Alias", ModelType.String, lAgentsDSItem3, DefaultFormats.ColWidthString20);

			lDisplaySetController.CurrentDisplaySet = lDisplaySetInfo;
			return lDisplaySetController;
		}
		#endregion _Auto_

		#endregion DisplaySets

		#region Filters
		#endregion Filters

		#region Population Interaction Units
		#region _Auto_
		/// <summary>
		/// Creates the specific IUPopulationController of the '_Auto_' IU pattern.
		/// </summary>
		/// <param name="exchangeInfo">IUPopulationContext reference.</param>
		/// <returns>Specific IUPopulationController of the '_Auto_' IU pattern.</returns>
		public static IUPopulationController Population__Auto_(ExchangeInfo exchangeInfo)
		{
			IUPopulationContext lContext = new IUPopulationContext(exchangeInfo,"Revision", "_Auto_");
			// Block size.
			lContext.BlockSize = 40;
			IUPopulationController lController = new IUPopulationController("_Auto_", "Revision", "Clas_1348178542592347UIPobCl_Auto_Alias", lContext, null);

			// Action _Auto_.
			lController.Action = Revision.Action__Auto_(lController);
			// Navigation _Auto_.
			lController.Navigation = Revision.Navigation__Auto_(lController);
			// DisplaySet '_Auto_'.
			DisplaySetByBlocksController lDisplaySetByBlocksController = new DisplaySetByBlocksController(Revision.DisplaySet__Auto_(lController));
			lDisplaySetByBlocksController.DisplaySetList.Add(Revision.DisplaySet__Auto_(lController).CurrentDisplaySet);
			lController.DisplaySet = lDisplaySetByBlocksController;
			// Get user preferences for this scenario.
			lController.SetPreferences(Logic.UserPreferences.GetScenarioPrefs("Revision:_Auto_") as PopulationPrefs);

			return lController;
		}
		#endregion _Auto_

		#endregion Population Interaction Units

		#region Instance Interaction Units
		#region IIU_Revision
		/// <summary>
		/// Creates the specific IUInstanceController of the 'IIU_Revision' IU pattern.
		/// </summary>
		/// <param name="exchangeInfo">IUInstanceContext reference.</param>
		/// <returns>Specific IUInstanceController of the 'IIU_Revision' IU pattern.</returns>
		public static IUInstanceController Instance_IIU_Revision(ExchangeInfo exchangeInfo)
		{
			IUInstanceContext lContext = new IUInstanceContext(exchangeInfo,"Revision", "IIU_Revision");
			IUInstanceController lController = new IUInstanceController("IIU_Revision", "Revision", "Clas_1348178542592347UIInst_1_Alias" ,lContext,null);

			// Action _Auto_.
			lController.Action = Revision.Action__Auto_(lController);
			// Navigation _Auto_.
			lController.Navigation = Revision.Navigation__Auto_(lController);
			// DisplaySet '_Auto_'.
			DisplaySetController lDisplaySetController = Revision.DisplaySet__Auto_(lController);
			lController.DisplaySet = lDisplaySetController;

			// Oid selector.
			lController.OidSelector = new ArgumentOVController("oidSelector", "Revision", "Clas_1348178542592347_Alias", "Revision", false, false, false, typeof(InteractionToolkit.Revision.IUPopulations._Auto_IT).FullName, null, "", null);

			// Get User preferences.
			lController.SetPreferences(Logic.UserPreferences.GetScenarioPrefs("Revision:IIU_Revision") as InstancePrefs);

			return lController;
		}
		#endregion IIU_Revision

		#endregion Instance Interaction Units

		#region M-D Interaction Units

		#endregion M-D Interaction Units

		#region  Service Interaction Units
		#region create_instanceInbound
		/// <summary>
		/// Creates the specific IUServiceController of the 'create_instance' IU pattern.
		/// </summary>
		/// <param name="exchangeInfo">IUServiceController reference.</param>
		/// <returns>Specific IUServiceController of the 'create_instance' IU pattern.</returns>
		public static IUServiceController Service_create_instanceInbound(ExchangeInfo exchangeInfo, IUController parentController)
		{
			string[] lAgentsService = { Agents.Administrador };
			IUServiceContext lContext = new IUServiceContext(exchangeInfo, "Revision", "create_instance", "SIU_create_instance");
			IUServiceController lController = new IUServiceController("create_instance", "New", "Clas_1348178542592347Ser_1_Alias", lAgentsService, "Revision", "create_instance", lContext, null);

			// This controller is an InboundArgument controller.
			lController.IsOutboundArgumentController = false;


			#region Inbound arguments
			IArguments InboundArguments = lController.InputFields;
			// Argument p_atrid_RevisarAeronave.
			InboundArguments.Add(new ArgumentDVController("p_atrid_RevisarAeronave", "id_RevisarAeronave", "Clas_1348178542592347Ser_1UIServ_1ElemAgrup_1_Alias", ModelType.Autonumeric, 0, false, null, lController));
 			// Argument p_atrFechaRevision.
			InboundArguments.Add(new ArgumentDVController("p_atrFechaRevision", "FechaRevision", "Clas_1348178542592347Ser_1UIServ_1ElemAgrup_3_Alias", ModelType.Date, 0, false, null, lController));
 			// Argument p_atrNombreRevisor.
			InboundArguments.Add(new ArgumentDVController("p_atrNombreRevisor", "NombreRevisor", "Clas_1348178542592347Ser_1UIServ_1ElemAgrup_4_Alias", ModelType.String, 100, false, null, lController));
 			// Argument p_atrId_Aeronave.
			InboundArguments.Add(new ArgumentDVController("p_atrId_Aeronave", "Id_Aeronave", "Clas_1348178542592347Ser_1UIServ_1ElemAgrup_5_Alias", ModelType.String, 100, false, null, lController));
 			#endregion Inbound arguments

			#region Outbound arguments
			// If the service has outbound arguments, the outbound arguments scenario is set here; otherwise, null.
			lController.OutboundArgumentsScenario = null;
			#endregion Outbound arguments


			return lController;
		}
		#endregion create_instanceInbound

		#region delete_instanceInbound
		/// <summary>
		/// Creates the specific IUServiceController of the 'delete_instance' IU pattern.
		/// </summary>
		/// <param name="exchangeInfo">IUServiceController reference.</param>
		/// <returns>Specific IUServiceController of the 'delete_instance' IU pattern.</returns>
		public static IUServiceController Service_delete_instanceInbound(ExchangeInfo exchangeInfo, IUController parentController)
		{
			string[] lAgentsService = { Agents.Administrador };
			IUServiceContext lContext = new IUServiceContext(exchangeInfo, "Revision", "delete_instance", "SIU_delete_instance");
			IUServiceController lController = new IUServiceController("delete_instance", "Destroy", "Clas_1348178542592347Ser_2_Alias", lAgentsService, "Revision", "delete_instance", lContext, null, false);

			// This controller is an InboundArgument controller.
			lController.IsOutboundArgumentController = false;


			#region Inbound arguments
			IArguments InboundArguments = lController.InputFields;
			ArgumentOVController lArgument = null;
			// Argument p_thisRevisarAeronave.
			lArgument = new ArgumentOVController("p_thisRevisarAeronave", "Revision", "Clas_1348178542592347Ser_2UIServ_1ElemAgrup_1_Alias", "Revision", false, true, false, typeof(InteractionToolkit.Revision.IUPopulations._Auto_IT).FullName, null, "", lController);
			lController.ArgumentThis = lArgument;
			InboundArguments.Add(lArgument);
			#endregion Inbound arguments

			#region Outbound arguments
			// If the service has outbound arguments, the outbound arguments scenario is set here; otherwise, null.
			lController.OutboundArgumentsScenario = null;
			#endregion Outbound arguments


			return lController;
		}
		#endregion delete_instanceInbound

		#region edit_instanceInbound
		/// <summary>
		/// Creates the specific IUServiceController of the 'edit_instance' IU pattern.
		/// </summary>
		/// <param name="exchangeInfo">IUServiceController reference.</param>
		/// <returns>Specific IUServiceController of the 'edit_instance' IU pattern.</returns>
		public static IUServiceController Service_edit_instanceInbound(ExchangeInfo exchangeInfo, IUController parentController)
		{
			string[] lAgentsService = { Agents.Administrador };
			IUServiceContext lContext = new IUServiceContext(exchangeInfo, "Revision", "edit_instance", "SIU_edit_instance");
			IUServiceController lController = new IUServiceController("edit_instance", "Edit", "Clas_1348178542592347Ser_3_Alias", lAgentsService, "Revision", "edit_instance", lContext, null, false);

			// This controller is an InboundArgument controller.
			lController.IsOutboundArgumentController = false;


			#region Inbound arguments
			IArguments InboundArguments = lController.InputFields;
			ArgumentOVController lArgument = null;
			// Argument p_thisRevisarAeronave.
			lArgument = new ArgumentOVController("p_thisRevisarAeronave", "Revision", "Clas_1348178542592347Ser_3UIServ_1ElemAgrup_1_Alias", "Revision", false, true, false, typeof(InteractionToolkit.Revision.IUPopulations._Auto_IT).FullName, null, "", lController);
			lController.ArgumentThis = lArgument;
			InboundArguments.Add(lArgument);
			#endregion Inbound arguments

			#region Outbound arguments
			// If the service has outbound arguments, the outbound arguments scenario is set here; otherwise, null.
			lController.OutboundArgumentsScenario = null;
			#endregion Outbound arguments


			return lController;
		}
		#endregion edit_instanceInbound

		#endregion Service Interaction Units
	}
}
