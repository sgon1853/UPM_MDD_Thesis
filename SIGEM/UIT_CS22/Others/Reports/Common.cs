// v3.8.4.5.b
using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;

using SIGEM.Client.Oids;

namespace SIGEM.Client.PrintingDriver.Common
{

	#region XMLReportsQueryHandler class: Manages reports configuration from App.Config
	public class XMLReportsQueryHandler : System.Configuration.IConfigurationSectionHandler
	{
		public XMLReportsQueryHandler(){}

		#region  IConfigurationSectionHandler Implementation
		public object Create(object parent, object configContext, System.Xml.XmlNode section)
		{
			return section.OuterXml; //.SelectNodes("ReportName").OuterXml;
		}
		#endregion

		/// <summary>
		/// Get the report configuration for a class.
		/// </summary>
		/// <param name="rootOID">Root OID of the report.</param>
		/// <returns></returns>
		public static string XMLReportQuery(Oid rootOID)
		{
			return XMLReportQuery(rootOID,string.Empty);
		}

		public static string XMLReportQuery(Oid rootOID, string ApplicationPath)
		{
			return XMLReportQuery(rootOID.ClassName, ApplicationPath);
		}
		public static string XMLReportQuery(string className, string ApplicationPath)
		{
			string PathFile = string.Empty;
			string NameOfFile = string.Empty;
			string strXML = string.Empty;
			string lClassName = "ClassName";

			System.Configuration.AppSettingsReader appconfig = new System.Configuration.AppSettingsReader();
			NameOfFile = appconfig.GetValue("ConfigurationOfReports",typeof(string)) as string;

			PathFile = Path.GetFullPath(NameOfFile);
			if(!File.Exists(PathFile))
			{
				if(!File.Exists(PathFile))
				{
					throw (new ArgumentException("File not found: " + PathFile));
				}
			}

			XmlDocument lxmlDoc = new XmlDocument();
			lxmlDoc.Load(PathFile);

			//Open the document and associate a navigator.
			XPathNavigator navXPath = lxmlDoc.CreateNavigator();

			string xpathQuery; //@"XMLReportQuery/@Name[text() = '" +XMLReportQueryName + "']";

			//Select nodes with the same class name.
			xpathQuery = @"/XMLReportsQuery/XMLReportQuery[@" + lClassName + " = '" + className.Trim() + "']";

			XPathExpression expXPath = navXPath.Compile(xpathQuery);

			//Order the reports by the type of report.
			expXPath.AddSort("@Type", XmlSortOrder.Ascending,XmlCaseOrder.None, string.Empty,XmlDataType.Text);
			XPathNodeIterator itNodes =	navXPath.Select(expXPath);

			return XMLReportsQueryHandler.GetOutXML(itNodes.Clone());
		}


		/// <summary>
		/// Create the XML with all the nodes.
		/// </summary>
		/// <param name="itNodes">Iterator node.</param>
		/// <returns>XML string with all the nodes.</returns>
		private static string GetOutXML(XPathNodeIterator itNodes)
		{
			string lStrRslt = string.Empty;
			if(itNodes != null)
				if(itNodes.Count > 0)
				{
					//Create XMLWriter.
					MemoryStream lXmlMemoryStrm  = new MemoryStream();
					XmlTextWriter lXmlWrt = new XmlTextWriter(lXmlMemoryStrm, new System.Text.UTF8Encoding());
					lXmlWrt.WriteStartDocument();
					lXmlWrt.WriteStartElement("Reports");

					//For all ordered nodes.
					while(itNodes.MoveNext())
					{

						XmlNode node = ((IHasXmlNode)itNodes.Current).GetNode();
						lXmlWrt.WriteRaw(node.OuterXml);

					}
					lXmlWrt.WriteEndElement();
					lXmlWrt.WriteEndDocument();


					lXmlWrt.Flush();
					lXmlMemoryStrm.Flush();
					lXmlMemoryStrm.Position = 0;
					System.IO.StreamReader lXmlStrmRd = new StreamReader(lXmlMemoryStrm);
					lStrRslt = lXmlStrmRd.ReadToEnd();

					lXmlWrt.Close();
					lXmlMemoryStrm.Close();
				}
			return lStrRslt;
		}

	}
	#endregion

