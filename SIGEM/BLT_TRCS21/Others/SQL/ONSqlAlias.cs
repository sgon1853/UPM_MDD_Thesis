// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using SIGEM.Business.Others.Enumerations;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// Represents an alias in the Sql sentence
	/// </summary>
	public class ONSqlAlias
	{
		#region Attributes
		// Alias
		public string Alias;
		// Alias table name
		public string Table;
		// Alias Path
		public ONPath OnPath;
		// Facet
		public string Facet;
		// Type of Join
		public JoinType JoiningType;

		#region left / right join
		// Alias where (when left or rigth join)
		public List<string> Wheres = new List<string>();

		// Tree Alias
		public ONSqlAlias FatherAlias;
		public List<ONSqlAlias> ChildAlias = new List<ONSqlAlias>();
		#endregion
		#endregion

		#region Contructors
		/// <summary>
		/// Default constuctor
		/// </summary>
		/// <param name="alias">Alias name</param>
		/// <param name="table">Table anem</param>
		/// <param name="onPath">Path to solve</param>
		public ONSqlAlias(string alias, string table, ONPath onPath, string facet)
			:this(JoinType.InnerJoin, null, alias, table, onPath, facet)
		{
		}
		/// <summary>
		/// Constructor for LeftJoin sentences
		/// </summary>
		/// <param name="joinType">Type of join</param>
		/// <param name="fatherAlias">Father alias</param>
		/// <param name="alias">Alias name</param>
		/// <param name="table">Table name</param>
		/// <param name="onPath">Sql to solve</param>
		public ONSqlAlias(JoinType joinType, ONSqlAlias fatherAlias, string alias, string table, ONPath onPath, string facet)
		{
			JoiningType = joinType;
			Alias = alias;
			Table = table;
			OnPath = onPath;
			Facet = facet;
			FatherAlias = fatherAlias;

			if (FatherAlias != null)
				FatherAlias.ChildAlias.Add(this);
		}
		#endregion

		#region Operators
		public override bool Equals(object onSqlAlias)
		{
			ONSqlAlias lOnSqlAlias = onSqlAlias as ONSqlAlias;
			string lAlias = onSqlAlias as string;

			if (lOnSqlAlias != null)
				return (string.Compare(Alias, lOnSqlAlias.Alias, true) == 0);

			if (lAlias != null)
				return (string.Compare(Alias, lAlias, true) == 0);

			return false;
		}
		public override int GetHashCode()
		{
			return (Alias.ToUpper().GetHashCode());
		}
		#endregion
	}
}

