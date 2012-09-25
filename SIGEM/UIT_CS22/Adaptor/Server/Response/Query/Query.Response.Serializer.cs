// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.Common;

namespace SIGEM.Client.Adaptor.Serializer
{
	using SIGEM.Client.Adaptor.DataFormats;

	#region Deserialize Xml <Query.Response>.
	internal static class XmlQueryResponseSerializer
	{
		#region Process -> <Query.Response>.
		public static QueryResponse Deserialize(XmlReader reader, QueryResponse queryResponse )
		{
			if (reader.IsStartElement( DTD.Response.TagQueryResponse))
			{
				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();
					#region <ERROR>
					if (reader.IsStartElement(DTD.Response.ServiceResponse.TagError))
					{
						throw XMLErrorSerializer.Deserialize(reader.ReadSubtree());
					}
					#endregion <ERROR>

					if (queryResponse == null)
					{
						queryResponse = new QueryResponse();
					}

					bool lIsHeadProcessed = false;
					Dictionary<string,DataColumn> lHeadOid = null;
					Dictionary<string, DataColumn> lHeadCol = null;
					List<string> lDisplayset = null;
					List<int> lDuplicates = null;

					do
					{
						#region <Head>.
						if (reader.IsStartElement(DTD.Response.QueryResponse.TagHead))
						{
							string lClassName = string.Empty;
							XmlHead.Deserialize(reader.ReadSubtree(), out lHeadOid, out lHeadCol, out lDisplayset, out lDuplicates, ref lClassName);
							queryResponse.ClassName = lClassName;
							lIsHeadProcessed = true;
						}
						#endregion <Head>.
						else
						{
							#region <Data>.
							if (reader.IsStartElement(DTD.Response.QueryResponse.TagData))
							{
								if (lIsHeadProcessed)
								{
									queryResponse.Data = XmlData.Deserialize(reader.ReadSubtree(), lHeadOid, lHeadCol, lDuplicates, queryResponse.ClassName);

									if(queryResponse.Data != null)
									{
										queryResponse.Data.ExtendedProperties.Add(DataTableProperties.DisplaySetNames, lDisplayset);
									}
								}
								else
								{
									throw new ArgumentOutOfRangeException("Node Data from Query.Response is processed before Head node in Xml");
								}
							}
							#endregion <Data>.
							else
							{
								#region <?>
								reader.Skip();
								if (reader.NodeType == XmlNodeType.None)
								{
									break;
								}
								else
								{
									continue;
								}
								#endregion <?>
							}
						}
					} while (reader.Read());
				}
				else
				{
					reader.Skip();
				}
			}
			else
			{
				throw new ArgumentException();
			}
			return queryResponse;
		}
		#endregion Process -> <Query.Response>.

		#region Deserialize Xml <Head>.
		public static class XmlHead
		{
		#region Process -> <Head> -> <Head.OID> and <Head.Cols>.
		public static void Deserialize(
			XmlReader reader,
			out Dictionary<string, DataColumn> headOid,
			out Dictionary<string, DataColumn> headCol,
			out List<string> displaySet,
			out List<int> duplicates,
			ref string className)
		{
			headOid = null;
			headCol = null;
			displaySet = null;
			duplicates = null;

			#region Process -> <Head>.
			if (reader.IsStartElement(DTD.Response.QueryResponse.TagHead))
			{
				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();
					do
					{
						#region Process -> <Head.OID>.
						if (reader.IsStartElement(DTD.Response.QueryResponse.Head.TagHeadOID))
						{
							headOid = XmlHeadOid.Deserialize(reader.ReadSubtree(), ref className, headOid);
						}
						#endregion Process -> <Head.OID>.
						else
						{
							#region Process -> <Head.Cols>.
							if (reader.IsStartElement(DTD.Response.QueryResponse.Head.TagHeadCols))
							{
								headCol = XmlHeadCols.Deserialize(reader.ReadSubtree(), out displaySet,  out duplicates, headCol);
							}
							#endregion Process -> <Head.Cols>.
							#region Process -> <?>.
							else
							{
								reader.Skip();
								if (reader.NodeType == XmlNodeType.None)
								{
									break;
								}
								else
								{
									continue;
								}
							}
							#endregion Process -> <?>.
						}
					} while (reader.Read());
				}
				else
				{
					reader.Skip();
				}
			}
			#endregion Process -> <Head>.
			else
			{
				throw new ArgumentException();
			}
		}
		#endregion Process -> <Head>.

