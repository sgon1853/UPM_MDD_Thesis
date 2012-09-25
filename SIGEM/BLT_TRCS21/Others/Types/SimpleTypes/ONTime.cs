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
	/// Basic Type ONTime
	/// </summary>
	public class ONTime : ONSimpleType
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
		public new static ONTime Null
		{
			get
			{
				ONTime lNull = new ONTime();
				lNull.Value = null;
				return lNull;
			}
		}
		/// <summary>
		/// Returns the minimum value of the ONTime type
		/// </summary>
		public static ONTime MinValue
		{
			get
			{
				return new ONTime(System.DateTime.MinValue);
			}
		}
		/// <summary>
		/// Returns the maximum value of the ONTime type
		/// </summary>
		public static ONTime MaxValue
		{
			get
			{
				return new ONTime(System.DateTime.MaxValue);
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
		public static ONTime DefaultValue
		{
			get
			{
				return new ONTime(new DateTime(1970, 1, 1 , 0 , 0, 0));
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public ONTime()
		{
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONTime(DateTime val)
		{
			TypedValue = val;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONTime(TimeSpan val)
		{
			TypedValue = new DateTime(1970, 1, 1 , val.Hours, val.Minutes, val.Seconds);
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONTime(ONTime val)
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
		public ONTime(string val)
		{
			TypedValue = Convert.ToDateTime(val, TimeFormat);
		}
		#endregion

		#region Operator (== !=)
		public override bool Equals(object obj)
		{
			ONTime lObj = obj as ONTime;

			if ((Value == null) && (lObj.Value == null))
				return true;

			if ((Value == null) || (lObj.Value == null))
				return false;

			DateTime lTime1 = TypedValue;
			DateTime lTime2 = lObj.TypedValue;

			return ((lTime1.Hour == lTime2.Hour) && (lTime1.Minute == lTime2.Minute) && (lTime1.Second == lTime2.Second) && (lTime1.Millisecond == lTime2.Millisecond));
		}
		public override int GetHashCode()
		{
			if (Value == null)
				return 0;
			else
			{
				DateTime lTime1 = TypedValue;
				return (lTime1.Hour.GetHashCode() + lTime1.Minute.GetHashCode() + lTime1.Second.GetHashCode() + lTime1.Millisecond.GetHashCode());
			}
		}
		public static ONBool operator==(ONTime obj1, ONTime obj2)
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
		public static ONBool operator!=(ONTime obj1, ONTime obj2)
		{
			return !(obj1 == obj2);
		}

		#endregion

		#region Operator (<)
		public static ONBool operator<(ONTime obj1, ONTime obj2)
		{
			if ((object) obj2 == null)
				throw new ONNullException(null);
			
			if (obj2.Value == null)
				return new ONBool(false);

			return (obj1 < obj2.TypedValue);
		}
		public static ONBool operator<(ONTime obj1, DateTime obj2)
		{
			if ((object) obj1 == null)
				throw new ONNullException(null);

			if (obj1.Value == null)
				return new ONBool(false);

			DateTime lObj1 = obj1.TypedValue;

			return new ONBool(
				(lObj1.Hour < obj2.Hour) || 
				((lObj1.Hour == obj2.Hour) && (lObj1.Minute < obj2.Minute)) || 
				((lObj1.Hour == obj2.Hour) && (lObj1.Minute == obj2.Minute) && (lObj1.Second < obj2.Second)) || 
				((lObj1.Hour == obj2.Hour) && (lObj1.Minute == obj2.Minute) && (lObj1.Second == obj2.Second) && (lObj1.Millisecond < obj2.Millisecond)));
		}
		#endregion

		#region Operator (>)
		public static ONBool operator>(ONTime obj1, ONTime obj2)
		{
			if ((object) obj2 == null)
				throw new ONNullException(null);
			
			if (obj2.Value == null)
				return new ONBool(false);

			return (obj1 > obj2.TypedValue);
		}
		public static ONBool operator>(ONTime obj1, DateTime obj2)
		{
			if ((object) obj1 == null)
				throw new ONNullException(null);

			if (obj1.Value == null)
				return new ONBool(false);

			DateTime lObj1 = obj1.TypedValue;

			return new ONBool(
				(lObj1.Hour > obj2.Hour) || 
				((lObj1.Hour == obj2.Hour) && (lObj1.Minute > obj2.Minute)) || 
				((lObj1.Hour == obj2.Hour) && (lObj1.Minute == obj2.Minute) && (lObj1.Second > obj2.Second)) || 
				((lObj1.Hour == obj2.Hour) && (lObj1.Minute == obj2.Minute) && (lObj1.Second == obj2.Second) && (lObj1.Millisecond > obj2.Millisecond)));
		}
		#endregion

		#region Operator (<=)
		public static ONBool operator<=(ONTime obj1, ONTime obj2)
		{
			if ((object) obj2 == null)
				throw new ONNullException(null);
			
			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);

			if (obj2.Value == null)
				return new ONBool(false);

			return (obj1 <= obj2.TypedValue);
		}
		public static ONBool operator<=(ONTime obj1, DateTime obj2)
		{
			if ((object) obj1 == null)
				throw new ONNullException(null);

			if (obj1.Value == null)
				return new ONBool(false);

			DateTime lObj1 = obj1.TypedValue;

			return new ONBool(
				(lObj1.Hour < obj2.Hour) || 
				((lObj1.Hour == obj2.Hour) && (lObj1.Minute < obj2.Minute)) || 
				((lObj1.Hour == obj2.Hour) && (lObj1.Minute == obj2.Minute) && (lObj1.Second < obj2.Second)) || 
				((lObj1.Hour == obj2.Hour) && (lObj1.Minute == obj2.Minute) && (lObj1.Second == obj2.Second) && (lObj1.Millisecond <= obj2.Millisecond)));
		}
		#endregion

		#region Operator (>=)
		public static ONBool operator>=(ONTime obj1, ONTime obj2)
		{
			if ((object) obj2 == null)
				throw new ONNullException(null);
			
			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);

			if (obj2.Value == null)
				return new ONBool(false);

			return (obj1 >= obj2.TypedValue);
		}
		public static ONBool operator>=(ONTime obj1, DateTime obj2)
		{
			if ((object) obj1 == null)
				throw new ONNullException(null);

			if (obj1.Value == null)
				return new ONBool(false);

			DateTime lObj1 = obj1.TypedValue;

			return new ONBool(
				(lObj1.Hour > obj2.Hour) || 
				((lObj1.Hour == obj2.Hour) && (lObj1.Minute > obj2.Minute)) || 
				((lObj1.Hour == obj2.Hour) && (lObj1.Minute == obj2.Minute) && (lObj1.Second > obj2.Second)) || 
				((lObj1.Hour == obj2.Hour) && (lObj1.Minute == obj2.Minute) && (lObj1.Second == obj2.Second) && (lObj1.Millisecond >= obj2.Millisecond)));
		}
		#endregion

		#region Operator (Max)	
		public static ONTime Max(ONTime obj1, ONTime obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null) ||((object) obj2 == null) || (obj2.Value == null))
				throw new ONNullException(null);
			
			if (TimeSpan.Compare(obj1.TypedValue.TimeOfDay, obj2.TypedValue.TimeOfDay) >= 0)
				return obj1;
			else
				return obj2;
		}
		#endregion

		#region Operator (Min)	
		public static ONTime Min(ONTime obj1, ONTime obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null) ||((object) obj2 == null) || (obj2.Value == null))
				throw new ONNullException(null);

			if (TimeSpan.Compare(obj1.TypedValue.TimeOfDay, obj2.TypedValue.TimeOfDay) >= 0)
				return obj2;
			else
				return obj1;
		}
		#endregion

		#region IComparable methods
		public override int CompareTo(object obj)
		{
			ONTime lVal = obj as ONTime;

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

