// v3.8.4.5.b
using System;

namespace SIGEM.Client.StandardFunctions
{
	/// <summary>
	/// Class that contains date and time functions.
	/// </summary>
	public class DateFunctions
	{

		#region DateFunctions

		#region Query

		/// <summary>
		/// Returns the date of the system
		/// </summary>
		public static DateTime? SystemDate()
		{
			return DateTime.Today;
		}

		/// <summary>
		/// Returns true if and only if year is a leap year; false in other case
		/// </summary>
		public static bool IsLeapYear(int? year)
		{
			int lyear;
			if (year.HasValue)
			{
				lyear = (int)year;
			}
			else
			{
				return false;
			}
			return DateTime.IsLeapYear(lyear);
		}

		/// <summary>
		/// Returns the days of difference between date1 and date2
		/// </summary>
		public static int? DaysDifferenceFromDate(DateTime? date1, DateTime? date2)
		{
			//Check if date1 and date2 are null.
			if (date1 == null || date2 == null)
			{
				return null;
			}
			if (date1 > date2)
			{
				return 0;
			}

			DateTime ldate1 = (DateTime)date1;
			DateTime ldate2 = (DateTime)date2;

			return ldate2.Subtract(ldate1).Days;
		}

		/// <summary>
		/// Returns the number of days in a specified month of the specified year
		/// For example, if month equals 2 for February, the return value is 28 or 29 depending
		/// upon whether year is a leap year.
		/// </summary>
		public static int? DaysInMonth(int? year, int? month)
		{
			int lyear, lmonth;

			if (year.HasValue && month.HasValue)
			{
				lyear = (int)year;
				lmonth = (int)month;
			}
			else
			{
				return null;
			}
			return DateTime.DaysInMonth(lyear, lmonth);
		}

		/// <summary>
		/// Returns the day in adate
		/// </summary>
		public static int? GetDay(DateTime? adate)
		{
			DateTime ladate;

			if (adate.HasValue)
			{
				ladate = (DateTime)adate;
			}
			else
			{
				return null;
			}
			return ladate.Day;
		}

		/// <summary>
		/// Returns the month in adate
		/// </summary>
		public static int? GetMonth(DateTime? adate)
		{
			DateTime ladate;
			if (adate.HasValue)
			{
				ladate = (DateTime)adate;
			}
			else
			{
				return null;
			}
			return ladate.Month;
		}

		/// <summary>
		/// Returns the year in adate
		/// </summary>
		public static int? GetYear(DateTime? adate)
		{
			DateTime ladate;
			if (adate.HasValue)
			{
				ladate = (DateTime)adate;
			}
			else
			{
				return null;
			}
			return ladate.Year;
		}

		/// <summary>
		/// Returns the day of week contained in adate.The possible values are 1 through 7
		/// </summary>
		public static int? GetDayOfWeek(DateTime? adate)
		{
			DateTime ladate;
			if (adate.HasValue)
			{
				ladate = (DateTime)adate;
			}
			else
			{
				return null;
			}

			return (int)ladate.DayOfWeek + 1;
		}

		/// <summary>
		/// Returns the day of year contained in adate. The possible values are either 1 through 365
		/// (if the year is not leap) or 1 through 366.
		/// </summary>
		public static int? GetDayOfYear(DateTime? adate)
		{
			DateTime ladate;
			if (adate.HasValue)
			{
				ladate = (DateTime)adate;
			}
			else
			{
				return null;
			}
			return (int)ladate.DayOfYear;
		}

