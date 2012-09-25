// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SIGEM.Client.Presentation;
using SIGEM.Client.Logics;
using SIGEM.Client.Oids;
using SIGEM.Client;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the object-valued Argument controller.
	/// </summary>
	public class ArgumentOVController : ArgumentOVControllerAbstract, ISelectionBackward

	{
		#region Members
		/// <summary>
		/// List of editors presentations of the object-valued Argument.
		/// </summary>
		private EditorList mEditors = null;
		/// <summary>
		/// Trigger presentation (magnifying glass) of the object-valued Argument.
		/// </summary>
		private ITriggerPresentation mTrigger;
		/// <summary>
		/// Supplementary information of the object-valued Argument.
		/// </summary>
		private DisplaySetController mSupplementaryInfo;
		/// <summary>
		/// Target selection scenario of the object-valued Argument.
		/// </summary>
		private string mSelectionScenario = string.Empty;
		/// <summary>
		/// Indicates whether the object-valued Argument has navigational filtering.
		/// </summary>
		private bool mNavigationalFiltering = false;

		private string mAlternateKeyName = string.Empty;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ArgumentOVController' class.
		/// </summary>
		/// <param name="name">Name of the object-valued Argument.</param>
		/// <param name="alias">Alias of the object-valued Argument.</param>
		/// <param name="idxml">IdXML of the object-valued Argument.</param>
		/// <param name="domain">Domain or class name of the object-valued Argument.</param>
		/// <param name="nullAllowed">Indicates whether the object-valued Argument allows null values.</param>
		/// <param name="multipleSelectionAllowed">Indicates whether the object-valued Argument allows multiple values.</param>
		/// <param name="isNavigationalFilter">Indicates whether the object-valued Argument has navigational filtering.</param>
		/// <param name="selectionScenario">Target selection scenario.</param>
		/// <param name="supplementaryInfo">Supplementary information.</param>
		/// <param name="parent">Parent controller.</param>
		[Obsolete("Since version 3.5.4.3")]
		public ArgumentOVController(
			string name,
			string alias,
			string idxml,
			string domain,
			bool nullAllowed,
			bool multiSelectionAllowed,
			bool isNavigationalFilter,
			string selectionScenario,
			DisplaySetController supplementaryInfo,
			IUController parent)
			: this(name, alias, idxml, domain, nullAllowed, multiSelectionAllowed, isNavigationalFilter, selectionScenario, supplementaryInfo, string.Empty, parent)
		{
		}

		/// <summary>
		/// Initializes a new instance of the 'ArgumentOVController' class.
		/// </summary>
		/// <param name="name">Name of the object-valued Argument.</param>
		/// <param name="alias">Alias of the object-valued Argument.</param>
		/// <param name="idxml">IdXML of the object-valued Argument.</param>
		/// <param name="domain">Domain or class name of the object-valued Argument.</param>
		/// <param name="nullAllowed">Indicates whether the object-valued Argument allows null values.</param>
		/// <param name="multipleSelectionAllowed">Indicates whether the object-valued Argument allows multiple values.</param>
		/// <param name="isNavigationalFilter">Indicates whether the object-valued Argument has navigational filtering.</param>
		/// <param name="selectionScenario">Target selection scenario.</param>
		/// <param name="supplementaryInfo">Supplementary information.</param>
		/// <param name="alternateKeyName">AlternateKey's name of the object-valued Argument.</param>
		/// <param name="parent">Parent controller.</param>
		public ArgumentOVController(
			string name,
			string alias,
			string idxml,
			string domain,
			bool nullAllowed,
			bool multiSelectionAllowed,
			bool isNavigationalFilter,
			string selectionScenario,
			DisplaySetController supplementaryInfo,
			string alternateKeyName,
			IUController parent)
			: base(name, alias, idxml, domain, nullAllowed, multiSelectionAllowed, parent)
		{
			mSelectionScenario = selectionScenario;
			mNavigationalFiltering = isNavigationalFilter;
			if (supplementaryInfo == null)
			{
				// Message that shows the number of selected elements (only in multiselelection).
				DisplaySetController lDisplaySetController = new DisplaySetController();
				SupplementaryInfo = lDisplaySetController;
			}
			else
			{
				// DisplaySet supplementary information.
				SupplementaryInfo = supplementaryInfo;
			}

			this.AlternateKeyName = alternateKeyName;

			mEditors = new EditorList(this);
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets the list of editor presentations of the object-valued Argument.
		/// </summary>
		public IList<IEditorPresentation> Editors
		{
			get
			{
				return mEditors;
			}
		}
		/// <summary>
		/// Gets or sets the trigger presentation (magnifying glass) of the object-valued Argument.
		/// </summary>
		public ITriggerPresentation Trigger
		{
			get
			{
				return mTrigger;
			}
			set
			{
				mTrigger = value;
				if (mTrigger != null)
				{
					mTrigger.Triggered += new EventHandler<TriggerEventArgs>(HandleTriggerExecute);
				}
			}
		}
		/// <summary>
		/// Gets or sets the supplementary information of the object-valued Argument.
		/// </summary>
		public DisplaySetController SupplementaryInfo
		{
			get
			{
				return mSupplementaryInfo;
			}
			set
			{
				mSupplementaryInfo = value;
			}
		}
		/// <summary>
		/// Gets or sets the supplementary information text showed for the object-valued Argument.
		/// </summary>
		public override string GetSupplementaryInfoText()
		{
			Presentation.Forms.LabelDisplaySetPresentation lSupInfoLabelPresentation = (mSupplementaryInfo.Viewer as Presentation.Forms.LabelDisplaySetPresentation);
			if (lSupInfoLabelPresentation != null)
			{
				return lSupInfoLabelPresentation.DisplayedText;
			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Gets or sets the value of the object-valued Argument.
		/// </summary>
		public override object Value
		{
			get
			{
				// If the pervious value contains more than one Oid, it is returned.
				if ((LastValueListOids != null) && (LastValueListOids.Count > 1))
				{
					return LastValueListOids;
				}
				return BuildOidsFromEditorsValue(true);
			}
			set
			{
				// If the value is the same that the previous one, do nothing.
				if (UtilFunctions.OidListEquals(LastValueListOids, value as List<Oid>))
				{
					return;
				}

				// If value is the same which was typed by the user, do nothing.

				// Set flags.
				IgnoreEditorsValueChangeEvent = true;
				IgnoreEditorsEnabledChangeEvent = true;

				// Assign the value
				SetValue(value);

				// Remove flags.
				IgnoreEditorsValueChangeEvent = false;
				IgnoreEditorsEnabledChangeEvent = false;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the object-valued Argument is enabled or not.
		/// </summary>
		public override bool Enabled
		{
			get
			{
				// Return false when one of the editors is disabled and
				// the instance selector is disabled, as well; otherwise, return true.
				// This behaviour is important when multiselection is used.
				foreach (IEditorPresentation lEditor in mEditors)
				{
					if ((lEditor != null) && (!lEditor.Enabled))
					{
						if ((mTrigger != null) && (!mTrigger.Enabled))
						{
							return false;
						}
					}
				}
				return true;
			}
			set
			{
				// Set flag.
				IgnoreEditorsEnabledChangeEvent = true;
				foreach (IEditorPresentation lEditor in mEditors)
				{
					lEditor.Enabled = value;
				}

				if (mTrigger != null)
				{
					mTrigger.Enabled = value;
				}
				// Remove flag.
				IgnoreEditorsEnabledChangeEvent = false;
			}
		}
		/// <summary>
		/// Sets only the Editors enabled property.
		/// </summary>
		public bool EnabledEditors
		{
			set
			{
				// Set flag.
				IgnoreEditorsEnabledChangeEvent = true;
				// This property is only used when multiselection as more than
				// one instance selected, in order to disable only the editors and
				// keep the instance selector enabled. This way, the user can select
				// other instances using the instance selector.
				foreach (IEditorPresentation lEditor in mEditors)
				{
					lEditor.Enabled = value;
				}
				// Remove flag.
				IgnoreEditorsEnabledChangeEvent = false;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the object-valued Argument is mandatory or not.
		/// </summary>
		public override bool Mandatory
		{
			get
			{
				if (Editors != null)
				{
					foreach (IEditorPresentation lEditor in Editors)
					{
						if ((lEditor != null) && (!lEditor.NullAllowed))
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}
			set
			{
				if (Editors != null)
				{
					// Set nullallowed for each editor depending on Mandatory value.
					foreach (IEditorPresentation lEditor in Editors)
					{
						if (lEditor != null)
						{
							lEditor.NullAllowed = !value;
						}
					}
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the object-valued Argument is visible or not.
		/// </summary>
		public override bool Visible
		{
			get
			{
				// Return false when one of the editors is not visible and
				// the instance selector is not visible, as well; otherwise, return true.
				if ((Trigger != null) && (!Trigger.Visible))
				{
					return false;
				}
				if (Editors != null)
				{
					foreach (IEditorPresentation lEditor in Editors)
					{
						if ((lEditor != null) && (!lEditor.Visible))
						{
							return !base.Visible;
						}
					}
				}
				return base.Visible;
			}
			set
			{
				base.Visible = value; // Set the label visible in base.
				if (Editors != null)
				{
					// Set each editor visible.
					foreach (IEditorPresentation lEditor in Editors)
					{
						lEditor.Visible = value;
					}
				}
				// Set the instance selector visible.
				if (Trigger != null)
				{
					Trigger.Visible = value;
				}
				// Set the supplementary information label visible.
				if (SupplementaryInfo != null)
				{
					SupplementaryInfo.Viewer.Visible = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the data-valued Argument is focused or not.
		/// </summary>
		public override bool Focused
		{
			get
			{
				if (Editors != null)
				{
					foreach (IEditorPresentation lEditor in Editors)
					{
						// If any editor is focused the Argument is focused.
						if ((lEditor != null) && (lEditor.Focused))
						{
							return true;
						}
					}
				}
				// If not any editor is focused, then ask the trigger.
				if (Trigger != null)
				{
					return Trigger.Focused;
				}
				return false;
			}
			set
			{
				if (Editors != null)
				{
					foreach (IEditorPresentation lEditor in Editors)
					{
						if (lEditor != null)
						{
							if (value)
							{
								// Set the focus to this editor and exit.
								lEditor.Focused = true;
								return;
							}
							else
							{
								if (lEditor.Focused)
								{
									lEditor.Focused = false;
									return;
								}
							}
						}
					}
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value inducating whether the object-valued Argument is selected.
		/// </summary>
		public override bool IsSelected
		{
			get
			{
				foreach (ILabelPresentation lValue in Editors)
				{
					IEditorPresentation lEditor = lValue as IEditorPresentation;
					if ((lEditor != null) && (lEditor.Focused))
					{
						return true;
					}
				}
				if (Trigger != null)
				{
					return Trigger.Focused;
				}
				return false;
			}
			set
			{
				foreach (ILabelPresentation lValue in Editors)
				{
					IEditorPresentation lEditor = lValue as IEditorPresentation;
					if (lEditor != null)
					{
						if (value)
						{
							lEditor.Focused = true;
							return;
						}
						else
						{
							if (lEditor.Focused)
							{
								lEditor.Focused = false;
								return;
							}
						}
					}
				}
			}
		}
		/// <summary>
		/// Gets a boolean value indicating whether the object-valued Argument has navigational filtering.
		/// </summary>
		public virtual bool IsNavigationalFilter
		{
			get
			{
				return mNavigationalFiltering;
			}
		}

		/// <summary>
		/// Gets or sets the name of the Alternate Key used by the object valued Argument.
		/// </summary>
		public virtual string AlternateKeyName
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
		/// Gets if his Parent is Service.
		/// </summary>
		public virtual bool IsServiceField
		{
			get
			{
				return (Parent as IUServiceController) != null ? true : false;
			}
		}
		/// <summary>
		/// Gets if his Parent is filter.
		/// </summary>
		public virtual bool IsFilterField
		{
			get
			{
				return (Parent as IUFilterController) != null ? true : false;
			}
		}
		#endregion Properties

		#region Event Handlers
		/// <summary>
		/// Handles the  Editor Execute command event
		/// </summary>
		/// <param name="e"></param>
		private void HandleEditorExecuteCommand(object sender, ExecuteCommandEventArgs e)
		{
			// If is for refresh get the OID
			if (e.ExecuteCommandType == ExecuteCommandType.ExecuteRefresh)
			{
				List<Oid> lSelectedOids = Value as List<Oid>;
				base.OnExecuteCommand(new ExecuteCommandRefreshEventArgs(lSelectedOids));
			}
			else
			{
				base.OnExecuteCommand(e);
			}
		}
		/// <summary>
		/// Occurs when trigger presentation (magnifying glass) of the object-valued Argument is triggered.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">TriggerEventArgs.</param>
		protected void HandleTriggerExecute(object sender, TriggerEventArgs e)
		{
			// Actualize the context.
			IUContext lContext = null;
			if (Parent != null)
			{
				Parent.UpdateContext();
				lContext = Parent.Context;
			}

			// Initialize class and service name.
			string lClassName = string.Empty;
			string lContainerName = string.Empty;

			if (IsServiceField)
			{
				IUServiceController lServiceController = Parent as IUServiceController;

				if (lServiceController != null)
				{
					lClassName = lServiceController.ClassName;
					lContainerName = lServiceController.Name;
				}
			}
			else if (IsFilterField)
			{
				IUFilterController lFilterController = Parent as IUFilterController;

				if (lFilterController != null)
				{
					IUPopulationContext lPopContext = lFilterController.Parent.Context as IUPopulationContext;
					lClassName = lPopContext.ClassName;
					lContainerName = lFilterController.Context.FilterName;
					lContext = lPopContext;
				}
			}
			// Create exchange information.
			ExchangeInfoSelectionForward lInfo =
				new ExchangeInfoSelectionForward(
				Domain,
				mSelectionScenario,
				lClassName,
				lContainerName,
				Name,
				MultiSelectionAllowed,
				IsNavigationalFilter,
				lContext);

			// Launch scenario.
			ScenarioManager.LaunchSelectionScenario(lInfo, this);
		}
		/// <summary>
		/// Handles the InstancesHasBeenSelected event from the Selector
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleSelectorInstancesHasBeenSelected(object sender, InstancesSelectedEventArgs e)
		{
			// Set flag.
			IgnoreEditorsValueChangeEvent = true;

			// Set values.
			List<Oid> lLastValue = LastValueListOids;
			SetValue(e.OidList);

			// Remove flag.
			IgnoreEditorsValueChangeEvent = false;

			// Set focus to the button.
			if (this.Trigger != null)
			{
				this.Trigger.Focused = true;
			}

			if (!UtilFunctions.OidListEquals(lLastValue, e.OidList))
			{
				// If more than one instance is selected, the dependency Rules should not be thrown.
				if ((e.OidList != null) && (e.OidList.Count > 1))
				{
					return;
				}

				OnValueChanged(new ValueChangedEventArgs(this, lLastValue, e.OidList, DependencyRulesAgentLogic.User));
			}
		}
		/// <summary>
		/// Event handler for ValueChanged event in the object-valued Argument.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleOIDValueChanged(object sender, ValueChangedEventArgs e)
		{
			// If flag is set, do nothing.
			if (IgnoreEditorsValueChangeEvent)
			{
				return;
			}

			// If the parent controller is an OutboundArgument controller, do nothing.
			IUServiceController lParentController = this.Parent as IUServiceController;
			if ((lParentController != null) && (lParentController.IsOutboundArgumentController))
			{
				return;
			}
			// Check if the value have changed.
			List<Oid> lLastValue = LastValueListOids;
			List<Oid> lValue = BuildOidsFromEditorsValue(false);
			bool lEquals = UtilFunctions.OidListEquals(lLastValue, lValue);

			// If there is no change, do nothing.
			if (lEquals)
			{
				return;
			}

			// Verify instance and show supplementary information
			if (lValue != null)
			{
				CheckInstance(lValue);
			}
			else
			{
				ShowSupplementaryInfo(null);
			}

			// Assign the current value as last value
			LastValueListOids = lValue;

			// Finally, if the last and current values are different, raise the event.
			OnValueChanged(new ValueChangedEventArgs(this,lLastValue,  lValue , DependencyRulesAgentLogic.User));
		}
		/// <summary>
		/// Event handler for EnableChanged event in the object-valued Argument.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EnableChangedEventArgs.</param>
		private void HandleOIDEnabledChanged(object sender, EnabledChangedEventArgs e)
		{
			// If flag is set, do nothing.
			if (IgnoreEditorsEnabledChangeEvent)
			{
				return;
			}

			bool lAllEquals=true;

			// For each editor presentation.
			for (int i=1; i < mEditors.Count; i++)
			{
				if (mEditors[i-1].Enabled != mEditors[i].Enabled)
				{
					lAllEquals = false;
					break;
				}
			}

			// Raise the event.
			if (lAllEquals)
			{
				OnEnableChanged(new EnabledChangedEventArgs(this, DependencyRulesAgentLogic.Internal, !mEditors[0].Enabled));
			}
		}
		#endregion Event Handlers

		#region Methods
		/// <summary>
		/// This method builds a list of Oids from the editor presentation values.
		/// </summary>
		/// <param name="executeQuery">Indicates wheter a quey must be executed to retrieve the primary Oid.</param>
		/// <returns>List of Oids.</returns>
		private List<Oid> BuildOidsFromEditorsValue(bool executeQuery)
		{
			// Get the editor's values.
			List<object> lFields = new List<object>();
			foreach (IEditorPresentation lEditor in mEditors)
			{
				// Check the editor null values.
				if ((lEditor.Value == null) || (lEditor.Value.ToString().Trim().Length == 0))
				{
					return null;
				}
				lFields.Add(lEditor.Value);
			}
			// Build the AlternateKey object from the 'editor' values.
			List<Oid> lOids = new List<Oid>();
			Oid lOid = null;
			try
			{
				// Logic API call.
				lOid = Logic.CreateOidFromOidFields(Domain, lFields, AlternateKeyName, executeQuery);
			}
			catch (Exception logicException)
			{
				//Exception lcustomException = new Exception(CultureManager.TranslateFixedString(LanguageConstantKeys.L_CONTROLLER_EXCEPTION, LanguageConstantValues.L_CONTROLLER_EXCEPTION), logicException);
				ScenarioManager.LaunchErrorScenario(logicException);
			}
			lOids.Add(lOid);

			// If current Oid is equals to the Last one, return it
			if (UtilFunctions.OidListEquals(lOids, LastValueListOids))
			{
				return LastValueListOids;
			}

			// Get SCD attribute values
			GetValuesForSCD(lOids);

			return lOids;
		}
		/// <summary>
		/// Sets the values and shows the supplementary information.
		/// </summary>
		/// <param name="oids"></param>
		private void SetValueOidFields(List<Oid> oids)
		{
			Oid oid = oids[0];

			if (AlternateKeyName != string.Empty)
			{
				// If it has alternate key, use it.
				oid = Logic.GetAlternateKeyFromOid(oid, AlternateKeyName);
			}

			int i = 0;
			// Set the  'Oid' values (AlternateKey or Oid object) to the OV editors.
			foreach (IEditorPresentation item in mEditors)
			{
				item.Value = oid.Fields[i].Value;
				i++;
			}
			// If there is not supplementary information, do nothing more.
			if (mSupplementaryInfo == null)
			{
				return;
			}
			// Show the supplementary information of the instance.
			ShowSupplementaryInfo(oids);
		}
		/// <summary>
		/// Shows the supplementary information of the instance.
		/// </summary>
		/// <param name="oids">List of Oids.</param>
		private void ShowSupplementaryInfo(List<Oid> oids)
		{
			if (mSupplementaryInfo == null)
			{
				return;
			}

			// Clear supplementary information.
			if ((oids == null) || (oids.Count == 0))
			{
				mSupplementaryInfo.SetPopulation(null, true, null);
				return;
			}

			// If there is only one instance.
			if (oids.Count == 1)
			{
				try
				{
					DataTable dataTable = null;
					string attributes = mSupplementaryInfo.DisplaySetAttributes;
					attributes = UtilFunctions.ReturnMissingAttributes(oids[0].ExtraInfo, attributes);
					if (attributes != "")
					{
						try
						{
							// Logic API call.
							dataTable = Logic.ExecuteQueryInstance(Logic.Agent, Domain, oids[0], attributes);
						}
						catch (Exception logicException)
						{
							ScenarioManager.LaunchErrorScenario(logicException);
						}
						oids[0].ExtraInfo.Merge(dataTable);
					}
					// Set the supplementary information.
					mSupplementaryInfo.SetPopulation(oids[0].ExtraInfo, true, null);
				}
				catch
				{
					// If there is something wrong, clear the supplementary information.
					mSupplementaryInfo.SetPopulation(null, true, null);
				}
			}
			else
			{
				// If there is more than one instance, show warning message.
				object[] lArgs = new object[1];
				lArgs[0] = oids.Count.ToString();
				string lMessage = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_ELEMENTSELECTED, LanguageConstantValues.L_ELEMENTSELECTED, lArgs);
				mSupplementaryInfo.SetMessage(lMessage);
			}
		}
		/// <summary>
		/// Manages the object-valued Argument when several instances has been selected.
		/// </summary>
		/// <param name="lOids">List of Oids.</param>
		private void SeveralInstancesSelected(List<Oid> lOids)
		{
			// Clear and disable editor presentations.
			foreach (IEditorPresentation lEditor in mEditors)
			{
				lEditor.Value = null;
				lEditor.Enabled = false;
			}

			// Show supplementary information.
			ShowSupplementaryInfo(lOids);
		}
		/// <summary>
		/// Initializes the Argument.
		/// </summary>
		public override void Initialize()
		{
			IUServiceController lServiceController = Parent as IUServiceController;

			// It is an Outbound Argument.
			if ((lServiceController != null) && (lServiceController.IsOutboundArgumentController))
			{
			}
			else
			{
				Mandatory = !NullAllowed;
			}

			base.Initialize();
		}
		/// <summary>
		/// This method is used to suscribe to the instances selector.
		/// </summary>
		/// <param name="instancesSelector">Instances selector (magnifying glass).</param>
		void ISelectionBackward.SuscribeSelectionBackward(IInstancesSelector instancesSelector)
		{
			if (instancesSelector != null)
			{
				instancesSelector.InstancesHasBeenSelected += new EventHandler<InstancesSelectedEventArgs>(HandleSelectorInstancesHasBeenSelected);
			}
		}

		public virtual void Refresh()
		{
			// Check if the value have changed.
			List<Oid> lLastValue = LastValueListOids;
			List<Oid> lValue = BuildOidsFromEditorsValue(false);

			// Verify instance and show supplementary information
			if (lValue != null)
			{
				CheckInstance(lValue);
			}
			else
			{
				ShowSupplementaryInfo(null);
			}

			// Finally, if the last and current values are different, raise the event.
			OnValueChanged(new ValueChangedEventArgs(this,lLastValue,lValue, DependencyRulesAgentLogic.User));
		}
		/// <summary>
		/// Checks if the instances exist.
		/// </summary>
		/// <param name="values">List of instances.</param>
		private void CheckInstance(List<Oid> values)
		{
			Oid lOidToCheck = values[0];

			// Check if the Instance with received Oid exists.
			string displaySet = string.Empty;

			if (mSupplementaryInfo != null)
			{
				displaySet = mSupplementaryInfo.DisplaySetAttributes;
			}

			try
			{
				// Execute query.
				DataTable dataTable = null;
				try
				{
					// Logic API call.
					lOidToCheck = lOidToCheck.GetAlternateKey(AlternateKeyName) == null ? lOidToCheck : (Oid)lOidToCheck.GetAlternateKey(AlternateKeyName);
					dataTable = Logic.ExecuteQueryInstance(Logic.Agent, Domain, AlternateKeyName, lOidToCheck, displaySet);
				}
				catch (Exception logicException)
				{
					ScenarioManager.LaunchErrorScenario(logicException);
				}

				// If there are no rows, launch error scenario.
				if (dataTable == null || (dataTable != null && dataTable.Rows.Count == 0))
				{
					ScenarioManager.LaunchErrorScenario(new Exception(CultureManager.TranslateString(LanguageConstantKeys.L_ERROR_NO_EXIST_INSTANCE, LanguageConstantValues.L_ERROR_NO_EXIST_INSTANCE)));
				}
				else
				{
					// If the instance exists, set its Oid fields from the datatable retrieved.
					if ((AlternateKeyName != string.Empty) && (dataTable != null) && (dataTable.Rows.Count == 1))
					{
						lOidToCheck = Adaptor.ServerConnection.GetOid(dataTable, dataTable.Rows[0], AlternateKeyName);
						values.Clear();
						values.Add(lOidToCheck);
					}
				}

				// Show supplementary information.
				if (mSupplementaryInfo != null)
				{
					mSupplementaryInfo.SetPopulation(dataTable, true, null);
				}
			}
			catch
			{
				// If something wrong happens, clear the supplementary information.
				if (mSupplementaryInfo != null)
				{
					mSupplementaryInfo.SetPopulation(null, true, null);
				}
			}
		}
		/// <summary>
		/// Sets the value of the object-valued Argument.
		/// </summary>
		/// <param name="value">Value of the object-valued Argument.</param>
		private void SetValue(object value)
		{
			// Assign the value and search the supplementary information.
			List<Oid> lOids = value as List<Oid>;

			// If the argument does not allow multiple selection, take only the first one.
			if ((lOids != null && lOids.Count > 1) && (!MultiSelectionAllowed))
			{
				lOids.RemoveRange(1, lOids.Count - 1);
			}

			// Set flag.
			IgnoreEditorsEnabledChangeEvent = true;

			try
			{
				// Enable the editor presentations if the last value had more than one instance.
				if (LastValueListOids != null && LastValueListOids.Count > 1)
				{
					foreach (IEditorPresentation lEditor in mEditors)
					{
						lEditor.Enabled = true;
					}
				}

				// If the value is null, clear the editor presentations.
				if (value == null || lOids.Count == 0)
				{
					foreach (IEditorPresentation lEditor in mEditors)
					{
						lEditor.Value = null;
					}
					mSupplementaryInfo.SetPopulation(null, true, null);
				}
				else
				{
					// If only one oid, assign values to the editor presentations.
					if (lOids.Count == 1)
					{
						SetValueOidFields(lOids);
					}
					else
					{
						SeveralInstancesSelected(lOids);
					}
				}

				// Catch the current value without raising the change event.
				LastValueListOids = lOids;
			}
			catch
			{
			}

			// Remove flag.
			IgnoreEditorsEnabledChangeEvent = false;
		}
		#endregion Methods

		#region EditorList class
		/// <summary>
		/// Editor presentations for attributes.
		/// </summary>
		private class EditorList : KeyedCollection<string, IEditorPresentation>
		{
			private ArgumentOVController mArgumentController = null;
			private Oid mOid = null;

			#region Remove Editor
			/// <summary>
			/// Editor presentation is removed and unsuscribed from events.
			/// </summary>
			/// <param name="editor">Editor reference.</param>
			private void DeleteEditor(IEditorPresentation editor)
			{
				if (editor != null)
				{
					editor.ExecuteCommand -= new EventHandler<ExecuteCommandEventArgs>(mArgumentController.HandleEditorExecuteCommand);
					editor.ValueChanged -= new EventHandler<ValueChangedEventArgs>(mArgumentController.HandleOIDValueChanged);
					editor.EnableChanged -= new EventHandler<EnabledChangedEventArgs>(mArgumentController.HandleOIDEnabledChanged);
				}
			}
			#endregion Remove Editor

			#region Insert Editor
			/// <summary>
			/// Editor presentation is added and sucribed to events.
			/// </summary>
			/// <param name="editor">Editor reference.</param>
			private void InsertEditor(IEditorPresentation editor, int index)
			{
				if (editor != null)
				{
					editor.ExecuteCommand += new EventHandler<ExecuteCommandEventArgs>(mArgumentController.HandleEditorExecuteCommand);
					editor.ValueChanged += new EventHandler<ValueChangedEventArgs>(mArgumentController.HandleOIDValueChanged);
					editor.EnableChanged += new EventHandler<EnabledChangedEventArgs>(mArgumentController.HandleOIDEnabledChanged);
					editor.MaxLength = mOid.Fields[index].MaxLength;
					editor.DataType = mOid.Fields[index].Type;
				}
			}
			#endregion Insert Editor

			#region Constructors
			/// <summary>
			/// Initializes a new instance of the 'EditorList' class.
			/// </summary>
			/// <param name="argumentController">Argument controller.</param>
			public EditorList(ArgumentOVController argumentController)
			{
				if (argumentController != null)
				{
					mArgumentController = argumentController;
					mOid = Oid.Create(mArgumentController.Domain);

					if (argumentController.AlternateKeyName != string.Empty)
					{
						mOid = (mOid.GetAlternateKey(argumentController.AlternateKeyName) as Oid);
					}
					for (int it = 0; it < mOid.Fields.Count; it++)
					{
						Add(null);
					}
				}
			}
			#endregion Constructors

			#region KeyedCollection interface

			protected override string GetKeyForItem(IEditorPresentation item)
			{
				return Count.ToString();
			}


			protected override void ClearItems()
			{
				int lCount = 0;
				foreach (IEditorPresentation litem in this)
				{
					DeleteEditor(litem);
					lCount++;
				}
				base.ClearItems();
			}

			protected override void InsertItem(int index, IEditorPresentation item)
			{
				InsertEditor(item, index);
				base.InsertItem(index, item);
			}

			protected override void RemoveItem(int index)
			{
				DeleteEditor(this[index]);
				base.RemoveItem(index);
			}

			protected override void SetItem(int index, IEditorPresentation item)
			{
				InsertEditor(item, index);
				base.SetItem(index, item);
			}


			#endregion KeyedCollection interface
		}
		#endregion EditorList class
	}
}
