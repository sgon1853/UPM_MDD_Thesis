// 3.4.4.5
using System;
using SIGEM.Business.XML;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Instance;
using SIGEM.Business.Query;
using SIGEM.Business.Attributes;
using SIGEM.Business.Exceptions;
using SIGEM.Business.Data;
using SIGEM.Business.Server;
using SIGEM.Business.Executive;
using SIGEM.Business.Collection;
using System.Security.Permissions;
using System.EnterpriseServices;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;

namespace SIGEM.Business.Action
{
	/// <summary>This class solves the Events and Transactions of the class Pasajero</summary>
	[ONActionClass("Pasajero")]
	[ONInterception()]
	internal class PasajeroAction : ONAction
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
		/// <summary>Constructor</summary>
		/// <param name="onContext">This parameter has the current context</param>
		public  PasajeroAction(ONServiceContext onContext) : base(onContext, "Pasajero")
		{
		}
		#endregion Constructors
		#region Service "New"
		/// <summary>This method checks the State Transition Diagram of the class "Pasajero"</summary>
		/// <param name="p_agrPasajeroAeronaveArg">This parameter represents the inbound argument p_agrPasajeroAeronave</param>
		/// <param name="p_atrid_PasajeroArg">This parameter represents the inbound argument p_atrid_Pasajero</param>
		/// <param name="p_atrNombreArg">This parameter represents the inbound argument p_atrNombre</param>
		[ONSTD("Pasajero", "create_instance")]
		public void CheckSTDiagram_create_instance(PasajeroAeronaveOid p_agrPasajeroAeronaveArg, ONInt p_atrid_PasajeroArg, ONText p_atrNombreArg)
		{
				Instance.StateObj = new ONString("Pasaje0");
		}
		/// <summary>
		/// This method solves the logical of the event New
		/// Pasajero's creation event
		/// </summary>
		/// <param name="p_agrPasajeroAeronaveArg">This parameter represents the inbound argument PasajeroAeronave</param>
		/// <param name="p_atrid_PasajeroArg">This parameter represents the inbound argument id_Pasajero</param>
		/// <param name="p_atrNombreArg">This parameter represents the inbound argument Nombre</param>
		[ONEvent("Pasajero", "create_instance", ServiceType = ServiceTypeEnumeration.New)]
		public PasajeroInstance Create_instanceServ(
			[ONInboundArgument("Clas_1348178542592658Ser_1Arg_4_Alias", PasajeroClassText.Create_instance_P_agrPasajeroAeronaveArgumentAlias, "", "Clas_1348178542592658Ser_1_Alias", PasajeroClassText.Create_instanceServiceAlias, "Clas_1348178542592658_Alias", PasajeroClassText.ClassAlias, AllowsNull = false)] PasajeroAeronaveOid p_agrPasajeroAeronaveArg,
			[ONInboundArgument("Clas_1348178542592658Ser_1Arg_1_Alias", PasajeroClassText.Create_instance_P_atrid_PasajeroArgumentAlias, "autonumeric", "Clas_1348178542592658Ser_1_Alias", PasajeroClassText.Create_instanceServiceAlias, "Clas_1348178542592658_Alias", PasajeroClassText.ClassAlias, AllowsNull = false)] ONInt p_atrid_PasajeroArg,
			[ONInboundArgument("Clas_1348178542592658Ser_1Arg_2_Alias", PasajeroClassText.Create_instance_P_atrNombreArgumentAlias, "text", "Clas_1348178542592658Ser_1_Alias", PasajeroClassText.Create_instanceServiceAlias, "Clas_1348178542592658_Alias", PasajeroClassText.ClassAlias, AllowsNull = false)] ONText p_atrNombreArg)
		{
			try
			{
				PasajeroData lData = new PasajeroData(OnContext);

				#region Construct OID
				Instance.Oid = new PasajeroOid();
				Instance.Oid.Id_PasajeroAttr = new ONInt(p_atrid_PasajeroArg);
				#endregion Construct OID

				#region Argument initialization 'p_atrNombre' (Nombre)
				Instance.NombreAttr = new ONText(p_atrNombreArg);
				#endregion Argument initialization 'p_atrNombre' (Nombre)

            	#region Argument initialization 'p_agrPasajeroAeronave' (PasajeroAeronave)
				if (p_agrPasajeroAeronaveArg != null)
				{
					PasajeroAeronaveData lPasajeroAeronaveData = new PasajeroAeronaveData(OnContext);
					if (!lPasajeroAeronaveData.Exist(p_agrPasajeroAeronaveArg, null))
						throw new ONInstanceNotExistException(null, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.ClassAlias);
					throw new ONStaticCreationException(null, PasajeroClassText.ClassAlias, "Clas_1348178542592658_Alias", PasajeroClassText.PasajeroAeronaveRoleAlias, "Agr_1348602429440718Rol_1_Alias");
				}
        	    #endregion Argument Initialization 'p_agrPasajeroAeronave' (PasajeroAeronave)

				#region Autonumeric attribute 'id_Pasajero'
				if (Instance.Id_PasajeroAttr < new ONInt(0))
				{
					PasajeroData lAutonumericData = new PasajeroData(OnContext);
					lAutonumericData.ClassName = "Pasajero";
					//Get Autonumeric
					Instance.Oid.Id_PasajeroAttr = lAutonumericData.GetAutonumericid_Pasajero();
				}
				#endregion Autonumeric attribute 'id_Pasajero'
			
				//Search if instance exists
				if (lData.Exist(Instance.Oid, null))
					throw new ONInstanceExistException(null, "Clas_1348178542592658_Alias", PasajeroClassText.ClassAlias);
			
				//Update the new instance
				lData.UpdateAdded(Instance);
			
            	#region Argument initialization 'p_agrPasajeroAeronave' (PasajeroAeronave)
            	if (p_agrPasajeroAeronaveArg != null)
            	{
            		PasajeroAeronaveData lPasajeroAeronaveData = new PasajeroAeronaveData(OnContext);
            		if (!lPasajeroAeronaveData.Exist(p_agrPasajeroAeronaveArg, null))
            			throw new ONInstanceNotExistException(null, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.ClassAlias);
            		PasajeroAeronaveInstance lInstance = p_agrPasajeroAeronaveArg.GetInstance(OnContext);

            	    Instance.PasajeroAeronaveRole.Add(lInstance);
            	    if (!lInstance.PasajeroRole.Contains(Instance))
            	    {
            	    	lInstance.PasajeroRole.Add(Instance);
            	    	lInstance.PasajeroRoleOidTemp = Instance.Oid;
            	    }

	                lPasajeroAeronaveData.UpdateEdited(lInstance);
    	        }
        	    #endregion Argument Initialization 'p_agrPasajeroAeronave' (PasajeroAeronave)

				#region Cardinality check for role 'PasajeroAeronave' 
				// Minimum cardinality check
				if (Instance.PasajeroAeronaveRole == null)
					throw new ONMinCardinalityException(null, PasajeroClassText.ClassAlias, "Clas_1348178542592658_Alias", PasajeroClassText.PasajeroAeronaveRoleAlias, "Agr_1348602429440718Rol_1_Alias", 1);
				#endregion Cardinality check for role 'PasajeroAeronave'

				#region Cardinality check for role 'PasajeroAeronave'
				// Maximum cardinality check (inverse)
				foreach (PasajeroAeronaveInstance lRelatedInstance in Instance.PasajeroAeronaveRole)
					if (lRelatedInstance.PasajeroRole.Count > 1)
						throw new ONMaxCardinalityException(null, PasajeroAeronaveClassText.ClassAlias, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.PasajeroRoleAlias, "Agr_1348602429440718Rol_2_Alias", 1);
				#endregion Cardinality check for role 'PasajeroAeronave'
			}
			catch (Exception e)
			{
				if (e is ONException)
				{
					throw e;
				}
				else
				{
					string ltraceItem = "Definition class: Pasajero, Service: create_instance, Component: PasajeroAction, Method: Create_instanceServ";
      				if (e is ONSystemException)
      				{
      					ONSystemException lException = e as ONSystemException;
            			lException.addTraceInformation(ltraceItem);
            			throw lException;
					}
      				throw new ONSystemException(e, ltraceItem);
      			}
      		}

			return Instance;

		}
		#endregion
		#region Service "Destroy"
		/// <summary>
		/// This method solves the logical of the event Destroy
		/// Pasajero's destruction event
		/// </summary>
		/// <param name="p_thisPasajeroArg">This parameter represents the inbound argument Pasajero</param>
		[ONEvent("Pasajero", "delete_instance", ServiceType = ServiceTypeEnumeration.Destroy)]
		public void Delete_instanceServ(
			[ONInboundArgument("Clas_1348178542592658Ser_2Arg_1_Alias", PasajeroClassText.Delete_instance_P_thisPasajeroArgumentAlias, "", "Clas_1348178542592658Ser_2_Alias", PasajeroClassText.Delete_instanceServiceAlias, "Clas_1348178542592658_Alias", PasajeroClassText.ClassAlias, AllowsNull = false)] PasajeroOid p_thisPasajeroArg)
		{
			try
			{
				PasajeroData lData = new PasajeroData(OnContext);


			// Static delete check
			if (Instance.PasajeroAeronaveRole.Count > 0)
				throw new ONStaticException(null, PasajeroClassText.ClassAlias, "Clas_1348178542592658_Alias", PasajeroClassText.PasajeroAeronaveRoleAlias, "Agr_1348602429440718Rol_1_Alias");

			// Delete relationships
			{
				PasajeroData lDataRel = new PasajeroData(OnContext);
				lDataRel.PasajeroAeronaveRoleDelete(Instance.Oid);
			}

			// Delete instance
			lData.UpdateDeleted(Instance);
			}
			catch (Exception e)
			{
				if (e is ONException)
				{
					throw e;
				}
				else
				{
					string ltraceItem = "Definition class: Pasajero, Service: delete_instance, Component: PasajeroAction, Method: Delete_instanceServ";
      				if (e is ONSystemException)
      				{
      					ONSystemException lException = e as ONSystemException;
            			lException.addTraceInformation(ltraceItem);
            			throw lException;
					}
      				throw new ONSystemException(e, ltraceItem);
      			}
      		}
		}
		#endregion
		#region Service "Edit"
		/// <summary>
		/// This method solves the logical of the event Edit
		/// Pasajero's change event
		/// </summary>
		/// <param name="p_thisPasajeroArg">This parameter represents the inbound argument Pasajero</param>
		[ONEvent("Pasajero", "edit_instance")]
		public void Edit_instanceServ(
			[ONInboundArgument("Clas_1348178542592658Ser_3Arg_1_Alias", PasajeroClassText.Edit_instance_P_thisPasajeroArgumentAlias, "", "Clas_1348178542592658Ser_3_Alias", PasajeroClassText.Edit_instanceServiceAlias, "Clas_1348178542592658_Alias", PasajeroClassText.ClassAlias, AllowsNull = false)] PasajeroOid p_thisPasajeroArg)
		{
			try
			{
			}
			catch (Exception e)
			{
				if (e is ONException)
				{
					throw e;
				}
				else
				{
					string ltraceItem = "Definition class: Pasajero, Service: edit_instance, Component: PasajeroAction, Method: Edit_instanceServ";
      				if (e is ONSystemException)
      				{
      					ONSystemException lException = e as ONSystemException;
            			lException.addTraceInformation(ltraceItem);
            			throw lException;
					}
      				throw new ONSystemException(e, ltraceItem);
      			}
      		}
		}
		#endregion
	}
}
