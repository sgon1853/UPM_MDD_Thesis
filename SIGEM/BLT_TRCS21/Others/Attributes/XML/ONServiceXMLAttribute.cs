// 3.4.4.5

using System;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// ONServiceXMLAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class ONServiceXMLAttribute : Attribute, IONAttribute
	{
		#region Members
		public string Service;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="service">Name of the service</param>
		public ONServiceXMLAttribute(string service)
		{
			Service = service;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return "<Service>" + Service + "</Service>";
		}
		#endregion

		#region Operators
		public override bool Equals(object obj)
		{
			ONServiceXMLAttribute lObject = obj as ONServiceXMLAttribute;

			if (lObject == null)
				return false;

			return (lObject.Service.Equals(Service));
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public static bool operator==(ONServiceXMLAttribute obj1, ONServiceXMLAttribute obj2)
		{
			if ((object) obj1 == null && (object) obj2 == null) 
				return true;

			if ((object) obj1 == null || (object) obj2 == null) 
				return false;

			return (obj1.Equals(obj2));
		}
		public static bool operator==(ONServiceXMLAttribute obj1, object obj2)
		{
			return (obj1 == ((ONServiceXMLAttribute) obj2));
		}
		public static bool operator!=(ONServiceXMLAttribute obj1, ONServiceXMLAttribute obj2)
		{
			return !(obj1 == obj2);
		}
		public static bool operator!=(ONServiceXMLAttribute obj1, object obj2)
		{
			return !(obj1 == obj2);
		}
		#endregion
	}
}

