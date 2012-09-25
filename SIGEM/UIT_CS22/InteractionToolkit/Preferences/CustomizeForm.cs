// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using SIGEM.Client.Controllers;
using SIGEM.Client.Logics.Preferences;

namespace SIGEM.Client.InteractionToolkit
{
    /// <summary>
    /// This form allows the customization of Query scenarios. Population at this moment
    /// New DisplaySets can be added and select the contained items
    /// At the end, the changes will be saved in the Customization server
    /// </summary>
    public partial class CustomizeForm : Form
    {
        #region Members
        /// <summary>
        /// Temporal list of Custom DisplaySets
        /// </summary>
        private List<DisplaySetInformation> mTempDisplaySets = new List<DisplaySetInformation>();
        /// <summary>
        /// Query Controller of the scenario to be Customized
        /// </summary>
        private IUQueryController mQueryController = null;
        #endregion Members

        #region Properties
        /// <summary>
        /// Returns the selected DisplaySet
        /// </summary>
        private DisplaySetInformation SelectedDisplaySet
        {
            get
            {
                if (lstDisplaySets.SelectedItems.Count < 1)
                    return null;
                try
                {
                    return mTempDisplaySets[lstDisplaySets.SelectedItems[0].Index];
                }
                catch
                {
                    return null;
                }
            }

        }
        #endregion Properties

        #region Constructor
        public CustomizeForm()
        {
            InitializeComponent();
        }
        #endregion Constructor

        #region Initialize
        /// <summary>
        /// Receives the Query controller of the scenario to be customized
        /// </summary>
        /// <param name="queryController">Query Controller</param>
        public void Initialize(IUQueryController queryController)
        {
            mQueryController = queryController;

            // Copy the DisplaySets to the temporal list
            foreach (DisplaySetInformation displaySet in mQueryController.DisplaySet.DisplaySetList)
            {
                DisplaySetInformation newDisplayset = new DisplaySetInformation(displaySet);
                mTempDisplaySets.Add(newDisplayset);
            }

            ApplyMultilanguage();

            LoadDisplaySets();

            LoadPopulationInfo();
        }
        #endregion Initialize

        #region Load Population Info
        /// <summary>
        /// Load the specific information of one population scenario
        /// </summary>
        private void LoadPopulationInfo()
        {
            IUPopulationController popController = mQueryController as IUPopulationController;

            // If it is not a Population Controller
            if (popController == null)
            {
                textBox_BlockSize.Enabled = false;
                return;
            }

            textBox_BlockSize.Enabled = true;

            textBox_BlockSize.Text = popController.Context.BlockSize.ToString();
        }
        #endregion Load Population Info

        #region ApplyMultilanguage
        /// <summary>
        /// Apply multilanguage to the scenario.
        /// </summary>
        private void ApplyMultilanguage()
        {
            // Title
            Text = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_TITLE, LanguageConstantValues.L_PREFS_TITLE);

