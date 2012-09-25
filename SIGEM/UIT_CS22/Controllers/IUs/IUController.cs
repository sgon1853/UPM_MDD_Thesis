// v3.8.4.5.b
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;
using SIGEM.Client.Presentation;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the IUController.
	/// </summary>
	public abstract class IUController : Controller
	{
		#region Members
		/// <summary>
		/// Interaction Unit name.
		/// </summary>
		private string mName = string.Empty;
		/// <summary>
		/// Interaction Unit alias.
		/// </summary>
		private string mAlias = string.Empty;
		/// <summary>
		/// XML Interaction Unit identifier.
		/// </summary>
		private string mIdXML = string.Empty;
		/// <summary>
		/// IU context
		/// </summary>
		protected IUContext mContext = null;
		/// <summary>
		/// Presentation of the execute service trigger
		/// </summary>
		private ITriggerPresentation mOkTrigger = null;
		/// <summary>
		/// Presentation of the cancel service trigger
		/// </summary>
		private ITriggerPresentation mCancelTrigger = null;
		/// <summary>
		/// Scenario of the IU
		/// </summary>
		private IScenarioPresentation mScenario = null;
		/// <summary>
		/// Scenario name for Preferences. It will be the key used to find it
		/// </summary>
		private string mScenarioPrefsName = string.Empty;
		/// <summary>
		/// Presentation of the save position trigger
		/// </summary>
		private ITriggerPresentation mSavePositionTrigger = null;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets the Interaction Unit name.
		/// </summary>
		public virtual string Name
		{
			get
			{
				return mName;
			}
			protected set
			{
				mName = value;
			}
		}
		/// <summary>
		/// Gets the Interaction Unit alias.
		/// </summary>
		public virtual string Alias
		{
			get
			{
				return mAlias;
			}
			protected set
			{
				mAlias = value;
			}
		}
		/// <summary>
		/// Gets the XML Interaction Unit identifier.
		/// </summary>
		public virtual string IdXML
		{
			get
			{
				return mIdXML;
			}
			protected set
			{
				mIdXML = value;
			}
		}
		/// <summary>
		/// Gets or sets the ExchangeInformation.
		/// </summary>
		public virtual ExchangeInfo ExchangeInformation 
		{ get 
			{ 
				return null; 
			} 
			set { } 
		}
		/// <summary>
		/// Gets or sets the Ok trigger.
		/// </summary>
		public virtual ITriggerPresentation OkTrigger
		{
			get
			{
				return mOkTrigger;
			}
			set
			{
				if (mOkTrigger != null)
				{
					mOkTrigger.Triggered -= new EventHandler<TriggerEventArgs>(HandleExecuteOk);
				}
				mOkTrigger = value;
				if (mOkTrigger != null)
				{
					mOkTrigger.Triggered += new EventHandler<TriggerEventArgs>(HandleExecuteOk);
				}
			}
		}
		/// <summary>
		/// Gets or sets the cancel trigger.
		/// </summary>
		public virtual ITriggerPresentation CancelTrigger
		{
			get
			{
				return mCancelTrigger;
			}
			set
			{
				if (mCancelTrigger != null)
				{
					mCancelTrigger.Triggered -= new EventHandler<TriggerEventArgs>(HandleExecuteCancel);
				}
				mCancelTrigger = value;
				if (mCancelTrigger != null)
				{
					mCancelTrigger.Triggered += new EventHandler<TriggerEventArgs>(HandleExecuteCancel);
				}
			}
		}
		/// <summary>
		/// Gets or sets the current scenario.
		/// </summary>
		public virtual IScenarioPresentation Scenario
		{
			get
			{
				return mScenario;
			}
			set
			{
				if (mScenario != null)
				{
					mScenario.Form.FormClosing -= new FormClosingEventHandler(HandleScenarioFormClosing);
					if (mScenario.ScenarioType == ScenarioType.Population ||
						mScenario.ScenarioType == ScenarioType.MasterDetail)
					{
						mScenario.Form.Shown -= new EventHandler(HandleFormShown);
					}
				}
				mScenario = value;
				if (mScenario != null)
				{
					mScenario.Form.FormClosing += new FormClosingEventHandler(HandleScenarioFormClosing);
					if (mScenario.ScenarioType == ScenarioType.Population ||
						mScenario.ScenarioType == ScenarioType.MasterDetail)
					{
						mScenario.Form.Shown += new EventHandler(HandleFormShown);
					}
				}
			}
		}
		/// <summary>
		/// Gets the selected instance of the IU
		/// </summary>
		public virtual List<Oid> InstancesSelected
		{
			get
			{
				return null;
			}
		}
		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		public virtual IUContext Context
		{
			get
			{
				return mContext;
			}
			set
			{
				mContext = value;
			}
		}
		/// <summary>
		/// Gets or Sets the Scenario name for User Prefrerences 
		/// </summary>
		private string ScenarioPrefsName
		{
			get
			{
				return mScenarioPrefsName;
			}
			set
			{
				mScenarioPrefsName = value;
			}
		}
		/// <summary>
		/// Gets or sets the cancel trigger.
		/// </summary>
		public virtual ITriggerPresentation SavePositionTrigger
		{
			get
			{
				return mSavePositionTrigger;
			}
			set
			{
				if (mSavePositionTrigger != null)
				{
					mSavePositionTrigger.Triggered -= new EventHandler<TriggerEventArgs>(OnSavePosition);
				}
				mSavePositionTrigger = value;
				if (mSavePositionTrigger != null)
				{
					mSavePositionTrigger.Triggered += new EventHandler<TriggerEventArgs>(OnSavePosition);
				}
			}
		}
		#endregion Properties

		#region Events
		///// <summary>
		///// Occurs when selected instance is changed.
		///// </summary>
		//public event SelectedInstanceChangedEventHandler OnSelectedInstanceChanged;
		///// <summary>
		///// Occurs when data of controller has updated.
		///// </summary>
		//public event UpdateDataEventHandler OnUpdateData;
		///// <summary>
		///// Notification event between IU Controllers
		///// </summary>
		//public event EventHandler<NotifyCommandEventArgs> NotifyCommand;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Executes actions related to Ok trigger event.
		/// </summary>
		private void HandleExecuteOk(Object sender, TriggerEventArgs e)
		{
			ProcessExecuteOk();
		}
		/// <summary>
		/// Executes actions related to Cancel event.
		/// </summary>
		private void HandleExecuteCancel(Object sender, TriggerEventArgs e)
		{
			ProcessExecuteCancel();
		}
		/// <summary>
		/// Executes actions related to FormClosing event.
		/// </summary>
		private void HandleScenarioFormClosing(object sender, FormClosingEventArgs e)
		{
			ProcessScenarioFormClosing(e);
		}
		/// <summary>
		/// Manage the Shown event of the form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void HandleFormShown(object sender, EventArgs e)
		{
			ProcessScenarioShown();
		}
		#endregion Event Handlers

		#region Process events
		/// <summary>
		/// Actions related with the Ok button.
		/// </summary>
		protected virtual void ProcessExecuteOk()
		{
			CloseScenario();
		}
		/// <summary>
		/// Actions related with the Cancel button.
		/// </summary>
		protected virtual void ProcessExecuteCancel()
		{
			CloseScenario();
		}
		/// <summary>
		/// Actions related with the FormClosing event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void ProcessScenarioFormClosing(FormClosingEventArgs e)
		{
		}
		/// <summary>
		/// Actions to do when the scenario is shown.
		/// </summary>
		protected virtual void ProcessScenarioShown()
		{
			ApplyPreferencesAfterShown();
		}
		#endregion Process events

		#region Methods
		/// <summary>
		/// Configures the controller from the data context.
		/// </summary>
		/// <param name="context">IU context</param>
		public virtual void ConfigureByContext(IUContext context){}
		/// <summary>
		/// Updates the data of the context.
		/// </summary>
		public virtual void UpdateContext()
		{
			IUClassContext lClassContext = Context as IUClassContext;

			if (lClassContext != null)
			{
				if (lClassContext.SelectedOids != null)
				{
					lClassContext.SelectedOids.Clear();
				}

				List<Oid> lInstancesSelected = InstancesSelected;
				if (lInstancesSelected != null)
				{
					if (lClassContext.SelectedOids == null)
					{
						lClassContext.SelectedOids = new List<Oid>();
					}
					lClassContext.SelectedOids.AddRange(lInstancesSelected);
				}
			}
		}
		/// <summary>
		/// Closes the scenario.
		/// </summary>
		protected virtual void CloseScenario()
		{
			// Close Scenario
			if (Scenario != null)
			{
				Scenario.Close();
			}
		}
		/// <summary>
		/// Initializes the IU controller.
		/// </summary>
		public virtual void Initialize()
		{
			if (Context != null)
			{
				Context.Initialized = false;
			}

			// Set context
			if (Context != null)
			{
				ConfigureByContext(Context);

				Context.Initialized = true;
			}

			ApplyMultilanguage();

			ApplyPreferences();
		}
		/// <summary>
		/// Apply multilanguage to the scenario.
		/// </summary>
		public virtual void ApplyMultilanguage()
		{
			// Apply multilanguage to the scenario.
			if (this.Scenario != null)
			{
				string title = CultureManager.TranslateString(this.IdXML, this.Alias, this.Scenario.Title);

				if (ExchangeInformation != null && ExchangeInformation.Text2Title != "")
				{
					title = ExchangeInformation.Text2Title;
				}
				this.Scenario.Title = title;
			}

			// Ok button.
			if (this.OkTrigger != null)
			{
				this.OkTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_OK, LanguageConstantValues.L_OK, this.OkTrigger.Value.ToString());
			}

			// Cancel button.
			if (this.CancelTrigger != null)
			{
				this.CancelTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_CANCEL, LanguageConstantValues.L_CANCEL, this.CancelTrigger.Value.ToString());
			}

			// SavePosition Trigger
			if (SavePositionTrigger != null)
			{
				SavePositionTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_SAVEPOSITION, LanguageConstantValues.L_POP_UP_MENU_SAVEPOSITION);
			}
		}

		#region Preferences
		/// <summary>
		/// Executes actions related to SavePosition event.
		/// </summary>
		protected virtual void OnSavePosition(Object sender, TriggerEventArgs e)
		{
			SavePosition();
		}
		/// <summary>
		/// Get and Apply the existing preferences for this scenario
		/// </summary>
		private void ApplyPreferences()
		{
			if (Scenario == null || Scenario.Form == null)
			{
				return;
			}

			if ((Context as IUClassContext) == null)
			{
				return;
			}

			// Set the scenario name for Preferences
			ScenarioPrefsName = (Context as IUClassContext).ClassName + ":F:" + Name;

			Logics.Preferences.FormPrefs prefs = Logics.Logic.UserPreferences.GetScenarioPrefs(ScenarioPrefsName) as Logics.Preferences.FormPrefs;

			if (prefs != null)
			{
				SetPreferences(prefs);
			}
		}
		/// <summary>
		/// Get and Apply the existing preferences for the scenario after the Shown event.
		/// </summary>
		private void ApplyPreferencesAfterShown()
		{
			if ((Context as IUClassContext) == null)
			{
				return;
			}

			// Get the scenario name for Preferences.
			ScenarioPrefsName = (Context as IUClassContext).ClassName + ":F:" + Name;
			Logics.Preferences.FormPrefs prefs = Logics.Logic.UserPreferences.GetScenarioPrefs(ScenarioPrefsName) as Logics.Preferences.FormPrefs;

			// If the Preferences contains properties, apply the splitters positions.
			if ((prefs != null) && (prefs.Properties != null))
			{
				Scenario.SetSplittersInfo(prefs.Properties);
			}
		}
		/// Apply the preferences to the Scenario
		/// </summary>
		/// <param name="prefs"></param>
		private void SetPreferences(Logics.Preferences.FormPrefs prefs)
		{
			if (prefs == null || Scenario == null)
			{
				return;
			}

			Scenario.SetPositionInfo(prefs.PosX, prefs.PosY, prefs.Width, prefs.Height, prefs.Properties);
		}
		/// <summary>
		/// Saves the scenario positions
		/// </summary>
		private void SavePosition()
		{
			if (Scenario == null)
			{
				return;
			}

			int x = 0;
			int y = 0;
			int width = 0;
			int height = 0;

			StringDictionary properties = new StringDictionary();
			Scenario.GetPositionInfo(ref x, ref y, ref width, ref height, properties);

			// Get information from the Scenario
			Logics.Logic.UserPreferences.SaveFormInfo(ScenarioPrefsName, x, y, width, height, properties);
		}
		#endregion Preferences

		#endregion Methods
	}
}

