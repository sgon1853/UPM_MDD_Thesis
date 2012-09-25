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
	public class NaveNodrizaExecutive : ONExecutive
	{
		#region Properties
		internal new NaveNodrizaInstance Instance
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


		#region Service "New"
		/// <summary>
		/// This method solves the logical of the event New
		/// // (NaveNodriza's creation event)
		/// </summary>
		/// <param name="p_atrid_NaveNodrizaArg">This parameter represents the inbound argument id_NaveNodriza</param>
		/// <param name="p_atrNombre_NaveNodrizaArg">This parameter represents the inbound argument Nombre_NaveNodriza</param>
		[AutoComplete()]
		internal NaveNodrizaInstance Create_instanceServ(ONInt p_atrid_NaveNodrizaArg, ONString p_atrNombre_NaveNodrizaArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			NaveNodrizaAction lAction = new NaveNodrizaAction(OnContext);
			lAction.Instance = new NaveNodrizaInstance(OnContext);
			Instance = lAction.Create_instanceServ(p_atrid_NaveNodrizaArg, p_atrNombre_NaveNodrizaArg);

			OnContext.OperationStack.Pop();
			OnContext.OperationStack.Push(Instance);
			
			return Instance;
		}
		#endregion


		#region Service "Destroy"
		/// <summary>
		/// This method solves the logical of the event Destroy
		/// // (NaveNodriza's destruction event)
		/// </summary>
		/// <param name="p_thisNaveNodrizaArg">This parameter represents the inbound argument NaveNodriza</param>
		[AutoComplete()]
		internal void Delete_instanceServ(NaveNodrizaOid p_thisNaveNodrizaArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			NaveNodrizaAction lAction = new NaveNodrizaAction(OnContext);
			lAction.Instance = Instance;
			lAction.Delete_instanceServ(p_thisNaveNodrizaArg);

		}
		#endregion


		#region Service "Edit"
		/// <summary>
		/// This method solves the logical of the event Edit
		/// // (NaveNodriza's change event)
		/// </summary>
		/// <param name="p_thisNaveNodrizaArg">This parameter represents the inbound argument NaveNodriza</param>
		[AutoComplete()]
		internal void Edit_instanceServ(NaveNodrizaOid p_thisNaveNodrizaArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			NaveNodrizaAction lAction = new NaveNodrizaAction(OnContext);
			lAction.Instance = Instance;
			lAction.Edit_instanceServ(p_thisNaveNodrizaArg);

		}
		#endregion
	}
}
