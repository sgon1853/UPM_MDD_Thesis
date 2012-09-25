// v3.8.4.5.b
namespace SIGEM.Client.InteractionToolkit
{
    partial class CustomizeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.dSetDisplaySetItems = new System.Data.DataSet();
            this.dTDisplaySetItems = new System.Data.DataTable();
            this.Expression = new System.Data.DataColumn();
            this.DataType = new System.Data.DataColumn();
            this.ItVisible = new System.Data.DataColumn();
            this.ItName = new System.Data.DataColumn();
            this.tabPpal = new System.Windows.Forms.TabControl();
            this.tabDisplaySets = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStripDisplaySetActions = new System.Windows.Forms.ToolStrip();
            this.tsb_DS_Copy = new System.Windows.Forms.ToolStripButton();
            this.tsb_DS_Rename = new System.Windows.Forms.ToolStripButton();
            this.tsb_DS_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripItemActions = new System.Windows.Forms.ToolStrip();
            this.tSB_Items_Up = new System.Windows.Forms.ToolStripButton();
            this.tSB_Items_Down = new System.Windows.Forms.ToolStripButton();
            this.tSB_Items_New = new System.Windows.Forms.ToolStripButton();
            this.tSB_Items_Delete = new System.Windows.Forms.ToolStripButton();
            this.gridItems = new System.Windows.Forms.DataGridView();
            this.lstDisplaySets = new System.Windows.Forms.ListView();
            this.tabOthers = new System.Windows.Forms.TabPage();
            this.lp_BlockSize = new System.Windows.Forms.Label();
            this.textBox_BlockSize = new System.Windows.Forms.TextBox();
            this.dataGridViewAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewVisible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dSetDisplaySetItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTDisplaySetItems)).BeginInit();
            this.tabPpal.SuspendLayout();
            this.tabDisplaySets.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStripDisplaySetActions.SuspendLayout();
            this.toolStripItemActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).BeginInit();
            this.tabOthers.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(663, 365);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(582, 365);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 27;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // dSetDisplaySetItems
            // 
            this.dSetDisplaySetItems.DataSetName = "DisplaySetItems";
            this.dSetDisplaySetItems.Tables.AddRange(new System.Data.DataTable[] {
            this.dTDisplaySetItems});
            // 
            // dTDisplaySetItems
            // 
            this.dTDisplaySetItems.Columns.AddRange(new System.Data.DataColumn[] {
            this.Expression,
            this.DataType,
            this.ItVisible,
            this.ItName});
            this.dTDisplaySetItems.TableName = "Items";
            // 
            // Expression
            // 
            this.Expression.ColumnName = "Expression";
            // 
            // DataType
            // 
            this.DataType.ColumnName = "DataType";
            // 
            // ItVisible
            // 
            this.ItVisible.Caption = "";
            this.ItVisible.ColumnName = "ItVisible";
            this.ItVisible.DataType = typeof(bool);
            // 
            // ItName
            // 
            this.ItName.Caption = "Name";
            this.ItName.ColumnName = "ItName";
            // 
            // tabPpal
            // 
            this.tabPpal.Controls.Add(this.tabDisplaySets);
            this.tabPpal.Controls.Add(this.tabOthers);
            this.tabPpal.Location = new System.Drawing.Point(5, 7);
            this.tabPpal.Name = "tabPpal";
            this.tabPpal.SelectedIndex = 0;
            this.tabPpal.Size = new System.Drawing.Size(735, 352);
            this.tabPpal.TabIndex = 32;
            // 
            // tabDisplaySets
            // 
            this.tabDisplaySets.Controls.Add(this.groupBox1);
            this.tabDisplaySets.Location = new System.Drawing.Point(4, 22);
            this.tabDisplaySets.Name = "tabDisplaySets";
            this.tabDisplaySets.Padding = new System.Windows.Forms.Padding(3);
            this.tabDisplaySets.Size = new System.Drawing.Size(727, 326);
            this.tabDisplaySets.TabIndex = 0;
            this.tabDisplaySets.Text = "Datos";
            this.tabDisplaySets.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.toolStripDisplaySetActions);
            this.groupBox1.Controls.Add(this.toolStripItemActions);
            this.groupBox1.Controls.Add(this.gridItems);
            this.groupBox1.Controls.Add(this.lstDisplaySets);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(721, 320);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            // 
            // toolStripDisplaySetActions
            // 
            this.toolStripDisplaySetActions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStripDisplaySetActions.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripDisplaySetActions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripDisplaySetActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_DS_Copy,
            this.tsb_DS_Rename,
            this.tsb_DS_Delete});
            this.toolStripDisplaySetActions.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripDisplaySetActions.Location = new System.Drawing.Point(9, 292);
            this.toolStripDisplaySetActions.Name = "toolStripDisplaySetActions";
            this.toolStripDisplaySetActions.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripDisplaySetActions.Size = new System.Drawing.Size(75, 25);
            this.toolStripDisplaySetActions.TabIndex = 40;
            // 
            // tsb_DS_Copy
            // 
            this.tsb_DS_Copy.AutoSize = false;
            this.tsb_DS_Copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_DS_Copy.Image = global::SIGEM.Client.Properties.Resources.newicon;
            this.tsb_DS_Copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DS_Copy.Name = "tsb_DS_Copy";
            this.tsb_DS_Copy.Size = new System.Drawing.Size(24, 21);
            this.tsb_DS_Copy.ToolTipText = "New";
            this.tsb_DS_Copy.Click += new System.EventHandler(this.btnCopyDisplaySet_Click);
            // 
            // tsb_DS_Rename
            // 
            this.tsb_DS_Rename.AutoSize = false;
            this.tsb_DS_Rename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_DS_Rename.Image = global::SIGEM.Client.Properties.Resources.edit;
            this.tsb_DS_Rename.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DS_Rename.Name = "tsb_DS_Rename";
            this.tsb_DS_Rename.Size = new System.Drawing.Size(24, 21);
            this.tsb_DS_Rename.ToolTipText = "Destroy";
            this.tsb_DS_Rename.Click += new System.EventHandler(this.btnRenameDisplaySet_Click);
            // 
            // tsb_DS_Delete
            // 
            this.tsb_DS_Delete.AutoSize = false;
            this.tsb_DS_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_DS_Delete.Image = global::SIGEM.Client.Properties.Resources.delete;
            this.tsb_DS_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DS_Delete.Name = "tsb_DS_Delete";
            this.tsb_DS_Delete.Size = new System.Drawing.Size(24, 21);
            this.tsb_DS_Delete.ToolTipText = "Edit";
            this.tsb_DS_Delete.Click += new System.EventHandler(this.btnDeleteDisplaySet_Click);
            // 
            // toolStripItemActions
            // 
            this.toolStripItemActions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStripItemActions.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripItemActions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripItemActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSB_Items_Up,
            this.tSB_Items_Down,
            this.tSB_Items_New,
            this.tSB_Items_Delete});
            this.toolStripItemActions.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripItemActions.Location = new System.Drawing.Point(609, 292);
            this.toolStripItemActions.Name = "toolStripItemActions";
            this.toolStripItemActions.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripItemActions.Size = new System.Drawing.Size(99, 25);
            this.toolStripItemActions.TabIndex = 39;
            // 
            // tSB_Items_Up
            // 
            this.tSB_Items_Up.AutoSize = false;
            this.tSB_Items_Up.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSB_Items_Up.Image = global::SIGEM.Client.Properties.Resources.up;
            this.tSB_Items_Up.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB_Items_Up.Name = "tSB_Items_Up";
            this.tSB_Items_Up.Size = new System.Drawing.Size(24, 21);
            this.tSB_Items_Up.ToolTipText = "New";
            this.tSB_Items_Up.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // tSB_Items_Down
            // 
            this.tSB_Items_Down.AutoSize = false;
            this.tSB_Items_Down.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSB_Items_Down.Image = global::SIGEM.Client.Properties.Resources.down;
            this.tSB_Items_Down.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB_Items_Down.Name = "tSB_Items_Down";
            this.tSB_Items_Down.Size = new System.Drawing.Size(24, 21);
            this.tSB_Items_Down.ToolTipText = "Destroy";
            this.tSB_Items_Down.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // tSB_Items_New
            // 
            this.tSB_Items_New.AutoSize = false;
            this.tSB_Items_New.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSB_Items_New.Image = global::SIGEM.Client.Properties.Resources.newicon;
            this.tSB_Items_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB_Items_New.Name = "tSB_Items_New";
            this.tSB_Items_New.Size = new System.Drawing.Size(24, 21);
            this.tSB_Items_New.ToolTipText = "Edit";
            this.tSB_Items_New.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // tSB_Items_Delete
            // 
            this.tSB_Items_Delete.AutoSize = false;
            this.tSB_Items_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSB_Items_Delete.Image = global::SIGEM.Client.Properties.Resources.delete;
            this.tSB_Items_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB_Items_Delete.Name = "tSB_Items_Delete";
            this.tSB_Items_Delete.Size = new System.Drawing.Size(24, 21);
            this.tSB_Items_Delete.ToolTipText = "Dato";
            this.tSB_Items_Delete.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // gridItems
            // 
            this.gridItems.AllowUserToAddRows = false;
            this.gridItems.AllowUserToDeleteRows = false;
            this.gridItems.AllowUserToResizeRows = false;
            this.gridItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gridItems.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridItems.ColumnHeadersHeight = 21;
            this.gridItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewAlias,
            this.dataGridViewName,
            this.dataGridViewDataType,
            this.dataGridViewVisible});
            this.gridItems.DataMember = "Items";
            this.gridItems.DataSource = this.dSetDisplaySetItems;
            this.gridItems.Location = new System.Drawing.Point(154, 13);
            this.gridItems.MultiSelect = false;
            this.gridItems.Name = "gridItems";
            this.gridItems.ReadOnly = true;
            this.gridItems.RowHeadersWidth = 4;
            this.gridItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridItems.ShowCellErrors = false;
            this.gridItems.ShowCellToolTips = false;
            this.gridItems.ShowEditingIcon = false;
            this.gridItems.ShowRowErrors = false;
            this.gridItems.Size = new System.Drawing.Size(553, 271);
            this.gridItems.TabIndex = 31;
            this.gridItems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridCell_DoubleClick);
            this.gridItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsGrid_CellEndEdit);
            // 
            // lstDisplaySets
            // 
            this.lstDisplaySets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstDisplaySets.HideSelection = false;
            this.lstDisplaySets.Location = new System.Drawing.Point(9, 13);
            this.lstDisplaySets.MultiSelect = false;
            this.lstDisplaySets.Name = "lstDisplaySets";
            this.lstDisplaySets.Size = new System.Drawing.Size(139, 271);
            this.lstDisplaySets.TabIndex = 30;
            this.lstDisplaySets.UseCompatibleStateImageBehavior = false;
            this.lstDisplaySets.View = System.Windows.Forms.View.List;
            this.lstDisplaySets.SelectedIndexChanged += new System.EventHandler(this.DisplaySet_SelectedIndexChanged);
            // 
            // tabOthers
            // 
            this.tabOthers.Controls.Add(this.lp_BlockSize);
            this.tabOthers.Controls.Add(this.textBox_BlockSize);
            this.tabOthers.Location = new System.Drawing.Point(4, 22);
            this.tabOthers.Name = "tabOthers";
            this.tabOthers.Padding = new System.Windows.Forms.Padding(3);
            this.tabOthers.Size = new System.Drawing.Size(727, 326);
            this.tabOthers.TabIndex = 1;
            this.tabOthers.Text = "Propiedades";
            this.tabOthers.UseVisualStyleBackColor = true;
            // 
            // lp_BlockSize
            // 
            this.lp_BlockSize.AutoEllipsis = true;
            this.lp_BlockSize.Location = new System.Drawing.Point(18, 26);
            this.lp_BlockSize.Name = "lp_BlockSize";
            this.lp_BlockSize.Size = new System.Drawing.Size(91, 16);
            this.lp_BlockSize.TabIndex = 30;
            this.lp_BlockSize.Text = "Block Size";
            // 
            // textBox_BlockSize
            // 
            this.textBox_BlockSize.Location = new System.Drawing.Point(114, 22);
            this.textBox_BlockSize.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_BlockSize.MaxLength = 0;
            this.textBox_BlockSize.Name = "textBox_BlockSize";
            this.textBox_BlockSize.Size = new System.Drawing.Size(45, 20);
            this.textBox_BlockSize.TabIndex = 29;
            this.textBox_BlockSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BlockSize_KeyPress);
            // 
            // dataGridViewAlias
            // 
            this.dataGridViewAlias.DataPropertyName = "ItName";
            this.dataGridViewAlias.HeaderText = "Alias";
            this.dataGridViewAlias.Name = "dataGridViewAlias";
            this.dataGridViewAlias.ReadOnly = true;
            this.dataGridViewAlias.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewAlias.Width = 120;
            // 
            // dataGridViewName
            // 
            this.dataGridViewName.DataPropertyName = "Expression";
            this.dataGridViewName.HeaderText = "Name";
            this.dataGridViewName.Name = "dataGridViewName";
            this.dataGridViewName.ReadOnly = true;
            this.dataGridViewName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewName.Width = 200;
            // 
            // dataGridViewDataType
            // 
            this.dataGridViewDataType.DataPropertyName = "DataType";
            this.dataGridViewDataType.HeaderText = "Data Type";
            this.dataGridViewDataType.Name = "dataGridViewDataType";
            this.dataGridViewDataType.ReadOnly = true;
            this.dataGridViewDataType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewVisible
            // 
            this.dataGridViewVisible.DataPropertyName = "ItVisible";
            this.dataGridViewVisible.HeaderText = "Visible";
            this.dataGridViewVisible.Name = "dataGridViewVisible";
            this.dataGridViewVisible.ReadOnly = true;
            this.dataGridViewVisible.Width = 60;
            // 
            // CustomizeForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(746, 394);
            this.Controls.Add(this.tabPpal);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomizeForm";
            this.Text = "Personalización";
            ((System.ComponentModel.ISupportInitialize)(this.dSetDisplaySetItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTDisplaySetItems)).EndInit();
            this.tabPpal.ResumeLayout(false);
            this.tabDisplaySets.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripDisplaySetActions.ResumeLayout(false);
            this.toolStripDisplaySetActions.PerformLayout();
            this.toolStripItemActions.ResumeLayout(false);
            this.toolStripItemActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
            this.tabOthers.ResumeLayout(false);
            this.tabOthers.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.Button btnOk;
        private System.Data.DataSet dSetDisplaySetItems;
        private System.Data.DataTable dTDisplaySetItems;
        private System.Data.DataColumn Expression;
        private System.Data.DataColumn DataType;
        private System.Data.DataColumn ItVisible;
        private System.Data.DataColumn ItName;
        private System.Windows.Forms.TabControl tabPpal;
        private System.Windows.Forms.TabPage tabDisplaySets;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gridItems;
        private System.Windows.Forms.ListView lstDisplaySets;
        private System.Windows.Forms.TabPage tabOthers;
        private System.Windows.Forms.ToolStrip toolStripItemActions;
        private System.Windows.Forms.ToolStripButton tSB_Items_Up;
        private System.Windows.Forms.ToolStripButton tSB_Items_Down;
        private System.Windows.Forms.ToolStripButton tSB_Items_New;
        private System.Windows.Forms.ToolStripButton tSB_Items_Delete;
        private System.Windows.Forms.ToolStrip toolStripDisplaySetActions;
        private System.Windows.Forms.ToolStripButton tsb_DS_Copy;
        private System.Windows.Forms.ToolStripButton tsb_DS_Rename;
        private System.Windows.Forms.ToolStripButton tsb_DS_Delete;
        private System.Windows.Forms.Label lp_BlockSize;
        private System.Windows.Forms.TextBox textBox_BlockSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewAlias;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewDataType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewVisible;
    }
}



