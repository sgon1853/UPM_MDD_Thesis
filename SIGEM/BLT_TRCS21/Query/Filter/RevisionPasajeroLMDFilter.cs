// 3.4.4.5
using System;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;
using SIGEM.Business.Types;
using SIGEM.Business.OID;

namespace SIGEM.Business.Query
{
	/// <summary>Provides all the information of the filter LMD that are defined in the class: RevisionPasajero</summary>
	[ONFilter("RevisionPasajero", "LMD")]
	internal class RevisionPasajeroLMDFilter : ONFilter
	{
		#region Members
		public ONDate InitDateVar;
		public ONDate FinalDateVar;
		#endregion

		#region Constructors
		public  RevisionPasajeroLMDFilter() : base("RevisionPasajero", "LMD")
		{
			InMemory = false;
			InData = true;
		}

		public  RevisionPasajeroLMDFilter(ONDate initDateVar, ONDate finalDateVar) : base("RevisionPasajero", "LMD")
		{	
			InMemory = false;
			InData = true;
		}
		#endregion

		#region Filter
		public override bool FilterInMemory(ONInstance instance)
		{
			RevisionPasajeroInstance lInstance = instance as RevisionPasajeroInstance;

			// SQL Optimized

			return true;
		}
		#endregion
	}
}
