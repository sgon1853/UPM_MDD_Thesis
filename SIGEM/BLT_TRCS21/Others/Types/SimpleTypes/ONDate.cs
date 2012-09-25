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
	/// Basic Type ONDate
	/// </summary>
	public class ONDate : ONSimpleType
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
		public new static ONDate Null
		{
			get
			{
				ONDate lNull = new ONDate();
				lNull.Value = null;
				return lNull;
			}
		}
		/// <summary>
		/// Returns the minimum value of the ONDate type
		/// </summary>
		public static ONDate MinValue
		{
			get
			{
				return new ONDate(System.DateTime.MinValue);
			}
		}
		/// <summary>
		/// Returns the maximum value of the ONDate type
		/// </summary>
		public static ONDate MaxValue
		{
			get
			{
				return new ONDate(System.DateTime.MaxValue);
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
		public static ONDate DefaultValue
		{
			get
			{
				return new ONDate(new DateTime(1970, 1, 1 , 0 , 0, 0));
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public ONDate()
		{
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONDate(DateTime val)
		{
			TypedValue = val;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="val">Value for this type</param>
		public ONDate(ONDate val)
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
		public ONDate(string val)
		{
			TypedValue = Convert.ToDateTime(val, DateFormat);
		}
		#endregion

		#region Operator (== !=)
		public override bool Equals(object obj)
		{
			ONDate lObj = obj as ONDate;

			if ((Value == null) && (lObj.Value == null))
				return true;

			if ((Value == null) || (lObj.Value == null))
				return false;

			DateTime lDate1 = TypedValue;
			DateTime lDate2 = lObj.TypedValue;

			return ((lDate1.Year == lDate2.Year) && (lDate1.Month == lDate2.Month) && (lDate1.Day == lDate2.Day));
		}
		public override int GetHashCode()
		{
			if (Value == null)
				return 0;
			else
			{
				DateTime lDate1 = TypedValue;
				return (lDate1.Year.GetHashCode() + lDate1.Month.GetHashCode() + lDate1.Day.GetHashCode ());
			}
		}
		public static ONBool operator==(ONDate obj1, ONDate obj2)
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
		public static ONBool operator!=(ONDate obj1, ONDate obj2)
		{
			return !(obj1 == obj2);
		}

		#endregion

		#region Operator (<)
		public static ONBool operator<(ONDate obj1, ONDate obj2)
		{
			if ((object) obj2 == null)
				throw new ONNullException(null);
			
			if (obj2.Value == null)
				return new ONBool(false);

			return (obj1 < obj2.TypedValue);
		}
		public static ONBool operator<(ONDate obj1, DateTime obj2)
		{
			if ((object) obj1 == null)
				throw new ONNullException(null);

			if (obj1.Value == null)
				return new ONBool(false);

			DateTime lObj1 = obj1.TypedValue;

			return new ONBool(
				(lObj1.Year < obj2.Year) || 
				((lObj1.Year == obj2.Year) && (lObj1.Month < obj2.Month)) || 
				((lObj1.Year == obj2.Year) && (lObj1.Month == obj2.Month) && (lObj1.Day < obj2.Day)));
		}
		#endregion

		#region Operator (>)
		public static ONBool operator>(ONDate obj1, ONDate obj2)
		{
			if ((object) obj2 == null)
				throw new ONNullException(null);
			
			if (obj2.Value == null)
				return new ONBool(false);

			return (obj1 > obj2.TypedValue);
		}
		public static ONBool operator>(ONDate obj1, DateTime obj2)
		{
			if ((object) obj1 == null)
				throw new ONNullException(null);

			if (obj1.Value == null)
				return new ONBool(false);

			DateTime lObj1 = obj1.TypedValue;

			return new ONBool(
				(lObj1.Year > obj2.Year) || 
				((lObj1.Year == obj2.Year) && (lObj1.Month > obj2.Month)) || 
				((lObj1.Year == obj2.Year) && (lObj1.Month == obj2.Month) && (lObj1.Day > obj2.Day)));
		}
		#endregion

		#region Operator (<=)
		public static ONBool operator<=(ONDate obj1, ONDate obj2)
		{
			if ((object) obj2 == null)
				throw new ONNullException(null);
			
			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);

			if (obj2.Value == null)
				return new ONBool(false);

			return (obj1 <= obj2.TypedValue);
		}
		public static ONBool operator<=(ONDate obj1, DateTime obj2)
		{
			if ((object) obj1 == null)
				throw new ONNullException(null);

			if (obj1.Value == null)
				return new ONBool(false);

			DateTime lObj1 = obj1.TypedValue;

			return new ONBool(
				(lObj1.Year < obj2.Year) || 
				((lObj1.Year == obj2.Year) && (lObj1.Month < obj2.Month)) || 
				((lObj1.Year == obj2.Year) && (lObj1.Month == obj2.Month) && (lObj1.Day <= obj2.Day)));
		}
		#endregion

		#region Operator (>=)
		public static ONBool operator>=(ONDate obj1, ONDate obj2)
		{
			if ((object) obj2 == null)
				throw new ONNullException(null);
			
			if ((obj1.Value == null) && (obj2.Value == null))
				return new ONBool(true);

			if (obj2.Value == null)
				return new ONBool(false);

			return (obj1 >= obj2.TypedValue);
		}
		public static ONBool operator>=(ONDate obj1, DateTime obj2)
		{
			if ((object) obj1 == null)
				throw new ONNullException(null);

			if (obj1.Value == null)
				return new ONBool(false);

			DateTime lObj1 = obj1.TypedValue;

			return new ONBool(
				(lObj1.Year > obj2.Year) || 
				((lObj1.Year == obj2.Year) && (lObj1.Month > obj2.Month)) || 
				((lObj1.Year == obj2.Year) && (lObj1.Month == obj2.Month) && (lObj1.Day >= obj2.Day)));
		}
		#endregion

		#region Operator (Max)	
		public static ONDate Max(ONDate obj1, ONDate obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null) ||((object) obj2 == null) || (obj2.Value == null))
				throw new ONNullException(null);
	
			if (DateTime.Compare(obj1.TypedValue.Date, obj2.TypedValue.Date) >= 0)
				return obj1;
			else
				return obj2;
		}
		#endregion

		#region Operator (Min)	
		public static ONDate Min(ONDate obj1, ONDate obj2)
		{
			if (((object) obj1 == null) || (obj1.Value == null) ||((object) obj2 == null) || (obj2.Value == null))
				throw new ONNullException(null);
	
			if (DateTime.Compare(obj1.TypedValue.Date, obj2.TypedValue.Date) >= 0)
				return obj2;
			else
				return obj1;
		}
		#endregion

		#region IComparable methods
		public override int CompareTo(object obj)
		{
			ONDate lVal = obj as ONDate;

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

