// v3.8.4.5.b
using System;
using System.Reflection;

using SIGEM.Client.Oids;

namespace SIGEM.Client.PrintingDriver
{
	#region Formats for save Word Documents
	public enum WordSaveFormat
	{
		wdFormatDocument=0,
		wdFormatDOSText=4,
		wdFormatDOSTextLineBreaks=5,
		wdFormatEncodedText=7,
		wdFormatHTML=8,
		wdFormatRTF=6,
		wdFormatTemplate=1,
		wdFormatText=2,
		wdFormatTextLineBreaks=3,
		wdFormatUnicodeText=7
	}
	#endregion

	#region Opion in Save
	public enum WordSaveOptions
	{
		wdDoNotSaveChanges = 0,
		wdSaveChanges = -1,
		wdPromptToSaveChanges = -2
	}
	#endregion

	#region Word -> MS Word COM Objects
	public class Word
	{
		/// <summary>
		/// MS Word COM Objects
		/// </summary>

		Word(){}

		#region Methods for Word Automation

		public static object GetWord()
		{
			return GetWord(false);
		}
		public static object GetWord(bool Visible)
		{
			object oWordObject = null;

			// Create the Word Object.
			System.Type oWord;
			oWord = System.Type.GetTypeFromProgID("Word.Application");
			oWordObject = System.Activator.CreateInstance(oWord);
			if ((Visible) && (oWordObject != null))
			{
				Word.SetVisibleWord(true,oWordObject);
			}

			return oWordObject;
		}

		public static object GetWordDocuments(object WordObject)
		{
			object WordDocuments = null;
			if (WordObject != null)
			{
				WordDocuments = WordObject.GetType().InvokeMember( "Documents",BindingFlags.GetProperty, null, WordObject, null );
			}
			return WordDocuments;
		}

		public static object GetWordDocumentTemplate(object WordDocuments, string PathTemplate)
		{
			PathTemplate = System.IO.Path.GetFullPath(PathTemplate);

			object WordDocument =null;
			object[] Parameters;

			// New Word document, based in the template
			Parameters = new object[4];
			Parameters[0] = PathTemplate;
			Parameters[1] = true;
			Parameters[1] = false;
			Parameters[2] = 0;
			Parameters[3] = true;

			WordDocument = WordDocuments.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, WordDocuments, Parameters);

			return WordDocument;
		}

		public static void Close(WordSaveOptions SaveOptions, object WordDocument)
		{
			object[] Parameters = InitializeParameters(3);
			Parameters[0] = SaveOptions;
			WordDocument.GetType().InvokeMember("Close", BindingFlags.InvokeMethod, null, WordDocument, null);
			System.Runtime.InteropServices.Marshal.ReleaseComObject(WordDocument);
			GC.Collect(); // force final cleanup!
		}

		public static void Quit(WordSaveOptions SaveOptions, object WordObject)
		{
			Quit(SaveOptions,WordObject,null);
		}
		public static void Quit(WordSaveOptions SaveOptions, object WordObject,object WordDocuments)
		{
			object[] Parameters = InitializeParameters(3);
			Parameters[0] = SaveOptions;

			if(WordDocuments != null)
			{
				Close(SaveOptions,WordDocuments);
			}
			WordObject.GetType().InvokeMember("Quit", BindingFlags.InvokeMethod, null, WordObject, null);

			System.Runtime.InteropServices.Marshal.ReleaseComObject (WordObject);
			GC.Collect(); // force final cleanup!
		}
		public static bool SetVisibleWord(bool visible, object WordObject)
		{
			bool Rslt;
			object[] Parameters;
			Rslt =  (bool) WordObject.GetType().InvokeMember( "Visible", BindingFlags.GetProperty,null, WordObject, null );
			Parameters = new Object[1];
			Parameters[0] = visible;
			WordObject.GetType().InvokeMember( "Visible", BindingFlags.SetProperty,null, WordObject, Parameters );
			return Rslt;
		}
		public static string ActivePrinter(object WordObject, string ActivePrinter)
		{
			string Rslt;
			object[] Parameters;

			object AppWordObject = WordObject.GetType().InvokeMember("Application",BindingFlags.GetProperty,null, WordObject, null );

			Rslt = (string)AppWordObject.GetType().InvokeMember("ActivePrinter", BindingFlags.GetProperty,null, WordObject, null );
			Parameters = new Object[1];
			Parameters[0] = ActivePrinter;
			AppWordObject.GetType().InvokeMember( "ActivePrinter", BindingFlags.SetProperty,null, WordObject, Parameters );
			return Rslt;
		}
		public static void PrintOut(object WordObject, int copies)
		{
			object[] Parameters;
			Parameters = InitializeParameters(18);

			#region Params PrintOut
			//	Params of method PrintOut
			//------------------------------------------------------
			// [Background]				0
			// [Append]					1
			// [Range]					2
			// [OutputFileName]			3
			// [From]					4
			// [To]						5
			// [Item]					6
			// [Copies]					7
			Parameters[7] = copies;
			// [Pages]					8
			// [PageType]				9
			// [PrintToFile]			10
			// [Collate]				11
			// [ActivePrinterMacGX]		12
			// [ManualDuplexPrint]		13
			// [PrintZoomColumn]		14
			// [PrintZoomRow]			15
			// [PrintZoomPaperWidth]	16
			// [PrintZoomPaperHeight]	17
			#endregion

			WordObject.GetType().InvokeMember("PrintOut", BindingFlags.InvokeMethod,null, WordObject, Parameters );

		}

