// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Oids
{
	public interface IAlternateKey
	{
		#region Properties
		/// <summary>
		/// Gets or sets the name of the current AlternateKey (alternate Oid).
		/// </summary>
		string AlternateKeyName { get; set; }
		#endregion Properties

		#region Methods
		/// <summary>
		/// Gets a reference to the 'PrimaryKey' (primary Oid).
		/// </summary>
		/// <returns>Primary Oid reference.</returns>
		IOid GetOid();
		/// <summary>
		/// Gets a reference to the 'AlternateKey' (alternate Oid).
		/// </summary>
		/// <param name="alternateKeyName">AlternateKey name.</param>
		/// <returns>Alternate Oid reference.</returns>
		IOid GetAlternateKey(string alternateKeyName);
		#endregion Methods
	}
}


