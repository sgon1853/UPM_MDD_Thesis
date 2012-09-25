// 3.4.4.5

using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using ONSQLConnection = System.Data.SqlClient.SqlConnection;
using ONSQLCommand = System.Data.SqlClient.SqlCommand;
using ONSQLParameter = System.Data.SqlClient.SqlParameter;

using SIGEM.Business.OID;
using SIGEM.Business.SQL;
using SIGEM.Business.Instance;
using SIGEM.Business.Collection;
using SIGEM.Business.Query;
using SIGEM.Business.Types;
using SIGEM.Business.Others.Enumerations;
using SIGEM.Business.Exceptions;


namespace SIGEM.Business.Data
{
	/// <summary>
	/// Superclass of Data
	/// </summary>
	internal class ONDBData : ONData
	{
		#region Members
		// Connection with the Data Base
		protected ONSQLConnection mSqlConnection;
		// Represents instruction SQL to be executed
		protected ONSQLCommand mSqlCommand;
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONDBData(ONContext onContext, string className)
			: base(onContext, className)
		{
			// Create connection
			if (OnContext == null)
				mSqlConnection = GetConnection();
			else
			{
				if (OnContext.SqlConnection == null)
					OnContext.SqlConnection = GetConnection();

				mSqlConnection = OnContext.SqlConnection as ONSQLConnection;
			}
			mSqlCommand = null;
		}
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONDBData(ONContext onContext)
			: base(onContext, "")
		{
			// Create connection
			if (OnContext == null)
				mSqlConnection = GetConnection();
			else
			{
				if (OnContext.SqlConnection == null)
					OnContext.SqlConnection = GetConnection();

				mSqlConnection = OnContext.SqlConnection as ONSQLConnection;
			}
			mSqlCommand = null;
		}
		/// <summary>
		/// Default Destructor
		/// </summary>
		~ONDBData()
		{
			Close();
		}
		#endregion
	
		#region ExecuteQuery
		/// <summary>
		/// Execution of a query
		/// </summary>
		/// <param name="onSql">Sentence SQL to be executed</param>
		public ONCollection ExecuteQuery(ONSql onSql)
		{
			return ExecuteSql(onSql, null, null, null, null, 0);
		}
		/// <summary>
		/// Execution of a sql
		/// </summary>
		/// <param name="onSql">Sentence SQL to be executed</param>
		/// <param name="onFilterList">List of filters to check</param>
		/// <param name="comparer">Order Criteria that must be followed by the query</param>
		/// <param name="startRowOID">OID frontier</param>
		/// <param name="blockSize">Number of instances to be returned</param>
		public virtual ONCollection ExecuteSql(ONSql onSql, ONFilterList onFilterList, ONDisplaySet displaySet, ONOrderCriteria comparer, ONOid startRowOID, int blockSize)
		{
			return null;
		}
		#endregion

		#region Exist
		/// <summary>
		/// Checks if a determinate instance exists
		/// </summary>
		/// <param name="oid">OID to search the instance</param>
		/// <param name="onFilterList">Filters to theck</param>
		/// <returns>If exists</returns>
		public override bool Exist(ONOid oid, ONFilterList onFilterList)
		{
			if ((onFilterList == null) || (!onFilterList.InMemory))
			{
				ONSqlCount lOnSql = new ONSqlCount();

				// Create select and first table
				string lAlias = lOnSql.CreateAlias(TableName, null, ClassName);
				lOnSql.AddSelect(lAlias);

				// Fix instance
				InhFixInstance(lOnSql, null, null, oid);

				// Add filter formula
				if(onFilterList != null)
					onFilterList.FilterInData(lOnSql, this);

				// Execute
				return (Convert.ToInt32(Execute(lOnSql)) > 0);
			}
			else
				return (Search(oid, onFilterList, null) != null);
		}
		#endregion Exist

