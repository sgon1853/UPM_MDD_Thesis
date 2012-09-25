// v3.8.4.5.b
using System;
using System.Collections.Generic;

namespace SIGEM.Client.Oids
{
	/// <summary>
	/// Class 'NaveNodrizaOid'.
	/// </summary>
	[Serializable]
	public class NaveNodrizaOid : Oid
	{

		#region Field Names [SCD]
		/// <summary>
		/// Field names for 'State Change Detection'.
		/// </summary>
		public static string[] FIELD_NAMES = { "id_NaveNodriza" };
		#endregion Field Names [SCD]

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'NaveNodrizaOid'.
		/// </summary>
		/// <param name="genericOid">Generic Oid.</param>
		public NaveNodrizaOid(Oid genericOid)
			: base("NaveNodriza", "NaveNodriza", "Clas_1347649273856884", genericOid.Fields)
		{
		}
		public NaveNodrizaOid(int id_NaveNodriza1Attr)
			: this()
		{
			// Attribute .id_NaveNodriza
			this.Id_NaveNodriza1Attr = id_NaveNodriza1Attr;
		}
		/// <summary>
		/// Initializes a new instance of 'NaveNodrizaOid'.
		/// </summary>
		/// <param name="values">Values.</param>
		public NaveNodrizaOid(List<object> values)
			: this()
		{
			SetValues(values);
		}
		/// <summary>
		/// Initializes a new instance of 'NaveNodrizaOid'.
		/// </summary>
		public NaveNodrizaOid()
			: base("NaveNodriza", "NaveNodriza", "Clas_1347649273856884")
		{
			// Attribute .id_NaveNodriza
			this.Fields.Add(FieldList.CreateField(string.Empty, ModelType.Autonumeric, 0));

		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Attribute Name is id_NaveNodriza
		/// </summary>
		public int Id_NaveNodriza1Attr
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
		/// Creates a 'NaveNodrizaOid' object in a secure way, which means that all its fields
		/// have to be valid. Otherwise, the function returns null.
		/// </summary>
		/// <returns>'NaveNodrizaOid' object or null.</returns>
		public static NaveNodrizaOid CreateSecure(int? id_NaveNodriza1Attr)
		{
			if(((id_NaveNodriza1Attr != null) && (Type.Equals(id_NaveNodriza1Attr.GetType(), typeof(int)))))
			{
				return new NaveNodrizaOid((int)id_NaveNodriza1Attr);
			}
			return null;
		}
		#endregion Methods [static]


	}
}
