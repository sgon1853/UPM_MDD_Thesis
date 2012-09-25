// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SIGEM.Client.Oids;
using SIGEM.Client.Presentation;
using SIGEM.Client.PrintingDriver;

namespace SIGEM.Client.InteractionToolkit
{
	public partial class PrintForm : Form
	{
		/// <summary>
		/// Oid List received in order to be printed
		/// </summary>
		private List<Oid> mSelectedOids = null;
		/// <summary>
		/// Available report templates
		/// </summary>
		private List<ReportConfiguration> mReports;

		/// <summary>
		/// Constructor
		/// </summary>
		public PrintForm()
		{
			InitializeComponent();

			// Icon assignament.
			this.Icon = UtilFunctions.BitmapToIcon(Properties.Resources.print);

			// Apply multilanguage.
			this.lblSelectPrinter.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PRINT_SELECT_PRINTER, LanguageConstantValues.L_PRINT_SELECT_PRINTER, this.lblSelectPrinter.Text);
			this.grpCopies.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PRINT_NUMBER_OF_COPIES, LanguageConstantValues.L_PRINT_NUMBER_OF_COPIES, this.grpCopies.Text);
			this.grpOutPut.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PRINT_OUTPUT, LanguageConstantValues.L_PRINT_OUTPUT, this.grpOutPut.Text);
			this.rdoBtnPrint.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PRINT, LanguageConstantValues.L_PRINT, this.rdoBtnPrint.Text);
			this.rdoBtnPreview.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PRINT_PREVIEW, LanguageConstantValues.L_PRINT_PREVIEW, this.rdoBtnPreview.Text);
			this.grbFrm.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PRINT_OPTIONS, LanguageConstantValues.L_PRINT_OPTIONS, this.grbFrm.Text);
			this.lblSelectTemplate.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PRINT_SELECT_TEMPLATE, LanguageConstantValues.L_PRINT_SELECT_TEMPLATE, this.lblSelectTemplate.Text);
			this.btnOk.Text = CultureManager.TranslateString(LanguageConstantKeys.L_OK, LanguageConstantValues.L_OK, this.btnOk.Text);
			this.btnCancel.Text = CultureManager.TranslateString(LanguageConstantKeys.L_CANCEL, LanguageConstantValues.L_CANCEL, this.btnCancel.Text);
			this.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PRINT, LanguageConstantValues.L_PRINT, this.Text);
		}

		/// <summary>
		/// Initialize the form
		/// </summary>
		public void Initialize(List<Oid> selectedOids)
		{
			mSelectedOids = selectedOids;

			// If no instances selected, inform and exit
			if (mSelectedOids == null || mSelectedOids.Count == 0)
			{
				string lMessageError = CultureManager.TranslateString(LanguageConstantKeys.L_NO_SELECTION, LanguageConstantValues.L_NO_SELECTION);
				Exception lException = new Exception(lMessageError, null);
				ScenarioManager.LaunchErrorScenario(lException);
				return;
			}

			cmbBoxSelectTemplate.SelectedIndexChanged += new EventHandler(HandlecmbBoxSelectTemplate_SelectedIndexChanged);

			// Load the templates from configuration file and fill the Combo
			LoadReportTemplates();

			// Show the number of selected instances in the Title.
			Text = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_ELEMENTSELECTED, LanguageConstantValues.L_ELEMENTSELECTED, mSelectedOids.Count);
			// Set the default printer
			lblNameOfPrint.Text = printDlg.PrinterSettings.PrinterName;

			ShowDialog();
		}

		/// <summary>
		/// Retrieve the information about the available reports from the configuration file and load the combo box
		/// Returns False if any error is raised
		/// </summary>
		private void LoadReportTemplates()
		{
			string pathFile = string.Empty;
			string nameOfFile = Properties.Settings.Default.ConfigurationOfReports;
			string strXML = string.Empty;
			string className = mSelectedOids[0].ClassName;

			mReports = Logics.Logic.InstanceReportsList.GetClassReports(className);

			foreach (ReportConfiguration lReport in mReports)
			{
				cmbBoxSelectTemplate.Items.Add(lReport.Alias);
			}

			if (cmbBoxSelectTemplate.Items.Count > 0)
				cmbBoxSelectTemplate.SelectedIndex = 0;

		}

		/// <summary>
		/// Call to the Printing class using the configuration of the selected report
		/// </summary>
		private void btnOk_Click(object sender, EventArgs e)
		{
			// If no report selected, exit
			if (cmbBoxSelectTemplate.SelectedIndex < 0)
			{
				return;
			}

			// Get report configuration
			ReportConfiguration lReport = mReports[cmbBoxSelectTemplate.SelectedIndex];

			// Loop for all the instances in the received list
			foreach (Oid oid in mSelectedOids)
			{
				switch (lReport.ReportType)
				{
					case ReportTypes.Word:
						{
							PrintingDriver.PrintToWord.Print(oid, lReport, rdoBtnPreview.Checked, (int)numUpDownCopies.Value, lblNameOfPrint.Text, "");
							break;
						}
					case ReportTypes.Excel:
						{
							PrintingDriver.PrintToExcel.Print(oid, lReport);
							break;
						}
					case ReportTypes.CrystalReports:
						{
							DataSet lDataSet = PrintingDriver.PrintToDataSets.GetData(oid, lReport.DatasetFile);
							ScenarioManager.LaunchCrystalReportPreviewScenario(lDataSet, lReport.ReportFilePath, null, !rdoBtnPreview.Checked);
							break;
						}
					case ReportTypes.RDLC:
						{
							DataSet lDataSet = PrintingDriver.PrintToDataSets.GetData(oid, lReport.DatasetFile);
							ScenarioManager.LaunchRDLReportPreviewScenario(lDataSet, lReport.ReportFilePath, null, !rdoBtnPreview.Checked);
							break;
						}
				}
			}


			// Close this form
			Close();
		}

		#region Event Handlers
		/// <summary>
		/// Select and configure the printer
		/// </summary>
		private void btnPrinter_Click(object sender, EventArgs e)
		{
			if (printDlg.PrinterSettings == null)
				printDlg.PrinterSettings = new System.Drawing.Printing.PrinterSettings();

			printDlg.ShowNetwork = true;
			printDlg.PrinterSettings.Copies = Convert.ToInt16(this.numUpDownCopies.Value);
			printDlg.ShowDialog();
			numUpDownCopies.Value = this.printDlg.PrinterSettings.Copies;
			lblNameOfPrint.Text = this.printDlg.PrinterSettings.PrinterName;
		}

		/// <summary>
		/// Handles change in template selection.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EventArgs object.</param>
		private void HandlecmbBoxSelectTemplate_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Show or hide 'Print' option depends on report type.

			// If no report selected, exit.
			if (cmbBoxSelectTemplate.SelectedIndex < 0)
			{
				return;
			}

			bool lPrintDirectlyAllowed = true;
			// Get report configuration.
			ReportConfiguration lReport = mReports[cmbBoxSelectTemplate.SelectedIndex];
			switch (lReport.ReportType)
			{
				case ReportTypes.Word:
					{
						lPrintDirectlyAllowed = PrintingDriver.PrintDirectlyCtes.Word;
						break;
					}
				case ReportTypes.Excel:
					{
						lPrintDirectlyAllowed = PrintingDriver.PrintDirectlyCtes.Excel;
						break;
					}
				case ReportTypes.CrystalReports:
					{
						lPrintDirectlyAllowed = PrintingDriver.PrintDirectlyCtes.CrystalReports;
						break;
					}
				case ReportTypes.RDLC:
					{
						lPrintDirectlyAllowed = PrintingDriver.PrintDirectlyCtes.RDLC;
						break;
					}
			}

			if (lPrintDirectlyAllowed)
			{
				this.rdoBtnPrint.Enabled = true;
			}
			else
			{
				this.rdoBtnPrint.Enabled = false;
				this.rdoBtnPreview.Checked = true;
			}
		}

		#endregion Event Handlers
	}
}
