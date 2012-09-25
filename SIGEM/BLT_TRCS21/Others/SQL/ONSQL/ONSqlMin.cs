// 3.4.4.5

using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace SIGEM.Business.SQL
{
	public class ONSqlMin : ONSqlScalar
	{
		#region Constructors
		public ONSqlMin()
		{
		}
		public ONSqlMin(ONSqlSelect onSqlSelect) : base(onSqlSelect)
		{
		}
		public ONSqlMin(ONSqlSelect onSqlSelect, string variable) : base(onSqlSelect, variable)
		{
		}
		#endregion

		#region Select
		public void AddSelect(string alias, string attribute)
		{
			StringBuilder lTextBuilder = new StringBuilder();

			lTextBuilder.Append("MIN(");
			lTextBuilder.Append(alias);
			lTextBuilder.Append(".");
			lTextBuilder.Append(attribute);
			lTextBuilder.Append(")");

			mSelectAttributes.Add(lTextBuilder.ToString());
		}
		#endregion
	}
}

