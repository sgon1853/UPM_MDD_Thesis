// v3.8.4.5.b
using System;
using System.Xml;
using System.Data;
using System.Collections.Generic;

using SIGEM.Client.Oids;
using SIGEM.Client.PrintingDriver.Common;

namespace SIGEM.Client.PrintingDriver
{

	#region PrintToXML class: Execute a set of queries that are specified in XML, and returns another XML
	public static class PrintToXML
	{
		/// <summary>
		/// Get the result after executing the queries of the report.
		/// </summary>
		/// <param name="oid">Root OID of the report.</param>
		/// <param name="nodeReport"></param>
		/// <returns>A XmlNode object with the XML result of the queries.</returns>
		public static XmlNode GetQueryXML(Oid oid, XmlNode nodeReport)
		{
			XmlNode nodeObject = null;
			XmlNode nodeRpt = null;
			if ((nodeReport != null) && (oid != null))
			{
				XmlNode nodeOIDProperties = null;

				nodeObject= XMLQuery.SelectObject(nodeReport, true);

				//Process OID
				nodeOIDProperties = XMLQuery.SelectProperties(nodeObject, false);

				DataTable rootInstance = Logics.Logic.ExecuteQueryInstance(Logics.Logic.Agent, oid, XMLQuery.DisplaySetFromNodeProperties(nodeOIDProperties));

				Dictionary<string, int> columNames = new Dictionary<string, int>();
				int i = 0;
				foreach (DataColumn column in rootInstance.Columns)
				{
					columNames.Add(column.Caption, i);
					i++;
				}
				SetValueOfNodeAttributeFromInstance(nodeOIDProperties, rootInstance.Rows[0], false, columNames);

				// Assing value to OID field.
				XmlNode nodeOID = XMLQuery.SelectOID(nodeObject, false);
				for(int it=0; it < oid.Fields.Count; it++)
				{
					nodeOID.ChildNodes[it].Attributes["Value", string.Empty].Value = oid.GetValues()[it].ToString();
				}

				//Process Roles
				ProcessRoles(nodeOIDProperties, oid);

				//Create the root node, representing the report
				nodeRpt = nodeObject.OwnerDocument.CreateElement("Report");

				//Insert object in the root node
				nodeRpt.AppendChild(nodeObject);
			}
			return nodeRpt;
		}

		#region Process Role nodes
		private static void ProcessRoles(XmlNode nodeProperties, Oid oid)
		{
			XmlNode[] aRoles =  XMLQuery.SelectRoles(nodeProperties,false);
			if (aRoles != null)
			{
				//Process existing roles
				for (int It=0; It<aRoles.Length; It++)
				{
					ProcessRole(aRoles[It],oid);
				}
			}
		}

