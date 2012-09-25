// 3.4.4.5

using System;
using System.Collections.Generic;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.Query;
using SIGEM.Business.Instance;
using SIGEM.Business.Collection;
using SIGEM.Business.SQL;

namespace SIGEM.Business.Data
{
	/// <summary>
	/// Superclass of Data
	/// </summary>
	internal abstract class ONLVData : ONDBData
	{
		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONLVData(ONContext onContext, string className)
			: base(onContext, className)
		{
		}
		#endregion

		#region Exist
		/// <summary>
		/// Checks if a determinate instance exists
		/// </summary>
		/// <param name="oid">OID to search the instance</param>
		/// <param name="horizontalVisibility">Controls if it is needed to check the horizontal visibility</param>
		public override bool Exist(ONOid oid, ONFilterList onFilterList)
		{
			ONFilterList lOnFilterList = null;
			if (onFilterList != null)
				lOnFilterList = new ONFilterList(onFilterList);
			else
				lOnFilterList = new ONFilterList();
			lOnFilterList.Add("QueryByOid", new QueryByOidFilter(oid));

			return (ExecuteQuery(new ONLinkedToList(), lOnFilterList, null, null, null, 0).Count > 0);
		}
				/// <summary>
		/// Searchs a determinate instance
		/// </summary>
		/// <param name="oid">OID to search the instance</param>
		/// <param name="onFilterList">Filter list</param>
		public override ONInstance Search(ONOid oid, ONFilterList onFilterList, ONDisplaySet displaySet)
		{
			ONFilterList lOnFilterList = null;
			if (onFilterList != null)
				lOnFilterList = new ONFilterList(onFilterList);
			else
				lOnFilterList = new ONFilterList();
			lOnFilterList.Add("QueryByOid", new QueryByOidFilter(oid));

			ONCollection lCollection = ExecuteQuery(new ONLinkedToList(), lOnFilterList, displaySet, null, null, 0);
			if (lCollection.Count > 0)
				return lCollection[0] as ONInstance;
			else
				return null;

		}
		/// <summary>
		/// Checks if the instance exists in local system.
		/// </summary>
		/// <param name="oid">OID to search the instance</param>
		public bool LocalExist(ONOid oid)
		{
			ONSqlSelect onSql = new ONSqlCount();

			// Create select and first table
			string lAlias = onSql.CreateAlias(ClassName, null, ClassName);
			onSql.AddSelect(lAlias);

			// Fix instance
			InhFixInstance(onSql, null, null, oid);

			// Execute
			return (Convert.ToInt32(Execute(onSql)) > 0);
		}

		public ONInstance LocalSearch(ONInstance instance)
		{
			ONSqlSelect onSql = new ONSqlSelect();

			// Create select and first table
			InhRetrieveInstances(onSql, null, null, OnContext);

			// Fix instance
			InhFixInstance(onSql, null, null, instance.Oid);

			// Execute
			ONCollection lCollection = ExecuteQuery(onSql, instance);

			if (lCollection.Count > 0)
				return lCollection[0];
			else
				return null;
		}
		#endregion

