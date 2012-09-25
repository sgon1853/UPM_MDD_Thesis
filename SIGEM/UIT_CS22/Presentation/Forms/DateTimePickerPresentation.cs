// v3.8.4.5.b
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of the .NET DateTimePicker control.
	/// </summary>
    class DateTimePickerPresentation : EditorPresentation, IEditorPresentation, IMaskPresentation
	{
		#region Members
		/// <summary>
		/// Data type.
		/// </summary>
		protected ModelType mDataType;
		/// <summary>
		/// MaskedTextBox instance.
		/// </summary>
		private MaskedTextBox mMaskedTextBoxIT;
		/// <summary>
		/// DateTimePicker instance.
		/// </summary>
		private DateTimePicker mDateTimePickerIT;
		/// <summary>
		/// Mask.
		/// </summary>
		private string mMask = null;
		/// <summary>
		/// Error provider for validating data.
		/// </summary>
		private ErrorProvider mErrorProvider;
		/// <summary>
		/// If the control allows null values.
		/// </summary>
		private bool mNullAllowed = false;
		/// <summary>
		/// Old text in the control.
		/// </summary>
		private string oldPickerText = "";
		/// <summary>
		/// Introduction Pattern validation error message.
		/// </summary>
		private string mIPValidationMessage = "";
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'DateTimePickerPresentation' class.
		/// </summary>
		/// <param name="maskedTextBox">.NET MaskedTextBox reference.</param>
		/// <param name="dateTimePicker">.NET DateTimePicker reference.</param>
		public DateTimePickerPresentation(MaskedTextBox maskedTextBox, DateTimePicker dateTimePicker)
		{
			mMaskedTextBoxIT = maskedTextBox;
			mDateTimePickerIT = dateTimePicker;
			if (mMaskedTextBoxIT != null)
			{
				// Create and configure the ErrorProvider control.
				mErrorProvider = new ErrorProvider();
				mErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

				// Link MaskedTextBox control events.
				mMaskedTextBoxIT.GotFocus += new EventHandler(HandleMaskedTextBoxITGotFocus);
				mMaskedTextBoxIT.LostFocus += new EventHandler(HandleMaskedTextBoxITLostFocus);
				mMaskedTextBoxIT.TextChanged += new EventHandler(HandleMaskedTextBoxITTextChanged);
				mMaskedTextBoxIT.EnabledChanged += new EventHandler(HandleMaskedTextBoxITEnabledChanged);
				mMaskedTextBoxIT.KeyDown += new KeyEventHandler(HandleMaskedTextBoxITKeyDown);
			}

			if (dateTimePicker != null)
			{
				mDateTimePickerIT.Enter += new System.EventHandler(HandleDateTimePickerITEnter);
				mDateTimePickerIT.KeyUp += new System.Windows.Forms.KeyEventHandler(HandleDateTimePickerITKeyUp);
				mDateTimePickerIT.DropDown += new System.EventHandler(HandleDateTimePickerITDropDown);
				mDateTimePickerIT.CloseUp += new System.EventHandler(HandleDateTimePickerCloseUp);
			}
		}
		/// <summary>
		/// Initializes a new instance of the 'DateTimePickerPresentation' class.
		/// </summary>
		/// <param name="maskedTextBox">.NET MaskedTextBox reference.</param>
		/// <param name="dataType">Data type.</param>
		/// <param name="mask">Introduction mask.</param>
		/// <param name="dateTimePicker">.NET DateTimePicker reference.</param>
		public DateTimePickerPresentation(MaskedTextBox maskedTextBox, ModelType dataType, string mask, DateTimePicker dateTimePicker)
			: this(maskedTextBox, dateTimePicker)
		{
			DataType = dataType;
			Mask = mask;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets data type.
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
				mMaskedTextBoxIT.TextAlign = HorizontalAlignment.Right;
			}
		}
		/// <summary>
		/// Gets or sets mask.
		/// </summary>
		public string Mask
		{
			get
			{
				return mMask;
			}
			set
			{
				mMask = value;
				if (mMaskedTextBoxIT != null)
				{
					mMaskedTextBoxIT.Mask = ConvertMask2DisplayMask(value);
					mMaskedTextBoxIT.TextMaskFormat = MaskFormat.IncludeLiterals;
				}
			}
		}
		/// <summary>
		/// Gets or sets value.
		/// </summary>
		public object Value
		{
			get
			{
				if (!HasValue())
				{
					RestoreMask(true);
					return null;
				}

				if (string.IsNullOrEmpty(Mask))
				{
					// Check that the data belongs to the specified type.
					if (!DefaultFormats.CheckDataType(mMaskedTextBoxIT.Text, mDataType, mNullAllowed))
					{
						return null;
					}
					return Logics.Logic.StringToModel(mDataType, mMaskedTextBoxIT.Text);
				}
				else
				{
					return Logics.Logic.StringToDateTime(mMaskedTextBoxIT.Text, Mask);
				}
			}
			set
			{
				if (value == null)
				{
					mMaskedTextBoxIT.Text = string.Empty;
				}
				else
				{
					if (string.IsNullOrEmpty(Mask))
					{
						mMaskedTextBoxIT.Text = DefaultFormats.ApplyInputFormat(value, mDataType);
					}
					else
					{
						if (mDataType == ModelType.DateTime)
						{
							// Time data type.
							string stringValue = DefaultFormats.ApplyInputFormat(value, mDataType, Mask);
							DateTime auxDateTimeValue = DateTime.ParseExact(stringValue, Mask, CultureManager.Culture);
							if (!value.Equals(auxDateTimeValue))
							{
								// If the value is not the same after applying the mask is because 
								// it does not satisfies the mask value.
								mMaskedTextBoxIT.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals; // In order to reset the editor Mask properly.
								mMaskedTextBoxIT.Mask = ""; // Reset the mask.
								mMaskedTextBoxIT.Text = DefaultFormats.ApplyInputFormat(value, mDataType);
							}
							else
							{
								// In other case it satisfies the mask. Apply the value.
								mMaskedTextBoxIT.Text = stringValue;
							}
						}
						else
						{
							mMaskedTextBoxIT.Text = DefaultFormats.ApplyInputFormat(value, mDataType, mMask);
						}
					}
				}

				// Clear the ErrorProvider control.
				if (mErrorProvider != null)
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
				return mMaskedTextBoxIT.Enabled;
			}
			set
			{
				mMaskedTextBoxIT.Enabled = value;
				mDateTimePickerIT.Enabled = value;
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

				//Set read only mode.
				if (ReadOnly)
				{
					return;
				}

				if (!mNullAllowed)
				{
					mMaskedTextBoxIT.BackColor = System.Drawing.SystemColors.Info;
				}
				else
				{
					// Set the back color value.
					if (Enabled)
					{
						mMaskedTextBoxIT.BackColor = System.Drawing.SystemColors.Window;
					}
					else
					{
						mMaskedTextBoxIT.BackColor = System.Drawing.SystemColors.Control;
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
		/// Gets or sets Focused property.
		/// </summary>
		public bool Focused
		{
			get
			{
				return mMaskedTextBoxIT.Focused;
			}
			set
			{
				if (value == true)
				{
                    ActivateParentTabPage(mMaskedTextBoxIT);
                    mMaskedTextBoxIT.Focus();
				}
				else if (mMaskedTextBoxIT.Parent != null)
				{
					mMaskedTextBoxIT.Parent.Focus();
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
				return mMaskedTextBoxIT.Visible;
			}
			set
			{
				mMaskedTextBoxIT.Visible = value;
				mDateTimePickerIT.Visible = value;
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
				return mMaskedTextBoxIT.ReadOnly;
			}
			set
			{
				mMaskedTextBoxIT.ReadOnly = value;
				if (value)
				{
					mDateTimePickerIT.Enabled = false;
				}
				else
				{
					mDateTimePickerIT.Enabled = true;
				}
			}
		}
		/// <summary>
		/// Introduction Pattern validation message
		/// </summary>
		public string IPValidationMessage
		{
			get
			{
				return mIPValidationMessage;
			}
			set
			{
				mIPValidationMessage = value;
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
		/// No commands associated.
		/// </summary>
		public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Executes action related to EnableChanged event.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleMaskedTextBoxITEnabledChanged(object sender, EventArgs e)
		{
			OnEnableChanged(new EnabledChangedEventArgs());
		}
		/// <summary>
		/// Executes action related to GotFocus event.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleMaskedTextBoxITGotFocus(object sender, EventArgs e)
		{
			mMaskedTextBoxIT.SelectAll();

			// Throw got focus event.
			OnGotFocus(new GotFocusEventArgs());
		}
		/// <summary>
		/// Executes actions related to LostFocus event.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleMaskedTextBoxITLostFocus(object sender, EventArgs e)
		{
			// Before leaving the control, check if the data belongs to the specified type.
			string lMessageError = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR, LanguageConstantValues.L_ERROR);
			if (HasValue() && !this.Validate(lMessageError))
			{
				return;
			}

			// Throw Value changed event.
			OnValueChanged(new ValueChangedEventArgs());
			// Throw lost focus event.
			OnLostFocus(new LostFocusEventArgs());
		}
		/// <summary>
		/// Executes the action related to the TextChanged event.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleMaskedTextBoxITTextChanged(object sender, EventArgs e)
		{
			// Clear the ErrorProvider control.
			if (mErrorProvider != null)
			{
				mErrorProvider.Clear();
			}

			// Raise the change value event if the control has not the focus.
			// If not, wait until it lose the focus.
			if (mMaskedTextBoxIT.Focused)
			{
				return;
			}

			// Check that the data belongs to the specified type.
			if (!DefaultFormats.CheckDataType(mMaskedTextBoxIT.Text, mDataType, true))
			{
				return;
			}

			// Throw value chaged event.
			OnValueChanged(new ValueChangedEventArgs());
		}
		/// <summary>
		/// Manages the KeyDown event and raises the Refresh execute command
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleMaskedTextBoxITKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteRefresh));
			}
		}
		/// <summary>
		///  Executes the action related to the CloseUp event.
		/// </summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleDateTimePickerCloseUp(object sender, EventArgs e)
		{
			oldPickerText = mMaskedTextBoxIT.Text;

			if (!string.IsNullOrEmpty(Mask))
			{
				mMaskedTextBoxIT.Text = DefaultFormats.ApplyInputFormat(this.mDateTimePickerIT.Value, mDataType, Mask);
			}
			else
			{
				mMaskedTextBoxIT.Text = DefaultFormats.ApplyInputFormat(this.mDateTimePickerIT.Value, mDataType);
			}
		}
		/// <summary>
		/// Executes the action related to the KeyUp event.
		/// </summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">KeyEventArgs.</param>
		private void HandleDateTimePickerITKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				mMaskedTextBoxIT.Text = oldPickerText;
			}
		}
		/// <summary>
		/// Executes the action related to the Enter event.
		/// </summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleDateTimePickerITEnter(object sender, EventArgs e)
		{
			DateTime? lValue = Logics.Logic.StringToDateTime(mMaskedTextBoxIT.Text, Mask);
			if (lValue != null)
			{
				try
				{
					// Protected from possible values out of the date and time limits.
					mDateTimePickerIT.Value = lValue.Value;
				}
				catch { }
			}
			else
			{
				mDateTimePickerIT.Value = DateTime.Now;
			}
			oldPickerText = mMaskedTextBoxIT.Text;
		}
		/// <summary>
		/// Executes the action related to the DropDown event.
		/// </summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleDateTimePickerITDropDown(object sender, EventArgs e)
		{
			oldPickerText = mMaskedTextBoxIT.Text;
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
		/// Check if the received string value matches with the Introduction Mask for DateTime datatype.
		/// </summary>
		/// <param name="stringValue">String to be checked.</param>
		/// <returns>Returns true if the string matches with Introduction Mask.</returns>
		private bool IsValidDateTimeAccordingMask(string stringValue)
		{
			bool result = true;
			if (mMask != string.Empty)
			{
				MaskedTextProvider maskTextProvider = new MaskedTextProvider(ConvertMask2DisplayMask(mMask));
				result = maskTextProvider.VerifyString(stringValue);
			}
			return result;
		}
		/// <summary>
		/// Validates the control value.
		/// </summary>
		/// <param name="defaultErrorMessage">Default validation message.</param>
		/// <returns>Boolean value indicating if the validation was ok or not.</returns>
		public bool Validate(string defaultErrorMessage)
		{
			// No graphical editor.
			if (mMaskedTextBoxIT == null)
			{
				return true;
			}

			// Null validation.
			if (!NullAllowed && !HasValue())
			{
				// Validation error.
				if (mErrorProvider != null)
				{
					mErrorProvider.SetError(this.mMaskedTextBoxIT, defaultErrorMessage);
				}
				return false;
			}

			// No value in the editor.
			if (NullAllowed && !HasValue())
			{
				return true;
			}

			// There is a value in the editor.
			bool lIsValid = true;

			if (string.IsNullOrEmpty(Mask))
			{
				// If there is not a mask, check the value according data type.
				lIsValid = DefaultFormats.CheckDataType(this.mMaskedTextBoxIT.Text, mDataType, mNullAllowed);
			}
			else
			{
				lIsValid = !(Logics.Logic.StringToDateTime(mMaskedTextBoxIT.Text, Mask) == null);
				if (mDataType == ModelType.DateTime)
				{
					lIsValid = lIsValid && IsValidDateTimeAccordingMask(this.mMaskedTextBoxIT.Text);
				}
				// If the editor had lost the mask and now has a valid value -> re-assign the mask.
				if (lIsValid)
				{
					RestoreMask(false);
				}
			}
			// Show the suitable error message.
			if (!lIsValid)
			{
				// Default or introduction pattern error message
				if (string.IsNullOrEmpty(IPValidationMessage))
				{
					object[] lArgs = new object[1];
					lArgs[0] = DefaultFormats.GetHelpMask(mDataType, string.Empty); ;
					string lErrorMessage = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_INVALID_FORMAT_MASK, LanguageConstantValues.L_VALIDATION_INVALID_FORMAT_MASK, lArgs);
					mErrorProvider.SetError(this.mMaskedTextBoxIT, lErrorMessage);
					mDateTimePickerIT.Value = DateTime.Now;
				}
				else
				{
					mErrorProvider.SetError(this.mMaskedTextBoxIT, IPValidationMessage);
				}
				return false;
			}

			// Validation OK.
			return true;
		}
		
		/// <summary>
		/// Converts the Mask into a C# input mask (based on C# MaskTextBox control Mask property).
		/// </summary>
		/// <param name="introMask">Introduction Pattern Masl to be converted.</param>
		/// <returns>Returns a mask based on C# MaskTextBox control Mask property.</returns>
		private string ConvertMask2DisplayMask(string introMask)
		{
			if (introMask == null)
				return "";

			string lMask = introMask;
			lMask = lMask.Replace("d", "0");
			lMask = lMask.Replace("M", "0");
			lMask = lMask.Replace("y", "0");
			lMask = lMask.Replace("H", "0");
			lMask = lMask.Replace("m", "0");
			lMask = lMask.Replace("s", "0");
			return lMask;
		}
		/// <summary>
		/// Return true if the editor has a value
		/// </summary>
		/// <returns></returns>
		private bool HasValue()
		{
			bool lResult;
			if (Mask != null && !Mask.Equals(string.Empty))
			{
				// Only Date and DateTime data types can have masks in this Presentation.
				// Get the DateTime value without Date and Time separators 
				// in order to check if the editor control has value.
				string lAuxStringValue = mMaskedTextBoxIT.Text.Replace(mMaskedTextBoxIT.Culture.DateTimeFormat.TimeSeparator, "");
				lAuxStringValue = lAuxStringValue.Replace(mMaskedTextBoxIT.Culture.DateTimeFormat.DateSeparator, "");
				lResult = !string.IsNullOrEmpty(lAuxStringValue.Trim());
			}
			else
			{
				lResult = !mMaskedTextBoxIT.Text.Equals(string.Empty);
			}
			return lResult;
		}
		/// <summary>
		/// Apply the mask to the graphical control
		/// </summary>
		/// <param name="onlyIfEditorEmpty">If this parameter is true, the Mask only will be applied if the editor is empty.</param>
		private void RestoreMask(bool onlyIfEditorEmpty)
		{
			// If there is a mask defined, the editor does not have value, and the editor had lost the mask -> re-assign the mask.
			if (onlyIfEditorEmpty)
			{
				if (mMaskedTextBoxIT.Text == string.Empty && mMaskedTextBoxIT.Mask == string.Empty && Mask != string.Empty)
				{
					Mask = mMask;
				}
			}
			else
			{
				if (mMaskedTextBoxIT.Mask == string.Empty && Mask != string.Empty)
				{
					Mask = mMask;
				}
			}
		}
		#endregion Methods
	}
}