		#region Search
		/// <summary>
		/// Returns the instance with the Oid
		/// </summary>
		/// <param name="oid">OID to search the instance</param>
		/// <param name="onFilterList">Filters to theck</param>
		/// <returns>The instance searched</returns>
		public override ONInstance Search(ONOid oid, ONFilterList onFilterList, ONDisplaySet displaySet)
		{
			ONSqlSelect lOnSql = new ONSqlSelect();

			// Create select and first table
			InhRetrieveInstances(lOnSql, displaySet, null, OnContext);

			// Fix instance
			InhFixInstance(lOnSql, null, null, oid);

			// Add filter formula
			onFilterList.FilterInData(lOnSql, this);

			// Execute
			ONCollection lCollection = ExecuteSql(lOnSql, onFilterList, displaySet, null, null, 0);

			if (lCollection.Count > 0)
				return lCollection[0] as ONInstance;
			else
				return null;
		}
		#endregion Search

		#region Queries
		
		/// <summary>
		/// Fix related instance
		/// </summary>
		/// <param name="onSql">Sentence SQL to be executed</param>
		/// <param name="linkedTo">List to reach the class to retrieve the related instance</param>
		protected bool AddLinkedTo(ONSqlSelect onSql, ONLinkedToList linkedTo)
		{
			// Fix related instance
			foreach(KeyValuePair<ONPath, ONOid> lDictionaryEntry in linkedTo)
			{
				ONPath lPath = lDictionaryEntry.Key as ONPath;
				ONOid lOID = lDictionaryEntry.Value as ONOid;

				string lAliasRelated = InhAddPath(onSql, lOID.ClassName, lPath, "", true);

				// Check Visibility
				if (!ONInstance.IsVisibleInv(ONContext.GetType_Instance(ClassName), lPath, OnContext))
					return false;

				ONDBData lData = ONContext.GetComponent_Data(lOID.ClassName, OnContext) as ONDBData;
				lData.InhFixInstance(onSql, null, lPath, lOID, true);
			}

			return (true);
		}
		#endregion

