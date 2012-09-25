// v3.8.4.5.b
using System;
using System.Collections.Generic;

namespace SIGEM.Client.Oids
{
	/// <summary>
	/// Class 'PasajeroOid'.
	/// </summary>
	[Serializable]
	public class PasajeroOid : Oid
	{

		#region Field Names [SCD]
		/// <summary>
		/// Field names for 'State Change Detection'.
		/// </summary>
		public static string[] FIELD_NAMES = { "id_Pasajero" };
		#endregion Field Names [SCD]

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'PasajeroOid'.
		/// </summary>
		/// <param name="genericOid">Generic Oid.</param>
		public PasajeroOid(Oid genericOid)
			: base("Pasajero", "Pasajero", "Clas_1348178542592658", genericOid.Fields)
		{
		}
		public PasajeroOid(int id_Pasajero1Attr)
			: this()
		{
			// Attribute .id_Pasajero
			this.Id_Pasajero1Attr = id_Pasajero1Attr;
		}
		/// <summary>
		/// Initializes a new instance of 'PasajeroOid'.
		/// </summary>
		/// <param name="values">Values.</param>
		public PasajeroOid(List<object> values)
			: this()
		{
			SetValues(values);
		}
		/// <summary>
		/// Initializes a new instance of 'PasajeroOid'.
		/// </summary>
		public PasajeroOid()
			: base("Pasajero", "Pasajero", "Clas_1348178542592658")
		{
			// Attribute .id_Pasajero
			this.Fields.Add(FieldList.CreateField(string.Empty, ModelType.Autonumeric, 0));

		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Attribute Name is id_Pasajero
		/// </summary>
		public int Id_Pasajero1Attr
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
		/// Creates a 'PasajeroOid' object in a secure way, which means that all its fields
		/// have to be valid. Otherwise, the function returns null.
		/// </summary>
		/// <returns>'PasajeroOid' object or null.</returns>
		public static PasajeroOid CreateSecure(int? id_Pasajero1Attr)
		{
			if(((id_Pasajero1Attr != null) && (Type.Equals(id_Pasajero1Attr.GetType(), typeof(int)))))
			{
				return new PasajeroOid((int)id_Pasajero1Attr);
			}
			return null;
		}
		#endregion Methods [static]


	}
}