	#region XMLQuery Class: XML nodes management using XPath
	public class XMLQuery
	{
		XMLQuery(){}

		#region Handle Node Report
		public static XmlNode SelectReport(string PathFile, Oid rootOID, string ReportName)
		{
			return SelectReport(PathFile, rootOID, ReportName, true);
		}
		public static XmlNode SelectReport(string PathFile, Oid rootOID, string ReportName, bool Cloning)
		{
			XmlNode nodeRslt = null;
			if(!File.Exists(PathFile))
			{
				PathFile = Path.GetFullPath(PathFile);
			}

				//Load the file with the Query
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(PathFile);
				nodeRslt = SelectReport(rootOID, xmlDoc, ReportName, Cloning);

			return nodeRslt;
		}

		public static XmlNode SelectReport(Oid rootOID, string xmlDocument, string ReportName, bool Cloning)
		{
			//Load the file with the Query
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(xmlDocument);
			return SelectReport(rootOID, xmlDoc, ReportName, Cloning);
		}

		public static XmlNode SelectReport(Oid rootOID, XmlDocument xmlDoc, string ReportName, bool Cloning)
		{
			string NameNode = "/Reports/ReportName[@Name = '"+ ReportName + "' and Report/Object/OID[@Class.Name = '"+ rootOID.ClassName + "']]/Report";
			return SelectNode(xmlDoc,NameNode,Cloning);
		}
		#endregion

		#region Handle Node Object
		public static XmlNode SelectObject(XmlNode nodeReport)
		{
			return SelectObject(nodeReport, true);
		}
		public static XmlNode SelectObject(XmlNode nodeReport, bool Cloning)
		{
			return SelectNode(nodeReport,"Object",Cloning);
		}
		#endregion

		#region Handle Node OID
		public static XmlNode SelectOID(XmlNode nodeObject)
		{
			return SelectOID(nodeObject,true);
		}
		public static XmlNode SelectOID(XmlNode nodeObject,bool Cloning)
		{
			return SelectNode(nodeObject,"/OID",Cloning);
		}

		#endregion

		#region Handle Node Properties
		public static XmlNode SelectProperties(XmlNode nodeObject)
		{
			return  SelectProperties(nodeObject,true);
		}
		public static XmlNode SelectProperties(XmlNode nodeObject, bool Cloning)
		{
			return SelectNode(nodeObject,"Properties",Cloning);
		}
		#endregion

		#region Handle Node Roles
		public static bool HasSubRoles(XmlNode nodeProperties)
		{
			bool Rslt;
			if (SelectNode(nodeProperties,"Role")==null)
				Rslt = false;
			else
				Rslt = true;

			return Rslt;
		}

		//Get the Roles from the Propertie
		public static XmlNode[] SelectRoles(XmlNode nodeProperties)
		{
			return SelectRoles(nodeProperties, true);
		}
		public static XmlNode[] SelectRoles(XmlNode nodeProperties, bool Cloning)
		{
			return SelectNodes(nodeProperties,"Role",Cloning);
		}
		#endregion

		#region Handle Node Facet
		public static bool HasSubFacets(XmlNode nodeProperties)
		{
			bool Rslt;
			if (SelectNode(nodeProperties,"Properties/Facet")==null)
				Rslt = false;
			else
				Rslt = true;

			return Rslt;
		}
		//Get the Facets from the Propertie
		public static XmlNode[] SelectFacets(XmlNode nodeProperties)
		{
			return SelectFacets(nodeProperties, true);
		}
		public static XmlNode[] SelectFacets(XmlNode nodeProperties, bool Cloning)
		{
			return SelectNodes(nodeProperties,"Facet",Cloning);
		}
		#endregion

