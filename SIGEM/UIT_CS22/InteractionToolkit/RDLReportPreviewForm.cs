// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace SIGEM.Client.InteractionToolkit
{
	public partial class RDLReportPreviewForm : Form
	{

		/// <summary>
		/// Indicates if the report has to be printed directly or not.
		/// </summary>
		private bool mPrint;
		
		/// <summary>
		/// DataSet used by the report.
		/// </summary>
		private DataSet mDataSet = null;

		/// <summary>
		/// Initializes a new instance of the 'RDLReportPreviewForm' class.
		/// </summary>
		public RDLReportPreviewForm()
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
		/// <param name="reportInfoDataSet">DataSet in which the report is based.</param>
		/// <param name="dataTable">DataTable used to fill the report.</param>
		/// <param name="reportPath">Path of the report.</param>
		/// <param name="parameters">Report parameters list.</param>
		/// <param name="print">Indicates if the report will be printed or only previewed.</param>
		public void Initialize(DataSet reportInfoDataSet, string reportPath, Dictionary<string, object> parameters, bool print)
		{
			this.mReportViewer.LocalReport.ReportPath = reportPath;

			mDataSet = reportInfoDataSet;

			this.mReportViewer.LocalReport.EnableExternalImages = true;

			try
			{
				// Process sub-reports.
				this.mReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(HandleLocalReportSubreportProcessing);

				// Check if the report has any parameter defined.
				ReportParameterInfoCollection reportParamCollection = this.mReportViewer.LocalReport.GetParameters();

				// Process parameters: Check the list of received parameters and the paramaters defined in the specific report.
				if ((parameters != null && parameters.Count > 0) &&
					(reportParamCollection != null && reportParamCollection.Count > 0))
				{
					List<ReportParameter> lParams = new List<ReportParameter>(parameters.Count);
					ReportParameter lAuxParameter;
					ReportParameterInfo lSpecificReportParameter; // Represents the information about an specific report parameter.
					foreach (string lKey in parameters.Keys)
					{
						string lParamValue = string.Empty; // Will store the value that is going to be assigned.

						lSpecificReportParameter = reportParamCollection[lKey];
						// Check that the received parameter exists in the current report.
						if (lSpecificReportParameter == null)
							continue;

						if (lSpecificReportParameter.Nullable && parameters[lKey] == null)
						{
							lParamValue = null;
						}
						else
						{
							if (parameters[lKey] != null)
							{
								lParamValue = parameters[lKey].ToString();
							}
						}
						
						lAuxParameter = new ReportParameter(lKey, lParamValue, true);
						if (reportParamCollection[lKey] != null)
						{
							// The parameter is also defined in the report.
							lParams.Add(lAuxParameter);
						}
					}
					
					// Assign the parameters to the report.
					this.mReportViewer.LocalReport.SetParameters(lParams);
				}

				// Set report DataSources.
				// For each Table contained in the DataSet --> add a ReportDataSource.
				string lDataSourceName = string.Empty;
				foreach (DataTable lDataTable in mDataSet.Tables)
				{
					lDataSourceName = lDataTable.TableName;
					// These data sources will be needed to fill the report.
					ReportDataSource lReportDS = new ReportDataSource(lDataSourceName, lDataTable);
					this.mReportViewer.LocalReport.DataSources.Add(lReportDS);
				}

				// Show the report in the viewer.
				this.mReportViewer.RefreshReport();
				this.mReportViewer.RenderingComplete += new RenderingCompleteEventHandler(HandleReportViewerRenderingComplete);
				mPrint = print;
			}
			catch (Exception e)
			{
				object[] lArgs = new object[1];
				lArgs[0] = reportPath;

				Exception excProcessingReport = new Exception(CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_ERROR_SHOWING_REPORT, LanguageConstantValues.L_ERROR_SHOWING_REPORT, lArgs), e);
				Presentation.ScenarioManager.LaunchErrorScenario(excProcessingReport);
			}
		}
		/// <summary>
		/// Handles the event raised by the report viewer when the report has been completely rendered.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleReportViewerRenderingComplete(object sender, RenderingCompleteEventArgs e)
		{
			if (mPrint)
			{
				mReportViewer.PrintDialog();
				Close();
			}
		}
		/// <summary>
		/// Manages the sub-report processing event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleLocalReportSubreportProcessing(object sender, SubreportProcessingEventArgs e)
		{
			for (int i = 0; i < e.DataSourceNames.Count; i++)
			{
				// The name of the table to be assigned as data source is equal to the class name
				string lAuxTableName = e.DataSourceNames[i];
				// Assign the suitable data table to the sub-report.
				ReportDataSource lSubReportDS = new ReportDataSource(e.DataSourceNames[i], mDataSet.Tables[lAuxTableName]);
				e.DataSources.Add(lSubReportDS);
			}
		}
	}
}
