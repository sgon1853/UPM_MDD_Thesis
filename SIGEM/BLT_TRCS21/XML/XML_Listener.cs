// 3.4.4.5

using System;
using System.Xml;
using System.Text;
using System.IO;
using System.Collections;
using System.EnterpriseServices;
using SIGEM.Business;
using SIGEM.Business.Attributes;
using SIGEM.Business.OID;
using SIGEM.Business.Query;
using SIGEM.Business.Server;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business.XML
{
	/// <summary>
	/// XML Gate
	/// </summary>
	[Transaction(TransactionOption.Supported, Isolation = TransactionIsolationLevel.Any)]
	public class XML_Listener : ServicedComponent
	{
		#region Constructors
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public XML_Listener()
		{
		}
		#endregion

		#region XMLRequest
		/// <summary>
		/// Method to have compatibility with old version fo client side.
		/// </summary>
		/// <param name="xmlRequest">The message with the request in XML format</param>
		public string Peticion(string xmlRequest)
		{
			return XMLRequest(xmlRequest);
		}
		/// <summary>
		/// Point of access to the application.
		/// </summary>
		/// <param name="xmlRequest">The message with the request in XML format</param>
		public string XMLRequest(string xmlRequest)
		{
			// Start Time
			DateTime lStartTime = DateTime.Now;

			// Response Type 
			string lResponseType = "Service.Response";

			// Request Attributes
			string lSecuence = "";
			string lDTDVersionResponse = "";
			double lDTDVersion = 0;

			try
			{
				// Create XMLReader
				XmlTextReader lXMLReader = new XmlTextReader(new StringReader(xmlRequest));
				lXMLReader.WhitespaceHandling = WhitespaceHandling.None;
				lXMLReader.MoveToContent();

				// Read Attributes
				string lApplication = lXMLReader.GetAttribute("Application");
				string lIdCnx = lXMLReader.GetAttribute("IdCnx");
				lSecuence = lXMLReader.GetAttribute("Sequence");
				lDTDVersion = ExtractDTDVersion(lXMLReader.GetAttribute("VersionDTD"));
				if (lDTDVersion >= 3.0)
					lDTDVersionResponse = "CARE.Response v. " + lDTDVersion.ToString("#.00", ONXml.ComXMLFormat);
				else
					lDTDVersionResponse = "CARE.Response v." + lDTDVersion.ToString("#.0", ONXml.ComXMLFormat);
				lXMLReader.ReadStartElement("Request");

				// Check Secure server
				if ((ONSecureControl.SecureServer) && (lDTDVersion < 3.1))
					throw new ONAgentValidationException(null);

				// Create XMLWriter
				MemoryStream lXMLMemoryStream = new MemoryStream();
				XmlTextWriter lXMLWriter = new XmlTextWriter(lXMLMemoryStream, new System.Text.UTF8Encoding());
				lXMLWriter.WriteStartDocument();

				// Create XMLWriterRequest
				XmlTextWriter lXMLWriterRequest = null;

				// Load agent
				ONOid lAgentOID = GetAgent(lXMLReader, lDTDVersion);
				string lTicket = "";
				if (lDTDVersion >= 3.1)
					lTicket = GetTicket(lXMLReader, lDTDVersion);

				lStartTime = DateTime.Now;
				if (lXMLReader.IsStartElement("Service.Request"))
				{
					string lService = lXMLReader.GetAttribute("Service");
					if ((lDTDVersion >= 3.1) && (string.Compare(lService, "MVAgentValidation", true) != 0)) 
					{
						using (ONContext lOnContext = new ONContext())
						{
							if (((object) lAgentOID == null) || !lAgentOID.IsAnonymousAgent)
							{
								ONOid lAgentOid = lOnContext.SetTicket(lDTDVersion, lApplication, lTicket);
								if (ONSecureControl.SecureServer)
									if (lAgentOID != lAgentOid)
										throw new ONAgentValidationException(null);
							}
						}
					}

					// Response Type 
					lResponseType = "Service.Response";

					// Read Attributes
					string lClass = lXMLReader.GetAttribute("Class");

					// Global services
					if ((lClass == "") || (lClass == "InteraccionGlobal") || (lClass == "GlobalTransactions"))
						lClass = "GlobalTransaction";

					ONXml lComponent = ONContext.GetComponent_XML(lClass);
					lComponent.XMLRequestService(ref lTicket, lAgentOID, lXMLReader, out lXMLWriterRequest, lDTDVersion, lApplication);
				}
				else if (lXMLReader.IsStartElement("Query.Request"))
				{
					if (lDTDVersion >= 3.1)
					{
						using (ONContext lOnContext = new ONContext())
						{
							if (((object) lAgentOID == null) || !lAgentOID.IsAnonymousAgent)
							{
								ONOid lAgentOid = lOnContext.SetTicket(lDTDVersion, lApplication, lTicket);
								if (ONSecureControl.SecureServer)
									if (lAgentOID != lAgentOid)
										throw new ONAgentValidationException(null);
							}
						}
					}

					// Response Type 
					lResponseType = "Query.Response";

					// Read Attributes
					string lClass = lXMLReader.GetAttribute("Class");

					ONXml lComponent = ONContext.GetComponent_XML(lClass);
					lComponent.XMLRequestQuery(ref lTicket, lAgentOID, lXMLReader, out lXMLWriterRequest, lDTDVersion, lApplication);
				}
				else
					throw new ONXMLException(null, "Request type");

				// Get Time
				DateTime lEndTime = DateTime.Now;
				double lElapsedTime = (lEndTime.Ticks - lStartTime.Ticks) / 10000;
				
				// Write Response
				lXMLWriter.WriteStartElement("Response");
				lXMLWriter.WriteAttributeString("Sequence", lSecuence);
				lXMLWriter.WriteAttributeString("VersionDTD", lDTDVersionResponse);

				lXMLWriter.WriteStartElement("Statistics");
				lXMLWriter.WriteAttributeString("StartTime", lStartTime.ToString().Trim());
				lXMLWriter.WriteAttributeString("EndTime", lEndTime.ToString().Trim());
				lXMLWriter.WriteAttributeString("ElapsedTime", lElapsedTime.ToString().Trim());
				lXMLWriter.WriteEndElement(); // Statistics

				if (lDTDVersion >= 3.1)
					lXMLWriter.WriteElementString("Ticket", lTicket);

				// Write Query / Service XML
				MemoryStream lXMLMemoryStreamRequest = lXMLWriterRequest.BaseStream as MemoryStream;
				lXMLWriter.Flush();
				lXMLWriterRequest.Flush();
				lXMLMemoryStreamRequest.Flush();
				lXMLMemoryStreamRequest.Position = 0;
				lXMLMemoryStreamRequest.WriteTo(lXMLMemoryStream);

				lXMLWriter.WriteEndElement(); // Response

				// Get Response
				lXMLWriter.Flush();
				lXMLMemoryStream.Flush();
				lXMLMemoryStream.Position = 0;
				StreamReader lXMLStreamReader = new StreamReader(lXMLMemoryStream);
				string lResponse = lXMLStreamReader.ReadToEnd();

				// Close Writer
				lXMLWriterRequest.Close();
				lXMLWriter.Close();

				// Close Reader
				lXMLReader.Close();
				return lResponse;
			}
			catch (Exception e)
			{
				// Create XMLWriter
				MemoryStream lXMLMemoryStream = new MemoryStream();
				XmlTextWriter lXMLWriter = new XmlTextWriter(lXMLMemoryStream, new System.Text.UTF8Encoding());
				lXMLWriter.WriteStartDocument();

				// Write Response
				lXMLWriter.WriteStartElement("Response");
				lXMLWriter.WriteAttributeString("Sequence", lSecuence);
				lXMLWriter.WriteAttributeString("VersionDTD", lDTDVersionResponse);

				// Get Time
				DateTime lEndTime = DateTime.Now;
				double lElapsedTime = (lEndTime.Ticks - lStartTime.Ticks) / 10000;
				
				// Write Statistics
				lXMLWriter.WriteStartElement("Statistics");
				lXMLWriter.WriteAttributeString("StartTime", lStartTime.ToString().Trim());
				lXMLWriter.WriteAttributeString("EndTime", lEndTime.ToString().Trim());
				lXMLWriter.WriteAttributeString("ElapsedTime", lElapsedTime.ToString().Trim());
				lXMLWriter.WriteEndElement(); // Statistics

				// Write ServiceResponse / QueryResponse
				lXMLWriter.WriteStartElement(lResponseType);
				if (lDTDVersion > 2)
				{
					if (e is ONException)
						ONXml.CreateXMLError(lXMLWriter, e as ONException, lDTDVersion);
					else
					{
						lXMLWriter.WriteStartElement("Error");
						lXMLWriter.WriteAttributeString("Type", "Model");
						lXMLWriter.WriteAttributeString("Number", "999");
						lXMLWriter.WriteElementString("Error.Message","");
						lXMLWriter.WriteElementString("Error.Params", "");
						lXMLWriter.WriteElementString("Error.Trace","");
						lXMLWriter.WriteEndElement(); // Error
					}
				}
				else 
				{
					lXMLWriter.WriteStartElement("Error");
					lXMLWriter.WriteAttributeString("Type", "External");
					lXMLWriter.WriteAttributeString("Number", "999");				
					lXMLWriter.WriteString(e.Message);
					lXMLWriter.WriteEndElement(); // Error
				}
				lXMLWriter.WriteEndElement(); // Service.Response / Query.Response

				// Write Response
				lXMLWriter.WriteEndElement(); // Response

				// Get Response
				lXMLWriter.Flush();
				lXMLMemoryStream.Flush();
				lXMLMemoryStream.Position = 0;
				StreamReader lXMLStreamReader = new StreamReader(lXMLMemoryStream);
				string lResponse = lXMLStreamReader.ReadToEnd();

				// Close Writer
				lXMLWriter.Close();

				return lResponse;
			}
		}
		#endregion

		#region Auxiliars
		/// <summary>
		/// Gets the Agent of the XML mesage
		/// </summary>
		/// <param name="xmlReader">The message with the request in XML format</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		private ONOid GetAgent(XmlTextReader xmlReader, double dtdVersion)
		{
			ONOid lAgentOID = null;

			if ((dtdVersion < 3.1) || (xmlReader.IsStartElement("Agent")))
			{
				object[] lParam = new object[2];
				lParam[0] = xmlReader;
				lParam[1] = dtdVersion;

				xmlReader.ReadStartElement("Agent");
				string lAgentClass = xmlReader.GetAttribute("Class");
				lAgentOID = ONContext.InvoqueMethod(ONContext.GetType_XML(lAgentClass), "XML2ON", lParam) as ONOid;
				xmlReader.ReadEndElement(); // Agent
			}

			return (lAgentOID);
		}
		/// <summary>
		/// Gets the Agent of the XML mesage
		/// </summary>
		/// <param name="xmlReader">The message with the request in XML format</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		private string GetTicket(XmlTextReader xmlReader, double dtdVersion)
		{
			if (xmlReader.IsStartElement("Ticket"))
				return xmlReader.ReadElementString("Ticket");
			else
				return "";
		}
		/// <summary>
		/// Gets the version of the DTD that has the XML message
		/// </summary>
		/// <param name="dtdVersion">Part the XML message that contains the Version of the DTD</param>
		private double ExtractDTDVersion(string dtdVersion)
		{
			// Default version
			if (dtdVersion == null)
				return 1.3;

			// Extract DTD version 
			System.Globalization.NumberFormatInfo numberFormatInfo = ONXml.ComXMLFormat.NumberFormat;
			numberFormatInfo.NumberDecimalSeparator = ".";
			numberFormatInfo.NumberGroupSeparator = " ";

			int lIndex = dtdVersion.IndexOf("v.") + 2;
			return Convert.ToDouble(dtdVersion.Substring(lIndex, dtdVersion.Length - lIndex), numberFormatInfo);
		}
		#endregion
	}
}

