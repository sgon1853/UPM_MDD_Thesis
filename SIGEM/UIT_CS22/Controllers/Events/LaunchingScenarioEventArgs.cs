// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Stores information that will be send to open a new scenario
	/// </summary>
	public class LaunchingScenarioEventArgs : EventArgs
	{
		#region Members
		/// <summary>
		/// Exchange information.
		/// </summary>
		private ExchangeInfo mExchangeInformation;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'LaunchingScenarioEventArgs' class.
		/// </summary>
		/// <param name="exchangeInformation">Exchange information.</param>
		public LaunchingScenarioEventArgs(ExchangeInfo exchangeInformation)
		{
			mExchangeInformation = exchangeInformation;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets the Exchange information.
		/// </summary>
		public ExchangeInfo ExchangeInformation
		{
			get
			{
				return mExchangeInformation;
			}
		}
		#endregion Properties
	}
}

