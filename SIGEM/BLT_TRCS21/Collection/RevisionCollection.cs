// 3.4.4.5
using System;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Collection
{
	internal class RevisionCollection : ONCollection
	{
		#region Members
		public RevisionPasajeroCollection RevisionPasajeroRoleTemp;
		#endregion

		#region Properties
		public RevisionOid Oid
		{
			get
			{
				if (Count > 0)
					return this[0].Oid;
				else
					return RevisionOid.Null;
			}
		}
		[ONAttribute("id_RevisarAeronave", "autonumeric")]
		public ONInt Id_RevisarAeronaveAttr
		{
			get
			{
				if (Count > 0)
					return this[0].Id_RevisarAeronaveAttr;
				else
					return ONInt.Null;
			}
		}
		[ONAttribute("NombreRevisor", "string")]
		public ONString NombreRevisorAttr
		{
			get
			{
				if (Count > 0)
					return this[0].NombreRevisorAttr;
				else
					return ONString.Null;
			}
		}
		[ONAttribute("FechaRevision", "date")]
		public ONDate FechaRevisionAttr
		{
			get
			{
				if (Count > 0)
					return this[0].FechaRevisionAttr;
				else
					return ONDate.Null;
			}
		}
		[ONAttribute("Id_Aeronave", "string")]
		public ONString Id_AeronaveAttr
		{
			get
			{
				if (Count > 0)
					return this[0].Id_AeronaveAttr;
				else
					return ONString.Null;
			}
		}
		[ONRole("RevisionPasajero", "RevisionPasajero", "Revision", "Revision")]
		public RevisionPasajeroCollection RevisionPasajeroRole
		{
			get
			{
				if (RevisionPasajeroRoleTemp == null)
				{
					RevisionPasajeroRoleTemp = new RevisionPasajeroCollection(OnContext);
					
					foreach ( RevisionInstance lInstance in Array)
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
		public RevisionCollection(ONContext onContext)
			:base(onContext, "Revision", false)
		{
		}
		#endregion Constructors
		
		#region Indexer
		public new RevisionInstance this[int i]
		{
			get
			{
				if(Count > i)
					return Array[i] as RevisionInstance;
				else
					return new RevisionInstance(OnContext);
			}
		}
		#endregion Indexer
	}
}
