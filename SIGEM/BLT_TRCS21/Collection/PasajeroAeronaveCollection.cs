// 3.4.4.5
using System;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Collection
{
	internal class PasajeroAeronaveCollection : ONCollection
	{
		#region Members
		public AeronaveCollection AeronaveRoleTemp;
		public PasajeroCollection PasajeroRoleTemp;
		public RevisionPasajeroCollection RevisionPasajeroRoleTemp;
		#endregion

		#region Properties
		public PasajeroAeronaveOid Oid
		{
			get
			{
				if (Count > 0)
					return this[0].Oid;
				else
					return PasajeroAeronaveOid.Null;
			}
		}
		[ONAttribute("id_PasajeroAeronave", "autonumeric")]
		public ONInt Id_PasajeroAeronaveAttr
		{
			get
			{
				if (Count > 0)
					return this[0].Id_PasajeroAeronaveAttr;
				else
					return ONInt.Null;
			}
		}
		[ONAttribute("NombreAeronave", "string")]
		public ONString NombreAeronaveAttr
		{
			get
			{
				if (Count > 0)
					return this[0].NombreAeronaveAttr;
				else
					return ONString.Null;
			}
		}
		[ONAttribute("NombrePasajero", "string")]
		public ONString NombrePasajeroAttr
		{
			get
			{
				if (Count > 0)
					return this[0].NombrePasajeroAttr;
				else
					return ONString.Null;
			}
		}
		[ONRole("Aeronave", "Aeronave", "PasajeroAeronave", "PasajeroAeronave")]
		public AeronaveCollection AeronaveRole
		{
			get
			{
				if (AeronaveRoleTemp == null)
				{
					AeronaveRoleTemp = new AeronaveCollection(OnContext);

					foreach (PasajeroAeronaveInstance lInstance in Array)
						AeronaveRoleTemp.AddRangeOrdered(lInstance.AeronaveRole, null, OnContext);
				}

				return AeronaveRoleTemp;
			}
		}
		[ONRole("Pasajero", "Pasajero", "PasajeroAeronave", "PasajeroAeronave")]
		public PasajeroCollection PasajeroRole
		{
			get
			{
				if (PasajeroRoleTemp == null)
				{
					PasajeroRoleTemp = new PasajeroCollection(OnContext);

					foreach (PasajeroAeronaveInstance lInstance in Array)
						PasajeroRoleTemp.AddRangeOrdered(lInstance.PasajeroRole, null, OnContext);
				}

				return PasajeroRoleTemp;
			}
		}
		[ONRole("RevisionPasajero", "RevisionPasajero", "PasajeroAeronave", "PasajeroAeronave")]
		public RevisionPasajeroCollection RevisionPasajeroRole
		{
			get
			{
				if (RevisionPasajeroRoleTemp == null)
				{
					RevisionPasajeroRoleTemp = new RevisionPasajeroCollection(OnContext);
					
					foreach ( PasajeroAeronaveInstance lInstance in Array)
						RevisionPasajeroRoleTemp.AddRangeOrdered(lInstance.RevisionPasajeroRole, null, OnContext);
				}
				
				return RevisionPasajeroRoleTemp;
			}
			set
			{
				RevisionPasajeroRoleTemp = value;
			}
		}
		#endregion Properties
		
		#region  Constructors
		public PasajeroAeronaveCollection(ONContext onContext)
			:base(onContext, "PasajeroAeronave", false)
		{
		}
		#endregion Constructors
		
		#region Indexer
		public new PasajeroAeronaveInstance this[int i]
		{
			get
			{
				if(Count > i)
					return Array[i] as PasajeroAeronaveInstance;
				else
					return new PasajeroAeronaveInstance(OnContext);
			}
		}
		#endregion Indexer
	}
}
