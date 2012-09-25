// v3.8.4.5.b
namespace SIGEM.Client.InteractionToolkit
{
	partial class PrintForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblNameOfPrint = new System.Windows.Forms.Label();
			this.lblSelectPrinter = new System.Windows.Forms.Label();
			this.btnPrinter = new System.Windows.Forms.Button();
			this.grpCopies = new System.Windows.Forms.GroupBox();
			this.numUpDownCopies = new System.Windows.Forms.NumericUpDown();
			this.grpOutPut = new System.Windows.Forms.GroupBox();
			this.rdoBtnPrint = new System.Windows.Forms.RadioButton();
			this.rdoBtnPreview = new System.Windows.Forms.RadioButton();
			this.printDlg = new System.Windows.Forms.PrintDialog();
			this.grbFrm = new System.Windows.Forms.GroupBox();
			this.lblSelectTemplate = new System.Windows.Forms.Label();
			this.cmbBoxSelectTemplate = new System.Windows.Forms.ComboBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.grpCopies.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numUpDownCopies)).BeginInit();
			this.grpOutPut.SuspendLayout();
			this.grbFrm.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblNameOfPrint);
			this.groupBox1.Controls.Add(this.lblSelectPrinter);
			this.groupBox1.Controls.Add(this.btnPrinter);
			this.groupBox1.Location = new System.Drawing.Point(8, 119);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(389, 49);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			// 
			// lblNameOfPrint
			// 
			this.lblNameOfPrint.Location = new System.Drawing.Point(160, 13);
			this.lblNameOfPrint.Name = "lblNameOfPrint";
			this.lblNameOfPrint.Size = new System.Drawing.Size(224, 28);
			this.lblNameOfPrint.TabIndex = 17;
			this.lblNameOfPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblSelectPrinter
			// 
			this.lblSelectPrinter.Location = new System.Drawing.Point(4, 19);
			this.lblSelectPrinter.Name = "lblSelectPrinter";
			this.lblSelectPrinter.Size = new System.Drawing.Size(110, 15);
			this.lblSelectPrinter.TabIndex = 16;
			this.lblSelectPrinter.Text = "Select printer";
			// 
			// btnPrinter
			// 
			this.btnPrinter.Image = ((System.Drawing.Image)(resources.GetObject("btnPrinter.Image")));
			this.btnPrinter.Location = new System.Drawing.Point(124, 11);
			this.btnPrinter.Name = "btnPrinter";
			this.btnPrinter.Size = new System.Drawing.Size(32, 32);
			this.btnPrinter.TabIndex = 0;
			this.btnPrinter.Click += new System.EventHandler(this.btnPrinter_Click);
			// 
			// grpCopies
			// 
			this.grpCopies.Controls.Add(this.numUpDownCopies);
			this.grpCopies.Location = new System.Drawing.Point(269, 44);
			this.grpCopies.Name = "grpCopies";
			this.grpCopies.Size = new System.Drawing.Size(128, 40);
			this.grpCopies.TabIndex = 2;
			this.grpCopies.TabStop = false;
			this.grpCopies.Text = "Number of copies";
			// 
			// numUpDownCopies
			// 
			this.numUpDownCopies.Location = new System.Drawing.Point(48, 16);
			this.numUpDownCopies.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numUpDownCopies.Name = "numUpDownCopies";
			this.numUpDownCopies.Size = new System.Drawing.Size(72, 20);
			this.numUpDownCopies.TabIndex = 0;
			this.numUpDownCopies.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numUpDownCopies.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// grpOutPut
			// 
			this.grpOutPut.Controls.Add(this.rdoBtnPrint);
			this.grpOutPut.Controls.Add(this.rdoBtnPreview);
			this.grpOutPut.Location = new System.Drawing.Point(8, 44);
			this.grpOutPut.Name = "grpOutPut";
			this.grpOutPut.Size = new System.Drawing.Size(202, 72);
			this.grpOutPut.TabIndex = 1;
			this.grpOutPut.TabStop = false;
			this.grpOutPut.Text = "Output";
			// 
			// rdoBtnPrint
			// 
			this.rdoBtnPrint.Checked = true;
			this.rdoBtnPrint.Location = new System.Drawing.Point(16, 21);
			this.rdoBtnPrint.Name = "rdoBtnPrint";
			this.rdoBtnPrint.Size = new System.Drawing.Size(168, 16);
			this.rdoBtnPrint.TabIndex = 0;
			this.rdoBtnPrint.TabStop = true;
			this.rdoBtnPrint.Text = "Print";
			// 
			// rdoBtnPreview
			// 
			this.rdoBtnPreview.Location = new System.Drawing.Point(16, 45);
			this.rdoBtnPreview.Name = "rdoBtnPreview";
			this.rdoBtnPreview.Size = new System.Drawing.Size(168, 16);
			this.rdoBtnPreview.TabIndex = 0;
			this.rdoBtnPreview.Text = "Preview";
			// 
			// printDlg
			// 
			this.printDlg.AllowSelection = true;
			this.printDlg.ShowHelp = true;
			// 
			// grbFrm
			// 
			this.grbFrm.Controls.Add(this.lblSelectTemplate);
			this.grbFrm.Controls.Add(this.cmbBoxSelectTemplate);
			this.grbFrm.Controls.Add(this.grpOutPut);
			this.grbFrm.Controls.Add(this.groupBox1);
			this.grbFrm.Controls.Add(this.grpCopies);
			this.grbFrm.Location = new System.Drawing.Point(9, 8);
			this.grbFrm.Name = "grbFrm";
			this.grbFrm.Size = new System.Drawing.Size(405, 177);
			this.grbFrm.TabIndex = 0;
			this.grbFrm.TabStop = false;
			this.grbFrm.Text = "Print Options";
			// 
			// lblSelectTemplate
			// 
			this.lblSelectTemplate.Location = new System.Drawing.Point(13, 16);
			this.lblSelectTemplate.Name = "lblSelectTemplate";
			this.lblSelectTemplate.Size = new System.Drawing.Size(96, 23);
			this.lblSelectTemplate.TabIndex = 26;
			this.lblSelectTemplate.Text = "Select template";
			this.lblSelectTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cmbBoxSelectTemplate
			// 
			this.cmbBoxSelectTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbBoxSelectTemplate.Location = new System.Drawing.Point(109, 16);
			this.cmbBoxSelectTemplate.Name = "cmbBoxSelectTemplate";
			this.cmbBoxSelectTemplate.Size = new System.Drawing.Size(288, 21);
			this.cmbBoxSelectTemplate.TabIndex = 0;
			// 
			// btnOk
			// 
			this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
			this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnOk.Location = new System.Drawing.Point(222, 191);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(88, 24);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "OK";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCancel.Location = new System.Drawing.Point(318, 191);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 24);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			// 
			// PrintForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(423, 221);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.grbFrm);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PrintForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Print";
			this.groupBox1.ResumeLayout(false);
			this.grpCopies.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numUpDownCopies)).EndInit();
			this.grpOutPut.ResumeLayout(false);
			this.grbFrm.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblNameOfPrint;
		private System.Windows.Forms.Label lblSelectPrinter;
		private System.Windows.Forms.Button btnPrinter;
		private System.Windows.Forms.GroupBox grpCopies;
		private System.Windows.Forms.NumericUpDown numUpDownCopies;
		protected System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox grpOutPut;
		private System.Windows.Forms.RadioButton rdoBtnPrint;
		private System.Windows.Forms.RadioButton rdoBtnPreview;
		private System.Windows.Forms.PrintDialog printDlg;
		protected System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox grbFrm;
		private System.Windows.Forms.Label lblSelectTemplate;
		private System.Windows.Forms.ComboBox cmbBoxSelectTemplate;
	}
}
