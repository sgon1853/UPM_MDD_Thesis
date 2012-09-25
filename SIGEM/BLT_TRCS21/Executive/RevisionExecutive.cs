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
	public class RevisionExecutive : ONExecutive
	{
		#region Properties
		internal new RevisionInstance Instance
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


		#region Service "New"
		/// <summary>
		/// This method solves the logical of the event New
		/// // (RevisarAeronave's creation event)
		/// </summary>
		/// <param name="p_atrid_RevisarAeronaveArg">This parameter represents the inbound argument id_RevisarAeronave</param>
		/// <param name="p_atrFechaRevisionArg">This parameter represents the inbound argument FechaRevision</param>
		/// <param name="p_atrNombreRevisorArg">This parameter represents the inbound argument NombreRevisor</param>
		/// <param name="p_atrId_AeronaveArg">This parameter represents the inbound argument Id_Aeronave</param>
		[AutoComplete()]
		internal RevisionInstance Create_instanceServ(ONInt p_atrid_RevisarAeronaveArg, ONDate p_atrFechaRevisionArg, ONString p_atrNombreRevisorArg, ONString p_atrId_AeronaveArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			RevisionAction lAction = new RevisionAction(OnContext);
			lAction.Instance = new RevisionInstance(OnContext);
			Instance = lAction.Create_instanceServ(p_atrid_RevisarAeronaveArg, p_atrFechaRevisionArg, p_atrNombreRevisorArg, p_atrId_AeronaveArg);

			OnContext.OperationStack.Pop();
			OnContext.OperationStack.Push(Instance);
			
			return Instance;
		}
		#endregion


		#region Service "Destroy"
		/// <summary>
		/// This method solves the logical of the event Destroy
		/// // (RevisarAeronave's destruction event)
		/// </summary>
		/// <param name="p_thisRevisarAeronaveArg">This parameter represents the inbound argument Revision</param>
		[AutoComplete()]
		internal void Delete_instanceServ(RevisionOid p_thisRevisarAeronaveArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			RevisionAction lAction = new RevisionAction(OnContext);
			lAction.Instance = Instance;
			lAction.Delete_instanceServ(p_thisRevisarAeronaveArg);

		}
		#endregion


		#region Service "Edit"
		/// <summary>
		/// This method solves the logical of the event Edit
		/// // (RevisarAeronave's change event)
		/// </summary>
		/// <param name="p_thisRevisarAeronaveArg">This parameter represents the inbound argument Revision</param>
		[AutoComplete()]
		internal void Edit_instanceServ(RevisionOid p_thisRevisarAeronaveArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			RevisionAction lAction = new RevisionAction(OnContext);
			lAction.Instance = Instance;
			lAction.Edit_instanceServ(p_thisRevisarAeronaveArg);

		}
		#endregion
	}
}
