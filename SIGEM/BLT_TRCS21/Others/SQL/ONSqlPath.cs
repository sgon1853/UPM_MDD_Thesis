// 3.4.4.5

using System;

namespace SIGEM.Business.SQL
{
	/// <summary>
	/// Represents the pair Path - Facet that defines the a table
	/// </summary>
	public class ONSqlPath
	{
		#region Attributes
		// Path that represents
		public ONPath OnPath;
		// Target Facet of the Path that represents
		public string Facet;
		// The path is used in a linked To element
		public bool IslinkedTo = false;
		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="onPath">Path to solve</param>
		/// <param name="facet">Facet of the path</param>
		public ONSqlPath(ONPath onPath, string facet)
		{
			OnPath = onPath;
			Facet = facet;
		}
		
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="onPath">Path to solve</param>
		/// <param name="facet">Facet of the path</param>
		/// <param name="islinkedTo">The path is used in a linked to element</param>
		public ONSqlPath(ONPath onPath, string facet, bool islinkedTo)
		{
			OnPath = onPath;
			Facet = facet;
			IslinkedTo = islinkedTo;
		}
		#endregion

		#region Operators
		public override bool Equals(object onSqlPath)
		{
			ONSqlPath lOnSqlPath = onSqlPath as ONSqlPath;

			// Not exists path
			if (lOnSqlPath == null)
				return false;

			// Solve void or null paths
			if ((OnPath == null || OnPath.Path == "") && (lOnSqlPath.OnPath == null || lOnSqlPath.OnPath.Path == ""))
				return (string.Compare(Facet, lOnSqlPath.Facet, true) == 0);

			return ((OnPath == lOnSqlPath.OnPath) && (string.Compare(Facet, lOnSqlPath.Facet, true) == 0) && (IslinkedTo = lOnSqlPath.IslinkedTo));
		}
		public override int GetHashCode()
		{
			int lHash = 0;

			if (OnPath != null)
				lHash += OnPath.GetHashCode();

			if (Facet != null)
				lHash += Facet.ToUpper().GetHashCode();

			return (lHash);
		}
		#endregion
	}
}

