// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIGEM.Client.Oids;

namespace SIGEM.Client.Logics
{
	public class DependencyRulesCache
	{
		List<KeyValuePair<Oid, DataTable>> mCacheAttributes = new List<KeyValuePair<Oid, DataTable>>();

		public DependencyRulesCache()
		{
		}

		public DataTable GetAttributesFromOVArgument(Oid oid, string displaySet)
		{
			if (oid == null)
			{
				return new DataTable();
			}

			KeyValuePair<Oid, DataTable> objectCache = new KeyValuePair<Oid, DataTable>(oid, oid.ExtraInfo);

			// Search in the cache list
			foreach (KeyValuePair<Oid, DataTable> cache in mCacheAttributes)
			{
				if ((cache.Key != null) && (cache.Key.Equals(oid)))
				{
					objectCache = cache;
					break;
				}
			}

			// If the instance is not in the cache, search and add it to the cache
			if ((objectCache.Key == null) || (objectCache.Value == null))
			{
				DataTable table = Logic.ExecuteQueryInstance(Logic.Agent, oid, displaySet);
				mCacheAttributes.Add(new KeyValuePair<Oid, DataTable>(oid, table));
				return table;
			}
			else
			{
				// Verify if all the attributes are in the datatable
				string[] displaySetElements = displaySet.Split(',');
				string newDisplaySet = "";
				foreach (string attribute in displaySetElements)
				{
					string atr = attribute.Trim();
					if (objectCache.Value.Columns[atr] == null)
					{
						if (newDisplaySet != "")
						{
							newDisplaySet += ",";
						}

						newDisplaySet += atr;
					}
				}

				// If any attirbute doesn't appear in the datatable, search them and merge datatables
				if (newDisplaySet != "")
				{
					DataTable table = Logic.ExecuteQueryInstance(Logic.Agent, oid, newDisplaySet);
					objectCache.Value.Merge(table);
				}
				return objectCache.Value;
			}
		}
	}
}

