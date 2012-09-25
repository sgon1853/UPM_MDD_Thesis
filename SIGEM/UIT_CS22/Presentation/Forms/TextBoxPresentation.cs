// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstaction of the .NET TextBox.
	/// </summary>
	public class TextBoxPresentation : EditorPresentation, IEditorPresentation
	{
		#region Members
		/// <summary>
		/// TextBox instance.
		/// </summary>
		private TextBox mTextBoxIT;
		/// <summary>
		/// Text data type.
		/// </summary>
		protected ModelType mDataType;
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
		/// Button instance
		/// </summary>
		private Button mButtonEnlargeIT;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'TextBoxPresentation', specifying data type.
		/// </summary>
		/// <param name="textBox">.NET TextBox reference.</param>
		public TextBoxPresentation(TextBox textBox)
			:base()
		{
			mTextBoxIT = textBox;
			if (mTextBoxIT != null)
			{
				PreviousValue = mTextBoxIT.Text;
				// Create and configure the ErrorProvider control.
				mErrorProvider = new ErrorProvider();
				mErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

				// Link TextBox control events.
				mTextBoxIT.Enter += new EventHandler(HandleTextBoxITEnter);
				mTextBoxIT.Leave += new EventHandler(HandleTextBoxITLeave);
				mTextBoxIT.TextChanged += new EventHandler(HandleTextBoxITTextChanged);
				mTextBoxIT.KeyDown += new KeyEventHandler(HandleTextBoxITKeyDown);
			}
		}
		/// <summary>
		/// Initializes a new instance of the 'TextBoxPresentation' class.
		/// </summary>
		/// <param name="textBox">.NET TextBox reference.</param>
		/// <param name="dataType">Data type of the control.</param>
		public TextBoxPresentation(TextBox textBox, ModelType dataType)
			: this(textBox)
		{
			DataType = dataType;
		}
		/// <summary>
		/// Initializes a new instance of TextBoxPresentation only for Text arguments
		/// </summary>
		/// <param name="textBox">.NET TextBox reference</param>
		/// <param name="button">.NET Button reference</param>
		public TextBoxPresentation(TextBox textBox, Button button)
			: this(textBox, ModelType.Text)
		{
			mTextBoxIT = textBox;
			mButtonEnlargeIT = button;

			if (mButtonEnlargeIT != null)
			{
				mButtonEnlargeIT.Click += new EventHandler(HandleButtonEnlargeITClick);
			}
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets DataType.
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
		/// Gets or sets ValidValues.
		/// </summary>
		public List<string> ValidValues
		{
			get
			{
				return null;
			}
			set
			{
			}
		}
		/// <summary>
		/// Gets or sets Value.
		/// </summary>
		public object Value
		{
			get
			{
				if (mTextBoxIT.Text == string.Empty)
				{
					return null;
				}

				// Check that the data belongs to the specified type.
				if (!DefaultFormats.CheckDataType(mTextBoxIT.Text, mDataType, mNullAllowed))
				{
					return null;
				}
				return mTextBoxIT.Text;
			}
			set
			{
				if (value == null)
				{
					mTextBoxIT.Text = string.Empty;
				}
				else
				{
					mTextBoxIT.Text = DefaultFormats.ApplyInputFormat(value, mDataType);
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
				return mTextBoxIT.Enabled;
			}
			set
			{
				mTextBoxIT.Enabled = value;
			}
		}
		/// <summary>
		/// Gets or sets Focused property.
		/// </summary>
		public bool Focused
		{
			get
			{
				return mTextBoxIT.Focused;
			}
			set
			{
				if (value == true)
				{
					ActivateParentTabPage(mTextBoxIT);
					mTextBoxIT.Focus();
				}
				else if (mTextBoxIT.Parent != null)
				{
					mTextBoxIT.Parent.Focus();
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

				//Set read only mode.
				if (ReadOnly)
				{
					return;
				}

				if (!mNullAllowed)
				{
					mTextBoxIT.BackColor = System.Drawing.SystemColors.Info;
				}
				else
				{
					// Set the back color value.
					if (Enabled)
					{
						mTextBoxIT.BackColor = System.Drawing.SystemColors.Window;
					}
					else
					{
						mTextBoxIT.BackColor = System.Drawing.SystemColors.Control;
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
				return mTextBoxIT.Visible;
			}
			set
			{
				mTextBoxIT.Visible = value;
				if (mButtonEnlargeIT != null)
				{
					mButtonEnlargeIT.Visible = value;
				}
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
				return mTextBoxIT.ReadOnly;
			}
			set
			{
				mTextBoxIT.ReadOnly = value;
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
		/// Executes the actions related to GotFocus event.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EventArgs</param>
		private void HandleTextBoxITEnter(object sender, EventArgs e)
		{
			// Throw got focus event
			OnGotFocus(new GotFocusEventArgs());
		}
		/// <summary>
		/// Executes actions related to LostFocus event.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleTextBoxITLeave(object sender, EventArgs e)
		{
			// Before leaving the control, check if the data belongs to the specified type.
			string lValidateMessage = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR, LanguageConstantValues.L_ERROR);
			if ((this.mTextBoxIT.Text != string.Empty) && (!this.Validate(lValidateMessage)))
			{
				return;
			}

			if (CheckValueChange(mTextBoxIT.Text))
			{
				// Value changed event
				OnValueChanged(new ValueChangedEventArgs());
			}

			PreviousValue = mTextBoxIT.Text;
			// Throw lost focus event.
			OnLostFocus(new LostFocusEventArgs());
		}
		/// <summary>
		/// Executes the action related to the TextChanged event.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleTextBoxITTextChanged(object sender, EventArgs e)
		{
			// Clear the ErrorProvider control.
			if (mErrorProvider != null)
			{
				mErrorProvider.Clear();
			}

			// Raise the change value event if the control has not the focus.
			// If not, wait until it lose the focus.
			if (mTextBoxIT.Focused)
			{
				return;
			}

			// Check that the data belongs to the specified type.
			if (!DefaultFormats.CheckDataType(mTextBoxIT.Text, mDataType, true))
			{
				return;
			}
			if (CheckValueChange(mTextBoxIT.Text))
			{

				// Throw value changed event.
				OnValueChanged(new ValueChangedEventArgs());
				PreviousValue = mTextBoxIT.Text;
			}

		}
		/// <summary>
		/// When button is pressed opens other form to write
		/// </summary>
		/// <param name="sender">Object sender</param>
		/// <param name="e">EventArgs</param>
		private void HandleButtonEnlargeITClick(object sender, EventArgs e)
		{
			InteractionToolkit.TextEnlargeForm lTextForm = new InteractionToolkit.TextEnlargeForm();

			string lText = lTextForm.Initialize(mTextBoxIT.Text, mTextBoxIT.ReadOnly || !mTextBoxIT.Enabled, NullAllowed);

			mTextBoxIT.Text = lText;
		}
		/// <summary>
		/// Suscriber to KeyDown event
		/// </summary>
		/// <param name="sender">Control that raise the event.</param>
		/// <param name="e">Key pressed.</param>
		private void HandleTextBoxITKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (!mTextBoxIT.Multiline)
				{
					OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteRefresh));
				}
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
			if ((this.mErrorProvider != null) && (this.mTextBoxIT != null))
			{
				mErrorProvider.SetIconPadding(this.mTextBoxIT, -20);

				// Null validation.
				if ((!this.NullAllowed) && (this.mTextBoxIT.Text == string.Empty))
				{
					mErrorProvider.SetError(this.mTextBoxIT, defaultErrorMessage);
					// Validation error.
					return false;
				}

				// Format validation.
				if (this.mTextBoxIT.Text != string.Empty)
				{
					if (!DefaultFormats.CheckDataType(this.mTextBoxIT.Text, mDataType, mNullAllowed))
					{
						string lMask = DefaultFormats.GetHelpMask(mDataType, string.Empty);
						mErrorProvider.SetError(this.mTextBoxIT, CultureManager.TranslateString(LanguageConstantKeys.L_VALIDATION_INVALID_FORMAT_MASK, LanguageConstantValues.L_VALIDATION_INVALID_FORMAT_MASK));

						// Validation error.
						return false;
					}
				}
				else
				{
					// Size Validation
					if ((mMaxLength != 0 ) && (mTextBoxIT.Text.Length > mMaxLength))
					{
						mErrorProvider.SetError(this.mTextBoxIT, CultureManager.TranslateString(LanguageConstantKeys.L_VALIDATION_SIZE_WITHOUT_ARGUMENT, LanguageConstantValues.L_VALIDATION_SIZE_WITHOUT_ARGUMENT) + mMaxLength);
						return false;
					}
				}
			}

			// Set the value with the correct format.
			this.Value = Logics.Logic.StringToModel(mDataType, mTextBoxIT.Text);

			// Validation OK.
			return true;
		}
		#endregion Methods
	}
}
