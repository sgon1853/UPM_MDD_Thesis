// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace SIGEM.Client.Adaptor.Serializer
{
using SIGEM.Client.Adaptor.DataFormats;

	#region Serialize/Deserialize OID
	/// <summary>
	/// Serializes/Deserializes OID to/from an XML stream.
	/// </summary>
	internal static class XMLAdaptorOIDSerializer
	{
		#region Serialize <OID>
		/// <summary>
		/// Serializes an Oid object to an XML stream.
		/// </summary>
		/// <param name="writer">XML stream to write.</param>
		/// <param name="oid">Oid.</param>
		/// <returns>XML stream with the Oid object.</returns>
		public static XmlWriter Serialize(XmlWriter writer, Oids.Oid oid)
		{
			writer.WriteStartElement(DTD.TagOID);
			writer.WriteAttributeString(DTD.OID.TagClass, oid.ClassName);
			foreach (KeyValuePair<ModelType,object> i in oid.GetFields())
			{
				writer.WriteStartElement(DTD.OID.TagOIDField);
				writer.WriteAttributeString(DTD.OID.TagType, Convert.MODELTypeToStringType(i.Key));
				try
				{
					writer.WriteString(Convert.TypeToXml(i.Key, i.Value));
				}
				catch(Exception ex)
				{
					StringBuilder lMessage = new StringBuilder("Fail OID Serialize [");
					lMessage.Append(oid.ClassName);
					lMessage.Append(']');
					throw new ApplicationException(lMessage.ToString(), ex);
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			return writer;
		}
		#endregion Serialize <OID>

		#region Deserialize over load Methods <OID>
		/// <summary>
		/// Deserializes Oid from an XML stream.
		/// </summary>
		/// <param name="xmlString">XML as a string.</param>
		/// <returns>Oid.</returns>
		public static Oids.Oid Deserialize(string xmlString)
		{
			return Deserialize(new XmlTextReader(new StringReader(xmlString)));
		}
		#endregion Deserialize over load Methods <OID>

		#region Deserialize <OID>.
		//public static Oids.Oid Deserialize(XmlReader reader, Oids.Oid oid)
		/// <summary>
		/// Deserializes Oid from an XML stream.
		/// </summary>
		/// <param name="reader">XML stream.</param>
		/// <returns>Oid.</returns>
		public static Oids.Oid Deserialize(XmlReader reader)
		{
			Oids.Oid lResult = null;
			if (reader.IsStartElement(DTD.TagOID))
			{
				string lClassName = reader.GetAttribute(DTD.OID.TagClass);
				List<KeyValuePair<ModelType,object>> lFields = new List<KeyValuePair<ModelType,object>>();

				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();
					do
					{
					#region Process tag <OID.Field>.
					if (reader.IsStartElement(DTD.OID.TagOIDField))
					{
						if (!reader.IsEmptyElement)
						{
							ModelType lType = Convert.StringTypeToMODELType(reader.GetAttribute(DTD.OID.TagType));
							lFields.Add(new KeyValuePair<ModelType, object>(lType, Convert.XmlToType(lType, reader.ReadString())));
						}
						else
						{
							throw new ArgumentException("Xml Reader have one OID.Field with empty Element.", "XmlReader reader");
						}
					}
					#endregion Process tag <OID.Field>.
					else
					{
						#region Process tag <?>
						reader.Skip();
						if (reader.NodeType == XmlNodeType.None)
						{
							break;
						}
						else
						{
							continue;
						}
						#endregion Process tag <?>
					}
					} while (reader.Read());
				}
				else
				{
					reader.Skip();
				}

				if(lClassName.Length > 0)
				{
					lResult = ServerConnection.CreateOid(lClassName,lFields);
				}
			}
			else
			{
				throw new ArgumentException("Xml Reader don't have the OID in Start Element.", "XmlReader reader");
			}
			return lResult;
		}
		#endregion Deserialize OID Field.
	}
	#endregion Serialize/Deserialize OID

	#region Serialize/Deserialize OID
	/// <summary>
	/// Serializes/Deserializes Alternate Key to/from an XML stream.
	/// </summary>
	internal static class XMLAlternateKeySerializer
	{
		#region Serialize <AlternateKey>
		/// <summary>
		/// Serializes an AlternateKey object to an XML stream.
		/// </summary>
		/// <param name="writer">XML stream to write.</param>
		/// <param name="alternateKey">AlternateKey object.</param>
		/// <returns>XML stream with the AlternateKey object.</returns>
		public static XmlWriter Serialize(XmlWriter writer, Oids.Oid alternateKey)
		{
			writer.WriteStartElement(DTD.TagAlternateKey);
			writer.WriteAttributeString(DTD.AlternateKey.TagName, alternateKey.ClassName);
			// Process element childs.
			foreach (KeyValuePair<ModelType, object> i in alternateKey.GetFields())
			{
				writer.WriteStartElement(DTD.AlternateKey.TagAlternateKeyField);
				writer.WriteAttributeString(DTD.AlternateKey.TagType, Convert.MODELTypeToStringType(i.Key));
				// Set value for this node.
				try
				{
					writer.WriteString(Convert.TypeToXml(i.Key, i.Value));
				}
				catch (Exception ex)
				{
					StringBuilder lMessage = new StringBuilder("Fail Alternate Key Serialize [");
					lMessage.Append(alternateKey.ClassName);
					lMessage.Append(" - ");
					lMessage.Append(alternateKey.AlternateKeyName);
					lMessage.Append(']');
					throw new ApplicationException(lMessage.ToString(), ex);
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			return writer;
		}
		#endregion Serialize <AlternateKey>
	}
	#endregion Serialize/Deserialize OID

	#region Serialize/Deserialize Agent
	/// <summary>
	/// Serializes/Deserializes Agent to/from XML stream.
	/// </summary>
	internal static class XMLAgentSerializer
	{
		#region Serialize Agent
		/// <summary>
		/// Serializes an Agent to an XML stream.
		/// </summary>
		/// <param name="writer">XML stream to write.</param>
		/// <param name="agent">Agent.</param>
		/// <returns>XML stream with the agent.</returns>
		internal static XmlWriter Serialize(XmlWriter writer, Oids.Oid agent)
		{
			if (agent != null && agent.IsValid())
			{
				writer.WriteStartElement(DTD.Request.TagAgent);
				XMLAdaptorOIDSerializer.Serialize(writer, agent);
				writer.WriteEndElement();
			}
			return writer;
		}
		#endregion Serialize Agent

		#region Deserialize Agent

		#region over load Methods
		/// <summary>
		/// Deserializes Agent from an XML stream.
		/// </summary>
		/// <param name="xmlString">XML as a string.</param>
		/// <returns>Oid (agent).</returns>
		public static Oids.Oid Deserialize(string xmlString)
		{
			return Deserialize(new XmlTextReader(new StringReader(xmlString)));
		}
		#endregion over load Methods

		/// <summary>
		/// Deserializes Agent from an XML stream.
		/// </summary>
		/// <param name="reader">XML stream.</param>
		/// <returns>Oid (agent).</returns>
		public static Oids.Oid Deserialize(XmlReader reader)
		{
			Oids.Oid lResult = null;
			if (reader.IsStartElement(DTD.Request.TagAgent))
			{
				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();
					lResult = XMLAdaptorOIDSerializer.Deserialize(reader);
				}
				else
				{
					reader.Skip();
				}
			}
			else
			{
				throw new ArgumentException("Xml Reader don't have the Agent in Start Element.", "XmlReader reader");
			}
			return lResult ;
		}
		#endregion Deserialize Agent
	}
	#endregion Serialize/Deserialize Agent
}

