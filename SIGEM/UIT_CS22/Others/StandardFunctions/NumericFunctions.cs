// v3.8.4.5.b
using System;

namespace SIGEM.Client.StandardFunctions
{
	public class NumericFunctions
	{
		#region NumericFunctions

		#region Constants

		/// <summary>
		/// Returns the value of the PI constant, equals to 3.14159265358979323846832
		/// </summary>
		public static decimal? PI()
		{
			return (decimal)Math.PI;
		}

		/// <summary>
		/// Returns the value of the Euler constant, equals to 2.7182818284590452354
		/// </summary>
		public static decimal? Euler()
		{
			return (decimal)Math.E;
		}

		#endregion Constants

		#region Trigonometric

		/// <summary>
		/// Returns the trigonometric sine of the argument Angle
		/// </summary>
		public static decimal? Sin(decimal? angle)
		{
			double langle;
			if (angle.HasValue)
			{
				langle = (double)angle;
			}
			else
			{
				return null;
			}
			return (decimal)Math.Sin((double)langle);
		}

		/// <summary>
		/// Returns the trigonometric cosine of the argument Angle
		/// </summary>
		public static decimal? Cos(decimal? angle)
		{
			double langle;
			if (angle.HasValue)
			{
				langle = (double)angle;
			}
			else
			{
				return null;
			}
			return (decimal)Math.Cos((double)langle);
		}

		/// <summary>
		/// Returns the trigonometric tangent of the argument Angle
		/// </summary>
		public static decimal? Tan(decimal? angle)
		{
			double langle;
			if (angle.HasValue)
			{
				langle = (double)angle;
			}
			else
			{
				return null;
			}
			return (decimal)Math.Tan((double)langle);
		}

		/// <summary>
		/// Returns the trigonometric tangent of the argument Angle
		/// </summary>
		public static decimal? Cot(decimal? angle)
		{
			double langle;
			if (angle.HasValue)
			{
				langle = (double)angle;
			}
			else
			{
				return null;
			}
			return (decimal)(1 / Math.Tan((double)langle));
		}

		/// <summary>
		/// Returns the trigonometric arc sine of the argument Value. The result is in the range of -PI/2 through PI/2
		/// </summary>
		public static decimal? Asin(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)Math.Asin((double)lvalue);
		}

