// v3.8.4.5.b
using System;
using System.Xml;
using SIGEM.Client.Oids;
using SIGEM.Client.PrintingDriver.Common;

namespace SIGEM.Client.PrintingDriver
{

	#region PrintToWord
	public static class PrintToWord 
	{

		public static bool Print(Oid rootOID, ReportConfiguration reportConfig, bool preview, int numCopies, string printerName, string fileName)
		{
			bool Rslt = false;

			//Create a Word object and add the Template specified in ReportPath.
			object oWord = Word.GetWord(false);
			object oWordTemplate = Word.GetWordDocumentTemplate(
				Word.GetWordDocuments(oWord), reportConfig.ReportFilePath);

			//Get the XML query requests.
			string XmlReport = Word.DataRequest(rootOID, oWordTemplate);
			if (XmlReport.Length > 0)
			{
				try
				{
					//Execute the queries.
					XmlDocument xmlDoc = new XmlDocument();
					xmlDoc.LoadXml(XmlReport);

					XmlNode nodeRslt = PrintToXML.GetQueryXML(rootOID, xmlDoc.DocumentElement);
					Word.DataProcessing(oWordTemplate, nodeRslt.OuterXml);

					// Ask for extra information, Preview or not, number of copies ...

					if (preview)
					{
						Word.SetVisibleWord(true, oWord);
					}
					else
					{
						// If filename has values, save the document else print it
						if (fileName != "")
						{
							Word.SaveAs(oWordTemplate, fileName);
							Word.Close(WordSaveOptions.wdDoNotSaveChanges, oWordTemplate);
						}
						else
						{
							// Print and close Word
							Word.ActivePrinter(oWord, printerName);
							Word.PrintOut(oWordTemplate, numCopies);
							// Before closing the created document, it must be saved.
							//  Save in the temp folder and delete it
							string tempFileName = System.IO.Path.GetTempFileName();
							Word.SaveAs(oWordTemplate, tempFileName);
							Word.Close(WordSaveOptions.wdDoNotSaveChanges, oWordTemplate);
							System.IO.File.Delete(tempFileName);
						}
						// Quit from Word
						Word.Quit(WordSaveOptions.wdDoNotSaveChanges, oWord);
					}
				}
				catch(Exception e)
				{
					Word.Quit(WordSaveOptions.wdDoNotSaveChanges,oWord);
					throw new Exception(e.Message,e);
				}
				Rslt = true;
			}
			return Rslt;
		}
		#endregion
	}
}
