// v3.8.4.5.b
using System;
using System.Xml;
using System.Data;
using System.Collections.Generic;

using SIGEM.Client.Oids;
using SIGEM.Client.PrintingDriver.Common;

namespace SIGEM.Client.PrintingDriver
{
	#region EventReportRequest event for XML
	#endregion EventReportRequest event for XML

	#region PrintToDataSets class: Execute a set of queries that are specified in XML, and returns another XML
	public static class PrintToDataSets
	{
		/// <summary>
		/// Fills and return a DataSet processing the data set.
		/// </summary>
		/// <param name="dataTable">DataTable to start the query.</param>
		/// <param name="dataSetFile">Path of the data set file.</param>
		/// <returns>A DataSet object with the suitable data.</returns>
		public static DataSet GetData(DataTable dataTable, string dataSetFile)
		{
			DataSet lDataSet = new DataSet();
			try
			{
				lDataSet.ReadXmlSchema(dataSetFile);
			}
			catch (Exception e)
			{
				object[] lArgs = new object[1];
				lArgs[0] = dataSetFile;
				string lErrorMessage = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_ERROR_DATASET_GENERICLOAD, LanguageConstantValues.L_ERROR_DATASET_GENERICLOAD, lArgs);

				Exception newException = new Exception(lErrorMessage, e);
				throw newException;
			}


			// Analize the Tables in the DataSet.
			DataTable lStartingClass = GetDataTable(lDataSet, dataTable.TableName);
			try 
			{
				lStartingClass.Merge(dataTable);
			}
			catch
			{
				DataRow[] lErrorRows = lStartingClass.GetErrors();
				foreach (DataRow errorRow in lErrorRows)
				{
					Exception newException = new Exception(errorRow.RowError);
					throw newException;
				}
			}
			

			// Process Table relationships.
			foreach (DataRow lRow in lStartingClass.Rows)
			{
				Oid lOid = Adaptor.ServerConnection.GetOid(lStartingClass, lRow);
				ProcessTableRelationships(lDataSet, lStartingClass, lOid);
			}
			
			return lDataSet;
		}
		/// <summary>
		/// Fills and return a DataSet processing the data set file and starting with the oid.
		/// </summary>
		/// <param name="oid">Oid of the instance.</param>
		/// <param name="dataSetFile">Path of the data set file.</param>
		/// <returns>A DataSet object with the suitable data.</returns>
		public static DataSet GetData(Oid oid, string dataSetFile)
		{
			DataSet lDataSet = new DataSet();
			try
			{
				lDataSet.ReadXmlSchema(dataSetFile);
			}
			catch (Exception e)
			{
				object[] lArgs = new object[1];
				lArgs[0] = dataSetFile;
				string lErrorMessage = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_ERROR_DATASET_GENERICLOAD, LanguageConstantValues.L_ERROR_DATASET_GENERICLOAD, lArgs);

				Exception newException = new Exception(lErrorMessage, e);
				throw newException;
			}

			// Analize the Tables in the DataSet.
			DataTable lStartingClass = GetDataTable(lDataSet, oid.ClassName);

			string lDisplaySet = GetDisplaySet(lStartingClass);
			DataTable rootInstance = Logics.Logic.ExecuteQueryInstance(Logics.Logic.Agent, oid, lDisplaySet);
			// Add data to DataSet.
			lStartingClass.Merge(rootInstance);
			
			// Process Table relationships.
			ProcessTableRelationships(lDataSet, lStartingClass, oid);

