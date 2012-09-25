// 3.4.4.5

using System;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using SIGEM.Business.Attributes;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Exceptions;
using SIGEM.Business.Server;
using SIGEM.Business.Query;
using SIGEM.Business.Instance;
using SIGEM.Business.Collection;

namespace SIGEM.Business.XML
{
	/// <summary>
	/// Superclass of XMLs
	/// </summary>
	internal abstract class ONXml
	{
		#region Members
		// Name of the class
		public string ClassName;
		// Default format
		public static CultureInfo ComXMLFormat = new CultureInfo("en-US");
		#endregion

		#region Constants
		public const string XMLTAG_NULL = "Null";
		public const string XMLTAG_LITERAL = "Literal";
		public const string XMLTAG_OID = "OID";
		public const string XMLTAG_OIDFIELD = "OID.Field";
		public const string XMLTAG_ARGUMENTS = "Arguments";
		public const string XMLTAG_ARGUMENT = "Argument";
		public const string XMLTAG_V = "V";
		public const string XMLTAG_O = "O";

		public const string XMLATT_CLASS = "Class";
		public const string XMLATT_NAME = "Name";
		public const string XMLATT_TYPE = "Type";

		public const string XMLTAG_CHANGEDETECTIONITEMS = "ChangeDetectionItems";
		public const string XMLTAG_CHANGEDETECTIONITEM = "ChangeDetectionItem";
		public const string XMLTAG_CHANGEDITEMS = "ChangedItems";
		public const string XMLTAG_CHANGEDITEM = "ChangedItem";
		public const string XMLTAG_CHANGEDITEMOLDVALUE = "ChangedItemOldValue";
		public const string XMLTAG_CHANGEDITEMNEWVALUE = "ChangedItemNewValue";
		
		public const string XMLATT_NAME_DTD20 = "name";

		public const string XMLTAG_FILTERVARIABLES = "Filter.Variables";
		public const string XMLTAG_FILTERVARIABLE = "Filter.Variable";
		public const int LENGTHPASSWORD = 30;
		public const string XMLTAG_ALTERNATEKEY = "AlternateKey";
		public const string XMLTAG_ALTERNATEKEYFIELD = "AlternateKey.Field";
		#endregion

		#region Properties
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public ONXml(string className)
		{
			ClassName = className;
		}
		#endregion

