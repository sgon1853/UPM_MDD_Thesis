
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
using SIGEM.Client.ControllerFactory;

namespace SIGEM.Client.InteractionToolkit.Pasajero.IUServices
{
	/// <summary>
	/// SIU_edit_instanceInboundIT service form of the application.
	/// </summary>
	public partial class SIU_edit_instanceInboundIT : Form
	{
		#region Members
		/// <summary>
		/// Service Controller.
		/// </summary>
		IUServiceController mController = null;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets the service controller.
		/// </summary>
		public IUServiceController Controller
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
		/// Initializes a new instance of the '${ServiceIU.PresentationIU.Name}InboundIT' class.
		/// </summary>
		public SIU_edit_instanceInboundIT()
		{
			InitializeComponent();
			
			// Icon assignament.
			this.Icon = UtilFunctions.BitmapToIcon(Properties.Resources.Pasajero_SIU_SIU_edit_instance);
		}
		#endregion Constructors
		
		#region Initialize
		/// <summary>
		/// Initializes the 'SIU_edit_instanceIT' instance form.
		/// </summary>
		/// <param name="exchangeInfo">Exchange information</param>
		/// <returns>IUServiceController</returns>
		public IUServiceController Initialize(ExchangeInfo exchangeInfo)
		{
			Controller = ControllerFactory.Pasajero.Service_edit_instanceInbound(exchangeInfo, null);

			// Scenario.
			Controller.Scenario = new ScenarioPresentation(this, ScenarioType.Service);
			ArgumentOVController lArgumentObjectValued = null;

			// p_thisPasajero
			lArgumentObjectValued = Controller.InputFields["p_thisPasajero"] as ArgumentOVController;
			lArgumentObjectValued.Label = new LabelPresentation(this.lp_thisPasajero);
			lArgumentObjectValued.Editors[0] = new MaskedTextBoxPresentation(this.maskedTextBoxp_thisPasajeroid_Pasajero1);
			lArgumentObjectValued.Trigger = new ButtonPresentation(this.bp_thisPasajero);
			if (lArgumentObjectValued.SupplementaryInfo != null)
			{
				lArgumentObjectValued.SupplementaryInfo.Viewer = new LabelDisplaySetPresentation(this.lp_thisPasajeroSupInfo);
			}

			// Next-Previous triggers.
			Controller.NextTrigger = new ButtonPresentation(this.bNext);
			Controller.PreviousTrigger = new ButtonPresentation(this.bPrevious);
			Controller.ApplyTrigger = new ButtonPresentation(this.bApply);

			// Ok.
			Controller.OkTrigger = new ButtonPresentation(this.bOk);

			// Cancel.
			Controller.CancelTrigger = new ButtonPresentation(this.bCancel);

			Controller.Initialize();
			
			return Controller;
		}
		#endregion Initialize
	}
}
