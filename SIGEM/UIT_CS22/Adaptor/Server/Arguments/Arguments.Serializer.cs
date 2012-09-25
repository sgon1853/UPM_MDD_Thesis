// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace SIGEM.Client.Adaptor.Serializer
{
	using SIGEM.Client.Adaptor.DataFormats;

	#region Serialize/Deserialize Argument
	/// <summary>
	/// Serialize/Deserialize an argument to/from an XML file.
	/// </summary>
	internal static class XMLArgumentSerializer
	{
		#region Serialize Argument
		/// <summary>
		/// Serializes an argument to an XML file.
		/// </summary>
		/// <param name="writer">XMLWriter where the argument is stored.</param>
		/// <param name="argument">Argument to serialize.</param>
		/// <returns>Returns the XMLWriter with the argument.</returns>
		public static XmlWriter Serialize(XmlWriter writer, Argument argument)
		{
			writer.WriteStartElement(DTD.Request.ServiceRequest.Arguments.TagArgument);
			writer.WriteAttributeString(DTD.Request.ServiceRequest.Arguments.Argument.TagName, argument.Name);
			ModelType modelType = Convert.StringTypeToMODELType(argument.Type);
			if (modelType == ModelType.Oid)
			{
				writer.WriteAttributeString(DTD.Request.ServiceRequest.Arguments.Argument.TagType, argument.ClassName);
			}
			else
			{
				writer.WriteAttributeString(DTD.Request.ServiceRequest.Arguments.Argument.TagType, argument.Type);
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
				else // <Literal>
				{
					string lvalue = Convert.TypeToXml(argument.Type, argument.Value); //<-- Convert TypeToXML()!!!!
					if (lvalue.Length > 0)
					{
						string lvalueTrim = lvalue.Trim();
						if (lvalueTrim.Length > 0)
						{
							writer.WriteStartElement(DTD.Request.ServiceRequest.Arguments.Argument.TagLiteral);
							writer.WriteValue(lvalue);
							writer.WriteEndElement();
						}
						else// if is string White spaces it value is <NULL>
						{
							writer.WriteElementString(DTD.Request.ServiceRequest.Arguments.Argument.TagNull, string.Empty);
						}
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
		#endregion Serialize Argument

		#region Deserialize Argument

		#region Deserialize over load methods
		/// <summary>
		/// Deserializes an argument from an XML file.
		/// </summary>
		/// <param name="xmlString">The XML file as a string.</param>
		/// <returns>Argument.</returns>
		public static Argument Deserialize(string xmlString)
		{
			return Deserialize(new XmlTextReader(new StringReader(xmlString)));
		}
		#endregion Deserialize over load methods

		/// <summary>
		/// Deserializes an argument from an XML file.
		/// </summary>
		/// <param name="reader">XMLReader where the argument is.</param>
		/// <returns>Argument.</returns>
		public static Argument Deserialize(XmlReader reader)
		{
			Argument lResult = null;
			if (reader.IsStartElement(DTD.Request.ServiceRequest.Arguments.TagArgument))
			{
				lResult = new Argument();
				string stringModelType = reader.GetAttribute(DTD.Request.ServiceRequest.Arguments.Argument.TagType);
				ModelType modelType = Convert.StringTypeToMODELType(stringModelType);

				if (modelType == ModelType.Oid)
				{
					lResult.Type = Convert.MODELTypeToStringType(ModelType.Oid);
					lResult.ClassName = stringModelType;
				}
				else
				{
					lResult.Type = stringModelType;
					lResult.ClassName = string.Empty;
				}
				lResult.Name = reader.GetAttribute(DTD.Request.ServiceRequest.Arguments.Argument.TagName);

				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();
					// if the argument is OutBound
					if (reader.IsStartElement(DTD.Request.ServiceRequest.Arguments.Argument.TagValue))
					{
						reader.Read();
					}
					switch (reader.LocalName)
					{
						case DTD.Request.ServiceRequest.Arguments.Argument.TagNull:
							try
							{
								lResult.Value = Convert.XmlToType(lResult.Type, null);
							}
							catch
							{
								// the argument is an object-valued argument
								lResult.Value = null;
							}
							reader.Skip();
						break;
						case DTD.Request.ServiceRequest.Arguments.Argument.TagLiteral:
						{
							if (reader.IsEmptyElement)
							{
								lResult.Value = string.Empty;
								reader.Skip();
							}
							else
							{
								lResult.Value = Convert.XmlToType(lResult.Type, reader.ReadString());
								reader.ReadEndElement();
							}
						}
						break;

						case DTD.TagOID:
						lResult.Value = XMLAdaptorOIDSerializer.Deserialize(reader.ReadSubtree());
						break;
					}
					reader.ReadEndElement();
				}
				else
				{
					reader.Skip();
				}
			}
			else
			{
				throw new ArgumentException("Xml Reader don't have the Argument in Start Element.", "XmlReader reader");
			}
			return lResult;
		}
		#endregion Deserialize OID Field
	}
	#endregion Serialize/Deserialize Argument

	#region Serialize/Deserialize Arguments
	/// <summary>
	/// Serialize/Deserialize arguments to/from an XML file.
	/// </summary>
	internal static class XMLArgumentsSerializer
	{
		#region Serialize Arguments
		/// <summary>
		/// Serializes arguments to an XML file.
		/// </summary>
		/// <param name="writer">XMLWriter where the arguments are stored.</param>
		/// <param name="arguments">Arguments to serialize.</param>
		/// <returns>Returns the XMLWriter with the arguments.</returns>
		public static XmlWriter Serialize(XmlWriter writer, Arguments arguments)
		{
			if (arguments != null)
			{
				writer.WriteStartElement(DTD.Request.ServiceRequest.TagArguments);
				foreach (Argument i in arguments.Values)
				{
					XMLArgumentSerializer.Serialize(writer, i);
				}
				writer.WriteEndElement();
			}
			return writer;
		}
		#endregion Serialize Argument

		#region Deserialize Argument

		#region Deserialize over load methods
		/// <summary>
		/// Deserializes arguments from an XML file.
		/// </summary>
		/// <param name="xmlString">The XML file as a string.</param>
		/// <returns>Arguments.</returns>
		public static Arguments Deserialize(string xmlString)
		{
			return Deserialize(new XmlTextReader(new StringReader(xmlString)));
		}
		#endregion Deserialize over load methods

		/// <summary>
		/// Deserializes arguments from an XML file.
		/// </summary>
		/// <param name="reader">XMLReader where the arguments are.</param>
		/// <returns>Arguments.</returns>
		public static Arguments Deserialize(XmlReader reader)
		{
			Arguments lResult = null;

			if (reader.IsStartElement(DTD.Request.ServiceRequest.TagArguments))
			{
				lResult = new Arguments();

				if (!reader.IsEmptyElement)
				{
				reader.ReadStartElement();
				do
				{
					if (reader.IsStartElement(DTD.Request.ServiceRequest.Arguments.TagArgument))
					{
					lResult.Add(XMLArgumentSerializer.Deserialize(reader.ReadSubtree()));
					}

				} while (reader.Read());
				}
				else
				{
				reader.Skip();
				}
			}
			else
			{
				throw new ArgumentException("Xml Reader don't have the " + DTD.Request.ServiceRequest.TagArguments + "in Start Element.", "XmlReader reader");
			}
			return lResult;
		}

		#endregion Deserialize OID Field
	}
	#endregion Serialize/Deserialize Arguments
}

