// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SIGEM.Client.Presentation.Forms
{
	public class AutonumericPresentation: MaskedTextBoxPresentation
	{
		#region Members
		/// <summary>
		/// "Auto" label.
		/// </summary>
		public Label mAutoLabelIT;
		/// <summary>
		/// CheckBox.
		/// </summary>
		public CheckBox mCheckBoxIT;
		/// <summary>
		/// Indicates if the Presentation is configured in Auto mode or not.
		/// </summary>
		private bool mAutoMode;
		/// <summary>
		/// Old value.
		/// </summary>
		public string mOldValue = string.Empty;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Creates a new instance for an autonumeric
		/// </summary>
		/// <param name="maskedTextBox"></param>
		/// <param name="label"></param>
		/// <param name="checkBox"></param>
		public AutonumericPresentation(MaskedTextBox maskedTextBox, Label label, CheckBox checkBox)
			: base(maskedTextBox)
		{
			mAutoMode = true;
			mAutoLabelIT = label;
			mCheckBoxIT = checkBox;
			DataType = ModelType.Autonumeric;

			if (mCheckBoxIT != null)
			{
				mCheckBoxIT.CheckedChanged += new EventHandler(HandleCheckBoxITChanged);
			}
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets value.
		/// </summary>
		public override object Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				if (value == null)
				{
					if (mAutoMode)
					{
						if (!mCheckBoxIT.Checked)
						{
							mCheckBoxIT.Checked = true;
						}
					}
					else if (!mCheckBoxIT.Checked && Value != null)
					{
						mCheckBoxIT.Checked = true;
					}
				}
				else
				{
					base.Value = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the control is enabled or not.
		/// </summary>
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				if (mAutoMode)
				{
					// Disable the "Auto" label.
					this.mAutoLabelIT.Enabled = value;
				}
				// Disable the editor control.
				base.Enabled = value;
				mCheckBoxIT.Enabled = value;
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether the control allows null values.
		/// </summary>
		public override bool NullAllowed
		{
			get
			{
				return base.NullAllowed;
			}
			set
			{
				base.NullAllowed = value;

			}
		}
		/// <summary>
		/// Gets or sets Focused property.
		/// </summary>
		public override bool Focused
		{
			get
			{
				return base.Focused;
			}
			set
			{
				if (mAutoMode)
				{
                    if (value)
                    {
                        ActivateParentTabPage(mCheckBoxIT);
                        this.mCheckBoxIT.Focus();
                    }
                    
				}
				else
				{
					base.Focused = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the control is visible or not.
		/// </summary>
		public override bool Visible
		{
			get
			{
				return mCheckBoxIT.Visible;
			}
			set
			{
				if (mAutoMode)
				{
					// Set the visible value for the "Auto" label control.
					mAutoLabelIT.Visible = value;
				}
				else
				{
					// Set the visible value for the editor control.
					mMaskedTextBoxIT.Visible = value;
				}
				mCheckBoxIT.Visible = value;
			}
		}
		#endregion Properties

		#region Events Handlers
		/// <summary>
		/// Occurs cheked property of the Checkbox changes between posts to the server.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EventArgs class.</param>
		private void HandleCheckBoxITChanged(object sender, EventArgs e)
		{
			if (mCheckBoxIT.Checked)
			{
				mAutoMode = true;
				mAutoLabelIT.Visible = true;
				mMaskedTextBoxIT.Visible = false;
				if (mMaskedTextBoxIT.Text == "-1")
				{
					mOldValue = string.Empty;
				}
				else
				{
					mOldValue = mMaskedTextBoxIT.Text;
				}
				mMaskedTextBoxIT.Text = "-1";
			}
			else
			{
				mAutoMode = false;
				mAutoLabelIT.Visible = false;
				mMaskedTextBoxIT.Visible = true;
				if (mMaskedTextBoxIT.Text == "-1")
				{
					mMaskedTextBoxIT.Text = mOldValue;
				}
				mMaskedTextBoxIT.Focus();
			}
		}
		#endregion Events Handlers

		#region Methods
		/// <summary>
		/// Occurs when the content of the masked text box changes between posts to the server.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EventArgs class.</param>
		protected override void ProcessMaskedTextBoxITTextChanged(object sender, EventArgs e)
		{
			if (!mMaskedTextBoxIT.Visible && mMaskedTextBoxIT.Text != "-1")
			{
				mCheckBoxIT.Checked = false;
			}

			base.ProcessMaskedTextBoxITTextChanged(sender, e);
		}

		/// <summary>
		/// Executes actions related to LostFocus event.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EventArgs.</param>
		protected override void ProcessMaskedTextBoxITLeave(object sender, EventArgs e)
		{
			if (mMaskedTextBoxIT.Visible && mMaskedTextBoxIT.Text == "-1")
			{
				mCheckBoxIT.Checked = true;
			}

			base.ProcessMaskedTextBoxITLeave(sender, e);
		}
		#endregion Methods
	}
}
