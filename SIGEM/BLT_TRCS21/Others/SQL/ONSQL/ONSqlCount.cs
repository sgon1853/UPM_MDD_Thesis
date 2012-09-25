// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// ONSqlCount
	/// </summary>
	public class ONSqlCount : ONSqlScalar
	{
		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONSqlCount()
		{
		}
		public ONSqlCount(ONSqlSelect onSqlSelect) : base(onSqlSelect)
		{
		}
		public ONSqlCount(ONSqlSelect onSqlSelect, string variable) : base(onSqlSelect, variable)
		{
		}
		#endregion

		#region Select
		/// <summary>
		/// Adds to the SELECT part of the SQL sentence the reserved word to do the required action for COUNT
		/// </summary>
		public override void AddSelect(string alias)
		{
			AddSelect();
		}
		/// <summary>
		/// Adds to the SELECT part of the SQL sentence the reserved word to do the required action for COUNT
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