		#region XMLRequest
		/// <summary>
		/// Treatment of the XML message that has a service request
		/// </summary>
		/// <param name="agentOID">OID of the agent connected to the system</param>
		/// <param name="xmlReader">Variable with the message XML to be treated</param>
		/// <param name="xmlWriter">Variable with the message XML to response</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		public void XMLRequestService(ref string ticket, ONOid agentOID, XmlTextReader xmlReader, out XmlTextWriter xmlWriter, double dtdVersion, string clientName)
		{
			try
			{
				// Read Response
				string lService = xmlReader.GetAttribute("Service");
				xmlReader.ReadStartElement("Service.Request");

				// Create XMLWriterRequest
				MemoryStream lXMLMemoryStream = new MemoryStream();
				xmlWriter = new XmlTextWriter(lXMLMemoryStream, new UTF8Encoding());

				MemoryStream lXMLMemoryStreamService = new MemoryStream();
				XmlTextWriter lXmlWriterService = new XmlTextWriter(lXMLMemoryStreamService, new UTF8Encoding());

				// Set Service Parameters
				object[] lParameters = new object[6];
				lParameters[0] = ticket;
				lParameters[1] = agentOID;
				lParameters[2] = xmlReader;
				lParameters[3] = lXmlWriterService;
				lParameters[4] = dtdVersion;
				lParameters[5] = clientName;

				// Execute
				ONContext.InvoqueMethod(this, typeof(ONServiceXMLAttribute), "<Service>" + lService + "</Service>", lParameters);
				ticket = lParameters[0] as string;

				// Read Request
				xmlReader.ReadEndElement(); // Service.Request
				xmlReader.ReadEndElement(); // Request

				// Write Response
				xmlWriter.WriteStartElement("Service.Response");
				xmlWriter.WriteStartElement("Error");
				xmlWriter.WriteAttributeString("Type", "Model");
				xmlWriter.WriteAttributeString("Number", "0");
				if (dtdVersion > 2)
				{
					xmlWriter.WriteElementString("Error.Message","");
					xmlWriter.WriteElementString("Error.Params", "");
					xmlWriter.WriteElementString("Error.Trace","");
				}
				xmlWriter.WriteEndElement(); // Error

				// Write Outbound Arguments
				lXmlWriterService.Flush();
				lXMLMemoryStreamService.Flush();
				lXMLMemoryStreamService.Position = 0;
				xmlWriter.WriteRaw(new StreamReader(lXMLMemoryStreamService).ReadToEnd());

				// Write Response
				xmlWriter.WriteEndElement(); // Service.Response
			}
			catch (ONException e)
			{
				MemoryStream lXMLMemoryStream = new MemoryStream();
				xmlWriter = new XmlTextWriter(lXMLMemoryStream, new UTF8Encoding());

				// Write Response
				xmlWriter.WriteStartElement("Service.Response");
				CreateXMLError(xmlWriter, e, dtdVersion);
				xmlWriter.WriteEndElement(); // Service.Response

				return;
			}
			catch (Exception e)
			{
                string message = e.Message;
               	MemoryStream lXMLMemoryStream = new MemoryStream();
				xmlWriter = new XmlTextWriter(lXMLMemoryStream, new UTF8Encoding());

				// Write Response
				xmlWriter.WriteStartElement("Service.Response");
				xmlWriter.WriteStartElement("Error");
				xmlWriter.WriteAttributeString("Type", "External");
				xmlWriter.WriteAttributeString("Number", "999");
				if (dtdVersion <= 2)
				{
					xmlWriter.WriteString(e.Message);
				}
				else
				{
                    if (e is ONSystemException)
                    {
                        ArrayList trace = ((ONSystemException)e).mTraceInformation;
                        xmlWriter.WriteElementString("Error.Message", message);
                        xmlWriter.WriteStartElement("Error.Trace");
                        int length = trace.Count;
                        int i = 0;
                        while (length > 0)
                        {
                            string mensaje = (string)trace[length - 1];
                            xmlWriter.WriteStartElement("Error.TraceItem");
                            xmlWriter.WriteAttributeString("Type", "External");
                            xmlWriter.WriteAttributeString("Number", i.ToString());
                            xmlWriter.WriteStartElement("Error.Message");
                            xmlWriter.WriteString(mensaje);
                            xmlWriter.WriteEndElement(); // Error.Message
                            xmlWriter.WriteElementString("Error.Params", "");
                            xmlWriter.WriteEndElement(); // Error.TraceItem
                            i += 1;
                            length -= 1;
                        }
                        xmlWriter.WriteEndElement(); // Error.Trace
                    }
                    else
                    {
                        xmlWriter.WriteElementString("Error.Message", message);
                        xmlWriter.WriteElementString("Error.Params", "");
                        xmlWriter.WriteElementString("Error.Trace", "");
                    }
				}

				xmlWriter.WriteEndElement(); // Error
				xmlWriter.WriteEndElement(); // Service.Response

				return;
			}
		}
		/// <summary>
		/// Treatment of the XML message that has a query request
		/// </summary>
		/// <param name="agentOID">OID of the agent connected to the system</param>
		/// <param name="xmlReader">Variable with the message XML to be treated</param>
		/// <param name="xmlWriter">Variable with the message XML to response</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		public void XMLRequestQuery(ref string ticket, ONOid agentOID, XmlTextReader xmlReader, out XmlTextWriter xmlWriter, double dtdVersion, string clientName)
		{
			try
			{
				// Read Request
				ONDisplaySet lDisplaySet;
				using (ONContext lOnContext = new ONContext())
				{
					lOnContext.OidAgent = agentOID;
					lDisplaySet = new ONDisplaySet(ClassName, xmlReader.GetAttribute("DisplaySet"), lOnContext.LeafActiveAgentFacets);
				}
				ONDisplaySet lDSRequested = new ONDisplaySet(lDisplaySet);
				xmlReader.ReadStartElement("Query.Request");

				// Create XMLWriterRequest
				MemoryStream lXMLMemoryStream = new MemoryStream();
				xmlWriter = new XmlTextWriter(lXMLMemoryStream, new UTF8Encoding());

				// Write Response
				xmlWriter.WriteStartElement("Query.Response");

				// Get start row & block size
				ONCollection lCollection = null;
				ONOid lStartRowOID = null;
				int lBlockSize = 1;

				// Execute
				if (xmlReader.IsStartElement("Query.Instance")) // QueryByOID
				{
					// Read Request
					xmlReader.ReadStartElement("Query.Instance");
					if (xmlReader.IsStartElement(ONXml.XMLTAG_ALTERNATEKEY))
						lCollection = QueryByAlternateKey(ref ticket, ref agentOID, xmlReader, lDisplaySet, dtdVersion, clientName);
					else
						lCollection = QueryByOid(ref ticket, ref agentOID, xmlReader, lDisplaySet, dtdVersion, clientName);
				}
				else if (xmlReader.IsStartElement("Query.Related")) // QueryByRelated
					lCollection = QueryByRelated(ref ticket, ref agentOID, xmlReader, lDisplaySet, out lStartRowOID, out lBlockSize, dtdVersion, clientName);
				else if (xmlReader.IsStartElement("Query.Filter")) // QueryByFilter
				{
					// Read Request
					string lFilterName = xmlReader.GetAttribute("Name");

					object[] ParametersList = new object[8];
					ParametersList[0] = ticket;
					ParametersList[1] = agentOID;
					ParametersList[2] = xmlReader;
					ParametersList[3] = lDisplaySet;
					ParametersList[4] = lStartRowOID;
					ParametersList[5] = lBlockSize;
					ParametersList[6] = dtdVersion;
					ParametersList[7] = clientName;
					// Execute
					lCollection = ONContext.InvoqueMethod(this, typeof (ONFilterXMLAttribute), "<Filter>" + lFilterName + "</Filter>", ParametersList) as ONCollection;
					ticket = ParametersList[0] as string;
					agentOID = ParametersList[1] as ONOid;
					lStartRowOID = ParametersList[4] as ONOid;
					lBlockSize = (int) ParametersList[5];
				}

				// Generate response
				ONQuery2XML(agentOID, xmlWriter, lCollection, lStartRowOID, lBlockSize, lDSRequested, dtdVersion);
				lCollection.Dispose();

				// Read Request
				xmlReader.ReadEndElement(); // Query.Request

				// Write Response
				xmlWriter.WriteEndElement(); // Query.Response

			}
			catch (ONException e)
			{
			
				MemoryStream lXMLMemoryStream = new MemoryStream();
				xmlWriter = new XmlTextWriter(lXMLMemoryStream, new UTF8Encoding());

				// Write Response
				xmlWriter.WriteStartElement("Query.Response");
				CreateXMLError(xmlWriter, e, dtdVersion);
				xmlWriter.WriteEndElement(); // Query.Response

				return;
			}
			catch (Exception e)
			{
				string message = e.Message;

				MemoryStream lXMLMemoryStream = new MemoryStream();
				xmlWriter = new XmlTextWriter(lXMLMemoryStream, new UTF8Encoding());

				// Write Response
				xmlWriter.WriteStartElement("Query.Response");
				xmlWriter.WriteStartElement("Error");
				xmlWriter.WriteAttributeString("Type", "External");
				xmlWriter.WriteAttributeString("Number", "999");
				if (dtdVersion <= 2)
				{
					xmlWriter.WriteString(e.Message);
				}
				else
				{
                     if ((e is ONSystemException) || (e.InnerException is ONSystemException))
                    {
                        ONSystemException lException = null;
                        if (e is ONSystemException)
                            lException = e as ONSystemException;
                        else
                            lException = e.InnerException as ONSystemException;
                        ArrayList trace = lException.mTraceInformation;
                        xmlWriter.WriteElementString("Error.Message", message);
                        xmlWriter.WriteElementString("Error.Params", "");
                        xmlWriter.WriteStartElement("Error.Trace");
                        int length = trace.Count;
                        int i = 0;
                        while (length > 0)
                        {
                            string mensaje = (string)trace[length - 1];
                            xmlWriter.WriteStartElement("Error.TraceItem");
                            xmlWriter.WriteAttributeString("Type", "External");
                            xmlWriter.WriteAttributeString("Number", i.ToString());
                            xmlWriter.WriteStartElement("Error.Message");
                            xmlWriter.WriteString(mensaje);
                            xmlWriter.WriteEndElement(); // Error.Message
                            xmlWriter.WriteElementString("Error.Params", "");
                            xmlWriter.WriteEndElement(); // Error.TraceItem
                            i += 1;
                            length -= 1;
                        }
                        xmlWriter.WriteEndElement(); // Error.Trace
                    }
                    else
                    {
                        xmlWriter.WriteElementString("Error.Message", message);
                        xmlWriter.WriteElementString("Error.Params", "");
                        xmlWriter.WriteElementString("Error.Trace", "");
                    }
				}
				
				xmlWriter.WriteEndElement(); // Error
				xmlWriter.WriteEndElement(); // Query.Response

				return;
			}
		}
		#endregion