		public static void SaveAs(object WordObject, string FileName)
		{
			object[] Parameters;
			Parameters = InitializeParameters(11);
			Parameters[0] = FileName;
			WordObject.GetType().InvokeMember("SaveAs",BindingFlags.InvokeMethod,null,WordObject,Parameters);
		}
		public static void SaveAs(object WordObject, string FileName, WordSaveFormat WordFormat)
		{
			#region Params SaveAs
//			expresión.SaveAs(
//				FileName,
//				FileFormat,
//				LockComments,
//				Password,
//				AddToRecentFiles,
//				WritePassword,
//				ReadOnlyRecommended,
//				EmbedTrueTypeFonts,
//				SaveNativePictureFormat,
//				SaveFormsData,
//				SaveAsAOCELetter)
			#endregion

			object[] Parameters;
			Parameters = InitializeParameters(11);
			Parameters[0] = FileName;
			Parameters[1] = WordFormat;

			WordObject.GetType().InvokeMember("SaveAs",BindingFlags.InvokeMethod,null,WordObject,Parameters);

		}

		#region Initialize Params for Invoke Methods
		private static object[] InitializeParameters(int ParamNumber)
		{
			object[] Parameters = new object[ParamNumber];
			if(Parameters != null)
			{
				for(int It=0; It< Parameters.Length; It++)
				{
					Parameters[It] = System.Reflection.Missing.Value;
				}
			}
			return Parameters;
		}
		#endregion

		#endregion

		#region Methods in VBA template of Word
		public static string DataRequest(Oid TheOID, string PathTemplate)
		{
			return DataRequest(TheOID,PathTemplate,true);
		}
		public static string DataRequest(Oid TheOID, string PathTemplate, bool Close)
		{
			string rslt;
			object WordObject = GetWord();
			rslt = DataRequest(TheOID,GetWordDocumentTemplate(GetWordDocuments(WordObject),PathTemplate));
			if(Close)
			{
				Word.Quit(WordSaveOptions.wdDoNotSaveChanges, WordObject);
			}

			return rslt;
		}

		public static string DataRequest(Oid TheOID, object WordDocument)
		{
			object[] Parameters;
			Parameters = new Object[1];
			Parameters[0] = OIDFields(TheOID,WordDocument);
			return (string) WordDocument.GetType().InvokeMember("DataRequest",BindingFlags.InvokeMethod,null,WordDocument,Parameters);
		}

		private static object OIDFields(Oid TheOID, object WordDocument)
		{
			object Colletion;
			Colletion = WordDocument.GetType().InvokeMember("OIDColletion",BindingFlags.InvokeMethod,null,WordDocument,null);

			object[] Parameters;
			Parameters = new Object[4];

			Parameters[1] = System.Reflection.Missing.Value;	//[Key]
			Parameters[2] = System.Reflection.Missing.Value;	//[Before]
			Parameters[3] = System.Reflection.Missing.Value;	//[After]

			foreach (object field in TheOID.GetValues())
			{
				Parameters[0] = field;
				Colletion.GetType().InvokeMember("Add",BindingFlags.InvokeMethod,null,Colletion,Parameters);
			}
			return Colletion;
		}

		public static bool DataProcessing(string PathTemplate, string xmlData)
		{
			bool Rslt;
			object WordObject = GetWord();
			Rslt = DataProcessing(GetWordDocumentTemplate(GetWordDocuments(WordObject),PathTemplate),xmlData);
			return Rslt;
		}

		public static bool DataProcessing(object WordDocument, string xmlData)
		{
			bool Rslt=false;
			object[] Parameters;
			Parameters = new Object[1];
			Parameters[0] = xmlData;
			Rslt = (bool)WordDocument.GetType().InvokeMember("DataProcessing",BindingFlags.InvokeMethod,null,WordDocument,Parameters);
			return  Rslt;
		}
		#endregion

	}
	#endregion
}
