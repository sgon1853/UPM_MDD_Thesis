// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections.Specialized;
using SIGEM.Client.Controllers;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction for a list of .Net controls.
	/// </summary>
	public class ControlListPresentation : IDisplaySetPresentation
	{
		#region Members
		/// <summary>
		/// List of pairs (label-control) for every attribute.
		/// </summary>
		private List<KeyValuePair<string, KeyValuePair<ILabelPresentation, IEditorPresentation>>> mControlList = new List<KeyValuePair<string, KeyValuePair<ILabelPresentation, IEditorPresentation>>>();
		/// <summary>
		/// Local Oid value
		/// </summary>
		private List<Oid> mValue = new List<Oid>();
		/// <summary>
		/// Datatable
		/// </summary>
		private DataTable mData = null;
		/// <summary>
		/// Editable controls in the scenario
		/// </summary>
		private StringCollection mEditableControls = new StringCollection();
		/// <summary>
		/// Internal Flag, in order to ignore the Value Changed event from the controls when we assign the value.
		/// </summary>
		private bool mIgnoreValueChangedEvents = false;
		#endregion Members

		#region Constructors
		public ControlListPresentation(
			ToolStripMenuItem options,
			ToolStripMenuItem exportToExcel,
			ToolStripMenuItem refresh,
			ToolStripMenuItem help,
			ToolStripMenuItem navigations)
		{
			if (exportToExcel != null)
			{
				exportToExcel.Text = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_EXPORT_TO_EXCEL, LanguageConstantValues.L_POP_UP_MENU_EXPORT_TO_EXCEL);
				exportToExcel.Click += new EventHandler(HandleExportToExcelClick);
			}

			if (refresh != null)
			{
				refresh.Text = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_REFRESH, LanguageConstantValues.L_POP_UP_MENU_REFRESH);
				refresh.Click += new EventHandler(HandleRefreshClick);
			}

			if (help != null)
			{
				help.Text = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_HELP, LanguageConstantValues.L_POP_UP_MENU_HELP);
			}

			if (options != null)
			{
				options.Text = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_OPTIONS, LanguageConstantValues.L_POP_UP_MENU_OPTIONS);
			}

			if (navigations != null)
			{
				navigations.Text = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_NAVIGATIONS, LanguageConstantValues.L_POP_UP_MENU_NAVIGATIONS);
			}
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets Enabled property.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return true;
			}
			set
			{
			}
		}
		/// <summary>
		/// Gets Values.
		/// </summary>
		public List<Oid> Values
		{
			get
			{
                return Oid.CopyOidList(mValue);
			}
			set
			{
			}
		}
		/// <summary>
		/// Gets the number of elements contained in the control.
		/// </summary>
		public int Count
		{
			get
			{
				return 1;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the control is displayed.
		/// </summary>
		public bool Visible
		{
			get
			{
				return true;
			}
			set
			{
			}
		}
		/// <summary>
		/// Gets the text value of the array element depending on its name.
		/// </summary>
		/// <param name="name">Name of the element.</param>
		/// <returns>Returns the text value of the element.</returns>
		public string this[string name]
		{
			get
			{
				string lMessageException = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR_NOT_IMPLEMENTED, LanguageConstantValues.L_ERROR_NOT_IMPLEMENTED);
				throw new Exception(lMessageException);
			}
		}
		/// <summary>
		/// Display texts for boolean values.
		/// </summary>
		public string DisplayTextBoolNull
		{
			get
			{
				return "";
			}
			set
			{
			}
		}
		/// <summary>
		/// Display texts for boolean values.
		/// </summary>
		public string DisplayTextBoolTrue
		{
			get
			{
				return "";
			}
			set
			{
			}
		}
		/// <summary>
		/// Display texts for boolean values.
		/// </summary>
		public string DisplayTextBoolFalse
		{
			get
			{
				return "";
			}
			set
			{
			}
		}
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when the selected item is changed.
		/// </summary>
		public event EventHandler<SelectedChangedEventArgs> SelectionChanged;
		/// <summary>
		/// This event is raised under different conditions
		/// </summary>
		public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Handles the export to excel event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleExportToExcelClick(object sender, EventArgs e)
		{
			if (mData != null && mValue.Count > 0)
			{
				Excel.ExportToExcel(mData);
			}
		}
		/// <summary>
		/// Contextual Menu Refresh population of instances.
		/// </summary>
		/// <param name="sender">Control reference that raise the click event.</param>
		/// <param name="e">Event arguments.</param>
		private void HandleRefreshClick(object sender, EventArgs e)
		{
			OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteRefresh));
		}
		/// <summary>
		/// Handles the Value changed event from the editor
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleEditorValueChanged(object sender, ValueChangedEventArgs e)
		{
			if (!mIgnoreValueChangedEvents)
			{
				OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ValuesHasBeenModified));
			}
		}
		#endregion Event Handlers

		#region Event Raisers
		/// <summary>
		/// Raises the Selection Changed event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnSelectionChanged(SelectedChangedEventArgs eventArgs)
		{
			EventHandler<SelectedChangedEventArgs> handler = SelectionChanged;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the Execute Command event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnExecuteCommand(ExecuteCommandEventArgs eventArgs)
		{
			EventHandler<ExecuteCommandEventArgs> handler = ExecuteCommand;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		#endregion Event Raisers

		#region Methods
		/// <summary>
		/// Adds a new control to the list
		/// </summary>
		/// <param name="attributeName"></param>
		/// <param name="aliasLabel"></param>
		/// <param name="valueControl"></param>
		public void AddControl(string attributeName, ILabelPresentation aliasLabel, IEditorPresentation valueControl)
		{
			if (aliasLabel != null)
				aliasLabel.Visible = false;

			if (valueControl != null)
				valueControl.Visible = false;

			KeyValuePair<ILabelPresentation, IEditorPresentation> pair =
				new KeyValuePair<ILabelPresentation, IEditorPresentation>(aliasLabel, valueControl);

			KeyValuePair<string, KeyValuePair<ILabelPresentation, IEditorPresentation>> control =
				new KeyValuePair<string, KeyValuePair<ILabelPresentation, IEditorPresentation>>(attributeName, pair);

			mControlList.Add(control);
		}
		/// <summary>
		/// Shows specified data on ListView.
		/// </summary>
		/// <param name="data">Data to show.</param>
		public void ShowData(DataTable data, List<Oid> selectedOids)
		{
			mIgnoreValueChangedEvents = true;

			// Clear Instance selector.
			mValue.Clear();
			// Clear viewer.
			CleanData();

			if ((data != null) && (data.Rows.Count > 0))
			{
				// Load from the DataTable.
				List<DataColumn> displaySetColumns = Adaptor.ServerConnection.GetDisplaySetColumns(data);

				DataRow row = data.Rows[0];
				mData = data;

				// Store the Oid
				mValue.Add(Adaptor.ServerConnection.GetLastOid(data));
				// Show data in the ListView
				foreach (DataColumn column in displaySetColumns)
				{
					foreach (KeyValuePair<string, KeyValuePair<ILabelPresentation, IEditorPresentation>> attribute in mControlList)
					{
						if (column.ColumnName.Equals(attribute.Key, StringComparison.OrdinalIgnoreCase))
						{
							// Editor
							if (attribute.Value.Value != null)
							{
								if (row.ItemArray[column.Ordinal] == DBNull.Value)
									attribute.Value.Value.Value = null;
								else
									attribute.Value.Value.Value = row.ItemArray[column.Ordinal];

								// If the control is an editable one, change the Read Only property
								if (mEditableControls.Contains(attribute.Key))
								{
									attribute.Value.Value.ReadOnly = false;
								}
							}
						}
					}
				}
			}

			mIgnoreValueChangedEvents = false;

			// Obtain the keys to enable or disable the actions and navigations.
			List<string> actionsKeys = new List<string>();
			List<string> navigationsKeys = new List<string>();
			try
			{
				DataRow row = data.Rows[0];
				actionsKeys.Add((string)row[Constants.ACTIONS_ACTIVATION_COLUMN_NAME]);
				navigationsKeys.Add((string)row[Constants.NAVIGATIONS_ACTIVATION_COLUMN_NAME]);
			}
			catch
			{
			}
			// Raise the event.
			OnSelectionChanged(new SelectedChangedEventArgs(mValue, actionsKeys, navigationsKeys));
		}
		/// <summary>
		/// Get Selected Cell (empty function due to compatibility of the interface).
		/// </summary>
		/// <param name="selectedRowNumber">Selected row.</param>
		/// <param name="columnName">Column name.</param>
		/// <returns></returns>
		public object GetSelectedCell(int selectedRowNumber, string columnName)
		{
			return null;
		}
		public void SetFormatDisplaySetItem(string name, string alias, ModelType modelType, List<KeyValuePair<object, string>> definedSelectionOptions, int width, bool editable, bool allowsNullInEditMode)
		{

			foreach (KeyValuePair<string, KeyValuePair<ILabelPresentation, IEditorPresentation>> attribute in mControlList)
			{
				if (name.Equals(attribute.Key, StringComparison.OrdinalIgnoreCase))
				{
					// Alias
					if (attribute.Value.Key != null)
					{
						attribute.Value.Key.Visible = true;
						attribute.Value.Key.Value = UtilFunctions.ProtectAmpersandChars(alias);
					}

					// If the control is a Selector, assign the possible values.
					if (attribute.Value.Value != null)
					{
						attribute.Value.Value.Visible = true;
						ISelectorPresentation selector = attribute.Value.Value as ISelectorPresentation;
						if (selector != null && definedSelectionOptions != null)
						{
							// Fill the presentation with the list of options.
							selector.Items = definedSelectionOptions;
						}

						// Add the control the editable control list
						if (editable)
						{
							mEditableControls.Add(name);
							// Suscribe to the Value changed event
							attribute.Value.Value.ValueChanged += new EventHandler<ValueChangedEventArgs>(HandleEditorValueChanged);

							// If the Display Set element allows null, set it to the control
							if (allowsNullInEditMode)
							{
								attribute.Value.Value.NullAllowed = true;
							}
						}
						else
						{
							attribute.Value.Value.NullAllowed = true;
						}

					}
				}
			}
		}
		/// <summary>
		/// Returns the modified rows (only one).
		/// </summary>
		/// <returns></returns>
		public DataTable GetModifiedRows()
		{
			if (!ValidateData())
			{
				return null;
			}

			// Get the values from the editable controls
			if (mData == null || mData.Rows.Count == 0)
			{
				return null;
			}

			DataRow row = mData.Rows[0];
			foreach (KeyValuePair<string, KeyValuePair<ILabelPresentation, IEditorPresentation>> attribute in mControlList)
			{
				if (mEditableControls.Contains(attribute.Key))
				{
					// Editor
					if (attribute.Value.Value != null)
					{
						object value = attribute.Value.Value.Value;
						if (value == null)
						{
							row[attribute.Key] = System.DBNull.Value;
						}
						else
						{
							row[attribute.Key] = value;
						}
					}
				}
			}
			return mData;
		}
		/// <summary>
		/// Validates the data type of every editable control
		/// </summary>
		/// <returns></returns>
		private bool ValidateData()
		{
			bool lResult = true;
			object[] lArgs = new object[1];
			foreach (KeyValuePair<string, KeyValuePair<ILabelPresentation, IEditorPresentation>> attribute in mControlList)
			{
				if (mEditableControls.Contains(attribute.Key))
				{
					lArgs[0] = attribute.Value.Key.Value;
					lResult = lResult & attribute.Value.Value.Validate(CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_NECESARY, LanguageConstantValues.L_VALIDATION_NECESARY, lArgs));
				}
			}

			return lResult;
		}
		/// <summary>
		/// Sets message (empty function due to compatibility of the interface).
		/// </summary>
		/// <param name="message">Message.</param>
		public void SetMessage(string message)
		{
		}
		/// <summary>
		/// Remove all items in the view.
		/// </summary>
		private void CleanData()
		{
			try
			{
				// Clean de selected Oid
				mValue.Clear();
				foreach (KeyValuePair<string, KeyValuePair<ILabelPresentation, IEditorPresentation>> attribute in mControlList)
				{
					if (attribute.Value.Value != null)
					{
						attribute.Value.Value.Value = null;
						attribute.Value.Value.ReadOnly = true;
					}
				}
			}
			catch { }
		}
		public void AddDisplaySetItem(string name, string alias, string idxml, ModelType modelType, List<KeyValuePair<object, string>> definedSelectionOptions, string[] agents, int width)
		{
			// Make visible the controls related with the attribute
			foreach (KeyValuePair<string, KeyValuePair<ILabelPresentation, IEditorPresentation>> attribute in mControlList)
			{
				if (name.Equals(attribute.Key, StringComparison.OrdinalIgnoreCase))
				{
					// Alias
					if (attribute.Value.Key != null)
					{
						attribute.Value.Key.Visible = true;
					}
					// Editor
					if (attribute.Value.Value != null)
					{
						attribute.Value.Value.Visible = true;
						attribute.Value.Value.DataType = modelType;
					}
				}
			}
		}
		/// <summary>
		/// Removes all the DisplaySet items
		/// </summary>
		public void RemoveAllDisplaySetItems()
		{
			// Make no visible the controls
			foreach (KeyValuePair<string, KeyValuePair<ILabelPresentation, IEditorPresentation>> attribute in mControlList)
			{
				if (attribute.Value.Key != null)
				{
					attribute.Value.Key.Visible = false;
				}
				if (attribute.Value.Value != null)
				{
					attribute.Value.Value.Visible = false;
				}
			}
		}
		/// <summary>
		/// Get columns width
		/// </summary>
		public List<int> GetColumnsWidth()
		{
			return null;
		}
		/// <summary>
		/// Suscriber to KeyDown event
		/// </summary>
		/// <param name="sender">Control that raise the event.</param>
		/// <param name="e">Key pressed.</param>
		private void ExecuteCommand_KeyDown(object sender, KeyEventArgs e)
		{
			Keys lKeyPressed = (Keys)e.KeyCode;
			if (!(e.Control && e.Alt && e.Shift))
			{
				switch (lKeyPressed)
				{
					case Keys.F5: // Refresh Population
						{
							OnExecuteCommand(new ExecuteCommandRefreshEventArgs(null));
						}
						break;
					default:
						break;
				}
			}
		}
		#endregion Methods
	}
}
