// v3.8.4.5.b
using System;

namespace SIGEM.Client.StandardFunctions
{
	public class StringFunctions
	{

		#region StringFunctions

		#region Searching

		/// <summary>
		/// Returns the first position of the substring specified by the argument subStr if it is
		/// found as part of the aString string. It returns 0 otherwise
		/// </summary>
		public static int? IndexOf(string aString, string subStr)
		{
			if (aString != null)
			{
				return aString.IndexOf(subStr) + 1;
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Search the first occurrence of the substring specified by the argument subStr in the
		/// aString string after the position specified by the argument from, if the substring
		/// is found, its position in the string is returned. It returns 0 otherwise
		/// </summary>
		public static int? IndexOfFrom(string aString, string subStr, int? from)
		{
			int lfrom;
			if (from.HasValue)
			{
				lfrom = (int)from;
			}
			else
			{
				return 0;
			}
			if (aString != null)
			{
				return aString.IndexOf(subStr, ((int)lfrom) - 1) + 1;
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Returns the last position of the substring specified by the argument subStr if it is
		/// found as part of the aString string, otherwise it returns 0
		/// </summary>
		public static int? LastIndexOf(string aString, string subStr)
		{
			if (aString != null)
			{
				return aString.LastIndexOf(subStr) + 1;
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Search the rightmost occurrence of the substring specified by the argument subStr
		/// in the aString string before the position specified by the argument ending, if the
		/// substring is found it position in the string is returned, otherwise it returns 0
		/// </summary>
		public static int? LastIndexOfFrom(string aString, string subStr, int? ending)
		{
			int lending;
			if (ending.HasValue)
			{
				lending = (int)ending;
			}
			else
			{
				return 0;
			}
			if (aString != null)
			{
				return aString.LastIndexOf(subStr, ((int)lending) - 1) + 1;
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Returns true if aString string starts with the substring specified by the argument
		/// prefix, false otherwise
		/// </summary>
		public static bool StrStartsWith(string aString, string prefix)
		{
			if (aString != null)
			{
				return aString.StartsWith(prefix);
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Returns true if aString string ends with the substring specified by the argument
		/// sufix, false otherwise
		/// </summary>
		public static bool StrEndsWith(string aString, string sufix)
		{
			if (aString != null)
			{
				return aString.EndsWith(sufix);
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Returns true if the substring that begins in the from position of the aString string,
		/// starts with the substring specified by the argument prefix, false otherwise
		/// </summary>
		public static bool StrStartsWithFrom(string aString, string prefix, int? from)
		{
			int lfrom;
			if (from.HasValue)
			{
				lfrom = (int)from;
			}
			else
			{
				return false;
			}
			if (aString != null)
			{
				return aString.IndexOf(prefix, (int)lfrom) == 0;
			}
			else
			{
				return false;
			}
		}

		#endregion Searching

		#region Substring functions

		/// <summary>
		/// Returns a string containing a number of size characters of the right side of the
		/// aString string
		/// </summary>
		public static string RightSubstring(string aString, int? size)
		{
			int lsize;
			if (size.HasValue)
			{
				lsize = ((int)size >= aString.Length) ? aString.Length : (int)size;
			}
			else
			{
				return null;
			}
			if (aString != null)
			{
				return aString.Substring(aString.Length - (int)lsize);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Returns a string containing a number of size characters of the left side of the
		/// aString string
		/// </summary>
		public static string LeftSubstring(string aString, int? size)
		{
			int lsize;
			if (size.HasValue)
			{
				lsize = ((int)size >= aString.Length) ? aString.Length : (int)size;
			}
			else
			{
				return null;
			}
			if (aString != null)
			{
				return aString.Substring(0, (int)lsize);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Returns a copy of a substring of the aString string. This copy starts at the from
		/// position of the aString string and copy an amount of size characters
		/// </summary>
		public static string Substring(string aString, int? from, int? size)
		{
			int lfrom, lsize;
			if (size.HasValue && from.HasValue)
			{
				lsize = (int)size;
				lfrom = (int)from;
			}
			else
			{
				return null;
			}
			if (aString != null)
			{
				return aString.Substring(((int)lfrom) - 1, (int)lsize);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Returns a new string which is a copy of aString string without its leading spaces
		/// </summary>
		public static string LeftTrim(string aString)
		{
			if (aString != null)
			{
				return aString.TrimStart(new char[] {' '});
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Returns a new string which is a copy of aString string without its trailing spaces
		/// </summary>
		public static string RightTrim(string aString)
		{
			if (aString != null)
			{
				return aString.TrimEnd(new char[] {' '});
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Returns a new string which is a copy of aString string without its both leading and
		/// trailing spaces
		/// </summary>
		public static string StrTrim(string aString)
		{
			if (aString != null)
			{
				return aString.Trim();
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Returns a copy of the aString string but with all occurrences of the subStr substring
		/// specified replaced by the other string
		/// </summary>
		public static string StrReplace(string aString, string subStr, string other)
		{
			if (aString != null)
			{
				return aString.Replace(subStr, other);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Returns a copy of the aString string but with all occurrences of the subStr substring
		/// specified replaced by the other string, starting from the from position
		/// </summary>
		public static string ReplaceFrom(string aString, string subStr, string other, int? from)
		{
			int lfrom;
			if (from.HasValue)
			{
				lfrom = (int)from;
			}
			else
			{
				return null;
			}
			if (aString != null)
			{
				System.Text.StringBuilder lBuffer = new System.Text.StringBuilder();
				int lOldPos = (int)lfrom - 1;
				lBuffer.Append(aString.Substring(0, ((int)lfrom) - 1));
				int pos = aString.IndexOf(subStr, ((int)lfrom) - 1);
				for (; pos >= 0; pos = aString.IndexOf(subStr, lOldPos))
				{
					lBuffer.Append(aString.Substring(lOldPos, pos - lOldPos));
					lBuffer.Append(other);
					lOldPos = pos + subStr.Length;
				}
				lBuffer.Append(aString.Substring(lOldPos));
				return lBuffer.ToString();
			}
			else
			{
			 	return null;
			}
		}

		/// <summary>
		/// Returns a copy of the aString string with a number of times occurrences of the
		/// subStr substring specified replaced by the other string, starting from the from position
		/// </summary>
		public static string ReplaceTimes(string aString, string subStr, string other, int? from, int? times)
		{
			int lfrom, ltimes;
			if (from.HasValue && times.HasValue)
			{
				lfrom = (int)from;
				ltimes = (int)times;
			}
			else
			{
				return null;
			}
			if (aString != null)
			{
				System.Text.StringBuilder lBuffer = new System.Text.StringBuilder();
				int lOldPos = (int)lfrom - 1;
				lBuffer.Append(aString.Substring(0, ((int)lfrom) - 1));
				int pos = aString.IndexOf(subStr, ((int)lfrom) - 1);
				for (; pos >= 0 && ltimes > 0; pos = aString.IndexOf(subStr, lOldPos), ltimes--)
				{
					lBuffer.Append(aString.Substring(lOldPos, pos - lOldPos));
					lBuffer.Append(other);
					lOldPos = pos + subStr.Length;
				}
				lBuffer.Append(aString.Substring(lOldPos));
				return lBuffer.ToString();
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Concatenates the secondStr string at the end of the firstStr string
		/// </summary>
		public static string Concat(string firstStr, string secondStr)
		{
			return System.String.Concat(firstStr, secondStr);
		}

		/// <summary>
		/// Returns a new String which is a copy of aString without the region defined by the
		/// start position specified with the specified length
		/// </summary>
		public static string StrDelete(string aString, int? start, int? length)
		{
			int lstart, llength;
			if (start.HasValue && length.HasValue)
			{
				lstart = (int)start;
				llength = (int)length;
			}
			else
			{
				return null;
			}

			if (aString != null)
			{
				return aString.Remove(((int)lstart) - 1, (int)llength);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Returns a copy of the aString string with a new string (insertion) inserted at the
		/// index position
		/// </summary>
		public static string StrInsert(string aString, int? index, string insertion)
		{
			int lindex;
			if (index.HasValue)
			{
				lindex = (int)index;
			}
			else
			{
				return null;
			}
			if (aString != null)
			{
				return aString.Insert(((int)lindex) - 1, insertion);
			}
			else
			{
				return null;
			}
		}

		#endregion Substring functions

		#region Case

		/// <summary>
		/// Return a new String with all its characters in their Uppercase variant
		/// </summary>
		public static string UpperCase(string aString)
		{
			if (aString != null)
			{
				return aString.ToUpper();
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Return a new String with all its characters in their Lowercase variant
		/// </summary>
		public static string LowerCase(string aString)
		{
			if (aString != null)
			{
				return aString.ToLower();
			}
			else
			{
				return null;
			}
		}

		#endregion Case

		#region Simple Queries

		/// <summary>
		/// Returns the size of the aString string
		/// </summary>
		public static int? Length(string aString)
		{
			if (aString != null)
			{
				return aString.Length;
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Returns a new Sting, copy of the aString string with its characters in reverse order
		/// </summary>
		public static string Reverse(string aString)
		{
			if (aString == null)
			{
				return null;
			}
			System.Text.StringBuilder lBuffer = new System.Text.StringBuilder();
			for (int i = aString.Length - 1; i >= 0; i--)
			{
				lBuffer.Append(aString[i]);
			}
			return lBuffer.ToString();
		}

		#endregion Simple Queries

		#region Comparison

		/// <summary>
		/// Compare both strings: firstStr and secondStr lexicographically. Returns a negative
		/// value when firstStr appears before secondStr, 0 if they are equals, or a positive value if firstStr  follows secondStr
		/// </summary>
		public static int? StrCompare(string firstStr, string secondStr)
		{
			if (firstStr == null)
			{
				return 0;
			}
			return firstStr.CompareTo(secondStr);
		}

		/// <summary>
		/// Compares both strings lexicographically ignoring case considerations. It returns
		/// a negative alue when the firstStr string appears before the secondStr in the lexical order, 0 if they are equals, or a positive value if the first string follows the second String
		/// </summary>
		public static int StrCompareIgnoreCase(string firstStr, string secondStr)
		{
			if (firstStr == null)
			{
				return 0;
			}
			return String.Compare(firstStr, secondStr, true);
		}

		/// <summary>
		/// Compares two regions defined by the arguments of this method: The first string
		/// region is defined by the substring of the aString string starting at the from position
		/// with a length size, the second one is a substring which starts at the otherFrom
		/// position of the other string with the length size. If both regions are equals
		/// (depending on the ignoreCase parameter) this method returns true, if not, or if one
		/// of the from or otherFrom arguments is negative, or some of the regions exceeds the
		/// end of the string, this method returns false.
		/// </summary>
		public static bool StrRegionMatches(string aString, bool? ignoreCase, int? from, string other, int? otherFrom, int? length)
		{
			int lfrom, lotherFrom, llength;
			bool lignoreCase;
			if (ignoreCase.HasValue && from.HasValue && otherFrom.HasValue && length.HasValue)
			{
				lignoreCase = (bool)ignoreCase;
				lfrom = (int)from;
				lotherFrom = (int)otherFrom;
				llength = (int)length;
			}
			else
			{
				return false;
			}

			if (aString == null)
			{
				return false;
			}
			if (lfrom < 0 || lotherFrom < 0 || lfrom + llength > aString.Length ||
				lotherFrom + llength > other.Length)
			{
				return false;
			}
			if (lignoreCase)
			{
				aString = aString.ToUpper();
				other = other.ToUpper();
			}

			return aString.Substring((int)lfrom, (int)llength).Equals(other.Substring((int)lotherFrom, (int)llength));
		}

		/// <summary>
		/// Return true if the string firstStr comes before the string secondStr in lexicographic order
		/// </summary>
		public static bool StrLesserThan(string firstStr, string secondStr)
		{
			if (firstStr == null)
			{
				return false;
			}
			return (string.Compare(firstStr, secondStr) < 0);
		}

		/// <summary>
		/// Return true if the string firstStr comes after the string secondStr in lexicographic order
		/// </summary>
		public static bool StrGreaterThan(string firstStr, string secondStr)
		{
			if (firstStr == null)
			{
				return false;
			}
			return (string.Compare(firstStr, secondStr) > 0);
		}

		/// <summary>
		/// Return true if the string firstStr and the string secondStr are lexicographically equals
		/// </summary>
		public static bool StrSameAs(string firstStr, string secondStr)
		{
			if (firstStr == null)
			{
				return false;
			}
			return (string.Compare(firstStr, secondStr) == 0);
		}

		#endregion Comparison

		#region Conversions
		/// <summary>
		/// Return the inbound text argument aText as a string
		/// </summary>
		public static string TextToStr(string aText)
		{
			return aText;
		}
		#endregion Conversions

		#endregion StringFunctions
	}
}

