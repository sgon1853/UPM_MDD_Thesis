// 3.4.4.5

using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// ONSqlGetOne
	/// </summary>
	public class ONSqlGetOne: ONSqlSelect
	{
		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONSqlGetOne()
		{
		}
		public ONSqlGetOne(ONSqlSelect onSqlSelect) : base(onSqlSelect)
		{
		}
		public ONSqlGetOne(ONSqlSelect onSqlSelect, string variable) : base(onSqlSelect, variable)
		{
		}
		#endregion

		#region Select
		/// <summary>
		/// Adds to the SELECT part of the SQL sentence the reserved word to do the required action for Get One
		/// </summary>
		public void AddSelect(string alias, string attribute)
		{
			StringBuilder lTextBuilder = new StringBuilder();

			lTextBuilder.Append("TOP 1 ");
			lTextBuilder.Append(alias);
			lTextBuilder.Append(".");
			lTextBuilder.Append(attribute);

			mSelectAttributes.Add(lTextBuilder.ToString());
		}
		#endregion
	}
}

