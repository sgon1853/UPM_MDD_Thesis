// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Oids
{

	/// <summary>
	/// Represents the information of an alternate key.
	/// </summary>
	public class AlternateKey: Oid
	{
		#region Members
		private Oid mParentOid = null;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets the name of the alternate key name that is being used in the current Oid (parent).
		/// </summary>
		public override string AlternateKeyName
		{
			get 
			{
				return mParentOid.AlternateKeyName;
			}
			set
			{
				mParentOid.AlternateKeyName = value;
			}
		}
		#endregion Properties

		#region Constructors
		protected AlternateKey(string className, string alias, string idXML, FieldList fields)
			: base(className, alias, idXML, fields)
		{}
		protected AlternateKey(string className, string alias, string idXML, Oid parentOid)
			: this(className, alias, idXML, null as FieldList)
		{
			mParentOid = parentOid;
		}
		protected AlternateKey(string className, string alias, string idXML, IList<IOidField> fields)
			: base(className, alias, idXML, fields)
		{}
		protected AlternateKey(string className, string alias, string idXML, object[] values, ModelType[] types)
			: base(className, alias, idXML, values, types)
		{}
		protected AlternateKey(string className, string alias, string idXML, IList<object> values, IList<ModelType> types)
			: base(className, alias, idXML, values, types)
		{}
		#endregion Constructors

		#region Methods
		/// <summary>
		/// Gets a reference to the 'PrimaryKey' (primary Oid).
		/// </summary>
		/// <returns>Primary Oid reference.</returns>
		public override IOid GetOid()
		{
			return mParentOid;
		}
		/// <summary>
		/// Gets a reference to the 'AlternateKey' (alternate Oid).
		/// </summary>
		/// <param name="alternateKeyName">AlternateKey name.</param>
		/// <returns>Alternate Oid reference.</returns>
		public override IOid GetAlternateKey(string alternateKeyName)
		{
			return ((Oid)this.GetOid()).GetAlternateKey(alternateKeyName);
		}
		#endregion Methods
	}
}

