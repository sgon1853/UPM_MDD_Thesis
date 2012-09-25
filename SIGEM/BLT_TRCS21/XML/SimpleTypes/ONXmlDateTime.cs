// 3.4.4.5
using System;
using System.Xml;
using SIGEM.Business.Types;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business.XML
{
	/// <summary>
	/// Basic Type ONXmlDateTime
	/// </summary>
	public class ONXmlDateTime
	{
		#region Read / Write XML
		/// <summary>
		/// Extracts from the XML information and create components that the system understands.
		/// </summary>
		/// <param name="xmlReader">Object that has the message XML to be treated</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		/// <param name="xmlElement">Element of the XML that is checked</param>
		public static ONDateTime XML2ON(XmlReader xmlReader, double dtdVersion, string xmlElement)
		{
			if (xmlReader.IsStartElement(ONXml.XMLTAG_NULL))
			{
				xmlReader.Skip();
				return ONDateTime.Null;
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
				throw new ONXMLFormatException(e, "DateTime");	
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
		public static void ON2XML(XmlWriter xmlWriter, ONDateTime val, double dtdVersion, string xmlElement)
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
				if (dtdVersion < 2.0) // Apply the locale format
					xmlWriter.WriteElementString(xmlElement, val.TypedValue.ToString());
				else
				{
					xmlWriter.WriteStartElement(xmlElement);
					if (xmlElement == ONXml.XMLTAG_OIDFIELD && dtdVersion > 2.0)
						xmlWriter.WriteAttributeString("Type", "datetime");

					if (val.TypedValue.Millisecond == 0)
						xmlWriter.WriteString(val.TypedValue.ToString("yyyy-MM-ddTHH:mm:ss"));
					else
						xmlWriter.WriteString(val.TypedValue.ToString("yyyy-MM-ddTHH:mm:ss.fff"));
					xmlWriter.WriteEndElement();
				}
			}
		}
		#endregion

		#region Pack / UnPack
		/// <summary>
		/// Checks the format of the data included in the XML message
		/// </summary>
		/// <param name="data">Contains the data to be checked</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		public static ONDateTime UnPack(string data, double dtdVersion)
		{
			data = data.Trim();
			if (data == "")
				throw new ONXMLFormatException(null, "DateTime");

			if (dtdVersion < 2.0) // Apply the locale format
				return new ONDateTime(data);
			else
			{
				try
				{
					data = data.Trim();
					int lFirst, lSecond, lThird, lFourth, lFifth, lSixth, lSeventh;

					//Separator position between year and month
					lFirst = data.IndexOf("-", 1);

					//Separator position between month and day
					lSecond = data.IndexOf("-", lFirst + 1);

					//Separator position between date and time
					lThird = data.IndexOf("T", lSecond + 1);
					if (lThird < 0)
						lThird = data.IndexOf("t", lSecond + 1);

					//Separator position between hour and minute
					lFourth = data.IndexOf(":", lThird + 1);

					//Separator position between minute and second
					lFifth = data.IndexOf(":", lFourth + 1);

					//Separator position between second and millisecond
					lSixth = data.IndexOf(".", lFifth + 1);

					// Time zone delimitator
					lSeventh = data.IndexOf("Z", 0);
					if (lSeventh < 0)
						lSeventh = data.IndexOf("z", 0);
					if (lSeventh < 0)
						lSeventh = data.IndexOf("+", 0);
					if (lSeventh < 0)
						lSeventh = data.IndexOf("-", lFourth);

					int lMiliSec, lSeconds, lMinutes;

					if (lSixth > -1)
					{
						//There are milliseconds
						lMinutes = Convert.ToInt16(data.Substring(lFourth + 1, lFifth - lFourth - 1));
						if (lSeventh > -1)
						{
							lSeconds = Convert.ToInt16(data.Substring(lFifth + 1, lSixth - lFifth - 1));
							lMiliSec = Convert.ToInt16(data.Substring(lSixth + 1, lSeventh - lSixth - 1));
						}
						else
						{
							lSeconds = Convert.ToInt16(data.Substring(lFifth + 1, lSixth - lFifth - 1));
							lMiliSec = Convert.ToInt16(data.Substring(lSixth + 1));
						}
					}
					else if (lFifth > -1)
					{
						//There are seconds but there are not milliseconds
						lMinutes = Convert.ToInt16(data.Substring(lFourth + 1, lFifth - lFourth - 1));
						lMiliSec = 0;
						if (lSeventh > -1)
							lSeconds = Convert.ToInt16(data.Substring(lFifth + 1, lSeventh - lFifth - 1));
						else
							lSeconds = Convert.ToInt16(data.Substring(lFifth + 1));
					}
					else
					{
						//There are only hour and minute
						lMiliSec = 0;
						lSeconds = 0;
						if (lSeventh > -1)
							lMinutes = Convert.ToInt16(data.Substring(lFourth + 1, lSeventh - lFourth - 1));
						else
							lMinutes = Convert.ToInt16(data.Substring(lFourth + 1));
					}

					DateTime lDateTime = new DateTime(System.Convert.ToInt16(data.Substring(0, lFirst)),
						Convert.ToInt16(data.Substring(lFirst + 1, lSecond - lFirst - 1)),
						Convert.ToInt16(data.Substring(lSecond + 1, lThird - lSecond - 1)),
						Convert.ToInt16(data.Substring(lThird + 1, lFourth - lThird - 1)),
						lMinutes,
						lSeconds, lMiliSec);

					return new ONDateTime(lDateTime);
				}
				catch (Exception e)
				{
					throw new ONXMLFormatException(e, "DateTime");
				}
			}
		}
		#endregion
	}
}

