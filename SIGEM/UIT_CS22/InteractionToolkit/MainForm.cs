// v3.8.4.5.b
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using System.Collections.Generic;

using SIGEM.Client.Controllers;
using SIGEM.Client.Presentation;
using SIGEM.Client.Presentation.Forms;

namespace SIGEM.Client.InteractionToolkit
{


	public partial class MainForm : Form
	{
		#region Members
		private IUMainController mController = null;
		#endregion Members
		
		#region Properties
		public IUMainController Controller
		{
			get
			{
				return mController;
			}
		}
		#endregion Properties
		
		#region Constructors
		public MainForm()
		{
			InitializeComponent();

			// Apply MultiLanguage.
			MultilanguageFixedString();

			Initialize();
		}
		
		public IUMainController Initialize()
		{
			mController = ControllerFactory.MainScenario.MainController();
			
			mController.Scenario = new ScenarioPresentation(this, ScenarioType.Other);			

			IListKeyed<HatElementController> lHatList = null;
			HatLeafController lHatLeafController = null;

			lHatList = mController.HatElementNodes;

			
			if (mController.Context.Agent == null)
			{
				this.Load += new EventHandler(LoadForm);
			}

			
			// Menu Items Linker
 			(lHatList["0"] as HatNodeController).Label = new ToolStripMenuItemPresentation(this.mHAT0, false);
			lHatLeafController = lHatList["0_0"] as HatLeafController;
			lHatLeafController.Trigger = new ToolStripMenuItemPresentation(this.mHAT0_0);
			lHatLeafController = lHatList["0_1"] as HatLeafController;
			lHatLeafController.Trigger = new ToolStripMenuItemPresentation(this.mHAT0_1);
 			(lHatList["1"] as HatNodeController).Label = new ToolStripMenuItemPresentation(this.mHAT1, false);
			lHatLeafController = lHatList["1_0"] as HatLeafController;
			lHatLeafController.Trigger = new ToolStripMenuItemPresentation(this.mHAT1_0);
			lHatLeafController = lHatList["1_1"] as HatLeafController;
			lHatLeafController.Trigger = new ToolStripMenuItemPresentation(this.mHAT1_1);
 			(lHatList["2"] as HatNodeController).Label = new ToolStripMenuItemPresentation(this.mHAT2, false);
			lHatLeafController = lHatList["2_0"] as HatLeafController;
			lHatLeafController.Trigger = new ToolStripMenuItemPresentation(this.mHAT2_0);
			lHatLeafController = lHatList["2_1"] as HatLeafController;
			lHatLeafController.Trigger = new ToolStripMenuItemPresentation(this.mHAT2_1);
 			(lHatList["3"] as HatNodeController).Label = new ToolStripMenuItemPresentation(this.mHAT3, false);
			lHatLeafController = lHatList["3_0"] as HatLeafController;
			lHatLeafController.Trigger = new ToolStripMenuItemPresentation(this.mHAT3_0);
			lHatLeafController = lHatList["3_1"] as HatLeafController;
			lHatLeafController.Trigger = new ToolStripMenuItemPresentation(this.mHAT3_1);
 			(lHatList["4"] as HatNodeController).Label = new ToolStripMenuItemPresentation(this.mHAT4, false);
			lHatLeafController = lHatList["4_0"] as HatLeafController;
			lHatLeafController.Trigger = new ToolStripMenuItemPresentation(this.mHAT4_0);
			lHatLeafController = lHatList["4_1"] as HatLeafController;
			lHatLeafController.Trigger = new ToolStripMenuItemPresentation(this.mHAT4_1);
			lHatLeafController = lHatList["4_2"] as HatLeafController;
			lHatLeafController.Trigger = new ToolStripMenuItemPresentation(this.mHAT4_2);

			mController.Initialize();
			
			// Wait until finish Splash window.
			MainSplashForm.SplashThread.Join();

			return mController;
		}
		#endregion Constructors

