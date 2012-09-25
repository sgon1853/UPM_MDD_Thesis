// v3.8.4.5.b
namespace SIGEM.Client.InteractionToolkit
{
	partial class ConditionalNavigationQuestionForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblQuestion = new System.Windows.Forms.Label();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Opcion = new System.Windows.Forms.DataGridViewButtonColumn();
			this.Column1 = new System.Windows.Forms.DataGridViewLinkColumn();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.grpBoxDestinations = new System.Windows.Forms.GroupBox();
			this.radioButton01 = new System.Windows.Forms.RadioButton();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.panel2.SuspendLayout();
			this.grpBoxDestinations.SuspendLayout();
			this.SuspendLayout();
			//
			// panel1
			//
			this.panel1.Controls.Add(this.lblQuestion);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(396, 33);
			this.panel1.TabIndex = 0;
			//
			// lblQuestion
			//
			this.lblQuestion.AutoEllipsis = true;
			this.lblQuestion.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblQuestion.Location = new System.Drawing.Point(3, 9);
			this.lblQuestion.Name = "lblQuestion";
			this.lblQuestion.Size = new System.Drawing.Size(568, 39);
			this.lblQuestion.TabIndex = 0;
			this.lblQuestion.Text = "Question";
			this.lblQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblQuestion.UseCompatibleTextRendering = true;
			//
			// dataGridView1
			//
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.ColumnHeadersVisible = false;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {this.Opcion,this.Column1});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.NullValue = "null data";
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridView1.EnableHeadersVisualStyles = false;
			this.dataGridView1.Location = new System.Drawing.Point(0, 62);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(574, 241);
			this.dataGridView1.TabIndex = 1;
			this.dataGridView1.VirtualMode = true;
			//
			// Opcion
			//
			this.Opcion.HeaderText = "Column1";
			this.Opcion.Name = "Opcion";
			this.Opcion.ReadOnly = true;
			//
			// Column1
			//
			this.Column1.HeaderText = "Column1";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			//
			// panel2
			//
			this.panel2.Controls.Add(this.btnCancel);
			this.panel2.Controls.Add(this.btnOK);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 266);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(574, 37);
			this.panel2.TabIndex = 2;
			//
			// btnCancel
			//
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(313, 8);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			//
			// btnOK
			//
			this.btnOK.Location = new System.Drawing.Point(232, 8);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			//
			// grpBoxDestinations
			//
			this.grpBoxDestinations.Controls.Add(this.radioButton01);
			this.grpBoxDestinations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpBoxDestinations.Location = new System.Drawing.Point(0, 62);
			this.grpBoxDestinations.Name = "grpBoxDestinations";
			this.grpBoxDestinations.Size = new System.Drawing.Size(574, 204);
			this.grpBoxDestinations.TabIndex = 3;
			this.grpBoxDestinations.TabStop = false;
			//
			// radioButton01
			//
			this.radioButton01.AutoSize = true;
			this.radioButton01.Location = new System.Drawing.Point(12, 19);
			this.radioButton01.Name = "radioButton01";
			this.radioButton01.Size = new System.Drawing.Size(14, 13);
			this.radioButton01.TabIndex = 0;
			this.radioButton01.TabStop = true;
			this.radioButton01.UseVisualStyleBackColor = true;
			//
			// ConditionalNavigationQuestionForm
			//
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(574, 303);
			this.ControlBox = false;
			this.Controls.Add(this.grpBoxDestinations);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConditionalNavigationQuestionForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Question";
			this.TopMost = false;
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.panel2.ResumeLayout(false);
			this.grpBoxDestinations.ResumeLayout(false);
			this.grpBoxDestinations.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewButtonColumn Opcion;
		private System.Windows.Forms.DataGridViewLinkColumn Column1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblQuestion;
		private System.Windows.Forms.GroupBox grpBoxDestinations;
		private System.Windows.Forms.RadioButton radioButton01;
		private System.Windows.Forms.Button btnCancel;
	}
}

