// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction used to represent a blob editor.
	/// </summary>
    public class BlobBoxPresentation :EditorPresentation, IEditorPresentation
	{
		#region Members
		/// <summary>
		/// MaskedTextBox instance.
		/// </summary>
		private MaskedTextBox mMaskedTextBoxIT;
		/// <summary>
		/// Blob data.
		/// </summary>
		private byte[] mBlobData = null;
		/// <summary>
		/// OpenFile dialog.
		/// </summary>
		private OpenFileDialog mOpenfile = new OpenFileDialog();
		/// <summary>
		/// Selection button.
		/// </summary>
		private Button mSelectionButton = null;
		/// <summary>
		/// If the control allows null values.
		/// </summary>
		private bool mNullAllowed = false;
		/// <summary>
		/// Control flag that disables 'TextValueChange' events.
		/// </summary>
		private bool mBlobIsFromController = false;
		/// <summary>
		/// Error provider for validating data.
		/// </summary>
		private ErrorProvider mErrorProvider;
		private string mDataMessage = "<< Data >>";
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'BlobBoxPresentation' class.
		/// </summary>
		/// <param name="maskedTextBox">.NET MaskedTextBox reference.</param>
		/// <param name="selectionButton">.NET Button reference used as selector or trigger.</param>
		public BlobBoxPresentation(MaskedTextBox maskedTextBox, Button selectionButton)
		{
			mMaskedTextBoxIT = maskedTextBox;
			if (mMaskedTextBoxIT != null)
			{
				// Create and configure the ErrorProvider control.
				mErrorProvider = new ErrorProvider();
				mErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

				// Link MaskedTextBox control events.
				mMaskedTextBoxIT.GotFocus += new EventHandler(HandleMaskedTextBoxITEnter);
				mMaskedTextBoxIT.LostFocus += new EventHandler(HandleMaskedTextBoxITLeave);
				mMaskedTextBoxIT.TextChanged += new EventHandler(HandleMaskedTextBoxITTextChanged);
				mMaskedTextBoxIT.EnabledChanged += new EventHandler(HandleMaskedTextBoxITEnabledChanged);
			}

			mSelectionButton = selectionButton;
			if (mSelectionButton != null)
			{
				mSelectionButton.Click += new EventHandler(HandleFileButtonClick);
			}

			mOpenfile.FileOk += new System.ComponentModel.CancelEventHandler(HandleOpenFileOkClick);
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
				return ModelType.Blob;
			}
			set
			{
			}
		}
		/// <summary>
		/// Gets the Mask.
		/// </summary>
		public string Mask
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}
		/// <summary>
		/// Gets or sets the value of the control.
		/// </summary>
		public object Value
		{
			get
			{

				return mBlobData;
			}
			set
			{
				mMaskedTextBoxIT.Text = string.Empty;
				mBlobIsFromController = true;
				mBlobData = value as byte[];
				if (mBlobData != null)
				{
					mMaskedTextBoxIT.Enabled = false;
					mMaskedTextBoxIT.Text = mDataMessage;
				}
				mBlobIsFromController = false;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the control is enabled or not.
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
				mSelectionButton.Enabled = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the control allows null values or not.
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
		/// Gets or sets a boolean value indicating whether the control has the focus.
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
		/// Gets or sets a boolean value indicating whether the control is visible or not.
		/// </summary>
		public bool Visible
		{
			get
			{
				return (mMaskedTextBoxIT.Visible & mSelectionButton.Visible);
			}
			set
			{
				mMaskedTextBoxIT.Visible = value;
				mSelectionButton.Visible = value;
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
				if (!value)
				{
					mSelectionButton.Enabled = false;
				}
				else
				{
					mSelectionButton.Enabled = true;
				}
			}
		}
		#endregion Properties

		#region Events
		/// <summary>
		/// No commands associated.
		/// </summary>
		public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
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
		/// Occurs when the enabled property of the control is changed.
		/// </summary>
		public event EventHandler<EnabledChangedEventArgs> EnableChanged;
		#endregion Events

		#region Events Handlers

		/// <summary>
		/// Executes the actions related to EnableChanged event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleMaskedTextBoxITEnabledChanged(object sender, EventArgs e)
		{
			if (!mBlobIsFromController)
			{
				OnEnableChanged(new EnabledChangedEventArgs());
			}
		}

		/// <summary>
		/// Executes the actions related to GotFocus event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleMaskedTextBoxITEnter(object sender, EventArgs e)
		{
			OnGotFocus(new GotFocusEventArgs());
		}
		/// <summary>
		/// Executes the actions related to LostFocus event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleMaskedTextBoxITLeave(object sender, EventArgs e)
		{
			// Before leaving the control, check if the data belongs to the specified type.
			string lMessage = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR, LanguageConstantValues.L_ERROR);
			if ((this.mMaskedTextBoxIT.Text != string.Empty) && (!this.Validate(lMessage)))
			{
				mBlobData = null;
				return;
			}

			// Raise the LostFocus event.
			// The Controller will decide if the current value is different of the previous.
			OnValueChanged(new ValueChangedEventArgs());

			// Throw lost focus event.
			if (LostFocus != null)
			{
				LostFocus(this, new LostFocusEventArgs());
			}
		}
		/// <summary>
		/// Executes the actions related to TextChanged event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleMaskedTextBoxITTextChanged(object sender, EventArgs e)
		{
			// Clear the ErrorProvider control.
			if (mErrorProvider != null)
			{
				mErrorProvider.Clear();
			}
			// Raise the LostFocus event if the control has teh focus,
			// if not, wait for losing the focus in order to raise it.
			if (mMaskedTextBoxIT.Focused)
			{
				return;
			}

			// Throw value changed event.
			OnValueChanged(new ValueChangedEventArgs());
		}
		/// <summary>
		/// Selection button click.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EventArgs.</param>
		private void HandleFileButtonClick(object sender, EventArgs e)
		{
			mOpenfile.ShowDialog();
		}
		/// <summary>
		/// Handles the Open file Ok click event.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">CancelEventArgs.</param>
		private void HandleOpenFileOkClick(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (ValidatePrivileges(mOpenfile.FileName))
			{
				mMaskedTextBoxIT.Text = mOpenfile.FileName;
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
			// Clear the ErrorProvider control.

			if (mBlobIsFromController)
			{
				return;
			}

			mBlobData = ReadFile(mMaskedTextBoxIT.Text);

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
			if ((this.mErrorProvider != null) && (this.mMaskedTextBoxIT != null))
			{
				if ((this.NullAllowed) && (this.mMaskedTextBoxIT.Text == string.Empty))
					return true;
				// Null validation.
				if ((!this.NullAllowed) && (this.mMaskedTextBoxIT.Text == string.Empty) && (this.Value == null))
				{
					SetMessageError(defaultErrorMessage);
					return false;
				}
				// File not loaded
				if ((string.Compare(mMaskedTextBoxIT.Text, mDataMessage, true) == 0) && (this.Value == null))
				{
					SetMessageError(LanguageConstantKeys.L_FILE_NOT_LOADED, LanguageConstantValues.L_FILE_NOT_LOADED);
					return false;
				}
				// File do not exist
				if ((string.Compare(mMaskedTextBoxIT.Text, mDataMessage, true) != 0) && (!File.Exists(mMaskedTextBoxIT.Text)))
				{
					SetMessageError(LanguageConstantKeys.L_FILE_NOT_EXIST, LanguageConstantValues.L_FILE_NOT_EXIST);
					return false;
				}
				if ((string.Compare(mMaskedTextBoxIT.Text, mDataMessage, true) != 0) &&  (!ValidatePrivileges(mMaskedTextBoxIT.Text)))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Set Message error on Error provider.
		/// </summary>
		/// <param name="messageError">Message to display on tooltip for error provider.</param>
		protected virtual void SetMessageError(string messageError)
		{
			SetMessageError(null, messageError, messageError);
		}
		/// <summary>
		///  Translate and set Message error on Error provider.
		/// </summary>
		/// <param name="idXML">Unique XML resource identifier.</param>
		/// <param name="defaultString">Default string returned if the resource is not found.</param>
		protected virtual void SetMessageError(string idXML, string defaultString)
		{
			SetMessageError(idXML, defaultString, defaultString);
		}
		/// <summary>
		///  Translate and set Message error on Error provider.
		/// </summary>
		/// <param name="idXML">Unique XML resource identifier.</param>
		/// <param name="defaultString">Default string returned if the resource is not found.</param>
		/// <param name="interactionToolkitAlias">Current alias of the interactiontoolkit layer.</param>
		protected virtual void SetMessageError(string idXML, string defaultString, string interactionToolKit)
		{
			string lMessageError = CultureManager.TranslateString(idXML, defaultString,interactionToolKit);
			mErrorProvider.SetError(this.mMaskedTextBoxIT, lMessageError );
			mErrorProvider.SetIconPadding(this.mMaskedTextBoxIT, -20);
		}
		/// <summary>
		/// Validates the privileges of the File.
		/// </summary>
		/// <param name="path">The path file.</param>
		/// <returns>Boolean value indicating if the validation was ok or not.</returns>
		public bool ValidatePrivileges(string path)
		{
			try
				{
					File.OpenRead(path);
				}
				catch
				{
					// File without read privileges.
					string lMessageError = CultureManager.TranslateString(LanguageConstantKeys.L_FILE_NOT_PRIVILEGES, LanguageConstantValues.L_FILE_NOT_PRIVILEGES);
					mErrorProvider.SetError(this.mMaskedTextBoxIT, lMessageError);
					mErrorProvider.SetIconPadding(this.mMaskedTextBoxIT, -20);
					return false;
				}

			return true;
		}


		/// <summary>
		/// Reads the content of the file specified as parameter.
		/// </summary>
		/// <param name="pathFile">Name of the file to read.</param>
		/// <returns>A binary array.</returns>
		private byte[] ReadFile(string pathFile)
		{
			byte[] lResult = null;

			if (string.Compare(pathFile, mDataMessage, true) == 0)
			{
				return mBlobData;
			}
			else
			{
				if (File.Exists(pathFile))
				{
					using (FileStream lfile = File.OpenRead(pathFile))
					{
						lResult = new byte[lfile.Length];
						lfile.Read(lResult, 0, lResult.Length);
					}
				}
			}
			return lResult;
		}
		#endregion Methods
	}
}
