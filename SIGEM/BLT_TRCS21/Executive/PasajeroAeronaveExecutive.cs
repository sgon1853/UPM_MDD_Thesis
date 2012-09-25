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
	public class PasajeroAeronaveExecutive : ONExecutive
	{
		#region Properties
		internal new PasajeroAeronaveInstance Instance
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


		#region Service "New"
		/// <summary>
		/// This method solves the logical of the event New
		/// // (PasajeroAeronave's creation event)
		/// </summary>
		/// <param name="p_agrAeronaveArg">This parameter represents the inbound argument Aeronave</param>
		/// <param name="p_agrPasajeroArg">This parameter represents the inbound argument Pasajero</param>
		/// <param name="p_atrid_PasajeroAeronaveArg">This parameter represents the inbound argument id_PasajeroAeronave</param>
		/// <param name="p_atrNombreAeronaveArg">This parameter represents the inbound argument NombreAeronave</param>
		/// <param name="p_atrNombrePasajeroArg">This parameter represents the inbound argument NombrePasajero</param>
		[AutoComplete()]
		internal PasajeroAeronaveInstance Create_instanceServ(AeronaveOid p_agrAeronaveArg, PasajeroOid p_agrPasajeroArg, ONInt p_atrid_PasajeroAeronaveArg, ONString p_atrNombreAeronaveArg, ONString p_atrNombrePasajeroArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			PasajeroAeronaveAction lAction = new PasajeroAeronaveAction(OnContext);
			lAction.Instance = new PasajeroAeronaveInstance(OnContext);
			Instance = lAction.Create_instanceServ(p_agrAeronaveArg, p_agrPasajeroArg, p_atrid_PasajeroAeronaveArg, p_atrNombreAeronaveArg, p_atrNombrePasajeroArg);

			OnContext.OperationStack.Pop();
			OnContext.OperationStack.Push(Instance);
			
			return Instance;
		}
		#endregion


		#region Service "Destroy"
		/// <summary>
		/// This method solves the logical of the event Destroy
		/// // (PasajeroAeronave's destruction event)
		/// </summary>
		/// <param name="p_thisPasajeroAeronaveArg">This parameter represents the inbound argument PasajeroAeronave</param>
		[AutoComplete()]
		internal void Delete_instanceServ(PasajeroAeronaveOid p_thisPasajeroAeronaveArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			PasajeroAeronaveAction lAction = new PasajeroAeronaveAction(OnContext);
			lAction.Instance = Instance;
			lAction.Delete_instanceServ(p_thisPasajeroAeronaveArg);

		}
		#endregion


		#region Service "Edit"
		/// <summary>
		/// This method solves the logical of the event Edit
		/// // (PasajeroAeronave's change event)
		/// </summary>
		/// <param name="p_thisPasajeroAeronaveArg">This parameter represents the inbound argument PasajeroAeronave</param>
		[AutoComplete()]
		internal void Edit_instanceServ(PasajeroAeronaveOid p_thisPasajeroAeronaveArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			PasajeroAeronaveAction lAction = new PasajeroAeronaveAction(OnContext);
			lAction.Instance = Instance;
			lAction.Edit_instanceServ(p_thisPasajeroAeronaveArg);

		}
		#endregion
	}
}
