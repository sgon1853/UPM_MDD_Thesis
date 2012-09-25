// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

using SIGEM.Client.Oids;

namespace SIGEM.Client
{
	/// <summary>
	/// Class 'ExchangeInfoNavigation'.
	/// </summary>
	[Serializable]
	public class ExchangeInfoNavigation : ExchangeInfo
	{
		#region Members
		/// <summary>
		/// Instance Role Path.
		/// </summary>
		private string mRolePath;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets role path.
		/// </summary>
		public string RolePath
		{
			get
			{
				return mRolePath;
			}
			set
			{
				mRolePath = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'ExchangeInfoNavigation'.
		/// </summary>
		/// <param name="classIUName">Class IU name.</param>
		/// <param name="iuName">IU name.</param>
		/// <param name="rolePath">Role path.</param>
		/// <param name="navigationalFilterIdentity">Navigational filter identity.</param>
		/// <param name="selectedOids">Selected oids.</param>
		/// <param name="previousContext">Previous context.</param>
		/// <param name="text2Title">Title to be shown the Target scenario</param>
		public ExchangeInfoNavigation(
			string classIUName,
			string iuName,
			string rolePath,
			string navigationalFilterIdentity,
			List<Oid> selectedOids,
			IUContext previousContext,
			string text2Title)
			: base( ExchangeType.Navigation, classIUName, iuName, previousContext)
		{
			SelectedOids = selectedOids;
			RolePath = rolePath;
			NavigationalFilterIdentity = navigationalFilterIdentity;
			Text2Title = text2Title;
		}

        /// <summary>
        /// Initializes a new instance of 'ExchangeInfoNavigation'.
        /// </summary>
        /// <param name="classIUName">Class IU name.</param>
        /// <param name="iuName">IU name.</param>
        /// <param name="rolePath">Role path.</param>
        /// <param name="navigationalFilterIdentity">Navigational filter identity.</param>
        /// <param name="selectedOids">Selected oids.</param>
        /// <param name="previousContext">Previous context.</param>
        /// <param name="text2Title">Title to be shown the Target scenario</param>
        /// <param name="defaultOrderCriteria">Default order criteria to be used in target scenario</param>
        public ExchangeInfoNavigation(
            string classIUName,
            string iuName,
            string rolePath,
            string navigationalFilterIdentity,
            List<Oid> selectedOids,
            IUContext previousContext,
            string text2Title,
            string defaultOrderCriteria)
            : this(classIUName, iuName, rolePath,navigationalFilterIdentity, selectedOids, previousContext, text2Title)
        {
            DefaultOrderCriteria = defaultOrderCriteria;
        }

		/// <summary>
		/// Initializes a new instance of 'ExchangeInfoNavigation' without the selected Oids.
		/// </summary>
		/// <param name="toBeCopied">To be copied.</param>
		public ExchangeInfoNavigation(ExchangeInfoNavigation toBeCopied)
			: this(toBeCopied.ClassName, toBeCopied.IUName, toBeCopied.RolePath, toBeCopied.NavigationalFilterIdentity, null, toBeCopied.Previous, toBeCopied.Text2Title, toBeCopied.DefaultOrderCriteria)
		{
		}
		#endregion Constructors
	}
}
