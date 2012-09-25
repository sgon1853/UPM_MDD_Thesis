 // v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Logics;
using SIGEM.Client.Controllers;
using SIGEM.Client.Presentation;
using SIGEM.Client.Oids;
using SIGEM.Client.InteractionToolkit;

namespace SIGEM.Client.ControllerFactory
{
	/// <summary>
	/// Class that manages the 'MainScenario' of the application.
	/// </summary>
	public static class MainScenario
	{
		/// <summary>
		/// Creates the main controller.
		/// </summary>
		/// <returns>IUMainController.</returns>
		public static IUMainController MainController()
		{
			// Create main context.
			IUMainContext lMainContext = new IUMainContext();

			// Create main controller.
			IUMainController lController = new IUMainController(lMainContext);

			#region Link menu items
			// Item: 0
			string[] lAgentsHatItem0 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatNodeController(
				"0",
				"Aeronave",
				"Vis_1348605968384256NodoJer_4_Alias",
				lAgentsHatItem0));

			// Item: 0_0
			string[] lAgentsHatItem0_0 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatLeafController(
				"0_0",
				"Crear Aeronave",
				"Vis_1348605968384256NodoJer_3_Alias",
				"Aeronave",
				typeof(InteractionToolkit.Aeronave.IUServices.Crear_AeronaveInboundIT).FullName,
				lAgentsHatItem0_0,
				""));

			// Item: 0_1
			string[] lAgentsHatItem0_1 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatLeafController(
				"0_1",
				"Aeronave",
				"Vis_1348605968384256NodoJer_5_Alias",
				"Aeronave",
				typeof(InteractionToolkit.Aeronave.IUPopulations.PIU_AeronaveIT).FullName,
				lAgentsHatItem0_1,
				""));

			// Item: 1
			string[] lAgentsHatItem1 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatNodeController(
				"1",
				"Nave Nodriza",
				"Vis_1348605968384256NodoJer_6_Alias",
				lAgentsHatItem1));

			// Item: 1_0
			string[] lAgentsHatItem1_0 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatLeafController(
				"1_0",
				"NaveNodriza",
				"Vis_1348605968384256NodoJer_7_Alias",
				"NaveNodriza",
				typeof(InteractionToolkit.NaveNodriza.IUPopulations.PIU_NaveNodrizaIT).FullName,
				lAgentsHatItem1_0,
				""));

			// Item: 1_1
			string[] lAgentsHatItem1_1 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatLeafController(
				"1_1",
				"Crear nave nodriza",
				"Vis_1348605968384256NodoJer_2_Alias",
				"NaveNodriza",
				typeof(InteractionToolkit.NaveNodriza.IUServices.Crear_NaveNodrizaInboundIT).FullName,
				lAgentsHatItem1_1,
				""));

			// Item: 2
			string[] lAgentsHatItem2 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatNodeController(
				"2",
				"Pasajero",
				"Vis_1348605968384256NodoJer_8_Alias",
				lAgentsHatItem2));

			// Item: 2_0
			string[] lAgentsHatItem2_0 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatLeafController(
				"2_0",
				"New",
				"Vis_1348605968384256NodoJer_9_Alias",
				"Pasajero",
				typeof(InteractionToolkit.Pasajero.IUServices.SIU_create_instanceInboundIT).FullName,
				lAgentsHatItem2_0,
				""));

			// Item: 2_1
			string[] lAgentsHatItem2_1 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatLeafController(
				"2_1",
				"Pasajero",
				"Vis_1348605968384256NodoJer_10_Alias",
				"Pasajero",
				typeof(InteractionToolkit.Pasajero.IUPopulations.PIU_PasajeroIT).FullName,
				lAgentsHatItem2_1,
				""));

			// Item: 3
			string[] lAgentsHatItem3 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatNodeController(
				"3",
				"Ocupacion aeronave",
				"Vis_1348605968384256NodoJer_11_Alias",
				lAgentsHatItem3));

			// Item: 3_0
			string[] lAgentsHatItem3_0 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatLeafController(
				"3_0",
				"New",
				"Vis_1348605968384256NodoJer_12_Alias",
				"PasajeroAeronave",
				typeof(InteractionToolkit.PasajeroAeronave.IUServices.SIU_create_instanceInboundIT).FullName,
				lAgentsHatItem3_0,
				""));

			// Item: 3_1
			string[] lAgentsHatItem3_1 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatLeafController(
				"3_1",
				"PasajeroAeronave",
				"Vis_1348605968384256NodoJer_13_Alias",
				"PasajeroAeronave",
				typeof(InteractionToolkit.PasajeroAeronave.IUMasterDetails.MDIU_PasajeroAeronaveIT).FullName,
				lAgentsHatItem3_1,
				""));

			// Item: 4
			string[] lAgentsHatItem4 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatNodeController(
				"4",
				"Revision Aeronave",
				"Vis_1348605968384256NodoJer_14_Alias",
				lAgentsHatItem4));

			// Item: 4_0
			string[] lAgentsHatItem4_0 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatLeafController(
				"4_0",
				"New",
				"Vis_1348605968384256NodoJer_15_Alias",
				"Revision",
				typeof(InteractionToolkit.Revision.IUServices.SIU_create_instanceInboundIT).FullName,
				lAgentsHatItem4_0,
				""));

			// Item: 4_1
			string[] lAgentsHatItem4_1 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatLeafController(
				"4_1",
				"RevisionPasajero",
				"Vis_1348605968384256NodoJer_16_Alias",
				"RevisionPasajero",
				typeof(InteractionToolkit.RevisionPasajero.IUPopulations.PIU_RevisionPasajeroIT).FullName,
				lAgentsHatItem4_1,
				""));

			// Item: 4_2
			string[] lAgentsHatItem4_2 = { Agents.Administrador };
			lController.HatElementNodes.Add(new HatLeafController(
				"4_2",
				"Revision",
				"Vis_1348605968384256NodoJer_17_Alias",
				"Revision",
				typeof(InteractionToolkit.Revision.IUInstances.IIU_RevisionIT).FullName,
				lAgentsHatItem4_2,
				""));

			#endregion Link menu items

			return lController;
		}
	}
}
