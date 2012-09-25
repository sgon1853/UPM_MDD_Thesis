// 3.4.4.5

using System;
using SIGEM.Business.OID;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// ONFacetAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	internal class ONFacetAttribute: Attribute, IONAttribute
	{
		#region Attribute
		public string ClassName;
		#endregion

		#region Properties
		#endregion

		#region Constructors
		public ONFacetAttribute(string className)
		{
			ClassName = className;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return "<Facet>" + ClassName + "</Facet>";
		}
		#endregion
	}
}

