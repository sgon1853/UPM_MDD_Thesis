// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Generic;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business;

namespace SIGEM.Business.Query
{
	/// <summary>
	/// List of filters
	/// </summary>
	internal partial class ONFilterList: Dictionary<string, ONFilter>
	{
        #region Properties
        public bool InLegacy
        {
            get
            {
                foreach (ONFilter lOnFilter in this.Values)
                    if (lOnFilter.InLegacy)
                        return true;

                return false;
            }
        }
        #endregion Properties

		#region Filter
		public void FilterInLegacy(ONContext onContext, ONLinkedToList lLinkedToList, ONOrderCriteria comparer, ONOid startRowOid, int blockSize)
		{
			foreach (ONFilter lOnFilter in Values)
				lOnFilter.FilterInLegacy(onContext, lLinkedToList, comparer, startRowOid, blockSize);
		}
		#endregion Filter
	}
}




