// 3.4.4.5

using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using SIGEM.Business.Types;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// Creates the SQL sentence that solves the deleting of data in Data Base
	/// </summary>
	public class ONSqlDelete : ONSql
	{
		#region Members
		// From
		protected string mTable;
		// Where
		protected StringCollection mFields;
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONSqlDelete() : base()
		{
			mFields = new StringCollection();
		}
		#endregion

		#region AddMethods
		/// <summary>
		/// Adds the table to do the request in the FROM part of the SQL sentence 
		/// </summary>
		/// <param name="table">Name of the table to put in the SQL sentence</param>
		public void AddFrom(string table)
		{
			mTable = table;
		}
		/// <summary>
		/// Adds the WHERE part of the SQL sentence and adds tha parameter according to this part
		/// </summary>
		/// <param name="fieldName">Name of the field to put in the SQL sentence</param>
		/// <param name="fieldValue">Value of the data to put in the SQL sentence</param>
		public void AddWhere(string fieldName, ONSimpleType fieldValue)
		{
			mFields.Add(fieldName);
			AddParameter(fieldName, fieldValue);
		}
		#endregion

		#region Clear / GenerateMethods
		public override void Clear()
		{
			mTable = "";
			mFields.Clear();
			base.Clear();
		}
		/// <summary>
		/// Constructs all SQL sentence
		/// </summary>
		public override string GenerateSQL(out ArrayList sqlParameters)
		{
			StringBuilder lTextBuilder = new StringBuilder();
			StringBuilder lTextBuilderTemp = new StringBuilder();
			sqlParameters = ParametersItem;

			// DELETE FROM
			lTextBuilder.Append("DELETE FROM ");
			lTextBuilder.Append(mTable);

			// WHERE
			foreach (string lItem in mFields)
			{
				if (lTextBuilderTemp.Length != 0)
					lTextBuilderTemp.Append(") AND (");

				lTextBuilderTemp.Append(lItem);
				lTextBuilderTemp.Append(" = ?");
			}
			if (lTextBuilderTemp.Length != 0)
			{
				lTextBuilder.Append(" WHERE (");
				lTextBuilder.Append(lTextBuilderTemp);
				lTextBuilder.Append(")");
			}

			return lTextBuilder.ToString();
		}
		#endregion
	}
}

