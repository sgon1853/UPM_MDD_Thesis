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
	[ONServerClass("NaveNodriza")]
	[ONInterception()]
	internal class NaveNodrizaServer : ONServer
	{
		#region Properties
		public new NaveNodrizaInstance Instance
		{
			get
			{
				return base.Instance as NaveNodrizaInstance;
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
		public  NaveNodrizaServer(ONServiceContext onContext, NaveNodrizaInstance instance) : base(onContext, instance, "NaveNodriza")
		{
		}
		#endregion

		#region Service "New"
		/// <summary>
		/// This method solves the logical of the service New
		///// (NaveNodriza's creation event)
		/// </summary>		
		/// <param name="p_atrid_NaveNodrizaArg">This parameter represents the inbound argument id_NaveNodriza</param>
		/// <param name="p_atrNombre_NaveNodrizaArg">This parameter represents the inbound argument Nombre_NaveNodriza</param>
		[ONTransactionalService("NaveNodriza", "create_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public NaveNodrizaInstance Create_instanceServ(ONInt p_atrid_NaveNodrizaArg, ONString p_atrNombre_NaveNodrizaArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
			
				// Call Executive
				NaveNodrizaExecutive lExecutive = new NaveNodrizaExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				Instance = lExecutive.Create_instanceServ(p_atrid_NaveNodrizaArg, p_atrNombre_NaveNodrizaArg);	

			}

			return Instance;
		}
		#endregion

		#region Service "Destroy"
		/// <summary>
		/// This method solves the logical of the service Destroy
		///// (NaveNodriza's destruction event)
		/// </summary>		
		/// <param name="p_thisNaveNodrizaArg">This parameter represents the inbound argument NaveNodriza</param>
		[ONTransactionalService("NaveNodriza", "delete_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void Delete_instanceServ(NaveNodrizaOid p_thisNaveNodrizaArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				NaveNodrizaExecutive lExecutive = new NaveNodrizaExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.Delete_instanceServ(p_thisNaveNodrizaArg);

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion

		#region Service "Edit"
		/// <summary>
		/// This method solves the logical of the service Edit
		///// (NaveNodriza's change event)
		/// </summary>		
		/// <param name="p_thisNaveNodrizaArg">This parameter represents the inbound argument NaveNodriza</param>
		[ONTransactionalService("NaveNodriza", "edit_instance")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Administrador")]
		public void Edit_instanceServ(NaveNodrizaOid p_thisNaveNodrizaArg)
		{
			// Create new context
			using (ONServiceContext lOnContext = new ONServiceContext(OnContext))
			{
				// Change to Transactional OnContext
				Instance.OnContext = lOnContext;
			
				// Call Executive
				NaveNodrizaExecutive lExecutive = new NaveNodrizaExecutive();
				lExecutive.OnContext = lOnContext;
				lExecutive.Instance = Instance;
				lExecutive.Edit_instanceServ(p_thisNaveNodrizaArg);

				// Change to Non-Transactional OnContext
				Instance.OnContext = OnContext;
			}
		}
		#endregion
	}
}
