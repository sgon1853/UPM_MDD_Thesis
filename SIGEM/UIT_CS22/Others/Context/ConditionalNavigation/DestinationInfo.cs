// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client
{
	/// <summary>
	/// Class 'DestinationInfo'.
	/// </summary>
	[Serializable]
	public class DestinationInfo
	{
		#region Members
		/// <summary>
		/// Associated text.
		/// </summary>
		private string mAssociatedText = string.Empty;
		/// <summary>
		/// Exchange information.
		/// </summary>
		private ExchangeInfo mExchangeInfo = null;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets the associated text.
		/// </summary>
		public string AssociatedText
		{
			get
			{
				return mAssociatedText;
			}
			protected set
			{
				mAssociatedText = value;
			}
		}
		/// <summary>
		/// Gets or sets the exchange information.
		/// </summary>
		public ExchangeInfo ExchangeInfo
		{
			get
			{
				return mExchangeInfo;
			}
			protected set
			{
				mExchangeInfo = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'DestinationInfo' class.
		/// </summary>
		/// <param name="exchangeInfo">Exchange information.</param>
		/// <param name="associatedText">Associated text.</param>
		public DestinationInfo(ExchangeInfo exchangeInfo, string associatedText)
		{
			ExchangeInfo = exchangeInfo;
			AssociatedText = associatedText;
		}
		#endregion Constructors
	}
}

