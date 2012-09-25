// 3.4.4.5

using System;
using System.Xml;
using System.Collections;
using System.Data;
using System.Globalization;

namespace SIGEM.Business.Types
{
	/// <summary>
	/// Base class for the ON types
	/// </summary>
	public abstract class ONSimpleType : IONType, IComparable
	{
		#region Members
		// Represents the value of the object
		private object mValue;
		// Format for the basic type ONBool
		public static CultureInfo BoolFormat = new CultureInfo("en-US");
		// Format for the basic type ONReal
		public static CultureInfo RealFormat = new CultureInfo("en-US");
		// Format for the basic type ONInt
		public static CultureInfo IntFormat = new CultureInfo("en-US");
		// Format for the basic type ONNat
		public static CultureInfo NatFormat = new CultureInfo("en-US");
		// Format for the basic type ONDate
		public static CultureInfo DateFormat = new CultureInfo("es-ES");
		// Format for the basic type ONDateTime
		public static CultureInfo DateTimeFormat = new CultureInfo("es-ES");
		// Format for the basic type ONTime
		public static CultureInfo TimeFormat = new CultureInfo("es-ES");
		#endregion

		#region Properties
		/// <summary>
		/// Represents the base property for the type in SQL
		/// </summary>
        public abstract SqlDbType SQLType
		{
			get;
		}
		/// <summary>
		/// Returns the value of the object
		/// </summary>
		public object Value
		{
			get
			{
				return mValue;
			}
			set
			{
				mValue = value;
			}
		}
		/// <summary>
		/// Returns the Null value of all basic types.
		/// </summary>
		public static ONSimpleType Null(string type)
		{
			if (string.Compare(type, "Int", true) == 0)
				return ONInt.Null;
			else if (string.Compare(type, "String", true) == 0)
				return ONString.Null;
			else if (string.Compare(type, "Bool", true) == 0)
				return ONBool.Null;
			else if (string.Compare(type, "Real", true) == 0)
				return ONReal.Null;
			else if ((string.Compare(type, "Autonumerico", true) == 0) || (string.Compare(type, "Autonumeric", true) == 0))
				return ONInt.Null;
			else if (string.Compare(type, "Date", true) == 0)
				return ONDate.Null;
			else if (string.Compare(type, "DateTime", true) == 0)
				return ONDateTime.Null;
			else if (string.Compare(type, "Time", true) == 0)
				return ONTime.Null;
			else if (string.Compare(type, "Nat", true) == 0)
				return ONNat.Null;
			else if (string.Compare(type, "Text", true) == 0)
				return ONText.Null;
			else if (string.Compare(type, "Password", true) == 0)
				return ONString.Null;

			return null;
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public ONSimpleType()
		{
		}
		#endregion

		#region Operator (== !=)
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (GetType() != obj.GetType())
				return false;

			ONSimpleType lObj = obj as ONSimpleType;

			if ((lObj.Value == null) && (Value == null))
				return true;

			if ((lObj.Value == null) || (Value == null))
				return false;

			return (lObj.Value.Equals(Value));
		}
		public override int GetHashCode()
		{
			if (Value == null)
				return 0;
			else
				return Value.GetHashCode();
		}
		public static bool operator==(ONSimpleType obj1, ONSimpleType obj2)
		{
			// Both are null --> true
			if ((((object)obj1 == null) || (obj1.Value == null)) && (((object)obj2 == null) || (obj2.Value == null)))
				return true;
		
			// One of them is null (not the other)--> false
			if (((object)obj1 == null) || (obj1.Value == null) || ((object)obj2 == null) || (obj2.Value == null))
				return false;

			// If they are from different types --> false
			if (obj1.GetType() != obj2.GetType())
				return false;

			return ((obj1.Value.Equals(obj2.Value)));
		}
		public static bool operator!=(ONSimpleType obj1, ONSimpleType obj2)
		{
			return !(obj1 == obj2);
		}
		#endregion

		#region IComparable methods
		public abstract int CompareTo(object obj);
		#endregion
	}
}

