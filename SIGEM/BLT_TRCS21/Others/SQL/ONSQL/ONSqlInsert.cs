// 3.4.4.5

using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using SIGEM.Business.Types;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// ONSqlInsert.
	/// </summary>
	public class ONSqlInsert : ONSql
	{
		#region Members
		// Into
		protected string mTable;
		// Values
		protected StringCollection mFields;
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONSqlInsert() : base()
		{
			mFields = new StringCollection();
		}
		#endregion

		#region AddMethods
		/// <summary>
		/// Adds the table to do the request in the FROM part of the SQL sentence 
		/// </summary>
		/// <param name="table">Name of the table to put in the SQL sentence</param>
		public void AddInto(string table)
		{
			mTable = table;
		}
		/// <summary>
		/// Adds the fields and their values needed to insert into data base
		/// </summary>
		/// <param name="fieldName">Name of the field where the value will be inserted</param>
		/// <param name="fieldValue">Value to be inserted in the Data Base</param>
		public void AddValue(string fieldName, string fieldValue)
		{
			mFields.Add(fieldName);
			AddParameter(fieldName, fieldValue);
		}		
		/// <summary>
		/// Adds the fields and their values needed to insert into data base
		/// </summary>
		/// <param name="fieldName">Name of the field where the value will be inserted</param>
		/// <param name="fieldName">Value to be inserted in the Data Base</param>
		public void AddValue(string fieldName, ONSimpleType fieldValue)
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
		/// Constructs all the SQL sentence
		/// </summary>
		public override string GenerateSQL(out ArrayList sqlParameters)
		{
			StringBuilder lTextBuilder = new StringBuilder();
			StringBuilder lTextBuilderTemp1 = new StringBuilder();
			StringBuilder lTextBuilderTemp2 = new StringBuilder();
			sqlParameters = ParametersItem;

			// INSERT INTO
			lTextBuilder.Append("INSERT INTO ");
			lTextBuilder.Append(mTable);

			// FIELDS
			lTextBuilder.Append(" (");
			foreach (string lItem in mFields)
			{
				if (lTextBuilderTemp1.Length != 0)
				{
					lTextBuilderTemp1.Append(", ");
					lTextBuilderTemp2.Append(", ");
				}

				lTextBuilderTemp1.Append(lItem);
				lTextBuilderTemp2.Append("?");
			}
			lTextBuilder.Append(lTextBuilderTemp1);
			lTextBuilder.Append(") VALUES (");
			lTextBuilder.Append(lTextBuilderTemp2);
			lTextBuilder.Append(")");

			return lTextBuilder.ToString();
		}
		#endregion
	}
}

