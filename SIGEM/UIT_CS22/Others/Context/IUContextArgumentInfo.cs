// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SIGEM.Client
{
	/// <summary>
	/// Class 'IUContextArgumentInfo'.
	/// </summary>
	[Serializable]
	public class IUContextArgumentInfo
	{
		#region Members
		/// <summary>
		/// Instance of Name.
		/// </summary>
		private string mName;
		/// <summary>
		/// Instance of Value.
		/// </summary>
		private object mValue;
		/// <summary>
		/// Instance of Enabled.
		/// </summary>
		private bool mEnabled;
		/// <summary>
		/// Instance of NullAllowed.
		/// </summary>
		private bool mNullAllowed;
		/// <summary>
		/// Instance of Mandatory.
		/// </summary>
		private bool mMandatory;
		/// <summary>
		/// Instance of Visible.
		/// </summary>
		private bool mVisible;
		/// <summary>
		/// Instance of Focused.
		/// </summary>
		private bool mFocused;
		/// <summary>
		/// Instance of PreLoadPopulation.
		/// </summary>
		private DataTable mPreLoadPopulation;
		/// <summary>
		/// Instance of SupplementaryInfo.
		/// </summary>
		private string mSupplementaryInfo;
		/// <summary>
		/// Order criteria (only when it has preload population).
		/// </summary>
		private string mOrderCriteria = string.Empty;
		/// <summary>
		/// Instance of PreLoadPopulationInitialized.
		/// </summary>
		private bool mPreLoadPopulationInitialized;
		/// <summary>
		/// Instance of MultipleSelectionAllowed.
		/// </summary>
		private bool mMultiSelectionAllowed;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets name.
		/// </summary>
		public string Name
		{
			get
			{
				return mName;
			}
			set
			{
				mName = value;
			}
		}
		/// <summary>
		/// Gets or sets argument value.
		/// </summary>
		public object Value
		{
			get
			{
				return mValue;
			}
			set
			{
				mValue = value;
			}
		}
		/// <summary>
		/// Gets or sets enabled.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return mEnabled;
			}
			set
			{
				mEnabled = value;
			}
		}
		/// <summary>
		/// Gets or sets NullAllowed.
		/// </summary>
		public bool NullAllowed
		{
			get
			{
				return mNullAllowed;
			}
			set
			{
				mNullAllowed = value;
			}
		}
		/// <summary>
		/// Gets or sets Mandatory.
		/// </summary>
		public bool Mandatory
		{
			get
			{
				return mMandatory;
			}
			set
			{
				mMandatory = value;
			}
		}
		/// <summary>
		/// Gets or sets Visible.
		/// </summary>
		public bool Visible
		{
			get
			{
				return mVisible;
			}
			set
			{
				mVisible = value;
			}
		}

		/// <summary>
		/// Gets or sets Focused.
		/// </summary>
		public bool Focused
		{
			get
			{
				return mFocused;
			}
			set
			{
				mFocused = value;
			}
		}
		/// <summary>
		/// Gets or sets pre-load population.
		/// </summary>
		public DataTable PreLoadPopulation
		{
			get
			{
				return mPreLoadPopulation;
			}
			set
			{
				mPreLoadPopulation = value;
			}
		}
		/// <summary>
		/// Gets or sets pre-load population initialized.
		/// </summary>
		public bool PreLoadPopulationInitialized
		{
			get
			{
				return mPreLoadPopulationInitialized;
			}
			set
			{
				mPreLoadPopulationInitialized = value;
			}
		}
		/// <summary>
		/// Gets or sets supplementary info.
		/// </summary>
		public string SupplementaryInfo
		{
			get
			{
				return mSupplementaryInfo;
			}
			set
			{
				mSupplementaryInfo = value;
			}
		}
		/// <summary>
		/// Gets or sets the order criteria associated to the argument (only when it has preload population).
		/// </summary>
		public string OrderCriteria
		{
			get
			{
				return mOrderCriteria;
			}
			set
			{
				mOrderCriteria = value;
			}
		}
		/// <summary>
		/// Gets or sets the multiple selection allowed property.
		/// </summary>
		public bool MultiSelectionAllowed
		{
			get
			{
				return mMultiSelectionAllowed;
			}
			set
			{
				mMultiSelectionAllowed = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'IUContextArgumentInfo' setting the name.
		/// </summary>
		/// <param name="Name">Name</param>
		public IUContextArgumentInfo(string Name)
		{
			mName = Name;
			mValue = null;
			mEnabled = true;
			mPreLoadPopulation = null;
		}
		/// <summary>
		/// Initializes a new instance of the 'IUContextArgumentInfo' class setting name, value, if it's enabled and a pre-load population.
		/// </summary>
		/// <param name="Name">Name</param>
		/// <param name="Value">Value</param>
		/// <param name="Enabled">Enabled</param>
		/// <param name="preLoadPopulation">Pre-load population</param>
		public IUContextArgumentInfo(string Name, object Value, bool Enabled, DataTable preLoadPopulation)
		{
			mName = Name;
			mValue = Value;
			mEnabled = Enabled;
			mPreLoadPopulation = preLoadPopulation;
			mPreLoadPopulationInitialized = false;
			mSupplementaryInfo = string.Empty;

		}
		#endregion Constructors
	}
}
