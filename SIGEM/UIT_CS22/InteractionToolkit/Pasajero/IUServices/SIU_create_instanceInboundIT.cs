
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
	/// SIU_create_instanceInboundIT service form of the application.
	/// </summary>
	public partial class SIU_create_instanceInboundIT : Form
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
		public SIU_create_instanceInboundIT()
		{
			InitializeComponent();
			
			// Icon assignament.
			this.Icon = UtilFunctions.BitmapToIcon(Properties.Resources.Pasajero_SIU_SIU_create_instance);
		}
		#endregion Constructors
		
		#region Initialize
		/// <summary>
		/// Initializes the 'SIU_create_instanceIT' instance form.
		/// </summary>
		/// <param name="exchangeInfo">Exchange information</param>
		/// <returns>IUServiceController</returns>
		public IUServiceController Initialize(ExchangeInfo exchangeInfo)
		{
			Controller = ControllerFactory.Pasajero.Service_create_instanceInbound(exchangeInfo, null);

			// Scenario.
			Controller.Scenario = new ScenarioPresentation(this, ScenarioType.Service);
			ArgumentDVController lArgumentDataValued = null;
			ArgumentOVController lArgumentObjectValued = null;

			// p_atrid_Pasajero
			lArgumentDataValued = Controller.InputFields["p_atrid_Pasajero"] as ArgumentDVController;
			lArgumentDataValued.Label = new LabelPresentation(this.lp_atrid_Pasajero);
			lArgumentDataValued.Editor = new AutonumericPresentation(this.maskedTextBoxp_atrid_Pasajero, this.lp_atrid_PasajeroAuto, this.checkBoxp_atrid_PasajeroAuto);

			// p_atrNombre
			lArgumentDataValued = Controller.InputFields["p_atrNombre"] as ArgumentDVController;
			lArgumentDataValued.Label = new LabelPresentation(this.lp_atrNombre);
			lArgumentDataValued.Editor = new TextBoxPresentation(this.textBoxp_atrNombre, this.bp_atrNombreEnlarge);

			// p_agrPasajeroAeronave
			lArgumentObjectValued = Controller.InputFields["p_agrPasajeroAeronave"] as ArgumentOVController;
			lArgumentObjectValued.Label = new LabelPresentation(this.lp_agrPasajeroAeronave);
			lArgumentObjectValued.Editors[0] = new MaskedTextBoxPresentation(this.maskedTextBoxp_agrPasajeroAeronaveid_PasajeroAeronave1);
			lArgumentObjectValued.Trigger = new ButtonPresentation(this.bp_agrPasajeroAeronave);
			if (lArgumentObjectValued.SupplementaryInfo != null)
			{
				lArgumentObjectValued.SupplementaryInfo.Viewer = new LabelDisplaySetPresentation(this.lp_agrPasajeroAeronaveSupInfo);
			}

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
