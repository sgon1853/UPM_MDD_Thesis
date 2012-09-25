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
	/// Class that manages the 'RevisionPasajero' class defined in the model.
	/// </summary>
	public static class RevisionPasajero
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
				"Clas_1348178673664478AccOfer_AutoElemAcc_1_Alias",
				lAgentsActionItem0,
				"RevisionPasajero",
				typeof(InteractionToolkit.RevisionPasajero.IUServices.SIU_create_instanceInboundIT).FullName, "",
				ActionItemType.Creation,
				false,
				true,
				lAttrActivationItem0);

			string[] lAgentsActionItem1 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem1 = new List<KeyValuePair<string, string[]>>();

			lActionController.Add(
				1,
				"Destroy",
				"Clas_1348178673664478AccOfer_AutoElemAcc_2_Alias",
				lAgentsActionItem1,
				"RevisionPasajero",
				typeof(InteractionToolkit.RevisionPasajero.IUServices.SIU_delete_instanceInboundIT).FullName,
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
				"Clas_1348178673664478AccOfer_AutoElemAcc_3_Alias",
				lAgentsActionItem2,
				"RevisionPasajero",
				typeof(InteractionToolkit.RevisionPasajero.IUServices.SIU_edit_instanceInboundIT).FullName,
				"",
				ActionItemType.Normal,
				false,
				true,
				lAttrActivationItem2);

			string[] lAgentsActionItem3 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem3 = new List<KeyValuePair<string, string[]>>();
			lActionController.Add(
				3,
				"RevisionPasajero",
				"Clas_1348178673664478AccOfer_AutoElemAcc_4_Alias",
				lAgentsActionItem3,
				"RevisionPasajero",
				typeof(InteractionToolkit.RevisionPasajero.IUInstances._Auto_IT).FullName,
				"",
				ActionItemType.Other,
				false,
				false,
				lAttrActivationItem3);

			string[] lAgentsActionItem4 = Logic.InstanceReportsList.GetAgentsForClass("RevisionPasajero");
			List<KeyValuePair<string, string[]>> lAttrActivationItem4 = new List<KeyValuePair<string, string[]>>();
			lActionController.Add(
				4,
				LanguageConstantValues.L_PRINT,
				LanguageConstantKeys.L_PRINT,
				lAgentsActionItem4,
				"RevisionPasajero",
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
				"Revision",
				"Clas_1348178673664478NavOfer_AutoElemNav_1_Alias",
				lAgentsNavigationItem0,
				"Revision",
				typeof(InteractionToolkit.Revision.IUInstances.IIU_RevisionIT).FullName,
				"RevisionPasajero",
				"",
				"Revision",
				"Clas_1348178542592347UIInst_1",
				"RevisionPasajero",
				"Clas_1348178673664478",
				"",
				lAttrActivationItem0,
				"");


			string[] lAgentsNavigationItem1 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem1 = new List<KeyValuePair<string, string[]>>();
			lNavigationController.Add(
				1,
				"PasajeroAeronave",
				"Clas_1348178673664478NavOfer_AutoElemNav_2_Alias",
				lAgentsNavigationItem1,
				"PasajeroAeronave",
				typeof(InteractionToolkit.PasajeroAeronave.IUInstances.IIU_PasajeroAeronaveIT).FullName,
				"RevisionPasajero",
				"",
				"PasajeroAeronave",
				"Clas_1348178542592177UIInst_2",
				"RevisionPasajero",
				"Clas_1348178673664478",
				"",
				lAttrActivationItem1,
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
			lDisplaySetInfo.Add("id_RevisionPasajero", "id_RevisionPasajero", "Clas_1348178673664478CjtoVis_AutoICtjoVis_1_Alias", ModelType.Autonumeric, lAgentsDSItem0, DefaultFormats.ColWidthAutonumeric);

			lDisplaySetController.CurrentDisplaySet = lDisplaySetInfo;
			return lDisplaySetController;
		}
		#endregion _Auto_

		#region DS_RevisionPasajero
		/// <summary>
		/// Creates the specific DisplaySetController of the 'DS_RevisionPasajero' DisplaySet pattern.
		/// </summary>
		/// <param name="parentController">Parent controller.</param>
		/// <returns>Specific DisplaySetController of the 'DS_RevisionPasajero' DisplaySet pattern.</returns>
		public static DisplaySetController DisplaySet_DS_RevisionPasajero(SIGEM.Client.Controllers.Controller parentController)
		{
			DisplaySetController lDisplaySetController = new DisplaySetController(parentController);
			DisplaySetInformation lDisplaySetInfo = new DisplaySetInformation("DS_RevisionPasajero");
			string[] lAgentsDSItem0 = { Agents.Administrador };
			lDisplaySetInfo.Add("PasajeroAeronave.Aeronave.id_Aeronave", "id_Aeronave", "Clas_1348178673664478CjtoVis_1ICtjoVis_1_Alias", ModelType.Autonumeric, lAgentsDSItem0, DefaultFormats.ColWidthAutonumeric);
			string[] lAgentsDSItem1 = { Agents.Administrador };
			lDisplaySetInfo.Add("PasajeroAeronave.Pasajero.id_Pasajero", "id_Pasajero", "Clas_1348178673664478CjtoVis_1ICtjoVis_2_Alias", ModelType.Autonumeric, lAgentsDSItem1, DefaultFormats.ColWidthAutonumeric);
			string[] lAgentsDSItem2 = { Agents.Administrador };
			lDisplaySetInfo.Add("PasajeroAeronave.Pasajero.Nombre", "Nombre", "Clas_1348178673664478CjtoVis_1ICtjoVis_3_Alias", ModelType.Text, lAgentsDSItem2, DefaultFormats.ColWidthText);

			lDisplaySetController.CurrentDisplaySet = lDisplaySetInfo;
			return lDisplaySetController;
		}
		#endregion DS_RevisionPasajero

		#endregion DisplaySets

		#region Filters
		#endregion Filters

		#region Population Interaction Units
		#region PIU_RevisionPasajero
		/// <summary>
		/// Creates the specific IUPopulationController of the 'PIU_RevisionPasajero' IU pattern.
		/// </summary>
		/// <param name="exchangeInfo">IUPopulationContext reference.</param>
		/// <returns>Specific IUPopulationController of the 'PIU_RevisionPasajero' IU pattern.</returns>
		public static IUPopulationController Population_PIU_RevisionPasajero(ExchangeInfo exchangeInfo)
		{
			IUPopulationContext lContext = new IUPopulationContext(exchangeInfo,"RevisionPasajero", "PIU_RevisionPasajero");
			// Block size.
			lContext.BlockSize = 40;
			IUPopulationController lController = new IUPopulationController("PIU_RevisionPasajero", "RevisionPasajero", "Clas_1348178673664478UIPobCl_1_Alias", lContext, null);

			// Action _Auto_.
			lController.Action = RevisionPasajero.Action__Auto_(lController);
			// Navigation _Auto_.
			lController.Navigation = RevisionPasajero.Navigation__Auto_(lController);
			// DisplaySet '_Auto_'.
			DisplaySetByBlocksController lDisplaySetByBlocksController = new DisplaySetByBlocksController(RevisionPasajero.DisplaySet__Auto_(lController));
			lDisplaySetByBlocksController.DisplaySetList.Add(RevisionPasajero.DisplaySet_DS_RevisionPasajero(lController).CurrentDisplaySet);
			lController.DisplaySet = lDisplaySetByBlocksController;
			// Get user preferences for this scenario.
			lController.SetPreferences(Logic.UserPreferences.GetScenarioPrefs("RevisionPasajero:PIU_RevisionPasajero") as PopulationPrefs);

			return lController;
		}
		#endregion PIU_RevisionPasajero

		#endregion Population Interaction Units

		#region Instance Interaction Units
		#region _Auto_
		/// <summary>
		/// Creates the specific IUInstanceController of the '_Auto_' IU pattern.
		/// </summary>
		/// <param name="exchangeInfo">IUInstanceContext reference.</param>
		/// <returns>Specific IUInstanceController of the '_Auto_' IU pattern.</returns>
		public static IUInstanceController Instance__Auto_(ExchangeInfo exchangeInfo)
		{
			IUInstanceContext lContext = new IUInstanceContext(exchangeInfo,"RevisionPasajero", "_Auto_");
			IUInstanceController lController = new IUInstanceController("_Auto_", "RevisionPasajero", "Clas_1348178673664478UIInst_1_Alias" ,lContext,null);

			// Action _Auto_.
			lController.Action = RevisionPasajero.Action__Auto_(lController);
			// Navigation _Auto_.
			lController.Navigation = RevisionPasajero.Navigation__Auto_(lController);
			// DisplaySet '_Auto_'.
			DisplaySetController lDisplaySetController = RevisionPasajero.DisplaySet__Auto_(lController);
			lController.DisplaySet = lDisplaySetController;

			// Oid selector.
			lController.OidSelector = new ArgumentOVController("oidSelector", "RevisionPasajero", "Clas_1348178673664478_Alias", "RevisionPasajero", false, false, false, typeof(InteractionToolkit.RevisionPasajero.IUPopulations.PIU_RevisionPasajeroIT).FullName, null, "", null);

			// Get User preferences.
			lController.SetPreferences(Logic.UserPreferences.GetScenarioPrefs("RevisionPasajero:_Auto_") as InstancePrefs);

			return lController;
		}
		#endregion _Auto_

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
			IUServiceContext lContext = new IUServiceContext(exchangeInfo, "RevisionPasajero", "create_instance", "SIU_create_instance");
			IUServiceController lController = new IUServiceController("create_instance", "New", "Clas_1348178673664478Ser_1_Alias", lAgentsService, "RevisionPasajero", "create_instance", lContext, null);

			// This controller is an InboundArgument controller.
			lController.IsOutboundArgumentController = false;


			#region Inbound arguments
			IArguments InboundArguments = lController.InputFields;
			// Argument p_atrid_RevisionPasajero.
			InboundArguments.Add(new ArgumentDVController("p_atrid_RevisionPasajero", "id_RevisionPasajero", "Clas_1348178673664478Ser_1UIServ_1ElemAgrup_1_Alias", ModelType.Autonumeric, 0, false, null, lController));
 			// Argument p_agrPasajeroAeronave.
			InboundArguments.Add(new ArgumentOVController("p_agrPasajeroAeronave", "PasajeroAeronave", "Clas_1348178673664478Ser_1UIServ_1ElemAgrup_3_Alias", "PasajeroAeronave", false, false, false , typeof(InteractionToolkit.PasajeroAeronave.IUPopulations.PIU_PasajeroAeronaveIT).FullName, null, "", lController));
			// Argument p_agrRevision.
			InboundArguments.Add(new ArgumentOVController("p_agrRevision", "Revision", "Clas_1348178673664478Ser_1UIServ_1ElemAgrup_4_Alias", "Revision", false, false, false , typeof(InteractionToolkit.Revision.IUPopulations._Auto_IT).FullName, null, "", lController));
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
			IUServiceContext lContext = new IUServiceContext(exchangeInfo, "RevisionPasajero", "delete_instance", "SIU_delete_instance");
			IUServiceController lController = new IUServiceController("delete_instance", "Destroy", "Clas_1348178673664478Ser_2_Alias", lAgentsService, "RevisionPasajero", "delete_instance", lContext, null, false);

			// This controller is an InboundArgument controller.
			lController.IsOutboundArgumentController = false;


			#region Inbound arguments
			IArguments InboundArguments = lController.InputFields;
			ArgumentOVController lArgument = null;
			// Argument p_thisRevisionPasajero.
			lArgument = new ArgumentOVController("p_thisRevisionPasajero", "RevisionPasajero", "Clas_1348178673664478Ser_2UIServ_1ElemAgrup_1_Alias", "RevisionPasajero", false, true, false, typeof(InteractionToolkit.RevisionPasajero.IUPopulations.PIU_RevisionPasajeroIT).FullName, null, "", lController);
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
			IUServiceContext lContext = new IUServiceContext(exchangeInfo, "RevisionPasajero", "edit_instance", "SIU_edit_instance");
			IUServiceController lController = new IUServiceController("edit_instance", "Edit", "Clas_1348178673664478Ser_3_Alias", lAgentsService, "RevisionPasajero", "edit_instance", lContext, null, false);

			// This controller is an InboundArgument controller.
			lController.IsOutboundArgumentController = false;


			#region Inbound arguments
			IArguments InboundArguments = lController.InputFields;
			ArgumentOVController lArgument = null;
			// Argument p_thisRevisionPasajero.
			lArgument = new ArgumentOVController("p_thisRevisionPasajero", "RevisionPasajero", "Clas_1348178673664478Ser_3UIServ_1ElemAgrup_1_Alias", "RevisionPasajero", false, true, false, typeof(InteractionToolkit.RevisionPasajero.IUPopulations.PIU_RevisionPasajeroIT).FullName, null, "", lController);
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
