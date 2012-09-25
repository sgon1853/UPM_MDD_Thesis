// 3.4.4.5

using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// ONSqlForAll
	/// </summary>
	public class ONSqlForAll : ONSqlScalar
	{
		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONSqlForAll()
		{
		}
		public ONSqlForAll(ONSqlSelect onSqlSelect) : base(onSqlSelect)
		{
		}
		public ONSqlForAll(ONSqlSelect onSqlSelect, string variable) : base(onSqlSelect, variable)
		{
		}
		#endregion

		#region Select
		/// <summary>
		/// Adds to the SELECT part of the SQL sentence the reserved word to do the required action for FOR ALL
		/// </summary>
		public void AddSelect()
		{
			mSelectAttributes.Add("Count(*)");
		}
		#endregion

		#region Parenthesis
		public void AddParenthesis(string disjunction)
		{
			StringBuilder lTextBuilder = new StringBuilder();

			lTextBuilder.Append("NOT(");
			lTextBuilder.Append(disjunction);
			lTextBuilder.Append(")");

			mWhereConjuntion.Add(lTextBuilder.ToString());
		}
		#endregion
	}
}

