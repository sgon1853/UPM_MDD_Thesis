// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace SIGEM.Client.Adaptor.Serializer
{

using SIGEM.Client.Adaptor.DataFormats;

	#region Serialize FilterVariable
	/// <summary>
	/// Serialize a filter variable to an XML file.
	/// </summary>
	internal static class XMLFilterVariableSerializer
	{
		#region Serialize FilterVariable
		/// <summary>
		/// Serializes a filter variable to an XML file.
		/// </summary>
		/// <param name="writer">XMLWriter where the argument is stored.</param>
		/// <param name="argument">Argument to serialize.</param>
		/// <returns>Returns the XMLWriter with the argument.</returns>
		public static XmlWriter Serialize(XmlWriter writer, Argument argument)
		{
			writer.WriteStartElement(DTD.Request.QueryRequest.QueryFilter.FilterVariables.TagFilterVariable);
			writer.WriteAttributeString(DTD.Request.QueryRequest.QueryFilter.FilterVariables.FilterVariable.TagName, argument.Name);
			ModelType modelType = Convert.StringTypeToMODELType(argument.Type);
			if (modelType == ModelType.Oid)
			{
				writer.WriteAttributeString(DTD.Request.QueryRequest.QueryFilter.FilterVariables.FilterVariable.TagType, argument.ClassName);
			}
			else
			{
				writer.WriteAttributeString(DTD.Request.QueryRequest.QueryFilter.FilterVariables.FilterVariable.TagType, argument.Type);
			}
			if (argument.Value == null)
			{
				writer.WriteElementString(DTD.Request.QueryRequest.QueryFilter.FilterVariables.FilterVariable.TagNull, string.Empty);
			}
			else
			{
				if (argument.Value is Oids.AlternateKey)
				{
					XMLAlternateKeySerializer.Serialize(writer, (Oids.AlternateKey)argument.Value);
				}
				else if (argument.Value is Oids.Oid)
				{
					XMLAdaptorOIDSerializer.Serialize(writer, argument.Value as Oids.Oid);
				}
				else // <Literal>
				{
					string lvalue = Convert.TypeToXml(argument.Type, argument.Value); //<-- Convert TypeToXML()!!!!					
					if (lvalue.Length > 0)
					{						
						// Check White spaces.
						string lvalueTrim = lvalue.Trim();	
						if (lvalueTrim.Length > 0)
						{
							writer.WriteStartElement(DTD.Request.QueryRequest.QueryFilter.FilterVariables.FilterVariable.TagLiteral);
							writer.WriteValue(lvalue);
							writer.WriteEndElement();
						}
						else// if is White spaces is <NULL>
						{							
							writer.WriteElementString(DTD.Request.QueryRequest.QueryFilter.FilterVariables.FilterVariable.TagNull, string.Empty);
						}
					}
					else // Is <NULL>
					{
						writer.WriteElementString(DTD.Request.QueryRequest.QueryFilter.FilterVariables.FilterVariable.TagNull, string.Empty);
					}
				}
			}
			writer.WriteEndElement();
			return writer;
		}
		#endregion Serialize FilterVariable
	}
	#endregion Serialize FilterVariable

	#region Serialize FilterVariables
	/// <summary>
	/// Serialize filter variables to an XML file.
	/// </summary>
	internal static class XMLFilterVariablesSerializer
	{
		#region Serialize FilterVariables
		/// <summary>
		/// Serializes filter variables to an XML file.
		/// </summary>
		/// <param name="writer">XMLWriter where the arguments are stored.</param>
		/// <param name="arguments">>Arguments to serialize.</param>
		/// <returns>Returns the XMLWriter with the arguments.</returns>
		public static XmlWriter Serialize(XmlWriter writer, Arguments arguments)
		{
		if (arguments != null)
		{
			writer.WriteStartElement(DTD.Request.QueryRequest.QueryFilter.TagFilterVariables);
			foreach (Argument i in arguments.Values)
			{
				XMLFilterVariableSerializer.Serialize(writer, i);
			}
			writer.WriteEndElement();
		}
		return writer;
		}
		#endregion Serialize FilterVariables
	}
	#endregion Serialize FilterVariables
}

