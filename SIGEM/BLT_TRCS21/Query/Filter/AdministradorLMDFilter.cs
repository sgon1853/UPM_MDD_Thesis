// 3.4.4.5
using System;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;
using SIGEM.Business.Types;
using SIGEM.Business.OID;

namespace SIGEM.Business.Query
{
	/// <summary>Provides all the information of the filter LMD that are defined in the class: Administrador</summary>
	[ONFilter("Administrador", "LMD")]
	internal class AdministradorLMDFilter : ONFilter
	{
		#region Members
		public ONDate InitDateVar;
		public ONDate FinalDateVar;
		#endregion

		#region Constructors
		public  AdministradorLMDFilter() : base("Administrador", "LMD")
		{
			InMemory = false;
			InData = true;
		}

		public  AdministradorLMDFilter(ONDate initDateVar, ONDate finalDateVar) : base("Administrador", "LMD")
		{	
			InMemory = false;
			InData = true;
		}
		#endregion

		#region Filter
		public override bool FilterInMemory(ONInstance instance)
		{
			AdministradorInstance lInstance = instance as AdministradorInstance;

			// SQL Optimized

			return true;
		}
		#endregion
	}
}
