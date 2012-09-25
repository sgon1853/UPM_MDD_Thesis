// 3.4.4.5
using System;
using SIGEM.Business.SQL;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.Instance;
using SIGEM.Business.Data;
using SIGEM.Business.Collection;

namespace SIGEM.Business.Query
{
	internal class QueryByOidFilter : ONFilter
	{
		#region Members
		private ONOid mOid;
		#endregion
		
		#region Constructors
		public QueryByOidFilter(ONOid oid)
			: base (oid.ClassName, "QueryByOid")
		{
			mOid = oid;
			
			Type lDataType = ONContext.GetType_Data(ClassName);
              		InMemory = false;
			InData = !lDataType.IsSubclassOf(typeof(ONLVData));
			InLegacy = lDataType.IsSubclassOf(typeof(ONLVData));
		}
		#endregion
		
		#region Filter
		public override bool FilterInMemory(ONInstance instance)
		{
			return (true);
		}
		public override void FilterInData(ONSqlSelect onSql, ONDBData data)
		{
			if (InLegacy)
				return;
			
			//Fix Instance
			data.InhFixInstance(onSql, null, null, mOid);
		}
		
		/// <summary>
		/// This method recovers an instance with the OID
		/// </summary>
		/// <param name="onContext">Recovers the context of the execution of the service</param>
		/// <param name="linkedTo">List to reach the class to retrieve the related instance</param>
		/// <param name="comparer">Order Criteria that must be followed</param>
		/// <param name="${startRowOid}">OID frontier</param>
		/// <param name="blockSize">Number of instances to be returned</param>
		/// <returns>The instance</returns>
		public override ONCollection FilterInLegacy(ONContext onContext, ONLinkedToList linkedTo, ONOrderCriteria comparer, ONOid startRowOID, int blockSize)
		{
			if (InData)
				return null;
			
			// Add parameters
			object[] lParameters = new object[2];
			lParameters[0] = onContext;
			lParameters[1] = mOid;
			ONCollection lCollection = ONContext.GetComponent_Collection(mOid.ClassName, onContext);
			ONInstance lInstance = ONContext.InvoqueMethod(ONContext.GetType_LV(mOid.ClassName), "QueryByOid", lParameters) as ONInstance;
			if (lInstance != null)
				lCollection.Add(lInstance);
			return lCollection;
		}
		#endregion Filter
	}
}
