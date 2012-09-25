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
	[ONServerClass("Aeronave")]
	[ONInterception()]
	internal class AeronaveServer : ONServer
	{
		#region Properties
		public new AeronaveInstance Instance
		{
			get
			{
				return base.Instance as AeronaveInstance;
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
		public  AeronaveServer(ONServiceContext onContext, AeronaveInstance instance) : base(onContext, instance, "Aeronave")
		{
		}
		#endregion

		#region Service "New"
		/// <summary>
		/// This method solves the logical of the service New
		///// (Aeronave's creation event)
		/// </summary>		
		/// <param name="p_agrPasajeroAeronaveArg">This parameter represents the inbound argument PasajeroAeronave</param>
		/// <param name="p_atrid_AeronaveArg">This parameter represents the inbound argument id_Aeronave</param>
		/// <param name="p_atrNombreArg">This parameter represents the inbound argument Nombre</param>
		/// <param name="p_atrMaximoPasajerosArg">This parameter represents the inbound argument MaximoPasajeros</param>
		/// <param name="p_atrOrigenArg">This parameter represents the inbound argument Origen</param>
		/// <param name="p_atrDestinoArg">This parameter represents the inbound argument Destino</param>
		[ONTransactionalService("Aeronave", "create_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public AeronaveInstance Create_instanceServ(PasajeroAeronaveOid p_agrPasajeroAeronaveArg, ONInt p_atrid_AeronaveArg, ONText p_atrNombreArg, ONInt p_atrMaximoPasajerosArg, ONText p_atrOrigenArg, ONText p_atrDestinoArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
			
				// Call Executive
				AeronaveExecutive lExecutive = new AeronaveExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				Instance = lExecutive.Create_instanceServ(p_agrPasajeroAeronaveArg, p_atrid_AeronaveArg, p_atrNombreArg, p_atrMaximoPasajerosArg, p_atrOrigenArg, p_atrDestinoArg);	

			}

			return Instance;
		}
		#endregion

		#region Service "Destroy"
		/// <summary>
		/// This method solves the logical of the service Destroy
		///// (Aeronave's destruction event)
		/// </summary>		
		/// <param name="p_thisAeronaveArg">This parameter represents the inbound argument Aeronave</param>
		[ONTransactionalService("Aeronave", "delete_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void Delete_instanceServ(AeronaveOid p_thisAeronaveArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				AeronaveExecutive lExecutive = new AeronaveExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.Delete_instanceServ(p_thisAeronaveArg);

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion

		#region Service "Edit"
		/// <summary>
		/// This method solves the logical of the service Edit
		///// (Aeronave's change event)
		/// </summary>		
		/// <param name="p_thisAeronaveArg">This parameter represents the inbound argument Aeronave</param>
		[ONTransactionalService("Aeronave", "edit_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void Edit_instanceServ(AeronaveOid p_thisAeronaveArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				AeronaveExecutive lExecutive = new AeronaveExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.Edit_instanceServ(p_thisAeronaveArg);

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion
	}
}
