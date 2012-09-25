// 3.4.4.5

using System;
using System.Xml;
using System.Collections;
using System.Reflection;
using SIGEM.Business;
using SIGEM.Business.Attributes;
using SIGEM.Business.OID;
using SIGEM.Business.Instance;
using SIGEM.Business.Collection;
using SIGEM.Business.Types;

namespace SIGEM.Business.Query
{
	/// <summary>
	/// Superclass of Querys
	/// </summary>
	internal abstract class ONQuery : IDisposable
	{
		#region Members
		public ONContext OnContext;
		public string ClassName;
		public bool IsLegacyView;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="onContext">Context with all the information about the execution of the request</param>
		/// <param name="className">Name of the class that represents the instance</param>
		public ONQuery(ONContext onContext, string className, bool isLegacyView)
		{
			if (onContext != null)
				OnContext = onContext;
			else
				OnContext = new ONContext();

			ClassName = className;
			IsLegacyView = isLegacyView;
		}
		#endregion

		#region Query methods
		public ONCollection QueryByOid(ONOid oid, ONDisplaySet displaySet)
        {
            //Create filters
            ONFilterList lOnFilterList = new ONFilterList();
            lOnFilterList.Add("QueryByOid", new QueryByOidFilter(oid));

			return QueryByFilter(null, lOnFilterList, displaySet, null, null, 0);
        }

		public ONCollection QueryByRelated(ONLinkedToList linkedTo, ONDisplaySet displaySet, string orderCriteria, ONOid startRowOID, int blockSize)
        {
			return QueryByFilter(linkedTo, null, displaySet, orderCriteria, startRowOID, blockSize);
        }

		public abstract ONCollection QueryByFilter(ONLinkedToList linkedTo, ONFilterList filters, ONDisplaySet displaySet, string orderCriteria, ONOid startRowOID, int blockSize);
		
		public ONCollection GetPopulation(ONDisplaySet displaySet, string orderCriteria, ONOid startRowOID, int blockSize)

        {
			return QueryByFilter(null, null, displaySet, orderCriteria, startRowOID, blockSize);
        }
        public ONOrderCriteria GetOrderCriteria(string orderCriteria)
        {
            ONOrderCriteria lComparer = ONContext.GetComponent_OrderCriteria(ClassName, orderCriteria);
            if ((lComparer != null) && (!lComparer.IsVisible(OnContext)))
                lComparer = null;
            return lComparer;
        }
		#endregion

		#region Indexer
		public object this [string onPath]
		{
			get
			{
				return this[new ONPath(onPath)];
			}
		}
		public object this [ONPath onPath]
		{
			get
			{
				if ((onPath == null) || (onPath.Count == 0))
					return this;

				string lRol = onPath.RemoveHead();
				PropertyInfo lProperty = null;

				// Last unique role (like attributes)
				if (onPath.Count == 0)
				{
					lProperty = ONContext.GetPropertyInfoWithAttribute(GetType(), typeof(ONAttributeAttribute), "<Attribute>" + lRol + "</Attribute>");
					if (lProperty != null)
						return (lProperty.GetValue(this, null));
				}

				// Roles
				lProperty = ONContext.GetPropertyInfoWithAttribute(GetType(), typeof(ONRoleAttribute), "<Role>" + lRol + "</Role>");
				if (lProperty != null)
				{
					if (onPath.Count == 0)
						return (lProperty.GetValue(this, null));
					else
						return (lProperty.GetValue(this, null) as ONQuery)[onPath];
				}

				return null;
			}
		}
		#endregion

	 	#region IDisposable Methods
		/// <summary>
		/// Destroy all the resources that are associated with the object
		/// </summary>
		public void Dispose()
		{
			if (OnContext != null)
			{
				OnContext.Dispose();
				OnContext = null;
			}
		}
		#endregion
	}
}

