// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Xml;
using System.Reflection;

using System.IO;

namespace SIGEM.Client.Adaptor.DataFormats
{
	/// <summary>
	/// Static functions for convert Types to XML and viceversa.
	/// </summary>
	public class Convert
	{
		private Convert() { }

		#region constants
		/// <summary>
		/// Constant that determines the default date format.
		/// </summary>
		public const string DateFormatString = @"yyyy-MM-dd";
		/// <summary>
		/// Constant that determines the default time format.
		/// </summary>
		public const string TimeFormatString = @"HH:mm:ss.fff";
		/// <summary>
		/// Constant that determines the default date-time format.
		/// </summary>
		public const string DateTimeFormatString = @"yyyy-MM-ddTHH:mm:ss.fff";
		#endregion constants

		/// <summary>
		/// Constant that determines the default XML format.
		/// </summary>
		public static System.Globalization.CultureInfo XMLFormat = CultureInfo.InvariantCulture;
		/// <summary>
		/// Converts types, expressed as ModelType, to string.
		/// </summary>
		/// <param name="type">Type to convert.</param>
		/// <returns>String.</returns>
		public static string MODELTypeToStringType(ModelType type)
		{
			return type.ToString().ToLower();
		}
		/// <summary>
		/// Converts types, expressed as string, to ModelType.
		/// </summary>
		/// <param name="type">Type to convert.</param>
		/// <returns>String.</returns>
		public static ModelType StringTypeToMODELType(string type)
		{
			ModelType lResult = ModelType.Oid;
			FieldInfo[] lMembers = typeof(ModelType).GetFields(BindingFlags.Static | BindingFlags.Public);
			foreach(FieldInfo lItem in lMembers)
			{
				if (string.Compare(type, lItem.Name, true) == 0)
				{
					lResult = (ModelType) lItem.GetValue(null);
					break;
				}
			}

			return lResult;
		}

		#region DateTime <-> XML representation-NI-127-(DateTime).
		/// <summary>
		/// Converts DateTime object to XML DateTime representation.
		/// </summary>
		/// <param name="dateTime">DateTime.</param>
		/// <returns>XMLDateTime.</returns>
		public static string DateTimeToXmlDateTime(DateTime dateTime)
		{
			return dateTime.ToString(DateTimeFormatString);
		}
		/// <summary>
		/// Converts XML DateTime element to DateTime representation.
		/// </summary>
		/// <param name="value">XML DateTime.</param>
		/// <returns>DateTime.</returns>
		public static DateTime? XmlDateTimeToDateTime(string value)
		{
			DateTime? lResult = (DateTime?)null;
			if (value != null)
			{
				lResult = DateTime.Parse(value, XMLFormat.DateTimeFormat);			
			}
			return lResult;
		}
		#endregion DateTime -> XML representation-NI-127-(DateTime).

		#region DateTime <-> XML representation-NI-127-(Time).
		/// <summary>
		/// Converts DateTime object to XML Time representation.
		/// </summary>
		/// <param name="dateTime">DateTime.</param>
		/// <returns>XMLTime.</returns>
		public static string DateTimeToXmlTime(DateTime dateTime)
		{
			return TimeSpanToXmlTime(dateTime.TimeOfDay);
		}
		/// <summary>
		/// Converts XML Time element to DateTime representation.
		/// </summary>
		/// <param name="value">XML Time.</param>
		/// <returns>DateTime.</returns>
		public static DateTime? XmlTimeToDateTime(string value)
		{
			DateTime? lResult = XmlDateTimeToDateTime(value);
			if (lResult != null)
			{
				lResult = DateTime.Parse(DateTimeToXmlTime(lResult.Value));
			}
			return lResult;
		}
		#endregion DateTime <-> XML representation-NI-127-(Time).

		#region DateTime <-> XML representation-NI-127-(Date).
		/// <summary>
		/// Converts DateTime object to XML Date representation.
		/// </summary>
		/// <param name="dateTime">DateTime.</param>
		/// <returns>XMLDate.</returns>
		public static string DateTimeToXmlDate(DateTime dateTime)
		{
			return dateTime.ToString(DateFormatString);
		}
		/// <summary>
		/// Converts XML Date element to DateTime representation.
		/// </summary>
		/// <param name="value">XML Date.</param>
		/// <returns>DateTime.</returns>
		public static DateTime? XmlDateToDateTime(string value)
		{
			DateTime? lResult = null;
			if ((value != null) && (value.Length > 0))
			{
				lResult = XmlDateTimeToDateTime(value);
			}

			return lResult;
		}
		#endregion DateTime -> XML representation-NI-127-(Date).

