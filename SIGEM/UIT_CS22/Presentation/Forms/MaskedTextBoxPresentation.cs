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
	/// Presentation abstraction of the .NET MaskedTextBox control.
	/// </summary>
	public class MaskedTextBoxPresentation : EditorPresentation, INumericPresentation, IMaskPresentation
	{
		#region Members
		/// <summary>
		/// Data type.
		/// </summary>
		protected ModelType mDataType;
		/// <summary>
		/// MaskedTextBox instance.
		/// </summary>
		protected MaskedTextBox mMaskedTextBoxIT;
		/// <summary>
		/// Mask.
		/// </summary>
		private string mMask = string.Empty;
		/// <summary>
		/// Error provider for validating data.
		/// </summary>
		private ErrorProvider mErrorProvider;
		/// <summary>
		/// If the control allows null values.
		/// </summary>
		private bool mNullAllowed = false;
		/// <summary>
		/// The max length of the control values.
		/// </summary>
		private int mMaxLength = 0;
		/// <summary>
		/// If the control do the validation.
		/// </summary>
		private bool mValidateData = true;
		/// <summary>
		/// Minimum value for the range validation.
		/// </summary>
		private decimal? mMinValue = null;
		/// <summary>
		/// Maximum value for the range validation.
		/// </summary>
		private decimal? mMaxValue = null;
		/// <summary>
		/// Introduction Pattern validation error message.
		/// </summary>
		private string mIPValidationMessage = "";
		/// <summary>
		/// Maximum number of integer digits
		/// </summary>
		private int? mMaxIntegerDigits = null;
		/// <summary>
		/// Maximum number of decimal digits
		/// </summary>
		private int mMaxDecimalDigits = 0;
		/// <summary>
		/// Minimum number of decimal digits
		/// </summary>
		private int mMinDecimalDigits = 0;
		/// <summary>
		/// Display mask
		/// </summary>
		private string mDisplayMask = "";
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'MaskedTextBoxPresentation' class.
		/// </summary>
		/// <param name="maskedTextBox">.NET MaskedTextBox reference.</param>
		public MaskedTextBoxPresentation(MaskedTextBox maskedTextBox)
			:base()
		{
			mMaskedTextBoxIT = maskedTextBox;

			if (mMaskedTextBoxIT != null)
			{
				PreviousValue = mMaskedTextBoxIT.Text;

				// Create and configure the ErrorProvider control.
				mErrorProvider = new ErrorProvider();
				mErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

				// Link MaskedTextBox control events.
				mMaskedTextBoxIT.Enter += new EventHandler(HandleMaskedTextBoxITEnter);
				mMaskedTextBoxIT.Leave += new EventHandler(HandleMaskedTextBoxITLeave);
				mMaskedTextBoxIT.TextChanged += new EventHandler(HandleMaskedTextBoxITTextChanged);
				mMaskedTextBoxIT.EnabledChanged += new EventHandler(HandleMaskedTextBoxITEnabledChanged);
				mMaskedTextBoxIT.KeyDown += new KeyEventHandler(HandleMaskedTextBoxITKeyDown);
				mMaskedTextBoxIT.KeyPress += new KeyPressEventHandler(HandleMaskedTextBoxITKeyPress);
			}
		}
		/// <summary>
		/// Initializes a new instance of the 'MaskedTextBoxPresentation' class.
		/// </summary>
		/// <param name="maskedTextBox">.NET MaskedTextBox reference.</param>
		/// <param name="ValidateData">Do the data validation.</param>
		public MaskedTextBoxPresentation(MaskedTextBox maskedTextBox, bool ValidateData)
			:this(maskedTextBox)
		{
			mValidateData = ValidateData;
		}
		/// <summary>
		/// Initializes a new instance of the 'MaskedTextBoxPresentation' class.
		/// </summary>
		/// <param name="maskedTextBox">.NET MaskedTextBox reference.</param>
		/// <param name="dataType">Data type of the control.</param>
		/// <param name="mask">Introduction mask.</param>
		[Obsolete("Since version 3.1.3.9.")]
		public MaskedTextBoxPresentation(MaskedTextBox maskedTextBox, ModelType dataType, string mask)
			:this(maskedTextBox)
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
				if (mDataType == ModelType.Int ||
					mDataType == ModelType.Nat ||
					mDataType == ModelType.Real ||
					mDataType == ModelType.Time)
				{
					mMaskedTextBoxIT.TextAlign = HorizontalAlignment.Right;
					BuildDisplayMask();
				}
				else
				{
					mMaskedTextBoxIT.TextAlign = HorizontalAlignment.Left;
				}
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
				if (DataType == ModelType.Time)
				{
					mMaskedTextBoxIT.Mask = ConvertMask2DisplayMask(mMask);
				}
				else
				{
					mMaskedTextBoxIT.Mask = mMask;
				}

				if (DataType == ModelType.String)
				{
					mMaskedTextBoxIT.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
				}
				else
				{
					mMaskedTextBoxIT.TextMaskFormat = MaskFormat.IncludeLiterals;
				}
			}
		}
		/// <summary>
		/// Gets or sets value.
		/// </summary>
		public virtual object Value
		{
			get
			{
				if (!HasValue())
				{
					RestoreMask(true);
					return null;
				}

				// Check that the data belongs to the specified type.
				if (!DefaultFormats.CheckDataType(mMaskedTextBoxIT.Text, mDataType, mNullAllowed))
				{
					return null;
				}
				return Logics.Logic.StringToModel(mDataType, mMaskedTextBoxIT.Text);
			}
			set
			{
				if (value == null)
				{
					mMaskedTextBoxIT.Text = string.Empty;
				}
				else
				{
					// String and Time can have a Mask
					if (!string.IsNullOrEmpty(Mask))
					{
						if (mDataType == ModelType.String)
						{
							if (IsValidStringAccordingMask(value.ToString()))
							{
								// If the string is going to be assigned satisfies the introduction mask,
								mMaskedTextBoxIT.Text = DefaultFormats.ApplyInputFormat(value, mDataType, Mask);
							}
							else
							{
								// Apply the incorrect value.
								mMaskedTextBoxIT.Mask = ""; // Reset the mask.
								mMaskedTextBoxIT.Text = DefaultFormats.ApplyInputFormat(value, mDataType);
							}
						}
						else
						{
							// Time data type.
							string stringValue = DefaultFormats.ApplyInputFormat(value, mDataType, Mask);
							TimeSpan auxTimeValue = TimeSpan.Parse(stringValue);
							if (!value.Equals(auxTimeValue))
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
					}
					else
					{
						// Validate the value using the specified numeric format
						if (NumericFormatValidationRequired())
						{
							string lValue = DefaultFormats.ApplyInputFormat(value, mDataType, mDisplayMask);
							if (!UtilFunctions.ObjectsEquals(value, Convert.ToDecimal(lValue)))
							{
								lValue = DefaultFormats.ApplyInputFormat(value, mDataType);
							}
							mMaskedTextBoxIT.Text = lValue;
						}
						else
						{
							mMaskedTextBoxIT.Text = DefaultFormats.ApplyInputFormat(value, mDataType);
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
		public virtual bool Enabled
		{
			get
			{
				return mMaskedTextBoxIT.Enabled;
			}
			set
			{
				mMaskedTextBoxIT.Enabled = value;
			}
		}
		/// <summary>
		/// Gets or sets NullAllowed property.
		/// </summary>
		public virtual bool NullAllowed
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
		public virtual bool Focused
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
		public virtual bool Visible
		{
			get
			{
				return mMaskedTextBoxIT.Visible;
			}
			set
			{
				mMaskedTextBoxIT.Visible = value;
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the max legth of the control value.
		/// </summary>
		public int MaxLength
		{
			get
			{
				return mMaxLength;
			}
			set
			{
				mMaxLength = value;
			}
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
			}
		}
		/// <summary>
		/// Minimum value that can be introduced.
		/// </summary>
		public decimal? MinValue
		{
			get
			{
				return mMinValue;
			}
			set
			{
				mMinValue = value;
			}
		}
		/// <summary>
		/// Maximum value that can be introduced.
		/// </summary>
		public decimal? MaxValue
		{
			get
			{
				return mMaxValue;
			}
			set
			{
				mMaxValue = value;
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
		/// <summary>
		/// Maximum number of integer digits
		/// </summary>
		public int? MaxIntegerDigits
		{
			get
			{
				return mMaxIntegerDigits;
			}
			set
			{
				mMaxIntegerDigits = value;
				BuildDisplayMask();
			}
		}
		/// <summary>
		/// Maximum number of decimal digits
		/// </summary>
		public int MaxDecimalDigits
		{
			get
			{
				return mMaxDecimalDigits;
			}
			set
			{
				mMaxDecimalDigits = value;
				BuildDisplayMask();
			}
		}
		/// <summary>
		/// Minimum number of decimal digits
		/// </summary>
		public int MinDecimalDigits
		{
			get
			{
				return mMinDecimalDigits;
			}
			set
			{
				mMinDecimalDigits = value;
				BuildDisplayMask();
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
		/// Executes action related to EnableChanged event.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleMaskedTextBoxITEnabledChanged(object sender, EventArgs e)
		{
			//When the enabled event change, the Error porvider is clean.
			if (mErrorProvider != null)
			{
				mErrorProvider.Clear();
			}

			OnEnableChanged(new EnabledChangedEventArgs());
		}
		/// <summary>
		/// Executes action related to GotFocus event.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleMaskedTextBoxITEnter(object sender, EventArgs e)
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
		private void HandleMaskedTextBoxITLeave(object sender, EventArgs e)
		{
			ProcessMaskedTextBoxITLeave(sender, e);
		}
		/// <summary>
		/// Process the action related to LostFocus event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessMaskedTextBoxITLeave(object sender, EventArgs e)
		{
			// Apply Display Mask in order to delete final 0 digits in decimal part and assigning the numeric separator.
			if (NumericFormatValidationRequired())
			{
				if (!string.IsNullOrEmpty(mMaskedTextBoxIT.Text))
				{
					try
					{
						decimal lDecValue = decimal.Parse(mMaskedTextBoxIT.Text);
						mMaskedTextBoxIT.Text = lDecValue.ToString(mDisplayMask);
					}
					catch { }
				}
			}
			
			// Do the validation
			if (mValidateData)
			{
				// Before leaving the control, check if the data belongs to the specified type.
				string lErrorMessage = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR, LanguageConstantValues.L_ERROR);
				if (HasValue() && !this.Validate(lErrorMessage))
				{
					return;
				}
			}

			if (CheckValueChange(mMaskedTextBoxIT.Text))
			{
				// Value changed event
				OnValueChanged(new ValueChangedEventArgs());
			}

			PreviousValue = mMaskedTextBoxIT.Text;

			// Lost focus event.
			OnLostFocus(new LostFocusEventArgs());
		}

		/// <summary>
		/// Suscriber to KeyDown event
		/// </summary>
		/// <param name="sender">Control that raise the event.</param>
		/// <param name="e">Key pressed.</param>
		private void HandleMaskedTextBoxITKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteRefresh));
			}
		}
		/// <summary>
		/// Executes the action related to the TextChanged event.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleMaskedTextBoxITTextChanged(object sender, EventArgs e)
		{
			ProcessMaskedTextBoxITTextChanged(sender, e);
		}

		/// <summary>
		/// Process the action related to the TextChanged event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessMaskedTextBoxITTextChanged(object sender, EventArgs e)
		{
			RestoreMask(true);
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

  			if (CheckValueChange(mMaskedTextBoxIT.Text))
			{
				// Value changed event
				OnValueChanged(new ValueChangedEventArgs());
				PreviousValue = mMaskedTextBoxIT.Text;
			}
		}
		/// <summary>
		/// Executes the action related to the KeyPress event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleMaskedTextBoxITKeyPress(object sender, KeyPressEventArgs e)
		{
			if (DataType == ModelType.Real)
			{
				if (e.KeyChar == ',' || e.KeyChar == '.')
				{
					// Only one decimal separator
					if (mMaskedTextBoxIT.Text.Contains(CultureManager.Culture.NumberFormat.NumberDecimalSeparator[0].ToString()))
					{
						e.KeyChar = '\0';
						return;
					}
					// Ignore decimal separator because it is a Real data type
					e.KeyChar = CultureManager.Culture.NumberFormat.NumberDecimalSeparator[0];
					return;
				}
			}

			// Only for numbers
			if (DataType == ModelType.Autonumeric || DataType == ModelType.Nat ||
				DataType == ModelType.Int || DataType == ModelType.Real)
			{
				if (!DefaultFormats.ValidCharForNumericValues.Contains(e.KeyChar))
				{
					e.KeyChar = '\0';
					return;
				}
			}

			// Check numeric format
			if (ProcessKeyForNumericFormat(e))
			{
				e.Handled = true;
			}
		}

		/// <summary>
		/// Return true if the key must be canceled based on the format information.
		/// </summary>
		/// <param name="e">Pressed key</param>
		/// <returns></returns>
		private bool ProcessKeyForNumericFormat(KeyPressEventArgs e)
		{
			// No format to be checked, return false
			if (!NumericFormatValidationRequired())
			{
				return false;
			}

			// It is not a number
			if (!Char.IsNumber(e.KeyChar))
			{
				return false;
			}

			// Get the value and selection posisiton from the Editor
			string lValue = mMaskedTextBoxIT.Text;
			int lSelPos = mMaskedTextBoxIT.SelectionStart;
			int lSelLength = mMaskedTextBoxIT.SelectionLength;
			bool lAtEnd = lSelPos == lValue.Length - lSelLength;

			// Get the value taken in account the pressed key
			if (lSelLength != 0)
			{
				lValue = lValue.Remove(lSelPos, lSelLength);
			}
			lValue = lValue.Insert(lSelPos, e.KeyChar.ToString());

			// Get decimal and thousand separators.
			char lGroupSep = CultureManager.Culture.NumberFormat.NumberGroupSeparator[0];
			char lDecimalSep = CultureManager.Culture.NumberFormat.NumberDecimalSeparator[0];
			// Eliminate the thousand separator.
			lValue = lValue.Replace(lGroupSep.ToString(), "");

			string lAuxDisplayMask = string.Empty;
			// If the mask allows decimal values, replace the 0 mask characters (mandatory digits) in the decimal part.
			if (mDisplayMask.Contains("."))
			{
				lAuxDisplayMask = mDisplayMask.Replace('0', '#');
				// Keep the first integer part digit.
				lAuxDisplayMask = lAuxDisplayMask.Replace("#.", "0.");
			}
			// If there is minimum decimal digits and this number is the same as the maximum decimal digits
			// add as many 0 as the number of decimal digits and then put the separator in the proper place.
			if (mMinDecimalDigits > 0 && mMinDecimalDigits.Equals(mMaxDecimalDigits))
			{
				lValue = lValue.Replace(lDecimalSep.ToString(), "");
				string lAux = "";
				if (mMinDecimalDigits > lValue.Length - 1)
				{
					lAux = new string('0', mMinDecimalDigits - lValue.Length + 1);
				}
				if (lValue.StartsWith("-"))
				{
					lValue = lValue.Insert(1, lAux);
				}
				else
				{
					lValue = lAux + lValue;
				}
				lValue = lValue.Insert(lValue.Length - mMinDecimalDigits, lDecimalSep.ToString());
				// Only if min dec. digits is equal to max dec. digits,
				// the mask must contain the 0 mask characters (mandatory digits).
				lAuxDisplayMask = mDisplayMask;
			}

			// Convert to numeric
			decimal lDecValue = 0;
			if (lValue != "")
			{
				try
				{
					lDecValue = System.Convert.ToDecimal(lValue);
				}
				catch
				{
					return false;
				}
			}

			// If value is out of integer or decimal bounds, cancel the key
			lValue = lDecValue.ToString();
			int lDecimalPointPos = lValue.IndexOf(lDecimalSep);
			int lDecimalDigits = 0;
			int lIntegerDigits = 0;
			if (lDecimalPointPos != -1)
			{
				lDecimalDigits = lValue.Length - lDecimalPointPos - 1;
				lIntegerDigits = lDecimalPointPos;
			}
			else
			{
				lIntegerDigits = lValue.Length;
			}
			if (lDecValue < 0)
			{
				lIntegerDigits--;
			}
			// Too many decimal digits
			if (lDecimalDigits > mMaxDecimalDigits)
			{
				return true;
			}
			if (mMaxIntegerDigits != null && mMaxIntegerDigits > 0 && lIntegerDigits > mMaxIntegerDigits)
			{
				return true;
			}

			// Define a display mask in order to allow the introduction of 0's after the decimal separator (in decimal part).
			string lDecimalMask = string.Empty;
			string lIntegerMask = string.Empty;
			string lCerosMask = new string('0', lDecimalDigits);
			if (lDecimalPointPos != -1)
			{
				int lDecimalPositionInMask = mDisplayMask.IndexOf(".");
				lDecimalMask = mDisplayMask.Substring(lDecimalPositionInMask + 1);
				lDecimalMask = lDecimalMask.Substring(lDecimalDigits);
				lDecimalMask = lCerosMask + lDecimalMask;
				lIntegerMask = mDisplayMask.Substring(0, lDecimalPositionInMask);
				lAuxDisplayMask = lIntegerMask + "." + lDecimalMask;
			}

			// Assign the new value to the Editor
			mMaskedTextBoxIT.Text = lDecValue.ToString(lAuxDisplayMask);
			if (lAtEnd)
			{
				mMaskedTextBoxIT.SelectionStart = mMaskedTextBoxIT.Text.Length;
			}
			else
			{
				mMaskedTextBoxIT.SelectionStart = lSelPos + 1;
			}

			return true;
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

			// If there is a mask defined, it has to be validated the value according the mask format.
			if (!string.IsNullOrEmpty(Mask))
			{
				// Format validation for String and Time data types which have any introduction mask.
				// String data type
				if (mDataType == ModelType.String)
				{
					lIsValid = IsValidStringAccordingMask(this.mMaskedTextBoxIT.Text);
				}
				// Time data type
				if (mDataType == ModelType.Time)
				{
					lIsValid = DefaultFormats.CheckDataType(this.mMaskedTextBoxIT.Text, mDataType, mNullAllowed)
								&& this.mMaskedTextBoxIT.MaskCompleted
								&& IsValidTimeAccordingMask(this.mMaskedTextBoxIT.Text);
				}
			}
			else
			{
				// Value data type validation, when there is not a mask defined.
				lIsValid = DefaultFormats.CheckDataType(this.mMaskedTextBoxIT.Text, mDataType, mNullAllowed);

				#region Numeric format validation
				if (NumericFormatValidationRequired())
				{
					// Check the value according numeric introduction pattern.
					decimal? lValue = null;
					try
					{
						lValue = decimal.Parse(mMaskedTextBoxIT.Text, System.Globalization.NumberStyles.Number, CultureManager.Culture);
					}
					catch 
					{
						lIsValid = false;
					}

					if (!lIsValid)
					{
						// It is not a numeric value, or it is not a correct value according the data type.
						object[] lArgs = new object[1];
						lArgs[0] = DefaultFormats.GetHelpMask(mDataType, string.Empty);
						string lErrorMessage = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_INVALID_FORMAT_MASK, LanguageConstantValues.L_VALIDATION_INVALID_FORMAT_MASK, lArgs);
						mErrorProvider.SetError(this.mMaskedTextBoxIT, lErrorMessage);
						return false;
					}

					if (lValue != null)
					{
						// Number of integer and decimal digits.
						int lDecimalDigits = GetNumberDecimalDigits(mMaskedTextBoxIT.Text);
						int lIntegerDigits = GetNumberIntegerDigits(mMaskedTextBoxIT.Text);
						if (MinDecimalDigits > 0)
						{
							if (lDecimalDigits < MinDecimalDigits)
							{
								mErrorProvider.SetError(this.mMaskedTextBoxIT, IPValidationMessage);
								return false;
							}
						}
						if (lDecimalDigits > MaxDecimalDigits)
						{
							mErrorProvider.SetError(this.mMaskedTextBoxIT, IPValidationMessage);
							return false;
						}
						if (MaxIntegerDigits != null)
						{
							if (lIntegerDigits > MaxIntegerDigits)
							{
								mErrorProvider.SetError(this.mMaskedTextBoxIT, IPValidationMessage);
								return false;
							}
						}

						// Minimum and Maximum value validation.
						if ((mMinValue != null || mMaxValue != null) && (mMinValue != mMaxValue))
						{
							if (mMinValue != null && lValue < mMinValue)
							{
								mErrorProvider.SetError(this.mMaskedTextBoxIT, IPValidationMessage);
								return false;
							}
							if (mMaxValue != null && lValue > mMaxValue)
							{
								mErrorProvider.SetError(this.mMaskedTextBoxIT, IPValidationMessage);
								return false;
							}
						}
					}
				}
				#endregion Numeric format validation
			}

			// Show the suitable error message.
			if (!lIsValid)
			{
				// Default or introduction pattern error message
				if (string.IsNullOrEmpty(IPValidationMessage))
				{
					object[] lArgs = new object[1];
					lArgs[0] = DefaultFormats.GetHelpMask(mDataType, string.Empty);
					string lErrorMessage = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_INVALID_FORMAT_MASK, LanguageConstantValues.L_VALIDATION_INVALID_FORMAT_MASK, lArgs);
					mErrorProvider.SetError(this.mMaskedTextBoxIT, lErrorMessage);
				}
				else
				{
					mErrorProvider.SetError(this.mMaskedTextBoxIT, IPValidationMessage);
				}
				// Validation error.
				return false;
			}
			else
			{
				// Size validation.
				if ((mMaxLength != 0) && (mMaskedTextBoxIT.Text.Length > mMaxLength))
				{
					string lMessageError = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_SIZE_WITHOUT_ARGUMENT, LanguageConstantValues.L_VALIDATION_SIZE_WITHOUT_ARGUMENT, mMaxLength);
					mErrorProvider.SetError(this.mMaskedTextBoxIT, lMessageError);
					return false;
				}
			}

			// Restore the mask to the graphical control
			RestoreMask(false);

			// Validation OK.
			return true;
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
			lMask = lMask.Replace("H", "0");
			lMask = lMask.Replace("m", "0");
			lMask = lMask.Replace("s", "0");
			return lMask;
		}

		/// <summary>
		/// Returns the number of decimal digits of the received number
		/// </summary>
		/// <param name="number">String containing the number.</param>
		/// <returns>Number of decimal digits of the received number.</returns>
		private int GetNumberDecimalDigits(string number)
		{
			char lDecimalSep = CultureManager.Culture.NumberFormat.NumberDecimalSeparator[0];
			int lPos = number.IndexOf(lDecimalSep);
			if (lPos.Equals(-1))
				return 0;

			return number.Length - lPos - 1;
		}

		/// <summary>
		/// Returns the number of integer digits of the received number.
		/// </summary>
		/// <param name="number">String containing the number.</param>
		/// <returns>Number of integer digits of the received number.</returns>
		private int GetNumberIntegerDigits(string number)
		{
			decimal lValue = decimal.Parse(number, System.Globalization.NumberStyles.Number, CultureManager.Culture);
			lValue = decimal.Truncate(lValue);
			lValue = Math.Abs(lValue);

			return lValue.ToString().Length;
		}

		/// <summary>
		/// Build the display mask using the information of the numeric format validations.
		/// </summary>
		private void BuildDisplayMask()
		{
			// Decimal part
			string lDecimalPart = "";
			if (mMinDecimalDigits > 0)
			{
				lDecimalPart = new string('0', mMinDecimalDigits);
			}

			if (mMaxDecimalDigits != 0)
			{
				lDecimalPart += new string('#', mMaxDecimalDigits);
				lDecimalPart = lDecimalPart.Substring(0, mMaxDecimalDigits);
			}
			else
			{
				lDecimalPart += "######";
			}

			// Integer part
			string lIntegerPart = "";
			if (mMaxIntegerDigits == null || mMaxIntegerDigits == 0)
			{
				lIntegerPart = new string('#', 3) + "0";
			}
			else
			{
				lIntegerPart = new string('#', mMaxIntegerDigits.Value - 1) + "0";
			}

			// Thousand separator
			int lAux = lIntegerPart.Length - 3;
			for (int i = lAux; i > 0; i = i - 3)
			{
				lIntegerPart = lIntegerPart.Insert(i, ",");
			}

			string lDisplayMask = lIntegerPart;
			if (lDecimalPart.Length > 0)
			{
				lDisplayMask += "." + lDecimalPart;
			}

			mDisplayMask = lDisplayMask;
		}

		/// <summary>
		/// Checks if the editor has value or not.
		/// </summary>
		/// <returns>Return true if the editor has a value.</returns>
		private bool HasValue()
		{
			bool lResult = true;
			if (Mask != null && !Mask.Equals(string.Empty))
			{
				// Only String and Time data types can have masks in this Presentation. Check them separately.
				if (mDataType == ModelType.Time)
				{
					// Get the Time value without Time separators in order to check if the editor control has value.
					string lAuxStringValue = mMaskedTextBoxIT.Text.Replace(mMaskedTextBoxIT.Culture.DateTimeFormat.TimeSeparator, "");
					lResult = !string.IsNullOrEmpty(lAuxStringValue.Trim());
				}
				else
				{
					// String data type.
					MaskedTextProvider maskTextProvider = new MaskedTextProvider(Mask);
					maskTextProvider.IncludeLiterals = false;
					if (!maskTextProvider.VerifyString(mMaskedTextBoxIT.Text))
					{
						// If the value assigned to the control does not satisfies the mask,
						// check if the editor control Text has value.
						lResult = (mMaskedTextBoxIT.Text != string.Empty);
					}
					else
					{
						// If the value assigned satisfies the mask
						// (it is not mandatory that the mask has been completed),
						// assign the value to the MaskTextProvider and then check if
						// it has value.
						maskTextProvider.Set(mMaskedTextBoxIT.Text);
						string auxString = maskTextProvider.ToString(false, false);
						lResult = !string.IsNullOrEmpty(auxString);
					}
				}
			}
			else
			{
				lResult = !mMaskedTextBoxIT.Text.Equals(string.Empty);
			}
			return lResult;
		}

		/// <summary>
		/// Check if the received string value matches with the Introduction Mask for String datatype.
		/// </summary>
		/// <param name="stringValue">String to be checked.</param>
		/// <returns>Returns true if the received string matches with Introduction Mask.</returns>
		private bool IsValidStringAccordingMask(string stringValue)
		{
			bool result = true;
			if (Mask != string.Empty)
			{
				MaskedTextProvider maskTextProvider = new MaskedTextProvider(Mask);
				maskTextProvider.IncludeLiterals = false;
				maskTextProvider.Set(stringValue);
				result = maskTextProvider.VerifyString(stringValue) && maskTextProvider.MaskCompleted;
			}
			return result;
		}

		/// <summary>
		/// Check if the received string value matches with the Introduction Mask for Time datatype.
		/// </summary>
		/// <param name="stringValue">String to be checked.</param>
		/// <returns>Returns true if the received string matches with Introduction Mask.</returns>
		private bool IsValidTimeAccordingMask(string stringValue)
		{
			bool result = true;
			if (Mask != string.Empty)
			{
				MaskedTextProvider maskTextProvider = new MaskedTextProvider(ConvertMask2DisplayMask(Mask));
				result = maskTextProvider.VerifyString(stringValue);
			}
			return result;
		}

		/// <summary>
		/// Return true if numeric format validations are required.
		/// </summary>
		/// <returns></returns>
		private bool NumericFormatValidationRequired()
		{
			if ( !(DataType == ModelType.Int || DataType == ModelType.Nat || DataType == ModelType.Real))
				return false;

			if (!(mMinDecimalDigits > 0 || mMaxDecimalDigits > 0 || mMinValue != null || mMaxValue != null))
				return false;

			return true;
		}
		#endregion Methods
	}
}
