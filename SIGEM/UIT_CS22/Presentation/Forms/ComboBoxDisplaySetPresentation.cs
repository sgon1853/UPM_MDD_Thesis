// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SIGEM.Client.Controllers;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of the .NET ComboBox control for DisplaySets.
	/// </summary>
	public class ComboBoxDisplaySetPresentation : EditorPresentation, IDisplaySetPresentation, IEditorPresentation
	{
		#region Members
		/// <summary>
		/// .NET ComboBox instance reference.
		/// </summary>
		protected ComboBox mComboBoxIT;
		/// <summary>
		/// Data types for elements of Oid.
		/// </summary>
		private List<ModelType> mTypeList = new List<ModelType>();
		/// <summary>
		/// Defined selection list for every attribute in the display set
		/// </summary>
		private List<List<KeyValuePair<object, string>>> mDefinedSelectionOptions = new List<List<KeyValuePair<object, string>>>();
		/// <summary>
		/// Error Provider for validating data.
		/// </summary>
		private ErrorProvider mErrorProvider;
		/// <summary>
		/// If the control performs the validation.
		/// </summary>
		private bool mValidateData = true;
		/// <summary>
		/// If the control allows null values.
		/// </summary>
		private bool mNullAllowed = true;
		/// <summary>
		/// Texts to be shown for boolean values.
		/// </summary>
		private List<KeyValuePair<object, string>> mBooleanValues = new List<KeyValuePair<object, string>>();
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'ComboBoxDisplaySetPresentation'.
		/// </summary>
		/// <param name="comboBox">ComboBox to display.</param>
		public ComboBoxDisplaySetPresentation(ComboBox comboBox)
			:base()
		{
			mComboBoxIT = comboBox;
			mComboBoxIT.Sorted = false;

			if (mComboBoxIT != null)
			{
				// Create and configure the ErrorProvider control.
				mErrorProvider = new ErrorProvider();
				mErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
				mComboBoxIT.SelectedIndexChanged += new EventHandler(HandleComboBoxITSelectedIndexChanged);
				mComboBoxIT.Enter += new EventHandler(HandleComboBoxITEnter);
				mComboBoxIT.Leave += new EventHandler(HandleComboBoxITLeave);
				mComboBoxIT.TextChanged += new EventHandler(HandleComboBoxITTextChanged);
				mComboBoxIT.EnabledChanged += new EventHandler(HandleComboBoxITEnabledChanged);
				mComboBoxIT.KeyDown += new KeyEventHandler(HandleComboBoxITKeyDown);
			}
			AssignDefaultTextForBooleans();
		}
		/// <summary>
		/// Initializes a new instance of 'ComboBoxDisplaySetPresentation'.
		/// </summary>
		/// <param name="comboBox">ComboBox to display.</param>
		/// <param name="validateData">True if the presentation has to validate the data</param>
		public ComboBoxDisplaySetPresentation(ComboBox comboBox, bool validateData)
			: this(comboBox)
		{
			mValidateData = validateData;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets Visible property.
		/// </summary>
		public bool Visible
		{
			get
			{
				return mComboBoxIT.Visible;
			}
			set
			{
				mComboBoxIT.Visible = value;
			}
		}
		/// <summary>
		/// Gets or sets Enabled property.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return mComboBoxIT.Enabled;
			}
			set
			{
				mComboBoxIT.Enabled = value;
			}
		}
		/// <summary>
		/// Display texts for Null boolean value.
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
		/// <summary>
		/// Display texts for True boolean value.
		/// </summary>
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
		/// <summary>
		/// Display texts for False boolean value.
		/// </summary>
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
		/// Model datatype.
		/// </summary>
		public ModelType DataType
		{
			get
			{
				return ModelType.Oid;
			}
			set
			{

			}
		}
		/// <summary>
		/// Get and Sets the Focus to the control.
		/// </summary>
		public bool Focused
		{
			get
			{
				return mComboBoxIT.Focused;
			}
			set
			{
				if (value == true)
				{
                    ActivateParentTabPage(mComboBoxIT);
                    mComboBoxIT.Focus();
                }
				else if (mComboBoxIT.Parent != null)
				{
					mComboBoxIT.Parent.Focus();
				}
			}
		}

		/// <summary>
		/// Allows null property
		/// </summary>
		public bool NullAllowed
		{
			get
			{
				return mNullAllowed;
			}
			set
			{
				mNullAllowed = value;

				//Set read only mode.
				if (ReadOnly)
				{
					return;
				}

				if (!mNullAllowed)
				{
					mComboBoxIT.BackColor = System.Drawing.SystemColors.Info;
				}
				else
				{
					// Set the back color value.
					if (Enabled)
					{
						mComboBoxIT.BackColor = System.Drawing.SystemColors.Window;
					}
					else
					{
						mComboBoxIT.BackColor = System.Drawing.SystemColors.Control;
					}

					// The Error provider is cleared.
					if ((this.Value == null) && (mErrorProvider != null))
					{
						mErrorProvider.Clear();
					}
				}
			}
		}
		/// <summary>
		/// Max length property.
		/// </summary>
		public int MaxLength
		{
			get
			{
				return 0;
			}
			set
			{

			}
		}
		/// <summary>
		/// Gets or sets Selected Oids.
		/// </summary>
		public List<Oid> Values
		{
			get
			{
				// Return the list of selected Oids.
				return GetSelectedOIDs();
			}
			set
			{
				// Set the Oid.
				// If value is null or contains more than one, do not select anyone.
				SetSelectedOids(value);
			}
		}
		/// <summary>
		/// Value implementation of value for ILabelPresentation, get the SelectedOIDs(), and set one DataTable.
		/// </summary>
		public object Value
		{
			get
			{
				return Values;
			}
			set
			{
				Values = value as List<Oid>;
				PreviousValue = mComboBoxIT.Text;
			}
		}
		/// <summary>
		/// Gets or sets the Read Only property
		/// </summary>
		public bool ReadOnly
		{
			get
			{
				return false;
			}
			set
			{
				Enabled = !value;
			}
		}
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when selected index of the ComboBox is changed.
		/// </summary>
		public event EventHandler<SelectedChangedEventArgs>  SelectionChanged;
		/// <summary>
		/// Occurs when Enabled property is changed.
		/// </summary>
		public event EventHandler<EnabledChangedEventArgs> EnableChanged;
		/// <summary>
		/// Occurs when the control get the Focus
		/// </summary>
		public event EventHandler<GotFocusEventArgs> GotFocus;
		/// <summary>
		/// Occurs when the control lost the Focus
		/// </summary>
		public event EventHandler<LostFocusEventArgs> LostFocus;
		/// <summary>
		/// Occurs when the display value changed
		/// </summary>
		public event EventHandler<ValueChangedEventArgs> ValueChanged;
		/// <summary>
		/// Never raised.
		/// </summary>
		public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Actions related with the EnabledChanged event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleComboBoxITEnabledChanged(object sender, EventArgs e)
		{
			OnEnabledChanged(new EnabledChangedEventArgs());
		}
		/// <summary>
		/// Actions related with the TextChanged event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleComboBoxITTextChanged(object sender, EventArgs e)
		{
			if (CheckValueChange(mComboBoxIT.Text))
			{
				// Value changed event
				OnValueChanged(new ValueChangedEventArgs());
			}
		}
		/// <summary>
		/// Actions related with the Leave event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleComboBoxITLeave(object sender, EventArgs e)
		{
			OnLostFocus(new LostFocusEventArgs());
		}
		/// <summary>
		/// Actions related with the Enter event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleComboBoxITEnter(object sender, EventArgs e)
		{
			OnGotFocus(new GotFocusEventArgs());
		}
		/// <summary>
		/// Suscriber to KeyDown event
		/// </summary>
		/// <param name="sender">Control that raise the event.</param>
		/// <param name="e">Key pressed.</param>
		private void HandleComboBoxITKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteRefresh));
			}
		}
		#endregion Event Handlers

		#region Event Raisers
		/// <summary>
		/// Raises the EnabledChanged Event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnEnabledChanged(EnabledChangedEventArgs eventArgs)
		{
			//When the enabled event change, the Error porvider is clean.
			if (mErrorProvider != null)
			{
				mErrorProvider.Clear();
			}

			EventHandler<EnabledChangedEventArgs> handler = EnableChanged;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}

		protected virtual void OnValueChanged(ValueChangedEventArgs eventArgs)
		{
			// Clear the ErrorProvider control.
			if (mErrorProvider != null)
			{
				mErrorProvider.Clear();
			}

			// Raise the change value event if the control has not the focus.
			// If not, wait until it lose the focus.
			if ( mComboBoxIT.Focused)
			{
				return;
			}

			// Search in the comboBox list when it has no selected item but it has text.
			if (mComboBoxIT.SelectedItem == null && !string.IsNullOrEmpty(mComboBoxIT.Text))
			{
				// Find the edited text in the combo list.
				int lItemPosition = this.mComboBoxIT.FindStringExact(this.mComboBoxIT.Text);
				// The text is in the list.
				if (lItemPosition != -1)
				{
					// Set the position.
					mComboBoxIT.SelectedIndex = lItemPosition;
				}
			}

			EventHandler<ValueChangedEventArgs> handler = ValueChanged;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Throw LostFocus event
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnLostFocus(LostFocusEventArgs eventArgs)
		{
			//Find edited text in combo in list
			int lSearchItemInCombo = this.mComboBoxIT.FindStringExact(this.mComboBoxIT.Text);
			string lPreloadErrorMessage = CultureManager.TranslateString(LanguageConstantKeys.L_VALIDATION_PRELOAD_ERROR, LanguageConstantValues.L_VALIDATION_PRELOAD_ERROR);
			//Locate error message inside combo control
			this.mErrorProvider.SetIconPadding(this.mComboBoxIT, -35);
			// Do the validation
			if (mValidateData)
			{
				//Shows errorProvider with new validation message
				if (lSearchItemInCombo == -1 && !string.IsNullOrEmpty(this.mComboBoxIT.Text))
				{
					mErrorProvider.SetError(this.mComboBoxIT, lPreloadErrorMessage);
					return;
				}

				//Delete any message when combo is empty
				if (string.IsNullOrEmpty(this.mComboBoxIT.Text))
				{
					//this.Validate(lErrorMessage);
					mErrorProvider.Clear();
				}
				// Before leaving the control, check if the data belongs to the specified type.
				string lErrorMessage = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR, LanguageConstantValues.L_ERROR);
				if ((this.mComboBoxIT.SelectedIndex  != -1) && (!this.Validate(lErrorMessage )))
				{
					return;
				}
			}

			if (CheckValueChange(mComboBoxIT.Text))
			{
				// Value changed event
				OnValueChanged(new ValueChangedEventArgs());
			}

			EventHandler<LostFocusEventArgs> handler = LostFocus;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the Got Focus event
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnGotFocus(GotFocusEventArgs eventArgs)
		{
			EventHandler<GotFocusEventArgs> handler = GotFocus;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raise ExecuteCommand Event.
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
		#endregion Event Raisers

		#region Methods
		/// <summary>
		/// Assign default text for boolean values
		/// Three texts are added to the list. Given order is important, related properties will use that order
		/// </summary>
		private void AssignDefaultTextForBooleans()
		{
			string boolNull = CultureManager.TranslateString(LanguageConstantKeys.L_BOOL_NULL, LanguageConstantValues.L_BOOL_NULL);
			mBooleanValues.Add(new KeyValuePair<object, string>(null, boolNull));
			string boolTrue = CultureManager.TranslateString(LanguageConstantKeys.L_BOOL_TRUE, LanguageConstantValues.L_BOOL_TRUE);
			mBooleanValues.Add(new KeyValuePair<object, string>(true, boolTrue));
			string boolFalse = CultureManager.TranslateString(LanguageConstantKeys.L_BOOL_FALSE, LanguageConstantValues.L_BOOL_FALSE);
			mBooleanValues.Add(new KeyValuePair<object, string>(false, boolFalse));
		}

		/// <summary>
		/// Adds an item to the DisplaySet (only the data type).
		/// </summary>
		/// <param name="name">Name of the item.</param>
		/// <param name="alias">Alias of the item.</param>
		/// <param name="idxml">XML item identifier.</param>
		/// <param name="modelType">Type of the item.</param>
		/// <param name="Agents">Agents.</param>
		/// <param name="width">Width.</param>
		public void AddDisplaySetItem(string name, string alias, string idxml, ModelType modelType, List<KeyValuePair<object, string>> definedSelectionOptions, string[] Agents, int width)
		{
			// Add an item to the displayset (only the data type).
			mTypeList.Add(modelType);
			if (modelType == ModelType.Bool)
			{
				mDefinedSelectionOptions.Add(mBooleanValues);
			}
			else
			{
				mDefinedSelectionOptions.Add(definedSelectionOptions);
			}
		}
		/// <summary>
		/// Shows data (empty function due to compatibility of the interface).
		/// </summary>
		/// <param name="data">Data to show.</param>
		public void ShowData(DataTable data, List<Oid> selectedOids)
		{
			// Null means empty ComboBox.
			if (data == null)
			{
				mComboBoxIT.DataSource = null;
				mComboBoxIT.Items.Clear();
				return;
			}

			// Load from the DataTable.
			// Create the expresion to be showed in the ComboBox control.
			try
			{
				// Add summary column with all the information of the row
				DataColumn lcolumn = data.Columns.Add("_exp", typeof(string));

				List<DataColumn> displaySetColumns = Adaptor.ServerConnection.GetDisplaySetColumns(data);

				foreach (System.Data.DataRow lRow in data.Rows)
				{
					string lAux = GetRowAsText(lRow, displaySetColumns);

					// Sets the value
					lRow["_exp"] = lAux;
				}
			}
			catch
			{
			}
			// Assign the DataSource object.
			mComboBoxIT.DataSource = data.DefaultView;
			// Show the column with the expression.
			mComboBoxIT.DisplayMember = "_exp";

			Values = selectedOids;
		}

		/// <summary>
		/// Sets message (empty function due to compatibility of the interface).
		/// </summary>
		/// <param name="message">Message to set.</param>
		public void SetMessage(string message)
		{
		}

		/// <summary>
		/// Converts the received row in a string.
		/// </summary>
		/// <param name="row">Data Row</param>
		/// <param name="displaySetColumns">Columns with the values to be converted</param>
		/// <returns>Return value is the concatenation of all the columns values</returns>
		private string GetRowAsText(DataRow row, List<DataColumn> displaySetColumns)
		{
			string lText = "";
			string lAux = "";
			int i = 0;
			// For all the columns in the Row
			foreach (DataColumn column in displaySetColumns)
			{
				// Defined selection case
				if (mDefinedSelectionOptions[i] != null)
				{
					lAux = "";
					for (int j = 0; j < mDefinedSelectionOptions[i].Count; j++)
					{
						if (row.ItemArray[column.Ordinal].GetType() == typeof(System.DBNull))
						{
							if (mDefinedSelectionOptions[i][j].Key == null)
							{
								lAux += mDefinedSelectionOptions[i][j].Value;
								break;
							}
						}
						else
						{
							if (mDefinedSelectionOptions[i][j].Key != null &&
								mDefinedSelectionOptions[i][j].Key.ToString() == row.ItemArray[column.Ordinal].ToString())
							{
								lAux += mDefinedSelectionOptions[i][j].Value;
								break;
							}
						}
					}

					// Value not found in the defined selection set. Apply default
					if (lAux == "" && row.ItemArray[column.Ordinal].GetType() != typeof(System.DBNull))
					{
						lAux = DefaultFormats.ApplyDisplayFormat(row.ItemArray[column.Ordinal], mTypeList[i]);
					}
				}
				else
				{
					lAux = DefaultFormats.ApplyDisplayFormat(row.ItemArray[column.Ordinal], mTypeList[i]);
				}
				i++;
				// Separates values with blank space
				if (lText != "")
					lText += " ";
				lText += lAux;
			}

			return lText;
		}
		/// <summary>
		/// Obtains selected Oids (only one).
		/// </summary>
		/// <returns>List of Oids (only one).</returns>
		private List<Oid> GetSelectedOIDs()
		{
			// Gets a list of Oids with the selected Oid.
			if (mComboBoxIT.SelectedIndex == -1)
			{
				return null;
			}

			Oids.Oid oid;
			DataView table = (DataView)mComboBoxIT.DataSource;
			DataRowView DataRowView = (DataRowView)mComboBoxIT.SelectedItem;
			oid = Adaptor.ServerConnection.GetOid(table.Table, DataRowView.Row);

			// Check if the selection is null
			if (oid == null)
			{
				return null;
			}

			// Check for null values in the Oid object.
			foreach (IOidField loidField in oid.Fields)
			{
				if (loidField.Value == null || loidField.Value == DBNull.Value)
				{
					return null;
				}
			}
			List<Oid> oids = new List<Oid>();
			oids.Add(oid);
			return oids;
		}
		/// <summary>
		/// Executes the actions related to SelectionChanged event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleComboBoxITSelectedIndexChanged(object sender, EventArgs e)
		{
			// Raise the event indicating the change of the selected instance (Oid).
			if (SelectionChanged != null)
			{
				SelectionChanged(sender, new SelectedChangedEventArgs(GetSelectedOIDs()));
			}
		}
		/// <summary>
		/// Sets Format DisplaySet Item (empty function due to compatibility of the interface).
		/// </summary>
		/// <param name="name">Item name.</param>
		/// <param name="alias">Item alias.</param>
		/// <param name="modelType">Item type.</param>
		/// <param name="width">Item width.</param>
		public void SetFormatDisplaySetItem(string name, string alias, ModelType modelType, List<KeyValuePair<object, string>> definedSelectionOptions, int width, bool editable, bool allowsNullInEditMode)
		{

		}

		public bool Validate(string errorMessage)
		{
			bool lResult = true;

			//Checks combo value with values contained in list
			int lSearchItemInCombo = this.mComboBoxIT.FindStringExact(this.mComboBoxIT.Text);
			if ((this.mErrorProvider != null) && (this.mComboBoxIT != null))
			{
				//Locate error message inside combo control
				mErrorProvider.SetIconPadding(this.mComboBoxIT, -35);

				// Null validation.
				if (!this.NullAllowed)
				{
					//Keep contained list validation message
					if (lSearchItemInCombo == -1 && !string.IsNullOrEmpty(this.mComboBoxIT.Text))
					{
						return false;
					}
					List<Oid> lOids = this.Value as List<Oid>;
					if ((lOids  == null) || (lOids.Count == 0 ))
					{
						mErrorProvider.SetError(this.mComboBoxIT, errorMessage);

						// Validation error.
						return false;
					}

				}
			}

			// Validation OK.
			return lResult;
		}
		/// <summary>
		/// Removes all the DisplaySet items
		/// </summary>
		public void RemoveAllDisplaySetItems()
		{
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
		/// Sets the selected index in the ComboBox.
		/// </summary>
		/// <param name="value">List containing the selected Oid.</param>
		private void SetSelectedOids(List<Oid> value)
		{
			// Store previous value
			this.PreviousValue = mComboBoxIT.Text;

			// Select the element of the list that matches the received Oid.
			List<Oid> lOids = value as List<Oid>;
			// If it is null or there are more than one, do not select anyone.
			if (lOids == null || lOids.Count != 1 || mComboBoxIT.DataSource == null)
			{
				mComboBoxIT.SelectedIndex = -1;
				return;
			}

			Oid lOidToFind = lOids[0];

			// Validate the selected OID
			if (!Oid.IsNotNullAndValid(lOidToFind))
			{
				mComboBoxIT.SelectedIndex = -1;
				return;
			}

			DataView table = (DataView)mComboBoxIT.DataSource;
			foreach (DataRowView row in table)
			{
				Oid lOid = Adaptor.ServerConnection.GetOid(table.Table, row.Row);
				if (lOidToFind.Equals(lOid))
				{
					mComboBoxIT.SelectedItem = row;
					return;
				}
			}
			mComboBoxIT.SelectedIndex = -1;
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
