
 

// v3.8.4.5.b
namespace SIGEM.Client.InteractionToolkit.Aeronave.IUInstances
{
	public partial class _Auto_IT
	{

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
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

			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripExportExcel = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripExportWord = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorOptions = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripPreferences = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSavePositions = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.pnlOIDSelector = new System.Windows.Forms.Panel();
			this.lOIDSelector = new System.Windows.Forms.Label();
			this.maskedTextBoxid_Aeronave1 = new System.Windows.Forms.MaskedTextBox();
			this.bOIDSelector = new System.Windows.Forms.Button();
			this.lSIOIDSelector = new System.Windows.Forms.Label();
			this.pnlActions = new System.Windows.Forms.Panel();
			this.toolstripActions = new System.Windows.Forms.ToolStrip();
			this.mnuActions_0 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolstripActions_0 = new System.Windows.Forms.ToolStripButton();
			this.mnuActions_1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolstripActions_1 = new System.Windows.Forms.ToolStripButton();
			this.mnuActions_2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolstripActions_2 = new System.Windows.Forms.ToolStripButton();
			this.mnuActions_3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolstripActions_3 = new System.Windows.Forms.ToolStripButton();
			this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripPrint = new System.Windows.Forms.ToolStripButton();
			this.pnlNavigations = new System.Windows.Forms.Panel();
			this.toolstripNavigations = new System.Windows.Forms.ToolStrip();
			this.mnuNavigations = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuNavigations_0 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolstripNavigations_0 = new System.Windows.Forms.ToolStripButton();
			this.pnlDisplaySet = new System.Windows.Forms.Panel();
			this.lstViewDisplaySet = new System.Windows.Forms.ListView();
			this.lstViewDisplaySetName = new System.Windows.Forms.ColumnHeader();
			this.lstViewDisplaySetValue = new System.Windows.Forms.ColumnHeader();
            this.panelFormInstanceButtons = new System.Windows.Forms.Panel();
            this.bCancel = new System.Windows.Forms.Button();
			this.contextMenuStrip.SuspendLayout();
			this.pnlOIDSelector.SuspendLayout();
			this.pnlActions.SuspendLayout();
			this.toolstripActions.SuspendLayout();
			this.pnlNavigations.SuspendLayout();
			this.toolstripNavigations.SuspendLayout();
            this.pnlDisplaySet.SuspendLayout();
            this.panelFormInstanceButtons.SuspendLayout();
            this.SuspendLayout();
			//
			// contextMenuStrip
			//
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.toolStripHelp,
				this.optionsToolStripMenuItem,
				this.toolStripSeparator1,
				this.mnuActions_0,
				this.mnuActions_1,
				this.mnuActions_2,
				this.mnuActions_3,
				this.mnuPrint,
				this.toolStripSeparator2,
				this.mnuNavigations});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(131, 148);
			//
			// toolStripHelp
			//
			this.toolStripHelp.Image = global::SIGEM.Client.Properties.Resources.help;
			this.toolStripHelp.Name = "toolStripHelp";
			this.toolStripHelp.Size = new System.Drawing.Size(130, 22);
			this.toolStripHelp.Text = "Help";
			//
			// optionsToolStripMenuItem
			//
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.toolStripExportExcel,
				this.toolStripExportWord,
				this.toolStripSeparatorOptions,
				this.toolStripRefresh,
				this.toolStripPreferences,
				this.toolStripSavePositions});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.optionsToolStripMenuItem.Text = "Options";
			//
			// toolStripExportExcel
			//
			this.toolStripExportExcel.Image = global::SIGEM.Client.Properties.Resources.exportToExcel;
			this.toolStripExportExcel.Name = "toolStripExportExcel";
			this.toolStripExportExcel.Size = new System.Drawing.Size(67, 22);
			this.toolStripExportExcel.Text = "Export To Excel";
			//
			// toolStripExportWord
			//
			this.toolStripExportWord.Image = global::SIGEM.Client.Properties.Resources.exportToWord;
			this.toolStripExportWord.Name = "toolStripExportWord";
			this.toolStripExportWord.Size = new System.Drawing.Size(67, 22);
			this.toolStripExportWord.Text = "Export To Word";
			this.toolStripExportWord.Visible = false;
			//
			// toolStripSeparatorOptions
			//
			this.toolStripSeparatorOptions.Name = "toolStripSeparatorOptions";
			this.toolStripSeparatorOptions.Size = new System.Drawing.Size(64, 6);
			//
			// toolStripRefresh
			//
			this.toolStripRefresh.Image = global::SIGEM.Client.Properties.Resources.refresh;
			this.toolStripRefresh.Name = "toolStripRefresh";
			this.toolStripRefresh.Size = new System.Drawing.Size(67, 22);
			this.toolStripRefresh.Text = "Refresh";
			//
			// toolStripPreferences
			//
			this.toolStripPreferences.Image = global::SIGEM.Client.Properties.Resources.preferences;
			this.toolStripPreferences.Name = "toolStripPreferences";
			this.toolStripPreferences.Size = new System.Drawing.Size(67, 22);
			this.toolStripPreferences.Text = "Preferences";
			//
			// toolStripSavePositions
			//
			this.toolStripSavePositions.Image = global::SIGEM.Client.Properties.Resources.savePosition;
			this.toolStripSavePositions.Name = "toolStripSavePositions";
			this.toolStripSavePositions.Size = new System.Drawing.Size(67, 22);
			this.toolStripSavePositions.Text = "Save position";
			//
			// toolStripSeparator1
			//
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(127, 6);
			//
			// mnuActions_0
			//
			this.mnuActions_0.Image = global::SIGEM.Client.Properties.Resources.Aeronave_SIU_Crear_Aeronave;
			this.mnuActions_0.Name = "mnuActions_0";
			this.mnuActions_0.Size = new System.Drawing.Size(130, 22);
			//
			// mnuActions_1
			//
			this.mnuActions_1.Image = global::SIGEM.Client.Properties.Resources.Aeronave_SIU_SIU_delete_instance;
			this.mnuActions_1.Name = "mnuActions_1";
			this.mnuActions_1.Size = new System.Drawing.Size(130, 22);
			//
			// mnuActions_2
			//
			this.mnuActions_2.Image = global::SIGEM.Client.Properties.Resources.Aeronave_SIU_SIU_edit_instance;
			this.mnuActions_2.Name = "mnuActions_2";
			this.mnuActions_2.Size = new System.Drawing.Size(130, 22);
			//
			// mnuActions_3
			//
			this.mnuActions_3.Image = global::SIGEM.Client.Properties.Resources.Aeronave_IIU__Auto_;
			this.mnuActions_3.Name = "mnuActions_3";
			this.mnuActions_3.Size = new System.Drawing.Size(130, 22);
			//
			// mnuPrint
			//
			this.mnuPrint.Image = global::SIGEM.Client.Properties.Resources.print;
			this.mnuPrint.Name = "mnuPrint";
			this.mnuPrint.Size = new System.Drawing.Size(130, 22);
			//
			// toolStripSeparator2
			//
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(127, 6);
			//
			// mnuNavigations
			//
			this.mnuNavigations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.mnuNavigations_0});
			this.mnuNavigations.Name = "mnuNavigations";
			this.mnuNavigations.Size = new System.Drawing.Size(130, 22);
			this.mnuNavigations.Text = "Navigations";
			//
			// mnuNavigations_0
			//
			this.mnuNavigations_0.Name = "mnuNavigations_0";
			this.mnuNavigations_0.Size = new System.Drawing.Size(67, 22);
			//
			// pnlOIDSelector
			//
			this.pnlOIDSelector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlOIDSelector.Controls.Add(this.lOIDSelector);
			this.pnlOIDSelector.Controls.Add(this.maskedTextBoxid_Aeronave1);
			this.pnlOIDSelector.Controls.Add(this.bOIDSelector);
			this.pnlOIDSelector.Controls.Add(this.lSIOIDSelector);
			this.pnlOIDSelector.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlOIDSelector.Location = new System.Drawing.Point(0, 0);
			this.pnlOIDSelector.Name = "pnlOIDSelector";
			this.pnlOIDSelector.Size = new System.Drawing.Size(620, 32);
			this.pnlOIDSelector.TabIndex = 0;
			//
			// lOIDSelector
			//
			this.lOIDSelector.AutoEllipsis = true;
			this.lOIDSelector.Location = new System.Drawing.Point(10, 9);
			this.lOIDSelector.Name = "lOIDSelector";
			this.lOIDSelector.Size = new System.Drawing.Size(90, 16);
			this.lOIDSelector.TabIndex = 0;
			this.lOIDSelector.Text = "Aeronave";
			//
			// maskedTextBoxid_Aeronave1
			//
			this.maskedTextBoxid_Aeronave1.Location = new System.Drawing.Point(102, 6);
			this.maskedTextBoxid_Aeronave1.Margin = new System.Windows.Forms.Padding(2);
			this.maskedTextBoxid_Aeronave1.Name = "maskedTextBoxid_Aeronave1";
			this.maskedTextBoxid_Aeronave1.Size = new System.Drawing.Size(72, 20);
			this.maskedTextBoxid_Aeronave1.TabIndex = 0;
			//
			// bOIDSelector
			//
			this.bOIDSelector.Image = global::SIGEM.Client.Properties.Resources.search;
			this.bOIDSelector.Location = new System.Drawing.Point(175, 5);
			this.bOIDSelector.Margin = new System.Windows.Forms.Padding(2);
			this.bOIDSelector.Name = "bOIDSelector";
			this.bOIDSelector.Size = new System.Drawing.Size(22, 22);
			this.bOIDSelector.TabIndex = 1;
			this.bOIDSelector.UseVisualStyleBackColor = true;
			//
			// lSIOIDSelector
			//
			this.lSIOIDSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lSIOIDSelector.AutoEllipsis = true;
			this.lSIOIDSelector.Location = new System.Drawing.Point(199, 9);
			this.lSIOIDSelector.Name = "lSIOIDSelector";
			this.lSIOIDSelector.Size = new System.Drawing.Size(330, 16);
			this.lSIOIDSelector.TabIndex = 0;
			//
			// pnlActions
			//
			this.pnlActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlActions.Controls.Add(this.toolstripActions);
			this.pnlActions.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlActions.Location = new System.Drawing.Point(620, 32);
			this.pnlActions.Name = "pnlActions";
			this.pnlActions.Size = new System.Drawing.Size(30, 140);
			this.pnlActions.TabIndex = 2;
			//
			// toolstripActions
			//
			this.toolstripActions.AutoSize = false;
			this.toolstripActions.BackColor = System.Drawing.SystemColors.ControlLight;
			this.toolstripActions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolstripActions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolstripActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.toolstripActions_0,
				this.toolstripActions_1,
				this.toolstripActions_2,
				this.toolstripActions_3,
				this.toolStripPrint});
			this.toolstripActions.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
			this.toolstripActions.Location = new System.Drawing.Point(0, 0);
			this.toolstripActions.Name = "toolstripActions";
			this.toolstripActions.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolstripActions.Size = new System.Drawing.Size(28, 138);
			this.toolstripActions.TabIndex = 2;
			//
			// toolstripActions_0
			//
			this.toolstripActions_0.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolstripActions_0.Image = global::SIGEM.Client.Properties.Resources.Aeronave_SIU_Crear_Aeronave;
			this.toolstripActions_0.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolstripActions_0.Name = "toolstripActions_0";
			this.toolstripActions_0.Size = new System.Drawing.Size(26, 20);
			this.toolstripActions_0.Text = "Crear Aeronave";
			//
			// toolstripActions_1
			//
			this.toolstripActions_1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolstripActions_1.Image = global::SIGEM.Client.Properties.Resources.Aeronave_SIU_SIU_delete_instance;
			this.toolstripActions_1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolstripActions_1.Name = "toolstripActions_1";
			this.toolstripActions_1.Size = new System.Drawing.Size(26, 20);
			this.toolstripActions_1.Text = "Destroy";
			//
			// toolstripActions_2
			//
			this.toolstripActions_2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolstripActions_2.Image = global::SIGEM.Client.Properties.Resources.Aeronave_SIU_SIU_edit_instance;
			this.toolstripActions_2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolstripActions_2.Name = "toolstripActions_2";
			this.toolstripActions_2.Size = new System.Drawing.Size(26, 20);
			this.toolstripActions_2.Text = "Edit";
			//
			// toolstripActions_3
			//
			this.toolstripActions_3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolstripActions_3.Image = global::SIGEM.Client.Properties.Resources.Aeronave_IIU__Auto_;
			this.toolstripActions_3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolstripActions_3.Name = "toolstripActions_3";
			this.toolstripActions_3.Size = new System.Drawing.Size(26, 20);
			this.toolstripActions_3.Text = "Aeronave";
			//
			// toolStripPrint
			//
			this.toolStripPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripPrint.Image = global::SIGEM.Client.Properties.Resources.print;
			this.toolStripPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripPrint.Name = "toolStripPrint";
			this.toolStripPrint.Size = new System.Drawing.Size(26, 20);
			this.toolStripPrint.Text = "Print";
			//
			// pnlNavigations
			//
			this.pnlNavigations.BackColor = System.Drawing.SystemColors.ControlLight;
			this.pnlNavigations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlNavigations.Controls.Add(this.toolstripNavigations);
			this.pnlNavigations.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlNavigations.Location = new System.Drawing.Point(0, 172);
			this.pnlNavigations.Name = "pnlNavigations";
			this.pnlNavigations.Size = new System.Drawing.Size(650, 30);
			this.pnlNavigations.TabIndex = 3;
			//
			// toolstripNavigations
			//
			this.toolstripNavigations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolstripNavigations.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolstripNavigations.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.toolstripNavigations_0});
			this.toolstripNavigations.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolstripNavigations.Location = new System.Drawing.Point(0, 0);
			this.toolstripNavigations.Name = "toolstripNavigations";
			this.toolstripNavigations.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolstripNavigations.Size = new System.Drawing.Size(648, 28);
			this.toolstripNavigations.TabIndex = 3;
			//
			// toolstripNavigations_0
			//
			this.toolstripNavigations_0.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolstripNavigations_0.Name = "toolstripNavigations_0";
			this.toolstripNavigations_0.Size = new System.Drawing.Size(48, 27);
			this.toolstripNavigations_0.Text = "PasajeroAeronave";
			this.toolstripNavigations_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			//
			// pnlDisplaySet
			//
			this.pnlDisplaySet.AutoScroll = true;
			this.pnlDisplaySet.Controls.Add(this.lstViewDisplaySet);
			this.pnlDisplaySet.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlDisplaySet.Location = new System.Drawing.Point(0, 32);
			this.pnlDisplaySet.Name = "pnlDisplaySet";
			this.pnlDisplaySet.Size = new System.Drawing.Size(620, 140);
			this.pnlDisplaySet.TabIndex = 1;
			//
			// lstViewDisplaySet
			//
			this.lstViewDisplaySet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
				this.lstViewDisplaySetName,
				this.lstViewDisplaySetValue});
			this.lstViewDisplaySet.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstViewDisplaySet.Location = new System.Drawing.Point(0, 0);
			this.lstViewDisplaySet.Name = "lstViewDisplaySet";
			this.lstViewDisplaySet.Size = new System.Drawing.Size(620, 140);
			this.lstViewDisplaySet.TabIndex = 0;
			this.lstViewDisplaySet.UseCompatibleStateImageBehavior = false;
			this.lstViewDisplaySet.View = System.Windows.Forms.View.Details;
			//
			// lstViewDisplaySetName
			//
			this.lstViewDisplaySetName.Text = "Name";
			this.lstViewDisplaySetName.Width = 200;
			//
			// lstViewDisplaySetValue
			//
			this.lstViewDisplaySetValue.Text = "Value";
			this.lstViewDisplaySetValue.Width = 295;
            //
            // panelFormInstanceButtons
            //
            this.panelFormInstanceButtons.Controls.Add(this.bCancel);
            this.panelFormInstanceButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFormInstanceButtons.Location = new System.Drawing.Point(0, 202);
            this.panelFormInstanceButtons.Name = "panelFormInstanceButtons";
            this.panelFormInstanceButtons.Size = new System.Drawing.Size(650, 30);
            this.panelFormInstanceButtons.TabIndex = 4;
			//
			// bCancel
			//
			this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(572, 4);
			this.bCancel.Margin = new System.Windows.Forms.Padding(2);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(75, 23);
			this.bCancel.TabIndex = 0;
			this.bCancel.Text = "Close";
			this.bCancel.UseVisualStyleBackColor = true;
			//
			// _Auto_IT
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(650, 232);
			this.Controls.Add(this.pnlDisplaySet);
			this.Controls.Add(this.pnlActions);
			this.Controls.Add(this.pnlNavigations);
			this.Controls.Add(this.pnlOIDSelector);
            this.Controls.Add(this.panelFormInstanceButtons);
			this.Name = "_Auto_IT";
			this.Text = "Aeronave";
            this.contextMenuStrip.ResumeLayout(false);
            this.pnlOIDSelector.ResumeLayout(false);
            this.pnlOIDSelector.PerformLayout();
			this.pnlActions.ResumeLayout(false);
			this.toolstripActions.ResumeLayout(false);
			this.toolstripActions.PerformLayout();
			this.pnlNavigations.ResumeLayout(false);
			this.pnlNavigations.PerformLayout();
			this.toolstripNavigations.ResumeLayout(false);
			this.toolstripNavigations.PerformLayout();
			this.pnlDisplaySet.ResumeLayout(false);
			this.pnlDisplaySet.PerformLayout();
            this.panelFormInstanceButtons.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip  contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripHelp;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem toolStripExportExcel;
		private System.Windows.Forms.ToolStripMenuItem toolStripExportWord;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorOptions;
		private System.Windows.Forms.ToolStripMenuItem toolStripRefresh;
		private System.Windows.Forms.ToolStripMenuItem toolStripPreferences;
		private System.Windows.Forms.ToolStripMenuItem toolStripSavePositions;
		private System.Windows.Forms.Panel pnlActions;
		private System.Windows.Forms.ToolStrip toolstripActions;
		private System.Windows.Forms.ToolStripMenuItem mnuActions_0;
		private System.Windows.Forms.ToolStripButton toolstripActions_0;
		private System.Windows.Forms.ToolStripMenuItem mnuActions_1;
		private System.Windows.Forms.ToolStripButton toolstripActions_1;
		private System.Windows.Forms.ToolStripMenuItem mnuActions_2;
		private System.Windows.Forms.ToolStripButton toolstripActions_2;
		private System.Windows.Forms.ToolStripMenuItem mnuActions_3;
		private System.Windows.Forms.ToolStripButton toolstripActions_3;
		private System.Windows.Forms.ToolStripMenuItem mnuPrint;
		private System.Windows.Forms.ToolStripButton toolStripPrint;
		private System.Windows.Forms.ToolStripMenuItem mnuNavigations;
		private System.Windows.Forms.Panel pnlNavigations;
		private System.Windows.Forms.ToolStrip toolstripNavigations;
		private System.Windows.Forms.ToolStripMenuItem mnuNavigations_0;
		private System.Windows.Forms.ToolStripButton toolstripNavigations_0;
		private System.Windows.Forms.Panel pnlOIDSelector;
		private System.Windows.Forms.Label lOIDSelector;
		private System.Windows.Forms.MaskedTextBox maskedTextBoxid_Aeronave1;
		private System.Windows.Forms.Button bOIDSelector;
		private System.Windows.Forms.Label lSIOIDSelector;
		private System.Windows.Forms.Panel pnlDisplaySet;
		private System.Windows.Forms.ListView lstViewDisplaySet;
		private System.Windows.Forms.ColumnHeader lstViewDisplaySetName;
		private System.Windows.Forms.ColumnHeader lstViewDisplaySetValue;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Panel panelFormInstanceButtons;
        private System.Windows.Forms.Button bCancel;
	}
}