		/// <summary>
		/// Counts the number of appearances of the day of the week between two dates
		/// </summary>
		public static int? GetNumDayOfWeekBetweenDates(int? dayofweek, DateTime? inidate, DateTime? enddate)
		{
			if (dayofweek == null || inidate == null || enddate == null)
			{
				return null;
			}
			if (inidate > enddate)
			{
				return 0;
			}

			int numTotal = (int)DaysDifferenceFromDate(inidate, enddate);
			int numDays = numTotal / 7;
			int diffDays = numTotal % 7;
			int iniDay = (int)GetDayOfWeek(inidate);
			int endDay = (int)GetDayOfWeek(enddate);

			if (endDay < iniDay)
			{
				endDay += 7;
			}

			if (dayofweek < iniDay)
			{
				dayofweek += 7;
			}

			int iniRest = endDay - diffDays;
			if ((dayofweek >= iniRest) && (dayofweek <= endDay))
			{
				numDays++;
			}

			return (int?)numDays;
		}

		/// <summary>
		/// Returns a date corresponding to the next day of the argument adate
		/// </summary>
		public static DateTime? Tomorrow(DateTime? adate)
		{
			DateTime ladate;
			if (adate.HasValue)
			{
				ladate = (DateTime)adate;
			}
			else
			{
				return null;
			}
			return ladate.AddDays(1);
		}

		/// <summary>
		/// Returns a date corresponding to the earlier day of the argument adate
		/// </summary>
		public static DateTime? Yesterday(DateTime? adate)
		{
			DateTime ladate;
			if (adate.HasValue)
			{
				ladate = (DateTime)adate;
			}
			else
			{
				return null;
			}
			return ladate.AddDays(-1);
		}

		/// <summary>
		/// Returns a date to which a specified time interval has been added.
		///   interval : String indicating the interval of time you want to add, and can be:
		///	   "yyyy" -> indicates that the value specified in number are years;
		///	   "m" -> indicates that the value specified in number are months;
		///	   "d" -> indicates that the value specified in number are days;
		///   number : The number of interval you want to add.
		///   adate : The date to which a specified time interval has been added.
		/// </summary>
		public static DateTime? DateAdd(string interval, int? number, DateTime? adate)
		{
			DateTime ladate;
			int lnumber;
			if (adate.HasValue && number.HasValue)
			{
				ladate = (DateTime)adate;
				lnumber = (int)number;
			}
			else
			{
				return adate;
			}
			switch (interval)
			{
				case "yyyy":
					return ladate.AddYears((int)lnumber);
				case "m":
					return ladate.AddMonths((int)lnumber);
				case "d":
					return ladate.AddDays(lnumber);
				default :
					return ladate;
			}
		}

		#endregion Query

		#region Conversions

		/// <summary>
		/// Returns a string representation of the argument adate.
		/// Converts adate to a string of the form: m/d/yy
		/// </summary>
		public static String ToShortDateFormat(DateTime? adate)
		{
			DateTime ladate;
			if (adate.HasValue)
			{
				ladate = (DateTime)adate;
			}
			else
			{
				return null;
			}
			return ladate.ToShortDateString();
		}

		/// <summary>
		/// Returns and string representation of the argument adate.
		/// Converts adate to a string of the form: mon dd, yyyy
		/// </summary>
		public static String ToMediumDateFormat(DateTime? adate)
		{
			DateTime ladate;
			if (adate.HasValue)
			{
				ladate = (DateTime)adate;
			}
			else
			{
				return null;
			}
			return ladate.ToString("MMMM dd, yyyy");
		}

		/// <summary>
		/// Returns and string representation of the argument adate.
		/// Converts adate to a string of the form: mmmm dd, yyyyFor instance: March 18, 2002
		/// </summary>
		public static String ToLongDateFormat(DateTime? adate)
		{
			DateTime ladate;
			if (adate.HasValue)
			{
				ladate = (DateTime)adate;
			}
			else
			{
				return null;
			}
			return ladate.ToLongDateString();
		}