		#region Handle Select Nodes
		//Get the Roles from the Propertie
		public static XmlNode[] SelectNodes(XmlNode node, string NodeName )
		{
			return SelectNodes(node, NodeName, true);
		}
		public static XmlNode[] SelectNodes(XmlNode node, string NodeName, bool Cloning)
		{
			XmlNode[] aNodes = null;
			XPathNavigator navXPath = null;
			if (node != null)
			{
				//Select Properties node
				//----------------------
				navXPath = node.CreateNavigator();
				XPathNodeIterator itNodes = navXPath.Select(NodeName);
				if(itNodes != null)
				{
					if (itNodes.Count > 0)
					{
						aNodes = new XmlNode[itNodes.Count];
						int It = 0;
						while(itNodes.MoveNext())
						{
							if(Cloning)
								aNodes[It] = ((IHasXmlNode)itNodes.Current).GetNode().Clone();
							else
								aNodes[It] = ((IHasXmlNode)itNodes.Current).GetNode();

							It++;
						}
					}
				}
			}
			return aNodes;
		}
		#endregion

		#region Handle Select Node

		public static XmlNode SelectNode(XmlNode node, string NodeName )
		{
			return  SelectNode(node,NodeName,true);
		}
		public static XmlNode SelectNode(XmlNode node, string NodeName , bool Cloning)
		{
			XmlNode nodeProperties	= null;
			XPathNavigator navXPath = null;
			if (node != null)
			{
				//Select Properties node
				//----------------------
				navXPath = node.CreateNavigator();
				XPathNodeIterator itNodes = navXPath.Select(NodeName);
				if(itNodes != null)
				{
					if (itNodes.Count > 0)
					{
						itNodes.MoveNext();
						if (Cloning)
						{
							nodeProperties = ((IHasXmlNode)itNodes.Current).GetNode().Clone();
						}
						else
						{
							nodeProperties = ((IHasXmlNode)itNodes.Current).GetNode();
						}
					}
				}
			}
			return nodeProperties;
		}
		#endregion

		#region Getting Class.Name, InvRole.Name, Role.Name

		/// <summary>
		/// Get the role name from the role represented in XmlNode.
		/// </summary>
		/// <param name="nodeRole">XmlNode that specifies the role.</param>
		/// <returns>A string with the name of the role.</returns>
		public static string SelectNameOfRole(XmlNode nodeRole)
		{
			XmlAttribute attRole = (XmlAttribute)nodeRole.Attributes.GetNamedItem("Role.Name");
			if (attRole == null)
				throw new ApplicationException("No especificado el Nombre del Role en el nodo Role");

			return attRole.Value;
		}

		#endregion

		/// <summary>
		/// Process a XmlNode to obtain the attributes of the DisplaySet (in a string as a list).
		/// </summary>
		/// <param name="nodeProperties">A XmlNode with the DisplaySet.</param>
		/// <returns>A string list (separed by commas) that represents the list of attributes.</returns>
		public static string DisplaySetFromNodeProperties(XmlNode nodeProperties)
		{
			if(nodeProperties != null)
				return string.Join(",", GetNamesOfAttributeInProperties(nodeProperties));
			else
				return string.Empty;
		}


		/// <summary>
		/// Process a XmlNode to obtain the attributes of the DisplaySet.
		/// </summary>
		/// <param name="nodeProperties">A XmlNode with the DisplaySet.</param>
		/// <returns>A string collection that represents the list of attributes.</returns>
		public static string[] GetNamesOfAttributeInProperties(XmlNode nodeProperties)
		{
			string [] DisplaySet = null;

			XmlNode[] aNodes = XMLQuery.SelectNodes(nodeProperties, "Attribute", false);
			if(aNodes != null)
			{
				DisplaySet = new string[aNodes.Length];

				for(int It = 0; It < aNodes.Length; It++)
				{
					DisplaySet[It] = aNodes[It].Attributes["Name"].Value;
				}
			}
			return DisplaySet;
		}
	}
	#endregion
}
