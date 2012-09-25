// 3.4.4.5
using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when a field of an Oid is not valid in the XML request.
	/// </summary>
	public class ONXMLAlternateKeyFieldException : ONException
	{
		public ONXMLAlternateKeyFieldException(Exception innerException, string idField, string field, string idClass, string className, string idAlternateKey, string alternateKeyName)
			: base(innerException, 51)
		{
			KeyList.Add("field", idField);
			KeyList.Add("alternateKey", idAlternateKey);
			KeyList.Add("class", idClass);
			mMessage = ONErrorText.AlternateKeyFieldFailure;
			mMessage = mMessage.Replace("${field}", field);
			mMessage = mMessage.Replace("${alternateKey}", alternateKeyName);
			mMessage = mMessage.Replace("${class}", className);

		}

		public override int Code
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
				return mMessage;
			}
		}
	}
}
