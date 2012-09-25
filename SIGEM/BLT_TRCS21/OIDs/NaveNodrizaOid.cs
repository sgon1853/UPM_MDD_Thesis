// 3.4.4.5
using System;
using System.Collections;
using SIGEM.Business.Types;
using SIGEM.Business.Instance;
using SIGEM.Business.Query;

namespace SIGEM.Business.OID
{
	internal class NaveNodrizaOid : ONOid
	{
		#region Members
		public ONInt Id_NaveNodrizaAttr;
		#endregion

		#region Properties
		public override object Value
		{
			get
			{
				if (Id_NaveNodrizaAttr == null) 
					return null;

				return this;
			}
			set
			{
				if (value == null)
				{
					Id_NaveNodrizaAttr = ONInt.Null;
				}
				else
				{
					Copy(value);
				}
			}
		}
		public static NaveNodrizaOid Null
		{
			get
			{
				NaveNodrizaOid lNull = new NaveNodrizaOid();
				lNull.Value = null;
				return lNull;
			}
		}			

		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public NaveNodrizaOid() : base("NaveNodriza")
		{
			Id_NaveNodrizaAttr = ONInt.Null;
		}

		public NaveNodrizaOid(NaveNodrizaOid oid) : base("NaveNodriza")
		{

			if (oid != null)
			{
				Id_NaveNodrizaAttr = new ONInt(oid.Id_NaveNodrizaAttr);
			}
			else
			{
				Id_NaveNodrizaAttr = ONInt.Null;
			}
		}

		public override void Copy(object oid)
		{
			NaveNodrizaOid lOid = oid as NaveNodrizaOid;
			
			if (lOid != null)
			{
				Id_NaveNodrizaAttr = new ONInt(lOid.Id_NaveNodrizaAttr);
			}
			base.Copy(oid);
		}
		#endregion

		#region GetInstance
		protected override ONInstance InhGetInstance(ONContext onContext)
		{
			return InhGetInstance(onContext, new ONFilterList());
		}

		protected override ONInstance InhGetInstance(ONContext onContext, ONFilterList onFilterList)
		{
			// Add oid filter to the list
			ONFilterList lOnFilterList = new ONFilterList(onFilterList);
			lOnFilterList.Add("QueryByOid", new QueryByOidFilter(this));
			
			// Search Instance
			ONInstance lInstance = new NaveNodrizaInstance(onContext);
			lInstance.Oid = this;
			lInstance = lInstance.Find(lOnFilterList);			
			if (lInstance == null)
				lInstance = new NaveNodrizaInstance(onContext);
			
			return lInstance;
		}

		public new NaveNodrizaInstance GetInstance(ONContext onContext)
		{
			return GetInstance(onContext, new ONFilterList());
		}

		public new NaveNodrizaInstance GetInstance(ONContext onContext, ONFilterList onFilterList)
		{
			return InhGetInstance(onContext, onFilterList) as NaveNodrizaInstance;
		}
		#endregion

		#region Operators
		public override bool Equals(object oid)
		{
			NaveNodrizaOid lOid = null;
			
			if (oid is NaveNodrizaOid)
				lOid = oid as NaveNodrizaOid;
			else if (oid is NaveNodrizaInstance)
				lOid = (oid as NaveNodrizaInstance).Oid;
			
			if ((object) lOid == null)
				return false;
			
			return (lOid.Id_NaveNodrizaAttr.Equals(Id_NaveNodrizaAttr));
		}

		public override int GetHashCode()
		{
			return Id_NaveNodrizaAttr.GetHashCode();
		}
		#endregion
		
		#region Inheritance Castings
		#endregion

		#region Enumerator
		public override IEnumerator GetEnumerator()
		{
			ArrayList lArrayOfOid = new ArrayList();
			lArrayOfOid.Add(Id_NaveNodrizaAttr);
			return lArrayOfOid.GetEnumerator();
		}
		#endregion

		#region Translate_Count
		public override int Count()
		{
			return 1;
		}
		#endregion
		
		#region IDsToString
		public override string IDsToString()
		{
			return "id_NaveNodriza=" + Id_NaveNodrizaAttr.TypedValue.ToString();
		}
		#endregion IDsToString
	}
}
