// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client
{
	/// <summary>
	/// Class 'IUMainContext'.
	/// </summary>
	[Serializable]
	public class IUMainContext : IUContext
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the 'IUMainContext' class.
		/// </summary>
		public IUMainContext()
			: base(ContextType.Main, RelationType.Action)
		{
		}
		#endregion Constructors
	}
}
