// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client
{
	/// <summary>
	/// Class 'IUInstanceContext'.
	/// </summary>
	[Serializable]
	public class IUInstanceContext : IUQueryContext
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'IUInstanceContext' class.
		/// </summary>
		/// <param name="info">Info.</param>
		/// <param name="className">Class name.</param>
		/// <param name="iuName">IU name.</param>
		public IUInstanceContext(ExchangeInfo info, string className, string iuName)
			: base(info, ContextType.Instance, className, iuName)
		{
		}

		#endregion Constructors
	}
}