		/// <summary>
		/// Returns the trigonometric arc cosine of the argument Value. The result is in the range of -PI/2 through PI/2
		/// </summary>
		public static decimal? Acos(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)Math.Acos((double)lvalue);
		}

		/// <summary>
		/// Returns the trigonometric arc tangent of the argument Value. The result is in the range of -PI/2 through PI/2
		/// </summary>
		public static decimal? Atan(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)Math.Atan((double)lvalue);
		}

		/// <summary>
		/// Returns the trigonometric arc tangent of the argument Value. The result is in the range of -PI/2 through PI/2
		/// </summary>
		public static decimal? Acot(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)(Math.PI / 2 - Math.Atan((double)lvalue));
		}

		/// <summary>
		/// Returns the trigonometric arc tangent of the argument Value. The result is in the range of -PI through PI
		/// </summary>
		public static decimal? Atan2(decimal? x, decimal? y)
		{
			double lx, ly;
			if (x.HasValue && y.HasValue)
			{
				lx = (double)x;
				ly = (double)y;
			}
			else
			{
				return null;
			}
			return (decimal)Math.Atan2((double)lx, (double)ly);
		}

		/// <summary>
		/// Converts an angle measured in degrees (Value) in an angle measured in radians. The result is mathematically equivalent to Value * PI / 180
		/// </summary>
		public static decimal? ToRadians(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)((double)lvalue * Math.PI / 180);
		}

		/// <summary>
		/// Converts an angle measured in radians (Value) in an angle measured in degrees. The result is mathematically equivalent to Value * 180 / PI
		/// </summary>
		public static decimal? ToDegrees(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)((double)lvalue * 180 / Math.PI);
		}

		/// <summary>
		/// Returns the trigonometric secant of the argument Value
		/// </summary>
		public static decimal? Sec(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)(1 / Math.Cos((double)lvalue));
		}

		/// <summary>
		/// Returns the trigonometric cosecant of the argument Value
		/// </summary>
		public static decimal? Csec(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)(1 / Math.Sin((double)lvalue));
		}

		/// <summary>
		/// Returns the trigonometric inverse secant of the argument Value
		/// </summary>
		public static decimal? Asec(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)(Math.PI / 2 - Math.Atan(Math.Sign((double)lvalue) / Math.Sqrt((double)lvalue * (double)lvalue - 1)));
		}

		/// <summary>
		/// Returns the trigonometric inverse cosecant of the argument Value
		/// </summary>
		public static decimal? Acsec(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)(Math.Atan(Math.Sign((double)lvalue) / Math.Sqrt((double)lvalue * (double)lvalue - 1)));
		}

		#endregion Trigonometric

		#region Elemental mathemathics

		/// <summary>
		/// Returns the Euler constant raised to the specified argument, e^Value
		/// </summary>
		public static decimal? Exp(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)Math.Exp((double)lvalue);
		}

		/// <summary>
		/// Returns the natural logarithm (base Euler constant) of the specified argument
		/// </summary>
		public static decimal? Log(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)Math.Log((double)lvalue);
		}

		/// <summary>
		/// Returns the value Base ^ Exponent
		/// </summary>
		public static decimal? Pow(decimal? Base, decimal? Exponent)
		{
			double lBase, lExponent;
			if (Base.HasValue && Exponent.HasValue)
			{
				lBase = (double)Base;
				lExponent = (double)Exponent;
			}
			else
			{
				return null;
			}
			return (decimal)Math.Pow((double)lBase, (double)lExponent);
		}

		/// <summary>
		/// Returns the square root of the specified value
		/// </summary>
		public static decimal? Sqrt(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)Math.Sqrt((double)lvalue);
		}

		/// <summary>
		/// Returns the absolute value of the specified argument
		/// </summary>
		public static int? AbsInt(int? value)
		{
			int lvalue;
			if (value.HasValue)
			{
				lvalue = (int)value;
			}
			else
			{
				return null;
			}
			return Math.Abs((int)lvalue);
		}

		/// <summary>
		/// Returns the absolute value of the specified argument
		/// </summary>
		public static decimal? AbsReal(decimal? value)
		{
			decimal lvalue;
			if (value.HasValue)
			{
				lvalue = (decimal)value;
			}
			else
			{
				return null;
			}
			return (decimal)(Math.Abs((decimal)lvalue));
		}

		/// <summary>
		/// Returns the minimum between Value1 and Value2
		/// </summary>
		public static decimal? Min(decimal? value1, decimal? value2)
		{
			decimal lvalue1, lvalue2;
			if (value1.HasValue && value2.HasValue)
			{
				lvalue1 = (decimal)value1;
				lvalue2 = (decimal)value2;
			}
			else
			{
				return null;
			}
			return (decimal)(Math.Min((decimal)lvalue1, (decimal)lvalue2));
		}

		/// <summary>
		/// Returns the maximum between Value1 and Value2
		/// </summary>
		public static decimal? Max(decimal? value1, decimal? value2)
		{
			decimal lvalue1, lvalue2;
			if (value1.HasValue && value2.HasValue)
			{
				lvalue1 = (decimal)value1;
				lvalue2 = (decimal)value2;
			}
			else
			{
				return null;
			}
			return (decimal)(Math.Max((decimal)lvalue1, (decimal)lvalue2));
		}

		/// <summary>
		/// Returns the minimum between Value1 and Value2
		/// </summary>
		public static int? MinInt(int? value1, int? value2)
		{
			int lvalue1, lvalue2;
			if (value1.HasValue && value2.HasValue)
			{
				lvalue1 = (int)value1;
				lvalue2 = (int)value2;
			}
			else
			{
				return null;
			}
			return Math.Min((int)lvalue1, (int)lvalue2);
		}

		/// <summary>
		/// Returns the maximum between Value1 and Value2
		/// </summary>
		public static int? MaxInt(int? value1, int? value2)
		{
			int lvalue1, lvalue2;
			if (value1.HasValue && value2.HasValue)
			{
				lvalue1 = (int)value1;
				lvalue2 = (int)value2;
			}
			else
			{
				return null;
			}
			return Math.Max((int)lvalue1, (int)lvalue2);
		}

		/// <summary>
		/// Returns the logarithm of Value, in base Base
		/// </summary>
		public static decimal? LogBase(decimal? value, decimal? Base)
		{
			double lvalue, lBase;
			if (value.HasValue && Base.HasValue)
			{
				lvalue = (double)value;
				lBase = (double)Base;
			}
			else
			{
				return null;
			}
			return (decimal)(Math.Log((double)lvalue, (double)lBase));
		}

		/// <summary>
		/// Returns the sign of the specified argument, represented as -1, 0 and 1, in correspondence with negative, zero or positive argument value
		/// </summary>
		public static int? Sign(decimal? value)
		{
			decimal lvalue;
			if (value.HasValue)
			{
				lvalue = (decimal)value;
			}
			else
			{
				return null;
			}
			return Math.Sign((decimal)lvalue);
		}

		/// <summary>
		/// Returns the greatest common divisor between Value1 and Value2.
		/// That is, the greatest integer value that exactly divides Value1
		/// (integer quotient and remainder 0) and exactly divides Value2
		/// </summary>
		public static int? Gcd(int? value1, int? value2)
		{
			int lvalue1, lvalue2;
			if (value1.HasValue && value2.HasValue)
			{
				lvalue1 = (int)value1;
				lvalue2 = (int)value2;
			}
			else
			{
				return null;
			}

			int lRem;
			int a = (int)lvalue1;
			int b = (int)lvalue2;

			while (b != 0)
			{
				lRem = a % b;
				a = b;
				b = lRem;
			}

			return a;
		}

		/// <summary>
		/// Returns the less common multiple between Value1 and Value2.
		/// That is, the lesser integer value that is exactly divisible by Value1 and Value2.
		/// </summary>
		public static int? Lcm(int? value1, int? value2)
		{
			int dvalue1, dvalue2;
			if (value1.HasValue && value2.HasValue)
			{
				dvalue1 = (int)value1;
				dvalue2 = (int)value2;
			}
			else
			{
				return null;
			}

			int lMayor, lMenor;
			int lvalue1 = (int)dvalue1;
			int lvalue2 = (int)dvalue2;

			if (lvalue1 >= lvalue2)
			{
				lMayor = lvalue1;
				lMenor = lvalue2;
			}
			else
			{
				lMayor = lvalue2;
				lMenor = lvalue1;
			}

			int lMcm = lMayor;

			while (lMcm % lMenor !=0)
				lMcm += lMayor;

			return lMcm;
		}

		#endregion Elemental mathemathics

		#region Rounding

		/// <summary>
		/// Returns the smallest value (closest to negative infinity) greater than or equal
		/// to Value, and mathematically equal to an integer
		/// </summary>
		public static int? Ceiling(decimal? value)
		{
			decimal lvalue;
			if (value.HasValue)
			{
				lvalue = (decimal)value;
			}
			else
			{
				return null;
			}
			return System.Convert.ToInt32(Math.Ceiling(lvalue));
		}

		/// <summary>
		/// Returns the largest value (closest to positive infinity) less than or equal
		/// to Value, and mathematically equal to an integer
		/// </summary>
		public static int? Floor(decimal? value)
		{
			decimal lvalue;
			if (value.HasValue)
			{
				lvalue = (decimal)value;
			}
			else
			{
				return null;
			}
			return System.Convert.ToInt32(Math.Floor(lvalue));
		}

		/// <summary>
		/// Returns the integer part of the real argument Value just removing the fractionary part.
		/// If Value is below 0, it returns the ceiling, otherwise it returns the floor
		/// </summary>
		public static int? Trunc(decimal? value)
		{
			decimal lvalue;
			if (value.HasValue)
			{
				lvalue = (decimal)value;
			}
			else
			{
				return null;
			}
			return System.Convert.ToInt32(Math.Truncate(lvalue));
		}

		/// <summary>
		/// Returns the integer value closest to Value
		/// </summary>
		public static int? Round(decimal? value)
		{
			decimal lvalue;
			if (value.HasValue)
			{
				lvalue = (decimal)value;
			}
			else
			{
				return null;
			}
			return System.Convert.ToInt32(Math.Round(lvalue, MidpointRounding.AwayFromZero));
		}

		/// <summary>
		/// Returns the number with the specified precision nearest the specified value,
		/// where digits is the number of significant fractional digits (precision) in
		/// the return value
		/// </summary>
		public static decimal? RoundEx(decimal? value, int? digits)
		{
			decimal lvalue;
			int ldigits;
			if (value.HasValue && digits.HasValue)
			{
				lvalue = (decimal)value;
				ldigits = (int)digits;
			}
			else
			{
				return null;
			}
			return (decimal)(Math.Round(lvalue, ldigits, MidpointRounding.AwayFromZero));
		}

		#endregion Rounding

		#region Conversions

		/// <summary>
		/// Returns the string representation of Value in decimal format
		/// </summary>
		public static string IntToStr(int? value)
		{
			int lvalue;
			if (value.HasValue)
			{
				lvalue = (int)value;
			}
			else
			{
				return null;
			}
			return Convert.ToString((int)lvalue);
		}

		/// <summary>
		/// Returns the string representation of Value
		/// </summary>
		public static string RealToStr(decimal? value)
		{
			decimal lvalue;
			if (value.HasValue)
			{
				lvalue = (decimal)value;
			}
			else
			{
				return null;
			}
			return Convert.ToString(lvalue);
		}

		/// <summary>
		/// Returns the string representation of Value in hexadecimal format
		/// </summary>
		public static string ToHexString(int? value)
		{
			int lvalue;
			if (value.HasValue)
			{
				lvalue = (int)value;
			}
			else
			{
				return null;
			}
			return Convert.ToString((int)lvalue, 16);
		}

		/// <summary>
		/// Returns the string representation of Value in octal format
		/// </summary>
		public static string ToOctalString(int? value)
		{
			int lvalue;
			if (value.HasValue)
			{
				lvalue = (int)value;
			}
			else
			{
				return null;
			}
			return Convert.ToString((int)lvalue, 8);
		}

		/// <summary>
		/// Returns the integer value represented in decimal format by the string Value
		/// </summary>
		public static int? StrToInt(string value)
		{
			return Convert.ToInt32(value);
		}

		/// <summary>
		/// Returns the real value represented by the string Value
		/// </summary>
		public static decimal? StrToReal(string value)
		{
			return (decimal)(Convert.ToDecimal(value));
		}

		/// <summary>
		/// Returns an integer represented by Value. Value must be a prefixed representation of
		/// a signed integer in decimal, octal or hexadecimal format. The specific prefixes
		/// depends on the specific implementation.
		/// </summary>
		public static int? DecodeString(string value)
		{
			// Function decodeString not yet implemented
			return 0;
		}

		#endregion Conversions

		#region Random

		/// <summary>
		/// Returns a uniformly distributed pseudo-random real number greater than or equal to 0
		/// and less than 1. The use of this method has a state because the generated sequence
		/// is pseudo-random, but it could be considered as stateless due to the imprecise
		/// connection among results of different calls
		/// </summary>
		public static decimal? Rnd()
		{
			return (decimal)((new Random()).NextDouble());
		}

		/// <summary>
		/// Returns a uniformly distributed pseudo-random discrete (integer) number greater than
		/// or equal to 0 and less than UpperBound
		/// </summary>
		public static int? RndInteger(int? upperBound)
		{
			int lupperBound;
			if (upperBound.HasValue)
			{
				lupperBound = (int)upperBound;
			}
			else
			{
				return null;
			}
			return (new Random()).Next((int)lupperBound);
		}

		/// <summary>
		/// Returns a uniformly distributed pseudo-random (real) number greater than or equal to 0
		/// and less than UpperBound
		/// </summary>
		public static decimal? RndReal(decimal? upperBound)
		{
			double lupperBound;
			if (upperBound.HasValue)
			{
				lupperBound = (double)upperBound;
			}
			else
			{
				return null;
			}
			return (decimal)((new Random()).NextDouble() * (double)lupperBound);
		}

		/// <summary>
		/// Returns a uniformly distributed pseudo-random discrete (integer) number greater than
		/// or equal to LowerBound and less than UpperBound
		/// </summary>
		public static int? RndIntBound(int? lowerBound, int? upperBound)
		{
			int lupperBound, llowerBound;
			if (upperBound.HasValue && lowerBound.HasValue)
			{
				lupperBound = (int)upperBound;
				llowerBound = (int)lowerBound;
			}
			else
			{
				return null;
			}
			return (new Random()).Next((int)llowerBound, (int)lupperBound);
		}

		/// <summary>
		/// Returns a uniformly distributed pseudo-random (real) number greater than or equal
		/// to LowerBound and less than UpperBound
		/// </summary>
		public static decimal? RndRealBound(decimal? lowerBound, decimal? upperBound)
		{
			double lupperBound, llowerBound;
			if (upperBound.HasValue && lowerBound.HasValue)
			{
				lupperBound = (double)upperBound;
				llowerBound = (double)lowerBound;
			}
			else
			{
				return null;
			}
			return (decimal)((new Random()).NextDouble() * ((double)lupperBound - (double)llowerBound) + (double)llowerBound);
		}

		#endregion Random

		#region Hyperbolic trigonometric

		/// <summary>
		/// Returns the hyperbolic sine of the argument Value. sinh(x)=(e^x-e^-x)/2
		/// </summary>
		public static decimal? Sinh(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)((Math.Exp((double)lvalue) - Math.Exp(-(double)lvalue)) / 2);
		}

		/// <summary>
		/// Returns the hyperbolic cosine of the argument Value. cosh(x)=(e^x+e^-x)/2
		/// </summary>
		public static decimal? Cosh(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)((Math.Exp((double)lvalue) + Math.Exp(-(double)lvalue)) / 2);
		}

		/// <summary>
		/// Returns the hyperbolic tangent of the argument Value. tanh(x)=(e^x-e^-x)/(e^x+e^-x)
		/// </summary>
		public static decimal? Tanh(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)((Math.Exp((double)lvalue) - Math.Exp(-(double)lvalue)) / (Math.Exp((double)lvalue) + Math.Exp(-(double)lvalue)));
		}

		/// <summary>
		/// Returns the hyperbolic cotangent of the argument Value. coth(x)=(e^x+e^-x)/(e^x-e^-x)
		/// </summary>
		public static decimal? Coth(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)((Math.Exp((double)lvalue) + Math.Exp(-(double)lvalue)) / (Math.Exp((double)lvalue) - Math.Exp(-(double)lvalue)));
		}

		/// <summary>
		/// Returns the hyperbolic secant of the argument Value. sech(x)=2/(e^x-e^-x)
		/// </summary>
		public static decimal? Sech(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)(2 / (Math.Exp((double)lvalue) + Math.Exp(-(double)lvalue)));
		}

		/// <summary>
		/// Returns the hyperbolic cosecant of the argument Value. csech(x)=2/(e^x+e^-x)
		/// </summary>
		public static decimal? Csech(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)(2 / (Math.Exp((double)lvalue) - Math.Exp(-(double)lvalue)));
		}

		/// <summary>
		/// Returns the inverse hyperbolic sine of the argument Value
		/// </summary>
		public static decimal? Asinh(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)(Math.Log((double)lvalue + Math.Sqrt((double)lvalue * (double)lvalue + 1)));
		}

		/// <summary>
		/// Returns the inverse hyperbolic cosine of the argument Value
		/// </summary>
		public static decimal? Acosh(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)(Math.Log((double)lvalue + Math.Sqrt((double)lvalue * (double)lvalue - 1)));
		}

		/// <summary>
		/// Returns the inverse hyperbolic tangent of the argument Value
		/// </summary>
		public static decimal? Atanh(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)(Math.Log((1 + (double)lvalue) / (1 - (double)lvalue)) / 2);
		}

		/// <summary>
		/// Returns the inverse hyperbolic cotangent of the argument Value
		/// </summary>
		public static decimal? Acoth(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)(Math.Log(((double)lvalue + 1) / ((double)lvalue - 1)) / 2);
		}

		/// <summary>
		/// Returns the inverse hyperbolic secant of the argument Value
		/// </summary>
		public static decimal? Asech(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)(Math.Log((Math.Sqrt(-(double)lvalue * (double)lvalue + 1) + 1) / (double)lvalue));
		}

		/// <summary>
		/// Returns the inverse hyperbolic cosecant of the argument Value
		/// </summary>
		public static decimal? Acsech(decimal? value)
		{
			double lvalue;
			if (value.HasValue)
			{
				lvalue = (double)value;
			}
			else
			{
				return null;
			}
			return (decimal)(Math.Log((double)(Sign((decimal)lvalue) * Math.Sqrt((double)lvalue * (double)lvalue + 1) + 1) / (double)lvalue));
		}

		#endregion Hyperbolic trigonometric

		#endregion NumericFunctions
	}
}

