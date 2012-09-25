// v3.8.4.5.b
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

using SIGEM.Client.Presentation;
using SIGEM.Client.Presentation.Forms;
using SIGEM.Client.Controllers;
using SIGEM.Client.Oids;

namespace SIGEM.Client.InteractionToolkit
{
	public partial class ChangePasswordForm : Form
	{
		private ErrorProvider lErrorProvider = new ErrorProvider();

		public ChangePasswordForm()
		{
			InitializeComponent();

			// Icon assignament.
			this.Icon = UtilFunctions.BitmapToIcon(Properties.Resources.changePassword);

			// Apply MultiLanguage.
			this.mbOK.Text = CultureManager.TranslateString(LanguageConstantKeys.L_OK, LanguageConstantValues.L_OK, this.mbOK.Text);
			this.mbCancel.Text = CultureManager.TranslateString(LanguageConstantKeys.L_CANCEL, LanguageConstantValues.L_CANCEL, this.mbCancel.Text);
			this.mlblRetypeNewPass.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_RETYPE, LanguageConstantValues.L_PASSWORD_RETYPE, this.mlblRetypeNewPass.Text);
			this.mlblNewPass.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_NEW, LanguageConstantValues.L_PASSWORD_NEW, this.mlblNewPass.Text);
			this.mlblCurrentPass.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_CURRENT, LanguageConstantValues.L_PASSWORD_CURRENT, this.mlblCurrentPass.Text);
			this.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_CAPTION, LanguageConstantValues.L_PASSWORD_CAPTION, this.Text);

			mTextBoxCurrenPas.Focus();
		}

		private void mbOK_Click(object sender, EventArgs e)
		{
			bool lbIsNull = false;
			lErrorProvider.Clear();
			lErrorProvider.BlinkRate = 500;
			lErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

			#region Validate that the values are not empty
			if (mTextBoxRetypeNewPass.Text == "")
			{
				mTextBoxRetypeNewPass.Focus();
				string lErrorMessage = CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_RETYPE_NEW_NOT_NULL, LanguageConstantValues.L_PASSWORD_RETYPE_NEW_NOT_NULL);
				lErrorProvider.SetError(mTextBoxRetypeNewPass, lErrorMessage);
				lbIsNull = true;
			}
			if (mTextBoxNewPass.Text == "")
			{
				mTextBoxNewPass.Focus();
				string lErrorMessage = CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_NEW_NOT_NULL, LanguageConstantValues.L_PASSWORD_NEW_NOT_NULL);
				lErrorProvider.SetError(mTextBoxNewPass, lErrorMessage );
				lbIsNull = true;
			}
			if (mTextBoxCurrenPas.Text == "")
			{
				mTextBoxCurrenPas.Focus();
				string lErrorMessage = CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_CURRENT_NOT_NULL, LanguageConstantValues.L_PASSWORD_CURRENT_NOT_NULL);
				lErrorProvider.SetError(mTextBoxCurrenPas, lErrorMessage );
				lbIsNull = true;
			}
			// There is an empty value
			if (lbIsNull == true)
			{
				return;
			}
			#endregion Validate that the values are not null.

			// Validate the new password retyped
			if (mTextBoxNewPass.Text != mTextBoxRetypeNewPass.Text)
			{
				mTextBoxNewPass.Focus();
				string lText = CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_NEW_AND_RETYPED_NOT_EQUAL, LanguageConstantValues.L_PASSWORD_NEW_AND_RETYPED_NOT_EQUAL);
				string lMessage = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR, LanguageConstantValues.L_ERROR);

				MessageBox.Show(lText, lMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Validate that the current password is correct.
			try
			{
				SIGEM.Client.Logics.Logic.Adaptor.Authenticate(SIGEM.Client.Logics.Logic.Agent, mTextBoxCurrenPas.Text);
			}
			catch
			{
				mTextBoxCurrenPas.Focus();
				string lText = CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_INCORRECT, LanguageConstantValues.L_PASSWORD_INCORRECT);
				string lMessage = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR, LanguageConstantValues.L_ERROR);
				MessageBox.Show(lText, lMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Update the new password
			try
			{
				if (SIGEM.Client.Logics.Logic.Adaptor.ChangePassWord(SIGEM.Client.Logics.Logic.Agent, mTextBoxCurrenPas.Text, mTextBoxNewPass.Text))
				{
					string lText = CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_UPDATED, LanguageConstantValues.L_PASSWORD_UPDATED);
					string lMessage = CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_CAPTION, LanguageConstantValues.L_PASSWORD_CAPTION);
					MessageBox.Show(lText, lMessage , MessageBoxButtons.OK, MessageBoxIcon.Information);
					Close();
				}
			}
			catch
			{
				string lText = CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_UPDATE_FAILED, LanguageConstantValues.L_PASSWORD_UPDATE_FAILED);
				string lMessage = CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_CAPTION, LanguageConstantValues.L_PASSWORD_CAPTION);
				MessageBox.Show(lText, lMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void mbCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		protected override void OnClosed(EventArgs e)
		{
			lErrorProvider.Dispose();
			base.OnClosed(e);
		}
	}
}

