// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace SIGEM.Client.Adaptor.Serializer
{
	using SIGEM.Client.Adaptor.DataFormats;

	#region Serialize/Deserialize ChangeDetectionItem
	/// <summary>
	/// Serialize/Deserialize an ChangeDetectionItem to/from an XML file.
	/// </summary>
	internal static class XMLChangeDetectionItemSerializer
	{
		#region Serialize ChangeDetectionItem
		/// <summary>
		/// Serializes an ChangeDetectionItem to an XML file.
		/// </summary>
		/// <param name="writer">XMLWriter where the ChangeDetectionItem is stored.</param>
		/// <param name="ChangeDetectionItem">ChangeDetectionItem to serialize.</param>
		/// <returns>Returns the XMLWriter with the ChangeDetectionItem.</returns>
		public static XmlWriter Serialize(XmlWriter writer, ChangeDetectionItem item)
		{
			writer.WriteStartElement(DTD.Request.ServiceRequest.ChangeDetectionItems.TagChangeDetectionItem);
			writer.WriteAttributeString(DTD.Request.ServiceRequest.ChangeDetectionItems.ChangeDetectionItem.TagName, item.Name);
			ModelType modelType = Convert.StringTypeToMODELType(item.Type);
			if (modelType == ModelType.Oid)
			{
				writer.WriteAttributeString(DTD.Request.ServiceRequest.ChangeDetectionItems.ChangeDetectionItem.TagType, item.ClassName);
			}
			else
			{
				writer.WriteAttributeString(DTD.Request.ServiceRequest.ChangeDetectionItems.ChangeDetectionItem.TagType, item.Type);
			}

			if (item.Value != null)
			{
				if (item.Value is Oids.AlternateKey)
				{
					XMLAlternateKeySerializer.Serialize(writer, (Oids.AlternateKey)item.Value);
				}
				else if (item.Value is Oids.Oid)
				{
					XMLAdaptorOIDSerializer.Serialize(writer, item.Value as Oids.Oid);
				}
				else // <Literal>
				{
					string lvalue = Convert.TypeToXml(item.Type, item.Value); //<-- Convert TypeToXML()!!!!
					if (lvalue.Length > 0)
					{
						string lvalueTrim = lvalue.Trim();
						if (lvalueTrim.Length > 0)
						{
							writer.WriteStartElement(DTD.Request.ServiceRequest.ChangeDetectionItems.ChangeDetectionItem.TagLiteral);
							writer.WriteValue(lvalue);
							writer.WriteEndElement();
						}
						else// if is string White spaces it value is <NULL>
						{
							writer.WriteElementString(DTD.Request.ServiceRequest.ChangeDetectionItems.ChangeDetectionItem.TagNull, string.Empty);
						}
					}
					else // Is <NULL>
					{
						writer.WriteElementString(DTD.Request.ServiceRequest.ChangeDetectionItems.ChangeDetectionItem.TagNull, string.Empty);
					}
				}
			}
			else // Is <NULL>
			{
				writer.WriteElementString(DTD.Request.ServiceRequest.ChangeDetectionItems.ChangeDetectionItem.TagNull, string.Empty);
			}
			writer.WriteEndElement();
			return writer;
		}
		#endregion Serialize ChangeDetectionItem

		#region Deserialize ChangeDetectionItem

		#region Deserialize over load methods
		/// <summary>
		/// Deserializes an ChangeDetectionItem from an XML file.
		/// </summary>
		/// <param name="xmlString">The XML file as a string.</param>
		/// <returns>ChangeDetectionItem.</returns>
		public static ChangeDetectionItem Deserialize(string xmlString)
		{
			return Deserialize(new XmlTextReader(new StringReader(xmlString)));
		}
		#endregion Deserialize over load methods

		/// <summary>
		/// Deserializes an ChangeDetectionItem from an XML file.
		/// </summary>
		/// <param name="reader">XMLReader where the ChangeDetectionItem is.</param>
		/// <returns>ChangeDetectionItem.</returns>
		public static ChangeDetectionItem Deserialize(XmlReader reader)
		{
			ChangeDetectionItem lResult = null;
			if (reader.IsStartElement(DTD.Request.ServiceRequest.ChangeDetectionItems.TagChangeDetectionItem))
			{
				lResult = new ChangeDetectionItem();
				string stringModelType = reader.GetAttribute(DTD.Request.ServiceRequest.ChangeDetectionItems.ChangeDetectionItem.TagType);
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
				lResult.Name = reader.GetAttribute(DTD.Request.ServiceRequest.ChangeDetectionItems.ChangeDetectionItem.TagName);

				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();
					// if the ChangeDetectionItem is OutBound
					if (reader.IsStartElement(DTD.Request.ServiceRequest.ChangeDetectionItems.ChangeDetectionItem.TagValue))
					{
						reader.Read();
					}
					switch (reader.LocalName)
					{
						case DTD.Request.ServiceRequest.ChangeDetectionItems.ChangeDetectionItem.TagNull:
							try
							{
								lResult.Value = Convert.XmlToType(lResult.Type, null);
							}
							catch
							{
								// the ChangeDetectionItem is an object-valued ChangeDetectionItem
								lResult.Value = null;
							}
							reader.Skip();
						break;
						case DTD.Request.ServiceRequest.ChangeDetectionItems.ChangeDetectionItem.TagLiteral:
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
				throw new ArgumentException("Xml Reader don't have the ChangeDetectionItem in Start Element.", "XmlReader reader");
			}
			return lResult;
		}
		#endregion  Deserialize ChangeDetectionItem


	}
	#endregion Serialize/Deserialize ChangeDetectionItem

	#region Serialize/Deserialize ChangeDetectionItems
	/// <summary>
	/// Serialize/Deserialize ChangeDetectionItems to/from an XML file.
	/// </summary>
	internal static class XMLChangeDetectionItemsSerializer
	{
		#region Serialize ChangeDetectionItems
		/// <summary>
		/// Serializes ChangeDetectionItems to an XML file.
		/// </summary>
		/// <param name="writer">XMLWriter where the ChangeDetectionItems are stored.</param>
		/// <param name="ChangeDetectionItems">ChangeDetectionItems to serialize.</param>
		/// <returns>Returns the XMLWriter with the ChangeDetectionItems.</returns>
		public static XmlWriter Serialize(XmlWriter writer, ChangeDetectionItems items)
		{
			if ((items != null) && (items.Count > 0 ))
			{
				writer.WriteStartElement(DTD.Request.ServiceRequest.TagChangeDetectionItems);
				foreach (ChangeDetectionItem i in items.Values)
				{
					XMLChangeDetectionItemSerializer.Serialize(writer, i);
				}
				writer.WriteEndElement();
			}
			return writer;
		}
		#endregion Serialize ChangeDetectionItem

		#region Deserialize ChangeDetectionItems

		#region Deserialize over load methods
		/// <summary>
		/// Deserializes ChangeDetectionItems from an XML file.
		/// </summary>
		/// <param name="xmlString">The XML file as a string.</param>
		/// <returns>ChangeDetectionItems.</returns>
		public static ChangeDetectionItems Deserialize(string xmlString)
		{
			return Deserialize(new XmlTextReader(new StringReader(xmlString)));
		}
		#endregion Deserialize over load methods

		/// <summary>
		/// Deserializes ChangeDetectionItems from an XML file.
		/// </summary>
		/// <param name="reader">XMLReader where the ChangeDetectionItems are.</param>
		/// <returns>ChangeDetectionItems.</returns>
		public static ChangeDetectionItems Deserialize(XmlReader reader)
		{
			ChangeDetectionItems lResult = null;

			if (reader.IsStartElement(DTD.Request.ServiceRequest.TagChangeDetectionItems))
			{
				lResult = new ChangeDetectionItems();

				if (!reader.IsEmptyElement)
				{
				reader.ReadStartElement();
				do
				{
					if (reader.IsStartElement(DTD.Request.ServiceRequest.ChangeDetectionItems.TagChangeDetectionItem))
					{
					lResult.Add(XMLChangeDetectionItemSerializer.Deserialize(reader.ReadSubtree()));
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
				throw new ArgumentException("Xml Reader don't have the " + DTD.Request.ServiceRequest.TagChangeDetectionItems + "in Start Element.", "XmlReader reader");
			}
			return lResult;
		}

		#endregion ChangeDetectionItems
	}
	#endregion Serialize/Deserialize ChangeDetectionItems
}


