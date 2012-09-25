// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

using SIGEM.Client.Oids;

namespace SIGEM.Client
{
	/// <summary>
	/// Class 'ExchangeInfoAction'.
	/// </summary>
	[Serializable]
	public class ExchangeInfoAction :
		ExchangeInfo
	{
		#region Constructors
		public ExchangeInfoAction(
			string classIUName,
			string iuName,
			string navigationalFilterIdentity,
			List<Oid> selectedOids,
			IUContext previousContext)
			: base(ExchangeType.Action, classIUName, iuName, previousContext)
		{
			SelectedOids = selectedOids;
			//RolePath = rolePath;
			NavigationalFilterIdentity = navigationalFilterIdentity;
		}
		/// <summary>
		/// Initializes a new instance of 'ExchangeInfoAction'.
		/// </summary>
		/// <param name="className">Class name.</param>
		/// <param name="iuName">IU name.</param>
		/// <param name="selectedOids">Selected Oids.</param>
		/// <param name="previousContext">Previous context.</param>
		public ExchangeInfoAction(
			string className,
			string iuName,
			List<Oid> selectedOids,
			IUContext previousContext)
			: base(ExchangeType.Action, className, iuName, previousContext)
		{
			SelectedOids = selectedOids;
		}
		#endregion Constructors
	}
}
