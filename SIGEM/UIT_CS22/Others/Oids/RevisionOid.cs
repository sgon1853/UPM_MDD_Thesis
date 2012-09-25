// v3.8.4.5.b
using System;
using System.Collections.Generic;

namespace SIGEM.Client.Oids
{
	/// <summary>
	/// Class 'RevisionOid'.
	/// </summary>
	[Serializable]
	public class RevisionOid : Oid
	{

		#region Field Names [SCD]
		/// <summary>
		/// Field names for 'State Change Detection'.
		/// </summary>
		public static string[] FIELD_NAMES = { "id_RevisarAeronave" };
		#endregion Field Names [SCD]

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'RevisionOid'.
		/// </summary>
		/// <param name="genericOid">Generic Oid.</param>
		public RevisionOid(Oid genericOid)
			: base("Revision", "Revision", "Clas_1348178542592347", genericOid.Fields)
		{
		}
		public RevisionOid(int id_RevisarAeronave1Attr)
			: this()
		{
			// Attribute .id_RevisarAeronave
			this.Id_RevisarAeronave1Attr = id_RevisarAeronave1Attr;
		}
		/// <summary>
		/// Initializes a new instance of 'RevisionOid'.
		/// </summary>
		/// <param name="values">Values.</param>
		public RevisionOid(List<object> values)
			: this()
		{
			SetValues(values);
		}
		/// <summary>
		/// Initializes a new instance of 'RevisionOid'.
		/// </summary>
		public RevisionOid()
			: base("Revision", "Revision", "Clas_1348178542592347")
		{
			// Attribute .id_RevisarAeronave
			this.Fields.Add(FieldList.CreateField(string.Empty, ModelType.Autonumeric, 0));

		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Attribute Name is id_RevisarAeronave
		/// </summary>
		public int Id_RevisarAeronave1Attr
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
		/// Creates a 'RevisionOid' object in a secure way, which means that all its fields
		/// have to be valid. Otherwise, the function returns null.
		/// </summary>
		/// <returns>'RevisionOid' object or null.</returns>
		public static RevisionOid CreateSecure(int? id_RevisarAeronave1Attr)
		{
			if(((id_RevisarAeronave1Attr != null) && (Type.Equals(id_RevisarAeronave1Attr.GetType(), typeof(int)))))
			{
				return new RevisionOid((int)id_RevisarAeronave1Attr);
			}
			return null;
		}
		#endregion Methods [static]


	}
}
