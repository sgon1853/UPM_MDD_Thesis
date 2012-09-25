// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Logics.Preferences
{
	public class InstancePrefs : IScenarioPrefs
	{
		#region Members
		/// <summary>
		/// Scenario name
		/// </summary>
		private string mName = "";
		/// <summary>
		/// Customized DisplaySet list
		/// </summary>
		private List<DisplaySetInformation> mDisplaySets = new List<DisplaySetInformation>();
		/// <summary>
		/// Selected DisplaySet Name
		/// </summary>
		private string mSelectedDisplaySetName = "";
		#endregion Members

		#region Properties
		public string Name
		{
			get
			{
				return mName;
			}
			set
			{
				mName = value;
			}
		}
		public List<DisplaySetInformation> DisplaySets
		{
			get
			{
				return mDisplaySets;
			}
		}
		public string SelectedDisplaySetName
		{
			get
			{
				return mSelectedDisplaySetName;
			}
			set
			{
				mSelectedDisplaySetName = value;
			}
		}
		#endregion Properties

		#region Constructor
		public InstancePrefs(string name)
		{
			mName = name;
		}
		#endregion Constructor

		#region To XML
		/// <summary>
		/// Serialize the data in XML to be saved in the Preferences Server
		/// </summary>
		/// <returns>XML string with all the info about the scenario preferences</returns>
		public virtual string ToXML()
		{
			MemoryStream memoryStream = new MemoryStream();

			XmlWriterSettings lWriterSettings = new XmlWriterSettings();
			lWriterSettings.Indent = true;
			lWriterSettings.CheckCharacters = true;
			lWriterSettings.NewLineOnAttributes = true;
			lWriterSettings.Encoding = Encoding.UTF8;

			XmlWriter writer = XmlWriter.Create(memoryStream, lWriterSettings);

			writer.WriteStartDocument();

			Serialize(writer);

			writer.WriteEndDocument();
			writer.Flush();
			memoryStream.Flush();
			memoryStream.Position = 0;

			StringBuilder stringBuilder = new StringBuilder();
			StreamReader lReader = new StreamReader(memoryStream);
			stringBuilder.Append(lReader.ReadToEnd());

			return stringBuilder.ToString();
		}
		#endregion To XML

		#region Serialize
		/// <summary>
		/// Serrialize to XML
		/// </summary>
		/// <param name="writer"></param>
		public virtual void Serialize(XmlWriter writer)
		{

			// Add the info to the Writer
			writer.WriteStartElement("Scenario");
			writer.WriteAttributeString("Type", "INS");
			writer.WriteAttributeString("Version", "1");
			writer.WriteAttributeString("Name", Name);

			SerializeInstanceElement(writer);

			// Scenario
			writer.WriteEndElement();
		}

		protected void SerializeInstanceElement(XmlWriter writer)
		{
			writer.WriteStartElement("InsScenario");
			writer.WriteAttributeString("SelectedDS", SelectedDisplaySetName);

			SerializeDisplaySets(writer);

			// InsScenario
			writer.WriteEndElement();
		}

		/// <summary>
		/// Serialize DisplaySets
		/// </summary>
		/// <param name="writer"></param>
		private void SerializeDisplaySets(XmlWriter writer)
		{
			foreach (DisplaySetInformation displaySet in DisplaySets)
			{
				displaySet.Serialize(writer);
			}
		}
		#endregion Serialize

		#region Deserialize
        /// <summary>
        /// Deserialize from XML node
        /// </summary>
        /// <param name="insNode"></param>
        /// <param name="version"></param>
		public virtual void Deserialize(XmlNode insNode, string version)
		{

            SelectedDisplaySetName = insNode.Attributes["SelectedDS"].Value;

            // Get all Customized Display Sets
            XmlNodeList lDisplaySetNodes = insNode.SelectNodes("DisplaySet");
            foreach (XmlNode lDisplaySetNode in lDisplaySetNodes)
            {
                DisplaySetInformation lDisplaySet = new DisplaySetInformation("");
                lDisplaySet.Deserialize(lDisplaySetNode, version);
                DisplaySets.Add(lDisplaySet);
            }
		}
		#endregion Deserialize
	}
}


