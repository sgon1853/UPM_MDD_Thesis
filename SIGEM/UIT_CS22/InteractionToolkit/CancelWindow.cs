// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIGEM.Client
{
	public partial class CancelWindow : Form
	{
		private bool mCancel = false;

		public CancelWindow()
		{
			InitializeComponent();
			
			// Apply MultiLanguage.
			this.btnCancel.Text = CultureManager.TranslateString(LanguageConstantKeys.L_CANCEL, LanguageConstantValues.L_CANCEL, this.btnCancel.Text);
			this.Text = CultureManager.TranslateString(LanguageConstantKeys.L_MULTIEXE_PROCESSING, LanguageConstantValues.L_MULTIEXE_PROCESSING, this.Text);
		}

		public bool Cancel
		{
			get
			{
				return mCancel;
			}
		set
			{
				mCancel = value;
			}
		}

		public int Value
		{
			set
			{
				System.Windows.Forms.Application.DoEvents();
				try
				{
					prgBarExecution.Value = value;
					Refresh();
				}
				catch { }
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			mCancel = true;
		}
		public int MaximunProgressBarValue
		{
			set
			{
				prgBarExecution.Maximum = value;
			}
		}
	}
}

