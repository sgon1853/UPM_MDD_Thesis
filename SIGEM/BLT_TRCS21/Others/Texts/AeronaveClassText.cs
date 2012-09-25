using System;

namespace SIGEM.Business
{
	public abstract class AeronaveClassText
	{
		#region Class
		/// <summary>
		/// Alias of the class
		/// </summary>
		public const string ClassAlias = "Aeronave";
		#endregion Class

		#region Roles
		/// <summary>
		/// Alias of the compound role PasajeroAeronave
		/// </summary>
		public const string PasajeroAeronaveRoleAlias = "PasajeroAeronave";

		#endregion Roles

		#region Services
		#region Service create_instance
		/// <summary>
		/// Alias of the service create_instance
		/// </summary>
		public const string Create_instanceServiceAlias = "New";

		/// <summary>
		/// Alias of the inbound argument p_agrPasajeroAeronave
		/// </summary>
		public const string Create_instance_P_agrPasajeroAeronaveArgumentAlias = "PasajeroAeronave";

		/// <summary>
		/// Alias of the inbound argument p_atrid_Aeronave
		/// </summary>
		public const string Create_instance_P_atrid_AeronaveArgumentAlias = "id_Aeronave";

		/// <summary>
		/// Alias of the inbound argument p_atrNombre
		/// </summary>
		public const string Create_instance_P_atrNombreArgumentAlias = "Nombre";

		/// <summary>
		/// Alias of the inbound argument p_atrMaximoPasajeros
		/// </summary>
		public const string Create_instance_P_atrMaximoPasajerosArgumentAlias = "MaximoPasajeros";

		/// <summary>
		/// Alias of the inbound argument p_atrOrigen
		/// </summary>
		public const string Create_instance_P_atrOrigenArgumentAlias = "Origen";

		/// <summary>
		/// Alias of the inbound argument p_atrDestino
		/// </summary>
		public const string Create_instance_P_atrDestinoArgumentAlias = "Destino";

		#endregion Service create_instance
		
		#region Service delete_instance
		/// <summary>
		/// Alias of the service delete_instance
		/// </summary>
		public const string Delete_instanceServiceAlias = "Destroy";

		/// <summary>
		/// Alias of the inbound argument p_thisAeronave
		/// </summary>
		public const string Delete_instance_P_thisAeronaveArgumentAlias = "Aeronave";

		#endregion Service delete_instance
		
		#region Service edit_instance
		/// <summary>
		/// Alias of the service edit_instance
		/// </summary>
		public const string Edit_instanceServiceAlias = "Edit";

		/// <summary>
		/// Alias of the inbound argument p_thisAeronave
		/// </summary>
		public const string Edit_instance_P_thisAeronaveArgumentAlias = "Aeronave";

		#endregion Service edit_instance
		
		#endregion Services

		#region Filters
		#endregion Filters
	}
}
