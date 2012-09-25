// 3.4.4.5
using System;
using System.Collections;
using SIGEM.Business.Types;
using SIGEM.Business.Instance;
using SIGEM.Business.Query;

namespace SIGEM.Business.OID
{
	internal class AeronaveOid : ONOid
	{
		#region Members
		public ONInt Id_AeronaveAttr;
		#endregion

		#region Properties
		public override object Value
		{
			get
			{
				if (Id_AeronaveAttr == null) 
					return null;

				return this;
			}
			set
			{
				if (value == null)
				{
					Id_AeronaveAttr = ONInt.Null;
				}
				else
				{
					Copy(value);
				}
			}
		}
		public static AeronaveOid Null
		{
			get
			{
				AeronaveOid lNull = new AeronaveOid();
				lNull.Value = null;
				return lNull;
			}
		}			

		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public AeronaveOid() : base("Aeronave")
		{
			Id_AeronaveAttr = ONInt.Null;
		}

		public AeronaveOid(AeronaveOid oid) : base("Aeronave")
		{

			if (oid != null)
			{
				Id_AeronaveAttr = new ONInt(oid.Id_AeronaveAttr);
			}
			else
			{
				Id_AeronaveAttr = ONInt.Null;
			}
		}

		public override void Copy(object oid)
		{
			AeronaveOid lOid = oid as AeronaveOid;
			
			if (lOid != null)
			{
				Id_AeronaveAttr = new ONInt(lOid.Id_AeronaveAttr);
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
			ONInstance lInstance = new AeronaveInstance(onContext);
			lInstance.Oid = this;
			lInstance = lInstance.Find(lOnFilterList);			
			if (lInstance == null)
				lInstance = new AeronaveInstance(onContext);
			
			return lInstance;
		}

		public new AeronaveInstance GetInstance(ONContext onContext)
		{
			return GetInstance(onContext, new ONFilterList());
		}

		public new AeronaveInstance GetInstance(ONContext onContext, ONFilterList onFilterList)
		{
			return InhGetInstance(onContext, onFilterList) as AeronaveInstance;
		}
		#endregion

		#region Operators
		public override bool Equals(object oid)
		{
			AeronaveOid lOid = null;
			
			if (oid is AeronaveOid)
				lOid = oid as AeronaveOid;
			else if (oid is AeronaveInstance)
				lOid = (oid as AeronaveInstance).Oid;
			
			if ((object) lOid == null)
				return false;
			
			return (lOid.Id_AeronaveAttr.Equals(Id_AeronaveAttr));
		}

		public override int GetHashCode()
		{
			return Id_AeronaveAttr.GetHashCode();
		}
		#endregion
		
		#region Inheritance Castings
		#endregion

		#region Enumerator
		public override IEnumerator GetEnumerator()
		{
			ArrayList lArrayOfOid = new ArrayList();
			lArrayOfOid.Add(Id_AeronaveAttr);
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
			return "id_Aeronave=" + Id_AeronaveAttr.TypedValue.ToString();
		}
		#endregion IDsToString
	}
}
