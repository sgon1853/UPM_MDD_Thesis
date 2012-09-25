// 3.4.4.5
using System;
using System.Collections;
using SIGEM.Business.Types;
using SIGEM.Business.Instance;
using SIGEM.Business.Query;

namespace SIGEM.Business.OID
{
	internal class PasajeroAeronaveOid : ONOid
	{
		#region Members
		public ONInt Id_PasajeroAeronaveAttr;
		#endregion

		#region Properties
		public override object Value
		{
			get
			{
				if (Id_PasajeroAeronaveAttr == null) 
					return null;

				return this;
			}
			set
			{
				if (value == null)
				{
					Id_PasajeroAeronaveAttr = ONInt.Null;
				}
				else
				{
					Copy(value);
				}
			}
		}
		public static PasajeroAeronaveOid Null
		{
			get
			{
				PasajeroAeronaveOid lNull = new PasajeroAeronaveOid();
				lNull.Value = null;
				return lNull;
			}
		}			

		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public PasajeroAeronaveOid() : base("PasajeroAeronave")
		{
			Id_PasajeroAeronaveAttr = ONInt.Null;
		}

		public PasajeroAeronaveOid(PasajeroAeronaveOid oid) : base("PasajeroAeronave")
		{

			if (oid != null)
			{
				Id_PasajeroAeronaveAttr = new ONInt(oid.Id_PasajeroAeronaveAttr);
			}
			else
			{
				Id_PasajeroAeronaveAttr = ONInt.Null;
			}
		}

		public override void Copy(object oid)
		{
			PasajeroAeronaveOid lOid = oid as PasajeroAeronaveOid;
			
			if (lOid != null)
			{
				Id_PasajeroAeronaveAttr = new ONInt(lOid.Id_PasajeroAeronaveAttr);
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
			ONInstance lInstance = new PasajeroAeronaveInstance(onContext);
			lInstance.Oid = this;
			lInstance = lInstance.Find(lOnFilterList);			
			if (lInstance == null)
				lInstance = new PasajeroAeronaveInstance(onContext);
			
			return lInstance;
		}

		public new PasajeroAeronaveInstance GetInstance(ONContext onContext)
		{
			return GetInstance(onContext, new ONFilterList());
		}

		public new PasajeroAeronaveInstance GetInstance(ONContext onContext, ONFilterList onFilterList)
		{
			return InhGetInstance(onContext, onFilterList) as PasajeroAeronaveInstance;
		}
		#endregion

		#region Operators
		public override bool Equals(object oid)
		{
			PasajeroAeronaveOid lOid = null;
			
			if (oid is PasajeroAeronaveOid)
				lOid = oid as PasajeroAeronaveOid;
			else if (oid is PasajeroAeronaveInstance)
				lOid = (oid as PasajeroAeronaveInstance).Oid;
			
			if ((object) lOid == null)
				return false;
			
			return (lOid.Id_PasajeroAeronaveAttr.Equals(Id_PasajeroAeronaveAttr));
		}

		public override int GetHashCode()
		{
			return Id_PasajeroAeronaveAttr.GetHashCode();
		}
		#endregion
		
		#region Inheritance Castings
		#endregion

		#region Enumerator
		public override IEnumerator GetEnumerator()
		{
			ArrayList lArrayOfOid = new ArrayList();
			lArrayOfOid.Add(Id_PasajeroAeronaveAttr);
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
			return "id_PasajeroAeronave=" + Id_PasajeroAeronaveAttr.TypedValue.ToString();
		}
		#endregion IDsToString
	}
}
