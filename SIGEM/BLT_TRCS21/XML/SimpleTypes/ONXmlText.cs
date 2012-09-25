// 3.4.4.5

using System;
using System.Xml;
using SIGEM.Business.Types;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business.XML
{
	/// <summary>
	/// Basic Type ONXmlText
	/// </summary>
	public abstract class ONXmlText
	{
		#region Read / Write XML
		/// <summary>
		/// Extracts from the XML information and create components that the system understands.
		/// </summary>
		/// <param name="xmlReader">Object that has the message XML to be treated</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		/// <param name="xmlElement">Element of the XML that is checked</param>
		public static ONText XML2ON(XmlReader xmlReader, double dtdVersion, string xmlElement)
		{
			if (xmlReader.IsStartElement(ONXml.XMLTAG_NULL))
			{
				xmlReader.Skip();
				return ONText.Null;
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
				throw new ONXMLFormatException(e, "Text");	
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
		public static void ON2XML(XmlWriter xmlWriter, ONText val, double dtdVersion, string xmlElement)
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
				xmlWriter.WriteString(val.TypedValue);
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
		public static ONText UnPack(string data, double dtdVersion)
		{
			if (data == "")
				throw new ONXMLFormatException(null, "Text");

			try
			{
				return new ONText(data);
			}
			catch(Exception e)
			{
				throw new ONXMLFormatException(e, "Text");
			}
		}
		#endregion
			
	}
}

