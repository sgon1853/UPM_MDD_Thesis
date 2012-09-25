// 3.4.4.5

using System;
using System.Collections.Generic;
using SIGEM.Business.SQL;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.Instance;
using SIGEM.Business.Data;
using SIGEM.Business.Collection;

namespace SIGEM.Business.Query
{
    internal class QueryByRelatedFilter : ONFilter
    {
        #region Members
        private ONPath ONPath;
        private ONOid mRelatedOid;
        #endregion

        #region Constructors
        public QueryByRelatedFilter(string className, ONPath onPath, ONOid relatedOid)
            : base(className, "QueryByRelated")
        {
              ONPath = onPath;
              mRelatedOid = relatedOid;

              Type lDataType = ONContext.GetType_Data(ClassName);
              InMemory = false;
              InData = !lDataType.IsSubclassOf(typeof(ONLVData));
              InLegacy = lDataType.IsSubclassOf(typeof(ONLVData));
        }
        #endregion Constructors

        #region Filter
        public override bool FilterInMemory(ONInstance instance)
        {
        	return(true);
        }
        public override void FilterInData(ONSqlSelect onSql, ONDBData data)
        {
        	if (InLegacy)
			return;

	       	data.InhAddPath(onSql, mRelatedOid.ClassName, ONPath, "");
        	
        	//Fix Instance
	      	ONDBData lData = ONContext.GetComponent_Data(mRelatedOid.ClassName, data.OnContext) as ONDBData;
	       	lData.InhFixInstance(onSql, null, ONPath, mRelatedOid);
        }
        /// <summary>
        /// This method recovers all the population of the database
        /// </summary>
        /// <param name="onContext">Recovers the context of the execution of the service</param>
        /// <param name="linkedTo">List to reach the class to retrieve the related instance</param>
        /// <param name="comparer">Order Criteria that must be followed</param>
        /// <param name="${startRowOid}">OID frontier</param>
        /// <param name="blockSize">Number of instances to be returned</param>
        /// <returns>The population</returns>
        public override ONCollection FilterInLegacy(ONContext onContext, ONLinkedToList linkedTo, ONOrderCriteria comparer, ONOid startRowOID, int blockSize)
        {
        	if (InData)
        		return null;
        	ONLinkedToList lLinkedToList = new ONLinkedToList();
        	// Add linkedToList to the new list
        	foreach (KeyValuePair<ONPath, ONOid> lDictionaryEntry in linkedTo)
        		lLinkedToList.mLinkedToList.Add(lDictionaryEntry.Key, lDictionaryEntry.Value);
        	// Add relatedOid to the new list
        	if (mRelatedOid != null)
        		lLinkedToList.mLinkedToList.Add(ONPath, mRelatedOid);
        	// Add parameters
        	object[] lParameters = new object[5];
        	lParameters[0] = onContext;
        	lParameters[1] = linkedTo;
        	lParameters[2] = comparer;
        	lParameters[3] = startRowOID;
        	lParameters[4] = blockSize;
        	
        	return ONContext.InvoqueMethod(ONContext.GetType_LV(ClassName), "QueryByRelated", lParameters) as ONCollection;
		}
        #endregion Filters
    }
}
