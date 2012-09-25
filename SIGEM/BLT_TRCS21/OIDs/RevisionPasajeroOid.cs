// 3.4.4.5
using System;
using System.Collections;
using SIGEM.Business.Types;
using SIGEM.Business.Instance;
using SIGEM.Business.Query;

namespace SIGEM.Business.OID
{
	internal class RevisionPasajeroOid : ONOid
	{
		#region Members
		public ONInt Id_RevisionPasajeroAttr;
		#endregion

		#region Properties
		public override object Value
		{
			get
			{
				if (Id_RevisionPasajeroAttr == null) 
					return null;

				return this;
			}
			set
			{
				if (value == null)
				{
					Id_RevisionPasajeroAttr = ONInt.Null;
				}
				else
				{
					Copy(value);
				}
			}
		}
		public static RevisionPasajeroOid Null
		{
			get
			{
				RevisionPasajeroOid lNull = new RevisionPasajeroOid();
				lNull.Value = null;
				return lNull;
			}
		}			

		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public RevisionPasajeroOid() : base("RevisionPasajero")
		{
			Id_RevisionPasajeroAttr = ONInt.Null;
		}

		public RevisionPasajeroOid(RevisionPasajeroOid oid) : base("RevisionPasajero")
		{

			if (oid != null)
			{
				Id_RevisionPasajeroAttr = new ONInt(oid.Id_RevisionPasajeroAttr);
			}
			else
			{
				Id_RevisionPasajeroAttr = ONInt.Null;
			}
		}

		public override void Copy(object oid)
		{
			RevisionPasajeroOid lOid = oid as RevisionPasajeroOid;
			
			if (lOid != null)
			{
				Id_RevisionPasajeroAttr = new ONInt(lOid.Id_RevisionPasajeroAttr);
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
			ONInstance lInstance = new RevisionPasajeroInstance(onContext);
			lInstance.Oid = this;
			lInstance = lInstance.Find(lOnFilterList);			
			if (lInstance == null)
				lInstance = new RevisionPasajeroInstance(onContext);
			
			return lInstance;
		}

		public new RevisionPasajeroInstance GetInstance(ONContext onContext)
		{
			return GetInstance(onContext, new ONFilterList());
		}

		public new RevisionPasajeroInstance GetInstance(ONContext onContext, ONFilterList onFilterList)
		{
			return InhGetInstance(onContext, onFilterList) as RevisionPasajeroInstance;
		}
		#endregion

		#region Operators
		public override bool Equals(object oid)
		{
			RevisionPasajeroOid lOid = null;
			
			if (oid is RevisionPasajeroOid)
				lOid = oid as RevisionPasajeroOid;
			else if (oid is RevisionPasajeroInstance)
				lOid = (oid as RevisionPasajeroInstance).Oid;
			
			if ((object) lOid == null)
				return false;
			
			return (lOid.Id_RevisionPasajeroAttr.Equals(Id_RevisionPasajeroAttr));
		}

		public override int GetHashCode()
		{
			return Id_RevisionPasajeroAttr.GetHashCode();
		}
		#endregion
		
		#region Inheritance Castings
		#endregion

		#region Enumerator
		public override IEnumerator GetEnumerator()
		{
			ArrayList lArrayOfOid = new ArrayList();
			lArrayOfOid.Add(Id_RevisionPasajeroAttr);
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
			return "id_RevisionPasajero=" + Id_RevisionPasajeroAttr.TypedValue.ToString();
		}
		#endregion IDsToString
	}
}