		/// <summary>
		/// Returns and string representation of the argument adate
		/// Converts adate to a string of the form: dow, mon dd, yyyy
		/// Where:
		/// dow is the day of the week (Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday).
		/// mon is the month (Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec),
		/// dd is the day of the month (01 through 31) as two decimal digits,
		/// yyyy is the year, as four decimal digits
		/// </summary>
		public static String ToFullDateFormat(DateTime? adate)
		{
			DateTime ladate;
			if (adate.HasValue)
			{
				ladate = (DateTime)adate;
			}
			else
			{
				return null;
			}
			return ladate.ToString("dddd, MMMM dd, yyyy");
		}

		/// <summary>
		/// Converts the string stringDate to a date representation. stringDate must be a short formatted string
		/// Converts adate to a string of the form: mmmm dd, yyyyFor instance: March 18, 2002
		/// </summary>
		public static DateTime? StringToDate(String stringDate)
		{
			return System.DateTime.Parse(stringDate);
		}

		/// <summary>
		/// Converts the arguments year, month and day to a date representation.
		///   year: Must be a positive value;
		///   month: Must be in the range 1..12;
		///   day: Must be in the range 1..31;
		/// </summary>
		public static DateTime? FormatToDate(int? year, int? month, int? day)
		{
			int lyear, lmonth, lday;
			if (year.HasValue && month.HasValue && day.HasValue)
			{
				lyear = (int)year;
				lmonth = (int)month;
				lday = (int)day;
			}
			else
			{
				return null;
			}
			return new DateTime(lyear, lmonth, lday);
		}

		#endregion Conversions

		#region Comparison

		/// <summary>
		/// Tests if adate1 is after the specified date adate2.
		/// Return: true if and only if adate1 is strictly later than the date represented by adate2.
		/// </summary>
		public static bool DateAfter(DateTime? adate1, DateTime? adate2)
		{
			DateTime ladate1, ladate2;
			if (adate1.HasValue && adate2.HasValue)
			{
				ladate1 = (DateTime)adate1;
				ladate2 = (DateTime)adate2;
			}
			else
			{
				return false;
			}
			return (DateTime)ladate1 > (DateTime)ladate2;
		}

		/// <summary>
		/// Tests if adate1 is before the specified date adate2.
		/// Return: true if and only if adate1 is strictly earlier than the date represented by adate2.
		/// </summary>
		public static bool DateBefore(DateTime? adate1, DateTime? adate2)
		{
			DateTime ladate1, ladate2;
			if (adate1.HasValue && adate2.HasValue)
			{
				ladate1 = (DateTime)adate1;
				ladate2 = (DateTime)adate2;
			}
			else
			{
				return false;
			}
			return (DateTime)ladate1 < (DateTime)ladate2;
		}

		/// <summary>
		/// Compares two dates for equality.
		/// The result is true if and only if the argument adate1 represents the same date as adate2.
		/// </summary>
		public static bool DateEquals(DateTime? adate1, DateTime? adate2)
		{
			return adate1.Equals(adate2);
		}

		#endregion Comparison

		#endregion DateFunctions

		#region DateTime functions

		#region Query

		/// <summary>
		/// Returns the date and time of the system
		/// </summary>
		public static DateTime? SystemDateTime()
		{
			return DateTime.Now;
		}

		/// <summary>
		/// This function extracts the information related to the date part of adatetime (year, month, day), discarding the time related information (hour, minute, second)
		/// </summary>
		public static DateTime? GetDatePart(DateTime? adatetime)
		{
			DateTime ladatetime;
			if (adatetime.HasValue)
			{
				ladatetime = (DateTime)adatetime;
			}
			else
			{
				return null;
			}
			return ladatetime.Date;
		}

		/// <summary>
		/// This function extracts the information related to the time part of adatetime (hour, minute, second), discarding the date related information (year, month, day)
		/// </summary>
		public static TimeSpan? GetTimePart(DateTime? adatetime)
		{
			DateTime ladatetime;
			if (adatetime.HasValue)
			{
				ladatetime = (DateTime)adatetime;
			}
			else
			{
				return null;
			}
			return new TimeSpan(ladatetime.Hour, ladatetime.Minute, ladatetime.Second);
		}

