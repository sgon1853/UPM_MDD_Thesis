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
	public class AeronaveExecutive : ONExecutive
	{
		#region Properties
		internal new AeronaveInstance Instance
		{
			get
			{
				return base.Instance as AeronaveInstance;
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
		/// // (Aeronave's creation event)
		/// </summary>
		/// <param name="p_agrPasajeroAeronaveArg">This parameter represents the inbound argument PasajeroAeronave</param>
		/// <param name="p_atrid_AeronaveArg">This parameter represents the inbound argument id_Aeronave</param>
		/// <param name="p_atrNombreArg">This parameter represents the inbound argument Nombre</param>
		/// <param name="p_atrMaximoPasajerosArg">This parameter represents the inbound argument MaximoPasajeros</param>
		/// <param name="p_atrOrigenArg">This parameter represents the inbound argument Origen</param>
		/// <param name="p_atrDestinoArg">This parameter represents the inbound argument Destino</param>
		[AutoComplete()]
		internal AeronaveInstance Create_instanceServ(PasajeroAeronaveOid p_agrPasajeroAeronaveArg, ONInt p_atrid_AeronaveArg, ONText p_atrNombreArg, ONInt p_atrMaximoPasajerosArg, ONText p_atrOrigenArg, ONText p_atrDestinoArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			AeronaveAction lAction = new AeronaveAction(OnContext);
			lAction.Instance = new AeronaveInstance(OnContext);
			Instance = lAction.Create_instanceServ(p_agrPasajeroAeronaveArg, p_atrid_AeronaveArg, p_atrNombreArg, p_atrMaximoPasajerosArg, p_atrOrigenArg, p_atrDestinoArg);

			OnContext.OperationStack.Pop();
			OnContext.OperationStack.Push(Instance);
			
			return Instance;
		}
		#endregion


		#region Service "Destroy"
		/// <summary>
		/// This method solves the logical of the event Destroy
		/// // (Aeronave's destruction event)
		/// </summary>
		/// <param name="p_thisAeronaveArg">This parameter represents the inbound argument Aeronave</param>
		[AutoComplete()]
		internal void Delete_instanceServ(AeronaveOid p_thisAeronaveArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			AeronaveAction lAction = new AeronaveAction(OnContext);
			lAction.Instance = Instance;
			lAction.Delete_instanceServ(p_thisAeronaveArg);

		}
		#endregion


		#region Service "Edit"
		/// <summary>
		/// This method solves the logical of the event Edit
		/// // (Aeronave's change event)
		/// </summary>
		/// <param name="p_thisAeronaveArg">This parameter represents the inbound argument Aeronave</param>
		[AutoComplete()]
		internal void Edit_instanceServ(AeronaveOid p_thisAeronaveArg)
		{
			if (Instance != null)
			{
				ONFilterList onfilt = new ONFilterList();
				onfilt.Add("QueryByOid", new QueryByOidFilter(Instance.Oid));
				Instance.Find(onfilt);
			}
			// Execute service
			AeronaveAction lAction = new AeronaveAction(OnContext);
			lAction.Instance = Instance;
			lAction.Edit_instanceServ(p_thisAeronaveArg);

		}
		#endregion
	}
}
