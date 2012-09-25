// v3.8.4.5.b
using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Windows.Forms;
using SIGEM.Client.Presentation;
using SIGEM.Client.Logics;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the IUMasterDetailController.
	/// </summary>
	public class IUMasterDetailController : IUController, IDetailController
	{
		#region Members
		/// <summary>
		/// Master IU.
		/// </summary>
		protected IUQueryController mMaster = null;
		/// <summary>
		///	Details collection.
		/// </summary>
		protected List<IDetailController> mDetails = null;
		/// <summary>
		/// Scenario alias as Detail of a Master-Detail
		/// </summary>
		private string mDetailAlias = string.Empty;
		/// <summary>
		/// Id of the Scenario alias as Detail
		/// </summary>
		private string mIdXMLDetailAlias = string.Empty;
		/// <summary>
		/// Graphical container of the detail
		/// </summary>
		private ITriggerPresentation mDetailContainer;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'IUMasterDetailController' class.
		/// </summary>
		/// <param name="name">Name of the Interaction Unit.</param>
		/// <param name="alias">Alias of the Interaction Unit.</param>
		/// <param name="idXML">IdXML of the Interaction Unit.</param>
		/// <param name="context">Context.</param>
		/// <param name="parent">Parent controller.</param>
		public IUMasterDetailController(string name, string alias, string idXML, IUMasterDetailContext context, IUController parent)
			: base()
		{
			Name = name;
			Alias = alias;
			IdXML = idXML;
			Context = context;
			Parent = parent;

			// Create Collection to insert details.
			mDetails = new List<IDetailController>();
		}
		#endregion Constructors

		#region Properties

		#region ExchangeInformation
		/// <summary>
		/// Gets or sets the ExchangeInformation.
		/// </summary>
		public override ExchangeInfo ExchangeInformation
		{
			get
			{
				if (Master != null)
				{
					return Master.ExchangeInformation;
				}
				return null;
			}
			set
			{
				if (Master != null)
				{
					Master.ExchangeInformation = value;
				}
			}
		}
		#endregion ExchangeInformation

		#region Master
		/// <summary>
		/// Gets or sets the master interaction unit.
		/// </summary>
		public IUQueryController Master
		{
			get
			{
				return mMaster;
			}
			set
			{
				if (Master != null)
				{
					Master.SelectedInstanceChanged -= new EventHandler<SelectedInstanceChangedEventArgs>(HandleMasterSelectedInstanceChanged);
					Master.RefreshMasterRequired -= new EventHandler<RefreshRequiredMasterEventArgs>(HandleMasterRefreshMasterRequired);
					Master.CheckForPendingChanges -= new EventHandler<CheckForPendingChangesEventArgs>(HandleMasterCheckForPendingChanges);
                    Master.CloseMainScenario -= new EventHandler<EventArgs>(HandleCloseMainScenario);
                }
				mMaster = value;
				if (Master != null)
				{
					Master.SelectedInstanceChanged += new EventHandler<SelectedInstanceChangedEventArgs>(HandleMasterSelectedInstanceChanged);
					Master.RefreshMasterRequired += new EventHandler<RefreshRequiredMasterEventArgs>(HandleMasterRefreshMasterRequired);
					Master.CheckForPendingChanges += new EventHandler<CheckForPendingChangesEventArgs>(HandleMasterCheckForPendingChanges);
                    Master.CloseMainScenario += new EventHandler<EventArgs>(HandleCloseMainScenario);
				}
			}
		}
		#endregion Master

		#region Details
		/// <summary>
		///	Gets the details collection of the interaction unit.
		/// </summary>
		public virtual List<IDetailController> Details
		{
			get
			{
				return mDetails;
			}
			protected set
			{
				mDetails = value;
			}
		} 
		#endregion Details

		#region InstancesSelected
		/// <summary>
		/// Get the selected instances of the interaction unit.
		/// </summary>
		public override List<Oid> InstancesSelected
		{
			get
			{
				// Return selected instance of the master
				if (Master != null)
					return Master.InstancesSelected;

				// Default value
				return null;
			}
		}
		#endregion InstancesSelected

		#region Detail
		/// <summary>
		/// Scenario alias as Detail of a Master-Detail
		/// </summary>
		public string DetailAlias
		{
			get
			{
				return mDetailAlias;
			}
			set
			{
				mDetailAlias = value;
				if (DetailContainer != null)
				{
					DetailContainer.Value = mDetailAlias;
				}
			}
		}
		/// <summary>
		/// Id of the Scenario alias as Detail
		/// </summary>
		public string IdXMLDetailAlias
		{
			get
			{
				return mIdXMLDetailAlias;
			}
			set
			{
				mIdXMLDetailAlias = value;
			}
		}
		/// <summary>
		/// Graphical container of the detail
		/// </summary>
		public ITriggerPresentation DetailContainer
		{
			get
			{
				return mDetailContainer;
			}
			set
			{
				if (mDetailContainer != null)
				{
					mDetailContainer.Triggered -= new EventHandler<TriggerEventArgs>(HandleDetailContainerTriggered);
				}
				mDetailContainer = value;
				if (mDetailContainer != null)
				{
					mDetailContainer.Triggered += new EventHandler<TriggerEventArgs>(HandleDetailContainerTriggered);
				}
			}
		}
		#endregion Detail
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when the Master must be refreshed.
		/// </summary>
		public event EventHandler<RefreshRequiredMasterEventArgs> RefreshMasterRequired;
		/// <summary>
		/// Occurs when this Detail must be refreshed.
		/// </summary>
		public event EventHandler<EventArgs> RefreshDetailRequired;
        /// <summary>
        /// Occurs when main scenario must be closed
        /// </summary>
        public event EventHandler<EventArgs> CloseMainScenario;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Occurs when the selected instance changes.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">SelectedInstanceChangedEventArgs.</param>
		private void HandleMasterSelectedInstanceChanged(object sender, SelectedInstanceChangedEventArgs e)
		{
			// Update context
			UpdateContext();

			// Update details
			ExchangeInfoNavigation lInfoNav;
			foreach (IDetailController lDetail in Details)
			{
				ExchangeInfo lExchgInfo = lDetail.ExchangeInformation;
				lInfoNav = new ExchangeInfoNavigation(lExchgInfo as ExchangeInfoNavigation);
                if (lDetail.IsActiveDetail() && e.SelectedInstances != null && e.SelectedInstances.Count == 1)
				{
					lInfoNav.SelectedOids = e.SelectedInstances;
				}
				else
				{
					lInfoNav.SelectedOids = null;
				}
				lDetail.ExchangeInformation = lInfoNav;
				ClearLastOids(lDetail as IUQueryController);
				lDetail.UpdateData(true);
			}
		}
		/// <summary>
		/// Handles the RefreshMasterRequired event from its Master
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleMasterRefreshMasterRequired(object sender, RefreshRequiredMasterEventArgs e)
		{
			// Propagate the Event
			OnRefreshMasterRequired(e);
		}
		/// <summary>
		/// Handles the RefeshMasterRequired from one detail
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleDetailRefreshMasterRequired(object sender, RefreshRequiredMasterEventArgs e)
		{
            if (RefreshMasterRequired != null)
            {
                RefreshRequiredMasterEventArgs args = new RefreshRequiredMasterEventArgs(Master.ExchangeInformation);
                OnRefreshMasterRequired(args);
                if (args.RefreshDone)
                {
                    e.RefreshDone = true;
                    return;
                }
            }
			// Refresh the Master
            if (Master.Context.SelectedOids != null && Master.Context.SelectedOids.Count > 0)
            {
                bool lRefreshSuccess = Master.RefreshOidList(Master.Context.SelectedOids);
                if (Master.Context.RelatedOids != null &&
                    Master.Context.RelatedOids.Count == 1)
                {
                    Master.OnRefreshRequired(new RefreshRequiredInstancesEventArgs(Master.Context.RelatedOids, null));
                }
                e.RefreshDone = lRefreshSuccess;
            }
		}
		/// <summary>
		/// Handles the CheckForPendingChanges event in the Master.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleMasterCheckForPendingChanges(object sender, CheckForPendingChangesEventArgs e)
		{
			e.Cancel = !CheckPendingChanges(false, false);
		}
		/// <summary>
		/// Handles the RefreshDetailRequired event for any detail.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleDetailRefreshDetailRequired(object sender, EventArgs e)
		{
			ProcessDetailRefreshDetailRequired(sender);
		}
		/// <summary>
		/// Handles the detail container Triggered event. Detail container becomes active.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleDetailContainerTriggered(object sender, TriggerEventArgs e)
		{
			// Raise the event only if there are no data.
			if (Master.Context.ExchangeInformation.SelectedOids == null ||
				Master.Context.ExchangeInformation.SelectedOids.Count == 0)
			{
				OnRefreshDetailRequired(new EventArgs());
			}
		}
        /// <summary>
        /// Handles the close scenario event. Main scenario must be closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleCloseMainScenario(object sender, EventArgs e)
        {
            if (Scenario != null)
            {
                CloseScenario();
            }
            else
            {
                OnCloseMainScenario(new EventArgs());                
            }
        }
		#endregion Event Handlers

		#region Process events
		/// <summary>
		/// Actions related with the Form closing.
		/// </summary>
		/// <param name="e"></param>
		protected override void ProcessScenarioFormClosing(FormClosingEventArgs e)
		{
			e.Cancel = !CheckPendingChanges(true, true);
		}
		/// <summary>
		/// Process the RefreshDetailRequired event for any detail.
		/// </summary>
		/// <param name="sender"></param>
		protected void ProcessDetailRefreshDetailRequired(object sender)
		{
			IDetailController lDetail = sender as IDetailController;

			if (lDetail == null)
				return;

			// Update the detail.
			ExchangeInfoNavigation lInfoNav;
			ExchangeInfo lExchgInfo = lDetail.ExchangeInformation;
			lInfoNav = new ExchangeInfoNavigation(lExchgInfo as ExchangeInfoNavigation);
			if (Master.InstancesSelected != null && Master.InstancesSelected.Count == 1)
			{
				lInfoNav.SelectedOids = Master.InstancesSelected;
			}
			else
			{
				lInfoNav.SelectedOids = null;
			}
			lDetail.ExchangeInformation = lInfoNav;
			ClearLastOids(lDetail as IUQueryController);
			lDetail.UpdateData(true);
		}
		#endregion Process events

		#region Event Raisers
		/// <summary>
		/// Raises the RefreshMasterRequired event.
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnRefreshMasterRequired(RefreshRequiredMasterEventArgs eventArgs)
		{
			EventHandler<RefreshRequiredMasterEventArgs> handler = RefreshMasterRequired;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the RefreshDetailRequired event.
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnRefreshDetailRequired(EventArgs eventArgs)
		{
			EventHandler<EventArgs> handler = RefreshDetailRequired;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
        /// <summary>
        /// Raises the CloseScenario event.
        /// </summary>
        /// <param name="eventArgs"></param>
        protected void OnCloseMainScenario(EventArgs eventArgs)
        {
            EventHandler<EventArgs> handler = CloseMainScenario;

            if (handler != null)
            {
                handler(this, eventArgs);
            }
        }
        #endregion Event Raisers

		#region Methods

		#region Initialize
		/// <summary>
		/// Initializes the controller.
		/// </summary>
		public override void Initialize()
		{
			foreach (IUController lDetail in Details)
			{
				lDetail.Initialize();
			}
			Master.Initialize();
			
			base.Initialize();
		}
		#endregion Initialize

		#region ApplyMultilanguage
		/// <summary>
		/// Apply multilanguage to the scenario.
		/// </summary>
		public override void ApplyMultilanguage()
		{
			base.ApplyMultilanguage();

			// Translate the alias as a detail.
			if (IdXMLDetailAlias != "")
			{
				DetailAlias = CultureManager.TranslateString(IdXMLDetailAlias, DetailAlias);
			}

			// Cancel button.
			if (this.CancelTrigger != null)
			{
				this.CancelTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_CLOSE, LanguageConstantValues.L_CLOSE, this.CancelTrigger.Value.ToString());
			}
		}
		#endregion ApplyMultilanguage

		#region Update Context
		/// <summary>
		/// Updates the context.
		/// </summary>
		public override void UpdateContext()
		{
			Master.UpdateContext();
		}
		#endregion Update Context

		#region UpdateData
		/// <summary>
		/// Update the data of the interaction unit.
		/// </summary>
		public void UpdateData(bool refresh)
		{
			// Update master interaction unit.
			if (Master != null)
			{
				Master.UpdateData(refresh);
			}
		}
		#endregion UpdateData

		#region ConfigureByContext
		/// <summary>
		/// Configures the scenarion depending on the context.
		/// </summary>
		/// <param name="context">Context.</param>
		public override void ConfigureByContext(IUContext context)
		{
			IUMasterDetailContext lContext = context as IUMasterDetailContext;

			if (lContext.Master == null)
				return;
				
			// Master
			Master.ConfigureByContext(lContext.Master);

			// Details
			for (int i = 0; i < lContext.Details.Count; i++)
			{
				Details[i].ConfigureByContext(lContext.Details[i]);
			}

			// Default
			base.ConfigureByContext(context);
		}
		#endregion ConfigureByContext

		#region Subitem management
		/// <summary>
		/// Adds a detail unit to the interaction unit.
		/// </summary>
		/// <param name="controllerDetail">Detail controller</param>
		/// <returns>IUController</returns>
		public IDetailController AddDetail(IDetailController controllerDetail, string detailAlias, string idXMLDetailAlias)
		{
			Details.Add(controllerDetail);
			controllerDetail.RefreshMasterRequired += new EventHandler<RefreshRequiredMasterEventArgs>(HandleDetailRefreshMasterRequired);
			controllerDetail.DetailAlias = detailAlias;
			controllerDetail.IdXMLDetailAlias = idXMLDetailAlias;
			controllerDetail.RefreshDetailRequired += new EventHandler<EventArgs>(HandleDetailRefreshDetailRequired);
            controllerDetail.CloseMainScenario += new EventHandler<EventArgs>(HandleCloseMainScenario);
			return controllerDetail;
		}
		#endregion Subitem management

		#region Clear Last Oids
		/// <summary>
		/// Initialize pagination state, for detail of this master.
		/// </summary>
		/// <param name="controller">Detail pattern controller.</param>
		/// <returns></returns>
		private bool ClearLastOids(IDetailController controller)
		{
			if (controller != null)
			{
				switch (controller.Context.ContextType)
				{
					case ContextType.Population:
						IUPopulationController lPopulationController = controller as IUPopulationController;
						if (lPopulationController != null)
						{
							if (lPopulationController.Context != null)
							{
								lPopulationController.Context.LastOids.Clear();
								return true;
							}
						}
						return false;

					case ContextType.MasterDetail:
						IUMasterDetailController masterDetailController = controller as IUMasterDetailController;
						if (masterDetailController != null)
						{
							ClearLastOids(masterDetailController.Master);

							foreach (IDetailController lDetail in masterDetailController.Details)
							{
								if (!ClearLastOids(lDetail as IUMasterDetailController))
								{
									ClearLastOids(lDetail);
								}
							}
							return true;
						}
						break;
					default:
						return true;

				}
			}

			return true;
		}
		
		#endregion Clear Last Oids

		/// <summary>
		/// Check if there is any change pending in the Master or Details
		/// Returns False if the user wants to cancel the action
		/// </summary>
		/// <returns></returns>
		public bool CheckPendingChanges(bool searchChangesInThisDS, bool searchChangesInAssociatedSIU)
		{
			if (searchChangesInThisDS && Master.DisplaySet.PendingChanges)
			{
				string message = CultureManager.TranslateString(LanguageConstantKeys.L_PENDINGCHANGES, LanguageConstantValues.L_PENDINGCHANGES);
				if (MessageBox.Show(message, Master.Alias, MessageBoxButtons.YesNo) == DialogResult.No)
				{
					return false;
				}
				// No pending changes in the Master or details
				Master.DisplaySet.PendingChanges = false;
			}

			foreach (IDetailController detail in Details)
			{
				if (!detail.CheckPendingChanges(true, searchChangesInAssociatedSIU))
				{
					return false;
				}
			}

			return true;
		}
		public void Refresh()
		{
			this.Master.Refresh();
		}
		/// <summary>
		/// Returns true if the detail is active.
		/// </summary>
		public bool IsActiveDetail()
		{
			if (DetailContainer == null)
			{
				return true;
			}

			return DetailContainer.Visible;
		}
		#endregion Methods
	}
}



