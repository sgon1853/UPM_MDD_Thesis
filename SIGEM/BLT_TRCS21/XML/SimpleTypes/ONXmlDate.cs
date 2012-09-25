// 3.4.4.5

using System;
using System.Xml;
using SIGEM.Business.Types;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business.XML
{
	/// <summary>
	/// Basic Type ONXmlDate
	/// </summary>
	public class ONXmlDate
	{
		#region Read / Write XML
		/// <summary>
		/// Extracts from the XML information and create components that the system understands.
		/// </summary>
		/// <param name="xmlReader">Object that has the message XML to be treated</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		/// <param name="xmlElement">Element of the XML that is checked</param>
		public static ONDate XML2ON(XmlReader xmlReader, double dtdVersion, string xmlElement)
		{
			if (xmlReader.IsStartElement(ONXml.XMLTAG_NULL))
			{
				xmlReader.Skip();
				return ONDate.Null;
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
				throw new ONXMLFormatException(e, "Date");	
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
		public static void ON2XML(XmlWriter xmlWriter, ONDate val, double dtdVersion, string xmlElement)
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
				{
					if ((val.TypedValue.Hour == 0) && (val.TypedValue.Minute == 0) && (val.TypedValue.Second == 0))
						xmlWriter.WriteElementString(xmlElement, val.TypedValue.ToString("d"));
					else
						xmlWriter.WriteElementString(xmlElement, val.TypedValue.ToString());
				}
				else
				{
					xmlWriter.WriteStartElement(xmlElement);
					if (xmlElement == ONXml.XMLTAG_OIDFIELD && dtdVersion > 2.0)
						xmlWriter.WriteAttributeString("Type", "date");

					xmlWriter.WriteString(val.TypedValue.ToString("yyyy-MM-dd"));
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
		public static ONDate UnPack(string data, double dtdVersion)
		{
			data = data.Trim();
			if (data == "") 
				throw new ONXMLFormatException(null, "Date");

			if (dtdVersion < 2.0) // Apply the locale format
				return new ONDate(data);
			else
			{
				try
				{
					data = data.Trim();
					int lFirst, lSecond;
					lFirst = data.IndexOf("-", 1);
					lSecond = data.IndexOf("-", lFirst + 1);
					System.DateTime lDate = new DateTime(Convert.ToInt16(data.Substring(0,lFirst)) , Convert.ToInt16(data.Substring(lFirst + 1,lSecond - lFirst - 1)) , Convert.ToInt16(data.Substring(lSecond + 1)) );
					return new ONDate(lDate);
				}
				catch (Exception e)
				{
					throw new ONXMLFormatException(e, "Date");
				}
			}
		}
		#endregion
	}
}

