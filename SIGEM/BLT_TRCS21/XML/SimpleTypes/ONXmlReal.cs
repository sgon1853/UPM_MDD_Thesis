// 3.4.4.5

using System;
using System.Xml;
using System.Globalization;
using SIGEM.Business.Types;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business.XML
{
	/// <summary>
	/// Basic Type ONXmlReal
	/// </summary>
	public abstract class ONXmlReal
	{
		#region Read / Write XML
		/// <summary>
		/// Extracts from the XML information and create components that the system understands.
		/// </summary>
		/// <param name="xmlReader">Object that has the message XML to be treated</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		/// <param name="xmlElement">Element of the XML that is checked</param>
		public static ONReal XML2ON(XmlReader xmlReader, double dtdVersion, string xmlElement)
		{
			if (xmlReader.IsStartElement(ONXml.XMLTAG_NULL))
			{
				xmlReader.Skip();
				return ONReal.Null;
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
				throw new ONXMLFormatException(e, "Real");	
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
		public static void ON2XML(XmlWriter xmlWriter, ONReal val, double dtdVersion, string xmlElement)
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
					xmlWriter.WriteElementString(xmlElement, val.TypedValue.ToString("0.##############"));
				else
				{
					xmlWriter.WriteStartElement(xmlElement);
					if (xmlElement == ONXml.XMLTAG_OIDFIELD && dtdVersion > 2.0)
						xmlWriter.WriteAttributeString("Type", "real");
				
					string lValue = Pack(val);
					xmlWriter.WriteString(lValue);
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
		public static ONReal UnPack(string data, double dtdVersion)
		{
			data = data.Trim();
			if (data == "")
				throw new ONXMLFormatException(null, "Real");

			if (dtdVersion < 2.0) // Apply the locale format
			{
				try
				{
					return new ONReal(Convert.ToDecimal(data));
				}
				catch (Exception e)
				{
					throw new ONXMLFormatException(e, "Real");
				}
			}

			if (data.IndexOf(",") > -1) 
				throw new ONXMLFormatException(null, "Real");

			if (data.IndexOf(".") == 0 ) 
				throw new ONXMLFormatException(null, "Real");

			try
			{
				return new ONReal(decimal.Parse(data, NumberStyles.Float, ONXml.ComXMLFormat));
			}
			catch (Exception e)
			{
				throw new ONXMLFormatException(e, "Real");
			}
		}
		/// <summary>
		/// Checks the format of the data previously to be included in the XML message
		/// </summary>
		/// <param name="val">Contains the data to be checked</param>
		public static string Pack(ONReal val)
		{
			if (val.TypedValue == 0)
				return "0";
			else
			{
				//Convert the value in a string with scientific format.
				string lValue = val.TypedValue.ToString("0.0#############E-0").Replace(",", ".");

				return lValue;
			}
		}
		#endregion
	}
}

