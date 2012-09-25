// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;

using SIGEM.Client.Oids;

namespace SIGEM.Client
{
	/// <summary>
	/// Class 'IUFilterContext'.
	/// </summary>
	[Serializable]
	public class IUFilterContext : IUInputFieldsContext
	{
		#region Members
		/// <summary>
		/// Instance of Selected Oids.
		/// </summary>
		protected List<Oid> mSelectedOids = new List<Oid>();
		/// <summary>
		/// Instance of Related Oids.
		/// </summary>
		protected List<Oid> mRelatedOids;
		/// <summary>
		/// Instance of Related Path.
		/// </summary>
		protected string mRelatedPath = string.Empty;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets the filter name.
		/// </summary>
		public string FilterName
		{
			get
			{
				return ContainerName;
			}
			protected set
			{
				ContainerName = value;
			}
		}
		/// <summary>
		/// Gets or sets selected Oids.
		/// </summary>
		public override List<Oid> SelectedOids
		{
			get
			{
				return mSelectedOids;
			}
			set
			{
				mSelectedOids = value;
			}
		}
		/// <summary>
		/// Gets or sets related Oids.
		/// </summary>
		public override List<Oid> RelatedOids
		{
			get
			{
				return mRelatedOids;
			}
			set
			{
				mRelatedOids = value;
			}
		}
		/// <summary>
		/// Gets related path.
		/// </summary>
		public override string RelatedPath
		{
			get
			{
				return mRelatedPath;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initalizes a new instance of the 'IUFilterContext' class.
		/// </summary>
		/// <param name="exchangeInfo">Exchange information.</param>
		/// <param name="className">Class name.</param>
		/// <param name="filterName">Filter name.</param>
		public IUFilterContext(ExchangeInfo exchangeInfo, string className, string filterName)
			: base(exchangeInfo, className, filterName, string.Empty)
		{
			ContextType = ContextType.Filter;
		}
		#endregion Constructors
	}
}
