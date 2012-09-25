using System;

namespace SIGEM.Business
{
	public abstract class RevisionClassText
	{
		#region Class
		/// <summary>
		/// Alias of the class
		/// </summary>
		public const string ClassAlias = "Revision";
		#endregion Class

		#region Roles
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
		/// Alias of the inbound argument p_atrid_RevisarAeronave
		/// </summary>
		public const string Create_instance_P_atrid_RevisarAeronaveArgumentAlias = "id_RevisarAeronave";

		/// <summary>
		/// Alias of the inbound argument p_atrFechaRevision
		/// </summary>
		public const string Create_instance_P_atrFechaRevisionArgumentAlias = "FechaRevision";

		/// <summary>
		/// Alias of the inbound argument p_atrNombreRevisor
		/// </summary>
		public const string Create_instance_P_atrNombreRevisorArgumentAlias = "NombreRevisor";

		/// <summary>
		/// Alias of the inbound argument p_atrId_Aeronave
		/// </summary>
		public const string Create_instance_P_atrId_AeronaveArgumentAlias = "Id_Aeronave";

		#endregion Service create_instance
		
		#region Service delete_instance
		/// <summary>
		/// Alias of the service delete_instance
		/// </summary>
		public const string Delete_instanceServiceAlias = "Destroy";

		/// <summary>
		/// Alias of the inbound argument p_thisRevisarAeronave
		/// </summary>
		public const string Delete_instance_P_thisRevisarAeronaveArgumentAlias = "Revision";

		#endregion Service delete_instance
		
		#region Service edit_instance
		/// <summary>
		/// Alias of the service edit_instance
		/// </summary>
		public const string Edit_instanceServiceAlias = "Edit";

		/// <summary>
		/// Alias of the inbound argument p_thisRevisarAeronave
		/// </summary>
		public const string Edit_instance_P_thisRevisarAeronaveArgumentAlias = "Revision";

		#endregion Service edit_instance
		
		#endregion Services

		#region Filters
		#endregion Filters
	}
}