		#region ExecuteQuery
		/// <summary>
		/// Retrieve all the instances of a determinate class that fulfil a determinate formula of searching
		/// </summary>
		/// <param name="linkedTo">List to reach the class to retrieve the related instance</param>
		/// <param name="filters">Formula to search concrete instances</param>
		/// <param name="comparer">Order Criteria that must be followed by the query</param>
		/// <param name="startRowOID">OID frontier</param>
		/// <param name="blockSize">Number of instances to be returned</param> 
		public override ONCollection ExecuteQuery(ONLinkedToList linkedTo, ONFilterList filters, ONDisplaySet displaySet, ONOrderCriteria comparer, ONOid startRowOid, int blockSize)
		{
            try
            {
			ONCollection lInstances = null;
			Type lTypeInstance = ONContext.GetType_Instance(ClassName);
			Type lTypeQuery = ONContext.GetType_Query(ClassName);

			// Initialize the list of related queries
			if (linkedTo == null)
				linkedTo = new ONLinkedToList();

			// Initialize the filter list
			if (filters == null)
				filters = new ONFilterList();

			ONLinkedToList lLinkedToLegacy = new ONLinkedToList();
			ONLinkedToList lLinkedToLocal = new ONLinkedToList();
			ONLinkedToList lLinkedToMixed = new ONLinkedToList();
			
			#region Treatment of LinkedTo
			foreach(KeyValuePair<ONPath, ONOid> lDictionaryEntry in linkedTo)
			{
				ONPath lPath = lDictionaryEntry.Key as ONPath;
				ONOid lOid = lDictionaryEntry.Value as ONOid;
			
				ONPath lInversePath = new ONPath(ONInstance.InversePath(lTypeInstance, lPath));
				Type lTypeTargetClassInstance = ONContext.GetType_Instance(ONInstance.GetTargetClass(OnContext, lTypeInstance, lPath));
				if ((lInversePath.Count == 0) || (!ONInstance.IsVisible(lTypeTargetClassInstance, lInversePath, OnContext)))
					return ONContext.GetComponent_Collection(ClassName, OnContext);
		
				bool lexistLV = false;
				ONData lData = ONContext.GetComponent_Data(InhGetTargetClassName(lPath), OnContext);

				if (lData.GetType().BaseType == typeof(ONLVData))
				{
					if (!lOid.Exist(OnContext, null))
						return ONContext.GetComponent_Collection(ClassName, OnContext);
				}
				
				foreach (string lRole in lInversePath.Roles)
				{
					lData = ONContext.GetComponent_Data(lData.InhGetTargetClassName(new ONPath(lRole)), OnContext);
					if (lData.GetType().BaseType == typeof(ONLVData))
						lexistLV = true;
				}
				if(!lexistLV)
					lLinkedToLocal.mLinkedToList.Add(lPath, lOid);
				else
					lLinkedToMixed.mLinkedToList.Add(lPath, lOid);
			}
			#endregion

			#region displaySet
			if (!filters.PreloadRelatedAttributes)
				displaySet = null;
			#endregion displaySet

			#region No link item
			if ((linkedTo.mLinkedToList.Count == 0) || (lLinkedToMixed.mLinkedToList.Count > 0))
			{
				if ((GetType().BaseType != typeof(ONLVData)) || (filters.InData))
					lInstances = SolveQuery(new ONLinkedToList(), filters, displaySet, comparer, startRowOid, blockSize);
			}
			#endregion

			#region Local Link
			if (lLinkedToLocal.mLinkedToList.Count > 0)
			{
				ONCollection lInstancesAux = SolveQuery(lLinkedToLocal, filters, displaySet, comparer, startRowOid, blockSize);
				if (lInstances != null)
					lInstances.Intersection(lInstancesAux);
				else
					lInstances = lInstancesAux;
			}
			#endregion
			
			#region Hybrid Link
			if (lLinkedToMixed.mLinkedToList.Count > 0)
			{
				ONCollection lInstancesAux = null;
		
				foreach(KeyValuePair<ONPath, ONOid> lDictionaryEntry in lLinkedToMixed)
				{
					ONPath lPath = lDictionaryEntry.Key as ONPath;
					ONOid lOid = lDictionaryEntry.Value as ONOid;

					if (lPath.Roles.Count == 1)
					{
						ONLinkedToList lLinked = new ONLinkedToList();
						lLinked[lPath] = lOid;
						ONCollection lInstanceColl = SolveQuery(lLinked, filters, displaySet, comparer, startRowOid, blockSize);
						if (lInstances != null)
							lInstances.Intersection(lInstanceColl);
						else
							lInstances = lInstanceColl;
						continue;
					}
					
					#region Optimized Path
					ONLinkedToList linkedToOptimized = new ONLinkedToList();
					ONPath lInversePath = new ONPath(ONInstance.InversePath(lTypeInstance, lPath));
					ONPath lOptimizedRole = new ONPath(lInversePath.RemoveHead() as string);				
					Type lTypeInstanceDestiny = ONContext.GetType_Instance(InhGetTargetClassName(lPath));

					bool lBeginIsLegacy = (ONInstance.IsLegacy(lTypeInstanceDestiny, lOptimizedRole));
					bool lEnterLoop = true;
					ONData lData = ONContext.GetComponent_Data(InhGetTargetClassName(lPath), OnContext);
					if(lData.GetType().BaseType != typeof(ONLVData))
						if (ONContext.GetType_Data(lData.InhGetTargetClassName(lOptimizedRole)).BaseType == typeof(ONLVData))
							lEnterLoop = false;		

					lPath.RemoveTail();
					if (lEnterLoop)
					{
						while (lInversePath.Roles.Count > 0)
						{
							lData = ONContext.GetComponent_Data(InhGetTargetClassName(lPath), OnContext);
							if ((!lBeginIsLegacy) && (ONContext.GetType_Data(lData.InhGetTargetClassName(new ONPath(lInversePath.Roles[0]))).BaseType == typeof(ONLVData)))
								break;
							if ((lBeginIsLegacy) && (ONContext.GetType_Data(lData.InhGetTargetClassName(new ONPath(lInversePath.Roles[0]))).BaseType != typeof(ONLVData)))
								break;

							lOptimizedRole.Roles.Add(lInversePath.RemoveHead());
							lPath.RemoveTail();
						}
					}
					
					linkedToOptimized[ONInstance.InversePath(lTypeInstanceDestiny, lOptimizedRole)] = lOid;
					if ((lPath.Count > 0) || (lBeginIsLegacy))
						// It is not the last role or it is leged
						lInstancesAux = ONContext.GetComponent_Data(InhGetTargetClassName(lPath), OnContext).ExecuteQuery(linkedToOptimized, null, null, comparer, startRowOid, blockSize);
					else
						// It is the last role and it is local
						lInstancesAux = ONContext.GetComponent_Data(InhGetTargetClassName(lPath), OnContext).ExecuteQuery(linkedToOptimized, null, displaySet, comparer, startRowOid, blockSize);
					#endregion

					#region Rest of the path
					lInstancesAux = lInstancesAux[lInversePath] as ONCollection;
					#endregion

					if (lInstances != null)
						lInstances.Intersection(lInstancesAux);
					else
						lInstances = lInstancesAux;
				}
			}
			#endregion
			
			return lInstances;
		}
			catch (Exception e)
			{
				string ltraceItem = "Method: SolveQuery, Component: ONDBData";
      			if (e is ONSystemException)
      			{
      				ONSystemException lException = e as ONSystemException;
            		lException.addTraceInformation(ltraceItem);
            		throw lException;
				}
      			throw new ONSystemException(e, ltraceItem);
      		}
		}
		/// <summary>
		/// Retrieve all the instances of a determinate class that fulfil a determinate formula of searching
		/// </summary>
		/// <param name="linkedTo">List to reach the class to retrieve the related instance</param>
		/// <param name="filters">Formula to search concrete instances</param>
		/// <param name="comparer">Order Criteria that must be followed by the query</param>
		/// <param name="startRowOid">OID frontier</param>
		/// <param name="blockSize">Number of instances to be returned</param>
		/// <returns>Instances that check the filter list</returns>
		protected virtual ONCollection SolveQuery(ONLinkedToList linkedTo, ONFilterList onFilterList, ONDisplaySet displaySet, ONOrderCriteria comparer, ONOid startRowOid, int blockSize)
        {
	        ONSqlSelect lOnSql = new ONSqlSelect();
		try
		{
			// Create select and first table
			InhRetrieveInstances(lOnSql, displaySet, null, OnContext);

	        	// Fix related instance
	        	if (!AddLinkedTo(lOnSql, linkedTo))
	        	return ONContext.GetComponent_Collection(ClassName, OnContext);

			// Add filter formula
			if (onFilterList != null)
				onFilterList.FilterInData(lOnSql, this);

			// Retrieve query instance number
			int lTotalNumInstances = -1;
			if (OnContext.CalculateQueryInstancesNumber)
			{
				if ((onFilterList == null) || (!onFilterList.InMemory))
				{
					ArrayList lSqlParameters;
					string lNumInstancesSqlSentence = ONSqlSelect.GenerateSQLAsCount(lOnSql, out lSqlParameters);
					lTotalNumInstances = Convert.ToInt32(ExecuteScalar(lNumInstancesSqlSentence, lSqlParameters));
				}
				OnContext.CalculateQueryInstancesNumber = false;
			}

			// OrderCriteria
			AddOrderCriteria(lOnSql, comparer, startRowOid, blockSize);

			// Execute
			ONCollection lONCollection = ExecuteSql(lOnSql, onFilterList, displaySet, comparer, startRowOid, blockSize);
	
			// Set Query instance number
			if (lTotalNumInstances > -1)
				lONCollection.totalNumInstances = lTotalNumInstances;
		
			return lONCollection;
		}
		catch (Exception e)
		{
			string ltraceItem = "Method: SolveQuery, Component: ONDBData";
			if (e is ONSystemException)
      			{
				ONSystemException lException = e as ONSystemException;
				lException.addTraceInformation(ltraceItem);
				throw lException;
			}
      			throw new ONSystemException(e, ltraceItem);
      		}
        }
		#endregion

