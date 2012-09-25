// v3.8.4.5.b
using System.Windows.Forms;
namespace SIGEM.Client.InteractionToolkit
{
	partial class AboutBoxForm
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
			this.components = new System.ComponentModel.Container();
			this.lblAplicationTitle = new System.Windows.Forms.Label();
			this.mTimerText = new System.Windows.Forms.Timer(this.components);
			this.mTimerToStart = new System.Windows.Forms.Timer(this.components);
			this.mTimerToClose = new System.Windows.Forms.Timer(this.components);
			this.lblVersionNumber = new System.Windows.Forms.Label();
			this.pictureBoxClose = new System.Windows.Forms.PictureBox();
			this.lblText = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
			this.SuspendLayout();
			//
			// lblAplicationTitle
			//
			this.lblAplicationTitle.BackColor = System.Drawing.Color.Transparent;
			this.lblAplicationTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAplicationTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
			this.lblAplicationTitle.Location = new System.Drawing.Point(193, 25);
			this.lblAplicationTitle.Name = "lblAplicationTitle";
			this.lblAplicationTitle.Size = new System.Drawing.Size(253, 19);
			this.lblAplicationTitle.TabIndex = 0;
			this.lblAplicationTitle.Text = "About Form Title";
			this.lblAplicationTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			//
			// mTimerToStart
			//
			this.mTimerToStart.Interval = 50;
			this.mTimerToStart.Tick += new System.EventHandler(this.TimerToPaint_Tick);
			//
			// mTimerToClose
			//
			this.mTimerToClose.Interval = 1000;
			this.mTimerToClose.Tag = "0.5";
			this.mTimerToClose.Tick += new System.EventHandler(this.TimerToClose_Tick);
			//
			// lblVersionNumber
			//
			this.lblVersionNumber.BackColor = System.Drawing.Color.Transparent;
			this.lblVersionNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblVersionNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
			this.lblVersionNumber.Location = new System.Drawing.Point(194, 44);
			this.lblVersionNumber.Name = "lblVersionNumber";
			this.lblVersionNumber.Size = new System.Drawing.Size(253, 19);
			this.lblVersionNumber.TabIndex = 3;
			this.lblVersionNumber.Text = "Version";
			this.lblVersionNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			//
			// lblText
			//
			this.lblText.BackColor = System.Drawing.Color.Transparent;
			this.lblText.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
			this.lblText.Location = new System.Drawing.Point(210, 173);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(228, 19);
			this.lblText.TabIndex = 3;
			this.lblText.Text = "Copyright";
			this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			//
			// pictureBoxClose
			//
			this.pictureBoxClose.BackColor = System.Drawing.Color.Transparent;
			this.pictureBoxClose.Image = global::SIGEM.Client.Properties.Resources.close;
			this.pictureBoxClose.Location = new System.Drawing.Point(429, 5);
			this.pictureBoxClose.Name = "pictureBoxClose";
			this.pictureBoxClose.Size = new System.Drawing.Size(16, 16);
			this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBoxClose.TabIndex = 6;
			this.pictureBoxClose.TabStop = false;
			this.pictureBoxClose.MouseLeave += new System.EventHandler(this.pictureBoxClose_MouseLeave);
			this.pictureBoxClose.Click += new System.EventHandler(this.pictureBoxClose_Click);
			this.pictureBoxClose.MouseHover += new System.EventHandler(this.pictureBoxClose_MouseHover);
			//
			// AboutBoxForm
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::SIGEM.Client.Properties.Resources.splashImage;
			this.ClientSize = new System.Drawing.Size(450, 201);
			this.Controls.Add(this.lblText);
			this.Controls.Add(this.pictureBoxClose);
			this.Controls.Add(this.lblVersionNumber);
			this.Controls.Add(this.lblAplicationTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AboutBoxForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AboutBoxForm";
			this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Double_Click);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion

		private Label lblAplicationTitle;
		private Label lblVersionNumber;
		private PictureBox pictureBoxClose;
		private Label lblText;
	}

}

