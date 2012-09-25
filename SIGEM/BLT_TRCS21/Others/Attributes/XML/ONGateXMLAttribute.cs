// 3.4.4.5

using System;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// ONGateXMLAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ONGateXMLAttribute: System.Attribute
	{
		#region Members
		public string ClassName;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="className">Name of the class</param>
		public ONGateXMLAttribute(string className)
		{
			ClassName = className;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return "<Class>" + ClassName + "</Class>";
		}
		#endregion

		#region Operators
		public override bool Equals(object obj)
		{
			ONGateXMLAttribute lObject = obj as ONGateXMLAttribute;

			if (lObject == null)
				return false;

			return (lObject.ClassName.Equals(ClassName));
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public static bool operator==(ONGateXMLAttribute obj1, ONGateXMLAttribute obj2)
		{
			if ((object) obj1 == null && (object) obj2 == null) 
				return true;

			if ((object) obj1 == null || (object) obj2 == null) 
				return false;

			return (obj1.Equals(obj2));
		}
		public static bool operator==(ONGateXMLAttribute obj1, object obj2)
		{
			return (obj1 == ((ONGateXMLAttribute) obj2));
		}
		public static bool operator!=(ONGateXMLAttribute obj1, ONGateXMLAttribute obj2)
		{
			return !(obj1 == obj2);
		}
		public static bool operator!=(ONGateXMLAttribute obj1, object obj2)
		{
			return !(obj1 == obj2);
		}
		#endregion
	}
}

