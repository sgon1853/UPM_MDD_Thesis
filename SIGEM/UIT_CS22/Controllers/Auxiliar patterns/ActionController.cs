// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;

using SIGEM.Client.Logics;
using SIGEM.Client.Presentation;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the Action controller.
	/// An Action Controller contains the action items defined on an scenario.
	/// </summary>
	public class ActionController : Controller
	{
		#region Members
		/// <summary>
		/// Name of the Action.
		/// </summary>
		private string mName;
		/// <summary>
		/// List of Action items.
		/// </summary>
		private Dictionary<int, ActionItemController> mActionItems = new Dictionary<int, ActionItemController>();
		/// <summary>
		/// Boolean value indicating whether there is any ActionItem that will be
		///  enabled/disabled by a precondition.
		/// </summary>
		private bool mAnyActionItemEnabledByCondition = false;
		/// <summary>
		/// Value indicating whether the conjuntion policy is applied (when selection multiple).
		/// </summary>
		private string mConjunctionPolicyInEnabledByCondition = string.Empty;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ActionController' class.
		/// </summary>
		/// <param name="name">Name of the Action controller.</param>
		/// <param name="parent">Parent controller.</param>
		/// <param name="conjunctionPolicyInEnabledByCondition">String value indicating the conjunction policy (AND or OR).</param>
		public ActionController(string name, IUController parent, string conjunctionPolicyInEnabledByCondition)
			: base(parent)
		{
			mName = name;
			ConjunctionPolicyInEnabledByCondition = conjunctionPolicyInEnabledByCondition;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets the name of the Action.
		/// </summary>
		public string Name
		{
			get
			{
				return mName;
			}
		}
		/// <summary>
		/// Gets the list of Action items.
		/// </summary>
		public Dictionary<int, ActionItemController> ActionItems
		{
			get
			{
				return mActionItems;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether Action controller is enabled or not.
		/// </summary>
		public bool Enabled
		{
			get
			{
				foreach (ActionItemController lItem in mActionItems.Values)
				{
					if (!lItem.Enabled)
					{
						return false;
					}
				}
				return true;
			}
			set
			{
				foreach (ActionItemController lItem in mActionItems.Values)
				{
					lItem.Enabled = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether Action controller is selected or not.
		/// </summary>
		public bool IsSelected
		{
			get
			{
				foreach (ActionItemController lItem in mActionItems.Values)
				{
					if (!lItem.IsSelected)
					{
						return false;
					}
				}
				return true;
			}
			set
			{
				foreach (ActionItemController lItem in mActionItems.Values)
				{
					lItem.IsSelected = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether there is any ActionItem that
		/// will be enabled/disabled by a precondition.
		/// </summary>
		public bool AnyActionItemEnabledByCondition
		{
			get
			{
				return mAnyActionItemEnabledByCondition;
			}
			set
			{
				mAnyActionItemEnabledByCondition = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the
		/// conjuntion policy is applied or not (when selection multiple).
		/// </summary>
		public string ConjunctionPolicyInEnabledByCondition
		{
			get
			{
				return mConjunctionPolicyInEnabledByCondition;
			}
			set
			{
				mConjunctionPolicyInEnabledByCondition = value;
			}
		}
		/// <summary>
		/// Set the Refresh by Row flag to all action items
		/// </summary>
		public bool RefreshByRow
		{
			set
			{
				foreach (ActionItemController lItem in mActionItems.Values)
				{
					lItem.RefreshByRow = value;
				}
			}
		}
		/// <summary>
		/// Set the Refresh Master flag to all action items
		/// </summary>
		public bool RefreshMaster
		{
			set
			{
				foreach (ActionItemController lItem in mActionItems.Values)
				{
					lItem.RefreshMaster = value;
				}
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
		/// Specifies that the Action Item is going to be executed.
		/// </summary>
		public event EventHandler<CheckForPendingChangesEventArgs> BeforeActionItemExecution;
		#endregion Events

		#region Event Handlers
		private void HandleActionItemLaunchingScenario(object sender, LaunchingScenarioEventArgs e)
		{
			// Propagate the Event
			OnLaunchingScenario(e);
		}
		private void HandleActionItemContextRequired(object sender, ContextRequiredEventArgs e)
		{
			// Propagate the Event
			OnContextRequired(e);
		}
		private void HandleActionItemSelectedInstancesRequired(object sender, SelectedInstancesRequiredEventArgs e)
		{
			// Propagate the Event
			OnSelectedInstancesRequired(e);
		}
		/// <summary>
		/// Handles the Action Item SelectNextPrevious Event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleActionItemSelectNextPreviousInstance(object sender, SelectNextPreviousInstanceEventArgs e)
		{
			// Propagate the Event
			OnSelectNextPreviousInstance(e);
		}
		/// <summary>
		/// Handles the Action Item RefreshRequired Event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleActionItemRefreshRequired(object sender, RefreshRequiredEventArgs e)
		{
			// Propagate the Event
			OnRefreshRequired(e);
		}
		/// <summary>
		/// Handles the Action Item BeforeActionItemExecution Event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleActionItemBeforeActionItemExecution(object sender, CheckForPendingChangesEventArgs e)
		{
			// Propagate the Event
			OnBeforeActionItemExecution(e);
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
		/// Raises the BeforeActionItemExecution event.
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnBeforeActionItemExecution(CheckForPendingChangesEventArgs eventArgs)
		{
			EventHandler<CheckForPendingChangesEventArgs> handler = BeforeActionItemExecution;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		#endregion Event Raisers

		#region Methods
		#region Subitem management
		/// <summary>
		/// Adds an Action item to the Action controller.
		/// </summary>
		/// <param name="number">Number of the Action item.</param>
		/// <param name="alias">Alias of the Action item.</param>
		/// <param name="idXML">IdXML of the Action item.</param>
		/// <param name="agents">List of agents that can access to the Action item.</param>
		/// <param name="className">Class name of the Action item.</param>
		/// <param name="iuName">Interaction unit name of the Action item.</param>
		/// <param name="navigationalFilteringIdentity">Navigational filtering identifier.</param>
		/// <param name="actionItemType">Type of the actiom item.</param>
		/// <param name="objectActionEnabled">If the Object-Action feature is enabled or not.</param>
		/// <param name="multiSelectionSupported">If multiselection is supported or not.</param>
		/// <param name="attributesListForFormulas">List of attributes needed for evaluating the enabling/disabling formulas.</param>
		/// <returns>ActionItemController.</returns>
		/// <returns></returns>
		public ActionItemController Add(
			int number,
			string alias,
			string idXML,
			string[] agents,
			string classIUName,
			string iuName,
			string navigationalFilteringIdentity,
			ActionItemType actionItemType,
			bool objectActionEnabled,
			bool multiSelectionSupported,
			List<KeyValuePair<string, string[]>> attributesListForFormulas)
		{
			// Check the attributes for enabled formulas depending on the connected agent.
			List<string> lAttributesForFormulas = new List<string>();
			if (attributesListForFormulas != null && Logic.Agent.IsActiveFacet(agents))
			{
				foreach (KeyValuePair<string, string[]> lKeyPair in attributesListForFormulas)
				{
					// Check the connected agent visibility.
					if (Logic.Agent.IsActiveFacet(lKeyPair.Key))
					{
						foreach (string lAttribute in lKeyPair.Value)
						{
							// Avoid adding repeated elements.
							if (!lAttributesForFormulas.Contains(lAttribute))
							{
								lAttributesForFormulas.Add(lAttribute);
							}
						}
					}
				}
			}

			// Set the 'AnyActionItemEnabledByCondition' flag to true.
			if ((lAttributesForFormulas != null) && (lAttributesForFormulas.Count > 0))
			{
				AnyActionItemEnabledByCondition = true;
			}

			List<string> lAgents = new List<string>(agents);
			ActionItemController lActionItem = new ActionItemController(number, alias, idXML, lAgents, classIUName, iuName, navigationalFilteringIdentity, actionItemType, objectActionEnabled, multiSelectionSupported, lAttributesForFormulas, this);
			mActionItems.Add(number, lActionItem);

			lActionItem.RefreshRequired += new EventHandler<RefreshRequiredEventArgs>(HandleActionItemRefreshRequired);
			lActionItem.SelectNextPreviousInstance += new EventHandler<SelectNextPreviousInstanceEventArgs>(HandleActionItemSelectNextPreviousInstance);
			lActionItem.BeforeExecute += new EventHandler<CheckForPendingChangesEventArgs>(HandleActionItemBeforeActionItemExecution);
			lActionItem.SelectedInstancesRequired += new EventHandler<SelectedInstancesRequiredEventArgs>(HandleActionItemSelectedInstancesRequired);
			lActionItem.ContextRequired += new EventHandler<ContextRequiredEventArgs>(HandleActionItemContextRequired);
			lActionItem.LaunchingScenario += new EventHandler<LaunchingScenarioEventArgs>(HandleActionItemLaunchingScenario);

			return lActionItem;
		}
		#endregion Subitem management

		#region Initialize
		/// <summary>
		/// Initializes the Action controller.
		/// </summary>
		public void Initialize()
		{
			foreach (ActionItemController lItem in ActionItems.Values)
			{
				lItem.Initialize();
			}
		}
		#endregion Initialize

		#region GetActionItemsIdXML
		/// <summary>
		/// Gets a list containing the Interaction Unit IdXML's of the Action items.
		/// </summary>
		/// <returns>List of Interaction Unit IdXML's.</returns>
		public List<string> GetActionItemsIdXML()
		{
			List<string> lIdXMLs = new List<string>();
			if (ActionItems != null)
			{
				foreach (ActionItemController lItem in ActionItems.Values)
				{
					if (Logic.Agent.IsActiveFacet(lItem.Agents))
					{
						lIdXMLs.Add(lItem.IdXML);
					}
				}
			}
			return lIdXMLs;
		}
		#endregion GetActionItemsIdXML

		#region GetActionAttributesForFormulas
		/// <summary>
		/// Gets the Action attributes needed for the enabling/disabling formulas.
		/// </summary>
		/// <returns>Comma-separated Action attributes.</returns>
		public string GetActionAttributesForFormulas()
		{
			List<string> lActionAttributes = new List<string>();
			if ((ActionItems != null) && (ActionItems.Values.Count > 0))
			{
				foreach (ActionItemController lItem in ActionItems.Values)
				{
					if (Logic.Agent.IsActiveFacet(lItem.Agents))
					{
						// Avoid adding repeated elements.
						if (lItem.ActionItemAttributesForFormulas.Length > 0)
						{
							string[] lAttributes = lItem.ActionItemAttributesForFormulas.Trim().Split(',');
							foreach (string lAttr in lAttributes)
							{
								if (!lActionAttributes.Contains(lAttr))
								{
									lActionAttributes.Add(lAttr);
								}
							}
						}
					}
				}
			}
			// Returns a comma-sepatated string.
			return UtilFunctions.StringList2String(lActionAttributes, ",");
		}
		#endregion GetActionAttributesForFormulas

		#region Enable/Disable items based on selected instances

		/// <summary>
		/// Depends on selected instances and the received keys, action items will be enabled or disabled
		/// </summary>
		/// <param name="actionKeys">Action keys</param>
		public void EnableItemsBasedOnSelectedInstances(int numSelectedInstances, List<string> actionKeys)
		{
			// No instance selected. Apply the Object-Action paradigm
			if (numSelectedInstances == 0)
			{
				foreach (ActionItemController lItem in ActionItems.Values)
				{
					if (Logic.Agent.IsActiveFacet(lItem.Agents))
					{
						if (lItem.ObjectActionEnabled)
						{
							lItem.Enabled = false;
						}
						else
						{
							lItem.Enabled = true;
						}
					}
				}
				return;
			}

			// No information about keys. Apply the multiselection condition
			char[] lResultingKeys = {};
			if (actionKeys != null && actionKeys.Count > 0)
			{
				lResultingKeys = actionKeys[0].ToCharArray();

				// Combine all the keys in one
				for(int i = 1; i < actionKeys.Count; i++)
				{
					string lKey = actionKeys[i];
					// For each value in the key
					for (int j = 0; j < lKey.Length; j++)
					{
						if (lKey[j].Equals('0'))
						{
							// Disable the action item if conjuntion policy is AND
							if (this.ConjunctionPolicyInEnabledByCondition.Equals("AND"))
							{
								lResultingKeys[j] = '0';
							}
						}
						else
						{
							// Check the final keys
							if (lResultingKeys[j].Equals('0'))
							{
								if (this.ConjunctionPolicyInEnabledByCondition.Equals("AND"))
								{
									lResultingKeys[j] = '0';
								}
								else
								{
									lResultingKeys[j] = '1';
								}
							}
						}
					}
				}
			}

			// Apply the keys to the action items
			int lKeyCounter = 0;
			foreach (ActionItemController lItem in ActionItems.Values)
			{
				if (Logic.Agent.IsActiveFacet(lItem.Agents))
				{
					// If there is multiselection (and it is not allowed) or the key is '0', disable the item
					char lKey = '1';
					if (lResultingKeys.Length > lKeyCounter)
						lKey = lResultingKeys[lKeyCounter];

					if (lKey.Equals('0') || (numSelectedInstances > 1 && !lItem.MultiSelectionSupported))
					{
						lItem.Enabled = false;
					}
					else
					{
						lItem.Enabled = true;
					}
					lKeyCounter++;
				}
			}

		}

		#endregion Enable/Disable items based on selected instances

		#endregion Methods
	}
}
