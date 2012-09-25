// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace SIGEM.Client.Adaptor.Serializer
{
	using SIGEM.Client.Adaptor.DataFormats;

	#region Serialize/Deserialize ArgumentInfo argument.
	/// <summary>
	/// Serialize a navigational filtering argument info into an XML file.
	/// </summary>
	internal static class XMLNavigationalFilteringArgumentInfoSerializer
	{
		#region Serialize ArgumentInfo argument.
		/// <summary>
		/// Serializes a navigational filtering argument info into an XML file.
		/// </summary>
		/// <param name="writer">XMLWriter where the argument is stored.</param>
		/// <param name="argument">Argument to serialize.</param>
		/// <returns>Returns the XMLWriter with the argument.</returns>
		public static XmlWriter Serialize(XmlWriter writer, ArgumentInfo argument)
		{
			writer.WriteStartElement(DTD.Request.ServiceRequest.Arguments.TagArgument);
			writer.WriteAttributeString(DTD.Request.ServiceRequest.Arguments.Argument.TagName, argument.Name);
			if (argument.Type == ModelType.Oid)
			{
				writer.WriteAttributeString(DTD.Request.ServiceRequest.Arguments.Argument.TagType, argument.ClassName);
			}
			else
			{
				writer.WriteAttributeString(DTD.Request.ServiceRequest.Arguments.Argument.TagType, Convert.MODELTypeToStringType(argument.Type));
			}

			if (argument.Value != null)
			{
				if (argument.Value is Oids.AlternateKey)
				{
					XMLAlternateKeySerializer.Serialize(writer, (Oids.AlternateKey)argument.Value);
				}
				else if (argument.Value is Oids.Oid)
				{
					XMLAdaptorOIDSerializer.Serialize(writer, argument.Value as Oids.Oid);
				}
				else
				{
					string lvalue = Convert.TypeToXml(argument.Type, argument.Value); //<-- Convert TypeToXML()!!!!
					if (lvalue.Length > 0)
					{
						writer.WriteStartElement(DTD.Request.ServiceRequest.Arguments.Argument.TagLiteral);
						writer.WriteValue(lvalue);
						writer.WriteEndElement();
					}
					else // Is <NULL>
					{
						writer.WriteElementString(DTD.Request.ServiceRequest.Arguments.Argument.TagNull, string.Empty);
					}
				}
			}
			else // Is <NULL>
			{
				writer.WriteElementString(DTD.Request.ServiceRequest.Arguments.Argument.TagNull, string.Empty);
			}
			writer.WriteEndElement();
			return writer;
		}
		#endregion Serialize ArgumentInfo argument.
	}
	#endregion Serialize/Deserialize ArgumentInfo argument.

	#region Serialize/Deserialize arguments list.
	/// <summary>
	/// Serialize a navigational filtering arguments list into an XML file.
	/// </summary>
	internal static class XMLNavigationalFilteringArgumentsListSerializer
	{
		#region Serialize arguments list.
		/// <summary>
		/// Serializes a navigational filtering arguments list into an XML file.
		/// </summary>
		/// <param name="writer">XMLWriter where the list of arguments is stored.</param>
		/// <param name="arguments">List of arguments to serialize.</param>
		/// <returns>Returns the XMLWriter with the list of arguments.</returns>
		public static XmlWriter Serialize(XmlWriter writer, ArgumentsList arguments)
		{
			if (arguments != null)
			{
				writer.WriteStartElement(DTD.Request.ServiceRequest.TagArguments);
				foreach (ArgumentInfo i in arguments)
				{
					XMLNavigationalFilteringArgumentInfoSerializer.Serialize(writer, i);
				}
				writer.WriteEndElement();
			}
			return writer;
		}
		#endregion Serialize arguments list.
	}
	#endregion Serialize/Deserialize arguments list.
}

