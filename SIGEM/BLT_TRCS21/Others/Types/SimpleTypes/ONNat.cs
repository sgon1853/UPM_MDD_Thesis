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
	/// Basic Type ONNat
	/// </summary>
	public class ONNat : ONSimpleType
	{
		#region Properties
		/// <summary>
		/// Represents the typed value of the object
		/// </summary>
		public int TypedValue
		{
			get
			{
				return (int) Value;
			}
			set
			{
				Value = value;
			}
		}
		/// <summary>
		/// Represents the Null value of this type.
		/// </summary>
		public new static ONNat Null
		{
			get
			{
				ONNat lNull = new ONNat();
				lNull.Value = null;
				return lNull;
			}
		}
		/// <summary>
		/// Returns the minimum value of the ONNat type
		/// </summary>
		public static ONNat MinValue
		{
			get
			{
				return new ONNat(0);
			}
		}
		/// <summary>
		/// Returns the maximum value of the ONNat type
		/// </summary>
		public static ONNat MaxValue
		{
			get
			{
				return new ONNat(int.MaxValue);
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
		public static ONNat DefaultValue
		{
			get
			{
				return new ONNat(0);
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public ONNat()
		{
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONNat(int val)
		{
			TypedValue = val;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONNat(ONNat val)
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
		public ONNat(string val)
		{
			TypedValue = Convert.ToInt32(val, NatFormat);
		}
		#endregion

		#region Operator (== !=)
		public static ONBool operator==(ONNat obj1, ONNat obj2)
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
		public static ONBool operator!=(ONNat obj1, ONNat obj2)
		{
			return !(obj1 == obj2);
		}
		#endregion

		#region Operator (+)
		public static ONNat operator+(ONNat obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONNat.Null;

			return new ONNat(obj1.TypedValue + obj2.TypedValue);
		}
		public static ONInt operator+(ONNat obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONInt.Null;

			return new ONInt(obj1.TypedValue + obj2.TypedValue);
		}
		public static ONReal operator+(ONNat obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue + obj2.TypedValue);
		}
		#endregion

		#region Operator (++)
		public static ONNat operator++(ONNat obj1)
		{
			return new ONNat(obj1.TypedValue + 1);
		}
		#endregion

		#region Operator (<)
		public static ONBool operator<(ONNat obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue < obj2.TypedValue);
		}
		public static ONBool operator<(ONNat obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue < obj2.TypedValue);
		}
		public static ONBool operator<(ONNat obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue < obj2.TypedValue);
		}
		#endregion

		#region Operator (>)
		public static ONBool operator>(ONNat obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue > obj2.TypedValue);
		}
		public static ONBool operator>(ONNat obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue > obj2.TypedValue);
		}
		public static ONBool operator>(ONNat obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue > obj2.TypedValue);
		}
		#endregion

		#region Operator (-)
		public static ONNat operator-(ONNat obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONNat.Null;

			return new ONNat(obj1.TypedValue - obj2.TypedValue);
		}
		public static ONInt operator-(ONNat obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONInt.Null;

			return new ONInt(obj1.TypedValue - obj2.TypedValue);
		}
		public static ONReal operator-(ONNat obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue - obj2.TypedValue);
		}
		#endregion

		#region Operator (<=)
		public static ONBool operator<=(ONNat obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue <= obj2.TypedValue);
		}
		public static ONBool operator<=(ONNat obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue <= obj2.TypedValue);
		}
		public static ONBool operator<=(ONNat obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue <= obj2.TypedValue);
		}
		#endregion

		#region Operator (>=)
		public static ONBool operator>=(ONNat obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue >= obj2.TypedValue);
		}
		public static ONBool operator>=(ONNat obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue >= obj2.TypedValue);
		}
		public static ONBool operator>=(ONNat obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue >= obj2.TypedValue);
		}
		#endregion

		#region Operator (*)
		public static ONNat operator*(ONNat obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONNat.Null;

			return new ONNat(obj1.TypedValue * obj2.TypedValue);
		}
		public static ONInt operator*(ONNat obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONInt.Null;

			return new ONInt(obj1.TypedValue * obj2.TypedValue);
		}
		public static ONReal operator*(ONNat obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue * obj2.TypedValue);
		}
		#endregion

		#region Operator (/)
		public static ONReal operator/(ONNat obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(((Decimal) obj1.TypedValue) / obj2.TypedValue);
		}
		public static ONReal operator/(ONNat obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(((Decimal) obj1.TypedValue) / obj2.TypedValue);
		}
		public static ONReal operator/(ONNat obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(((Decimal) obj1.TypedValue) / obj2.TypedValue);
		}
		#endregion

		#region Operator (%)
		public static ONNat operator%(ONNat obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONNat.Null;

			return new ONNat(obj1.TypedValue % obj2.TypedValue);
		}
		public static ONNat operator%(ONNat obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONNat.Null;

			return new ONNat(obj1.TypedValue % obj2.TypedValue);
		}
		public static ONReal operator%(ONNat obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue % obj2.TypedValue);
		}
		#endregion

		#region Operator (Exp)
		
		public static ONReal Exp(ONNat obj1)
		{
			if (((object) obj1 == null) || (obj1.Value == null))
				throw new ONNullException(null);
			
			return new ONReal(Convert.ToDecimal(Math.Exp(obj1.TypedValue)));
		}
		#endregion

		#region Operator (Pow)
		
		public static ONReal Pow(ONNat obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Pow(obj1.TypedValue, obj2.TypedValue)));
		}
		public static ONReal Pow(ONNat obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Pow(obj1.TypedValue, obj2.TypedValue)));
		}
		public static ONReal Pow(ONNat obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Pow(obj1.TypedValue, Convert.ToDouble(obj2.TypedValue))));
		}
		#endregion

		#region Operator (Max)
		
		public static ONNat Max(ONNat obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONNat.Null;

			return new ONNat(Math.Max(obj1.TypedValue, obj2.TypedValue));
		}
		public static ONInt Max(ONNat obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONInt.Null;

			return new ONInt(Math.Max(obj1.TypedValue, obj2.TypedValue));
		}
		public static ONReal Max(ONNat obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(Math.Max(obj1.TypedValue, obj2.TypedValue));
		}
		#endregion

		#region Operator (Min)
		
		public static ONNat Min(ONNat obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONNat.Null;

			return new ONNat(Math.Min(obj1.TypedValue, obj2.TypedValue));
		}
		public static ONInt Min(ONNat obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONInt.Null;

			return new ONInt(Math.Min(obj1.TypedValue, obj2.TypedValue));
		}
		public static ONReal Min(ONNat obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(Math.Min(obj1.TypedValue, obj2.TypedValue));
		}
		#endregion

		#region IComparable methods
		public override int CompareTo(object obj)
		{
			ONNat lVal = obj as ONNat;

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

