
// v3.8.4.5.b
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SIGEM.Client.Controllers;
using SIGEM.Client.Presentation;
using SIGEM.Client.Presentation.Forms;

namespace SIGEM.Client.InteractionToolkit.Revision.IUInstances
{
	/// <summary>
	/// IIU_RevisionIT instance form of the application.
	/// </summary>
	public partial class IIU_RevisionIT : Form
	{
		#region Members
		/// <summary>
		/// Instance controller.
		/// </summary>
		IUInstanceController mController = null;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets the instance controller.
		/// </summary>
		public IUInstanceController Controller
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
		/// Initializes a new instance of the 'IIU_RevisionIT' class.
		/// </summary>
		public IIU_RevisionIT()
		{
			InitializeComponent();

			// Icon assignament.
			this.Icon = UtilFunctions.BitmapToIcon(Properties.Resources.Revision_IIU_IIU_Revision);
		}
		#endregion Constructors

		#region Initialize
		/// <summary>
		/// Initializes the 'IIU_RevisionIT' instance form.
		/// </summary>
		/// <param name="exchangeInfo">Exchange information.</param>
		/// <returns>IUInstanceController.</returns>
		public IUInstanceController Initialize(ExchangeInfo exchangeInfo)
		{
			// Controller factory.
			Controller = ControllerFactory.Revision.Instance_IIU_Revision(exchangeInfo);

			Controller.Scenario = new ScenarioPresentation(this, ScenarioType.Instance);


			#region Instance IIU_Revision Links
			// DisplaySet
			Controller.DisplaySet.Viewer = new ListViewPresentation(lstViewDisplaySet, toolStripExportExcel, toolStripExportWord, toolStripRefresh, toolStripHelp, optionsToolStripMenuItem, mnuNavigations);
			// OID Selector
            Controller.OidSelector.Label = new LabelPresentation(this.lOIDSelector);
			Controller.OidSelector.Editors[0] = new MaskedTextBoxPresentation(this.maskedTextBoxid_RevisarAeronave1);
			Controller.OidSelector.Trigger = new ButtonPresentation(this.bOIDSelector);
			if (Controller.OidSelector.SupplementaryInfo != null)
			{
				Controller.OidSelector.SupplementaryInfo.Viewer = new LabelDisplaySetPresentation(this.lSIOIDSelector);
			}

			// Actions
			Controller.Action.ActionItems[0].Trigger = new ToolStripButtonPresentation(this.toolstripActions_0, this.mnuActions_0);
			Controller.Action.ActionItems[1].Trigger = new ToolStripButtonPresentation(this.toolstripActions_1, this.mnuActions_1);
			Controller.Action.ActionItems[2].Trigger = new ToolStripButtonPresentation(this.toolstripActions_2, this.mnuActions_2);
			Controller.Action.ActionItems[3].Trigger = new ToolStripButtonPresentation(this.toolstripActions_3, this.mnuActions_3);
			Controller.Action.ActionItems[4].Trigger = new ToolStripButtonPresentation(this.toolStripPrint, this.mnuPrint);

			// Navigations
			Controller.Navigation.NavigationItems[0].Trigger = new ToolStripButtonPresentation(this.toolstripNavigations_0, this.mnuNavigations_0);
			this.pnlDisplaySet.ContextMenuStrip = contextMenuStrip;
			Controller.DisplaySet.PreferencesTrigger = new ToolStripMenuItemPresentation(this.toolStripPreferences);
			#endregion Instance IIU_Revision Links

			// Save position trigger
			Controller.SavePositionTrigger = new ToolStripMenuItemPresentation(toolStripSavePositions);

			// Close Button
			Controller.CancelTrigger = new ButtonPresentation(this.bCancel);
			
			// Initialize controller.
			Controller.Initialize();
			
			return Controller;
		}
		#endregion Initialize

	}
}
