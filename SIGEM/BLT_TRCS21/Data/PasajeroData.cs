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
	[ONDataClass("Pasajero")]
	internal class PasajeroData : ONDBData
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
				return CtesBD.TBL_PASAJERO;
			}
		}
		#endregion Properties
		
		#region Constructors
		/// <summary>
		/// Constructor of the specific Data class
		/// </summary>
		/// <param name="onContext">Current context</param>
		public PasajeroData(ONContext onContext)
			: base(onContext, "Pasajero")
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
		public static PasajeroInstance LoadFacet(ONContext onContext, ONDisplaySet displaySet, object[] columns, ref int index)
		{
			PasajeroInstance lInstance = new PasajeroInstance(onContext);
			lInstance.Oid = new PasajeroOid();

			// Field 'id_Pasajero'
			lInstance.Oid.Id_PasajeroAttr = new ONInt(Convert.ToInt32(columns[index++]));
			lInstance.StateObj = new ONString(((string) columns[index++]).TrimEnd());
			lInstance.Lmd = new ONDateTime((DateTime) columns[index++]);
			if ((displaySet != null) && (displaySet.Contains("Nombre")))
			{
				lInstance.NombreAttr = new ONText((string) columns[index++]);
			}

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
			PasajeroCollection lQuery = null;
			bool lWithStartRow = (startRowOid != null).TypedValue;
			long lCount = -1;
			if (!lWithStartRow)
				lCount = 0;

			IDataReader lDataReader = null;
			ONSQLConnection lOnSQLConnection = null;

			try
			{
				lDataReader = Execute(onSql) as IDataReader;

				PasajeroInstance lInstance = null;
				PasajeroInstance lAntInstance = null;
				if (lDataReader != null)
				{
					object[] lColumns;
					if(displaySet == null)
						lColumns = new object[3];
					else
						lColumns = new object[displaySet.ElementsInData];

					lQuery = new PasajeroCollection(OnContext);
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
				string ltraceItem = "Method: ExecuteSql, Component: PasajeroData";
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
			PasajeroInstance lInstance = null;
			if (startRowOid != null)
			{
				lInstance = new PasajeroInstance(OnContext);
				lInstance.Oid = startRowOid as PasajeroOid;	
			}
			
			//Default OrderCriteria
			if (comparer == null)
			{
				string lAlias = onSql.GetAlias("Pasajero", initialPath);
				if (lInstance != null)
				{
					onSql.AddOrderBy(lAlias, CtesBD.FLD_PASAJERO_ID_PASAJERO, OrderByTypeEnumerator.Asc, lInstance.Oid.Id_PasajeroAttr);
				}
				else
				{
					onSql.AddOrderBy(lAlias, CtesBD.FLD_PASAJERO_ID_PASAJERO, OrderByTypeEnumerator.Asc, null);
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
					onSql.AddOrderBy(PasajeroData.AddPath(onSql, JoinType.LeftJoin, lOrderCriteriaItem.Facet, lPath, null, lOrderCriteriaItem.DomainArgument, false), lOrderCriteriaItem.Attribute, lOrderCriteriaItem.Type, lAttrStartRow);
					lUseStartRow = (lAttrStartRow != null);
				}
				else
					onSql.AddOrderBy(PasajeroData.AddPath(onSql, JoinType.LeftJoin, lOrderCriteriaItem.Facet, lPath, null, lOrderCriteriaItem.DomainArgument, false), lOrderCriteriaItem.Attribute, lOrderCriteriaItem.Type, null);
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
			PasajeroInstance lInstance = instance as PasajeroInstance;

			ONSqlInsert lOnSql = new ONSqlInsert();
			lOnSql.AddInto(CtesBD.TBL_PASAJERO);
			// Field 'id_Pasajero'
			lOnSql.AddValue(CtesBD.FLD_PASAJERO_ID_PASAJERO, lInstance.Oid.Id_PasajeroAttr);
			lOnSql.AddValue(CtesBD.FLD_PASAJERO_ESTADOOBJ, lInstance.StateObj);
			lOnSql.AddValue(CtesBD.FLD_PASAJERO_FUM, new ONDateTime(DateTime.Now));
			// Field 'Nombre'
			lOnSql.AddValue(CtesBD.FLD_PASAJERO_NOMBRE, lInstance.NombreAttr);
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
			PasajeroInstance lInstance = instance as PasajeroInstance;
			
			ONSqlDelete lOnSql = new ONSqlDelete();
			lOnSql.AddFrom(CtesBD.TBL_PASAJERO);
			lOnSql.AddWhere(CtesBD.FLD_PASAJERO_ID_PASAJERO, lInstance.Oid.Id_PasajeroAttr);
			Execute(lOnSql);
		}
		#endregion

		#region Update Edited
		/// <summary>This method builds the SQL statement to edit the instance in database</summary>
		/// <param name="instance">This parameter represents the instance to be modified in the database</param>
		public override void UpdateEdited(ONInstance instance)
		{
			PasajeroInstance lInstance = instance as PasajeroInstance;

			ONSqlUpdate lOnSql = new ONSqlUpdate();
			lOnSql.AddUpdate(CtesBD.TBL_PASAJERO);
			// Field 'id_Pasajero'
			// Update not needed (OID)
			lOnSql.AddSet(CtesBD.FLD_PASAJERO_ESTADOOBJ, lInstance.StateObj);
			lOnSql.AddSet(CtesBD.FLD_PASAJERO_FUM, new ONDateTime(DateTime.Now));
			// Field 'Nombre'
			if (lInstance.NombreAttrModified)
				lOnSql.AddSet(CtesBD.FLD_PASAJERO_NOMBRE, lInstance.NombreAttr);

			lOnSql.AddWhere(CtesBD.FLD_PASAJERO_ID_PASAJERO, lInstance.Oid.Id_PasajeroAttr);
			Execute(lOnSql);
		}
		#endregion

		#region Autonumeric
		/// <summary>Obtains the last autonumeric that has the attribute id_Pasajero</summary>
		public ONInt GetAutonumericid_Pasajero()
		{
			ONSqlMax lOnSql = new ONSqlMax();
			string lAlias = lOnSql.CreateAlias(CtesBD.TBL_PASAJERO, null, "Pasajero");
			lOnSql.AddSelect(lAlias, CtesBD.FLD_PASAJERO_ID_PASAJERO);
		
			object lRet = Execute(lOnSql);
			if (lRet is DBNull)
				return new ONInt(1);
			else
				return new ONInt(Convert.ToInt32(lRet) + 1);
		}
 		#endregion Autonumeric
		#endregion Services

		#region Relationship / Inheritance

		#region Role 'PasajeroAeronave'
		public ONCollection PasajeroAeronaveRole(PasajeroAeronaveOid oid)
		{
			ONSqlSelect lOnSql = new ONSqlSelect();
			
			//Create select
			PasajeroAeronaveData.AddPath(lOnSql, "Pasajero", new ONPath("Pasajero"), null, "");
			RetrieveInstances(lOnSql, null, new ONPath("Pasajero"), OnContext);
			
			//Fix related instance
			PasajeroAeronaveData.FixInstance(lOnSql, null, null, oid);
			
			//Execute
			return ExecuteQuery(lOnSql);
		}


		public void PasajeroAeronaveRoleInsert(PasajeroOid localOid, PasajeroAeronaveOid relatedOid)
		{
			ONSqlUpdate lOnSql = new ONSqlUpdate();
			lOnSql.AddUpdate(CtesBD.TBL_PASAJEROAERONAVE);
			lOnSql.AddSet(CtesBD.FLD_PASAJEROAERONAVE_FK_PASAJERO_1, localOid.Id_PasajeroAttr);
			lOnSql.AddWhere(CtesBD.FLD_PASAJEROAERONAVE_ID_PASAJEROAERONAVE, relatedOid.Id_PasajeroAeronaveAttr);
			Execute(lOnSql);
		}

		public void PasajeroAeronaveRoleDelete(PasajeroOid localOid, PasajeroAeronaveOid relatedOid)
		{
			ONSqlUpdate lOnSql = new ONSqlUpdate();
			lOnSql.AddUpdate(CtesBD.TBL_PASAJEROAERONAVE);
			lOnSql.AddSet(CtesBD.FLD_PASAJEROAERONAVE_FK_PASAJERO_1, ONInt.Null);
			lOnSql.AddWhere(CtesBD.FLD_PASAJEROAERONAVE_FK_PASAJERO_1, localOid.Id_PasajeroAttr);
			lOnSql.AddWhere(CtesBD.FLD_PASAJEROAERONAVE_ID_PASAJEROAERONAVE, relatedOid.Id_PasajeroAeronaveAttr);
			Execute(lOnSql);
		}

		public void PasajeroAeronaveRoleDelete(PasajeroOid oid)
		{

			ONSqlUpdate lOnSql = new ONSqlUpdate();
			lOnSql.AddUpdate(CtesBD.TBL_PASAJEROAERONAVE);
			lOnSql.AddSet(CtesBD.FLD_PASAJEROAERONAVE_FK_PASAJERO_1, ONInt.Null);
			lOnSql.AddWhere(CtesBD.FLD_PASAJEROAERONAVE_FK_PASAJERO_1, oid.Id_PasajeroAttr);
			
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
			string lAliasProcessed = onSql.GetAlias("Pasajero", processedPath, isLinkedTo);
			if (lAliasProcessed == "")
			{
				force = false;
				lAliasProcessed = onSql.CreateAlias(joinType, lAliasProcessed, CtesBD.TBL_PASAJERO, processedPath, "Pasajero", force, isLinkedTo);
			}
			
			//Target table 
			string lAlias = onSql.GetAlias("PasajeroAeronave", lOnPath, isLinkedTo);
			if (lAlias == "")
			{
				force = false;
				lAlias = onSql.CreateAlias(joinType, lAliasProcessed, CtesBD.TBL_PASAJEROAERONAVE, lOnPath, "PasajeroAeronave", force, isLinkedTo);
				onSql.AddAliasWhere(lAlias, lAliasProcessed + "." + CtesBD.FLD_PASAJERO_ID_PASAJERO + "=" + lAlias + "." + CtesBD.FLD_PASAJEROAERONAVE_FK_PASAJERO_1);
			}
			//Target path
			if ((((object) onPath == null) || (onPath.Count == 0)) && (string.Compare("PasajeroAeronave", facet, true) == 0) && (!force))
				return lAlias;
				
			return PasajeroAeronaveData.AddPath(onSql, joinType, facet, onPath, lOnPath, "", force, isLinkedTo);
		}		
		#endregion Role 'PasajeroAeronave'

		#region Facet 'Pasajero'
		private static string PasajeroFacetAddSql(ONSqlSelect onSql, ONPath onPath)
		{
			return PasajeroFacetAddSql(JoinType.InnerJoin, onSql, onPath, false, false);
		}
		private static string PasajeroFacetAddSql(ONSqlSelect onSql, ONPath onPath, bool force)
		{
			return PasajeroFacetAddSql(JoinType.InnerJoin, onSql, onPath, force, false);
		}
		private static string PasajeroFacetAddSql(JoinType joinType, ONSqlSelect onSql, ONPath onPath, bool force)
		{
			return PasajeroFacetAddSql(joinType, onSql, onPath, force, false);
		}
		private static string PasajeroFacetAddSql(JoinType joinType, ONSqlSelect onSql, ONPath onPath, bool force, bool isLinkedTo)
		{
			//Target table
			string lAliasFacet = "";
			string lAlias = onSql.CreateAlias(joinType, "", CtesBD.TBL_PASAJERO, onPath, "Pasajero", false, isLinkedTo);

			// Load facet from 'Pasajero' to 'Pasajero'
			lAliasFacet = onSql.GetAlias("Pasajero", onPath, isLinkedTo);
			if ((lAliasFacet == "") || force)
			{
				if (force)
					lAliasFacet = onSql.CreateAlias(joinType, "", CtesBD.TBL_PASAJERO, onPath, "Pasajero", force, isLinkedTo);
				else
					lAliasFacet = onSql.CreateAlias(joinType, lAlias, CtesBD.TBL_PASAJERO, onPath, "Pasajero", force, isLinkedTo);
				onSql.AddAliasWhere(lAliasFacet, lAlias + "." + CtesBD.FLD_PASAJERO_ID_PASAJERO + " = " + lAliasFacet + "." + CtesBD.FLD_PASAJERO_ID_PASAJERO);
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
					return PasajeroFacetAddSql(joinType, onSql, processedOnPath, forceLastAlias, isLinkedTo);

				if ((processedOnPath == null) || (processedOnPath.Count == 0))
					return (onSql.CreateAlias(joinType, "", CtesBD.TBL_PASAJERO, null, "Pasajero", false, isLinkedTo));
				else
					return (onSql.CreateAlias(joinType, "", CtesBD.TBL_PASAJERO, processedOnPath, "Pasajero", false, isLinkedTo));
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
			if (string.Compare(lRole, "PasajeroAeronave", true) == 0)
			{
				if (lOnPathExist)
					return PasajeroAeronaveData.AddPath(onSql, joinType, facet, lOnPath, lProcessedOnPath, "", forceLastAlias, isLinkedTo);
				else
					return PasajeroAeronaveRoleAddSql(onSql, joinType, facet, lOnPath, processedOnPath, lRole, forceLastAlias, isLinkedTo);
			}
			
			initialClass = "Pasajero";

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
		public static void FixInstance(ONSqlSelect onSql, ONPath onPath, ONPath processedOnPath, PasajeroOid oid)
		{
			FixInstance(onSql, onPath, processedOnPath, oid, false);
		}
		/// <summary>This method adds to the SQL statement the part that fixes the instance</summary>
		/// <param name="onSql">This parameter has the current SQL statement</param>
		/// <param name="onPath">Path to add to SQL statement</param>
		/// <param name="processedOnPath">Path pocessed until the call of this method</param>
		/// <param name="oid">OID to fix the instance in the SQL statement</param>
		/// <param name="isLinkedTo">The alias belongs to a role in a linked To element</param>
		public static void FixInstance(ONSqlSelect onSql, ONPath onPath, ONPath processedOnPath, PasajeroOid oid, bool isLinkedTo)
		{
			if ((onPath != null) && (string.Compare(onPath.Path, "agent", true) == 0))
			{
				if (onSql.GetParameter("agent") == null)
				{
					string lAlias = AddPath(onSql, JoinType.InnerJoin, "Pasajero", onPath, processedOnPath, "Pasajero", false, isLinkedTo);
					onSql.AddWhere(lAlias + "." + CtesBD.FLD_PASAJERO_ID_PASAJERO + " = ?");
					onSql.AddWhereParameter("agent", oid.Id_PasajeroAttr);
				}
			}
			else
			{
				string lAlias = AddPath(onSql, JoinType.InnerJoin, "Pasajero", onPath, processedOnPath, "", false, isLinkedTo);
				onSql.AddWhere(lAlias + "." + CtesBD.FLD_PASAJERO_ID_PASAJERO + " = ?");
				onSql.AddWhereParameter("", oid.Id_PasajeroAttr);
			}
		}
		public override void InhFixInstance(ONSqlSelect onSql, ONPath onPath, ONPath OnPath, ONOid oid)
		{
			FixInstance(onSql, onPath, OnPath, oid as PasajeroOid);
		}

		public override void InhFixInstance(ONSqlSelect onSql, ONPath onPath, ONPath OnPath, ONOid oid, bool isLinkedTo)
		{
			FixInstance(onSql, onPath, OnPath, oid as PasajeroOid, isLinkedTo);
		}
		#endregion FixInstance

		#region AlternateKeys
		#endregion AlternateKeys

		#region GetTargetClassName
		public static string GetTargetClassName(ONPath onPath)
		{
			ONPath lOnPath = new ONPath(onPath);
			
			if (lOnPath.Count == 0)
				return "Pasajero";
			
			string lRol = lOnPath.RemoveHead();
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
			string lAlias = onSql.CreateAlias(CtesBD.TBL_PASAJERO, onPath, "Pasajero");
			ONDisplaySet lSourceDS = null; 
			if(displaySet != null)
			{
				lSourceDS = new ONDisplaySet(displaySet);
				displaySet.Clear();
			}
			if (displaySet == null)
				onSql.AddSelect(lAlias + "." + CtesBD.FLD_PASAJERO_ID_PASAJERO + ", " + lAlias + "." + CtesBD.FLD_PASAJERO_ESTADOOBJ + ", " + lAlias + "." + CtesBD.FLD_PASAJERO_FUM);
			else
			{
				displaySet.Add(new ONDisplaySetItem(CtesBD.FLD_PASAJERO_ID_PASAJERO));
				onSql.AddSelect(lAlias + "." + CtesBD.FLD_PASAJERO_ID_PASAJERO);
				
				displaySet.Add(new ONDisplaySetItem(CtesBD.FLD_PASAJERO_ESTADOOBJ));
				onSql.AddSelect(lAlias + "." + CtesBD.FLD_PASAJERO_ESTADOOBJ);
				
				displaySet.Add(new ONDisplaySetItem(CtesBD.FLD_PASAJERO_FUM));
				onSql.AddSelect(lAlias + "." + CtesBD.FLD_PASAJERO_FUM);
				
				if (lSourceDS.Contains(CtesBD.FLD_PASAJERO_NOMBRE))
				{
					displaySet.Add(new ONDisplaySetItem(CtesBD.FLD_PASAJERO_NOMBRE));
					onSql.AddSelect(lAlias + "." + CtesBD.FLD_PASAJERO_NOMBRE);
				}
					
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
						string lFacetName = PasajeroInstance.GetTargetClass(onContext, typeof(PasajeroInstance), new ONPath(lDisplaySetItem.Path));
						onSql.AddSelect(AddPath(onSql, JoinType.LeftJoin, lFacetName, new ONPath(lPath), onPath, "", false) + "." + ONInstance.GetFieldNameOfAttribute(typeof(PasajeroInstance), new ONPath(lDisplaySetItem.Path)));
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
				/// <summary>Load the data retrieved from the Data Base to components of the application</summary>
		/// <param name="onContext">This parameter has the current context</param>
		/// <param name="oid">OID of the instance whose text attribute value is wanted to be loaded</param>
		public static ONText LoadTextNombre(ONContext onContext, PasajeroOid oid)
		{
			ONSqlSelect lOnSql = new ONSqlSelect();
			
			lOnSql.CreateAlias(CtesBD.TBL_PASAJERO, null, "Pasajero");
			lOnSql.AddSelect(CtesBD.FLD_PASAJERO_NOMBRE);

			FixInstance(lOnSql, null, null, oid);
			
			// Create Data Component
			PasajeroData lData = new PasajeroData(onContext);
			
			// Execute query
			ArrayList lSqlParameters;
			ONText lReturn = new ONText((string) lData.ExecuteScalar(lOnSql.GenerateSQL(out lSqlParameters), lSqlParameters));
			return lReturn;
		}
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

			PasajeroInstance lInstance1 = instance1 as PasajeroInstance;
			PasajeroInstance lInstance2 = instance2 as PasajeroInstance;

			int lCompare = 0;

			// id_Pasajero(Asc)
			lCompare = lInstance1.Id_PasajeroAttr.CompareTo(lInstance2.Id_PasajeroAttr);
			if (lCompare != 0)
				return lCompare;

			return 0;						
		}
		#endregion Compare Oid
	}
}
