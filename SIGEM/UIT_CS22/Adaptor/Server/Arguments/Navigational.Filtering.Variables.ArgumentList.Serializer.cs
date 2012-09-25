// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace SIGEM.Client.Adaptor.Serializer
{
	using SIGEM.Client.Adaptor.DataFormats;

	#region Serialize/Deserialize ArgumentInfo filter variable.
	/// <summary>
	/// Serialize a navigational filtering argument info into an XML file.
	/// </summary>
	internal static class XMLNavigationalFilteringVariableInfoSerializer
	{
		#region Serialize ArgumentInfo filter variable.
		/// <summary>
		/// Serializes a navigational filtering argument info into an XML file.
		/// </summary>
		/// <param name="writer">XMLWriter where the argument is stored.</param>
		/// <param name="argument">Argument to serialize.</param>
		/// <returns>Returns the XMLWriter with the argument.</returns>
		public static XmlWriter Serialize(XmlWriter writer, ArgumentInfo argument)
		{
			writer.WriteStartElement(DTD.Request.QueryRequest.QueryFilter.FilterVariables.TagFilterVariable);
			writer.WriteAttributeString(DTD.Request.QueryRequest.QueryFilter.FilterVariables.FilterVariable.TagName, argument.Name);
			if (argument.Type == ModelType.Oid)
			{
				writer.WriteAttributeString(DTD.Request.ServiceRequest.Arguments.Argument.TagType, argument.ClassName);
			}
			else
			{
				writer.WriteAttributeString(DTD.Request.QueryRequest.QueryFilter.FilterVariables.FilterVariable.TagType, Convert.MODELTypeToStringType(argument.Type));
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
						writer.WriteStartElement(DTD.Request.QueryRequest.QueryFilter.FilterVariables.FilterVariable.TagLiteral);
						writer.WriteValue(lvalue);
						writer.WriteEndElement();
					}
					else // Is <NULL>
					{
						writer.WriteElementString(DTD.Request.QueryRequest.QueryFilter.FilterVariables.FilterVariable.TagNull, string.Empty);
					}
				}
			}
			else // Is <NULL>
			{
				writer.WriteElementString(DTD.Request.QueryRequest.QueryFilter.FilterVariables.FilterVariable.TagNull, string.Empty);
			}
			writer.WriteEndElement();
			return writer;
		}
		#endregion Serialize ArgumentInfo filter variable.
	}
	#endregion Serialize/Deserialize ArgumentInfo filter variable.

	#region Serialize/Deserialize variables list.
	/// <summary>
	/// Serialize a navigational filtering variables list into an XML file.
	/// </summary>
	internal static class XMLNavigationalFilteringVariablesListSerializer
	{
		#region Serialize variables list.
		/// <summary>
		/// Serializes a navigational filtering variables list into an XML file.
		/// </summary>
		/// <param name="writer">XMLWriter where the list of variables is stored.</param>
		/// <param name="variables">List of variables to serialize.</param>
		/// <returns>Returns the XMLWriter with the list of variables.</returns>
		public static XmlWriter Serialize(XmlWriter writer, ArgumentsList variables)
		{
			if (variables != null)
			{
				writer.WriteStartElement(DTD.Request.QueryRequest.QueryFilter.TagFilterVariables);
				foreach (ArgumentInfo i in variables)
				{
					XMLNavigationalFilteringVariableInfoSerializer.Serialize(writer, i);
				}
				writer.WriteEndElement();
			}
			return writer;
		}
		#endregion Serialize variables list.
	}
	#endregion Serialize/Deserialize variables list.
}


