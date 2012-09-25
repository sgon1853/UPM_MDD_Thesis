
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

namespace SIGEM.Client.InteractionToolkit.NaveNodriza.IUServices
{
	/// <summary>
	/// Crear_NaveNodrizaInboundIT service form of the application.
	/// </summary>
	public partial class Crear_NaveNodrizaInboundIT : Form
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
		public Crear_NaveNodrizaInboundIT()
		{
			InitializeComponent();
			
			// Icon assignament.
			this.Icon = UtilFunctions.BitmapToIcon(Properties.Resources.NaveNodriza_SIU_Crear_NaveNodriza);
		}
		#endregion Constructors
		
		#region Initialize
		/// <summary>
		/// Initializes the 'Crear_NaveNodrizaIT' instance form.
		/// </summary>
		/// <param name="exchangeInfo">Exchange information</param>
		/// <returns>IUServiceController</returns>
		public IUServiceController Initialize(ExchangeInfo exchangeInfo)
		{
			Controller = ControllerFactory.NaveNodriza.Service_create_instanceInbound(exchangeInfo, null);

			// Scenario.
			Controller.Scenario = new ScenarioPresentation(this, ScenarioType.Service);
			ArgumentDVController lArgumentDataValued = null;

			// p_atrid_NaveNodriza
			lArgumentDataValued = Controller.InputFields["p_atrid_NaveNodriza"] as ArgumentDVController;
			lArgumentDataValued.Label = new LabelPresentation(this.lp_atrid_NaveNodriza);
			lArgumentDataValued.Editor = new AutonumericPresentation(this.maskedTextBoxp_atrid_NaveNodriza, this.lp_atrid_NaveNodrizaAuto, this.checkBoxp_atrid_NaveNodrizaAuto);

			// p_atrNombre_NaveNodriza
			lArgumentDataValued = Controller.InputFields["p_atrNombre_NaveNodriza"] as ArgumentDVController;
			lArgumentDataValued.Label = new LabelPresentation(this.lp_atrNombre_NaveNodriza);
			lArgumentDataValued.Editor = new TextBoxPresentation(this.textBoxp_atrNombre_NaveNodriza);

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
