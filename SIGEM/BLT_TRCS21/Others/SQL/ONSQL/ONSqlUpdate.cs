// 3.4.4.5

using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using SIGEM.Business.Types;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// ONSqlUpdate.
	/// </summary>
	public class ONSqlUpdate : ONSql
	{
		#region Members
		// Update
		protected string mTable;
		// Set
		protected StringCollection mFields;
		// Where
		protected StringCollection mWhereConjuncion;
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONSqlUpdate() : base()
		{
			mFields = new StringCollection();
			mWhereConjuncion = new StringCollection();
		}
		#endregion

		#region AddMethods
		/// <summary>
		/// Adds the table that will be updated
		/// </summary>
		/// <param name="className">Name of the table to put in the SQL sentence</param>
		public void AddUpdate(string className)
		{
			mTable = className;
		}
		/// <summary>
		/// Adds the values that will be updated in the DataBase
		/// </summary>
		/// <param name="fieldName">Name of the field to put in the SQL sentence</param>
		/// <param name="fieldValue">Value of the data to put in the SQL sentence</param>
		public void AddSet(string fieldName, ONSimpleType fieldValue)
		{
			mFields.Add(fieldName);
			AddParameter(fieldName, fieldValue);
		}
		/// <summary>
		/// Adds the WHERE part of the SQL sentence and adds tha parameter according to this part
		/// </summary>
		/// <param name="fieldName">Name of the field to put in the SQL sentence</param>
		/// <param name="fieldValue">Value of the data to put in the SQL sentence</param>
		public void AddWhere(string fieldName, ONSimpleType fieldValue)
		{
			mWhereConjuncion.Add(fieldName + " = ?");
			AddParameter(fieldName, fieldValue);
		}
		#endregion

		#region Clear / GenerateMethods
		public override void Clear()
		{
			mTable = "";;
			mFields.Clear();
			mWhereConjuncion.Clear();
			base.Clear();
		}
		/// <summary>
		/// Constructs all SQL sentence
		/// </summary>
		public override string GenerateSQL(out ArrayList sqlParameters)
		{
			bool lFirst = true;
			StringBuilder lTextBuilder = new StringBuilder();
			sqlParameters = ParametersItem;

			// UPDATE
			lTextBuilder.Append("UPDATE ");
			lTextBuilder.Append(mTable);

			// SET
			lTextBuilder.Append(" SET ");
			foreach (string lItem in mFields)
			{
				if (!lFirst)
					lTextBuilder.Append(", ");
				else
					lFirst = false;

				lTextBuilder.Append(lItem);
				lTextBuilder.Append(" = ?");
			}

			// WHERE
			StringBuilder lTextTempBuilder = new StringBuilder();
			foreach (string lItem in mWhereConjuncion)
			{
				if (lTextTempBuilder.Length != 0)
					lTextTempBuilder.Append(") AND (");

				lTextTempBuilder.Append(lItem);
			}
			if (lTextTempBuilder.Length != 0)
			{
				lTextBuilder.Append(" WHERE (");
				lTextBuilder.Append(lTextTempBuilder);
				lTextBuilder.Append(")");
			}

			return lTextBuilder.ToString();
		}
		#endregion
	}
}

