// 3.4.4.5
using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Instance;
using SIGEM.Business.Collection;
using SIGEM.Business.Query;
using SIGEM.Business.Attributes;
using SIGEM.Business.Exceptions;
using SIGEM.Business.Server;
using SIGEM.Business.Data;

namespace SIGEM.Business.XML
{
	/// <summary>
	/// This class extracts from the XML the appropiatte information to solve the Request
	/// </summary>
	[ONGateXMLAttribute("Revision")]
	internal class RevisionXml : ONXml
	{
		#region Properties
		public static RevisionOid Null
		{
			get
			{
				RevisionOid lNull = new RevisionOid();
				return lNull;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public  RevisionXml() : base("Revision")
		{
		}
		#endregion

		#region Service "New"
		/// <summary>
		/// Extracts the information needed to process the execution of the service: create_instance
		/// </summary>
		/// <param name="agentOid">OID with the agent connected to the system</param>
		/// <param name="xmlReader">XML with the request message</param>
		/// <param name="xmlWriter">This parameter has the response message XML</param>
		/// <param name="dtdVersion">Version of DTD that follows the XML message</param>
		[ONServiceXML("create_instance")]
		public void Create_instanceServ(ref string ticket, ref ONOid agentOid, XmlReader xmlReader, XmlWriter xmlWriter, double dtdVersion, string clientName)
		{
			// Process the service arguments
			ONServiceInfo lSInfo = new ONServiceInfo("Clas_1348178542592347Ser_1_Alias", RevisionClassText.Create_instanceServiceAlias, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias);
			lSInfo.AddDataValuedArgument("p_atrid_RevisarAeronave", false, DataTypeEnumerator.Autonumeric, 0, "Clas_1348178542592347Ser_1Arg_1_Alias", RevisionClassText.Create_instance_P_atrid_RevisarAeronaveArgumentAlias);
			lSInfo.AddDataValuedArgument("p_atrFechaRevision", false, DataTypeEnumerator.Date, 0, "Clas_1348178542592347Ser_1Arg_3_Alias", RevisionClassText.Create_instance_P_atrFechaRevisionArgumentAlias);
			lSInfo.AddDataValuedArgument("p_atrNombreRevisor", false, DataTypeEnumerator.String, 100, "Clas_1348178542592347Ser_1Arg_4_Alias", RevisionClassText.Create_instance_P_atrNombreRevisorArgumentAlias);
			lSInfo.AddDataValuedArgument("p_atrId_Aeronave", false, DataTypeEnumerator.String, 100, "Clas_1348178542592347Ser_1Arg_5_Alias", RevisionClassText.Create_instance_P_atrId_AeronaveArgumentAlias);

			try
			{
				lSInfo.XML2ON(xmlReader, dtdVersion, true);
			}
			catch (Exception e)
			{
				throw new ONServiceArgumentsException(e, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias, "Clas_1348178542592347Ser_1_Alias", RevisionClassText.Create_instanceServiceAlias);
			}

			ONInt lP_atrid_RevisarAeronaveArg = (ONInt) ((ONArgumentInfo) lSInfo.mArgumentList["p_atrid_RevisarAeronave"]).Value;
			ONDate lP_atrFechaRevisionArg = (ONDate) ((ONArgumentInfo) lSInfo.mArgumentList["p_atrFechaRevision"]).Value;
			ONString lP_atrNombreRevisorArg = (ONString) ((ONArgumentInfo) lSInfo.mArgumentList["p_atrNombreRevisor"]).Value;
			ONString lP_atrId_AeronaveArg = (ONString) ((ONArgumentInfo) lSInfo.mArgumentList["p_atrId_Aeronave"]).Value;
			
			// Create Context
			ONServiceContext lOnContext = new ONServiceContext();
			lOnContext.OidAgent = agentOid;
	
			// Execute Service
			RevisionInstance lInstance = null;
			try
			{
				using (RevisionServer lServer = new RevisionServer(lOnContext, null))
				{	
					lServer.Create_instanceServ(lP_atrid_RevisarAeronaveArg,lP_atrFechaRevisionArg,lP_atrNombreRevisorArg,lP_atrId_AeronaveArg);
					lInstance = lServer.Instance;
				}	
				ticket = lOnContext.GetTicket(dtdVersion, clientName);
			}
			catch (SecurityException)
			{
				throw new ONAccessAgentValidationException(null);
			}
			catch
			{
				throw;
			}
			
			// Write Oid
			if (dtdVersion >= 3.1)
			{
				if (lInstance != null)
					ON2XML(xmlWriter, lInstance.Oid, dtdVersion, ONXml.XMLTAG_OIDFIELD);
			}			
			
			// Write Outbound Arguments
			xmlWriter.WriteStartElement("Arguments");
			// Write Outbound Arguments
			xmlWriter.WriteEndElement(); // Arguments

		}

		#endregion

		#region Service "Destroy"
		/// <summary>
		/// Extracts the information needed to process the execution of the service: delete_instance
		/// </summary>
		/// <param name="agentOid">OID with the agent connected to the system</param>
		/// <param name="xmlReader">XML with the request message</param>
		/// <param name="xmlWriter">This parameter has the response message XML</param>
		/// <param name="dtdVersion">Version of DTD that follows the XML message</param>
		[ONServiceXML("delete_instance")]
		public void Delete_instanceServ(ref string ticket, ref ONOid agentOid, XmlReader xmlReader, XmlWriter xmlWriter, double dtdVersion, string clientName)
		{
			// Process the service arguments
			ONServiceInfo lSInfo = new ONServiceInfo("Clas_1348178542592347Ser_2_Alias", RevisionClassText.Delete_instanceServiceAlias, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias);
			lSInfo.AddOIDArgument("p_thisRevisarAeronave", false, "Revision", "Clas_1348178542592347Ser_2Arg_1_Alias", RevisionClassText.Delete_instance_P_thisRevisarAeronaveArgumentAlias);

			try
			{
				lSInfo.XML2ON(xmlReader, dtdVersion, true);
			}
			catch (Exception e)
			{
				throw new ONServiceArgumentsException(e, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias, "Clas_1348178542592347Ser_2_Alias", RevisionClassText.Delete_instanceServiceAlias);
			}

			RevisionOid lP_thisRevisarAeronaveArg = (RevisionOid) ((ONArgumentInfo) lSInfo.mArgumentList["p_thisRevisarAeronave"]).Value;
			
			// Create Context
			ONServiceContext lOnContext = new ONServiceContext();
			lOnContext.OidAgent = agentOid;
	
			// Execute Service
			RevisionInstance lInstance = null;
			try
			{
                ONFilterList lFilterList = new ONFilterList();
				RevisionInstance lThisInstance = new RevisionInstance(lOnContext);
				if (lP_thisRevisarAeronaveArg != null)
				{
					lFilterList = new ONFilterList();
					lFilterList.Add("HorizontalVisibility", new RevisionHorizontalVisibility());
					lThisInstance = lP_thisRevisarAeronaveArg.GetInstance(lOnContext, lFilterList);
					if (lThisInstance == null)
						throw new ONInstanceNotExistException(null, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias);
				}

				using (RevisionServer lServer = new RevisionServer(lOnContext, lThisInstance))
				{	
					lServer.Delete_instanceServ(lP_thisRevisarAeronaveArg);
					lInstance = lServer.Instance;
				}	
				ticket = lOnContext.GetTicket(dtdVersion, clientName);
			}
			catch (SecurityException)
			{
				throw new ONAccessAgentValidationException(null);
			}
			catch
			{
				throw;
			}
			
			// Write Oid
			if (dtdVersion >= 3.1)
			{
				if (lInstance != null)
					ON2XML(xmlWriter, lInstance.Oid, dtdVersion, ONXml.XMLTAG_OIDFIELD);
			}			
			
			// Write Outbound Arguments
			xmlWriter.WriteStartElement("Arguments");
			// Write Outbound Arguments
			xmlWriter.WriteEndElement(); // Arguments

		}

		#endregion

		#region Service "Edit"
		/// <summary>
		/// Extracts the information needed to process the execution of the service: edit_instance
		/// </summary>
		/// <param name="agentOid">OID with the agent connected to the system</param>
		/// <param name="xmlReader">XML with the request message</param>
		/// <param name="xmlWriter">This parameter has the response message XML</param>
		/// <param name="dtdVersion">Version of DTD that follows the XML message</param>
		[ONServiceXML("edit_instance")]
		public void Edit_instanceServ(ref string ticket, ref ONOid agentOid, XmlReader xmlReader, XmlWriter xmlWriter, double dtdVersion, string clientName)
		{
			// Process the service arguments
			ONServiceInfo lSInfo = new ONServiceInfo("Clas_1348178542592347Ser_3_Alias", RevisionClassText.Edit_instanceServiceAlias, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias);
			lSInfo.AddOIDArgument("p_thisRevisarAeronave", false, "Revision", "Clas_1348178542592347Ser_3Arg_1_Alias", RevisionClassText.Edit_instance_P_thisRevisarAeronaveArgumentAlias);

			try
			{
				lSInfo.XML2ON(xmlReader, dtdVersion, true);
			}
			catch (Exception e)
			{
				throw new ONServiceArgumentsException(e, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias, "Clas_1348178542592347Ser_3_Alias", RevisionClassText.Edit_instanceServiceAlias);
			}

			RevisionOid lP_thisRevisarAeronaveArg = (RevisionOid) ((ONArgumentInfo) lSInfo.mArgumentList["p_thisRevisarAeronave"]).Value;
			
			// Create Context
			ONServiceContext lOnContext = new ONServiceContext();
			lOnContext.OidAgent = agentOid;
	
			// Execute Service
			RevisionInstance lInstance = null;
			try
			{
                ONFilterList lFilterList = new ONFilterList();
				RevisionInstance lThisInstance = new RevisionInstance(lOnContext);
				if (lP_thisRevisarAeronaveArg != null)
				{
					lFilterList = new ONFilterList();
					lFilterList.Add("HorizontalVisibility", new RevisionHorizontalVisibility());
					lThisInstance = lP_thisRevisarAeronaveArg.GetInstance(lOnContext, lFilterList);
					if (lThisInstance == null)
						throw new ONInstanceNotExistException(null, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias);
				}

				using (RevisionServer lServer = new RevisionServer(lOnContext, lThisInstance))
				{	
					lServer.Edit_instanceServ(lP_thisRevisarAeronaveArg);
					lInstance = lServer.Instance;
				}	
				ticket = lOnContext.GetTicket(dtdVersion, clientName);
			}
			catch (SecurityException)
			{
				throw new ONAccessAgentValidationException(null);
			}
			catch
			{
				throw;
			}
			
			// Write Oid
			if (dtdVersion >= 3.1)
			{
				if (lInstance != null)
					ON2XML(xmlWriter, lInstance.Oid, dtdVersion, ONXml.XMLTAG_OIDFIELD);
			}			
			
			// Write Outbound Arguments
			xmlWriter.WriteStartElement("Arguments");
			// Write Outbound Arguments
			xmlWriter.WriteEndElement(); // Arguments

		}

		#endregion

		#region QueryByOid
		/// <summary>
		/// Process the request dedicated to loof for an instance with an OID given
		/// </summary>
		/// <param name="agentOid">OID with the agent connected to the system</param>
		/// <param name="xmlReader">XML with the request message</param>
		/// <param name="dtdVersion">Version of DTD that follows the XML message</param>
		public override ONCollection QueryByOid(ref string ticket, ref ONOid agentOid, XmlReader xmlReader, ONDisplaySet displaySet, double dtdVersion, string clientName)
		{
			// Get OID class
			string lClass = xmlReader.GetAttribute("Class");
			
			// Set OID parameter
			ONOid lOID = XML2ON(xmlReader, dtdVersion);
			
			// Read Request
			xmlReader.ReadEndElement(); // Query.Instance
			
			// Read Order Criteria
			if (xmlReader.IsStartElement("Sort")) // Sort
				xmlReader.Skip();
			
            //Read Filter Navigation
            if (xmlReader.IsStartElement("NavFilt"))
				xmlReader.Skip();

			// Create Context
			ONContext lOnContext = new ONContext();
			lOnContext.OidAgent = agentOid;
			
			// Execute
			RevisionQuery lQuery = new RevisionQuery(lOnContext);
			ONCollection lCollection = lQuery.QueryByOid(lOID, displaySet);
			ticket = lOnContext.GetTicket(dtdVersion, clientName);
			
			return lCollection;
		}
		#endregion

		#region QueryByRelated
		public override ONCollection QueryByRelated(ref string ticket, ref ONOid agentOid, XmlReader xmlReader, ONDisplaySet displaySet, out ONOid startRowOID, out int blockSize, double dtdVersion, string clientName)
		{
			ONLinkedToList lLinkedTo = null;
			ONFilterList lFilters = new ONFilterList();
			
			// Read Request
			blockSize = int.Parse(xmlReader.GetAttribute("BlockSize"));
			if (!xmlReader.IsEmptyElement)
			{
				xmlReader.ReadStartElement("Query.Related");
				if (xmlReader.IsStartElement("StartRow"))
				{
					xmlReader.ReadStartElement("StartRow");
					startRowOID = XML2ON(xmlReader, dtdVersion);
					xmlReader.ReadEndElement(); // StartRow
				}
				else
					startRowOID = null;
			
				// Fill relations
				lLinkedTo = GetLinkedTo(xmlReader, dtdVersion);
			
				// Read Request
				xmlReader.ReadEndElement(); // Query.Related
			}
			else
			{
				xmlReader.ReadStartElement("Query.Related");
				lLinkedTo = new ONLinkedToList();
				startRowOID = null;
			}
			
			// Read Order Criteria
			string lOrderCriteria = "";
			if (xmlReader.IsStartElement("Sort"))
			{
				lOrderCriteria = xmlReader.GetAttribute("Criterium");
				xmlReader.Skip(); // Sort
			}
                                 
			//Read Navigational Filter
			string lNavFilterId = "";
			ONOid lSelectedObject = null;
			if (xmlReader.IsStartElement("NavFilt"))
			{
				xmlReader.ReadStartElement("NavFilt");
				if (xmlReader.IsStartElement("NavFilt.SelectedObject"))
				{
				        lNavFilterId = xmlReader.GetAttribute("NavFilterID");
					xmlReader.ReadStartElement("NavFilt.SelectedObject");
					string lClassName = xmlReader.GetAttribute("Class");
					if (lClassName == "")
						lClassName = "GlobalTransaction"; 
					object[] lArgs = new object[2];
					lArgs[0] = xmlReader;
					lArgs[1] = dtdVersion;
					lSelectedObject = ONContext.InvoqueMethod(ONContext.GetType_XML(lClassName), "XML2ON", lArgs) as ONOid;
					
					lArgs = new Object[1];
					lArgs[0] = lSelectedObject;
					
					lFilters.Add(lNavFilterId, ONContext.GetComponent_NavigationalFilter(lNavFilterId, lArgs) as ONFilter);					
				}
				else if (xmlReader.IsStartElement("NavFilt.Argument"))
				{
					string lClassName = xmlReader.GetAttribute("Class");
					if (lClassName == "")
						lClassName = "GlobalTransaction"; 
					string lServiceName = xmlReader.GetAttribute("Service");
					string lArgumentName = xmlReader.GetAttribute("Argument");
					xmlReader.ReadStartElement("NavFilt.Argument");
					
					string lFilterName = lClassName + "_" + lServiceName + "Service_" + lArgumentName + "_NavigationalFilter";
					if (xmlReader.IsStartElement("Arguments"))
					{
						object[] lArgs = new object[3];
						lArgs[0] = xmlReader;
						lArgs[1] = dtdVersion;
						lArgs[2] = lArgumentName;
										
						lFilters.Add(lFilterName, ONContext.InvoqueMethod(ONContext.GetComponent_XML(lClassName), typeof(ONNavigationalFilterXMLAttribute), "<Filter>" + lServiceName + "</Filter>", lArgs) as ONFilter);
					}
					else
					{
						lFilters.Add(lFilterName, ONContext.GetComponent_Filter(lFilterName, null) as ONFilter);
					
					}
					
					xmlReader.ReadEndElement(); //NavFilt.Argument
				}
				else if (xmlReader.IsStartElement("NavFilt.ServiceIU"))
				{
					lNavFilterId = xmlReader.GetAttribute("NavFilterID");
					xmlReader.ReadStartElement("NavFilt.ServiceIU");

					if (xmlReader.IsStartElement("Arguments"))
					{
						object[] lArgs = new object[2];
						lArgs[0] = xmlReader;
						lArgs[1] = dtdVersion;
						
						ONServiceInfo lSInfo = new ONServiceInfo("", "", "", "");
						lSInfo.XML2ON(xmlReader, dtdVersion, false);

						HybridDictionary lArguments = new HybridDictionary(true);
						foreach (ONArgumentInfo lArgument in lSInfo.mArgumentList.Values)
						{
							lArguments.Add(lArgument.Name, lArgument.Value);
						}
						lFilters.Add(lNavFilterId, ONContext.GetComponent_NavigationalFilter(lNavFilterId, new object[1] { lArguments }) as ONFilter);
					}
					else
						lFilters.Add(lNavFilterId, ONContext.GetComponent_NavigationalFilter(lNavFilterId, null) as ONFilter);

					xmlReader.ReadEndElement(); //NavFilt.ServiceIU	
				}
				else if (xmlReader.IsStartElement("NavFilt.Variable"))
				{
					string lClassName = xmlReader.GetAttribute("Class");
					string lSourceFilterName = xmlReader.GetAttribute("Filter");
					string lVariableName = xmlReader.GetAttribute("Variable");
					
					xmlReader.ReadStartElement("NavFilt.Variable");
					string lFilterName = lClassName + "_" + lSourceFilterName + "Filter_" + lVariableName + "_NavigationalFilter";
					if (xmlReader.IsStartElement("Filter.Variables"))
					{
						object[] lArgs = new object[3];
						lArgs[0] = xmlReader;
						lArgs[1] = dtdVersion;
						lArgs[2] = lVariableName;
						
						lFilters.Add(lFilterName, ONContext.InvoqueMethod(ONContext.GetComponent_XML(lClassName), typeof(ONNavigationalFilterXMLAttribute), "<Filter>" + lSourceFilterName + "</Filter>", lArgs) as ONFilter);
					}
					else
					{
						lFilters.Add(lFilterName, ONContext.GetComponent_Filter(lFilterName, null) as ONFilter);

					}

					xmlReader.ReadEndElement(); //NavFilt.Variable
				}
			}
			
			// Create Context
			ONContext lOnContext = new ONContext();
			lOnContext.OidAgent = agentOid;

			foreach (ONFilter lFilter in lFilters.Values)
			{
				lFilter.CheckFilterVariables(lOnContext);
			}

			// Execute
			lOnContext.CalculateQueryInstancesNumber = true;
			RevisionQuery lQuery = new RevisionQuery(lOnContext);
			ONCollection lCollection = lQuery.QueryByFilter(lLinkedTo, lFilters, displaySet, lOrderCriteria, startRowOID, blockSize);
			ticket = lOnContext.GetTicket(dtdVersion, clientName);
			
			return lCollection;
		}
		#endregion

		#region QueryByFilter "LMD"

		
		/// <summary>Extracts all the information about the filter query that has the XML message</summary>
		/// <param name="agentOid">OID with the agent connected to the system</param>
		/// <param name="xmlReader">XML with the request message</param>
		/// <param name="startRowOID">OID necessary to start the search</param>
		/// <param name="blockSize">Represents the number of instances to be returned</param>
		/// <param name="dtdVersion">Version of DTD that follows the XML message</param>
		[ONFilterXML("LMD")]
		public ONCollection QueryByFilter_LMD(ref string ticket, ref ONOid agentOid, XmlReader xmlReader, ONDisplaySet displaySet, out ONOid startRowOID, out int blockSize, double dtdVersion, string clientName)
		{
			ONLinkedToList lLinkedTo = null;
			ONFilterList lFilters = new ONFilterList();
			
			// Read Request
			blockSize = int.Parse(xmlReader.GetAttribute("BlockSize"));
			if (!xmlReader.IsEmptyElement)
			{
				xmlReader.ReadStartElement("Query.Filter");
				if (xmlReader.IsStartElement("StartRow"))
				{
					xmlReader.ReadStartElement("StartRow");
					startRowOID = XML2ON(xmlReader, dtdVersion);
					xmlReader.ReadEndElement(); // StartRow
				}
				else
					startRowOID = null;
			
				// Fill relations
				lLinkedTo = GetLinkedTo(xmlReader, dtdVersion);
			
				// Fill filter variables
				lFilters["LMD"] = GetFilter_LMD(xmlReader, dtdVersion);
			
				// Read Request
				xmlReader.ReadEndElement(); // Query.Filter
			}
			else
			{
				xmlReader.ReadStartElement("Query.Filter");
				lLinkedTo = new ONLinkedToList();
				startRowOID = null;
			}
			
			// Read Order Criteria
			string lOrderCriteria = "";
			if (xmlReader.IsStartElement("Sort"))
			{
				lOrderCriteria = xmlReader.GetAttribute("Criterium");
				xmlReader.Skip(); // Sort
			}
			
			//Read Filter Navigation
            if (xmlReader.IsStartElement("NavFilt"))
				xmlReader.Skip();
		
			// Create Context
			ONContext lOnContext = new ONContext();
			lOnContext.OidAgent = agentOid;
			
			// Execute
			lOnContext.CalculateQueryInstancesNumber = true;
			RevisionQuery lQuery = new RevisionQuery(lOnContext);
			ONCollection lCollection = lQuery.QueryByFilter(lLinkedTo, lFilters, displaySet, lOrderCriteria, startRowOID, blockSize);
			ticket = lOnContext.GetTicket(dtdVersion, clientName);

			return lCollection;
		}

		/// <summary>Extracts from the XML message the filters that have to be solved</summary>
		/// <param name="xmlReader">XML with the request message</param>
		/// <param name="dtdVersion">Version of DTD that follows the XML message</param>
		public RevisionLMDFilter GetFilter_LMD(XmlReader xmlReader, double dtdVersion)
		{
			// Create filter
			RevisionLMDFilter lFilter = new RevisionLMDFilter();
			
			// Process the variable filters
			ONServiceInfo lSInfo = new ONServiceInfo("", "LMD", "Clas_1348178542592347_Alias", "Revision");
			lSInfo.AddFilterVariable("InitDate", DataTypeEnumerator.Date, 0, "", "InitDate");
			lSInfo.AddFilterVariable("FinalDate", DataTypeEnumerator.Date, 0, "", "FinalDate");
			
			try
			{
				lSInfo.XML2ONFilterVariables(xmlReader, dtdVersion);
			}
			catch (Exception e)
			{
				throw new ONServiceArgumentsException(e, "Clas_1348178542592347_Alias", "Revision", "", "LMD");
			}
			
			lFilter.InitDateVar = (ONDate) ((ONArgumentInfo) lSInfo.mArgumentList["InitDate"]).Value;
			lFilter.FinalDateVar = (ONDate) ((ONArgumentInfo) lSInfo.mArgumentList["FinalDate"]).Value;
			
			return lFilter;
		}

		/// <summary>Extracts all the information about the filter query that has the XML message</summary>
		/// <param name="agentOid">OID with the agent connected to the system</param>
		/// <param name="xmlReader">XML with the request message</param>
		/// <param name="startRowOID">OID necessary to start the search</param>
		/// <param name="blockSize">Represents the number of instances to be returned</param>
		/// <param name="dtdVersion">Version of DTD that follows the XML message</param>
		[ONFilterXML("FUM")]
		public ONCollection QueryByFilter_FUM(ref string ticket, ref ONOid agentOid, XmlReader xmlReader, ONDisplaySet displaySet, out ONOid startRowOID, out int blockSize, double dtdVersion, string clientName)
		{
			ONLinkedToList lLinkedTo = null;
			ONFilterList lFilters = new ONFilterList();
			
			// Read Request
			blockSize = int.Parse(xmlReader.GetAttribute("BlockSize"));
			if (!xmlReader.IsEmptyElement)
			{
				xmlReader.ReadStartElement("Query.Filter");
				if (xmlReader.IsStartElement("StartRow"))
				{
					xmlReader.ReadStartElement("StartRow");
					startRowOID = XML2ON(xmlReader, dtdVersion);
					xmlReader.ReadEndElement(); // StartRow
				}
				else
					startRowOID = null;
			
				// Fill relations
				lLinkedTo = GetLinkedTo(xmlReader, dtdVersion);
			
				// Fill filter variables
				lFilters["FUM"] = GetFilter_FUM(xmlReader, dtdVersion);
			
				// Read Request
				xmlReader.ReadEndElement(); // Query.Filter
			}
			else
			{
				xmlReader.ReadStartElement("Query.Filter");
				lLinkedTo = new ONLinkedToList();
				startRowOID = null;
			}
			
			// Read Order Criteria
			string lOrderCriteria = "";
			if (xmlReader.IsStartElement("Sort"))
			{
				lOrderCriteria = xmlReader.GetAttribute("Criterium");
				xmlReader.Skip(); // Sort
			}
			
			// Create Context
			ONContext lOnContext = new ONContext();
			lOnContext.OidAgent = agentOid;
			
			// Execute
			RevisionQuery lQuery = new RevisionQuery(lOnContext);
			ONCollection lCollection = lQuery.QueryByFilter(lLinkedTo, lFilters, displaySet, lOrderCriteria, startRowOID, blockSize);
			ticket = lOnContext.GetTicket(dtdVersion, clientName);
			
			return lCollection;
		}

		/// <summary>Extracts from the XML message the filters that have to be solved</summary>
		/// <param name="xmlReader">XML with the request message</param>
		/// <param name="dtdVersion">Version of DTD that follows the XML message</param>
		public RevisionLMDFilter GetFilter_FUM(XmlReader xmlReader, double dtdVersion)
		{
			// Create filter
			RevisionLMDFilter lFilter = new RevisionLMDFilter();
			
			// Process the variable filters
			ONServiceInfo lSInfo = new ONServiceInfo("", "FUM", "Clas_1348178542592347_Alias", "Revision");
			lSInfo.AddFilterVariable("FDesde", DataTypeEnumerator.Date, 0, "", "FDesde");
			lSInfo.AddFilterVariable("FHasta", DataTypeEnumerator.Date, 0, "", "FHasta");
			
			try
			{
				lSInfo.XML2ONFilterVariables(xmlReader, dtdVersion);
			}
			catch (Exception e)
			{
				throw new ONServiceArgumentsException(e, "Clas_1348178542592347_Alias", "Revision", "", "FUM");
			}
			
			lFilter.InitDateVar = (ONDate) ((ONArgumentInfo) lSInfo.mArgumentList["FDesde"]).Value;
			lFilter.FinalDateVar = (ONDate) ((ONArgumentInfo) lSInfo.mArgumentList["FHasta"]).Value;
			
			return lFilter;
		}
		#endregion

		#region XML - ON (OIDs)
		/// <summary>
		/// Extracts the OID from the XML message and converts it in structures of the application
		/// </summary>
		/// <param name="xmlReader">XML with the request message</param>
		/// <param name="dtdVersion">Version of DTD that follows the XML message</param>
		public static RevisionOid XML2ON(XmlReader xmlReader, double dtdVersion)
		{
			try
			{
				if (xmlReader.IsStartElement(ONXml.XMLTAG_NULL))
				{
					xmlReader.Skip();
					return RevisionXml.Null;
				}

				if (!xmlReader.IsStartElement(ONXml.XMLTAG_OID))
					throw new ONXMLStructureException(null, ONXml.XMLTAG_OID);
			}
			catch(Exception e)
			{
				throw new ONXMLStructureException(e, ONXml.XMLTAG_OID);
			}
			
			string lClass = xmlReader.GetAttribute("Class");
			
			if (string.Compare(lClass, "Revision", true) != 0)
				throw new ONXMLOIDWrongClassException(null, "Clas_1348178542592347_Alias", "Revision", lClass);
			
			RevisionOid lOid = new RevisionOid();
			xmlReader.ReadStartElement(ONXml.XMLTAG_OID);

			try
			{
				lOid.Id_RevisarAeronaveAttr = ONXmlAutonumeric.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_OIDFIELD);
			}
			catch(Exception e)
			{
				throw new ONXMLOIDFieldException(e,"Clas_1348178542592347_Alias", "Revision", "Clas_1348178542592347Atr_1_Alias", "id_RevisarAeronave");
			}

			try
			{
				xmlReader.ReadEndElement();
			}
			catch
			{
				throw new ONXMLStructureException(null, ONXml.XMLTAG_OID);
			}

			return lOid;
		}

		/// <summary>
		/// Converts an OID into XML in order to put it in XML message response
		/// </summary>
		/// <param name="xmlWriter">XML with the response message</param>
		/// <param name="oid">OID that will be insert into XML message</param>
		/// <param name="dtdVersion">Version of DTD that follows the XML message</param>
		/// <param name="xmlElement">Element in the XML message in order to convert in the right form of the XML</param>
		public static void ON2XML(XmlWriter xmlWriter, RevisionOid oid, double dtdVersion, string xmlElement)
		{
			if (oid == null)
				xmlWriter.WriteElementString(ONXml.XMLTAG_NULL, null);
			else
			{
				xmlWriter.WriteStartElement(ONXml.XMLTAG_OID);
				xmlWriter.WriteAttributeString(ONXml.XMLATT_CLASS, "Revision");
				ONXmlAutonumeric.ON2XML(xmlWriter, oid.Id_RevisarAeronaveAttr, dtdVersion, xmlElement);

				xmlWriter.WriteEndElement(); // OID
			}
		}
		#endregion

		#region XML - ON (Querys)
		/// <summary>
		/// Converts the instances returned by a query in XML format in order to put it in XML message response
		/// </summary>
		/// <param name="agentOid">OID with the agent connected to the system</param>
		/// <param name="xmlWriter">This parameter has the response message XML</param>
		/// <param name="val">Instances that fullfils with the query request</param>
		/// <param name="startRowOID">OID necessary to start the search</param>
		/// <param name="blockSize">Represents the number of instances to be returned</param>
		/// <param name="displaySet">Attributes to be returned in the response</param>
		/// <param name="dtdVersion">Version of DTD that follows the XML message</param>
		public override void ONQuery2XML(ONOid agentOid, XmlWriter xmlWriter, ONCollection val, ONOid startRowOID, int blockSize, ONDisplaySet displaySet, double dtdVersion)
		{
		
			xmlWriter.WriteStartElement("Head");
			xmlWriter.WriteStartElement("Head.OID");
			xmlWriter.WriteAttributeString("Class", "Revision");
			xmlWriter.WriteStartElement("Head.OID.Field");
			xmlWriter.WriteAttributeString("Name", "id_RevisarAeronave");
			xmlWriter.WriteAttributeString("Type", "autonumeric");
			xmlWriter.WriteEndElement(); // Head.OID.Field

			xmlWriter.WriteEndElement(); // Head.OID
			
			xmlWriter.WriteStartElement("Head.Cols");
			foreach (ONDisplaySetItem lDisplaySetItem in displaySet)
			{
				xmlWriter.WriteStartElement("Head.Col");
				xmlWriter.WriteAttributeString("Name", lDisplaySetItem.Path);
				string lType = ONInstance.GetTypeOfAttribute(typeof(RevisionInstance), new ONPath(lDisplaySetItem.Path));
				if(lType == "")
					lType = "string";
				xmlWriter.WriteAttributeString("Type", lType);
				xmlWriter.WriteEndElement(); // Head.Col
			}
			xmlWriter.WriteEndElement(); // Head.Cols
			xmlWriter.WriteEndElement(); // Head
			
			// Search StartRow
			int i = 0;
			if (startRowOID != null)
				i = val.IndexOf(startRowOID) + 1;
			
			// Instance count
			int lInstances = 0;
			int lNumInstances = 0;
			string lLastBlock = "True";
			if (i >= 0)
			{
				lNumInstances = val.Count - i;
				if ((blockSize > 0) && (lNumInstances > blockSize))
				{
					lNumInstances = blockSize;
					lLastBlock = "False";
				}
			}
			
			xmlWriter.WriteStartElement("Data");
			xmlWriter.WriteAttributeString("Rows", lNumInstances.ToString());
			xmlWriter.WriteAttributeString("LastBlock", lLastBlock);
			xmlWriter.WriteAttributeString("TotalRows", val.totalNumInstances.ToString());
			while ((lInstances++ < lNumInstances) && (i < val.Count))
			{
				RevisionInstance lInstance = val[i++] as RevisionInstance;
			
				xmlWriter.WriteStartElement("R");
				ONXmlAutonumeric.ON2XML(xmlWriter, lInstance.Oid.Id_RevisarAeronaveAttr, dtdVersion, ONXml.XMLTAG_O);


				foreach (ONDisplaySetItem lDisplaySetItem in displaySet)
				{
					if (lDisplaySetItem.Visibility == VisibilityState.NotChecked)
					{
						if (ONInstance.IsVisible(lInstance.GetType(), lDisplaySetItem.Path, lInstance.OnContext))
							lDisplaySetItem.Visibility = VisibilityState.Visible;
						else
							lDisplaySetItem.Visibility = VisibilityState.NotVisible;
					}
					if (lDisplaySetItem.Visibility == VisibilityState.NotVisible) // No Visibility
						xmlWriter.WriteElementString("V", null);
					else
					{
						ONSimpleType lAttribute;
						if(lDisplaySetItem.HasHV)
							lAttribute = lInstance.DisplaysetItemValue(lDisplaySetItem.Path);
						else
							lAttribute = lInstance[lDisplaySetItem.Path] as ONSimpleType;
			
						if (lAttribute is ONInt)
							ONXmlInt.ON2XML(xmlWriter, lAttribute as ONInt, dtdVersion, ONXml.XMLTAG_V);
						else if (lAttribute is ONString)
							ONXmlString.ON2XML(xmlWriter, lAttribute as ONString, dtdVersion, ONXml.XMLTAG_V);
						else if (lAttribute is ONBlob)
							ONXmlBlob.ON2XML(xmlWriter, lAttribute as ONBlob, dtdVersion, ONXml.XMLTAG_V);
						else if (lAttribute is ONBool)
							ONXmlBool.ON2XML(xmlWriter, lAttribute as ONBool, dtdVersion, ONXml.XMLTAG_V);
						else if (lAttribute is ONReal)
							ONXmlReal.ON2XML(xmlWriter, lAttribute as ONReal, dtdVersion, ONXml.XMLTAG_V);
						else if (lAttribute is ONInt)
							ONXmlAutonumeric.ON2XML(xmlWriter, lAttribute as ONInt, dtdVersion, ONXml.XMLTAG_V);
						else if (lAttribute is ONDate)
							ONXmlDate.ON2XML(xmlWriter, lAttribute as ONDate, dtdVersion, ONXml.XMLTAG_V);
						else if (lAttribute is ONDateTime)
							ONXmlDateTime.ON2XML(xmlWriter, lAttribute as ONDateTime, dtdVersion, ONXml.XMLTAG_V);
						else if (lAttribute is ONTime)
							ONXmlTime.ON2XML(xmlWriter, lAttribute as ONTime, dtdVersion, ONXml.XMLTAG_V);
						else if (lAttribute is ONNat)
							ONXmlNat.ON2XML(xmlWriter, lAttribute as ONNat, dtdVersion, ONXml.XMLTAG_V);
						else if (lAttribute is ONText)
							ONXmlText.ON2XML(xmlWriter, lAttribute as ONText, dtdVersion, ONXml.XMLTAG_V);
						else if (lAttribute is ONString)
							ONXmlPassword.ON2XML(xmlWriter, lAttribute as ONString, dtdVersion, ONXml.XMLTAG_V);
					}
				}
				xmlWriter.WriteEndElement(); // R
			}
			xmlWriter.WriteEndElement(); // Data
		}
		#endregion
	}
}