            // Tabs
            tabDisplaySets.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_TAB_DATA, LanguageConstantValues.L_PREFS_TAB_DATA);
            tabOthers.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_TAB_OTHERS, LanguageConstantValues.L_PREFS_TAB_OTHERS);

            // Buttons
            tsb_DS_Copy.ToolTipText = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_BTN_COPYDS, LanguageConstantValues.L_PREFS_BTN_COPYDS);
            tsb_DS_Rename.ToolTipText = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_BTN_RENAMEDS, LanguageConstantValues.L_PREFS_BTN_RENAMEDS);
            tsb_DS_Delete.ToolTipText = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_BTN_DELDS, LanguageConstantValues.L_PREFS_BTN_DELDS);
            tSB_Items_Up.ToolTipText = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_BTN_UP, LanguageConstantValues.L_PREFS_BTN_UP);
            tSB_Items_Down.ToolTipText = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_BTN_DOWN, LanguageConstantValues.L_PREFS_BTN_DOWN);
            tSB_Items_New.ToolTipText = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_BTN_ADDITEM, LanguageConstantValues.L_PREFS_BTN_ADDITEM);
            tSB_Items_Delete.ToolTipText = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_BTN_DELITEM, LanguageConstantValues.L_PREFS_BTN_DELITEM);

            // Columns
            gridItems.Columns["dataGridViewName"].HeaderText = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_NAME, LanguageConstantValues.L_PREFS_NAME);
            gridItems.Columns["dataGridViewAlias"].HeaderText = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_ALIAS, LanguageConstantValues.L_PREFS_ALIAS);
            gridItems.Columns["dataGridViewDataType"].HeaderText = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_DATATYPE, LanguageConstantValues.L_PREFS_DATATYPE);

            // Block Size
            lp_BlockSize.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_BLOCKSIZE, LanguageConstantValues.L_PREFS_BLOCKSIZE);

            // Ok button.
            btnOk.Text = CultureManager.TranslateString(LanguageConstantKeys.L_OK, LanguageConstantValues.L_OK, btnOk.Text);
            // Cancel button.
            btnCancel.Text = CultureManager.TranslateString(LanguageConstantKeys.L_CANCEL, LanguageConstantValues.L_CANCEL, btnCancel.Text);
        }
        #endregion ApplyMultilanguage

        #region DisplaySets Management
        /// <summary>
        /// Load the DisplaySets data in the screen
        /// </summary>
        private void LoadDisplaySets()
        {
            int indexToBeSelected = 0;
            bool found = false;
            // Load the list with the DisplaySet Names
            foreach (DisplaySetInformation displaySet in this.mTempDisplaySets)
            {
                lstDisplaySets.Items.Add(displaySet.Name);
                if (displaySet.Name == mQueryController.DisplaySet.CurrentDisplaySet.Name)
                {
                    found = true;
                }
                if (!found)
                {
                    indexToBeSelected++;
                }

            }

            // Select the current DisplaySet
            if (found)
            {
                lstDisplaySets.Items[indexToBeSelected].Selected = true;
                ShowDisplaySetItems(indexToBeSelected);
            }
        }

        /// <summary>
        /// Returns the DisplaySet items of the Modeled DisplaySet
        /// </summary>
        /// <param name="displaySetsList">DisplaySets list</param>
        /// <param name="displaySetName">DisplaySet name</param>
        /// <returns></returns>
        private DisplaySetInformation FindDisplaySet(string displaySetName)
        {
            foreach (DisplaySetInformation displaySet in mTempDisplaySets)
            {
                if (displaySet.Name.Equals(displaySetName, StringComparison.OrdinalIgnoreCase))
                {
                    return displaySet;
                }
            }

            return null;
        }

        private void DisplaySet_SelectedIndexChanged(object sender, EventArgs e)
        {
            // The selection in the DisplaySet has changed. Refresh the Items grid

            ListView.SelectedListViewItemCollection selectedItems = lstDisplaySets.SelectedItems;
            if (selectedItems == null || selectedItems.Count == 0)
                return;

            ListViewItem item = lstDisplaySets.SelectedItems[0];

            // If selected DisplaySet is a Modeled one, disable actions
            if (SelectedDisplaySet == null || !SelectedDisplaySet.Custom)
            {
                tsb_DS_Rename.Enabled = false;
                tsb_DS_Delete.Enabled = false;
                toolStripItemActions.Enabled = false;
            }
            else
            {
                tsb_DS_Rename.Enabled = true;
                tsb_DS_Delete.Enabled = true;
                toolStripItemActions.Enabled = true;
            }

            // Show the related items
            ShowDisplaySetItems(item.Index);
        }

        /// <summary>
        /// Returns a non existing name for one DisplaySet
        /// Starts checking with the received initial name
        /// </summary>
        /// <param name="initialName"></param>
        /// <returns></returns>
        private string GetUniqueName(string initialName)
        {
            bool nameOk = false;
            string finalName = initialName;
            int counter = 1;
            while (!nameOk)
            {
                if (FindDisplaySet(finalName) == null)
                {
                    nameOk = true;
                }
                else
                {
                    finalName = initialName + counter.ToString();
                    counter++;
                }
            }

            return finalName;
        }
        #endregion DisplaySets Management

        #region GridItems Management
        private void ShowDisplaySetItems(DisplaySetInformation displaySet)
        {
            dTDisplaySetItems.Rows.Clear();

            foreach (DisplaySetItem item in displaySet.DisplaySetItems)
            {
                DataRow row = dTDisplaySetItems.Rows.Add();
                row["ItVisible"] = item.Visible;
                row["ItName"] = item.Alias;
                row["Expression"] = item.Name;
                row["DataType"] = item.ModelType.ToString();
            }

            gridItems.AllowUserToAddRows = false;
            gridItems.AllowUserToDeleteRows = false;
            // Set Items grid properties
            if (displaySet.Custom)
            {
                // Custom DisplaySets can be modified
                gridItems.ReadOnly = false;
                gridItems.Columns["dataGridViewVisible"].ReadOnly = false;
            }
            else
            {
                // Modeled DisplaySets can not be modified
                gridItems.ReadOnly = true;
                gridItems.Columns["dataGridViewVisible"].ReadOnly = true;
            }
        }
        /// <summary>
        /// Show the items of the specified index
        /// </summary>
        /// <param name="index"></param>
        private void ShowDisplaySetItems(int index)
        {
            if (index > mTempDisplaySets.Count - 1)
                return;

            ShowDisplaySetItems(mTempDisplaySets[index]);
        }

        private void ItemsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Save the modified data in the Custom DisplaySet list
            try
            {
                if (SelectedDisplaySet == null || !SelectedDisplaySet.Custom)
                {
                    return;
                }

                DisplaySetItem item = SelectedDisplaySet.DisplaySetItems[e.RowIndex];

                if (item == null)
                    return;

                object value = gridItems.Rows[e.RowIndex].Cells["dataGridViewVisible"].Value;

                if (value != null)
                {
                    item.Visible = (bool)value;
                }
            }
            catch
            {
            }
        }
        #endregion GridItems Management

        private static bool IsCustom(DisplaySetInformation displaySet)
        {
            return displaySet.Custom;
        }
        #region Set information to Controller
        /// <summary>
        /// Assigns the new properties to the controller
        /// </summary>
        private void SetInfoToController()
        {

            // Remove the custom DisplaySets in the controller
            mQueryController.DisplaySet.DisplaySetList.RemoveAll(IsCustom);

            // Copy the customized DisplaySets
            foreach (DisplaySetInformation displaySet in mTempDisplaySets)
            {
                if (displaySet.Custom)
                {
                    mQueryController.DisplaySet.DisplaySetList.Add(displaySet);
                }
            }

            // Assign the current DisplaySet to the controller
            mQueryController.DisplaySet.CurrentDisplaySet = mQueryController.DisplaySet.GetDisplaySetByName(SelectedDisplaySet.Name);
            
			// If current DisplaySet is editable or not, show or hide the trigger
			if (mQueryController.DisplaySet.ExecuteServiceTrigger != null)
			{
				mQueryController.DisplaySet.ExecuteServiceTrigger.Visible = mQueryController.DisplaySet.CurrentDisplaySet.Editable;
			}

            // Assign specific properties
            SetInfoToPopulationController();
        }

        /// <summary>
        /// Assigns the specific population controller properties
        /// </summary>
        private void SetInfoToPopulationController()
        {
            IUPopulationController popController = mQueryController as IUPopulationController;

            if (popController == null)
                return;

            popController.Context.BlockSize = int.Parse(textBox_BlockSize.Text);
        }
        #endregion Set information to Controller

        #region Save Scenario Info
        /// <summary>
        /// Save the Scenario info in the Preferences Server
        /// </summary>
        private void SaveScenarioInfo()
        {
            IUPopulationController popController = mQueryController as IUPopulationController;
            if (popController !=null)
            {
                Logics.Logic.UserPreferences.SavePopulationInfo(popController.Context.ClassName + ":" + popController.Name, int.Parse(textBox_BlockSize.Text), popController.DisplaySet.CurrentDisplaySet.Name, popController.DisplaySet.DisplaySetList);
                return;
            }

            IUInstanceController insController = mQueryController as IUInstanceController;
            if (insController != null)
            {
                Logics.Logic.UserPreferences.SaveInstanceInfo(insController.Context.ClassName + ":" + insController.Name, insController.DisplaySet.CurrentDisplaySet.Name, insController.DisplaySet.DisplaySetList);
            }
        }
        #endregion Save Scenario Info

        #region Button Ok
        private void btnOk_Click(object sender, EventArgs e)
        {

            if (!ValidateData())
                return;

            // Set info to the Controller
            SetInfoToController();

            // Save info in the Customization server
            SaveScenarioInfo();

            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion Button Ok

        #region Validate Data
        private bool ValidateData()
        {
            object[] lArgs = new object[1];

            if (textBox_BlockSize.Enabled == true)
            {
                lArgs[0] = CultureManager.TranslateString(LanguageConstantKeys.L_PREFS_BLOCKSIZE, LanguageConstantValues.L_PREFS_BLOCKSIZE);
                if (textBox_BlockSize.Text == "")
                {
                    string errorMsg = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_NECESARY, LanguageConstantValues.L_VALIDATION_NECESARY, lArgs);
                    MessageBox.Show(errorMsg, CultureManager.TranslateString(LanguageConstantKeys.L_WARNING, LanguageConstantValues.L_WARNING), MessageBoxButtons.OK);
                    textBox_BlockSize.Focus();
                    return false;
                }
                try
                {
                    int i = int.Parse(textBox_BlockSize.Text);
                }
                catch
                {
                    string errorMsg = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_INTEGER_ERROR, LanguageConstantValues.L_VALIDATION_INTEGER_ERROR, lArgs);
                    MessageBox.Show(errorMsg, CultureManager.TranslateString(LanguageConstantKeys.L_WARNING, LanguageConstantValues.L_WARNING), MessageBoxButtons.OK);
                    textBox_BlockSize.Focus();
                    return false;
                }
            }

            return true;
        }
        #endregion Validate Data

        #region Button Copy DisplaySet

        private void btnCopyDisplaySet_Click(object sender, EventArgs e)
        {
            // Copy the selected DisplaySet

            ListView.SelectedListViewItemCollection selectedItems = lstDisplaySets.SelectedItems;
            if (selectedItems == null || selectedItems.Count == 0)
                return;

            ListViewItem item = lstDisplaySets.SelectedItems[0];

            string displaySetName = item.Text;
            DisplaySetInformation displaySet = mTempDisplaySets[item.Index];

            // Copy the information
            string newName = GetUniqueName("Customize");

            // Copy the DisplaySet items
            DisplaySetInformation newDisplaySet = new DisplaySetInformation(displaySet);
            newDisplaySet.Name = newName;
            newDisplaySet.Custom = true;
            mTempDisplaySets.Add(newDisplaySet);

            // Add to the Tree and select it
            ListViewItem newItem = lstDisplaySets.Items.Add(newName);
            newItem.Selected = true;
        }
        #endregion Button Copy DisplaySet

        #region Button Delete DisplaySet
        /// <summary>
        /// Delete the selected Custom DisplaySet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteDisplaySet_Click(object sender, EventArgs e)
        {
            // Delete the selected DisplaySet
            ListView.SelectedListViewItemCollection selectedItems = lstDisplaySets.SelectedItems;
            if (selectedItems == null || selectedItems.Count == 0)
                return;

            ListViewItem item = lstDisplaySets.SelectedItems[0];

            int indexToBeDeleted = item.Index;

            // Modeled displaysets can not be deleted
            if (SelectedDisplaySet == null || !SelectedDisplaySet.Custom)
            {
                return;
            }


            object[] lArgs = new object[1];
            lArgs[0] = item.Text;
            string errorMsg = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_CONFIRMDELETE, LanguageConstantValues.L_CONFIRMDELETE, lArgs);
            if (MessageBox.Show(errorMsg, CultureManager.TranslateString(LanguageConstantKeys.L_WARNING, LanguageConstantValues.L_WARNING), MessageBoxButtons.YesNo) == DialogResult.No)
                return;


            mTempDisplaySets.RemoveAt(indexToBeDeleted);
            lstDisplaySets.Items.RemoveAt(indexToBeDeleted);
            
            // Select the previous DisplaySet
            if (indexToBeDeleted >= lstDisplaySets.Items.Count)
            {
                lstDisplaySets.Items[indexToBeDeleted - 1].Selected = true;
            }
            else
            {
                lstDisplaySets.Items[indexToBeDeleted].Selected = true;
            }
        }
        #endregion Button Delete DisplaySet

        #region Button Up
        private void btnUp_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItems = lstDisplaySets.SelectedItems;
            if (selectedItems == null || selectedItems.Count == 0)
                return;

            ListViewItem item = lstDisplaySets.SelectedItems[0];

            int displaySetIndex = item.Index;
            // Modeled DisplaySet can not be modified
            if (SelectedDisplaySet == null || !SelectedDisplaySet.Custom)
                return;

            MoveUpDown(true, displaySetIndex);
        }

        #endregion Button Up

        #region Button Down
        private void btnDown_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItems = lstDisplaySets.SelectedItems;
            if (selectedItems == null || selectedItems.Count == 0)
                return;

            ListViewItem item = lstDisplaySets.SelectedItems[0];

            int displaySetIndex = item.Index;
            // Modeled DisplaySet can not be modified
            if (SelectedDisplaySet == null || !SelectedDisplaySet.Custom)
                return;

            MoveUpDown(false, displaySetIndex);
        }
        #endregion Button Down

        #region Move Up & Down
        /// <summary>
        /// Moves Up and Doen the selected DisplaySet item
        /// </summary>
        /// <param name="moveUp"></param>
        private void MoveUpDown(bool moveUp, int displaySetIndex)
        {
            if (gridItems.SelectedRows == null || gridItems.SelectedRows.Count != 1)
                return;

            // Index of the selected item
            DataGridViewRow row = gridItems.SelectedRows[0];
            int itemIndex = row.Index;

            // First line
            if (moveUp && itemIndex == 0)
                return;

            // Last line
            if (!moveUp && itemIndex == gridItems.Rows.Count - 1)
                return;
            
            // Move the item in the Selected DisplaySet
            DisplaySetItem item = SelectedDisplaySet.DisplaySetItems[itemIndex];
            SelectedDisplaySet.DisplaySetItems.RemoveAt(itemIndex);
            if (moveUp)
            {
                SelectedDisplaySet.DisplaySetItems.Insert(itemIndex - 1, item);
            }
            else
            {
                SelectedDisplaySet.DisplaySetItems.Insert(itemIndex + 1, item);
            }
            
            // Update the Grid
            ShowDisplaySetItems(displaySetIndex);

            
            if (moveUp)
            {
                gridItems.Rows[itemIndex - 1].Selected = true;
            }
            else
            {
                gridItems.Rows[itemIndex + 1].Selected = true;
            }

        }
        #endregion Move Up & Down


        #region Grid Double Click
        /// <summary>
        /// Edit the selected row in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridCell_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Only custom DisplaySets can be modified
            if (SelectedDisplaySet == null || !SelectedDisplaySet.Custom)
                return;

            int itemIndex = e.RowIndex;
            try
            {
                DisplaySetItem item = SelectedDisplaySet.DisplaySetItems[itemIndex];

                EditDisplaySetItemForm form = new EditDisplaySetItemForm();
                form.Initialize(item);
                if (form.ShowDialog() != DialogResult.OK)
                    return;
            }
            catch
            {
            }
            ShowDisplaySetItems(SelectedDisplaySet);
            gridItems.Rows[itemIndex].Selected = true;
        }
        #endregion Grid Double Click

        #region Add DisplaySet item button
        /// <summary>
        /// Add a new DisplaySet item and opens the edit scenario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            // Only custom DisplaySets can be modified
            if (SelectedDisplaySet == null || !SelectedDisplaySet.Custom)
                return;

            try
            {
                List<string> agents = new List<string>();
                agents.Add(Logics.Logic.Agent.ClassName);
				DisplaySetItem item = new DisplaySetItem("", "", "", ModelType.String, agents, 0, true);
                SelectedDisplaySet.DisplaySetItems.Add(item);

                EditDisplaySetItemForm form = new EditDisplaySetItemForm();
                form.Initialize(item);
                if (form.ShowDialog() != DialogResult.OK)
                {
                    SelectedDisplaySet.DisplaySetItems.Remove(item);
                    return;
                }
            }
            catch
            {
            }
            ShowDisplaySetItems(SelectedDisplaySet);
            gridItems.Rows[gridItems.Rows.Count - 1].Selected = true;
        }
        #endregion Add DisplaySet item button

        #region Delete DisplaySet item button
        /// <summary>
        /// Deletes the selected DisplaySet item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            // Only custom DisplaySets can be modified
            if (SelectedDisplaySet == null || !SelectedDisplaySet.Custom)
                return;

            if (gridItems.SelectedRows == null || gridItems.SelectedRows.Count != 1)
                return;

            int itemIndex = gridItems.SelectedRows[0].Index;
            try
            {
                DisplaySetItem item = SelectedDisplaySet.DisplaySetItems[itemIndex];

                object[] lArgs = new object[1];
                lArgs[0] = item.Alias;
                string errorMsg = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_CONFIRMDELETE, LanguageConstantValues.L_CONFIRMDELETE, lArgs);
                if (MessageBox.Show(errorMsg, CultureManager.TranslateString(LanguageConstantKeys.L_WARNING, LanguageConstantValues.L_WARNING), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SelectedDisplaySet.DisplaySetItems.RemoveAt(itemIndex);
            }
            catch
            {
            }
            ShowDisplaySetItems(SelectedDisplaySet);
        }
        #endregion Delete DisplaySet item button

        #region Rename DisplaySet Button
        /// <summary>
        /// Changes the name to the Custom selected DisplaySet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRenameDisplaySet_Click(object sender, EventArgs e)
        {
            // Only custom DisplaySets can be modified
            if (SelectedDisplaySet == null || !SelectedDisplaySet.Custom)
                return;

            string oldName = SelectedDisplaySet.Name;

            // Create the not valid name list with the other DisplaySet names
            StringCollection notValidNames = new StringCollection();
            foreach (DisplaySetInformation displaySet in mTempDisplaySets)
            {
                if (displaySet.Name != oldName)
                    notValidNames.Add(displaySet.Name);
            }

            // Open the form
            RenameDisplaySetForm form = new RenameDisplaySetForm();
            form.Initialize(oldName, notValidNames);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            // If not canceled, changes the names
            string newName = form.DisplaySetName;
            lstDisplaySets.SelectedItems[0].Text = newName;
            SelectedDisplaySet.Name = newName;
        }
        #endregion Rename DisplaySet Button

        #region BlockSize KeyPress Handler
        private void BlockSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only numeric characters and back
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
                e.KeyChar = '\0';
        }
        #endregion BlockSize KeyPress Handler
    }
}
