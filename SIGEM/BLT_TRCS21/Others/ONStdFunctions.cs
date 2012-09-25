// 3.4.4.5

using System;
using SIGEM.Business.Types;

namespace SIGEM.Business
{
	/// <summary>
	/// Standard functions
	/// </summary>
	public abstract class ONStdFunctions
	{
		#region Date functions
		#region Query
  
		/// <summary>
		/// Return the date of the system
		/// </summary>
		public static ONDate systemDate ()
		{	
			return new ONDate(System.DateTime.Today);
		}
	
		/// <summary>
		/// Return true if and only if year is a leap year; false in other case
		/// </summary>		
		public static ONBool isLeapYear (ONInt year)
		{
			if (year == null)
				return ONBool.Null;

			return new ONBool(System.DateTime.IsLeapYear(year.TypedValue));
		}
			
		/// <summary>
		/// Return the days of difference between date1 and date2
		/// </summary>		
		public static ONInt daysDifferenceFromDate (ONDate date1, ONDate date2)
		{
			if (date1 == null || date2 == null)
				return ONInt.Null;

			if (date1 > date2)
				return new ONInt(0);

			return new ONInt(date2.TypedValue.Subtract(date1.TypedValue).Days);
		}
	
		/// <summary>
		/// Return the number of days in a specified month of the specified year
		/// For example, if month equals 2 for February, the return value is 28 or 29 depending 
		/// upon whether year is a leap year.
		/// </summary>				
		public static ONInt daysInMonth (ONInt year, ONInt month)
		{
			if (year == null || month == null)
				return ONInt.Null;

			return new ONInt(System.DateTime.DaysInMonth(year.TypedValue, month.TypedValue));
		}
	
		/// <summary>
		/// Return the day in adate
		/// </summary>			
		public static ONInt getDay (ONDate adate)
		{
			if (adate == null)
				return ONInt.Null;

			return new ONInt(adate.TypedValue.Day);
		}

		/// <summary>
		/// Return the month in adate
		/// </summary>				
		public static ONInt getMonth (ONDate adate)
		{
			if (adate == null)
				return ONInt.Null;

			return new ONInt(adate.TypedValue.Month);
		}

		/// <summary>
		/// Counts the number of appearances of the day of the week between two dates
		/// </summary>
		public static ONInt getNumDayOfWeekBetweenDates (ONInt dayofweek, ONDate inidate, ONDate enddate)
		{
			if (dayofweek == null || inidate == null || enddate == null)
				return null;

			if (inidate > enddate)
				return new ONInt(0);

			ONInt numTotal = (ONInt)daysDifferenceFromDate(inidate, enddate);
			ONInt numDays = ONInt.Divint(numTotal, (new ONReal(7)));
			ONInt diffDays = numTotal % new ONInt(7);
			ONInt iniDay = (ONInt)getDayOfWeek(inidate);
			ONInt endDay = (ONInt)getDayOfWeek(enddate);

			if (endDay < iniDay)
			{
				endDay += new ONInt(7);
			}

			if (dayofweek < iniDay)
			{
				dayofweek += new ONInt(7);
			}

			ONInt iniRest = endDay - diffDays;
			if ((dayofweek >= iniRest) && (dayofweek <= endDay))
			{
				numDays++;
			}
			return (ONInt)numDays;
		}

		/// <summary>
		/// Return the year in adate
		/// </summary>			
		public static ONInt getYear (ONDate adate)
		{
			if (adate == null)
				return ONInt.Null;

			return new ONInt (adate.TypedValue.Year);
		}
	
		/// <summary>
		/// Return the day of week contained in adate.The possible values are 1 through 7
		/// </summary>		
		public static ONInt getDayOfWeek (ONDate adate)
		{
			if (adate == null)
				return ONInt.Null;

			int lDayOfWeek = (int) adate.TypedValue.DayOfWeek + 1;
			return new ONInt(lDayOfWeek);
		}
	
		/// <summary>
		/// Return the day of year contained in adate. The possible values are either 1 through 365 
		/// (if the year is not leap) or 1 through 366.
		/// </summary>				
		public static ONInt getDayOfYear (ONDate adate)
		{
			if (adate == null)
				return ONInt.Null;

			return new ONInt(adate.TypedValue.DayOfYear);
		}
	
		/// <summary>
		/// Return a date corresponding to the next day of the argument aDate
		/// </summary>				
		public static ONDate tomorrow (ONDate adate)
		{
			if (adate == null)
				return ONDate.Null;

			return new ONDate(adate.TypedValue.AddDays(1));
		}

		/// <summary>
		/// Return a date corresponding to the earlier day of the argument aDate
		/// </summary>				
		public static ONDate yesterday (ONDate adate)
		{
			if (adate == null)
				return ONDate.Null;

			return new ONDate(adate.TypedValue.AddDays(-1));
		}
	
		/// <summary>
		/// Returns a date to which a specified time interval has been added.
		/// </summary>				
		public static ONDate dateAdd (ONString interval, ONInt number, ONDate adate)
		{
			if (interval == null || number == null || adate == null)
				return ONDate.Null;

			switch (interval.TypedValue)
			{
				case "yyyy":
					return new ONDate(adate.TypedValue.AddYears(number.TypedValue));
				case "m":
					return new ONDate(adate.TypedValue.AddMonths(number.TypedValue));
				case "d":
					return new ONDate(adate.TypedValue.AddDays(number.TypedValue));
				default :
					return adate;
			}
		}

		#endregion Query

		#region Conversions
		/// <summary>
		/// Return a string representation of the argument adate. 
		/// Converts aDate to a string of the form: m/d/yy
		/// </summary>	
		public static ONString toShortDateFormat (ONDate adate)
		{
			if (adate == null)
				return ONString.Null;

			return new ONString(adate.TypedValue.ToString("d/M/yy"));
		}
		/// <summary>
		/// Return and string representation of the argument aDate. 
		/// Converts aDate to a string of the form: mon dd, yyyy 
		/// </summary>	
		public static ONString toMediumDateFormat (ONDate adate)
		{
			if (adate == null)
				return ONString.Null;

			return new ONString(adate.TypedValue.ToString("MMM dd, yyyy"));
		}
		/// <summary>
		/// Return and string representation of the argument adate. 
		/// Converts aDate to a string of the form: mmmm dd, yyyyFor instance: March 18, 2002
		/// </summary>			
		public static ONString toLongDateFormat (ONDate adate)
		{
			if (adate == null)
				return ONString.Null;

			return new ONString(adate.TypedValue.ToString("MMMM dd, yyyy"));
		}
		/// <summary>
		/// Return and string representation of the argument aDate
		/// Converts aDate to a string of the form: dow, mon dd, yyyy
		/// Where:
		/// dow is the day of the week (Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday). 
		/// mon is the month (Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec),
		/// dd is the day of the month (01 through 31) as two decimal digits, 
		/// yyyy is the year, as four decimal digits
		/// </summary>			
		public static ONString toFullDateFormat (ONDate adate)
		{
			if (adate == null)
				return ONString.Null;

			return new ONString(adate.TypedValue.ToString("dddd, MMMM dd, yyyy"));
		}
		/// <summary>
		/// Convert the string stringDate to a date representation. stringDate must be a short formatted string
		/// Converts aDate to a string of the form: mmmm dd, yyyyFor instance: March 18, 2002
		/// </summary>	
		public static ONDate stringToDate (ONString stringDate)
		{
			if (stringDate == null)
				return ONDate.Null;

			return new ONDate(System.DateTime.Parse(stringDate.TypedValue));
		}
		/// <summary>
		/// Convert the arguments year, month and day to a date representation.
		/// year :Must be a positive value
		/// month :Must be in the range 1..12
		/// day :Must be in the range 1..31
		/// </summary>				
		public static ONDate formatToDate (ONInt year, ONInt month, ONInt day)
		{
			if (year == null || month == null || day == null)
				return ONDate.Null;

			return new ONDate(new DateTime(year.TypedValue, month.TypedValue, day.TypedValue, 0, 0, 0));
		}
		#endregion Conversions

