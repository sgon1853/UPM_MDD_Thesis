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
	public class PasajeroExecutive : ONExecutive
	{
		#region Properties
		internal new PasajeroInstance Instance
		{
			get
			{
				return base.Instance as PasajeroInstance;
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
		/// // (Pasajero's creation event)
		/// </summary>
		/// <param name="p_agrPasajeroAeronaveArg">This parameter represents the inbound argument PasajeroAeronave</param>
		/// <param name="p_atrid_PasajeroArg">This parameter represents the inbound argument id_Pasajero</param>
		/// <param name="p_atrNombreArg">This parameter represents the inbound argument Nombre</param>
		[AutoComplete()]
		internal PasajeroInstance Create_instanceServ(PasajeroAeronaveOid p_agrPasajeroAeronaveArg, ONInt p_atrid_PasajeroArg, ONText p_atrNombreArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			PasajeroAction lAction = new PasajeroAction(OnContext);
			lAction.Instance = new PasajeroInstance(OnContext);
			Instance = lAction.Create_instanceServ(p_agrPasajeroAeronaveArg, p_atrid_PasajeroArg, p_atrNombreArg);

			OnContext.OperationStack.Pop();
			OnContext.OperationStack.Push(Instance);
			
			return Instance;
		}
		#endregion


		#region Service "Destroy"
		/// <summary>
		/// This method solves the logical of the event Destroy
		/// // (Pasajero's destruction event)
		/// </summary>
		/// <param name="p_thisPasajeroArg">This parameter represents the inbound argument Pasajero</param>
		[AutoComplete()]
		internal void Delete_instanceServ(PasajeroOid p_thisPasajeroArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			PasajeroAction lAction = new PasajeroAction(OnContext);
			lAction.Instance = Instance;
			lAction.Delete_instanceServ(p_thisPasajeroArg);

		}
		#endregion


		#region Service "Edit"
		/// <summary>
		/// This method solves the logical of the event Edit
		/// // (Pasajero's change event)
		/// </summary>
		/// <param name="p_thisPasajeroArg">This parameter represents the inbound argument Pasajero</param>
		[AutoComplete()]
		internal void Edit_instanceServ(PasajeroOid p_thisPasajeroArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			PasajeroAction lAction = new PasajeroAction(OnContext);
			lAction.Instance = Instance;
			lAction.Edit_instanceServ(p_thisPasajeroArg);

		}
		#endregion
	}
}
