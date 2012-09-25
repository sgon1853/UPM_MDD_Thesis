// 3.4.4.5

using System;
using System.Xml;
using SIGEM.Business.Types;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business.XML
{
	/// <summary>
	/// Basic Type ONXmlBool
	/// </summary>
	public abstract class ONXmlBool
	{
		#region Read / Write XML
		/// <summary>
		/// Extracts from the XML information and create components that the system understands.
		/// </summary>
		/// <param name="xmlReader">Object that has the message XML to be treated</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		/// <param name="xmlElement">Element of the XML that is checked</param>
		public static ONBool XML2ON(XmlReader xmlReader, double dtdVersion, string xmlElement)
		{
			if (xmlReader.IsStartElement(ONXml.XMLTAG_NULL))
			{
				xmlReader.Skip();
				return ONBool.Null;
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
				throw new ONXMLFormatException(e, "Bool");	
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
		public static void ON2XML(XmlWriter xmlWriter, ONBool val, double dtdVersion, string xmlElement)
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
					if (val.TypedValue)
						xmlWriter.WriteElementString(xmlElement, "Verdadero");
					else
						xmlWriter.WriteElementString(xmlElement, "Falso");
				}
				else
				{
					xmlWriter.WriteStartElement(xmlElement);
					if (xmlElement == ONXml.XMLTAG_OIDFIELD && dtdVersion > 2.0)
						xmlWriter.WriteAttributeString("Type", "bool");

					if (val.TypedValue) 
						xmlWriter.WriteString("true");
					else
						xmlWriter.WriteString("false");

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
		public static ONBool UnPack(string data, double dtdVersion)
		{
			data = data.Trim();
			if (data == "") 
				throw new ONXMLFormatException(null, "Bool");

			if (dtdVersion < 2.0) // Apply the locale format
			{
				try
				{
					return new ONBool(Convert.ToBoolean(data));
				}
				catch (Exception e)
				{
					throw new ONXMLFormatException(e, "Bool");
				}
			}
			else
			{
				if ((string.Compare(data , "true", true) == 0) || (data == "1"))
					return new ONBool(true);
				else if ((string.Compare(data, "false", true) == 0) || (data == "0"))
					return new ONBool(false);
				else
					throw new ONXMLFormatException(null, "Bool");
			}
		}
		#endregion
	}
}

