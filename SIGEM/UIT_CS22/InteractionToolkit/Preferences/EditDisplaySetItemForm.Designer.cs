// v3.8.4.5.b
namespace SIGEM.Client.InteractionToolkit
{
    partial class EditDisplaySetItemForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.dSetDisplaySetItems = new System.Data.DataSet();
            this.dTDisplaySetItems = new System.Data.DataTable();
            this.Expression = new System.Data.DataColumn();
            this.DataType = new System.Data.DataColumn();
            this.ItVisible = new System.Data.DataColumn();
            this.ItName = new System.Data.DataColumn();
            this.lp_Name = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.lp_Alias = new System.Windows.Forms.Label();
            this.checkBoxp_Visible = new System.Windows.Forms.CheckBox();
            this.textBox_Alias = new System.Windows.Forms.TextBox();
            this.lp_DataType = new System.Windows.Forms.Label();
            this.lp_Visible = new System.Windows.Forms.Label();
            this.comboBox_DataType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dSetDisplaySetItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTDisplaySetItems)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(432, 96);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(351, 96);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
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
            this.ItVisible.Caption = "Visible";
            this.ItVisible.ColumnName = "ItVisible";
            this.ItVisible.DataType = typeof(bool);
            // 
            // ItName
            // 
            this.ItName.Caption = "Name";
            this.ItName.ColumnName = "ItName";
            // 
            // lp_Name
            // 
            this.lp_Name.AutoEllipsis = true;
            this.lp_Name.Location = new System.Drawing.Point(20, 24);
            this.lp_Name.Name = "lp_Name";
            this.lp_Name.Size = new System.Drawing.Size(59, 16);
            this.lp_Name.TabIndex = 28;
            this.lp_Name.Text = "Name";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(84, 20);
            this.textBox_Name.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Name.MaxLength = 0;
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(129, 20);
            this.textBox_Name.TabIndex = 0;
            // 
            // lp_Alias
            // 
            this.lp_Alias.AutoEllipsis = true;
            this.lp_Alias.Location = new System.Drawing.Point(251, 24);
            this.lp_Alias.Name = "lp_Alias";
            this.lp_Alias.Size = new System.Drawing.Size(78, 16);
            this.lp_Alias.TabIndex = 30;
            this.lp_Alias.Text = "Alias";
            // 
            // checkBoxp_Visible
            // 
            this.checkBoxp_Visible.Location = new System.Drawing.Point(306, 62);
            this.checkBoxp_Visible.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxp_Visible.Name = "checkBoxp_Visible";
            this.checkBoxp_Visible.Size = new System.Drawing.Size(20, 20);
            this.checkBoxp_Visible.TabIndex = 3;
            this.checkBoxp_Visible.UseVisualStyleBackColor = false;
            // 
            // textBox_Alias
            // 
            this.textBox_Alias.Location = new System.Drawing.Point(306, 21);
            this.textBox_Alias.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Alias.MaxLength = 0;
            this.textBox_Alias.Name = "textBox_Alias";
            this.textBox_Alias.Size = new System.Drawing.Size(129, 20);
            this.textBox_Alias.TabIndex = 1;
            // 
            // lp_DataType
            // 
            this.lp_DataType.AutoEllipsis = true;
            this.lp_DataType.AutoSize = true;
            this.lp_DataType.Location = new System.Drawing.Point(20, 66);
            this.lp_DataType.Name = "lp_DataType";
            this.lp_DataType.Size = new System.Drawing.Size(59, 16);
            this.lp_DataType.TabIndex = 33;
            this.lp_DataType.Text = "Data Type";
            // 
            // lp_Visible
            // 
            this.lp_Visible.AutoEllipsis = true;
            this.lp_Visible.Location = new System.Drawing.Point(251, 66);
            this.lp_Visible.Name = "lp_Visible";
            this.lp_Visible.Size = new System.Drawing.Size(53, 16);
            this.lp_Visible.TabIndex = 34;
            this.lp_Visible.Text = "Visible";
            // 
            // comboBox_DataType
            // 
            this.comboBox_DataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DataType.FormattingEnabled = true;
            this.comboBox_DataType.Location = new System.Drawing.Point(85, 61);
            this.comboBox_DataType.Name = "comboBox_DataType";
            this.comboBox_DataType.Size = new System.Drawing.Size(128, 21);
            this.comboBox_DataType.TabIndex = 2;
            // 
            // EditDisplaySetItemForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(515, 125);
            this.Controls.Add(this.comboBox_DataType);
            this.Controls.Add(this.lp_Visible);
            this.Controls.Add(this.lp_DataType);
            this.Controls.Add(this.textBox_Alias);
            this.Controls.Add(this.lp_Name);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.lp_Alias);
            this.Controls.Add(this.checkBoxp_Visible);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditDisplaySetItemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editar";
            ((System.ComponentModel.ISupportInitialize)(this.dSetDisplaySetItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTDisplaySetItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label lp_Name;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label lp_Alias;
        private System.Windows.Forms.CheckBox checkBoxp_Visible;
        private System.Windows.Forms.TextBox textBox_Alias;
        private System.Windows.Forms.Label lp_DataType;
        private System.Windows.Forms.Label lp_Visible;
        private System.Windows.Forms.ComboBox comboBox_DataType;
    }
}

