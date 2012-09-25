// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Generic;
using SIGEM.Business;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Query
{
	/// <summary>
	/// ONOrderCriteria
	/// </summary>
    internal abstract class ONOrderCriteria : IComparer
	{
		#region Members
		public string OrderCriteriaName;
		public bool InMemory;
		public bool InDataIni;
		public bool InData;
		public bool InLegacy;

		public List<ONOrderCriteriaItem> OrderCriteriaSqlItem = new List<ONOrderCriteriaItem>();
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="orderCriteriaName">Name of the order criteria thar represents</param>
		public ONOrderCriteria(string orderCriteriaName)
		{
			OrderCriteriaName = orderCriteriaName;
			InMemory = false;
			InDataIni = false;
			InData = false;
			InLegacy = false;
		}
		#endregion

		#region AddAttribute
		/// <summary>
		/// Construction the part of SQL sentence that is Oder By 
		/// </summary>
		/// <param name="onPath">Role path in case that the attribute is from another class</param>
		/// <param name="facet">Name of the class that has the attribute to be fixed in SQL sentence</param>
		/// <param name="attribute">Name of the attribute that is needed to the sort</param>
		/// <param name="type">Direction of the order. Could be ascending o descending</param>
		public void AddSqlAttribute(ONPath onPath, string facet, string attribute, OrderByTypeEnumerator type)
		{
			AddSqlAttribute(onPath, facet, attribute, type, "");
		}

		public void AddSqlAttribute(ONPath onPath, string facet, string attribute, OrderByTypeEnumerator type, string domainArgument)
		{
			foreach (ONOrderCriteriaItem lOCItem in OrderCriteriaSqlItem)
				if ((lOCItem.OnPath == onPath) && (lOCItem.Attribute == attribute))
					return;

			OrderCriteriaSqlItem.Add(new ONOrderCriteriaItem(onPath, facet, attribute, type, domainArgument));
		}
		#endregion

		#region Operators
		public virtual int Compare(object x, object y)
		{
			return 0;
		}
		public virtual int CompareSql(object x, object y)
		{
			return 0;
		}
        public virtual int CompareUnion(object x, object y)
		{
			return 0;
		}
		#endregion

        #region Visibility
        public virtual bool IsVisible(ONContext context)
        {
            return true;
        }
        #endregion Visibility

	}
}

