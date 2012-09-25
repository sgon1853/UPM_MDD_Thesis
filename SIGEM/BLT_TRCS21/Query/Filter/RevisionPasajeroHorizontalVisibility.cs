// 3.4.4.5
using System;
using System.Collections.Specialized;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.Instance;
using SIGEM.Business.SQL;
using SIGEM.Business.Data;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business.Query
{
	///<summary>
	///This class adds to the SQL statement the formula of horizontal visibility
	///</summary>
	internal partial class RevisionPasajeroHorizontalVisibility : ONFilter
	{
		#region Members
		private bool mIsUnableToFilterInData = false;
		#endregion Members
		
		#region Constructors
		protected RevisionPasajeroHorizontalVisibility(string className, string filterName)
			:base (className, filterName)
		{
			InMemory = false;
			InData = false;
		}
		public RevisionPasajeroHorizontalVisibility()
			:this ("RevisionPasajero", "HorizontalVisibility")
		{
		}
		#endregion Constructors
		
		#region Filter In Memory
		public override bool FilterInMemory(ONInstance instance)
		{
			if(!InMemory)
				return true;

			#region Horizontal visibility for agent 'Administrador'
			if(instance.OnContext.LeafActiveAgentFacets.Contains("Administrador"))
			{
				return true;
			}
			#endregion Horizontal visibility for agent 'Administrador'


			return (false);
		}
		#endregion Filter In Memory

		#region IsUnableToFilterInData
		/// <summary>
		/// Checks if the horizontal visibility filter must be executed in memory.
		/// In case there are more than one active facet for the connected agent with at least
		/// one not optimizable conjunction, the hole filter must be executed in memory.
		/// </summary>
		protected override bool IsUnableToFilterInData(ONContext onContext)
		{
			StringCollection lActiveFacets = onContext.LeafActiveAgentFacets;
		
			// Agent facets with empty HV formula
			if (lActiveFacets.Contains("Administrador"))
			{
				InMemory = false;
				return true;
			}
			
			return mIsUnableToFilterInData;
		}
		#endregion IsUnableToFilterInData		
	}
}
