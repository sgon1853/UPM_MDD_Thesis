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
	/// Class that manages the 'NaveNodriza' class defined in the model.
	/// </summary>
	public static class NaveNodriza
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
				"Crear nave nodriza",
				"Clas_1347649273856884AccOfer_AutoElemAcc_1_Alias",
				lAgentsActionItem0,
				"NaveNodriza",
				typeof(InteractionToolkit.NaveNodriza.IUServices.Crear_NaveNodrizaInboundIT).FullName, "",
				ActionItemType.Creation,
				false,
				true,
				lAttrActivationItem0);

			string[] lAgentsActionItem1 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem1 = new List<KeyValuePair<string, string[]>>();

			lActionController.Add(
				1,
				"Destroy",
				"Clas_1347649273856884AccOfer_AutoElemAcc_2_Alias",
				lAgentsActionItem1,
				"NaveNodriza",
				typeof(InteractionToolkit.NaveNodriza.IUServices.SIU_delete_instanceInboundIT).FullName,
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
				"Clas_1347649273856884AccOfer_AutoElemAcc_3_Alias",
				lAgentsActionItem2,
				"NaveNodriza",
				typeof(InteractionToolkit.NaveNodriza.IUServices.SIU_edit_instanceInboundIT).FullName,
				"",
				ActionItemType.Normal,
				false,
				true,
				lAttrActivationItem2);

			string[] lAgentsActionItem3 = { Agents.Administrador };
			List<KeyValuePair<string, string[]>> lAttrActivationItem3 = new List<KeyValuePair<string, string[]>>();
			lActionController.Add(
				3,
				"NaveNodriza",
				"Clas_1347649273856884AccOfer_AutoElemAcc_4_Alias",
				lAgentsActionItem3,
				"NaveNodriza",
				typeof(InteractionToolkit.NaveNodriza.IUInstances._Auto_IT).FullName,
				"",
				ActionItemType.Other,
				false,
				false,
				lAttrActivationItem3);

			string[] lAgentsActionItem4 = Logic.InstanceReportsList.GetAgentsForClass("NaveNodriza");
			List<KeyValuePair<string, string[]>> lAttrActivationItem4 = new List<KeyValuePair<string, string[]>>();
			lActionController.Add(
				4,
				LanguageConstantValues.L_PRINT,
				LanguageConstantKeys.L_PRINT,
				lAgentsActionItem4,
				"NaveNodriza",
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
			lDisplaySetInfo.Add("id_NaveNodriza", "id_NaveNodriza", "Clas_1347649273856884CjtoVis_AutoICtjoVis_1_Alias", ModelType.Autonumeric, lAgentsDSItem0, DefaultFormats.ColWidthAutonumeric);
			string[] lAgentsDSItem1 = { Agents.Administrador };
			lDisplaySetInfo.Add("Nombre_NaveNodriza", "Nombre_NaveNodriza", "Clas_1347649273856884CjtoVis_AutoICtjoVis_2_Alias", ModelType.String, lAgentsDSItem1, DefaultFormats.ColWidthString20);

			lDisplaySetController.CurrentDisplaySet = lDisplaySetInfo;
			return lDisplaySetController;
		}
		#endregion _Auto_

		#endregion DisplaySets

		#region Filters
		#endregion Filters

		#region Population Interaction Units
		#region PIU_NaveNodriza
		/// <summary>
		/// Creates the specific IUPopulationController of the 'PIU_NaveNodriza' IU pattern.
		/// </summary>
		/// <param name="exchangeInfo">IUPopulationContext reference.</param>
		/// <returns>Specific IUPopulationController of the 'PIU_NaveNodriza' IU pattern.</returns>
		public static IUPopulationController Population_PIU_NaveNodriza(ExchangeInfo exchangeInfo)
		{
			IUPopulationContext lContext = new IUPopulationContext(exchangeInfo,"NaveNodriza", "PIU_NaveNodriza");
			// Block size.
			lContext.BlockSize = 40;
			IUPopulationController lController = new IUPopulationController("PIU_NaveNodriza", "PIU_NaveNodriza", "Clas_1347649273856884UIPobCl_1_Alias", lContext, null);

			// Action _Auto_.
			lController.Action = NaveNodriza.Action__Auto_(lController);
			// DisplaySet '_Auto_'.
			DisplaySetByBlocksController lDisplaySetByBlocksController = new DisplaySetByBlocksController(NaveNodriza.DisplaySet__Auto_(lController));
			lController.DisplaySet = lDisplaySetByBlocksController;
			// Get user preferences for this scenario.
			lController.SetPreferences(Logic.UserPreferences.GetScenarioPrefs("NaveNodriza:PIU_NaveNodriza") as PopulationPrefs);

			return lController;
		}
		#endregion PIU_NaveNodriza

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
			IUInstanceContext lContext = new IUInstanceContext(exchangeInfo,"NaveNodriza", "_Auto_");
			IUInstanceController lController = new IUInstanceController("_Auto_", "NaveNodriza", "Clas_1347649273856884UIInst_1_Alias" ,lContext,null);

			// Action _Auto_.
			lController.Action = NaveNodriza.Action__Auto_(lController);
			// DisplaySet '_Auto_'.
			DisplaySetController lDisplaySetController = NaveNodriza.DisplaySet__Auto_(lController);
			lController.DisplaySet = lDisplaySetController;

			// Oid selector.
			lController.OidSelector = new ArgumentOVController("oidSelector", "NaveNodriza", "Clas_1347649273856884_Alias", "NaveNodriza", false, false, false, typeof(InteractionToolkit.NaveNodriza.IUPopulations.PIU_NaveNodrizaIT).FullName, null, "", null);

			// Get User preferences.
			lController.SetPreferences(Logic.UserPreferences.GetScenarioPrefs("NaveNodriza:_Auto_") as InstancePrefs);

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
			IUServiceContext lContext = new IUServiceContext(exchangeInfo, "NaveNodriza", "create_instance", "Crear_NaveNodriza");
			IUServiceController lController = new IUServiceController("create_instance", "New", "Clas_1347649273856884Ser_1_Alias", lAgentsService, "NaveNodriza", "create_instance", lContext, null);

			// This controller is an InboundArgument controller.
			lController.IsOutboundArgumentController = false;


			#region Inbound arguments
			IArguments InboundArguments = lController.InputFields;
			// Argument p_atrid_NaveNodriza.
			InboundArguments.Add(new ArgumentDVController("p_atrid_NaveNodriza", "id_NaveNodriza", "Clas_1347649273856884Ser_1UIServ_1ElemAgrup_1_Alias", ModelType.Autonumeric, 0, false, null, lController));
 			// Argument p_atrNombre_NaveNodriza.
			InboundArguments.Add(new ArgumentDVController("p_atrNombre_NaveNodriza", "Nombre_NaveNodriza", "Clas_1347649273856884Ser_1UIServ_1ElemAgrup_2_Alias", ModelType.String, 100, false, null, lController));
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
			IUServiceContext lContext = new IUServiceContext(exchangeInfo, "NaveNodriza", "delete_instance", "SIU_delete_instance");
			IUServiceController lController = new IUServiceController("delete_instance", "Destroy", "Clas_1347649273856884Ser_2_Alias", lAgentsService, "NaveNodriza", "delete_instance", lContext, null, false);

			// This controller is an InboundArgument controller.
			lController.IsOutboundArgumentController = false;


			#region Inbound arguments
			IArguments InboundArguments = lController.InputFields;
			ArgumentOVController lArgument = null;
			// Argument p_thisNaveNodriza.
			lArgument = new ArgumentOVController("p_thisNaveNodriza", "NaveNodriza", "Clas_1347649273856884Ser_2UIServ_1ElemAgrup_1_Alias", "NaveNodriza", false, true, false, typeof(InteractionToolkit.NaveNodriza.IUPopulations.PIU_NaveNodrizaIT).FullName, null, "", lController);
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
			IUServiceContext lContext = new IUServiceContext(exchangeInfo, "NaveNodriza", "edit_instance", "SIU_edit_instance");
			IUServiceController lController = new IUServiceController("edit_instance", "Edit", "Clas_1347649273856884Ser_3_Alias", lAgentsService, "NaveNodriza", "edit_instance", lContext, null, false);

			// This controller is an InboundArgument controller.
			lController.IsOutboundArgumentController = false;


			#region Inbound arguments
			IArguments InboundArguments = lController.InputFields;
			ArgumentOVController lArgument = null;
			// Argument p_thisNaveNodriza.
			lArgument = new ArgumentOVController("p_thisNaveNodriza", "NaveNodriza", "Clas_1347649273856884Ser_3UIServ_1ElemAgrup_1_Alias", "NaveNodriza", false, true, false, typeof(InteractionToolkit.NaveNodriza.IUPopulations.PIU_NaveNodrizaIT).FullName, null, "", lController);
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
