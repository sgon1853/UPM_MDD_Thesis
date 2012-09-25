// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace SIGEM.Client.Adaptor.Exceptions
{

	#region Response Exception

	#region ErrorParamType(Key | Literal)
	/// <summary>
	/// Enumeration of the differents error types due to parameters.
	/// </summary>
	public enum ErrorParamType
	{
		Key,
		Literal,
	}
	#endregion ErrorParamType(Key | Literal)

	#region ErrorTraceType(Model | External)
	/// <summary>
	/// Enumeration of the differents error types due to traces.
	/// </summary>
	public enum ErrorTraceType
	{
		Model,
		External,
	}
	#endregion ErrorTraceType(Model | External)

	#region ErrorType(Model | External)
	/// <summary>
	/// Enumeration of the differents generic error types.
	/// </summary>
	public enum ErrorType
	{
		Model,
		External,
	}
	#endregion ErrorType(Model | External)

	#region <Error> - > ResponseException
	/// <summary>
	/// Manages a Response Exception.
	/// </summary>
	public class ResponseException : ApplicationException
	{
		public const string ErrorKey = "Key";
		public const string ErrorLiteral = "Literal";

		public const string ErrorModel = "Model";
		public const string ErrorExternal = "External";

		#region Constructors
		/// <summary>
		/// Initializes a new empty instance of ResponseException.
		/// </summary>
		public ResponseException():this(string.Empty,null){}
		/// <summary>
		/// Initializes a new instance of ResponseException.
		/// </summary>
		/// <param name="message">Message to show.</param>
		public ResponseException(string message) : this(message, null) { }
		/// <summary>
		/// Initializes a new instance of ResponseException.
		/// </summary>
		/// <param name="message">Message to show.</param>
		/// <param name="innerException">Inner exception.</param>
		public ResponseException(string message, Exception innerException)
			: base(message, innerException) { this.mErrorMessage = message; }
		#endregion Constructors

		#region Static methods
		#region Request ErrorType to string Name
		/// <summary>
		/// Converts ErrorType element to string representation.
		/// </summary>
		/// <param name="errorType">ErrorType.</param>
		/// <returns>string.</returns>
		public static string ErrorTypeTostring(ErrorType errorType)
		{
			string lResult = string.Empty;
			switch (errorType)
			{
				case ErrorType.External:
					lResult = ErrorExternal;
					break;

				case ErrorType.Model:
					lResult = ErrorModel;
					break;
			}
			return lResult;
		}
		#endregion Request ErrorType to string Name

		#region Request string Name error type to ErrorType
		/// <summary>
		/// Converts string element to ErrorType representation.
		/// </summary>
		/// <param name="errorType">ErrorType string.</param>
		/// <returns>ErrorType.</returns>
		public static ErrorType stringToErrorType(string errorType)
		{
			ErrorType lResult = ErrorType.Model;
			switch (errorType)
			{
				case ErrorExternal:
					lResult = ErrorType.External;
					break;

				case ErrorModel:
					lResult = ErrorType.Model;
					break;
			}

			return lResult;
		}
		#endregion Request string Name error type to ErrorType

		#endregion Static methods

		#region Type aux. Methods
		/// <summary>
		/// Sets the ErrorType in the Exception.
		/// </summary>
		/// <param name="errorType">ErrorType as a string.</param>
		public void SetErrorType(string errorType)
		{
			this.Type = stringToErrorType(errorType);
		}
		/// <summary>
		/// Gets the ErrorType of the Exception.
		/// </summary>
		/// <returns>ErrorType as a string.</returns>
		public string GetErrorType()
		{
			return ErrorTypeTostring(this.Type);
		}
		#endregion Type aux. Methods

		#region Type
		private ErrorType mType = ErrorType.Model;
		/// <summary>
		/// Gets or sets the error type.
		/// </summary>
		public ErrorType Type
		{
			#region get Type
			get
			{
				return mType;
			}
			#endregion get Type
			#region set Type
			set
			{
				mType = value;
			}
		#endregion set Type
		}
		#endregion Type

		#region Number
		/// <summary>
		/// Gets or sets Number, a coded numerical value that is assigned to a specific exception.
		/// </summary>
		public int Number
		{
			get
			{
				return base.HResult;
			}
			set
			{
				base.HResult = value;
			}
		}
		#endregion Number

		#region Message
		private string mErrorMessage = string.Empty;
		/// <summary>
		/// Gets the ErrorMessage of the Exception.
		/// </summary>
		public override string  Message
		{
			get
			{
				return mErrorMessage;
			}
		}
		/// <summary>
		/// Sets the ErrorMessage of the Exception.
		/// </summary>
		/// <param name="message">Message as a string.</param>
		public void SetMessage(string message)
		{
			this.mErrorMessage = message;
		}
		#endregion Message

		#region Parameters
		private Parameters mParameters = null;
		/// <summary>
		/// Gets or sets Parameters.
		/// </summary>
		public Parameters Parameters
		{
			get
			{
				return mParameters;
			}
			set
			{
				mParameters = value;
			}
		}
		#endregion Parameters

		#region Traces
		private Traces mTraces = null;
		/// <summary>
		/// Gets or sets Traces.
		/// </summary>
		public Traces Traces
		{
			get
			{
				return mTraces;
			}
			set
			{
				mTraces = value;
			}
		}
		#endregion Traces
		#region ChangedItems
		private ChangedItems mChangedItems;

		/// <summary>
		/// Gets or sets the changed items.
		/// </summary>
		/// <value>The changed items.</value>
		public ChangedItems ChangedItems
		{
			get { return mChangedItems; }
			set { mChangedItems = value; }
		}
		#endregion ChangedItems
	}
	#endregion ResponseException

	#region Parameter -> <Error.Param>
	/// <summary>
	/// Stores the information related to a specific parameter.
	/// </summary>
	public class Parameter
	{
		public Parameter() { }
		public string Text;
		public string Key;
		public ErrorParamType Type;

		public override string ToString()
		{
			StringBuilder lResult = new StringBuilder();
			lResult.Append("Parameter: Type=");
			lResult.Append(Type.ToString());
			lResult.Append(", Key=");
			lResult.Append(Key);
			lResult.Append(", Message=");
			lResult.Append(Text);
			lResult.Append("\n");
			return lResult.ToString();
		}
	}
	#endregion Parameter -> <Error.Param>

	#region Trace -> <Error.TraceItem>
	/// <summary>
	/// Stores the information related to a specific trace.
	/// </summary>
	public class Trace
	{
		public string Message = string.Empty;
		public Parameters Parameters;
		public ErrorTraceType Type;
		public string Number;

		public override string ToString()
		{
			StringBuilder lResult = new StringBuilder();
			lResult.Append("Error Trace:(");
			lResult.Append("Type=");
			lResult.Append(Type.ToString());
			lResult.Append(", Number=");
			lResult.Append(Number);
			lResult.Append("), Message ");
			lResult.Append(Message);
			lResult.Append("\n");
			lResult.Append("\tParameters:");
			lResult.Append("\n");
			foreach(Parameter i in Parameters)
			{
				lResult.Append("\t -> ");
				lResult.Append(i.ToString());
				lResult.Append("\n");
			}
			return lResult.ToString();
		}
	}
	#endregion Trace -> <Error.TraceItem>

	#region Parameters
	/// <summary>
	/// List of Parameter.
	/// </summary>
	public class Parameters : List<Parameter>
	{
		public Parameters() { }
	}
	 #endregion Parameters.

	#region Traces
	/// <summary>
	/// List of Trace.
	/// </summary>
	public class Traces : List<Trace>
	{
		public Traces() { }
	}
	#endregion Traces

	#endregion Response Exception

	#region Request Exception
	/// <summary>
	/// Manages a Request Exception.
	/// </summary>
	public class RequestException: ApplicationException
	{
		public const string ErrorTypeModel = "Model";
		public const string ErrorTypeExternal = "External";
		/// <summary>
		/// Initializes a new instance of RequestException.
		/// </summary>
		/// <param name="message">Message to show.</param>
		public RequestException(string message) : this(message, null) { }
		/// <summary>
		/// Initializes a new instance of RequestException.
		/// </summary>
		/// <param name="message">Message to show.</param>
		/// <param name="innerException">Inner exception.</param>
		public RequestException(string message, Exception innerException)
		: base(message, innerException) {}

		#region Static methods
		#region Request ErrorType to string Name
		/// <summary>
		/// Converts ErrorType element to string representation.
		/// </summary>
		/// <param name="errorType">ErrorType.</param>
		/// <returns>String.</returns>
		public static string ErrorTypeTostring(ErrorType errorType)
		{
			string lResult = string.Empty;
			switch (errorType)
			{
				case ErrorType.External:
					lResult = ErrorTypeExternal;
					break;

				case ErrorType.Model:
					lResult = ErrorTypeModel;
					break;
			}
			return lResult;
		}
		#endregion Request ErrorType to string Name

		#region Request string Name error type to ErrorType
		/// <summary>
		/// Converts string element to ErrorType representation.
		/// </summary>
		/// <param name="errorType">ErrorType as a string.</param>
		/// <returns>ErrorType.</returns>
		public static ErrorType stringToErrorType(string errorType)
		{
			ErrorType lResult = ErrorType.Model;
			switch(errorType)
			{
				case ErrorTypeExternal:
					lResult = ErrorType.External;
				break;

				case ErrorTypeModel:
					lResult = ErrorType.Model;
				break;
			}

			return lResult;
		}
		#endregion Request string Name error type to ErrorType
		#endregion Static methods

		/// <summary>
		/// Sets ErrorType to a specific exception.
		/// </summary>
		/// <param name="errorType">ErrorType as a string.</param>
		public void SetErrorType(string errorType)
		{
			this.Type = stringToErrorType(errorType);
		}
		/// <summary>
		/// Gets ErrorType of a specific exception.
		/// </summary>
		/// <returns>String.</returns>
		public string GetErrorType()
		{
			return ErrorTypeTostring(this.Type);
		}

		private ErrorType mType = ErrorType.Model;
		/// <summary>
		/// Gets or sets error type.
		/// </summary>
		public ErrorType Type
		{
			get
			{
				return mType;
			}
			set
			{
				mType = value;
			}
		}
		/// <summary>
		/// Gets or sets Number, a coded numerical value that is assigned to a specific exception.
		/// </summary>
		public int Number
		{
			get
			{
				return base.HResult;
			}
			set
			{
				base.HResult = value;
			}
		}
		/// <summary>
		/// Gets the message that explains the current exception.
		/// </summary>
		public new string  Message
		{
			get
			{
				return base.Message;
			}
		}
	}
	#endregion Request Exception
}

