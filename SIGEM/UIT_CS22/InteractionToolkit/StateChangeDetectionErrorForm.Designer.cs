namespace SIGEM.Client.InteractionToolkit
{
	partial class StateChangeDetectionErrorForm
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
			this.labelErrorDescription = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonRetry = new System.Windows.Forms.Button();
			this.panelButtons = new System.Windows.Forms.Panel();
			this.dataGridViewValues = new System.Windows.Forms.DataGridView();
			this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.previousValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CurrentValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataSetValues = new System.Data.DataSet();
			this.dataTableValues = new System.Data.DataTable();
			this.dataColumn1 = new System.Data.DataColumn();
			this.dataColumn2 = new System.Data.DataColumn();
			this.dataColumn3 = new System.Data.DataColumn();
			this.panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSetValues)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTableValues)).BeginInit();
			this.SuspendLayout();
			// 
			// labelErrorDescription
			// 
			this.labelErrorDescription.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelErrorDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelErrorDescription.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelErrorDescription.Location = new System.Drawing.Point(0, 0);
			this.labelErrorDescription.Name = "labelErrorDescription";
			this.labelErrorDescription.Size = new System.Drawing.Size(576, 59);
			this.labelErrorDescription.TabIndex = 0;
			this.labelErrorDescription.Text = "ALGUNOS VALORES HAN CAMBIADO, EL PROCESO NO HA SIDO EJECUTADO. PUEDE REINTENTAR E" +
				"JECUTAR CON LOS VALORES ACTUALES O CERRAR LA PANTALLA";
			this.labelErrorDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelErrorDescription.UseMnemonic = false;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(495, 4);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 0;
			this.buttonClose.Text = "Cerrar";
			this.buttonClose.UseVisualStyleBackColor = true;
			// 
			// buttonRetry
			// 
			this.buttonRetry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRetry.AutoSize = true;
			this.buttonRetry.DialogResult = System.Windows.Forms.DialogResult.Retry;
			this.buttonRetry.Location = new System.Drawing.Point(415, 4);
			this.buttonRetry.Name = "buttonRetry";
			this.buttonRetry.Size = new System.Drawing.Size(75, 23);
			this.buttonRetry.TabIndex = 1;
			this.buttonRetry.Text = "Reintentar";
			this.buttonRetry.UseVisualStyleBackColor = true;
			// 
			// panelButtons
			// 
			this.panelButtons.Controls.Add(this.buttonClose);
			this.panelButtons.Controls.Add(this.buttonRetry);
			this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelButtons.Location = new System.Drawing.Point(0, 272);
			this.panelButtons.Name = "panelButtons";
			this.panelButtons.Size = new System.Drawing.Size(576, 32);
			this.panelButtons.TabIndex = 2;
			// 
			// dataGridViewValues
			// 
			this.dataGridViewValues.AllowUserToAddRows = false;
			this.dataGridViewValues.AllowUserToDeleteRows = false;
			this.dataGridViewValues.AutoGenerateColumns = false;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewValues.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.description,
            this.previousValue,
            this.CurrentValue});
			this.dataGridViewValues.DataMember = "tableValues";
			this.dataGridViewValues.DataSource = this.dataSetValues;
			this.dataGridViewValues.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewValues.Location = new System.Drawing.Point(0, 59);
			this.dataGridViewValues.Name = "dataGridViewValues";
			this.dataGridViewValues.ReadOnly = true;
			this.dataGridViewValues.RowHeadersVisible = false;
			this.dataGridViewValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewValues.ShowCellErrors = false;
			this.dataGridViewValues.ShowCellToolTips = false;
			this.dataGridViewValues.ShowEditingIcon = false;
			this.dataGridViewValues.ShowRowErrors = false;
			this.dataGridViewValues.Size = new System.Drawing.Size(576, 213);
			this.dataGridViewValues.TabIndex = 3;
			// 
			// description
			// 
			this.description.DataPropertyName = "Description";
			this.description.HeaderText = "Description";
			this.description.Name = "description";
			this.description.ReadOnly = true;
			this.description.Width = 220;
			// 
			// previousValue
			// 
			this.previousValue.DataPropertyName = "previousValue";
			this.previousValue.HeaderText = "previousValue";
			this.previousValue.Name = "previousValue";
			this.previousValue.ReadOnly = true;
			this.previousValue.Width = 150;
			// 
			// CurrentValue
			// 
			this.CurrentValue.DataPropertyName = "CurrentValue";
			this.CurrentValue.HeaderText = "CurrentValue";
			this.CurrentValue.Name = "CurrentValue";
			this.CurrentValue.ReadOnly = true;
			this.CurrentValue.Width = 150;
			// 
			// dataSetValues
			// 
			this.dataSetValues.DataSetName = "NewDataSet";
			this.dataSetValues.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTableValues});
			// 
			// dataTableValues
			// 
			this.dataTableValues.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3});
			this.dataTableValues.TableName = "tableValues";
			// 
			// dataColumn1
			// 
			this.dataColumn1.ColumnName = "Description";
			// 
			// dataColumn2
			// 
			this.dataColumn2.Caption = "previousValue";
			this.dataColumn2.ColumnName = "previousValue";
			// 
			// dataColumn3
			// 
			this.dataColumn3.Caption = "CurrentValue";
			this.dataColumn3.ColumnName = "CurrentValue";
			// 
			// StateChangeDetectionErrorForm
			// 
			this.AcceptButton = this.buttonClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(576, 304);
			this.Controls.Add(this.dataGridViewValues);
			this.Controls.Add(this.panelButtons);
			this.Controls.Add(this.labelErrorDescription);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StateChangeDetectionErrorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SCD Error";
			this.panelButtons.ResumeLayout(false);
			this.panelButtons.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSetValues)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTableValues)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelErrorDescription;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonRetry;
		private System.Windows.Forms.Panel panelButtons;
		private System.Windows.Forms.DataGridView dataGridViewValues;
		private System.Data.DataSet dataSetValues;
		private System.Data.DataTable dataTableValues;
		private System.Data.DataColumn dataColumn1;
		private System.Data.DataColumn dataColumn2;
		private System.Data.DataColumn dataColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn description;
		private System.Windows.Forms.DataGridViewTextBoxColumn previousValue;
		private System.Windows.Forms.DataGridViewTextBoxColumn CurrentValue;
	}
}


