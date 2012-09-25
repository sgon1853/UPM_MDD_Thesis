// 3.4.4.5
using System;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;
using SIGEM.Business.Types;
using SIGEM.Business.OID;

namespace SIGEM.Business.Query
{
	/// <summary>Provides all the information of the filter LMD that are defined in the class: Revision</summary>
	[ONFilter("Revision", "LMD")]
	internal class RevisionLMDFilter : ONFilter
	{
		#region Members
		public ONDate InitDateVar;
		public ONDate FinalDateVar;
		#endregion

		#region Constructors
		public  RevisionLMDFilter() : base("Revision", "LMD")
		{
			InMemory = false;
			InData = true;
		}

		public  RevisionLMDFilter(ONDate initDateVar, ONDate finalDateVar) : base("Revision", "LMD")
		{	
			InMemory = false;
			InData = true;
		}
		#endregion

		#region Filter
		public override bool FilterInMemory(ONInstance instance)
		{
			RevisionInstance lInstance = instance as RevisionInstance;

			// SQL Optimized

			return true;
		}
		#endregion
	}
}
