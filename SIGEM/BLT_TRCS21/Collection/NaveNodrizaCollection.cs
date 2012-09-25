// 3.4.4.5
using System;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.Attributes;
using SIGEM.Business.Instance;

namespace SIGEM.Business.Collection
{
	internal class NaveNodrizaCollection : ONCollection
	{
		#region Members
		#endregion

		#region Properties
		public NaveNodrizaOid Oid
		{
			get
			{
				if (Count > 0)
					return this[0].Oid;
				else
					return NaveNodrizaOid.Null;
			}
		}
		[ONAttribute("id_NaveNodriza", "autonumeric")]
		public ONInt Id_NaveNodrizaAttr
		{
			get
			{
				if (Count > 0)
					return this[0].Id_NaveNodrizaAttr;
				else
					return ONInt.Null;
			}
		}
		[ONAttribute("Nombre_NaveNodriza", "string")]
		public ONString Nombre_NaveNodrizaAttr
		{
			get
			{
				if (Count > 0)
					return this[0].Nombre_NaveNodrizaAttr;
				else
					return ONString.Null;
			}
		}
		#endregion Properties
		
		#region  Constructors
		public NaveNodrizaCollection(ONContext onContext)
			:base(onContext, "NaveNodriza", false)
		{
		}
		#endregion Constructors
		
		#region Indexer
		public new NaveNodrizaInstance this[int i]
		{
			get
			{
				if(Count > i)
					return Array[i] as NaveNodrizaInstance;
				else
					return new NaveNodrizaInstance(OnContext);
			}
		}
		#endregion Indexer
	}
}
