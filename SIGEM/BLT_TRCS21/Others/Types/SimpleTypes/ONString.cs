// 3.4.4.5

using System;
using System.Xml;
using System.Data;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business.Types
{
	/// <summary>
	/// Basic Type ONString
	/// </summary>
	public class ONString : ONSimpleType
	{
		#region Members
		// Length of the string
		protected int mTam;
		#endregion

		#region Properties
		/// <summary>
		/// Represents the typed value of the object
		/// </summary>
		public string TypedValue
		{
			get
			{
				return Value as string;
			}
			set
			{
				Value = value;
			}
		}
		/// <summary>
		/// Represents the Null value of this type.
		/// </summary>
		public new static ONString Null
		{
			get
			{
				ONString lNull = new ONString();
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
                return SqlDbType.Char;
			}
		}
		/// <summary>
		/// Represents the default value of this type.
		/// </summary>
		public static ONString DefaultValue
		{
			get
			{
				return new ONString("");
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public ONString()
		{
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONString(string val)
		{
			TypedValue = val;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONString(ONString val)
		{
			if (val == null)
				Value = null;
			else
				Value = val.Value;
		}
		#endregion

		#region Castings
		#endregion

		#region Operator (== !=)
		public override bool Equals(object obj)
		{
			ONString lObj2 = obj as ONString;

			if ((Value == null) && (((object) lObj2 == null) || (lObj2.Value == null)))
				return true;

			if ((Value == null) || (((object) lObj2 == null) || (lObj2.Value == null)))
				return false;

			return (string.Compare(TypedValue.TrimEnd(), lObj2.TypedValue.TrimEnd(), true) == 0);
		}
		public override int GetHashCode()
		{
			if (Value == null)
				return 0;
			else
				return TypedValue.TrimEnd().ToLower().GetHashCode();
		}
		public static ONBool operator==(ONString obj1, ONString obj2)
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
		public static ONBool operator!=(ONString obj1, ONString obj2)
		{
			return !(obj1 == obj2);
		}
		#endregion

		#region Operator (+)
		public static ONString operator+(ONString obj1, ONString obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONString.Null;

			return new ONString(obj1.TypedValue + obj2.TypedValue);
		}
		#endregion

		#region Operator (<)
		public static ONBool operator<(ONString obj1, ONString obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(string.Compare(obj1.TypedValue, obj2.TypedValue, true) < 0);
		}
		public static ONBool operator<(ONString obj1, ONText obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(string.Compare(obj1.TypedValue, obj2.TypedValue, true) < 0);
		}
		#endregion

		#region Operator (>)
		public static ONBool operator>(ONString obj1, ONString obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(string.Compare(obj1.TypedValue, obj2.TypedValue, true) > 0);
		}
		public static ONBool operator>(ONString obj1, ONText obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(string.Compare(obj1.TypedValue, obj2.TypedValue, true) > 0);
		}
		#endregion

		#region Operator (<=)
		public static ONBool operator<=(ONString obj1, ONString obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(string.Compare(obj1.TypedValue, obj2.TypedValue, true) <= 0);
		}
		public static ONBool operator<=(ONString obj1, ONText obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(string.Compare(obj1.TypedValue, obj2.TypedValue, true) <= 0);
		}
		#endregion

		#region Operator (>=)
		public static ONBool operator>=(ONString obj1, ONString obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(string.Compare(obj1.TypedValue, obj2.TypedValue, true) >= 0);
		}
		public static ONBool operator>=(ONString obj1, ONText obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(string.Compare(obj1.TypedValue, obj2.TypedValue, true) >= 0);
		}
		#endregion

		#region Operator (Like)
		public static ONBool Like(ONString obj1, ONString obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null) ||((object) obj2 == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue.ToUpper().StartsWith(obj2.TypedValue.ToUpper()));
				
		}
		#endregion
		
		#region Operator (Contains)
		public static ONBool Contains(ONString obj1, ONString obj2)
		{
		if (((object)obj1 == null) || (obj1.Value == null) || ((object)obj2 == null) || (obj2.Value == null))
      			return new ONBool(false);

      		return new ONBool(obj1.TypedValue.ToUpper().IndexOf(obj2.TypedValue.ToUpper()) != -1);

		}
		#endregion


		#region Operations
		public ONString Trim ()
		{
			return new ONString(TypedValue.Trim());
		}
		#endregion

		#region IComparable methods
		public override int CompareTo(object obj)
		{
			ONString lVal = obj as ONString;

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

