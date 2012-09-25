// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace SIGEM.Client.Adaptor.Serializer
{
	#region XML Service Response Serializer/Deserializer.
	internal static class XMLServiceResponseSerializer
	{
		#region Serialize ServiceRequest.
		public static XmlWriter Serialize(XmlWriter writer, ServiceResponse serviceResponse)
		{
			writer.WriteStartElement(DTD.Response.TagServiceResponse);
			writer.WriteElementString(DTD.Error.TagError, string.Empty);

			XMLAdaptorOIDSerializer.Serialize(writer, serviceResponse.Oid);
			XMLArgumentsSerializer.Serialize(writer, serviceResponse.Arguments);

			writer.WriteEndElement();
			return writer;
		}
		#endregion Serialize ServiceRequest.


		#region Deserialize Request.

		#region over load Method.
		public static ServiceResponse Deserialize(string xmlResponse)
		{
			return Deserialize(xmlResponse, null);
		}
		public static ServiceResponse Deserialize(string xmlString, ServiceResponse serviceResponse)
		{
			return Deserialize(new XmlTextReader(new StringReader(xmlString)), serviceResponse);
		}
		public static ServiceResponse Deserialize(XmlReader reader)
		{
			return Deserialize(reader, null);
		}
		#endregion over load Method.

		public static ServiceResponse Deserialize(XmlReader reader, ServiceResponse serviceResponse)
		{
			if (reader.IsStartElement(DTD.Response.TagServiceResponse))
			{
				if (serviceResponse == null)
				{
					serviceResponse = new ServiceResponse();
				}

				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();
					do
					{
						#region <ERROR>
						if (reader.IsStartElement(DTD.Response.ServiceResponse.TagError))
						{
							if(int.Parse(reader.GetAttribute(DTD.Error.TagNumber))!=0)
							{
								throw XMLErrorSerializer.Deserialize(reader.ReadSubtree());
							}
							else
							{
								reader.Skip();
							}
						}
						#endregion <ERROR>

						#region <OID>
						if (reader.IsStartElement(DTD.TagOID))
						{
							serviceResponse.Oid = XMLAdaptorOIDSerializer.Deserialize(reader.ReadSubtree());
						}
						#endregion <OID>

						#region <Arguments>
						if (reader.IsStartElement(DTD.Response.ServiceResponse.TagArguments))
						{
							serviceResponse.Arguments = XMLArgumentsSerializer.Deserialize(reader.ReadSubtree());
						}
						#endregion <Arguments>
					} while (reader.Read());
				}
				else
				{
					reader.Skip();
				}
			}
			else
			{
				throw new ArgumentException("Xml Reader don't have the Service.Response in Start Element.", "XmlReader reader");
			}
			return serviceResponse;
		}
		#endregion Deserialize Request.
	}
	#endregion XML Service Response Serializer/Deserializer.
}

