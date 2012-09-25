// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace SIGEM.Client.Adaptor.Serializer
{
	internal static class XMLStatisticsSerializer
	{
		#region Serialize Statistics.
		public static XmlWriter Serialize(XmlWriter writer, Statistics statistics)
		{
			writer.WriteStartElement(DTD.TagRequest);

			writer.WriteAttributeString("StartTime", statistics.StartTime);
			writer.WriteAttributeString("EndTime", statistics.EndTime);
			writer.WriteAttributeString("ElapsedTime", statistics.ElapsedTime);

			writer.WriteEndElement();
			return writer;
		}
		#endregion Serialize Statistics.

		#region Deserialize Request.

		#region over load Methods.
		public static Statistics Deserialize(string xmlResponse)
		{
			return Deserialize(xmlResponse, null);
		}
		public static Statistics Deserialize(string xmlString, Statistics statistics)
		{
			return Deserialize(new XmlTextReader(new StringReader(xmlString)), statistics);
		}
		public static Statistics Deserialize(XmlReader reader)
		{
			return Deserialize(reader, null);
		}
		#endregion over load Methods.

		public static Statistics Deserialize(XmlReader reader, Statistics statistics)
		{
			if (reader.IsStartElement("Statistics"))
			{
				if (statistics == null)
				{
					statistics = new Statistics();
				}

				statistics.StartTime = reader.GetAttribute("StartTime");
				statistics.EndTime = reader.GetAttribute("EndTime");
				statistics.ElapsedTime = reader.GetAttribute("ElapsedTime");

				reader.Skip();
			}
			else
			{
				throw new ArgumentException("Xml Reader don't have the Statitics in Start Element.", "XmlReader reader");
			}
			return statistics;
		}
		#endregion Deserialize Request.
	}
}

