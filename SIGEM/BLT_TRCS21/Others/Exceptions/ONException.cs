// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Base class for the rest of the exception classes.
	/// </summary>
	public abstract class ONException : ApplicationException
	{
		#region Members
		// Message of the error
		protected string mMessage;
		//Code of the error
		protected int mCode;
		public HybridDictionary KeyList = new HybridDictionary(true);
		#endregion

		#region Constants
		public const string OID = "OID";
		public const string ARGUMENT = "ARGUMENT";
		#endregion

		#region Constructor
		public ONException(System.Exception innerException, int code)
			:base("", innerException)
		{
			mCode = code;
		}
		#endregion
		
		#region Properties
		public virtual int Code
		{
			get
			{
				if (InnerException != null)
				{
					ONException lONExp = InnerException as ONException;
					if (lONExp != null)
						return lONExp.Code;
					else
					{
						ErrorWrapper lErrorWrapper = new ErrorWrapper(InnerException);
						return lErrorWrapper.ErrorCode;
					}
				}
				else
					return mCode;
			}
		}

		public virtual int OwnCode
		{
			get
			{
				return mCode;
			}
		}

		public override string Message
		{
			get
			{
				if (InnerException != null)
					return InnerException.Message;

				return mMessage;
			}
		}

		public virtual string OwnMessage
		{
			get
			{
				return mMessage;
			}
		}
		
		public virtual HybridDictionary Params
		{
			get
			{
				if (InnerException != null)
				{
					ONException lONExp = InnerException as ONException;
					if (lONExp != null)
						return lONExp.Params;
					else
					{
						return new HybridDictionary();
					}
				}
				else
					return KeyList;
			}
		}

		public virtual HybridDictionary OwnParams
		{
			get
			{
				return KeyList;
			}
		}

		#endregion

		protected string FormatMsg(string errorMsg)
		{
			string lRet = errorMsg;
			string lKey, lValue;
			foreach(DictionaryEntry lItem in KeyList)
			{
				lKey = (string) lItem.Key;
				lKey = "${" + lKey + "}";
				lValue = (string) lItem.Value;
				lRet = lRet.Replace(lKey, lValue);
			}
			return lRet;
		}

		public bool IsExternal()
		{
			if (InnerException != null)
			{
				ONException lONExp = InnerException as ONException;
				if (lONExp != null)
					return lONExp.IsExternal();
				else
					return true;
			}
			else
				return false;
		}

		public void GetErrorList(List<ONExceptionInfo> errorList)
		{
			errorList.Add(new ONExceptionInfo(mCode, mMessage, KeyList));
			if (InnerException != null)
			{
				ONException lONExp = InnerException as ONException;
				if (lONExp != null)
					lONExp.GetErrorList(errorList);
				else
					errorList.Add(new ONExceptionInfo(Code, InnerException.ToString(), null));
			}
		}
	}
}

