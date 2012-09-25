// v3.8.4.5.b
using System;
using System.Collections.Generic;

namespace SIGEM.Client.Oids
{
	/// <summary>
	/// Class 'PasajeroAeronaveOid'.
	/// </summary>
	[Serializable]
	public class PasajeroAeronaveOid : Oid
	{

		#region Field Names [SCD]
		/// <summary>
		/// Field names for 'State Change Detection'.
		/// </summary>
		public static string[] FIELD_NAMES = { "id_PasajeroAeronave" };
		#endregion Field Names [SCD]

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'PasajeroAeronaveOid'.
		/// </summary>
		/// <param name="genericOid">Generic Oid.</param>
		public PasajeroAeronaveOid(Oid genericOid)
			: base("PasajeroAeronave", "PasajeroAeronave", "Clas_1348178542592177", genericOid.Fields)
		{
		}
		public PasajeroAeronaveOid(int id_PasajeroAeronave1Attr)
			: this()
		{
			// Attribute .id_PasajeroAeronave
			this.Id_PasajeroAeronave1Attr = id_PasajeroAeronave1Attr;
		}
		/// <summary>
		/// Initializes a new instance of 'PasajeroAeronaveOid'.
		/// </summary>
		/// <param name="values">Values.</param>
		public PasajeroAeronaveOid(List<object> values)
			: this()
		{
			SetValues(values);
		}
		/// <summary>
		/// Initializes a new instance of 'PasajeroAeronaveOid'.
		/// </summary>
		public PasajeroAeronaveOid()
			: base("PasajeroAeronave", "PasajeroAeronave", "Clas_1348178542592177")
		{
			// Attribute .id_PasajeroAeronave
			this.Fields.Add(FieldList.CreateField(string.Empty, ModelType.Autonumeric, 0));

		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Attribute Name is id_PasajeroAeronave
		/// </summary>
		public int Id_PasajeroAeronave1Attr
		{
			get
			{
				return (int) this.Fields[0].Value;
			}
			protected set
			{
				this.Fields[0].Value = value;
			}
		}
		#endregion Properties

		#region Methods [static]
		/// <summary>
		/// Creates a 'PasajeroAeronaveOid' object in a secure way, which means that all its fields
		/// have to be valid. Otherwise, the function returns null.
		/// </summary>
		/// <returns>'PasajeroAeronaveOid' object or null.</returns>
		public static PasajeroAeronaveOid CreateSecure(int? id_PasajeroAeronave1Attr)
		{
			if(((id_PasajeroAeronave1Attr != null) && (Type.Equals(id_PasajeroAeronave1Attr.GetType(), typeof(int)))))
			{
				return new PasajeroAeronaveOid((int)id_PasajeroAeronave1Attr);
			}
			return null;
		}
		#endregion Methods [static]


	}
}
