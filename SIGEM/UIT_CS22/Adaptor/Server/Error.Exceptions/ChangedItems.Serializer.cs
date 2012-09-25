// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using SIGEM.Client.Adaptor.Exceptions;

namespace SIGEM.Client.Adaptor.Serializer
{
	using SIGEM.Client.Adaptor.DataFormats;

	#region Serialize/Deserialize ChangedItem
	/// <summary>
	/// Serialize/Deserialize an ChangedItem to/from an XML file.
	/// </summary>
	internal static class XMLChangedItemSerializer
	{

		#region Deserialize ChangedItem

		#region Deserialize over load methods
		/// <summary>
		/// Deserializes an ChangedItem from an XML file.
		/// </summary>
		/// <param name="xmlString">The XML file as a string.</param>
		/// <returns>ChangedItem.</returns>
		public static ChangedItem Deserialize(string xmlString)
		{
			return Deserialize(new XmlTextReader(new StringReader(xmlString)));
		}
		#endregion Deserialize over load methods

		/// <summary>
		/// Deserializes an ChangedItem from an XML file.
		/// </summary>
		/// <param name="reader">XMLReader where the ChangedItem is.</param>
		/// <returns>ChangedItem.</returns>
		public static ChangedItem Deserialize(XmlReader reader)
		{
			ChangedItem lResult = null;
			if (reader.IsStartElement(DTD.Error.ChangedItems.TagChangedItem))
			{
				lResult = new ChangedItem();
				string stringModelType = reader.GetAttribute(DTD.Error.ChangedItems.ChangedItem.TagType);
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
				lResult.Name = reader.GetAttribute(DTD.Error.ChangedItems.ChangedItem.TagName);

				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();

					if (reader.IsStartElement(DTD.Error.ChangedItems.ChangedItem.TagChangedItemOldValue))
					{
						lResult.OldValue =  XMLChangedItemValueSerializer.Deserialize(reader.ReadSubtree(),lResult.Type, lResult.ClassName);
						reader.ReadEndElement();
					}

					if (reader.IsStartElement(DTD.Error.ChangedItems.ChangedItem.TagChangedItemNewValue))
					{
						lResult.NewValue = XMLChangedItemValueSerializer.Deserialize(reader.ReadSubtree(), lResult.Type, lResult.ClassName);
						reader.ReadEndElement();
					}
				}
				else
				{
					reader.Skip();
				}
			}
			else
			{
				throw new ArgumentException("Xml Reader don't have the ChangedItem in Start Element.", "XmlReader reader");
			}
			return lResult;
		}
		#endregion  Deserialize ChangedItem
	}
	#endregion Serialize/Deserialize ChangedItem
	internal static class XMLChangedItemValueSerializer
	{
		public static object Deserialize(XmlReader reader, string type, string className)
		{
			object value = null;
			if ((reader.IsStartElement(DTD.Error.ChangedItems.ChangedItem.TagChangedItemOldValue))
				|| (reader.IsStartElement(DTD.Error.ChangedItems.ChangedItem.TagChangedItemNewValue)))
			{
				 reader.ReadStartElement();

				switch (reader.LocalName)
				{
					case DTD.Error.ChangedItems.ChangedItem.TagNull:
						try
						{
							value = Convert.XmlToType(type, null);
						}
						catch
						{// the ChangedItem is an object-valued ChangedItem
							value = null;
						}
						reader.Skip();
						break;
					case DTD.Error.ChangedItems.ChangedItem.TagLiteral:
						{
							if (reader.IsEmptyElement)
							{
								value = string.Empty;
								reader.Skip();
							}
							else
							{
								value = Convert.XmlToType(type, reader.ReadString());
								reader.ReadEndElement();
							}
						}
						break;

					case DTD.TagOID:
						value = XMLAdaptorOIDSerializer.Deserialize(reader.ReadSubtree());
						reader.ReadEndElement();
						break;
				}
				reader.ReadEndElement();
			}
		  return value;
		 }

	}

	#region Serialize/Deserialize ChangedItems
	/// <summary>
	/// Serialize/Deserialize ChangedItems to/from an XML file.
	/// </summary>
	internal static class XMLChangedItemsSerializer
	{

		#region Deserialize ChangedItems

		#region Deserialize over load methods
		/// <summary>
		/// Deserializes ChangedItems from an XML file.
		/// </summary>
		/// <param name="xmlString">The XML file as a string.</param>
		/// <returns>ChangedItems.</returns>
		public static ChangedItems Deserialize(string xmlString)
		{
			return Deserialize(new XmlTextReader(new StringReader(xmlString)));
		}
		#endregion Deserialize over load methods

		/// <summary>
		/// Deserializes ChangedItems from an XML file.
		/// </summary>
		/// <param name="reader">XMLReader where the ChangedItems are.</param>
		/// <returns>ChangedItems.</returns>
		public static ChangedItems Deserialize(XmlReader reader)
		{
			ChangedItems lResult = null;

			if (reader.IsStartElement(DTD.Error.TagChangedItems))
			{
				lResult = new ChangedItems();

				if (!reader.IsEmptyElement)
				{
				reader.ReadStartElement();
				do
				{
					if (reader.IsStartElement(DTD.Error.ChangedItems.TagChangedItem))
					{
						lResult.Add(XMLChangedItemSerializer.Deserialize(reader.ReadSubtree()));
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
				throw new ArgumentException("Xml Reader don't have the " + DTD.Error.TagChangedItems + "in Start Element.", "XmlReader reader");
			}
			return lResult;
		}

		#endregion ChangedItems

	}
	#endregion Serialize/Deserialize ChangedItems
}


