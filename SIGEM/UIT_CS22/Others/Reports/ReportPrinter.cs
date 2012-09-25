// v3.8.4.5.b
using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Collections.Generic;

using SIGEM.Client.PrintingDriver.Common;
using SIGEM.Client.Oids;

namespace SIGEM.Client.PrintingDriver
{

	#region Constants

	/// <summary>
	/// Indicates if print directly is allowed for specific report type
	/// </summary>
	public static class PrintDirectlyCtes
	{
		/// <summary>
		/// Has to be directly printed if it is a Microsoft Word document?
		/// </summary>
		public const bool Word = true;
		/// <summary>
		/// Has to be directly printed if it is a Microsoft Excel sheet?
		/// </summary>
		public const bool Excel = false;
		/// <summary>
		/// Has to be directly printed if it is a Crystal Reports report?
		/// </summary>
		public const bool CrystalReports = true;
		/// <summary>
		/// Has to be directly printed if it is a Microsoft Reporting Services report?
		/// </summary>
		public const bool RDLC = true;
	}

	/// <summary>
	/// Report types
	/// </summary>
	public enum ReportTypes
	{
		Unknown,
		/// <summary>
		/// Microsoft Word.
		/// </summary>
		Word,
		/// <summary>
		/// Microsoft Excel.
		/// </summary>
		Excel,
		/// <summary>
		/// Crystal Reports.
		/// </summary>
		CrystalReports,
		/// <summary>
		/// Microsoft Reporting Services report definition language.
		/// </summary>
		RDLC
	}
	#endregion Constants

	#region InstanceReports
	/// <summary>
	/// Stores reports information from XML configuration file
	/// </summary>
	public class InstanceReports
	{
		#region Members
		/// <summary>
		/// List of report configurations
		/// </summary>
		private List<ReportConfiguration> mReports = new List<ReportConfiguration>();
		#endregion Members

		#region Constructors
		public InstanceReports()
		{
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// List of report configurations
		/// </summary>
		public List<ReportConfiguration> Reports
		{
			get
			{
				return mReports;
			}
		}
		#endregion Properties

		#region Methods
		/// <summary>
		/// Load report from the XML configuration file.
		/// </summary>
		/// <param name="configurationFile">Reports configuration file name.</param>
		public void LoadFromFile(string configurationFile)
		{
			Reports.Clear();

			// Verify the path and file specified in MainMenuReports Settings.
			string lFilePath = configurationFile;
			if (!System.IO.File.Exists(lFilePath))
			{
				lFilePath = System.Windows.Forms.Application.StartupPath + "\\" + lFilePath;
				if (!System.IO.File.Exists(lFilePath))
				{
					return;
				}
			}

			try
			{
				XmlDocument lXMLDoc = new XmlDocument();
				lXMLDoc.Load(lFilePath);

				XmlNodeList lNodeList = lXMLDoc.GetElementsByTagName("Report");

				int lCode = 1;
				char[] lSeparators = new char[] { ',' };
				foreach (XmlNode lNode in lNodeList)
				{
					string lAgentsWithoutBlanks = UtilFunctions.GetProtectedXmlNodeValue(configurationFile, lNode, "agents");
					lAgentsWithoutBlanks = lAgentsWithoutBlanks.Replace(" ", string.Empty);
					string[] lAgents = lAgentsWithoutBlanks.Split(lSeparators, StringSplitOptions.RemoveEmptyEntries);
					// If connected agent is not in the list, skip that report.
					if (lAgents.Length > 0 && !Logics.Logic.Agent.IsActiveFacet(lAgents))
					{
						continue;
					}

					// Get report info.
					string lClass = UtilFunctions.GetProtectedXmlNodeValue(configurationFile, lNode, "class");
					string lReportTypeString = UtilFunctions.GetProtectedXmlNodeValue(configurationFile, lNode, "reporttype");

					// If type is not supported, skip it.
					ReportTypes lReportType = ReportTypes.Unknown;
					switch (lReportTypeString.ToUpper())
					{
						case "WORD":
							lReportType = ReportTypes.Word;
							break;
						case "EXCEL":
							lReportType = ReportTypes.Excel;
							break;
						case "CRYSTAL":
							lReportType = ReportTypes.CrystalReports;
							break;
						case "RDLC":
							lReportType = ReportTypes.RDLC;
							break;
					}
					if (lReportType == ReportTypes.Unknown)
					{
						continue;
					}

					string lDataSetFileName = UtilFunctions.GetProtectedXmlNodeValue(configurationFile, lNode, "datasetfile");

					// Get complete filename.
					if (File.Exists(lDataSetFileName))
					{
						FileInfo lDataSetFile = new FileInfo(lDataSetFileName);
						lDataSetFileName = lDataSetFile.FullName;
					}

					// Get language information.
					XmlNodeList lLanguageNodeList = lNode.SelectNodes("Language");
					bool lExistLanguage = false;
					string lAlias = "";
					string lReportFileName = "";
					foreach (XmlNode lNodeLanguage in lLanguageNodeList)
					{
						string lLanguageKey = UtilFunctions.GetProtectedXmlNodeValue(configurationFile, lNodeLanguage, "key");
						if (lLanguageKey.Length == 0 ||
							lLanguageKey.Equals(CultureManager.Culture.Name, StringComparison.InvariantCultureIgnoreCase))
						{
							lAlias = UtilFunctions.GetProtectedXmlNodeValue(configurationFile, lNodeLanguage, "alias");
							lReportFileName = UtilFunctions.GetProtectedXmlNodeValue(configurationFile, lNodeLanguage, "reportfilepath");
							lExistLanguage = true;
							break;
						}
					}

					// If report is not for current language, skip it.
					if (!lExistLanguage)
						continue;

					if (File.Exists(lReportFileName))
					{
						FileInfo lReportFile = new FileInfo(lReportFileName);
						lReportFileName = lReportFile.FullName;
					}

					// Add instance report info to the list.
					ReportConfiguration lReport = new ReportConfiguration(lCode.ToString(), lAlias, lClass, lReportFileName, lReportType, lDataSetFileName);
					Reports.Add(lReport);
					lCode++;
				}
			}
			catch (Exception e)
			{
				Exception excProcessingFile = new Exception(CultureManager.TranslateString(LanguageConstantKeys.L_ERROR_LOADING_REPORTSCONFIG, LanguageConstantValues.L_ERROR_LOADING_REPORTSCONFIG), e);
				Presentation.ScenarioManager.LaunchErrorScenario(excProcessingFile);
			}
		}

		/// <summary>
		/// Returns defined reports for specified class.
		/// </summary>
		/// <param name="className"></param>
		/// <returns></returns>
		public List<ReportConfiguration> GetClassReports(string className)
		{
			List<ReportConfiguration> lList = new List<ReportConfiguration>();

			foreach (ReportConfiguration lReport in Reports)
			{
				if (lReport.ClassName.Equals(className, StringComparison.InvariantCultureIgnoreCase))
				{
					lList.Add(lReport);
				}
			}

			return lList;
		}

		/// <summary>
		/// Returns Report Agents for specified class
		/// </summary>
		/// <param name="className"></param>
		/// <returns></returns>
		public string[] GetAgentsForClass(string className)
		{
			if (GetClassReports(className).Count > 0)
			{
				return Logics.Agents.All;
			}

			return new string[]{""};
		}

		#endregion Methods
	}

