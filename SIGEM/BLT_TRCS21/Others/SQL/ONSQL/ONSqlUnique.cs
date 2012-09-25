// 3.4.4.5

using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// ONSqlUnique
	/// </summary>
	public class ONSqlUnique : ONSqlScalar
	{
		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONSqlUnique()
		{
		}
		public ONSqlUnique(ONSqlSelect onSqlSelect) : base(onSqlSelect)
		{
		}
		public ONSqlUnique(ONSqlSelect onSqlSelect, string variable) : base(onSqlSelect, variable)
		{
		}
		#endregion Constructors

		#region Select
		/// <summary>
		/// Adds to the SELECT part of the SQL sentence the reserved word to do the required action for UNIQUE
		/// </summary>
		public override void AddSelect(string alias)
		{
			AddSelect();
		}
		/// <summary>
		/// Adds to the SELECT part of the SQL sentence the reserved word to do the required action for UNIQUE
		/// </summary>
		public void AddSelect()
		{
			if (mDistinct.Count == 0)
			{
				if (mSelectAttributes.Count == 0)
					mSelectAttributes.Add("COUNT(*)");
			}
			else if (mDistinct.Count == 1)
			{
				if (mSelectAttributes.Count == 0)
					mSelectAttributes.Add("COUNT(DISTINCT " + mDistinct[0] + ")");
			}
			else
			{
				if (mSelectAttributes.Count == 0)
				{
					foreach (string lDistinct in mDistinct)
						mSelectAttributes.Add(lDistinct);
				}
			}
		}
		#endregion Select

		#region Generate Method
		/// <summary>
		/// Generate Select Sql
		/// </summary>
		/// <param name="sqlParameters">Sql parameters</param>
		/// <returns>Sql string</returns>
		public override string GenerateSQL(out ArrayList sqlParameters)
		{
			StringBuilder lSql = new StringBuilder();
			if (mDistinct.Count <= 1)
				lSql.Append(base.GenerateSQL(out sqlParameters));
			else
			{
				lSql.Append("SELECT COUNT(*) FROM (");
				lSql.Append(base.GenerateSQL(out sqlParameters));
				lSql.Append(") lAux");
			}

			return lSql.ToString();
		}
		#endregion Generate Method
	}
}

