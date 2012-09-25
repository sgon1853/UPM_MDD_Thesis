// 3.4.4.5
using System;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;
using SIGEM.Business.Types;
using SIGEM.Business.OID;

namespace SIGEM.Business.Query
{
	/// <summary>Provides all the information of the filter LMD that are defined in the class: Pasajero</summary>
	[ONFilter("Pasajero", "LMD")]
	internal class PasajeroLMDFilter : ONFilter
	{
		#region Members
		public ONDate InitDateVar;
		public ONDate FinalDateVar;
		#endregion

		#region Constructors
		public  PasajeroLMDFilter() : base("Pasajero", "LMD")
		{
			InMemory = false;
			InData = true;
		}

		public  PasajeroLMDFilter(ONDate initDateVar, ONDate finalDateVar) : base("Pasajero", "LMD")
		{	
			InMemory = false;
			InData = true;
		}
		#endregion

		#region Filter
		public override bool FilterInMemory(ONInstance instance)
		{
			PasajeroInstance lInstance = instance as PasajeroInstance;

			// SQL Optimized

			return true;
		}
		#endregion
	}
}
