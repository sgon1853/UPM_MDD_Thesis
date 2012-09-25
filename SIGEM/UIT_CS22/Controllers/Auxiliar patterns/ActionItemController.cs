// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using SIGEM.Client.Presentation;
using SIGEM.Client.Logics;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	#region ActionItemType enum
	public enum ActionItemType
	{
		/// <summary>
		/// None.
		/// </summary>
		None,
		/// <summary>
		/// Is a creation class service.
		/// </summary>
		Creation,
		/// <summary>
		/// Is a destruction class service.
		/// </summary>
		Destruction,
		/// <summary>
		/// Is a normal class service, no creation and no destruction.
		/// </summary>
		Normal,
		/// <summary>
		/// Is a global service.
		/// </summary>
		Global,
		/// <summary>
		/// Other interaction units, Instance, Population or Master-Detail.
		/// </summary>
		Other,
		/// <summary>
		/// Is the Print action. Opens the Print scenario.
		/// </summary>
		Print
	}
	#endregion ActionItemType enum

	/// <summary>
	/// Class that manages the Action item controller.
	/// An Action Item is an element in an scenario that has associated a method trigger event.
	/// </summary>
	public class ActionItemController : Controller, IActionItemSuscriber
	{
		#region Members
		/// <summary>
		/// Class name.
		/// </summary>
		private string mClassIUName;
		/// <summary>
		/// Interaction unit name.
		/// </summary>
		private string mIUName;
		/// <summary>
		/// Number of the Action item.
		/// </summary>
		private int mNumber;
		/// <summary>
		/// Alias of the Action item.
		/// </summary>
		private string mAlias;
		/// <summary>
		/// IdXMLof the Action item.
		/// </summary>
		private string mIdXML;
		/// <summary>
		/// List of agents of the Action item.
		/// </summary>
		private List<string> mAgents;
		/// <summary>
		/// Trigger presentation of the Action item.
		/// </summary>
		private ITriggerPresentation mTrigger;
		/// <summary>
		/// Navigational filtering identifier.
		/// </summary>
		private string mNavigationalFilteringIdentity = string.Empty;
		/// <summary>
		/// Boolean value indicating whether it has navigational filtering.
		/// </summary>
		private bool mNavigationalFilter = false;
		/// <summary>
		/// Acction type
		/// </summary>
		private ActionItemType mActionItemType;
		/// <summary>
		/// Refresh by row flag
		/// </summary>
		private bool mRefreshByRow = Logic.RefreshByRowAfterServiceExecution;
		/// <summary>
		/// Refresh Master flag
		/// </summary>
		private bool mRefreshMaster = Logic.RefreshMasterAfterServiceExecution;
		/// <summary>
		/// ActionItem attributes (comma-separated) needed for the enabling/disabling formulas.
		/// </summary>
		private string mActionItemAttributesForFormulas = string.Empty;
		/// <summary>
		/// Boolean value indicating whether the multiselection is supported or not.
		/// </summary>
		private bool mMultiSelectionSupported = true;
		/// <summary>
		/// Boolean value indicating whether the Object-Action feature is enabled or not.
		/// </summary>
		private bool mObjectActionEnabled = false;
        /// <summary>
        /// Shortcut key list for this action item
        /// </summary>
        private List<Keys> mShortcutList = new List<Keys>();
        /// <summary>
        /// Close main scenario after successful service execution
        /// </summary>
        private bool mCloseScenario = false;
        /// <summary>
        /// Default order criteria to be used in the destination
        /// </summary>
        private string mDefaultOrderCriteria = "";
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ActionItemController' class.
		/// </summary>
		/// <param name="number">Number of the Action item.</param>
		/// <param name="alias">Alias of the Action item.</param>
		/// <param name="idXML">IdXML of the Action item.</param>
		/// <param name="agents">List of agents that can acces to the Action item.</param>
		/// <param name="classIUName">Class name of the Action item.</param>
		/// <param name="iuName">Interaction unit name of the Action item.</param>
		/// <param name="navigationalFilteringIdentity">Navigational filtering identifier.</param>
		/// <param name="actionType">Type of the action item.</param>
		/// <param name="objectActionEnabled">Boolean value indicating whether the Object-Action feature is enabled or not.</param>
		/// <param name="multiSelectionSupported">If multiselection is supported or not.</param>
		/// <param name="attributesListForFormulas">List of attributes needed for evaluating the enabling/disabling formulas.</param>
		/// <param name="parent">Parent controller.</param>
		public ActionItemController(
			int number,
			string alias,
			string idXML,
			List<string> agents,
			string classIUName,
			string iuName,
			string navigationalFilteringIdentity,
			ActionItemType actionType,
			bool objectActionEnabled,
			bool multiSelectionSupported,
			List<string> attributesListForFormulas,
			ActionController parent)
			: base(parent)
		{
			mNumber = number;
			mAlias = alias;
			mIdXML = idXML;
			mAgents = agents;
			ClassIUName = classIUName;
			IUName = iuName;
			NavigationalFilteringIdentity = navigationalFilteringIdentity;
			ActionItemType = actionType;
			ObjectActionEnabled = objectActionEnabled;
			MultiSelectionSupported = multiSelectionSupported;

			// Build the attributes for enabling/disabling formulas (comma-sepatated string).
			if (attributesListForFormulas != null)
			{
				StringBuilder lAttributes = new StringBuilder();
				foreach (string lItem in attributesListForFormulas)
				{
					lAttributes.Append(lItem);
					lAttributes.Append(',');
				}
				// Remove the last comma.
				if (lAttributes.Length > 0)
				{
					lAttributes.Length--;
				}

				ActionItemAttributesForFormulas = lAttributes.ToString();
			}
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Action Item Type.
		/// </summary>
		public virtual ActionItemType ActionItemType
		{
			get
			{
				return mActionItemType;
			}
			set
			{
				mActionItemType = value;
			}
		}
		/// <summary>
		/// Gets or sets the class name where is defined the service associated with this Action Item.
		/// </summary>
		public string ClassIUName
		{
			get
			{
				return mClassIUName;
			}
			set
			{
				mClassIUName = value;
			}
		}
		/// <summary>
		/// Gets or sets the interaction unit name or type (depending platform) where the Action Item is.
		/// </summary>
		public string IUName
		{
			get
			{
				return mIUName;
			}
			set
			{
				mIUName = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether it has navigational filtering.
		/// </summary>
		public virtual bool IsNavigationalFilter
		{
			get
			{
				return mNavigationalFilter;
			}
			protected set
			{
				mNavigationalFilter = value;
			}
		}
		/// <summary>
		/// Gets or sets the navigational filtering identifier.
		/// </summary>
		public string NavigationalFilteringIdentity
		{
			get
			{
				return mNavigationalFilteringIdentity;
			}
			protected set
			{
				mNavigationalFilteringIdentity = (value == null? string.Empty: value);
				if (mNavigationalFilteringIdentity.Length > 0)
				{
					IsNavigationalFilter = true;
				}
			}
		}
		/// <summary>
		/// Gets the number of the Action Item.
		/// </summary>
		public int Number
		{
			get
			{
				return mNumber;
			}
		}
		/// <summary>
		/// Gets the alias of the Action Item.
		/// </summary>
		public string Alias
		{
			get
			{
				return mAlias;
			}
		}
		/// <summary>
		/// Gets the IdXML of the Action Item.
		/// </summary>
		public string IdXML
		{
			get
			{
				return mIdXML;
			}
		}
		/// <summary>
		/// Gets or sets the list of agents that can access to the Action Item.
		/// </summary>
		public List<string> Agents
		{
			get
			{
				return mAgents;
			}
			set
			{
				mAgents = value;
			}
		}
		/// <summary>
		/// Gets or sets the method trigger event of the Action Item.
		/// </summary>
		public ITriggerPresentation Trigger
		{
			get
			{
				return mTrigger;
			}
			set
			{
				if (mTrigger != null)
				{
					mTrigger.Triggered -= new EventHandler<TriggerEventArgs>(Execute);
				}
				mTrigger = value;
				if (mTrigger != null)
				{
					mTrigger.Triggered += new EventHandler<TriggerEventArgs>(Execute);
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the method trigger event of the Action Item is enabled or not.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return mTrigger.Enabled;
			}
			set
			{
				mTrigger.Enabled = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether trigger of the Action Item is focused or not.
		/// </summary>
		public bool IsSelected
		{
			get
			{
				return mTrigger.Focused;
			}
			set
			{
				mTrigger.Focused = value;
			}
		}
		/// <summary>
		/// Gets the ActionItem attributes (comma-separated) needed for
		/// the enabling/disabling formulas.
		/// </summary>
		public string ActionItemAttributesForFormulas
		{
			get
			{
				return mActionItemAttributesForFormulas;
			}
			set
			{
				mActionItemAttributesForFormulas = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the multiselection is supported or not.
		/// </summary>
		public bool MultiSelectionSupported
		{
			get
			{
				return mMultiSelectionSupported;
			}
			set
			{
				mMultiSelectionSupported = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the Object-Action feature is enabled or not.
		/// </summary>
		public bool ObjectActionEnabled
		{
			get
			{
				return mObjectActionEnabled;
			}
			set
			{
				mObjectActionEnabled = value;
			}
		}
		/// <summary>
		/// Refresh Master flag
		/// </summary>
		public bool RefreshMaster
		{
			get
			{
				return mRefreshMaster;
			}
			set
			{
				mRefreshMaster = value;
			}
		}
		/// <summary>
		/// Refresh by row flag
		/// </summary>
		public bool RefreshByRow
		{
			get
			{
				return mRefreshByRow;
			}
			set
			{
				mRefreshByRow = value;
			}
		}
        /// <summary>
        /// Close main scenario after successful service execution
        /// </summary>
        public bool CloseScenario
        {
            get
            {
                return mCloseScenario;
            }
            set
            {
                mCloseScenario = value;
            }
        }
        /// <summary>
        /// Default order criteria to be used in the destination
        /// </summary>
        public string DefaultOrderCriteria
        {
            get
            {
                return mDefaultOrderCriteria;
            }
            set
            {
                mDefaultOrderCriteria = value;
            }
        }
		#endregion Properties

		#region Events
		public event EventHandler<LaunchingScenarioEventArgs> LaunchingScenario;
		public event EventHandler<ContextRequiredEventArgs> ContextRequired;
		public event EventHandler<SelectedInstancesRequiredEventArgs> SelectedInstancesRequired;
		/// <summary>
		/// Request a data refresh as a result of some specific action
		/// </summary>
		public event EventHandler<RefreshRequiredEventArgs> RefreshRequired;
		/// <summary>
		/// Select the next or previous instance
		/// </summary>
		public event EventHandler<SelectNextPreviousInstanceEventArgs> SelectNextPreviousInstance;
		/// <summary>
		/// Validation before the action item execution.
		/// </summary>
		public event EventHandler<CheckForPendingChangesEventArgs> BeforeExecute;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Event raised when an instance has been refreshed.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">RefreshInstanceArgs arguments.</param>
        private void HandleRefreshRequired(object sender, RefreshRequiredEventArgs e)
		{
            OnRefreshRequired(e);
		}
		/// <summary>
		/// Event raised when the previous instance needs to be selected.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">SelectNextPreviousInstanceArgs arguments.</param>
		private void HandleSelectPreviousInstance(object sender, SelectNextPreviousInstanceEventArgs e)
		{
			// Propagates the event
			OnSelectNextPreviousInstance(e);
		}
		/// <summary>
		/// Event raised when the next instance needs to be selected.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">SelectNextPreviousInstanceArgs arguments.</param>
		private void HandleSelectNextInstance(object sender, SelectNextPreviousInstanceEventArgs e)
		{
			// Propagates the event
			OnSelectNextPreviousInstance(e);
		}
		/// <summary>
		/// This method refreshes the parent controller after the succesfull execution of a service.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">ServiceResultEventArgs</param>
		private void HandleServiceResponse(object sender, ServiceResultEventArgs e)
		{
			// If the service has been executed succesfully, refresh the Parent controller.
			if (e.Success)
			{
				// Refresh the Master.
				if (RefreshMaster)
				{
					RefreshRequiredMasterEventArgs eventArgsRefreshMaster = new RefreshRequiredMasterEventArgs(e.ExchangeInfoReceived);
                    // Main scenario must be closed or not
                    eventArgsRefreshMaster.CloseScenario = CloseScenario;
                    OnRefreshRequired(eventArgsRefreshMaster);
					// If Master has been refreshed, do nothing.
					if (eventArgsRefreshMaster.RefreshDone)
					{
						return;
					}
				}
				// Refresh mode. Depending on the local flag, the number of intances, the service type and its definition class:
				//  If the service type is not Creation and its definition class is the same as the selected instances class,
				//  then refresh only the selected instances. The number of selected instances has to be lesser or equals than 5.
				//  Other case, refresh complete population.
				RefreshRequiredEventArgs eventArgs = new RefreshRequiredEventArgs(e.ExchangeInfoReceived);

				if (RefreshByRow &&
					e.ExchangeInfoReceived != null &&
					ActionItemType != ActionItemType.Creation &&
					e.ExchangeInfoReceived.SelectedOids != null &&
					e.ExchangeInfoReceived.SelectedOids.Count > 0 &&
					e.ExchangeInfoReceived.SelectedOids.Count < 5)
				{
					string lInstancesClass = e.ExchangeInfoReceived.SelectedOids[0].ClassName;
					if (lInstancesClass.Equals(this.ClassIUName, StringComparison.OrdinalIgnoreCase))
					{
						eventArgs = new RefreshRequiredInstancesEventArgs(e.ExchangeInfoReceived.SelectedOids, e.ExchangeInfoReceived);
					}
				}

                // Main scenario must be closed or not
                eventArgs.CloseScenario = CloseScenario;

				// Raise the event.
				OnRefreshRequired(eventArgs);
			}
		}
		#endregion Event Handlers

		#region Event Raisers
		protected void OnLaunchingScenario(LaunchingScenarioEventArgs eventArgs)
		{
			EventHandler<LaunchingScenarioEventArgs> handler = LaunchingScenario;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		protected void OnContextRequired(ContextRequiredEventArgs eventArgs)
		{
			EventHandler<ContextRequiredEventArgs> handler = ContextRequired;

			if (handler != null)
			{
				handler(this, eventArgs);
			}

		}
		protected void OnSelectedInstancesRequired(SelectedInstancesRequiredEventArgs eventArgs)
		{
			EventHandler<SelectedInstancesRequiredEventArgs> handler = SelectedInstancesRequired;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the RefreshRequired event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnRefreshRequired(RefreshRequiredEventArgs eventArgs)
		{
			EventHandler<RefreshRequiredEventArgs> handler = RefreshRequired;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the RefreshRequired event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnSelectNextPreviousInstance(SelectNextPreviousInstanceEventArgs eventArgs)
		{
			EventHandler<SelectNextPreviousInstanceEventArgs> handler = SelectNextPreviousInstance;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the BeforeExecute event.
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnBeforeExecute(CheckForPendingChangesEventArgs eventArgs)
		{
			EventHandler<CheckForPendingChangesEventArgs> handler = BeforeExecute;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		#endregion Event Raisers

		#region Methods
		#region Execute
		/// <summary>
		/// Executes the Action Item.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">TriggerEventArgs</param>
		public void Execute(object sender, TriggerEventArgs e)
		{
			try
			{
				// Validate if there are pending changes in the interaction unit
				// which contains the action item.
				CheckForPendingChangesEventArgs eventArg = new CheckForPendingChangesEventArgs();
				OnBeforeExecute(eventArg);
				if (eventArg.Cancel)
				{
					return;
				}

				ContextRequiredEventArgs contextEventArgs = new ContextRequiredEventArgs();
				OnContextRequired(contextEventArgs);

				SelectedInstancesRequiredEventArgs lSelectedInstancesEventArgs = new SelectedInstancesRequiredEventArgs();
				OnSelectedInstancesRequired(lSelectedInstancesEventArgs);
				ExchangeInfoAction lExchangeInfoAction = null;
				if (this.IsNavigationalFilter)
				{
					lExchangeInfoAction = new ExchangeInfoAction(ClassIUName, IUName, NavigationalFilteringIdentity, lSelectedInstancesEventArgs.SelectedInstances, contextEventArgs.Context);
				}
				else
				{
					lExchangeInfoAction = new ExchangeInfoAction(ClassIUName, IUName, lSelectedInstancesEventArgs.SelectedInstances, contextEventArgs.Context);
				}

                // Set default order criteria
                lExchangeInfoAction.DefaultOrderCriteria = DefaultOrderCriteria;

				// Raise the event Launching Scenario.
				LaunchingScenarioEventArgs lLaunchScenarioArgs = new LaunchingScenarioEventArgs(lExchangeInfoAction);
				OnLaunchingScenario(lLaunchScenarioArgs);

				// Launch scenario.
				ScenarioManager.LaunchActionScenario(lExchangeInfoAction, this);
			}
			catch (Exception err)
			{
				ScenarioManager.LaunchErrorScenario(err);
			}
		}
		#endregion Execute

		#region SuscribeActionEvents
		/// <summary>
		/// This method is used to associate the Action Item to a method trigger event.
		/// </summary>
		/// <param name="actionServiceEvents">ActionServiceEvents interface compliant.</param>
        void IActionItemSuscriber.SuscribeActionEvents(IActionItemEvents actionServiceEvents)
		{
			if (actionServiceEvents != null)
			{
				actionServiceEvents.ServiceResponse += new EventHandler<ServiceResultEventArgs>(HandleServiceResponse);
				actionServiceEvents.SelectNextInstance += new EventHandler<SelectNextPreviousInstanceEventArgs>(HandleSelectNextInstance);
				actionServiceEvents.SelectPreviousInstance += new EventHandler<SelectNextPreviousInstanceEventArgs>(HandleSelectPreviousInstance);
                actionServiceEvents.RefreshRequired += new EventHandler<RefreshRequiredEventArgs>(HandleRefreshRequired);
			}
		}
		#endregion SuscribeActionEvents

		#region Initialize
		/// <summary>
		/// Initializes the Action Item.
		/// </summary>
		public void Initialize()
		{
			// ACTIONITEM MULTILANGUAGE: Apply multilaguage to the action item trigger.
			if (this.Trigger != null)
			{
				this.Trigger.Value = CultureManager.TranslateString(this.IdXML, this.Alias, this.Trigger.Value.ToString());
				// END ACTIONITEM MULTILANGUAGE

				// Applies agent visibility.
				if (Logic.Agent.IsActiveFacet(mAgents))
				{
					Enabled = true;
					this.Trigger.Visible = true;
				}
				else
				{
					Enabled = false;
					this.Trigger.Visible = false;
				}
			}
		}
		#endregion Initialize

        #region Shortcut key List
        /// <summary>
        /// Add the double click shortcut to the action item
        /// </summary>
        public void AddShortcutKey()
        {
            mShortcutList.Add(Keys.NoName);
        }

        /// <summary>
        /// Add a new shortcut key to the action item
        /// </summary>
        /// <param name="keyData">A System.Windows.Forms.Keys representing the key that was pressed, combined
        ///     with any modifier flags that indicate which Control, Shift, and Alt keys were
        ///     pressed at the same time. Possible values are obtained be applying the bitwise
        ///     OR (|) operator to constants from the System.Windows.Forms.Keys enumeration.</param>
        public void AddShortcutKey(Keys keyData)
        {
            mShortcutList.Add(keyData);
        }

        /// <summary>
        /// Return true if the received key is one of the shortcut for the action item
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        public bool CheckShortcutKey(Keys keyData)
        {
            return mShortcutList.Contains(keyData);
        }
        #endregion Shortcut key List
		#endregion Methods
	}
}
