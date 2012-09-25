// 3.4.4.5
using System;
using System.Collections;
using SIGEM.Business.Types;
using SIGEM.Business.Instance;
using SIGEM.Business.Query;

namespace SIGEM.Business.OID
{
	internal class PasajeroOid : ONOid
	{
		#region Members
		public ONInt Id_PasajeroAttr;
		#endregion

		#region Properties
		public override object Value
		{
			get
			{
				if (Id_PasajeroAttr == null) 
					return null;

				return this;
			}
			set
			{
				if (value == null)
				{
					Id_PasajeroAttr = ONInt.Null;
				}
				else
				{
					Copy(value);
				}
			}
		}
		public static PasajeroOid Null
		{
			get
			{
				PasajeroOid lNull = new PasajeroOid();
				lNull.Value = null;
				return lNull;
			}
		}			

		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public PasajeroOid() : base("Pasajero")
		{
			Id_PasajeroAttr = ONInt.Null;
		}

		public PasajeroOid(PasajeroOid oid) : base("Pasajero")
		{

			if (oid != null)
			{
				Id_PasajeroAttr = new ONInt(oid.Id_PasajeroAttr);
			}
			else
			{
				Id_PasajeroAttr = ONInt.Null;
			}
		}

		public override void Copy(object oid)
		{
			PasajeroOid lOid = oid as PasajeroOid;
			
			if (lOid != null)
			{
				Id_PasajeroAttr = new ONInt(lOid.Id_PasajeroAttr);
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
			ONInstance lInstance = new PasajeroInstance(onContext);
			lInstance.Oid = this;
			lInstance = lInstance.Find(lOnFilterList);			
			if (lInstance == null)
				lInstance = new PasajeroInstance(onContext);
			
			return lInstance;
		}

		public new PasajeroInstance GetInstance(ONContext onContext)
		{
			return GetInstance(onContext, new ONFilterList());
		}

		public new PasajeroInstance GetInstance(ONContext onContext, ONFilterList onFilterList)
		{
			return InhGetInstance(onContext, onFilterList) as PasajeroInstance;
		}
		#endregion

		#region Operators
		public override bool Equals(object oid)
		{
			PasajeroOid lOid = null;
			
			if (oid is PasajeroOid)
				lOid = oid as PasajeroOid;
			else if (oid is PasajeroInstance)
				lOid = (oid as PasajeroInstance).Oid;
			
			if ((object) lOid == null)
				return false;
			
			return (lOid.Id_PasajeroAttr.Equals(Id_PasajeroAttr));
		}

		public override int GetHashCode()
		{
			return Id_PasajeroAttr.GetHashCode();
		}
		#endregion
		
		#region Inheritance Castings
		#endregion

		#region Enumerator
		public override IEnumerator GetEnumerator()
		{
			ArrayList lArrayOfOid = new ArrayList();
			lArrayOfOid.Add(Id_PasajeroAttr);
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
			return "id_Pasajero=" + Id_PasajeroAttr.TypedValue.ToString();
		}
		#endregion IDsToString
	}
}
