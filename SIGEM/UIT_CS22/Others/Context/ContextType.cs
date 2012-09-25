// v3.8.4.5.b
using System;

namespace SIGEM.Client
{
	/// <summary>
	/// Enumeration of 'ContextType'.
	/// </summary>
	[Serializable]
	public enum ContextType
	{
		Main,
		Instance,
		Population,
		MasterDetail,
		Service,
		Special,
		Error,
		InputFields,
		Filter,
		QueryService
	}
}
