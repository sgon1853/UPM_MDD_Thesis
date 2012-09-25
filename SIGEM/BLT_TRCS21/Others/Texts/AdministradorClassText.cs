using System;

namespace SIGEM.Business
{
	public abstract class AdministradorClassText
	{
		#region Class
		/// <summary>
		/// Alias of the class
		/// </summary>
		public const string ClassAlias = "Administrador";
		#endregion Class

		#region Roles
		#endregion Roles

		#region Services
		#region Service create_instance
		/// <summary>
		/// Alias of the service create_instance
		/// </summary>
		public const string Create_instanceServiceAlias = "New";

		/// <summary>
		/// Alias of the inbound argument p_atrid_Administrador
		/// </summary>
		public const string Create_instance_P_atrid_AdministradorArgumentAlias = "id_Administrador";

		/// <summary>
		/// Alias of the inbound argument p_password
		/// </summary>
		public const string Create_instance_P_passwordArgumentAlias = "password";

		#endregion Service create_instance
		
		#region Service delete_instance
		/// <summary>
		/// Alias of the service delete_instance
		/// </summary>
		public const string Delete_instanceServiceAlias = "Destroy";

		/// <summary>
		/// Alias of the inbound argument p_thisAdministrador
		/// </summary>
		public const string Delete_instance_P_thisAdministradorArgumentAlias = "Administrador";

		#endregion Service delete_instance
		
		#region Service edit_instance
		/// <summary>
		/// Alias of the service edit_instance
		/// </summary>
		public const string Edit_instanceServiceAlias = "Edit";

		/// <summary>
		/// Alias of the inbound argument p_thisAdministrador
		/// </summary>
		public const string Edit_instance_P_thisAdministradorArgumentAlias = "Administrador";

		#endregion Service edit_instance
		
		#region Service setPassword
		/// <summary>
		/// Alias of the service setPassword
		/// </summary>
		public const string SetPasswordServiceAlias = "Set password";

		/// <summary>
		/// Alias of the inbound argument p_thisAdministrador
		/// </summary>
		public const string SetPassword_P_thisAdministradorArgumentAlias = "Administrador";

		/// <summary>
		/// Alias of the inbound argument p_NewPassword
		/// </summary>
		public const string SetPassword_P_NewPasswordArgumentAlias = "New password";

		#endregion Service setPassword
		
		#endregion Services

		#region Filters
		#endregion Filters
	}
}
