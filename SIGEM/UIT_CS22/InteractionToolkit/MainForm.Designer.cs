// v3.8.4.5.b
using System.Windows.Forms;

namespace SIGEM.Client.InteractionToolkit
{
	partial class MainForm
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
				components.Dispose();

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
			this.mMenuBar = new System.Windows.Forms.MenuStrip();
			this.mFileSubMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.mExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mHelpSubMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.mAboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuReports = new System.Windows.Forms.ToolStripMenuItem();
			this.mWindowSubMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.mMaximizeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mMinimizeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mSplitWindowMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mCascadeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mTileHorizontalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mTileVerticalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mSplitWindowMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.mCloseAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mStatusBar = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.toolStripStatusLabelCulture = new System.Windows.Forms.ToolStripStatusLabel();

			this.mMenuBar.SuspendLayout();
			this.mStatusBar.SuspendLayout();
			this.SuspendLayout();

			// Begin HAT.
			#region HAT Menus Constructors.
			this.mValidateAgent = new System.Windows.Forms.ToolStripMenuItem();
			this.mValidateAgent.Name = "MVAgentValidation";
			this.mValidateAgent.Text = "Login";
			this.mValidateAgent.Image = global::SIGEM.Client.Properties.Resources.loginMenu;
			this.mValidateAgent.Click += new System.EventHandler(this.mValidateAgent_Click);
			this.mChangePassword = new System.Windows.Forms.ToolStripMenuItem();
			this.mChangePassword.Name = "MVChangePassWord";
			this.mChangePassword.Text = "Change Password";
			this.mChangePassword.Image = global::SIGEM.Client.Properties.Resources.changePassword; 
			this.mChangePassword.Visible = true;

		// Instance and config menu Items.
		// Menu 0 ->  Aeronave.
		this.mHAT0 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT0.Name = "0";
		this.mHAT0.Text = "Aeronave";
		// Menu 0_0 ->  Crear Aeronave.
		this.mHAT0_0 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT0_0.Name = "0_0";
		this.mHAT0_0.Text = "Crear Aeronave";
		this.mHAT0_0.Image = global::SIGEM.Client.Properties.Resources.Aeronave_SIU_Crear_Aeronave;
		// Menu 0_1 ->  Aeronave.
		this.mHAT0_1 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT0_1.Name = "0_1";
		this.mHAT0_1.Text = "Aeronave";
		this.mHAT0_1.Image = global::SIGEM.Client.Properties.Resources.Aeronave_PIU_PIU_Aeronave;

		// Instance and config menu Items.
		// Menu 1 ->  Nave Nodriza.
		this.mHAT1 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT1.Name = "1";
		this.mHAT1.Text = "Nave Nodriza";
		// Menu 1_0 ->  NaveNodriza.
		this.mHAT1_0 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT1_0.Name = "1_0";
		this.mHAT1_0.Text = "NaveNodriza";
		this.mHAT1_0.Image = global::SIGEM.Client.Properties.Resources.NaveNodriza_PIU_PIU_NaveNodriza;
		// Menu 1_1 ->  Crear nave nodriza.
		this.mHAT1_1 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT1_1.Name = "1_1";
		this.mHAT1_1.Text = "Crear nave nodriza";
		this.mHAT1_1.Image = global::SIGEM.Client.Properties.Resources.NaveNodriza_SIU_Crear_NaveNodriza;

		// Instance and config menu Items.
		// Menu 2 ->  Pasajero.
		this.mHAT2 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT2.Name = "2";
		this.mHAT2.Text = "Pasajero";
		// Menu 2_0 ->  New.
		this.mHAT2_0 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT2_0.Name = "2_0";
		this.mHAT2_0.Text = "New";
		this.mHAT2_0.Image = global::SIGEM.Client.Properties.Resources.Pasajero_SIU_SIU_create_instance;
		// Menu 2_1 ->  Pasajero.
		this.mHAT2_1 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT2_1.Name = "2_1";
		this.mHAT2_1.Text = "Pasajero";
		this.mHAT2_1.Image = global::SIGEM.Client.Properties.Resources.Pasajero_PIU_PIU_Pasajero;