		#region OrderCriteria
		/// <summary> This method adds the order criteria to the SQL statement </summary>
		/// <param name="onSql"> This parameter represents the SQL component </param>
		/// <param name="comparer"> This parameter has all the information refering to the order criteria to add to SQL statement</param>
		/// <param name="startRowOid">This parameter has the OID necessary to start the search</param>
		/// <param name="blockSize">This parameter represents the number of instances to be returned</param>
		public void AddOrderCriteria(ONSqlSelect onSql, ONOrderCriteria comparer, ONOid startRowOid, int blockSize)
		{
			AddOrderCriteria(onSql, comparer, startRowOid, blockSize, null);
		}
		/// <summary> This method adds the order criteria to the SQL statement </summary>
		/// <param name="onSql"> This parameter represents the SQL component </param>
		/// <param name="startRowOid">This parameter has the OID necessary to start the search</param>
		/// <param name="blockSize">This parameter represents the number of instances to be returned</param>
		/// <param name="initialPath"> This parameter has the path of the instances reached in a For All</param>
		public void AddOrderCriteria(ONSqlSelect onSql, ONOid startRowOid, int blockSize, ONPath initialPath)
		{
			AddOrderCriteria(onSql, null, startRowOid, blockSize, initialPath);
		}
		/// <summary> This method adds the order criteria to the SQL statement </summary>
		/// <param name="onSql"> This parameter represents the SQL component </param>
		/// <param name="comparer"> This parameter has all the information refering to the order criteria to add to SQL statement</param>
		/// <param name="startRowOid">This parameter has the OID necessary to start the search</param>
		/// <param name="blockSize">This parameter represents the number of instances to be returned</param>
		/// <param name="initialPath"> This parameter has the path of the instances reached in a For All</param>
		protected virtual void AddOrderCriteria(ONSqlSelect onSql, ONOrderCriteria comparer, ONOid startRowOid, int blockSize, ONPath initialPath)
		{
		}
		#endregion

