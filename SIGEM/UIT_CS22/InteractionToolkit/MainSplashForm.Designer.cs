// v3.8.4.5.b
using System.Windows.Forms;

namespace SIGEM.Client.InteractionToolkit
{

	partial class MainSplashForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainSplashForm));
			this.mlblStatus = new System.Windows.Forms.Label();
			this.mpnlStatus = new System.Windows.Forms.Panel();
			this.mlblTimeRemaining = new System.Windows.Forms.Label();
			this.mTimer = new System.Windows.Forms.Timer(this.components);
			this.mTimerToClose = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			//
			// mlblStatus
			//
			this.mlblStatus.BackColor = System.Drawing.Color.Transparent;
			this.mlblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mlblStatus.ForeColor = System.Drawing.Color.Yellow;
			this.mlblStatus.Location = new System.Drawing.Point(190, 99);
			this.mlblStatus.Name = "mlblStatus";
			this.mlblStatus.Size = new System.Drawing.Size(250, 19);
			this.mlblStatus.TabIndex = 0;
			//
			// mpnlStatus
			//
			this.mpnlStatus.BackColor = System.Drawing.Color.Transparent;
			this.mpnlStatus.Location = new System.Drawing.Point(190, 149);
			this.mpnlStatus.Name = "mpnlStatus";
			this.mpnlStatus.Size = new System.Drawing.Size(250, 24);
			this.mpnlStatus.TabIndex = 1;
			this.mpnlStatus.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintProgressBarInPanel);
			//
			// mlblTimeRemaining
			//
			this.mlblTimeRemaining.BackColor = System.Drawing.Color.Transparent;
			this.mlblTimeRemaining.ForeColor = System.Drawing.Color.White;
			this.mlblTimeRemaining.Location = new System.Drawing.Point(161, 176);
			this.mlblTimeRemaining.Name = "mlblTimeRemaining";
			this.mlblTimeRemaining.Size = new System.Drawing.Size(279, 16);
			this.mlblTimeRemaining.TabIndex = 2;
			this.mlblTimeRemaining.Text = "";
			//
			// mTimer
			//
			this.mTimer.Interval = 50;
			this.mTimer.Tick += new System.EventHandler(this.TimerToPaint_Tick);
			//
			// mTimerToClose
			//
			this.mTimerToClose.Interval = 1000;
			this.mTimerToClose.Tag = "0.5";
			this.mTimerToClose.Tick += new System.EventHandler(this.TimerToClose_Tick);
			//
			// MainSplashForm
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::SIGEM.Client.Properties.Resources.splashImage;
			this.ClientSize = new System.Drawing.Size(468, 201);
			this.Controls.Add(this.mlblStatus);
			this.Controls.Add(this.mlblTimeRemaining);
			this.Controls.Add(this.mpnlStatus);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "MainSplashForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MainSplashForm";
			this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Double_Click);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label mlblTimeRemaining;
		private System.Windows.Forms.Timer mTimer;
		private System.Windows.Forms.Label mlblStatus;
		private System.Windows.Forms.Panel mpnlStatus;
		private System.Windows.Forms.Timer mTimerToClose;

	}

}

