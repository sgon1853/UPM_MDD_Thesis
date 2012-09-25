// 3.4.4.5
using System;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Collection
{
	internal class AeronaveCollection : ONCollection
	{
		#region Members
		public PasajeroAeronaveCollection PasajeroAeronaveRoleTemp;
		#endregion

		#region Properties
		public AeronaveOid Oid
		{
			get
			{
				if (Count > 0)
					return this[0].Oid;
				else
					return AeronaveOid.Null;
			}
		}
		[ONAttribute("id_Aeronave", "autonumeric")]
		public ONInt Id_AeronaveAttr
		{
			get
			{
				if (Count > 0)
					return this[0].Id_AeronaveAttr;
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
		[ONAttribute("MaximoPasajeros", "int")]
		public ONInt MaximoPasajerosAttr
		{
			get
			{
				if (Count > 0)
					return this[0].MaximoPasajerosAttr;
				else
					return ONInt.Null;
			}
		}
		[ONAttribute("Origen", "text")]
		public ONText OrigenAttr
		{
			get
			{
				if (Count > 0)
					return this[0].OrigenAttr;
				else
					return ONText.Null;
			}
		}
		[ONAttribute("Destino", "text")]
		public ONText DestinoAttr
		{
			get
			{
				if (Count > 0)
					return this[0].DestinoAttr;
				else
					return ONText.Null;
			}
		}
		[ONRole("PasajeroAeronave", "PasajeroAeronave", "Aeronave", "Aeronave")]
		public PasajeroAeronaveCollection PasajeroAeronaveRole
		{
			get
			{
				if (PasajeroAeronaveRoleTemp == null)
				{
					PasajeroAeronaveRoleTemp = new PasajeroAeronaveCollection(OnContext);
					
					foreach ( AeronaveInstance lInstance in Array)
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
		public AeronaveCollection(ONContext onContext)
			:base(onContext, "Aeronave", false)
		{
		}
		#endregion Constructors
		
		#region Indexer
		public new AeronaveInstance this[int i]
		{
			get
			{
				if(Count > i)
					return Array[i] as AeronaveInstance;
				else
					return new AeronaveInstance(OnContext);
			}
		}
		#endregion Indexer
	}
}
