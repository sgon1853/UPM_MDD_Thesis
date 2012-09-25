// v3.8.4.5.b
using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;

using SIGEM.Client.Presentation;
using SIGEM.Client.Logics;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the Navigation Items controller.
	/// A Navigation Item is an element that allows navigate between scenarios.
	/// </summary>
	public class NavigationItemController : Controller, INavigationItemSuscriber
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
		/// Role path of the Action item.
		/// </summary>
		private string mRolePath;
		/// <summary>
		/// Number of the Navigation Item.
		/// </summary>
		private int mNumber;
		/// <summary>
		/// Alias of the Navigation Item.
		/// </summary>
		private string mAlias;
		/// <summary>
		/// IdXML of the Navigation Item.
		/// </summary>
		private string mIdXML;
		/// <summary>
		/// List of agents of the Navigation Item.
		/// </summary>
		private List<string> mAgents;
		/// <summary>
		/// Trigger presentation of the Navigation Item.
		/// </summary>
		private ITriggerPresentation mTrigger;
		/// <summary>
		/// Navigational filtering of the Navigation Item.
		/// </summary>
		private string mNavigationalFilteringIdentity;
		/// <summary>
		/// Target scenario Alias
		/// </summary>
		private string mTargetScenarioAlias;
		/// <summary>
		/// Target scenario Alias Id XML
		/// </summary>
		private string mIdXMLTargetScenarioAlias;
		/// <summary>
		/// Selected instance Alias
		/// </summary>
		private string mInstanceAlias;
		/// <summary>
		/// Selected instance Alias Id XML
		/// </summary>
		private string mIdXMLInstanceAlias;
		/// <summary>
		/// Display Set to be shown in the target scenario title
		/// </summary>
		private string mDisplaySet2TargetScenario;
		/// <summary>
		/// NavigationItem attributes (comma-separated) needed for the enabling/disabling formulas.
		/// </summary>
		private string mNavigationItemAttributesForFormulas = string.Empty;
        /// <summary>
        /// Alternative key name
        /// </summary>
		private string mAlternateKeyName = string.Empty;
        /// <summary>
        /// Shortcut key list for this action item
        /// </summary>
        private List<Keys> mShortcutList = new List<Keys>();
        /// <summary>
        /// Refresh data when data in destination if navigation are modified
        /// </summary>
        private bool mRefreshInstanceFromNavigation = true;
        /// <summary>
        /// Default order criteria to be used in the destination
        /// </summary>
        private string mDefaultOrderCriteria = "";
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'NavigationItemController' class.
		/// </summary>
		/// <param name="number">Number of the Navigation Item.</param>
		/// <param name="alias">Alias of the Navigation Item.</param>
		/// <param name="idXML">IdXML of the Navigation Item.</param>
		/// <param name="agents">List of agents that can acces to the Navigation Item.</param>
		/// <param name="classIUName">Class name of the Navigation Item.</param>
		/// <param name="iuName">Interaction unit name of the Navigation Item.</param>
		/// <param name="rolePath">Role path of the Navigation Item.</param>
		/// <param name="navigationalFilteringIdentity">Navigational filtering identifier.</param>
		/// <param name="parent">Parent controller.</param>
		/// <param name="targetScenarioAlias">Alias of the Target scenario</param>
		/// <param name="idXMLTargetScenarioAlias">IdXML of the Target scenario</param>
		/// <param name="instanceAlias">Selected instance Alias</param>
		/// <param name="idXMLInstanceAlias">IdXML selected instance Alias</param>
		/// <param name="displaySetToTargetScenario">Display Set to be shown in the target scenario. Attributes separates by commas</param>
		/// <param name="attributesListForFormulas">List of attributes needed for evaluating the enabling/disabling formulas.</param>
		[Obsolete("Since version 3.5.4.3")]
		public NavigationItemController(
			int number,
			string alias,
			string idXML,
			List<string> agents,
			string classIUName,
			string iuName,
			string rolePath,
			string navigationalFilteringIdentity,
			string targetScenarioAlias,
			string idXMLTargetScenarioAlias,
			string instanceAlias,
			string idXMLInstanceAlias,
			string displaySetToTargetScenario,
			List<string> attributesListForFormulas,
			NavigationController parent)
			: base(parent)
		{
			mNumber = number;
			mAlias = alias;
			mIdXML = idXML;
			mAgents = agents;
			ClassIUName = classIUName;
			IUName = iuName;
			RolePath = rolePath;
			NavigationalFilteringIdentity = navigationalFilteringIdentity;
			TargetScenarioAlias = targetScenarioAlias;
			IdXMLTargetScenarioAlias = idXMLTargetScenarioAlias;
			InstanceAlias = instanceAlias;
			IdXMLInstanceAlias = idXMLInstanceAlias;
			DisplaySet2TargetScenario = displaySetToTargetScenario;

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

				NavigationItemAttributesForFormulas = lAttributes.ToString();
			}
		}
		/// <summary>
		/// Initializes a new instance of the 'NavigationItemController' class.
		/// </summary>
		/// <param name="number">Number of the Navigation Item.</param>
		/// <param name="alias">Alias of the Navigation Item.</param>
		/// <param name="idXML">IdXML of the Navigation Item.</param>
		/// <param name="agents">List of agents that can acces to the Navigation Item.</param>
		/// <param name="classIUName">Class name of the Navigation Item.</param>
		/// <param name="iuName">Interaction unit name of the Navigation Item.</param>
		/// <param name="rolePath">Role path of the Navigation Item.</param>
		/// <param name="navigationalFilteringIdentity">Navigational filtering identifier.</param>
		/// <param name="parent">Parent controller.</param>
		/// <param name="targetScenarioAlias">Alias of the Target scenario</param>
		/// <param name="idXMLTargetScenarioAlias">IdXML of the Target scenario</param>
		/// <param name="instanceAlias">Selected instance Alias</param>
		/// <param name="idXMLInstanceAlias">IdXML selected instance Alias</param>
		/// <param name="displaySetToTargetScenario">Display Set to be shown in the target scenario. Attributes separates by commas</param>
		/// <param name="attributesListForFormulas">List of attributes needed for evaluating the enabling/disabling formulas.</param>
		public NavigationItemController(
			int number,
			string alias,
			string idXML,
			List<string> agents,
			string classIUName,
			string iuName,
			string rolePath,
			string navigationalFilteringIdentity,
			string targetScenarioAlias,
			string idXMLTargetScenarioAlias,
			string instanceAlias,
			string idXMLInstanceAlias,
			string displaySetToTargetScenario,
			List<string> attributesListForFormulas,
			NavigationController parent,
			string alternateKeyName)
			: base(parent)
		{
			mNumber = number;
			mAlias = alias;
			mIdXML = idXML;
			mAgents = agents;
			ClassIUName = classIUName;
			IUName = iuName;
			RolePath = rolePath;
			NavigationalFilteringIdentity = navigationalFilteringIdentity;
			TargetScenarioAlias = targetScenarioAlias;
			IdXMLTargetScenarioAlias = idXMLTargetScenarioAlias;
			InstanceAlias = instanceAlias;
			IdXMLInstanceAlias = idXMLInstanceAlias;
			DisplaySet2TargetScenario = displaySetToTargetScenario;
			AlternateKeyName = alternateKeyName;

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

				NavigationItemAttributesForFormulas = lAttributes.ToString();
			}
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets the class name where is defined the navigation associated with this Navigation Item.
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
		/// Gets or sets the interaction unit name or type (depending platform) where the Navigation Item is.
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
		/// Gets or sets the Navigation Item role path.
		/// </summary>
		public string RolePath
		{
			get
			{
				return mRolePath;
			}
			set
			{
				mRolePath = value;
			}
		}
		/// <summary>
		/// Gets the Navigation Item number.
		/// </summary>
		public int Number
		{
			get
			{
				return mNumber;
			}
		}
		/// <summary>
		/// Gets the Navigation Item alias.
		/// </summary>
		public string Alias
		{
			get
			{
				return mAlias;
			}
		}
		/// <summary>
		/// Gets the Navigation Item XML identifier.
		/// </summary>
		public string IdXML
		{
			get
			{
				return mIdXML;
			}
		}
		/// <summary>
		/// Gets the list of agents that can acces to the Navigation Item.
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
		/// Gets or sets the navigation trigger event associate with the Navigation Item.
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
		/// Gets or sets the navigational filtering of the Navigation Item.
		/// </summary>
		public string NavigationalFilteringIdentity
		{
			get
			{
				return mNavigationalFilteringIdentity;
			}
			protected set
			{
				mNavigationalFilteringIdentity = value;
			}
		}
		/// <summary>
		/// Gets the parent controller of the Navigation Item.
		/// </summary>
		public new NavigationController Parent
		{
			get
			{
				return base.Parent as NavigationController;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether trigger of the Navigation Item is enabled or not.
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
		/// Gets or sets a boolean value indicating whether the Navigation Item is focused or not.
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
		/// Target scenario Alias
		/// </summary>
		public string TargetScenarioAlias
		{
			get
			{
				return mTargetScenarioAlias;
			}
			set
			{
				mTargetScenarioAlias = value;
			}
		}
		/// <summary>
		/// Target scenario Alias Id XML
		/// </summary>
		public string IdXMLTargetScenarioAlias
		{
			get
			{
				return mIdXMLTargetScenarioAlias;
			}
			set
			{
				mIdXMLTargetScenarioAlias = value;
			}
		}
		/// <summary>
		/// Selected instance Alias
		/// </summary>
		public string InstanceAlias
		{
			get
			{
				return mInstanceAlias;
			}
			set
			{
				mInstanceAlias = value;
			}
		}
		/// <summary>
		/// Selected instance Alias Id XML
		/// </summary>
		public string IdXMLInstanceAlias
		{
			get
			{
				return mIdXMLInstanceAlias;
			}
			set
			{
				mIdXMLInstanceAlias = value;
			}
		}
		/// <summary>
		/// Display Set to be shown in the target scenario title
		/// </summary>
		public string DisplaySet2TargetScenario
		{
			get
			{
				return mDisplaySet2TargetScenario;
			}
			set
			{
				mDisplaySet2TargetScenario = value;
			}
		}
		/// <summary>
		/// Gets the NavigationItem attributes (comma-separated) needed for
		/// the enabling/disabling formulas.
		/// </summary>
		public string NavigationItemAttributesForFormulas
		{
			get
			{
				return mNavigationItemAttributesForFormulas;
			}
			set
			{
				mNavigationItemAttributesForFormulas = value;
			}
		}
		/// <summary>
		/// Gets or sets the alternate key name that is going to be used.
		/// </summary>
		public string AlternateKeyName
		{
			get
			{
				return mAlternateKeyName;
			}
			set
			{
				mAlternateKeyName = value;
			}
		}
        /// <summary>
        /// Refresh data when data in destination if navigation are modified
        /// </summary>
        public bool RefreshInstanceFromNavigation
        {
            get
            {
                return mRefreshInstanceFromNavigation;
            }
            set
            {
                mRefreshInstanceFromNavigation = value;
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
		#endregion Events

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
		#region Event Handlers
        /// <summary>
        /// Handles the Refesh instance event from a navigation scenario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleRefreshRequired(object sender, RefreshRequiredEventArgs e)
        {
            if (RefreshInstanceFromNavigation)
            {
                //List<Oid> oidList = new List<Oid>();
                //oidList.Add(e.CurrentOid);
                OnRefreshRequired(e);
            }
        }
		#endregion Event Handlers

		#region Methods
		#region Execute
		/// <summary>
		/// Executes a Navigation Item navigation trigger associated.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">TriggerEventArgs.</param>
		public void Execute(Object sender, TriggerEventArgs e)
		{
			try
			{
				// Update context.
				ContextRequiredEventArgs contextEventArgs = new ContextRequiredEventArgs();
				OnContextRequired(contextEventArgs);

				SelectedInstancesRequiredEventArgs lSelectedInstancesEventArgs = new SelectedInstancesRequiredEventArgs();
				OnSelectedInstancesRequired(lSelectedInstancesEventArgs);

				if ((lSelectedInstancesEventArgs.SelectedInstances != null) && (lSelectedInstancesEventArgs.SelectedInstances.Count > 0))
				{
					// Launch scenario.
					// Calculate the title text for the target scenario.
					string text2Title = "";
					if (lSelectedInstancesEventArgs.SelectedInstances.Count == 1)
					{
						Oid lAuxOid = lSelectedInstancesEventArgs.SelectedInstances[0];
						if (AlternateKeyName != string.Empty)
						{
							lAuxOid = Logics.Logic.GetAlternateKeyFromOid(lAuxOid, AlternateKeyName);
						}
						text2Title = UtilFunctions.GetText2Title(TargetScenarioAlias, 
										InstanceAlias,
										lAuxOid, 
										DisplaySet2TargetScenario);
					}
					ScenarioManager.LaunchNavigationScenario(
						new ExchangeInfoNavigation(
							ClassIUName,
							IUName,
							RolePath,
							NavigationalFilteringIdentity,
							lSelectedInstancesEventArgs.SelectedInstances,
							contextEventArgs.Context,
							text2Title, DefaultOrderCriteria), this);
				}
				else
				{
					string lMessage = CultureManager.TranslateString(LanguageConstantKeys.L_NO_SELECTION, LanguageConstantValues.L_NO_SELECTION);
					ScenarioManager.LaunchErrorScenario(new Exception(lMessage));
				}
			}
			catch (Exception err)
			{
				ScenarioManager.LaunchErrorScenario(err);
			}
		}
		#endregion Execute

		#region Initialize
		/// <summary>
		/// Initializes the Navigation Item.
		/// </summary>
		public void Initialize()
		{
			if (this.Trigger != null)
			{
				// NAVIGATIONITEM MULTILANGUAGE: Apply multilaguage to the navigation item trigger.
				this.Trigger.Value = CultureManager.TranslateString(this.IdXML, this.Alias, this.Trigger.Value.ToString());
				// Multilanguage to the navigational information aliases
				mTargetScenarioAlias = CultureManager.TranslateString(mIdXMLTargetScenarioAlias, mTargetScenarioAlias);
				mInstanceAlias = CultureManager.TranslateString(mIdXMLInstanceAlias, mInstanceAlias);
				// END NAVIGATIONITEM MULTILANGUAGE

				// Applies agent visibility.
				if (Logic.Agent.IsActiveFacet(mAgents))
				{
					this.Trigger.Visible = true;
				}
				else
				{
					this.Trigger.Visible = false;
				}
			}
		}
		#endregion Initialize

        #region Shortcut key List
        /// <summary>
        /// Add the double click shortcut to the navigation item
        /// </summary>
        public void AddShortcutKey()
        {
            mShortcutList.Add(Keys.NoName);
        }

        /// <summary>
        /// Add a new shortcut key to the navigation item
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

        #region INavigationItemSuscriber

        public event EventHandler<RefreshRequiredEventArgs> RefreshRequired;

        void INavigationItemSuscriber.SuscribeNavigationEvents(INavigationItemEvents navigationItemEvents)
        {
            if (navigationItemEvents != null)
            {
                navigationItemEvents.RefreshRequired += new EventHandler<RefreshRequiredEventArgs>(HandleRefreshRequired);
            }
        }
        #endregion INavigationItemSuscriber
	}
}
