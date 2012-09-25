
 




// v3.8.4.5.b
namespace SIGEM.Client.InteractionToolkit.PasajeroAeronave.IUPopulations
{
	public partial class PIU_PasajeroAeronaveIT
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

			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripExportExcel = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripExportWord = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorOptions = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripRetrieveAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripPreferences = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSaveColumnWidth = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSavePositions = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
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
			this.mnuActions_4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolstripActions_4 = new System.Windows.Forms.ToolStripButton();
			this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripPrint = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.pnlNavigations = new System.Windows.Forms.Panel();
			this.toolstripNavigations = new System.Windows.Forms.ToolStrip();
			this.mnuNavigations = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuNavigations_0 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolstripNavigations_0 = new System.Windows.Forms.ToolStripButton();
			this.mnuNavigations_1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolstripNavigations_1 = new System.Windows.Forms.ToolStripButton();
			this.mnuNavigations_2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolstripNavigations_2 = new System.Windows.Forms.ToolStripButton();
			this.gPopulation = new System.Windows.Forms.DataGridView();
			this.statusStripDisplaySetInfoAux = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelBlank = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripDropDownButtonSave = new System.Windows.Forms.ToolStripDropDownButton();
			this.panelFormPIUButtons = new System.Windows.Forms.Panel();
			this.bOk = new System.Windows.Forms.Button();
			this.bCancel = new System.Windows.Forms.Button();
			this.contextMenuStrip.SuspendLayout();
			this.pnlActions.SuspendLayout();
			this.toolstripActions.SuspendLayout();
			this.pnlNavigations.SuspendLayout();
			this.toolstripNavigations.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gPopulation)).BeginInit();
			this.statusStripDisplaySetInfoAux.SuspendLayout();
			this.panelFormPIUButtons.SuspendLayout();
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
				this.mnuActions_4,
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
				this.toolStripRetrieveAll,
				this.toolStripRefresh,
				this.toolStripPreferences,
				this.toolStripSaveColumnWidth,
				this.toolStripSavePositions});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.optionsToolStripMenuItem.Text = "Options";
			//
			// toolStripExportExcel
			//
			this.toolStripExportExcel.Image = global::SIGEM.Client.Properties.Resources.exportToExcel;
			this.toolStripExportExcel.Name = "toolStripExportExcel";
			this.toolStripExportExcel.Size = new System.Drawing.Size(174, 22);
			this.toolStripExportExcel.Text = "Export To Excel}";
			//
			// toolStripExportWord
			//
			this.toolStripExportWord.Image = global::SIGEM.Client.Properties.Resources.exportToWord;
			this.toolStripExportWord.Name = "toolStripExportWord";
			this.toolStripExportWord.Size = new System.Drawing.Size(174, 22);
			this.toolStripExportWord.Text = "Export To Word";
			this.toolStripExportWord.Visible = false;
			//
			// toolStripSeparatorOptions
			//
			this.toolStripSeparatorOptions.Name = "toolStripSeparatorOptions";
			this.toolStripSeparatorOptions.Size = new System.Drawing.Size(171, 6);
			//
			// toolStripRetrieveAll
			//
			this.toolStripRetrieveAll.Name = "toolStripRetrieveAll";
			this.toolStripRetrieveAll.Size = new System.Drawing.Size(174, 22);
			this.toolStripRetrieveAll.Text = "Retrieve All";
			//
			// toolStripRefresh
			//
			this.toolStripRefresh.Image = global::SIGEM.Client.Properties.Resources.refresh;
			this.toolStripRefresh.Name = "toolStripRefresh";
			this.toolStripRefresh.Size = new System.Drawing.Size(174, 22);
			this.toolStripRefresh.Text = "Refresh";
			//
			// toolStripPreferences
			//
			this.toolStripPreferences.Image = global::SIGEM.Client.Properties.Resources.preferences;
			this.toolStripPreferences.Name = "toolStripPreferences";
			this.toolStripPreferences.Size = new System.Drawing.Size(174, 22);
			this.toolStripPreferences.Text = "Preferences";
			//
			// toolStripSaveColumnWidth
			//
			this.toolStripSaveColumnWidth.Image = global::SIGEM.Client.Properties.Resources.saveWidth;
			this.toolStripSaveColumnWidth.Name = "toolStripSaveColumnWidth";
			this.toolStripSaveColumnWidth.Size = new System.Drawing.Size(174, 22);
			this.toolStripSaveColumnWidth.Text = "Save column width";
			//
			// toolStripSavePositions
			//
			this.toolStripSavePositions.Image = global::SIGEM.Client.Properties.Resources.savePosition;
			this.toolStripSavePositions.Name = "toolStripSavePositions";
			this.toolStripSavePositions.Size = new System.Drawing.Size(174, 22);
			this.toolStripSavePositions.Text = "Save position";
			//
			// toolStripSeparator1
			//
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(127, 6);
			//
			// mnuActions_0
			//
			this.mnuActions_0.Image = global::SIGEM.Client.Properties.Resources.PasajeroAeronave_SIU_SIU_create_instance;
			this.mnuActions_0.Name = "mnuActions_0";
			this.mnuActions_0.Size = new System.Drawing.Size(130, 22);
			//
			// mnuActions_1
			//
			this.mnuActions_1.Image = global::SIGEM.Client.Properties.Resources.PasajeroAeronave_SIU_SIU_delete_instance;
			this.mnuActions_1.Name = "mnuActions_1";
			this.mnuActions_1.Size = new System.Drawing.Size(130, 22);
			//
			// mnuActions_2
			//
			this.mnuActions_2.Image = global::SIGEM.Client.Properties.Resources.PasajeroAeronave_SIU_SIU_edit_instance;
			this.mnuActions_2.Name = "mnuActions_2";
			this.mnuActions_2.Size = new System.Drawing.Size(130, 22);
			//
			// mnuActions_3
			//
			this.mnuActions_3.Image = global::SIGEM.Client.Properties.Resources.PasajeroAeronave_IIU_IIU_PasajeroAeronave;
			this.mnuActions_3.Name = "mnuActions_3";
			this.mnuActions_3.Size = new System.Drawing.Size(130, 22);
			//
			// mnuActions_4
			//
			this.mnuActions_4.Image = global::SIGEM.Client.Properties.Resources.PasajeroAeronave_MDIU_MDIU_PasajeroAeronave;
			this.mnuActions_4.Name = "mnuActions_4";
			this.mnuActions_4.Size = new System.Drawing.Size(130, 22);
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
				this.mnuNavigations_0,
				this.mnuNavigations_1,
				this.mnuNavigations_2});
			this.mnuNavigations.Name = "mnuNavigations";
			this.mnuNavigations.Size = new System.Drawing.Size(130, 22);
			this.mnuNavigations.Text = "Navigations";
			//
			// mnuNavigations_0
			//
			this.mnuNavigations_0.Name = "mnuNavigations_0";
			this.mnuNavigations_0.Size = new System.Drawing.Size(67, 22);
			//
			// mnuNavigations_1
			//
			this.mnuNavigations_1.Name = "mnuNavigations_1";
			this.mnuNavigations_1.Size = new System.Drawing.Size(67, 22);
			//
			// mnuNavigations_2
			//
			this.mnuNavigations_2.Name = "mnuNavigations_2";
			this.mnuNavigations_2.Size = new System.Drawing.Size(67, 22);
			//
			// pnlActions
			//
			this.pnlActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlActions.Controls.Add(this.toolstripActions);
			this.pnlActions.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlActions.Location = new System.Drawing.Point(520, 0);
			this.pnlActions.Name = "pnlActions";
			this.pnlActions.Size = new System.Drawing.Size(30, 300);
			this.pnlActions.TabIndex = 0;
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
				this.toolstripActions_4,
				this.toolStripPrint});
			this.toolstripActions.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
			this.toolstripActions.Location = new System.Drawing.Point(0, 0);
			this.toolstripActions.Name = "toolstripActions";
			this.toolstripActions.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolstripActions.Size = new System.Drawing.Size(28, 298);
			this.toolstripActions.TabIndex = 0;
			//
			// toolstripActions_0
			//
			this.toolstripActions_0.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolstripActions_0.Image = global::SIGEM.Client.Properties.Resources.PasajeroAeronave_SIU_SIU_create_instance;
			this.toolstripActions_0.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolstripActions_0.Name = "toolstripActions_0";
			this.toolstripActions_0.Size = new System.Drawing.Size(26, 20);
			this.toolstripActions_0.Text = "New";
			//
			// toolstripActions_1
			//
			this.toolstripActions_1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolstripActions_1.Image = global::SIGEM.Client.Properties.Resources.PasajeroAeronave_SIU_SIU_delete_instance;
			this.toolstripActions_1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolstripActions_1.Name = "toolstripActions_1";
			this.toolstripActions_1.Size = new System.Drawing.Size(26, 20);
			this.toolstripActions_1.Text = "Destroy";
			//
			// toolstripActions_2
			//
			this.toolstripActions_2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolstripActions_2.Image = global::SIGEM.Client.Properties.Resources.PasajeroAeronave_SIU_SIU_edit_instance;
			this.toolstripActions_2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolstripActions_2.Name = "toolstripActions_2";
			this.toolstripActions_2.Size = new System.Drawing.Size(26, 20);
			this.toolstripActions_2.Text = "Edit";
			//
			// toolstripActions_3
			//
			this.toolstripActions_3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolstripActions_3.Image = global::SIGEM.Client.Properties.Resources.PasajeroAeronave_IIU_IIU_PasajeroAeronave;
			this.toolstripActions_3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolstripActions_3.Name = "toolstripActions_3";
			this.toolstripActions_3.Size = new System.Drawing.Size(26, 20);
			this.toolstripActions_3.Text = "PasajeroAeronave";
			//
			// toolstripActions_4
			//
			this.toolstripActions_4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolstripActions_4.Image = global::SIGEM.Client.Properties.Resources.PasajeroAeronave_MDIU_MDIU_PasajeroAeronave;
			this.toolstripActions_4.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolstripActions_4.Name = "toolstripActions_4";
			this.toolstripActions_4.Size = new System.Drawing.Size(26, 20);
			this.toolstripActions_4.Text = "PasajeroAeronave";
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
			this.pnlNavigations.Location = new System.Drawing.Point(0, 300);
			this.pnlNavigations.Name = "pnlNavigations";
			this.pnlNavigations.Size = new System.Drawing.Size(550, 30);
			this.pnlNavigations.TabIndex = 0;
			//
			// toolstripNavigations
			//
			this.toolstripNavigations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolstripNavigations.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolstripNavigations.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.toolstripNavigations_0,
				this.toolstripNavigations_1,
				this.toolstripNavigations_2});
			this.toolstripNavigations.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolstripNavigations.Location = new System.Drawing.Point(0, 0);
			this.toolstripNavigations.Name = "toolstripNavigations";
			this.toolstripNavigations.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolstripNavigations.Size = new System.Drawing.Size(548, 28);
			this.toolstripNavigations.TabIndex = 0;
			//
			// toolstripNavigations_0
			//
			this.toolstripNavigations_0.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolstripNavigations_0.Name = "toolstripNavigations_0";
			this.toolstripNavigations_0.Size = new System.Drawing.Size(48, 27);
			this.toolstripNavigations_0.Text = "RevisionPasajero";
			this.toolstripNavigations_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			//
			// toolstripNavigations_1
			//
			this.toolstripNavigations_1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolstripNavigations_1.Name = "toolstripNavigations_1";
			this.toolstripNavigations_1.Size = new System.Drawing.Size(48, 27);
			this.toolstripNavigations_1.Text = "Aeronave";
			this.toolstripNavigations_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			//
			// toolstripNavigations_2
			//
			this.toolstripNavigations_2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolstripNavigations_2.Name = "toolstripNavigations_2";
			this.toolstripNavigations_2.Size = new System.Drawing.Size(48, 27);
			this.toolstripNavigations_2.Text = "Pasajero";
			this.toolstripNavigations_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			//
			// gPopulation
			//
			this.gPopulation.AllowUserToAddRows = false;
			this.gPopulation.AllowUserToDeleteRows = false;
			this.gPopulation.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;
			this.gPopulation.AutoGenerateColumns= false;
			this.gPopulation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gPopulation.Location = new System.Drawing.Point(0, 0);
			this.gPopulation.Name = "gPopulation";
			this.gPopulation.ReadOnly = true;
			this.gPopulation.RowHeadersVisible = false;
			this.gPopulation.RowTemplate.Height = 18;
			this.gPopulation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gPopulation.ShowCellErrors = false;
			this.gPopulation.ShowCellToolTips = false;
			this.gPopulation.ShowEditingIcon = false;
			this.gPopulation.ShowRowErrors = false;
			this.gPopulation.Size = new System.Drawing.Size(520, 278);
			this.gPopulation.TabIndex = 0;
			//
			// statusStripDisplaySetInfoAux
			//
			this.statusStripDisplaySetInfoAux.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripStatusLabelBlank,
			this.toolStripStatusLabelCount,
			this.toolStripDropDownButtonSave});
			this.statusStripDisplaySetInfoAux.Location = new System.Drawing.Point(0, 278);
			this.statusStripDisplaySetInfoAux.Name = "statusStripDisplaySetInfoAux";
			this.statusStripDisplaySetInfoAux.Size = new System.Drawing.Size(520, 22);
			this.statusStripDisplaySetInfoAux.TabIndex = 3;
			//
			// toolStripStatusLabelBlank
			//
			this.toolStripStatusLabelBlank.Name = "toolStripStatusLabelBlank";
			this.toolStripStatusLabelBlank.Size = new System.Drawing.Size(505, 17);
			this.toolStripStatusLabelBlank.Spring = true;
			this.toolStripStatusLabelBlank.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			//
			// toolStripStatusLabelCount
			//
			this.toolStripStatusLabelCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabelCount.Name = "toolStripStatusLabelCount";
			this.toolStripStatusLabelCount.Size = new System.Drawing.Size(29, 17);
			this.toolStripStatusLabelCount.Text = "0/...";
			this.toolStripStatusLabelCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			//
			// toolStripDropDownButtonSave
			//
			this.toolStripDropDownButtonSave.Enabled = false;
			this.toolStripDropDownButtonSave.Image = global::SIGEM.Client.Properties.Resources.apply;
			this.toolStripDropDownButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonSave.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.toolStripDropDownButtonSave.Name = "toolStripDropDownButtonSave";
			this.toolStripDropDownButtonSave.ShowDropDownArrow = false;
			this.toolStripDropDownButtonSave.Size = new System.Drawing.Size(66, 17);
			this.toolStripDropDownButtonSave.Text = "Save";
			this.toolStripDropDownButtonSave.Visible = false;
			//
			// panelFormPIUButtons
			//
			this.panelFormPIUButtons.Controls.Add(this.bOk);
			this.panelFormPIUButtons.Controls.Add(this.bCancel);
			this.panelFormPIUButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelFormPIUButtons.Location = new System.Drawing.Point(0, 330);
			this.panelFormPIUButtons.Name = "panelFormPIUButtons";
			this.panelFormPIUButtons.Size = new System.Drawing.Size(550, 30);
			this.panelFormPIUButtons.TabIndex = 2;
			//
			// bOk
			//
			this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bOk.Location = new System.Drawing.Point(394, 4);
			this.bOk.Margin = new System.Windows.Forms.Padding(2);
			this.bOk.Name = "bOk";
			this.bOk.Size = new System.Drawing.Size(75, 23);
			this.bOk.TabIndex = 0;
			this.bOk.Text = "Select";
			this.bOk.UseVisualStyleBackColor = true;
			//
			// bCancel
			//
			this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(472, 4);
			this.bCancel.Margin = new System.Windows.Forms.Padding(2);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(75, 23);
			this.bCancel.TabIndex = 1;
			this.bCancel.Text = "Close";
			this.bCancel.UseVisualStyleBackColor = true;
			//
			// PIU_PasajeroAeronaveIT
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(550, 360);
			this.Controls.Add(this.gPopulation);
			this.Controls.Add(this.statusStripDisplaySetInfoAux);
			this.Controls.Add(this.pnlActions);
			this.Controls.Add(this.pnlNavigations);
			this.Controls.Add(this.panelFormPIUButtons);
			this.Name = "PIU_PasajeroAeronaveIT";
			this.Text = "PasajeroAeronave";
			this.contextMenuStrip.ResumeLayout(false);
			this.pnlActions.ResumeLayout(false);
			this.pnlActions.PerformLayout();
			this.toolstripActions.ResumeLayout(false);
			this.toolstripActions.PerformLayout();
			this.pnlNavigations.ResumeLayout(false);
			this.pnlNavigations.PerformLayout();
			this.toolstripNavigations.ResumeLayout(false);
			this.toolstripNavigations.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.gPopulation)).EndInit();
			this.statusStripDisplaySetInfoAux.ResumeLayout(false);
			this.statusStripDisplaySetInfoAux.PerformLayout();
			this.panelFormPIUButtons.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripHelp;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem toolStripExportExcel;
		private System.Windows.Forms.ToolStripMenuItem toolStripExportWord;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorOptions;
		private System.Windows.Forms.ToolStripMenuItem toolStripRetrieveAll;
		private System.Windows.Forms.ToolStripMenuItem toolStripRefresh;
		private System.Windows.Forms.ToolStripMenuItem toolStripPreferences;
		private System.Windows.Forms.ToolStripMenuItem toolStripSaveColumnWidth;
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
		private System.Windows.Forms.ToolStripMenuItem mnuActions_4;
		private System.Windows.Forms.ToolStripButton toolstripActions_4;
		private System.Windows.Forms.ToolStripMenuItem mnuPrint;
		private System.Windows.Forms.ToolStripButton toolStripPrint;
		private System.Windows.Forms.ToolStripMenuItem mnuNavigations;
		private System.Windows.Forms.Panel pnlNavigations;
		private System.Windows.Forms.ToolStrip toolstripNavigations;
		private System.Windows.Forms.ToolStripMenuItem mnuNavigations_0;
		private System.Windows.Forms.ToolStripButton toolstripNavigations_0;
		private System.Windows.Forms.ToolStripMenuItem mnuNavigations_1;
		private System.Windows.Forms.ToolStripButton toolstripNavigations_1;
		private System.Windows.Forms.ToolStripMenuItem mnuNavigations_2;
		private System.Windows.Forms.ToolStripButton toolstripNavigations_2;
		private System.Windows.Forms.DataGridView gPopulation;
		private System.Windows.Forms.StatusStrip statusStripDisplaySetInfoAux;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBlank;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCount;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonSave;
		private System.Windows.Forms.Panel panelFormPIUButtons;
		private System.Windows.Forms.Button bOk;
		private System.Windows.Forms.Button bCancel;
	}
}