		#region Services
		public override void UpdateAdded(ONInstance instance)
		{
		}
		public override void UpdateDeleted(ONInstance instance)
		{
		}
		public override void UpdateEdited(ONInstance instance)
		{
		}
		#endregion

		#region Path Management
		public virtual string InhAddPath(ONSqlSelect onSql, string facet, ONPath onPath, string initialClass)
		{
			return "";
		}
		public virtual string InhAddPath(ONSqlSelect onSql, JoinType joinType, string facet, ONPath onPath, string initialClass)
		{
			return "";
		}
		public virtual string InhAddPath(ONSqlSelect onSql, string facet, ONPath onPath, string initialClass, bool isLinkedTo)
		{
			return "";
		}
		public virtual void InhFixInstance(ONSqlSelect onSql, ONPath onPath, ONPath processedOnPath, ONOid oid)
		{
		}
		public virtual void InhFixInstance(ONSqlSelect onSql, ONPath onPath, ONPath processedOnPath, ONOid oid, bool isLinkedTo)
		{
		}
		public virtual string InhRetrieveInstances(ONSqlSelect onSql, ONDisplaySet displaySet, ONPath onPath, ONContext onContext)
		{
			return "";
		}
		#endregion

		#region LoadRelated
		public void LoadRelated(ONContext onContext, ONDisplaySet displaySet, object[] columns, int index, ONInstance instance)
		{
			// Related attributes
			for (; index < displaySet.ElementsInData; index++)
			{
				ONDisplaySetItem lDSItem = displaySet[index];
				string lTypeName = ONInstance.GetTypeOfAttribute(instance.GetType(), new ONPath(lDSItem.Path));

				instance.RelatedValues.Add(lDSItem.Path, ONDBData.GetBasicTypeFromDB(lTypeName, columns[index]));
			}
		}
		#endregion LoadRelated
		