		// Instance and config menu Items.
		// Menu 3 ->  Ocupacion aeronave.
		this.mHAT3 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT3.Name = "3";
		this.mHAT3.Text = "Ocupacion aeronave";
		// Menu 3_0 ->  New.
		this.mHAT3_0 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT3_0.Name = "3_0";
		this.mHAT3_0.Text = "New";
		this.mHAT3_0.Image = global::SIGEM.Client.Properties.Resources.PasajeroAeronave_SIU_SIU_create_instance;
		// Menu 3_1 ->  PasajeroAeronave.
		this.mHAT3_1 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT3_1.Name = "3_1";
		this.mHAT3_1.Text = "PasajeroAeronave";
		this.mHAT3_1.Image = global::SIGEM.Client.Properties.Resources.PasajeroAeronave_MDIU_MDIU_PasajeroAeronave;

		// Instance and config menu Items.
		// Menu 4 ->  Revision Aeronave.
		this.mHAT4 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT4.Name = "4";
		this.mHAT4.Text = "Revision Aeronave";
		// Menu 4_0 ->  New.
		this.mHAT4_0 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT4_0.Name = "4_0";
		this.mHAT4_0.Text = "New";
		this.mHAT4_0.Image = global::SIGEM.Client.Properties.Resources.Revision_SIU_SIU_create_instance;
		// Menu 4_1 ->  RevisionPasajero.
		this.mHAT4_1 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT4_1.Name = "4_1";
		this.mHAT4_1.Text = "RevisionPasajero";
		this.mHAT4_1.Image = global::SIGEM.Client.Properties.Resources.RevisionPasajero_PIU_PIU_RevisionPasajero;
		// Menu 4_2 ->  Revision.
		this.mHAT4_2 = new System.Windows.Forms.ToolStripMenuItem();
		this.mHAT4_2.Name = "4_2";
		this.mHAT4_2.Text = "Revision";
		this.mHAT4_2.Image = global::SIGEM.Client.Properties.Resources.Revision_IIU_IIU_Revision;
			// 
			// mnuReports
			// 
			this.mnuReports.Name = "mnuReports";
			this.mnuReports.Size = new System.Drawing.Size(66, 20);
			this.mnuReports.Text = "Informes";
			#endregion HAT Menus Constructors.
			// End HAT.