		#region Load form
		private void LoadForm(object sender, EventArgs e)
		{
			this.Text = "Desktop";
			// If there is only one agent and it is anonymous do not show login.
			if (!CheckIsAnonymousAndLogIn())
			{
				mValidateAgent_Click(sender, e);
			}
			else
			{
				// Assign the unique agent anonymous as the connected agent.
				string lAgentClassName = Logics.Agents.All[0].ToString();
				Oids.Oid lagent = Oids.Oid.Create(lAgentClassName);
				Logics.Logic.Agent = lagent as Oids.AgentInfo;

				// Load instance reports configuration.
				Logics.Logic.InstanceReportsList.LoadFromFile(Properties.Settings.Default.ConfigurationOfReports);

				// Load configuration reports file.
				LoadConfigurationReportsFile();
			}
		}
		#endregion Load form
		
		#region Events
		private void SuccessAuthenticate(object pObject, EventArgs pEventArgs)
		{
			// Check if the agent exists.
			if (Logics.Logic.Agent != null)
			{
				Oids.Oid lAgent = Logics.Logic.Agent;
				Logics.LogInAgent logInAgent = Logics.Agents.GetLogInAgentByName(lAgent.ClassName);
				if (logInAgent.AlternateKeyName != string.Empty)
				{
					// Get the alternate key of the Oid.
					lAgent = (Oids.Oid)lAgent.GetAlternateKey(logInAgent.AlternateKeyName);
				}
				// HAT Multilanguage: Apply multilanguage to the HAT elements.
				Controller.ApplyMultilanguage();

				// ... and to the fixed strings.
				MultilanguageFixedString();

				Controller.ApplyConnectedAgentVisibility();
				
				// Put the connected agent and the culture on the main form status label.
				StringBuilder lStringBuilder = new StringBuilder();
				if ((lAgent != null) && (lAgent is Oids.AnonymousAgentInfo))
				{
					lStringBuilder.Append(logInAgent.Alias);
				}
				else
				{
					lStringBuilder.Append(logInAgent.Alias);
					lStringBuilder.Append(" : ");
					lStringBuilder.Append(UtilFunctions.OidFieldsToString(lAgent, ' '));
				}
				toolStripStatusLabel.Text = UtilFunctions.ProtectAmpersandChars(lStringBuilder.ToString());

				// Show Current culture.
				toolStripStatusLabelCulture.Text = CultureManager.Culture.Name;

				// Load instance reports configuration.
				Logics.Logic.InstanceReportsList.LoadFromFile(Properties.Settings.Default.ConfigurationOfReports);

				// Load configuration reports file.
				LoadConfigurationReportsFile();
			}
			else
			{
				Close();
			}
		}
		private void MultilanguageFixedString()
		{
			string [] lAgentsList = Logics.Agents.All;
			// Check if anonymous agent is connectedUnique anonymous agent without multilanguage.
			if ((Logics.Logic.Agent != null) && (Logics.Logic.Agent is Oids.AnonymousAgentInfo))  
			{
				// Case unique agent and is anonymous.
				if (lAgentsList.Length == 1)
				{
					// Case with multilanguage.
					if (CultureManager.SupportedLanguages.Count > 1)
					{
						// Apply Change Language menu entry.
						this.mValidateAgent.Text = CultureManager.TranslateString(LanguageConstantKeys.L_MAIN_CHANGE_LANGUAGE, LanguageConstantValues.L_MAIN_CHANGE_LANGUAGE);
						// Hide Change Password menu entry.
						this.mChangePassword.Visible = false;
					}
				}
				// Case various agents and is anonymous.
				else
				{
					this.mValidateAgent.Text = CultureManager.TranslateString(LanguageConstantKeys.L_LOGIN, LanguageConstantValues.L_LOGIN);
					this.mChangePassword.Visible = false;
				}
			}
			// Agent connected is not anonymous.
			else
			{
				this.mValidateAgent.Text = CultureManager.TranslateString(LanguageConstantKeys.L_LOGIN, LanguageConstantValues.L_LOGIN);
				this.mChangePassword.Visible = true;
			}
			this.mFileSubMenu.Text = CultureManager.TranslateString(LanguageConstantKeys.L_MAIN_MENU_FILE, LanguageConstantValues.L_MAIN_MENU_FILE);
			this.mChangePassword.Text = CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_CAPTION, LanguageConstantValues.L_PASSWORD_CAPTION);
			this.mExitMenuItem.Text = CultureManager.TranslateString(LanguageConstantKeys.L_MAIN_MENU_FILE_EXIT, LanguageConstantValues.L_MAIN_MENU_FILE_EXIT);
			this.mHelpSubMenu.Text = CultureManager.TranslateString(LanguageConstantKeys.L_MAIN_MENU_HELP, LanguageConstantValues.L_MAIN_MENU_HELP);
			this.mAboutMenuItem.Text = CultureManager.TranslateString(LanguageConstantKeys.L_MAIN_MENU_HELP_ABOUT, LanguageConstantValues.L_MAIN_MENU_HELP_ABOUT);
			this.mWindowSubMenu.Text = CultureManager.TranslateString(LanguageConstantKeys.L_MAIN_MENU_WINDOW, LanguageConstantValues.L_MAIN_MENU_WINDOW);
			this.mMaximizeMenuItem.Text = CultureManager.TranslateString(LanguageConstantKeys.L_MAIN_MENU_WINDOW_MAXIMIZE, LanguageConstantValues.L_MAIN_MENU_WINDOW_MAXIMIZE);
			this.mMinimizeMenuItem.Text = CultureManager.TranslateString(LanguageConstantKeys.L_MAIN_MENU_WINDOW_MINIMIZE, LanguageConstantValues.L_MAIN_MENU_WINDOW_MINIMIZE);
			this.mCascadeMenuItem.Text = CultureManager.TranslateString(LanguageConstantKeys.L_MAIN_MENU_WINDOW_CASCADE, LanguageConstantValues.L_MAIN_MENU_WINDOW_CASCADE);
			this.mTileHorizontalMenuItem.Text = CultureManager.TranslateString(LanguageConstantKeys.L_MAIN_MENU_WINDOW_HORIZONTAL, LanguageConstantValues.L_MAIN_MENU_WINDOW_HORIZONTAL);
			this.mTileVerticalMenuItem.Text = CultureManager.TranslateString(LanguageConstantKeys.L_MAIN_MENU_WINDOW_VERTICAL, LanguageConstantValues.L_MAIN_MENU_WINDOW_VERTICAL);
			this.mCloseAllMenuItem.Text = CultureManager.TranslateString(LanguageConstantKeys.L_MAIN_MENU_WINDOW_CLOSEALL, LanguageConstantValues.L_MAIN_MENU_WINDOW_CLOSEALL);
			
		}
		private void CancelAthenticate(object sender, EventArgs e)
		{
			if (Logics.Logic.Agent == null)
			{
				Close();
			}

		}
		private void mValidateAgent_Click(object sender, EventArgs e)
		{
			// If there are opened windows close all.
			if ((Logics.Logic.Agent != null) && (this.MdiChildren.Length > 0))
			{
				string lText = CultureManager.TranslateString(LanguageConstantKeys.L_MAIN_CLOSE_ALL_WINDOWS, LanguageConstantValues.L_MAIN_CLOSE_ALL_WINDOWS);
				string lCaption = CultureManager.TranslateString(LanguageConstantKeys.L_WARNING, LanguageConstantValues.L_WARNING);
				if (MessageBox.Show(lText, lCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
				{
					// Close all the windows.
					mCloseAllMenuItem_Click(this, e);
				}
				else
				{
					// Do not execute the login.
					return;
				}
			}

			// Create the login form.
			LoginForm lLoginForm = new LoginForm();
			
			// Subscribe to the authentication events.
			lLoginForm.SuccessAthenticate += new EventHandler(SuccessAuthenticate);
			lLoginForm.CancelAthenticate += new EventHandler(CancelAthenticate);

			// If there are no connected agent hide the menu.
			if (Logics.Logic.Agent == null)
			{
				Controller.HideAllHATEntries();
			}

			// Show the login window.
			lLoginForm.ShowDialog();
		}
		private void mChangePassword_Click(object sender, EventArgs e)
		{
			// Open the Change Password window.
			ChangePasswordForm lChangePasswordForm = new ChangePasswordForm();
			lChangePasswordForm.ShowDialog();
		}
		
		private void mExitMenuItem_Click(object sender, EventArgs e)
		{
			// Close the application.
			this.Close();
		}
		
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// If there are opened windows.
			if (this.MdiChildren.Length > 0)
			{
				// Show the message to close all the windows.
				string lText = CultureManager.TranslateString(LanguageConstantKeys.L_MAIN_CLOSE, LanguageConstantValues.L_MAIN_CLOSE);
				string lCaption = CultureManager.TranslateString(LanguageConstantKeys.L_WARNING, LanguageConstantValues.L_WARNING);
				if (MessageBox.Show(lText, lCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					// Do not close the windows and the application.
					e.Cancel = true;
					return;
				}
			}
		}
		private void mAboutMenuItem_Click(object sender, EventArgs e)
		{
			// Show the AboutBox window.
			AboutBoxForm lAboutBox = new AboutBoxForm();
			lAboutBox.ShowDialog();
		}
		private void mMaximizeMenuItem_Click(object sender, EventArgs e)
		{
			//For each child form set the window state to Maximized.
			foreach (Form chform in this.MdiChildren)
			{
				chform.WindowState = FormWindowState.Maximized;
			}
		}
		private void mMinimizeMenuItem_Click(object sender, EventArgs e)
		{
			// For each child form set the window state to Minimized.
			foreach (Form chform in this.MdiChildren)
			{
				chform.WindowState = FormWindowState.Minimized;
			}
		}
		private void mCascadeMenuItem_Click(object sender, EventArgs e)
		{
			this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
		}
		private void mTileHorizontalMenuItem_Click(object sender, EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileHorizontal);
		}
		private void mTileVerticalMenuItem_Click(object sender, EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileVertical);
		}
		private void mCloseAllMenuItem_Click(object sender, EventArgs e)
		{
			// Close each child form.
			foreach (Form chform in this.MdiChildren)
			{
				chform.Close();
			}
		}
		#endregion Events

