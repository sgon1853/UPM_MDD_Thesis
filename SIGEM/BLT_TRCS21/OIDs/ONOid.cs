// 3.4.4.5

using System;
using System.Collections;
using SIGEM.Business.Types;
using SIGEM.Business.Instance;
using SIGEM.Business.Query;
using SIGEM.Business.Data;

namespace SIGEM.Business.OID
{
	/// <summary>
	/// Superclass of OIDs
	/// </summary>
	internal abstract class ONOid : IEnumerable, IONType
	{
		#region Members
		/// <summary>
		/// Class name of the oid
		/// </summary>
		public string ClassName;
		/// <summary>
		/// If it is an anonymous agent oid
		/// </summary>
		public bool IsAnonymousAgent;
		#endregion Members

		#region Properties
		public abstract object Value
		{
			get;set;
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="className">Name of the class that represents the OID</param>
		public ONOid(string className)
		{
			ClassName = className;
			IsAnonymousAgent = false;
		}
		/// <summary>
		/// Method of Copy
		/// </summary>
		/// <param name="oid">OID to be copied</param>
		public virtual void Copy(object oid)
		{
			ONOid lOID = oid as ONOid;

			if (lOID != null)
			{
				ClassName = lOID.ClassName;
			}
		}
		#endregion Constructors
		
		/// <summary>
		/// Checks if a determinate instance exists
		/// </summary>
		/// <param name="onContext">Context</param>
		/// <param name="onFilterList">Filters to theck</param>
		/// <returns>If exists</returns>
		public bool Exist(ONContext onContext, ONFilterList onFilterList)
		{
			ONData lData = ONContext.GetComponent_Data(ClassName, onContext);

			return lData.Exist(this, onFilterList);
		}

		#region GetInstance
		/// <summary>
		/// Retrieve the instance that represents the OID
		/// </summary>
		/// <param name="onContext">Context with all the information about the execution of the request</param>
		public ONInstance GetInstance(ONContext onContext)
		{
			return GetInstance(onContext, new ONFilterList());
		}
		/// <summary>
		/// Retrieve the instance that represents the OID
		/// </summary>
		/// <param name="onContext">Context with all the information about the execution of the request</param>
		public ONInstance GetInstance(ONContext onContext, ONFilterList onFilterList)
		{
			return InhGetInstance(onContext, onFilterList);
		}
		protected abstract ONInstance InhGetInstance(ONContext onContext);
		protected abstract ONInstance InhGetInstance(ONContext onContext, ONFilterList onFilterList);
		#endregion

		#region Operator (== !=)
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public static ONBool operator==(ONOid obj1, ONOid obj2)
		{
			if ((((object) obj1 == null) || (obj1.Value == null)) && (((object) obj2 == null) || (obj2.Value == null)))
				return new ONBool(true);

			if (((object) obj1 == null) || (obj1.Value == null) || ((object) obj2 == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.Equals(obj2));
		}
		public static ONBool operator!=(ONOid obj1, ONOid obj2)
		{
			return !(obj1 == obj2);
		}
		#endregion

		#region Enumerator
		public abstract IEnumerator GetEnumerator();
		#endregion
		
		#region Count
		public abstract int Count();
		#endregion
		
		#region IDsToString
		public abstract string IDsToString();
		#endregion IDsToString		
	}
}

