// 3.4.4.5

using System;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// Attribute asssociated to the XML component refered to the filters.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class ONNavigationalFilterXMLAttribute : Attribute, IONAttribute
	{
		#region Members
		public string NavFilter;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="filter">Name of the filter that represents the attribute</param>
		public ONNavigationalFilterXMLAttribute(string navFilter)
		{
			NavFilter = navFilter;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return "<Filter>" + NavFilter + "</Filter>";
		}
		#endregion

		#region Operators
		public override bool Equals(object obj)
		{
			ONNavigationalFilterXMLAttribute lObject = obj as ONNavigationalFilterXMLAttribute;

			if (lObject == null)
				return false;

			return (lObject.NavFilter == NavFilter);
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public static bool operator ==(ONNavigationalFilterXMLAttribute obj1, ONNavigationalFilterXMLAttribute obj2)
		{
			if ((object)obj1 == null && (object)obj2 == null)
				return true;

			if ((object)obj1 == null || (object)obj2 == null)
				return false;

			return (obj1.Equals(obj2));
		}
		public static bool operator ==(ONNavigationalFilterXMLAttribute obj1, object obj2)
		{
			return (obj1 == ((ONNavigationalFilterXMLAttribute)obj2));
		}
		public static bool operator !=(ONNavigationalFilterXMLAttribute obj1, ONNavigationalFilterXMLAttribute obj2)
		{
			return !(obj1 == obj2);
		}
		public static bool operator !=(ONNavigationalFilterXMLAttribute obj1, object obj2)
		{
			return !(obj1 == obj2);
		}
		#endregion
	}
}
