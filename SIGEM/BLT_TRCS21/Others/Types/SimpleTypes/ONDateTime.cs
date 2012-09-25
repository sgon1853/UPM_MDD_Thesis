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
	/// Basic Type ONDateTime
	/// </summary>
	public class ONDateTime : ONSimpleType
	{
		#region Properties
		/// <summary>
		/// Represents the typed value of the object
		/// </summary>
		public DateTime TypedValue
		{
			get
			{
				return (DateTime) Value;
			}
			set
			{
				Value = value;
			}
		}
		/// <summary>
		/// Represents the Null value of this type.
		/// </summary>
		public new static ONDateTime Null
		{
			get
			{
				ONDateTime lNull = new ONDateTime();
				lNull.Value = null;
				return lNull;
			}
		}
		/// <summary>
		/// Returns the minimum value of the ONDateTime type
		/// </summary>
		public static ONDateTime MinValue
		{
			get
			{
				return new ONDateTime(System.DateTime.MinValue);
			}
		}
		/// <summary>
		/// Returns the maximum value of the ONDateTime type
		/// </summary>
		public static ONDateTime MaxValue
		{
			get
			{
				return new ONDateTime(System.DateTime.MaxValue);
			}
		}
		/// <summary>
		/// Returns the type in SQL of this type of data.
		/// </summary>
		public override SqlDbType SQLType
		{
			get
			{
				return SqlDbType.DateTime;
			}
		}
		/// <summary>
		/// Represents the default value of this type.
		/// </summary>
		public static ONDateTime DefaultValue
		{
			get
			{
				return new ONDateTime(new DateTime(1970, 1, 1 , 0 , 0, 0));
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public ONDateTime()
		{
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONDateTime(DateTime val)
		{
			TypedValue = val;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONDateTime(ONDateTime val)
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
		public ONDateTime(string val)
		{
			TypedValue = Convert.ToDateTime(val, DateTimeFormat);
		}
		#endregion

		#region Operator (== !=)
		public static ONBool operator==(ONDateTime obj1, ONDateTime obj2)
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
		public static ONBool operator!=(ONDateTime obj1, ONDateTime obj2)
		{
			return !(obj1 == obj2);
		}

		#endregion

		#region Operator (<)
		public static ONBool operator<(ONDateTime obj1, ONDateTime obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue < obj2.TypedValue);
		}
		public static ONBool operator<(ONDateTime obj1, DateTime obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null) )
				throw new ONNullException(null);

			return new ONBool(obj1.TypedValue < obj2);
		}
		#endregion

		#region Operator (>)
		public static ONBool operator>(ONDateTime obj1, ONDateTime obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue > obj2.TypedValue);
		}
		public static ONBool operator>(ONDateTime obj1, DateTime obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null))
				throw new ONNullException(null);

			return new ONBool(obj1.TypedValue > obj2);
		}
		#endregion

		#region Operator (<=)
		public static ONBool operator<=(ONDateTime obj1, ONDateTime obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue <= obj2.TypedValue);
		}
		public static ONBool operator<=(ONDateTime obj1, DateTime obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null))
				throw new ONNullException(null);

			return new ONBool(obj1.TypedValue <= obj2);
		}
		#endregion

		#region Operator (>=)
		public static ONBool operator>=(ONDateTime obj1, ONDateTime obj2)
		{
			if (((object) obj1 == null) || ((object) obj2 == null))
				throw new ONNullException(null);

			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);
			
			if ((obj1.Value == null) || (obj2.Value == null))
				return new ONBool(false);

			return new ONBool(obj1.TypedValue >= obj2.TypedValue);
		}
		public static ONBool operator>=(ONDateTime obj1, DateTime obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null) )
				throw new ONNullException(null);

			return new ONBool(obj1.TypedValue >= obj2);
		}
		#endregion

		#region Operator (Max)	
		public static ONDateTime Max(ONDateTime obj1, ONDateTime obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null) ||((object) obj2 == null) || (obj2.Value == null))
				throw new ONNullException(null);
	
			if (DateTime.Compare(obj1.TypedValue, obj2.TypedValue) >= 0)
				return obj1;
			else
				return obj2;
		}
		#endregion

		#region Operator (Min)	
		public static ONDateTime Min(ONDateTime obj1, ONDateTime obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null) ||((object) obj2 == null) || (obj2.Value == null))
				throw new ONNullException(null);
	
			if (DateTime.Compare(obj1.TypedValue, obj2.TypedValue) >= 0)
				return obj2;
			else
				return obj1;
		}
		#endregion

		#region IComparable methods
		public override int CompareTo(object obj)
		{
			ONDateTime lVal = obj as ONDateTime;

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

