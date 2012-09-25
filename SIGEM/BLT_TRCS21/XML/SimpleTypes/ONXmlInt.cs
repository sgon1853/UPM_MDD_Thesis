// 3.4.4.5

using System;
using System.Xml;
using SIGEM.Business.Types;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business.XML
{
	/// <summary>
	/// Basic Type ONXmlInt
	/// </summary>
	public abstract class ONXmlInt
	{
		#region Read / Write XML
		/// <summary>
		/// Extracts from the XML information and create components that the system understands.
		/// </summary>
		/// <param name="xmlReader">Object that has the message XML to be treated</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		/// <param name="xmlElement">Element of the XML that is checked</param>
		public static ONInt XML2ON(XmlReader xmlReader, double dtdVersion, string xmlElement)
		{
			if (xmlReader.IsStartElement(ONXml.XMLTAG_NULL))
			{
				xmlReader.Skip();
				return ONInt.Null;
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
				throw new ONXMLFormatException(e, "Int");	
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
		public static void ON2XML(XmlWriter xmlWriter, ONInt val, double dtdVersion, string xmlElement)
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
					xmlWriter.WriteAttributeString("Type", "int");

				xmlWriter.WriteString(val.TypedValue.ToString());
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
		public static ONInt UnPack(string data, double dtdVersion)
		{
			data = data.Trim();
			if (data == "") 
				throw new ONXMLFormatException(null, "Int");

			try
			{
				return new ONInt(Convert.ToInt32(data));
			}
			catch(Exception e)
			{
				throw new ONXMLFormatException(e, "Int");
			}
		}
		#endregion
	}
}

