// 3.4.4.5

using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using SIGEM.Business.Types;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// Base class of the other classes focused in the construction of SQL sentences 
	/// </summary>
	public abstract class ONSql
	{
		#region Members
		// Parameters
		public StringCollection ParametersName;
        private ArrayList mParametersItem;
		#endregion

        #region Properties
        public virtual ArrayList ParametersItem
        {
            get 
            {
                return mParametersItem;
            }
        }
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ONSql()
		{
			ParametersName = new StringCollection();
			mParametersItem = new ArrayList();
		}
		#endregion

		#region AddMethods
		/// <summary>
		/// Add the parameter to the list of parameters of the SQL sentence
		/// </summary>
		/// <param name="name">Name of the parameter</param>
		/// <param name="parameter">value of the parameter</param>
		public void AddParameter(string name, string parameter)
		{
			ParametersName.Add(name);
			ParametersItem.Add(parameter);
		}
		/// <summary>
		/// Add the parameter to the list of parameters of the SQL sentence
		/// </summary>
		/// <param name="name">Name of the parameter</param>
		/// <param name="parameter">Data type of the parameter</param>
		public void AddParameter(string name, IONType parameter)
		{
			ParametersName.Add(name);
			ParametersItem.Add(parameter);
		}
		/// <summary>
		/// Add the parameter to the list of parameters of the SQL sentence
		/// </summary>
		/// <param name="parameter">Parameter</param>
		public void AddParameter(ONSqlSelect parameter)
		{
			ParametersName.Add("");
			ParametersItem.Add(parameter);
		}
		#endregion

		#region GetMethods
		public virtual IONType GetParameter(string name)
		{
			for (int i = 0; i < ParametersName.Count; i++)
				if (string.Compare(name, ParametersName[i], true) == 0)
					return (ParametersItem[i] as IONType);

			return null;
		}
		#endregion

		#region Clear / GenerateMethods
		public virtual void Clear()
		{
			ParametersItem.Clear();
		}
		public virtual string GenerateSQL(out ArrayList sqlParameters)
		{
			sqlParameters = new ArrayList();
			return "";
		}
		public static string Replace(string sql, int numParameter)
		{
			StringBuilder lTextBuilder = new StringBuilder();
			string lSql = sql;

			// Find ? number 'numParameter'
			int lIndex = lSql.IndexOf("?", 0);
			
			//Replacement of ?
			if (lIndex != -1)
			{
				lSql = lSql.Remove(lIndex, 1);
				lSql = lSql.Insert(lIndex, "@" + numParameter);
				lTextBuilder.Append(lSql);
			}
			return lTextBuilder.ToString();
		}
		#endregion
	}
}

