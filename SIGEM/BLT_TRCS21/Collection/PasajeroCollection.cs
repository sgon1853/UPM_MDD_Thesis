// 3.4.4.5
using System;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Collection
{
	internal class PasajeroCollection : ONCollection
	{
		#region Members
		public PasajeroAeronaveCollection PasajeroAeronaveRoleTemp;
		#endregion

		#region Properties
		public PasajeroOid Oid
		{
			get
			{
				if (Count > 0)
					return this[0].Oid;
				else
					return PasajeroOid.Null;
			}
		}
		[ONAttribute("id_Pasajero", "autonumeric")]
		public ONInt Id_PasajeroAttr
		{
			get
			{
				if (Count > 0)
					return this[0].Id_PasajeroAttr;
				else
					return ONInt.Null;
			}
		}
		[ONAttribute("Nombre", "text")]
		public ONText NombreAttr
		{
			get
			{
				if (Count > 0)
					return this[0].NombreAttr;
				else
					return ONText.Null;
			}
		}
		[ONRole("PasajeroAeronave", "PasajeroAeronave", "Pasajero", "Pasajero")]
		public PasajeroAeronaveCollection PasajeroAeronaveRole
		{
			get
			{
				if (PasajeroAeronaveRoleTemp == null)
				{
					PasajeroAeronaveRoleTemp = new PasajeroAeronaveCollection(OnContext);
					
					foreach ( PasajeroInstance lInstance in Array)
						PasajeroAeronaveRoleTemp.AddRangeOrdered(lInstance.PasajeroAeronaveRole, null, OnContext);
				}
				
				return PasajeroAeronaveRoleTemp;
			}
			set
			{
				PasajeroAeronaveRoleTemp = value;
			}
		}
		#endregion Properties
		
		#region  Constructors
		public PasajeroCollection(ONContext onContext)
			:base(onContext, "Pasajero", false)
		{
		}
		#endregion Constructors
		
		#region Indexer
		public new PasajeroInstance this[int i]
		{
			get
			{
				if(Count > i)
					return Array[i] as PasajeroInstance;
				else
					return new PasajeroInstance(OnContext);
			}
		}
		#endregion Indexer
	}
}
