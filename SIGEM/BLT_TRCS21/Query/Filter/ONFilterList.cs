// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Generic;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Query
{
	/// <summary>
	/// List of filters
	/// </summary>
	internal partial class ONFilterList : Dictionary<string, ONFilter>
	{
		#region Members
		/// <summary>
		/// Returns if some filter has some memory check
		/// </summary>
		public bool InMemory
		{
			get
			{
				foreach (ONFilter lOnFilter in Values)
					if (lOnFilter.InMemory)
						return true;

				return false;
			}
		}

		public bool PreloadRelatedAttributes
		{
			get
			{
				foreach (ONFilter lOnFilter in Values)
					if (!lOnFilter.PreloadRelatedAttributes)
						return false;

				return true;
			}
		}
		#endregion Members

		#region Constructors
		/// <summary>
		/// Create a new filter list
		/// </summary>
		public ONFilterList()
		{
		}
		/// <summary>
		/// Create a new filter list with initialization
		/// </summary>
		/// <param name="onFilterList">Items to copy</param>
		public ONFilterList(ONFilterList onFilterList)
			: base(onFilterList)
		{
		}
		#endregion Constructors

		#region Filter
		/// <summary>
		/// Makes the memory checks
		/// </summary>
		/// <param name="instances">Instance to check</param>
		/// <returns>if the instance check all the filters</returns>
		public bool FilterInMemory(ONInstance instances)
		{
			foreach (ONFilter lOnFilter in Values)
				if (!lOnFilter.FilterInMemory(instances))
					return false;

			return true;
		}
		#endregion Filter
		
		public new void Add(string key, ONFilter value)
		{
			if (value.InData || value.InLegacy || value.InMemory)
				base.Add(key, value);
		}
	}
}

