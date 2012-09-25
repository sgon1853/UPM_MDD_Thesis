// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using SIGEM.Client.Controllers;
using SIGEM.Client.Presentation.Forms;
using SIGEM.Client.InteractionToolkit;
using SIGEM.Client.Adaptor.Exceptions;
using SIGEM.Client.Adaptor;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Presentation
{
	public enum ShowState
	{
		None = 0,
		Showing = 1,
		Showed = 2,
	}
	#region Scenario Methods.
	/// <summary>
	/// Class that manages an scenario.
	/// </summary>
	public static class ScenarioManager
	{
		#region Static Member Variables.
		/// <summary>
		/// Refers to main scenario.
		/// </summary>
		private static MainForm mMainForm = null;

		/// <summary>
		/// Flag to avoid unnecessarily events
		/// </summary>
		private static ShowState mShowState = ShowState.None;

		#endregion Static Member Variables.

		#region Close application
		/// <summary>
		/// Indicates if application is shutting down.
		/// </summary>
		public static bool IsClosing = false;
		/// <summary>
		/// Closes the current application.
		/// </summary>
		public static void CloseApplication()
		{
			IsClosing = true;
			System.Windows.Forms.Application.Exit();
		}
		#endregion Close application

		#region Main Form Properties

		/// <summary>
		/// Gets or sets Main Scenario.
		/// </summary>
		public static MainForm MainForm
		{
			get
			{
				if(mMainForm == null)
				{
					mMainForm = new MainForm();
				}
				return mMainForm;
			}
		}
		#endregion Main Form Properties

		public static ShowState LaunchShowState
		{
			get { return mShowState; }
			private set { mShowState = value; }
		}

		#region Auxiliar Scenario Methods.

		#region Initialize
		/// <summary>
		/// Initializes an scenario.
		/// </summary>
		/// <param name="scenario">Scenario to initialize.</param>
		/// <param name="exchangeInfo">Scenario information.</param>
		/// <returns></returns>
		private static IUController Initialize(Form scenario, ExchangeInfo exchangeInfo)
		{
			IUController lResult = null;

			// Initialize form with context.
			#region call Initialize an show form.
			if (scenario != null)
			{
				// Find Initialize method for scenario.
				try
				{
					MethodInfo lInitialize = scenario.GetType().GetMethod("Initialize");
					if (lInitialize != null)
					{
						object[] lArgs = new object[1];
						lArgs[0] = exchangeInfo;
						lResult = lInitialize.Invoke(scenario, lArgs) as IUController;
					}
				}
				catch (Exception exception)
				{
					//throw;
					ScenarioManager.LaunchErrorScenario(exception);
				}
			}
			#endregion call Initialize an show form.

			return lResult;
		}
		#endregion Initialize

		#region CreateScenarioInstance
		/// <summary>
		/// Creates a new scenario.
		/// </summary>
		/// <param name="exchangeInfo">Scenario information.</param>
		/// <returns>The scenario created.</returns>
		private static Form CreateScenarioInstance(ExchangeInfo exchangeInfo)
		{
			Form lScenario = null;

			// Instance & Initialize Scenario.
			Type lScenarioType = Type.GetType(exchangeInfo.IUName);
			if (lScenarioType != null)
			{
				// Instance Scenario.
				lScenario = Activator.CreateInstance(lScenarioType) as Form;

				// Set Form position
				if (lScenario != null && lScenario.StartPosition == FormStartPosition.WindowsDefaultLocation)
				{
					lScenario.StartPosition = FormStartPosition.Manual;
					lScenario.Location = new System.Drawing.Point((MainForm.ClientSize.Width - lScenario.Width) / 2, ((MainForm.ClientSize.Height - lScenario.Height) / 2) * 3/4);
				}
			}

			return lScenario;
		}
		#endregion CreateScenarioInstance

		#endregion Auxiliar Scenario Methods.

		#region Launch Conditional Navigation  Scenario
		public static void LaunchConditionalNavigationScenario(ExchangeInfoConditionalNavigation exchangeInfoConditionalNavigation, IActionItemSuscriber actionItem)
		{
			ExchangeInfo exchangeInfo = null;
			if (exchangeInfoConditionalNavigation.ConditionalNavigationInfo.Count == 1)
			{
				exchangeInfo = exchangeInfoConditionalNavigation.ConditionalNavigationInfo[0].ExchangeInfo;
			}
			else
			{
				Form lScenario = CreateScenarioInstance(exchangeInfoConditionalNavigation);
				Initialize(lScenario, exchangeInfoConditionalNavigation);
				lScenario.ShowDialog(MainForm);
				if ((exchangeInfoConditionalNavigation != null) && (exchangeInfoConditionalNavigation.DestinationInfo != null))
				{
					exchangeInfo = exchangeInfoConditionalNavigation.DestinationInfo.ExchangeInfo;
				}
			}

			if (exchangeInfo != null)
			{
				switch (exchangeInfo.ExchangeType)
				{
					case ExchangeType.Navigation:
						LaunchNavigationScenario(exchangeInfo as ExchangeInfoNavigation, null);
						break;
					case ExchangeType.Action:
						LaunchActionScenario(exchangeInfo as ExchangeInfoAction, actionItem);
						break;
				}
			}
		}
		#endregion Launch Conditional Navigation  Scenario

		#region Launch Action Scenario
		/// <summary>
		/// Launchs an action in an scenario.
		/// </summary>
		/// <param name="actionInfo">Action information.</param>
		/// <param name="actionItem">Item involved in the action.</param>
		public static void LaunchActionScenario(ExchangeInfoAction actionInfo, IActionItemSuscriber actionItem)
		{
			// Print Scenario
			if (actionInfo.IUName == typeof(PrintForm).FullName)
			{
				LaunchPrintScenario(actionInfo.SelectedOids);
				return;
			}

			// Instance Scenario, initialize and return the controller instance inside the Scenario.
			Form lScenario = CreateScenarioInstance(actionInfo);
			IUController lController = Initialize(lScenario, actionInfo);
			if (lController != null)
			{
				if (actionItem != null)
				{
					actionItem.SuscribeActionEvents(lController as IActionItemEvents);
				}
			}
			if (lScenario != null)
			{
                IUServiceController lIUServiceController = lController as IUServiceController;
                if (lIUServiceController != null && !lIUServiceController.ShowScenario)
                {
                    lIUServiceController.Execute();
                }
                else
                {
                    lScenario.MdiParent = MainForm;
                    LaunchShowState = ShowState.Showing;
                    lScenario.Show();
                    LaunchShowState = ShowState.Showed;
                }
			}
		}
		#endregion Launch Action Scenario

		#region Launch Navigation Scenario
		/// <summary>
		/// Launchs a navigation in an scenario.
		/// </summary>
		/// <param name="navigationInfo">Navigation information.</param>
		public static void LaunchNavigationScenario(ExchangeInfoNavigation navigationInfo, INavigationItemSuscriber navigationItem)
		{
			// Instance Scenario, initialize and return the controller instance inside the Scenario.
			Form lScenario = CreateScenarioInstance(navigationInfo);
			IUController lController = Initialize(lScenario, navigationInfo);

            if (lController != null)
            {
                if (navigationItem != null)
                {
                    IUMasterDetailController lMasterDetailController = lController as IUMasterDetailController;
                    if (lMasterDetailController != null)
                    {
                        navigationItem.SuscribeNavigationEvents(lMasterDetailController.Master as INavigationItemEvents);
                    }
                    else
                    {
                        navigationItem.SuscribeNavigationEvents(lController as INavigationItemEvents);
                    }
                }
            }

			if (lScenario != null)
			{
				lScenario.MdiParent = MainForm;
				LaunchShowState = ShowState.Showing;
				lScenario.Show();
				LaunchShowState = ShowState.Showed;
			}
		}
		#endregion Launch Navigation Scenario

		#region Launch Selection Scenario
		/// <summary>
		/// Launchs a selection in an scenario.
		/// </summary>
		/// <param name="selectionForwardInfo">Origin scenario information.</param>
		/// <param name="argument">Selected oid information.</param>
		public static void LaunchSelectionScenario(
			ExchangeInfoSelectionForward selectionForwardInfo,
			ISelectionBackward argument)
		{

			// Instance Scenario, initialize and return the controller instance inside the Scenario.
			Form lScenario = CreateScenarioInstance(selectionForwardInfo);
			IUController lController = Initialize(lScenario, selectionForwardInfo);

			if (lController!= null)
			{
				if (argument != null)
				{
					argument.SuscribeSelectionBackward(lController as IInstancesSelector);
				}
			}
			if (lScenario != null)
			{
				lScenario.MdiParent = MainForm;
				LaunchShowState = ShowState.Showing;
				lScenario.Show();
				LaunchShowState = ShowState.Showed;
			}
		}
		#endregion Launch Selection Scenario

		#region Launch Error Scenario
		/// <summary>
		/// Launches the error scenario of the application.
		/// </summary>
		/// <param name="exception">Exception to show.</param>
		public static void LaunchErrorScenario(Exception exception)
		{
			ResponseException responseException = GetServerError(exception);

			ErrorForm lErrorForm = new ErrorForm();
			if (responseException == null)
			{
				// Client application exception.
				lErrorForm.Initialize(exception);
			}
			else
			{
				// Business logic exception.
				lErrorForm.Initialize(responseException);
			}

			// Launch the error form.
			lErrorForm.ShowDialog();
		}
		/// <summary>
		/// Identify if the error or exception comes from the business logic.
		/// </summary>
		/// <param name="exception">Exception to analize.</param>
		/// <returns></returns>
		private static ResponseException GetServerError(Exception exception)
		{
			ResponseException responseException = null;

			while (exception != null)
			{
				responseException = exception as ResponseException;
				if (responseException != null)
				{
					break;
				}
				exception = exception.InnerException;
			}

			return responseException;
		}
		#endregion Launch Error Scenario

		#region Launch Outbount Argument Scenario
		public static void LaunchOutbountArgumentsScenario(ExchangeInfo exchangeInfo)
		{
			// Instance Scenario, initialize and return the controller instance inside the Scenario.
			Form lScenario = CreateScenarioInstance(exchangeInfo);
			IUController lController = Initialize(lScenario, exchangeInfo);
			if (lController != null)
			{
				lScenario.MdiParent = MainForm;
				LaunchShowState = ShowState.Showing;
				lScenario.Show();
				LaunchShowState = ShowState.Showed;
			}
		}
		#endregion Launch Outboun Argumenst Scenario

		#region Launch Multiexecution Report Scenario
		public static void LaunchMultiExecutionReportScenario(System.Data.DataTable report, String serviceAlias, Dictionary<string, KeyValuePair<string, string>> inboundArgument, Dictionary<string, KeyValuePair<string, string>> outboundArgument)
		{
			// Instance Scenario, initialize and return the controller instance inside the Scenario.
			MultiExecutionReport lScenario = new MultiExecutionReport(serviceAlias);
			lScenario.MdiParent = MainForm;
			lScenario.SetFormat(report, inboundArgument, outboundArgument);
			lScenario.ShowReport(report);
			LaunchShowState = ShowState.Showing;
			lScenario.Show();
			LaunchShowState = ShowState.Showed;
		}
		#endregion Launch Multiexecution Report Scenario

		#region Launch Print Scenario
		/// <summary>
		/// Launchs the Print scenario.
		/// </summary>
		public static void LaunchPrintScenario(List<Oid> oids)
		{
			if (oids == null || oids.Count == 0)
			{
				return;
			}

			PrintForm lPrintForm = new PrintForm();
			lPrintForm.Initialize(oids);
		}
		#endregion Launch Print Scenario
		#region Launch Preferences Scenario
		public static bool LaunchPreferencesScenario(IUQueryController queryController)
		{
			CustomizeForm lForm = new CustomizeForm();

			lForm.Initialize(queryController);

			if (lForm.ShowDialog() == DialogResult.OK)
			{
				return true;
			}

			return false;
		}
		#endregion Launch Preferences Scenario

		#region Launch State Change Detection error Scenario
		/// <summary>
		/// Opens the 'State Change Detected' error scenario.
		/// </summary>
		/// <param name="SCDInfo">Contains information about the 'State Change Detected' of the OV arguments of a service.</param>
		/// <param name="newValues">Contains the new values of the fields that participates in the 'State Change Detected' of the OV arguments of a service.</param>
		/// <param name="showValues">Inidicates if the differences has to be showed or not.</param>
		/// <param name="allowRetry">Indicates if the user can retry teh service execution or not.</param>
		/// <returns>Returns true if the user retries the service execution.</returns>
		public static bool LaunchSCDErrorScenario(Dictionary<string, KeyValuePair<Oid, DisplaySetInformation>> SCDInfo, Dictionary<string, object> newValues, bool showValues, bool allowRetry)
		{
			StateChangeDetectionErrorForm lForm = new StateChangeDetectionErrorForm();

			lForm.Initialize(SCDInfo, newValues, showValues, allowRetry);

			DialogResult lResult = lForm.ShowDialog();
			if (lResult == DialogResult.Retry)
			{
				// Launch service again, using current values.
				return true;
			}

			return false;
		}
		#endregion Launch State Change Detection error Scenario
		#region Launch Crystal Report Preview Scenario
		/// <summary>
		/// Instanciate and open the Crystal Reports report preview scenario.
		/// </summary>
		/// <param name="reportInfoDataSet">Contains the report DataSet information.</param>
		/// <param name="reportPath">Path of the report definition file.</param>
		/// <param name="reportParams">List of parameter values to initialize the report.</param>
		/// <param name="print">Indicates if the report will be directly printed or will be previewed only.</param>
		public static void LaunchCrystalReportPreviewScenario(System.Data.DataSet reportInfoDataSet, string reportPath, Dictionary<string, object> reportParams, bool print)
		{
		}
		#endregion Launch Crystal Report Preview Scenario
		
		#region Launch RDL Report Preview Scenario
		/// <summary>
		/// Instanciate and open the RDL report preview scenario.
		/// </summary>
		/// <param name="reportInfoDataSet">Contains the report DataSet information.</param>
		/// <param name="reportPath">Path of the report definition file.</param>
		/// <param name="reportParams">List of parameter values to initialize the report.</param>
		/// <param name="print">Indicates if the report will be directly printed or will be previewed only.</param>
		public static void LaunchRDLReportPreviewScenario(System.Data.DataSet reportInfoDataSet, string reportPath, Dictionary<string, object> reportParams, bool print)
		{
		}
		#endregion Launch RDL Report Preview Scenario

		#region Launch Filter for Report Scenario
		/// <summary>
		/// Launchs the Print scenario.
		/// </summary>
		public static void LaunchFilterForReportScenario(string className, string filterName, string dataSetFile, string reportFile, string windowTitle)
		{
			try
			{
				Form lScenario = null;

				// Create the Form name
				string lFormName = "SIGEM.Client.InteractionToolkit." + className + ".Filters." + filterName;

				Type lScenarioType = Type.GetType(lFormName);
				if (lScenarioType == null)
				{
					LaunchErrorScenario(new Exception("Filter form not found")); 
					return;
				}

				// Instance Scenario.
				lScenario = Activator.CreateInstance(lScenarioType) as Form;

				MethodInfo lInitialize = lScenario.GetType().GetMethod("Initialize");
				if (lInitialize != null)
				{
					object[] lArgs = new object[3];
					lArgs[0] = dataSetFile;
					lArgs[1] = reportFile;
					lArgs[2] = windowTitle;
					lInitialize.Invoke(lScenario, lArgs);
					lScenario.MdiParent = MainForm;
					LaunchShowState = ShowState.Showing;
					lScenario.Show();
					LaunchShowState = ShowState.Showed;
				}
			}
			catch (Exception e)
			{
				LaunchErrorScenario(e);
			}
		}
		#endregion Launch Filter for Report Scenario
	}
	#endregion Scenario Methods.
}
