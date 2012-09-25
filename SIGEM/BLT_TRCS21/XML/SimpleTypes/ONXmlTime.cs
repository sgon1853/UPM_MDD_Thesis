// 3.4.4.5

using System;
using System.Xml;
using SIGEM.Business.Types;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business.XML
{
	/// <summary>
	/// Basic Type ONXmlTime
	/// </summary>
	public class ONXmlTime
	{
		#region Read / Write XML
		/// <summary>
		/// Extracts from the XML information and create components that the system understands.
		/// </summary>
		/// <param name="xmlReader">Object that has the message XML to be treated</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		/// <param name="xmlElement">Element of the XML that is checked</param>
		public static ONTime XML2ON(XmlReader xmlReader, double dtdVersion, string xmlElement)
		{
			if (xmlReader.IsStartElement(ONXml.XMLTAG_NULL))
			{
				xmlReader.Skip();
				return ONTime.Null;
			}

			if (!xmlReader.IsStartElement(xmlElement) )
				throw new ONXMLStructureException(null, xmlElement);
			
			string lValue;
			try
			{
				lValue = xmlReader.ReadElementString();
			}
			catch( Exception e)
			{
				throw new ONXMLFormatException(e, "Time");	
			}
	
			return UnPack(lValue, dtdVersion);
		}
		/// <summary>
		/// Creates XML elements from the data of the system.
		/// </summary>
		/// <param name="xmlWriter">Object with the XML message to add new information and return to client side</param>
		/// <param name="val">Value to be puted inside the XML message</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		/// <param name="xmlElement">Element of the XML that is checked</param>
		public static void ON2XML(XmlWriter xmlWriter, ONTime val, double dtdVersion, string xmlElement)
		{
			if (val == null)
			{
				if (xmlElement == ONXml.XMLTAG_V)
					xmlWriter.WriteElementString(xmlElement, "");
				else
					xmlWriter.WriteElementString(ONXml.XMLTAG_NULL, null);
			}
			else
			{
				xmlWriter.WriteStartElement(xmlElement);
				if (xmlElement == ONXml.XMLTAG_OIDFIELD && dtdVersion > 2.0)
					xmlWriter.WriteAttributeString("Type", "text");

				if (val.TypedValue.Millisecond == 0)
					xmlWriter.WriteString(val.TypedValue.ToString("HH:mm:ss"));
				else
					xmlWriter.WriteString(val.TypedValue.ToString("HH:mm:ss.fff"));

				xmlWriter.WriteEndElement();
			}
		}
		#endregion

		#region Pack / UnPack
		/// <summary>
		/// Checks the format of the data included in the XML message
		/// </summary>
		/// <param name="data">Contains the data to be checked</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		public static ONTime UnPack(string data, double dtdVersion)
		{
			data = data.Trim();
			if (data == "")
				throw new ONXMLFormatException(null, "Time");

			if (dtdVersion < 2.0) // Apply the locale format
				return new ONTime(data);
			else
			{
				try
				{
					int lSeparator = data.IndexOf("T", 0);
					if (lSeparator < 0)
						lSeparator = data.IndexOf("t", 0);

					//Separator position between hour and minute
					int lFirst = data.IndexOf(":", 0);

					//Separator position between minute and second
					int lSecond = data.IndexOf(":", lFirst + 1);

					//Separator position between second and millisecond
					int lThird = data.IndexOf(".", lSecond + 1);

					// Time zone delimitator
					int lFourth = data.IndexOf("Z", 0);
					if (lFourth < 0)
						lFourth = data.IndexOf("z", 0);
					if (lFourth < 0)
						lFourth = data.IndexOf("+", 0);
					if (lFourth < 0)
						lFourth = data.IndexOf("-", 0);

					int lMiliSec, lSeconds, lMinutes;
					lMiliSec = 0;
					lSeconds = 0;

					if (lThird > -1)
					{
						//There are miliseconds
						lSeconds = Convert.ToInt16(data.Substring(lSecond + 1, lThird - lSecond - 1));
						lMinutes = Convert.ToInt16(data.Substring(lFirst + 1, lSecond - lFirst - 1));
						if (lFourth > 0)
							lMiliSec = Convert.ToInt16(data.Substring(lThird + 1, lFourth - lThird - 1));
						else
							lMiliSec = Convert.ToInt16(data.Substring(lThird + 1));
					}
					else if (lSecond > 0)
					{
						//There are seconds but there are not miliseconds
						lMinutes = Convert.ToInt16(data.Substring(lFirst + 1, lSecond - lFirst - 1));
						if (lFourth > 0)
							lSeconds = Convert.ToInt16(data.Substring(lSecond + 1, lFourth - lSecond - 1));
						else
							lSeconds = Convert.ToInt16(data.Substring(lSecond + 1));
					}
					else
					{
						//There are only hours and minutes
						if (lFourth > 0)
							lMinutes = Convert.ToInt16(data.Substring(lFirst + 1, lFourth - lFirst - 1));
						else
							lMinutes = Convert.ToInt16(data.Substring(lFirst + 1));
					}

					DateTime lTime = new DateTime(
						1970,
						1,
						1,
						Convert.ToInt16(data.Substring(lSeparator + 1, lFirst - lSeparator - 1)),
						lMinutes,
						lSeconds,
						lMiliSec);

					return new ONTime(lTime);
				}
				catch (Exception e)
				{
					throw new ONXMLFormatException(e, "Time");
				}
			}
		}
		#endregion
	}
}

