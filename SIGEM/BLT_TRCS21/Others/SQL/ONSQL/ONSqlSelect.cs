// 3.4.4.5

using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using SIGEM.Business.Types;
using SIGEM.Business.Others.Enumerations;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// Represents a Select Sql sentence
	/// </summary>
	public class ONSqlSelect : ONSql
	{
		#region Members

		#region Embedded sentences
		// Variable to put in
		public string Variable;

		// Subquery managenet
		public ONSqlSelect SuperQuery;
		#endregion

		#region Select sentence
		// Array to put the fields to return
		protected List<string> mSelectAttributes = new List<string>();
		// Select parameters
		protected List<string> mSelectParametersName = new List<string>();
		protected ArrayList mSelectParametersItem = new ArrayList();
		/// <summary>
		/// If the select is distinct or not
		/// </summary>
		protected bool mIsDistinct = false;
		#endregion
		
		#region From sentence
		// Hash table to map paths with From fields
		protected Dictionary<ONSqlPath, ONSqlAlias> mFrom = new Dictionary<ONSqlPath, ONSqlAlias>();

		// Hash table to map paths with Alias
		protected Dictionary<string, ONSqlAlias> mFromAlias = new Dictionary<string, ONSqlAlias>();
		#endregion

		#region Where sentence
		// Collection of Where conjunctions
		protected List<string> mWhereConjuntion = new List<string>();
		// Collection of Where disjunction
		protected List<string> mWhereDisjunction = new List<string>();
		// Where parameters
		protected List<string> mWhereParametersName = new List<string>();
		protected ArrayList mWhereParametersItem = new ArrayList();
		// Where parameters (for Disjunctions)
		protected List<string> mWhereDisjParametersName = new List<string>();
		protected ArrayList mWhereDisjParametersItem = new ArrayList();
		// Indicates when the where part is a Disjunction
		protected bool mInDisjunction = false;
		protected StringBuilder mCurrentDisjunction;
		#endregion

		#region OrderBy sentence
		// Collection of OrderBy Fields
		protected List<string> mOrderBy = new List<string>();

		// Collection of OrderBy disjunctions
		protected List<string> mOrderByDisjuntion = new List<string>();

		// Collection of OrderBy Parameters
		public ArrayList OrderByParameters = new ArrayList();
		#endregion
		#endregion

		#region Properties
		public override ArrayList ParametersItem
		{
			get
			{
				ArrayList lParameters = new ArrayList();

				lParameters.AddRange(mSelectParametersItem);
				lParameters.AddRange(mWhereParametersItem);
				lParameters.AddRange(mWhereDisjParametersItem);

				return lParameters;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public ONSqlSelect()
			:this(null, null)
		{
		}
		/// <summary>
		/// Constructor for embedded Sql sentences in condition
		/// </summary>
		/// <param name="onSqlSelect">Sql object to embed</param>
		public ONSqlSelect(ONSqlSelect onSqlSelect)
			:this(onSqlSelect, null)
		{
		}
		/// <summary>
		/// Constructor for embedded Sql sentences in select
		/// </summary>
		/// <param name="onSqlSelect">Sql object to embed</param>
		/// <param name="variable">Virtual field to put in</param>
		public ONSqlSelect(ONSqlSelect onSqlSelect, string variable)
		{
			Variable = variable;
			SuperQuery = onSqlSelect;
		}
		#endregion

		#region AddMethods
		/// <summary>
		/// Add select field
		/// </summary>
		/// <param name="select">Field name</param>
		public virtual void AddSelect(string select)
		{
			mSelectAttributes.Add(select);
		}

		/// <summary>
		/// Add a conjunction in the where disjunctions
		/// </summary>
		/// <param name="conjuncion">Conjunction string</param>
		public void AddWhereDisjunction(string conjunction)
		{
			if (mCurrentDisjunction.Length > 0)
				mCurrentDisjunction.Append(" AND " + conjunction);
			else
				mCurrentDisjunction.Append(conjunction);
		}

		/// <summary>
		/// Add where conjunction
		/// </summary>
		/// <param name="conjuncion">Conjunction string</param>
		public void AddWhere(string conjuncion)
		{
			if (mInDisjunction)
				AddWhereDisjunction(conjuncion);
			else
				mWhereConjuntion.Add(conjuncion);
		}
		
		/// <summary>
		/// Every WHERE part will be added as a disjunction
		/// </summary>
		public void StartDisjunction()
		{
			mInDisjunction = true;
			mCurrentDisjunction = new StringBuilder();
		}
		
		/// <summary>
		/// Every WHERE part will be added as a conjunction
		/// </summary>
		public void FinishDisjunction()
		{
			mInDisjunction = false;
			if (mCurrentDisjunction.Length > 0)
			{
				mWhereDisjunction.Add(mCurrentDisjunction.ToString());
				mCurrentDisjunction = new StringBuilder();
			}
		}				

		/// <summary>
		/// Add OrderBy fields
		/// </summary>
		/// <param name="alias">Table alias name</param>
		/// <param name="field">Field name</param>
		/// <param name="orderByType">Asc/Des order type</param>
		/// <param name="val">Bound value</param>
		public void AddOrderBy(string alias, string field, OrderByTypeEnumerator orderByType, ONSimpleType val)
		{
			// Compose alias
			StringBuilder lFieldBuilder = new StringBuilder();
			if (alias != "")
			{
				lFieldBuilder.Append(alias);
				lFieldBuilder.Append(".");
				lFieldBuilder.Append(field);
			}
			else
				lFieldBuilder.Append(field);

			// Add OrderBy
			if (orderByType == OrderByTypeEnumerator.Des)
				mOrderBy.Add(lFieldBuilder.ToString() + " DESC");
			else
				mOrderBy.Add(lFieldBuilder.ToString());

			// Without StartRow
			if (val == null)
				return;

			// Extract previous OrderBy
			StringBuilder lDisjuntionBuilder = new StringBuilder();
			if (mOrderByDisjuntion.Count > 0) // Not first StartRow
			{
				lDisjuntionBuilder.Append(mOrderByDisjuntion[mOrderByDisjuntion.Count - 1]);
				lDisjuntionBuilder.Append(" AND ");
				mOrderByDisjuntion.RemoveAt(mOrderByDisjuntion.Count - 1);
			}

			// Add Field
			lDisjuntionBuilder.Append(lFieldBuilder);

			// Add StartRow
			if (orderByType == OrderByTypeEnumerator.Des)
			{
				mOrderByDisjuntion.Add(lDisjuntionBuilder.ToString() + " < ?");
				AddOrderByParameter(val);
			}
			else
			{
				mOrderByDisjuntion.Add(lDisjuntionBuilder.ToString() + " > ?");
				AddOrderByParameter(val);
			}

			mOrderByDisjuntion.Add(lDisjuntionBuilder.ToString() + " = ?");
		}
		/// <summary>
		/// Add OrderBy value parameter
		/// </summary>
		/// <param name="parameter">Value</param>
		protected void AddOrderByParameter(IONType parameter)
		{
			OrderByParameters.Add(parameter);
		}
		/// <summary>
		/// Add OrderBy subquery parameter
		/// </summary>
		/// <param name="parameter">Sql sentence</param>
		protected void AddOrderByParameter(ONSqlSelect parameter)
		{
			OrderByParameters.Add(parameter);
		}
		/// <summary>
		/// Add the parameter to the list of parameters of the SQL sentence
		/// </summary>
		/// <param name="name">Name of the parameter</param>
		/// <param name="parameter">Data type of the parameter</param>
		public string AddWhereParameter(string name, IONType parameterValue)
		{
			if (mInDisjunction)
			{
				mWhereDisjParametersName.Add(name);
				mWhereDisjParametersItem.Add(parameterValue);
			}
			else
			{
				mWhereParametersName.Add(name);
				mWhereParametersItem.Add(parameterValue);
			}

			if (parameterValue is ONString)
				return "RTRIM(?)";

			return "?";
		}
		public string AddWhereParameter(IONType parameterValue)
		{
			if (mInDisjunction)
			{
				mWhereDisjParametersName.Add("");
				mWhereDisjParametersItem.Add(parameterValue);
			}
			else
			{
				mWhereParametersName.Add("");
				mWhereParametersItem.Add(parameterValue);
			}

			if (parameterValue is ONString)
				return "RTRIM(?)";

			return "?";
		}
		/// <summary>
		/// Add the parameter to the list of parameters of the SQL sentence
		/// </summary>
		/// <param name="parameter">Parameter</param>
		public string AddWhereParameter(ONSqlSelect parameter)
		{
			if (mInDisjunction)
			{
				mWhereDisjParametersName.Add("");
				mWhereDisjParametersItem.Add(parameter);
			}
			else
			{
				mWhereParametersName.Add("");
				mWhereParametersItem.Add(parameter);
			}
			
			return "?";
		}
		/// <summary>
		/// Generates a Equal operator for querying the database.
		/// </summary>
		/// <param name="leftParameterName">Name of the left parameter</param>
		/// <param name="leftParameterValue">Value of the left parameter</param>
		/// <param name="rightParameterName">Name of the right parameter</param>
		/// <param name="rightParameterValue">Name of the right parameter</param>
		/// <returns></returns>
		public string AddEQComparison(string leftParameterName, IONType leftParameterValue, string rightParameterName, IONType rightParameterValue)
		{
			// If the value of the right parameter is null it returns the 'IS NULL' operand.
			if (rightParameterValue.Value == null)
				return AddWhereParameter(leftParameterName, leftParameterValue) + " IS NULL";
			else if (leftParameterValue.Value == null) //If the value of the left parameter is null it returns 'Right_Operand IS NULL' operand but
				return AddWhereParameter(rightParameterName, rightParameterValue) + " IS NULL";

			// It returns "? = ?" adding the parameters to the sentence
			return AddWhereParameter(leftParameterName, leftParameterValue) + " = " + AddWhereParameter(rightParameterName, rightParameterValue);
		}
		public string AddEQComparison(IONType leftParameterValue, IONType rightParameterValue)
		{
			return AddEQComparison("", leftParameterValue, "", rightParameterValue);
		}
		/// <summary>
		/// Generates a Equal operator with the right operand for querying the database.
		/// </summary>
		/// <param name="parameterName">Name of the parameter</param>
		/// <param name="parameterValue">Value of the parameter</param>
		/// <returns></returns>
		public string AddEQComparison(string parameterName, IONType parameterValue)
		{
			// If the value is null it returns the 'IS NULL' operand.
			if (parameterValue.Value == null)
				return " IS NULL";

			// It returns " = ?" adding the parameter to the sentence
			return " = " + AddWhereParameter(parameterName, parameterValue);
		}
		public string AddEQComparison(IONType parameterValue)
		{
			return AddEQComparison("", parameterValue);
		}
		/// <summary>
		/// Generates a Not Equal operator for querying the database.
		/// </summary>
		/// <param name="leftParameterName">Name of the left parameter</param>
		/// <param name="leftparameterValue">Value of the left parameter</param>
		/// <param name="rightParameterName">Name of the right parameter</param>
		/// <param name="rightParameterValue">Name of the right parameter</param>
		/// <returns></returns>
		public string AddNEQComparison(string leftParameterName, IONType leftParameterValue, string rightParameterName, IONType rightParameterValue)
		{
			// If the value of the right parameter is null it returns the 'IS NOT NULL' operand.
			if (rightParameterValue.Value == null)
				return AddWhereParameter(leftParameterName, leftParameterValue) + " IS NOT NULL";
			else if (leftParameterValue.Value == null) //If the value of the left parameter is null it returns 'Right_Operand IS NOT NULL' operand but
				return AddWhereParameter(rightParameterName, rightParameterValue) + " IS NOT NULL";

			// It returns "? <> ?" adding the parameters to the sentence
			return AddWhereParameter(leftParameterName, leftParameterValue) + " <> " + AddWhereParameter(rightParameterName, rightParameterValue);
		}
		public string AddNEQComparison(IONType leftParameterValue, IONType rightParameterValue)
		{
			return AddNEQComparison("", leftParameterValue, "", rightParameterValue);
		}
		/// <summary>
		/// Generates a Not Equal operator with the right operand for querying the database.
		/// </summary>
		/// <param name="parameterName">Name of the parameter</param>
		/// <param name="parameterValue">Value of the parameter</param>
		/// <returns></returns>
		public string AddNEQComparison(string parameterName, IONType parameterValue)
		{
			// If the value is null it returns the 'IS NOT NULL' operand.
			if (parameterValue.Value == null)
				return " IS NOT NULL";

			// It returns " <> ?" adding the parameter to the sentence
			return " <> " + AddWhereParameter(parameterName, parameterValue);
		}
		public string AddNEQComparison(IONType parameterValue)
		{
			return AddNEQComparison("", parameterValue);
		}
		/// <summary>
		/// Add the parameter to the list of parameters of the SQL sentence
		/// </summary>
		/// <param name="name">Name of the parameter</param>
		/// <param name="parameter">Data type of the parameter</param>
		public void AddSelectParameter(string name, IONType parameter)
		{
			mSelectParametersName.Add(name);
			mSelectParametersItem.Add(parameter);
		}
		/// <summary>
		/// Add the parameter to the list of parameters of the SQL sentence
		/// </summary>
		/// <param name="parameter">Parameter</param>
		public void AddSelectParameter(ONSqlSelect parameter)
		{
			mSelectParametersName.Add("");
			mSelectParametersItem.Add(parameter);
		}
		#endregion

		#region Path / Alias Management
		/// <summary>
		/// Generate a valid and unique alias
		/// </summary>
		/// <param name="alias">Source alias</param>
		/// <returns>Valid and Unique alias</returns>
		private string GenerateAlias(string alias)
		{
			StringBuilder lAliasBuilder = new StringBuilder();
			int lLastDot = alias.LastIndexOf('.');
			if (lLastDot < 0) lLastDot = 0;
			lAliasBuilder.Append(alias.Substring(lLastDot));

			int i = 1;

			// Go to root
			ONSqlSelect lRoot = this;
			while (lRoot.SuperQuery != null)
				lRoot = lRoot.SuperQuery;

			// Search alias
			while ((GetOnSqlAlias(lAliasBuilder.ToString()) != null) || (lRoot.GetOnSqlAliasInSql(lAliasBuilder.ToString()) != null) || (GetOnSqlAliasInSql(lAliasBuilder.ToString()) != null))
			{
				lAliasBuilder = new StringBuilder();

				lAliasBuilder.Append(alias);
				lAliasBuilder.Append("_");
				lAliasBuilder.Append(i.ToString());

				i++;
			}

			return lAliasBuilder.ToString();
		}

		/// <summary>
		/// Find or create an alias for this table
		/// </summary>
		/// <param name="table">Table</param>
		/// <param name="onPath">Path to solve</param>
		/// <param name="facet">Facet of the path to solve</param>
		/// <returns>Valid / Unique alias</returns>
 		public string CreateAlias(string table, ONPath onPath, string facet)
		{
			return CreateAlias(JoinType.InnerJoin, "", table, onPath, facet, false);
		}
		/// <summary>
		/// Find or create an alias for this table with left/right join
		/// </summary>
		/// <param name="fatherAlias">Father alias</param>
		/// <param name="table">Table</param>
		/// <param name="onPath">Path to solve</param>
		/// <param name="facet">Facet of the path to solve</param>
		/// <returns></returns>
		public string CreateAlias(string fatherAlias, string table, ONPath onPath, string facet)
		{
			return CreateAlias(JoinType.InnerJoin, fatherAlias, table, onPath, facet, false);
		}
		/// <summary>
		/// Find or create an alias for this table with left/right join
		/// </summary>
		/// <param name="table">Table</param>
		/// <param name="onPath">Path to solve</param>
		/// <param name="facet">Facet of the path to solve</param>
		/// <param name="force">Add the almost the last alias to the select</param>
		/// <returns></returns>
		public string CreateAlias(string table, ONPath onPath, string facet, bool force)
		{
			return CreateAlias(JoinType.InnerJoin, "", table, onPath, facet, force);
		}
		/// <summary>
		/// Find or create an alias for this table with left/right join
		/// </summary>
		/// <param name="fatherAlias">Father alias</param>
		/// <param name="table">Table</param>
		/// <param name="onPath">Path to solve</param>
		/// <param name="facet">Facet of the path to solve</param>
		/// <param name="force">Add the almost the last alias to the select</param>
		/// <returns></returns>
		public string CreateAlias(string fatherAlias, string table, ONPath onPath, string facet, bool force)
		{
			return CreateAlias(JoinType.InnerJoin, fatherAlias, table, onPath, facet, force);
		}
		/// <summary>
		/// Find or create an alias for this table with left/right join
		/// </summary>
		/// <param name="joinType">Type of join</param>
		/// <param name="fatherAlias">Father alias</param>
		/// <param name="table">Table</param>
		/// <param name="onPath">Path to solve</param>
		/// <param name="facet">Facet of the path to solve</param>
		/// <param name="force">Add the almost the last alias to the select</param>
		/// <returns></returns>
		public string CreateAlias(JoinType joinType, string fatherAlias, string table, ONPath onPath, string facet, bool force)
		{
			return CreateAlias(joinType, fatherAlias, table, onPath, facet, force, false);
		}
		/// <summary>
		/// Find or create an alias for this table with left/right join
		/// </summary>
		/// <param name="joinType">Type of join</param>
		/// <param name="fatherAlias">Father alias</param>
		/// <param name="table">Table</param>
		/// <param name="onPath">Path to solve</param>
		/// <param name="facet">Facet of the path to solve</param>
		/// <param name="force">Add the almost the last alias to the select</param>
		/// <param name="isLinkedTo">The alias belongs to a role in a linked To element</param>
		/// <returns></returns>
		public string CreateAlias(JoinType joinType, string fatherAlias, string table, ONPath onPath, string facet, bool force, bool isLinkedTo)
		{
			ONSqlPath lOnSqlPath = new ONSqlPath(onPath, facet, isLinkedTo);
			ONSqlAlias lOnSqlAlias = GetOnSqlAlias(lOnSqlPath);

			if ((lOnSqlAlias == null) || (force && !mFrom.ContainsKey(lOnSqlPath)))
			{
				// Search father alias
				ONSqlAlias lOnSqlAliasFather = null;
				if (fatherAlias != "")
				{
					lOnSqlAliasFather = GetOnSqlAlias(fatherAlias);
					lOnSqlAlias = new ONSqlAlias(joinType, lOnSqlAliasFather, GenerateAlias(table), table, onPath, facet);
				}
				else
					lOnSqlAlias = new ONSqlAlias(joinType, null, GenerateAlias(table), table, onPath, facet);

				// Path no exist
				mFrom.Add(lOnSqlPath, lOnSqlAlias);
				mFromAlias.Add(lOnSqlAlias.Alias, lOnSqlAlias);
			}
			
			// Change the left join to inner join if it is needed
			if ((joinType == JoinType.InnerJoin) && (onPath != null))
			{
				ONSqlAlias lTemp = lOnSqlAlias;
				do
				{
					if (mFrom.ContainsKey(lOnSqlPath))
					{
						lTemp.JoiningType = JoinType.InnerJoin;
						lTemp = lTemp.FatherAlias;
					
						if(lTemp != null)
							lOnSqlPath = new ONSqlPath(lTemp.OnPath, lTemp.Facet, isLinkedTo);
					}
					else
						lTemp = null;
				}
				while (lTemp != null);
			}

			return lOnSqlAlias.Alias;
		}
		/// <summary>
		/// Add where conjunctions to this alias
		/// </summary>
		/// <param name="alias">Alias string</param>
		/// <param name="wheres">Where conjuncions</param>
		public void AddAliasWhere(string alias, params string[] wheres)
		{
			ONSqlAlias lOnSqlAlias = GetOnSqlAlias(alias);
			lOnSqlAlias.Wheres.AddRange(wheres);
		}
		/// <summary>
		/// Search alias
		/// </summary>
		/// <param name="facet">Facet name</param>
		/// <param name="onPath">Path to solve</param>
		/// <returns>Alias string</returns>
		public string GetAlias(string facet, ONPath onPath)
		{
			return GetAlias(facet, onPath, false);
		}
				/// <summary>
		/// Search alias
		/// </summary>
		/// <param name="facet">Facet name</param>
		/// <param name="onPath">Path to solve</param>
		/// <param name="isLinkedTo">The alias belongs to a role in a linked To element</param>
		/// <returns>Alias string</returns>
		public string GetAlias(string facet, ONPath onPath, bool isLinkedTo)
		{
			ONSqlPath lOnSqlPath = new ONSqlPath(onPath, facet, isLinkedTo);
			ONSqlAlias lOnSqlAlias = GetOnSqlAlias(lOnSqlPath);

			// Path no exist
			if (lOnSqlAlias == null)
				return "";

			return lOnSqlAlias.Alias;
		}
		/// <summary>
		/// Search Sql alias
		/// </summary>
		/// <param name="alias">Alias string</param>
		/// <returns>Sql Alias</returns>
		public ONSqlAlias GetOnSqlAlias(string alias)
		{
			ONSqlAlias lOnSqlAlias = null;
			if (mFromAlias.ContainsKey(alias))
				lOnSqlAlias = mFromAlias[alias];

			if ((lOnSqlAlias == null) && (SuperQuery != null))
				return (SuperQuery.GetOnSqlAlias(alias));

			return lOnSqlAlias;
		}
		/// <summary>
		/// Search Sql alias
		/// </summary>
		/// <param name="lOnSqlPath">Sql paht</param>
		/// <returns></returns>
		public ONSqlAlias GetOnSqlAlias(ONSqlPath lOnSqlPath)
		{
			ONSqlAlias lOnSqlAlias = null;
			foreach(KeyValuePair<ONSqlPath, ONSqlAlias>lFrom in mFrom)
			{
				if ((lFrom.Key.OnPath == lOnSqlPath.OnPath) && (lFrom.Key.Facet == lOnSqlPath.Facet) && ((lFrom.Key.IslinkedTo == lOnSqlPath.IslinkedTo) || (lOnSqlPath.IslinkedTo)))
					return lFrom.Value;
			}

			if ((lOnSqlAlias == null) && (SuperQuery != null))
				return (SuperQuery.GetOnSqlAlias(lOnSqlPath));

			return lOnSqlAlias;
		}
		/// <summary>
		/// Search Sql alias including subqueries
		/// </summary>
		/// <param name="alias">Alias string</param>
		/// <returns>Sql Alias</returns>
		public ONSqlAlias GetOnSqlAliasInSql(string alias)
		{
			// owner from
			ONSqlAlias lOnSqlAlias = null;
			if (mFromAlias.ContainsKey(alias))
				return mFromAlias[alias];

			// Search in subqueries
			foreach (object parameter in ParametersItem)
			{
				ONSqlSelect onSubSelect = parameter as ONSqlSelect;
				if (onSubSelect != null)
					lOnSqlAlias = onSubSelect.GetOnSqlAliasInSql(alias);

				if (lOnSqlAlias != null)
					return lOnSqlAlias;
			}

			return lOnSqlAlias;
		}		
		/// <summary>
		/// Search table of the path and facet
		/// </summary>
		/// <param name="facet">Facet name</param>
		/// <param name="onPath">Path to solve</param>
		/// <returns>Table name</returns>
		public string GetTable(string facet, ONPath onPath)
		{
			ONSqlPath lOnSqlPath = new ONSqlPath(onPath, facet);
			ONSqlAlias lOnSqlAlias = GetOnSqlAlias(lOnSqlPath);

			// Path no exist
			if (lOnSqlAlias == null)
				return "";

			return lOnSqlAlias.Table;
		}
		#endregion

		#region GetMethods
		public IONType GetWhereParameter(string name)
		{
			for (int i = 0; i < mWhereParametersName.Count; i++)
				if (string.Compare(name, mWhereParametersName[i], true) == 0)
					return (mWhereParametersItem[i] as IONType);

			return null;
		}
		public IONType GetWhereDisjParameter(string name)
		{
			for (int i = 0; i < mWhereDisjParametersName.Count; i++)
				if (string.Compare(name, mWhereDisjParametersName[i], true) == 0)
					return (mWhereDisjParametersItem[i] as IONType);

			return null;
		}
		public IONType GetSelectParameter(string name)
		{
			for (int i = 0; i < mSelectParametersName.Count; i++)
				if (string.Compare(name, mSelectParametersName[i], true) == 0)
					return (mSelectParametersItem[i] as IONType);

			return null;
		}
		public override IONType GetParameter(string name)
		{
			IONType lParameter = null;
			
			lParameter = GetSelectParameter(name);
			if (lParameter != null)
				return lParameter;

			lParameter = GetWhereParameter(name);
			if (lParameter != null)
				return lParameter;

			lParameter = GetWhereDisjParameter(name);
			if (lParameter != null)
				return lParameter;

			return null;
		}
		#endregion

		#region Clear / Generate Methods
		/// <summary>
		/// Clear members
		/// </summary>
		public override void Clear()
		{
			mSelectAttributes.Clear();
			mFrom.Clear();
			mFromAlias.Clear();
			mWhereConjuntion.Clear();
			mWhereDisjunction.Clear();
			mOrderBy.Clear();
			mOrderByDisjuntion.Clear();
			mWhereParametersItem.Clear();
			mWhereDisjParametersItem.Clear();
			mSelectParametersItem.Clear();
			mWhereParametersName.Clear();
			mWhereDisjParametersName.Clear();
			mSelectParametersName.Clear();

			base.Clear();
		}
		/// <summary>
		/// Generate Select Sql
		/// </summary>
		/// <returns>Sql string</returns>
		public override string GenerateSQL(out ArrayList sqlParameters)
		{
			StringBuilder lTextBuilder = new StringBuilder();
			sqlParameters = new ArrayList();

			#region Select
			StringBuilder lTextSelect = new StringBuilder();
			foreach (string lSelectAttribute in mSelectAttributes)
			{
				if (lTextSelect.Length > 0)
					lTextSelect.Append(", ");

				lTextSelect.Append(lSelectAttribute);
			}
			sqlParameters.AddRange(mSelectParametersItem);

			lTextBuilder.Append("SELECT ");
			if (mIsDistinct)
				lTextBuilder.Append("DISTINCT ");
			lTextBuilder.Append(lTextSelect);
			#endregion

			StringBuilder lTextWhereJoin = new StringBuilder();

			#region From
			bool lGenerateFrom = false;
			if (mFrom.Count > 0)
			{
				foreach (ONSqlAlias lOnSqlAlias in mFrom.Values)
				{
					if ((lOnSqlAlias.FatherAlias == null) || ((SuperQuery != null) && (SuperQuery.GetAlias(lOnSqlAlias.FatherAlias.Table, lOnSqlAlias.FatherAlias.OnPath) != "")))
					{
						lGenerateFrom = true;
						break;
					}
				}
			}
			if (lGenerateFrom)
			{
				StringBuilder lTextFrom = new StringBuilder();
				foreach (ONSqlAlias lOnSqlAlias in mFrom.Values)
				{
					if ((lOnSqlAlias.FatherAlias == null) || ((SuperQuery != null) && (SuperQuery.GetOnSqlAlias(lOnSqlAlias.FatherAlias.Alias) != null)))
					{
						foreach (string lConjunction in lOnSqlAlias.Wheres)
							mWhereConjuntion.Add(lConjunction);

						if (lTextFrom.Length > 0)
							lTextFrom.Append(", ");

						lTextFrom.Append(lOnSqlAlias.Table);
						lTextFrom.Append(" ");
						lTextFrom.Append(lOnSqlAlias.Alias);

						GenerateSQL_Join(lOnSqlAlias, lTextFrom, lTextWhereJoin);
					}
				}
				lTextBuilder.Append(" FROM ");
				lTextBuilder.Append(lTextFrom);
			}
			#endregion

			#region Where
			if ((mOrderByDisjuntion.Count > 0) || (mWhereConjuntion.Count > 0) || (mWhereDisjunction.Count > 0)  || (lTextWhereJoin.Length > 0))
			{
				StringBuilder lTextWhere = new StringBuilder();

				#region Left Join
				if (lTextWhereJoin.Length > 0)
				{
					if (lTextWhere.Length > 0)
						lTextWhere.Append(" AND ");

					if ((lTextWhereJoin.Length > 1) || (mOrderByDisjuntion.Count > 0) || (mWhereConjuntion.Count > 0))
						lTextWhere.Append("(");

					lTextWhere.Append(lTextWhereJoin);

					if ((lTextWhereJoin.Length > 1) || (mOrderByDisjuntion.Count > 0) || (mWhereConjuntion.Count > 0))
						lTextWhere.Append(")");
				}
				#endregion

				#region Order by (where)
				if (mOrderByDisjuntion.Count > 0)
				{
					if (lTextWhere.Length > 0)
						lTextWhere.Append(" AND ");

					if ((lTextWhereJoin.Length > 0) || (mWhereConjuntion.Count > 0) || (mWhereDisjunction.Count > 0))
						lTextWhere.Append("(");

					StringBuilder lTextWhereTemp = new StringBuilder();
					for (int i = 0; i < mOrderByDisjuntion.Count; i++)
					{
						if (lTextWhereTemp.Length != 0)
							lTextWhereTemp.Append(" OR ");

						if (mOrderByDisjuntion.Count > 1)
							lTextWhereTemp.Append("(");

						lTextWhereTemp.Append(mOrderByDisjuntion[i]);

						if (mOrderByDisjuntion.Count > 1)
							lTextWhereTemp.Append(")");
					}
					lTextWhere.Append(lTextWhereTemp);

					if ((lTextWhereJoin.Length > 0) || (mWhereConjuntion.Count > 0) || (mWhereDisjunction.Count > 0))
						lTextWhere.Append(")");
				}
				for (int i = 0; i < OrderByParameters.Count; i++)
					for (int j = 0; j <= i; j++)
						sqlParameters.Add(OrderByParameters[j]);
				sqlParameters.AddRange(OrderByParameters);
				#endregion

				#region Where
				if (mWhereConjuntion.Count > 0)
				{
					foreach (string lItem in mWhereConjuntion)
					{
						if (lTextWhere.Length > 0)
							lTextWhere.Append(" AND ");

						if ((lTextWhereJoin.Length > 0) || (mOrderByDisjuntion.Count > 0) || (mWhereConjuntion.Count > 1))
							lTextWhere.Append("(");

						lTextWhere.Append(lItem);

						if ((lTextWhereJoin.Length > 0) || (mOrderByDisjuntion.Count > 0) || (mWhereConjuntion.Count > 1))
							lTextWhere.Append(")");
					}
				}
				sqlParameters.AddRange(mWhereParametersItem);
				#endregion

				#region Where (Disjunctions)
				if (mWhereDisjunction.Count > 0)
				{
					StringBuilder lTextWhereDisjunctions = new StringBuilder();
					foreach (string lItem in mWhereDisjunction)
					{
						if (lTextWhereDisjunctions.Length > 0)
							lTextWhereDisjunctions.Append(" OR ");

						if (mWhereDisjunction.Count > 1)
							lTextWhereDisjunctions.Append("(");

						lTextWhereDisjunctions.Append(lItem);

						if (mWhereDisjunction.Count > 1)
							lTextWhereDisjunctions.Append(")");
					}

					// Add the disjunctions into the sentence as a conjunction
					if (lTextWhere.Length > 0)
						lTextWhere.Append(" AND (").Append(lTextWhereDisjunctions).Append(")");
					else
						lTextWhere.Append(lTextWhereDisjunctions);
				}
				sqlParameters.AddRange(mWhereDisjParametersItem);
				#endregion
				
				lTextBuilder.Append(" WHERE ");
				lTextBuilder.Append(lTextWhere);
			}
			#endregion

			#region Order by
			if (mOrderBy.Count > 0)
			{
				StringBuilder lTextOrderBy = new StringBuilder();

				foreach (string lItem in mOrderBy)
				{
					if (lTextOrderBy.Length > 0)
						lTextOrderBy.Append(", ");

					lTextOrderBy.Append(lItem);
				}

				lTextBuilder.Append(" ORDER BY ");
				lTextBuilder.Append(lTextOrderBy);
			}
			#endregion

			// Solve Subqueries
			lTextBuilder = GenerateSQL_SubQueries(lTextBuilder, ref sqlParameters);


			return lTextBuilder.ToString();
		}
		/// <summary>
		/// Generate Join of the Sql sentence
		/// </summary>
		/// <param name="onSqlAlias">Sql node in the LeftJoin Tree</param>
		/// <returns></returns>
		private void GenerateSQL_Join(ONSqlAlias onSqlAlias, StringBuilder from, StringBuilder where)
		{

			foreach (ONSqlAlias lOnSqlAliasChild in onSqlAlias.ChildAlias)
			{
				if (GetOnSqlAlias(lOnSqlAliasChild.Alias) != null)
				{
					if (lOnSqlAliasChild.JoiningType == JoinType.InnerJoin)
						from.Append(" INNER JOIN ");
					else
						from.Append(" LEFT OUTER JOIN ");

					from.Append(lOnSqlAliasChild.Table);
					from.Append(" ");
					from.Append(lOnSqlAliasChild.Alias);
					from.Append(" ON ");
	
					StringBuilder lWheres = new StringBuilder();
					foreach (string lWhere in lOnSqlAliasChild.Wheres)
					{
						if (lWheres.Length != 0)
							lWheres.Append(" AND ");
	
						lWheres.Append("(");
						lWheres.Append(lWhere);
						lWheres.Append(")");
					}
	
					from.Append(lWheres);
	
					GenerateSQL_Join(lOnSqlAliasChild, from, where);
				}
			}
		}
		/// <summary>
		/// Generate subqueries of the Sql sentence
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		private StringBuilder GenerateSQL_SubQueries(StringBuilder sql, ref ArrayList parameters)
		{
			for (int i = 0; i < parameters.Count; )
			{
				ONSqlSelect lOnSqlSubquery = parameters[i] as ONSqlSelect;

				if (lOnSqlSubquery != null)
					// Insert sql text
					sql = GenerateSQL_SubQueriesReplace(sql, i, lOnSqlSubquery, ref parameters);
				else
					i++;
			}

			return sql;
		}
		/// <summary>
		/// Replace Sql parameters with subqueries
		/// </summary>
		/// <param name="sql">Source sql sentence</param>
		/// <param name="numParameter">Parameter number</param>
		/// <param name="onSqlSubquery">Sql to insert</param>
		/// <returns></returns>
		private StringBuilder GenerateSQL_SubQueriesReplace(StringBuilder sql, int numParameter, ONSqlSelect onSqlSubquery, ref ArrayList parameters)
		{
			StringBuilder lTextBuilder = new StringBuilder();
			string lSql = sql.ToString();

			// Find ? number 'numParameter'
			int lIndex = 0;
			for (int i = 0; i <= numParameter; i++)
				lIndex = lSql.IndexOf("?", lIndex + 1);

			ArrayList lSubParameters;
			string lSubSql = onSqlSubquery.GenerateSQL(out lSubParameters);

			// Insert subquery parameters
			parameters.RemoveAt(numParameter);
			int j = numParameter;
			for (int i = 0; i < lSubParameters.Count; i++)
				parameters.Insert(j++, lSubParameters[i]);

			// Mix sql's
			lTextBuilder.Append(lSql.Substring(0, lIndex));
			lTextBuilder.Append("(");
			lTextBuilder.Append(lSubSql);
			lTextBuilder.Append(")");
			lTextBuilder.Append(lSql.Substring(lIndex + 1, sql.Length - lIndex - 1));

			return lTextBuilder;
		}
		/// <summary>
		/// Generates a sql as a Count
		/// </summary>
		/// <param name="onSql">Sql to generate</param>
		/// <param name="sqlParameters">Sql parameters</param>
		public static string GenerateSQLAsCount(ONSqlSelect onSql, out ArrayList sqlParameters)
		{
			StringBuilder lSql = new StringBuilder();
			List<string> mTempOrderBy = new List<string>();
			List<string> mTempSelectAttributes = onSql.mSelectAttributes;

			//The sql is generated with a single select attribute
			onSql.mSelectAttributes = new List<string>(1);
			onSql.mSelectAttributes.Add(mTempSelectAttributes[0]);
		
			lSql.Append("SELECT COUNT(*) FROM (");
			if (onSql.mOrderBy.Count > 0)
			{
				// Generate the sentence without the OrderBy part
				mTempOrderBy = onSql.mOrderBy;
				onSql.mOrderBy = new List<string>();
				lSql.Append(onSql.GenerateSQL(out sqlParameters));
				onSql.mOrderBy = mTempOrderBy;
			}
			else
				lSql.Append(onSql.GenerateSQL(out sqlParameters));
			lSql.Append(") lAux");

			// Restore the original select attribute list
			onSql.mSelectAttributes = mTempSelectAttributes;
		
			return lSql.ToString();
		}
		#endregion
	}
}

