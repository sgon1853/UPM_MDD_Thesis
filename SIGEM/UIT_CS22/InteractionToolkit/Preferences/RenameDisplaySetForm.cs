// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIGEM.Client.InteractionToolkit
{
    /// <summary>
    /// Edits one name. Checks that the new one is valid, using the not valid names list
    /// </summary>
    public partial class RenameDisplaySetForm : Form
    {
        #region Members
        /// <summary>
        /// Name of the DisplaySet
        /// </summary>
        private string mName;
        /// <summary>
        /// Not valid names list. Received from the caller scenario
        /// </summary>
        private StringCollection mNotValidNames = null;
        #endregion Members

        #region Properties
        public string DisplaySetName
        {
            get
            {
                return mName;
            }
        }
        #endregion Properties

        #region Constructor
        public RenameDisplaySetForm()
        {
            InitializeComponent();
        }
        #endregion Constructor

        #region Initialize
        /// <summary>
        /// Receives the old name and the not valid names list
        /// </summary>
        /// <param name="displaySetName"></param>
        /// <param name="notValidNames"></param>
        public void Initialize(string displaySetName, StringCollection notValidNames)
        {
            mName = displaySetName;
            mNotValidNames = notValidNames;
            ApplyMultilanguage();
            LoadValues();
        }
        #endregion Initialize

        #region ApplyMultilanguage
		/// <summary>
		/// Apply multilanguage to the scenario.
		/// </summary>
        private void ApplyMultilanguage()
        {
            // Title
            Text = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_RENAMETITLE, LanguageConstantValues.L_PREFS_RENAMETITLE);
            // Name Alias
            lp_Name.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_NAME, LanguageConstantValues.L_PREFS_NAME);
            // Ok button.
            btnOk.Text = CultureManager.TranslateString(LanguageConstantKeys.L_OK, LanguageConstantValues.L_OK);
            // Cancel button.
            btnCancel.Text = CultureManager.TranslateString(LanguageConstantKeys.L_CANCEL, LanguageConstantValues.L_CANCEL);
        }
        #endregion ApplyMultilanguage

        #region Load Values
        /// <summary>
        /// Load the Initial values in the form
        /// </summary>
        private void LoadValues()
        {
            textBox_Name.Text = DisplaySetName;
        }
        #endregion Load Values

        #region Validate Data
        /// <summary>
        /// Check the data in the form
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            if (textBox_Name.Text == "")
            {
                object[] lArgs = new object[1];
                lArgs[0] = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_NAME, LanguageConstantValues.L_PREFS_NAME);
                string errorMsg = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_NECESARY, LanguageConstantValues.L_VALIDATION_NECESARY, lArgs);
                MessageBox.Show(errorMsg, CultureManager.TranslateString(LanguageConstantKeys.L_WARNING, LanguageConstantValues.L_WARNING, ""), MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_Name.Focus();
                return false;
            }

            if (mNotValidNames != null)
            {
                if (mNotValidNames.Contains(textBox_Name.Text))
                {
                    string errorMsg = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_ERR_NOTVALIDNAME, LanguageConstantValues.L_PREFS_ERR_NOTVALIDNAME);
                    MessageBox.Show(errorMsg, CultureManager.TranslateString(LanguageConstantKeys.L_WARNING, LanguageConstantValues.L_WARNING, ""), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox_Name.Focus();
                    return false;
                }
            }

            return true;
        }
        #endregion Validate Data

        #region Button Ok
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
                return;

            mName = textBox_Name.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
        #endregion Button Ok
    }
}