		private static void ProcessRole(XmlNode nodeRole, Oid oid)
		{
			//Add <List.Object> node as a child of nodeRole
			DataTable rspRole;

			//Get related instances from Role
			string ClasOfRole = "";
			try
			{
				ClasOfRole = nodeRole.Attributes.GetNamedItem("Class.Name").Value;
			}
			catch (Exception e)
			{
				string errorMsg = "Role class name not found.\r\nPlease check the template";
				throw new Exception(errorMsg, e);
			}

			string InvRole = "";
			try
			{
				InvRole = nodeRole.Attributes.GetNamedItem("InvRole.Name").Value;
			}
			catch (Exception e)
			{
				string errorMsg = "Inverse role name for class name '" + ClasOfRole +
					"' not found.\r\nPlease check the template";
				throw new Exception(errorMsg, e);
			}

			string DisplaySet = "";
			try
			{
				DisplaySet = XMLQuery.DisplaySetFromNodeProperties(
					XMLQuery.SelectProperties(nodeRole, false));
			}
			catch (Exception e)
			{
				string errorMsg = "Display set for class name '" + ClasOfRole +
					"' not found.\r\nPlease check the template";
				throw new Exception(errorMsg, e);
			}

			// Add the info about the related object
			Dictionary<string, Oid> linkItems = new Dictionary<string,Oid>(StringComparer.CurrentCultureIgnoreCase);
			linkItems.Add(InvRole, oid);
			try
			{
				rspRole = Logics.Logic.Adaptor.ExecuteQueryRelated(
					Logics.Logic.Agent, ClasOfRole, linkItems, DisplaySet, "", null, 0);
			}
			catch (Exception e)
			{
				string errorMsg = "Error in Query related. For class name '" +
					ClasOfRole + "', Inverse rol name '" + InvRole + "' and DisplaySet '" +
					DisplaySet + "'.\r\nPlease check the template";
				throw new Exception(errorMsg, e);
			}

			//Get the properties from Properties node
			XmlNode	nodeRoleProperties = XMLQuery.SelectProperties(nodeRole, true);

			//Create a List.Object node
			XmlNode lstObject = nodeRole.OwnerDocument.CreateElement("List.Object");

			// Get the Column names
			Dictionary<string, int> columNames = new Dictionary<string, int>();
			int i = 0;
			foreach (DataColumn column in rspRole.Columns)
			{
				columNames.Add(column.Caption, i);
				i++;
			}

			//For each instance --> Add the OID and the attributes
			for (int ItInst = 0; ItInst < rspRole.Rows.Count; ItInst++)
			{
				XmlNode nodeObject = nodeRole.OwnerDocument.CreateElement("Object");

				//Create a copy and add the attributes, then insert it in the <Object> node
				XmlNode nodeObjectOID = CreateOIDNodeFromResponse(nodeObject.OwnerDocument,
					Adaptor.ServerConnection.GetOid(rspRole, rspRole.Rows[ItInst]));
				nodeObject.AppendChild(nodeObjectOID);

				//Create a copy and add the properties
				XmlNode nodeObjectProperties = SetValueOfNodeAttributeFromInstance(nodeRoleProperties, rspRole.Rows[ItInst], true, columNames);
				nodeObject.AppendChild(nodeObjectProperties);

				//Process sub-roles
				ProcessRoles(nodeObjectProperties, Adaptor.ServerConnection.GetOid(rspRole, rspRole.Rows[ItInst]));

				//Add the <Object> node in <List.Object> node
				lstObject.AppendChild(nodeObject);
			}
			nodeRole.AppendChild(lstObject);
		}
		#endregion Process Role nodes


		#region Private Methods
		private static XmlNode CreateOIDNodeFromResponse(XmlDocument OwnerDocument, Oid oid)
		{
			XmlNode nodeOID = OwnerDocument.CreateElement("OID");
			nodeOID.Attributes.Append(OwnerDocument.CreateAttribute("Class.Name"));
			nodeOID.Attributes["Class.Name"].Value = oid.ClassName;

			XmlNode nodeOIDField = OwnerDocument.CreateElement("OID.Field");
			nodeOIDField.Attributes.Append(OwnerDocument.CreateAttribute("Value"));

			for (int It=0; It<oid.Fields.Count; It++)
			{
				nodeOIDField.Attributes["Value"].Value = oid.GetValues()[It].ToString();
				nodeOID.AppendChild(nodeOIDField.Clone());
			}
			return nodeOID;
		}

		/// <summary>
		/// Assign values to the attributes of the report, from the instance passed as parameter.
		/// </summary>
		/// <param name="nodeProperties">XmlNode to be filled. It contains tha empty attributes.</param>
		/// <param name="Instance">Instance object that contains the values for the attributes.</param>
		/// <param name="Cloning">If True: the returned node is a new one (cloned from original). False: the returned node is the same, but modified.</param>
		/// <param name="attPos">Attributes value positions in the DataRow, keyed by the Attribute name.</param>
		/// <returns>A XmlNode, that can be a new one or a modified one.</returns>
		private static XmlNode SetValueOfNodeAttributeFromInstance(XmlNode nodeProperties, DataRow Instance, bool Cloning, Dictionary<string, int> attPos)
		{
			XmlNode nodeProp = null;
			if (nodeProperties != null)
			{
				if (Cloning)
					nodeProp = nodeProperties.Clone();
				else
					nodeProp = nodeProperties;

				XmlNode[] aNodes = XMLQuery.SelectNodes(nodeProp ,"Attribute",false);

				int numReportAttr = aNodes.Length;
				for (int It = 0; It < numReportAttr; It++)
				{
					string Name = aNodes[It].Attributes["Name"].Value;

					XmlAttribute att =  aNodes[It].OwnerDocument.CreateAttribute("Value");
					if (Instance.ItemArray[attPos[Name]] == null)
						att.Value = "";
					else
						att.Value = Instance.ItemArray[attPos[Name]].ToString();

					aNodes[It].Attributes.Append(att);
				}
			}
			return nodeProp;
		}

		#endregion  Private Methods
	}
	#endregion EventReportRequest event for XML
}
