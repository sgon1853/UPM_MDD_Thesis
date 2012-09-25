// 3.4.4.5

using System;
using System.Xml;
using System.Data;
using System.Globalization; 
using SIGEM.Business.Exceptions;
using SIGEM.Business.XML;

namespace SIGEM.Business.Types
{
	/// <summary>
	/// Basic Type ONBool
	/// </summary>
	public class ONBool : ONSimpleType
	{
		#region Properties
		/// <summary>
		/// Represents the typed value of the object
		/// </summary>
		public bool TypedValue
		{
			get
			{
				return (bool) Value;
			}
			set
			{
				Value = value;
			}
		}
		/// <summary>
		/// Represents the Null value of this type.
		/// </summary>
		public new static ONBool Null
		{
			get
			{
				ONBool lNull = new ONBool();
				lNull.Value = null;
				return lNull;
			}
		}
		/// <summary>
		/// Returns the type in SQL of this type of data.
		/// </summary>
		public override SqlDbType SQLType
		{
			get
			{
				return SqlDbType.Int;
			}
		}
		/// <summary>
		/// Represents the default value of this type.
		/// </summary>
		public static ONBool DefaultValue
		{
			get
			{
				return new ONBool(false);
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public ONBool()
		{
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONBool(bool val)
		{
			TypedValue = val;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONBool(ONBool val)
		{
			if (val == null)
				Value = null;
			else
			Value = val.Value;
		}
		/// <summary>
		/// Constructor added in order to translate constants. In the formula translation
		/// it is necessary to create ON types from a string in a formula tree. 
		/// </summary>
		/// <param name="val">Value for this type in string format</param>
		public ONBool(string val)
		{
			TypedValue = Convert.ToBoolean(val, BoolFormat);
		}
		#endregion

		#region Operator (== !=)
		public static ONBool operator==(ONBool obj1, ONBool obj2)
		{
			if ((object) obj1 == null && (object) obj2 == null)
				return new ONBool(true);

			if (((object) obj1 == null || obj1.Value == null) &&
				((object) obj2 == null || obj2.Value == null))
				return new ONBool(true);

			if ((object) obj1 == null || (object) obj2 == null)
				return new ONBool(false);

			return new ONBool(obj1.Equals(obj2));
		}
		public static ONBool operator==(ONBool obj1, bool obj2)
		{
			if ((object) obj1 == null)
				return new ONBool(false);

			return new ONBool(obj1.Equals(obj2));
		}
		public static ONBool operator!=(ONBool obj1, ONBool obj2)
		{
			return !(obj1 == obj2);
		}
		public static ONBool operator!=(ONBool obj1, bool obj2)
		{
			return new ONBool(!(obj1.TypedValue == obj2));
		}
		#endregion

		#region Operator (&)
		public static ONBool operator&(ONBool obj1, ONBool obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null) ||((object) obj2 == null) || (obj2.Value == null))
				throw new ONNullException(null);

			return new ONBool(obj1.TypedValue & obj2.TypedValue);
		}
		public static bool operator&(ONBool obj1, bool obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null))
				throw new ONNullException(null);

			return obj1.TypedValue & obj2;
		}
		public static bool operator&(bool obj1, ONBool obj2)
		{
			if (((object) obj2 == null) || (obj2.Value == null))
				throw new ONNullException(null);

			return obj2.TypedValue & obj1;
		}
		#endregion

		#region Operator (|)
		public static ONBool operator|(ONBool obj1, ONBool obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null) ||((object) obj2 == null) || (obj2.Value == null))
				throw new ONNullException(null);
			
			return new ONBool(obj1.TypedValue | obj2.TypedValue);
		}
		public static bool operator|(ONBool obj1, bool obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null))
				throw new ONNullException(null);
			
			return obj1.TypedValue | obj2;
		}
		public static bool operator|(bool obj1, ONBool obj2)
		{
			if (((object) obj2 == null) || (obj2.Value == null))
				throw new ONNullException(null);
			
			return obj2.TypedValue | obj1;
		}
		#endregion

		#region Operator (^)
		public static ONBool operator^(ONBool obj1, ONBool obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null) ||((object) obj2 == null) || (obj2.Value == null))
				throw new ONNullException(null);

			return new ONBool(obj1.TypedValue ^ obj2.TypedValue);
		}
		#endregion

		#region Operator (true false)
		public static bool operator true(ONBool obj1)
		{
			return obj1.TypedValue;
		}
		public static bool operator false(ONBool obj1)
		{
			return !obj1.TypedValue;
		}
		#endregion

		#region Operator (!)
		public static ONBool operator!(ONBool obj1)
		{
			return new ONBool(!obj1.TypedValue);
		}
		#endregion

		#region IComparable methods
		public override int CompareTo(object obj)
		{
			ONBool lVal = obj as ONBool;

			if (lVal == null)
				return 1;

			if (Value == null && lVal.Value == null)
				return 0;

			if (Value == null)
				return -1;

			if (lVal.Value == null)
				return 1;

			return (TypedValue.CompareTo(lVal.TypedValue));
		}
		#endregion
	}
}

