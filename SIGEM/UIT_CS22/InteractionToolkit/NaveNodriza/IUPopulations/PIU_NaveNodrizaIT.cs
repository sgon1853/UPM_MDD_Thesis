
// v3.8.4.5.b
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SIGEM.Client;
using SIGEM.Client.Controllers;
using SIGEM.Client.Presentation;
using SIGEM.Client.Presentation.Forms;

namespace SIGEM.Client.InteractionToolkit.NaveNodriza.IUPopulations
{
	/// <summary>
	/// PIU_NaveNodrizaIT population form of the application.
	/// </summary>
	public partial class PIU_NaveNodrizaIT : Form
	{
		#region Members
		/// <summary>
		/// Population controller.
		/// </summary>
		IUPopulationController mController = null;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets of population controller.
		/// </summary>
		public IUPopulationController Controller
		{
			get
			{
				return mController;

			}
			protected set
			{
				mController = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'PIU_NaveNodrizaIT' class.
		/// </summary>
		public PIU_NaveNodrizaIT()
		{
			InitializeComponent();

			// Icon assignament.
			this.Icon = UtilFunctions.BitmapToIcon(Properties.Resources.NaveNodriza_PIU_PIU_NaveNodriza);
		}
		#endregion Constructors

		#region Initialize
		/// <summary>
		/// Initializes the 'PIU_NaveNodrizaIT' instance form
		/// </summary>
		/// <param name="exchangeInfo">Exchange information.</param>
		/// <returns>IUPopulationController</returns>
		public IUPopulationController Initialize(ExchangeInfo exchangeInfo)
		{

			// Controller factory.
			Controller = ControllerFactory.NaveNodriza.Population_PIU_NaveNodriza(exchangeInfo);
			Controller.Scenario = new ScenarioPresentation(this, ScenarioType.Population);


			#region Population PIU_NaveNodriza Links

			// Displayset
			Controller.DisplaySet.Population = new DataGridViewPresentation(this.gPopulation, this.toolStripExportExcel, this.toolStripExportWord, this.toolStripRetrieveAll, this.toolStripRefresh, this.toolStripHelp, this.optionsToolStripMenuItem, null, exchangeInfo);
			Controller.DisplaySet.NumberOfInstances = new ToolStripStatusLabelNumberInstancesPresentation(this.toolStripStatusLabelCount);
			Controller.DisplaySet.First = null;
			Controller.DisplaySet.Previous = null;
			Controller.DisplaySet.Refresh = null;
			Controller.DisplaySet.Next = null;
			Controller.DisplaySet.ExecuteServiceTrigger = new ToolStripDropDownButtonPresentation(this.toolStripDropDownButtonSave);
			Controller.DisplaySet.PreferencesTrigger = new ToolStripMenuItemPresentation(this.toolStripPreferences);
			Controller.DisplaySet.SaveColumnsWidthTrigger = new ToolStripMenuItemPresentation(this.toolStripSaveColumnWidth);
			// Order Criteria

			// Actions
			Controller.Action.ActionItems[0].Trigger = new ToolStripButtonPresentation(this.toolstripActions_0,this.mnuActions_0);
			Controller.Action.ActionItems[1].Trigger = new ToolStripButtonPresentation(this.toolstripActions_1,this.mnuActions_1);
			Controller.Action.ActionItems[2].Trigger = new ToolStripButtonPresentation(this.toolstripActions_2,this.mnuActions_2);
			Controller.Action.ActionItems[3].Trigger = new ToolStripButtonPresentation(this.toolstripActions_3,this.mnuActions_3);
			Controller.Action.ActionItems[4].Trigger = new ToolStripButtonPresentation(this.toolStripPrint,this.mnuPrint);

			// Contextual Menu
			this.gPopulation.ContextMenuStrip = this.contextMenuStrip;
			#endregion Population PIU_NaveNodriza Links

			// Save position trigger
			Controller.SavePositionTrigger = new ToolStripMenuItemPresentation(this.toolStripSavePositions);

			// Ok.
			Controller.OkTrigger = new ButtonPresentation(this.bOk);
			// Cancel.
			Controller.CancelTrigger = new ButtonPresentation(this.bCancel);

			// Initialize controller.
			Controller.Initialize();

			return Controller;
		}
		#endregion Initialize
	}
}
