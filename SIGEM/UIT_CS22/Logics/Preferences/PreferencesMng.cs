// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using System.Collections.Specialized;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Logics.Preferences
{
	/// <summary>
	/// Manages the User Preferences.
	/// Contains a list with the current user preferences and offers the Get and Save functionality
	/// </summary>
	public class PreferencesMng
	{

		#region Members
		/// <summary>
		/// Cache Scenario Preferences list
		/// </summary>
		private List<IScenarioPrefs> mScenarioPreferences = new List<IScenarioPrefs>();
		private string mUserPreferencesFilePath = string.Empty;
		#endregion Members

		#region Properties
		/// <summary>
		/// Path of Preferences file
		/// </summary>
		public string UserPreferencesFilePath
		{
			get
			{
				return mUserPreferencesFilePath;
			}
		}
		#endregion Properties
		
		#region Constructor
		public PreferencesMng()
		{
		}
		#endregion Constructor

		#region Get element
		/// <summary>
		/// Returns the User preferences for the specified scenario
		/// </summary>
		/// <param name="scenarioName">Scenario name to be found</param>
		/// <returns></returns>
		public IScenarioPrefs GetScenarioPrefs(string scenarioName)
		{
			if (scenarioName == "" || Properties.Settings.Default.ConnectionStringUserPreferences == "")
				return null;

			if (mScenarioPreferences.Count == 0)
			{
				ReadPreferencesFromFile();
			}

			foreach (IScenarioPrefs scenario in mScenarioPreferences)
			{
				if (scenario.Name == scenarioName)
					return scenario;
			}

			return null;
		}
		#endregion Get element

		#region Save

		#region Save Form info
		/// <summary>
		/// Save the info related with one Form
		/// </summary>
		/// <param name="scenarioName">Form name</param>
		/// <param name="posX">Position X</param>
		/// <param name="posY">Position Y</param>
		/// <param name="width">Form width</param>
		/// <param name="height">Form height</param>
		/// <param name="properties">Extra graphical information list</param>
		public void SaveFormInfo(string scenarioName, int posX, int posY, int width, int height, StringDictionary properties)
		{
			// In the scenario is in the cache, update it
			FormPrefs formPrefs = GetScenarioPrefs(scenarioName) as FormPrefs;
			if (formPrefs == null)
			{
				// Population Scenario doesn't exist in the cache. Create a new one
				formPrefs = new FormPrefs(scenarioName);
				mScenarioPreferences.Add(formPrefs);
			}
			formPrefs.PosX = posX;
			formPrefs.PosY = posY;
			formPrefs.Width = width;
			formPrefs.Height = height;
			formPrefs.Properties = properties;

			// Save in the Preferences Server
			SaveScenario(formPrefs);
		}
		#endregion Save Form info

		#region Save Instance info
		/// <summary>
		/// Save the preferences for one Instance Interaction Unit
		/// </summary>
		/// <param name="scenarioName">PIU Name</param>
		/// <param name="selectedDisplaSet">Selected DisplaySet name</param>
		/// <param name="displaySets">Customized DisplaySet list</param>
		public void SaveInstanceInfo(string scenarioName, string selectedDisplaSet, List<DisplaySetInformation> displaySets)
		{
			// In the scenario is in the cache, update it
			InstancePrefs insScenario = GetScenarioPrefs(scenarioName) as InstancePrefs;
			if (insScenario != null)
			{
				insScenario.DisplaySets.RemoveRange(0, insScenario.DisplaySets.Count);
			}
			else
			{
				// Instance Scenario doesn't exist in the cache. Create a new one
				insScenario = new InstancePrefs(scenarioName);
				mScenarioPreferences.Add(insScenario);
			}
			insScenario.SelectedDisplaySetName = selectedDisplaSet;
			// Add the DisplaySet to the scenario
			foreach (DisplaySetInformation displaySet in displaySets)
			{
				if (displaySet.Custom)
				{
					insScenario.DisplaySets.Add(displaySet);
				}
			}

			// Save in the Preferences Server
			SaveScenario(insScenario);
		}
		#endregion Save Instance info

		#region Save Population info
		/// <summary>
		/// Save the preferences for one Population Interaction Unit
		/// </summary>
		/// <param name="scenarioName">PIU Name</param>
		/// <param name="blockSize">Block size</param>
		/// <param name="selectedDisplaSet">Selected DisplaySet name</param>
		/// <param name="displaySets">Customized DisplaySet list</param>
		public void SavePopulationInfo(string scenarioName, int blockSize, string selectedDisplaSet, List<DisplaySetInformation> displaySets)
		{
			// In the scenario is in the cache, update it
			PopulationPrefs popScenario = GetScenarioPrefs(scenarioName) as PopulationPrefs;
			if (popScenario != null)
			{
				popScenario.DisplaySets.RemoveRange(0, popScenario.DisplaySets.Count);
			}
			else
			{
				// Population Scenario doesn't exist in the cache. Create a new one
				popScenario = new PopulationPrefs(scenarioName,blockSize);
				mScenarioPreferences.Insert(0, popScenario);
			}
			popScenario.SelectedDisplaySetName = selectedDisplaSet;
			// Add the DisplaySet to the scenario
			foreach (DisplaySetInformation displaySet in displaySets)
			{
				if (displaySet.Custom)
				{
					popScenario.DisplaySets.Add(displaySet);
				}
			}

			// Save in the Preferences Server
			SaveScenario(popScenario);
		}
		#endregion Save Population info

		#region Save Scenario
		/// <summary>
		/// Save the scenario info in the Preferences server
		/// </summary>
		/// <param name="popScenario"></param>
		private void SaveScenario(IScenarioPrefs scenarioPrefs)
		{
			// Preferences must be active
			if (Properties.Settings.Default.ConnectionStringUserPreferences == "")
				return;

			SavePreferencesToFile();
		}

		#endregion Save Scenario

		#endregion Save

		#region Preferences File Management

		#region Read Preferences From File
		/// <summary>
		/// Reads the user preferences from a existing XML file
		/// </summary>
		private void ReadPreferencesFromFile()
		{

			try
			{
				string fileName = GetPreferencesFileName(true);
				if (fileName == "")
					return;

                XmlDocument lXMLDocument = new XmlDocument();
                lXMLDocument.Load(fileName);

                // Get main node
                XmlNodeList lMainNodeList = lXMLDocument.GetElementsByTagName("Preferences");
                if (lMainNodeList.Count != 1)
                {
                    return;
                }

                // Preferences node.
                XmlNode lPreferencesNode = lMainNodeList[0];
                // Get all Scenario nodes
                XmlNodeList lScenarioNodes = lPreferencesNode.SelectNodes("Scenario");

                IScenarioPrefs pref = null;
                foreach (XmlNode lScenarioNode in lScenarioNodes)
                {
                    pref = CreatePreference(lScenarioNode);
                    if (pref != null)
                    {
                        mScenarioPreferences.Add(pref);
                    }
                }
			}
			catch
			{
			}
		}
		#endregion Read Preferences From File

		#region Save to File
		/// <summary>
		/// Save the user preferences in a file
		/// </summary>
		private void SavePreferencesToFile()
		{
			try
			{
				string fileName = GetPreferencesFileName(false);

				FileStream fileStream = new FileStream(fileName, FileMode.Create);

				XmlWriterSettings lWriterSettings = new XmlWriterSettings();
				lWriterSettings.Indent = true;
				lWriterSettings.CheckCharacters = true;
				lWriterSettings.NewLineOnAttributes = true;
				lWriterSettings.Encoding = Encoding.UTF8;

				XmlWriter writer = XmlWriter.Create(fileStream, lWriterSettings);

				writer.WriteStartDocument();

				writer.WriteStartElement("Preferences");

				foreach (IScenarioPrefs scenario in mScenarioPreferences)
				{
					scenario.Serialize(writer);
				}

				writer.WriteEndElement();

				writer.WriteEndDocument();
				writer.Flush();

				fileStream.Flush();
				fileStream.Close();
			}
			catch
			{
			}
		}
		#endregion Save to File

		#endregion Preferences File Management

		#region Create Preference
		private IScenarioPrefs CreatePreference(XmlNode scenarioNode)
		{
            if (scenarioNode.ChildNodes.Count == 0)
                return null;

            string type = scenarioNode.Attributes["Type"].Value;
            string version = scenarioNode.Attributes["Version"].Value;
            string name = scenarioNode.Attributes["Name"].Value;

			IScenarioPrefs pref = null;

			switch (type)
			{
				case "INS":
					{
						pref = new InstancePrefs(name);
                        pref.Deserialize(scenarioNode.ChildNodes[0], version);
						break;
					}
				case "POP":
					{
						pref = new PopulationPrefs(name, 0);
                        pref.Deserialize(scenarioNode.ChildNodes[0], version);
						break;
					}
				case "FORM":
					{
						pref = new FormPrefs(name);
                        pref.Deserialize(scenarioNode.ChildNodes[0], version);
						break;
					}
			}

			return pref;
		}
		#endregion Create Preference

		#region Auxiliar functions
		/// <summary>
		/// Returns the User Id to be used to access the preferences.
		/// </summary>
		/// <returns>String composed by the agent class name followed by the identification fields values.</returns>
		private string GetUserId()
		{
			string lUserId = Logic.Agent.ClassName;
			if (!(Logic.Agent is Oids.AnonymousAgentInfo))
			{
				foreach(KeyValuePair<ModelType, object> lField in Logic.Agent.GetFields())
				{
					switch (lField.Key)
					{
						case ModelType.Date:
						{
							lUserId += "_" + ((DateTime)lField.Value).ToString("yyyyMMdd");
							break;
						}
						case ModelType.DateTime:
						{
							lUserId += "_" + ((DateTime)lField.Value).ToString("yyyyMMddhhmmss");
							break;
						}
						case ModelType.Time:
						{
							lUserId += "_" + ((DateTime)lField.Value).ToString("hhmmss");
							break;
						}
						default:
						{
							lUserId += "_" + lField.Value.ToString();
							break;
						}
					}
				}
			}
			return lUserId;
		}
		/// <summary>
		/// Returns the file name to read and save the User Preferences.
		/// </summary>
		/// <param name="verify">True in order to verify that the file exist.</param>
		/// <returns>A string with the User Preferences file name.</returns>
		private string GetPreferencesFileName(bool verify)
		{

			if (mUserPreferencesFilePath != string.Empty)
			{
				return mUserPreferencesFilePath;
			}

			try
			{
				// Extracts the name from the the Property 'ConnectionStringUserPreferences'.
				string nameOfFile = Properties.Settings.Default.ConnectionStringUserPreferences.Substring(7);

				if (Properties.Settings.Default.UserPreferencesByUser.Equals(true))
				{
					// If 'UserPreferencesByUser' are enabled, the user preferences file will require the user id. sufix.
					string lExtension = Path.GetExtension(nameOfFile);
					nameOfFile = nameOfFile.Substring(0, nameOfFile.Length - lExtension.Length) + "_" + GetUserId() + lExtension;
				}

				mUserPreferencesFilePath = string.Empty;

				if (Path.IsPathRooted(nameOfFile))
				{
					mUserPreferencesFilePath = nameOfFile;
				}
				else
				{
					mUserPreferencesFilePath = Path.GetFullPath(System.Windows.Forms.Application.StartupPath + "\\" + nameOfFile);
				}

				if (!verify)
				{
					return mUserPreferencesFilePath;
				}

				if (!File.Exists(mUserPreferencesFilePath))
				{
					mUserPreferencesFilePath = "";
				}
			}
			catch{}

			return mUserPreferencesFilePath;

		}
		#endregion Auxiliar functions
	}
}

