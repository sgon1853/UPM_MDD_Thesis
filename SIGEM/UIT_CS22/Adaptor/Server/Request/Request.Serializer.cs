// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace SIGEM.Client.Adaptor.Serializer
{
	#region XML Request Serializer/Deserializer
	/// <summary>
	/// Serializes request to an XML stream.
	/// </summary>
	internal static class XMLRequestSerializer
	{
		#region Serialize Request
		/// <summary>
		/// Serializes request to an XML stream.
		/// </summary>
		/// <param name="writer">XML stream to write.</param>
		/// <param name="request">Request.</param>
		/// <returns>XML stream with the request.</returns>
		public static XmlWriter Serialize(XmlWriter writer, Request request)
		{
			writer.WriteStartElement(DTD.TagRequest);

			writer.WriteAttributeString(DTD.Request.TagApplication, request.Application);
			writer.WriteAttributeString(DTD.Request.TagIdCnx, request.ConnectionString);
			writer.WriteAttributeString(DTD.Request.TagSequence, request.Sequence.ToString());
			writer.WriteAttributeString(DTD.Request.TagVersionDTD, request.VersionDTD);

			// Agent.
			XMLAgentSerializer.Serialize(writer, request.Agent);

			// Ticket.
			writer.WriteStartElement(DTD.Request.TagTicket);
			writer.WriteString(request.Ticket);
			writer.WriteEndElement();

			#region Proces Query.
			if (request.IsQuery)
			{
				XMLQueryRequestSerializer.Serialize(writer, request.Query);
			}
			#endregion Proces Query.
			else
			{
				#region Proces Service.
				if (request.IsService)
				{
					XMLServiceRequestSerializer.Serialize(writer, request.Service);
				}
				#endregion Proces Service.
			}
			writer.WriteEndElement();
			return writer;
		}
		#endregion Serialize Request

		#region Serialize Root Element.
		/// <summary>
		/// Serialize a request as a StringBuilder.
		/// </summary>
		/// <param name="stringBuilder">StringBuilder.</param>
		/// <param name="request">Request.</param>
		/// <returns>StringBuilder.</returns>
		public static StringBuilder SerializeRoot(StringBuilder stringBuilder, Request request)
		{
			if (stringBuilder == null)
			{
				stringBuilder = new StringBuilder();
			}

			MemoryStream lMemoryStream = XMLRequestSerializer.SerializeRoot(request);
			if (lMemoryStream != null)
			{
				using (lMemoryStream)
				{
					lMemoryStream.Position = 0;
					StreamReader lReader = new StreamReader(lMemoryStream);
					using (lReader)
					{
					stringBuilder.Append(lReader.ReadToEnd());
					}
				}
			}
			return stringBuilder;
		}
		/// <summary>
		/// Serialize a request as root node of an XML stream.
		/// </summary>
		/// <param name="xmlWriter">XML stream.</param>
		/// <param name="request">Request.</param>
		/// <returns>XML stream with request as root node.</returns>
		public static XmlWriter SerializeRoot(XmlWriter xmlWriter, Request request)
		{
			XmlWriter lXMLWriter = null;
			MemoryStream lMemoryStream = XMLRequestSerializer.SerializeRoot(request);

			if (lMemoryStream != null)
			{
				lXMLWriter = XmlWriter.Create(lMemoryStream);
			}
			return lXMLWriter;
		}
		/// <summary>
		/// Serializes a request as a MemoryStream.
		/// </summary>
		/// <param name="request">Request.</param>
		/// <returns>MemoryStream.</returns>
		public static MemoryStream SerializeRoot(Request request)
		{
			MemoryStream lResult = new MemoryStream();

			// Create XMLWriter
			if (request != null)
			{
				XmlWriterSettings lWriterSettings = new XmlWriterSettings();
				lWriterSettings.Indent = true;
				lWriterSettings.CheckCharacters = true;
				lWriterSettings.NewLineOnAttributes = true;
				lWriterSettings.Encoding = Encoding.UTF8;

				XmlWriter lWriter = XmlWriter.Create(lResult, lWriterSettings);
				lWriter.WriteStartDocument();

				Serialize(lWriter,request);

				lWriter.WriteEndDocument();
				lWriter.Flush();
				lResult.Flush();
				lResult.Position = 0;
			}
			return lResult;
		}
		#endregion Serialize Root Element.
	}
	#endregion XML Request Serializer/Deserializer
}

