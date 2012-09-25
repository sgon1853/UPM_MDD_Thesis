// 3.4.4.5
using System;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Collection
{
	internal class RevisionPasajeroCollection : ONCollection
	{
		#region Members
		public RevisionCollection RevisionRoleTemp;
		public PasajeroAeronaveCollection PasajeroAeronaveRoleTemp;
		#endregion

		#region Properties
		public RevisionPasajeroOid Oid
		{
			get
			{
				if (Count > 0)
					return this[0].Oid;
				else
					return RevisionPasajeroOid.Null;
			}
		}
		[ONAttribute("id_RevisionPasajero", "autonumeric")]
		public ONInt Id_RevisionPasajeroAttr
		{
			get
			{
				if (Count > 0)
					return this[0].Id_RevisionPasajeroAttr;
				else
					return ONInt.Null;
			}
		}
		[ONRole("Revision", "Revision", "RevisionPasajero", "RevisionPasajero")]
		public RevisionCollection RevisionRole
		{
			get
			{
				if (RevisionRoleTemp == null)
				{
					RevisionRoleTemp = new RevisionCollection(OnContext);

					foreach (RevisionPasajeroInstance lInstance in Array)
						RevisionRoleTemp.AddRangeOrdered(lInstance.RevisionRole, null, OnContext);
				}

				return RevisionRoleTemp;
			}
		}
		[ONRole("PasajeroAeronave", "PasajeroAeronave", "RevisionPasajero", "RevisionPasajero")]
		public PasajeroAeronaveCollection PasajeroAeronaveRole
		{
			get
			{
				if (PasajeroAeronaveRoleTemp == null)
				{
					PasajeroAeronaveRoleTemp = new PasajeroAeronaveCollection(OnContext);

					foreach (RevisionPasajeroInstance lInstance in Array)
						PasajeroAeronaveRoleTemp.AddRangeOrdered(lInstance.PasajeroAeronaveRole, null, OnContext);
				}

				return PasajeroAeronaveRoleTemp;
			}
		}
		#endregion Properties
		
		#region  Constructors
		public RevisionPasajeroCollection(ONContext onContext)
			:base(onContext, "RevisionPasajero", false)
		{
		}
		#endregion Constructors
		
		#region Indexer
		public new RevisionPasajeroInstance this[int i]
		{
			get
			{
				if(Count > i)
					return Array[i] as RevisionPasajeroInstance;
				else
					return new RevisionPasajeroInstance(OnContext);
			}
		}
		#endregion Indexer
	}
}