			// 
			// mMenuBar
			// 
			this.mMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mFileSubMenu
			,this.mHAT0
			,this.mHAT1
			,this.mHAT2
			,this.mHAT3
			,this.mHAT4
			 ,this.mnuReports
			 ,this.mWindowSubMenu
			 ,this.mHelpSubMenu});


 		// Nodo 0.
		this.mHAT0.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mHAT0_0
			, this.mHAT0_1
		});
 		// Nodo 1.
		this.mHAT1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mHAT1_0
			, this.mHAT1_1
		});
 		// Nodo 2.
		this.mHAT2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mHAT2_0
			, this.mHAT2_1
		});
 		// Nodo 3.
		this.mHAT3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mHAT3_0
			, this.mHAT3_1
		});
 		// Nodo 4.
		this.mHAT4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mHAT4_0
			, this.mHAT4_1
			, this.mHAT4_2
		});
			this.mMenuBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.mMenuBar.Location = new System.Drawing.Point(0, 0);
			this.mMenuBar.MdiWindowListItem = this.mWindowSubMenu;
			this.mMenuBar.Name = "mMenuBar";
			this.mMenuBar.Size = new System.Drawing.Size(632, 24);
			this.mMenuBar.TabIndex = 0;
			this.mMenuBar.Text = "MenuStrip";
			// 
			// mFileSubMenu
			// 
			this.mFileSubMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.mValidateAgent,
				this.mChangePassword,
				this.mExitMenuItem
			});
			this.mFileSubMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
			this.mFileSubMenu.Name = "mFileSubMenu";
			this.mFileSubMenu.Size = new System.Drawing.Size(35, 20);
			this.mFileSubMenu.Text = "File";
			// 
			// mChangePassword
			// 
			this.mChangePassword.Name = "mChangePassword";
			this.mChangePassword.Size = new System.Drawing.Size(162, 22);
			this.mChangePassword.Text = "Change Password";
			this.mChangePassword.Click += new System.EventHandler(this.mChangePassword_Click);
			// 
			// mExitMenuItem
			// 
			this.mExitMenuItem.Name = "mExitMenuItem";
			this.mExitMenuItem.Size = new System.Drawing.Size(152, 22);
			this.mExitMenuItem.Text = "Exit";
			this.mExitMenuItem.Click += new System.EventHandler(this.mExitMenuItem_Click);
			this.mExitMenuItem.Image = global::SIGEM.Client.Properties.Resources.exit; 
			// 
			// mHelpSubMenu
			// 
			this.mHelpSubMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mAboutMenuItem});
			this.mHelpSubMenu.Name = "mHelpSubMenu";
			this.mHelpSubMenu.Size = new System.Drawing.Size(40, 20);
			this.mHelpSubMenu.Text = "Help";

			// 
			// mAboutMenuItem
			// 
			this.mAboutMenuItem.Name = "mAboutMenuItem";
			this.mAboutMenuItem.Image = global::SIGEM.Client.Properties.Resources.help; 
			this.mAboutMenuItem.Size = new System.Drawing.Size(152, 22);
			this.mAboutMenuItem.Text = "About ...";
			this.mAboutMenuItem.Click += new System.EventHandler(this.mAboutMenuItem_Click);

			// 
			// mStatusBar
			// 
			this.mStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.toolStripStatusLabel,
				this.toolStripStatusLabelCulture});
			this.mStatusBar.Location = new System.Drawing.Point(0, 431);
			this.mStatusBar.Name = "mStatusBar";
			this.mStatusBar.Size = new System.Drawing.Size(632, 22);
			this.mStatusBar.TabIndex = 2;
			this.mStatusBar.Text = "StatusStrip";
			// 
			// mWindowSubMenu
			// 
			this.mWindowSubMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.mMaximizeMenuItem,
				this.mMinimizeMenuItem,
				this.mSplitWindowMenuItem1,
				this.mCascadeMenuItem,
				this.mTileHorizontalMenuItem,
				this.mTileVerticalMenuItem,
				this.mSplitWindowMenuItem2,
				this.mCloseAllMenuItem});
			this.mWindowSubMenu.Name = "mWindowSubMenu";
			this.mWindowSubMenu.Size = new System.Drawing.Size(57, 17);
			this.mWindowSubMenu.Text = "Window";
			// 
			// mMaximizeMenuItem
			// 
			this.mMaximizeMenuItem.Name = "mMaximizeMenuItem";
			this.mMaximizeMenuItem.Size = new System.Drawing.Size(152, 22);
			this.mMaximizeMenuItem.Image = global::SIGEM.Client.Properties.Resources.maximize;
			this.mMaximizeMenuItem.Text = "Maximize";
			this.mMaximizeMenuItem.Click += new System.EventHandler(this.mMaximizeMenuItem_Click);
			// 
			// mMinimizeMenuItem
			// 
			this.mMinimizeMenuItem.Name = "mMinimizeMenuItem";
			this.mMinimizeMenuItem.Size = new System.Drawing.Size(152, 22);
			this.mMinimizeMenuItem.Image = global::SIGEM.Client.Properties.Resources.minimize;
			this.mMinimizeMenuItem.Text = "Minimize";
			this.mMinimizeMenuItem.Click += new System.EventHandler(this.mMinimizeMenuItem_Click);
			// 
			// mSplitWindowMenuItem1
			// 
			this.mSplitWindowMenuItem1.Name = "mSplitWindowMenuItem1";
			this.mSplitWindowMenuItem1.Size = new System.Drawing.Size(149, 6);
			// 
			// mCascadeMenuItem
			// 
			this.mCascadeMenuItem.Name = "mCascadeMenuItem";
			this.mCascadeMenuItem.Size = new System.Drawing.Size(152, 22);
			this.mCascadeMenuItem.Image = global::SIGEM.Client.Properties.Resources.cascade;
			this.mCascadeMenuItem.Text = "Cascade";
			this.mCascadeMenuItem.Click += new System.EventHandler(this.mCascadeMenuItem_Click);
			// 
			// mTileHorizontalMenuItem
			// 
			this.mTileHorizontalMenuItem.Name = "mTileHorizontalMenuItem";
			this.mTileHorizontalMenuItem.Size = new System.Drawing.Size(152, 22);
			this.mTileHorizontalMenuItem.Image = global::SIGEM.Client.Properties.Resources.tileHorizontal;
			this.mTileHorizontalMenuItem.Text = "Tile Horizontal";
			this.mTileHorizontalMenuItem.Click += new System.EventHandler(this.mTileHorizontalMenuItem_Click);
			// 
			// mTileVerticalMenuItem
			// 
			this.mTileVerticalMenuItem.Name = "mTileVerticalMenuItem";
			this.mTileVerticalMenuItem.Size = new System.Drawing.Size(152, 22);
			this.mTileVerticalMenuItem.Image = global::SIGEM.Client.Properties.Resources.tileVertical;
			this.mTileVerticalMenuItem.Text = "Tile Vertical";
			this.mTileVerticalMenuItem.Click += new System.EventHandler(this.mTileVerticalMenuItem_Click);
			// 
			// mSplitWindowMenuItem2
			// 
			this.mSplitWindowMenuItem2.Name = "mSplitWindowMenuItem2";
			this.mSplitWindowMenuItem2.Size = new System.Drawing.Size(149, 6);
			// 
			// mCloseAllMenuItem
			// 
			this.mCloseAllMenuItem.Name = "mCloseAllMenuItem";
			this.mCloseAllMenuItem.Size = new System.Drawing.Size(152, 22);
			this.mCloseAllMenuItem.Image = global::SIGEM.Client.Properties.Resources.closeAll;
			this.mCloseAllMenuItem.Text = "Close All";
			this.mCloseAllMenuItem.Click += new System.EventHandler(this.mCloseAllMenuItem_Click);
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
				| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
				| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.toolStripStatusLabel.Image = global::SIGEM.Client.Properties.Resources.agent;
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(58, 20);
			this.toolStripStatusLabel.Text = "Status";
			// 
			// toolStripStatusLabelCulture
			// 
			this.toolStripStatusLabelCulture.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
				| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
				| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabelCulture.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
			this.toolStripStatusLabelCulture.Margin = new System.Windows.Forms.Padding(1, 3, 0, 2);
			this.toolStripStatusLabelCulture.Name = "toolStripStatusLabelCulture";
			this.toolStripStatusLabelCulture.Size = new System.Drawing.Size(46, 17);
			this.toolStripStatusLabelCulture.Text = "Culture";

			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(632, 453);
			this.Controls.Add(this.mStatusBar);
			this.Controls.Add(this.mMenuBar);
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.mMenuBar;
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.mMenuBar.ResumeLayout(false);
			this.mMenuBar.PerformLayout();
			this.mStatusBar.ResumeLayout(false);
			this.mStatusBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private System.Windows.Forms.MenuStrip mMenuBar;
		private System.Windows.Forms.StatusStrip mStatusBar;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.ToolStripMenuItem mAboutMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mFileSubMenu;
		private System.Windows.Forms.ToolStripMenuItem mExitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mHelpSubMenu;
		private System.Windows.Forms.ToolTip ToolTip;

		private ToolStripMenuItem mValidateAgent;
		private ToolStripMenuItem mChangePassword;
		private ToolStripStatusLabel toolStripStatusLabelCulture;
		private ToolStripMenuItem mnuReports;
		private ToolStripMenuItem mWindowSubMenu;
		private ToolStripMenuItem mMaximizeMenuItem;
		private ToolStripMenuItem mMinimizeMenuItem;
		private ToolStripSeparator mSplitWindowMenuItem1;
		private ToolStripMenuItem mCascadeMenuItem;
		private ToolStripMenuItem mTileHorizontalMenuItem;
		private ToolStripMenuItem mTileVerticalMenuItem;
		private ToolStripSeparator mSplitWindowMenuItem2;
		private ToolStripMenuItem mCloseAllMenuItem;

		// Begin HAT.----------------------------------------------------
		#region HAT Menus.
		// Menu Items.
		/// <summary>
		/// /// Aeronave
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT0; //// Aeronave
		/// <summary>
		/// /// Crear Aeronave
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT0_0; //// Crear Aeronave
		/// <summary>
		/// /// Aeronave
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT0_1; //// Aeronave
		// Menu Items.
		/// <summary>
		/// /// Nave Nodriza
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT1; //// Nave Nodriza
		/// <summary>
		/// /// NaveNodriza
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT1_0; //// NaveNodriza
		/// <summary>
		/// /// Crear nave nodriza
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT1_1; //// Crear nave nodriza
		// Menu Items.
		/// <summary>
		/// /// Pasajero
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT2; //// Pasajero
		/// <summary>
		/// /// New
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT2_0; //// New
		/// <summary>
		/// /// Pasajero
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT2_1; //// Pasajero
		// Menu Items.
		/// <summary>
		/// /// Ocupacion aeronave
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT3; //// Ocupacion aeronave
		/// <summary>
		/// /// New
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT3_0; //// New
		/// <summary>
		/// /// PasajeroAeronave
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT3_1; //// PasajeroAeronave
		// Menu Items.
		/// <summary>
		/// /// Revision Aeronave
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT4; //// Revision Aeronave
		/// <summary>
		/// /// New
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT4_0; //// New
		/// <summary>
		/// /// RevisionPasajero
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT4_1; //// RevisionPasajero
		/// <summary>
		/// /// Revision
		/// </summary>
		private System.Windows.Forms.ToolStripMenuItem mHAT4_2; //// Revision
		#endregion HAT Menus.
		// End HAT.----------------------------------------------------
		
	}
}
