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
	[ONServerClass("PasajeroAeronave")]
	[ONInterception()]
	internal class PasajeroAeronaveServer : ONServer
	{
		#region Properties
		public new PasajeroAeronaveInstance Instance
		{
			get
			{
				return base.Instance as PasajeroAeronaveInstance;
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
		public  PasajeroAeronaveServer(ONServiceContext onContext, PasajeroAeronaveInstance instance) : base(onContext, instance, "PasajeroAeronave")
		{
		}
		#endregion

		#region Service "New"
		/// <summary>
		/// This method solves the logical of the service New
		///// (PasajeroAeronave's creation event)
		/// </summary>		
		/// <param name="p_agrAeronaveArg">This parameter represents the inbound argument Aeronave</param>
		/// <param name="p_agrPasajeroArg">This parameter represents the inbound argument Pasajero</param>
		/// <param name="p_atrid_PasajeroAeronaveArg">This parameter represents the inbound argument id_PasajeroAeronave</param>
		/// <param name="p_atrNombreAeronaveArg">This parameter represents the inbound argument NombreAeronave</param>
		/// <param name="p_atrNombrePasajeroArg">This parameter represents the inbound argument NombrePasajero</param>
		[ONTransactionalService("PasajeroAeronave", "create_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public PasajeroAeronaveInstance Create_instanceServ(AeronaveOid p_agrAeronaveArg, PasajeroOid p_agrPasajeroArg, ONInt p_atrid_PasajeroAeronaveArg, ONString p_atrNombreAeronaveArg, ONString p_atrNombrePasajeroArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
			
				// Call Executive
				PasajeroAeronaveExecutive lExecutive = new PasajeroAeronaveExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				Instance = lExecutive.Create_instanceServ(p_agrAeronaveArg, p_agrPasajeroArg, p_atrid_PasajeroAeronaveArg, p_atrNombreAeronaveArg, p_atrNombrePasajeroArg);	

			}

			return Instance;
		}
		#endregion

		#region Service "Destroy"
		/// <summary>
		/// This method solves the logical of the service Destroy
		///// (PasajeroAeronave's destruction event)
		/// </summary>		
		/// <param name="p_thisPasajeroAeronaveArg">This parameter represents the inbound argument PasajeroAeronave</param>
		[ONTransactionalService("PasajeroAeronave", "delete_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void Delete_instanceServ(PasajeroAeronaveOid p_thisPasajeroAeronaveArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				PasajeroAeronaveExecutive lExecutive = new PasajeroAeronaveExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.Delete_instanceServ(p_thisPasajeroAeronaveArg);

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion

		#region Service "Edit"
		/// <summary>
		/// This method solves the logical of the service Edit
		///// (PasajeroAeronave's change event)
		/// </summary>		
		/// <param name="p_thisPasajeroAeronaveArg">This parameter represents the inbound argument PasajeroAeronave</param>
		[ONTransactionalService("PasajeroAeronave", "edit_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void Edit_instanceServ(PasajeroAeronaveOid p_thisPasajeroAeronaveArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				PasajeroAeronaveExecutive lExecutive = new PasajeroAeronaveExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.Edit_instanceServ(p_thisPasajeroAeronaveArg);

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion
	}
}
