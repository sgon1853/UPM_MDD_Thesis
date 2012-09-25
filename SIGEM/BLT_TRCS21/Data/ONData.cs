// 3.4.4.5

using System;
using System.Collections;
using System.Windows.Forms;
using SIGEM.Business;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.Instance;
using SIGEM.Business.Collection;
using SIGEM.Business.Query;
using SIGEM.Business.SQL;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business.Data
{
	/// <summary>
	/// Superclass of Data
	/// </summary>
	internal abstract class ONData
	{
		#region Members
		public string ClassName;
		protected ONContext mOnContext;
		#endregion

        #region Properties
		/// <summary>
		/// This member returns the current Context
		/// </summary>
        public ONContext OnContext
        {
	        get
	        {
		        return mOnContext;
	        }
        }
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONData(ONContext onContext, string className)
		{
			ClassName = className;
			mOnContext = onContext;
		}
		#endregion

        #region Properties
		public virtual string TableName
		{
			get
			{
				return ClassName;
			}
		}
		#endregion

		#region Exist
		/// <summary>
		/// Checks if a determinate instance exists
		/// </summary>
		/// <param name="oid">OID to search the instance</param>
		/// <param name="onFilterList">Filters to theck</param>
		/// <returns>If exists</returns>
		public abstract bool Exist(ONOid oid, ONFilterList onFilterList);
		#endregion Exist

		#region Search
		/// <summary>
		/// Returns the instance with the Oid
		/// </summary>
		/// <param name="oid">OID to search the instance</param>
		/// <param name="onFilterList">Filters to theck</param>
		/// <returns>The instance searched</returns>
		public abstract ONInstance Search(ONOid oid, ONFilterList onFilterList, ONDisplaySet displaySet);
		#endregion Search

		#region Queries
		#region ExecuteQuery
		/// <summary>
		/// Retrieve all the instances of a determinate class that fulfil a determinate formula of searching
		/// </summary>
		/// <param name="linkedTo">List to reach the class to retrieve the related instance</param>
		/// <param name="filters">Formula to search concrete instances</param>
		/// <param name="comparer">Order Criteria that must be followed by the query</param>
		/// <param name="startRowOID">OID frontier</param>
		/// <param name="blockSize">Number of instances to be returned</param>
        public abstract ONCollection ExecuteQuery(ONLinkedToList linkedTo, ONFilterList filters, ONDisplaySet displaySet, ONOrderCriteria comparer, ONOid startRowOid, int blockSize);
        #endregion
		#endregion

		#region Services
		public abstract void UpdateAdded(ONInstance instance);
		public abstract void UpdateDeleted(ONInstance instance);
		public abstract void UpdateEdited(ONInstance instance);
		#endregion Services

		#region Path Management
        public virtual string InhGetTargetClassName(ONPath onPath)
        {
	        if ((onPath == null) || (onPath.Count == 0))
		        return ClassName;
	        else
		        return "";
        }
		#endregion Path Management

        #region Compare Oid
		/// <summary>
		/// Compare with oid fields
		/// </summary>
		/// <param name="instance1">Instance 1</param>
		/// <param name="instance2">Instance 2</param>
		/// <returns>0 if equals, -1 if instance1 is minor, 1 if instance2 is mayor</returns>
        public virtual int CompareUnionOID(ONInstance instance1, ONInstance instance2)
		{
			return 0;
		}
        #endregion Compare Oid
	}
}

