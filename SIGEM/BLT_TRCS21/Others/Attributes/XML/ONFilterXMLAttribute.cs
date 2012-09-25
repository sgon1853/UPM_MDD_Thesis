// 3.4.4.5

using System;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// Attribute asssociated to the XML component refered to the filters.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class ONFilterXMLAttribute: Attribute, IONAttribute
	{
		#region Members
		public string Filter;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="filter">Name of the filter that represents the attribute</param>
		public ONFilterXMLAttribute(string filter)
		{
			Filter = filter;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return "<Filter>" + Filter + "</Filter>";
		}
		#endregion

		#region Operators
		public override bool Equals(object obj)
		{
			ONFilterXMLAttribute lObject = obj as ONFilterXMLAttribute;

			if (lObject == null)
				return false;

			return (lObject.Filter == Filter);
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public static bool operator==(ONFilterXMLAttribute obj1, ONFilterXMLAttribute obj2)
		{
			if ((object) obj1 == null && (object) obj2 == null) 
				return true;

			if ((object) obj1 == null || (object) obj2 == null) 
				return false;

			return (obj1.Equals(obj2));
		}
		public static bool operator==(ONFilterXMLAttribute obj1, object obj2)
		{
			return (obj1 == ((ONFilterXMLAttribute) obj2));
		}
		public static bool operator!=(ONFilterXMLAttribute obj1, ONFilterXMLAttribute obj2)
		{
			return !(obj1 == obj2);
		}
		public static bool operator!=(ONFilterXMLAttribute obj1, object obj2)
		{
			return !(obj1 == obj2);
		}
		#endregion
	}
}

