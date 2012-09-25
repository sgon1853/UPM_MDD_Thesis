// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace SIGEM.Client.Presentation
{
	/// <summary>
	/// Default formats and sizes for all the Presentations
	/// </summary>
	public static class DefaultFormats
	{
		#region Constants
		// Grid Column default width
		public const int ColWidthAutonumeric = 50;
		public const int ColWidthBool = 40;
		public const int ColWidthBoolDefinedSelection = 60;
		public const int ColWidthTime = 50;
		public const int ColWidthDate = 70;
		public const int ColWidthDateTime = 120;
		public const int ColWidthInt = 50;
		public const int ColWidthNat = 50;
		public const int ColWidthReal = 70;
		public const int ColWidthPassword = 100;
		public const int ColWidthText = 150;
		public const int ColWidthString5 = 45;
		public const int ColWidthString10 = 85;
		public const int ColWidthString20 = 150;
		public const int ColWidthBlob = 100;
		#endregion Constants

		#region Members
		private static char[] ValidCharForNumeric = new char[] { '-', '\b', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
		public static List<char> ValidCharForNumericValues = new List<char>(ValidCharForNumeric);
		#endregion Members

		#region Display Formats

		#region Apply Display Format
		/// <summary>
		/// Apply display format by default for all data types
		/// All the values will be shown using those formats
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="dataType">Data type.</param>
		/// <returns>string.</returns>
		public static string ApplyDisplayFormat(object value, ModelType dataType)
		{
			if (value == null)
			{
				return string.Empty;
			}
			try
			{
				string format = GetDefaultDisplayMask(dataType);
				switch (dataType)
				{
					case ModelType.Date:
						DateTime DateAux = System.Convert.ToDateTime(value, CultureManager.Culture);
						return DateAux.ToString(format);

					case ModelType.DateTime:
						DateTime DateTimeAux = System.Convert.ToDateTime(value, CultureManager.Culture);
						return DateTimeAux.ToString(format);

					case ModelType.Time:
						DateTime TimeAux = System.Convert.ToDateTime(value.ToString(), CultureManager.Culture);
						return TimeAux.ToString(format);

					case ModelType.Autonumeric:
					case ModelType.Int:
						int IntAux = System.Convert.ToInt32(value);
						return IntAux.ToString(format);

					case ModelType.Nat:
						UInt32 NatAux = System.Convert.ToUInt32(value);
						return NatAux.ToString(format);

					case ModelType.Real:
						Decimal RealAux = System.Convert.ToDecimal(value);
						return RealAux.ToString(format);

					case ModelType.String:
					case ModelType.Password:
					case ModelType.Blob:
					case ModelType.Text:
						string StringAux = System.Convert.ToString(value);
						return StringAux.ToString(CultureManager.Culture);

					case ModelType.Bool:
						bool BoolAux = System.Convert.ToBoolean(value);
						return BoolAux.ToString(CultureManager.Culture);

					default:
						return value.ToString();
				}
			}
			catch
			{
				return value.ToString();
			}
		}
		#endregion  Apply Display Format

		#region Get Default Display Mask
		/// <summary>
		/// Gets the default display mask for a Model data type.
		/// </summary>
		/// <param name="modelType">ModelType of the display control.</param>
		/// <returns>Format mask.</returns>
		public static string GetDefaultDisplayMask(ModelType modelType)
		{
			switch (modelType)
			{
				case ModelType.Autonumeric:
				case ModelType.Nat:
				case ModelType.Int:
					return "";
				case ModelType.Bool:
					return "bool";  // Special format. Manages by the DataGridViewPresentation

				case ModelType.Real:
					return "#,##0.#####";  // Thousand separator and decimal point (5 decimals)

				case ModelType.Date:
					return CultureManager.Culture.DateTimeFormat.ShortDatePattern;

				case ModelType.Time:
					return CultureManager.Culture.DateTimeFormat.LongTimePattern;

				case ModelType.DateTime:
					return CultureManager.Culture.DateTimeFormat.ShortDatePattern + " " + CultureManager.Culture.DateTimeFormat.LongTimePattern;

				default:
					return "";
			}
		}
		#endregion Get Default Display Mask

		#endregion Display Formats

		#region Input Formats

		#region Apply Input Format
		/// <summary>
		/// Apply input format for all the data types
		/// This function converts any data type to String, in order to be used in input fields.
		/// Returned strings values must be validated as correct values for the specified data type by the
		/// function 'CheckDataType'.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="dataType">Data type.</param>
		/// <returns>string.</returns>
		public static string ApplyInputFormat(object value, ModelType dataType)
		{
			if (value == null)
			{
				return string.Empty;
			}
			try
			{
				switch (dataType)
				{
					case ModelType.Date:
						DateTime DateAux = System.Convert.ToDateTime(value, CultureManager.Culture);
						return DateAux.ToString(CultureManager.Culture.DateTimeFormat.ShortDatePattern, CultureManager.Culture);

					case ModelType.DateTime:
						DateTime DateTimeAux = System.Convert.ToDateTime(value, CultureManager.Culture);
						return DateTimeAux.ToString(CultureManager.Culture.DateTimeFormat.ShortDatePattern + " " +
							CultureManager.Culture.DateTimeFormat.LongTimePattern, CultureManager.Culture);

					case ModelType.Time:
						DateTime TimeAux = System.Convert.ToDateTime(value.ToString(), CultureManager.Culture);
						return TimeAux.ToString(CultureManager.Culture.DateTimeFormat.LongTimePattern, CultureManager.Culture);

					case ModelType.Autonumeric:
					case ModelType.Int:
						int IntAux = System.Convert.ToInt32(value);
						return IntAux.ToString(CultureManager.Culture);

					case ModelType.Nat:
						UInt32 NatAux = System.Convert.ToUInt32(value);
						return NatAux.ToString(CultureManager.Culture);

					case ModelType.Real:
						Decimal RealAux = System.Convert.ToDecimal(value);
						return RealAux.ToString(CultureManager.Culture);

					case ModelType.String:
					case ModelType.Password:
					case ModelType.Blob:
					case ModelType.Text:
						string StringAux = System.Convert.ToString(value);
						return StringAux.ToString(CultureManager.Culture);

					case ModelType.Bool:
						if ((value == null) || (value.ToString().ToUpper() == "NULL"))
							return "null";
						bool BoolAux = System.Convert.ToBoolean(value);
						return BoolAux.ToString(CultureManager.Culture);

					default:
						return value.ToString();
				}
			}
			catch
			{
				// Default value
				return value.ToString();
			}
		}
		/// <summary>
		/// Apply input format for all the data types according an introduction mask.
		/// This function converts any data type to String, in order to be used in input fields.
		/// Returned strings values must be validated as correct values for the specified data type by the
		/// function 'CheckDataType'.
		/// </summary>
		/// <param name="value">Value to be formated.</param>
		/// <param name="dataType">Data type of the value to be formated.</param>
		/// <param name="mask">Introduction mask.</param>
		/// <returns>A string representing the Value formatted.</returns>
		public static string ApplyInputFormat(object value, ModelType dataType, string mask)
		{
			if (value == null)
			{
				return string.Empty;
			}
			try
			{
				switch (dataType)
				{
					case ModelType.Date:
						DateTime DateAux = System.Convert.ToDateTime(value, CultureManager.Culture);
						return DateAux.ToString(mask);

					case ModelType.DateTime:
						DateTime DateTimeAux = System.Convert.ToDateTime(value, CultureManager.Culture);
						return DateTimeAux.ToString(mask);

					case ModelType.Time:
						DateTime TimeAux = System.Convert.ToDateTime(value.ToString(), CultureManager.Culture);
						return TimeAux.ToString(mask);

					case ModelType.Autonumeric:
					case ModelType.Int:
						int IntAux = System.Convert.ToInt32(value);
						return IntAux.ToString(mask);

					case ModelType.Nat:
						UInt32 NatAux = System.Convert.ToUInt32(value);
						return NatAux.ToString(mask);

					case ModelType.Real:
						Decimal RealAux = System.Convert.ToDecimal(value);
						return RealAux.ToString(mask);

					case ModelType.String:
					case ModelType.Password:
					case ModelType.Blob:
					case ModelType.Text:
						string StringAux = System.Convert.ToString(value);
						return StringAux.ToString();

					case ModelType.Bool:
						if ((value == null) || (value.ToString().ToUpper() == "NULL"))
							return "null";
						bool BoolAux = System.Convert.ToBoolean(value);
						return BoolAux.ToString(CultureManager.Culture);

					default:
						return value.ToString();
				}
			}
			catch
			{
				// Default value
				return value.ToString();
			}
		}
		#endregion Apply Input Format

		#region Check Data Type
		/// <summary>
		/// Checks the value depending on its data type.
		/// This function must be sincronized with its opposite function 'ApplyInputFormat'
		/// </summary>
		/// <param name="value">Value to check.</param>
		/// <param name="modelType">ModelType of the value to check.</param>
		/// <param name="nullAllowed">Indicates if the value can be null or not.</param>
		/// <returns>If the cheking was ok, return true. Otherwise, return false.</returns>
		public static bool CheckDataType(string value, ModelType modelType, bool nullAllowed)
		{
			try
			{
				// Manage null values depending if the value accepts null or not.
				if (value == string.Empty)
				{
					return nullAllowed;
				}
				switch (modelType)
				{
					case ModelType.Autonumeric:
						if (Convert.ToInt32(value) < -1)
						{
							return false;
						}
						break;

					case ModelType.Date:
						System.Convert.ToDateTime(value, CultureManager.Culture);
						break;

					case ModelType.Time:
						DateTime lDate = System.Convert.ToDateTime(value, CultureManager.Culture);
						string lValue = lDate.ToString(CultureManager.Culture.DateTimeFormat.LongTimePattern, CultureManager.Culture);

						// Time Formats accepted for Edition.
						string[] lTimeFormats = {
							CultureManager.Culture.DateTimeFormat.ShortTimePattern,
							CultureManager.Culture.DateTimeFormat.LongTimePattern,
							CultureManager.Culture.DateTimeFormat.RFC1123Pattern,
							"%h:%m tt",
							"%h:%m:%s tt",
							"%h:%m:%s.%f tt",
							"%h:%m:%s.%ff tt",
							"%h:%m:%s.%fff tt",
							"%h:%m",
							"%h:%m:%s",
							"%h:%m:%s.%f",
							"%h:%m:%s.%ff",
							"%h:%m:%s.%fff"};

						DateTime.ParseExact(lValue, lTimeFormats, CultureManager.Culture, DateTimeStyles.None);
						break;

					case ModelType.DateTime:
						System.Convert.ToDateTime(value, CultureManager.Culture);
						break;

					case ModelType.Int:
						Int32.Parse(value, System.Globalization.NumberStyles.Number, CultureManager.Culture);
						break;

					case ModelType.Nat:
						UInt32.Parse(value, System.Globalization.NumberStyles.Number, CultureManager.Culture);
						break;

					case ModelType.Real:
						double.Parse(value, System.Globalization.NumberStyles.Number, CultureManager.Culture);
						break;

					case ModelType.Password:
					case ModelType.Text:
					case ModelType.String:
					case ModelType.Blob:
					case ModelType.Oid:
					case ModelType.Bool:
						return true;
					default:
						return false;
				}
			}
			catch
			{
				// If some error ocurred, return false.
				return false;
			}

			// Otherwise, return true.
			return true;
		}
		#endregion Check Data Type

		#region Get Help Mask
		/// <summary>
		/// Gets the introduction mask for an input control.
		/// </summary>
		/// <param name="modelType">ModelType of the input control.</param>
		/// <param name="mask">Default mask.</param>
		/// <returns>Help mask.</returns>
		public static string GetHelpMask(ModelType modelType, string mask)
		{
			try
			{
				if (mask != string.Empty)
				{
					return mask;
				}
				else
				{
					switch (modelType)
					{
						case ModelType.Autonumeric:
							return CultureManager.TranslateString(LanguageConstantKeys.L_VALIDATION_AUTONUMERIC_TYPE, LanguageConstantValues.L_VALIDATION_AUTONUMERIC_TYPE);

						case ModelType.Date:
							return CultureManager.Culture.DateTimeFormat.ShortDatePattern;

						case ModelType.Time:
							return CultureManager.Culture.DateTimeFormat.LongTimePattern;

						case ModelType.DateTime:
							return CultureManager.Culture.DateTimeFormat.ShortDatePattern + " " + CultureManager.Culture.DateTimeFormat.LongTimePattern;

						case ModelType.Int:
							return CultureManager.TranslateString(LanguageConstantKeys.L_VALIDATION_INTEGER_TYPE, LanguageConstantValues.L_VALIDATION_INTEGER_TYPE);

						case ModelType.Nat:
							return ">0";

						case ModelType.Real:
							string lGroupSizes = string.Empty;
							string lDecimalDigits = string.Empty;
							for (int i = 0; i < CultureManager.Culture.NumberFormat.NumberDecimalDigits; i++)
							{
								lDecimalDigits += "0";
							}
							for (int i = 0; i < CultureManager.Culture.NumberFormat.NumberGroupSizes[0]; i++)
							{
								lGroupSizes += "0";
							}
							//mCulture.NumberFormat.NumberGroupSeparator
							return "1" + "" + lGroupSizes + CultureManager.Culture.NumberFormat.NumberDecimalSeparator + lDecimalDigits;

							default:
							return string.Empty;
					}
				}
			}
			catch
			{
				return string.Empty;
			}
		}
		#endregion Get Help Mask

		#endregion Input Formats
	}
}

