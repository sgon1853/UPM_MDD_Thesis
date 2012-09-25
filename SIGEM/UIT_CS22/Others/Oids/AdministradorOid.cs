// v3.8.4.5.b
using System;
using System.Collections.Generic;

namespace SIGEM.Client.Oids
{
	/// <summary>
	/// Class 'AdministradorOid'.
	/// </summary>
	[Serializable]
	public class AdministradorOid : AgentInfo
	{

		#region Field Names [SCD]
		/// <summary>
		/// Field names for 'State Change Detection'.
		/// </summary>
		public static string[] FIELD_NAMES = { "id_Administrador" };
		#endregion Field Names [SCD]

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'AdministradorOid'.
		/// </summary>
		/// <param name="genericOid">Generic Oid.</param>
		public AdministradorOid(Oid genericOid)
			: base("Administrador", "Administrador", "Clas_1348605050880238", genericOid.Fields)
		{
		}
		public AdministradorOid(int id_Administrador1Attr)
			: this()
		{
			// Attribute .id_Administrador
			this.Id_Administrador1Attr = id_Administrador1Attr;
		}
		/// <summary>
		/// Initializes a new instance of 'AdministradorOid'.
		/// </summary>
		/// <param name="values">Values.</param>
		public AdministradorOid(List<object> values)
			: this()
		{
			SetValues(values);
		}
		/// <summary>
		/// Initializes a new instance of 'AdministradorOid'.
		/// </summary>
		public AdministradorOid()
			: base("Administrador", "Administrador", "Clas_1348605050880238")
		{
			// Attribute .id_Administrador
			this.Fields.Add(FieldList.CreateField(string.Empty, ModelType.Autonumeric, 0));

		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Attribute Name is id_Administrador
		/// </summary>
		public int Id_Administrador1Attr
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
		/// Creates a 'AdministradorOid' object in a secure way, which means that all its fields
		/// have to be valid. Otherwise, the function returns null.
		/// </summary>
		/// <returns>'AdministradorOid' object or null.</returns>
		public static AdministradorOid CreateSecure(int? id_Administrador1Attr)
		{
			if(((id_Administrador1Attr != null) && (Type.Equals(id_Administrador1Attr.GetType(), typeof(int)))))
			{
				return new AdministradorOid((int)id_Administrador1Attr);
			}
			return null;
		}
		#endregion Methods [static]


	}
}