		#region Methods
		/// <summary>
		/// Checks if the Login form must be showed or not. 
		/// This is calculated according the number of agents defined in the View, 
		/// its kind (anonymous or not), and the number of languages of the application.
		/// </summary>
		/// <returns>A boolean value indicating if the Login form is going to be shown or not.</returns>
		private bool CheckIsAnonymousAndLogIn()
		{
			bool lResult = false;

			if (Logics.Agents.All.Length == 1)
			{
				// Create agent info.
				string lAgentClassName = Logics.Agents.All[0].ToString();
				Oids.Oid lagent = Oids.Oid.Create(lAgentClassName);
				Oids.AgentInfo lAgentInfo = lagent as Oids.AgentInfo;

				if (lAgentInfo is Oids.AnonymousAgentInfo)
				{
					if (CultureManager.SupportedLanguages.Count > 1)
					{
						// Apply Change Language menu entry.
						this.mValidateAgent.Text = CultureManager.TranslateString(LanguageConstantKeys.L_MAIN_CHANGE_LANGUAGE, LanguageConstantValues.L_MAIN_CHANGE_LANGUAGE);
						// Login form will be showed, because there are more than one language.
						lResult = false;
					}
					else
					{
						// Hide Login and Change Password menu entries.
						this.mValidateAgent.Visible = false;

						// Login form will not be showed, because the unique agent is anonymous.
						// and there is only one language.
						lResult = true;

						// Assign the unique language supported.
						foreach (System.Globalization.CultureInfo cultureInfo in CultureManager.SupportedLanguages.Values)
						{
							CultureManager.Culture = cultureInfo;
						}
					}

					// Show anonymous agent alias.
					StringBuilder lStringBuilder = new StringBuilder();
					lStringBuilder.Append(CultureManager.TranslateString(lAgentInfo.IdXML + "_Alias", UtilFunctions.ProtectAmpersandChars(lAgentInfo.Alias)));
					toolStripStatusLabel.Text = lStringBuilder.ToString();
					// Show Current culture.
					toolStripStatusLabelCulture.Text = CultureManager.Culture.Name;

					// Hide Change Password menu entry.
					this.mChangePassword.Visible = false;
				}
				else
				{
					// Login form will be showed, because the agent is not anonymous.
					lResult = false;
				}
			}
			
			// Result return.
			return lResult;
		}