		#region TypeFromDB
		public static ONSimpleType GetBasicTypeFromDB(string type, object value)
		{
			switch(type.ToLower())
			{
				case "autonumeric":
				case "int":
					if(value is DBNull)
						return ONInt.Null;
					else
						return new ONInt(Convert.ToInt32(value));
				case "blob":
					if (value is DBNull)
						return ONBlob.Null;
					else
						return new ONBlob(value as byte[]);
				case "bool":
					if (value is DBNull)
						return ONBool.Null;
					else
						return new ONBool(Convert.ToBoolean(value));
				case "date":
					if (value is DBNull)
						return ONDate.Null;
					else
						return new ONDate(Convert.ToDateTime(value));
				case "datetime":
					if (value is DBNull)
						return ONDateTime.Null;
					else
						return new ONDateTime(Convert.ToDateTime(value));
				case "nat":
					if (value is DBNull)
						return ONNat.Null;
					else
						return new ONNat(Convert.ToInt32(value));
				case "real":
					if (value is DBNull)
						return ONReal.Null;
					else
						return new ONReal(Convert.ToDecimal(value));
				case "string":
					if (value is DBNull)
						return ONString.Null;
					else
						return new ONString(((string) value).TrimEnd());
				case "text":
					if (value is DBNull)
						return ONText.Null;
					else
						return new ONText((string) value);
				case "time":
					if (value is DBNull)
						return ONTime.Null;
					else
						return new ONTime(Convert.ToDateTime(value));
				default:
					return value as ONSimpleType;
			}

		}
		#endregion TypeFromDB
		
		#region Execute
		/// <summary>
		/// Executes the sentence SQL fixing the parameters needed.
		/// </summary>
		/// <param name="onSql">Sentence SQL to be executed</param>
		public object Execute(ONSql onSql)
		{
			ArrayList lSqlParameters;
			string lSql = onSql.GenerateSQL(out lSqlParameters);

			if (onSql is ONSqlScalar)
				return ExecuteScalar(lSql, lSqlParameters);
			else if (onSql is ONSqlSelect)
				return ExecuteReader(lSql, lSqlParameters);
			else if (onSql is ONSqlInsertAutoInc)
				return ExecuteInsertAutoIncScalar(lSql, lSqlParameters);
			else
				ExecuteNonQuery(lSql, lSqlParameters);

            return null;
		}
		/// <summary>
		/// Execute a SQL Sentence in Scalar Mode with th sentence given in a string
		/// </summary>
		public object ExecuteScalar(string sql)
		{
			return ExecuteScalar(sql, null);
		}
		public object ExecuteScalar(string sql, ArrayList parameters)
		{
            if (parameters != null)
            {
			    // Replacement of ? in the parametrized querys
			    for (int i = 0; i < parameters.Count; )
			    {
			    	sql = ONSql.Replace(sql, i);
			    	i++;
		    	}
            }

            CreateCommand(sql, parameters);
			
