// v3.8.4.5.b
using System;
using System.Collections.Generic;

namespace SIGEM.Client.Oids
{
	/// <summary>
	/// Class 'AeronaveOid'.
	/// </summary>
	[Serializable]
	public class AeronaveOid : Oid
	{

		#region Field Names [SCD]
		/// <summary>
		/// Field names for 'State Change Detection'.
		/// </summary>
		public static string[] FIELD_NAMES = { "id_Aeronave" };
		#endregion Field Names [SCD]

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'AeronaveOid'.
		/// </summary>
		/// <param name="genericOid">Generic Oid.</param>
		public AeronaveOid(Oid genericOid)
			: base("Aeronave", "Aeronave", "Clas_1348178411520734", genericOid.Fields)
		{
		}
		public AeronaveOid(int id_Aeronave1Attr)
			: this()
		{
			// Attribute .id_Aeronave
			this.Id_Aeronave1Attr = id_Aeronave1Attr;
		}
		/// <summary>
		/// Initializes a new instance of 'AeronaveOid'.
		/// </summary>
		/// <param name="values">Values.</param>
		public AeronaveOid(List<object> values)
			: this()
		{
			SetValues(values);
		}
		/// <summary>
		/// Initializes a new instance of 'AeronaveOid'.
		/// </summary>
		public AeronaveOid()
			: base("Aeronave", "Aeronave", "Clas_1348178411520734")
		{
			// Attribute .id_Aeronave
			this.Fields.Add(FieldList.CreateField(string.Empty, ModelType.Autonumeric, 0));

		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Attribute Name is id_Aeronave
		/// </summary>
		public int Id_Aeronave1Attr
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
		/// Creates a 'AeronaveOid' object in a secure way, which means that all its fields
		/// have to be valid. Otherwise, the function returns null.
		/// </summary>
		/// <returns>'AeronaveOid' object or null.</returns>
		public static AeronaveOid CreateSecure(int? id_Aeronave1Attr)
		{
			if(((id_Aeronave1Attr != null) && (Type.Equals(id_Aeronave1Attr.GetType(), typeof(int)))))
			{
				return new AeronaveOid((int)id_Aeronave1Attr);
			}
			return null;
		}
		#endregion Methods [static]


	}
}
