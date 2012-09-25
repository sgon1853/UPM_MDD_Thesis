// 3.4.4.5
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using SIGEM.Business.Query;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Query
{
	public class ONDisplaySet: List<ONDisplaySetItem>
	{
		#region Members
		public string ClassName;
		private int mElementsInData = 0;
		#endregion Members

		#region Properties
		public int ElementsInData
		{
			get
			{
				if (mElementsInData == 0)
				{
					foreach (ONDisplaySetItem lDisplaySetItem in this)
						if (lDisplaySetItem.InData)
							mElementsInData++;
				}
				return mElementsInData;
			}
		}
		public bool InMemory
		{
			get
			{
				foreach (ONDisplaySetItem lDisplaySetItem in this)
					if (lDisplaySetItem.InMemory)
						return true;

				return false;
			}
		}
		public bool InData
		{
			get
			{
				foreach (ONDisplaySetItem lDisplaySetItem in this)
					if (lDisplaySetItem.InData)
						return true;

				return false;
			}
		}
		public bool InLegacy
		{
			get
			{
				foreach (ONDisplaySetItem lDisplaySetItem in this)
					if (lDisplaySetItem.InLegacy)
						return true;

				return false;
			}
		}
		#endregion Properties

		#region Contains
		public bool Contains(string displaySetItem)
		{
			foreach (ONDisplaySetItem lDisplaySetItem in this)
				if (string.Compare(lDisplaySetItem.Path, displaySetItem, true) == 0)
					return true;

			return false;
		}
		#endregion Contains

		#region Constructors
		public ONDisplaySet(string className, string displaySet, StringCollection activeAgentFacets)
			: base()
		{
			ClassName = className;
			string lDisplaySet = displaySet.Replace(" ", "");

			if (lDisplaySet.Length > 0)
			{
				string[] lDisplaySetArray = lDisplaySet.Split(',');

				foreach (string lItem in lDisplaySetArray)
					Add(new ONDisplaySetItem(ClassName, lItem, activeAgentFacets));
			}
		}
		public ONDisplaySet(ONDisplaySet displaySet)
			: base()
		{
			ClassName = displaySet.ClassName;

			foreach (ONDisplaySetItem lDisplaySetItem in displaySet)
				Add(new ONDisplaySetItem(lDisplaySetItem));
		}
		#endregion Constructors
		
		#region Indexer
		public ONDisplaySetItem this[string path]
		{
		    get
		    {
		        foreach (ONDisplaySetItem lDisplaySetItem in this)
		            if (string.Compare(lDisplaySetItem.Path, path, true) == 0)
		                return lDisplaySetItem;
		        
		        return null;
		    }
		}
		#endregion
		
		#region Remove
		public void Remove(string path)
		{
		    this.Remove(this[path]);
		}
		#endregion		
	}
}
