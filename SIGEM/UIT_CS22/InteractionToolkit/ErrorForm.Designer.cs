// v3.8.4.5.b
namespace SIGEM.Client.InteractionToolkit
{
	partial class ErrorForm
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
			this.labelErrorDesciption = new System.Windows.Forms.Label();
			this.richTextBoxDetails = new System.Windows.Forms.RichTextBox();
			this.buttonCopy = new System.Windows.Forms.Button();
			this.buttonReport = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonHide = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelErrorDesciption
			// 
			this.labelErrorDesciption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelErrorDesciption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelErrorDesciption.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelErrorDesciption.Location = new System.Drawing.Point(15, 9);
			this.labelErrorDesciption.Name = "labelErrorDesciption";
			this.labelErrorDesciption.Size = new System.Drawing.Size(422, 59);
			this.labelErrorDesciption.TabIndex = 0;
			this.labelErrorDesciption.Text = "Error description";
			this.labelErrorDesciption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.labelErrorDesciption.UseMnemonic = false;
			// 
			// richTextBoxDetails
			// 
			this.richTextBoxDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBoxDetails.BulletIndent = 10;
			this.richTextBoxDetails.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBoxDetails.Location = new System.Drawing.Point(12, 71);
			this.richTextBoxDetails.Name = "richTextBoxDetails";
			this.richTextBoxDetails.ReadOnly = true;
			this.richTextBoxDetails.Size = new System.Drawing.Size(427, 123);
			this.richTextBoxDetails.TabIndex = 2;
			this.richTextBoxDetails.TabStop = false;
			this.richTextBoxDetails.Text = "";
			// 
			// buttonCopy
			// 
			this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonCopy.Location = new System.Drawing.Point(12, 200);
			this.buttonCopy.Name = "buttonCopy";
			this.buttonCopy.Size = new System.Drawing.Size(75, 23);
			this.buttonCopy.TabIndex = 2;
			this.buttonCopy.Text = "Copy";
			this.buttonCopy.UseVisualStyleBackColor = true;
			this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
			// 
			// buttonReport
			// 
			this.buttonReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonReport.Location = new System.Drawing.Point(93, 200);
			this.buttonReport.Name = "buttonReport";
			this.buttonReport.Size = new System.Drawing.Size(75, 23);
			this.buttonReport.TabIndex = 3;
			this.buttonReport.Text = "Report";
			this.buttonReport.UseVisualStyleBackColor = true;
			this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(362, 200);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 0;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonHide
			// 
			this.buttonHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonHide.Location = new System.Drawing.Point(281, 200);
			this.buttonHide.Name = "buttonHide";
			this.buttonHide.Size = new System.Drawing.Size(75, 23);
			this.buttonHide.TabIndex = 1;
			this.buttonHide.Text = "Hide";
			this.buttonHide.UseVisualStyleBackColor = true;
			this.buttonHide.Click += new System.EventHandler(this.buttonHide_Click);
			// 
			// ErrorForm
			// 
			this.AcceptButton = this.buttonClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(450, 230);
			this.Controls.Add(this.buttonHide);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonReport);
			this.Controls.Add(this.buttonCopy);
			this.Controls.Add(this.richTextBoxDetails);
			this.Controls.Add(this.labelErrorDesciption);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ErrorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Error";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelErrorDesciption;
		private System.Windows.Forms.RichTextBox richTextBoxDetails;
		private System.Windows.Forms.Button buttonCopy;
		private System.Windows.Forms.Button buttonReport;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonHide;
	}
}

