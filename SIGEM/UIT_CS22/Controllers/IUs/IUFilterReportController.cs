// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SIGEM.Client.Oids;
using SIGEM.Client.Logics;
using SIGEM.Client.Presentation;
using SIGEM.Client.PrintingDriver;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the IUFilterReportController.
	/// </summary>
	public class IUFilterReportController: IUFilterController
	{
		#region Members
		/// <summary>
		/// Report file name.
		/// </summary>
		private string mReportFile;
		/// <summary>
		/// DataSet file name.
		/// </summary>
		private string mDataSetFile;
		/// <summary>
		/// Report parameters.
		/// </summary>
		private Dictionary<string, object> mReportParams = new Dictionary<string, object>();
		/// <summary>
		/// Population context information required to do the query.
		/// </summary>
		private IUPopulationContext mPopulationContext = null;
		/// <summary>
		/// Indicates if the report will be directly printed or only displayed in preview scenario.
		/// </summary>
		private bool mPrintDirectly;
		/// <summary>
		/// Presentation of the preview trigger
		/// </summary>
		private ITriggerPresentation mPreviewTrigger = null;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'IUFilterReportController' class.
		/// </summary>
		/// <param name="filterController">IUFilterController related.</param>
		/// <param name="reportFile">Path of the report file.</param>
		/// <param name="dataSetFile">Path of the associated dataset file.</param>
		/// <param name="printDirectly">Print ot Preview</param>
		public IUFilterReportController(IUFilterController filterController, string reportFile, string dataSetFile, bool printDirectly)
			: base(filterController.Name, filterController.Alias, filterController.IdXML, filterController.DefaultOrderCriteria, filterController.Context, filterController.Parent)
		{
			// Set report properties.
			mReportFile = reportFile;
			mDataSetFile = dataSetFile;
			mPrintDirectly = printDirectly;

			// Set the Population Context.
			mPopulationContext = (filterController.Parent.Context as IUPopulationContext);
			
			// Assign the filter variables values.
			this.InputFields = filterController.InputFields;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets the display set used for the report.
		/// </summary>
		public string DisplaySet
		{
			get
			{
				return mPopulationContext.DisplaySetAttributes;
			}
		}
		/// <summary>
		/// Gets the Report file name.
		/// </summary>
		public string ReportFile
		{
			get
			{
				return mReportFile;
			}
		}
		/// <summary>
		/// Gets the DataSet file name.
		/// </summary>
		public string DataSetFile
		{
			get
			{
				return mDataSetFile;
			}
		}
		/// <summary>
		/// Report parameters.
		/// </summary>
		public Dictionary<string, object> ReportParams
		{
			get
			{
				return mReportParams;
			}
			set
			{
				mReportParams = value;
			}
		}
		/// <summary>
		/// Gets a boolean indicating whether the report will be directly printed or only displayed in preview scenario.
		/// </summary>
		public bool PrintDirectly
		{
			get
			{
				return mPrintDirectly;
			}
			set
			{
				mPrintDirectly = value;
			}
		}
		/// <summary>
		/// Gets or sets the Preview trigger.
		/// </summary>
		public virtual ITriggerPresentation PreviewTrigger
		{
			get
			{
				return mPreviewTrigger;
			}
			set
			{
				if (mPreviewTrigger != null)
				{
					mPreviewTrigger.Triggered -= new EventHandler<TriggerEventArgs>(HandlePreviewTrigger);
				}
				mPreviewTrigger = value;
				if (mPreviewTrigger != null)
				{
					mPreviewTrigger.Triggered += new EventHandler<TriggerEventArgs>(HandlePreviewTrigger);
				}
			}
		}
		#endregion Properties

		#region Event Handlers
		private void HandlePreviewTrigger(object sender, TriggerEventArgs e)
		{
			PrintDirectly = false;
			Execute();
		}
		#endregion Event Handlers

		#region Methods
		/// <summary>
		/// Initializes the input fields properties.
		/// </summary>
		public override void Initialize()
		{
			// Configure the report and filter from the report information file.
			if (!ReadReportInfo())
			{
				return;
			}

			base.Initialize();
		}
		/// <summary>
		/// Process the report and show it.
		/// </summary>
		/// <returns>A boolean value indicating the success or failure in the process.</returns>
		public override bool Execute()
		{
            if (!CheckNullAndFormatFilterVariablesValues())
            {
                return false;
            }

			// Load filter variable values in the parameters collection
			foreach (ArgumentController lFilterVariable in InputFields)
			{
				object lFilterVariableValue = lFilterVariable.Value;
				ArgumentOVControllerAbstract lOVController = lFilterVariable as ArgumentOVControllerAbstract;
				if (lOVController != null)
				{
					if (lOVController.Value != null)
					{
						lFilterVariableValue = lOVController.GetSupplementaryInfoText();
					}
				}

				if (ReportParams.ContainsKey(lFilterVariable.Name))
				{
					ReportParams[lFilterVariable.Name] = lFilterVariableValue;
				}
				else
				{
					ReportParams.Add(lFilterVariable.Name, lFilterVariableValue);
				}
			}

			// Execute Filter Event initialization.
			ExecuteFilterEventArgs lExecuteFilterEventArgs = new ExecuteFilterEventArgs(this);

			try
			{
				// Get the data according the report definition.
				DataTable lDataTable = GetData();

				// Create the DataSet from the base DataTable, taking into account the DataSet structure.
				DataSet lDataSet = PrintingDriver.PrintToDataSets.GetData(lDataTable, mDataSetFile);

				if (GetReportType() == ReportTypes.CrystalReports)
				{
					// Show 'Crystal' preview scenario.
					ScenarioManager.LaunchCrystalReportPreviewScenario(lDataSet, ReportFile, ReportParams, PrintDirectly);
				}
				if (GetReportType() == ReportTypes.RDLC)
				{
					// Show 'RDLC' preview scenario.
					ScenarioManager.LaunchRDLReportPreviewScenario(lDataSet, ReportFile, ReportParams, PrintDirectly);
				}
				
			}
			catch (Exception exc)
			{
				Exception gettingDataException = new Exception(CultureManager.TranslateString(LanguageConstantKeys.L_ERROR_REPORT_GETTINGDATA, LanguageConstantValues.L_ERROR_REPORT_GETTINGDATA), exc);
				Presentation.ScenarioManager.LaunchErrorScenario(gettingDataException);

				// Indicate that an error has occurred during the report configuration.
				lExecuteFilterEventArgs.Success = false;
			}

			OnExecuteFilter(lExecuteFilterEventArgs);

			return lExecuteFilterEventArgs.Success;
		}
		
		/// <summary>
		/// Gets the report type depending on the report file extension.
		/// </summary>
		/// <returns>String with the report type name.</returns>
		public ReportTypes GetReportType()
		{
			if (this.mReportFile.EndsWith(".rdlc", StringComparison.InvariantCultureIgnoreCase))
				return ReportTypes.RDLC;

			if (this.mReportFile.EndsWith(".rpt", StringComparison.InvariantCultureIgnoreCase))
				return ReportTypes.CrystalReports;

			return ReportTypes.Unknown;
		}

		/// <summary>
		/// Obtain the report information from the received files.
		/// </summary>
		/// <returns></returns>
		private bool ReadReportInfo()
		{
			DataSet lDataSet = new DataSet();
			try
			{
				lDataSet.ReadXmlSchema(mDataSetFile);
			}
			catch (Exception e)
			{
				Exception excReadingFile = new Exception(CultureManager.TranslateString(LanguageConstantKeys.L_ERROR_LOADING_REPORTSCONFIG, LanguageConstantValues.L_ERROR_LOADING_REPORTSCONFIG), e);
				ScenarioManager.LaunchErrorScenario(excReadingFile);
				return false;
			}

			DataTable lStartingClass = lDataSet.Tables[this.ClassName];
			if (lStartingClass == null)
			{
				object[] lArgs = new object[2];
				lArgs[0] = this.ClassName;
				lArgs[1] = lDataSet.DataSetName;
				string lErrorMessage = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_ERROR_DATASET_TABLENOTFOUND, LanguageConstantValues.L_ERROR_DATASET_TABLENOTFOUND, lArgs);

				ScenarioManager.LaunchErrorScenario(new Exception(lErrorMessage));
				return false;
			}

			foreach (DataColumn lCol in lStartingClass.Columns)
			{
				if (!string.IsNullOrEmpty(mPopulationContext.DisplaySetAttributes))
					mPopulationContext.DisplaySetAttributes += ",";
				mPopulationContext.DisplaySetAttributes += lCol.ColumnName;
			}

			return true;
		}

		/// <summary>
		/// Execute the needed query in order to get the data for the report.
		/// </summary>
		/// <returns>A DataTable object with the instances retrieved.</returns>
		public DataTable GetData()
		{
			mPopulationContext.LastBlock = false;
			this.UpdateContext();

			DataTable lData = null;
			// Loop till all instances has been retrieved.
			while (!mPopulationContext.LastBlock)
			{
				DataTable lTempData = Logic.ExecuteQueryFilter(mPopulationContext);
				if (lTempData != null)
				{
					mPopulationContext.LastOid = Adaptor.ServerConnection.GetLastOid(lTempData);
					if (lData == null)
					{
						lData = lTempData;
					}
					else
					{
						lData.Merge(lTempData);
					}
				}
			}

			return lData;
		}

		/// <summary>
		/// Apply multilanguage strings to the scenario elements
		/// </summary>
		public override void ApplyMultilanguage()
		{
			base.ApplyMultilanguage();

			if (OkTrigger != null)
			{
				OkTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_PRINT, LanguageConstantValues.L_PRINT, OkTrigger.Value.ToString());
			}
			if (PreviewTrigger != null)
			{
				PreviewTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_PRINT_PREVIEW, LanguageConstantValues.L_PRINT_PREVIEW, PreviewTrigger.Value.ToString());
			}

		}
		#endregion Methods
	}
}