		#region ExecuteQuery
		/// <summary>
		/// Execution of a query
		/// </summary>
		/// <param name="onSql">Sentence SQL to be executed</param>
		/// <param name="comparer">Order Criteria that must be followed by the query</param>
		/// <param name="startRowOID">OID frontier</param>
		/// <param name="blockSize">Number of instances to be returned</param>
		public ONCollection ExecuteQuery(ONSql onSql, ONInstance instance)
		{
			return ExecuteQuery(onSql, null, null, null, 0, instance);
		}
		public virtual ONCollection ExecuteQuery(ONSql onSql, ONFilterList onFilterList, ONOrderCriteria comparer, ONOid startRowOID, int blockSize, ONInstance instance)
		{
			return null;
		}
		public override ONCollection ExecuteQuery(ONLinkedToList linkedTo, ONFilterList filters, ONDisplaySet displaySet, ONOrderCriteria comparer, ONOid startRowOid, int blockSize)
		{
			ONCollection lInstances = null;
			Type lTypeInstance = ONContext.GetType_Instance(ClassName);

			ONLinkedToList lLinkedToLegacy = new ONLinkedToList();
			ONLinkedToList lLinkedToLocal = new ONLinkedToList();

			if (linkedTo == null)
				linkedTo = new ONLinkedToList();

			if (filters == null)
				filters = new ONFilterList();
				
			#region Treatment of LinkedTo
			foreach (KeyValuePair<ONPath, ONOid> lDictionaryEntry in linkedTo)
			{
				ONPath lPath = lDictionaryEntry.Key as ONPath;
				ONOid lOid = lDictionaryEntry.Value as ONOid;

				if (ONInstance.IsLegacy(lTypeInstance, lPath))
					lLinkedToLegacy.mLinkedToList.Add(lPath, lOid);
				else
					lLinkedToLocal.mLinkedToList.Add(lPath, lOid);
			}
			#endregion Treatment of LinkedTo

			ONFilterList lFiltersToLegacy = new ONFilterList();
			ONFilterList lFiltersToLocal = new ONFilterList();

			#region Treatment of Filters
			ONFilterList lFiltersToMixed = new ONFilterList();
			foreach (ONFilter lFilter in filters.Values)
			{
				if (lFilter.InLegacy)
					lFiltersToLegacy.Add(lFilter.FilterName, lFilter);
				else
					lFiltersToLocal.Add(lFilter.FilterName, lFilter);
			}
			#endregion Treatment of Filters

			if ((lFiltersToLegacy.Count <= 1) && (lFiltersToLocal.Count == 0) && (lLinkedToLocal.mLinkedToList.Count == 0))
			{
				#region No leged filters
				if (lFiltersToLegacy.Count == 0)
				{
					QueryByRelatedFilter lFilter = new QueryByRelatedFilter(ClassName, null, null);
					lInstances = lFilter.FilterInLegacy(OnContext, lLinkedToLegacy, comparer, startRowOid, (blockSize == 0 ? 0 : blockSize + 1));
				}
				#endregion No leged filters

				#region Solve leged filters
				foreach (ONFilter lFilter in lFiltersToLegacy.Values)
				{
					ONCollection lInstancesAux = lFilter.FilterInLegacy(OnContext, lLinkedToLegacy, comparer, startRowOid, (blockSize == 0 ? 0 : blockSize + 1));

					if (lInstances == null)
						lInstances = lInstancesAux;
					else
						lInstances.Intersection(lInstancesAux);
				}
				#endregion Solve leged filters
				
				if ((lInstances.totalNumInstances == -1) && (lInstances.Count <= blockSize) && ((startRowOid == null).TypedValue))
					lInstances.totalNumInstances = lInstances.Count;				

			}
			else
			{

				#region Solve leged filters
				foreach (ONFilter lFilter in lFiltersToLegacy.Values)
				{
					ONCollection lInstancesAux = lFilter.FilterInLegacy(OnContext, lLinkedToLegacy, comparer, null, 0);

					if (lInstances == null)
						lInstances = lInstancesAux;
					else
						lInstances.Intersection(lInstancesAux);
				}
				#endregion Solve leged filters

				#region Solve local filters in data
				if ((lFiltersToLocal.InData) || (lLinkedToLocal.mLinkedToList.Count > 0))
				{
					ONCollection lInstancesAux = base.ExecuteQuery(lLinkedToLocal, lFiltersToLocal, displaySet, null, null, 0);

					if (lInstances == null)
						lInstances = lInstancesAux;
					else
						lInstances.Intersection(lInstancesAux);
				}
				#endregion Solve local filters in data

				#region No leged filters
				if (lFiltersToLegacy.Count == 0)
				{
					int maxNumberQueries = Convert.ToInt32(ONContext.GetType_LV(ClassName).GetField("maxNumberQueries").GetValue(null));

					if ((lInstances == null || lInstances.Count > maxNumberQueries) || (lLinkedToLegacy.mLinkedToList.Count > 0) || (comparer != null))
					{
						ONCollection lInstancesAux = lInstances;
						QueryByRelatedFilter lFilter = new QueryByRelatedFilter(ClassName, null, null);
						lInstances = lFilter.FilterInLegacy(OnContext, lLinkedToLegacy, comparer, null, 0);

						if (lInstancesAux != null)
							lInstances.Intersection(lInstancesAux);
					}
					else
					{
						ONCollection lInstancesAux = ONContext.GetComponent_Collection(ClassName, OnContext);
						foreach (object lobject in lInstances)
						{
							ONInstance lInstance = lobject as ONInstance;
							QueryByOidFilter lFilter = new QueryByOidFilter(lInstance.Oid);
							lInstancesAux.AddRange(lFilter.FilterInLegacy(OnContext, null, null, null, 0));
						}
						lInstances = lInstancesAux.Clone() as ONCollection;
					}
				} 
				#endregion No leged filters

				#region Solve local filters in memory
				if ((lInstances != null) && (lFiltersToLocal.InMemory))
				{
					ONCollection lInstancesAux = lInstances;
					lInstances = ONContext.GetComponent_Collection(ClassName, OnContext);

					foreach (ONInstance lInstance in lInstancesAux)
						if (lFiltersToLocal.FilterInMemory(lInstance))
							lInstances.Add(lInstance);
				}
				#endregion Solve local filters in memory
				
				lInstances.totalNumInstances = lInstances.Count;
			}

			return lInstances;
		}
		#endregion ExecuteQuery
	}
}
