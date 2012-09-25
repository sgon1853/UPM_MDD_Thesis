// v3.8.4.5.b
namespace SIGEM.Client.InteractionToolkit
{
	partial class RDLReportPreviewForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public System.Data.DataTable Dt = null;

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
			this.mReportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
			this.SuspendLayout();
			// 
			// mReportViewer
			// 
			this.mReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mReportViewer.Location = new System.Drawing.Point(0, 0);
			this.mReportViewer.Name = "mReportViewer";
			this.mReportViewer.Size = new System.Drawing.Size(694, 437);
			this.mReportViewer.TabIndex = 0;
			// 
			// RDLReportViewerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(694, 437);
			this.Controls.Add(this.mReportViewer);
			this.Name = "RDLReportViewerForm";
			this.Text = "Report Preview";
			this.ResumeLayout(false);

		}

		#endregion

		private Microsoft.Reporting.WinForms.ReportViewer mReportViewer;
	}
}
