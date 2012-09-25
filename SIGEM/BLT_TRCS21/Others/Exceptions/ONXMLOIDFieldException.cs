// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when a field of an Oid is not valid in the XML request.
	/// </summary>
	public class ONXMLOIDFieldException : ONException
	{
		public ONXMLOIDFieldException(Exception innerException, string idClass, string className, string idField, string field)
			: base(innerException, 34)
		{
			KeyList.Add("class", idClass);
			KeyList.Add("field", idField);
			mMessage = ONErrorText.OIDProcessWrongField;
			mMessage = mMessage.Replace("${class}", className);
			mMessage = mMessage.Replace("${field}", field);

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

