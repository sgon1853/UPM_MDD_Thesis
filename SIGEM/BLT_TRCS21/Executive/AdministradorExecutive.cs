// 3.4.4.5
using System;
using SIGEM.Business.XML;
using System.Runtime.InteropServices;
using System.EnterpriseServices;
using SIGEM.Business.Attributes;
using SIGEM.Business.Exceptions;
using SIGEM.Business.Instance;
using SIGEM.Business.Types;
using SIGEM.Business.OID;
using SIGEM.Business.Data;
using SIGEM.Business.Query;
using SIGEM.Business.Server;
using SIGEM.Business.Action;


namespace SIGEM.Business.Executive
{
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Transaction(TransactionOption.Required)]
	public class AdministradorExecutive : ONExecutive
	{
		#region Properties
		internal new AdministradorInstance Instance
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


		#region Service "New"
		/// <summary>
		/// This method solves the logical of the event New
		/// // (Administrador's creation event)
		/// </summary>
		/// <param name="p_atrid_AdministradorArg">This parameter represents the inbound argument id_Administrador</param>
		/// <param name="p_passwordArg">This parameter represents the inbound argument password</param>
		[AutoComplete()]
		internal AdministradorInstance Create_instanceServ(ONInt p_atrid_AdministradorArg, ONString p_passwordArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			AdministradorAction lAction = new AdministradorAction(OnContext);
			lAction.Instance = new AdministradorInstance(OnContext);
			Instance = lAction.Create_instanceServ(p_atrid_AdministradorArg, p_passwordArg);

			OnContext.OperationStack.Pop();
			OnContext.OperationStack.Push(Instance);
			
			return Instance;
		}
		#endregion


		#region Service "Destroy"
		/// <summary>
		/// This method solves the logical of the event Destroy
		/// // (Administrador's destruction event)
		/// </summary>
		/// <param name="p_thisAdministradorArg">This parameter represents the inbound argument Administrador</param>
		[AutoComplete()]
		internal void Delete_instanceServ(AdministradorOid p_thisAdministradorArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			AdministradorAction lAction = new AdministradorAction(OnContext);
			lAction.Instance = Instance;
			lAction.Delete_instanceServ(p_thisAdministradorArg);

		}
		#endregion


		#region Service "Edit"
		/// <summary>
		/// This method solves the logical of the event Edit
		/// // (Administrador's change event)
		/// </summary>
		/// <param name="p_thisAdministradorArg">This parameter represents the inbound argument Administrador</param>
		[AutoComplete()]
		internal void Edit_instanceServ(AdministradorOid p_thisAdministradorArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			AdministradorAction lAction = new AdministradorAction(OnContext);
			lAction.Instance = Instance;
			lAction.Edit_instanceServ(p_thisAdministradorArg);

		}
		#endregion


		#region Service "Set password"
		/// <summary>
		/// This method solves the logical of the event Set password
		/// // (Password event)
		/// </summary>
		/// <param name="p_thisAdministradorArg">This parameter represents the inbound argument Administrador</param>
		/// <param name="p_NewPasswordArg">This parameter represents the inbound argument New password</param>
		[AutoComplete()]
		internal void SetPasswordServ(AdministradorOid p_thisAdministradorArg, ONString p_NewPasswordArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			AdministradorAction lAction = new AdministradorAction(OnContext);
			lAction.Instance = Instance;
			lAction.SetPasswordServ(p_thisAdministradorArg, p_NewPasswordArg);

		}
		#endregion

		#region Service "MVChangePassWord"
		///<summary>
		///This method solves the logical of the service MVChangePassWord
		///</summary>
		/// <param name="oldpasswordArg">This parameter represents the inbound argument oldpassword</param>
		/// <param name="newpasswordArg">This parameter represents the inbound argument newpassword</param>
		[AutoComplete()]
		internal void MVChangePassWordServ(ONString oldpasswordArg, ONString newpasswordArg)
		{
			// Execute service
			AdministradorAction lAction = new AdministradorAction(OnContext);
			lAction.Instance = Instance;
			lAction.MVChangePassWordServ(oldpasswordArg, newpasswordArg);
		}
		#endregion
	}
}
