// 3.4.4.5
using System;
using System.Data;
using System.Collections;
using ONSQLDataReader = System.Data.SqlClient.SqlDataReader;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Instance;
using SIGEM.Business.Query;
using SIGEM.Business.Attributes;
using SIGEM.Business.SQL;
using SIGEM.Business.Exceptions;
using SIGEM.Business.Action;
using SIGEM.Business.Collection;
using SIGEM.Business.Others.Enumerations;

namespace SIGEM.Business.Data
{
	[ONDataClass("")]
	internal class GlobalTransactionData : ONDBData
	{
		#region Constructors
		/// <summary>Constructor</summary>
		/// <param name="onContext">This parameter has the current context</param>
		public GlobalTransactionData(ONContext onContext) : base(onContext, "")
		{
		}
		#endregion Constructors

		#region Sql Management
		/// <summary>This method adds to the SQL statement any path that appears in a formula</summary>
		/// <param name="onSql">This parameter has the current SQL statement</param>
		/// <param name="facet">First class, the beginning of the path</param>
		/// <param name="onPath">Path to add to SQL statement</param>
		/// <param name="processedOnPath">Path pocessed until the call of this method</param>
		/// <param name="initialClass">Domain of the object valued argument, object valued filter variables or AGENT when it should be necessary</param>
		public static string AddPath(ONSqlSelect onSql, string facet, ONPath onPath, ONPath processedOnPath, string initialClass)
		{
			return AddPath(onSql, JoinType.InnerJoin, facet, onPath, processedOnPath, initialClass, false);
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
			return AddPath(onSql, JoinType.InnerJoin, facet, onPath, processedOnPath, initialClass, forceLastAlias);
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
			// initialClass is used for Object-valued arguments, object-valued filter variables, agent instance, ...
			ONPath lProcessedOnPath = new ONPath(processedOnPath);
			ONPath lOnPath = new ONPath(onPath);
			
			// Calculate processed path
			string lRole = lOnPath.RemoveHead() as string;
			lProcessedOnPath += lRole;
			
			// Search Path
			if (lOnPath.Count == 0)
			{
				string lAlias = onSql.GetAlias(facet, lProcessedOnPath);
				if ((lAlias != "") && (!forceLastAlias))
					return (lAlias);
			}
 			// Create path
			if (initialClass == "") // Simple paths
			{
				if (string.Compare(lRole, "NaveNodriza", true) == 0)
					return NaveNodrizaData.AddPath(onSql, joinType, facet, lOnPath, lProcessedOnPath, "", forceLastAlias);
				if (string.Compare(lRole, "Aeronave", true) == 0)
					return AeronaveData.AddPath(onSql, joinType, facet, lOnPath, lProcessedOnPath, "", forceLastAlias);
				if (string.Compare(lRole, "Pasajero", true) == 0)
					return PasajeroData.AddPath(onSql, joinType, facet, lOnPath, lProcessedOnPath, "", forceLastAlias);
				if (string.Compare(lRole, "PasajeroAeronave", true) == 0)
					return PasajeroAeronaveData.AddPath(onSql, joinType, facet, lOnPath, lProcessedOnPath, "", forceLastAlias);
				if (string.Compare(lRole, "Revision", true) == 0)
					return RevisionData.AddPath(onSql, joinType, facet, lOnPath, lProcessedOnPath, "", forceLastAlias);
				if (string.Compare(lRole, "RevisionPasajero", true) == 0)
					return RevisionPasajeroData.AddPath(onSql, joinType, facet, lOnPath, lProcessedOnPath, "", forceLastAlias);
				if (string.Compare(lRole, "Administrador", true) == 0)
					return AdministradorData.AddPath(onSql, joinType, facet, lOnPath, lProcessedOnPath, "", forceLastAlias);
			}
			
			// Solve path with initialPath
			object[] lParameters = new object[6];
			lParameters[0] = onSql;
			lParameters[1] = facet;
			lParameters[2] = lOnPath;
			lParameters[3] = lProcessedOnPath;
			lParameters[4] = "";
			lParameters[5] = forceLastAlias;

			return ONContext.InvoqueMethod(ONContext.GetType_Data(initialClass), "AddPath", lParameters) as string;
		}
		#endregion Sql Management


		#region Association Operators
		#endregion Association Operators
	}
}
