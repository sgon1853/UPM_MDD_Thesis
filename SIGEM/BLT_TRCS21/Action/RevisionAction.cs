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
	/// <summary>This class solves the Events and Transactions of the class Revision</summary>
	[ONActionClass("Revision")]
	[ONInterception()]
	internal class RevisionAction : ONAction
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
		/// <summary>Constructor</summary>
		/// <param name="onContext">This parameter has the current context</param>
		public  RevisionAction(ONServiceContext onContext) : base(onContext, "Revision")
		{
		}
		#endregion Constructors
		#region Service "New"
		/// <summary>This method checks the State Transition Diagram of the class "Revision"</summary>
		/// <param name="p_atrid_RevisarAeronaveArg">This parameter represents the inbound argument p_atrid_RevisarAeronave</param>
		/// <param name="p_atrFechaRevisionArg">This parameter represents the inbound argument p_atrFechaRevision</param>
		/// <param name="p_atrNombreRevisorArg">This parameter represents the inbound argument p_atrNombreRevisor</param>
		/// <param name="p_atrId_AeronaveArg">This parameter represents the inbound argument p_atrId_Aeronave</param>
		[ONSTD("Revision", "create_instance")]
		public void CheckSTDiagram_create_instance(ONInt p_atrid_RevisarAeronaveArg, ONDate p_atrFechaRevisionArg, ONString p_atrNombreRevisorArg, ONString p_atrId_AeronaveArg)
		{
				Instance.StateObj = new ONString("Revisi0");
		}
		/// <summary>
		/// This method solves the logical of the event New
		/// RevisarAeronave's creation event
		/// </summary>
		/// <param name="p_atrid_RevisarAeronaveArg">This parameter represents the inbound argument id_RevisarAeronave</param>
		/// <param name="p_atrFechaRevisionArg">This parameter represents the inbound argument FechaRevision</param>
		/// <param name="p_atrNombreRevisorArg">This parameter represents the inbound argument NombreRevisor</param>
		/// <param name="p_atrId_AeronaveArg">This parameter represents the inbound argument Id_Aeronave</param>
		[ONEvent("Revision", "create_instance", ServiceType = ServiceTypeEnumeration.New)]
		public RevisionInstance Create_instanceServ(
			[ONInboundArgument("Clas_1348178542592347Ser_1Arg_1_Alias", RevisionClassText.Create_instance_P_atrid_RevisarAeronaveArgumentAlias, "autonumeric", "Clas_1348178542592347Ser_1_Alias", RevisionClassText.Create_instanceServiceAlias, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias, AllowsNull = false)] ONInt p_atrid_RevisarAeronaveArg,
			[ONInboundArgument("Clas_1348178542592347Ser_1Arg_3_Alias", RevisionClassText.Create_instance_P_atrFechaRevisionArgumentAlias, "date", "Clas_1348178542592347Ser_1_Alias", RevisionClassText.Create_instanceServiceAlias, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias, AllowsNull = false)] ONDate p_atrFechaRevisionArg,
			[ONInboundArgument("Clas_1348178542592347Ser_1Arg_4_Alias", RevisionClassText.Create_instance_P_atrNombreRevisorArgumentAlias, "string", "Clas_1348178542592347Ser_1_Alias", RevisionClassText.Create_instanceServiceAlias, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias, Length = 100, AllowsNull = false)] ONString p_atrNombreRevisorArg,
			[ONInboundArgument("Clas_1348178542592347Ser_1Arg_5_Alias", RevisionClassText.Create_instance_P_atrId_AeronaveArgumentAlias, "string", "Clas_1348178542592347Ser_1_Alias", RevisionClassText.Create_instanceServiceAlias, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias, Length = 100, AllowsNull = false)] ONString p_atrId_AeronaveArg)
		{
			try
			{
				RevisionData lData = new RevisionData(OnContext);

				#region Construct OID
				Instance.Oid = new RevisionOid();
				Instance.Oid.Id_RevisarAeronaveAttr = new ONInt(p_atrid_RevisarAeronaveArg);
				#endregion Construct OID

				#region Argument initialization 'p_atrFechaRevision' (FechaRevision)
				Instance.FechaRevisionAttr = new ONDate(p_atrFechaRevisionArg);
				#endregion Argument initialization 'p_atrFechaRevision' (FechaRevision)

				#region Argument initialization 'p_atrNombreRevisor' (NombreRevisor)
				Instance.NombreRevisorAttr = new ONString(p_atrNombreRevisorArg);
				#endregion Argument initialization 'p_atrNombreRevisor' (NombreRevisor)

				#region Argument initialization 'p_atrId_Aeronave' (Id_Aeronave)
				Instance.Id_AeronaveAttr = new ONString(p_atrId_AeronaveArg);
				#endregion Argument initialization 'p_atrId_Aeronave' (Id_Aeronave)

				#region Autonumeric attribute 'id_RevisarAeronave'
				if (Instance.Id_RevisarAeronaveAttr < new ONInt(0))
				{
					RevisionData lAutonumericData = new RevisionData(OnContext);
					lAutonumericData.ClassName = "Revision";
					//Get Autonumeric
					Instance.Oid.Id_RevisarAeronaveAttr = lAutonumericData.GetAutonumericid_RevisarAeronave();
				}
				#endregion Autonumeric attribute 'id_RevisarAeronave'
			
				//Search if instance exists
				if (lData.Exist(Instance.Oid, null))
					throw new ONInstanceExistException(null, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias);
			
				//Update the new instance
				lData.UpdateAdded(Instance);
			
			}
			catch (Exception e)
			{
				if (e is ONException)
				{
					throw e;
				}
				else
				{
					string ltraceItem = "Definition class: Revision, Service: create_instance, Component: RevisionAction, Method: Create_instanceServ";
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
		/// RevisarAeronave's destruction event
		/// </summary>
		/// <param name="p_thisRevisarAeronaveArg">This parameter represents the inbound argument Revision</param>
		[ONEvent("Revision", "delete_instance", ServiceType = ServiceTypeEnumeration.Destroy)]
		public void Delete_instanceServ(
			[ONInboundArgument("Clas_1348178542592347Ser_2Arg_1_Alias", RevisionClassText.Delete_instance_P_thisRevisarAeronaveArgumentAlias, "", "Clas_1348178542592347Ser_2_Alias", RevisionClassText.Delete_instanceServiceAlias, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias, AllowsNull = false)] RevisionOid p_thisRevisarAeronaveArg)
		{
			try
			{
				RevisionData lData = new RevisionData(OnContext);


			#region Cardinality check for role 'RevisionPasajero'
			// Minimum cardinality check (inverse)
			if (Instance.RevisionPasajeroRole.Count > 0)
				throw new ONMinCardinalityException(null, RevisionPasajeroClassText.ClassAlias, "Clas_1348178673664478_Alias", RevisionPasajeroClassText.RevisionRoleAlias, "Agr_1348602167296276Rol_2_Alias", 1);
			#endregion Cardinality check for role 'RevisionPasajero'

			// Delete relationships
			{
				RevisionData lDataRel = new RevisionData(OnContext);
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
					string ltraceItem = "Definition class: Revision, Service: delete_instance, Component: RevisionAction, Method: Delete_instanceServ";
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
		/// RevisarAeronave's change event
		/// </summary>
		/// <param name="p_thisRevisarAeronaveArg">This parameter represents the inbound argument Revision</param>
		[ONEvent("Revision", "edit_instance")]
		public void Edit_instanceServ(
			[ONInboundArgument("Clas_1348178542592347Ser_3Arg_1_Alias", RevisionClassText.Edit_instance_P_thisRevisarAeronaveArgumentAlias, "", "Clas_1348178542592347Ser_3_Alias", RevisionClassText.Edit_instanceServiceAlias, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias, AllowsNull = false)] RevisionOid p_thisRevisarAeronaveArg)
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
					string ltraceItem = "Definition class: Revision, Service: edit_instance, Component: RevisionAction, Method: Edit_instanceServ";
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
