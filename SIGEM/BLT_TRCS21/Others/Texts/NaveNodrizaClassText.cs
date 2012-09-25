using System;

namespace SIGEM.Business
{
	public abstract class NaveNodrizaClassText
	{
		#region Class
		/// <summary>
		/// Alias of the class
		/// </summary>
		public const string ClassAlias = "NaveNodriza";
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
		/// Alias of the inbound argument p_atrid_NaveNodriza
		/// </summary>
		public const string Create_instance_P_atrid_NaveNodrizaArgumentAlias = "id_NaveNodriza";

		/// <summary>
		/// Alias of the inbound argument p_atrNombre_NaveNodriza
		/// </summary>
		public const string Create_instance_P_atrNombre_NaveNodrizaArgumentAlias = "Nombre_NaveNodriza";

		#endregion Service create_instance
		
		#region Service delete_instance
		/// <summary>
		/// Alias of the service delete_instance
		/// </summary>
		public const string Delete_instanceServiceAlias = "Destroy";

		/// <summary>
		/// Alias of the inbound argument p_thisNaveNodriza
		/// </summary>
		public const string Delete_instance_P_thisNaveNodrizaArgumentAlias = "NaveNodriza";

		#endregion Service delete_instance
		
		#region Service edit_instance
		/// <summary>
		/// Alias of the service edit_instance
		/// </summary>
		public const string Edit_instanceServiceAlias = "Edit";

		/// <summary>
		/// Alias of the inbound argument p_thisNaveNodriza
		/// </summary>
		public const string Edit_instance_P_thisNaveNodrizaArgumentAlias = "NaveNodriza";

		#endregion Service edit_instance
		
		#endregion Services

		#region Filters
		#endregion Filters
	}
}
