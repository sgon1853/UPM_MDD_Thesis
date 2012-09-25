// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;
using SIGEM.Business.Attributes;
using SIGEM.Business.Query;
using SIGEM.Business.Instance;
using SIGEM.Business.Collection;
using SIGEM.Business.Executive;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Exceptions;
using SIGEM.Business.Data;
using System.Collections.Generic;

namespace SIGEM.Business.Server
{
	[ONServerClass("Administrador")]
	[ONInterception()]
	internal class AdministradorServer : ONServer
	{
		#region Properties
		public new AdministradorInstance Instance
		{
			get
			{
				return base.Instance as AdministradorInstance;
			}
			set
			{
				base.Instance = value;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="onContext">This parameter has the current context</param>
		/// <param name="instance">This parameter has the instance that exectues the service</param>
		public  AdministradorServer(ONServiceContext onContext, AdministradorInstance instance) : base(onContext, instance, "Administrador")
		{
		}
		#endregion

		#region Service "New"
		/// <summary>
		/// This method solves the logical of the service New
		///// (Administrador's creation event)
		/// </summary>		
		/// <param name="p_atrid_AdministradorArg">This parameter represents the inbound argument id_Administrador</param>
		/// <param name="p_passwordArg">This parameter represents the inbound argument password</param>
		[ONTransactionalService("Administrador", "create_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public AdministradorInstance Create_instanceServ(ONInt p_atrid_AdministradorArg, ONString p_passwordArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
			
				// Call Executive
				AdministradorExecutive lExecutive = new AdministradorExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				Instance = lExecutive.Create_instanceServ(p_atrid_AdministradorArg, p_passwordArg);	

			}

			return Instance;
		}
		#endregion

		#region Service "Destroy"
		/// <summary>
		/// This method solves the logical of the service Destroy
		///// (Administrador's destruction event)
		/// </summary>		
		/// <param name="p_thisAdministradorArg">This parameter represents the inbound argument Administrador</param>
		[ONTransactionalService("Administrador", "delete_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void Delete_instanceServ(AdministradorOid p_thisAdministradorArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				AdministradorExecutive lExecutive = new AdministradorExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.Delete_instanceServ(p_thisAdministradorArg);

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion

		#region Service "Edit"
		/// <summary>
		/// This method solves the logical of the service Edit
		///// (Administrador's change event)
		/// </summary>		
		/// <param name="p_thisAdministradorArg">This parameter represents the inbound argument Administrador</param>
		[ONTransactionalService("Administrador", "edit_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void Edit_instanceServ(AdministradorOid p_thisAdministradorArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				AdministradorExecutive lExecutive = new AdministradorExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.Edit_instanceServ(p_thisAdministradorArg);

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion

		#region Service "Set password"
		/// <summary>
		/// This method solves the logical of the service Set password
		///// (Password event)
		/// </summary>		
		/// <param name="p_thisAdministradorArg">This parameter represents the inbound argument Administrador</param>
		/// <param name="p_NewPasswordArg">This parameter represents the inbound argument New password</param>
		[ONTransactionalService("Administrador", "setPassword")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void SetPasswordServ(AdministradorOid p_thisAdministradorArg, ONString p_NewPasswordArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				AdministradorExecutive lExecutive = new AdministradorExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.SetPasswordServ(p_thisAdministradorArg, p_NewPasswordArg);

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion

		#region Service "MVChangePassWord"
		/// <summary>
		/// This method solves the logical of the event MVChangePassWord
		
		/// </summary>
		[ONTransactionalService("Administrador", "MVChangePassWord")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void MVChangePassWordServ(ONString oldpasswordArg, ONString newpasswordArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				AdministradorExecutive lExecutive = new AdministradorExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.MVChangePassWordServ(oldpasswordArg, newpasswordArg);		

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion		
				
		#region Service "ValidatePassword"
		[ONTransactionalService("Administrador", "MVAgentValidation")]
		public void MVAgentValidationServ(AdministradorOid agentOid, ONString passwordArg)
		{
			passwordArg.TypedValue = ONSecureControl.CipherPassword(passwordArg.TypedValue);
			ONString lPassword = new ONString((Instance.PassWordAttr));
			if (!((lPassword.TypedValue == passwordArg.TypedValue) && (string.Compare(agentOid.ClassName, "Administrador", true) == 0 )))
				throw new ONAgentValidationException(null);
			OnContext.OidAgent = agentOid;
		}
		#endregion		
	}
}
