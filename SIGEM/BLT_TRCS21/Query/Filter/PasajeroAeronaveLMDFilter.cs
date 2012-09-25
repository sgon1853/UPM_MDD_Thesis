// 3.4.4.5
using System;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;
using SIGEM.Business.Types;
using SIGEM.Business.OID;

namespace SIGEM.Business.Query
{
	/// <summary>Provides all the information of the filter LMD that are defined in the class: PasajeroAeronave</summary>
	[ONFilter("PasajeroAeronave", "LMD")]
	internal class PasajeroAeronaveLMDFilter : ONFilter
	{
		#region Members
		public ONDate InitDateVar;
		public ONDate FinalDateVar;
		#endregion

		#region Constructors
		public  PasajeroAeronaveLMDFilter() : base("PasajeroAeronave", "LMD")
		{
			InMemory = false;
			InData = true;
		}

		public  PasajeroAeronaveLMDFilter(ONDate initDateVar, ONDate finalDateVar) : base("PasajeroAeronave", "LMD")
		{	
			InMemory = false;
			InData = true;
		}
		#endregion

		#region Filter
		public override bool FilterInMemory(ONInstance instance)
		{
			PasajeroAeronaveInstance lInstance = instance as PasajeroAeronaveInstance;

			// SQL Optimized

			return true;
		}
		#endregion
	}
}