		#region Comparison
				
		/// <summary>
		/// Tests if adate1 is after the specified date adate2.
		/// Return: true if and only if adate1 is strictly later than the date represented by adate2.
		/// </summary>
		public static ONBool dateAfter (ONDate adate1, ONDate adate2)
		{
			if (adate1 == null || adate2 == null)
				return ONBool.Null;

			return new ONBool(adate1.TypedValue > adate2.TypedValue);
		}
	
		/// <summary>
		/// Tests if adate1 is before the specified date adate2.
		/// Return: true if and only if adate1 is strictly earlier than the date represented by adate2.
		/// </summary>			
		public static ONBool dateBefore (ONDate adate1, ONDate adate2)
		{
			if (adate1 == null || adate2 == null)
				return ONBool.Null;

			return  new ONBool(adate1.TypedValue < adate2.TypedValue);
		}
	
		/// <summary>
		/// Compare two dates for equality.
		/// The result is true if and only if the argument adate1 represents the same date as adate2.
		/// </summary>				
		public static ONBool dateEquals (ONDate adate1, ONDate adate2)
		{
			if (adate1 == null || adate2 == null)
				return ONBool.Null;

			return new ONBool(adate1.TypedValue.Equals(adate2.TypedValue));
		}

		#endregion Comparison
		#endregion Date functions

		#region DateTime functions
		#region Query

		/// <summary>
		/// Return the date and time of the system
		/// </summary>	
		public static ONDateTime systemDateTime ()
		{
			return new ONDateTime(System.DateTime.Now);
		}

		/// <summary>
		/// This function extracts the information related to the date part of adatetime (year, month, day), discarding the time related information (hour, minute, second)
		/// </summary>			
		public static ONDate getDatePart (ONDateTime adatetime)
		{
			if (adatetime == null )
				return ONDate.Null;

			return new ONDate(adatetime.TypedValue.Date);
		}
	
		/// <summary>
		/// This function extracts the information related to the time part of adatetime (hour, minute, second), discarding the date related information (year, month, day)
		/// </summary>				
		public static ONTime getTimePart (ONDateTime adatetime)
		{
			if (adatetime == null)
				return ONTime.Null;

			DateTime lDateTime = adatetime.TypedValue;			
			return new ONTime(new DateTime(1970, 1, 1, lDateTime.Hour, lDateTime.Minute, lDateTime.Second));
		}
		
		/// <summary>
		/// Returns a datetime to which a specified time interval has been added.Interval : The interval of time you want to add.
		/// </summary>				
		public static ONDateTime dateTimeAdd (ONString interval, ONInt number, ONDateTime adatetime )
		{
			if (interval == null || number == null || adatetime == null)
				return ONDateTime.Null;

			switch (interval.TypedValue)
			{
				case "yyyy":
					return new ONDateTime(adatetime.TypedValue.AddYears(number.TypedValue));
				case "m":
					return new ONDateTime(adatetime.TypedValue.AddMonths(number.TypedValue));
				case "d":
					return new ONDateTime(adatetime.TypedValue.AddDays(number.TypedValue));
				case "h":
					return new ONDateTime(adatetime.TypedValue.AddHours(number.TypedValue));
				case "n":
					return new ONDateTime(adatetime.TypedValue.AddMinutes(number.TypedValue));
				case "s":
					return new ONDateTime(adatetime.TypedValue.AddSeconds(number.TypedValue));
				default :
					return new ONDateTime(adatetime.TypedValue);
			}
		}

		/// <summary>
		/// Return the days of difference between datetime1 and datetime2
		/// </summary>		
		public static ONInt daysDifferenceFromDateTime (ONDateTime datetime1, ONDateTime datetime2)
		{
			if (datetime1 == null || datetime2 == null)
				return ONInt.Null;

			if (datetime1 > datetime2)
				return new ONInt(0);

			return new ONInt(datetime2.TypedValue.Subtract(datetime1.TypedValue).Days);
		}
	
		#endregion Query

		#region Conversions

		/// <summary>
		/// Convert the arguments adate and atime to a datetime representation. 
		/// </summary>	
		public static ONDateTime toDateTime(ONDate adate, ONTime atime)
		{
			if (adate == null || atime == null)
				return ONDateTime.Null;

			DateTime lDate = adate.TypedValue;
			DateTime lTime = atime.TypedValue;

			return new ONDateTime(new DateTime(lDate.Year, lDate.Month, lDate.Day, lTime.Hour, lTime.Minute, lTime.Second));
		}

		/// <summary>
		/// Converts aDate to a string of the form: dow mon dd hh:mm:ss zzz yyyy
		/// </summary>
		public static ONString dateTimeToString(ONDateTime aDateTime)
		{
			if (aDateTime == null)
				return ONString.Null;

			return new ONString(aDateTime.TypedValue.ToString("MMMM dd, yyyy HH:mm:ss"));
		}
			
		/// <summary>
		/// Convert the string stringDate to a datetime representation
		/// </summary>
		public static ONDateTime stringToDateTime(ONString stringDate)
		{
			if (stringDate == null)
				return ONDateTime.Null;

			return new ONDateTime(System.DateTime.Parse(stringDate.TypedValue));
		}
					
		/// <summary>
		/// Convert the arguments (year, month , day,hour, minute and second) to a datetime representation.
		/// </summary>
		/// <param name="year">Must be a positive value</param>
		/// <param name="month">Must be in the range 1..12</param>
		/// <param name="day">Must be in the range 1..31</param>
		/// <param name="hour">Must be in the range 0..23</param>
		/// <param name="minute">Must be in the range 0..59</param>
		/// <param name="second">Must be in the range 0..59</param>
		/// <returns></returns>
		public static ONDateTime formatToDateTime (ONInt year, ONInt month, ONInt day, ONInt hour, ONInt minute, ONInt second)
		{
			if (year == null || month == null || day == null || hour == null || minute == null || second == null)
				return ONDateTime.Null;

			return new ONDateTime(new DateTime(year.TypedValue, month.TypedValue, day.TypedValue, hour.TypedValue, minute.TypedValue, second.TypedValue));
		}
	
