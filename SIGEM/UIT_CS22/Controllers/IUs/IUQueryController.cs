// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;

using SIGEM.Client.Presentation;
using SIGEM.Client.Oids;
using SIGEM.Client.Logics;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the IUQueryController.
	/// </summary>
	public abstract class IUQueryController : IUController, IDetailController, INavigationItemEvents
	{
		#region Members
		/// <summary>
		/// Action pattern of the interaction unit.
		/// </summary>
		private ActionController mAction = null;
		/// <summary>
		/// Navigation pattern of the interaction unit.
		/// </summary>
		private NavigationController mNavigation = null;
		/// <summary>
		/// DisplaySet pattern of the interaction unit.
		/// </summary>
		private DisplaySetController mDisplaySet  = null;
		/// <summary>
		/// Associated service controller
		/// </summary>
		private IUServiceController mAssociatedServiceController;
		/// <summary>
		/// Presentation of the Associated service clear trigger
		/// </summary>
		private ITriggerPresentation mAssociatedServiceClearTrigger = null;
		/// <summary>
		/// Enable the service when the scenario is displayed.
		/// </summary>
		private bool mStartServiceEnabled;
		/// <summary>
		/// Scenario alias as Detail of a Master-Detail
		/// </summary>
		private string mDetailAlias = string.Empty;
		/// <summary>
		/// Id of the Scenario alias as Detail
		/// </summary>
		private string mIdXMLDetailAlias = string.Empty;
		/// <summary>
		/// Graphical container of the detail.
		/// </summary>
		private ITriggerPresentation mDetailContainer;
		/// <summary>
		/// Used to ignore the change selection event from the Viewer
		/// </summary>
		private bool mFlagIgnoreSelectionChange = false;
		#endregion Members

		#region Properties

		#region Action Controller Items
		/// <summary>
		/// Gets or sets the IU Action.
		/// </summary>
		public ActionController Action
		{
			get
			{
				return mAction;
			}
			set
			{
				if (mAction != null)
				{
					mAction.RefreshRequired -= new EventHandler<RefreshRequiredEventArgs>(HandleActionRefreshRequired);
					mAction.SelectNextPreviousInstance -= new EventHandler<SelectNextPreviousInstanceEventArgs>(HandleActionSelectNextPreviousInstance);
					mAction.BeforeActionItemExecution -= new EventHandler<CheckForPendingChangesEventArgs>(HandleActionBeforeActionItemExecution);
					mAction.SelectedInstancesRequired -= new EventHandler<SelectedInstancesRequiredEventArgs>(HandleActionNavigationSelectedInstancesRequired);
					mAction.ContextRequired -= new EventHandler<ContextRequiredEventArgs>(HandleActionNavigationContextRequired);
				}
				mAction = value;
				if (mAction != null)
				{
					mAction.RefreshRequired += new EventHandler<RefreshRequiredEventArgs>(HandleActionRefreshRequired);
					mAction.SelectNextPreviousInstance += new EventHandler<SelectNextPreviousInstanceEventArgs>(HandleActionSelectNextPreviousInstance);
					mAction.BeforeActionItemExecution += new EventHandler<CheckForPendingChangesEventArgs>(HandleActionBeforeActionItemExecution);
					mAction.SelectedInstancesRequired += new EventHandler<SelectedInstancesRequiredEventArgs>(HandleActionNavigationSelectedInstancesRequired);
					mAction.ContextRequired += new EventHandler<ContextRequiredEventArgs>(HandleActionNavigationContextRequired);
				}
			}
		}

		#endregion Action Controller Items

		#region Navigation Controller Items
		/// <summary>
		/// Gets or sets the IU Navigation.
		/// </summary>
		public NavigationController Navigation
		{
			get
			{
				return mNavigation;
			}
			set
			{
				if (mNavigation != null)
				{
					mNavigation.SelectedInstancesRequired -= new EventHandler<SelectedInstancesRequiredEventArgs>(HandleActionNavigationSelectedInstancesRequired);
					mNavigation.ContextRequired -= new EventHandler<ContextRequiredEventArgs>(HandleActionNavigationContextRequired);
					mNavigation.RefreshRequired -= new EventHandler<RefreshRequiredEventArgs>(HandleNavigationRefreshRequired);
				}
				mNavigation = value;
				if (mNavigation != null)
				{
					mNavigation.SelectedInstancesRequired += new EventHandler<SelectedInstancesRequiredEventArgs>(HandleActionNavigationSelectedInstancesRequired);
					mNavigation.ContextRequired += new EventHandler<ContextRequiredEventArgs>(HandleActionNavigationContextRequired);
					mNavigation.RefreshRequired += new EventHandler<RefreshRequiredEventArgs>(HandleNavigationRefreshRequired);
				}
			}
		}
		#endregion Navigation Controller Items

		#region Display Set Controller
		/// <summary>
		/// Gets or sets the IU DisplaySet.
		/// </summary>
		public DisplaySetController DisplaySet
		{
			get
			{
				return mDisplaySet;
			}
			set
			{
				if(mDisplaySet != null)
				{
					mDisplaySet.SelectedInstanceChanged -= new EventHandler<SelectedInstanceChangedEventArgs>(HandleDisplaySetSelectedInstanceChanged);
					mDisplaySet.ExecuteCommand -= new EventHandler<ExecuteCommandEventArgs>(HandleDisplaySetExecuteCommand);
				}
				mDisplaySet = value;
				if (mDisplaySet != null)
				{
					mDisplaySet.SelectedInstanceChanged += new EventHandler<SelectedInstanceChangedEventArgs>(HandleDisplaySetSelectedInstanceChanged);
					mDisplaySet.ExecuteCommand += new EventHandler<ExecuteCommandEventArgs>(HandleDisplaySetExecuteCommand);
				}
			}
		}
		#endregion Display Set Controller

		#region InstancesSelected --> DisplaySet.Values
		/// <summary>
		/// Gets the selected IU instances.
		/// </summary>
		public override List<Oid> InstancesSelected
		{
			get
			{
				return DisplaySet.Values;
			}
		}
		#endregion InstancesSelected --> DisplaySet.Values

		#region Context
		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		public new IUQueryContext Context
		{
			get
			{
				return mContext as IUQueryContext;
			}
			set
			{
				mContext = value;
			}
		}
		#endregion Context

		#region Associated Service
		/// <summary>
		/// Gets or sets the associated service controller.
		/// </summary>
		public IUServiceController AssociatedServiceController
		{
			get
			{
				return mAssociatedServiceController;
			}
			set
			{
				if (mAssociatedServiceController != null)
				{
				}
				mAssociatedServiceController = value;
				if (mAssociatedServiceController != null)
				{
					AssociatedServiceController.BeforeExecute += new EventHandler<CheckForPendingChangesEventArgs>(HandleAssociatedServiceControllerBeforeExecute);
					mAssociatedServiceController.ServiceResponse += new EventHandler<ServiceResultEventArgs>(HandleAssociatedServiceControllerServiceResponse);
				}
			}
		}
		/// <summary>
		/// Gets or sets the cancel trigger.
		/// </summary>
		public ITriggerPresentation AssociatedServiceClearTrigger
		{
			get
			{
				return mAssociatedServiceClearTrigger;
			}
			set
			{
				if (mAssociatedServiceClearTrigger != null)
				{
					mAssociatedServiceClearTrigger.Triggered -= new EventHandler<TriggerEventArgs>(HandleAssociatedServiceClearTriggered);
				}
				mAssociatedServiceClearTrigger = value;
				if (mAssociatedServiceClearTrigger != null)
				{
					mAssociatedServiceClearTrigger.Triggered += new EventHandler<TriggerEventArgs>(HandleAssociatedServiceClearTriggered);
				}
			}
		}
		/// <summary>
		/// Gets or sets the StartServiceEnabled property.
		/// </summary>
		public bool StartServiceEnabled
		{
			get
			{
				return mStartServiceEnabled;
			}
			set
			{
				mStartServiceEnabled = value;
			}
		}
		#endregion Associated Service

		#region Detail
		/// <summary>
		/// Scenario alias as Detail of a Master-Detail.
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
		/// Graphical container of the detail.
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

		#region Constructors
		/// <summary>
		/// Used from derived classes.
		/// </summary>
		protected IUQueryController() : base() { }
		#endregion Constructors

		#region Events
		/// <summary>
		/// Occurs when selected instance is changed.
		/// </summary>
		public event EventHandler<SelectedInstanceChangedEventArgs> SelectedInstanceChanged;
		/// <summary>
		/// Occurs when the Master must be refreshed.
		/// </summary>
		public event EventHandler<RefreshRequiredMasterEventArgs> RefreshMasterRequired;
		/// <summary>
		/// Occurs when Checking for pending changes is required
		/// </summary>
		public event EventHandler<CheckForPendingChangesEventArgs> CheckForPendingChanges;
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
		private void HandleActionNavigationContextRequired(object sender, ContextRequiredEventArgs e)
		{
			UpdateContext();
			e.Context = Context;
		}
		private void HandleActionNavigationSelectedInstancesRequired(object sender, SelectedInstancesRequiredEventArgs e)
		{
			ProcessActionNavigationSelectedInstancesRequired(sender, e);
		}
		/// <summary>
		/// Handles the Display Set ExecuteCommand event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleDisplaySetExecuteCommand(object sender, ExecuteCommandEventArgs e)
		{
			ProcessDisplaySetExecuteCommand(sender, e);
		}
		/// <summary>
		/// Handles the Action SelectNextPrevious event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleActionSelectNextPreviousInstance(object sender, SelectNextPreviousInstanceEventArgs e)
		{
			List<Oid> oidList = null;
			if (e.SelectNext)
			{
				oidList = SelectNextInstance(e.CurrentOid, e.RefreshInstance);
			}
			else
			{
				oidList = SelectPreviousInstance(e.CurrentOid, e.RefreshInstance);
			}
			e.NewSelectedInstance = oidList;
		}
		/// <summary>
		/// Handles the Action RefreshRequired event
		/// </summary>
		private void HandleActionRefreshRequired(object sender, RefreshRequiredEventArgs e)
		{
			ProcessActionRefreshRequired(sender, e);

		}
		/// <summary>
		/// Executes the actions related to OnSelectedInstanceChanged event.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">SelectedInstanceChangedEventArgs.</param>
		private void HandleDisplaySetSelectedInstanceChanged(object sender, SelectedInstanceChangedEventArgs e)
		{
			if (mFlagIgnoreSelectionChange)
			{
				return;
			}

			ProcessDisplaySetSelectedInstanceChanged(sender, e);
		}
		/// <summary>
		/// Handles the ServiceResponse event from the associated service
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleAssociatedServiceControllerServiceResponse(object sender, ServiceResultEventArgs e)
		{
			ProcessAssociatedServiceControllerServiceResponse(sender, e);
		}
		/// <summary>
		/// Handles the BeforeExecute event from the associated service.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleAssociatedServiceControllerBeforeExecute(object sender, CheckForPendingChangesEventArgs e)
		{
			ProcessAssociatedServiceControllerBeforeExecute(sender, e);
		}
		/// <summary>
		/// Handles the Triggered event from the Associated service clear button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleAssociatedServiceClearTriggered(object sender, TriggerEventArgs e)
		{
			// Clear the associated service. Start again
			EnableAssociatedService(false);
		}
		/// <summary>
		/// Handles the BeforeActionItemExecution event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleActionBeforeActionItemExecution(object sender, CheckForPendingChangesEventArgs e)
		{
			ProcessBeforeActionExecution(e);
		}
		/// <summary>
		/// Handles the detail container Triggered event. Detail container becomes active.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleDetailContainerTriggered(object sender, TriggerEventArgs e)
		{
			// Raise the event only if there are no data 
			if (Context.ExchangeInformation.SelectedOids == null ||
				Context.ExchangeInformation.SelectedOids.Count == 0)
			{
				OnRefreshDetailRequired(new EventArgs());
			}
		}
  	  	/// <summary>
  	  	/// Handles the RefresRequired evento from Navigation
  	  	/// </summary>
  	  	/// <param name="sender"></param>
  	  	/// <param name="e"></param>
  	  	private void HandleNavigationRefreshRequired(object sender, RefreshRequiredEventArgs e)
  	  	{
  	  	  	ProcessNavigationRefreshRequired(sender, e);
  	  	}
		#endregion Event Handlers

		#region Process events
  	  	/// <summary>
  	  	/// Process the refresh required from navigation
  	  	/// </summary>
  		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessNavigationRefreshRequired(object sender, RefreshRequiredEventArgs e)
		{
			// Refresh Master
			RefreshRequiredMasterEventArgs lArgs = new RefreshRequiredMasterEventArgs(Context.ExchangeInformation);
			OnRefreshMasterRequired(lArgs);
			// If refresh has been done, do nothing
			if (lArgs.RefreshDone)
				return;

			// Default refresh
			ProcessActionRefreshRequired(sender, e);
		}
		/// <summary>
		/// Process the Selected instances event from action or navigation
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessActionNavigationSelectedInstancesRequired(object sender, SelectedInstancesRequiredEventArgs e)
		{
			e.SelectedInstances = DisplaySet.Values;
		}
		/// <summary>
		/// Refresh the corresponding instance, depends on the received parameter
		/// </summary>
		/// <param name="e"></param>
		protected virtual void ProcessActionRefreshRequired(object sender, RefreshRequiredEventArgs e)
		{

			if (e.RefreshType == RefreshRequiredType.RefreshMaster)
			{
				OnRefreshMasterRequired((RefreshRequiredMasterEventArgs)e);
			}
			else
			{
				if (this.Context.ContextType == ContextType.Population &&
					e.RefreshType == RefreshRequiredType.RefreshInstances)
				{
					if (((RefreshRequiredInstancesEventArgs)e).Instances.Count == 1)
					{
						RefreshThisOid(((RefreshRequiredInstancesEventArgs)e).Instances[0]);
					}
					else
					{
						RefreshOidList(((RefreshRequiredInstancesEventArgs)e).Instances);
					}
				}
				else
				{
					// Last case, refresh all the population
					Refresh();
				}

				//  Refresh the navigation origin
				if (Context.RelatedOids != null &&
					Context.RelatedOids.Count == 1)
				{
					OnRefreshRequired(new RefreshRequiredInstancesEventArgs(Context.RelatedOids, null));
				}
			}
				
			// If Scenario must be closed ...
			if (e.CloseScenario)
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
		}
		/// <summary>
		/// Actions related with the Service Response event from the associated service
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessAssociatedServiceControllerServiceResponse(object sender, ServiceResultEventArgs e)
		{
			// if no success
			if (e.Success)
			{
				// Refresh the Query controller, the received instances
				RefreshOidList(e.ExchangeInfoReceived.SelectedOids);

				// Refresh the DataGrid.
				Refresh();

				// Clear and disable the associated service.
				if (AssociatedServiceController != null && Logic.Agent.IsActiveFacet(AssociatedServiceController.Agents))
				{
					EnableAssociatedService(false);
				}
			}
		}
		/// <summary>
		/// Actions related with the BeforeExecute event from the associated service
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessAssociatedServiceControllerBeforeExecute(object sender, CheckForPendingChangesEventArgs e)
		{
			e.Cancel = !CheckPendingChanges(true, false);
		}
		/// <summary>
		/// Actions related woth the Display Set Execute command event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessDisplaySetExecuteCommand(object sender, ExecuteCommandEventArgs e)
		{
			if (e.ExecuteCommandType == ExecuteCommandType.ExecuteRefresh ||
				e.ExecuteCommandType == ExecuteCommandType.ExecuteRetriveAll)
			{
				// If there is any pending change, cancel the refresh
				if (!CheckPendingChanges(true, true))
				{
					e.ExecuteCommandType = ExecuteCommandType.None;
				}
			}
		}
		/// <summary>
		/// Actions related with the Selected instance changed from the DisplaySet
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessDisplaySetSelectedInstanceChanged(object sender, SelectedInstanceChangedEventArgs e)
		{
			if (!CheckPendingChanges(false, true))
			{
				DisplaySet.RaiseSelectecInstanceChanged = false;
				DisplaySet.Values = DisplaySet.PreviousValue;
				DisplaySet.RaiseSelectecInstanceChanged = true;
				return;
			}

			// Keep the previous value
			DisplaySet.PreviousValue = e.SelectedInstances;

			int lNumInstances = 0;
			if (e.SelectedInstances != null)
			{
				lNumInstances = e.SelectedInstances.Count;
			}

			#region Enable/disable navigations
			if (Navigation != null)
			{
				Navigation.EnableItemsBasedOnSelectedInstances(lNumInstances, e.EnabledNavigationsKeys);
			}
			#endregion Enable/disable navigations

			#region Enable/disable actions
			// Enable or disable the actions.
			if (Action != null)
			{
				Action.EnableItemsBasedOnSelectedInstances(lNumInstances, e.EnabledActionsKeys);
			}
			#endregion Enable/disable actions

			// Disable the Associated service.
			EnableAssociatedService(false);

			// Execute default instance change.
			OnSelectedInstanceChanged(e);
		}
		/// <summary>
		/// Actions related with the before action execution.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void ProcessBeforeActionExecution(CheckForPendingChangesEventArgs e)
		{
			e.Cancel = !CheckPendingChanges(true, true);
		}
		/// <summary>
		/// Actions related with the Cancel button.
		/// </summary>
		protected override void ProcessExecuteCancel()
		{
			if (!CheckPendingChanges(true, true))
			{
				return;
			}

			CloseScenario();
		}
		/// <summary>
		/// Actions related with the Form closing.
		/// </summary>
		/// <param name="e"></param>
		protected override void ProcessScenarioFormClosing(FormClosingEventArgs e)
		{
			e.Cancel = !CheckPendingChanges(true, true);
		}
		#endregion Process events

		#region Event Raisers
		/// <summary>
		/// Raises the SelectedInstanceChanged event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnSelectedInstanceChanged(SelectedInstanceChangedEventArgs eventArgs)
		{
			EventHandler<SelectedInstanceChangedEventArgs> handler = SelectedInstanceChanged;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the RefreshRequiredMaster event
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
		/// Raises the CheckForPendingChangesEventArgs event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnCheckForPendingChangesEventArgs(CheckForPendingChangesEventArgs eventArgs)
		{
			EventHandler<CheckForPendingChangesEventArgs> handler = CheckForPendingChanges;

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
		/// Raises the RefreshRequired event
		/// </summary>
		/// <param name="eventArgs"></param>
		public void OnRefreshRequired(RefreshRequiredEventArgs eventArgs)
		{
			EventHandler<RefreshRequiredEventArgs> handler = RefreshRequired;

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

		/// <summary>
		/// Refreshes the data of the IU.
		/// </summary>
		public virtual void Refresh() { }
		/// <summary>
		/// Loads the initial data to the IU.
		/// </summary>
		protected virtual void LoadInitialData() { }
		/// <summary>
		/// Update the data of the IU.
		/// </summary>
		public virtual void UpdateData(bool refresh)
		{
			EnableAssociatedService(false);
		}
		/// <summary>
		/// Apply multilanguage to the scenario.
		/// </summary>
		public override void ApplyMultilanguage()
		{
			base.ApplyMultilanguage();

			// Apply multilanguage to the Associated service
			if (AssociatedServiceController != null)
			{
				AssociatedServiceController.ApplyMultilanguage();
				if (AssociatedServiceController.OkTrigger != null)
				{
					AssociatedServiceController.OkTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_EXECUTE, LanguageConstantValues.L_EXECUTE, AssociatedServiceController.OkTrigger.Value.ToString());
				}
			}
			if (AssociatedServiceClearTrigger != null)
			{
				AssociatedServiceClearTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_CLEAR, LanguageConstantValues.L_CLEAR, AssociatedServiceClearTrigger.Value.ToString());
			}

			if (DisplaySet != null)
			{
				DisplaySet.ApplyMultilanguage();
			}

			// Translate the alias as a detail.
			if (IdXMLDetailAlias != "")
			{
				DetailAlias = CultureManager.TranslateString(IdXMLDetailAlias, DetailAlias);
			}
		}
		/// <summary>
		/// Configures the controller from the data context.
		/// </summary>
		/// <param name="context">IU context</param>
		public void ConfigureByContext(IUQueryContext context)
		{
			if (AssociatedServiceController != null && context != null && context.AssociatedServiceContext != null)
			{
				AssociatedServiceController.ConfigureByContext(context.AssociatedServiceContext);
			}

			base.ConfigureByContext(context);
		}

		/// <summary>
		/// Updates the context values from the InteractionToolkit layer.
		/// </summary>
		public override void UpdateContext()
		{
			// Build the DisplaySet attributes.
			Context.DisplaySetAttributes = BuildDisplaySetAttributes();

			// Default
			base.UpdateContext();

			// Associanted service
			if (AssociatedServiceController != null)
			{
				AssociatedServiceController.UpdateContext();
			}
		}

		#region Set population
		/// <summary>
		/// Sets the data to the Population.
		/// </summary>
		/// <param name="data">Data to set.</param>
		protected virtual void SetPopulation(DataTable data, bool discardExistingData, List<Oid> selectedOids)
		{
			DataTable lData = data;

			if (lData != null && lData.Rows.Count > 0)
			{
				// Boolean value that indicates if there are formulas/conditions defined for actions or navigations.
				bool lThereAreFormulas = (Action != null && Action.AnyActionItemEnabledByCondition) ||
					(Navigation != null && Navigation.AnyNavigationItemEnabledByCondition);

				// When there is data and formulas, add the columns and calculate the formulas.
				if (lThereAreFormulas)
				{
					// Add the actions and navigations formula's columns.
					lData.Columns.Add(Constants.ACTIONS_ACTIVATION_COLUMN_NAME);
					lData.Columns.Add(Constants.NAVIGATIONS_ACTIVATION_COLUMN_NAME);

					// Gets the state formulas (enabled or disabled) for action and navigation
					//  items (preconditions).
					GetActionsNavigationsEnabledStateFormulas(lData);
				}
			}

			// Set data to the DisplaySet.
			if (DisplaySet != null)
			{
				DisplaySet.SetPopulation(lData, discardExistingData, selectedOids);
			}
		}
		#endregion Set population

		/// <summary>
		/// Initializes the actions, navigations and displaysets.
		/// </summary>
		public override void Initialize()
		{
			// Initializes the action controller.
			if( Action != null)
			{
				Action.Initialize();
			}

			// Initializes the navigation controller.
			if(Navigation != null)
			{
				Navigation.Initialize();
			}
			// Initializes the displayset controller.
			if (DisplaySet != null)
			{
				DisplaySet.Initialize();
			}

			// Associated service initialization
			if (AssociatedServiceController != null)
			{
				if (Logic.Agent.IsActiveFacet(AssociatedServiceController.Agents))
				{
					EnableAssociatedService(true);
				}
				else
				{
					// Connected user is no agent for the associated service
					EnableAssociatedService(false);
				}

			}

			// Load the initial data
			LoadInitialData();

			base.Initialize();
		}

		#region Execute actions based on the selected command
		/// <summary>
		/// Execute the first creation action
		/// </summary>
		protected virtual void ExecuteCommandFirstCreateActionService()
		{
			if (Action != null && Action.ActionItems != null && Action.ActionItems.Count > 0)
			{
				foreach (ActionItemController lItem in this.Action.ActionItems.Values)
				{
					if (Logic.Agent.IsActiveFacet(lItem.Agents) && 
						lItem.ActionItemType == ActionItemType.Creation && lItem.Enabled)
					{
						lItem.Execute(null, new TriggerEventArgs());
						return;
					}
				}
			}
		}
		/// <summary>
		/// Execute the first Destroy action
		/// </summary>
		protected virtual void ExecuteCommandFirstDestroyActionService()
		{
			if (Action != null && Action.ActionItems != null && Action.ActionItems.Count > 0)
			{
				foreach (ActionItemController lItem in this.Action.ActionItems.Values)
				{
					if (Logic.Agent.IsActiveFacet(lItem.Agents) && 
						lItem.ActionItemType == ActionItemType.Destruction && lItem.Enabled)
					{
						lItem.Execute(null, new TriggerEventArgs());
						return;
					}
				}
			}
		}
		/// <summary>
		/// Execute the first no creation no destroy action
		/// </summary>
		protected virtual void ExecuteCommandFirstNotDestroyNotCreateActionService()
		{
			// If there is associated service and the user is an agent for this service ...
			List<Oid> selectedInstances = DisplaySet.Values;
			if (AssociatedServiceController != null &&
				Logic.Agent.IsActiveFacet(AssociatedServiceController.Agents) )
			{
				EnableAssociatedService(true);
				SetInstanceToAssociatedService(selectedInstances);
				return;
			}

			if (Action != null && Action.ActionItems != null && Action.ActionItems.Count > 0)
			{
				foreach (ActionItemController lItem in Action.ActionItems.Values)
				{
					if (Logic.Agent.IsActiveFacet(lItem.Agents) && 
						lItem.ActionItemType == ActionItemType.Normal && lItem.Enabled)
					{
						lItem.Execute(null, new TriggerEventArgs());
						return;
					}
				}
			}
		}
		#endregion Execute actions based on the selected command

		#region Select Next-Previous
		/// <summary>
		/// Selects the previous instance and refresh the data of the Oid passed as parameter.
		/// </summary>
		/// <param name="oidToBeRefresh">Oid object to be updated.</param>
		/// <param name="refreshInstance">Indicates if the instance has to be updated.</param>
		/// <returns>The list of updated Oids.</returns>
		private List<Oid> SelectPreviousInstance(Oid oidToBeRefresh, bool refreshInstance)
		{
			DisplaySetByBlocksController displaySetController = mDisplaySet as DisplaySetByBlocksController;
			if (displaySetController == null)
			{
				return null;
			}

			int currentIndex = displaySetController.GetRowFromOid(oidToBeRefresh);

			// Curretn Oid doesn't exist in the population.
			if (currentIndex == -1)
			{
				return null;
			}

			// Refresh the Instance in the population.
			if (refreshInstance)
			{
				RefreshThisOid(oidToBeRefresh);
			}

			int afterRefreshIndex = displaySetController.GetRowFromOid(oidToBeRefresh);

			// Instance doesn't exist in the population and it was in the first row.
			if (afterRefreshIndex == -1 && currentIndex == 0)
			{
				displaySetController.SelectRow(0);
			}
			else
			{
				displaySetController.SelectRow(currentIndex - 1);
			}

			return displaySetController.Values;
		}
		/// <summary>
		/// Selects the next instance and refresh the data of the Oid passed as parameter.
		/// </summary>
		/// <param name="oidToBeRefresh">Oid object to be updated.</param>
		/// <param name="refreshInstance">Indicates if the instance has to be updated.</param>
		/// <returns>The list of updated Oids.</returns>
		private List<Oid> SelectNextInstance(Oid oidToBeRefresh, bool refreshInstance)
		{
			DisplaySetByBlocksController displaySetController = mDisplaySet as DisplaySetByBlocksController;
			if (displaySetController == null)
			{
				return null;
			}

			int currentIndex = displaySetController.GetRowFromOid(oidToBeRefresh);

			// Curretn Oid doesn't exist in the population.
			if (currentIndex == -1)
			{
				return null;
			}

			// Refresh the Instance in the population.
			if (refreshInstance)
			{
				RefreshThisOid(oidToBeRefresh);
			}

			// If the selected row is the last one, get next block.
			IUPopulationContext populationContext = Context as IUPopulationContext;
			if (populationContext != null &&
				currentIndex == displaySetController.DataTable.Rows.Count - 1 &&
				populationContext.LastBlock == false)
			{
				UpdateData(false);
			}

			int afterRefreshIndex = displaySetController.GetRowFromOid(oidToBeRefresh);

			if (afterRefreshIndex == -1)
			{
				// Instance doesn't appear in the population, select the previous index.
				displaySetController.SelectRow(currentIndex);
			}
			else
			{
				displaySetController.SelectRow(currentIndex + 1);
			}

			return displaySetController.Values;
		}
		#endregion Select Next-Previous

		/// <summary>
		/// Updates the data of the Oid object passed as parameter.
		/// </summary>
		/// <param name="oidToBeRefresh">Oid to be updated.</param>
		public void RefreshThisOid(Oid oidToBeRefresh)
		{
			// Get the attributes values of this instance and update the proper Datatable row.
			try
			{
				DisplaySetByBlocksController displaySetController = mDisplaySet as DisplaySetByBlocksController;
				if (displaySetController == null)
				{
					return;
				}

				DataTable lInstance = Logic.ExecuteQueryInstance(Logic.Agent, oidToBeRefresh, DisplaySet.DisplaySetAttributes);

				bool instanceNotExist = false;
				if (lInstance == null || lInstance.Rows.Count == 0)
				{
					instanceNotExist = true;
				}

				// Get the proper row.
				Oid oidRow;
				int rowIndex = 0;
				// Ignore any change in the selected instance
				mFlagIgnoreSelectionChange = true;
				foreach (DataRow row in displaySetController.DataTable.Rows)
				{
					oidRow = Adaptor.ServerConnection.GetOid(displaySetController.DataTable, row);
					if (oidRow.Equals(oidToBeRefresh))
					{
						if (instanceNotExist)
						{
							row.Delete();
						}
						else
						{
							displaySetController.DataTable.Merge(lInstance);
							// Get the current position.
							displaySetController.SelectRow(rowIndex);
						}
						break;
					}
					rowIndex++;
				}
				mFlagIgnoreSelectionChange = false;
			}
			catch
			{
				mFlagIgnoreSelectionChange = false;
			}
		}
        /// <summary>
        /// Updates the data of the Oid objects list passed as parameter.
        /// </summary>
        /// <param name="oidList">Oid list to be refreshed</param>
        /// <returns>True if refresh has been done</returns>
        public bool RefreshOidList(List<Oid> oidList)
		{
			// Get the attributes values of every instance and update the proper Datatable row.
			try
			{
				// Only for population controllers
				DisplaySetByBlocksController displaySetController = mDisplaySet as DisplaySetByBlocksController;
                if (displaySetController == null || displaySetController.DataTable == null)
				{
					Refresh();
					return true;
				}

				foreach (Oid lOid in oidList)
				{
					DataTable lInstance = Logic.ExecuteQueryInstance(Logic.Agent, lOid, DisplaySet.DisplaySetAttributes);

					bool instanceNotExist = false;
					if (lInstance == null || lInstance.Rows.Count == 0)
					{
						instanceNotExist = true;
					}

					// Get the proper row.
					Oid oidRow;
					int rowIndex = 0;
					// Ignore any change in the selected instance
					mFlagIgnoreSelectionChange = true;
					foreach (DataRow row in displaySetController.DataTable.Rows)
					{
						oidRow = Adaptor.ServerConnection.GetOid(displaySetController.DataTable, row);
						if (oidRow.Equals(lOid))
						{
							if (instanceNotExist)
							{
								row.Delete();
							}
							else
							{
								displaySetController.DataTable.Merge(lInstance);
							}
							break;
						}
						rowIndex++;
					}
					mFlagIgnoreSelectionChange = false;
				}
				displaySetController.Values = oidList;
			}
			catch
			{
				mFlagIgnoreSelectionChange = false;
                return false;
			}

            return true;
		}
		/// <summary>
		/// Check if there is any change pending in the Display Set viewer or in the Associated Service IU.
		/// </summary>
		/// <param name="searchChangesInThisDS">Indicates if the current DisplaySet has to be checked for pending changes.</param>
		/// <param name="searchChangesInAssociatedSIU">Indicates if the Associated Service IU has to be checked for pending changes.</param>
		/// <returns>Returns False if the user wants to cancel the action.</returns>
		public bool CheckPendingChanges(bool searchChangesInThisDS, bool searchChangesInAssociatedSIU)
		{
			CheckForPendingChangesEventArgs eventArgs = new CheckForPendingChangesEventArgs();
			OnCheckForPendingChangesEventArgs(eventArgs);
			if (eventArgs.Cancel)
			{
				return false;
			}

			bool result = true;
			if (!searchChangesInThisDS && !searchChangesInAssociatedSIU)
			{
				return true;
			}
			// Check in associated SIU.
			if (searchChangesInAssociatedSIU &&
				AssociatedServiceController != null && AssociatedServiceController.PendingChanges)
			{
				string message = CultureManager.TranslateString(LanguageConstantKeys.L_PENDINGCHANGES, LanguageConstantValues.L_PENDINGCHANGES);
				// Show message in order to ask about to continue or cancel the pending subjects.
				if (MessageBox.Show(message, Alias, MessageBoxButtons.YesNo) == DialogResult.No)
				{
					result = false;
				}
				// No pending changes or user doesn't want to execute associated Service IU.
				if (result)
				{
					AssociatedServiceController.PendingChanges = false;
				}
				return result;
			}

			// No pending changes in Display Set, or user doesn't want to save them.
			if (searchChangesInThisDS && DisplaySet.PendingChanges)
			{
				string message = CultureManager.TranslateString(LanguageConstantKeys.L_PENDINGCHANGES, LanguageConstantValues.L_PENDINGCHANGES);
				if (MessageBox.Show(message, Alias, MessageBoxButtons.YesNo) == DialogResult.No)
				{
					result = false;
				}
				// No pending changes or user doesn't want to save them.
				if (result)
				{
					DisplaySet.PendingChanges = false;
				}
			}
			
			return result;
		}

		#region Associated Service
		/// <summary>
		/// Enables/disables the associated service
		/// </summary>
		protected void EnableAssociatedService(bool enable)
		{
			if (AssociatedServiceController == null)
			{
				return;
			}

			ClearAssociatedService();

			if (StartServiceEnabled && !enable)
			{
				return;
			}

			AssociatedServiceController.Enable(enable);
			if (AssociatedServiceClearTrigger != null)
			{
				AssociatedServiceClearTrigger.Enabled = enable;
			}
		}
		/// <summary>
		/// Clear associated service.
		/// </summary>
		protected void ClearAssociatedService()
		{
			if (AssociatedServiceController == null)
			{
				return;
			}

			// Clear input fields context values.
			AssociatedServiceController.Context.ClearInputFields();

			// Create new EchangeInfoAction to service context
			IUServiceContext lServiceContext = AssociatedServiceController.Context;
			ExchangeInfoAction lExchangeInfoAction = new ExchangeInfoAction(lServiceContext.ClassName, lServiceContext.IuName, null, Context);
			AssociatedServiceController.Context.ExchangeInformation = lExchangeInfoAction;

			AssociatedServiceController.Initialize();

			// Hidden argument this.
			if (string.Compare(AssociatedServiceController.ClassName, Context.ClassName) == 0 && AssociatedServiceController.ArgumentThis != null)
			{
				AssociatedServiceController.ArgumentThis.Visible = false;
				AssociatedServiceController.Context.InputFields[AssociatedServiceController.ArgumentThis.Name].Visible = false;
			}
		}
		/// <summary>
		/// Initialize the service controller with the selected instance.
		/// </summary>
		/// <param name="instance"></param>
		protected void SetInstanceToAssociatedService(List<Oid> instance)
		{
			if (AssociatedServiceController == null)
			{
				return;
			}

			// If no instance, disable the associated service
			if ((instance == null || instance.Count != 1) && !StartServiceEnabled)
			{
				EnableAssociatedService(false);
				return;
			}

			// Enable and clear the service
			AssociatedServiceController.PendingChanges = false;
			EnableAssociatedService(true);

			// Initialice the selected Oid in the service context
			AssociatedServiceController.Context.ExchangeInformation.SelectedOids = instance;

			// Initialize associated service controller.
			AssociatedServiceController.Context.Initialized = false;
			AssociatedServiceController.Initialize();

			// Hidden argument this.
			if (string.Compare(AssociatedServiceController.ClassName, Context.ClassName) == 0 && AssociatedServiceController.ArgumentThis != null)
			{
				AssociatedServiceController.ArgumentThis.Visible = false;
				AssociatedServiceController.Context.InputFields[AssociatedServiceController.ArgumentThis.Name].Visible = false;
			}
		
			// Set focus in the first visible and enabled argument
			foreach(ArgumentController lArgController in AssociatedServiceController.InputFields )
			{
				if (lArgController.Visible && lArgController.Enabled)
				{
					lArgController.Focused = true;
					break;
				}
			}
		}
		#endregion Associated Service

		#region Enable actions
		/// <summary>
		/// Enables or disables the action items.
		/// </summary>
		/// <param name="value">True if actions are wanted to be enabled; False if actions are wanted to be disabled.</param>
		public void EnableActions(bool value)
		{
			if (Action != null)
			{
				// Enable or disable the action items.
				Action.Enabled = value;
			}
		}
		#endregion Enable actions

		#region Enable navigations
		/// <summary>
		/// Enables or disables the navigation items.
		/// </summary>
		/// <param name="value">True if navigations are wanted to be enabled; False if navigations are wanted to be disabled.</param>
		public void EnableNavigations(bool value)
		{
			if (Navigation != null)
			{
				// Enable or disable the navigation items.
				Navigation.Enabled = value;
			}
		}
		#endregion Enable navigations

		#region Build DisplaySet attributes
		/// <summary>
		/// Gets the DisplaySet attributes which participates in the formulas
		/// for enabling or disabling the action/navigation elements.
		/// </summary>
		/// <returns>DisplaySet attributtes (comma-separated).</returns>
		protected virtual string BuildDisplaySetAttributes()
		{
			// Compose the DisplaySetAttributes taking into account the
			// action and navigation attributes needed for enabling/disabling formulas.
			StringBuilder lDisplaySetAttributes = new StringBuilder();

			// DisplaySet attributes.
			if (DisplaySet != null)
			{
				string lDSAttributes = DisplaySet.DisplaySetAttributes;
				if (lDSAttributes.Length > 0)
				{
					lDisplaySetAttributes.Append(lDSAttributes);
					lDisplaySetAttributes.Append(',');
				}
			}

			// Action attributes for formulas.
			if (Action != null)
			{
				string lAttributesForActions = Action.GetActionAttributesForFormulas();
				if (lAttributesForActions.Length > 0)
				{
					lDisplaySetAttributes.Append(lAttributesForActions);
					lDisplaySetAttributes.Append(',');
				}
			}

			// Navigation attributes for formulas.
			if (Navigation != null)
			{
				string lAttributesForNavigations = Navigation.GetNavigationAttributesForFormulas();
				if (lAttributesForNavigations.Length > 0)
				{
					lDisplaySetAttributes.Append(lAttributesForNavigations);
					lDisplaySetAttributes.Append(',');
				}
			}

			// Remove the last comma.
			if (lDisplaySetAttributes.Length > 0)
			{
				lDisplaySetAttributes.Length--;
			}

			// Remove the repeated sttributes.
			List<string> lNoRepeatedAttributes = new List<string>();
			if (lDisplaySetAttributes.Length > 0)
			{
				string[] lAttributes = lDisplaySetAttributes.ToString().Trim().Split(',');
				foreach (string lAttr in lAttributes)
				{
					if (!lNoRepeatedAttributes.Contains(lAttr))
					{
						lNoRepeatedAttributes.Add(lAttr);
					}
				}
			}

			// Returns a comma-sepatated string.
			return UtilFunctions.StringList2String(lNoRepeatedAttributes, ",");
		}
		#endregion Build DisplaySet attributes

		#region Get actions/navigations enabled state formulas
		/// <summary>
		/// Gets the state (enabled or disabled) for action and navigation items depending on precondition formulas.
		/// </summary>
		/// <param name="data">Datatable to add the actions and navigations state.</param>
		protected virtual void GetActionsNavigationsEnabledStateFormulas(DataTable data)
		{
			// Get the action items IdXML's.
			List<string> lActionListIdXML = null;
			if (Action != null && Action.AnyActionItemEnabledByCondition)
			{
				lActionListIdXML = Action.GetActionItemsIdXML();
			}

			// Get the navigation items IdXML's.
			List<string> lNavigationListIdXML = null;
			if (Navigation != null && Navigation.AnyNavigationItemEnabledByCondition)
			{
				lNavigationListIdXML = Navigation.GetNavigationItemsIdXML();
			}

			// Gets the enabled state formulas for action and navigations (depending on preconditions).
			if (lActionListIdXML != null || lNavigationListIdXML != null)
			{
				Logic.GetActionsNavigationsEnabledStateFormulas(Context, data, lActionListIdXML, lNavigationListIdXML);
			}
		}
		#endregion Get actions/navigations enabled state formulas

		/// <summary>
		/// Returns true if the detail is active.
		/// </summary>
		public bool IsActiveDetail()
		{
			// If no graphical container, is active as detail.
			if (DetailContainer == null)
			{
				return true;
			}

			return DetailContainer.Visible;
		}
		#endregion Methods
		#region INavigationItemEvents implementation
		/// <summary>
		/// Occurs when the related instance, origin of navigation, must be refreshed
		/// </summary>
		public event EventHandler<RefreshRequiredEventArgs> RefreshRequired;
		#endregion INavigationItemEvents implementation
	}
}