		#region Auxiliar
		/// <summary>
		/// Treatment the part of the XML message that has links items
		/// </summary>
		/// <param name="xmlReader">Variable with the message XML to be treated</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		public ONLinkedToList GetLinkedTo(XmlReader xmlReader, double dtdVersion)
		{
			ONLinkedToList lLinkedToList = new ONLinkedToList();

			if (!xmlReader.IsStartElement("LinkedTo"))
				return lLinkedToList;

			if (xmlReader.IsEmptyElement)
			{
				xmlReader.ReadStartElement("LinkedTo");
				return lLinkedToList;
			}

			xmlReader.ReadStartElement("LinkedTo");
			while (xmlReader.IsStartElement("Link.Item"))
			{
				ONPath lPath = new ONPath(xmlReader.GetAttribute("Role"));
				xmlReader.ReadStartElement("Link.Item");

				object[] lParam = new object[2];
				lParam[0] = xmlReader;
				lParam[1] = dtdVersion;

				string lClassInstance = xmlReader.GetAttribute("Class");
				ONOid lInstance = ONContext.InvoqueMethod(ONContext.GetType_XML(lClassInstance), "XML2ON", lParam) as ONOid;

				lLinkedToList[lPath] = lInstance;

				xmlReader.ReadEndElement(); // Link.Item
			}
			xmlReader.ReadEndElement(); // LinkedTo

			return lLinkedToList;
		}
		/// <summary>
		/// Checks if the value has only digits
		/// </summary>
		/// <param name="stringValue">Data to be checked</param>
		public static bool OnlyDigits(string stringValue)
		{
			for (CharEnumerator lEnum = stringValue.GetEnumerator(); lEnum.MoveNext() == true; )
			{
				if (lEnum.Current < '0' || lEnum.Current > '9')
					return false;
			}
			return true;
		}
		/// <summary>
		/// Creates the XML message with the error ocurred during the proccess of the request
		/// </summary>
		/// <param name="xmlReader">Variable with the message XML to be treated</param>
		/// <param name="e">Exception to be treated</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		public static void CreateXMLError(XmlTextWriter xmlWriter, ONException e, double dtdVersion)
		{			
			xmlWriter.WriteStartElement("Error");
			xmlWriter.WriteAttributeString("Type", "Model");
			xmlWriter.WriteAttributeString("Number", e.Code.ToString());
			if (dtdVersion <= 2.0) // Normal error.
				xmlWriter.WriteString(e.Message);
			else // Create the trace error.
			{
				xmlWriter.WriteStartElement("Error.Message");
				xmlWriter.WriteString(e.Message);
				xmlWriter.WriteEndElement(); // Error.Message
				xmlWriter.WriteStartElement("Error.Params"); 

				//The first error.
				if (e.Params.Count > 0)
				{
					foreach(DictionaryEntry lElement in e.Params)
					{
						xmlWriter.WriteStartElement("Error.Param");
						string lKey = lElement.Key.ToString();
						bool lIsLiteral = false;
						if (lKey.StartsWith("*"))
						{
							lKey = lKey.Remove(0,1);
							lIsLiteral = true;
						}
						xmlWriter.WriteAttributeString("Key", lKey);
						if (lIsLiteral)
							xmlWriter.WriteAttributeString("Type", "Literal");
						else
							xmlWriter.WriteAttributeString("Type", "Key");
						if (lElement.Value != null)
							xmlWriter.WriteString(lElement.Value.ToString());
						else
							xmlWriter.WriteString("");
						xmlWriter.WriteEndElement(); // Error.Param
					}
				}

				xmlWriter.WriteEndElement(); // Error.Params
				
				//Next errors (father errors)
				Exception lException = e;
				xmlWriter.WriteStartElement("Error.Trace");
				while (lException != null)
				{
					xmlWriter.WriteStartElement("Error.TraceItem");
					ONException lOnException = lException as ONException;
					if (lOnException != null) //Model errors.
					{
						xmlWriter.WriteAttributeString("Type", "Model");
						xmlWriter.WriteAttributeString("Number", lOnException.OwnCode.ToString());
						xmlWriter.WriteStartElement("Error.Message");
						xmlWriter.WriteString(lOnException.OwnMessage);
						xmlWriter.WriteEndElement(); // End Error.Message
						xmlWriter.WriteStartElement("Error.Params");

						if (lOnException.KeyList.Count > 0)
						{
							foreach(DictionaryEntry lElement in lOnException.KeyList)
							{
								xmlWriter.WriteStartElement("Error.Param");
								string lKey = lElement.Key.ToString();
								bool lIsLiteral = false;
								if (lKey.StartsWith("*"))
								{
									lKey = lKey.Remove(0,1);
									lIsLiteral = true;
								}
								xmlWriter.WriteAttributeString("Key", lKey);
								if (lIsLiteral)
									xmlWriter.WriteAttributeString("Type", "Literal");
								else
									xmlWriter.WriteAttributeString("Type", "Key");
								if (lElement.Value != null)
									xmlWriter.WriteString(lElement.Value.ToString());
								else
									xmlWriter.WriteString("");
								xmlWriter.WriteEndElement(); // Error.Param
							}
						}

						xmlWriter.WriteEndElement(); // Error.Params
					}
					else //System errors
					{
						xmlWriter.WriteAttributeString("Type", "External");
						ErrorWrapper lErrorWrapper = new ErrorWrapper(lException);
						xmlWriter.WriteAttributeString("Number", lErrorWrapper.ErrorCode.ToString());
						xmlWriter.WriteStartElement("Error.Message");
						xmlWriter.WriteString(lException.Message);
						xmlWriter.WriteEndElement(); // End Error.Message
						xmlWriter.WriteElementString("Error.Params", "");
					}

					xmlWriter.WriteEndElement(); // End Error.TraceITem
					lException = lException.InnerException;
				}
				xmlWriter.WriteEndElement(); // End Error.Trace	

				if (e is ONChangeDetectionErrorsException)
				{
					xmlWriter.WriteStartElement(XMLTAG_CHANGEDITEMS);
					foreach (DictionaryEntry lDifference in (e as ONChangeDetectionErrorsException).stateDifferences)
					{
						xmlWriter.WriteStartElement(XMLTAG_CHANGEDITEM);
						xmlWriter.WriteAttributeString(ONXml.XMLATT_NAME, (lDifference.Key as string));
						ONChangeTypeToXML(xmlWriter, (lDifference.Value as ONChangeDetectionInfo), dtdVersion);				
						xmlWriter.WriteEndElement(); // ChangedItem
					}
					xmlWriter.WriteEndElement(); // ChangedItems
				}
			}
			xmlWriter.WriteEndElement(); // Error			
		}

