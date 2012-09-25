// 3.4.4.5
using System;
using System.Data;
using System.Collections;
using ONSQLDataReader = System.Data.SqlClient.SqlDataReader;
using ONSQLConnection = System.Data.SqlClient.SqlConnection;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Instance;
using SIGEM.Business.Collection;
using SIGEM.Business.Query;
using SIGEM.Business.Attributes;
using SIGEM.Business.SQL;
using SIGEM.Business.Exceptions;
using SIGEM.Business.Action;
using SIGEM.Business.Others.Enumerations;

namespace SIGEM.Business.Data
{
	[ONDataClass("RevisionPasajero")]
	internal class RevisionPasajeroData : ONDBData
	{
		#region Constants

		#endregion Constants
	
		#region Properties
		/// <summary>
		/// This member returns the name of the DB table
		/// </summary>
		public override string TableName
		{
			get
			{
				return CtesBD.TBL_REVISIONPASAJERO;
			}
		}
		#endregion Properties
		
		#region Constructors
		/// <summary>
		/// Constructor of the specific Data class
		/// </summary>
		/// <param name="onContext">Current context</param>
		public RevisionPasajeroData(ONContext onContext)
			: base(onContext, "RevisionPasajero")
		{
		}
		#endregion Constructors

		#region Queries
		#region LoadFacet
		/// <summary>
		/// Load the data retrieved from the Data Base to components of the application
		/// </summary>
		/// <param name="onContext">This parameter has the current context</param>
		/// <param name="columns">This parameter has the data collected from the database</param>
		/// <param name="index">This parameter has the position of the first data to fix in the application</param>
		public static RevisionPasajeroInstance LoadFacet(ONContext onContext, ONDisplaySet displaySet, object[] columns, ref int index)
		{
			RevisionPasajeroInstance lInstance = new RevisionPasajeroInstance(onContext);
			lInstance.Oid = new RevisionPasajeroOid();

			lInstance.RevisionRoleOidTemp = new RevisionOid();
			lInstance.PasajeroAeronaveRoleOidTemp = new PasajeroAeronaveOid();
			// Field 'id_RevisionPasajero'
			lInstance.Oid.Id_RevisionPasajeroAttr = new ONInt(Convert.ToInt32(columns[index++]));
			// Field 'fk_Revision_1'
			lInstance.RevisionRoleOidTemp.Id_RevisarAeronaveAttr = new ONInt(Convert.ToInt32(columns[index++]));
			// Field 'fk_PasajeroAero_1'
			lInstance.PasajeroAeronaveRoleOidTemp.Id_PasajeroAeronaveAttr = new ONInt(Convert.ToInt32(columns[index++]));
			lInstance.StateObj = new ONString(((string) columns[index++]).TrimEnd());
			lInstance.Lmd = new ONDateTime((DateTime) columns[index++]);

			lInstance.Modified = false;
			return lInstance;
		}
		#endregion LoadFacet