		/// <summary>
		/// Returns a datetime to which a specified time interval has been added.Interval : The interval of time you want to add.
		///   interval : String indicating the interval of time you want to add, and can be:
		///	   "yyyy" -> indicates that the value specified in number are years;
		///	   "m" -> indicates that the value specified in number are months;
		///	   "d" -> indicates that the value specified in number are days;
		///	   "h" -> indicates that the value specified in number are hours;
		///	   "n" -> indicates that the value specified in number are minutes;
		///	   "s" -> indicates that the value specified in number are seconds;
		///   number : The number of interval you want to add.
		///   adatetime : The date to which a specified time interval has been added.
		/// </summary>
		public static DateTime? DateTimeAdd(string interval, int? number, DateTime? adatetime)
		{
			DateTime ladatetime;
			int lnumber;
			if (adatetime.HasValue && number.HasValue)
			{
				ladatetime = (DateTime)adatetime;
				lnumber = (int)number;
			}
			else
			{
				return adatetime;
			}
			switch (interval)
			{
				case "yyyy":
					return ladatetime.AddYears(lnumber);
				case "m":
					return ladatetime.AddMonths(lnumber);
				case "d":
					return ladatetime.AddDays(lnumber);
				case "h":
					return ladatetime.AddHours(lnumber);
				case "n":
					return ladatetime.AddMinutes(lnumber);
				case "s":
					return ladatetime.AddSeconds(lnumber);
				default :
					return ladatetime;
			}
		}

		/// <summary>
		/// Returns the days of difference between datetime1 and datetime2
		/// </summary>
		public static int? DaysDifferenceFromDateTime(DateTime? datetime1, DateTime? datetime2)
		{
			//Check if datetime1 and datetime2 are null.
			if (datetime1 == null || datetime2 == null)
			{
				return null;
			}
			if (datetime1 > datetime2)
			{
				return 0;
			}

			DateTime ldatetime1 = (DateTime)datetime1;
			DateTime ldatetime2 = (DateTime)datetime2;

			return ldatetime2.Subtract(ldatetime1).Days;
		}

		#endregion Query

		#region Conversions

		/// <summary>
		/// Converts the arguments adate and atime to a datetime representation.
		/// </summary>
		public static DateTime? ToDateTime(DateTime? adate, TimeSpan? atime)
		{
			DateTime ladate;
			TimeSpan latime;
			if (adate.HasValue && atime.HasValue)
			{
				ladate = (DateTime)adate;
				latime = (TimeSpan)atime;
			}
			else
			{
				return null;
			}
			return new DateTime(ladate.Year, ladate.Month, ladate.Day, latime.Hours, latime.Minutes, latime.Seconds);
		}

		/// <summary>
		/// Converts adatetime to a string of the form: dow mon dd hh:mm:ss zzz yyyy
		/// </summary>
		public static string DateTimeToString(DateTime? adatetime)
		{
			DateTime ladatetime;
			if (adatetime.HasValue)
			{
				ladatetime = (DateTime)adatetime;
			}
			else
			{
				return null;
			}
			return ladatetime.ToString("dddd MMMM dd, hh:mm:ss zzz yyyy");
		}

		/// <summary>
		/// Converts the string stringDate to a datetime representation
		/// </summary>
		public static DateTime? StringToDateTime(string stringDate)
		{
			return System.DateTime.Parse(stringDate);
		}

