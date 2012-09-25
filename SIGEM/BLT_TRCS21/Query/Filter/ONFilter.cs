// 3.4.4.5

using System;
using System.Collections;
using SIGEM.Business.Instance;
using SIGEM.Business.OID;

namespace SIGEM.Business.Query
{
	/// <summary>
	/// Super class of the filters defined in the model
	/// </summary>
	internal abstract partial class ONFilter
	{
		#region Members
		/// <summary>
		/// Name of the class of the filter
		/// </summary>
		public string ClassName;
		/// <summary>
		/// Name of the filter
		/// </summary>
		public string FilterName;
		/// <summary>
		/// Returns if the filter has some memory check
		/// </summary>
		public bool InMemory = false;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="className">Name of the class of the filter</param>
		/// <param name="filterName">Name of the filter</param>
		public ONFilter(string className, string filterName)
		{
			ClassName = className;
			FilterName = filterName;
		}
		#endregion Constructors

		#region Filter
		/// <summary>
		/// Makes the memory checks
		/// </summary>
		/// <param name="instance">Instance to check</param>
		/// <returns>if the instance it check the filter</returns>
		public virtual bool FilterInMemory(ONInstance instance)
		{
			return true;
		}
		#endregion Filter
		
		#region IsUnableToFilterInData
		/// <summary>
		/// Checks if the horizontal visibility filter must be executed in memory.
		/// In case there are more than one active facet for the connected agent with at least
		/// one not optimizable conjunction, the hole filter must be executed in memory.
		/// </summary>
		protected virtual bool IsUnableToFilterInData(ONContext onContext)
		{
			return false;
		}
		#endregion IsUnableToFilterInData			
		
		#region CheckFilterVariables
		/// <summary>
		/// Check Horizontal Visibility of the filter variables and arguments (in Navigational filters)
		/// </summary>
		/// <param name="onContext">Context to obtain the instance and connected agent</param>
		public virtual void CheckFilterVariables(ONContext onContext)
		{
		}
		#endregion CheckFilterVariables
	}
}