		#region TimeSpan <-> XML representation-NI-127-(Time).
		/// <summary>
		/// Converts TimeSpan object to XML Time representation.
		/// </summary>
		/// <param name="timeSpan">TimeSpan.</param>
		/// <returns>XMLTime.</returns>
		public static string TimeSpanToXmlTime(TimeSpan timeSpan)
		{
			//"TIME": hh:mm:ss
					
			StringBuilder lTime = new StringBuilder();
			
			lTime.Append(string.Format("{0:00}", timeSpan.Hours));
			lTime.Append(":");
			lTime.Append(string.Format("{0:00}", timeSpan.Minutes));
			lTime.Append(":");
			lTime.Append(string.Format("{0:00}", timeSpan.Seconds));
			lTime.Append(".");
			lTime.Append(string.Format("{0:000}", timeSpan.Milliseconds));
			return lTime.ToString();
		}
		/// <summary>
		/// Converts XML Time element to TimeSpan representation.
		/// </summary>
		/// <param name="value">XML Time.</param>
		/// <returns>TimeSpan.</returns>
		public static TimeSpan? XmlTimeToTimeSpan(string value)
		{
			TimeSpan? lResult = null;
			if (value != null)
			{
				try
				{
					lResult = TimeSpan.Parse(value);
				}
				catch
				{
					lResult = null;
				}

				if (lResult == null)
				{
					// Is string an DateTime ?
					DateTime? timeSpan = null;
					try
					{
						timeSpan = XmlDateToDateTime(value);
					}
					catch
					{
						timeSpan = null;
					}

					if (timeSpan != null)
					{
						//"TIME": hh:mm:ss.fff
						lResult = timeSpan.Value.TimeOfDay;
					}
				}
			}
			return lResult;
		}
		#endregion TimeSpan <-> XML representation-NI-127-(Time).

