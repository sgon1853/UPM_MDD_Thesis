
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

namespace SIGEM.Client.InteractionToolkit.Revision.IUServices
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
			this.Icon = UtilFunctions.BitmapToIcon(Properties.Resources.Revision_SIU_SIU_create_instance);
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
			Controller = ControllerFactory.Revision.Service_create_instanceInbound(exchangeInfo, null);

			// Scenario.
			Controller.Scenario = new ScenarioPresentation(this, ScenarioType.Service);
			ArgumentDVController lArgumentDataValued = null;

			// p_atrid_RevisarAeronave
			lArgumentDataValued = Controller.InputFields["p_atrid_RevisarAeronave"] as ArgumentDVController;
			lArgumentDataValued.Label = new LabelPresentation(this.lp_atrid_RevisarAeronave);
			lArgumentDataValued.Editor = new AutonumericPresentation(this.maskedTextBoxp_atrid_RevisarAeronave, this.lp_atrid_RevisarAeronaveAuto, this.checkBoxp_atrid_RevisarAeronaveAuto);

			// p_atrFechaRevision
			lArgumentDataValued = Controller.InputFields["p_atrFechaRevision"] as ArgumentDVController;
			lArgumentDataValued.Label = new LabelPresentation(this.lp_atrFechaRevision);
			lArgumentDataValued.Editor = new DateTimePickerPresentation(this.maskedTextBoxp_atrFechaRevision, this.dtpp_atrFechaRevision);

			// p_atrNombreRevisor
			lArgumentDataValued = Controller.InputFields["p_atrNombreRevisor"] as ArgumentDVController;
			lArgumentDataValued.Label = new LabelPresentation(this.lp_atrNombreRevisor);
			lArgumentDataValued.Editor = new TextBoxPresentation(this.textBoxp_atrNombreRevisor);

			// p_atrId_Aeronave
			lArgumentDataValued = Controller.InputFields["p_atrId_Aeronave"] as ArgumentDVController;
			lArgumentDataValued.Label = new LabelPresentation(this.lp_atrId_Aeronave);
			lArgumentDataValued.Editor = new TextBoxPresentation(this.textBoxp_atrId_Aeronave);

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