		#endregion Conversions

		#region Comparison

		/// <summary>
		/// Tests if adatetime1 is after the specified datetime adatetime2
		/// </summary>			
		/// <returns>true if and only if adatetime1 is strictly later than the datetime represented by adatetime2</returns>
		public static ONBool datetimeAfter (ONDateTime adatetime1, ONDateTime adatetime2)
		{
			if (adatetime1 == null || adatetime2 == null)
				return ONBool.Null;

			return new ONBool(adatetime1.TypedValue > adatetime2.TypedValue);
		}
	
		/// <summary>
		/// Tests if adatetime1 is before the specified datetime adatetime2
		/// </summary>
		/// <returns>true if and only if adatetime1 is strictly earlier than the datetime represented by adatetime2</returns>
		public static ONBool datetimeBefore (ONDateTime adatetime1, ONDateTime adatetime2)
		{
			if (adatetime1 == null || adatetime2 == null)
				return ONBool.Null;

			return new ONBool(adatetime1.TypedValue < adatetime2.TypedValue);
		}
	
		/// <summary>
		/// Compare two datetimes for equality
		/// </summary>				
		/// <returns>true if and only if the argument adatetime1 represents the same datetime as adatetime2</returns>
		public static ONBool datetimeEquals (ONDateTime adatetime1, ONDateTime adatetime2)
		{
			if (adatetime1 == null || adatetime2 == null)
				return ONBool.Null;

			return new ONBool(adatetime1.TypedValue.Equals(adatetime2.TypedValue));
		}
	
		#endregion Comparison
		#endregion DateTime functions

		#region Time functions
		#region Query

		/// <summary>
		/// Return the time of the system
		/// </summary>			
		public static ONTime systemTime ()
		{
			DateTime lNow = System.DateTime.Now;
			return new ONTime(new DateTime(1970, 1, 1, lNow.Hour, lNow.Minute, lNow.Second));
		}

		/// <summary>
		/// Return the hour of atime
		/// </summary>			
		public static ONInt getHour (ONTime atime)
		{
			if (atime == null)
				return ONInt.Null;

			return new ONInt(atime.TypedValue.Hour);
		}
	
		/// <summary>
		/// Return the minutes of atime
		/// </summary>				
		public static ONInt getMinute (ONTime atime)
		{
			if (atime == null)
				return ONInt.Null;

			return new ONInt(atime.TypedValue.Minute);
		}

		/// <summary>
		/// Return the seconds of atime
		/// </summary>		
		public static ONInt getSecond (ONTime atime)
		{
			if (atime == null)
				return ONInt.Null;

			return new ONInt(atime.TypedValue.Second);
		}
	

		/// <summary>
		/// Returns a time to which a specified time interval has been added.Interval : The interval of time you want to add
		/// </summary>		
		public static ONTime timeAdd (ONString interval, ONInt number, ONTime atime )
		{
			if (interval == null || number == null || atime == null)
				return ONTime.Null;

			switch (interval.TypedValue)
			{
				case "h":
					return new ONTime(atime.TypedValue.AddHours(number.TypedValue));
				case "n":
					return new ONTime(atime.TypedValue.AddMinutes(number.TypedValue));
				case "s":
					return new ONTime(atime.TypedValue.AddSeconds(number.TypedValue));
				default :
					return atime;
			}
		}

		#endregion Query

		#region Conversions

		/// <summary>
		/// Convert the arguments (hour, minute and second) to a time representation
		/// </summary>				
		public static ONTime formatToTime (ONInt hour, ONInt minute, ONInt second)
		{
			if (hour == null || minute == null || second == null)
				return ONTime.Null;
			
			return new ONTime(new DateTime(1970, 1, 1, hour.TypedValue, minute.TypedValue, second.TypedValue));
		}

		/// <summary>
		/// Convert the string stringTime to a time representation
		/// </summary>				
		public static ONTime stringToTime (ONString stringTime)
		{
			if (stringTime == null)
				return ONTime.Null;

			return new ONTime(System.DateTime.Parse(stringTime.TypedValue));
		}
		
		/// <summary>
		/// Return a string representation of the argument atime
		/// </summary>				
		public static ONString timeToString (ONTime atime)
		{
			if (atime == null)
				return ONString.Null;

			return new ONString(atime.TypedValue.ToString("HH:mm:ss"));
		}
		
		#endregion Conversions

		#region Comparison

		/// <summary>
		/// Tests if atime1 is after the specified time atime2. 
		/// </summary>				
		/// <param name="atime1"></param>
		/// <param name="atime2"></param>
		/// <returns>true if and only if atime1 is strictly later than the time represented by atime2</returns>
		public static ONBool timeAfter (ONTime atime1, ONTime atime2)
		{
			if (atime1 == null || atime2 == null)
				return ONBool.Null;

			return new ONBool(atime1.TypedValue > atime2.TypedValue);
		}
	
		/// <summary>
		/// Tests if atime1 is before the specified time atime2
		/// </summary>
		/// <returns>true if and only if atime1 is strictly earlier than the time represented by atime2</returns>
		public static ONBool timeBefore (ONTime atime1, ONTime atime2)
		{
			if (atime1 == null || atime2 == null)
				return ONBool.Null;

			return new ONBool(atime1.TypedValue < atime2.TypedValue);

		}
	
		/// <summary>
		/// Compare two times for equality
		/// </summary>
		/// <param name="atime1"></param>
		/// <param name="atime2"></param>
		/// <returns>true if and only if the argument atime1 represents the same time as atime2</returns>
		public static ONBool timeEquals (ONTime atime1, ONTime atime2)
		{
			if (atime1 == null || atime2 == null)
				return ONBool.Null;

			return new ONBool(atime1.TypedValue.Equals(atime2.TypedValue));
		}
		
		#endregion Comparison
		#endregion Time functions

		#region Numeric functions
		#region Constants

		/// <summary>
		/// Returns the value of the PI constant, equals to 3.14159265358979323846832
		/// </summary>			
		public static ONReal PI()
		{
			return new ONReal(Convert.ToDecimal(Math.PI));
		}
	
		/// <summary>
		/// Returns the value of the Euler constant, equals to 2.7182818284590452354
		/// </summary>
		public static ONReal Euler()
		{
			return new ONReal(Convert.ToDecimal(Math.E));
		}
	
		#endregion Constants

		#region Trigonometric

