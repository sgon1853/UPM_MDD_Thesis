// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SIGEM.Client.Controllers;
using SIGEM.Client.Logics.Preferences;

namespace SIGEM.Client.InteractionToolkit
{
    /// <summary>
    /// Edits the properties of one DisplaySet item
    /// </summary>
    public partial class EditDisplaySetItemForm : Form
    {
        #region Members
        /// <summary>
        /// Item to be edited
        /// </summary>
	private DisplaySetItem mItem;
        #endregion Members

        #region Properties

        #endregion Properties

        #region Constructor
        public EditDisplaySetItemForm()
        {
            InitializeComponent();
        }
        #endregion Constructor

        #region Initialize
        /// <summary>
        /// Receives the Query controller of the scenario to be customized
        /// </summary>
        /// <param name="queryController">Query Controller</param>
        public void Initialize(DisplaySetItem item)
        {
            mItem = item;
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
            Text = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_EDITITEM_TITLE, LanguageConstantValues.L_PREFS_EDITITEM_TITLE);
            // Name 
            lp_Name.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_NAME, LanguageConstantValues.L_PREFS_NAME);
            // Alias
            lp_Alias.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_ALIAS, LanguageConstantValues.L_PREFS_ALIAS);
            // DataType
            lp_DataType.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_DATATYPE, LanguageConstantValues.L_PREFS_DATATYPE);
            // Visible
            lp_Visible.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_VISIBLE, LanguageConstantValues.L_PREFS_VISIBLE);
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
            // Load the combo with the Model data types
            List<KeyValuePair<ModelType, string>> modelDataTypes = new List<KeyValuePair<ModelType, string>>();
            modelDataTypes.Add(new KeyValuePair<ModelType, string>(ModelType.Autonumeric, "Autonumeric"));
            modelDataTypes.Add(new KeyValuePair<ModelType, string>(ModelType.Blob, "Blob"));
            modelDataTypes.Add(new KeyValuePair<ModelType, string>(ModelType.Bool, "Bool"));
            modelDataTypes.Add(new KeyValuePair<ModelType, string>(ModelType.Time, "Time"));
            modelDataTypes.Add(new KeyValuePair<ModelType, string>(ModelType.Date, "Date"));
            modelDataTypes.Add(new KeyValuePair<ModelType, string>(ModelType.DateTime, "DateTime"));
            modelDataTypes.Add(new KeyValuePair<ModelType, string>(ModelType.Int, "Int"));
            modelDataTypes.Add(new KeyValuePair<ModelType, string>(ModelType.Nat, "Nat"));
            modelDataTypes.Add(new KeyValuePair<ModelType, string>(ModelType.Real, "Real"));
            modelDataTypes.Add(new KeyValuePair<ModelType, string>(ModelType.Text, "Text"));
            modelDataTypes.Add(new KeyValuePair<ModelType, string>(ModelType.String, "String"));

            comboBox_DataType.DataSource = modelDataTypes;
            comboBox_DataType.DisplayMember = "Value";
            comboBox_DataType.ValueMember = "Key";


            // Set the values to the editors
            textBox_Name.Text = mItem.Name;
            textBox_Alias.Text = mItem.Alias;
            checkBoxp_Visible.Checked = mItem.Visible;
            comboBox_DataType.SelectedValue = mItem.ModelType;
        }
        #endregion Load Values

        #region Button Ok
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
                return;

            mItem.Name = textBox_Name.Text;
            mItem.Alias = textBox_Alias.Text;
            mItem.Visible = checkBoxp_Visible.Checked;
            mItem.ModelType = (ModelType) comboBox_DataType.SelectedValue;

            // Assign the default width
            if (mItem.Width == 0)
            {
                switch (mItem.ModelType)
                {
                    case ModelType.Bool:
                        mItem.Width=Presentation.DefaultFormats.ColWidthBool;
                        break;
                    case ModelType.Time:
                        mItem.Width=Presentation.DefaultFormats.ColWidthTime;
                        break;
                    case ModelType.Date:
                        mItem.Width=Presentation.DefaultFormats.ColWidthDate;
                        break;
                    case ModelType.DateTime:
                        mItem.Width=Presentation.DefaultFormats.ColWidthDateTime;
                        break;
                    case ModelType.Autonumeric:
                    case ModelType.Nat:
                    case ModelType.Int:
                    case ModelType.Real:
                        mItem.Width = Presentation.DefaultFormats.ColWidthNat;
                        break;
                    default:
                        mItem.Width=Presentation.DefaultFormats.ColWidthString10;
                        break;

                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }


        #endregion Button Ok

        #region Validate Data
        private bool ValidateData()
        {
            object[] lArgs = new object[1];
    
            if (textBox_Name.Text == "")
            {
                lArgs[0] = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_NAME, LanguageConstantValues.L_PREFS_NAME);
                string errorMsg = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_NECESARY, LanguageConstantValues.L_VALIDATION_NECESARY, lArgs);
                MessageBox.Show(errorMsg, CultureManager.TranslateString(LanguageConstantKeys.L_WARNING, LanguageConstantValues.L_WARNING), MessageBoxButtons.OK);
                textBox_Name.Focus();
                return false;
            }

            if (comboBox_DataType.SelectedIndex == -1)
            {
                lArgs[0] = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_DATATYPE, LanguageConstantValues.L_PREFS_DATATYPE);
                string errorMsg = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_NECESARY, LanguageConstantValues.L_VALIDATION_NECESARY, lArgs);
                MessageBox.Show(errorMsg, CultureManager.TranslateString(LanguageConstantKeys.L_WARNING, LanguageConstantValues.L_WARNING), MessageBoxButtons.OK);
                comboBox_DataType.Focus();
                return false;
            }

            return true;
        }
        #endregion Validate Data
    }
}
