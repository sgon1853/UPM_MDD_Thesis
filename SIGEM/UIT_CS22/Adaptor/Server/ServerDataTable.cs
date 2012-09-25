// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Data;
using System.Data.Common;

using SIGEM.Client.Oids;
using SIGEM.Client.Adaptor.DataFormats;
using SIGEM.Client.Adaptor.Exceptions;

namespace SIGEM.Client.Adaptor
{
	#region Constants for Data Table Properties
	/// <summary>
	/// Constants for Data Table Properties.
	/// Is used to identify the type of information obtained of the XML server response.
	/// </summary>
	public static class DataTableProperties
	{
		public const string DisplaySetNames = "DisplaySet";
		public const string OidFieldNames = "Oid";
		/// <summary>
		/// Constants for Data Column Properties.
		/// Is used to identify the type of information obtained of the XML server response.
		/// </summary>
		public static class DataColumnProperties
		{
			public const string ModelType = "ModelType";
			public const string IsDisplaySetItem = "IsDisplaySetItem";
			public const string IsOidItem = "IsOidItem";
			internal const string Index = "Index";
		}
	}
	#endregion Constants for Data Table Properties

	#region ServerConnection -> Aux Method
	public partial class ServerConnection
	{
		#region Auxiliar methods for GetDisplaySetColumns & GetOidColums [Private]
		/// <summary>
		/// Gets a set of a given property from a DataTable.
		/// </summary>
		/// <param name="dataTable">DataTable the set is given from.</param>
		/// <param name="propertyName">The column required.</param>
		/// <returns>Returns the set of indicated columns.</returns>
		private static List<DataColumn> GetSetOfColumns(DataTable dataTable, string propertyName)
		{
			List<DataColumn> lResult = null;
			List<string> lOidFields = dataTable.ExtendedProperties[propertyName] as List<string>;
			if (lOidFields != null)
			{
				lResult = new List<DataColumn>();
				foreach (string lName in lOidFields)
				{
					lResult.Add(dataTable.Columns[lName]);
				}
			}
			return lResult;
		}
		#endregion Auxiliar methods for GetDisplaySetColumns & GetOidColums

		#region Get the DisplaySet from DataTable
		/// <summary>
		/// Gets a set of DisplaySet columns from a DataTable.
		/// </summary>
		/// <param name="dataTable">DataTable the set is given from.</param>
		/// <returns>Returns the set of DisplaySet.</returns>
		public static List<DataColumn> GetDisplaySetColumns(DataTable dataTable)
		{
			return GetSetOfColumns(dataTable, DataTableProperties.DisplaySetNames);
		}
		#endregion Get the DisplaySet from DataTable

		#region Get the OID columns from DataTable
		/// <summary>
		/// Gets the Oid columns from a DataTable.
		/// </summary>
		/// <param name="dataTable">DataTable the set is given from.</param>
		/// <returns>Returns the set of Oid.</returns>
		public static List<DataColumn> GetOidColumns(DataTable dataTable)
		{
			return GetSetOfColumns(dataTable, DataTableProperties.OidFieldNames);
		}
		#endregion Get the OID columns from DataTable

		#region Get the Oid from a DataRow
		/// <summary>
		/// Gets the Oid from a DataRow. The DataRow is a row of the DataTable.
		/// </summary>
		/// <param name="dataTable">DataTable the Oid is given from.</param>
		/// <param name="row">Row the Oid is requested.</param>
		/// <returns>Returns the Oid corresponding to the DataRow.</returns>
		public static Oid GetOid(DataTable dataTable, DataRow row)
		{
			return GetOid(dataTable, row, string.Empty);
		}
		/// <summary>
		/// Gets the Oid from a DataRow. The DataRow is a row of the DataTable.
		/// </summary>
		/// <param name="dataTable">DataTable the Oid is given from.</param>
		/// <param name="row">Row the Oid is requested.</param>
		/// <param name="alternateKeyName">Name of the alternate key, if proceed.</param>
		/// <returns>Returns the Oid corresponding to the DataRow.</returns>
		public static Oid GetOid(DataTable dataTable, DataRow row, string alternateKeyName)
		{
			Oid resultOid = null;
			List<DataColumn> oidFields = GetOidColumns(dataTable);
			if (oidFields != null)
			{
				resultOid = Oid.Create(dataTable.TableName);
				resultOid.Fields.Clear();
				foreach (DataColumn lDataColumn in oidFields)
				{
					ModelType type = (ModelType)lDataColumn.ExtendedProperties[DataTableProperties.DataColumnProperties.ModelType];
					object rowValue = row[lDataColumn.Ordinal];
					if (rowValue == null || rowValue == DBNull.Value)
					{
						return null;
					}

					if (type == ModelType.String && rowValue.ToString().Trim().Length == 0)
					{
						return null;
					}

					IOidField oidField = FieldList.CreateField(string.Empty, type);
					oidField.Value = rowValue;
					resultOid.Fields.Add(oidField);
				}

				string lAlternateKeyName = alternateKeyName;
				if (lAlternateKeyName == string.Empty)
				{
					// If no alternateKeyName is specified, ask to the current oid.
					lAlternateKeyName = resultOid.AlternateKeyName;
				}

				if (lAlternateKeyName != string.Empty)
				{
					// Try to load the alternate key fields with the values contained in the row.
					IOid auxAlternateKey = resultOid.GetAlternateKey(lAlternateKeyName);
					if ((auxAlternateKey as AlternateKey) != null)
					{
						foreach (IOidField oidField in auxAlternateKey.Fields)
						{
							// It is not guaranteed if the alternate key field is in the datatable.
							if (dataTable.Columns.Contains(oidField.Name))
							{
								oidField.Value = row[oidField.Name];
							}
						}
					}
				}
			}
			return resultOid;
		}
		#endregion Get the Oid from DataRow

		#region Get Last Oid from a DataTable
		/// <summary>
		/// Gets Last Oid from a DataTable.
		/// </summary>
		/// <param name="dataTable">DataTable reference.</param>
		/// <returns>The last Oid from the DataTable.</returns>
		public static Oid GetLastOid(DataTable dataTable)
		{
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				DataRow lastRow = dataTable.Rows[dataTable.Rows.Count - 1];
				return GetOid(dataTable, lastRow);
			}
			return null;
		}
		#endregion Get Last Oid from a DataTable
	}
	#endregion ServerConnection -> Aux Method
}

