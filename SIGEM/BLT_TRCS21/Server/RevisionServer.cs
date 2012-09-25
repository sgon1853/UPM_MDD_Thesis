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
	[ONServerClass("Revision")]
	[ONInterception()]
	internal class RevisionServer : ONServer
	{
		#region Properties
		public new RevisionInstance Instance
		{
			get
			{
				return base.Instance as RevisionInstance;
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
		public  RevisionServer(ONServiceContext onContext, RevisionInstance instance) : base(onContext, instance, "Revision")
		{
		}
		#endregion

		#region Service "New"
		/// <summary>
		/// This method solves the logical of the service New
		///// (RevisarAeronave's creation event)
		/// </summary>		
		/// <param name="p_atrid_RevisarAeronaveArg">This parameter represents the inbound argument id_RevisarAeronave</param>
		/// <param name="p_atrFechaRevisionArg">This parameter represents the inbound argument FechaRevision</param>
		/// <param name="p_atrNombreRevisorArg">This parameter represents the inbound argument NombreRevisor</param>
		/// <param name="p_atrId_AeronaveArg">This parameter represents the inbound argument Id_Aeronave</param>
		[ONTransactionalService("Revision", "create_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public RevisionInstance Create_instanceServ(ONInt p_atrid_RevisarAeronaveArg, ONDate p_atrFechaRevisionArg, ONString p_atrNombreRevisorArg, ONString p_atrId_AeronaveArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
			
				// Call Executive
				RevisionExecutive lExecutive = new RevisionExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				Instance = lExecutive.Create_instanceServ(p_atrid_RevisarAeronaveArg, p_atrFechaRevisionArg, p_atrNombreRevisorArg, p_atrId_AeronaveArg);	

			}

			return Instance;
		}
		#endregion

		#region Service "Destroy"
		/// <summary>
		/// This method solves the logical of the service Destroy
		///// (RevisarAeronave's destruction event)
		/// </summary>		
		/// <param name="p_thisRevisarAeronaveArg">This parameter represents the inbound argument Revision</param>
		[ONTransactionalService("Revision", "delete_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void Delete_instanceServ(RevisionOid p_thisRevisarAeronaveArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				RevisionExecutive lExecutive = new RevisionExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.Delete_instanceServ(p_thisRevisarAeronaveArg);

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion

		#region Service "Edit"
		/// <summary>
		/// This method solves the logical of the service Edit
		///// (RevisarAeronave's change event)
		/// </summary>		
		/// <param name="p_thisRevisarAeronaveArg">This parameter represents the inbound argument Revision</param>
		[ONTransactionalService("Revision", "edit_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void Edit_instanceServ(RevisionOid p_thisRevisarAeronaveArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				RevisionExecutive lExecutive = new RevisionExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.Edit_instanceServ(p_thisRevisarAeronaveArg);

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion
	}
}
