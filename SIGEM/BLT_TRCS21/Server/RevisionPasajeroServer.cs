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
	[ONServerClass("RevisionPasajero")]
	[ONInterception()]
	internal class RevisionPasajeroServer : ONServer
	{
		#region Properties
		public new RevisionPasajeroInstance Instance
		{
			get
			{
				return base.Instance as RevisionPasajeroInstance;
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
		public  RevisionPasajeroServer(ONServiceContext onContext, RevisionPasajeroInstance instance) : base(onContext, instance, "RevisionPasajero")
		{
		}
		#endregion

		#region Service "New"
		/// <summary>
		/// This method solves the logical of the service New
		///// (RevisionPasajero's creation event)
		/// </summary>		
		/// <param name="p_agrPasajeroAeronaveArg">This parameter represents the inbound argument PasajeroAeronave</param>
		/// <param name="p_agrRevisionArg">This parameter represents the inbound argument Revision</param>
		/// <param name="p_atrid_RevisionPasajeroArg">This parameter represents the inbound argument id_RevisionPasajero</param>
		[ONTransactionalService("RevisionPasajero", "create_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public RevisionPasajeroInstance Create_instanceServ(PasajeroAeronaveOid p_agrPasajeroAeronaveArg, RevisionOid p_agrRevisionArg, ONInt p_atrid_RevisionPasajeroArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
			
				// Call Executive
				RevisionPasajeroExecutive lExecutive = new RevisionPasajeroExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				Instance = lExecutive.Create_instanceServ(p_agrPasajeroAeronaveArg, p_agrRevisionArg, p_atrid_RevisionPasajeroArg);	

			}

			return Instance;
		}
		#endregion

		#region Service "Destroy"
		/// <summary>
		/// This method solves the logical of the service Destroy
		///// (RevisionPasajero's destruction event)
		/// </summary>		
		/// <param name="p_thisRevisionPasajeroArg">This parameter represents the inbound argument RevisionPasajero</param>
		[ONTransactionalService("RevisionPasajero", "delete_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void Delete_instanceServ(RevisionPasajeroOid p_thisRevisionPasajeroArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				RevisionPasajeroExecutive lExecutive = new RevisionPasajeroExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.Delete_instanceServ(p_thisRevisionPasajeroArg);

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion

		#region Service "Edit"
		/// <summary>
		/// This method solves the logical of the service Edit
		///// (RevisionPasajero's change event)
		/// </summary>		
		/// <param name="p_thisRevisionPasajeroArg">This parameter represents the inbound argument RevisionPasajero</param>
		[ONTransactionalService("RevisionPasajero", "edit_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void Edit_instanceServ(RevisionPasajeroOid p_thisRevisionPasajeroArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				RevisionPasajeroExecutive lExecutive = new RevisionPasajeroExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.Edit_instanceServ(p_thisRevisionPasajeroArg);

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion
	}
}
