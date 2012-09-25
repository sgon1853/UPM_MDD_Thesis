// v3.8.4.5.b
using System;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Collections.Generic;

using SIGEM.Client.Presentation;
using SIGEM.Client.Oids;
using SIGEM.Client.Logics;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the Navigation controller.
	/// A Navigation Controller contains all the navigations defined on an scenario.
	/// </summary>
	public class NavigationController : Controller
	{
		#region Members
		/// <summary>
		/// Navigation name.
		/// </summary>
		private string mName;
		/// <summary>
		/// List of Navigation Items
		/// </summary>
		private Dictionary<int, NavigationItemController> mNavigationItems = new Dictionary<int, NavigationItemController>();
		/// <summary>
		/// Boolean value indicating whether there is any NavigationItem that will be enabled/disabled by a precondition.
		/// </summary>
		private bool mAnyNavigationItemEnabledByCondition = false;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'NavigationController' class.
		/// </summary>
		/// <param name="name">Name of the Navigation controller.</param>
		/// <param name="parent">Parent controller.</param>
		public NavigationController(string name, IUController parent)
			: base(parent)
		{
			mName = name;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets the parent controller of the Navigation.
		/// </summary>
		public new IUQueryController Parent
		{
			get
			{
				return base.Parent as IUQueryController;
			}
		}
		/// <summary>
		/// Gets the Navigation name.
		/// </summary>
		public string Name
		{
			get
			{
				return mName;
			}
		}
		/// <summary>
		/// Gets the Navigation Items list.
		/// </summary>
		public Dictionary<int, NavigationItemController> NavigationItems
		{
			get
			{
				return mNavigationItems;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether Navigation Controller is enabled or not.
		/// </summary>
		public bool Enabled
		{
			get
			{
				foreach (NavigationItemController lItem in mNavigationItems.Values)
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
				foreach (NavigationItemController lItem in mNavigationItems.Values)
				{
					lItem.Enabled = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether a Nagigation Item in the Navigation Controller is selected or not.
		/// </summary>
		public bool IsSelected
		{
			get
			{
				foreach (NavigationItemController lItem in mNavigationItems.Values)
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
				foreach (NavigationItemController lItem in mNavigationItems.Values)
				{
					lItem.IsSelected = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether there is any NavigationItem that
		/// will be enabled/disabled by a precondition.
		/// </summary>
		public bool AnyNavigationItemEnabledByCondition
		{
			get
			{
				return mAnyNavigationItemEnabledByCondition;
			}
			set
			{
				mAnyNavigationItemEnabledByCondition = value;
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
		#endregion Events

		#region Event Handlers
		private void HandleNavigationItemLaunchingScenario(object sender, LaunchingScenarioEventArgs e)
		{
			// Propagate the Event
			OnLaunchingScenario(e);
		}

		private void HandleNavigationItemContextRequired(object sender, ContextRequiredEventArgs e)
		{
			// Propagate the Event
			OnContextRequired(e);
		}

		private void HandleNavigationItemSelectedInstancesRequired(object sender, SelectedInstancesRequiredEventArgs e)
		{
			// Propagate the Event
			OnSelectedInstancesRequired(e);
		}
        private void HandleNavigationItemRefreshRequired(object sender, RefreshRequiredEventArgs e)
        {
            // Propagate the Event
            OnRefreshRequired(e);
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
		#endregion Event Raisers

		#region Methods
		#region Subitem management
		/// Adds a Navigation Item to the Navigation controller.
		/// </summary>
		/// <param name="number">Number of the Navigation Item.</param>
		/// <param name="alias">Alias of the Navigation Item.</param>
		/// <param name="idXML">IdXML of the Navigation Item.</param>
		/// <param name="agents">List of agents that can access to the Navigation Item.</param>
		/// <param name="classIUName">Class name of the Navigation Item.</param>
		/// <param name="iuName">Interaction unit name of the Navigation Item.</param>
		/// <param name="rolePath">Role path of the Navigation Item.</param>
		/// <param name="navigationalFilteringIdentity">Navigational filtering.</param>
		/// <param name="targetScenarioAlias">Alias of the target scenario.</param>
		/// <param name="idXMLTargetScenarioAlias">IdXML of the Target scenario</param>
		/// <param name="instanceAlias">Selected instance Alias</param>
		/// <param name="idXMLInstanceAlias">IdXML selected instance Alias</param>
		/// <param name="displaySetToTargetScenario">Display Set to be shown in the target scenario. Attributes separates by commas</param>
		/// <param name="attributesListForFormulas ">List of attributes needed for evaluating the enabling/disabling formulas.</param>
		/// <returns>NavigationItemController</returns>
		[Obsolete("Since version 3.5.4.3")]
		public NavigationItemController Add(
			int number,
			string alias,
			string idXML,
			string[] agents,
			string classIUName,
			string iuName,
			string rolePath,
			string navigationalFilteringIdentity,
			string targetScenarioAlias,
			string idXMLTargetScenarioAlias,
			string instanceAlias,
			string idXMLInstanceAlias,
			string displaySetToTargetScenario,
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

			// Set the 'AnyNavigationItemEnabledByCondition' flag to true.
			if ((lAttributesForFormulas != null) && (lAttributesForFormulas.Count > 0))
			{
				AnyNavigationItemEnabledByCondition = true;
			}

			List<string> lAgents = new List<string>(agents);
			NavigationItemController lNavigationItem = new NavigationItemController(
				number,
				alias,
				idXML,
				lAgents,
				classIUName,
				iuName,
				rolePath,
				navigationalFilteringIdentity,
				targetScenarioAlias,
				idXMLTargetScenarioAlias,
				instanceAlias,
				idXMLInstanceAlias,
				displaySetToTargetScenario,
				lAttributesForFormulas,
				this);
			lNavigationItem.SelectedInstancesRequired += new EventHandler<SelectedInstancesRequiredEventArgs>(HandleNavigationItemSelectedInstancesRequired);
			lNavigationItem.ContextRequired += new EventHandler<ContextRequiredEventArgs>(HandleNavigationItemContextRequired);
			lNavigationItem.LaunchingScenario += new EventHandler<LaunchingScenarioEventArgs>(HandleNavigationItemLaunchingScenario);
			mNavigationItems.Add(number, lNavigationItem);
			return lNavigationItem;
		}
		/// Adds a Navigation Item to the Navigation controller.
		/// </summary>
		/// <param name="number">Number of the Navigation Item.</param>
		/// <param name="alias">Alias of the Navigation Item.</param>
		/// <param name="idXML">IdXML of the Navigation Item.</param>
		/// <param name="agents">List of agents that can access to the Navigation Item.</param>
		/// <param name="classIUName">Class name of the Navigation Item.</param>
		/// <param name="iuName">Interaction unit name of the Navigation Item.</param>
		/// <param name="rolePath">Role path of the Navigation Item.</param>
		/// <param name="navigationalFilteringIdentity">Navigational filtering.</param>
		/// <param name="targetScenarioAlias">Alias of the target scenario.</param>
		/// <param name="idXMLTargetScenarioAlias">IdXML of the Target scenario</param>
		/// <param name="instanceAlias">Selected instance Alias</param>
		/// <param name="idXMLInstanceAlias">IdXML selected instance Alias</param>
		/// <param name="displaySetToTargetScenario">Display Set to be shown in the target scenario. Attributes separates by commas</param>
		/// <param name="attributesListForFormulas ">List of attributes needed for evaluating the enabling/disabling formulas.</param>
		/// <returns>NavigationItemController</returns>
		public NavigationItemController Add(
			int number,
			string alias,
			string idXML,
			string[] agents,
			string classIUName,
			string iuName,
			string rolePath,
			string navigationalFilteringIdentity,
			string targetScenarioAlias,
			string idXMLTargetScenarioAlias,
			string instanceAlias,
			string idXMLInstanceAlias,
			string displaySetToTargetScenario,
			List<KeyValuePair<string, string[]>> attributesListForFormulas,
			string alternateKeyName)
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

			// Set the 'AnyNavigationItemEnabledByCondition' flag to true.
			if ((lAttributesForFormulas != null) && (lAttributesForFormulas.Count > 0))
			{
				AnyNavigationItemEnabledByCondition = true;
			}

			List<string> lAgents = new List<string>(agents);
			NavigationItemController lNavigationItem = new NavigationItemController(
				number,
				alias,
				idXML,
				lAgents,
				classIUName,
				iuName,
				rolePath,
				navigationalFilteringIdentity,
				targetScenarioAlias,
				idXMLTargetScenarioAlias,
				instanceAlias,
				idXMLInstanceAlias,
				displaySetToTargetScenario,
				lAttributesForFormulas,
				this,
				alternateKeyName);
			lNavigationItem.SelectedInstancesRequired += new EventHandler<SelectedInstancesRequiredEventArgs>(HandleNavigationItemSelectedInstancesRequired);
			lNavigationItem.ContextRequired += new EventHandler<ContextRequiredEventArgs>(HandleNavigationItemContextRequired);
			lNavigationItem.LaunchingScenario += new EventHandler<LaunchingScenarioEventArgs>(HandleNavigationItemLaunchingScenario);
            lNavigationItem.RefreshRequired += new EventHandler<RefreshRequiredEventArgs>(HandleNavigationItemRefreshRequired);
			mNavigationItems.Add(number, lNavigationItem);
			return lNavigationItem;
		}
		#endregion Subitem management

		#region Initialize
		/// <summary>
		/// Initilalizes the Navigation controller.
		/// </summary>
		public void Initialize()
		{
			foreach (NavigationItemController lItem in mNavigationItems.Values)
			{
				lItem.Initialize();
			}
		}
		#endregion Initialize

		#region GetNavigationItemsIdXML
		/// <summary>
		/// Gets a list containing the Interaction Unit IdXML's of the Navigation items.
		/// </summary>
		/// <returns>List of Interaction Unit IdXML's.</returns>
		public List<string> GetNavigationItemsIdXML()
		{
			List<string> lIdXMLs = new List<string>();
			if (NavigationItems != null)
			{
				foreach (NavigationItemController lItem in NavigationItems.Values)
				{
					if (Logic.Agent.IsActiveFacet(lItem.Agents))
					{
						lIdXMLs.Add(lItem.IdXML);
					}
				}
			}
			return lIdXMLs;
		}
		#endregion GetNavigationItemsIdXML

		#region GetNavigationAttributesForFormulas
		/// <summary>
		/// Gets the Navigation attributes needed for the enabling/disabling formulas.
		/// </summary>
		/// <returns>Comma-separated Navigation attributes.</returns>
		public string GetNavigationAttributesForFormulas()
		{
			List<string> lNavigationAttributes = new List<string>();
			if ((NavigationItems != null) && (NavigationItems.Values.Count > 0))
			{
				foreach (NavigationItemController lItem in NavigationItems.Values)
				{
					if (Logic.Agent.IsActiveFacet(lItem.Agents))
					{
						// Avoid adding repeated elements.
						if (lItem.NavigationItemAttributesForFormulas.Length > 0)
						{
							string[] lAttributes = lItem.NavigationItemAttributesForFormulas.Trim().Split(',');
							foreach (string lAttr in lAttributes)
							{
								if (!lNavigationAttributes.Contains(lAttr))
								{
									lNavigationAttributes.Add(lAttr);
								}
							}
						}
					}
				}
			}
			// Returns a comma-sepatated string.
			return UtilFunctions.StringList2String(lNavigationAttributes, ",");
		}
		#endregion GetNavigationAttributesForFormulas

		#region Set navigation items enabled state

		/// <summary>
		/// Depends on selected instances and the received keys, navigation items will be enabled or disabled
		/// </summary>
		/// <param name="navigationKeys">Navigation keys</param>
		public void EnableItemsBasedOnSelectedInstances(int numSelectedInstances, List<string> navigationKeys)
		{
			// No instance or more than one selected, disable all the navigation items
			if (numSelectedInstances != 1)
			{
				Enabled = false;
				return;
			}

			// If no keys, enable all the navigations
			if (navigationKeys == null || navigationKeys.Count == 0)
			{
				Enabled = true;
				return;
			}

			// There is only one instance, evaluate its key
			char[] lResultingKeys = navigationKeys[0].ToCharArray();
			// Apply the keys to the navigation items
			int lKeyCounter = 0;
			foreach (NavigationItemController lItem in NavigationItems.Values)
			{
				if (Logic.Agent.IsActiveFacet(lItem.Agents))
				{
					char lKey = '1';
					if (lResultingKeys.Length > lKeyCounter)
						lKey = lResultingKeys[lKeyCounter];

					if (lKey.Equals('0'))
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

		#endregion Set navigation items enabled state

		#endregion Methods
	}
}
