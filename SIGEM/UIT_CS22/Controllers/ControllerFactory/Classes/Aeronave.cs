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
	/// Class that manages the 'Aeronave' class defined in the model.
	/// </summary>
	public static class Aeronave
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
				"Crear Aeronave",
				"Clas_1348178411520734AccOfer_AutoElemAcc_1_Alias",
				lAgentsActionItem0,
				"Aeronave",
				typeof(InteractionToolkit.Aeronave.IUServices.Crear_AeronaveInboundIT).FullName, "",
				ActionItemType.Creation,
				false,
				true,
				lAttrActivationItem0);

			string[] lAgentsActionItem1 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem1 = new List<KeyValuePair<string, string[]>>();

			lActionController.Add(
				1,
				"Destroy",
				"Clas_1348178411520734AccOfer_AutoElemAcc_2_Alias",
				lAgentsActionItem1,
				"Aeronave",
				typeof(InteractionToolkit.Aeronave.IUServices.SIU_delete_instanceInboundIT).FullName,
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
				"Clas_1348178411520734AccOfer_AutoElemAcc_3_Alias",
				lAgentsActionItem2,
				"Aeronave",
				typeof(InteractionToolkit.Aeronave.IUServices.SIU_edit_instanceInboundIT).FullName,
				"",
				ActionItemType.Normal,
				false,
				true,
				lAttrActivationItem2);

			string[] lAgentsActionItem3 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem3 = new List<KeyValuePair<string, string[]>>();
			lActionController.Add(
				3,
				"Aeronave",
				"Clas_1348178411520734AccOfer_AutoElemAcc_4_Alias",
				lAgentsActionItem3,
				"Aeronave",
				typeof(InteractionToolkit.Aeronave.IUInstances._Auto_IT).FullName,
				"",
				ActionItemType.Other,
				false,
				false,
				lAttrActivationItem3);

			string[] lAgentsActionItem4 = Logic.InstanceReportsList.GetAgentsForClass("Aeronave");
			List<KeyValuePair<string, string[]>> lAttrActivationItem4 = new List<KeyValuePair<string, string[]>>();
			lActionController.Add(
				4,
				LanguageConstantValues.L_PRINT,
				LanguageConstantKeys.L_PRINT,
				lAgentsActionItem4,
				"Aeronave",
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
				"PasajeroAeronave",
				"Clas_1348178411520734NavOfer_AutoElemNav_1_Alias",
				lAgentsNavigationItem0,
				"PasajeroAeronave",
				typeof(InteractionToolkit.PasajeroAeronave.IUInstances.IIU_PasajeroAeronaveIT).FullName,
				"Aeronave",
				"",
				"PasajeroAeronave",
				"Clas_1348178542592177UIInst_2",
				"Aeronave",
				"Clas_1348178411520734",
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
			lDisplaySetInfo.Add("id_Aeronave", "id_Aeronave", "Clas_1348178411520734CjtoVis_AutoICtjoVis_1_Alias", ModelType.Autonumeric, lAgentsDSItem0, DefaultFormats.ColWidthAutonumeric);
			string[] lAgentsDSItem1 = { Agents.Administrador };
			lDisplaySetInfo.Add("Nombre", "Nombre", "Clas_1348178411520734CjtoVis_AutoICtjoVis_2_Alias", ModelType.Text, lAgentsDSItem1, DefaultFormats.ColWidthText);
			string[] lAgentsDSItem2 = { Agents.Administrador };
			lDisplaySetInfo.Add("MaximoPasajeros", "MaximoPasajeros", "Clas_1348178411520734CjtoVis_AutoICtjoVis_3_Alias", ModelType.Int, lAgentsDSItem2, DefaultFormats.ColWidthInt);
			string[] lAgentsDSItem3 = { Agents.Administrador };
			lDisplaySetInfo.Add("Origen", "Origen", "Clas_1348178411520734CjtoVis_AutoICtjoVis_4_Alias", ModelType.Text, lAgentsDSItem3, DefaultFormats.ColWidthText);
			string[] lAgentsDSItem4 = { Agents.Administrador };
			lDisplaySetInfo.Add("Destino", "Destino", "Clas_1348178411520734CjtoVis_AutoICtjoVis_5_Alias", ModelType.Text, lAgentsDSItem4, DefaultFormats.ColWidthText);

			lDisplaySetController.CurrentDisplaySet = lDisplaySetInfo;
			return lDisplaySetController;
		}
		#endregion _Auto_

		#endregion DisplaySets

		#region Filters
		#endregion Filters

		#region Population Interaction Units
		#region PIU_Aeronave
		/// <summary>
		/// Creates the specific IUPopulationController of the 'PIU_Aeronave' IU pattern.
		/// </summary>
		/// <param name="exchangeInfo">IUPopulationContext reference.</param>
		/// <returns>Specific IUPopulationController of the 'PIU_Aeronave' IU pattern.</returns>
		public static IUPopulationController Population_PIU_Aeronave(ExchangeInfo exchangeInfo)
		{
			IUPopulationContext lContext = new IUPopulationContext(exchangeInfo,"Aeronave", "PIU_Aeronave");
			// Block size.
			lContext.BlockSize = 40;
			IUPopulationController lController = new IUPopulationController("PIU_Aeronave", "Aeronave", "Clas_1348178411520734UIPobCl_1_Alias", lContext, null);

			// Action _Auto_.
			lController.Action = Aeronave.Action__Auto_(lController);
			// Navigation _Auto_.
			lController.Navigation = Aeronave.Navigation__Auto_(lController);
			// DisplaySet '_Auto_'.
			DisplaySetByBlocksController lDisplaySetByBlocksController = new DisplaySetByBlocksController(Aeronave.DisplaySet__Auto_(lController));
			lController.DisplaySet = lDisplaySetByBlocksController;
			// Get user preferences for this scenario.
			lController.SetPreferences(Logic.UserPreferences.GetScenarioPrefs("Aeronave:PIU_Aeronave") as PopulationPrefs);

			return lController;
		}
		#endregion PIU_Aeronave

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
			IUInstanceContext lContext = new IUInstanceContext(exchangeInfo,"Aeronave", "_Auto_");
			IUInstanceController lController = new IUInstanceController("_Auto_", "Aeronave", "Clas_1348178411520734UIInst_1_Alias" ,lContext,null);

			// Action _Auto_.
			lController.Action = Aeronave.Action__Auto_(lController);
			// Navigation _Auto_.
			lController.Navigation = Aeronave.Navigation__Auto_(lController);
			// DisplaySet '_Auto_'.
			DisplaySetController lDisplaySetController = Aeronave.DisplaySet__Auto_(lController);
			lController.DisplaySet = lDisplaySetController;

			// Oid selector.
			lController.OidSelector = new ArgumentOVController("oidSelector", "Aeronave", "Clas_1348178411520734_Alias", "Aeronave", false, false, false, typeof(InteractionToolkit.Aeronave.IUPopulations.PIU_AeronaveIT).FullName, null, "", null);

			// Get User preferences.
			lController.SetPreferences(Logic.UserPreferences.GetScenarioPrefs("Aeronave:_Auto_") as InstancePrefs);

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
			IUServiceContext lContext = new IUServiceContext(exchangeInfo, "Aeronave", "create_instance", "Crear_Aeronave");
			IUServiceController lController = new IUServiceController("create_instance", "New", "Clas_1348178411520734Ser_1_Alias", lAgentsService, "Aeronave", "create_instance", lContext, null);

			// This controller is an InboundArgument controller.
			lController.IsOutboundArgumentController = false;


			#region Inbound arguments
			IArguments InboundArguments = lController.InputFields;
			// Argument p_atrid_Aeronave.
			InboundArguments.Add(new ArgumentDVController("p_atrid_Aeronave", "id_Aeronave", "Clas_1348178411520734Ser_1UIServ_1ElemAgrup_1_Alias", ModelType.Autonumeric, 0, false, null, lController));
 			// Argument p_atrNombre.
			InboundArguments.Add(new ArgumentDVController("p_atrNombre", "Nombre", "Clas_1348178411520734Ser_1UIServ_1ElemAgrup_2_Alias", ModelType.Text, 0, false, null, lController));
 			// Argument p_atrMaximoPasajeros.
			InboundArguments.Add(new ArgumentDVController("p_atrMaximoPasajeros", "MaximoPasajeros", "Clas_1348178411520734Ser_1UIServ_1ElemAgrup_3_Alias", ModelType.Int, 0, false, null, lController));
 			// Argument p_atrOrigen.
			InboundArguments.Add(new ArgumentDVController("p_atrOrigen", "Origen", "Clas_1348178411520734Ser_1UIServ_1ElemAgrup_4_Alias", ModelType.Text, 0, false, null, lController));
 			// Argument p_atrDestino.
			InboundArguments.Add(new ArgumentDVController("p_atrDestino", "Destino", "Clas_1348178411520734Ser_1UIServ_1ElemAgrup_5_Alias", ModelType.Text, 0, false, null, lController));
 			// Argument p_agrPasajeroAeronave.
			InboundArguments.Add(new ArgumentOVController("p_agrPasajeroAeronave", "PasajeroAeronave", "Clas_1348178411520734Ser_1UIServ_1ElemAgrup_6_Alias", "PasajeroAeronave", false, false, false , typeof(InteractionToolkit.PasajeroAeronave.IUPopulations.PIU_PasajeroAeronaveIT).FullName, null, "", lController));
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
			IUServiceContext lContext = new IUServiceContext(exchangeInfo, "Aeronave", "delete_instance", "SIU_delete_instance");
			IUServiceController lController = new IUServiceController("delete_instance", "Destroy", "Clas_1348178411520734Ser_2_Alias", lAgentsService, "Aeronave", "delete_instance", lContext, null, false);

			// This controller is an InboundArgument controller.
			lController.IsOutboundArgumentController = false;


			#region Inbound arguments
			IArguments InboundArguments = lController.InputFields;
			ArgumentOVController lArgument = null;
			// Argument p_thisAeronave.
			lArgument = new ArgumentOVController("p_thisAeronave", "Aeronave", "Clas_1348178411520734Ser_2UIServ_1ElemAgrup_1_Alias", "Aeronave", false, true, false, typeof(InteractionToolkit.Aeronave.IUPopulations.PIU_AeronaveIT).FullName, null, "", lController);
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
			IUServiceContext lContext = new IUServiceContext(exchangeInfo, "Aeronave", "edit_instance", "SIU_edit_instance");
			IUServiceController lController = new IUServiceController("edit_instance", "Edit", "Clas_1348178411520734Ser_3_Alias", lAgentsService, "Aeronave", "edit_instance", lContext, null, false);

			// This controller is an InboundArgument controller.
			lController.IsOutboundArgumentController = false;


			#region Inbound arguments
			IArguments InboundArguments = lController.InputFields;
			ArgumentOVController lArgument = null;
			// Argument p_thisAeronave.
			lArgument = new ArgumentOVController("p_thisAeronave", "Aeronave", "Clas_1348178411520734Ser_3UIServ_1ElemAgrup_1_Alias", "Aeronave", false, true, false, typeof(InteractionToolkit.Aeronave.IUPopulations.PIU_AeronaveIT).FullName, null, "", lController);
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
