// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

using SIGEM.Client.Oids;
using SIGEM.Client.Controllers;

namespace SIGEM.Client
{
	#region enum ExchangeType
	/// <summary>
	/// Enumeration of 'ExchangeType'.
	/// </summary>
	[Serializable]
	public enum ExchangeType
	{
		Generic,
		Navigation,
		Action,
		SelectionForward,
		SelectionBackward,
		ConditionalNavigation
	}
	#endregion enum ExchangeType

	/// <summary>
	/// Class 'ExchangeInfo'.
	/// </summary>
	[Serializable]
	public class ExchangeInfo
	{
		#region Members
		/// <summary>
		/// Instance Exchange Type.
		/// </summary>
		private ExchangeType mExchangeType;
		/// <summary>
		/// Instance Class Name.
		/// </summary>
		private string mClassName;
		/// <summary>
		/// Instance IU Name.
		/// </summary>
		private string mIUName;
		/// <summary>
		/// Instance Previous.
		/// </summary>
		private IUContext mPrevious;
		/// <summary>
		/// Instance Selected Oids.
		/// </summary>
		private List<Oid> mSelectedOids;
		/// <summary>
		/// Instance Custom Data.
		/// </summary>
		private Dictionary<string, object> mCustomData;
		/// <summary>
		/// Instance Navigational Filter (Only for selecting instances with navigational filter).
		/// </summary>
		private bool mNavigationalFilter;
		/// <summary>
		/// Instance Navigational Filter Identity.
		/// </summary>
		private string mNavigationalFilterIdentity;
		/// <summary>
		/// Text to be shown in the title of the target scenario
		/// </summary>
		private string mText2Title = "";
        /// <summary>
        /// Default order criteria to be used in the destination query
        /// </summary>
        private string mDefaultOrderCriteria;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets exchange type.
		/// </summary>
		public ExchangeType ExchangeType
		{
			get
			{
				return mExchangeType;
			}
			protected set
			{
				mExchangeType = value;
			}
		}
		/// <summary>
		/// Gets or sets class name.
		/// </summary>
		public string ClassName
		{
			get
			{
				return mClassName;
			}
			protected set
			{
				mClassName = value;
			}
		}
		/// <summary>
		/// Gets or sets IU name.
		/// </summary>
		public string IUName
		{
			get
			{
				return mIUName;
			}
			protected set
			{
				mIUName = value;
			}
		}
		/// <summary>
		/// Gets or sets navigational filter.
		/// </summary>
		public bool NavigationalFilter
		{
			get
			{
				return mNavigationalFilter;
			}
			set
			{
				mNavigationalFilter = value;
			}
		}
		/// <summary>
		/// Gets or sets navigational filter identity.
		/// </summary>
		public string NavigationalFilterIdentity
		{
			get
			{
				return mNavigationalFilterIdentity;
			}
			protected set
			{
				mNavigationalFilterIdentity = value == null ? string.Empty: value;
				// If has NavigationalFilterIdentity has NavigationalFilter.
				NavigationalFilter = mNavigationalFilterIdentity.Length > 0 ? true: false;
			}
		}
		/// <summary>
		/// Gets or sets previous.
		/// </summary>
		public IUContext Previous
		{
			get
			{
				return mPrevious;
			}
			protected set
			{
				mPrevious = value;
			}
		}
		/// <summary>
		/// Gets or sets selected oids.
		/// </summary>
		public List<Oid> SelectedOids
		{
			get
			{
				return mSelectedOids;
			}
			set
			{
				if (value == null)
				{
					mSelectedOids = new List<Oid>();
				}
				else
				{
					mSelectedOids = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets custom data.
		/// </summary>
		public Dictionary<string,object> CustomData
		{
			get
			{
				return mCustomData;
			}
			set
			{
				if (value == null)
				{
					mCustomData = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
				}
				else
				{
					mCustomData = value;
				}
			}
		}
		/// <summary>
		/// Text to be shown in the title of the target scenario
		/// </summary>
		public string Text2Title
		{
			get
			{
				return mText2Title;
			}
			protected set
			{
				mText2Title = value;
			}
		}
        /// <summary>
        /// Default order criteria to be used in the destination query
        /// </summary>
        public string DefaultOrderCriteria
        {
            get
            {
                return mDefaultOrderCriteria;
            }
            set
            {
                mDefaultOrderCriteria = value;
            }
        }
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'ExchangeInfo' without navigational filter.
		/// </summary>
		/// <param name="exchangeType">Exchange type.</param>
		/// <param name="className">Class name.</param>
		/// <param name="iuName">IU Name.</param>
		/// <param name="previousContext">Previous context.</param>
		public ExchangeInfo(
			ExchangeType exchangeType,
			string className,
			string iuName,
			IUContext previousContext)
			:this(exchangeType,className,iuName,false,previousContext)
		{
		}
		/// <summary>
		/// Initializes a new instance of 'ExchangeInfo' for generic use.
		/// </summary>
		/// <param name="className">Class name.</param>
		/// <param name="iuName">IU name.</param>
		/// <param name="previousContext">Previous context.</param>
		public ExchangeInfo(
			string className,
			string iuName,
			IUContext previousContext)
			: this(ExchangeType.Generic, className, iuName, false, previousContext)
		{
		}
		/// <summary>
		/// Initializes a new instance of 'ExchangeInfo'.
		/// </summary>
		/// <param name="exchangeType">Exchange type.</param>
		/// <param name="className">Class name.</param>
		/// <param name="iuName">IU Name.</param>
		/// <param name="isNavigationalFilter">Navigational filter.</param>
		/// <param name="previousContext">Previous context.</param>
		public ExchangeInfo(
			ExchangeType exchangeType,
			string className,
			string iuName,
			bool isNavigationalFilter,
			IUContext previousContext)
		{
			ExchangeType = exchangeType;
			ClassName = className;
			IUName = iuName;
			NavigationalFilter = isNavigationalFilter;
			Previous = previousContext;
			// Initialize
			CustomData = null;
		}
		#endregion Constructors

		#region Methods
		/// <summary>
		/// Returns the Role name of the last navigation done
		/// </summary>
			/// <returns>string.</returns>
		public string GetLastNavigationRole()
		{
			// Search in the context stack until the first navigation info
			if (Previous != null)
			{
				return GetLastNavigationRole(Previous.ExchangeInformation);
			}
			return string.Empty;
		}
		/// <summary>
		/// Returns the Role name of the last navigation done.
		/// </summary>
		/// <param name="info">Exchange info.</param>
		/// <returns>string.</returns>
		private string GetLastNavigationRole(ExchangeInfo info)
		{
			if (info == null)
			{
				return string.Empty;
			}
			// Stop condition: If the ExchangeType is diferent than Navigation
			if (info.ExchangeType == ExchangeType.Navigation)
			{
				return ((ExchangeInfoNavigation)info).RolePath;
			}
			else
			{
				return string.Empty;
			}
		}
		/// <summary>
		/// Returns the Oids of the last navigation done.
		/// </summary>
		/// <returns>List<Oid>.</returns>
		public List<Oid> GetLastNavigationRoleOids()
		{
			// Search in the context stack until the first navigation info
			if (Previous != null)
			{
				return GetLastNavigationRoleOids(Previous.ExchangeInformation);
			}
			return null;
		}
		/// <summary>
		/// Returns the Oids of the last navigation done.
		/// </summary>
		/// <param name="info">Exchange info.</param>
		/// <returns>List<Oid>.</returns>
		private List<Oid> GetLastNavigationRoleOids(ExchangeInfo info)
		{
			if (info == null)
			{
				return null;
			}
			// Stop condition: If the ExchangeType is diferent than Navigation
			if (info.ExchangeType == ExchangeType.Navigation)
			{
				return ((ExchangeInfoNavigation)info).SelectedOids;
			}
			else
			{
				return null;
			}
		}
		/// <summary>
		/// Get oids of a class.
		/// </summary>
		/// <param name="className">Class name.</param>
		/// <returns>List<Oid>.</returns>
		public List<Oid> GetOidsOfClass(string className)
		{
			switch (ExchangeType)
			{
			case ExchangeType.Action:
				if (UtilFunctions.OidsBelongToClass(((ExchangeInfoAction)this).SelectedOids, className))
				{
					return ((ExchangeInfoAction)this).SelectedOids;
				}
				break;
			case ExchangeType.Navigation:
				if (UtilFunctions.OidsBelongToClass(((ExchangeInfoNavigation)this).SelectedOids, className))
				{
					return ((ExchangeInfoNavigation)this).SelectedOids;
				}
				break;
			}
			if (Previous != null && Previous.ExchangeInformation != null)
			{
				return Previous.ExchangeInformation.GetOidsOfClass(className);
			}
			return null;
		}

		/// <summary>
		/// Clears the Extra information stored in the navigated Oids.
		/// </summary>
		public void ClearExtraInfo()
		{
			if (this.GetLastNavigationRole() != "")
			{
				List<Oid> lLastNavigationOids = this.GetLastNavigationRoleOids();
				foreach (Oid lOid in lLastNavigationOids)
				{
					lOid.ExtraInfo = null;
				}
			}

			// Clear the Previous context ExtraInfo.
			if (this.Previous != null)
			{
				Previous.ExchangeInformation.ClearExtraInfo();
			}
		}
		#endregion Methods

		public static bool IsNavigation(ExchangeInfo exchangeInfo)
		{
			return (exchangeInfo.ExchangeType == ExchangeType.Navigation);
		}

		public static bool HasSelectedOids(ExchangeInfo exchangeInfo)
		{
			return (exchangeInfo.SelectedOids != null && exchangeInfo.SelectedOids.Count > 0);
		}

	}
}