		/// <summary>
		/// Returns the trigonometric sine of the argument Angle
		/// </summary>				
		public static ONReal sin (ONReal angle)
		{
			if (angle == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Sin(Convert.ToDouble(angle.TypedValue))));
		}
	
		/// <summary>
		/// Returns the trigonometric cosine of the argument Angle
		/// </summary>					
		public static ONReal cos (ONReal angle)
		{
			if (angle == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Cos(Convert.ToDouble(angle.TypedValue))));
		}

		/// <summary>
		/// Returns the trigonometric tangent of the argument Angle
		/// </summary>			
		public static ONReal tan (ONReal angle)
		{
			if (angle == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Tan(Convert.ToDouble(angle.TypedValue))));
		}

		/// <summary>
		/// Returns the trigonometric tangent of the argument Angle
		/// </summary>			
		public static ONReal cot (ONReal angle)
		{
			if (angle == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(1 / Math.Tan(Convert.ToDouble(angle.TypedValue))));
		}

		/// <summary>
		/// Returns the trigonometric arc sine of the argument Value. The result is in the range of -PI/2 through PI/2
		/// </summary>			
		public static ONReal asin (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Asin(Convert.ToDouble(value.TypedValue))));
		}
	
		/// <summary>
		/// Returns the trigonometric arc cosine of the argument Value. The result is in the range of -PI/2 through PI/2
		/// </summary>			
		public static ONReal acos (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Acos(Convert.ToDouble(value.TypedValue))));
		}
	
		/// <summary>
		/// Returns the trigonometric arc tangent of the argument Value. The result is in the range of -PI/2 through PI/2
		/// </summary>			
		public static ONReal atan (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Atan(Convert.ToDouble(value.TypedValue))));
		}

		/// <summary>
		/// Returns the trigonometric arc tangent of the argument Value. The result is in the range of -PI/2 through PI/2
		/// </summary>		
		public static ONReal acot (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.PI / 2 - Math.Atan(Convert.ToDouble(value.TypedValue))));
		}

		/// <summary>
		/// Returns the trigonometric arc tangent of the argument Value. The result is in the range of -PI through PI
		/// </summary>			
		public static ONReal atan2 (ONReal x, ONReal y)
		{

			if (x == null || y == null)
				return ONReal.Null;

			decimal lX = x.TypedValue;
			decimal lY = y.TypedValue;

			if (lX == 0)
				return new ONReal(Convert.ToDecimal(Math.Sign(lY) * 1.5707963267949));
			else if (lX > 0)
				return new ONReal(Convert.ToDecimal(Math.Atan(Convert.ToDouble(lY / lX))));
			else
				return new ONReal(Convert.ToDecimal(Math.Atan(Convert.ToDouble(lY / lX)) + 3.14159265358979 * Math.Sign(lY)));
		}
	
		/// <summary>
		/// Converts an angle measured in degrees (Value) in an angle measured in radians. The result is mathematically equivalent to Value * PI / 180
		/// </summary>			
		public static ONReal toRadians (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(value.TypedValue * Convert.ToDecimal(Math.PI) / 180);
		}

		/// <summary>
		/// Converts an angle measured in radians (Value) in an angle measured in degrees. The result is mathematically equivalent to Value * 180 / PI
		/// </summary>				
		public static ONReal toDegrees (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(value.TypedValue * 180 / Convert.ToDecimal(Math.PI));
		}

		/// <summary>
		/// Returns the trigonometric secant of the argument Value
		/// </summary>				
		public static ONReal sec (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(1 / Math.Cos(Convert.ToDouble(value.TypedValue))));
		}
	
		/// <summary>
		/// Returns the trigonometric cosecant of the argument Value
		/// </summary>				
		public static ONReal csec (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(1 / Math.Sin(Convert.ToDouble(value.TypedValue))));
		}
	
		/// <summary>
		/// Returns the trigonometric inverse secant of the argument Value
		/// </summary>			
		public static ONReal asec (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.PI / 2 - Math.Atan(Math.Sign(value.TypedValue) / Math.Sqrt(Convert.ToDouble(value.TypedValue) * Convert.ToDouble(value.TypedValue) - 1))));
		}
	
		/// <summary>
		/// Returns the trigonometric inverse cosecant of the argument Value
		/// </summary>		
		public static ONReal acsec (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Atan(Math.Sign(value.TypedValue) / Math.Sqrt(Convert.ToDouble(value.TypedValue) * Convert.ToDouble(value.TypedValue) - 1))));
		}

		#endregion Trigonometric
	
		#region Elemental mathemathics

		/// <summary>
		/// Returns the Euler constant raised to the specified argument, e^Value
		/// </summary>		
		public static ONReal exp (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Exp(Convert.ToDouble(value.TypedValue))));
		}

		/// <summary>
		/// Returns the natural logarithm (base Euler constant) of the specified argument
		/// </summary>			
		public static ONReal log (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Log(Convert.ToDouble(value.TypedValue))));
		}
	
		/// <summary>
		/// Returns the value Base ^ Exponent
		/// </summary>				
		public static ONReal pow (ONReal Base, ONReal Exponent)
		{
			if (Base == null || Exponent == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Pow(Convert.ToDouble(Base.TypedValue), Convert.ToDouble(Exponent.TypedValue))));
		}
	
		/// <summary>
		/// Returns the square root of the specified value
		/// </summary>			
		public static ONReal sqrt (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(value.TypedValue))));
		}
	
		/// <summary>
		/// Returns the absolute value of the specified argument
		/// </summary>				
		public static ONNat absInt (ONInt value)
		{
			if (value == null)
				return ONNat.Null;

			return new ONNat(Math.Abs(value.TypedValue));
		}
	
		/// <summary>
		/// Returns the absolute value of the specified argument
		/// </summary>				
		public static ONReal absReal (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Math.Abs(value.TypedValue));
		}
	
		/// <summary>
		/// Returns the minimum between Value1 and Value2
		/// </summary>				
		public static ONReal minimum (ONReal value1, ONReal value2)
		{
			if (value1 == null || value2 == null)
				return ONReal.Null;

			return new ONReal(Math.Min(value1.TypedValue, value2.TypedValue));
		}
	
		/// <summary>
		/// Returns the maximum between Value1 and Value2
		/// </summary>				
		public static ONReal maximum (ONReal value1, ONReal value2)
		{
			if (value1 == null || value2 == null)
				return ONReal.Null;

			return new ONReal(Math.Max(value1.TypedValue, value2.TypedValue));
		}
	
		/// <summary>
		/// Returns the minimum between Value1 and Value2
		/// </summary>				
		public static ONInt minInt (ONInt value1, ONInt value2)
		{
			if (value1 == null || value2 == null)
				return ONInt.Null;

			return new ONInt(Math.Min(value1.TypedValue, value2.TypedValue));
		}
	
		/// <summary>
		/// Returns the maximum between Value1 and Value2
		/// </summary>				
		public static ONInt maxInt (ONInt value1, ONInt value2)
		{
			if (value1 == null || value2 == null)
				return ONInt.Null;

			return new ONInt(Math.Max(value1.TypedValue, value2.TypedValue));
		}
	
		/// <summary>
		/// Returns the logarithm of Value, in base Base
		/// </summary>			
		public static ONReal logBase (ONReal value, ONReal Base)
		{
			if (value == null || Base == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Log(Convert.ToDouble(value.TypedValue), Convert.ToDouble(Base.TypedValue))));
		}
	
		/// <summary>
		/// Returns the sign of the specified argument, represented as -1, 0 and 1, in correspondence with negative, zero or positive argument value
		/// </summary>			
		public static ONInt sign (ONReal value)
		{
			if (value == null)
				return ONInt.Null;

			return new ONInt(Math.Sign(value.TypedValue));
		}
	
		/// <summary>
		/// Returns the greatest common divisor between Value1 and Value2. 
		/// That is, the greatest integer value that exactly divides Value1 
		/// (integer quotient and remainder 0) and exactly divides Value2
		/// </summary>			
		public static ONInt gcd (ONInt value1, ONInt value2)
		{
			if (value1 == null || value2 == null)
				return ONInt.Null;

			int lRem;
			int a = value1.TypedValue;
			int b = value2.TypedValue;

			while (b != 0) 
			{
				lRem = a % b;
				a = b;
				b = lRem;
			}

			return new ONInt(a);
		}
		
		/// <summary>
		/// Returns the less common multiple between Value1 and Value2. 
		/// That is, the lesser integer value that is exactly divisible by Value1 and Value2.
		/// </summary>			
		public static ONInt lcm (ONInt value1, ONInt value2)
		{
			if (value1 == null || value2 == null)
				return ONInt.Null;

			int lMayor, lMenor;

			if (value1.TypedValue >= value2.TypedValue)
			{
				lMayor = value1.TypedValue;
				lMenor = value2.TypedValue;
			}
			else
			{
				lMayor = value2.TypedValue;
				lMenor = value1.TypedValue;
			}

			int lMcm = lMayor;

			while (lMcm % lMenor !=0)
				lMcm += lMayor;

			return new ONInt(lMcm);
		}
		
		#endregion Elemental mathemathics

		#region Rounding
		/// <summary>
		/// Returns the smallest value (closest to negative infinity) greater than or equal 
		/// to Value, and mathematically equal to an integer
		/// </summary>	
		public static ONInt ceiling (ONReal value)
		{
			if (value == null)
				return ONInt.Null;

			return new ONInt(Convert.ToInt32(Math.Ceiling(Convert.ToDouble(value.TypedValue))));
		}
		/// <summary>
		/// Returns the largest value (closest to positive infinity) less than or equal 
		/// to Value, and mathematically equal to an integer
		/// </summary>			
		public static ONInt floor (ONReal value)
		{
			if (value == null)
				return ONInt.Null;

			return new ONInt(Convert.ToInt32(Math.Floor(Convert.ToDouble(value.TypedValue))));
		}
		/// <summary>
		/// Returns the integer part of the real argument Value just removing the fractionary part.
		/// If Value is below 0, it returns the ceiling, otherwise it returns the floor
		/// </summary>				
		public static ONInt trunc (ONReal value)
		{
			if (value == null)
				return ONInt.Null;

			return new ONInt(Convert.ToInt32(Decimal.Truncate(value.TypedValue)));
		}
		/// <summary>
		/// Returns the integer value closest to Value
		/// </summary>			
		public static ONInt round (ONReal value)
		{
			if (value == null)
				return ONInt.Null;

			int lTemp = Convert.ToInt32(Math.Round(value.TypedValue));

			if (value.TypedValue - lTemp == 0.5m)
				lTemp++;
			
				return new ONInt(lTemp);
		}
		/// <summary>
		/// Returns the number with the specified precision nearest the specified value, 
		/// where digits is the number of significant fractional digits (precision) in 
		/// the return value
		/// </summary>			
		public static ONReal roundEx (ONReal value, ONInt digits)
		{
			if (value == null || digits == null)
				return ONReal.Null;

			decimal lvalue;
			int ldigits;
			
			lvalue = value.TypedValue;
			ldigits = digits.TypedValue;
			
			return new ONReal((decimal)(Math.Round(lvalue, ldigits, MidpointRounding.AwayFromZero)));
		}
		#endregion Rounding

		#region Conversions

		/// <summary>
		/// Returns the string representation of Value in decimal format
		/// </summary>		
		public static ONString intToStr (ONInt value)
		{
			if (value == null)
				return ONString.Null;
			
			return new ONString(Convert.ToString(value.TypedValue));
		}
	
		/// <summary>
		/// Returns the string representation of Value
		/// </summary>		
		public static ONString realToStr (ONReal value)
		{
			if (value == null)
				return ONString.Null;

			string lTemp = Convert.ToString(value.TypedValue);
			lTemp = lTemp.Replace(",", ".");
			return new ONString(lTemp);
		}

		/// <summary>
		/// Returns the string representation of Value in hexadecimal format
		/// </summary>			
		public static ONString toHexString (ONInt value)
		{
			if (value == null)
				return ONString.Null;

			return new ONString(Convert.ToString(value.TypedValue, 16));
		}
	
		/// <summary>
		/// Returns the string representation of Value in octal format
		/// </summary>			
		public static ONString toOctalString (ONInt value)
		{
			if (value == null)
				return ONString.Null;

			return new ONString(Convert.ToString(value.TypedValue, 8));
		}
	
		/// <summary>
		/// Returns the integer value represented in decimal format by the string Value
		/// </summary>				
		public static ONInt strToInt (ONString value)
		{
			if (value == null)
				return ONInt.Null;

			return new ONInt(Convert.ToInt32(value.TypedValue));
		}

		/// <summary>
		/// Returns the real value represented by the string Value
		/// </summary>			
		public static ONReal strToReal (ONString value)
		{
			if (value == null)
				return ONReal.Null;

			string lTemp = value.TypedValue;
			lTemp = lTemp.Replace(".", ",");
			return new ONReal(Convert.ToDecimal(lTemp));
		}

		/// <summary>
		/// Returns an integer represented by Value. Value must be a prefixed representation of
		/// a signed integer in decimal, octal or hexadecimal format. The specific prefixes 
		/// depends on the specific implementation. 
		/// </summary>			
		public static ONInt decodeString (ONString value)
		{
			if (value == null)
				return ONInt.Null;

			// Function decodeString not yet implemented
			return new ONInt(0);
		}
	
		#endregion Conversions

		#region Random

		/// <summary>
		/// Returns a uniformly distributed pseudo-random real number greater than or equal to 0
		/// and less than 1. The use of this method has a state because the generated sequence
		/// is pseudo-random, but it could be considered as stateless due to the imprecise 
		/// connection among results of different calls
		/// </summary>			
		public static ONReal rnd ()
		{
			return new ONReal(Convert.ToDecimal((new Random()).NextDouble()));
		}

		/// <summary>
		/// Returns a uniformly distributed pseudo-random discrete (integer) number greater than 
		/// or equal to 0 and less than UpperBound
		/// </summary>			
		public static ONInt rndInteger (ONInt upperBound)
		{
			if (upperBound == null)
				return ONInt.Null;

			return new ONInt((new Random()).Next(upperBound.TypedValue));
		}
	
		/// <summary>
		/// Returns a uniformly distributed pseudo-random (real) number greater than or equal to 0 
		/// and less than UpperBound
		/// </summary>				
		public static ONReal rndReal (ONReal upperBound)
		{
			if (upperBound == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal((new Random()).NextDouble() * Convert.ToDouble(upperBound.TypedValue)));
		}
	
		/// <summary>
		/// Returns a uniformly distributed pseudo-random discrete (integer) number greater than 
		/// or equal to LowerBound and less than UpperBound
		/// </summary>				
		public static ONInt rndIntBound (ONInt lowerBound, ONInt upperBound)
		{
			if (lowerBound == null || upperBound == null)
				return ONInt.Null;

			return new ONInt((new Random()).Next(lowerBound.TypedValue, upperBound.TypedValue));
		}

		/// <summary>
		/// Returns a uniformly distributed pseudo-random (real) number greater than or equal 
		/// to LowerBound and less than UpperBound
		/// </summary>			
		public static ONReal rndRealBound (ONReal lowerBound, ONReal upperBound)
		{
			if (lowerBound == null || upperBound == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal((new Random()).NextDouble()) * (upperBound.TypedValue - lowerBound.TypedValue) + lowerBound.TypedValue);
		}
	
		#endregion Random

		#region Hyperbolic trigonometric
		
		/// <summary>
		/// Returns the hyperbolic sine of the argument Value. sinh(x)=(e^x-e^-x)/2
		/// </summary>				
		public static ONReal sinh (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Sinh(Convert.ToDouble(value.TypedValue))));
		}
	
		/// <summary>
		/// Returns the hyperbolic cosine of the argument Value. cosh(x)=(e^x+e^-x)/2
		/// </summary>				
		public static ONReal cosh (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Cosh(Convert.ToDouble(value.TypedValue))));
		}
	
		/// <summary>
		/// Returns the hyperbolic tangent of the argument Value. tanh(x)=(e^x-e^-x)/(e^x+e^-x)
		/// </summary>			
		public static ONReal tanh (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Tanh(Convert.ToDouble(value.TypedValue))));
		}
	
		/// <summary>
		/// Returns the hyperbolic cotangent of the argument Value. coth(x)=(e^x+e^-x)/(e^x-e^-x)
		/// </summary>				
		public static ONReal coth (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal((Math.Exp(Convert.ToDouble(value.TypedValue)) + Math.Exp(Convert.ToDouble(-value.TypedValue))) / (Math.Exp(Convert.ToDouble(value.TypedValue)) - Math.Exp(Convert.ToDouble(-value.TypedValue)))));
		}
	
		/// <summary>
		/// Returns the hyperbolic secant of the argument Value. sech(x)=2/(e^x-e^-x)
		/// </summary>			
		public static ONReal sech (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(2 / (Math.Exp(Convert.ToDouble(value.TypedValue)) + Math.Exp(Convert.ToDouble(-value.TypedValue)))));
		}
	
		/// <summary>
		/// Returns the hyperbolic cosecant of the argument Value. csech(x)=2/(e^x+e^-x)
		/// </summary>			
		public static ONReal csech (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(2 / (Math.Exp(Convert.ToDouble(value.TypedValue)) - Math.Exp(Convert.ToDouble(-value.TypedValue)))));
		}
	

		/// <summary>
		/// Returns the inverse hyperbolic sine of the argument Value
		/// </summary>			
		public static ONReal asinh (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Log(Convert.ToDouble(value.TypedValue) + Math.Sqrt(Convert.ToDouble(value.TypedValue) * Convert.ToDouble(value.TypedValue) + 1))));
		}
	

		/// <summary>
		/// Returns the inverse hyperbolic cosine of the argument Value
		/// </summary>				
		public static ONReal acosh (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Log(Convert.ToDouble(value.TypedValue) + Math.Sqrt(Convert.ToDouble(value.TypedValue) * Convert.ToDouble(value.TypedValue) - 1))));
		}
	
		/// <summary>
		/// Returns the inverse hyperbolic tangent of the argument Value
		/// </summary>		
		public static ONReal atanh (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Log((1 + Convert.ToDouble(value.TypedValue)) / (1 - Convert.ToDouble(value.TypedValue)))) / 2);
		}
	
		/// <summary>
		/// Returns the inverse hyperbolic cotangent of the argument Value
		/// </summary>			
		public static ONReal acoth (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Log((Convert.ToDouble(value.TypedValue) + 1) / (Convert.ToDouble(value.TypedValue) - 1)) / 2));
		}
	
		/// <summary>
		/// Returns the inverse hyperbolic secant of the argument Value
		/// </summary>			
		public static ONReal asech (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Log(Convert.ToDouble((Math.Sqrt(Convert.ToDouble(-value.TypedValue) * Convert.ToDouble(value.TypedValue) + 1) + 1) / Convert.ToDouble(value.TypedValue)))));
		}
	
		/// <summary>
		/// Returns the inverse hyperbolic cosecant of the argument Value
		/// </summary>			
		public static ONReal acsech (ONReal value)
		{
			if (value == null)
				return ONReal.Null;

			return new ONReal(Convert.ToDecimal(Math.Log((Math.Sign(value.TypedValue) * Math.Sqrt(Convert.ToDouble(value.TypedValue) * Convert.ToDouble(value.TypedValue) + 1) + 1) / Convert.ToDouble(value.TypedValue))));
		}
	
		#endregion Hyperbolic trigonometric
		#endregion Numeric functions

		#region String functions
		#region Searching
		/// <summary>
		/// Returns the first position of the substring specified by the argument subStr if it is 
		/// found as part of the aString string. It returns 0 otherwise
		/// </summary>		
		public static ONInt indexOf (ONString aString, ONString subStr)
		{
			if (aString == null || subStr == null)
				return ONInt.Null;

			return new ONInt(aString.TypedValue.IndexOf(subStr.TypedValue) + 1);
		}
		/// <summary>
		/// Search the first occurrence of the substring specified by the argument subStr in the 
		/// aString string after the position specified by the argument from, if the substring 
		/// is found, its position in the string is returned. It returns 0 otherwise
		/// </summary>			
		public static ONInt indexOfFrom (ONString aString, ONString subStr, ONInt from)
		{
			if (aString == null || subStr == null || from == null)
				return ONInt.Null;

			return new ONInt(aString.TypedValue.IndexOf(subStr.TypedValue, from.TypedValue - 1) + 1);
		}
		/// <summary>
		/// Returns the last position of the substring specified by the argument subStr if it is 
		/// found as part of the aString string, otherwise it returns 0
		/// </summary>			
		public static ONInt lastIndexOf (ONString aString, ONString subStr)
		{
			if (aString == null || subStr == null)
				return ONInt.Null;

			return new ONInt(aString.TypedValue.LastIndexOf(subStr.TypedValue) + 1);
		}
		/// <summary>
		/// Search the rightmost occurrence of the substring specified by the argument subStr 
		/// in the aString string before the position specified by the argument ending, if the 
		/// substring is found it position in the string is returned, otherwise it returns 0
		/// </summary>				
		public static ONInt lastIndexOfFrom (ONString aString, ONString subStr, ONInt ending)
		{
			if (aString == null || subStr == null || ending == null)
				return ONInt.Null;

			return new ONInt(aString.TypedValue.LastIndexOf(subStr.TypedValue, ending.TypedValue - 1) + 1);
		}
		/// <summary>
		/// Returns true if aString string starts with the substring specified by the argument
		/// prefix, false otherwise
		/// </summary>				
		public static ONBool strStartsWith (ONString aString, ONString prefix)
		{
			if (aString == null || prefix == null)
				return ONBool.Null;

			return new ONBool(aString.TypedValue.StartsWith(prefix.TypedValue));
		}
		/// <summary>
		/// Returns true if aString string ends with the substring specified by the argument 
		/// sufix, false otherwise
		/// </summary>				
		public static ONBool strEndsWith (ONString aString, ONString sufix)
		{
			if (aString == null || sufix == null)
				return ONBool.Null;

			return new ONBool(aString.TypedValue.EndsWith(sufix.TypedValue));
		}
		/// <summary>
		/// Returns true if the substring that begins in the from position of the aString string, 
		/// starts with the substring specified by the argument prefix, false otherwise
		/// </summary>		
		public static ONBool strStartsWithFrom (ONString aString, ONString prefix, ONInt from)
		{
			if (aString == null || prefix == null || from == null)
				return ONBool.Null;

			return new ONBool(aString.TypedValue.IndexOf(prefix.TypedValue, from.TypedValue - 1) == from.TypedValue - 1);
		}
		#endregion Searching

		#region Substring functions
		/// <summary>
		/// Returns a string containing a number of size characters of the right side of the 
		/// aString string
		/// </summary>		
		public static ONString rightSubstring (ONString aString, ONInt size)
		{
			if (aString == null || size == null)
				return ONString.Null;

			return new ONString(aString.TypedValue.Substring(aString.TypedValue.Length - size.TypedValue));
		}
		/// <summary>
		/// Returns a string containing a number of size characters of the left side of the 
		/// aString string
		/// </summary>			
		public static ONString leftSubstring (ONString aString, ONInt size)
		{
			if (aString == null || size == null)
				return ONString.Null;

			return new ONString(aString.TypedValue.Substring(0, size.TypedValue));
		}
		/// <summary>
		/// Returns a copy of a substring of the aString string. This copy starts at the from 
		/// position of the aString string and copy an amount of size characters
		/// </summary>				
		public static ONString substring (ONString aString, ONInt from, ONInt size)
		{
			if (aString == null || from == null || size == null)
				return ONString.Null;

			return new ONString(aString.TypedValue.Substring(from.TypedValue - 1, size.TypedValue));
		}
		/// <summary>
		/// Returns a new string which is a copy of aString string without its leading spaces
		/// </summary>		
		public static ONString leftTrim (ONString aString)
		{
			if (aString == null)
				return ONString.Null;

			return new ONString(aString.TypedValue.TrimStart(new char[] {' '}));
		}
		/// <summary>
		/// Returns a new string which is a copy of aString string without its trailing spaces
		/// </summary>		
		public static ONString rightTrim (ONString aString)
		{
			if (aString == null)
				return ONString.Null;

			return new ONString(aString.TypedValue.TrimEnd(new char[] {' '}));
		}
		/// <summary>
		/// Returns a new string which is a copy of aString string without its both leading and
		/// trailing spaces
		/// </summary>				
		public static ONString strTrim (ONString aString)
		{
			if (aString == null)
				return ONString.Null;

			return new ONString(aString.TypedValue.Trim());
		}
		/// <summary>
		/// Returns a copy of the aString string but with all occurrences of the subStr substring 
		/// specified replaced by the other string
		/// </summary>				
		public static ONString strReplace (ONString aString, ONString subStr, ONString other)
		{
			if (aString == null || subStr == null || other == null)
				return ONString.Null;

			return new ONString(aString.TypedValue.Replace(subStr.TypedValue, other.TypedValue));
		}
		/// <summary>
		/// Returns a copy of the aString string but with all occurrences of the subStr substring 
		/// specified replaced by the other string, starting from the from position
		/// </summary>		
		public static ONString replaceFrom (ONString aString, ONString subStr, ONString other, ONInt from)
		{
			if (aString == null || subStr == null || other == null || from == null)
				return ONString.Null;

			string lAString = aString.TypedValue;
			string lSubStr = subStr.TypedValue;
			string lOther = other.TypedValue;
			int lFrom = from.TypedValue;

			System.Text.StringBuilder lBuffer = new System.Text.StringBuilder();
			int lOldPos = lFrom - 1;
			lBuffer.Append(lAString.Substring(0, lFrom - 1));
			int pos = lAString.IndexOf(lSubStr, lFrom - 1);
			for (; pos >= 0; pos = lAString.IndexOf(lSubStr, lOldPos))
			{
				lBuffer.Append(lAString.Substring(lOldPos, pos - lOldPos));
				lBuffer.Append(lOther);
				lOldPos = pos + lSubStr.Length;
			}
			lBuffer.Append(lAString.Substring(lOldPos));
			return new ONString(lBuffer.ToString());
		}
		/// <summary>
		/// Returns a copy of the aString string with a number of times occurrences of the 
		/// subStr substring specified replaced by the other string, starting from the from position
		/// </summary>			
		public static ONString replaceTimes (ONString aString, ONString subStr, ONString other, ONInt from, ONInt times)
		{
			if (aString == null || subStr == null || other == null || from == null || times == null)
				return ONString.Null;

			string lAString = aString.TypedValue;
			string lSubStr = subStr.TypedValue;
			string lOther = other.TypedValue;
			int lFrom = from.TypedValue;
			int lTimes = times.TypedValue;

			System.Text.StringBuilder lBuffer = new System.Text.StringBuilder();
			int lOldPos = lFrom - 1;
			lBuffer.Append(lAString.Substring(0, lFrom - 1));
			int pos = lAString.IndexOf(lSubStr, lFrom - 1); 
			for (; pos >= 0 && lTimes > 0; pos = lAString.IndexOf(lSubStr, lOldPos), lTimes--)
			{
				lBuffer.Append(lAString.Substring(lOldPos, pos - lOldPos));
				lBuffer.Append(lOther);
				lOldPos = pos + lSubStr.Length;
			}
			lBuffer.Append(lAString.Substring(lOldPos));
			return new ONString(lBuffer.ToString());
		}
		/// <summary>
		/// Concatenates the secondStr string at the end of the firstStr string
		/// </summary>		
		public static ONString concat (ONString firstStr, ONString secondStr)
		{
			return new ONString(System.String.Concat(firstStr.TypedValue, secondStr.TypedValue));
		}
		/// <summary>
		/// Returns a new String which is a copy of aString without the region defined by the 
		/// start position specified with the specified length
		/// </summary>
		public static ONString strDelete (ONString aString, ONInt start, ONInt length)
		{
			if (aString == null || start == null || length == null)
				return ONString.Null;

			string lAString = aString.TypedValue;
			int lStart = start.TypedValue;
			int lLength = length.TypedValue;

			return new ONString(lAString.Remove(lStart - 1, lLength));
		}
		/// <summary>
		/// Returns a copy of the aString string with a new string (insertion) inserted at the 
		/// index position
		/// </summary>			
		public static ONString strInsert (ONString aString, ONInt index, ONString insertion)
		{
			if (aString == null || index == null || insertion == null)
				return ONString.Null;

			return new ONString(aString.TypedValue.Insert(index.TypedValue - 1, insertion.TypedValue));
		}
		#endregion Substring functions

		#region Case

		/// <summary>
		/// Return a new String with all its characters in their Uppercase variant
		/// </summary>			
		public static ONString upperCase (ONString aString)
		{
			if (aString == null)
				return ONString.Null;

			return new ONString(aString.TypedValue.ToUpper());
		}

		/// <summary>
		/// Return a new String with all its characters in their Lowercase variant
		/// </summary>		
		public static ONString lowerCase (ONString aString)
		{
			if (aString == null)
				return ONString.Null;

			return new ONString(aString.TypedValue.ToLower());
		}
	
		#endregion Case

		#region Simple Queries

		/// <summary>
		/// Returns the size of the aString string
		/// </summary>	
		public static ONInt length (ONString aString)
		{
			if (aString == null)
				return ONInt.Null;

			return new ONInt(aString.TypedValue.Length);
		}
	
		/// <summary>
		/// Returns a new Sting, copy of the aString string with its characters in reverse order
		/// </summary>			
		public static ONString reverse (ONString aString)
		{
			if (aString == null)
				return ONString.Null;

			System.Text.StringBuilder buffer = new System.Text.StringBuilder();
			for (int i = aString.TypedValue.Length - 1; i >= 0; i--)
				buffer.Append(aString.TypedValue[i]);
			return new ONString(buffer.ToString());
		}
	
		#endregion Simple Queries

		#region Comparison
	
		/// <summary>
		/// Compare both strings: firstStr and secondStr lexicographically. Returns a negative 
		/// value when firstStr appears before secondStr, 0 if they are equals, or a positive value if firstStr  follows secondStr
		/// </summary>			
		public static ONInt strCompare (ONString firstStr, ONString secondStr)
		{
			if (firstStr == null || secondStr == null)
				return ONInt.Null;

			return new ONInt(firstStr.TypedValue.CompareTo(secondStr.TypedValue));
		}
	
		/// <summary>
		/// Compares both strings lexicographically ignoring case considerations. It returns 
		/// a negative value when the firstStr string appears before the secondStr in the lexical order, 0 if they are equals, or a positive value if the first string follows the second String
		/// </summary>			
		public static ONInt strCompareIgnoreCase (ONString firstStr, ONString secondStr)
		{
			if (firstStr == null || secondStr == null)
				return ONInt.Null;

			return new ONInt(String.Compare(firstStr.TypedValue, secondStr.TypedValue, true));
		}
	
		/// <summary>
		/// Compares two regions defined by the arguments of this method: The first string 
		/// region is defined by the substring of the aString string starting at the from position 
		/// with a length size, the second one is a substring which starts at the otherfrom 
		/// position of the other string with the length size. If both regions are equals 
		/// (depending on the ignoreCase parameter) this method returns true, if not, or if one
		///  of the from or otherfrom arguments is negative, or some of the regions exceeds the
		///   end of the string, this method returns false
		/// </summary>				
		public static ONBool strRegionMatches (ONString aString, ONBool ignoreCase, ONInt from, ONString other, ONInt otherFrom, ONInt length)
		{
			if (aString == null || ignoreCase == null || from == null || other == null || otherFrom == null || length == null)
				return ONBool.Null;

			string lAString = aString.TypedValue;
			int lFrom = from.TypedValue;
			int lOtherFrom = otherFrom.TypedValue;
			string lOther = other.TypedValue;
			int lLength = length.TypedValue;

			if (lFrom < 0 || lOtherFrom < 0 || lFrom + lLength > lAString.Length || 
				lOtherFrom + lLength > lOther.Length)
				return new ONBool(false);
			if (ignoreCase.TypedValue)
			{
				lAString = lAString.ToUpper();
				lOther = lOther.ToUpper();
			}
			return new ONBool(lAString.Substring(lFrom, lLength).Equals(lOther.Substring(lOtherFrom, lLength)));
		}
	
		/// <summary>
		/// Return true if the string firstStr comes before the string secondStr in lexicographic order
		/// </summary>				
		public static ONBool strLesserThan (ONString firstStr, ONString secondStr)
		{
			if (firstStr == null || secondStr == null)
				return ONBool.Null;

			return new ONBool((string.Compare(firstStr.TypedValue, secondStr.TypedValue) < 0));
		}
	
		/// <summary>
		/// Return true if the string firstStr comes after the string secondStr in lexicographic order
		/// </summary>			
		public static ONBool strGreaterThan (ONString firstStr, ONString secondStr)
		{
			if (firstStr == null || secondStr == null)
				return ONBool.Null;

			return new ONBool(string.Compare(firstStr.TypedValue, secondStr.TypedValue) > 0);
		}
	
		/// <summary>
		/// Return true if the string firstStr and the string secondStr are lexicographically equals
		/// </summary>		
		public static ONBool strSameAs (ONString firstStr, ONString secondStr)
		{
			if (firstStr == null || secondStr == null)
				return ONBool.Null;

			return new ONBool((string.Compare(firstStr.TypedValue, secondStr.TypedValue) == 0));
		}
	
		#endregion Comparison

		#region Conversions

		/// <summary>
		/// Returns the string representation of value
		/// </summary>			
		public static ONString textToStr(ONText value)
		{
			if (value == null)
				return ONString.Null;

			return new ONString(value.TypedValue);
		}

		#endregion Conversions
		#endregion String functions
	}
}


