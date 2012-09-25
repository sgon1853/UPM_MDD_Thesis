// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of the .NET CheckBox control.
	/// </summary>
	public class CheckBoxPresentation: EditorPresentation, IEditorPresentation
	{
		#region Members
		/// <summary>
		/// CheckBox instance.
		/// </summary>
		private CheckBox mCheckBoxIT;
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
		/// Initializes a new instance of the 'CheckBoxPresentation' class.
		/// </summary>
		/// <param name="button">.NET CheckBox reference.</param>
		public CheckBoxPresentation(CheckBox checkBox)
			:base()
		{
			mCheckBoxIT = checkBox;
			if (mCheckBoxIT != null)
			{
				// Set the previous value.
				PreviousValue = mCheckBoxIT.CheckState;

				// Create and configure the ErrorProvider control.
				mErrorProvider = new ErrorProvider();
				mErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

				// Link MaskedTextBox control events.
				mCheckBoxIT.GotFocus += new EventHandler(HandleCheckBoxITGotFocus);
				mCheckBoxIT.LostFocus += new EventHandler(HandleCheckBoxITLostFocus);
				mCheckBoxIT.CheckStateChanged += new EventHandler(HandleCheckBoxITStateChanged);
				mCheckBoxIT.KeyDown += new KeyEventHandler(HandleCheckBoxITKeyDown);
			}
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets the ModelType.
		/// </summary>
		public ModelType DataType
		{
			get
			{
				return ModelType.Bool;
			}
			set
			{
			}
		}
		/// <summary>
		/// Gets the list of valid values for the control.
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
		/// Gets or sets a value indicating whether the control allows null values.
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
				mCheckBoxIT.ThreeState = value;

				//Set read only mode.
				if (ReadOnly)
				{
					return;
				}
				if (!mNullAllowed)
				{
					mCheckBoxIT.BackColor = System.Drawing.SystemColors.Info;
				}
				else
				{
					// Set the back color value.
					if (Enabled)
					{
						mCheckBoxIT.BackColor = System.Drawing.SystemColors.Window;
					}
					else
					{
						mCheckBoxIT.BackColor = System.Drawing.SystemColors.Control;
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
		/// Gets or sets the value of the control.
		/// </summary>
		public object Value
		{
			get
			{
				if (mCheckBoxIT.CheckState == CheckState.Indeterminate)
				{
					return null;
				}
				else
				{
					return (mCheckBoxIT.CheckState == CheckState.Checked);
				}
			}
			set
			{
				if (value == null)
				{
					if (mNullAllowed)
					{
						mCheckBoxIT.CheckState = CheckState.Indeterminate;
					}
				}
				else
				{
					if (value.Equals(true))
					{
						mCheckBoxIT.CheckState = CheckState.Checked;
					}
					else
					{
						mCheckBoxIT.CheckState = CheckState.Unchecked;
					}
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the control is visible or not.
		/// </summary>
		public bool Visible
		{
			get
			{
				return mCheckBoxIT.Visible;
			}
			set
			{
				mCheckBoxIT.Visible = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the control is enabled or not.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return mCheckBoxIT.Enabled;
			}
			set
			{
				mCheckBoxIT.Enabled = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the control has the focus.
		/// </summary>
		public bool Focused
		{
			get
			{
				return mCheckBoxIT.Focused;
			}
			set
			{
				if (value == true)
				{
					mCheckBoxIT.Focus();
				}
				else if (mCheckBoxIT.Parent != null)
				{
					mCheckBoxIT.Parent.Focus();
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
		/// Occurs when the control loses focus.
		/// </summary>
		public event EventHandler<LostFocusEventArgs> LostFocus;
		/// <summary>
		/// Occurs when the value of the control is changed.
		/// </summary>
		public event EventHandler<ValueChangedEventArgs> ValueChanged;
		/// <summary>
		/// Occurs when the enable property of the control is changed.
		/// </summary>
		public event EventHandler<EnabledChangedEventArgs> EnableChanged;
		/// <summary>
		/// Never occurs
		/// </summary>
		public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Executes the actions related to GotFocus event.
		/// </summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleCheckBoxITGotFocus(object sender, EventArgs e)
		{
			OnGotFocus(new GotFocusEventArgs());
		}
		/// <summary>
		/// Occurs when the control loses the focus.
		/// </summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleCheckBoxITLostFocus(object sender, EventArgs e)
		{
			// Before leaving the control, check if the data belongs to the specified type.
			string lValidateMessage = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR, LanguageConstantValues.L_ERROR);
			if (!this.Validate(lValidateMessage))
			{
				return;
			}

			OnLostFocus(new LostFocusEventArgs());
		}
		/// <summary>
		/// Executes the actions related to ValueChanged event.
		/// </summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleCheckBoxITStateChanged(object sender, EventArgs e)
		{
			// Clear the ErrorProvider control.
			if (mErrorProvider != null)
			{
				mErrorProvider.Clear();
			}

			if (CheckValueChange(mCheckBoxIT.CheckState))
			{
				// Value changed event
				OnValueChanged(new ValueChangedEventArgs());
			}

			PreviousValue = mCheckBoxIT.CheckState;
		}

		/// <summary>
		/// Suscriber to KeyDown event
		/// </summary>
		/// <param name="sender">Control that raise the event.</param>
		/// <param name="e">Key pressed.</param>
		private void HandleCheckBoxITKeyDown(object sender, KeyEventArgs e)
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
			bool lResult = true;

			if ((this.mErrorProvider != null) && (this.mCheckBoxIT != null))
			{
				// Null validation.
				if ((!this.NullAllowed) && (this.Value == null))
				{
					mErrorProvider.SetError(this.mCheckBoxIT, defaultErrorMessage);
					lResult = false;
				}
			}

			return lResult;
		}
		#endregion Methods
	}
}