			try
			{
				object lRet = mSqlCommand.ExecuteScalar();
				Close();
				return lRet;
			}
			catch
			{
				Close();
				throw;
			}
		}
		public void ExecuteNonQuery(string sql)
		{
			ExecuteNonQuery(sql, null);
		}
		public void ExecuteNonQuery(string sql, ArrayList parameters)
		{
            if (parameters != null)
            {
                // Replacement of ? in the parametrized querys
			    for (int i = 0; i < parameters.Count; )
			    {
			    	sql = ONSql.Replace(sql, i);
			    	i++;
			    }
            }

			CreateCommand(sql, parameters);
			
			try
			{
				mSqlCommand.ExecuteNonQuery();
				Close();
			}
			catch
			{
				Close();
				throw;
			}
		}
		public object ExecuteReader(string sql)
		{
			return ExecuteReader(sql, null);
		}
		public object ExecuteReader(string sql, ArrayList parameters)
		{
            if (parameters != null)
            {
                // Replacement of ? in the parametrized querys
			    for (int i = 0; i < parameters.Count; )
			    {
				    sql = ONSql.Replace(sql, i);
				    i++;
			    }
            }

			CreateCommand(sql, parameters);
			
			try
			{
				object lRet = mSqlCommand.ExecuteReader();
				Close();
				return lRet;
			}
			catch
			{
				Close();
				throw;
			}
		}
		/// <summary>
		/// Execute a Insert SQL Sentence with an autoincremental field and returns the added value
		/// </summary>
		public object ExecuteInsertAutoIncScalar(string sql)
		{
			return ExecuteInsertAutoIncScalar(sql, null);
		}
		public object ExecuteInsertAutoIncScalar(string sql, ArrayList parameters)
		{
			return ExecuteScalar(sql, parameters);
		}
		#endregion Execute

		#region Close / GetConnection / CloseConnection
		private void CreateCommand(string sql, ArrayList parameters)
		{

			// Open Connection
			if (mSqlConnection.State == ConnectionState.Closed)
			{
				mSqlConnection.Open();
				if (OnContext is ONServiceContext) // Services
				{
					ONSQLCommand lSqlCommand = new ONSQLCommand("SET TRANSACTION ISOLATION LEVEL READ COMMITTED", mSqlConnection);
					lSqlCommand.ExecuteNonQuery();
					lSqlCommand.Dispose();
					lSqlCommand = null;
				}
				else // Queries
				{
					ONSQLCommand lSqlCommand = new ONSQLCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED", mSqlConnection);
					lSqlCommand.ExecuteNonQuery();
					lSqlCommand.Dispose();
					lSqlCommand = null;
				}
			}

			// Create Command
			mSqlCommand = new ONSQLCommand(sql, mSqlConnection);

			// Parameters
			if (parameters != null)
			{
				string lName = "";
				int lIndex = 0;
				foreach(ONSimpleType lParameterItem in parameters)
				{
					lName = "@" + lIndex;
					if (lParameterItem.Value == null)
					{
						ONSQLParameter lParameter = mSqlCommand.Parameters.Add(lName, lParameterItem.SQLType);
						lParameter.IsNullable = true;
						lParameter.Value = DBNull.Value;
					}
					else
					{
						(mSqlCommand as ONSQLCommand).Parameters.Add(lName, lParameterItem.SQLType).Value = lParameterItem.Value;
					}
                    lIndex++;
				}
			}
		}
		/// <summary>
		/// Close all the elements refering with the Data Base: connections, readers...
		/// </summary>
		public void Close()
		{
			// Delete SQLCommand
			if (mSqlCommand != null)
			{
				mSqlCommand.Dispose();
				mSqlCommand = null;
			}
		}
		/// <summary>
		/// Starts a new connection with the Data Base
		/// </summary>
		public static ONSQLConnection GetConnection()
		{
			return (new ONSQLConnection(CtesBD.ConnectionString));
		}
		/// <summary>
		/// Close the connection with the Data Base
		/// </summary>
		/// <param name="connection">Connection to be closed</param>
		public static void CloseConnection(object connection)
		{
			ONSQLConnection lConnection = connection as ONSQLConnection;

			if (lConnection != null)
			{
				lConnection.Close();
				lConnection.Dispose();
				lConnection = null;
			}
		}
		#endregion
	}
}

