// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client
{
	/// <summary>
	/// Class 'IUMasterDetailContext'.
	/// </summary>
	[Serializable]
	public class IUMasterDetailContext : IUClassContext
	{
		#region Members
		/// <summary>
		/// Instance of Master.
		/// </summary>
		private IUClassContext mMaster = null;
		/// <summary>
		/// Instance of Details.
		/// </summary>
		private List<IUClassContext> mDetails = new List<IUClassContext>();
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets master.
		/// </summary>
		public IUClassContext Master
		{
			get
			{
				return mMaster;
			}
			set
			{
				mMaster = value;
			}
		}
		/// <summary>
		/// Gets details.
		/// </summary>
		public List<IUClassContext> Details
		{
			get
			{
				return mDetails;
			}
			set
			{
				mDetails = value;
			}
		}
		/// <summary>
		/// Gets or sets selected Oids.
		/// </summary>
		public override List<Oid> SelectedOids
		{
			get
			{
				if (Master == null)
				{
					return null;
				}
				else
				{
					return Master.SelectedOids;
				}
			}
			set
			{
				if (Master != null)
				{
					Master.SelectedOids = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets related Oids.
		/// </summary>
		public override List<Oid> RelatedOids
		{
			get
			{
				if (Master == null)
				{
					return null;
				}
				else
				{
					return Master.RelatedOids;
				}
			}
			set
			{
				if (Master != null)
				{
					Master.RelatedOids = value;
				}
			}
		}
		/// <summary>
		/// Gets related path.
		/// </summary>
		public override string RelatedPath
		{
			get
			{
				if (Master != null)
				{
					return Master.RelatedPath;
				}
				else
				{
					return string.Empty;
				}
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'IUMasterDetailContext'.
		/// </summary>
		/// <param name="info">Exchange information.</param>
		/// <param name="className">Class name.</param>
		/// <param name="iuName">Interaction unit name.</param>
		public IUMasterDetailContext(string className, string iuName)
			: base( null,ContextType.MasterDetail,className, iuName)
		{
		}

		#endregion Constructors
	}
}
