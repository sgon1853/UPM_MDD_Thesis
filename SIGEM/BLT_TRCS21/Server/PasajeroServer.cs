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
	[ONServerClass("Pasajero")]
	[ONInterception()]
	internal class PasajeroServer : ONServer
	{
		#region Properties
		public new PasajeroInstance Instance
		{
			get
			{
				return base.Instance as PasajeroInstance;
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
		public  PasajeroServer(ONServiceContext onContext, PasajeroInstance instance) : base(onContext, instance, "Pasajero")
		{
		}
		#endregion

		#region Service "New"
		/// <summary>
		/// This method solves the logical of the service New
		///// (Pasajero's creation event)
		/// </summary>		
		/// <param name="p_agrPasajeroAeronaveArg">This parameter represents the inbound argument PasajeroAeronave</param>
		/// <param name="p_atrid_PasajeroArg">This parameter represents the inbound argument id_Pasajero</param>
		/// <param name="p_atrNombreArg">This parameter represents the inbound argument Nombre</param>
		[ONTransactionalService("Pasajero", "create_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public PasajeroInstance Create_instanceServ(PasajeroAeronaveOid p_agrPasajeroAeronaveArg, ONInt p_atrid_PasajeroArg, ONText p_atrNombreArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
			
				// Call Executive
				PasajeroExecutive lExecutive = new PasajeroExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				Instance = lExecutive.Create_instanceServ(p_agrPasajeroAeronaveArg, p_atrid_PasajeroArg, p_atrNombreArg);	

			}

			return Instance;
		}
		#endregion

		#region Service "Destroy"
		/// <summary>
		/// This method solves the logical of the service Destroy
		///// (Pasajero's destruction event)
		/// </summary>		
		/// <param name="p_thisPasajeroArg">This parameter represents the inbound argument Pasajero</param>
		[ONTransactionalService("Pasajero", "delete_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void Delete_instanceServ(PasajeroOid p_thisPasajeroArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				PasajeroExecutive lExecutive = new PasajeroExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.Delete_instanceServ(p_thisPasajeroArg);

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion

		#region Service "Edit"
		/// <summary>
		/// This method solves the logical of the service Edit
		///// (Pasajero's change event)
		/// </summary>		
		/// <param name="p_thisPasajeroArg">This parameter represents the inbound argument Pasajero</param>
		[ONTransactionalService("Pasajero", "edit_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void Edit_instanceServ(PasajeroOid p_thisPasajeroArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				PasajeroExecutive lExecutive = new PasajeroExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.Edit_instanceServ(p_thisPasajeroArg);

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion
	}
}
