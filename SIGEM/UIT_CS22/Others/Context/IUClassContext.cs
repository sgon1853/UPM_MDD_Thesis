// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client
{
	/// <summary>
	/// Class 'IUClassContext'.
	/// </summary>
	[Serializable]
	public abstract class IUClassContext : IUContext
	{
		#region Members
		/// <summary>
		/// Class name.
		/// </summary>
		private string mClassName;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets the class name.
		/// </summary>
		public string ClassName
		{
			get
			{
				return mClassName;
			}
			set
			{
				mClassName = value;
			}
		}
		/// <summary>
		/// Gets or sets selected oids.
		/// </summary>
		public abstract List<Oid> SelectedOids {get; set; }
		/// <summary>
		/// Gets or sets related oids.
		/// </summary>
		public abstract List<Oid> RelatedOids	{get; set; }
		/// <summary>
		/// Gets or sets related path.
		/// </summary>
		public abstract string RelatedPath	{get; }
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'IUClassContext' class.
		/// </summary>
		/// <param name="exchangeInfo">Exchange information.</param>
		/// <param name="contextType">Context type.</param>
		/// <param name="className">Class name.</param>
		/// <param name="iuName">Interaction unit name.</param>
		public IUClassContext(ExchangeInfo exchangeInfo, ContextType contextType, string className, string iuName)
			: base(exchangeInfo, contextType, iuName)
		{
			ClassName = className;
		}

		#endregion Constructors
	}
}
