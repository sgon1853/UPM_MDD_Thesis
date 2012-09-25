// v3.8.4.5.b
namespace SIGEM.Client.InteractionToolkit
{
    partial class RenameDisplaySetForm
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
            ((System.ComponentModel.ISupportInitialize)(this.dSetDisplaySetItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTDisplaySetItems)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(190, 57);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(109, 57);
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
            this.ItVisible.Caption = "";
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
            this.lp_Name.Location = new System.Drawing.Point(7, 24);
            this.lp_Name.Name = "lp_Name";
            this.lp_Name.Size = new System.Drawing.Size(86, 16);
            this.lp_Name.TabIndex = 28;
            this.lp_Name.Text = "Nuevo Nombre";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(98, 20);
            this.textBox_Name.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Name.MaxLength = 0;
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(168, 20);
            this.textBox_Name.TabIndex = 0;
            // 
            // RenameDisplaySetForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(273, 86);
            this.Controls.Add(this.lp_Name);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RenameDisplaySetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cambiar Nombre";
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
    }
}

