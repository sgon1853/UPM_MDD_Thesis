// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace SIGEM.Client
{
	#region Excel -> MS Excel COM Objects.
	public class Excel
	{
		/// <summary>
		/// MS Excel COM Objects.
		/// </summary>
		Excel() { }

		#region Methods for Excel Automation
		private static object GetExcel()
		{
			return GetExcel(false);
		}

		private static object GetExcel(bool Visible)
		{
#if (DEBUG)
			Visible = true;
#endif
			object oExcelObject = null;

			// Create the Excel Object.
			System.Type oExcel;
			oExcel = System.Type.GetTypeFromProgID("Excel.Application");
			oExcelObject = System.Activator.CreateInstance(oExcel);
			if ((Visible) && (oExcelObject != null))
			{
				Excel.SetVisibleExcel(true, oExcelObject);
			}
			return oExcelObject;
		}

		private static object GetExcelWorkbooks(object ExcelObject)
		{
			object ExcelWorkbooks = null;
			if (ExcelObject != null)
			{
				ExcelWorkbooks = ExcelObject.GetType().InvokeMember("Workbooks", BindingFlags.GetProperty, null, ExcelObject, null);
			}
			return ExcelWorkbooks;
		}

		private static object GetExcelWorksheet(object ExcelWorkbooksObject)
		{
			object ExcelWorksheet = null;
			if (ExcelWorkbooksObject != null)
			{
				ExcelWorksheet = ExcelWorkbooksObject.GetType().InvokeMember("Worksheets", BindingFlags.GetProperty, null, ExcelWorkbooksObject, null);
			}
			return ExcelWorksheet;
		}

		private static object ExcelWorkbooksAdd(object ExcelWorkbooks)
		{
			object ExcelWorkbook = null;
			if (ExcelWorkbooks != null)
			{
				object[] Parameters;
				Parameters = new Object[1];
				Parameters[0] = "";
				ExcelWorkbook = ExcelWorkbooks.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, ExcelWorkbooks, Parameters);
			}
			return ExcelWorkbook;
		}

		private static object SetExcelCell(int ExcelRowCell, int ExcelColumnCell, object ExcelCellValue, object ExcelObject)
		{
			object Excel = null;
			if (ExcelObject != null)
			{
				object[] Parameters;

				Parameters = new Object[3];
				Parameters[0] = ExcelRowCell;
				Parameters[1] = ExcelColumnCell;
				Parameters[2] = ExcelCellValue;
				ExcelObject.GetType().InvokeMember("Cells", BindingFlags.SetProperty, null, ExcelObject, Parameters);
			}
			return Excel;
		}

		private static bool SetVisibleExcel(bool visible, object ExcelObject)
		{
			bool Rslt;
			object[] Parameters;
			// ExcelObject.visible = false
			Rslt = (bool)ExcelObject.GetType().InvokeMember("Visible", BindingFlags.GetProperty, null, ExcelObject, null);
			Parameters = new Object[1];
			Parameters[0] = visible;
			ExcelObject.GetType().InvokeMember("Visible", BindingFlags.SetProperty, null, ExcelObject, Parameters);
			return Rslt;
		}

		private static object CreateExcelObject()
		{
			object excel = GetExcel();
			object excelBook = GetExcelWorkbooks(excel);
			object excelDoc = ExcelWorkbooksAdd(excelBook);

			return excel;
		}

		#region Export to Word
		public static void ExportToWord(DataTable dataTable)
		{

		}
		#endregion Export to Word

		#region Export to Excel
		/// <summary>
		/// Export the DataTable data to Excel
		/// </summary>
		/// <param name="dataTable">DataTable to export</param>
		public static void ExportToExcel(DataTable dataTable)
		{
			// Create the Excel object
			object excel = CreateExcelObject();

			// Insert the columns names at the first Excel row
			int columnIndex = 0;
			foreach (DataColumn col in dataTable.Columns)
			{
				columnIndex++;
				SetExcelCell(1, columnIndex, col.Caption, excel);
			}

			// Insert on each Excel cell the argument value of each row.
			int rowIndex = 0;
			foreach (DataRow row in dataTable.Rows)
			{
				rowIndex++;
				columnIndex = 0;
				foreach (DataColumn col in dataTable.Columns)
				{
					columnIndex++;
					SetExcelCell(rowIndex + 1, columnIndex, row[col.Caption].ToString(), excel);
				}
			}

			// Set the Excel document visible.
			SetVisibleExcel(true, excel);
		}

		/// <summary>
		/// Export the dataGridView data to Excel
		/// </summary>
		/// <param name="dataGridView">DataGridView to export</param>
		public static void ExportToExcel(DataGridView dataGridView)
		{
			SIGEM.Client.CancelWindow cancelWindow = new SIGEM.Client.CancelWindow();
			cancelWindow.Show();
			cancelWindow.MaximunProgressBarValue = dataGridView.RowCount;

			// Create the Excel object.
			object excel = CreateExcelObject();

			try
			{
				#region Insert columns names
				// Insert the columns names at the first Excel row
				int columnIndex = 0;
				foreach (DataGridViewColumn col in dataGridView.Columns)
				{
					columnIndex++;
					SetExcelCell(1, columnIndex, col.HeaderText, excel);

					// If the process is cancelled.
					if (cancelWindow.Cancel)
					{
						SetVisibleExcel(true, excel);
						cancelWindow.Close();
						return;
					}
				}
				#endregion Insert columns names

				#region Insert values
				// Insert on each Excel cell the argument value of each row.
				int rowIndex = 0;
				foreach (DataGridViewRow row in dataGridView.Rows)
				{
					rowIndex++;
					columnIndex = 0;
					foreach (DataGridViewColumn col in dataGridView.Columns)
					{
						columnIndex++;
						if (col is DataGridViewComboBoxColumn)
						{
							SetExcelCell(rowIndex + 1, columnIndex, row.Cells[col.Name].FormattedValue, excel);
						}
						else
						{
							if (row.Cells[col.Name].Value.GetType().Name == "TimeSpan")
								SetExcelCell(rowIndex + 1, columnIndex, row.Cells[col.Name].Value.ToString(), excel);
							else
								SetExcelCell(rowIndex + 1, columnIndex, row.Cells[col.Name].Value, excel);
						}

						// If the process is cancelled.
						if (cancelWindow.Cancel)
						{
							SetVisibleExcel(true, excel);
							cancelWindow.Close();
							return;
						}
					}
					cancelWindow.Value = rowIndex;
				}
				#endregion Insert values
			}
			catch (Exception e)
			{
				SetVisibleExcel(true, excel);
				cancelWindow.Close();
				Presentation.ScenarioManager.LaunchErrorScenario(e);
			}

			// Close the cancel window.
			cancelWindow.Close();

			// Set the Excel document visible.
			SetVisibleExcel(true, excel);
		}

		/// <summary>
		/// Export ListView data to Excel
		/// </summary>
		/// <param name="listView">ListView to export</param>
		public static void ExportToExcel(ListView listView)
		{
			// Create the Excel object.
			object excel = CreateExcelObject();

			try
			{
				#region Insert columns names
				// Insert the columns names at the first Excel row.
				int columnIndex = 0;
				foreach (ColumnHeader col in listView.Columns)
				{
					columnIndex++;
					SetExcelCell(1, columnIndex, col.Text, excel);
				}
				#endregion Insert columns names

				#region Insert data values
				// Insert on each Excel cell the arguments name and the arguments value.
				for (int index = 0; index < listView.Items.Count; index++)
				{
					SetExcelCell(index + 2, 1, listView.Items[index].SubItems[0].Text, excel);
					SetExcelCell(index + 2, 2, listView.Items[index].SubItems[1].Text, excel);
				}
				#endregion Insert data values
			}
			catch (Exception e)
			{
				SetVisibleExcel(true, excel);
				Presentation.ScenarioManager.LaunchErrorScenario(e);
			}

			// Set the Excel document visible.
			SetVisibleExcel(true, excel);
		}
		#endregion Export to Excel

		#endregion Methods for Excel Automation
	}
	#endregion Excel -> MS Excel COM Objects.
}