			return lDataSet;
		}
		/// <summary>
		/// Analize table relationships and get related data.
		/// </summary>
		/// <param name="dataSet">DataSet that is filled with the retrieved instances.</param>
		/// <param name="dataTable">Table whose relations are going to be processed.</param>
		/// <param name="oid">Oid related.</param>
		private static void ProcessTableRelationships(DataSet dataSet, DataTable dataTable, Oid oid)
		{

			// Process the Parent relations.
			foreach (DataRelation lRelation in dataTable.ParentRelations)
			{
				// Only has to be processed the relations that are marked as PARENT in property PROCESS.
				string lHasToBeProcessedRelation = lRelation.ExtendedProperties["PROCESS"] as string;
				if (lHasToBeProcessedRelation == null || lHasToBeProcessedRelation == "")
				{
					object[] lArgs = new object[1];
					lArgs[0] = "PROCESS";
					string lErrorMessage = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_ERROR_DATASET_DEFINITION, LanguageConstantValues.L_ERROR_DATASET_DEFINITION, lArgs);
					throw new Exception(lErrorMessage);
				}
				// If not has to be processed --> Skip and continue.
				if (!lHasToBeProcessedRelation.Equals("PARENT", StringComparison.InvariantCultureIgnoreCase))
				{
					continue;
				}

				string lRelatedClassDataTable = lRelation.ExtendedProperties["RELATEDCLASSDATATABLE"] as string;
				DataTable lRelatedInstances = ProcessRelationship(dataSet, lRelation, oid, true);
				foreach (DataRow lRow in lRelatedInstances.Rows)
				{
					Oid lOid = Adaptor.ServerConnection.GetOid(lRelatedInstances, lRow);
					ProcessTableRelationships(dataSet, dataSet.Tables[lRelatedClassDataTable], lOid);
				}
			}

			// Process the Child relations.
			foreach (DataRelation lRelation in dataTable.ChildRelations)
			{
				// Only has to be processed the relations that are marked as CHILD in property PROCESS.
				string lHasToBeProcessedRelation = lRelation.ExtendedProperties["PROCESS"] as string;
				if (lHasToBeProcessedRelation == null || lHasToBeProcessedRelation == "")
				{
					object[] lArgs = new object[1];
					lArgs[0] = "PROCESS";
					string lErrorMessage = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_ERROR_DATASET_DEFINITION, LanguageConstantValues.L_ERROR_DATASET_DEFINITION, lArgs);
					throw new Exception(lErrorMessage);
				}

				// If not has to be processed --> Skip and continue.
				if (!lHasToBeProcessedRelation.Equals("CHILD", StringComparison.InvariantCultureIgnoreCase))
				{
					continue;
				}

				string lRelatedClassDataTable = lRelation.ExtendedProperties["RELATEDCLASSDATATABLE"] as string;

				DataTable lRelatedInstances = ProcessRelationship(dataSet, lRelation, oid, false);
				foreach (DataRow lRow in lRelatedInstances.Rows)
				{
					Oid lOid = Adaptor.ServerConnection.GetOid(lRelatedInstances, lRow);
					ProcessTableRelationships(dataSet, dataSet.Tables[lRelatedClassDataTable], lOid);
				}
			}
			
		}

		/// <summary>
		/// Gets the related instances according to the information in the DataRelation object. 
		/// </summary>
		/// <param name="dataSet">DataSet that is filled with the retrieved instances.</param>
		/// <param name="relation">DataRelation to be processed.</param>
		/// <param name="oid">Oid related.</param>
		/// <param name="asParent">Indicate if the relation is processed as Parent or not.</param>
		/// <returns>A DataTable with the related instances.</returns>
		private static DataTable ProcessRelationship(DataSet dataSet, DataRelation relation, Oid oid, bool asParent)
		{
			string lRelatedClass = relation.ExtendedProperties["RELATEDCLASS"] as string;
			if (lRelatedClass == null || lRelatedClass == "")
			{
				object[] lArgs = new object[1];
				lArgs[0] = "RELATEDCLASS";
				string lErrorMessage = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_ERROR_DATASET_DEFINITION, LanguageConstantValues.L_ERROR_DATASET_DEFINITION, lArgs);
				throw new Exception(lErrorMessage);
			}

			string lInvRole = relation.ExtendedProperties["INVERSEROLE"] as string;
			if (lInvRole == null || lInvRole == "")
			{
				object[] lArgs = new object[1];
				lArgs[0] = "INVERSEROLE";
				string lErrorMessage = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_ERROR_DATASET_DEFINITION, LanguageConstantValues.L_ERROR_DATASET_DEFINITION, lArgs);
				throw new Exception(lErrorMessage);
			}

			string lRelatedClassDataTable = relation.ExtendedProperties["RELATEDCLASSDATATABLE"] as string;
			if (lRelatedClassDataTable == null || lRelatedClassDataTable == "")
			{
				object[] lArgs = new object[1];
				lArgs[0] = "RELATEDCLASSDATATABLE";
				string lErrorMessage = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_ERROR_DATASET_DEFINITION, LanguageConstantValues.L_ERROR_DATASET_DEFINITION, lArgs);
				throw new Exception(lErrorMessage);
			}

			DataTable lRelatedTable = GetDataTable(dataSet, lRelatedClassDataTable);
			DataTable lTable = null;
			if (asParent)
			{
				lTable = relation.ChildTable;
			}
			else
			{
				lTable = relation.ParentTable;
			}

			// Add the info about the related object.
			Dictionary<string, Oid> linkItems = new Dictionary<string, Oid>(StringComparer.CurrentCultureIgnoreCase);
			linkItems.Add(lInvRole, oid);
			DataTable lRspRole = null;
			string lDisplaySet = GetDisplaySet(lRelatedTable);
			try
			{
				lRspRole = Logics.Logic.Adaptor.ExecuteQueryRelated(
					Logics.Logic.Agent, lRelatedClass, linkItems, lDisplaySet, "", null, 0);
			}
			catch (Exception e)
			{
				object[] lArgs = new object[2];
				lArgs[0] = lTable.TableName;
				lArgs[1] = lInvRole;
				string lErrorMessage = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_ERROR_DATASET_QUERYRELATED, LanguageConstantValues.L_ERROR_DATASET_QUERYRELATED, lArgs);
				throw new Exception(lErrorMessage, e);
			}
			lRelatedTable.Merge(lRspRole);

			// If it is a MM relationship.
			string lMMTableName = relation.ExtendedProperties["MMTABLENAME"] as string;
			if (lMMTableName != "")
			{
				// Manage MM relationship.
				DataTable lMMTable = GetDataTable(dataSet, lMMTableName);

				object[] lRowData = new object[lTable.PrimaryKey.Length + lRelatedTable.PrimaryKey.Length];
				foreach (DataRow lRow in lRspRole.Rows)
				{
					int i = 0;
					foreach (object lOidField in oid.GetValues())
					{
						lRowData.SetValue(lOidField, i);
						i++;
					}
					Oid lOid = Adaptor.ServerConnection.GetOid(lRspRole, lRow);
					foreach (object lOidField in lOid.GetValues())
					{
						lRowData.SetValue(lOidField, i);
						i++;
					}

					// Add the row into the MM table.
					try
					{
						if (!lMMTable.Rows.Contains(lRowData))
						{
							lMMTable.Rows.Add(lRowData);
						}
					}
					catch{ }

				}
			}

			return lRspRole;
		}

		private static string GetDisplaySet(DataTable lStartingClass)
		{
			string lDisplaySet = "";
			foreach (DataColumn lCol in lStartingClass.Columns)
			{
				if (lDisplaySet != "")
					lDisplaySet += ",";
				lDisplaySet += lCol.ColumnName;
			}

			return lDisplaySet;
		}

		private static DataTable GetDataTable(DataSet lDataSet, string dataTableName)
		{
			DataTable lTable = lDataSet.Tables[dataTableName];
			if (lTable == null)
			{
				object[] lArgs = new object[2];
				lArgs[0] = dataTableName;
				lArgs[1] = lDataSet.DataSetName;
				string lErrorMessage = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_ERROR_DATASET_TABLENOTFOUND, LanguageConstantValues.L_ERROR_DATASET_TABLENOTFOUND, lArgs);

				Exception exception = new Exception(lErrorMessage);
				throw exception;
			}

			return lTable;
		}
	}
	#endregion EventReportRequest event for XML
}