		#region Reports file
		private void LoadConfigurationReportsFile()
		{
			// Remove Report item controller and the report menu entries.
			if (mController.HatElementNodes.Exist("Report_0"))
			{
				mController.HatElementNodes.Remove(mController.HatElementNodes["Report_0"]);
				mnuReports.DropDownItems.Clear();
			}
			// Hide Reports menu option.
			mnuReports.Visible = false;

			HatNodeController lController = null;
			try
			{
				// Verify the path and file specified in MainMenuReports Settings.
				string lFilePath = Properties.Settings.Default.MainMenuReports;
				if (!System.IO.File.Exists(lFilePath))
				{
					lFilePath = System.Windows.Forms.Application.StartupPath + "\\" + lFilePath;
					if (!System.IO.File.Exists(lFilePath))
					{
						return;
					}
				}


				XmlDocument lXMLDoc = new XmlDocument();
				lXMLDoc.Load(lFilePath);

				XmlNodeList lMainNodeList = lXMLDoc.GetElementsByTagName("Reports");
				if (lMainNodeList.Count != 1)
				{
					return;
				}

				// Main Reports node.
				XmlNode lMainNode = lMainNodeList[0];

				// Get the Reports Menu information.
				char[] lSeparators = new char[] { ',' };
				string lAgentsWithoutBlanks = UtilFunctions.GetProtectedXmlNodeValue(Properties.Settings.Default.MainMenuReports, lMainNode, "agents");
				lAgentsWithoutBlanks = lAgentsWithoutBlanks.Replace(" ", string.Empty);
				string[] lAgents = lAgentsWithoutBlanks.Split(lSeparators, StringSplitOptions.RemoveEmptyEntries);
				if (lAgents.Length > 0 && !Logics.Logic.Agent.IsActiveFacet(lAgents))
				{
					return;
				}

				// No agent specified means all the agents of the application.
				lAgents = Logics.Agents.All;

				XmlNodeList lLanguageNodeList = lMainNode.SelectNodes("Language");
				bool lExistLanguage = false;
				string lAlias = "";
				foreach (XmlNode lNodeLanguage in lLanguageNodeList)
				{
					string lLanguageKey = UtilFunctions.GetProtectedXmlNodeValue(Properties.Settings.Default.MainMenuReports, lNodeLanguage, "key");					
					if (lLanguageKey.Length == 0 ||
						lLanguageKey.Equals(CultureManager.Culture.Name, StringComparison.InvariantCultureIgnoreCase))
					{
						lAlias = UtilFunctions.GetProtectedXmlNodeValue(Properties.Settings.Default.MainMenuReports, lNodeLanguage, "alias");
						lExistLanguage = true;
						break;
					}
				}

				// If report is not for current language, skip it.
				if (lExistLanguage)
				{
					lController = new HatNodeController("Report_0", lAlias, "", lAgents);

					// Assign the presentation to the controller.
					lController.Label = new ToolStripMenuItemPresentation(mnuReports, false);

					// Add the controller item to the Hat list.
					mController.HatElementNodes.Add(lController);

					// Get the sub menu items.
					AddReportMenuItems(lMainNode.ChildNodes, mnuReports);
				}
			}
			catch (Exception e)
			{
				Exception exc = new Exception(CultureManager.TranslateString(LanguageConstantKeys.L_ERROR_LOADING_REPORTSCONFIG, LanguageConstantValues.L_ERROR_LOADING_REPORTSCONFIG), e);
				ScenarioManager.LaunchErrorScenario(exc);
			}

			if (mnuReports.DropDownItems.Count > 0)
			{
				mnuReports.Visible = true;
			}
		}

