// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of the .NET ComboBox control for Defined Selection patterns.
	/// </summary>
    public class ComboBoxPresentation : EditorPresentation, ISelectorPresentation
	{
		#region Members
		/// <summary>
		/// ComboBox data type.
		/// </summary>
		protected ModelType mDataType;
		/// <summary>
		/// ComboBox instance.
		/// </summary>
		private ComboBox mComboBoxIT;
		/// <summary>
		/// Error provider for validating data.
		/// </summary>
		private ErrorProvider mErrorProvider;
		/// <summary>
		/// If the control allows null values.
		/// </summary>
		private bool mNullAllowed = false;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'ComboBoxPresentation', setting ComboBox data type.
		/// </summary>
		/// <param name="comboBox">ComboBox instance.</param>
		public ComboBoxPresentation(ComboBox comboBox)
		{
			mComboBoxIT = comboBox;
			if (mComboBoxIT != null)
			{
				// Create and configure the ErrorProvider control.
				mErrorProvider = new ErrorProvider();
				mErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
				// Link MaskedTextBox control events.
				mComboBoxIT.GotFocus += new EventHandler(HandleComboBoxITGotFocus);
				mComboBoxIT.LostFocus += new EventHandler(HandleComboBoxITLostFocus);
				mComboBoxIT.SelectionChangeCommitted += new EventHandler(HandleComboBoxITSelectionChangeCommitted);
				mComboBoxIT.KeyDown += new KeyEventHandler(HandleComboBoxITKeyDown);
			}
		}
		/// <summary>
		/// Initializes a new instance of 'ComboBoxPresentation', setting ComboBox data type.
		/// </summary>
		/// <param name="comboBox">ComboBox instance.</param>
		/// <param name="dataType">Data type.</param>
		public ComboBoxPresentation(ComboBox comboBox, ModelType dataType)
			: this(comboBox)
		{
			DataType = dataType;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets ComboBox data type.
		/// </summary>
		public ModelType DataType
		{
			get
			{
				return mDataType;
			}
			set
			{
				mDataType = value;
			}
		}
		/// <summary>
		/// Gets or sets selected index of the ComboBox.
		/// </summary>
		public object Value
		{
			get
			{
				if (mComboBoxIT.SelectedIndex == -1)
				{
					return null;
				}
				KeyValuePair<object, string> pair = (KeyValuePair<object, string>)mComboBoxIT.SelectedItem;
				return pair.Key;
			}
			set
			{
				if (value == null)
				{
					mComboBoxIT.SelectedItem = null;
					mComboBoxIT.SelectedIndex = -1;
				}
				else
				{
					mComboBoxIT.SelectedValue = value;
				}
				//Delete validation when values going to be introduced
				if (mErrorProvider != null && mComboBoxIT.FindStringExact(mComboBoxIT.Text) != -1)
				{
					mErrorProvider.Clear();
				}
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
		/// Gets or sets Focused property.
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
		/// Gets or sets NullAllowed property.
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

				//return when is disabled
				if (!mComboBoxIT.Enabled)
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
		/// Sets ComboBox items.
		/// </summary>
		public IList<KeyValuePair<object, string>> Items
		{
			get
			{
				return null;
			}
			set
			{
				mComboBoxIT.Items.Clear();
				mComboBoxIT.DataSource = value;
				mComboBoxIT.DisplayMember = "Value";
				mComboBoxIT.ValueMember = "Key";
			}
		}
		/// <summary>
		/// Gets or sets ComboBox selected index.
		/// </summary>
		public int SelectedItem
		{
			get
			{
				return mComboBoxIT.SelectedIndex;
			}
			set
			{
				mComboBoxIT.SelectedIndex = value;
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the max legth of the control value.
		/// </summary>
		public int MaxLength
		{
			get
			{
				return 0;
			}
			set{}
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
		/// Occurs when the control gets focus.
		/// </summary>
		public event EventHandler<GotFocusEventArgs> GotFocus;
		/// <summary>
		/// Occurs when the control loses the focus.
		/// </summary>
		public event EventHandler<LostFocusEventArgs> LostFocus;
		/// <summary>
		/// Occurs when the control value is changed.
		/// </summary>
		public event EventHandler<ValueChangedEventArgs> ValueChanged;
		/// <summary>
		/// Occurs when enable property is changed.
		/// </summary>
		public event EventHandler<EnabledChangedEventArgs> EnableChanged;
		/// <summary>
		/// Execute Command event Implementation of IEditorPresentation interface.
		/// </summary>
		public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Executes actions related to GotFocus event.
		/// </summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">Object sender.</param>
		private void HandleComboBoxITGotFocus(object sender, EventArgs e)
		{
			OnGotFocus(new GotFocusEventArgs());
		}
		/// <summary>
		/// Executes actions related to LostFocus event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleComboBoxITLostFocus(object sender, EventArgs e)
		{
			// Before leaving the control, check if the data belongs to the specified type.
			string lMessage = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR, LanguageConstantValues.L_ERROR);
			if (!this.Validate(lMessage))
			{
				//Delete any errorProvider when combo field is empty
				if (string.IsNullOrEmpty(this.mComboBoxIT.Text))
				{
					this.mErrorProvider.Clear();
				}
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

			// Throw lost focus event
			OnLostFocus(new LostFocusEventArgs());
		}

		/// <summary>
		/// Executes actions related to SelectionChangeCommitted event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleComboBoxITSelectionChangeCommitted(object sender, EventArgs e)
		{
			// Clear the ErrorProvider control.
			if (mErrorProvider != null)
			{
				mErrorProvider.Clear();
			}

			// Throw value changed event.
			OnValueChanged(new ValueChangedEventArgs());
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
		/// Raise the EnableChanged event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnEnableChanged(EnabledChangedEventArgs eventArgs)
		{
			EventHandler<EnabledChangedEventArgs> handler = EnableChanged;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raise the ValueChanged event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnValueChanged(ValueChangedEventArgs eventArgs)
		{
			EventHandler<ValueChangedEventArgs> handler = ValueChanged;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raise the LostFocus event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnLostFocus(LostFocusEventArgs eventArgs)
		{
			EventHandler<LostFocusEventArgs> handler = LostFocus;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raise the GotFocus event
		/// </summary>
		/// <param name="eventArgs"></param>
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
		/// Validates the control value.
		/// </summary>
		/// <param name="defaultErrorMessage">Default validation message.</param>
		/// <returns>Boolean value indicating if the validation was ok or not.</returns>
		public bool Validate(string defaultErrorMessage)
		{
			//Integer value for predictive combobox. If valis is -1 a validation message will showed
			int lSearchItemInCombo = this.mComboBoxIT.FindStringExact(this.mComboBoxIT.Text);
			//Validation message for predictive combobox
			string lDefSelectionErrorMessage = CultureManager.TranslateString(LanguageConstantKeys.L_VALIDATION_DEFSELECTION_ERROR, LanguageConstantValues.L_VALIDATION_DEFSELECTION_ERROR);
			if ((this.mErrorProvider != null) && (this.mComboBoxIT != null))
			{
				mErrorProvider.SetIconPadding(this.mComboBoxIT, -35);

				//Shows errorProvider with new validation message
				if (lSearchItemInCombo == -1 && !string.IsNullOrEmpty(this.mComboBoxIT.Text))
				{
					mErrorProvider.SetError(this.mComboBoxIT, lDefSelectionErrorMessage);
					this.mComboBoxIT.Focus();
					this.mComboBoxIT.Select();
					return false;
				}
				// Null validation.
				if ((!this.NullAllowed) && (this.Value == null))
				{
					//combobox value is not a value contained in list or is null, show new validation message
					if ((lSearchItemInCombo == -1 || lSearchItemInCombo == 0) && string.IsNullOrEmpty(this.mComboBoxIT.Text))
					{
						mErrorProvider.SetError(this.mComboBoxIT, defaultErrorMessage);
						// Validation error.
						return false;
					}
				}
				//Delete error provider when field is empty or value is correct
				if (lSearchItemInCombo > 0 || string.IsNullOrEmpty(this.mComboBoxIT.Text))
				{
					this.mErrorProvider.Clear();
				}
			}
			// Validation OK.
			return true;
		}
		/// <summary>
		/// Inserts an item into the ComboBox list at specified index.
		/// </summary>
		/// <param name="index">Index where item is inserted.</param>
		/// <param name="optionValue">Item to insert</param>
		public void InsertItem(int index, string optionValue)
		{
			mComboBoxIT.Items.Insert(index, optionValue);
		}
		/// <summary>
		/// Adds an item to the ComboBox list.
		/// </summary>
		/// <param name="optionValue"></param>
		public void InsertItem(string optionValue)
		{
			mComboBoxIT.Items.Add(optionValue);
		}
		/// <summary>
		/// Removes an item in the ComboBox list at specified index.
		/// </summary>
		/// <param name="index">Index where item will be removed.</param>
		public void RemoveItem (int index)
		{
			mComboBoxIT.Items.RemoveAt(index);
		}
		/// <summary>
		/// Removes the specified item from the ComboBox list.
		/// </summary>
		/// <param name="optionValue">Item to remove.</param>
		public void RemoveItem (string optionValue)
		{
			mComboBoxIT.Items.Remove(optionValue);
		}
		#endregion Methods
	}
}