		/// <summary>
		/// Converts the arguments (year, month, day, hour, minute and second) to a datetime representation.
		/// </summary>
		/// <param name="year">Must be a positive value</param>
		/// <param name="month">Must be in the range 1..12</param>
		/// <param name="day">Must be in the range 1..31</param>
		/// <param name="hour">Must be in the range 0..23</param>
		/// <param name="minute">Must be in the range 0..59</param>
		/// <param name="second">Must be in the range 0..59</param>
		/// <returns></returns>
		public static DateTime? FormatToDateTime(int? year, int? month, int? day, int? hour, int? minute, int? second)
		{
			int lyear, lmonth, lday, lhour, lminute, lsecond;
			if (year.HasValue && month.HasValue && day.HasValue && hour.HasValue && minute.HasValue && second.HasValue)
			{
				lyear = (int)year;
				lmonth = (int)month;
				lday = (int)day;
				lhour = (int)hour;
				lminute = (int)minute;
				lsecond = (int)second;
			}
			else
			{
				return null;
			}
			return new DateTime(lyear, lmonth, lday, lhour, lminute, lsecond);
		}

		#endregion Conversions

		#region Comparison

		/// <summary>
		/// Tests if adatetime1 is after the specified datetime adatetime2
		/// </summary>
		/// <returns>true if and only if adatetime1 is strictly later than the datetime represented by adatetime2</returns>
		public static bool DatetimeAfter(DateTime? adatetime1, DateTime? adatetime2)
		{
			DateTime ladatetime1, ladatetime2;
			if (adatetime1.HasValue && adatetime2.HasValue)
			{
				ladatetime1 = (DateTime)adatetime1;
				ladatetime2 = (DateTime)adatetime2;
			}
			else
			{
				return false;
			}
			return (DateTime)ladatetime1 > (DateTime)ladatetime2;
		}

		/// <summary>
		/// Tests if adatetime1 is before the specified datetime adatetime2
		/// </summary>
		/// <returns>true if and only if adatetime1 is strictly earlier than the datetime represented by adatetime2</returns>
		public static bool DatetimeBefore(DateTime? adatetime1, DateTime? adatetime2)
		{
			DateTime ladatetime1, ladatetime2;
			if (adatetime1.HasValue && adatetime2.HasValue)
			{
				ladatetime1 = (DateTime)adatetime1;
				ladatetime2 = (DateTime)adatetime2;
			}
			else
			{
				return false;
			}
			return (DateTime)ladatetime1 < (DateTime)ladatetime2;
		}

		/// <summary>
		/// Compare two datetimes for equality
		/// </summary>
		/// <returns>true if and only if the argument adatetime1 represents the same datetime as adatetime2</returns>
		public static bool DatetimeEquals(DateTime? adatetime1, DateTime? adatetime2)
		{
			DateTime ladatetime1, ladatetime2;
			if (adatetime1.HasValue && adatetime2.HasValue)
			{
				ladatetime1 = (DateTime)adatetime1;
				ladatetime2 = (DateTime)adatetime2;
			}
			else
			{
				return false;
			}
			return ladatetime1.Equals(ladatetime2);
		}

		#endregion Comparison

		#endregion DateTime Functions

		#region Time Functions

		#region Query

		/// <summary>
		/// Returns the time of the system
		/// </summary>
		public static TimeSpan? SystemTime()
		{
			DateTime lTime = System.DateTime.Now;
			return new TimeSpan(lTime.Hour, lTime.Minute, lTime.Second);
		}

		/// <summary>
		/// Returns the hour of atime
		/// </summary>
		public static int? GetHour(TimeSpan? atime)
		{
			TimeSpan latime;
			if (atime.HasValue)
			{
				latime = (TimeSpan)atime;
			}
			else
			{
				return null;
			}
			return latime.Hours;
		}

		/// <summary>
		/// Returns the minutes of atime
		/// </summary>
		public static int? GetMinute(TimeSpan? atime)
		{
			TimeSpan latime;
			if (atime.HasValue)
			{
				latime = (TimeSpan)atime;
			}
			else
			{
				return null;
			}
			return latime.Minutes;
		}

		/// <summary>
		/// Returns the seconds of atime
		/// </summary>
		public static int? GetSecond(TimeSpan? atime)
		{
			TimeSpan latime;
			if (atime.HasValue)
			{
				latime = (TimeSpan)atime;
			}
			else
			{
				return null;
			}
			return latime.Seconds;
		}

