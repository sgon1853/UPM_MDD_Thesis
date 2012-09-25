// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SIGEM.Client.Controllers;

namespace SIGEM.Client.InteractionToolkit
{
	public partial class ConditionalNavigationQuestionForm : Form
	{
		ExchangeInfoConditionalNavigation mExchangeInfoConditional = null;

		public ConditionalNavigationQuestionForm()
		{
			InitializeComponent();

			// Apply MultiLanguage.
			this.lblQuestion.Text = CultureManager.TranslateString(LanguageConstantKeys.L_QUESTION, LanguageConstantValues.L_QUESTION, this.lblQuestion.Text);
			this.btnCancel.Text = CultureManager.TranslateString(LanguageConstantKeys.L_CANCEL, LanguageConstantValues.L_CANCEL, this.btnCancel.Text);
			this.btnOK.Text = CultureManager.TranslateString(LanguageConstantKeys.L_OK, LanguageConstantValues.L_OK, this.btnOK.Text);
			this.Text = CultureManager.TranslateString(LanguageConstantKeys.L_QUESTION, LanguageConstantValues.L_QUESTION, this.Text);

		}

		public IUController Initialize(ExchangeInfo exchangeInfo)
		{
			// ExchangeInfo with Destinations
			mExchangeInfoConditional = exchangeInfo as ExchangeInfoConditionalNavigation;

			if (mExchangeInfoConditional != null)
			{
				this.SuspendLayout();

				Text = CultureManager.TranslateString(LanguageConstantKeys.L_QUESTION, LanguageConstantValues.L_QUESTION, Text);
				lblQuestion.Text = mExchangeInfoConditional.ConditionalNavigationInfo.Question;

				int lCount = 0;
				int lnexty = 0;
				int lseedy = 19;
				int lgrBox = 0;
				foreach (DestinationInfo lDestinationInfo in mExchangeInfoConditional.ConditionalNavigationInfo)
				{
					#region radioButton01
					if (lCount == 0)
					{
						radioButton01.AutoSize = true;
						radioButton01.Size = new System.Drawing.Size(14, 13);
						radioButton01.Location = new System.Drawing.Point(12, lseedy);
						radioButton01.TabIndex = lCount;
						radioButton01.TabStop = true;
						radioButton01.UseVisualStyleBackColor = true;
						radioButton01.Text = lDestinationInfo.AssociatedText;
						radioButton01.Tag = lCount;

						lnexty = radioButton01.Location.Y;
					}
					#endregion radioButton01
					else
					{
						lnexty += lseedy;
						//
						// radioButton
						//
						RadioButton rb = new RadioButton();
						rb.AutoSize = true;
						rb.Location = new System.Drawing.Point(12, lnexty);
						rb.Size = new System.Drawing.Size(14, 13);
						rb.Location = new System.Drawing.Point(12, lnexty);
						rb.Name = "radioButton" + lCount.ToString();
						rb.TabIndex = lCount;
						rb.TabStop = true;
						rb.UseVisualStyleBackColor = true;
						rb.Text = lDestinationInfo.AssociatedText;
						rb.Tag = lCount;

						grpBoxDestinations.Controls.Add(rb);


						lgrBox += (lnexty + 10);
					}
					lCount++;
				}

				// Adjust the form size
				this.Size = new Size(this.Size.Width, grpBoxDestinations.Top + panel2.Height + (lCount + 2) * lseedy);
				// Select the first option
				radioButton01.Checked = true;
				this.ResumeLayout();
			}
			return null;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			foreach (Control control in grpBoxDestinations.Controls)
			{
				RadioButton rb = control as RadioButton;
				if (rb != null)
				{
					if (rb.Checked)
					{
						int selected = (int)rb.Tag;
						mExchangeInfoConditional.SelectDestinationInfo(selected);
						Close();
					}
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}

