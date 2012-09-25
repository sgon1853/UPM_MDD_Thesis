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
	/// <summary>This class solves the Events and Transactions of the class PasajeroAeronave</summary>
	[ONActionClass("PasajeroAeronave")]
	[ONInterception()]
	internal class PasajeroAeronaveAction : ONAction
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
		/// <summary>Constructor</summary>
		/// <param name="onContext">This parameter has the current context</param>
		public  PasajeroAeronaveAction(ONServiceContext onContext) : base(onContext, "PasajeroAeronave")
		{
		}
		#endregion Constructors
		#region Service "New"
		/// <summary>This method checks the State Transition Diagram of the class "PasajeroAeronave"</summary>
		/// <param name="p_agrAeronaveArg">This parameter represents the inbound argument p_agrAeronave</param>
		/// <param name="p_agrPasajeroArg">This parameter represents the inbound argument p_agrPasajero</param>
		/// <param name="p_atrid_PasajeroAeronaveArg">This parameter represents the inbound argument p_atrid_PasajeroAeronave</param>
		/// <param name="p_atrNombreAeronaveArg">This parameter represents the inbound argument p_atrNombreAeronave</param>
		/// <param name="p_atrNombrePasajeroArg">This parameter represents the inbound argument p_atrNombrePasajero</param>
		[ONSTD("PasajeroAeronave", "create_instance")]
		public void CheckSTDiagram_create_instance(AeronaveOid p_agrAeronaveArg, PasajeroOid p_agrPasajeroArg, ONInt p_atrid_PasajeroAeronaveArg, ONString p_atrNombreAeronaveArg, ONString p_atrNombrePasajeroArg)
		{
				Instance.StateObj = new ONString("Pasaje0");
		}
		/// <summary>
		/// This method solves the logical of the event New
		/// PasajeroAeronave's creation event
		/// </summary>
		/// <param name="p_agrAeronaveArg">This parameter represents the inbound argument Aeronave</param>
		/// <param name="p_agrPasajeroArg">This parameter represents the inbound argument Pasajero</param>
		/// <param name="p_atrid_PasajeroAeronaveArg">This parameter represents the inbound argument id_PasajeroAeronave</param>
		/// <param name="p_atrNombreAeronaveArg">This parameter represents the inbound argument NombreAeronave</param>
		/// <param name="p_atrNombrePasajeroArg">This parameter represents the inbound argument NombrePasajero</param>
		[ONEvent("PasajeroAeronave", "create_instance", ServiceType = ServiceTypeEnumeration.New)]
		public PasajeroAeronaveInstance Create_instanceServ(
			[ONInboundArgument("Clas_1348178542592177Ser_1Arg_5_Alias", PasajeroAeronaveClassText.Create_instance_P_agrAeronaveArgumentAlias, "", "Clas_1348178542592177Ser_1_Alias", PasajeroAeronaveClassText.Create_instanceServiceAlias, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.ClassAlias, AllowsNull = true)] AeronaveOid p_agrAeronaveArg,
			[ONInboundArgument("Clas_1348178542592177Ser_1Arg_6_Alias", PasajeroAeronaveClassText.Create_instance_P_agrPasajeroArgumentAlias, "", "Clas_1348178542592177Ser_1_Alias", PasajeroAeronaveClassText.Create_instanceServiceAlias, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.ClassAlias, AllowsNull = true)] PasajeroOid p_agrPasajeroArg,
			[ONInboundArgument("Clas_1348178542592177Ser_1Arg_1_Alias", PasajeroAeronaveClassText.Create_instance_P_atrid_PasajeroAeronaveArgumentAlias, "autonumeric", "Clas_1348178542592177Ser_1_Alias", PasajeroAeronaveClassText.Create_instanceServiceAlias, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.ClassAlias, AllowsNull = false)] ONInt p_atrid_PasajeroAeronaveArg,
			[ONInboundArgument("Clas_1348178542592177Ser_1Arg_8_Alias", PasajeroAeronaveClassText.Create_instance_P_atrNombreAeronaveArgumentAlias, "string", "Clas_1348178542592177Ser_1_Alias", PasajeroAeronaveClassText.Create_instanceServiceAlias, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.ClassAlias, Length = 100, AllowsNull = false)] ONString p_atrNombreAeronaveArg,
			[ONInboundArgument("Clas_1348178542592177Ser_1Arg_9_Alias", PasajeroAeronaveClassText.Create_instance_P_atrNombrePasajeroArgumentAlias, "string", "Clas_1348178542592177Ser_1_Alias", PasajeroAeronaveClassText.Create_instanceServiceAlias, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.ClassAlias, Length = 100, AllowsNull = false)] ONString p_atrNombrePasajeroArg)
		{
			try
			{
				PasajeroAeronaveData lData = new PasajeroAeronaveData(OnContext);

				#region Construct OID
				Instance.Oid = new PasajeroAeronaveOid();
				Instance.Oid.Id_PasajeroAeronaveAttr = new ONInt(p_atrid_PasajeroAeronaveArg);
				#endregion Construct OID

				#region Argument initialization 'p_atrNombreAeronave' (NombreAeronave)
				Instance.NombreAeronaveAttr = new ONString(p_atrNombreAeronaveArg);
				#endregion Argument initialization 'p_atrNombreAeronave' (NombreAeronave)

				#region Argument initialization 'p_atrNombrePasajero' (NombrePasajero)
				Instance.NombrePasajeroAttr = new ONString(p_atrNombrePasajeroArg);
				#endregion Argument initialization 'p_atrNombrePasajero' (NombrePasajero)

            	#region Argument initialization 'p_agrAeronave' (Aeronave)
				if (p_agrAeronaveArg != null)
				{
					AeronaveData lAeronaveData = new AeronaveData(OnContext);
					if (!lAeronaveData.Exist(p_agrAeronaveArg, null))
						throw new ONInstanceNotExistException(null, "Clas_1348178411520734_Alias", AeronaveClassText.ClassAlias);

					Instance.AeronaveRole = null;
					Instance.AeronaveRoleOidTemp = p_agrAeronaveArg;

					// Maximum cardinality check (inverse role)
					if (p_agrAeronaveArg.GetInstance(OnContext).PasajeroAeronaveRole.Count >= 1)
						throw new ONMaxCardinalityException(null, AeronaveClassText.ClassAlias, "Clas_1348178411520734_Alias", AeronaveClassText.PasajeroAeronaveRoleAlias, "Agr_1348602167296130Rol_1_Alias", 1);
				}
        	    #endregion Argument Initialization 'p_agrAeronave' (Aeronave)

            	#region Argument initialization 'p_agrPasajero' (Pasajero)
				if (p_agrPasajeroArg != null)
				{
					PasajeroData lPasajeroData = new PasajeroData(OnContext);
					if (!lPasajeroData.Exist(p_agrPasajeroArg, null))
						throw new ONInstanceNotExistException(null, "Clas_1348178542592658_Alias", PasajeroClassText.ClassAlias);

					Instance.PasajeroRole = null;
					Instance.PasajeroRoleOidTemp = p_agrPasajeroArg;

					// Maximum cardinality check (inverse role)
					if (p_agrPasajeroArg.GetInstance(OnContext).PasajeroAeronaveRole.Count >= 1)
						throw new ONMaxCardinalityException(null, PasajeroClassText.ClassAlias, "Clas_1348178542592658_Alias", PasajeroClassText.PasajeroAeronaveRoleAlias, "Agr_1348602429440718Rol_1_Alias", 1);
				}
        	    #endregion Argument Initialization 'p_agrPasajero' (Pasajero)

				#region Autonumeric attribute 'id_PasajeroAeronave'
				if (Instance.Id_PasajeroAeronaveAttr < new ONInt(0))
				{
					PasajeroAeronaveData lAutonumericData = new PasajeroAeronaveData(OnContext);
					lAutonumericData.ClassName = "PasajeroAeronave";
					//Get Autonumeric
					Instance.Oid.Id_PasajeroAeronaveAttr = lAutonumericData.GetAutonumericid_PasajeroAeronave();
				}
				#endregion Autonumeric attribute 'id_PasajeroAeronave'
			
				//Search if instance exists
				if (lData.Exist(Instance.Oid, null))
					throw new ONInstanceExistException(null, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.ClassAlias);
			
				//Update the new instance
				lData.UpdateAdded(Instance);
			
				#region Cardinality check for role 'Aeronave'
				// Maximum cardinality check (inverse)
				foreach (AeronaveInstance lRelatedInstance in Instance.AeronaveRole)
					if (lRelatedInstance.PasajeroAeronaveRole.Count > 1)
						throw new ONMaxCardinalityException(null, AeronaveClassText.ClassAlias, "Clas_1348178411520734_Alias", AeronaveClassText.PasajeroAeronaveRoleAlias, "Agr_1348602167296130Rol_1_Alias", 1);
				#endregion Cardinality check for role 'Aeronave'

				#region Cardinality check for role 'Pasajero'
				// Maximum cardinality check (inverse)
				foreach (PasajeroInstance lRelatedInstance in Instance.PasajeroRole)
					if (lRelatedInstance.PasajeroAeronaveRole.Count > 1)
						throw new ONMaxCardinalityException(null, PasajeroClassText.ClassAlias, "Clas_1348178542592658_Alias", PasajeroClassText.PasajeroAeronaveRoleAlias, "Agr_1348602429440718Rol_1_Alias", 1);
				#endregion Cardinality check for role 'Pasajero'
			}
			catch (Exception e)
			{
				if (e is ONException)
				{
					throw e;
				}
				else
				{
					string ltraceItem = "Definition class: PasajeroAeronave, Service: create_instance, Component: PasajeroAeronaveAction, Method: Create_instanceServ";
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
		/// PasajeroAeronave's destruction event
		/// </summary>
		/// <param name="p_thisPasajeroAeronaveArg">This parameter represents the inbound argument PasajeroAeronave</param>
		[ONEvent("PasajeroAeronave", "delete_instance", ServiceType = ServiceTypeEnumeration.Destroy)]
		public void Delete_instanceServ(
			[ONInboundArgument("Clas_1348178542592177Ser_2Arg_1_Alias", PasajeroAeronaveClassText.Delete_instance_P_thisPasajeroAeronaveArgumentAlias, "", "Clas_1348178542592177Ser_2_Alias", PasajeroAeronaveClassText.Delete_instanceServiceAlias, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.ClassAlias, AllowsNull = false)] PasajeroAeronaveOid p_thisPasajeroAeronaveArg)
		{
			try
			{
				PasajeroAeronaveData lData = new PasajeroAeronaveData(OnContext);


			#region Cardinality check for role 'Aeronave'
			// Minimum cardinality check (inverse)
			if (Instance.AeronaveRole.Count > 0)
				throw new ONMinCardinalityException(null, AeronaveClassText.ClassAlias, "Clas_1348178411520734_Alias", AeronaveClassText.PasajeroAeronaveRoleAlias, "Agr_1348602167296130Rol_1_Alias", 1);
			#endregion  Cardinality check for role 'Aeronave'

			#region Cardinality check for role 'Pasajero'
			// Minimum cardinality check (inverse)
			if (Instance.PasajeroRole.Count > 0)
				throw new ONMinCardinalityException(null, PasajeroClassText.ClassAlias, "Clas_1348178542592658_Alias", PasajeroClassText.PasajeroAeronaveRoleAlias, "Agr_1348602429440718Rol_1_Alias", 1);
			#endregion  Cardinality check for role 'Pasajero'

			#region Cardinality check for role 'RevisionPasajero'
			// Minimum cardinality check (inverse)
			if (Instance.RevisionPasajeroRole.Count > 0)
				throw new ONMinCardinalityException(null, RevisionPasajeroClassText.ClassAlias, "Clas_1348178673664478_Alias", RevisionPasajeroClassText.PasajeroAeronaveRoleAlias, "Agr_1348602167296649Rol_2_Alias", 1);
			#endregion Cardinality check for role 'RevisionPasajero'

			// Delete relationships
			{
				PasajeroAeronaveData lDataRel = new PasajeroAeronaveData(OnContext);
				lDataRel.RevisionPasajeroRoleDelete(Instance.Oid);
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
					string ltraceItem = "Definition class: PasajeroAeronave, Service: delete_instance, Component: PasajeroAeronaveAction, Method: Delete_instanceServ";
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
		/// PasajeroAeronave's change event
		/// </summary>
		/// <param name="p_thisPasajeroAeronaveArg">This parameter represents the inbound argument PasajeroAeronave</param>
		[ONEvent("PasajeroAeronave", "edit_instance")]
		public void Edit_instanceServ(
			[ONInboundArgument("Clas_1348178542592177Ser_3Arg_1_Alias", PasajeroAeronaveClassText.Edit_instance_P_thisPasajeroAeronaveArgumentAlias, "", "Clas_1348178542592177Ser_3_Alias", PasajeroAeronaveClassText.Edit_instanceServiceAlias, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.ClassAlias, AllowsNull = false)] PasajeroAeronaveOid p_thisPasajeroAeronaveArg)
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
					string ltraceItem = "Definition class: PasajeroAeronave, Service: edit_instance, Component: PasajeroAeronaveAction, Method: Edit_instanceServ";
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