		#region ExecuteSql
		/// <summary>
		/// Executes the SQL statment over the Data Base connected
		/// </summary>
		/// <param name="onSql">This parameter has the current SQL statment</param>
		/// <param name="onFilterList">List of filters to check</param>
		/// <param name="comparer">This parameter has all the information refering to the order criteria to add to SQL statment</param>
		/// <param name="startRowOid">This parameter has the OID necessary to start the search</param>
		/// <param name="blockSize">This parameter represents the number of instances to be returned</param>
		public override ONCollection ExecuteSql(ONSql onSql, ONFilterList onFilterList, ONDisplaySet displaySet, ONOrderCriteria orderCriteria, ONOid startRowOid, int blockSize)
		{
			RevisionPasajeroCollection lQuery = null;
			bool lWithStartRow = (startRowOid != null).TypedValue;
			long lCount = -1;
			if (!lWithStartRow)
				lCount = 0;

			IDataReader lDataReader = null;
			ONSQLConnection lOnSQLConnection = null;

			try
			{
				lDataReader = Execute(onSql) as IDataReader;

				RevisionPasajeroInstance lInstance = null;
				RevisionPasajeroInstance lAntInstance = null;
				if (lDataReader != null)
				{
					object[] lColumns;
					if(displaySet == null)
						lColumns = new object[5];
					else
						lColumns = new object[displaySet.ElementsInData];

					lQuery = new RevisionPasajeroCollection(OnContext);
					bool lFoundStartRow = false;
					while (lDataReader.Read())
					{
						lAntInstance = lInstance;

						// Read Columns
						lDataReader.GetValues(lColumns);

						// Read Instance
						int lIndex = 0;
						lInstance = LoadFacet(OnContext, displaySet, lColumns, ref lIndex);

						// Read related attributes
						if (displaySet != null)
							LoadRelated(OnContext, displaySet, lColumns, lIndex, lInstance);

						if (lCount >= 0) // Add the load instance
						{
							if ((onFilterList == null) || (!onFilterList.InMemory))
							{
								// Add to the Instance list
								lQuery.Add(lInstance);
								lCount++;
							}
							else
							{
								ONSQLConnection lSQLConnectionOld = (ONSQLConnection) lInstance.OnContext.SqlConnection;

								// Set another connection because it is imposible to use 
								// the same connection that is used in the DataReader
								if (lOnSQLConnection == null)
									lOnSQLConnection = GetConnection();
								lInstance.OnContext.SqlConnection = lOnSQLConnection;

								if (onFilterList.FilterInMemory(lInstance))
								{
									// Add to the Instance list
									lQuery.Add(lInstance);
									lCount++;
								}

								lInstance.OnContext.SqlConnection = lSQLConnectionOld;
							}
						}
						else
						{
							if ((orderCriteria != null) && (orderCriteria.InMemory)) // Need to load for ordering in memory after loading
							{
								if (lAntInstance != null) 
								{
									// Set another connection because it is imposible to use 
									// the same connection that is used in the DataReader
									ONSQLConnection lOnSQLConnectionOld = lInstance.OnContext.SqlConnection as ONSQLConnection;
									if (lOnSQLConnection == null)
										lOnSQLConnection = GetConnection();
									lInstance.OnContext.SqlConnection = lOnSQLConnection;

									int lCompare = orderCriteria.CompareSql(lInstance, lAntInstance);
									if (lCompare != 0)
									{
										if (lFoundStartRow)
											lCount = 1;
										else
											lQuery.Clear();
									}

									// Restores the old connection
									lInstance.OnContext.SqlConnection = lOnSQLConnectionOld;
								}

								if ((onFilterList == null) || (!onFilterList.InMemory))
								{
									// Add to the Instance list
									lQuery.Add(lInstance);
								}
								else
								{
									ONSQLConnection lSQLConnectionOld = (ONSQLConnection)lInstance.OnContext.SqlConnection;

									// Set another connection because it is imposible to use 
									// the same connection that is used in the DataReader
									if (lOnSQLConnection == null)
										lOnSQLConnection = GetConnection();
									lInstance.OnContext.SqlConnection = lOnSQLConnection;

									if (onFilterList.FilterInMemory(lInstance))
									{
										// Add to the Instance list
										lQuery.Add(lInstance);
									}
									else
										lCount--;

									lInstance.OnContext.SqlConnection = lSQLConnectionOld;
								}

								if (lInstance.Oid.Equals(startRowOid))
									lFoundStartRow = true;
							}

							else if (lInstance.Oid.Equals(startRowOid)) // Search the start row
								lCount = 0;
						}

						// Stop loading
						if ((blockSize != 0) && (lCount > blockSize))
						{
							if (orderCriteria == null)
								break;
							else
							{
								// Set another connection because it is imposible to use 
								// the same connection that is used in the DataReader
								ONSQLConnection lOnSQLConnectionOld = lInstance.OnContext.SqlConnection as ONSQLConnection;
								if (lOnSQLConnection == null)
									lOnSQLConnection = GetConnection();
								lInstance.OnContext.SqlConnection = lOnSQLConnection;

								int lCompare = orderCriteria.CompareSql(lInstance, lAntInstance);

								// Restores the old connection
								lInstance.OnContext.SqlConnection = lOnSQLConnectionOld;

								if (lCompare > 0)
									break;
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				string ltraceItem = "Method: ExecuteSql, Component: RevisionPasajeroData";
      			if (e is ONSystemException)
      			{
      				ONSystemException lException = e as ONSystemException;
      				lException.addTraceInformation(ltraceItem);
      				throw lException;
				}
    	  		throw new ONSystemException(e, ltraceItem);
      		}
			finally
			{
				if (lOnSQLConnection != null)
					ONDBData.CloseConnection(lOnSQLConnection);
				if (lDataReader != null)
				{
					if (mSqlCommand != null)
						mSqlCommand.Cancel();
					lDataReader.Close();
				}
				Close();
				if ((onFilterList != null) && (onFilterList.InMemory) && !lWithStartRow && (lCount <= blockSize))
					lQuery.totalNumInstances = lQuery.Count;				
			}

			return lQuery;
		}
		#endregion ExecuteSql

		#region AddOrderCriteria
		///<summary> This method adds the order criteria to the SQL statement </summary>
		///<param name = "onSql"> This parameter represents the SQL component </param>
		///<param name = "comparer"> This parameter has all the information refering to the order criteria to add to SQL statement</param>
		/// <param name="startRowOid">This parameter has the OID necessary to start the search</param>
		/// <param name="blockSize">This parameter represents the number of instances to be returned</param>
		protected override void AddOrderCriteria(ONSqlSelect onSql, ONOrderCriteria comparer, ONOid startRowOid, int blockSize, ONPath initialPath)
		{
			//Initilizate StartRow
			RevisionPasajeroInstance lInstance = null;
			if (startRowOid != null)
			{
				lInstance = new RevisionPasajeroInstance(OnContext);
				lInstance.Oid = startRowOid as RevisionPasajeroOid;	
			}
			
			//Default OrderCriteria
			if (comparer == null)
			{
				string lAlias = onSql.GetAlias("RevisionPasajero", initialPath);
				if (lInstance != null)
				{
					onSql.AddOrderBy(lAlias, CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO, OrderByTypeEnumerator.Asc, lInstance.Oid.Id_RevisionPasajeroAttr);
				}
				else
				{
					onSql.AddOrderBy(lAlias, CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO, OrderByTypeEnumerator.Asc, null);
				}
				return;
			}
			
			//Add OrderCriteria
			bool lUseStartRow = (!comparer.InMemory);
			foreach (ONOrderCriteriaItem lOrderCriteriaItem in comparer.OrderCriteriaSqlItem)
			{
				ONPath lPath = new ONPath(lOrderCriteriaItem.OnPath);
				if((lInstance != null) && (lUseStartRow))
				{
					ONSimpleType lAttrStartRow = null;

					if (lPath.Path == "")
						lAttrStartRow = lInstance[lOrderCriteriaItem.Attribute] as ONSimpleType;
					else
					{
						ONCollection lCollection = (lInstance[lPath.Path] as ONCollection);
						if((lCollection != null) && (lCollection.Count > 0))
							lAttrStartRow = lCollection[0][lOrderCriteriaItem.Attribute] as ONSimpleType;
					}
					onSql.AddOrderBy(RevisionPasajeroData.AddPath(onSql, JoinType.LeftJoin, lOrderCriteriaItem.Facet, lPath, null, lOrderCriteriaItem.DomainArgument, false), lOrderCriteriaItem.Attribute, lOrderCriteriaItem.Type, lAttrStartRow);
					lUseStartRow = (lAttrStartRow != null);
				}
				else
					onSql.AddOrderBy(RevisionPasajeroData.AddPath(onSql, JoinType.LeftJoin, lOrderCriteriaItem.Facet, lPath, null, lOrderCriteriaItem.DomainArgument, false), lOrderCriteriaItem.Attribute, lOrderCriteriaItem.Type, null);
			}
			return;
		}
		#endregion AddOrderCriteria
		#endregion Queries

		#region Services
		#region Update Added
		/// <summary>This method builds the SQL statement to insert the instance in database</summary>
		/// <param name="instance">This parameter represents the instance to be inserted in the database</param>
		public override void UpdateAdded(ONInstance instance)
		{
			RevisionPasajeroInstance lInstance = instance as RevisionPasajeroInstance;

			ONSqlInsert lOnSql = new ONSqlInsert();
			lOnSql.AddInto(CtesBD.TBL_REVISIONPASAJERO);
			// Field 'id_RevisionPasajero'
			lOnSql.AddValue(CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO, lInstance.Oid.Id_RevisionPasajeroAttr);
			// Field 'fk_Revision_1'
			if (lInstance.RevisionRoleOidTemp != null)
				lOnSql.AddValue(CtesBD.FLD_REVISIONPASAJERO_FK_REVISION_1, lInstance.RevisionRoleOidTemp.Id_RevisarAeronaveAttr);
			else
				lOnSql.AddValue(CtesBD.FLD_REVISIONPASAJERO_FK_REVISION_1, ONInt.Null);
			// Field 'fk_PasajeroAero_1'
			if (lInstance.PasajeroAeronaveRoleOidTemp != null)
				lOnSql.AddValue(CtesBD.FLD_REVISIONPASAJERO_FK_PASAJEROAERO_1, lInstance.PasajeroAeronaveRoleOidTemp.Id_PasajeroAeronaveAttr);
			else
				lOnSql.AddValue(CtesBD.FLD_REVISIONPASAJERO_FK_PASAJEROAERO_1, ONInt.Null);
			lOnSql.AddValue(CtesBD.FLD_REVISIONPASAJERO_ESTADOOBJ, lInstance.StateObj);
			lOnSql.AddValue(CtesBD.FLD_REVISIONPASAJERO_FUM, new ONDateTime(DateTime.Now));
			Execute(lOnSql);
		}
		#endregion Update Added

		#region Update Deleted
		///<summary> 
		/// This method builds the SQL statment to delete the instance in database 
		///</summary>
		///<param name = instance>
		///This parameter represents the instance to be deleted in the database
		///</param>
		public override void UpdateDeleted (ONInstance instance)
		{
			RevisionPasajeroInstance lInstance = instance as RevisionPasajeroInstance;
			
			ONSqlDelete lOnSql = new ONSqlDelete();
			lOnSql.AddFrom(CtesBD.TBL_REVISIONPASAJERO);
			lOnSql.AddWhere(CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO, lInstance.Oid.Id_RevisionPasajeroAttr);
			Execute(lOnSql);
		}
		#endregion

		#region Update Edited
		/// <summary>This method builds the SQL statement to edit the instance in database</summary>
		/// <param name="instance">This parameter represents the instance to be modified in the database</param>
		public override void UpdateEdited(ONInstance instance)
		{
			RevisionPasajeroInstance lInstance = instance as RevisionPasajeroInstance;

			ONSqlUpdate lOnSql = new ONSqlUpdate();
			lOnSql.AddUpdate(CtesBD.TBL_REVISIONPASAJERO);
			// Field 'id_RevisionPasajero'
			// Update not needed (OID)
			// Field 'fk_Revision_1'
			if (lInstance.RevisionRoleAttrModified)
			{
				if (lInstance.RevisionRoleOidTemp != null)
					lOnSql.AddSet(CtesBD.FLD_REVISIONPASAJERO_FK_REVISION_1, lInstance.RevisionRoleOidTemp.Id_RevisarAeronaveAttr);
				else
					lOnSql.AddSet(CtesBD.FLD_REVISIONPASAJERO_FK_REVISION_1, ONInt.Null);
			}	
			// Field 'fk_PasajeroAero_1'
			if (lInstance.PasajeroAeronaveRoleAttrModified)
			{
				if (lInstance.PasajeroAeronaveRoleOidTemp != null)
					lOnSql.AddSet(CtesBD.FLD_REVISIONPASAJERO_FK_PASAJEROAERO_1, lInstance.PasajeroAeronaveRoleOidTemp.Id_PasajeroAeronaveAttr);
				else
					lOnSql.AddSet(CtesBD.FLD_REVISIONPASAJERO_FK_PASAJEROAERO_1, ONInt.Null);
			}	
			lOnSql.AddSet(CtesBD.FLD_REVISIONPASAJERO_ESTADOOBJ, lInstance.StateObj);
			lOnSql.AddSet(CtesBD.FLD_REVISIONPASAJERO_FUM, new ONDateTime(DateTime.Now));

			lOnSql.AddWhere(CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO, lInstance.Oid.Id_RevisionPasajeroAttr);
			Execute(lOnSql);
		}
		#endregion

		#region Autonumeric
		/// <summary>Obtains the last autonumeric that has the attribute id_RevisionPasajero</summary>
		public ONInt GetAutonumericid_RevisionPasajero()
		{
			ONSqlMax lOnSql = new ONSqlMax();
			string lAlias = lOnSql.CreateAlias(CtesBD.TBL_REVISIONPASAJERO, null, "RevisionPasajero");
			lOnSql.AddSelect(lAlias, CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO);
		
			object lRet = Execute(lOnSql);
			if (lRet is DBNull)
				return new ONInt(1);
			else
				return new ONInt(Convert.ToInt32(lRet) + 1);
		}
 		#endregion Autonumeric
		#endregion Services

		#region Relationship / Inheritance

		#region Role 'Revision'
		public ONCollection RevisionRole(RevisionOid oid)
		{
			ONSqlSelect lOnSql = new ONSqlSelect();
			
			//Create select
			RevisionData.AddPath(lOnSql, "RevisionPasajero", new ONPath("RevisionPasajero"), null, "");
			RetrieveInstances(lOnSql, null, new ONPath("RevisionPasajero"), OnContext);
			
			//Fix related instance
			RevisionData.FixInstance(lOnSql, null, null, oid);
			
			//Execute
			return ExecuteQuery(lOnSql);
		}


		public void RevisionRoleInsert(RevisionPasajeroOid localOid, RevisionOid relatedOid)
		{
			ONSqlUpdate lOnSql = new ONSqlUpdate();
			lOnSql.AddUpdate(CtesBD.TBL_REVISIONPASAJERO);
			lOnSql.AddSet(CtesBD.FLD_REVISIONPASAJERO_FK_REVISION_1, relatedOid.Id_RevisarAeronaveAttr);
			lOnSql.AddWhere(CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO, localOid.Id_RevisionPasajeroAttr);
			Execute(lOnSql);
		}

		public void RevisionRoleDelete(RevisionPasajeroOid localOid, RevisionOid relatedOid)
		{
			ONSqlUpdate lOnSql = new ONSqlUpdate();
			lOnSql.AddUpdate(CtesBD.TBL_REVISIONPASAJERO);
			lOnSql.AddSet(CtesBD.FLD_REVISIONPASAJERO_FK_REVISION_1, ONInt.Null);
			lOnSql.AddWhere(CtesBD.FLD_REVISIONPASAJERO_FK_REVISION_1, relatedOid.Id_RevisarAeronaveAttr);
			lOnSql.AddWhere(CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO, localOid.Id_RevisionPasajeroAttr);
			Execute(lOnSql);
		}

		public void RevisionRoleDelete(RevisionPasajeroOid oid)
		{
			ONSqlUpdate lOnSql = new ONSqlUpdate();
			lOnSql.AddUpdate(CtesBD.TBL_REVISIONPASAJERO);
			lOnSql.AddSet(CtesBD.FLD_REVISIONPASAJERO_FK_REVISION_1, ONInt.Null);
			lOnSql.AddWhere(CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO, oid.Id_RevisionPasajeroAttr);
			
			Execute(lOnSql);
		}
		
		private static string RevisionRoleAddSql( ONSqlSelect onSql, string facet, ONPath onPath, ONPath processedPath, string role, bool force)
		{
			return RevisionRoleAddSql(onSql, JoinType.InnerJoin, facet, onPath, processedPath, role, force, false);
		}

		private static string RevisionRoleAddSql(ONSqlSelect onSql, JoinType joinType, string facet, ONPath onPath, ONPath processedPath, string role, bool force)
		{
			return RevisionRoleAddSql(onSql, joinType, facet, onPath, processedPath, role, force, false);
		}
		private static string RevisionRoleAddSql(ONSqlSelect onSql, JoinType joinType, string facet, ONPath onPath, ONPath processedPath, string role, bool force, bool isLinkedTo)
		{
			ONPath lOnPath = new ONPath(processedPath);
			lOnPath += role;
			
			//Source table
			string lAliasProcessed = onSql.GetAlias("RevisionPasajero", processedPath, isLinkedTo);
			if (lAliasProcessed == "")
			{
				force = false;
				lAliasProcessed = onSql.CreateAlias(joinType, lAliasProcessed, CtesBD.TBL_REVISIONPASAJERO, processedPath, "RevisionPasajero", force, isLinkedTo);
			}	
			
			//Target table
			string lAlias = onSql.GetAlias("Revision", lOnPath, isLinkedTo);
			if (lAlias == "")
			{
				force = false;
				lAlias = onSql.CreateAlias(joinType, lAliasProcessed, CtesBD.TBL_REVISION, lOnPath, "Revision", force, isLinkedTo);
				onSql.AddAliasWhere(lAlias, lAliasProcessed + "." + CtesBD.FLD_REVISIONPASAJERO_FK_REVISION_1 + "=" + lAlias + "." + CtesBD.FLD_REVISION_ID_REVISARAERONAVE);
			}
			//Target path
			if ((((object) onPath == null) || (onPath.Count == 0)) && (string.Compare("Revision", facet, true) == 0) && (!force))
				return lAlias;
					
			return RevisionData.AddPath(onSql, joinType, facet, onPath, lOnPath, "", force, isLinkedTo);
		}		
		#endregion Role 'Revision'

		#region Role 'PasajeroAeronave'
		public ONCollection PasajeroAeronaveRole(PasajeroAeronaveOid oid)
		{
			ONSqlSelect lOnSql = new ONSqlSelect();
			
			//Create select
			PasajeroAeronaveData.AddPath(lOnSql, "RevisionPasajero", new ONPath("RevisionPasajero"), null, "");
			RetrieveInstances(lOnSql, null, new ONPath("RevisionPasajero"), OnContext);
			
			//Fix related instance
			PasajeroAeronaveData.FixInstance(lOnSql, null, null, oid);
			
			//Execute
			return ExecuteQuery(lOnSql);
		}


		public void PasajeroAeronaveRoleInsert(RevisionPasajeroOid localOid, PasajeroAeronaveOid relatedOid)
		{
			ONSqlUpdate lOnSql = new ONSqlUpdate();
			lOnSql.AddUpdate(CtesBD.TBL_REVISIONPASAJERO);
			lOnSql.AddSet(CtesBD.FLD_REVISIONPASAJERO_FK_PASAJEROAERO_1, relatedOid.Id_PasajeroAeronaveAttr);
			lOnSql.AddWhere(CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO, localOid.Id_RevisionPasajeroAttr);
			Execute(lOnSql);
		}

		public void PasajeroAeronaveRoleDelete(RevisionPasajeroOid localOid, PasajeroAeronaveOid relatedOid)
		{
			ONSqlUpdate lOnSql = new ONSqlUpdate();
			lOnSql.AddUpdate(CtesBD.TBL_REVISIONPASAJERO);
			lOnSql.AddSet(CtesBD.FLD_REVISIONPASAJERO_FK_PASAJEROAERO_1, ONInt.Null);
			lOnSql.AddWhere(CtesBD.FLD_REVISIONPASAJERO_FK_PASAJEROAERO_1, relatedOid.Id_PasajeroAeronaveAttr);
			lOnSql.AddWhere(CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO, localOid.Id_RevisionPasajeroAttr);
			Execute(lOnSql);
		}

		public void PasajeroAeronaveRoleDelete(RevisionPasajeroOid oid)
		{
			ONSqlUpdate lOnSql = new ONSqlUpdate();
			lOnSql.AddUpdate(CtesBD.TBL_REVISIONPASAJERO);
			lOnSql.AddSet(CtesBD.FLD_REVISIONPASAJERO_FK_PASAJEROAERO_1, ONInt.Null);
			lOnSql.AddWhere(CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO, oid.Id_RevisionPasajeroAttr);
			
			Execute(lOnSql);
		}
		
		private static string PasajeroAeronaveRoleAddSql( ONSqlSelect onSql, string facet, ONPath onPath, ONPath processedPath, string role, bool force)
		{
			return PasajeroAeronaveRoleAddSql(onSql, JoinType.InnerJoin, facet, onPath, processedPath, role, force, false);
		}

		private static string PasajeroAeronaveRoleAddSql(ONSqlSelect onSql, JoinType joinType, string facet, ONPath onPath, ONPath processedPath, string role, bool force)
		{
			return PasajeroAeronaveRoleAddSql(onSql, joinType, facet, onPath, processedPath, role, force, false);
		}
		private static string PasajeroAeronaveRoleAddSql(ONSqlSelect onSql, JoinType joinType, string facet, ONPath onPath, ONPath processedPath, string role, bool force, bool isLinkedTo)
		{
			ONPath lOnPath = new ONPath(processedPath);
			lOnPath += role;
			
			//Source table
			string lAliasProcessed = onSql.GetAlias("RevisionPasajero", processedPath, isLinkedTo);
			if (lAliasProcessed == "")
			{
				force = false;
				lAliasProcessed = onSql.CreateAlias(joinType, lAliasProcessed, CtesBD.TBL_REVISIONPASAJERO, processedPath, "RevisionPasajero", force, isLinkedTo);
			}	
			
			//Target table
			string lAlias = onSql.GetAlias("PasajeroAeronave", lOnPath, isLinkedTo);
			if (lAlias == "")
			{
				force = false;
				lAlias = onSql.CreateAlias(joinType, lAliasProcessed, CtesBD.TBL_PASAJEROAERONAVE, lOnPath, "PasajeroAeronave", force, isLinkedTo);
				onSql.AddAliasWhere(lAlias, lAliasProcessed + "." + CtesBD.FLD_REVISIONPASAJERO_FK_PASAJEROAERO_1 + "=" + lAlias + "." + CtesBD.FLD_PASAJEROAERONAVE_ID_PASAJEROAERONAVE);
			}
			//Target path
			if ((((object) onPath == null) || (onPath.Count == 0)) && (string.Compare("PasajeroAeronave", facet, true) == 0) && (!force))
				return lAlias;
					
			return PasajeroAeronaveData.AddPath(onSql, joinType, facet, onPath, lOnPath, "", force, isLinkedTo);
		}		
		#endregion Role 'PasajeroAeronave'

		#region Facet 'RevisionPasajero'
		private static string RevisionPasajeroFacetAddSql(ONSqlSelect onSql, ONPath onPath)
		{
			return RevisionPasajeroFacetAddSql(JoinType.InnerJoin, onSql, onPath, false, false);
		}
		private static string RevisionPasajeroFacetAddSql(ONSqlSelect onSql, ONPath onPath, bool force)
		{
			return RevisionPasajeroFacetAddSql(JoinType.InnerJoin, onSql, onPath, force, false);
		}
		private static string RevisionPasajeroFacetAddSql(JoinType joinType, ONSqlSelect onSql, ONPath onPath, bool force)
		{
			return RevisionPasajeroFacetAddSql(joinType, onSql, onPath, force, false);
		}
		private static string RevisionPasajeroFacetAddSql(JoinType joinType, ONSqlSelect onSql, ONPath onPath, bool force, bool isLinkedTo)
		{
			//Target table
			string lAliasFacet = "";
			string lAlias = onSql.CreateAlias(joinType, "", CtesBD.TBL_REVISIONPASAJERO, onPath, "RevisionPasajero", false, isLinkedTo);

			// Load facet from 'RevisionPasajero' to 'RevisionPasajero'
			lAliasFacet = onSql.GetAlias("RevisionPasajero", onPath, isLinkedTo);
			if ((lAliasFacet == "") || force)
			{
				if (force)
					lAliasFacet = onSql.CreateAlias(joinType, "", CtesBD.TBL_REVISIONPASAJERO, onPath, "RevisionPasajero", force, isLinkedTo);
				else
					lAliasFacet = onSql.CreateAlias(joinType, lAlias, CtesBD.TBL_REVISIONPASAJERO, onPath, "RevisionPasajero", force, isLinkedTo);
				onSql.AddAliasWhere(lAliasFacet, lAlias + "." + CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO + " = " + lAliasFacet + "." + CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO);
			}
			return lAliasFacet;
		}
		#endregion
		#endregion Relationship / Inheritance

		#region Sql Management
		#region AddPath
		/// <summary>This method adds to the SQL statement any path that appears in a formula</summary>
		/// <param name="onSql">This parameter has the current SQL statement</param>
		/// <param name="facet">First class, the beginning of the path</param>
		/// <param name="onPath">Path to add to SQL statement</param>
		/// <param name="processedOnPath">Path pocessed until the call of this method</param>
		/// <param name="initialClass">Domain of the object valued argument, object valued filter variables or AGENT when it should be necessary</param>
		public static string AddPath(ONSqlSelect onSql, string facet, ONPath onPath, ONPath processedOnPath, string initialClass)
		{
			return AddPath(onSql, JoinType.InnerJoin, facet, onPath, processedOnPath, initialClass, false, false);
		}
		/// <summary>This method adds to the SQL statement any path that appears in a formula</summary>
		/// <param name="onSql">This parameter has the current SQL statement</param>
		/// <param name="facet">First class, the beginning of the path</param>
		/// <param name="onPath">Path to add to SQL statement</param>
		/// <param name="processedOnPath">Path pocessed until the call of this method</param>
		/// <param name="initialClass">Domain of the object valued argument, object valued filter variables or AGENT when it should be necessary</param>
		/// <param name="forceLastAlias">Create almost the last alias in the sql</param>
		public static string AddPath(ONSqlSelect onSql, string facet, ONPath onPath, ONPath processedOnPath, string initialClass, bool forceLastAlias)
		{
			return AddPath(onSql, JoinType.InnerJoin, facet, onPath, processedOnPath, initialClass, forceLastAlias, false);
		}
		/// <summary>This method adds to the SQL statement any path that appears in a formula</summary>
		/// <param name="onSql">This parameter has the current SQL statement</param>
		/// <param name="joinType">This parameter has the type of join</param>
		/// <param name="facet">First class, the beginning of the path</param>
		/// <param name="onPath">Path to add to SQL statement</param>
		/// <param name="processedOnPath">Path pocessed until the call of this method</param>
		/// <param name="initialClass">Domain of the object valued argument, object valued filter variables or AGENT when it should be necessary</param>
		/// <param name="forceLastAlias">Create almost the last alias in the sql</param>
		public static string AddPath(ONSqlSelect onSql, JoinType joinType, string facet, ONPath onPath, ONPath processedOnPath, string initialClass, bool forceLastAlias)
		{
			return AddPath(onSql, joinType, facet, onPath, processedOnPath, initialClass, forceLastAlias, false);
		}
		/// <summary>This method adds to the SQL statement any path that appears in a formula</summary>
		/// <param name="onSql">This parameter has the current SQL statement</param>
		/// <param name="joinType">This parameter has the type of join</param>
		/// <param name="facet">First class, the beginning of the path</param>
		/// <param name="onPath">Path to add to SQL statement</param>
		/// <param name="processedOnPath">Path pocessed until the call of this method</param>
		/// <param name="initialClass">Domain of the object valued argument, object valued filter variables or AGENT when it should be necessary</param>
		/// <param name="forceLastAlias">Create almost the last alias in the sql</param>
		/// <param name="isLinkedTo">The alias belongs to a role in a linked To element</param>
		public static string AddPath(ONSqlSelect onSql, JoinType joinType, string facet, ONPath onPath, ONPath processedOnPath, string initialClass, bool forceLastAlias, bool isLinkedTo)
		{
			// initialClass is used for Object-valued arguments, object-valued filter variables, agent instance, ...
			ONPath lProcessedOnPath = new ONPath(processedOnPath);
			ONPath lOnPath = new ONPath(onPath);
			bool lOnPathExist = true;
			object[] lParameters = new object[8];

			if (initialClass != "")
			{
				string lRol = lOnPath.RemoveHead();
				lProcessedOnPath += lRol;
				// Solve path with initialPath
				lParameters[0] = onSql;
				lParameters[1] = joinType;
				lParameters[2] = facet;
				lParameters[3] = lOnPath;
				lParameters[4] = lProcessedOnPath;
				lParameters[5] = "";
				lParameters[6] = forceLastAlias;
				lParameters[7] = isLinkedTo;

				return ONContext.InvoqueMethod(ONContext.GetType_Data(initialClass), "AddPath", lParameters) as string;

			}

			// Search max solved path
			ONPath lMaxSolvedPath = new ONPath(onPath);
			string lMaxSolvedPathDomain = facet;
			while ((lMaxSolvedPath.Count > 0) && (onSql.GetAlias(lMaxSolvedPathDomain, lProcessedOnPath + lMaxSolvedPath, isLinkedTo) == ""))
			{
				lMaxSolvedPath.RemoveTail();
				lMaxSolvedPathDomain = GetTargetClassName(lMaxSolvedPath);
			}
			if (lMaxSolvedPath.Count > 0)
			{
				lProcessedOnPath += lMaxSolvedPath;
				for (int i = 0; i < lMaxSolvedPath.Count; i++)
					lOnPath.RemoveHead();

				lParameters[0] = onSql;
				lParameters[1] = joinType;
				lParameters[2] = facet;
				lParameters[3] = lOnPath;
				lParameters[4] = lProcessedOnPath;
				lParameters[5] = "";
				lParameters[6] = forceLastAlias;
				lParameters[7] = isLinkedTo;

				return ONContext.InvoqueMethod(ONContext.GetType_Data(lMaxSolvedPathDomain), "AddPath", lParameters) as string;
			}

			// Create inheritance path
			if ((onPath == null) || (onPath.Count == 0))
			{
				if (forceLastAlias)
					return RevisionPasajeroFacetAddSql(joinType, onSql, processedOnPath, forceLastAlias, isLinkedTo);

				if ((processedOnPath == null) || (processedOnPath.Count == 0))
					return (onSql.CreateAlias(joinType, "", CtesBD.TBL_REVISIONPASAJERO, null, "RevisionPasajero", false, isLinkedTo));
				else
					return (onSql.CreateAlias(joinType, "", CtesBD.TBL_REVISIONPASAJERO, processedOnPath, "RevisionPasajero", false, isLinkedTo));
			}

			// Calculate processed path
			string lRole = lOnPath.RemoveHead() as string;
			lProcessedOnPath += lRole;
			
			// Search Path
			if (lOnPath.Count == 0)
			{
				string lAlias = onSql.GetAlias(facet, lProcessedOnPath, isLinkedTo);
				if ((lAlias != "") && (!forceLastAlias))
					return (lAlias);
				else
					lOnPathExist = false;
			}
			else
			{
				string lTargetClass = GetTargetClassName(new ONPath(lRole));
			
				// Agent & OV Argument Control
				if ((lTargetClass == "") && (initialClass != ""))
					lTargetClass = initialClass;
			
				string lAlias = onSql.GetAlias(lTargetClass, lProcessedOnPath, isLinkedTo);
				if (lAlias == "")
					lOnPathExist = false;
			}
			
			// Create path
			if (string.Compare(lRole, "Revision", true) == 0)
			{
				if (lOnPathExist)
					return RevisionData.AddPath(onSql, joinType, facet, lOnPath, lProcessedOnPath, "", forceLastAlias, isLinkedTo);
				else
					return RevisionRoleAddSql(onSql, joinType, facet, lOnPath, processedOnPath, lRole, forceLastAlias, isLinkedTo);
			}

			if (string.Compare(lRole, "PasajeroAeronave", true) == 0)
			{
				if (lOnPathExist)
					return PasajeroAeronaveData.AddPath(onSql, joinType, facet, lOnPath, lProcessedOnPath, "", forceLastAlias, isLinkedTo);
				else
					return PasajeroAeronaveRoleAddSql(onSql, joinType, facet, lOnPath, processedOnPath, lRole, forceLastAlias, isLinkedTo);
			}

			initialClass = "RevisionPasajero";

			// Solve path with initialPath
			lParameters[0] = onSql;
			lParameters[1] = joinType;
			lParameters[2] = facet;
			lParameters[3] = lOnPath;
			lParameters[4] = lProcessedOnPath;
			lParameters[5] = "";
			lParameters[6] = forceLastAlias;
			lParameters[7] = isLinkedTo;

			return ONContext.InvoqueMethod(ONContext.GetType_Data(initialClass), "AddPath", lParameters) as string;
		}
		public override string InhAddPath( ONSqlSelect onSql, string facet, ONPath onPath, string initialClass)
		{
			// initialClass is used for Object-valued arguments, object-valued filter variables, agent instance, ...
			return AddPath(onSql, facet, onPath, null, initialClass);
		}
		public override string InhAddPath( ONSqlSelect onSql, JoinType joinType, string facet, ONPath onPath, string initialClass)
		{
			// initialClass is used for Object-valued arguments, object-valued filter variables, agent instance, ...
			return AddPath(onSql, joinType, facet, onPath, null, initialClass, false);
		}
		public override string InhAddPath(ONSqlSelect onSql, string facet, ONPath onPath, string initialClass, bool isLinkedTo)
		{
			return AddPath(onSql, JoinType.InnerJoin, facet, onPath, null, initialClass, false, isLinkedTo);
		}
		#endregion AddPath

		#region FixInstance
		/// <summary>This method adds to the SQL statement the part that fixes the instance</summary>
		/// <param name="onSql">This parameter has the current SQL statement</param>
		/// <param name="onPath">Path to add to SQL statement</param>
		/// <param name="processedOnPath">Path pocessed until the call of this method</param>
		/// <param name="oid">OID to fix the instance in the SQL statement</param>
		public static void FixInstance(ONSqlSelect onSql, ONPath onPath, ONPath processedOnPath, RevisionPasajeroOid oid)
		{
			FixInstance(onSql, onPath, processedOnPath, oid, false);
		}
		/// <summary>This method adds to the SQL statement the part that fixes the instance</summary>
		/// <param name="onSql">This parameter has the current SQL statement</param>
		/// <param name="onPath">Path to add to SQL statement</param>
		/// <param name="processedOnPath">Path pocessed until the call of this method</param>
		/// <param name="oid">OID to fix the instance in the SQL statement</param>
		/// <param name="isLinkedTo">The alias belongs to a role in a linked To element</param>
		public static void FixInstance(ONSqlSelect onSql, ONPath onPath, ONPath processedOnPath, RevisionPasajeroOid oid, bool isLinkedTo)
		{
			if ((onPath != null) && (string.Compare(onPath.Path, "agent", true) == 0))
			{
				if (onSql.GetParameter("agent") == null)
				{
					string lAlias = AddPath(onSql, JoinType.InnerJoin, "RevisionPasajero", onPath, processedOnPath, "RevisionPasajero", false, isLinkedTo);
					onSql.AddWhere(lAlias + "." + CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO + " = ?");
					onSql.AddWhereParameter("agent", oid.Id_RevisionPasajeroAttr);
				}
			}
			else
			{
				string lAlias = AddPath(onSql, JoinType.InnerJoin, "RevisionPasajero", onPath, processedOnPath, "", false, isLinkedTo);
				onSql.AddWhere(lAlias + "." + CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO + " = ?");
				onSql.AddWhereParameter("", oid.Id_RevisionPasajeroAttr);
			}
		}
		public override void InhFixInstance(ONSqlSelect onSql, ONPath onPath, ONPath OnPath, ONOid oid)
		{
			FixInstance(onSql, onPath, OnPath, oid as RevisionPasajeroOid);
		}

		public override void InhFixInstance(ONSqlSelect onSql, ONPath onPath, ONPath OnPath, ONOid oid, bool isLinkedTo)
		{
			FixInstance(onSql, onPath, OnPath, oid as RevisionPasajeroOid, isLinkedTo);
		}
		#endregion FixInstance

		#region AlternateKeys
		#endregion AlternateKeys

		#region GetTargetClassName
		public static string GetTargetClassName(ONPath onPath)
		{
			ONPath lOnPath = new ONPath(onPath);
			
			if (lOnPath.Count == 0)
				return "RevisionPasajero";
			
			string lRol = lOnPath.RemoveHead();
			if (string.Compare(lRol, "Revision", true) == 0)
				return RevisionData.GetTargetClassName(lOnPath);
			if (string.Compare(lRol, "PasajeroAeronave", true) == 0)
				return PasajeroAeronaveData.GetTargetClassName(lOnPath);
			
			return "";
		}
		public override string InhGetTargetClassName(ONPath onPath)
		{
			return GetTargetClassName(onPath);
		}
		#endregion GetTargetClassName

		#region RetrieveInstances
		public static string RetrieveInstances(ONSqlSelect onSql, ONDisplaySet displaySet, ONPath onPath, ONContext onContext)
		{
			string lAlias = onSql.CreateAlias(CtesBD.TBL_REVISIONPASAJERO, onPath, "RevisionPasajero");
			ONDisplaySet lSourceDS = null; 
			if(displaySet != null)
			{
				lSourceDS = new ONDisplaySet(displaySet);
				displaySet.Clear();
			}
			if (displaySet == null)
				onSql.AddSelect(lAlias + "." + CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO + ", " + lAlias + "." + CtesBD.FLD_REVISIONPASAJERO_FK_REVISION_1 + ", " + lAlias + "." + CtesBD.FLD_REVISIONPASAJERO_FK_PASAJEROAERO_1 + ", " + lAlias + "." + CtesBD.FLD_REVISIONPASAJERO_ESTADOOBJ + ", " + lAlias + "." + CtesBD.FLD_REVISIONPASAJERO_FUM);
			else
			{
				displaySet.Add(new ONDisplaySetItem(CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO));
				onSql.AddSelect(lAlias + "." + CtesBD.FLD_REVISIONPASAJERO_ID_REVISIONPASAJERO);
				
				displaySet.Add(new ONDisplaySetItem(CtesBD.FLD_REVISIONPASAJERO_FK_REVISION_1));
				onSql.AddSelect(lAlias + "." + CtesBD.FLD_REVISIONPASAJERO_FK_REVISION_1);
				
				displaySet.Add(new ONDisplaySetItem(CtesBD.FLD_REVISIONPASAJERO_FK_PASAJEROAERO_1));
				onSql.AddSelect(lAlias + "." + CtesBD.FLD_REVISIONPASAJERO_FK_PASAJEROAERO_1);
				
				displaySet.Add(new ONDisplaySetItem(CtesBD.FLD_REVISIONPASAJERO_ESTADOOBJ));
				onSql.AddSelect(lAlias + "." + CtesBD.FLD_REVISIONPASAJERO_ESTADOOBJ);
				
				displaySet.Add(new ONDisplaySetItem(CtesBD.FLD_REVISIONPASAJERO_FUM));
				onSql.AddSelect(lAlias + "." + CtesBD.FLD_REVISIONPASAJERO_FUM);
				
			}


			// Related attributes
			if (displaySet != null)
			{
				foreach (ONDisplaySetItem lDisplaySetItem in lSourceDS)
				{
					if((lDisplaySetItem.Path.IndexOf(".") > 0) && (lDisplaySetItem.InData) && (! lDisplaySetItem.HasHV))
					{
						displaySet.Add(lDisplaySetItem);

						string lPath = lDisplaySetItem.Path.Substring(0, lDisplaySetItem.Path.LastIndexOf("."));
						string lFacetName = RevisionPasajeroInstance.GetTargetClass(onContext, typeof(RevisionPasajeroInstance), new ONPath(lDisplaySetItem.Path));
						onSql.AddSelect(AddPath(onSql, JoinType.LeftJoin, lFacetName, new ONPath(lPath), onPath, "", false) + "." + ONInstance.GetFieldNameOfAttribute(typeof(RevisionPasajeroInstance), new ONPath(lDisplaySetItem.Path)));
					}
				}
			}
			return (lAlias);
		}
		public override string InhRetrieveInstances(ONSqlSelect onSql, ONDisplaySet displaySet, ONPath onPath, ONContext onContext)
		{
			return RetrieveInstances(onSql, displaySet, onPath, onContext);
		}
		#endregion RetrieveInstances

		#region LoadTexts
		#endregion LoadTexts
		
		#region LoadBlobs
		#endregion LoadBlobs

		#endregion Sql Management

		#region Association Operators
		#endregion Association Operators
		
		#region Compare Oid
		/// <summary>
		/// Compare with oid fields
		/// </summary>
		/// <param name="instance1">Instance 1</param>
		/// <param name="instance2">Instance 2</param>
		/// <returns>0 if equals, -1 if instance1 is minor, 1 if instance2 is mayor</returns>
		public override int CompareUnionOID(ONInstance instance1, ONInstance instance2)
		{
			// Null Management
			if ((instance1 == null) && (instance2 == null))
				return 0;
			else if (instance1 == null)
				return -1;
			else if (instance2 == null)
				return 1;

			RevisionPasajeroInstance lInstance1 = instance1 as RevisionPasajeroInstance;
			RevisionPasajeroInstance lInstance2 = instance2 as RevisionPasajeroInstance;

			int lCompare = 0;

			// id_RevisionPasajero(Asc)
			lCompare = lInstance1.Id_RevisionPasajeroAttr.CompareTo(lInstance2.Id_RevisionPasajeroAttr);
			if (lCompare != 0)
				return lCompare;

			return 0;						
		}
		#endregion Compare Oid
	}
}
