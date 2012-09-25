// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Logics;
using SIGEM.Client.Presentation;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the Report HAT leafs.
	/// </summary>
	public class HatLeafReportController : HatElementController
	{
		#region Members
		/// <summary>
		/// Class name of the Filter needed by the report.
		/// </summary>
		private string mClassName;
		/// <summary>
		/// Filter to be used.
		/// </summary>
		private string mFilterName;
		/// <summary>
		/// DataSet file name.
		/// </summary>
		private string mDataSetFile;
		/// <summary>
		/// Report definition file name.
		/// </summary>
		private string mReportFile;
		/// <summary>
		/// Window Title.
		/// </summary>
		private string mWindowTitle;
		/// <summary>
		/// Trigger presentation.
		/// </summary>
		private ITriggerPresentation mTrigger;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets the Class name of the Filter needed by the report.
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
		/// Gets or sets the filter name.
		/// </summary>
		public string FilterName
		{
			get
			{
				return mFilterName;
			}
			set
			{
				mFilterName = value;
			}
		}
		/// <summary>
		/// Gets or sets the DataSet file name.
		/// </summary>
		public string DataSetFile
		{
			get
			{
				return mDataSetFile;
			}
			set
			{
				mDataSetFile = value;
			}
		}
		/// <summary>
		/// Gets or sets the Report definition file name.
		/// </summary>
		public string ReportFile
		{
			get
			{
				return mReportFile;
			}
			set
			{
				mReportFile = value;
			}
		}
		/// <summary>
		/// Gets or sets the Window Title.
		/// </summary>
		public string WindowTitle
		{
			get
			{
				return mWindowTitle;
			}
			set
			{
				mWindowTitle = value;
			}
		}
		/// <summary>
		/// Gets or sets the trigger presentation.
		/// </summary>
		public ITriggerPresentation Trigger
		{
			get
			{
				return mTrigger;
			}
			set
			{
				if (mTrigger != null)
				{
					mTrigger.Triggered -= new EventHandler<TriggerEventArgs>(Execute);
				}

				mTrigger = value;

				if (mTrigger != null)
				{
					mTrigger.Triggered += new EventHandler<TriggerEventArgs>(Execute);
					mTrigger.Value = Alias;
				}
			}
		}
		/// <summary>
		/// Gets or Sets the alias of the HAT node and apply it to the Label.
		/// </summary>
		public override string Alias
		{
			get
			{
				return base.Alias;
			}
			set
			{
				base.Alias = value;

				if (Trigger != null)
				{
					Trigger.Value = value;
				}
			}
		}

		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'HatLeafReportController' class.
		/// </summary>
		/// <param name="name">Node name. It must be unique.</param>
		/// <param name="className">Class name.</param>
		/// <param name="filterName">Filter name.</param>
		/// <param name="alias">Text to be shown in the menu.</param>
		/// <param name="dataSetFile">DataSet file name.</param>
		/// <param name="reportFile">Report definition file name.</param>
		/// <param name="windowTitle">Target form title.</param>
		/// <param name="agents">Agents allowed.</param>
		public HatLeafReportController(string name, string className, string filterName, string alias, string dataSetFile, string reportFile, string windowTitle, string[] agents)
			:base(name, alias, "", agents)
		{
			ClassName = className;
			FilterName = filterName;
			DataSetFile = dataSetFile;
			ReportFile = reportFile;
			WindowTitle = windowTitle;
		}
		#endregion Constructors

		#region Execute
		/// <summary>
		/// Executes the Report leaf associated method.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">TriggerEventArgs object.</param>
		public void Execute(object sender, TriggerEventArgs e)
		{
			try
			{
				// Launch Filter scenario.
				ScenarioManager.LaunchFilterForReportScenario(ClassName, FilterName, DataSetFile, ReportFile, WindowTitle);
			}
			catch (Exception err)
			{
				ScenarioManager.LaunchErrorScenario(err);
			}
		}
		#endregion Execute

		#region Methods

		/// <summary>
		/// Apply Agents visibility.
		/// </summary>
		public override void ApplyAgentsVisibility()
		{
			if (Trigger == null)
			{
				return;
			}

			Trigger.Visible = Logics.Logic.Agent.IsActiveFacet(Agents);
		}
		#endregion Methods
	}
}

