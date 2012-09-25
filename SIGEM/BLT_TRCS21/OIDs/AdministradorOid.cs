// 3.4.4.5
using System;
using System.Collections;
using SIGEM.Business.Types;
using SIGEM.Business.Instance;
using SIGEM.Business.Query;

namespace SIGEM.Business.OID
{
	internal class AdministradorOid : ONOid
	{
		#region Members
		public ONInt Id_AdministradorAttr;
		#endregion

		#region Properties
		public override object Value
		{
			get
			{
				if (Id_AdministradorAttr == null) 
					return null;

				return this;
			}
			set
			{
				if (value == null)
				{
					Id_AdministradorAttr = ONInt.Null;
				}
				else
				{
					Copy(value);
				}
			}
		}
		public static AdministradorOid Null
		{
			get
			{
				AdministradorOid lNull = new AdministradorOid();
				lNull.Value = null;
				return lNull;
			}
		}			

		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public AdministradorOid() : base("Administrador")
		{
			Id_AdministradorAttr = ONInt.Null;
		}

		public AdministradorOid(AdministradorOid oid) : base("Administrador")
		{

			if (oid != null)
			{
				Id_AdministradorAttr = new ONInt(oid.Id_AdministradorAttr);
			}
			else
			{
				Id_AdministradorAttr = ONInt.Null;
			}
		}

		public override void Copy(object oid)
		{
			AdministradorOid lOid = oid as AdministradorOid;
			
			if (lOid != null)
			{
				Id_AdministradorAttr = new ONInt(lOid.Id_AdministradorAttr);
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
			ONInstance lInstance = new AdministradorInstance(onContext);
			lInstance.Oid = this;
			lInstance = lInstance.Find(lOnFilterList);			
			if (lInstance == null)
				lInstance = new AdministradorInstance(onContext);
			
			return lInstance;
		}

		public new AdministradorInstance GetInstance(ONContext onContext)
		{
			return GetInstance(onContext, new ONFilterList());
		}

		public new AdministradorInstance GetInstance(ONContext onContext, ONFilterList onFilterList)
		{
			return InhGetInstance(onContext, onFilterList) as AdministradorInstance;
		}
		#endregion

		#region Operators
		public override bool Equals(object oid)
		{
			AdministradorOid lOid = null;
			
			if (oid is AdministradorOid)
				lOid = oid as AdministradorOid;
			else if (oid is AdministradorInstance)
				lOid = (oid as AdministradorInstance).Oid;
			
			if ((object) lOid == null)
				return false;
			
			return (lOid.Id_AdministradorAttr.Equals(Id_AdministradorAttr));
		}

		public override int GetHashCode()
		{
			return Id_AdministradorAttr.GetHashCode();
		}
		#endregion
		
		#region Inheritance Castings
		#endregion

		#region Enumerator
		public override IEnumerator GetEnumerator()
		{
			ArrayList lArrayOfOid = new ArrayList();
			lArrayOfOid.Add(Id_AdministradorAttr);
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
			return "id_Administrador=" + Id_AdministradorAttr.TypedValue.ToString();
		}
		#endregion IDsToString
	}
}
