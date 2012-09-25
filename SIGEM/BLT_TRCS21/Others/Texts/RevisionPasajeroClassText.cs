using System;

namespace SIGEM.Business
{
	public abstract class RevisionPasajeroClassText
	{
		#region Class
		/// <summary>
		/// Alias of the class
		/// </summary>
		public const string ClassAlias = "RevisionPasajero";
		#endregion Class

		#region Roles
		/// <summary>
		/// Alias of the component role Revision
		/// </summary>
		public const string RevisionRoleAlias = "Revision";

		/// <summary>
		/// Alias of the component role PasajeroAeronave
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
		/// Alias of the inbound argument p_agrRevision
		/// </summary>
		public const string Create_instance_P_agrRevisionArgumentAlias = "Revision";

		/// <summary>
		/// Alias of the inbound argument p_atrid_RevisionPasajero
		/// </summary>
		public const string Create_instance_P_atrid_RevisionPasajeroArgumentAlias = "id_RevisionPasajero";

		#endregion Service create_instance
		
		#region Service delete_instance
		/// <summary>
		/// Alias of the service delete_instance
		/// </summary>
		public const string Delete_instanceServiceAlias = "Destroy";

		/// <summary>
		/// Alias of the inbound argument p_thisRevisionPasajero
		/// </summary>
		public const string Delete_instance_P_thisRevisionPasajeroArgumentAlias = "RevisionPasajero";

		#endregion Service delete_instance
		
		#region Service edit_instance
		/// <summary>
		/// Alias of the service edit_instance
		/// </summary>
		public const string Edit_instanceServiceAlias = "Edit";

		/// <summary>
		/// Alias of the inbound argument p_thisRevisionPasajero
		/// </summary>
		public const string Edit_instance_P_thisRevisionPasajeroArgumentAlias = "RevisionPasajero";

		#endregion Service edit_instance
		
		#endregion Services

		#region Filters
		#endregion Filters
	}
}
