// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

using SIGEM.Client.Oids;

namespace SIGEM.Client
{
	/// <summary>
	/// Class 'ExchangeInfoSelectionForward'.
	/// </summary>
	[Serializable]
	public class ExchangeInfoSelectionForward : ExchangeInfo
	{
		#region Members
		/// <summary>
		/// Only for selecting instances.
		/// </summary>
		private bool mMultiSelectionAllowed;
		/// <summary>
		/// Origin Service Class Name.
		/// </summary>
		private string mServiceClassName;
		/// <summary>
		/// Origin Class Name.
		/// </summary>
		private string mServiceName;
		/// <summary>
		/// Origin Argument Name.
		/// </summary>
		private string mArgumentName;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets allow multi selection.
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

		public string ServiceClassName
		{
			get
			{
				return mServiceClassName;
			}
			set
			{
				mServiceClassName = value;
			}
		}
		/// <summary>
		/// Gets or sets service name.
		/// </summary>
		public string ServiceName
		{
			get
			{
				return mServiceName;
			}
			set
			{
				mServiceName = value;
			}
		}
		/// <summary>
		/// Gets or sets argument name.
		/// </summary>
		public string ArgumentName
		{
			get
			{
				return mArgumentName;
			}
			set
			{
				mArgumentName = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'ExchangeInfoSelectionForward'.
		/// </summary>
		/// <param name="classIUName">Class IU name.</param>
		/// <param name="iuName">IU name.</param>
		/// <param name="originClassServiceName">Origin class service name.</param>
		/// <param name="originServiceName">Origin service name.</param>
		/// <param name="originArgumentName">Origin argument name.</param>
		/// <param name="isMultiSelection">Is multiple selection.</param>
		/// <param name="isNavigationalFilter">Is navigational filter.</param>
		/// <param name="previousContext">Previous context.</param>
		public ExchangeInfoSelectionForward(
			string classIUName,
			string iuName,
			string originClassServiceName,
			string originServiceName,
			string originArgumentName,
			bool isMultiSelection,
			bool isNavigationalFilter,
			IUContext previousContext)
			: base(ExchangeType.SelectionForward, classIUName, iuName, isNavigationalFilter, previousContext)
		{
			ServiceClassName = originClassServiceName;
			ServiceName = originServiceName;
			ArgumentName = originArgumentName;
			MultiSelectionAllowed = isMultiSelection;
		}
		#endregion Constructors
	}
	//#endregion ExchangeInfoSelectionForward
}