		#region Deserialize Xml <Head.Cols> -> <Head.Col>.
		public static class XmlHeadCols
		{
			#region Process -> <Head.Cols> -> <Head.Col>.
			public static Dictionary<string,DataColumn> Deserialize(
			XmlReader reader,
			out List<string> displaySet,
			out List<int> duplicates,
			Dictionary<string,DataColumn> headCol
			)
			{
			displaySet = null;
			duplicates = null;

			#region Process -> <Head.Cols>.
			if (headCol == null)
			{
				 headCol = new Dictionary<string, DataColumn>();
			}

			if (reader.IsStartElement(DTD.Response.QueryResponse.Head.TagHeadCols))
			{
				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();
					displaySet = new List<string>();
					duplicates = new List<int>();
					int lIndex = 0;
					do
					{
						#region Process -> <Head.Col>.
						if (reader.IsStartElement(DTD.Response.QueryResponse.Head.TagHeadCol))
						{
							string lColumnType = reader.GetAttribute(DTD.Response.QueryResponse.Head.TagType);
							string lColumnName = reader.GetAttribute(DTD.Response.QueryResponse.Head.TagName);

							displaySet.Add(lColumnName);

							// Check for Duplicates.
							if (!headCol.ContainsKey(lColumnName))
							{
								ModelType lModelType = Convert.StringTypeToMODELType(lColumnType);
								Type ltype = Convert.MODELTypeToNetType(lModelType);
								DataColumn ldataColumn = new DataColumn(lColumnName, ltype);
								ldataColumn.AllowDBNull = true;
								// Insert the extended properties for Data Columns.
								ldataColumn.ExtendedProperties.Add(DataTableProperties.DataColumnProperties.ModelType, lModelType);
								ldataColumn.ExtendedProperties.Add(DataTableProperties.DataColumnProperties.IsDisplaySetItem, true);
								ldataColumn.ExtendedProperties.Add(DataTableProperties.DataColumnProperties.Index, lIndex);
								headCol.Add(ldataColumn.ColumnName, ldataColumn);
							}
							else
							{
								duplicates.Add(lIndex);
							}
							lIndex++;
						}
						#endregion Process -> <Head.Col>.
						#region Process -> <?>.
						else
						{
							reader.Skip();
							if (reader.NodeType == XmlNodeType.None)
							{
								break;
							}
							else
							{
								continue;
							}
						}
						#endregion Process -> <?>.
					} while (reader.Read());
					}
					else
					{
						reader.Skip();
					}
				}
				#endregion Process -> <Head.Cols>.
				else
				{
					throw new ArgumentException();
				}
				return headCol;
			}
			#endregion Process -> <Head.Cols> -> <Head.Col>.
		}
		#endregion Deserialize Xml <Head.Cols> -> <Head.Col>.

		#region Deserialize Xml <Head.OID> -> <Head.OID.Field>.
		public static class XmlHeadOid
		{
			#region Process -> <Head.OID>.
			public static Dictionary<string, DataColumn> Deserialize(
				XmlReader reader,
				ref string className,
				Dictionary<string, DataColumn> headOID)
			{
				#region Process -> <Head.OID>
				if (reader.IsStartElement(DTD.Response.QueryResponse.Head.TagHeadOID))
				{
					className = reader.GetAttribute(DTD.Response.QueryResponse.Head.TagClass);

					if (!reader.IsEmptyElement)
					{
						reader.ReadStartElement();

						if (headOID == null)
						{
							headOID = new Dictionary<string, DataColumn>();
						}

						int lIndex = 0;
						do
						{
							#region Process <Head.OID.Field>.
							if (reader.IsStartElement(DTD.Response.QueryResponse.Head.TagHeadOIDField))
							{
								ModelType lModelType = Convert.StringTypeToMODELType(reader.GetAttribute(DTD.Response.QueryResponse.Head.TagType));
								Type ltype = Convert.MODELTypeToNetType(lModelType);

								DataColumn ldataColumn = new DataColumn(reader.GetAttribute(DTD.Response.QueryResponse.Head.TagName), ltype);
								ldataColumn.AllowDBNull = false;
								ldataColumn.ExtendedProperties.Add(DataTableProperties.DataColumnProperties.ModelType, lModelType);
								ldataColumn.ExtendedProperties.Add(DataTableProperties.DataColumnProperties.IsOidItem, true);
								ldataColumn.ExtendedProperties.Add(DataTableProperties.DataColumnProperties.Index, lIndex);
								headOID.Add(ldataColumn.ColumnName, ldataColumn);
								lIndex++;
							}
							#endregion Process <Head.OID.Field>.
							#region Process <?>.
							else
							{
								reader.Skip();
								if (reader.NodeType == XmlNodeType.None)
								{
									break;
								}
								else
								{
									continue;
								}
							}
							#endregion Process <?>.

						} while (reader.Read());
					}
					else
					{
						reader.Skip();
					}
				}
				#endregion Process -> <Head.OID>
				else
				{
					throw new ArgumentException();
				}
				return headOID;
			}
			#endregion Process -> <Head.OID>.
		}
		#endregion Deserialize Xml <Head.OID> -> <Head.OID.Field>.
		}
		#endregion Deserialize Xml <Head>.

		#region Deserialize Xml <Data>.
		public static class XmlData
		{
		public const string PrefixUniqueConstraint = "PK_";

		#region Create new DataTable from Dictionary<string,DataColumn>.
		public static DataTable Create(
			string className,
			Dictionary<string,DataColumn> headOid,
			Dictionary<string,DataColumn> headCol,
			List<int> duplicates)
		{
			#region Instance a DataTable.
			DataTable lResult = null;
			if (className == null)
			{
				className = string.Empty;
			}

			if (className.Length > 0)
			{
				lResult = new DataTable(className);
			}
			else
			{
				lResult = new DataTable();
			}
			#endregion Instance a DataTable.

			DataColumnCollection lColumns = lResult.Columns;

			#region Add Oid Fields to DataSet.
			if (headOid != null)
			{
				// Save Oid Info.
				string[] lOidNames = new string[headOid.Count];
				headOid.Keys.CopyTo(lOidNames, 0);
				lResult.ExtendedProperties.Add(DataTableProperties.OidFieldNames, new List<string>(lOidNames));

				// Add Oid Columns.
				DataColumn[] lOidFields = new DataColumn[headOid.Count];
				headOid.Values.CopyTo(lOidFields, 0);
				lColumns.AddRange(lOidFields);

				#region Add DisplaySet to DataTable.
				if (headCol != null)
				{
					foreach (DataColumn i in lOidFields)
					{
						if (headCol.ContainsKey(i.ColumnName))
						{
							duplicates.Add((int)headCol[i.ColumnName].ExtendedProperties[DataTableProperties.DataColumnProperties.Index]);
							headCol.Remove(i.ColumnName);
						}
					}

					// Add DisplaySet - OID Columns.
					DataColumn[] lDisplaySet = new DataColumn[headCol.Count];
					headCol.Values.CopyTo(lDisplaySet, 0);
					lColumns.AddRange(lDisplaySet);
				}
				#endregion Add DisplaySet to DataSet.

				#region Add Oid Fields to UniqueConstraint.
				StringBuilder lUniqueConstraintName = new StringBuilder(PrefixUniqueConstraint);
				lUniqueConstraintName.Append(lResult.TableName);
				lResult.Constraints.Add(new UniqueConstraint(lUniqueConstraintName.ToString(), lOidFields, true));
				#endregion Add Oid Fields to UniqueConstraint.
			}
			#endregion Add Oid Fields to DataSet.
			else
			{
				throw new ArgumentException(); // TODO: Add rich message error.
			}
			return lResult;
		}
		#endregion Create new DataTable.

		#region Deserialize Data from Dictionary<string,DataColumn>.
		public static DataTable Deserialize(
			XmlReader reader,
			Dictionary<string,DataColumn> headOid,
			Dictionary<string,DataColumn> headCol,
			List<int> duplicates,
			string className)
		{
			#region Create DataTable.
			DataTable ldataTable = Create(className, headOid, headCol, duplicates);
			#endregion Create DataTable.

			#region Process <Data> -> Process <R>.
			if (reader.IsStartElement(DTD.Response.QueryResponse.TagData))
			{
				#region Save in DataTable <Rows>, <LastBlock> and <TotalRows>.
				ldataTable.ExtendedProperties.Add(DTD.Response.QueryResponse.Data.TagRows,
					uint.Parse(reader.GetAttribute(DTD.Response.QueryResponse.Data.TagRows)));

				ldataTable.ExtendedProperties.Add(DTD.Response.QueryResponse.Data.TagLastBlock,
					bool.Parse(reader.GetAttribute(DTD.Response.QueryResponse.Data.TagLastBlock)));

				// Get the total number of row, taking into account that can be not returned.
				string totalRows = string.Empty;
				try
				{
					totalRows = reader.GetAttribute(DTD.Response.QueryResponse.Data.TagTotalRows);
				}
				catch {}

				if ((totalRows == null) || (totalRows.Length == 0))
				{
					ldataTable.ExtendedProperties.Add(DTD.Response.QueryResponse.Data.TagTotalRows, int.Parse("-1"));
				}
				else
				{
					ldataTable.ExtendedProperties.Add(DTD.Response.QueryResponse.Data.TagTotalRows, int.Parse(totalRows));
				}
				#endregion Save in DataTable <Rows>, <LastBlock> and <TotalRows>.

				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();

					#region Convert to List, headOid and headCol.
					DataColumn[] lColumns = null;
					List<DataColumn> lHeadOid = new List<DataColumn>();
					lColumns = new DataColumn[headOid.Count];
					headOid.Values.CopyTo(lColumns, 0);
					lHeadOid.AddRange(lColumns);
					List<DataColumn> lHeadCol = new List<DataColumn>();
					lColumns = new DataColumn[headCol.Count];
					headCol.Values.CopyTo(lColumns, 0);
					lHeadCol.AddRange(lColumns);
					#endregion Convert to List, headOid and headCol.

					do
					{
						#region Process -> <R>.
						if (reader.IsStartElement(DTD.Response.QueryResponse.Data.TagR))
						{
							ldataTable.Rows.Add(XmlR.Deserialize(reader.ReadSubtree(), lHeadOid, lHeadCol,duplicates));
						}
						#endregion Process -> <R>.
						#region Process -> <?>.
						else
						{
							reader.Skip();
							if (reader.NodeType == XmlNodeType.None)
							{
								break;
							}
							else
							{
								continue;
							}
						}
						#endregion Process -> <?>.
					} while (reader.Read());
				}
				else
				{
					reader.Skip();
				}
			}
			#endregion Process <Data>.
			else
			{
				throw new ArgumentException(); // TODO: Add rich message error.
			}
			return ldataTable;
		}
		#endregion Deserialize Data Dictionary<string,DataColumn> .

		#region Deserialize Xml <R> -> (<O></O>) and (<V></V>) </R>.
		public static class XmlR
		{
			#region Deserialize <R>.
			public static object[] Deserialize(
				XmlReader reader,
				List<DataColumn> headOid,
				List<DataColumn> headCol,
				List<int> duplicates)
			{
				// Array of data.
				object[] lResult = null;

				// Array of data for Oid and displaySet.
				List<object> lDataRowOId = null;
				List<object> lDataRowCol = null;

				#region Process -> <R>.
				if (reader.IsStartElement(DTD.Response.QueryResponse.Data.TagR))
				{
					if (!reader.IsEmptyElement)
					{
						lDataRowOId = new List<object>(headOid.Count);
						lDataRowCol = new List<object>(headCol.Count);

						reader.ReadStartElement();

						int lOidRows = 0;
						int lColRows = 0;
						int lDataCol = 0;
						do
						{
							#region Process -> <O>.
							if (reader.IsStartElement(DTD.Response.QueryResponse.Data.R.TagO))
							{
								object lvalue = null;
								if (!reader.IsEmptyElement)
								{
									lvalue = Convert.XmlToType((ModelType)headOid[lOidRows].ExtendedProperties[DataTableProperties.DataColumnProperties.ModelType], reader.ReadString());
								}
								else
								{
									lvalue = Convert.XmlToType((ModelType)headOid[lOidRows].ExtendedProperties[DataTableProperties.DataColumnProperties.ModelType], null);
								}
								lDataRowCol.Add(lvalue);
								lOidRows++;
							}
							#endregion Process -> <O>.
							else
							{
								#region Process -> <V>.
								if (reader.IsStartElement(DTD.Response.QueryResponse.Data.R.TagV))
								{
									object lvalue = null;
									if (!duplicates.Contains(lColRows))
									{
										if (!reader.IsEmptyElement)
										{
											lvalue = Convert.XmlToType((ModelType)headCol[lDataCol].ExtendedProperties[DataTableProperties.DataColumnProperties.ModelType], reader.ReadString());
										}
										else
										{
											lvalue = Convert.XmlToType((ModelType)headCol[lDataCol].ExtendedProperties[DataTableProperties.DataColumnProperties.ModelType], null);
										}
										lDataRowCol.Add(lvalue);
										lDataCol++;
									}
									else
									{
										if (!reader.IsEmptyElement)
										{
											reader.ReadString();
										}
									}
								lColRows++;
								}
								#endregion Process -> <V>.
							#region Process <?>.
							else
							{
								reader.Skip();
								if (reader.NodeType == XmlNodeType.None)
								{
									break;
								}
								else
								{
									continue;
								}
							}
							#endregion Process <?>.
							}
						} while (reader.Read());
					}
					#endregion Process -> <R>.
					else
					{
						reader.Skip();
					}
				}
				#region return Result in Array of objects.
				if (lDataRowOId != null)
				{
					if (lDataRowCol != null)
					{
						lDataRowOId.AddRange(lDataRowCol);
					}
					lResult = lDataRowOId.ToArray();
				}
				#endregion return Result in Array of objects.
				return lResult;
			}
			#endregion Deserialize R
		}
		#endregion Deserialize Xml <R> -> (<O></O>) and (<V></V>) </R>.
		}
		#endregion Deserialize Xml <Data>..
	}
	#endregion Deserialize Xml <Query.Response>.
}

