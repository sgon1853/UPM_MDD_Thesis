// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace SIGEM.Client.Adaptor.Serializer
{
	internal static class XMLServiceRequestSerializer
	{
		#region Serialize ServiceRequest.
		public static XmlWriter Serialize(XmlWriter writer, ServiceRequest serviceRequest)
		{
			writer.WriteStartElement(DTD.Request.TagServiceRequest);
			writer.WriteAttributeString(DTD.Request.ServiceRequest.TagClass, serviceRequest.Class);
			writer.WriteAttributeString(DTD.Request.ServiceRequest.TagService, serviceRequest.Name);

			XMLArgumentsSerializer.Serialize(writer, serviceRequest.Arguments);

			XMLChangeDetectionItemsSerializer.Serialize(writer, serviceRequest.ChangeDetectionItems);
			writer.WriteEndElement();
			return writer;
		}
		#endregion Serialize ServiceRequest.

		#region Deserialize Request.

		#region over load Methods.
		public static ServiceRequest Deserialize(string xmlResponse)
		{
			return Deserialize(xmlResponse, null);
		}
		public static ServiceRequest Deserialize(string xmlString, ServiceRequest serviceRequest)
		{
			return Deserialize(new XmlTextReader(new StringReader(xmlString)), serviceRequest);
		}
		public static ServiceRequest Deserialize(XmlReader reader)
		{
			return Deserialize(reader, null);
		}
		#endregion over load Methods.

		public static ServiceRequest Deserialize(XmlReader reader, ServiceRequest serviceRequest)
		{
			if (reader.IsStartElement(DTD.Request.TagServiceRequest))
			{
				if (serviceRequest == null)
				{
					serviceRequest = new ServiceRequest();
				}

				serviceRequest.Class = reader.GetAttribute(DTD.Request.ServiceRequest.TagClass);
				serviceRequest.Name = reader.GetAttribute(DTD.Request.ServiceRequest.TagService);

				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();
					serviceRequest.Arguments = XMLArgumentsSerializer.Deserialize(reader.ReadSubtree());
				}
				else
				{
					reader.Skip();
				}
			}
			else
			{
				throw new ArgumentException("Xml Reader don't have the Service.Request in Start Element.", "XmlReader reader");
			}

			return serviceRequest;
		}
		#endregion Deserialize Request.
	}
}

