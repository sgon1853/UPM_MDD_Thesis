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
	/// Presentation abstraction of the .NET ListView control.
	/// </summary>
	public class ListViewPresentation: IDisplaySetPresentation
	{
		#region Members
		private ListView mListViewIT;
		private List<ModelType> mTypeList = new List<ModelType>();
		private List<Oid> mValue = new List<Oid>();
		private List<List<KeyValuePair<object, string>>> mDefinedSelectionOptions = new List<List<KeyValuePair<object, string>>>();
		/// <summary>
		/// Texts to be shown for boolean values without defined selection.
		/// </summary>
		private List<KeyValuePair<object, string>> mBooleanValues = new List<KeyValuePair<object, string>>();
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'ListViewPresentation'.
		/// </summary>
		/// <param name="listView">ListView instance.</param>
		public ListViewPresentation(ListView listView,
			ToolStripMenuItem exportToExcel,
			ToolStripMenuItem exportToWord,
			ToolStripMenuItem refresh,
			ToolStripMenuItem help,
			ToolStripMenuItem options,
			ToolStripMenuItem navigations)
		{
			mListViewIT = listView;
			if (exportToExcel != null)
			{
				exportToExcel.Text = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_EXPORT_TO_EXCEL, LanguageConstantValues.L_POP_UP_MENU_EXPORT_TO_EXCEL);
				exportToExcel.Click += new EventHandler(HandleExportToExcelClick);
			}

			if (exportToWord != null)
			{
				exportToWord.Text = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_EXPORT_TO_WORD, LanguageConstantValues.L_POP_UP_MENU_EXPORT_TO_WORD);
				exportToWord.Click += new EventHandler(HandleExportToWordClick);
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
			AssignDefaultTextForBooleans();
		}
		#endregion Constructors

		#region Properties
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
				return mListViewIT.Visible;
			}
			set
			{
				mListViewIT.Visible = value;
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
				mBooleanValues.Insert(0, new KeyValuePair<object, string>(null, value));
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
		/// Gets or sets Enabled property.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return mListViewIT.Enabled;
			}
			set
			{
				mListViewIT.Enabled = value;
			}
		}
		/// <summary>
		/// Gets Values.
		/// </summary>
		public List<Oid> Values
		{
			get
			{
				return mValue;
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
		/// No commands asociated.
		/// </summary>
		public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Handles the Export to Excel trigger
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void HandleExportToExcelClick(object sender, EventArgs e)
		{
			if (mListViewIT != null)
			{
				Excel.ExportToExcel(mListViewIT);
			}
		}
		/// <summary>
		/// Handles the Export to Word trigger
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void HandleExportToWordClick(object sender, EventArgs e)
		{
		}
		/// <summary>
		/// Contextual Menu Refresh population of instances.
		/// </summary>
		/// <param name="sender">Control reference that raise the click event.</param>
		/// <param name="e">Event arguments.</param>
		void HandleRefreshClick(object sender, EventArgs e)
		{
			OnExecuteCommand(new ExecuteCommandRefreshEventArgs(null));
		}
		#endregion Event Handlers

		#region Event Raisers
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
		/// Removes all the DisplaySet items
		/// </summary>
		public void RemoveAllDisplaySetItems()
		{
			while (mListViewIT.Items.Count > 0)
			{
				mListViewIT.Items.RemoveAt(0);
			}
			mTypeList.RemoveRange(0, mTypeList.Count);
			mDefinedSelectionOptions.RemoveRange(0, mTypeList.Count);
		}
		/// <summary>
		/// Returns in a list tha width of every visible column
		/// </summary>
		/// <returns></returns>
		public List<int> GetColumnsWidth()
		{
			return null;
		}
		/// <summary>
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
		/// Shows specified data on ListView.
		/// </summary>
		/// <param name="data">Data to show.</param>
		public void ShowData(DataTable data, List<Oid> selectedOids)
		{
			// Clear Instance selector.
			mValue.Clear();
			// Clear viewer.
			CleanData();

			if ((data != null) && (data.Rows.Count > 0))
			{
				// Load from the DataTable.
				List<DataColumn> displaySetColumns = Adaptor.ServerConnection.GetDisplaySetColumns(data);

				DataRow row = data.Rows[0];

				// Store the Oid
				mValue.Add(Adaptor.ServerConnection.GetLastOid(data));
				// Show data in the ListView
				int i = 0;
				foreach (DataColumn column in displaySetColumns)
				{
					// Do not continue, if displaySetColumns contains more columns
					// than elements defined in the ListView control.
					if (i < mListViewIT.Items.Count)
					{

						if (mDefinedSelectionOptions[i] != null)
						{
							for (int j = 0; j < mDefinedSelectionOptions[i].Count; j++)
							{
								if ((mDefinedSelectionOptions[i][j].Key != null) && (mDefinedSelectionOptions[i][j].Key.ToString() == row.ItemArray[column.Ordinal].ToString()))
								{
									mListViewIT.Items[i].SubItems[1].Text = mDefinedSelectionOptions[i][j].Value;
									break;
								}
							}
						}
						else
						{
							mListViewIT.Items[i].SubItems[1].Text = DefaultFormats.ApplyDisplayFormat(row.ItemArray[column.Ordinal], mTypeList[i]);
						}
					}
					i++;
				}
			}

			if (SelectionChanged != null)
			{
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
				SelectionChanged(this, new SelectedChangedEventArgs(mValue, actionsKeys, navigationsKeys));
			}
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
			if (Exist(name))
			{
				return;
			}

			//Add items form DisplaySet
			string[] lAgents = Logics.Agents.All;
			this.AddDisplaySetItem(name, alias, "", modelType, definedSelectionOptions, lAgents, width);

			//Add a new column to the ListView
			int index = mListViewIT.Items.IndexOfKey(name);
			mListViewIT.Items[index].Text = alias;
			mTypeList[index] = modelType;
			if (modelType == ModelType.Bool && definedSelectionOptions == null)
			{
				mDefinedSelectionOptions[index] = mBooleanValues;
			}
			else
			{
				mDefinedSelectionOptions[index] = definedSelectionOptions;
			}
		}
		private bool Exist(string attributeName)
		{
			foreach (ListViewItem item in mListViewIT.Items)
			{
				if (string.Compare(item.Name, attributeName, true) == 0)
				{
					return true;
				}
			}
			return false;
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
				foreach (ListViewItem item in mListViewIT.Items)
				{
					item.SubItems[1].Text = string.Empty;
				}
			}
			catch
			{
			}
		}
		/// <summary>
		/// Adds an attribute to the display set
		/// </summary>
		/// <param name="name"></param>
		/// <param name="alias"></param>
		/// <param name="idxml"></param>
		/// <param name="modelType"></param>
		/// <param name="definedSelectionOptions"></param>
		/// <param name="agents"></param>
		/// <param name="width"></param>
		public void AddDisplaySetItem(string name, string alias, string idxml, ModelType modelType, List<KeyValuePair<object, string>> definedSelectionOptions, string[] agents, int width)
		{
			ListViewItem item = new ListViewItem(new string[] { alias, "" });
			item.Name = name;
			mListViewIT.Items.Add(item);
			mTypeList.Add(modelType);
			if (modelType == ModelType.Bool && definedSelectionOptions == null)
			{
				mDefinedSelectionOptions.Add(mBooleanValues);
			}
			else
			{
				mDefinedSelectionOptions.Add(definedSelectionOptions);
			}
		}
		/// <summary>
		/// Returns the modified rows.
		/// Empty implementation
		/// </summary>
		/// <returns></returns>
		public DataTable GetModifiedRows()
		{
			return null;
		}
		#endregion Methods
	}
}
