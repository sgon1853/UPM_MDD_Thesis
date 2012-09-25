// v3.8.4.5.b
using System;
using System.Reflection;

using SIGEM.Client.Oids;

namespace SIGEM.Client.PrintingDriver
{
	#region Excel -> MS Excel COM Objects
	public class Excel
	{
		/// <summary>
		/// MS Excel COM Objects
		/// </summary>
		Excel() { }

		#region Methods for Excel Automation
		public static object GetExcel()
		{
			return GetExcel(false);
		}

		public static object GetExcel(bool Visible)
		{
			object oExcelObject = null;

			// Create the Excel Object.
			System.Type oExcel;
			oExcel = System.Type.GetTypeFromProgID("Excel.Application");
			oExcelObject = System.Activator.CreateInstance(oExcel);
			if ((Visible) && (oExcelObject != null))
			Excel.SetVisibleExcel(true, oExcelObject);

			return oExcelObject;
		}

		public static object GetExcelWorkbooks(object ExcelObject)
		{
			object ExcelWorkbooks = null;
			if (ExcelObject != null)
			{
				ExcelWorkbooks = ExcelObject.GetType().InvokeMember("Workbooks", BindingFlags.GetProperty, null, ExcelObject, null);
			}

			return ExcelWorkbooks;
		}

		public static object GetExcelTemplate(object ExcelWorkbooks, string PathTemplate)
		{
			PathTemplate = System.IO.Path.GetFullPath(PathTemplate);

			object ExcelDocument = null;
			object[] Parameters;

			// New Excel document, based in the template
			Parameters = new object[1];
			Parameters[0] = PathTemplate;

			ExcelDocument = ExcelWorkbooks.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, ExcelWorkbooks, Parameters);

			return ExcelDocument;
		}

		public static bool SetVisibleExcel(bool visible, object ExcelObject)
		{
			bool Rslt;
			object[] Parameters;
			// ExcelObject.visible = false
			Rslt =  (bool) ExcelObject.GetType().InvokeMember( "Visible", BindingFlags.GetProperty,null, ExcelObject, null );
			Parameters = new Object[1];
			Parameters[0] = visible;
			ExcelObject.GetType().InvokeMember( "Visible", BindingFlags.SetProperty,null, ExcelObject, Parameters );

			return Rslt;
		}

		public static string ActivePrinter(object ExcelObject, string ActivePrinter)
		{
			string Rslt;
			object[] Parameters;

			object AppExcelObject = ExcelObject.GetType().InvokeMember("Application",BindingFlags.GetProperty,null, ExcelObject, null );

			Rslt =  (string)AppExcelObject.GetType().InvokeMember("ActivePrinter", BindingFlags.GetProperty,null, ExcelObject, null );
			Parameters = new Object[1];
			Parameters[0] = ActivePrinter;
			AppExcelObject.GetType().InvokeMember( "ActivePrinter", BindingFlags.SetProperty,null, ExcelObject, Parameters );

			return Rslt;
		}

		public static void PrintOut(object ExcelObject, int copies)
		{
			object[] Parameters;
			Parameters = InitializeParameters(18);

			#region Params PrintOut
			//	Params of method PrintOut
			//------------------------------------------------------
			// [Background]			0
			// [Append]			1
			// [Range]			2
			// [OutputFileName]		3
			// [From]			4
			// [To]				5
			// [Item]			6
			// [Copies]			7
			Parameters[7] = copies;
			// [Pages]			8
			// [PageType]			9
			// [PrintToFile]		10
			// [Collate]			11
			// [ActivePrinterMacGX]		12
			// [ManualDuplexPrint]		13
			// [PrintZoomColumn]		14
			// [PrintZoomRow]		15
			// [PrintZoomPaperWidth]	16
			// [PrintZoomPaperHeight]	17
			#endregion Params PrintOut

			ExcelObject.GetType().InvokeMember("PrintOut", BindingFlags.InvokeMethod,null, ExcelObject, Parameters );
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
		#endregion Initialize Params for Invoke Methods

		#endregion Methods for Excel Automation

		#region Methods in VBA template of Excel
		public static string DataRequest(Oid TheOID, string PathTemplate)
		{
			string rslt;
			object ExcelObject = GetExcel();
			rslt = DataRequest(TheOID, GetExcelTemplate(GetExcelWorkbooks(ExcelObject), PathTemplate));

			return rslt;
		}

		public static string DataRequest(Oid TheOID, object ExcelDocument)
		{
			object[] Parameters;
			Parameters = new Object[1];
			Parameters[0] = OIDFields(TheOID, ExcelDocument);

			return (string)ExcelDocument.GetType().InvokeMember("DataRequest", BindingFlags.InvokeMethod, null, ExcelDocument, Parameters);
		}

		private static object OIDFields(Oid TheOID, object ExcelDocument)
		{
			object Colletion;
			Colletion = ExcelDocument.GetType().InvokeMember("OIDColletion", BindingFlags.InvokeMethod, null, ExcelDocument, null);

			object[] Parameters;
			Parameters = new Object[4];

			Parameters[1] = System.Reflection.Missing.Value; //[Key]
			Parameters[2] = System.Reflection.Missing.Value; //[Before]
			Parameters[3] = System.Reflection.Missing.Value; //[After]

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
			object ExcelObject = GetExcel();
			Rslt = DataProcessing(GetExcelTemplate(GetExcelWorkbooks(ExcelObject), PathTemplate), xmlData);

			return Rslt;
		}

		public static bool DataProcessing(object ExcelDocument, string xmlData)
		{
			bool Rslt=false;
			object[] Parameters;
			Parameters = new Object[1];
			Parameters[0] = xmlData;
			Rslt = (bool)ExcelDocument.GetType().InvokeMember("DataProcessing", BindingFlags.InvokeMethod, null, ExcelDocument, Parameters);

			return  Rslt;
		}
		#endregion Methods in VBA template of Excel
	}
	#endregion Excel -> MS Excel COM Objects
}