		#region NET Data Type To Xml Data type (NI-127).
		/// <summary>
		/// Converts NET Data Type element to XML Data Type representation.
		/// </summary>
		/// <param name="type">Type of dates for MODEL{Autonumeric,Nat,Int,String,Tex,Real,Bool,Date,Time,DateTime}</param>
		/// <param name="value">Typed data or null.</param>
		/// <returns>XML Data Type.</returns>
		public static string TypeToXml(ModelType type, object value)
		{
			string lResult = null;
			if (value != null)
			{
				string lvalue = null;
				if (value is TimeSpan)
				{
					lvalue = "0001/01/01 " + value.ToString();
				}
				else
				{
					lvalue = value.ToString();
				}
				try
				{
					switch (type)
					{
					#region Type {AUTONUMERIC,NAT,INT}-> typeof(Int32);
					case ModelType.Autonumeric:
					case ModelType.Nat:
					case ModelType.Int:
						Int32 lInt32 = Int32.Parse(lvalue);
						lResult = XmlConvert.ToString(lInt32);
						break;
					#endregion Type -> typeof(Int32);

					#region Type {STRING,TEXT}-> typeof(string).
					case ModelType.String:
					case ModelType.Text:
					case ModelType.Password:
						lResult = value.ToString();
						break;
					#endregion Type -> typeof(string)

					#region Type -> typeof(decimal).
					case ModelType.Real:
						decimal ldecimal = decimal.Parse(lvalue);
						lResult = XmlConvert.ToString(ldecimal);
						break;
					#endregion Type -> typeof(decimal).

					#region Type {BOOL}-> typeof(Boolean)
					case ModelType.Bool:
						bool lbool = bool.Parse(lvalue);
						lResult = XmlConvert.ToString((bool)value);
						break;
					#endregion Type {BOOL}-> typeof(Boolean).

					#region Type {DATE,DATETIME,TIME}-> typeof(DateTime).
					case ModelType.Date:
						DateTime lDate = DateTime.Parse(lvalue);
						lResult = DateTimeToXmlDate(lDate);
						break;
					case ModelType.DateTime:
						DateTime lDateTime = DateTime.Parse(lvalue);
						lResult = DateTimeToXmlDateTime(lDateTime);
						break;
					case ModelType.Time:
						DateTime lTime = DateTime.Parse(lvalue);
						lResult = DateTimeToXmlTime(lTime);
						break;
					#endregion Type {DATE,DATETIME,TIME}-> typeof(DateTime).

					#region Type {BLOB}-> typeof(byte[])
					case ModelType.Blob:
						if (value.GetType() == typeof(string))
						{
							lResult = value.ToString(); ;
						}
						else
						{
							lResult = System.Convert.ToBase64String(value as byte[]);
						}
						break;
					#endregion Type {BLOB}-> typeof(byte[])
					}
				}
				catch (ArgumentNullException exNull)
				{
					StringBuilder lMessage = new StringBuilder();
					lMessage.Append("Argument value is Null (string type = ");
					lMessage.Append(type);
					lMessage.Append(", ");
					lMessage.Append(value.GetType().ToString());
					lMessage.Append(" value = null");
					lMessage.Append(")");
					throw new ApplicationException(lMessage.ToString(), exNull);
				}
				catch (FormatException exFormat)
				{
					StringBuilder lMessage = new StringBuilder();
					lMessage.Append("Incorrect Type or Format for value Argument (string type = ");
					lMessage.Append(type);
					lMessage.Append(", ");
					lMessage.Append(value.GetType().ToString());
					lMessage.Append(" value = ");
					lMessage.Append(lvalue);
					lMessage.Append(")");
					throw new ApplicationException(lMessage.ToString(), exFormat);
				}
				catch (OverflowException exOverflow)
				{
					StringBuilder lMessage = new StringBuilder();
					lMessage.Append("Overflow in value Argument for the type (string type = ");
					lMessage.Append(type);
					lMessage.Append(", ");
					lMessage.Append(value.GetType().ToString());
					lMessage.Append(" value = ");
					lMessage.Append(lvalue);
					lMessage.Append(")");
					throw new ApplicationException(lMessage.ToString(), exOverflow);
				}
				catch
				{
					throw;
				}
				if (lResult == null)
				{
					throw (new ApplicationException("Convert.TypeToXml -> Data type: " + type + " not found."));
				}
			}
			else
			{
				throw new ArgumentNullException("value", "Argument value can't be null");
			}
			return lResult;
		}
		/// <summary>
		/// Converts .NET Data Type element to XML Data Type representation.
		/// </summary>
		/// <param name="type">Type of dates for MODEL{Autonumeric,Nat,Int,String,Tex,Real,Bool,Date,Time,DateTime}</param>
		/// <param name="value">Typed data or null.</param>
		/// <returns>XML Data Type.</returns>
		public static string TypeToXml(string type, object value)
		{
			return TypeToXml(StringTypeToMODELType(type), value);
		}
		#endregion NET Type To Xml (NI-127).

		#region Xml Data Type to .NET Data Type (or Null typed).
		/// <summary>
		/// Converts XML Data Type element to .NET Nullable Data Type representation.
		/// </summary>
		/// <param name="type">Type of dates for MODEL{Autonumeric, Nat, Int, String, Tex, Real, Bool, Date, Time, DateTime}</param>
		/// <param name="xmlvalue">Value from XML.</param>
		/// <returns>Nullable Typed Data.</returns>
		public static Nullable<T> XmlToType<T>(ModelType type, string xmlvalue)
			where T : struct
		{
			return (Nullable<T>)XmlToType(type, xmlvalue);
		}

		/// <summary>
		/// Converts XML Data Type element to .NET Data Type representation.
		/// </summary>
		/// <param name="type">Type of dates for MODEL{Autonumeric,Nat,Int,String,Tex,Real,Bool,Date,Time,DateTime}</param>
		/// <param name="xmlvalue">Value from XML.</param>
		/// <returns>Typed Data or Typed Data Null.</returns>
		public static object XmlToType(ModelType type, string xmlvalue)
		{
			object lResult = null;

