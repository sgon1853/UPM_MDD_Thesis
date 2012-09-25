// 3.4.4.5

using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// ONSqlSum
	/// </summary>
	public class ONSqlSum : ONSqlScalar
	{
		#region Members
		/// <summary>
		/// Alias of the table where the value to sum is
		/// </summary>
		protected string mAlias;
		/// <summary>
		/// Field name where the value to sum is
		/// </summary>
		protected string mField;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONSqlSum()
		{
		}
		public ONSqlSum(ONSqlSelect onSqlSelect) : base(onSqlSelect)
		{
		}
		public ONSqlSum(ONSqlSelect onSqlSelect, string variable) : base(onSqlSelect, variable)
		{
		}
		#endregion Constructors

		#region Select
		/// <summary>
		/// Adds to the SELECT part of the SQL sentence the reserved word to do the required action for SUM
		/// </summary>
		public void AddSelect(string alias, string field)
		{
			mAlias = alias;
			mField = field;

			if (mDistinct.Count == 0)
			{
				StringBuilder lTextBuilder = new StringBuilder();

				lTextBuilder.Append("SUM(");
				lTextBuilder.Append(mAlias);
				lTextBuilder.Append(".");
				lTextBuilder.Append(mField);
				lTextBuilder.Append(")");

				mSelectAttributes.Add(lTextBuilder.ToString());
			}
			else
			{
				if (mSelectAttributes.Count == 0)
				{
					foreach (string lDistinct in mDistinct)
						mSelectAttributes.Add(lDistinct);
				}
				if (!mSelectAttributes.Contains(mAlias + "." + mField))
					mSelectAttributes.Add(mAlias + "." + mField);
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
			if (mDistinct.Count == 0)
				lSql.Append(base.GenerateSQL(out sqlParameters));
			else
			{
				lSql.Append("SELECT SUM(");
				lSql.Append("lAux.");
				lSql.Append(mField);
				lSql.Append(") FROM (");
				lSql.Append(base.GenerateSQL(out sqlParameters));
				lSql.Append(") lAux");
			}

			return lSql.ToString();
		}
		#endregion Generate Method
	}
}

