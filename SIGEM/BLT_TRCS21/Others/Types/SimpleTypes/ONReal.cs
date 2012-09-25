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
	/// Basic Type ONReal
	/// </summary>
	public class ONReal : ONSimpleType
	{
		#region Properties
		/// <summary>
		/// Represents the typed value of the object
		/// </summary>
		public decimal TypedValue
		{
			get
			{
				return (decimal) Value;
			}
			set
			{
				Value = value;
			}
		}
		/// <summary>
		/// Represents the Null value of this type.
		/// </summary>
		public new static ONReal Null
		{
			get
			{
				ONReal lNull = new ONReal();
				lNull.Value = null;
				return lNull;
			}
		}
		/// <summary>
		/// Returns the minimum value of the ONReal type
		/// </summary>
		public static ONReal MinValue
		{
			get
			{
				return new ONReal(decimal.MinValue);
			}
		}
		/// <summary>
		/// Returns the maximum value of the ONReal type
		/// </summary>
		public static ONReal MaxValue
		{
			get
			{
				return new ONReal(decimal.MaxValue);
			}
		}
		/// <summary>
		/// Returns the type in SQL of this type of data.
		/// </summary>
		public override SqlDbType SQLType
		{
			get
			{
				return SqlDbType.Decimal;
			}
		}
		/// <summary>
		/// Represents the default value of this type.
		/// </summary>
		public static ONReal DefaultValue
		{
			get
			{
				return new ONReal(0m);
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public ONReal()
		{
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONReal(decimal val)
		{
			TypedValue = val;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONReal(ONReal val)
		{	
			if (val == null)
				Value = null;
			else
				Value = val.Value;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONReal(ONInt val)
		{
			TypedValue = (decimal) val.TypedValue;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONReal(ONNat val)
		{
			TypedValue = (decimal) val.TypedValue;
		}
		/// <summary>
		/// Constructor added in order to translate constants. In the formula translation
		/// it is necessary to create ON types from a string in a formula tree. 
		/// </summary>
		/// <param name="val">Value for this type in string format</param> 
		public ONReal(string val)
		{
			TypedValue = Convert.ToDecimal(val, RealFormat);
		}
		#endregion

		#region Castings
		public static implicit operator ONReal(ONNat val)
		{
			if (val.Value == null)
				return ONReal.Null;

			return (new ONReal(val.TypedValue));
		}
		public static implicit operator ONReal(ONInt val)
		{
			if (val.Value == null)
				return ONReal.Null;

			return (new ONReal(val.TypedValue));
		}
		#endregion

		#region Operator (== !=)
		public static ONBool operator==(ONReal obj1, ONReal obj2)
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
		public static ONBool operator!=(ONReal obj1, ONReal obj2)
		{
			return !(obj1 == obj2);
		}
		#endregion

		#region Operator (+)
		public static ONReal operator+(ONReal obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue + obj2.TypedValue);
		}
		public static ONReal operator+(ONReal obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue + obj2.TypedValue);
		}
		public static ONReal operator+(ONReal obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue + obj2.TypedValue);
		}
		#endregion

		#region Operator (++)
		public static ONReal operator++(ONReal obj1)
		{
			return new ONReal(obj1.TypedValue + 1);
		}
		#endregion

		#region Operator (<)
		public static ONBool operator<(ONReal obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue < obj2.TypedValue);
		}
		public static ONBool operator<(ONReal obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue < obj2.TypedValue);
		}
		public static ONBool operator<(ONReal obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue < obj2.TypedValue);
		}
		#endregion

		#region Operator (>)
		public static ONBool operator>(ONReal obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue > obj2.TypedValue);
		}
		public static ONBool operator>(ONReal obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue > obj2.TypedValue);
		}
		public static ONBool operator>(ONReal obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue > obj2.TypedValue);
		}
		#endregion

		#region Operator (-)
		public static ONReal operator-(ONReal obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue - obj2.TypedValue);
		}
		public static ONReal operator-(ONReal obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue - obj2.TypedValue);
		}
		public static ONReal operator-(ONReal obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue - obj2.TypedValue);
		}
		#endregion

		#region Operator (<=)
		public static ONBool operator<=(ONReal obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue <= obj2.TypedValue);
		}
		public static ONBool operator<=(ONReal obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue <= obj2.TypedValue);
		}
		public static ONBool operator<=(ONReal obj1, ONNat obj2)
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
		public static ONBool operator>=(ONReal obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue >= obj2.TypedValue);
		}
		public static ONBool operator>=(ONReal obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue >= obj2.TypedValue);
		}
		public static ONBool operator>=(ONReal obj1, ONNat obj2)
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
		public static ONReal operator*(ONReal obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue * obj2.TypedValue);
		}
		public static ONReal operator*(ONReal obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue * obj2.TypedValue);
		}
		public static ONReal operator*(ONReal obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue * obj2.TypedValue);
		}
		#endregion

		#region Operator (/)
		public static ONReal operator/(ONReal obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue / obj2.TypedValue);
		}
		public static ONReal operator/(ONReal obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue / obj2.TypedValue);
		}
		public static ONReal operator/(ONReal obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue / obj2.TypedValue);
		}
		#endregion

		#region Operator (%)
		public static ONReal operator%(ONReal obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue % obj2.TypedValue);
		}
		public static ONReal operator%(ONReal obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue % obj2.TypedValue);
		}
		public static ONReal operator%(ONReal obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(obj1.TypedValue % obj2.TypedValue);
		}
		#endregion

		#region Operator (Exp)
		
		public static ONReal Exp(ONReal obj1)
		{
			if (((object) obj1 == null) || (obj1.Value == null))
				throw new ONNullException(null);

			return new ONReal(Convert.ToDecimal(Math.Exp(Convert.ToDouble(obj1.TypedValue))));
		}

		#endregion

		#region Operator (Pow)
		
		public static ONReal Pow(ONReal obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Pow(Convert.ToDouble(obj1.TypedValue), Convert.ToDouble(obj2.TypedValue))));
		}
		public static ONReal Pow(ONReal obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Pow(Convert.ToDouble(obj1.TypedValue), Convert.ToDouble(obj2.TypedValue))));
		}
		public static ONReal Pow(ONReal obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Pow(Convert.ToDouble(obj1.TypedValue), Convert.ToDouble(obj2.TypedValue))));
		}

		#endregion

		#region Operator (Max)
		
		public static ONReal Max(ONReal obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(Math.Max(obj1.TypedValue, obj2.TypedValue));
		}
		public static ONReal Max(ONReal obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(Math.Max(obj1.TypedValue, obj2.TypedValue));
		}
		public static ONReal Max(ONReal obj1, ONNat obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(Math.Max(obj1.TypedValue, obj2.TypedValue));
		}

		#endregion

		#region Operator (Min)
		
		public static ONReal Min(ONReal obj1, ONReal obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(Math.Min(obj1.TypedValue, obj2.TypedValue));
		}
		public static ONReal Min(ONReal obj1, ONInt obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) || (obj2.Value == null))
				return ONReal.Null;

			return new ONReal(Math.Min(obj1.TypedValue, obj2.TypedValue));
		}
		public static ONReal Min(ONReal obj1, ONNat obj2)
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
			ONReal lVal = obj as ONReal;

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