		/// <summary>
		/// Returns a time to which a specified time interval has been added.
		///   interval : String indicating the interval of time you want to add, and can be:
		///	   "h" -> indicates that the value specified in number are hours;
		///	   "n" -> indicates that the value specified in number are minutes;
		///	   "s" -> indicates that the value specified in number are seconds;
		///   number : The number of interval you want to add.
		///   atime : The time to which a specified time interval has been added.
		/// </summary>
		public static TimeSpan? TimeAdd(string interval, int? number, TimeSpan? atime)
		{
			TimeSpan latime;
			int lnumber;
			if (atime.HasValue && number.HasValue)
			{
				latime = (TimeSpan)atime;
				lnumber = (int)number;
			}
			else
			{
				return atime;
			}
			switch (interval)
			{
				case "h":
					return latime.Add(new TimeSpan(lnumber, 0, 0));
				case "n":
					return latime.Add(new TimeSpan(0, lnumber, 0));
				case "s":
					return latime.Add(new TimeSpan(0, 0, lnumber));
				default :
					return atime;
			}
		}

		#endregion Query

		#region Conversions

		/// <summary>
		/// Convert the arguments (hour, minute and second) to a time representation
		/// </summary>
		public static TimeSpan? FormatToTime(int? hour, int? minute, int? second)
		{
			int lhour, lminute, lsecond;
			if (hour.HasValue && minute.HasValue && second.HasValue)
			{
				lhour = (int)hour;
				lminute = (int)minute;
				lsecond = (int)second;
			}
			else
			{
				return null;
			}
			return new TimeSpan(lhour, lminute, lsecond);
		}

		/// <summary>
		/// Converts the string stringTime to a time representation
		/// </summary>
		public static TimeSpan? StringToTime(string stringTime)
		{
			return System.TimeSpan.Parse(stringTime);
		}

		/// <summary>
		/// Returns a string representation of the argument atime
		/// </summary>
		public static string TimeToString(TimeSpan? atime)
		{
			TimeSpan latime;
			if (atime.HasValue)
			{
				latime = (TimeSpan)atime;
			}
			else
			{
				return null;
			}
			return latime.ToString();
		}

		#endregion Conversions

		#region Comparison

		/// <summary>
		/// Tests if atime1 is after the specified time atime2.
		/// </summary>
		/// <param name="atime1"></param>
		/// <param name="atime2"></param>
		/// <returns>true if and only if atime1 is strictly later than the time represented by atime2</returns>
		public static bool TimeAfter(TimeSpan? atime1, TimeSpan? atime2)
		{
			TimeSpan latime1, latime2;
			if (atime1.HasValue && atime2.HasValue)
			{
				latime1 = (TimeSpan)atime1;
				latime2 = (TimeSpan)atime2;
			}
			else
			{
				return false;
			}
			return  (TimeSpan)latime1 > (TimeSpan)latime2;
		}

		/// <summary>
		/// Tests if atime1 is before the specified time atime2
		/// </summary>
		/// <returns>true if and only if atime1 is strictly earlier than the time represented by atime2</returns>
		public static bool TimeBefore(TimeSpan? atime1, TimeSpan? atime2)
		{
			TimeSpan latime1, latime2;
			if (atime1.HasValue && atime2.HasValue)
			{
				latime1 = (TimeSpan)atime1;
				latime2 = (TimeSpan)atime2;
			}
			else
			{
				return false;
			}
			return (TimeSpan)latime1 < (TimeSpan)latime2;
		}

		/// <summary>
		/// Compares two times for equality
		/// </summary>
		/// <param name="atime1"></param>
		/// <param name="atime2"></param>
		/// <returns>true if and only if the argument atime1 represents the same time as atime2</returns>
		public static bool TimeEquals(TimeSpan? atime1, TimeSpan? atime2)
		{
			return atime1.Equals(atime2);
		}

		#endregion Comparison

		#endregion Time Functions
	}
}

