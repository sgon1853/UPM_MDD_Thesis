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
	public class RevisionPasajeroExecutive : ONExecutive
	{
		#region Properties
		internal new RevisionPasajeroInstance Instance
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


		#region Service "New"
		/// <summary>
		/// This method solves the logical of the event New
		/// // (RevisionPasajero's creation event)
		/// </summary>
		/// <param name="p_agrPasajeroAeronaveArg">This parameter represents the inbound argument PasajeroAeronave</param>
		/// <param name="p_agrRevisionArg">This parameter represents the inbound argument Revision</param>
		/// <param name="p_atrid_RevisionPasajeroArg">This parameter represents the inbound argument id_RevisionPasajero</param>
		[AutoComplete()]
		internal RevisionPasajeroInstance Create_instanceServ(PasajeroAeronaveOid p_agrPasajeroAeronaveArg, RevisionOid p_agrRevisionArg, ONInt p_atrid_RevisionPasajeroArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			RevisionPasajeroAction lAction = new RevisionPasajeroAction(OnContext);
			lAction.Instance = new RevisionPasajeroInstance(OnContext);
			Instance = lAction.Create_instanceServ(p_agrPasajeroAeronaveArg, p_agrRevisionArg, p_atrid_RevisionPasajeroArg);

			OnContext.OperationStack.Pop();
			OnContext.OperationStack.Push(Instance);
			
			return Instance;
		}
		#endregion


		#region Service "Destroy"
		/// <summary>
		/// This method solves the logical of the event Destroy
		/// // (RevisionPasajero's destruction event)
		/// </summary>
		/// <param name="p_thisRevisionPasajeroArg">This parameter represents the inbound argument RevisionPasajero</param>
		[AutoComplete()]
		internal void Delete_instanceServ(RevisionPasajeroOid p_thisRevisionPasajeroArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			RevisionPasajeroAction lAction = new RevisionPasajeroAction(OnContext);
			lAction.Instance = Instance;
			lAction.Delete_instanceServ(p_thisRevisionPasajeroArg);

		}
		#endregion


		#region Service "Edit"
		/// <summary>
		/// This method solves the logical of the event Edit
		/// // (RevisionPasajero's change event)
		/// </summary>
		/// <param name="p_thisRevisionPasajeroArg">This parameter represents the inbound argument RevisionPasajero</param>
		[AutoComplete()]
		internal void Edit_instanceServ(RevisionPasajeroOid p_thisRevisionPasajeroArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			RevisionPasajeroAction lAction = new RevisionPasajeroAction(OnContext);
			lAction.Instance = Instance;
			lAction.Edit_instanceServ(p_thisRevisionPasajeroArg);

		}
		#endregion
	}
}
