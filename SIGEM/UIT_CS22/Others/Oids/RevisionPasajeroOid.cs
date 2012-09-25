// v3.8.4.5.b
using System;
using System.Collections.Generic;

namespace SIGEM.Client.Oids
{
	/// <summary>
	/// Class 'RevisionPasajeroOid'.
	/// </summary>
	[Serializable]
	public class RevisionPasajeroOid : Oid
	{

		#region Field Names [SCD]
		/// <summary>
		/// Field names for 'State Change Detection'.
		/// </summary>
		public static string[] FIELD_NAMES = { "id_RevisionPasajero" };
		#endregion Field Names [SCD]

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'RevisionPasajeroOid'.
		/// </summary>
		/// <param name="genericOid">Generic Oid.</param>
		public RevisionPasajeroOid(Oid genericOid)
			: base("RevisionPasajero", "RevisionPasajero", "Clas_1348178673664478", genericOid.Fields)
		{
		}
		public RevisionPasajeroOid(int id_RevisionPasajero1Attr)
			: this()
		{
			// Attribute .id_RevisionPasajero
			this.Id_RevisionPasajero1Attr = id_RevisionPasajero1Attr;
		}
		/// <summary>
		/// Initializes a new instance of 'RevisionPasajeroOid'.
		/// </summary>
		/// <param name="values">Values.</param>
		public RevisionPasajeroOid(List<object> values)
			: this()
		{
			SetValues(values);
		}
		/// <summary>
		/// Initializes a new instance of 'RevisionPasajeroOid'.
		/// </summary>
		public RevisionPasajeroOid()
			: base("RevisionPasajero", "RevisionPasajero", "Clas_1348178673664478")
		{
			// Attribute .id_RevisionPasajero
			this.Fields.Add(FieldList.CreateField(string.Empty, ModelType.Autonumeric, 0));

		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Attribute Name is id_RevisionPasajero
		/// </summary>
		public int Id_RevisionPasajero1Attr
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
		/// Creates a 'RevisionPasajeroOid' object in a secure way, which means that all its fields
		/// have to be valid. Otherwise, the function returns null.
		/// </summary>
		/// <returns>'RevisionPasajeroOid' object or null.</returns>
		public static RevisionPasajeroOid CreateSecure(int? id_RevisionPasajero1Attr)
		{
			if(((id_RevisionPasajero1Attr != null) && (Type.Equals(id_RevisionPasajero1Attr.GetType(), typeof(int)))))
			{
				return new RevisionPasajeroOid((int)id_RevisionPasajero1Attr);
			}
			return null;
		}
		#endregion Methods [static]


	}
}
