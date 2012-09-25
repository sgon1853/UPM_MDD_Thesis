// 3.4.4.5
using System;
using System.Collections;
using System.Collections.Specialized;
using SIGEM.Business.Types;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when a precondition is not satisfied.
	/// </summary>
	public class ONAlternateKeyException : ONException
	{
		public ONAlternateKeyException(Exception innerException, string idAlternateKey, string alternateKeyMessage, ListDictionary parameters)
			: base(innerException, 49)
		{
			KeyList.Add("alternateKey", idAlternateKey);
			mMessage = ONErrorText.AlternateKeyFailure;
			mMessage = mMessage.Replace("${alternateKey}", alternateKeyMessage);

			foreach (DictionaryEntry lParameterElem in parameters)
			{
				ONString lParameter = lParameterElem.Value as ONString;

				KeyList.Add("*" + lParameterElem.Key, lParameter.TypedValue);
			}
		}

	}
}
