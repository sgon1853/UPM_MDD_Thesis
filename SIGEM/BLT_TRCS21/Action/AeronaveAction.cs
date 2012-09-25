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
	/// <summary>This class solves the Events and Transactions of the class Aeronave</summary>
	[ONActionClass("Aeronave")]
	[ONInterception()]
	internal class AeronaveAction : ONAction
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
		/// <summary>Constructor</summary>
		/// <param name="onContext">This parameter has the current context</param>
		public  AeronaveAction(ONServiceContext onContext) : base(onContext, "Aeronave")
		{
		}
		#endregion Constructors
		#region Service "New"
		/// <summary>This method checks the State Transition Diagram of the class "Aeronave"</summary>
		/// <param name="p_agrPasajeroAeronaveArg">This parameter represents the inbound argument p_agrPasajeroAeronave</param>
		/// <param name="p_atrid_AeronaveArg">This parameter represents the inbound argument p_atrid_Aeronave</param>
		/// <param name="p_atrNombreArg">This parameter represents the inbound argument p_atrNombre</param>
		/// <param name="p_atrMaximoPasajerosArg">This parameter represents the inbound argument p_atrMaximoPasajeros</param>
		/// <param name="p_atrOrigenArg">This parameter represents the inbound argument p_atrOrigen</param>
		/// <param name="p_atrDestinoArg">This parameter represents the inbound argument p_atrDestino</param>
		[ONSTD("Aeronave", "create_instance")]
		public void CheckSTDiagram_create_instance(PasajeroAeronaveOid p_agrPasajeroAeronaveArg, ONInt p_atrid_AeronaveArg, ONText p_atrNombreArg, ONInt p_atrMaximoPasajerosArg, ONText p_atrOrigenArg, ONText p_atrDestinoArg)
		{
				Instance.StateObj = new ONString("Aerona0");
		}
		/// <summary>
		/// This method solves the logical of the event New
		/// Aeronave's creation event
		/// </summary>
		/// <param name="p_agrPasajeroAeronaveArg">This parameter represents the inbound argument PasajeroAeronave</param>
		/// <param name="p_atrid_AeronaveArg">This parameter represents the inbound argument id_Aeronave</param>
		/// <param name="p_atrNombreArg">This parameter represents the inbound argument Nombre</param>
		/// <param name="p_atrMaximoPasajerosArg">This parameter represents the inbound argument MaximoPasajeros</param>
		/// <param name="p_atrOrigenArg">This parameter represents the inbound argument Origen</param>
		/// <param name="p_atrDestinoArg">This parameter represents the inbound argument Destino</param>
		[ONEvent("Aeronave", "create_instance", ServiceType = ServiceTypeEnumeration.New)]
		public AeronaveInstance Create_instanceServ(
			[ONInboundArgument("Clas_1348178411520734Ser_1Arg_6_Alias", AeronaveClassText.Create_instance_P_agrPasajeroAeronaveArgumentAlias, "", "Clas_1348178411520734Ser_1_Alias", AeronaveClassText.Create_instanceServiceAlias, "Clas_1348178411520734_Alias", AeronaveClassText.ClassAlias, AllowsNull = false)] PasajeroAeronaveOid p_agrPasajeroAeronaveArg,
			[ONInboundArgument("Clas_1348178411520734Ser_1Arg_1_Alias", AeronaveClassText.Create_instance_P_atrid_AeronaveArgumentAlias, "autonumeric", "Clas_1348178411520734Ser_1_Alias", AeronaveClassText.Create_instanceServiceAlias, "Clas_1348178411520734_Alias", AeronaveClassText.ClassAlias, AllowsNull = false)] ONInt p_atrid_AeronaveArg,
			[ONInboundArgument("Clas_1348178411520734Ser_1Arg_2_Alias", AeronaveClassText.Create_instance_P_atrNombreArgumentAlias, "text", "Clas_1348178411520734Ser_1_Alias", AeronaveClassText.Create_instanceServiceAlias, "Clas_1348178411520734_Alias", AeronaveClassText.ClassAlias, AllowsNull = false)] ONText p_atrNombreArg,
			[ONInboundArgument("Clas_1348178411520734Ser_1Arg_3_Alias", AeronaveClassText.Create_instance_P_atrMaximoPasajerosArgumentAlias, "int", "Clas_1348178411520734Ser_1_Alias", AeronaveClassText.Create_instanceServiceAlias, "Clas_1348178411520734_Alias", AeronaveClassText.ClassAlias, AllowsNull = false)] ONInt p_atrMaximoPasajerosArg,
			[ONInboundArgument("Clas_1348178411520734Ser_1Arg_4_Alias", AeronaveClassText.Create_instance_P_atrOrigenArgumentAlias, "text", "Clas_1348178411520734Ser_1_Alias", AeronaveClassText.Create_instanceServiceAlias, "Clas_1348178411520734_Alias", AeronaveClassText.ClassAlias, AllowsNull = false)] ONText p_atrOrigenArg,
			[ONInboundArgument("Clas_1348178411520734Ser_1Arg_5_Alias", AeronaveClassText.Create_instance_P_atrDestinoArgumentAlias, "text", "Clas_1348178411520734Ser_1_Alias", AeronaveClassText.Create_instanceServiceAlias, "Clas_1348178411520734_Alias", AeronaveClassText.ClassAlias, AllowsNull = false)] ONText p_atrDestinoArg)
		{
			try
			{
				AeronaveData lData = new AeronaveData(OnContext);

				#region Construct OID
				Instance.Oid = new AeronaveOid();
				Instance.Oid.Id_AeronaveAttr = new ONInt(p_atrid_AeronaveArg);
				#endregion Construct OID

				#region Argument initialization 'p_atrNombre' (Nombre)
				Instance.NombreAttr = new ONText(p_atrNombreArg);
				#endregion Argument initialization 'p_atrNombre' (Nombre)

				#region Argument initialization 'p_atrMaximoPasajeros' (MaximoPasajeros)
				Instance.MaximoPasajerosAttr = new ONInt(p_atrMaximoPasajerosArg);
				#endregion Argument initialization 'p_atrMaximoPasajeros' (MaximoPasajeros)

				#region Argument initialization 'p_atrOrigen' (Origen)
				Instance.OrigenAttr = new ONText(p_atrOrigenArg);
				#endregion Argument initialization 'p_atrOrigen' (Origen)

				#region Argument initialization 'p_atrDestino' (Destino)
				Instance.DestinoAttr = new ONText(p_atrDestinoArg);
				#endregion Argument initialization 'p_atrDestino' (Destino)

            	#region Argument initialization 'p_agrPasajeroAeronave' (PasajeroAeronave)
				if (p_agrPasajeroAeronaveArg != null)
				{
					PasajeroAeronaveData lPasajeroAeronaveData = new PasajeroAeronaveData(OnContext);
					if (!lPasajeroAeronaveData.Exist(p_agrPasajeroAeronaveArg, null))
						throw new ONInstanceNotExistException(null, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.ClassAlias);
					throw new ONStaticCreationException(null, AeronaveClassText.ClassAlias, "Clas_1348178411520734_Alias", AeronaveClassText.PasajeroAeronaveRoleAlias, "Agr_1348602167296130Rol_1_Alias");
				}
        	    #endregion Argument Initialization 'p_agrPasajeroAeronave' (PasajeroAeronave)

				#region Autonumeric attribute 'id_Aeronave'
				if (Instance.Id_AeronaveAttr < new ONInt(0))
				{
					AeronaveData lAutonumericData = new AeronaveData(OnContext);
					lAutonumericData.ClassName = "Aeronave";
					//Get Autonumeric
					Instance.Oid.Id_AeronaveAttr = lAutonumericData.GetAutonumericid_Aeronave();
				}
				#endregion Autonumeric attribute 'id_Aeronave'
			
				//Search if instance exists
				if (lData.Exist(Instance.Oid, null))
					throw new ONInstanceExistException(null, "Clas_1348178411520734_Alias", AeronaveClassText.ClassAlias);
			
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
            	    if (!lInstance.AeronaveRole.Contains(Instance))
            	    {
            	    	lInstance.AeronaveRole.Add(Instance);
            	    	lInstance.AeronaveRoleOidTemp = Instance.Oid;
            	    }

	                lPasajeroAeronaveData.UpdateEdited(lInstance);
    	        }
        	    #endregion Argument Initialization 'p_agrPasajeroAeronave' (PasajeroAeronave)

				#region Cardinality check for role 'PasajeroAeronave' 
				// Minimum cardinality check
				if (Instance.PasajeroAeronaveRole == null)
					throw new ONMinCardinalityException(null, AeronaveClassText.ClassAlias, "Clas_1348178411520734_Alias", AeronaveClassText.PasajeroAeronaveRoleAlias, "Agr_1348602167296130Rol_1_Alias", 1);
				#endregion Cardinality check for role 'PasajeroAeronave'

				#region Cardinality check for role 'PasajeroAeronave'
				// Maximum cardinality check (inverse)
				foreach (PasajeroAeronaveInstance lRelatedInstance in Instance.PasajeroAeronaveRole)
					if (lRelatedInstance.AeronaveRole.Count > 1)
						throw new ONMaxCardinalityException(null, PasajeroAeronaveClassText.ClassAlias, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.AeronaveRoleAlias, "Agr_1348602167296130Rol_2_Alias", 1);
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
					string ltraceItem = "Definition class: Aeronave, Service: create_instance, Component: AeronaveAction, Method: Create_instanceServ";
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
		/// Aeronave's destruction event
		/// </summary>
		/// <param name="p_thisAeronaveArg">This parameter represents the inbound argument Aeronave</param>
		[ONEvent("Aeronave", "delete_instance", ServiceType = ServiceTypeEnumeration.Destroy)]
		public void Delete_instanceServ(
			[ONInboundArgument("Clas_1348178411520734Ser_2Arg_1_Alias", AeronaveClassText.Delete_instance_P_thisAeronaveArgumentAlias, "", "Clas_1348178411520734Ser_2_Alias", AeronaveClassText.Delete_instanceServiceAlias, "Clas_1348178411520734_Alias", AeronaveClassText.ClassAlias, AllowsNull = false)] AeronaveOid p_thisAeronaveArg)
		{
			try
			{
				AeronaveData lData = new AeronaveData(OnContext);


			// Static delete check
			if (Instance.PasajeroAeronaveRole.Count > 0)
				throw new ONStaticException(null, AeronaveClassText.ClassAlias, "Clas_1348178411520734_Alias", AeronaveClassText.PasajeroAeronaveRoleAlias, "Agr_1348602167296130Rol_1_Alias");

			// Delete relationships
			{
				AeronaveData lDataRel = new AeronaveData(OnContext);
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
					string ltraceItem = "Definition class: Aeronave, Service: delete_instance, Component: AeronaveAction, Method: Delete_instanceServ";
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
		/// Aeronave's change event
		/// </summary>
		/// <param name="p_thisAeronaveArg">This parameter represents the inbound argument Aeronave</param>
		[ONEvent("Aeronave", "edit_instance")]
		public void Edit_instanceServ(
			[ONInboundArgument("Clas_1348178411520734Ser_3Arg_1_Alias", AeronaveClassText.Edit_instance_P_thisAeronaveArgumentAlias, "", "Clas_1348178411520734Ser_3_Alias", AeronaveClassText.Edit_instanceServiceAlias, "Clas_1348178411520734_Alias", AeronaveClassText.ClassAlias, AllowsNull = false)] AeronaveOid p_thisAeronaveArg)
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
					string ltraceItem = "Definition class: Aeronave, Service: edit_instance, Component: AeronaveAction, Method: Edit_instanceServ";
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
