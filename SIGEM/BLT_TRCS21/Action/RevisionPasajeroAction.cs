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
	/// <summary>This class solves the Events and Transactions of the class RevisionPasajero</summary>
	[ONActionClass("RevisionPasajero")]
	[ONInterception()]
	internal class RevisionPasajeroAction : ONAction
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
		/// <summary>Constructor</summary>
		/// <param name="onContext">This parameter has the current context</param>
		public  RevisionPasajeroAction(ONServiceContext onContext) : base(onContext, "RevisionPasajero")
		{
		}
		#endregion Constructors
		#region Service "New"
		/// <summary>This method checks the State Transition Diagram of the class "RevisionPasajero"</summary>
		/// <param name="p_agrPasajeroAeronaveArg">This parameter represents the inbound argument p_agrPasajeroAeronave</param>
		/// <param name="p_agrRevisionArg">This parameter represents the inbound argument p_agrRevision</param>
		/// <param name="p_atrid_RevisionPasajeroArg">This parameter represents the inbound argument p_atrid_RevisionPasajero</param>
		[ONSTD("RevisionPasajero", "create_instance")]
		public void CheckSTDiagram_create_instance(PasajeroAeronaveOid p_agrPasajeroAeronaveArg, RevisionOid p_agrRevisionArg, ONInt p_atrid_RevisionPasajeroArg)
		{
				Instance.StateObj = new ONString("Revisi0");
		}
		/// <summary>
		/// This method solves the logical of the event New
		/// RevisionPasajero's creation event
		/// </summary>
		/// <param name="p_agrPasajeroAeronaveArg">This parameter represents the inbound argument PasajeroAeronave</param>
		/// <param name="p_agrRevisionArg">This parameter represents the inbound argument Revision</param>
		/// <param name="p_atrid_RevisionPasajeroArg">This parameter represents the inbound argument id_RevisionPasajero</param>
		[ONEvent("RevisionPasajero", "create_instance", ServiceType = ServiceTypeEnumeration.New)]
		public RevisionPasajeroInstance Create_instanceServ(
			[ONInboundArgument("Clas_1348178673664478Ser_1Arg_3_Alias", RevisionPasajeroClassText.Create_instance_P_agrPasajeroAeronaveArgumentAlias, "", "Clas_1348178673664478Ser_1_Alias", RevisionPasajeroClassText.Create_instanceServiceAlias, "Clas_1348178673664478_Alias", RevisionPasajeroClassText.ClassAlias, AllowsNull = false)] PasajeroAeronaveOid p_agrPasajeroAeronaveArg,
			[ONInboundArgument("Clas_1348178673664478Ser_1Arg_4_Alias", RevisionPasajeroClassText.Create_instance_P_agrRevisionArgumentAlias, "", "Clas_1348178673664478Ser_1_Alias", RevisionPasajeroClassText.Create_instanceServiceAlias, "Clas_1348178673664478_Alias", RevisionPasajeroClassText.ClassAlias, AllowsNull = false)] RevisionOid p_agrRevisionArg,
			[ONInboundArgument("Clas_1348178673664478Ser_1Arg_1_Alias", RevisionPasajeroClassText.Create_instance_P_atrid_RevisionPasajeroArgumentAlias, "autonumeric", "Clas_1348178673664478Ser_1_Alias", RevisionPasajeroClassText.Create_instanceServiceAlias, "Clas_1348178673664478_Alias", RevisionPasajeroClassText.ClassAlias, AllowsNull = false)] ONInt p_atrid_RevisionPasajeroArg)
		{
			try
			{
				RevisionPasajeroData lData = new RevisionPasajeroData(OnContext);

				#region Construct OID
				Instance.Oid = new RevisionPasajeroOid();
				Instance.Oid.Id_RevisionPasajeroAttr = new ONInt(p_atrid_RevisionPasajeroArg);
				#endregion Construct OID

            	#region Argument initialization 'p_agrPasajeroAeronave' (PasajeroAeronave)
				if (p_agrPasajeroAeronaveArg != null)
				{
					PasajeroAeronaveData lPasajeroAeronaveData = new PasajeroAeronaveData(OnContext);
					if (!lPasajeroAeronaveData.Exist(p_agrPasajeroAeronaveArg, null))
						throw new ONInstanceNotExistException(null, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.ClassAlias);

					Instance.PasajeroAeronaveRole = null;
					Instance.PasajeroAeronaveRoleOidTemp = p_agrPasajeroAeronaveArg;

					// Maximum cardinality check (inverse role)
					if (p_agrPasajeroAeronaveArg.GetInstance(OnContext).RevisionPasajeroRole.Count >= 1)
						throw new ONMaxCardinalityException(null, PasajeroAeronaveClassText.ClassAlias, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.RevisionPasajeroRoleAlias, "Agr_1348602167296649Rol_1_Alias", 1);
				}
        	    #endregion Argument Initialization 'p_agrPasajeroAeronave' (PasajeroAeronave)

            	#region Argument initialization 'p_agrRevision' (Revision)
				if (p_agrRevisionArg != null)
				{
					RevisionData lRevisionData = new RevisionData(OnContext);
					if (!lRevisionData.Exist(p_agrRevisionArg, null))
						throw new ONInstanceNotExistException(null, "Clas_1348178542592347_Alias", RevisionClassText.ClassAlias);

					Instance.RevisionRole = null;
					Instance.RevisionRoleOidTemp = p_agrRevisionArg;

					// Maximum cardinality check (inverse role)
					if (p_agrRevisionArg.GetInstance(OnContext).RevisionPasajeroRole.Count >= 1)
						throw new ONMaxCardinalityException(null, RevisionClassText.ClassAlias, "Clas_1348178542592347_Alias", RevisionClassText.RevisionPasajeroRoleAlias, "Agr_1348602167296276Rol_1_Alias", 1);
				}
        	    #endregion Argument Initialization 'p_agrRevision' (Revision)

				#region Autonumeric attribute 'id_RevisionPasajero'
				if (Instance.Id_RevisionPasajeroAttr < new ONInt(0))
				{
					RevisionPasajeroData lAutonumericData = new RevisionPasajeroData(OnContext);
					lAutonumericData.ClassName = "RevisionPasajero";
					//Get Autonumeric
					Instance.Oid.Id_RevisionPasajeroAttr = lAutonumericData.GetAutonumericid_RevisionPasajero();
				}
				#endregion Autonumeric attribute 'id_RevisionPasajero'
			
				//Search if instance exists
				if (lData.Exist(Instance.Oid, null))
					throw new ONInstanceExistException(null, "Clas_1348178673664478_Alias", RevisionPasajeroClassText.ClassAlias);
			
				//Update the new instance
				lData.UpdateAdded(Instance);
			
				#region Cardinality check for role 'PasajeroAeronave' 
				// Minimum cardinality check
				if (Instance.PasajeroAeronaveRole == null)
					throw new ONMinCardinalityException(null, RevisionPasajeroClassText.ClassAlias, "Clas_1348178673664478_Alias", RevisionPasajeroClassText.PasajeroAeronaveRoleAlias, "Agr_1348602167296649Rol_2_Alias", 1);
				#endregion Cardinality check for role 'PasajeroAeronave'

				#region Cardinality check for role 'PasajeroAeronave'
				// Maximum cardinality check (inverse)
				foreach (PasajeroAeronaveInstance lRelatedInstance in Instance.PasajeroAeronaveRole)
					if (lRelatedInstance.RevisionPasajeroRole.Count > 1)
						throw new ONMaxCardinalityException(null, PasajeroAeronaveClassText.ClassAlias, "Clas_1348178542592177_Alias", PasajeroAeronaveClassText.RevisionPasajeroRoleAlias, "Agr_1348602167296649Rol_1_Alias", 1);
				#endregion Cardinality check for role 'PasajeroAeronave'

				#region Cardinality check for role 'Revision' 
				// Minimum cardinality check
				if (Instance.RevisionRole == null)
					throw new ONMinCardinalityException(null, RevisionPasajeroClassText.ClassAlias, "Clas_1348178673664478_Alias", RevisionPasajeroClassText.RevisionRoleAlias, "Agr_1348602167296276Rol_2_Alias", 1);
				#endregion Cardinality check for role 'Revision'

				#region Cardinality check for role 'Revision'
				// Maximum cardinality check (inverse)
				foreach (RevisionInstance lRelatedInstance in Instance.RevisionRole)
					if (lRelatedInstance.RevisionPasajeroRole.Count > 1)
						throw new ONMaxCardinalityException(null, RevisionClassText.ClassAlias, "Clas_1348178542592347_Alias", RevisionClassText.RevisionPasajeroRoleAlias, "Agr_1348602167296276Rol_1_Alias", 1);
				#endregion Cardinality check for role 'Revision'
			}
			catch (Exception e)
			{
				if (e is ONException)
				{
					throw e;
				}
				else
				{
					string ltraceItem = "Definition class: RevisionPasajero, Service: create_instance, Component: RevisionPasajeroAction, Method: Create_instanceServ";
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
		/// RevisionPasajero's destruction event
		/// </summary>
		/// <param name="p_thisRevisionPasajeroArg">This parameter represents the inbound argument RevisionPasajero</param>
		[ONEvent("RevisionPasajero", "delete_instance", ServiceType = ServiceTypeEnumeration.Destroy)]
		public void Delete_instanceServ(
			[ONInboundArgument("Clas_1348178673664478Ser_2Arg_1_Alias", RevisionPasajeroClassText.Delete_instance_P_thisRevisionPasajeroArgumentAlias, "", "Clas_1348178673664478Ser_2_Alias", RevisionPasajeroClassText.Delete_instanceServiceAlias, "Clas_1348178673664478_Alias", RevisionPasajeroClassText.ClassAlias, AllowsNull = false)] RevisionPasajeroOid p_thisRevisionPasajeroArg)
		{
			try
			{
				RevisionPasajeroData lData = new RevisionPasajeroData(OnContext);


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
					string ltraceItem = "Definition class: RevisionPasajero, Service: delete_instance, Component: RevisionPasajeroAction, Method: Delete_instanceServ";
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
		/// RevisionPasajero's change event
		/// </summary>
		/// <param name="p_thisRevisionPasajeroArg">This parameter represents the inbound argument RevisionPasajero</param>
		[ONEvent("RevisionPasajero", "edit_instance")]
		public void Edit_instanceServ(
			[ONInboundArgument("Clas_1348178673664478Ser_3Arg_1_Alias", RevisionPasajeroClassText.Edit_instance_P_thisRevisionPasajeroArgumentAlias, "", "Clas_1348178673664478Ser_3_Alias", RevisionPasajeroClassText.Edit_instanceServiceAlias, "Clas_1348178673664478_Alias", RevisionPasajeroClassText.ClassAlias, AllowsNull = false)] RevisionPasajeroOid p_thisRevisionPasajeroArg)
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
					string ltraceItem = "Definition class: RevisionPasajero, Service: edit_instance, Component: RevisionPasajeroAction, Method: Edit_instanceServ";
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
