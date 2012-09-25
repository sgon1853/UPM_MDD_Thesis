
// v3.8.4.5.b
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SIGEM.Client.Controllers;
using SIGEM.Client.Presentation;
using SIGEM.Client.Presentation.Forms;

namespace SIGEM.Client.InteractionToolkit.PasajeroAeronave.IUMasterDetails
{
	/// <summary>
	/// ${OnPresentationIU.Name}IT master-detail form of the application.
	/// </summary>
	public partial class MDIU_PasajeroAeronaveIT : Form
	{
		#region Member
		/// <summary>
		/// Master-Detail Controller.
		/// </summary>
		IUMasterDetailController mController = null;
		#endregion Member

		#region Properties
		/// <summary>
		/// Gets or sets de master-detail controller.
		/// </summary>
		public IUMasterDetailController Controller
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
		/// Initializes a new instance of the 'MDIU_PasajeroAeronaveIT' class.
		/// </summary>
		public MDIU_PasajeroAeronaveIT()
		{
			InitializeComponent();

			// Icon assignament.
			this.Icon = UtilFunctions.BitmapToIcon(Properties.Resources.PasajeroAeronave_MDIU_MDIU_PasajeroAeronave);
		}
		#endregion Constructors
		
		#region Initialize
		/// <summary>
		/// Initializes the 'MDIU_PasajeroAeronaveIT' instance form.
		/// </summary>
		/// <param name="exchangeInfo">Exchange information</param>
		/// <returns>IUMasterDetailController</returns>
		public IUMasterDetailController Initialize(ExchangeInfo exchangeInfo)
		{
			Controller = ControllerFactory.PasajeroAeronave.MasterDetail_MDIU_PasajeroAeronave(exchangeInfo);

			Controller.Scenario = new ScenarioPresentation(this, ScenarioType.MasterDetail);


			#region Master-Detail MDIU_PasajeroAeronave Links

			IUInstanceController ControllerMaster = Controller.Master as IUInstanceController;
			#region Instance IIU_PasajeroAeronave Links
			// DisplaySet
			ControllerMaster.DisplaySet.Viewer = new ListViewPresentation(lstViewDisplaySetMaster, MastertoolStripExportExcel, MastertoolStripExportWord, MastertoolStripRefresh, MastertoolStripHelp, MasteroptionsToolStripMenuItem, mnuNavigationsMaster);
			// OID Selector
            ControllerMaster.OidSelector.Label = new LabelPresentation(this.lOIDSelectorMaster);
			ControllerMaster.OidSelector.Editors[0] = new MaskedTextBoxPresentation(this.maskedTextBoxMasterid_PasajeroAeronave1);
			ControllerMaster.OidSelector.Trigger = new ButtonPresentation(this.bOIDSelectorMaster);
			if (ControllerMaster.OidSelector.SupplementaryInfo != null)
			{
				ControllerMaster.OidSelector.SupplementaryInfo.Viewer = new LabelDisplaySetPresentation(this.lSIOIDSelectorMaster);
			}

			// Actions
			ControllerMaster.Action.ActionItems[0].Trigger = new ToolStripButtonPresentation(this.toolstripActionsMaster_0, this.mnuActionsMaster_0);
			ControllerMaster.Action.ActionItems[1].Trigger = new ToolStripButtonPresentation(this.toolstripActionsMaster_1, this.mnuActionsMaster_1);
			ControllerMaster.Action.ActionItems[2].Trigger = new ToolStripButtonPresentation(this.toolstripActionsMaster_2, this.mnuActionsMaster_2);
			ControllerMaster.Action.ActionItems[3].Trigger = new ToolStripButtonPresentation(this.toolstripActionsMaster_3, this.mnuActionsMaster_3);
			ControllerMaster.Action.ActionItems[4].Trigger = new ToolStripButtonPresentation(this.toolstripActionsMaster_4, this.mnuActionsMaster_4);
			ControllerMaster.Action.ActionItems[5].Trigger = new ToolStripButtonPresentation(this.toolStripPrintMaster, this.mnuPrintMaster);

			// Navigations
			ControllerMaster.Navigation.NavigationItems[0].Trigger = new ToolStripButtonPresentation(this.toolstripNavigationsMaster_0, this.mnuNavigationsMaster_0);
			ControllerMaster.Navigation.NavigationItems[1].Trigger = new ToolStripButtonPresentation(this.toolstripNavigationsMaster_1, this.mnuNavigationsMaster_1);
			ControllerMaster.Navigation.NavigationItems[2].Trigger = new ToolStripButtonPresentation(this.toolstripNavigationsMaster_2, this.mnuNavigationsMaster_2);
			this.pnlDisplaySetMaster.ContextMenuStrip = MastercontextMenuStrip;
			ControllerMaster.DisplaySet.PreferencesTrigger = new ToolStripMenuItemPresentation(this.MastertoolStripPreferences);
			#endregion Instance IIU_PasajeroAeronave Links
	
			// Save position
			Controller.SavePositionTrigger = new ToolStripMenuItemPresentation(MastertoolStripSavePositions);

			IUPopulationController ControllerDetail0 = Controller.Details[0] as IUPopulationController;
			#region Population PIU_Pasajero Links

			// Displayset
			ControllerDetail0.DisplaySet.Population = new DataGridViewPresentation(this.gPopulationDetail0, this.Detail0toolStripExportExcel, this.Detail0toolStripExportWord, this.Detail0toolStripRetrieveAll, this.Detail0toolStripRefresh, this.Detail0toolStripHelp, this.Detail0optionsToolStripMenuItem, this.mnuNavigationsDetail0, exchangeInfo);
			ControllerDetail0.DisplaySet.NumberOfInstances = new ToolStripStatusLabelNumberInstancesPresentation(this.toolStripStatusLabelCountDetail0);
			ControllerDetail0.DisplaySet.First = null;
			ControllerDetail0.DisplaySet.Previous = null;
			ControllerDetail0.DisplaySet.Refresh = null;
			ControllerDetail0.DisplaySet.Next = null;
			ControllerDetail0.DisplaySet.ExecuteServiceTrigger = new ToolStripDropDownButtonPresentation(this.toolStripDropDownButtonSaveDetail0);
			ControllerDetail0.DisplaySet.PreferencesTrigger = new ToolStripMenuItemPresentation(this.Detail0toolStripPreferences);
			ControllerDetail0.DisplaySet.SaveColumnsWidthTrigger = new ToolStripMenuItemPresentation(this.Detail0toolStripSaveColumnWidth);
			// Order Criteria

			// Actions
			ControllerDetail0.Action.ActionItems[0].Trigger = new ToolStripButtonPresentation(this.toolstripActionsDetail0_0,this.mnuActionsDetail0_0);
			ControllerDetail0.Action.ActionItems[1].Trigger = new ToolStripButtonPresentation(this.toolstripActionsDetail0_1,this.mnuActionsDetail0_1);
			ControllerDetail0.Action.ActionItems[2].Trigger = new ToolStripButtonPresentation(this.toolstripActionsDetail0_2,this.mnuActionsDetail0_2);
			ControllerDetail0.Action.ActionItems[3].Trigger = new ToolStripButtonPresentation(this.toolstripActionsDetail0_3,this.mnuActionsDetail0_3);
			ControllerDetail0.Action.ActionItems[4].Trigger = new ToolStripButtonPresentation(this.toolStripPrintDetail0,this.mnuPrintDetail0);

			// Navigations
			ControllerDetail0.Navigation.NavigationItems[0].Trigger = new ToolStripButtonPresentation(this.toolstripNavigationsDetail0_0, this.mnuNavigationsDetail0_0);

			// Contextual Menu
			this.gPopulationDetail0.ContextMenuStrip = this.Detail0contextMenuStrip;
			#endregion Population PIU_Pasajero Links
			#endregion Master-Detail MDIU_PasajeroAeronave Links

			// Close button
			Controller.CancelTrigger = new ButtonPresentation(this.bCancel);
			
			// Initialize scenario
			Controller.Initialize();
			
			return Controller;
		}
		#endregion Initialize
	}
}
