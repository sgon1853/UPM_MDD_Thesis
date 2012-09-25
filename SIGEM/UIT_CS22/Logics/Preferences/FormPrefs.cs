// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections.Specialized;

namespace SIGEM.Client.Logics.Preferences
{
	public class FormPrefs : IScenarioPrefs
	{
		#region Members
		/// <summary>
		/// Scenario name
		/// </summary>
		private string mName = "";
		private int mPosX = 0;
		private int mPosY = 0;
		private int mWidth = 0;
		private int mHeight = 0;
		private StringDictionary mProperties = new StringDictionary();
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
		public int PosX
		{
			get
			{
				return mPosX;
			}
			set
			{
				mPosX = value;
			}
		}
		public int PosY
		{
			get
			{
				return mPosY;
			}
			set
			{
				mPosY = value;
			}
		}
		public int Width
		{
			get
			{
				return mWidth;
			}
			set
			{
				mWidth = value;
			}
		}
		public int Height
		{
			get
			{
				return mHeight;
			}
			set
			{
				mHeight = value;
			}
		}
		public StringDictionary Properties
		{
			get
			{
				return mProperties;
			}
			set
			{
				mProperties = value;
			}
		}
		#endregion Properties

		#region Constructor
		public FormPrefs(string name)
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
		/// Serialize to XML
		/// </summary>
		/// <param name="writer"></param>
		public virtual void Serialize(XmlWriter writer)
		{

			writer.WriteStartElement("Scenario");
			writer.WriteAttributeString("Type", "FORM");
			writer.WriteAttributeString("Version", "1");
			writer.WriteAttributeString("Name", Name);

			writer.WriteStartElement("Form");
			
			writer.WriteAttributeString("X", PosX.ToString());
			writer.WriteAttributeString("Y", PosY.ToString());
			writer.WriteAttributeString("W", Width.ToString());
			writer.WriteAttributeString("H", Height.ToString());

			foreach (string key in Properties.Keys)
			{
				writer.WriteStartElement("Property");
				writer.WriteAttributeString("Name", key);
				writer.WriteValue(Properties[key]);
				writer.WriteEndElement();
			}

			// Form
			writer.WriteEndElement();

			// Scenario
			writer.WriteEndElement();
			
		}
		#endregion Serialize

		#region Deserialize
        /// <summary>
        /// Deserialize from XML node
        /// </summary>
        /// <param name="formNode"></param>
        /// <param name="version"></param>
		public virtual void Deserialize(XmlNode formNode, string version)
		{

			PosX = int.Parse(formNode.Attributes["X"].Value);
            PosY = int.Parse(formNode.Attributes["Y"].Value);
            Width = int.Parse(formNode.Attributes["W"].Value);
            Height = int.Parse(formNode.Attributes["H"].Value);

			string key = "";
			string value = "";

            XmlNodeList lPropertyNodes = formNode.SelectNodes("Property");

            foreach (XmlNode lProperty in lPropertyNodes)
            {
                key = lProperty.Attributes["Name"].Value;
                value = lProperty.InnerText;
                mProperties.Add(key, value);
            }
		}
		#endregion Deserialize
	}
}

