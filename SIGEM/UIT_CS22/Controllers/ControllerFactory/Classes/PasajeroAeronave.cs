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
	/// Class that manages the 'PasajeroAeronave' class defined in the model.
	/// </summary>
	public static class PasajeroAeronave
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
				"Clas_1348178542592177AccOfer_AutoElemAcc_1_Alias",
				lAgentsActionItem0,
				"PasajeroAeronave",
				typeof(InteractionToolkit.PasajeroAeronave.IUServices.SIU_create_instanceInboundIT).FullName, "",
				ActionItemType.Creation,
				false,
				true,
				lAttrActivationItem0);

			string[] lAgentsActionItem1 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem1 = new List<KeyValuePair<string, string[]>>();

			lActionController.Add(
				1,
				"Destroy",
				"Clas_1348178542592177AccOfer_AutoElemAcc_2_Alias",
				lAgentsActionItem1,
				"PasajeroAeronave",
				typeof(InteractionToolkit.PasajeroAeronave.IUServices.SIU_delete_instanceInboundIT).FullName,
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
				"Clas_1348178542592177AccOfer_AutoElemAcc_3_Alias",
				lAgentsActionItem2,
				"PasajeroAeronave",
				typeof(InteractionToolkit.PasajeroAeronave.IUServices.SIU_edit_instanceInboundIT).FullName,
				"",
				ActionItemType.Normal,
				false,
				true,
				lAttrActivationItem2);

			string[] lAgentsActionItem3 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem3 = new List<KeyValuePair<string, string[]>>();
			lActionController.Add(
				3,
				"PasajeroAeronave",
				"Clas_1348178542592177AccOfer_AutoElemAcc_4_Alias",
				lAgentsActionItem3,
				"PasajeroAeronave",
				typeof(InteractionToolkit.PasajeroAeronave.IUInstances.IIU_PasajeroAeronaveIT).FullName,
				"",
				ActionItemType.Other,
				false,
				false,
				lAttrActivationItem3);

			string[] lAgentsActionItem4 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem4 = new List<KeyValuePair<string, string[]>>();
			lActionController.Add(
				4,
				"PasajeroAeronave",
				"Clas_1348178542592177AccOfer_AutoElemAcc_5_Alias",
				lAgentsActionItem4,
				"PasajeroAeronave",
				typeof(InteractionToolkit.PasajeroAeronave.IUMasterDetails.MDIU_PasajeroAeronaveIT).FullName,
				"",
				ActionItemType.Other,
				false,
				false,
				lAttrActivationItem4);

			string[] lAgentsActionItem5 = Logic.InstanceReportsList.GetAgentsForClass("PasajeroAeronave");
			List<KeyValuePair<string, string[]>> lAttrActivationItem5 = new List<KeyValuePair<string, string[]>>();
			lActionController.Add(
				5,
				LanguageConstantValues.L_PRINT,
				LanguageConstantKeys.L_PRINT,
				lAgentsActionItem5,
				"PasajeroAeronave",
				typeof(InteractionToolkit.PrintForm).FullName,
				"",
				ActionItemType.Print,
				true,
				true,
				lAttrActivationItem5);

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
				"Clas_1348178542592177NavOfer_AutoElemNav_1_Alias",
				lAgentsNavigationItem0,
				"RevisionPasajero",
				typeof(InteractionToolkit.RevisionPasajero.IUInstances._Auto_IT).FullName,
				"PasajeroAeronave",
				"",
				"RevisionPasajero",
				"Clas_1348178673664478UIInst_1",
				"PasajeroAeronave",
				"Clas_1348178542592177",
				"",
				lAttrActivationItem0,
				"");


			string[] lAgentsNavigationItem1 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem1 = new List<KeyValuePair<string, string[]>>();
			lNavigationController.Add(
				1,
				"Aeronave",
				"Clas_1348178542592177NavOfer_AutoElemNav_2_Alias",
				lAgentsNavigationItem1,
				"Aeronave",
				typeof(InteractionToolkit.Aeronave.IUInstances._Auto_IT).FullName,
				"PasajeroAeronave",
				"",
				"Aeronave",
				"Clas_1348178411520734UIInst_1",
				"PasajeroAeronave",
				"Clas_1348178542592177",
				"",
				lAttrActivationItem1,
				"");


			string[] lAgentsNavigationItem2 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem2 = new List<KeyValuePair<string, string[]>>();
			lNavigationController.Add(
				2,
				"Pasajero",
				"Clas_1348178542592177NavOfer_AutoElemNav_3_Alias",
				lAgentsNavigationItem2,
				"Pasajero",
				typeof(InteractionToolkit.Pasajero.IUInstances._Auto_IT).FullName,
				"PasajeroAeronave",
				"",
				"Pasajero",
				"Clas_1348178542592658UIInst_1",
				"PasajeroAeronave",
				"Clas_1348178542592177",
				"",
				lAttrActivationItem2,
				"");


			return lNavigationController;
		}
		#endregion _Auto_
		#endregion Navigations

		#region Order Criterias
		#endregion Order Criterias

		#region DisplaySets
		#region DS_PasajeroAeronave
		/// <summary>
		/// Creates the specific DisplaySetController of the 'DS_PasajeroAeronave' DisplaySet pattern.
		/// </summary>
		/// <param name="parentController">Parent controller.</param>
		/// <returns>Specific DisplaySetController of the 'DS_PasajeroAeronave' DisplaySet pattern.</returns>
		public static DisplaySetController DisplaySet_DS_PasajeroAeronave(SIGEM.Client.Controllers.Controller parentController)
		{
			DisplaySetController lDisplaySetController = new DisplaySetController(parentController);
			DisplaySetInformation lDisplaySetInfo = new DisplaySetInformation("DS_PasajeroAeronave");
			string[] lAgentsDSItem0 = { Agents.Administrador };
			lDisplaySetInfo.Add("Aeronave.id_Aeronave", "id_Aeronave", "Clas_1348178542592177CjtoVis_1ICtjoVis_1_Alias", ModelType.Autonumeric, lAgentsDSItem0, DefaultFormats.ColWidthAutonumeric);

			lDisplaySetController.CurrentDisplaySet = lDisplaySetInfo;
			return lDisplaySetController;
		}
		#endregion DS_PasajeroAeronave

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
			lDisplaySetInfo.Add("id_PasajeroAeronave", "id_PasajeroAeronave", "Clas_1348178542592177CjtoVis_AutoICtjoVis_1_Alias", ModelType.Autonumeric, lAgentsDSItem0, DefaultFormats.ColWidthAutonumeric);
			string[] lAgentsDSItem1 = { Agents.Administrador };
			lDisplaySetInfo.Add("NombreAeronave", "NombreAeronave", "Clas_1348178542592177CjtoVis_AutoICtjoVis_2_Alias", ModelType.String, lAgentsDSItem1, DefaultFormats.ColWidthString20);
			string[] lAgentsDSItem2 = { Agents.Administrador };
			lDisplaySetInfo.Add("NombrePasajero", "NombrePasajero", "Clas_1348178542592177CjtoVis_AutoICtjoVis_3_Alias", ModelType.String, lAgentsDSItem2, DefaultFormats.ColWidthString20);

			lDisplaySetController.CurrentDisplaySet = lDisplaySetInfo;
			return lDisplaySetController;
		}
		#endregion _Auto_

		#region DS_PasajeroAeronave1
		/// <summary>
		/// Creates the specific DisplaySetController of the 'DS_PasajeroAeronave1' DisplaySet pattern.
		/// </summary>
		/// <param name="parentController">Parent controller.</param>
		/// <returns>Specific DisplaySetController of the 'DS_PasajeroAeronave1' DisplaySet pattern.</returns>
		public static DisplaySetController DisplaySet_DS_PasajeroAeronave1(SIGEM.Client.Controllers.Controller parentController)
		{
			DisplaySetController lDisplaySetController = new DisplaySetController(parentController);
			DisplaySetInformation lDisplaySetInfo = new DisplaySetInformation("DS_PasajeroAeronave1");
			string[] lAgentsDSItem0 = { Agents.Administrador };
			lDisplaySetInfo.Add("Pasajero.id_Pasajero", "id_Pasajero", "Clas_1348178542592177CjtoVis_2ICtjoVis_1_Alias", ModelType.Autonumeric, lAgentsDSItem0, DefaultFormats.ColWidthAutonumeric);
			string[] lAgentsDSItem1 = { Agents.Administrador };
			lDisplaySetInfo.Add("Pasajero.Nombre", "Nombre", "Clas_1348178542592177CjtoVis_2ICtjoVis_2_Alias", ModelType.Text, lAgentsDSItem1, DefaultFormats.ColWidthText);

			lDisplaySetController.CurrentDisplaySet = lDisplaySetInfo;
			return lDisplaySetController;
		}
		#endregion DS_PasajeroAeronave1

		#endregion DisplaySets

		#region Filters
		#endregion Filters

		#region Population Interaction Units
		#region PIU_PasajeroAeronave
		/// <summary>
		/// Creates the specific IUPopulationController of the 'PIU_PasajeroAeronave' IU pattern.
		/// </summary>
		/// <param name="exchangeInfo">IUPopulationContext reference.</param>
		/// <returns>Specific IUPopulationController of the 'PIU_PasajeroAeronave' IU pattern.</returns>
		public static IUPopulationController Population_PIU_PasajeroAeronave(ExchangeInfo exchangeInfo)
		{
			IUPopulationContext lContext = new IUPopulationContext(exchangeInfo,"PasajeroAeronave", "PIU_PasajeroAeronave");
			// Block size.
			lContext.BlockSize = 40;
			IUPopulationController lController = new IUPopulationController("PIU_PasajeroAeronave", "PasajeroAeronave", "Clas_1348178542592177UIPobCl_1_Alias", lContext, null);

			// Action _Auto_.
			lController.Action = PasajeroAeronave.Action__Auto_(lController);
			// Navigation _Auto_.
			lController.Navigation = PasajeroAeronave.Navigation__Auto_(lController);
			// DisplaySet '_Auto_'.
			DisplaySetByBlocksController lDisplaySetByBlocksController = new DisplaySetByBlocksController(PasajeroAeronave.DisplaySet__Auto_(lController));
			lDisplaySetByBlocksController.DisplaySetList.Add(PasajeroAeronave.DisplaySet_DS_PasajeroAeronave1(lController).CurrentDisplaySet);
			lController.DisplaySet = lDisplaySetByBlocksController;
			// Get user preferences for this scenario.
			lController.SetPreferences(Logic.UserPreferences.GetScenarioPrefs("PasajeroAeronave:PIU_PasajeroAeronave") as PopulationPrefs);

			return lController;
		}
		#endregion PIU_PasajeroAeronave

		#endregion Population Interaction Units

		#region Instance Interaction Units
		#region IIU_PasajeroAeronave
		/// <summary>
		/// Creates the specific IUInstanceController of the 'IIU_PasajeroAeronave' IU pattern.
		/// </summary>
		/// <param name="exchangeInfo">IUInstanceContext reference.</param>
		/// <returns>Specific IUInstanceController of the 'IIU_PasajeroAeronave' IU pattern.</returns>
		public static IUInstanceController Instance_IIU_PasajeroAeronave(ExchangeInfo exchangeInfo)
		{
			IUInstanceContext lContext = new IUInstanceContext(exchangeInfo,"PasajeroAeronave", "IIU_PasajeroAeronave");
			IUInstanceController lController = new IUInstanceController("IIU_PasajeroAeronave", "PasajeroAeronave", "Clas_1348178542592177UIInst_2_Alias" ,lContext,null);

			// Action _Auto_.
			lController.Action = PasajeroAeronave.Action__Auto_(lController);
			// Navigation _Auto_.
			lController.Navigation = PasajeroAeronave.Navigation__Auto_(lController);
			// DisplaySet 'DS_PasajeroAeronave'.
			DisplaySetController lDisplaySetController = PasajeroAeronave.DisplaySet_DS_PasajeroAeronave(lController);
			lController.DisplaySet = lDisplaySetController;

			// Oid selector.
			lController.OidSelector = new ArgumentOVController("oidSelector", "PasajeroAeronave", "Clas_1348178542592177_Alias", "PasajeroAeronave", false, false, false, typeof(InteractionToolkit.PasajeroAeronave.IUPopulations.PIU_PasajeroAeronaveIT).FullName, null, "", null);

			// Get User preferences.
			lController.SetPreferences(Logic.UserPreferences.GetScenarioPrefs("PasajeroAeronave:IIU_PasajeroAeronave") as InstancePrefs);

			return lController;
		}
		#endregion IIU_PasajeroAeronave

		#endregion Instance Interaction Units

		#region M-D Interaction Units

		#region MDIU_PasajeroAeronave
		/// <summary>
		/// Creates the specific IUMasterDetailController of the 'MDIU_PasajeroAeronave' IU pattern.
		/// </summary>
		/// <param name="exchangeInfo">Exchange information received.</param>
		/// <returns>Specific IUMasterDetailController of the 'MDIU_PasajeroAeronave' IU pattern.</returns>
		public static IUMasterDetailController MasterDetail_MDIU_PasajeroAeronave(ExchangeInfo exchangeInfo)
		{
			IUMasterDetailContext lContext = new IUMasterDetailContext("PasajeroAeronave", "MDIU_PasajeroAeronave");
			IUMasterDetailController lController = new IUMasterDetailController("MDIU_PasajeroAeronave", "PasajeroAeronave", "Clas_1348178542592177UIMaDet_1_Alias", lContext, null);

			// Master interaction unit.
			lController.Master = Instance_IIU_PasajeroAeronave(exchangeInfo);

			// Detail interaction units.
			ExchangeInfoNavigation infoDetail0 = new ExchangeInfoNavigation("Pasajero", "", "PasajeroAeronave","", null, null, "");
			lController.AddDetail(Pasajero.Population_PIU_Pasajero(infoDetail0), "Pasajero", "Clas_1348178542592177UIMaDet_1Det_2_Alias");

			return lController;
		}
		#endregion MDIU_PasajeroAeronave

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
			IUServiceContext lContext = new IUServiceContext(exchangeInfo, "PasajeroAeronave", "create_instance", "SIU_create_instance");
			IUServiceController lController = new IUServiceController("create_instance", "New", "Clas_1348178542592177Ser_1_Alias", lAgentsService, "PasajeroAeronave", "create_instance", lContext, null);

			// This controller is an InboundArgument controller.
			lController.IsOutboundArgumentController = false;


			#region Inbound arguments
			IArguments InboundArguments = lController.InputFields;
			// Argument p_atrid_PasajeroAeronave.
			InboundArguments.Add(new ArgumentDVController("p_atrid_PasajeroAeronave", "id_PasajeroAeronave", "Clas_1348178542592177Ser_1UIServ_1ElemAgrup_1_Alias", ModelType.Autonumeric, 0, false, null, lController));
 			// Argument p_agrAeronave.
			InboundArguments.Add(new ArgumentOVController("p_agrAeronave", "Aeronave", "Clas_1348178542592177Ser_1UIServ_1ElemAgrup_5_Alias", "Aeronave", true, false, false , typeof(InteractionToolkit.Aeronave.IUPopulations.PIU_AeronaveIT).FullName, null, "", lController));
			// Argument p_agrPasajero.
			InboundArguments.Add(new ArgumentOVController("p_agrPasajero", "Pasajero", "Clas_1348178542592177Ser_1UIServ_1ElemAgrup_6_Alias", "Pasajero", true, false, false , typeof(InteractionToolkit.Pasajero.IUPopulations.PIU_PasajeroIT).FullName, null, "", lController));
			// Argument p_atrNombreAeronave.
			InboundArguments.Add(new ArgumentDVController("p_atrNombreAeronave", "NombreAeronave", "Clas_1348178542592177Ser_1UIServ_1ElemAgrup_8_Alias", ModelType.String, 100, false, null, lController));
 			// Argument p_atrNombrePasajero.
			InboundArguments.Add(new ArgumentDVController("p_atrNombrePasajero", "NombrePasajero", "Clas_1348178542592177Ser_1UIServ_1ElemAgrup_9_Alias", ModelType.String, 100, false, null, lController));
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
			IUServiceContext lContext = new IUServiceContext(exchangeInfo, "PasajeroAeronave", "delete_instance", "SIU_delete_instance");
			IUServiceController lController = new IUServiceController("delete_instance", "Destroy", "Clas_1348178542592177Ser_2_Alias", lAgentsService, "PasajeroAeronave", "delete_instance", lContext, null, false);

			// This controller is an InboundArgument controller.
			lController.IsOutboundArgumentController = false;


			#region Inbound arguments
			IArguments InboundArguments = lController.InputFields;
			ArgumentOVController lArgument = null;
			// Argument p_thisPasajeroAeronave.
			lArgument = new ArgumentOVController("p_thisPasajeroAeronave", "PasajeroAeronave", "Clas_1348178542592177Ser_2UIServ_1ElemAgrup_1_Alias", "PasajeroAeronave", false, true, false, typeof(InteractionToolkit.PasajeroAeronave.IUPopulations.PIU_PasajeroAeronaveIT).FullName, null, "", lController);
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
			IUServiceContext lContext = new IUServiceContext(exchangeInfo, "PasajeroAeronave", "edit_instance", "SIU_edit_instance");
			IUServiceController lController = new IUServiceController("edit_instance", "Edit", "Clas_1348178542592177Ser_3_Alias", lAgentsService, "PasajeroAeronave", "edit_instance", lContext, null, false);

			// This controller is an InboundArgument controller.
			lController.IsOutboundArgumentController = false;


			#region Inbound arguments
			IArguments InboundArguments = lController.InputFields;
			ArgumentOVController lArgument = null;
			// Argument p_thisPasajeroAeronave.
			lArgument = new ArgumentOVController("p_thisPasajeroAeronave", "PasajeroAeronave", "Clas_1348178542592177Ser_3UIServ_1ElemAgrup_1_Alias", "PasajeroAeronave", false, true, false, typeof(InteractionToolkit.PasajeroAeronave.IUPopulations.PIU_PasajeroAeronaveIT).FullName, null, "", lController);
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
