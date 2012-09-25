// 3.4.4.5

using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using SIGEM.Business.Types;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// ONSqlInsertAutoInc.
	/// </summary>
	public class ONSqlInsertAutoInc : ONSqlInsert
	{
		#region Members
		// Sequence Name
		protected string mSequenceName;
		// AutoIncrementable Field Name
		protected string mAutoIncFieldName;
		#endregion
	
		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONSqlInsertAutoInc() : base()
		{
		}
		#endregion
	
		#region AddMethods
		/// <summary>
		/// Adds the name of the sequence generator needed in some RDBMS 
		/// </summary>
		/// <param name="sequence">Name of the sequence</param>
		public void AddSequenceName(string sequenceName)
		{
			mSequenceName = sequenceName;
		}
		/// <summary>
		/// Adds the name of the incrementable field name 
		/// </summary>
		/// <param name="table">Name of the field</param>
		public void AddAutoIncFieldName(string fieldName)
		{
			mAutoIncFieldName = fieldName;
		}
		#endregion
	
		#region GenerateMethods
		/// <summary>
		/// Constructs all the SQL sentence
		/// </summary>
		public override string GenerateSQL(out ArrayList sqlParameters)
		{
			StringBuilder lTextBuilder = new StringBuilder();
	
			lTextBuilder.Append("; SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]");
	
			return base.GenerateSQL(out sqlParameters) + lTextBuilder.ToString();
		}
		#endregion
	}
}

