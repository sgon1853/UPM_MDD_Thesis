// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of the .NET Password control.
	/// </summary>
	public class PasswordPresentation: IEditorPresentation
	{
		#region Members
		/// <summary>
		/// MaskedTextBox control for password
		/// </summary>
		private MaskedTextBox mMaskedTextBoxPasswordIT;
		/// <summary>
		/// MaskedTextBox presentation instance for password.
		/// </summary>
		private MaskedTextBoxPresentation mMaskedTextBoxPasswordPresentation;
		/// <summary>
		/// MaskedTextBox control for confirmation
		/// </summary>
		private MaskedTextBox mMaskedTextBoxConfirmIT;
		/// <summary>
		/// MaskedTextBox presentation instance for confirm.
		/// </summary>
		private MaskedTextBoxPresentation mMaskedTextBoxConfirmPresentation;
		/// <summary>
		/// Error provider for validating data.
		/// </summary>
		private ErrorProvider mErrorProvider;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'PasswordPresentation' class.
		/// </summary>
		/// <param name="passwordTextBox">.NET MaskedTextBox reference fot password.</param>
		/// <param name="confirmTextBox">.NET MaskedTextBox reference for confirmation.</param>
		public PasswordPresentation(MaskedTextBox passwordTextBox, MaskedTextBox confirmTextBox)
		{
			mMaskedTextBoxPasswordIT = passwordTextBox;
			mMaskedTextBoxPasswordPresentation = new MaskedTextBoxPresentation(passwordTextBox);
			mMaskedTextBoxConfirmIT = confirmTextBox;
			mMaskedTextBoxConfirmPresentation = new MaskedTextBoxPresentation(confirmTextBox);

			// Create and configure the ErrorProvider control.
			mErrorProvider = new ErrorProvider();
			mErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

			// Link MaskedTextBox control events.
			mMaskedTextBoxPasswordPresentation.GotFocus += new EventHandler<GotFocusEventArgs>(HandlePasswordPresenationGotFocus);
			mMaskedTextBoxPasswordPresentation.LostFocus += new EventHandler<LostFocusEventArgs>(HandlePasswordPresenationLostFocus);
			mMaskedTextBoxPasswordPresentation.ValueChanged += new EventHandler<ValueChangedEventArgs>(HandlePasswordPresenationValueChanged);
			mMaskedTextBoxPasswordPresentation.EnableChanged += new EventHandler<EnabledChangedEventArgs>(HandlePasswordPresenationEnabledChanged);
		}
		/// <summary>
		/// Initializes a new instance of the 'PasswordPresentation' class.
		/// </summary>
		/// <param name="passwordTextBox">.NET MaskedTextBox reference fot password.</param>
		/// <param name="confirmTextBox">.NET MaskedTextBox reference for confirmation.</param>
		/// <param name="dataType">Data type.</param>
		/// <param name="mask">Introduction mask.</param>
		public PasswordPresentation(MaskedTextBox maskedTextBox, MaskedTextBox confirmTextBox, ModelType dataType, string mask)
			: this(maskedTextBox, confirmTextBox)
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
				return mMaskedTextBoxPasswordPresentation.DataType;
			}
			set
			{
				mMaskedTextBoxPasswordPresentation.DataType = value;
				mMaskedTextBoxConfirmPresentation.DataType = value;
			}
		}
		/// <summary>
		/// Gets or sets mask.
		/// </summary>
		public string Mask
		{
			get
			{
				return mMaskedTextBoxPasswordPresentation.Mask;
			}
			set
			{
				mMaskedTextBoxPasswordPresentation.Mask = value;
			}
		}
		/// <summary>
		/// Gets or sets value.
		/// </summary>
		public object Value
		{
			get
			{
				return mMaskedTextBoxPasswordPresentation.Value;
			}
			set
			{
				if (value == null)
				{
					mMaskedTextBoxPasswordPresentation.Value = value;
					mMaskedTextBoxConfirmPresentation.Value = value;
				}
				else
				{
					mMaskedTextBoxPasswordPresentation.Value = value;
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
				return (mMaskedTextBoxPasswordPresentation.Enabled & mMaskedTextBoxConfirmPresentation.Enabled);
			}
			set
			{
				mMaskedTextBoxPasswordPresentation.Enabled = value;
				mMaskedTextBoxConfirmPresentation.Enabled = value;
			}
		}
		/// <summary>
		/// Gets or sets NullAllowed property.
		/// </summary>
		public bool NullAllowed
		{
			get
			{
				return mMaskedTextBoxPasswordPresentation.NullAllowed;
			}
			set
			{
				mMaskedTextBoxPasswordPresentation.NullAllowed = value;
				mMaskedTextBoxConfirmPresentation.NullAllowed = value;
				if (value)
				{
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
				return mMaskedTextBoxPasswordPresentation.Focused;
			}
			set
			{
				mMaskedTextBoxPasswordPresentation.Focused = value;
			}
		}
		/// <summary>
		/// Gets or sets Visible property.
		/// </summary>
		public bool Visible
		{
			get
			{
				return (mMaskedTextBoxPasswordPresentation.Visible & mMaskedTextBoxConfirmPresentation.Visible);
			}
			set
			{
				mMaskedTextBoxPasswordPresentation.Visible = value;
				mMaskedTextBoxConfirmPresentation.Visible = value;
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the max legth of the control value.
		/// </summary>
		public int MaxLength
		{
			get
			{
				return mMaskedTextBoxPasswordPresentation.MaxLength;
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
				return mMaskedTextBoxPasswordPresentation.ReadOnly;
			}
			set
			{
				mMaskedTextBoxPasswordPresentation.ReadOnly = value;
				mMaskedTextBoxConfirmPresentation.ReadOnly = value;
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
		/// No Commands associated.
		/// </summary>
		public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Handles the Enabled Changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandlePasswordPresenationEnabledChanged(object sender, EnabledChangedEventArgs e)
		{
			OnEnableChanged(e);
		}
		/// <summary>
		/// Handles the Value changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandlePasswordPresenationValueChanged(object sender, ValueChangedEventArgs e)
		{
			// Clear the error provider
			if (mErrorProvider != null)
			{
				mErrorProvider.Clear();
			}
			OnValueChanged(e);
		}
		/// <summary>
		/// Handles the Lost Focus event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandlePasswordPresenationLostFocus(object sender, LostFocusEventArgs e)
		{
			OnLostFocus(e);
		}
		/// <summary>
		/// Handles the GotFocus event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandlePasswordPresenationGotFocus(object sender, GotFocusEventArgs e)
		{
			OnGotFocus(e);
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
			if ((this.mErrorProvider != null) && (this.mMaskedTextBoxPasswordPresentation != null) && (this.mMaskedTextBoxConfirmPresentation != null))
			{
				mErrorProvider.SetIconPadding(mMaskedTextBoxPasswordIT, -20);
				mErrorProvider.SetIconPadding(mMaskedTextBoxConfirmIT, -20);

				// Null validation.
				if ((!this.NullAllowed) && (mMaskedTextBoxPasswordPresentation.Value == null))
				{
					mErrorProvider.SetError(this.mMaskedTextBoxPasswordIT, defaultErrorMessage);
					mErrorProvider.SetError(this.mMaskedTextBoxConfirmIT , defaultErrorMessage);

					// Validation error.
					return false;
				}
				else if ((this.NullAllowed) &&
					(mMaskedTextBoxPasswordPresentation.Value == null) && (mMaskedTextBoxConfirmPresentation.Value == null))
				{
					// Both password and confirm presentations are null, and the argument allows null.
					// Validation OK.
					return true;
				}

				// Make sure that both editors (Password and confirm) have the same value.
				if (!mMaskedTextBoxPasswordPresentation.Value.Equals(mMaskedTextBoxConfirmPresentation.Value))
				{
					mErrorProvider.SetError(this.mMaskedTextBoxPasswordIT, CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_NEW_AND_RETYPED_NOT_EQUAL, LanguageConstantValues.L_PASSWORD_NEW_AND_RETYPED_NOT_EQUAL));
					mErrorProvider.SetError(this.mMaskedTextBoxConfirmIT, CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_NEW_AND_RETYPED_NOT_EQUAL, LanguageConstantValues.L_PASSWORD_NEW_AND_RETYPED_NOT_EQUAL));

					// Validation error.
					return false;
				}
			}

			// Validation OK.
			return true;
		}
		#endregion Methods
	}
}
