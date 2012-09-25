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
	/// <summary>This class solves the Events and Transactions of the class Administrador</summary>
	[ONActionClass("Administrador")]
	[ONInterception()]
	internal class AdministradorAction : ONAction
	{
		#region Properties
		public new AdministradorInstance Instance
		{
			get
			{
				return base.Instance as AdministradorInstance;
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
		public  AdministradorAction(ONServiceContext onContext) : base(onContext, "Administrador")
		{
		}
		#endregion Constructors
		#region Service "New"
		/// <summary>This method checks the State Transition Diagram of the class "Administrador"</summary>
		/// <param name="p_atrid_AdministradorArg">This parameter represents the inbound argument p_atrid_Administrador</param>
		/// <param name="p_passwordArg">This parameter represents the inbound argument p_password</param>
		[ONSTD("Administrador", "create_instance")]
		public void CheckSTDiagram_create_instance(ONInt p_atrid_AdministradorArg, ONString p_passwordArg)
		{
				Instance.StateObj = new ONString("Admini0");
		}
		/// <summary>
		/// This method solves the logical of the event New
		/// Administrador's creation event
		/// </summary>
		/// <param name="p_atrid_AdministradorArg">This parameter represents the inbound argument id_Administrador</param>
		/// <param name="p_passwordArg">This parameter represents the inbound argument password</param>
		[ONEvent("Administrador", "create_instance", ServiceType = ServiceTypeEnumeration.New)]
		public AdministradorInstance Create_instanceServ(
			[ONInboundArgument("Clas_1348605050880238Ser_1Arg_1_Alias", AdministradorClassText.Create_instance_P_atrid_AdministradorArgumentAlias, "autonumeric", "Clas_1348605050880238Ser_1_Alias", AdministradorClassText.Create_instanceServiceAlias, "Clas_1348605050880238_Alias", AdministradorClassText.ClassAlias, AllowsNull = false)] ONInt p_atrid_AdministradorArg,
			[ONInboundArgument("Clas_1348605050880238Ser_1Arg_2_Alias", AdministradorClassText.Create_instance_P_passwordArgumentAlias, "password", "Clas_1348605050880238Ser_1_Alias", AdministradorClassText.Create_instanceServiceAlias, "Clas_1348605050880238_Alias", AdministradorClassText.ClassAlias, Length = AdministradorXml.LENGTHPASSWORDADMINISTRADOR, AllowsNull = false)] ONString p_passwordArg)
		{
			try
			{
				AdministradorData lData = new AdministradorData(OnContext);

				#region Construct OID
				Instance.Oid = new AdministradorOid();
				Instance.Oid.Id_AdministradorAttr = new ONInt(p_atrid_AdministradorArg);
				#endregion Construct OID

				#region Argument initialization 'p_password' (password)
				// Cipher password argument
				Instance.PassWordAttr = new ONString(ONSecureControl.CipherPassword(p_passwordArg.TypedValue));
				#endregion Argument initialization 'p_password' (password)

				#region Autonumeric attribute 'id_Administrador'
				if (Instance.Id_AdministradorAttr < new ONInt(0))
				{
					AdministradorData lAutonumericData = new AdministradorData(OnContext);
					lAutonumericData.ClassName = "Administrador";
					//Get Autonumeric
					Instance.Oid.Id_AdministradorAttr = lAutonumericData.GetAutonumericid_Administrador();
				}
				#endregion Autonumeric attribute 'id_Administrador'
			
				//Search if instance exists
				if (lData.Exist(Instance.Oid, null))
					throw new ONInstanceExistException(null, "Clas_1348605050880238_Alias", AdministradorClassText.ClassAlias);
			
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
					string ltraceItem = "Definition class: Administrador, Service: create_instance, Component: AdministradorAction, Method: Create_instanceServ";
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
		/// Administrador's destruction event
		/// </summary>
		/// <param name="p_thisAdministradorArg">This parameter represents the inbound argument Administrador</param>
		[ONEvent("Administrador", "delete_instance", ServiceType = ServiceTypeEnumeration.Destroy)]
		public void Delete_instanceServ(
			[ONInboundArgument("Clas_1348605050880238Ser_2Arg_1_Alias", AdministradorClassText.Delete_instance_P_thisAdministradorArgumentAlias, "", "Clas_1348605050880238Ser_2_Alias", AdministradorClassText.Delete_instanceServiceAlias, "Clas_1348605050880238_Alias", AdministradorClassText.ClassAlias, AllowsNull = false)] AdministradorOid p_thisAdministradorArg)
		{
			try
			{
				AdministradorData lData = new AdministradorData(OnContext);


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
					string ltraceItem = "Definition class: Administrador, Service: delete_instance, Component: AdministradorAction, Method: Delete_instanceServ";
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
		/// Administrador's change event
		/// </summary>
		/// <param name="p_thisAdministradorArg">This parameter represents the inbound argument Administrador</param>
		[ONEvent("Administrador", "edit_instance")]
		public void Edit_instanceServ(
			[ONInboundArgument("Clas_1348605050880238Ser_3Arg_1_Alias", AdministradorClassText.Edit_instance_P_thisAdministradorArgumentAlias, "", "Clas_1348605050880238Ser_3_Alias", AdministradorClassText.Edit_instanceServiceAlias, "Clas_1348605050880238_Alias", AdministradorClassText.ClassAlias, AllowsNull = false)] AdministradorOid p_thisAdministradorArg)
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
					string ltraceItem = "Definition class: Administrador, Service: edit_instance, Component: AdministradorAction, Method: Edit_instanceServ";
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
		#region Service "Set password"
		/// <summary>
		/// This method solves the logical of the event Set password
		/// Password event
		/// </summary>
		/// <param name="p_thisAdministradorArg">This parameter represents the inbound argument Administrador</param>
		/// <param name="p_NewPasswordArg">This parameter represents the inbound argument New password</param>
		[ONEvent("Administrador", "setPassword")]
		public void SetPasswordServ(
			[ONInboundArgument("Clas_1348605050880238Ser_4Arg_1_Alias", AdministradorClassText.SetPassword_P_thisAdministradorArgumentAlias, "", "Clas_1348605050880238Ser_4_Alias", AdministradorClassText.SetPasswordServiceAlias, "Clas_1348605050880238_Alias", AdministradorClassText.ClassAlias, AllowsNull = false)] AdministradorOid p_thisAdministradorArg,
			[ONInboundArgument("Clas_1348605050880238Ser_4Arg_2_Alias", AdministradorClassText.SetPassword_P_NewPasswordArgumentAlias, "password", "Clas_1348605050880238Ser_4_Alias", AdministradorClassText.SetPasswordServiceAlias, "Clas_1348605050880238_Alias", AdministradorClassText.ClassAlias, Length = AdministradorXml.LENGTHPASSWORDADMINISTRADOR, AllowsNull = false)] ONString p_NewPasswordArg)
		{
			try
			{
				//Cipher password argument
				ONString lPassword = new ONString(ONSecureControl.CipherPassword(p_NewPasswordArg.TypedValue));
				//Update Attribute
				Instance.PassWordAttr = lPassword;
			}
			catch (Exception e)
			{
				if (e is ONException)
				{
					throw e;
				}
				else
				{
					string ltraceItem = "Definition class: Administrador, Service: setPassword, Component: AdministradorAction, Method: SetPasswordServ";
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

		#region Service "MVChangePassWord"


		/// <summary>This method solve the logical of the event MVChangePassWord</summary>
		/// <param name="oldpasswordArg">This parameter represents the inbound argument oldpassword</param>
		/// <param name="newpasswordArg">This parameter represents the inbound argument newpassword</param>
		[ONEvent("Administrador", "MVChangePassWord")]
		public void MVChangePassWordServ(
			[ONInboundArgument("", "oldpassword", "password", "", "MVChangePassWord", "Clas_1348605050880238_Alias", "Administrador", AllowsNull = false)] ONString oldpasswordArg,
			[ONInboundArgument("", "newpassword", "password", "", "MVChangePassWord", "Clas_1348605050880238_Alias", "Administrador", AllowsNull = false)] ONString newpasswordArg)
		{
			ONString lPassword = new ONString(Instance.PassWordAttr);
			//Cipher password argument
			ONString lOldPassword = new ONString(ONSecureControl.CipherPassword(oldpasswordArg.TypedValue));
			
			if (lPassword.TypedValue == lOldPassword.TypedValue)
			{
				//Cipher password argument
				lPassword = new ONString(ONSecureControl.CipherPassword(newpasswordArg.TypedValue));
				//Update Attribute
				Instance.PassWordAttr = lPassword;
			}
			else
				throw new ONAgentValidationException(null);
		}
		#endregion
	}
}
