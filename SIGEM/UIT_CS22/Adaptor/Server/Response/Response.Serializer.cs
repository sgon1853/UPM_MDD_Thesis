// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace SIGEM.Client.Adaptor.Serializer
{
	#region XML Response Serializer/Deserializer.
	internal static class XMLResponseSerializer
	{
		#region Deserialize over load Methods.
		public static Response Deserialize(string xmlResponse)
		{
			return Deserialize(xmlResponse, null);
		}
		public static Response Deserialize(string xmlString, Response response)
		{
			return Deserialize(new XmlTextReader(new StringReader(xmlString)), response);
		}
		public static Response Deserialize(XmlReader reader)
		{
			return Deserialize(reader, null);
		}
		#endregion Deserialize over load Methods.

		#region Deserialize <Response>.
		public static Response Deserialize(XmlReader reader, Response response)
		{
			if (reader.IsStartElement(DTD.TagResponse))
			{
				if (response == null)
				{
					response = new Response();
				}

				response.Sequence = uint.Parse(reader.GetAttribute(DTD.Response.TagSequence));
				response.VersionDTD = reader.GetAttribute(DTD.Response.TagVersionDTD);

				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();
					do
					{
						#region Process of <Statitistics>.
						if (reader.IsStartElement(DTD.Response.TagStatistics))
						{
							response.Statistics = XMLStatisticsSerializer.Deserialize(reader.ReadSubtree(), response.Statistics);
						}
						#endregion Process of <Statitistics>.
						else
						{
							#region Process of <Ticket>.
							if (reader.IsStartElement(DTD.Response.TagTicket))
							{
								response.Ticket = reader.ReadString();
							}
							#endregion Process of <Ticket>.
							else
							{
								#region Process of <Service.Response>.
								if (reader.IsStartElement(DTD.Response.TagServiceResponse))
								{
									response.ServerResponse = XMLServiceResponseSerializer.Deserialize(reader.ReadSubtree(), response.Service);
								}
								#endregion Process of <Service.Response>.
								else
								{
									#region Process of <Query.Response>.
									if (reader.IsStartElement(DTD.Response.TagQueryResponse))
									{
										response.ServerResponse = XmlQueryResponseSerializer.Deserialize(reader.ReadSubtree(), response.Query);
									}
									#endregion Process of <Query.Response>.
									else
									{
										#region Process of <?>.
										reader.Skip();
										if (reader.NodeType == XmlNodeType.None)
										{
											break;
										}
										else
										{
											continue;
										}
										#endregion Process of <?>.
									}
								}
							}
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
				throw new ArgumentException("Xml Reader don't have the Response in Start Element.", "XmlReader reader");
			}
			return response;
		}
		#endregion Deserialize <Response>.
	}
	#endregion XML Response Deserializer.
}

