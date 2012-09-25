// 3.4.4.5
using System;
using System.Collections;
using SIGEM.Business.Types;
using SIGEM.Business.Instance;
using SIGEM.Business.Query;

namespace SIGEM.Business.OID
{
	internal class RevisionOid : ONOid
	{
		#region Members
		public ONInt Id_RevisarAeronaveAttr;
		#endregion

		#region Properties
		public override object Value
		{
			get
			{
				if (Id_RevisarAeronaveAttr == null) 
					return null;

				return this;
			}
			set
			{
				if (value == null)
				{
					Id_RevisarAeronaveAttr = ONInt.Null;
				}
				else
				{
					Copy(value);
				}
			}
		}
		public static RevisionOid Null
		{
			get
			{
				RevisionOid lNull = new RevisionOid();
				lNull.Value = null;
				return lNull;
			}
		}			

		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public RevisionOid() : base("Revision")
		{
			Id_RevisarAeronaveAttr = ONInt.Null;
		}

		public RevisionOid(RevisionOid oid) : base("Revision")
		{

			if (oid != null)
			{
				Id_RevisarAeronaveAttr = new ONInt(oid.Id_RevisarAeronaveAttr);
			}
			else
			{
				Id_RevisarAeronaveAttr = ONInt.Null;
			}
		}

		public override void Copy(object oid)
		{
			RevisionOid lOid = oid as RevisionOid;
			
			if (lOid != null)
			{
				Id_RevisarAeronaveAttr = new ONInt(lOid.Id_RevisarAeronaveAttr);
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
			ONInstance lInstance = new RevisionInstance(onContext);
			lInstance.Oid = this;
			lInstance = lInstance.Find(lOnFilterList);			
			if (lInstance == null)
				lInstance = new RevisionInstance(onContext);
			
			return lInstance;
		}

		public new RevisionInstance GetInstance(ONContext onContext)
		{
			return GetInstance(onContext, new ONFilterList());
		}

		public new RevisionInstance GetInstance(ONContext onContext, ONFilterList onFilterList)
		{
			return InhGetInstance(onContext, onFilterList) as RevisionInstance;
		}
		#endregion

		#region Operators
		public override bool Equals(object oid)
		{
			RevisionOid lOid = null;
			
			if (oid is RevisionOid)
				lOid = oid as RevisionOid;
			else if (oid is RevisionInstance)
				lOid = (oid as RevisionInstance).Oid;
			
			if ((object) lOid == null)
				return false;
			
			return (lOid.Id_RevisarAeronaveAttr.Equals(Id_RevisarAeronaveAttr));
		}

		public override int GetHashCode()
		{
			return Id_RevisarAeronaveAttr.GetHashCode();
		}
		#endregion
		
		#region Inheritance Castings
		#endregion

		#region Enumerator
		public override IEnumerator GetEnumerator()
		{
			ArrayList lArrayOfOid = new ArrayList();
			lArrayOfOid.Add(Id_RevisarAeronaveAttr);
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
			return "id_RevisarAeronave=" + Id_RevisarAeronaveAttr.TypedValue.ToString();
		}
		#endregion IDsToString
	}
}
