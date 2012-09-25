// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace SIGEM.Client.Logics.Preferences
{
    public class PopulationPrefs : InstancePrefs
    {
        #region Members
        private int mBlockSize;
        #endregion Members

        #region Properties
        public int BlockSize
        {
            get
            {
                return mBlockSize;
            }
            set
            {
                mBlockSize = value;
            }
        }
        #endregion Properties

        #region Constructor
        public PopulationPrefs(string name, int blockSize)
            :base(name)
        {
            mBlockSize = blockSize;
        }
        #endregion Constructor

        #region To XML
        /// <summary>
        /// Serialize the data in XML to be saved in the Preferences Server
        /// </summary>
        /// <returns>XML string with all the info about the scenario preferences</returns>
        public override string ToXML()
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
        public override void Serialize(XmlWriter writer)
        {
            // Add the info to the Writer
            writer.WriteStartElement("Scenario");
            writer.WriteAttributeString("Type", "POP");
            writer.WriteAttributeString("Version", "1");
            writer.WriteAttributeString("Name", Name);


            writer.WriteStartElement("PopScenario");
            writer.WriteAttributeString("BlockSize", BlockSize.ToString());

            SerializeInstanceElement(writer);

            // PopScenario
            writer.WriteEndElement();

            // Scenario
            writer.WriteEndElement();
        }
        #endregion Serialize

        #region Deserialize
        /// <summary>
        /// Deserialize from XML node
        /// </summary>
        /// <param name="popNode"></param>
        /// <param name="version">Data version</param>
        public override void Deserialize(XmlNode popNode, string version)
        {
            BlockSize = int.Parse(popNode.Attributes["BlockSize"].Value);
            base.Deserialize(popNode.ChildNodes[0], version);
        }
        #endregion Deserialize
    }
}


