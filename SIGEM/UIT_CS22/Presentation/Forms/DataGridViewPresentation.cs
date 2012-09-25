// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using SIGEM.Client.Controllers;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of the .NET DataGridView control.
	/// </summary>
	public class DataGridViewPresentation: IDisplaySetByBlocksPresentation
	{
		#region Members
		/// <summary>
		/// .NET DataGridView instance reference.
		/// </summary>
		protected DataGridView mDataGridViewIT;
		/// <summary>
		/// Texts to be shown for boolean values.
		/// </summary>
		private List<KeyValuePair<object, string>> mBooleanValues = new List<KeyValuePair<object, string>>();
		/// <summary>
		/// Flag to avoid raising extra events.
		/// </summary>
		private bool mRaiseEventCurrentCellChanged = false;
		/// <summary>
		/// Flag to enable or disable errors reporting.
		/// </summary>
		private bool mEnabledDataErrorReporting = false;
		/// <summary>
		/// Flag to handle the edit control key press event
		/// </summary>
		private bool mManageEditControlKeyPress = false;
		/// <summary>
		/// Indicates how to show the boolean values, Check Box mode when value is true or text mode if value is false
		/// </summary>
		private bool mShowBooleanAsCheckBox = true;
		/// <summary>
		/// Previous cell value, in order to compare after editing
		/// </summary>
		private object mPreviousCellValue = null;
		#endregion Members

		#region Constructors
		public DataGridViewPresentation(
			DataGridView dataGridView,
			ToolStripMenuItem exportToExcel,
			ToolStripMenuItem exportToWord,
			ToolStripMenuItem retriveAll,
			ToolStripMenuItem refresh,
			ToolStripMenuItem help,
			ToolStripMenuItem options,
			ToolStripMenuItem navigations,
			ExchangeInfo exchangeInfo)
		{
			mDataGridViewIT = dataGridView;
			if (mDataGridViewIT != null)
			{
				mDataGridViewIT.SelectionChanged += new EventHandler(HandleDataGridITCurrentCellChanged);
				mDataGridViewIT.Scroll += new ScrollEventHandler(HandleDataGridViewITScroll);
				mDataGridViewIT.DataError += new DataGridViewDataErrorEventHandler(HandleDataDridViewITDataError);
				mDataGridViewIT.KeyDown += new KeyEventHandler(HandleDataGridViewITKeyDown);
				mDataGridViewIT.DoubleClick += new EventHandler(HandleDataGridViewITDoubleClick);
				mDataGridViewIT.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(HandleDataGridViewITEditingControlShowing);
				mDataGridViewIT.CellBeginEdit += new DataGridViewCellCancelEventHandler(HandleDataGridViewITCellBeginEdit);
				mDataGridViewIT.CellEndEdit += new DataGridViewCellEventHandler(HandleDataGridViewITCellEndEdit);

				if (exportToExcel!= null)
				{
					exportToExcel.Text = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_EXPORT_TO_EXCEL, LanguageConstantValues.L_POP_UP_MENU_EXPORT_TO_EXCEL);
					exportToExcel.Click +=new EventHandler(HandleExportToExcel);
				}
				if (exportToWord != null)
				{
					exportToWord.Text = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_EXPORT_TO_WORD, LanguageConstantValues.L_POP_UP_MENU_EXPORT_TO_WORD);
					exportToWord.Click += new EventHandler(HandleExportToWord);
				}

				//If the argumet allow multiselection, the grid is configure in multiselection mode,
				//otherwise the grid is configure in simple selection mode.
				ExchangeInfoSelectionForward exchangeInfoSelection = exchangeInfo as ExchangeInfoSelectionForward;
				if (exchangeInfoSelection != null)
				{
					mDataGridViewIT.MultiSelect = exchangeInfoSelection.MultiSelectionAllowed;
				}

				if (retriveAll != null)
				{
					retriveAll.Text = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_RETRIEVE_ALL, LanguageConstantValues.L_POP_UP_MENU_RETRIEVE_ALL);
					retriveAll.Click += new EventHandler(HandleRetrieveAllClick);
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

                Form lContainerForm = mDataGridViewIT.FindForm();
                if (lContainerForm != null)
                {
                    lContainerForm.Shown += new EventHandler(HandleContainerForm_Shown);
                }
                else
                {
                    mRaiseEventCurrentCellChanged = true;
                }
			}
			// Assigns the default texts for boolean values
			AssignDefaultTextForBooleans();
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
				return mDataGridViewIT.Enabled;
			}
			set
			{
				mDataGridViewIT.Enabled = value;
			}
		}
		/// <summary>
		/// Gets or sets the selected Oids.
		/// </summary>
		public List<Oid> Values
		{
			get
			{
				return GetSelectedOIDs();
			}
			set
			{
				// Set the Oid.
				// If value is null or contains more than one, do not select anyone.
				SelectRowsByOid(value);
			}
		}
		/// <summary>
		/// Gets or sets Visible property.
		/// </summary>
		public bool Visible
		{
			get
			{
				return mDataGridViewIT.Visible;
			}
			set
			{
				mDataGridViewIT.Visible = value;
			}
		}
		/// <summary>
		/// Gets the number of elements (rows) contained in the control.
		/// </summary>
		public int Count
		{
			get
			{
				DataView lDataView = mDataGridViewIT.DataSource as DataView;

				if (lDataView == null)
				{
					return 0;
				}
				return lDataView.Count;
			}
		}
		/// <summary>
		/// Gets the number of selected elements (rows) of the control.
		/// </summary>
		public int SelectedCount
		{
			get
			{
				return mDataGridViewIT.SelectedRows.Count;
			}
		}
		/// <summary>
		/// Gets a cell text of the grid depending on the row index and column name.
		/// </summary>
		/// <param name="rowIndex">Row index.</param>
		/// <param name="columnName">Column name.</param>
		/// <returns>Returns the text of the required cell.</returns>
		public object this[int rowIndex, string columnName]
		{
			get
			{
				if (mDataGridViewIT.CurrentRow == null)
				{
					return null;
				}

				DataView lDataView = mDataGridViewIT.DataSource as DataView;
				if (lDataView == null)
				{
					return null;
				}
				return lDataView[rowIndex][columnName];
			}
		}
		/// <summary>
		/// Display Texts for Boolean values
		/// </summary>
		public string DisplayTextBoolNull
		{
			get
			{
				if (mBooleanValues.Count > 0)
				{
					return mBooleanValues[0].Value;
				}
				return "";
			}
			set
			{
				if (mBooleanValues.Count > 0)
				{
					mBooleanValues.RemoveAt(0);
				}
				mBooleanValues.Insert(0, new KeyValuePair<object,string>(null,value));
			}
		}
		public string DisplayTextBoolTrue
		{
			get
			{
				if (mBooleanValues.Count > 1)
				{
					return mBooleanValues[1].Value;
				}
				return "";
			}
			set
			{
				if (mBooleanValues.Count > 1)
				{
					mBooleanValues.RemoveAt(1);
				}
				mBooleanValues.Insert(1, new KeyValuePair<object, string>(true, value));
			}
		}
		public string DisplayTextBoolFalse
		{
			get
			{
				if (mBooleanValues.Count > 2)
				{
					return mBooleanValues[2].Value;
				}
				return "";
			}
			set
			{
				if (mBooleanValues.Count > 2)
				{
					mBooleanValues.RemoveAt(2);
				}
				mBooleanValues.Insert(2, new KeyValuePair<object, string>(false, value));
			}
		}
		/// <summary>
		/// Indicates how to show the boolean values, Check Box or text
		/// </summary>
		public bool ShowBooleanAsCheckBox
		{
			get
			{
				return mShowBooleanAsCheckBox;
			}
			set
			{
				mShowBooleanAsCheckBox = value;
			}
		}
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when selected cell is changed.
		/// </summary>
		public event EventHandler<SelectedChangedEventArgs> SelectionChanged;
		/// <summary>
		/// Occurs when more blocks are required.
		/// </summary>
		public event EventHandler<TriggerEventArgs> MoreBlocks;
		/// <summary>
		/// Execute Command event Implementation of IEditorPresentation interface.
		/// </summary>
		public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Executes actions related to CellChanged event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleDataGridITCurrentCellChanged(object sender, System.EventArgs e)
		{
			if (mRaiseEventCurrentCellChanged)
			{
                ProcessDataGridITCurrentCellChanged(sender, e);
			}
		}

        /// <summary>
        /// Launch the OnSelection event initializing the event arguments
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProcessDataGridITCurrentCellChanged(object sender, EventArgs e)
        {
            List<string> actionsKeys = new List<string>();
            List<string> navigationsKeys = new List<string>();
            GetEnabledActionsNavigationsKeys(actionsKeys, navigationsKeys);
            OnSelectionChanged(new SelectedChangedEventArgs(GetSelectedOIDs(), actionsKeys, navigationsKeys));
        }
		/// <summary>
		/// Actions related with the KeyDown event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleDataGridViewITKeyDown(object sender, KeyEventArgs e)
		{

            // Manage default Keys
			Keys lKeyPressed = (Keys)e.KeyCode;

			if (!(e.Control && e.Alt && e.Shift))
			{
				switch (lKeyPressed)
				{
					case Keys.F5: // Refresh Population
						e.Handled = true;
						List<Oid> lSelectedOids = this.GetSelectedOIDs();
						OnExecuteCommand(new ExecuteCommandRefreshEventArgs(lSelectedOids));
						break;

					case Keys.F6: // Retrive All Instances.
						e.Handled = true;
						OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteRetriveAll));
						break;


					case Keys.F9: // Execute service
						e.Handled = true;
						OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteDisplaySetService));
						break;

					default:
						break;
				}

                if (e.Handled)
                    return;
			}

            // Raise the event 
            ExecuteCommandEventArgs lEventArgs = new ExecuteCommandEventArgs(e.KeyData);
            OnExecuteCommand(lEventArgs);

            // It has not been handled, then default behaviour
            if (!lEventArgs.Handled)
            {
                if (!(e.Control && e.Alt && e.Shift))
                {
                    switch (lKeyPressed)
                    {
                        case Keys.Delete: // Execute the First Destroy Service in Acctions Pattern.
                            e.Handled = true;
                            OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteFirstDestroyActionService));
                            break;

                        case Keys.Insert: // Execute the First Create Service in Acctions Pattern.
                            e.Handled = true;
                            OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteFirstCreateActionService));
                            break;

                        case Keys.Enter: // Selecte Instance.
                            e.Handled = true;
                            OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteSelectInstance));
                            break;
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
		}
		/// <summary>
		/// Select Instance with double click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleDataGridViewITDoubleClick(object sender, EventArgs e)
		{
			// If it is the header row, do nothing
			if (((MouseEventArgs)e).Y < mDataGridViewIT.ColumnHeadersHeight)
			{
				return;
			}


            // Raise de double click, if it is handled, skip next step
            ExecuteCommandEventArgs lEventAgs = new ExecuteCommandEventArgs(Keys.NoName);
            OnExecuteCommand(lEventAgs);
            if (lEventAgs.Handled)
            {
                return;
            }

			OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteSelectInstance));
		}
		/// <summary>
		/// Actions related with the Scroll event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleDataGridViewITScroll(object sender, ScrollEventArgs e)
		{
			if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
			{
				// If the last row is visible, launch the MoreBlocks event.
				if (e.NewValue < e.OldValue)
				{
					return;
				}
				try
				{
					if ((mDataGridViewIT.Rows[mDataGridViewIT.RowCount - 2].State & DataGridViewElementStates.Displayed) == DataGridViewElementStates.Displayed)
					{
						OnMoreBlocks(new TriggerEventArgs());
					}
				}
				catch
				{
				}
			}
		}
		/// <summary>
		/// Contextual Menu Refresh population of instances.
		/// </summary>
		/// <param name="sender">Control reference that raise the click event.</param>
		/// <param name="e">Event arguments.</param>
		private void HandleRefreshClick(object sender, EventArgs e)
		{
			// Refresh grid data
			List<Oid> lSelectedOids = GetSelectedOIDs();
			OnExecuteCommand(new ExecuteCommandRefreshEventArgs(lSelectedOids));
		}
		/// <summary>
		/// Actions related with Export to Excel click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleExportToExcel(object sender, EventArgs e)
		{
			if (mDataGridViewIT != null)
			{
				Excel.ExportToExcel(mDataGridViewIT);
			}
		}
		/// <summary>
		/// Actions related with Export to Word click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleExportToWord(object sender, EventArgs e)
		{
		}
		/// <summary>
		/// Contextual Menu Retrieve All Instances.
		/// </summary>
		/// <param name="sender">Control reference that raise the click event.</param>
		/// <param name="e">Event arguments.</param>
		private void HandleRetrieveAllClick(object sender, EventArgs e)
		{
			OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteRetriveAll));
		}
		/// <summary>
		/// Handle the Grid editing errors
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleDataDridViewITDataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			if (mEnabledDataErrorReporting)
			{
				MessageBox.Show(e.Exception.Message);
			}
		}
		/// <summary>
		/// Handle the Cell Begin Edit event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleDataGridViewITCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{
			mEnabledDataErrorReporting = true;
			// Keep the current cell value
			mPreviousCellValue = mDataGridViewIT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

			// If the selected column is a Decimal, enables the EditControl KeyPress management
			if (mDataGridViewIT.Columns[e.ColumnIndex].ValueType.Name.Equals("Decimal"))
			{
				mManageEditControlKeyPress = true;
			}
			else
			{
				mManageEditControlKeyPress = false;
			}
		}
		/// <summary>
		/// Handles the Editing control showing event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleDataGridViewITEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			// If the flag is up, suscribe to the KeyPress event
			if (mManageEditControlKeyPress)
			{
				if (e.Control != null)
				{
					e.Control.KeyPress += new KeyPressEventHandler(HandleEditControlKeyPress);
				}
			}
		}
		/// <summary>
		/// Handles the KeyPress event from the Cell editor.
		/// Only for decimal data types
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleEditControlKeyPress(object sender, KeyPressEventArgs e)
		{
			// If the column data type is numeric with decimals, use the , or . as decimal separator
			if (e.KeyChar == ',' || e.KeyChar == '.')
			{
				e.KeyChar = CultureManager.Culture.NumberFormat.NumberDecimalSeparator[0];
				return;
			}
		}
		/// <summary>
		/// Handles the End Edit in Cells
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleDataGridViewITCellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			mEnabledDataErrorReporting = false;
			// Compare the current value with the previous one.
			object currentValue = mDataGridViewIT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			if (currentValue == null)
			{
				if (mPreviousCellValue == null)
				{
					return;
				}
			}
			else
			{
				if (currentValue.Equals(mPreviousCellValue))
				{
					return;
				}
			}

			DataRowView rowView = mDataGridViewIT.Rows[e.RowIndex].DataBoundItem as DataRowView;
			if (rowView != null)
			{
				rowView.EndEdit();
			}

			// Raise the Execute command method in order to notify the editing
			OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ValuesHasBeenModified));
		}
        /// <summary>
        /// Handles the Shown form event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleContainerForm_Shown(object sender, EventArgs e)
        {
            // Enable to raise the grid selection change event, after the Form is shown, in order to avoid 
            //  unnecessary SelectionChange events
            mRaiseEventCurrentCellChanged = true;

            // If there is any row selected, launch the event
            if (mDataGridViewIT.SelectedRows.Count > 0)
            {
                ProcessDataGridITCurrentCellChanged(null, null);
            }
        }
		#endregion Event Handlers

		#region Event Raisers
		/// <summary>
		/// Raises the MoreBlocks event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnMoreBlocks(TriggerEventArgs eventArgs)
		{
			EventHandler<TriggerEventArgs> handler = MoreBlocks;

			if (handler != null)
			{
				handler(this, new TriggerEventArgs());
			}
		}
		/// <summary>
		/// Raises the ExecuteCommand event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnExecuteCommand(ExecuteCommandEventArgs eventArgs)
		{
			EventHandler<ExecuteCommandEventArgs> handler = ExecuteCommand;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the SelectionChanged event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnSelectionChanged(SelectedChangedEventArgs eventArgs)
		{
			EventHandler<SelectedChangedEventArgs> handler = SelectionChanged;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		#endregion Event Raisers

		#region Methods
		/// <summary>
		/// Assign default text for boolean values
		/// Three texts are added to the list. Given order is important, related properties will use that order
		/// </summary>
		private void AssignDefaultTextForBooleans()
		{
			string boolNull = CultureManager.TranslateString(LanguageConstantKeys.L_BOOL_NULL, LanguageConstantValues.L_BOOL_NULL);
			mBooleanValues.Add(new KeyValuePair<object, string>(DBNull.Value, boolNull));
			string boolTrue = CultureManager.TranslateString(LanguageConstantKeys.L_BOOL_TRUE, LanguageConstantValues.L_BOOL_TRUE);
			mBooleanValues.Add(new KeyValuePair<object, string>(true, boolTrue));
			string boolFalse = CultureManager.TranslateString(LanguageConstantKeys.L_BOOL_FALSE, LanguageConstantValues.L_BOOL_FALSE);
			mBooleanValues.Add(new KeyValuePair<object, string>(false, boolFalse));
		}
		/// <summary>
		/// Sets message (empty function due to compatibility of the interface).
		/// </summary>
		/// <param name="message">Message.</param>
		public void SetMessage(string message)
		{
		}
		/// <summary>
		/// Gets an specific cell, specifying row and column.
		/// </summary>
		/// <param name="rowNumber">Row where is the cell.</param>
		/// <param name="columnName">Column where is the cell.</param>
		/// <returns>The cell specified.</returns>
		public object GetCell(int rowNumber, string columnName)
		{
			DataView lDataView = mDataGridViewIT.DataSource as DataView;
			if (lDataView == null)
			{
				return null;
			}
			return lDataView[rowNumber][columnName];
		}
		/// <summary>
		/// Gets or sets the number of the first row on the DataGrid.
		/// </summary>
		public int FirstVisibleRow
		{
			get
			{
				VScrollBar lScrollBar = null;

				// Get vertical scroll control.
				foreach (object lObject in mDataGridViewIT.Controls)
				{
					lScrollBar = lObject as VScrollBar;
					if (lScrollBar != null)
					{
						break;
					}
				}

				// Check if need more pages (if last row is showed).
				if (lScrollBar != null)
				{
					return lScrollBar.Value;
				}
				return 0;
			}
			set
			{
				VScrollBar lScrollBar = null;

				// Get vertical scroll control.
				foreach (object lObject in mDataGridViewIT.Controls)
				{
					lScrollBar = lObject as VScrollBar;
					if (lScrollBar != null)
					{
						break;
					}
				}

				// Check if need more pages (if last row is showed).
				if (lScrollBar != null)
				{
					lScrollBar.Value = value;
				}
			}
		}
		/// <summary>
		/// Gets the selected cell, specifying the selected row and the column name.
		/// </summary>
		/// <param name="selectedRowNumber">Selected row.</param>
		/// <param name="columnName">Column name.</param>
		/// <returns>Selected cell.</returns>
		public object GetSelectedCell(int selectedRowNumber, string columnName)
		{
			if (mDataGridViewIT.SelectedRows.Count == 0)
			{
				return null;
			}

			DataView lDataView = mDataGridViewIT.DataSource as DataView;
			if (lDataView == null)
			{
				return null;
			}

			int lRow = 0;
			lRow = mDataGridViewIT.SelectedRows[selectedRowNumber].Index;
			return lDataView[lRow][columnName].ToString();
		}
		/// <summary>
		/// Shows specified data on DataGrid and select the rows
		/// </summary>
		/// <param name="data"></param>
		/// <param name="selectedOids"></param>
		public void ShowData(DataTable data, List<Oid> selectedOids)
		{
			if (data == null)
			{
				return;
			}
            
            // Previous selection
            List<Oid> lPreviousSelection = Values;

			mDataGridViewIT.SuspendLayout();
			mDataGridViewIT.AutoGenerateColumns = false;
			// To avoid innecesary SelectionChanged events.
            bool lPreviousEventFlagValue = mRaiseEventCurrentCellChanged;
			mRaiseEventCurrentCellChanged = false;
			mDataGridViewIT.DataSource = data.DefaultView;
			mDataGridViewIT.ClearSelection();
			mDataGridViewIT.ResumeLayout();
			SelectRowsByOid(selectedOids);
            mRaiseEventCurrentCellChanged = lPreviousEventFlagValue;

            // If selection has change, raise the event
            if (!UtilFunctions.OidListEquals(lPreviousSelection, selectedOids))
            {
                ProcessDataGridITCurrentCellChanged(null, null);
            }
		}
		/// <summary>
		/// Adds DisplaySet item (empty function due to compatibility of the interface).
		/// </summary>
		/// <param name="name">Item name.</param>
		/// <param name="alias">Item alias.</param>
		/// <param name="idxml">XML item identifier.</param>
		/// <param name="modelType">ModelType.</param>
		/// <param name="agents">Agents.</param>
		public void AddDisplaySetItem(string name, string alias, string idxml, ModelType modelType, List<KeyValuePair<object, string>> definedSelectionOptions, string[] agents, int width)
		{
		}
		/// <summary>
		/// Gets the Oid of an specified row.
		/// </summary>
		/// <param name="rowIndex">Specified row.</param>
		/// <returns>Selected row Oid.</returns>
		public Oid GetOid(int rowIndex)
		{
			DataView lDataView = mDataGridViewIT.DataSource as DataView;
			if (lDataView == null)
			{
				return null;
			}
			return Adaptor.ServerConnection.GetOid(lDataView.Table, lDataView[rowIndex].Row);

		}
		/// <summary>
		/// Returns the Oid list from the selected rows
		/// </summary>
		/// <returns>List of Oids.</returns>
		private List<Oid> GetSelectedOIDs()
		{
			try
			{
				// If no selection, return null
				if (mDataGridViewIT.SelectedRows.Count == 0 || mDataGridViewIT.DataSource == null || mDataGridViewIT.Rows.Count == 0)
				{
					return null;
				}

				Oids.Oid oid;
				DataView table = (DataView) mDataGridViewIT.DataSource;
				List<Oid> lOids = new List<Oid>();

				foreach (DataGridViewRow row in mDataGridViewIT.SelectedRows)
				{
					if (row.DataBoundItem != null)
					{
						DataRowView rowView = row.DataBoundItem as DataRowView;
						if (rowView != null)
						{
							oid = Adaptor.ServerConnection.GetOid(table.Table, rowView.Row);
							if (mDataGridViewIT.SelectedRows.Count == 1)
							{
								oid.ExtraInfo = table.Table.Clone();
								oid.ExtraInfo.LoadDataRow(rowView.Row.ItemArray, true);
							}
							lOids.Add(oid);
						}
					}
				}
				return lOids;
			}
			catch
			{
			}
			return null;
		}
		/// <summary>
		/// Selects the rows of the dataview that represents the OID included in the oids argument
		/// </summary>
		/// <param name="oids">Selected OIDS List</param>
		private void SelectRowsByOid(List<Oid> oids)
		{
			mRaiseEventCurrentCellChanged = false;
			mDataGridViewIT.ClearSelection();
			if (oids == null || oids.Count == 0)
			{
				OnSelectionChanged(new SelectedChangedEventArgs(null));
				mRaiseEventCurrentCellChanged = true;
				return;
			}

			foreach (DataGridViewRow row in mDataGridViewIT.Rows)
			{
				if (row.DataBoundItem != null)
				{
					DataRowView rowView = row.DataBoundItem as DataRowView;
					if (rowView != null)
					{
						Oid loid = Adaptor.ServerConnection.GetOid(((DataView)this.mDataGridViewIT.DataSource).Table, rowView.Row);
						foreach (Oid selectedOid in oids)
						{
							if (selectedOid.Equals(loid))
							{
								row.Selected = true;
								if (oids.Count == 1)
								{
									mDataGridViewIT.CurrentCell = row.Cells[0];
								}
								mDataGridViewIT.Focus();
							}
						}
					}
				}
			}

			// Obtain the keys to enable or disable the actions and navigations.
			List<string> actionsKeys = new List<string>();
			List<string> navigationsKeys = new List<string>();
			GetEnabledActionsNavigationsKeys(actionsKeys, navigationsKeys);
			// Raise the event.
			OnSelectionChanged(new SelectedChangedEventArgs(GetSelectedOIDs(), actionsKeys, navigationsKeys));

			mRaiseEventCurrentCellChanged = true;
		}
		/// <summary>
		/// Sets format to a column. If the column doesn't exist then creates a new one
		/// </summary>
		/// <param name="name"></param>
		/// <param name="alias"></param>
		/// <param name="modelType"></param>
		/// <param name="definedSelectionOptions"></param>
		/// <param name="width"></param>
		public void SetFormatDisplaySetItem(string name, string alias, ModelType modelType, List<KeyValuePair<object, string>> definedSelectionOptions, int width, bool editable, bool allowsNullInEditMode)
		{
			string lColumnName = name;
			int counter = 1;
			while (Exist(lColumnName))
			{
				lColumnName = name + counter.ToString();
				counter++;
			}

			// Add a new column to the Grid
			DataGridViewColumn lColumn = null;

			// For boolean data type and editable ones, use the checkbox column
			if ((definedSelectionOptions == null) && (modelType == ModelType.Bool) && (editable || ShowBooleanAsCheckBox))
			{
				DataGridViewCheckBoxColumn lCheckBoxColumn = new DataGridViewCheckBoxColumn();
				lCheckBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;
				if (editable && !allowsNullInEditMode)
				{
					lCheckBoxColumn.ThreeState = false;
				}
				else
				{
					lCheckBoxColumn.ThreeState = true;
				}

				lColumn = lCheckBoxColumn;
			}
			else
			{
				if (definedSelectionOptions != null || modelType == ModelType.Bool)
				{
					DataGridViewComboBoxColumn lComboBoxColumn = new DataGridViewComboBoxColumn();
					lComboBoxColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
					if (definedSelectionOptions != null)
					{
						lComboBoxColumn.DataSource = definedSelectionOptions;
					}
					else
					{
						lComboBoxColumn.DataSource = mBooleanValues;
					}
					lComboBoxColumn.DisplayMember = "Value";
					lComboBoxColumn.ValueMember = "Key";
					lComboBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;
					lColumn = lComboBoxColumn;
				}
				else
				{
					lColumn = new DataGridViewTextBoxColumn();
					switch (modelType)
					{
						case ModelType.Time:
						case ModelType.Autonumeric:
						case ModelType.Date:
						case ModelType.DateTime:
						case ModelType.Int:
						case ModelType.Nat:
						case ModelType.Real:
							lColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
							break;
					}
					lColumn.DefaultCellStyle.Format = DefaultFormats.GetDefaultDisplayMask(modelType);
				}
			}
			// Assign the width
			if (width != 0)
			{
				if ((modelType == ModelType.Bool) && (definedSelectionOptions != null))
				{
					lColumn.Width = DefaultFormats.ColWidthBoolDefinedSelection;
				}
				else
				{
					lColumn.Width = width;
				}
			}

			lColumn.DataPropertyName = name;
			lColumn.Name = lColumnName;
			if (alias == null)
			{
				alias = string.Empty;
			}
			lColumn.HeaderText = (alias.Length == 0 ? name : alias);

			// IF the column is editable ...
			if (editable)
			{
				lColumn.ReadOnly = false;
				mDataGridViewIT.ReadOnly = false;
				mDataGridViewIT.EditMode = DataGridViewEditMode.EditOnEnter;
				mDataGridViewIT.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
				mDataGridViewIT.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
			}
			else
			{
				lColumn.ReadOnly = true;
			}

			mDataGridViewIT.SuspendLayout();
			mDataGridViewIT.Columns.Add(lColumn);
			mDataGridViewIT.ResumeLayout();
		}
		private bool Exist( string attributeName)
		{
			foreach (DataGridViewColumn lColumn in mDataGridViewIT.Columns)
			{
				if (string.Compare(lColumn.DataPropertyName, attributeName, true) == 0)
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Selects the row indicated by the parameter (0 index).
		/// </summary>
		/// <param name="index">Index of the row to be updated.</param>
		public void SelectRow(int index)
		{
			try
			{
				mDataGridViewIT.ClearSelection();
				mDataGridViewIT.Rows[index].Selected = true;
			}
			catch
			{
			}
		}
		/// <summary>
		/// Gets the index of the row where the Oid is located.
		/// </summary>
		/// <param name="oid">Oid object to be searched.</param>
		/// <returns>An integer indicating the index of the row where the Oid is located. -1 in case the Oid is not found.</returns>
		public int GetRowFromOid(Oid oid)
		{
			try
			{
				if (mDataGridViewIT.DataSource == null || mDataGridViewIT.Rows.Count == 0)
				{
					return -1;
				}

				Oid oidRow;
				DataView lDataView = mDataGridViewIT.DataSource as DataView;
				if (lDataView == null)
				{
					return -1;
				}

				foreach (DataGridViewRow row in mDataGridViewIT.Rows)
				{
					oidRow = GetOid(row.Index);
					if (oidRow.Equals(oid))
					{
						return row.Index;
					}
				}
			}
			catch
			{
				return -1;
			}
			return -1;
		}
		/// <summary>
		/// Removes all the DisplaySet items
		/// </summary>
		public void RemoveAllDisplaySetItems()
		{
			mDataGridViewIT.SuspendLayout();
			while (mDataGridViewIT.Columns.Count > 0)
			{
				mDataGridViewIT.Columns.RemoveAt(0);
			}
			mDataGridViewIT.ResumeLayout();
		}
		/// <summary>
		/// Returns in a list tha width of every visible column
		/// </summary>
		/// <returns></returns>
		public List<int> GetColumnsWidth()
		{
			List<int> columnsWidth = new List<int>();

			foreach (DataGridViewColumn col in mDataGridViewIT.Columns)
			{
				if (col.Visible)
				{
					columnsWidth.Add(col.Width);
				}
			}

			return columnsWidth;
		}
		/// <summary>
		/// Returns the modified rows
		/// </summary>
		/// <returns></returns>
		public DataTable GetModifiedRows()
		{
			List<List<KeyValuePair<string, object>>> list = new List<List<KeyValuePair<string, object>>>();

			DataView table = (DataView)mDataGridViewIT.DataSource;
			DataTable modifiedRows = table.Table.Clone();
			foreach (DataGridViewRow row in mDataGridViewIT.Rows)
			{
				if (row.DataBoundItem != null)
				{
					DataRowView rowView = row.DataBoundItem as DataRowView;
					if (rowView != null && rowView.Row.RowState == DataRowState.Modified)
					{
						modifiedRows.LoadDataRow(rowView.Row.ItemArray, true);
					}
				}
			}

			return modifiedRows;
		}
		/// <summary>
		/// Add to the received list the enabled keys for Actions and Navigations.
		/// </summary>
		/// <param name="actionsKeys">Actions keys.</param>
		/// <param name="navigationsKeys">Navigations keys.</param>
		private void GetEnabledActionsNavigationsKeys(List<string> actionsKeys, List<string> navigationsKeys)
		{
			try
			{
				// If no selection, return null
				if (mDataGridViewIT.SelectedRows.Count == 0 || mDataGridViewIT.DataSource == null || mDataGridViewIT.Rows.Count == 0)
				{
					return;
				}

				DataView table = mDataGridViewIT.DataSource as DataView;

                if (table == null || !table.Table.Columns.Contains(Constants.ACTIONS_ACTIVATION_COLUMN_NAME))
                {
                    return;
                }

				foreach (DataGridViewRow row in mDataGridViewIT.SelectedRows)
				{
					DataRowView lRow = table[row.Index];

					if (row.DataBoundItem != null)
					{
						actionsKeys.Add((string)lRow[Constants.ACTIONS_ACTIVATION_COLUMN_NAME]);
						navigationsKeys.Add((string)lRow[Constants.NAVIGATIONS_ACTIVATION_COLUMN_NAME]);
					}
				}
			}
			catch
			{
			}
		}
		#endregion Methods
	}
}
