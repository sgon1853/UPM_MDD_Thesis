// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client
{
	/// <summary>
	/// Class 'IUQueryContext'.
	/// </summary>
	[Serializable]
	public class IUQueryContext : IUClassContext
	{
		#region Members
		/// <summary>
		/// Instance of Display Set Attributes.
		/// </summary>
		private string mDisplaySetAttributes;
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
		/// <summary>
		/// Associated Service context.
		/// </summary>
		private IUServiceContext mAssociatedServiceContext = null;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets display set attributes.
		/// </summary>
		public string DisplaySetAttributes
		{
			get
			{
				return mDisplaySetAttributes;
			}
			set
			{
				mDisplaySetAttributes = value;
			}
		}
		/// <summary>
		/// Gets or sets selected oids.
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
		/// Gets or sets related oids.
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
		/// Gets or sets related path.
		/// </summary>
		public override string RelatedPath
		{
			get
			{
				return mRelatedPath;
			}
		}
		/// <summary>
		/// Associated service context
		/// </summary>
		public IUServiceContext AssociatedServiceContext
		{
			get
			{
				return mAssociatedServiceContext;
			}
			set
			{
				mAssociatedServiceContext = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'IUQueryContext'.
		/// </summary>
		/// <param name="info">Info.</param>
		/// <param name="iuType">IU type.</param>
		/// <param name="className">Class name.</param>
		/// <param name="iuName">IU name.</param>
		public IUQueryContext(ExchangeInfo info, ContextType iuType, string className, string iuName)
			: base(info, iuType, className, iuName)
		{
            // Copy the selected Oids from the origin scenario. In this scenario they are the related Oids
            if (info.SelectedOids != null && info.SelectedOids.Count > 0)
            {
                RelatedOids = new List<Oid>();
                foreach (Oid lOid in info.SelectedOids)
                {
                    RelatedOids.Add(Oid.Create(lOid.ClassName, lOid.GetFields()));
                }
            }
		}

		#endregion Constructors
	}
}
