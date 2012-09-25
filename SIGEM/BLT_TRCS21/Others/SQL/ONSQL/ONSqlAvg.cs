// 3.4.4.5

using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// ONSqlAvg
	/// </summary>
	public class ONSqlAvg : ONSqlScalar
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
		/// <summary>
		/// Oasis type of the model attribute that represents
		/// </summary>
		protected string mOasisType;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONSqlAvg()
		{
		}
		public ONSqlAvg(ONSqlSelect onSqlSelect) : base(onSqlSelect)
		{
		}
		public ONSqlAvg(ONSqlSelect onSqlSelect, string variable) : base(onSqlSelect, variable)
		{
		}
		#endregion Constructors

		#region Select
		/// <summary>
		/// Adds to the SELECT part of the SQL sentence the reserved word to do the required action for AVG
		/// </summary>
		/// <param name="alias">Table alias of the field</param>
		/// <param name="field">Field name to calculate the average</param>
		public void AddSelect(string alias, string field)
		{
			AddSelect(alias, field, "real");
		}
		/// <summary>
		/// Adds to the SELECT part of the SQL sentence the reserved word to do the required action for AVG
		/// </summary>
		/// <param name="alias">Table alias of the field</param>
		/// <param name="field">Field name to calculate the average</param>
		/// <param name="oasisType">Oasis type of the model attribute that represents</param>
		public void AddSelect(string alias, string field, string oasisType)
		{
			mAlias = alias;
			mField = field;
			mOasisType = oasisType;

			if (mDistinct.Count == 0)
			{
				StringBuilder lTextBuilder = new StringBuilder();

				if (string.Compare(oasisType, "real", true) == 0)
				{
					lTextBuilder.Append("AVG(");
					lTextBuilder.Append(mAlias);
					lTextBuilder.Append(".");
					lTextBuilder.Append(mField);
					lTextBuilder.Append(")");
				}
				else
				{
					lTextBuilder.Append("AVG(CAST(");
					lTextBuilder.Append(mAlias);
					lTextBuilder.Append(".");
					lTextBuilder.Append(mField);
					lTextBuilder.Append(" AS DECIMAL(19,6)))");
				}

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

				lSql.Append("SELECT ");
				if (string.Compare(mOasisType, "real", true) == 0)
				{
					lSql.Append("AVG(");
					lSql.Append("lAux.");
					lSql.Append(mField);
					lSql.Append(")");
				}
				else
				{
					lSql.Append("AVG(CAST(");
					lSql.Append("lAux.");
					lSql.Append(mField);
					lSql.Append(" AS DECIMAL(19,6)))");
				}

				lSql.Append(" FROM (");
				lSql.Append(base.GenerateSQL(out sqlParameters));
				lSql.Append(") lAux");
			}

			return lSql.ToString();
		}
		#endregion Generate Method
	}
}

