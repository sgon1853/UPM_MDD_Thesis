// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Specialized;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// ONSqlExist
	/// </summary>
	public class ONSqlExist : ONSqlScalar
	{
		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONSqlExist()
		{
		}
		public ONSqlExist(ONSqlSelect onSqlSelect) : base(onSqlSelect)
		{
		}
		public ONSqlExist(ONSqlSelect onSqlSelect, string variable) : base(onSqlSelect, variable)
		{
		}
		#endregion

		#region Select
		/// <summary>
		/// Adds to the SELECT part of the SQL sentence the reserved word to do the required action for EXIST
		/// </summary>
		public void AddSelect()
		{
			if (SuperQuery != null)
				mSelectAttributes.Add("*");
			else
				mSelectAttributes.Add("Count(*)");
		}
		#endregion
	}
}

