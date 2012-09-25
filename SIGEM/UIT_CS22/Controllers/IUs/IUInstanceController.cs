// v3.8.4.5.b
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using SIGEM.Client.Presentation;
using SIGEM.Client.Adaptor;
using SIGEM.Client.Logics;
using SIGEM.Client.Oids;
using SIGEM.Client.Logics.Preferences;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the IUInstanceController.
	/// </summary>
	public class IUInstanceController : IUQueryController
	{
		#region Members
		/// <summary>
		/// Indicates the object valuate argument controller.
		/// </summary>
		private ArgumentOVController mOidSelector = null;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'IUInstanceController' class.
		/// </summary>
		/// <param name="name">Name of the Interaction Unit.</param>
		/// <param name="alias">Alias of the Interaction Unit.</param>
		/// <param name="idXML">IdXML of the Interaction Unit.</param>
		/// <param name="context">Context</param>
		/// <param name="parent">Parent controller</param>
		public IUInstanceController(string name, string alias, string idXML, IUInstanceContext context, IUController parent)
			: base()
		{

			Name = name;
			Alias = alias;
			IdXML = idXML;
			Context = context;
			Parent = parent;

		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets the ExchangeInformation.
		/// </summary>
		public override ExchangeInfo ExchangeInformation
		{
			get
			{
				return Context.ExchangeInformation;
			}
			set
			{
				Context.ExchangeInformation = value;
			}
		}
		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		public new IUInstanceContext Context
		{
			get
			{
				return mContext as IUInstanceContext;
			}
			set
			{
				mContext = value;
			}
		}
		/// <summary>
		/// Gets the selected instances.
		/// </summary>
		public override List<Oid> InstancesSelected
		{
			get
			{
				if (OidSelector != null)
				{
					return OidSelector.Value as List<Oid>;
				}
				return new List<Oid>();
			}
		}
		/// <summary>
		/// Gets or sets the selector Oid.
		/// </summary>
		public ArgumentOVController OidSelector
		{
			get
			{
				return mOidSelector;
			}
			set
			{
				if (mOidSelector != null)
				{
					mOidSelector.ValueChanged -= new EventHandler<ValueChangedEventArgs>(HandleOidSelectorValueChanged);
				}
				mOidSelector = value;
				if (mOidSelector != null)
				{
					mOidSelector.ValueChanged += new EventHandler<ValueChangedEventArgs>(HandleOidSelectorValueChanged);
				}
			}
		}
		/// <summary>
		/// Override the DisplaySetController to suscribe an execute command event
		/// </summary>
		public new DisplaySetController DisplaySet
		{
			get
			{
				return base.DisplaySet;
			}
			set
			{
				base.DisplaySet = value;
			}
		}
		#endregion Properties

		#region Event Handlers
		/// <summary>
		/// Executes Oid Selector change.
		/// </summary>
		private void HandleOidSelectorValueChanged(object sender, ValueChangedEventArgs e)
		{
			// Checking pending changes
			if (!CheckPendingChanges(true, true))
			{
				OidSelector.Value = DisplaySet.PreviousValue;
				return;
			}
			DisplaySet.PreviousValue = OidSelector.Value as List<Oid>;
			// The Oid in the Oid Selector has been modified. Raise the new value
			UpdateData(true);
			if (DisplaySet == null)
			{
				List<Oid> oidSelected = e.NewValue as List<Oid>;
				OnSelectedInstanceChanged(new SelectedInstanceChangedEventArgs(oidSelected));
			}
		}
		#endregion Event Handlers

		#region Process Events
		/// <summary>
		/// Actions related with the DisplaySet Execute Command event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void ProcessDisplaySetExecuteCommand(object sender, ExecuteCommandEventArgs e)
		{
			base.ProcessDisplaySetExecuteCommand(sender, e);

			switch (e.ExecuteCommandType)
			{
				case ExecuteCommandType.ExecuteFirstDestroyActionService:
					ExecuteCommandFirstDestroyActionService();
					break;
				case ExecuteCommandType.ExecuteFirstCreateActionService:
					ExecuteCommandFirstCreateActionService();
					break;
				case ExecuteCommandType.ExecuteFirstNotDestroyNotCreateActionService:
					ExecuteCommandFirstNotDestroyNotCreateActionService();
					break;
				case ExecuteCommandType.ExecuteRefresh:
					Refresh();
					break;
				default:
					break;
			}
		}
		/// <summary>
		/// Actions related with the Service response event from the associated service.
		/// Refresh the instance and reload the service context
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void ProcessAssociatedServiceControllerServiceResponse(object sender, ServiceResultEventArgs e)
		{
			if (!e.Success)
			{
				return;
			}

			// Refresh the shown instance
			Refresh();

			// Reload the service controller
			SetInstanceToAssociatedService(DisplaySet.Values);
		}
		/// <summary>
		/// Actions related with the Selected instance changed from the DisplaySet.
		/// Do the standar and at the end, check the associated service and initialize it
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void ProcessDisplaySetSelectedInstanceChanged(object sender, SelectedInstanceChangedEventArgs e)
		{
			base.ProcessDisplaySetSelectedInstanceChanged(sender, e);

			// Initializes the associated service
			if (AssociatedServiceController != null &&
				Logic.Agent.IsActiveFacet(AssociatedServiceController.Agents))
			{
				SetInstanceToAssociatedService(e.SelectedInstances);
			}
		}
		#endregion Process Events

		#region Methods
		/// <summary>
		///  Initializes the IU instance controller.
		/// </summary>
		public override void Initialize()
		{
			// Update context
			UpdateContext();

			OidSelector.Initialize();

			// Enabled the associated clear trigger
			if (AssociatedServiceClearTrigger != null)
			{
				AssociatedServiceClearTrigger.Visible = false;
			}

			base.Initialize();
		}
		/// <summary>
		/// Updates the data of the IU
		/// </summary>
		public override void UpdateData(bool refresh)
		{
			// Execute default update
			UpdateContext();
			DataTable lData = null;
			// Search the Oid of the instance to be presented
			// Depending on the ExchangeInfo Type
			if (Context.ExchangeInformation.ExchangeType == ExchangeType.Navigation)
			{
				List<Oid> lOids = new List<Oid>(1);
				if (Context.ExchangeInformation.SelectedOids.Count != 0)
				{
					try
					{
						// Logic API call.
						lData = Logic.ExecuteQueryRelated(Context);
					}
					catch (Exception logicException)
					{
						//Exception lcustomException = new Exception(CultureManager.TranslateFixedString(LanguageConstantKeys.L_CONTROLLER_EXCEPTION, LanguageConstantValues.L_CONTROLLER_EXCEPTION), logicException);
						ScenarioManager.LaunchErrorScenario(logicException);
					}
					Oid lOid = Adaptor.ServerConnection.GetLastOid(lData);
					if (lOid != null)
					{
						lOids.Add(lOid);
					}
				}
				// Keep the previous value
				DisplaySet.PreviousValue = OidSelector.Value as List<Oid>;
				// If one instance is selected, set the value and disable the Oid Selector
				OidSelector.Value = lOids;
				// Disable the Oid Selector
				OidSelector.Enabled = false;
			}

			List<Oid> lInstancesSelected = InstancesSelected;
			ShowData(lInstancesSelected);
		}
		/// <summary>
		/// Showes the data of the instance.
		/// </summary>
		/// <param name="lOids">The instance Oid list</param>
		private void ShowData(List<Oid> lOids)
		{
			// Query by instance
			DataTable lData = null;
			if (lOids != null && lOids.Count > 0)
			{
				try
				{
					// Logic API call.
					lData = Logic.ExecuteQueryInstance(Logic.Agent, OidSelector.Domain, lOids[0], this.BuildDisplaySetAttributes());
				}
				catch (Exception logicException)
				{
					ScenarioManager.LaunchErrorScenario(logicException);
				}
			}

			// Clear Oid Selector value.
			if (lData != null && lData.Rows.Count == 0)
			{
				mOidSelector.Value = null;
			}

			// Show Population.
			SetPopulation(lData, true, lOids);
		}
		/// <summary>
		/// Configures the controller from the data context.
		/// </summary>
		/// <param name="context">IU Instance Context</param>
		public void ConfigureByContext(IUInstanceContext context)
		{
			IUInstanceContext lContext = context as IUInstanceContext;
			// Default
			base.ConfigureByContext(context);
		}
		/// <summary>
		/// Load the initial population based on hte informaation received
		/// </summary>
		protected override void LoadInitialData()
		{
			if (Context.ExchangeInformation.SelectedOids.Count != 0 &&
				Context.ExchangeInformation.ExchangeType == ExchangeType.Action)
			{
				List<Oid> lOids = new List<Oid>(1);
				if (String.Compare(Context.ExchangeInformation.SelectedOids[0].ClassName, Context.ClassName, true) != 0)
				{
					// If the target class belongs to the inheritance hierarchy net of the source class.
					if (Logics.Logic.IsClassInheritanceHierarchy(Context.ClassName, Context.ExchangeInformation.SelectedOids[0].ClassName))
					{
						DataTable instanceDataTable = Logic.ExecuteQueryInstance(Logic.Agent, OidSelector.Domain, Context.ExchangeInformation.SelectedOids[0], this.BuildDisplaySetAttributes());
						Oid selectedOid = Adaptor.ServerConnection.GetLastOid(instanceDataTable);

						// Keep the previous value
						DisplaySet.PreviousValue = OidSelector.Value as List<Oid>;
						lOids.Add(selectedOid);
						OidSelector.Value = lOids;
					}
					else
					{
						// Disable navigations and Exit.
						this.EnableNavigations(false);
						return;
					}
				}
				else
				{
					// Keep the previous value
					DisplaySet.PreviousValue = OidSelector.Value as List<Oid>;
					lOids.Add(Context.ExchangeInformation.SelectedOids[0]);
					OidSelector.Value = lOids;
				}
			}
			
			// Update data.
			UpdateData(true);
		}
		/// <summary>
		/// Refreshes the data of the instance.
		/// </summary>
		public override void Refresh()
		{
			try
			{
				// Update data
				UpdateData(true);
			}
			catch (Exception e)
			{
				ScenarioManager.LaunchErrorScenario(e);
			}
		}
		/// <summary>
		/// Apply multilanguage to the scenario.
		/// </summary>
		public override void ApplyMultilanguage()
		{
			base.ApplyMultilanguage();

			// Apply multilanguege to the scenario.
			if (this.OidSelector != null)
			{
				this.OidSelector.Label.Value = CultureManager.TranslateString(this.IdXML, this.Alias, this.OidSelector.Label.Value.ToString());
			}

			// Cancel button.
			if (this.CancelTrigger != null)
			{
				this.CancelTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_CLOSE, LanguageConstantValues.L_CLOSE, this.CancelTrigger.Value.ToString());
			}
		}
		/// <summary>
		/// Assign the preferences to this controller
		/// </summary>
		/// <param name="preferences"></param>
		public void SetPreferences(InstancePrefs preferences)
		{
			// Gets the custom DIsplaySets
			if (preferences == null)
				return;

			// Add teh custom DisplaySets to the DisplaySet List
			foreach (DisplaySetInformation displaySet in preferences.DisplaySets)
			{
				DisplaySet.DisplaySetList.Add(displaySet);
			}

			// Assign the selected DisplaySet
			if (!preferences.SelectedDisplaySetName.Equals(""))
			{
				DisplaySetInformation displaySet = DisplaySet.GetDisplaySetByName(preferences.SelectedDisplaySetName);
				if (displaySet != null)
				{
					DisplaySet.CurrentDisplaySet = displaySet;
				}
			}
		}
		#endregion Methods
	}
}
