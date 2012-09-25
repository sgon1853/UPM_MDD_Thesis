// 3.4.4.5

using System;
using System.Collections;
using SIGEM.Business.Instance;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Collection;
using SIGEM.Business;

namespace SIGEM.Business.Query
{
	/// <summary>
	/// Super class of the filters defined in the model
	/// </summary>
	internal abstract partial class ONFilter
	{
        #region Members
        public bool InLegacy = false; 
        #endregion

		#region Filter
		public virtual ONCollection FilterInLegacy(ONContext onContext, ONLinkedToList lLinkedToList, ONOrderCriteria comparer, ONOid startRowOid, int blockSize)
		{
			return null;
		}
		#endregion Filter
	}
}

