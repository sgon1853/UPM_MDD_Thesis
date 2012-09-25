// 3.4.4.5
using System;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;
using SIGEM.Business.Types;
using SIGEM.Business.OID;

namespace SIGEM.Business.Query
{
	/// <summary>Provides all the information of the filter LMD that are defined in the class: NaveNodriza</summary>
	[ONFilter("NaveNodriza", "LMD")]
	internal class NaveNodrizaLMDFilter : ONFilter
	{
		#region Members
		public ONDate InitDateVar;
		public ONDate FinalDateVar;
		#endregion

		#region Constructors
		public  NaveNodrizaLMDFilter() : base("NaveNodriza", "LMD")
		{
			InMemory = false;
			InData = true;
		}

		public  NaveNodrizaLMDFilter(ONDate initDateVar, ONDate finalDateVar) : base("NaveNodriza", "LMD")
		{	
			InMemory = false;
			InData = true;
		}
		#endregion

		#region Filter
		public override bool FilterInMemory(ONInstance instance)
		{
			NaveNodrizaInstance lInstance = instance as NaveNodrizaInstance;

			// SQL Optimized

			return true;
		}
		#endregion
	}
}