		/// <summary>
		/// Add report menu items recursively, from the XML node.
		/// </summary>
		/// <param name="xmlNodeList">List of nodes that represent reports or submenu entries.</param>
		/// <param name="menuItem">ToolStripMenuItem object to be filled with nodes information.</param>
		private void AddReportMenuItems(XmlNodeList xmlNodeList, ToolStripMenuItem menuItem)
		{
			char[] lSeparators = new char[] { ',' };
			
			// Process each node: Report or SubMenu.
			foreach (XmlNode node in xmlNodeList)
			{
				try
				{
					if (node.Name.Equals("Report", StringComparison.InvariantCultureIgnoreCase))
					{
						#region Get Report information
						string lAgentsWithoutBlanks = UtilFunctions.GetProtectedXmlNodeValue(Properties.Settings.Default.MainMenuReports, node, "agents");
						lAgentsWithoutBlanks = lAgentsWithoutBlanks.Replace(" ", string.Empty);
						string[] lAgents = lAgentsWithoutBlanks.Split(lSeparators, StringSplitOptions.RemoveEmptyEntries);
						if (lAgents.Length > 0 && !Logics.Logic.Agent.IsActiveFacet(lAgents))
						{
							continue;
						}

						// Connected agent can see this report.
						lAgents = Logics.Agents.All;
						string lClassName = UtilFunctions.GetProtectedXmlNodeValue(Properties.Settings.Default.MainMenuReports, node, "class");
						string lFilterName = UtilFunctions.GetProtectedXmlNodeValue(Properties.Settings.Default.MainMenuReports, node, "filtername");
						string lDataSetFileName = UtilFunctions.GetProtectedXmlNodeValue(Properties.Settings.Default.MainMenuReports, node, "datasetfile");
						if (File.Exists(lDataSetFileName))
						{
							FileInfo lDataSetFileInfo = new FileInfo(lDataSetFileName);
							lDataSetFileName = lDataSetFileInfo.FullName;
						}

						#region Get report language information
						XmlNodeList lLanguageNodeList = node.SelectNodes("Language");
						bool lExistLanguage = false;
						string lAlias = "";
						string lTitle = "";
						string lReportFileName = "";
						foreach (XmlNode lNodeLanguage in lLanguageNodeList)
						{
							string lLanguageKey = UtilFunctions.GetProtectedXmlNodeValue(Properties.Settings.Default.MainMenuReports, lNodeLanguage, "key");
							if (lLanguageKey.Length == 0 ||
								lLanguageKey.Equals(CultureManager.Culture.Name, StringComparison.InvariantCultureIgnoreCase))
							{
								lAlias = UtilFunctions.GetProtectedXmlNodeValue(Properties.Settings.Default.MainMenuReports, lNodeLanguage, "alias");
								lTitle = UtilFunctions.GetProtectedXmlNodeValue(Properties.Settings.Default.MainMenuReports, lNodeLanguage, "title");
								lReportFileName = UtilFunctions.GetProtectedXmlNodeValue(Properties.Settings.Default.MainMenuReports, lNodeLanguage, "reportfilepath");
								lExistLanguage = true;
								break;
							}
						}
						#endregion Get report language information

						// If report is not for current language, skip it.
						if (!lExistLanguage)
							continue;
						
						if (File.Exists(lReportFileName))
						{
							FileInfo lReportFileInfo = new FileInfo(lReportFileName);
							lReportFileName = lReportFileInfo.FullName;
						}

						// Create the Menu Report Controller.
						HatLeafReportController lController = new HatLeafReportController("Report_" + mController.HatElementNodes.Count.ToString(), lClassName, lFilterName, lAlias, lDataSetFileName, lReportFileName, lTitle, lAgents);

						// Add the option to the menu.
						ToolStripMenuItem lItem = new ToolStripMenuItem(lController.Alias);
						menuItem.DropDownItems.Add(lItem);
						// Assign the presentation to the controller.
						lController.Trigger = new ToolStripMenuItemPresentation(lItem, true);
						// Add the controller item to the Hat list.
						mController.HatElementNodes.Add(lController);
						#endregion Get Report information
					}

					if (node.Name.Equals("SubMenu", StringComparison.InvariantCultureIgnoreCase))
					{
						#region Get SubMenu information
						string lAgentsWithoutBlanks = UtilFunctions.GetProtectedXmlNodeValue(Properties.Settings.Default.MainMenuReports, node, "agents");
						lAgentsWithoutBlanks = lAgentsWithoutBlanks.Replace(" ", string.Empty);
						string[] lAgents = lAgentsWithoutBlanks.Split(lSeparators, StringSplitOptions.RemoveEmptyEntries);
						if (lAgents.Length > 0 && !Logics.Logic.Agent.IsActiveFacet(lAgents))
						{
							continue;
						}

						// Connected agent can see this report.
						lAgents = Logics.Agents.All;

						#region Get submenu language information
						XmlNodeList lLanguageNodeList = node.SelectNodes("Language");
						bool lExistLanguage = false;
						string lAlias = "";
						foreach (XmlNode lNodeLanguage in lLanguageNodeList)
						{
							string lLanguageKey = UtilFunctions.GetProtectedXmlNodeValue(Properties.Settings.Default.MainMenuReports, lNodeLanguage, "key");
							if (lLanguageKey.Length == 0 ||
								lLanguageKey.Equals(CultureManager.Culture.Name, StringComparison.InvariantCultureIgnoreCase))
							{
								lAlias = UtilFunctions.GetProtectedXmlNodeValue(Properties.Settings.Default.MainMenuReports, lNodeLanguage, "alias");
								lExistLanguage = true;
								break;
							}
						}
						#endregion Get submenu language information

						// If report is not for current language, skip it.
						if (!lExistLanguage)
							continue;

						// Create the Sub Menu Controller.
						HatNodeController lController = new HatNodeController("Report_" + mController.HatElementNodes.Count.ToString(), lAlias, "", lAgents);

						// Add the option to the menu.
						ToolStripMenuItem lItem = new ToolStripMenuItem(lController.Alias);
						menuItem.DropDownItems.Add(lItem);
						// Add the controller item to the Hat list.
						mController.HatElementNodes.Add(lController);

						// Add the sub elements of this sub menu.
						if (node.ChildNodes.Count > 0)
						{
							AddReportMenuItems(node.ChildNodes, lItem);
						}
						#endregion Get SubMenu information
					}
				}
				catch (Exception e)
				{
					Exception exc = new Exception(CultureManager.TranslateString(LanguageConstantKeys.L_ERROR_LOADING_REPORTSCONFIG, LanguageConstantValues.L_ERROR_LOADING_REPORTSCONFIG), e);
					ScenarioManager.LaunchErrorScenario(exc);
				}
			}
		}
		#endregion Reports file

		#endregion Methods
	}
}
