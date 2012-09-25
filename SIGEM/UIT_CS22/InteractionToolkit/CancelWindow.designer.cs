// v3.8.4.5.b
using System.Windows.Forms;
namespace SIGEM.Client
{
	partial class CancelWindow
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
			this.prgBarExecution = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(117, 32);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(90, 26);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// prgBarExecution
			// 
			this.prgBarExecution.Location = new System.Drawing.Point(12, 12);
			this.prgBarExecution.Name = "prgBarExecution";
			this.prgBarExecution.Size = new System.Drawing.Size(300, 14);
			this.prgBarExecution.TabIndex = 1;
			// 
			// CancelWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(324, 64);
			this.ControlBox = false;
			this.Controls.Add(this.prgBarExecution);
			this.Controls.Add(this.btnCancel);
			this.Name = "CancelWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Processing ...";
			this.ResumeLayout(false);
		}
	
		#endregion
	
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ProgressBar prgBarExecution;
	}
}