			switch (type)
			{
			// Type -> typeof(Int32);
			case ModelType.Autonumeric:
			case ModelType.Nat:
			case ModelType.Int:
				if (xmlvalue == null)
				{
					lResult = (int?)null;
				}
				else
				{
					lResult = Int32.Parse(xmlvalue);
				}
				break;
			// Type -> typeof(string)
			case ModelType.String:
			case ModelType.Text:
			case ModelType.Password:
				if (xmlvalue == null)
				{
					lResult = (string)null;
				}
				else
				{
					lResult = xmlvalue;
				}
				break;
			// Type -> typeof(decimal);
			case ModelType.Real:
				if (xmlvalue == null)
				{
					lResult = (decimal?)null;
				}
				else
				{

					lResult = decimal.Parse(xmlvalue, System.Globalization.NumberStyles.Float, XMLFormat);
				}
				break;
			// Type -> typeof(Boolean);
			case ModelType.Bool:
				if (xmlvalue == null)
				{
					lResult = (bool?)null;
				}
				else
				{
					lResult = bool.Parse(xmlvalue);
				}
				break;
			// Type -> typeof(DateTime);
			case ModelType.Date:
				lResult = Adaptor.DataFormats.Convert.XmlDateToDateTime(xmlvalue);
				break;
			case ModelType.DateTime:
				lResult = Adaptor.DataFormats.Convert.XmlDateTimeToDateTime(xmlvalue);
				break;
			case ModelType.Time:
				lResult = Adaptor.DataFormats.Convert.XmlTimeToTimeSpan(xmlvalue);
				break;
			case ModelType.Blob:
				if (xmlvalue == null)
				{
					lResult = null;
				}
				else
				{
					lResult = System.Convert.FromBase64String(xmlvalue);
				}
				break;

			default:
				throw (new ApplicationException("XmlToType -> Data type: " + type + " not found."));
			}
			return lResult;
		}
		/// <summary>
		/// Converts XML Data Type element to .NET Data Type representation.
		/// </summary>
		/// <param name="type">Type of dates for MODEL{Autonumeric,Nat,Int,String,Tex,Real,Bool,Date,Time,DateTime}</param>
		/// <param name="xmlvalue">Value from XML.</param>
		/// <returns>Typed Data or Typed Data Null.</returns>
		public static object XmlToType(string type, string xmlvalue)
		{
			return XmlToType(StringTypeToMODELType(type), xmlvalue);
		}
		#endregion Xml Type to .NET Type (or Null typed).

		#region Mappings ModelType to Type Net.
		/// <summary>
		/// Maps ModelType type to .NET Type.
		/// </summary>
		/// <param name="type">ModelType type.</param>
		/// <returns>.NET Type.</returns>
		public static Type MODELTypeToNetType(ModelType type)
		{
			Type lResult = null;
			switch (type)
			{
			// Type -> typeof(Int32);
			case ModelType.Autonumeric:
				lResult = typeof(Int32); // or typeof(UInt32)
				break;
			case ModelType.Nat:
			case ModelType.Int:
				lResult = typeof(Int32);
				break;
			// Type -> typeof(string)
			case ModelType.String:
			case ModelType.Text:
				lResult = typeof(string);
				break;
			// Type -> typeof(decimal);
			case ModelType.Real:
				lResult = typeof(decimal);
				break;
			// Type -> typeof(Boolean);
			case ModelType.Bool:
				lResult = typeof(bool);
				break;
			// Type -> typeof(DateTime);
			case ModelType.Date:
				lResult = typeof(DateTime);
				break;
			case ModelType.DateTime:
				lResult = typeof(DateTime);
				break;
			// Type -> typeof(TimeSpan)
			case ModelType.Time:
				lResult = typeof(TimeSpan);
				break;
			case ModelType.Blob:
				lResult = typeof(Byte[]);
				break;
			}
			return lResult;
		}
		/// <summary>
		/// Maps ModelType type to .NET Type.
		/// </summary>
		/// <param name="type">ModelType type.</param>
		/// <returns>.NET Type.</returns>
		public static Type MODELTypeToNetType(string type)
		{
			return MODELTypeToNetType(StringTypeToMODELType(type));
		}
		#endregion Mappings Type Net.
	}
}

