using System;

namespace SIGEM.Business
{
	public abstract class PasajeroAeronaveClassText
	{
		#region Class
		/// <summary>
		/// Alias of the class
		/// </summary>
		public const string ClassAlias = "PasajeroAeronave";
		#endregion Class

		#region Roles
		/// <summary>
		/// Alias of the component role Aeronave
		/// </summary>
		public const string AeronaveRoleAlias = "Aeronave";

		/// <summary>
		/// Alias of the component role Pasajero
		/// </summary>
		public const string PasajeroRoleAlias = "Pasajero";

		/// <summary>
		/// Alias of the compound role RevisionPasajero
		/// </summary>
		public const string RevisionPasajeroRoleAlias = "RevisionPasajero";

		#endregion Roles

		#region Services
		#region Service create_instance
		/// <summary>
		/// Alias of the service create_instance
		/// </summary>
		public const string Create_instanceServiceAlias = "New";

		/// <summary>
		/// Alias of the inbound argument p_agrAeronave
		/// </summary>
		public const string Create_instance_P_agrAeronaveArgumentAlias = "Aeronave";

		/// <summary>
		/// Alias of the inbound argument p_agrPasajero
		/// </summary>
		public const string Create_instance_P_agrPasajeroArgumentAlias = "Pasajero";

		/// <summary>
		/// Alias of the inbound argument p_atrid_PasajeroAeronave
		/// </summary>
		public const string Create_instance_P_atrid_PasajeroAeronaveArgumentAlias = "id_PasajeroAeronave";

		/// <summary>
		/// Alias of the inbound argument p_atrNombreAeronave
		/// </summary>
		public const string Create_instance_P_atrNombreAeronaveArgumentAlias = "NombreAeronave";

		/// <summary>
		/// Alias of the inbound argument p_atrNombrePasajero
		/// </summary>
		public const string Create_instance_P_atrNombrePasajeroArgumentAlias = "NombrePasajero";

		#endregion Service create_instance
		
		#region Service delete_instance
		/// <summary>
		/// Alias of the service delete_instance
		/// </summary>
		public const string Delete_instanceServiceAlias = "Destroy";

		/// <summary>
		/// Alias of the inbound argument p_thisPasajeroAeronave
		/// </summary>
		public const string Delete_instance_P_thisPasajeroAeronaveArgumentAlias = "PasajeroAeronave";

		#endregion Service delete_instance
		
		#region Service edit_instance
		/// <summary>
		/// Alias of the service edit_instance
		/// </summary>
		public const string Edit_instanceServiceAlias = "Edit";

		/// <summary>
		/// Alias of the inbound argument p_thisPasajeroAeronave
		/// </summary>
		public const string Edit_instance_P_thisPasajeroAeronaveArgumentAlias = "PasajeroAeronave";

		#endregion Service edit_instance
		
		#endregion Services

		#region Filters
		#endregion Filters
	}
}
