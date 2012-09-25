// v3.8.4.5.b
using System;
using System.Xml;

using SIGEM.Client.Oids;
using SIGEM.Client.PrintingDriver.Common;

namespace SIGEM.Client.PrintingDriver
{
	#region PrintToExcel
	public static class PrintToExcel
	{
		public static bool Print(Oid rootOID, ReportConfiguration reportConfig)
		{
			bool Rslt = false;

			//Create a Excel object and add the Template specified in ReportPath.
			object oExcel = Excel.GetExcel(false);
			object oExcelTemplate = Excel.GetExcelTemplate(
			Excel.GetExcelWorkbooks(oExcel), reportConfig.ReportFilePath);

			//Get the XML query requests.
			string XmlReport = Excel.DataRequest(rootOID, oExcelTemplate);
			if (XmlReport.Length > 0)
			{
				//Execute the queries.
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(XmlReport);
				XmlNode nodeRslt = PrintToXML.GetQueryXML(rootOID, xmlDoc.DocumentElement);
				Excel.DataProcessing(oExcelTemplate, nodeRslt.OuterXml);
				Excel.SetVisibleExcel(true, oExcel);
				Rslt = true;
			}

			return Rslt;
		}
	}
	#endregion PrintToExcel
}
