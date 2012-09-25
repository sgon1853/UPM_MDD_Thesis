// 3.4.4.5
using System;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;
using SIGEM.Business.Types;
using SIGEM.Business.OID;

namespace SIGEM.Business.Query
{
	/// <summary>Provides all the information of the filter LMD that are defined in the class: Aeronave</summary>
	[ONFilter("Aeronave", "LMD")]
	internal class AeronaveLMDFilter : ONFilter
	{
		#region Members
		public ONDate InitDateVar;
		public ONDate FinalDateVar;
		#endregion

		#region Constructors
		public  AeronaveLMDFilter() : base("Aeronave", "LMD")
		{
			InMemory = false;
			InData = true;
		}

		public  AeronaveLMDFilter(ONDate initDateVar, ONDate finalDateVar) : base("Aeronave", "LMD")
		{	
			InMemory = false;
			InData = true;
		}
		#endregion

		#region Filter
		public override bool FilterInMemory(ONInstance instance)
		{
			AeronaveInstance lInstance = instance as AeronaveInstance;

			// SQL Optimized

			return true;
		}
		#endregion
	}
}