		public static string GetNameTypeForXml(DataTypeEnumerator lType)
		{
			switch (lType)
			{
				case (DataTypeEnumerator.Int):
					return "int";
				case (DataTypeEnumerator.Bool):
					return "bool";
				case (DataTypeEnumerator.Blob):
					return "blob";
				case (DataTypeEnumerator.Date):
					return "date";
				case (DataTypeEnumerator.DateTime):
					return "datetime";
				case (DataTypeEnumerator.Nat):
					return "nat";
				case (DataTypeEnumerator.Real):
					return "real";
				case (DataTypeEnumerator.String):
					return "string";
				case (DataTypeEnumerator.Text):
					return "text";
				case (DataTypeEnumerator.Time):
					return "time";
			}
			return "";
		}

		public static void GetType_XMLSimple(DataTypeEnumerator pType, XmlTextWriter xmlWriter, ONSimpleType pValue, double dtdVersion)
		{
			switch (pType)
			{
				case (DataTypeEnumerator.Int):
					ONXmlInt.ON2XML(xmlWriter, pValue as ONInt, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				case (DataTypeEnumerator.Bool):
					ONXmlBool.ON2XML(xmlWriter, pValue as ONBool, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				case (DataTypeEnumerator.Blob):
					ONXmlBlob.ON2XML(xmlWriter, pValue as ONBlob, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				case (DataTypeEnumerator.Date):
					ONXmlDate.ON2XML(xmlWriter, pValue as ONDate, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				case (DataTypeEnumerator.DateTime):
					ONXmlDateTime.ON2XML(xmlWriter, pValue as ONDateTime, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				case (DataTypeEnumerator.Nat):
					ONXmlNat.ON2XML(xmlWriter, pValue as ONNat, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				case (DataTypeEnumerator.Real):
					ONXmlReal.ON2XML(xmlWriter, pValue as ONReal, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				case (DataTypeEnumerator.String):
					ONXmlString.ON2XML(xmlWriter, pValue as ONString, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				case (DataTypeEnumerator.Text):
					ONXmlText.ON2XML(xmlWriter, pValue as ONText, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				case (DataTypeEnumerator.Time):
					ONXmlTime.ON2XML(xmlWriter, pValue as ONTime, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
			}
		}

		public static void ONChangeTypeToXML(XmlTextWriter xmlWriter, ONChangeDetectionInfo pDifference, double dtdVersion)
		{
			if (pDifference.Type != DataTypeEnumerator.OID)
			{
				xmlWriter.WriteAttributeString(ONXml.XMLATT_TYPE, GetNameTypeForXml(pDifference.Type));
				//Write Old Value
				xmlWriter.WriteStartElement(XMLTAG_CHANGEDITEMOLDVALUE);
				GetType_XMLSimple(pDifference.Type, xmlWriter, pDifference.OldValue as ONSimpleType, dtdVersion);
				xmlWriter.WriteEndElement(); // ChangedItemOldValue

				//Write New Value
				xmlWriter.WriteStartElement(XMLTAG_CHANGEDITEMNEWVALUE);
				GetType_XMLSimple(pDifference.Type, xmlWriter, pDifference.NewValue as ONSimpleType, dtdVersion);
				xmlWriter.WriteEndElement(); // ChangedItemNewValue
			}
			else
			{
				xmlWriter.WriteAttributeString(ONXml.XMLATT_TYPE, pDifference.ClassName);
				//Write Old Value
				xmlWriter.WriteStartElement(XMLTAG_CHANGEDITEMOLDVALUE);

				Type lTypeXML = ONContext.GetType_XML(pDifference.ClassName);
				object[] parameters = new object[4];
				parameters[0] = xmlWriter;
				parameters[1] = pDifference.OldValue;
				parameters[2] = dtdVersion;
				parameters[3] = ONXml.XMLTAG_OIDFIELD;
				ONContext.InvoqueMethod(lTypeXML, "ON2XML", parameters);			
				xmlWriter.WriteEndElement(); // ChangedItemOldValue

				//Write New Value
				xmlWriter.WriteStartElement(XMLTAG_CHANGEDITEMNEWVALUE);
				parameters[1] = pDifference.NewValue;
				ONContext.InvoqueMethod(lTypeXML, "ON2XML", parameters);
				xmlWriter.WriteEndElement(); // ChangedItemNewValue
			}
		}
		#endregion
		
		/// <summary>
		/// Process the XML message that is a search of an instance with their OID. Is empty, it implementation is done in child classes
		/// </summary>
		/// <param name="agentOID">OID of the agent connected to the system</param>
		/// <param name="xmlReader">Variable with the message XML to be treated</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		public virtual ONCollection QueryByOid(ref string ticket, ref ONOid agentOid, XmlReader xmlReader, ONDisplaySet displaySet, double dtdVersion, string clientName)
		{
			return null;
		}
		/// <summary>
		/// Process the XML message that is a search of an instance with an alternate key. Is empty, its implementation is done in child classes
		/// </summary>
		/// <param name="agentOID">OID of the agent connected to the system</param>
		/// <param name="xmlReader">Variable with the message XML to be treated</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		public virtual ONCollection QueryByAlternateKey(ref string ticket, ref ONOid agentOid, XmlReader xmlReader, ONDisplaySet displaySet, double dtdVersion, string clientName)
		{
			return null;
		}
		/// <summary>
		/// Process the XML message that is a search of instances related with an instance.
		/// </summary>
		/// <param name="agentOID">OID of the agent connected to the system</param>
		/// <param name="xmlReader">Variable with the message XML to be treated</param>
		/// <param name="startRowOID">OID to start the searching</param>
		/// <param name="blockSize">Number of instance to be retrieved for the query</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		public virtual ONCollection QueryByRelated(ref string ticket, ref ONOid agentOid, XmlReader xmlReader, ONDisplaySet displaySet, out ONOid startRowOID, out int blockSize, double dtdVersion, string clientName)
		{
			startRowOID = null;
			blockSize = -1;
			return null;
		}
		/// <summary>
		/// Method to convert the data providen by the system to a message with XML format.Is empty, it implementation is done in child classes
		/// </summary>
		/// <param name="agentOID">OID of the agent connected to the system</param>
		/// <param name="xmlWriter">Variable with the message XML to be treated</param>
		/// <param name="val">Variable with the message XML to be treated</param>
		/// <param name="startRowOID">OID to start the searching</param>
		/// <param name="blockSize">Number of instance to be retrieved for the query</param>
		/// <param name="dtdVersion">Version of the DTD that follows the XML message</param>
		public virtual void ONQuery2XML(ONOid agentOID, XmlWriter xmlWriter, ONCollection val, ONOid startRowOID, int blockSize, ONDisplaySet displaySet, double dtdVersion)
		{
		
		}
	}
}
