// 3.4.4.5
using System;
using System.Collections;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Instance;
using SIGEM.Business.Collection;
using SIGEM.Business.Data;
using SIGEM.Business.Attributes;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business.Query
{
	[ONQueryClassAttribute("Revision")]
	internal class RevisionQuery : ONQuery
	{
		#region Members
		public RevisionPasajeroQuery RevisionPasajeroRoleTemp;
		#endregion Members
		
		#region Constructors
		/// <summary>Constructor</summary>
		/// <param name="onContext">This parameter has the current context</param>
		public RevisionQuery(ONContext onContext) : base(onContext, "Revision", false)
		{
		}
		#endregion Constructors
	
		#region QueryByFilter
		/// <summary>Solves the filters defined in this class</summary>
		/// <param name="linkedTo">This parameter has the related instance to retrieve the requested instances</param>
		/// <param name="filters">This parameter has all the filters defined with this class</param>
		/// <param name="orderCriteria">This parameter has the name of the order criteria to add to SQL statement</param>
		/// <param name="startRowOID">This parameter has the OID necessary to start the search</param>
		/// <param name="blockSize">This parameter represents the number of instances to be returned</param>
		public override ONCollection QueryByFilter(ONLinkedToList linkedTo, ONFilterList filters, ONDisplaySet displaySet, string orderCriteria, ONOid startRowOID, int blockSize)
		{
			// OrderCriteria
			ONOrderCriteria lComparer = GetOrderCriteria(orderCriteria);

			// Horizontal visibility
			if (filters == null)
				filters = new ONFilterList();
			filters.Add("HorizontalVisibility", new RevisionHorizontalVisibility());
			
			// Linked To List
			if (linkedTo == null)
				linkedTo = new ONLinkedToList();

			// Call Data
			try
			{
				RevisionData lData = new RevisionData(OnContext);
				ONCollection lCollection = lData.ExecuteQuery(linkedTo, filters, displaySet, lComparer, startRowOID, blockSize);
			
				// OrderCriteria
				if (lComparer != null)
					lCollection.Sort(lComparer);
			
				return lCollection;
			}
			catch (Exception e)
			{
				if (e is ONException)
				{
					throw e;
				}
				else
				{
					string ltraceItem = "Error in query, Method: ExecuteQuery, Component: RevisionQuery";
      					if (e is ONSystemException)
      					{
      						ONSystemException lException = e as ONSystemException;
				            		lException.addTraceInformation(ltraceItem);
            						throw lException;
					}
      					throw new ONSystemException(e, ltraceItem);
      				}
      			}
		}
		#endregion QueryByFilter
	}
}
