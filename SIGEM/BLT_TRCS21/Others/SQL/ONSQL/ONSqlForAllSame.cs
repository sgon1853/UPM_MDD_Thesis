// 3.4.4.5

using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// ONSqlForAllSame
	/// </summary>
	public class ONSqlForAllSame : ONSqlScalar
	{
		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONSqlForAllSame()
		{
		}
		public ONSqlForAllSame(ONSqlSelect onSqlSelect) : base(onSqlSelect)
		{
		}
		public ONSqlForAllSame(ONSqlSelect onSqlSelect, string variable) : base(onSqlSelect, variable)
		{
		}
		#endregion

		#region Select
		/// <summary>
		/// Adds to the SELECT part of the SQL sentence the reserved word to do the required action for ForAll Same
		/// </summary>
		public void AddSelect(string alias, string attribute)
		{
			StringBuilder lTextBuilder = new StringBuilder();

			lTextBuilder.Append("COUNT(DISTINCT ");
			lTextBuilder.Append(alias);
			lTextBuilder.Append(".");
			lTextBuilder.Append(attribute);
			lTextBuilder.Append(")");

			mSelectAttributes.Add(lTextBuilder.ToString());
		}
		#endregion
	}
}

