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
	/// <summary>This class solves the Events and Transactions of the class NaveNodriza</summary>
	[ONActionClass("NaveNodriza")]
	[ONInterception()]
	internal class NaveNodrizaAction : ONAction
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
		/// <summary>Constructor</summary>
		/// <param name="onContext">This parameter has the current context</param>
		public  NaveNodrizaAction(ONServiceContext onContext) : base(onContext, "NaveNodriza")
		{
		}
		#endregion Constructors
		#region Service "New"
		/// <summary>This method checks the State Transition Diagram of the class "NaveNodriza"</summary>
		/// <param name="p_atrid_NaveNodrizaArg">This parameter represents the inbound argument p_atrid_NaveNodriza</param>
		/// <param name="p_atrNombre_NaveNodrizaArg">This parameter represents the inbound argument p_atrNombre_NaveNodriza</param>
		[ONSTD("NaveNodriza", "create_instance")]
		public void CheckSTDiagram_create_instance(ONInt p_atrid_NaveNodrizaArg, ONString p_atrNombre_NaveNodrizaArg)
		{
				Instance.StateObj = new ONString("NaveNo0");
		}
		/// <summary>
		/// This method solves the logical of the event New
		/// NaveNodriza's creation event
		/// </summary>
		/// <param name="p_atrid_NaveNodrizaArg">This parameter represents the inbound argument id_NaveNodriza</param>
		/// <param name="p_atrNombre_NaveNodrizaArg">This parameter represents the inbound argument Nombre_NaveNodriza</param>
		[ONEvent("NaveNodriza", "create_instance", ServiceType = ServiceTypeEnumeration.New)]
		public NaveNodrizaInstance Create_instanceServ(
			[ONInboundArgument("Clas_1347649273856884Ser_1Arg_1_Alias", NaveNodrizaClassText.Create_instance_P_atrid_NaveNodrizaArgumentAlias, "autonumeric", "Clas_1347649273856884Ser_1_Alias", NaveNodrizaClassText.Create_instanceServiceAlias, "Clas_1347649273856884_Alias", NaveNodrizaClassText.ClassAlias, AllowsNull = false)] ONInt p_atrid_NaveNodrizaArg,
			[ONInboundArgument("Clas_1347649273856884Ser_1Arg_2_Alias", NaveNodrizaClassText.Create_instance_P_atrNombre_NaveNodrizaArgumentAlias, "string", "Clas_1347649273856884Ser_1_Alias", NaveNodrizaClassText.Create_instanceServiceAlias, "Clas_1347649273856884_Alias", NaveNodrizaClassText.ClassAlias, Length = 100, AllowsNull = false)] ONString p_atrNombre_NaveNodrizaArg)
		{
			try
			{
				NaveNodrizaData lData = new NaveNodrizaData(OnContext);

				#region Construct OID
				Instance.Oid = new NaveNodrizaOid();
				Instance.Oid.Id_NaveNodrizaAttr = new ONInt(p_atrid_NaveNodrizaArg);
				#endregion Construct OID

				#region Argument initialization 'p_atrNombre_NaveNodriza' (Nombre_NaveNodriza)
				Instance.Nombre_NaveNodrizaAttr = new ONString(p_atrNombre_NaveNodrizaArg);
				#endregion Argument initialization 'p_atrNombre_NaveNodriza' (Nombre_NaveNodriza)

				#region Autonumeric attribute 'id_NaveNodriza'
				if (Instance.Id_NaveNodrizaAttr < new ONInt(0))
				{
					NaveNodrizaData lAutonumericData = new NaveNodrizaData(OnContext);
					lAutonumericData.ClassName = "NaveNodriza";
					//Get Autonumeric
					Instance.Oid.Id_NaveNodrizaAttr = lAutonumericData.GetAutonumericid_NaveNodriza();
				}
				#endregion Autonumeric attribute 'id_NaveNodriza'
			
				//Search if instance exists
				if (lData.Exist(Instance.Oid, null))
					throw new ONInstanceExistException(null, "Clas_1347649273856884_Alias", NaveNodrizaClassText.ClassAlias);
			
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
					string ltraceItem = "Definition class: NaveNodriza, Service: create_instance, Component: NaveNodrizaAction, Method: Create_instanceServ";
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
		/// NaveNodriza's destruction event
		/// </summary>
		/// <param name="p_thisNaveNodrizaArg">This parameter represents the inbound argument NaveNodriza</param>
		[ONEvent("NaveNodriza", "delete_instance", ServiceType = ServiceTypeEnumeration.Destroy)]
		public void Delete_instanceServ(
			[ONInboundArgument("Clas_1347649273856884Ser_2Arg_1_Alias", NaveNodrizaClassText.Delete_instance_P_thisNaveNodrizaArgumentAlias, "", "Clas_1347649273856884Ser_2_Alias", NaveNodrizaClassText.Delete_instanceServiceAlias, "Clas_1347649273856884_Alias", NaveNodrizaClassText.ClassAlias, AllowsNull = false)] NaveNodrizaOid p_thisNaveNodrizaArg)
		{
			try
			{
				NaveNodrizaData lData = new NaveNodrizaData(OnContext);


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
					string ltraceItem = "Definition class: NaveNodriza, Service: delete_instance, Component: NaveNodrizaAction, Method: Delete_instanceServ";
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
		/// NaveNodriza's change event
		/// </summary>
		/// <param name="p_thisNaveNodrizaArg">This parameter represents the inbound argument NaveNodriza</param>
		[ONEvent("NaveNodriza", "edit_instance")]
		public void Edit_instanceServ(
			[ONInboundArgument("Clas_1347649273856884Ser_3Arg_1_Alias", NaveNodrizaClassText.Edit_instance_P_thisNaveNodrizaArgumentAlias, "", "Clas_1347649273856884Ser_3_Alias", NaveNodrizaClassText.Edit_instanceServiceAlias, "Clas_1347649273856884_Alias", NaveNodrizaClassText.ClassAlias, AllowsNull = false)] NaveNodrizaOid p_thisNaveNodrizaArg)
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
					string ltraceItem = "Definition class: NaveNodriza, Service: edit_instance, Component: NaveNodrizaAction, Method: Edit_instanceServ";
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
