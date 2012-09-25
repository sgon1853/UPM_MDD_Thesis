// 3.4.4.5

using System;

namespace SIGEM.Business.Query
{
	/// <summary>
	/// ONrderCriteriaItem.
	/// </summary>
	public class ONOrderCriteriaItem
	{
		#region Members
		public ONPath OnPath;
		public string Facet;
		public string Attribute;
		public OrderByTypeEnumerator Type;
		public string DomainArgument = "";
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="onPath">Role path in case that the attribute is from another class</param>
		/// <param name="facet">Name of the class that has the attribute to be fixed in SQL sentence</param>
		/// <param name="attribute">Name of the attribute that is needed to the sort</param>
		/// <param name="type">Direction of the order. Could be ascending o descending</param>
		public ONOrderCriteriaItem(ONPath onPath, string facet, string attribute, OrderByTypeEnumerator type)
		{
			OnPath = onPath;
			Facet = facet;
			Attribute = attribute;
			Type = type;
		}

		public ONOrderCriteriaItem(ONPath onPath, string facet, string attribute, OrderByTypeEnumerator type, string domainArgument)
		{
			OnPath = onPath;
			Facet = facet;
			Attribute = attribute;
			Type = type;
			DomainArgument = domainArgument;
		}
		#endregion
	}
}

