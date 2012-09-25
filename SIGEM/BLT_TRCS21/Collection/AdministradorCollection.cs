// 3.4.4.5
using System;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Collection
{
	internal class AdministradorCollection : ONCollection
	{
		#region Members
		#endregion

		#region Properties
		public AdministradorOid Oid
		{
			get
			{
				if (Count > 0)
					return this[0].Oid;
				else
					return AdministradorOid.Null;
			}
		}
		[ONAttribute("id_Administrador", "autonumeric")]
		public ONInt Id_AdministradorAttr
		{
			get
			{
				if (Count > 0)
					return this[0].Id_AdministradorAttr;
				else
					return ONInt.Null;
			}
		}
		#endregion Properties
		
		#region  Constructors
		public AdministradorCollection(ONContext onContext)
			:base(onContext, "Administrador", false)
		{
		}
		#endregion Constructors
		
		#region Indexer
		public new AdministradorInstance this[int i]
		{
			get
			{
				if(Count > i)
					return Array[i] as AdministradorInstance;
				else
					return new AdministradorInstance(OnContext);
			}
		}
		#endregion Indexer
	}
}