	#endregion InstanceReports

	#region ReportConfiguration
	/// <summary>
	/// Store intance report information
	/// </summary>
	public class ReportConfiguration
	{
		#region Members
		/// <summary>
		/// Report code
		/// </summary>
		private string mCode;
		/// <summary>
		/// Report Alias
		/// </summary>
		private string mAlias;
		/// <summary>
		/// Report Model class
		/// </summary>
		private string mClassName;
		/// <summary>
		/// Complete filename of the report (.dot, .xlt, rdlc, rpt)
		/// </summary>
		private string mReportFilePath;
		/// <summary>
		/// Report type
		/// </summary>
		private ReportTypes mReportType;
		/// <summary>
		/// Dataset complete filename. Only for Crystal and RDLC reports
		/// </summary>
		private string mDatasetFile;
		#endregion Members

		#region Constructors
		public ReportConfiguration()
		{
		}
		public ReportConfiguration(string code, string alias, string className, string reportFilePath, ReportTypes reportType, string datasetFile)
		{
			mCode = code;
			mAlias = alias;
			mClassName = className;
			mReportFilePath = reportFilePath;
			mReportType = reportType;
			mDatasetFile = datasetFile;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Code that identifies the report.
		/// </summary>
		public string Code
		{
			get
			{
				return mCode;
			}
			set
			{
				mCode = value;
			}
		}

		/// <summary>
		/// Report Alias
		/// </summary>
		public string Alias
		{
			get
			{
				return mAlias;
			}
			set
			{
				mAlias = value;
			}
		}

		/// <summary>
		/// Report Model class
		/// </summary>
		public string ClassName
		{
			get
			{
				return mClassName;
			}
			set
			{
				mClassName = value;
			}
		}

		/// <summary>
		/// Complete filename of the report (.dot, .xlt, rdlc, rpt)
		/// </summary>
		public string ReportFilePath
		{
			get
			{
				return mReportFilePath;
			}
			set
			{
				mReportFilePath = value;
			}
		}

		/// <summary>
		/// Report type
		/// </summary>
		public ReportTypes ReportType
		{
			get
			{
				return mReportType;
			}
			set
			{
				mReportType = value;
			}
		}

		/// <summary>
		/// Dataset complete filename. Only for Crystal and RDLC reports
		/// </summary>
		public string DatasetFile
		{
			get
			{
				return mDatasetFile;
			}
			set
			{
				mDatasetFile = value;
			}
		}
		

		#endregion Properties

		#region Methods
		#endregion Methods
	}
	#endregion ReportConfiguration
}
