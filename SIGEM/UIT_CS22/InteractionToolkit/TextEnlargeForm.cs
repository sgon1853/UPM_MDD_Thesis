// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SIGEM.Client.Presentation;
using SIGEM.Client.InteractionToolkit;

namespace SIGEM.Client.InteractionToolkit
{
    public partial class TextEnlargeForm : Form
    {

        #region Members
        /// <summary>
        /// Text received
        /// </summary>
        string mOriginalText = "";

        /// <summary>
        /// Ok button has been pressed
        /// </summary>
        bool mOkPressed = false;
        #endregion Members

        public TextEnlargeForm()
        {
            InitializeComponent();

			// Icon assignament.
	    this.Icon = UtilFunctions.BitmapToIcon(Properties.Resources.enlarge);

            // Apply MultiLanguage.
            this.btnOk.Text = CultureManager.TranslateString(LanguageConstantKeys.L_OK, LanguageConstantValues.L_OK, this.btnOk.Text);
            this.btnCancel.Text = CultureManager.TranslateString(LanguageConstantKeys.L_CANCEL, LanguageConstantValues.L_CANCEL, this.btnCancel.Text);

        }

        public string Initialize(string lText, bool readOnly, bool nullAllowed)
        {
            mOriginalText = lText;
            this.txtEnlarge.Text = lText;
            if (readOnly)
            {
                this.txtEnlarge.ReadOnly = true;
                this.btnOk.Visible = false;
            }
            else
            {
                this.txtEnlarge.ReadOnly = false;
                this.btnOk.Visible = true;
            }

            if (!nullAllowed)
            {
                this.txtEnlarge.BackColor = System.Drawing.SystemColors.Info;
            }

            ShowDialog();

            if (mOkPressed)
                return txtEnlarge.Text;
            else
                return mOriginalText;
        }
        
        /// <summary>
        /// Method to close form and accept all modifications
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {

            mOkPressed = true;
            this.Close();   
            
        }
        /// <summary>
        /// Method to close the Form
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

