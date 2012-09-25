// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace SIGEM.Client.InteractionToolkit
{
	public partial class CrystalPreviewForm : Form
	{
		/// <summary>
		/// Initializes a new instance of the 'CrystalPreviewForm' class.
		/// </summary>
		public CrystalPreviewForm()
		{
			InitializeComponent();

			// Icon assignament.
			this.Icon = UtilFunctions.BitmapToIcon(Properties.Resources.print);

			// Apply multilanguage.
			this.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PRINT_PREVIEW, LanguageConstantValues.L_PRINT_PREVIEW);
		}

		/// <summary>
		/// Configures the report viewer control.
		/// </summary>
		/// <param name="reportInfoDataSet">Dataset with the data</param>
		/// <param name="reportPath">Crystal Report document file path</param>
		/// <param name="parameters">Optional parameters to be passed to the report</param>
		/// <param name="print">Print or preview</param>
		public void Initialize(DataSet reportInfoDataSet, string reportPath, Dictionary<string, object> parameters, bool print)
		{
			ReportDocument lReport = new ReportDocument();
			lReport.Load(reportPath);
			lReport.SetDataSource(reportInfoDataSet);

			try
			{

				// If the report has any parameter defined.
				if ((lReport.ParameterFields.Count > 0) &&
					(parameters != null && parameters.Count > 0))
				{
					foreach (string lKey in parameters.Keys)
					{
						if (lReport.ParameterFields[lKey] == null) continue;

						ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();
						if (parameters[lKey] == null)
						{
							discreteVal.Value = string.Empty;
						}
						else
						{
							discreteVal.Value = parameters[lKey].ToString();
						}
						lReport.ParameterFields[lKey].CurrentValues.Add(discreteVal);
					}
				}

				// Load the report in the viewer control.
				this.crystalReportViewer.ReportSource = lReport;
			}
			catch (Exception e)
			{
				object[] lArgs = new object[1];
				lArgs[0] = reportPath;

				Exception excProcessingReport = new Exception(CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_ERROR_SHOWING_REPORT, LanguageConstantValues.L_ERROR_SHOWING_REPORT, lArgs), e);
				Presentation.ScenarioManager.LaunchErrorScenario(excProcessingReport);
			}
			
			if (print)
			{
				// Print the Report.
				this.crystalReportViewer.PrintReport();
			}
		}

	}
}
