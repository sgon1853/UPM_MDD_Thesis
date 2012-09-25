// v3.8.4.5.b
using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;

using SIGEM.Client.Presentation;
using SIGEM.Client.Logics;
using SIGEM.Client.Oids;
using SIGEM.Client.Logics.Preferences;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the DisplaySet controller.
	/// </summary>
	public class DisplaySetController : Controller
	{
		#region Members
		/// <summary>
		/// DisplaySet attributes (string splitted using commas).
		/// </summary>
		private string mDisplaySetAttributes = string.Empty;
		/// <summary>
		/// DisplaySet presentation.
		/// </summary>
		private IDisplaySetPresentation mViewer = null;
		/// <summary>
		/// List of last Oids selected.
		/// </summary>
		private List<Oid> mLastValues = null;
		/// <summary>
		/// Number of Instances presented in the grid
		/// </summary>
		private ILabelPresentation mNumberOfInstances;
		///// <summary>
		///// List of Model defined DisplaySets for this scenario.
		///// </summary>
		private List<DisplaySetInformation> mDisplaySetList = new List<DisplaySetInformation>();
		/// <summary>
		/// Current DisplaySet Information
		/// </summary>
		private DisplaySetInformation mCurrentDisplaySet;
		/// <summary>
		/// Trigger. Open the Preferences dialog
		/// </summary>
		private ITriggerPresentation mPreferencesTrigger;
		/// <summary>
		/// Trigger. Save the columns width
		/// </summary>
		private ITriggerPresentation mSaveColumnsWidthTrigger;
		/// <summary>
		/// Trigger. Service execution
		/// </summary>
		private ITriggerPresentation mExecuteServiceTrigger;
		/// <summary>
		/// Indicates that exist changes pending in the Display Set viewer
		/// </summary>
		private bool mPendingChanges = false;
		/// <summary>
		/// Flag to avoid loops with the SelectecInstanceChanged even
		/// </summary>
		private bool mRaiseSelectecInstanceChanged = true;
		/// <summary>
		/// Previous value, in order to compare with the new ones
		/// </summary>
		private List<Oid> mPreviousValue = null;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'DisplaySetController' class.
		/// </summary>
		/// <param name="name">Name of the DisplaySet.</param>
		public DisplaySetController()
			: base(null)
		{
		}
		/// <summary>
		/// Initializes a new instance of the 'DisplaySetController' class.
		/// </summary>
		/// <param name="parent">Parent controller.</param>
		public DisplaySetController(Controller parent)
			: base(parent)
		{
		}
		#endregion Constructors

		#region Properties
		#region Display Set Attributes
		/// <summary>
		/// Gets the atrtibutes of the DisplaySet (string splitted using commas).
		/// </summary>
		public string DisplaySetAttributes
		{
			get
			{
				List<string> lDSElements = new List<string>();
				if (CurrentDisplaySet != null && CurrentDisplaySet.DisplaySetItems.Count > 0)
				{
					foreach (DisplaySetItem litem in CurrentDisplaySet.DisplaySetItems)
					{
						if (litem.Visible)
						{
							if (!lDSElements.Contains(litem.Name))
							{
								lDSElements.Add(litem.Name);
							}
						}
					}
				}
				return UtilFunctions.StringList2String(lDSElements,",");
			}
		}
		#endregion Display Set Attributes

		/// <summary>
		/// Gets or sets the DisplaySet presentation.
		/// </summary>
		public IDisplaySetPresentation Viewer
		{
			get
			{
				return mViewer;
			}
			set
			{
				if (mViewer != null)
				{
					mViewer.ExecuteCommand -= new EventHandler<ExecuteCommandEventArgs>(HandleViewerExecuteCommand);
					mViewer.SelectionChanged -= new EventHandler<SelectedChangedEventArgs>(HandleViewerSelectionChanged);
				}

				mViewer = value;
				if (mViewer != null && CurrentDisplaySet != null)
				{
					// Add DisplaySet elements to the presentation.
					foreach (DisplaySetItem item in CurrentDisplaySet.DisplaySetItems)
					{
						List<string> lAgents = new List<string>(item.Agents.ToArray());
						if (Logic.Agent.IsActiveFacet(lAgents) && item.Visible)
						{
							mViewer.AddDisplaySetItem(item.Name, item.Alias, item.IdXML, item.ModelType, item.DefinedSelectionOptions, item.Agents.ToArray(), item.Width);
						}
					}
					mViewer.SelectionChanged += new EventHandler<SelectedChangedEventArgs>(HandleViewerSelectionChanged);
					IEditorPresentation lEditorPresentation = mViewer as IEditorPresentation;
					if (lEditorPresentation != null )
					{
						ArgumentOVPreloadController lArgumentOVPreloadController  = this.Parent as ArgumentOVPreloadController;
						if (lArgumentOVPreloadController  != null)
						{
							lEditorPresentation.NullAllowed = lArgumentOVPreloadController.NullAllowed;
						}
					}

					mViewer.ExecuteCommand += new EventHandler<ExecuteCommandEventArgs>(HandleViewerExecuteCommand);

				}
			}
		}
		/// <summary>
		/// Gets or sets the value of the DisplaySet.
		/// </summary>
		public List<Oid> Values
		{
			get
			{
				if (Viewer != null)
				{
					return Viewer.Values;
				}
				return null;
			}
			set
			{
				if (Viewer != null)
				{
					Viewer.Values = value;
					mLastValues = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicationg whether the DisplaySet presentation is enabled or not.
		/// </summary>
		public bool Enabled
		{
			get
			{
				if (Viewer != null)
				{
					return Viewer.Enabled;
				}
				return false;
			}
			set
			{
				if (Viewer != null)
				{
					Viewer.Enabled = value;
				}
			}
		}
		/// <summary>
		/// Get or Sets the Number of Instances Presentation
		/// </summary>
		public ILabelPresentation NumberOfInstances
		{
			get
			{
				return mNumberOfInstances;
			}
			set
			{
				this.mNumberOfInstances = value;
			}
		}
		/// <summary>
		/// Gets the list of Modeled DisplaySets information.
		/// </summary>
		public List<DisplaySetInformation> DisplaySetList
		{
			get
			{
				return mDisplaySetList;
			}
			protected set
			{
				mDisplaySetList = value;
			}
		}
		/// <summary>
		/// User preferences trigger
		/// </summary>
		public ITriggerPresentation PreferencesTrigger
		{
			get
			{
				return mPreferencesTrigger;
			}
			set
			{
				if (mPreferencesTrigger != null)
				{
					mPreferencesTrigger.Triggered -= new EventHandler<TriggerEventArgs>(HandlePreferencesTriggered);
				}
				mPreferencesTrigger = value;
				if (mPreferencesTrigger != null)
				{
					mPreferencesTrigger.Triggered += new EventHandler<TriggerEventArgs>(HandlePreferencesTriggered);
					mPreferencesTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_PREFERENCES, LanguageConstantValues.L_POP_UP_MENU_PREFERENCES);
				}

			}
		}
		/// <summary>
		/// User preferences. Save columns width trigger.
		/// </summary>
		public ITriggerPresentation SaveColumnsWidthTrigger
		{
			get
			{
				return mSaveColumnsWidthTrigger;
			}
			set
			{
				if (mSaveColumnsWidthTrigger != null)
				{
					mSaveColumnsWidthTrigger.Triggered -= new EventHandler<TriggerEventArgs>(HandleSaveColumnsWidthTriggered);
				}
				mSaveColumnsWidthTrigger = value;
				if (mSaveColumnsWidthTrigger != null)
				{
					mSaveColumnsWidthTrigger.Triggered += new EventHandler<TriggerEventArgs>(HandleSaveColumnsWidthTriggered);
					mSaveColumnsWidthTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_SAVECOLUMNSWIDTH, LanguageConstantValues.L_POP_UP_MENU_SAVECOLUMNSWIDTH);

					// If the selected DisplaySet is a modeled one, no save columns available
					if (CurrentDisplaySet != null && CurrentDisplaySet.Custom )
					{
						mSaveColumnsWidthTrigger.Enabled = true;
					}
					else
					{
						mSaveColumnsWidthTrigger.Enabled = false;
					}
				}

			}
		}
		/// <summary>
		/// Current DisplaySet
		/// </summary>
		public DisplaySetInformation CurrentDisplaySet
		{
			get
			{
				return mCurrentDisplaySet;
			}
			set
			{
				mCurrentDisplaySet = value;
				// If mCurrentDisplaySet doesn't appear in the list, add it.
				if (mCurrentDisplaySet != null)
				{
					mCurrentDisplaySet.CheckEditableItems();
					bool found = false;
					foreach (DisplaySetInformation displaySetInfo in DisplaySetList)
					{
						if (displaySetInfo.Name.Equals(mCurrentDisplaySet.Name))
						{
							found = true;
							break;
						}
					}
					if (!found)
					{
						DisplaySetList.Add(mCurrentDisplaySet);
					}

					// Set the New DisplaySet items to the viewer
					ChangeDisplaySetItems();
				}
			}
		}
		/// <summary>
		/// Trigger for executing the Service associated with the DisplaySet
		/// </summary>
		public ITriggerPresentation ExecuteServiceTrigger
		{
			get
			{
				return mExecuteServiceTrigger;
			}
			set
			{
				if (mExecuteServiceTrigger != null)
				{
					mExecuteServiceTrigger.Triggered -= new EventHandler<TriggerEventArgs>(HandleExecuteServiceTriggered);
				}
				mExecuteServiceTrigger = value;
				if (mExecuteServiceTrigger != null)
				{
					mExecuteServiceTrigger.Triggered += new EventHandler<TriggerEventArgs>(HandleExecuteServiceTriggered);
				}

			}
		}
		/// <summary>
		/// Indicates that exist changes pending to be saved
		/// </summary>
		public bool PendingChanges
		{
			get
			{
				return mPendingChanges;
			}
			set
			{
				mPendingChanges = value;
				if (ExecuteServiceTrigger != null)
				{
					ExecuteServiceTrigger.Enabled = value;
				}
			}
		}
		/// <summary>
		/// Flag to avoid loops with the SelectecInstanceChanged even
		/// </summary>
		public bool RaiseSelectecInstanceChanged
		{
			get
			{
				return mRaiseSelectecInstanceChanged;
			}
			set
			{
				mRaiseSelectecInstanceChanged = value;
			}
		}
		/// <summary>
		/// Previous value, in order to compare with the new ones
		/// </summary>
		public List<Oid> PreviousValue
		{
			get
			{
				return mPreviousValue;
			}
			set
			{
				mPreviousValue = value;
			}
		}
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when the selected instance changes.
		/// </summary>
		public event EventHandler<SelectedInstanceChangedEventArgs> SelectedInstanceChanged;
		/// <summary>
		/// Occurs when from presentation want execute commands.
		/// </summary>
		public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Hsndles the Execute command event from the viewer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleViewerExecuteCommand(object sender, ExecuteCommandEventArgs e)
		{
			ProcessViewerExecuteCommand(sender, e);
		}
		/// <summary>
		/// Event handler when selection changes.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">TriggerEventArgs.</param>
		private void HandleViewerSelectionChanged(object sender, SelectedChangedEventArgs e)
		{
			OnSelectedInstanceChanged(new SelectedInstanceChangedEventArgs(e.SelectedInstances, e.EnabledActionsKeys, e.EnabledNavigationsKeys));
		}
		/// <summary>
		/// Preferences Trigger Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandlePreferencesTriggered(object sender, TriggerEventArgs e)
		{
			IUQueryController queryController = Parent as IUQueryController;

			if (queryController == null)
				return;

			if (!queryController.CheckPendingChanges(true, true))
			{
				return;
			}

			bool lRefresh = false;
			lRefresh = ScenarioManager.LaunchPreferencesScenario(queryController);

			if (!lRefresh)
				return;

			// Refresh all data
			OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteRefresh));
		}
		/// <summary>
		/// SaveColumnsWidth Trigger Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleSaveColumnsWidthTriggered(object sender, TriggerEventArgs e)
		{
			ProcessSaveColumnsWidthTriggered(sender, e);
		}
		/// <summary>
		/// Execute service triggered event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleExecuteServiceTriggered(object sender, TriggerEventArgs e)
		{
			ProcessExecuteServiceTriggered(sender, e);
		}
		#endregion Event Handlers

		#region Process Event
		/// <summary>
		/// Actions related with the Execute command event from the viewer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessViewerExecuteCommand(object sender, ExecuteCommandEventArgs e)
		{
			// Execute the service
			if (e.ExecuteCommandType == ExecuteCommandType.ExecuteDisplaySetService)
			{
				ProcessExecuteServiceTriggered(this, new TriggerEventArgs());
				return;
			}

			// Values has been modified in the Display Set viewer
			if (e.ExecuteCommandType == ExecuteCommandType.ValuesHasBeenModified)
			{
				PendingChanges = true;
				return;
			}

			OnExecuteCommand(e);
		}
		/// <summary>
		/// Actions related with the Save Columns Width event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessSaveColumnsWidthTriggered(object sender, TriggerEventArgs e)
		{
			if (CurrentDisplaySet == null || !CurrentDisplaySet.Custom)
			{
				return;
			}

			// Get the columns width from the Viewer
			List<int> columnsWidth = Viewer.GetColumnsWidth();
			if (columnsWidth == null)
				return;

			// Assign the width to the visible items in the custom DisplaySet
			try
			{
				int colIndex = 0;
				foreach (DisplaySetItem item in CurrentDisplaySet.DisplaySetItems)
				{
					if (item.Visible)
					{
						item.Width = columnsWidth[colIndex];
						colIndex++;
					}
				}
			}
			catch
			{
			}

			SaveScenarioInfoInPrefecences();
		}
		#endregion Process Event

		#region Event Raisers
		/// <summary>
		/// Raise ExecuteCommand event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnExecuteCommand(ExecuteCommandEventArgs e)
		{
			EventHandler<ExecuteCommandEventArgs> handler = ExecuteCommand;

			if (handler != null)
			{
				handler(this, e);
			}
		}
		/// <summary>
		/// Raises the Selected Instance Changed
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnSelectedInstanceChanged(SelectedInstanceChangedEventArgs eventArgs)
		{
			if (!RaiseSelectecInstanceChanged)
			{
				return;
			}

			EventHandler<SelectedInstanceChangedEventArgs> handler = SelectedInstanceChanged;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		#endregion Event Raisers

		#region Methods

		#region Initialize
		/// <summary>
		/// Initializes the DisplaySet.
		/// </summary>
		public virtual void Initialize()
		{
			// Initialize every DisplaySet in the list
			foreach (DisplaySetInformation displaySetInfo in DisplaySetList)
			{
				displaySetInfo.Initialize();
			}

			// Configure DisplaySet items depending on application agent.
			if (CurrentDisplaySet != null && mViewer != null)
			{
				ChangeDisplaySetItems();
			}

			// If current DisplaySet is editable or not, show or hide the trigger
			if (ExecuteServiceTrigger != null)
			{
				ExecuteServiceTrigger.Visible = CurrentDisplaySet.Editable;
			}
		}
		#endregion Initialize

		#region Set Message
		/// <summary>
		/// Sets a message to show.
		/// </summary>
		/// <param name="message">String message.</param>
		public virtual void SetMessage(string message)
		{
			if (Viewer != null)
			{
				Viewer.SetMessage(message);
			}
		}
		#endregion Set Message

		#region Set population
		/// <summary>
		/// Fills the DisplaySet presentation with the DataTable.
		/// </summary>
		/// <param name="data">Data to populate.</param>
		public virtual void SetPopulation(DataTable data, bool discardExistingData, List<Oid> selectedOids)
		{
			if (Viewer != null)
			{
				Viewer.ShowData(data, selectedOids);
			}

			// No pending changes
			PendingChanges = false;
		}
		#endregion Set population

		#region Change DisplaySet Items
		/// <summary>
		/// Removes the current DisplaySetItems and assigns to the Viewer the items of the selected DisplaySet
		/// </summary>
		private void ChangeDisplaySetItems()
		{
			if (CurrentDisplaySet == null || Viewer == null)
			{
				return;
			}

			// If the selected DisplaySet is a modeled one, no Save Columns is available
			if (SaveColumnsWidthTrigger != null)
			{
				if (CurrentDisplaySet.Custom)
					SaveColumnsWidthTrigger.Enabled = true;
				else
					SaveColumnsWidthTrigger.Enabled = false;
			}

			// Remove the current ones
			Viewer.RemoveAllDisplaySetItems();

			foreach (DisplaySetItem lItem in CurrentDisplaySet.DisplaySetItems)
			{
				if (lItem.Visible)
				{
					bool allowsNullInEditMode = false;
					if (CurrentDisplaySet.ServiceInfo != null)
					{
						allowsNullInEditMode = CurrentDisplaySet.ServiceInfo.DisplaySetElementAllowsNull(lItem.Name);
					}
					// Set format to the displayset items.
					SetFormatDisplaySetItem(lItem.Name, lItem.Alias, lItem.ModelType, lItem.DefinedSelectionOptions, lItem.Width, lItem.Editable && CurrentDisplaySet.Editable, allowsNullInEditMode);
				}
			}
		}
		#endregion Change DisplaySet Items

		#region Set format DisplaySet item
		/// <summary>
		/// Formats a DisplaySet item.
		/// </summary>
		/// <param name="name">Name of the item.</param>
		/// <param name="alias">Alias of the item.</param>
		/// <param name="modelType">ModelType of the item.</param>
		/// <param name="definedSelectionOptions">Defined selection options of the item.</param>
		/// <param name="width">Width of the item.</param>
		/// <param name="editable">If the item is editable or not.</param>
		/// <param name="allowsNullInEditMode">If the items allows null values in edit mode.</param>
		public virtual void SetFormatDisplaySetItem(
			string name,
			string alias,
			ModelType modelType,
			List<KeyValuePair<object, string>> definedSelectionOptions,
			int width,
			bool editable,
			bool allowsNullInEditMode)
		{
			if (Viewer != null)
			{
				Viewer.SetFormatDisplaySetItem(name, alias, modelType, definedSelectionOptions, width, editable, allowsNullInEditMode);
			}
		}
		#endregion Set format DisplaySet item

		/// <summary>
		/// Save the information in user preferences
		/// </summary>
		protected virtual void SaveScenarioInfoInPrefecences()
		{
			// For Instance interaction unit, no columns width information is stored
		}
		/// <summary>
		/// Return the DisplaySet by name
		/// </summary>
		/// <param name="displaySetName"></param>
		/// <returns></returns>
		public DisplaySetInformation GetDisplaySetByName(string displaySetName)
		{
			if (DisplaySetList == null)
			{
				return null;
			}

			foreach (DisplaySetInformation displaySet in DisplaySetList)
			{
				if (displaySet.Name.Equals(displaySetName, StringComparison.InvariantCultureIgnoreCase))
				{
					return displaySet;
				}
			}
			return null;
		}
		/// <summary>
		/// Process the Display Service execute event
		/// </summary>
		protected virtual void ProcessExecuteServiceTriggered(object sender, TriggerEventArgs e)
		{
			// Before executing the DisplaySet Service it is needed to check for pending changes in associated Service IU.
			IUQueryController queryController = Parent as IUQueryController;
			if (queryController != null && !queryController.CheckPendingChanges(false, true))
			{
				return;
			}
			if (CurrentDisplaySet.ServiceInfo == null)
			{
				return;
			}

			// Gets modified rows from the viewer
			DataTable modifiedRows = Viewer.GetModifiedRows();
			if (modifiedRows == null)
			{
				return;
			}

			// Validate the modified data
			if (!ValidateModifiedRows(modifiedRows))
			{
				return;
			}

			// Error datatable
			DataTable errorReport = new DataTable();
			// Column for the OID
			string instanceColumnsName = CurrentDisplaySet.ServiceInfo.SelectedInstanceArgumentAlias;
			errorReport.Columns.Add(instanceColumnsName);
			// Column for the error message
			string lReportMessage = CultureManager.TranslateString(LanguageConstantKeys.L_MULTIEXE_EXECUTION, LanguageConstantValues.L_MULTIEXE_EXECUTION);
			errorReport.Columns.Add(lReportMessage);

			IUServiceContext lServiceContext = null;
			// For every modified row do...
			foreach (DataRow rowValues in modifiedRows.Rows)
			{
				// Create new IUServiceContext.
				lServiceContext = new IUServiceContext(null, CurrentDisplaySet.ServiceInfo.ClassServiceName, CurrentDisplaySet.ServiceInfo.ServiceName, null);
				// Add argunment this to the service context.
				List<Oid> instanceOIDs = new List<Oid>();
				Oid instanceOID = Adaptor.ServerConnection.GetOid(modifiedRows, rowValues);
				instanceOIDs.Add(instanceOID);
				lServiceContext.InputFields.Add(CurrentDisplaySet.ServiceInfo.SelectedInstanceArgumentName, new IUContextArgumentInfo(CurrentDisplaySet.ServiceInfo.SelectedInstanceArgumentName, instanceOIDs, true, null));

				// Fill the collections for the other inbound arguments.
				foreach (DisplaySetServiceArgumentInfo argumentInfo in CurrentDisplaySet.ServiceInfo.ArgumentDisplaySetPairs.Values)
				{
					object value = rowValues[argumentInfo.DSElementName];
					if (value.GetType() == typeof(System.DBNull))
					{
						value = null;
					}
					//Add input arguments with context.
					lServiceContext.InputFields.Add(argumentInfo.Name, new IUContextArgumentInfo(argumentInfo.Name, value, true, null));
				}
				try
				{
					// Execute service.
					Logic.ExecuteService(lServiceContext);
				}
				catch (Exception exc)
				{
					string lAlternateKeyName = CurrentDisplaySet.ServiceInfo.SelectedInstanceArgumentAlternateKeyName;
					if (lAlternateKeyName != string.Empty)
					{
						instanceOID = Logic.GetAlternateKeyFromOid(instanceOID, lAlternateKeyName);
					}

					// Add a new row in the error datatable.
					DataRow newReportRow = errorReport.NewRow();
					newReportRow[lReportMessage] = exc.Message;
					newReportRow[instanceColumnsName] = UtilFunctions.OidFieldsToString(instanceOID, ' ');
					errorReport.Rows.Add(newReportRow);
				}
			}

			// If errors have been found, show them
			if (errorReport.Rows.Count > 0)
			{
				// Show error message to the user
				ScenarioManager.LaunchMultiExecutionReportScenario(errorReport, CurrentDisplaySet.ServiceInfo.ServiceAlias, null, null);
			}

			// Remove the Pending changes mark
			PendingChanges = false;

			// Refresh the data
			OnExecuteCommand(new ExecuteCommandRefreshEventArgs(null));
		}
		/// <summary>
		/// Validates the modified rows values. Not Null and datatype
		/// </summary>
		/// <param name="modifiedRows"></param>
		/// <returns></returns>
		private bool ValidateModifiedRows(DataTable modifiedRows)
		{
			// Error datatable
			DataTable errorReport = new DataTable();
			// Column for the OID
			string instanceColumnsName = CurrentDisplaySet.ServiceInfo.SelectedInstanceArgumentAlias;
			errorReport.Columns.Add(instanceColumnsName);
			// Column for the error message
			string lReportMessage = CultureManager.TranslateString(LanguageConstantKeys.L_MULTIEXE_EXECUTION, LanguageConstantValues.L_MULTIEXE_EXECUTION);
			errorReport.Columns.Add(lReportMessage);

			// Not null and Datatype validation
			foreach (DataRow rowValues in modifiedRows.Rows)
			{
				Oid instanceOID = Adaptor.ServerConnection.GetOid(modifiedRows, rowValues);
				foreach (DisplaySetServiceArgumentInfo argumentInfo in CurrentDisplaySet.ServiceInfo.ArgumentDisplaySetPairs.Values)
				{
					object value = rowValues[argumentInfo.DSElementName];

					// Null validation
					if (value.GetType() == typeof(System.DBNull))
					{
						if (!argumentInfo.AllowsNull)
						{

							// Add a nuw row in the error datatable
							DataRow newReportRow = errorReport.NewRow();
							string nameInScenario = argumentInfo.Alias;
							object[] lArgs = new object[1];
							lArgs[0] = nameInScenario;
							string lErrorMessage = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_NECESARY, LanguageConstantValues.L_VALIDATION_NECESARY, lArgs);
							newReportRow[lReportMessage] = lErrorMessage;
							newReportRow[instanceColumnsName] = UtilFunctions.OidFieldsToString(instanceOID, ' ');
							errorReport.Rows.Add(newReportRow);
						}
					}
					else
					{
						// Data type validation
						if (!DefaultFormats.CheckDataType(value.ToString(), argumentInfo.DataType, false))
						{
							// Add a nuw row in the error datatable
							DataRow newReportRow = errorReport.NewRow();
							string lMask = DefaultFormats.GetHelpMask(argumentInfo.DataType, string.Empty);
							object[] lArgs = new object[1];
							lArgs[0] = lMask;
							string lErrorMessage = argumentInfo.Alias + ":  ";
							lErrorMessage += CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_INVALID_FORMAT_MASK, LanguageConstantValues.L_VALIDATION_INVALID_FORMAT_MASK, lArgs);
							newReportRow[lReportMessage] = lErrorMessage;
							newReportRow[instanceColumnsName] = UtilFunctions.OidFieldsToString(instanceOID, ' ');
							errorReport.Rows.Add(newReportRow);
						}
					}
				}
			}

			// If errors have been found, show them
			if (errorReport.Rows.Count > 0)
			{
				// Show error message to the user
				ScenarioManager.LaunchMultiExecutionReportScenario(errorReport, CurrentDisplaySet.ServiceInfo.ServiceAlias, null, null);
				return false;
			}

			return true;
		}
		/// <summary>
		/// Apply multilanguage to the DisplaySet elements.
		/// </summary>
		public void ApplyMultilanguage()
		{
			if (ExecuteServiceTrigger != null)
			{
				ExecuteServiceTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_SAVE, LanguageConstantValues.L_SAVE, ExecuteServiceTrigger.Value.ToString());
			}
		}
		#endregion Methods
	}
}
