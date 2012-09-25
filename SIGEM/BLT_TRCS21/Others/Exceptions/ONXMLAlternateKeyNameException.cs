// 3.4.4.5
using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown reading the class from XML request when is not the awaited one.
	/// </summary>
	public class ONXMLAlternateKeyNameException : ONException
	{
		public ONXMLAlternateKeyNameException(Exception innerException, string wrongAlternateKeyName, string idClass, string className)
			: base(innerException, 50)
		{
			KeyList.Add("*alternateKey", wrongAlternateKeyName);
			KeyList.Add("class", className);
			mMessage = ONErrorText.AlternateKeyNameFailure;
			mMessage = mMessage.Replace("${alternateKey}", wrongAlternateKeyName);
			mMessage = mMessage.Replace("${class}", className);
		}
	}
}


