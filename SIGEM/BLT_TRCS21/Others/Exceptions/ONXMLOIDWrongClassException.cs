// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown reading the class from XML request when is not the awaited one.
	/// </summary>
	public class ONXMLOIDWrongClassException : ONException
	{
		public ONXMLOIDWrongClassException(Exception innerException, string idClass, string className, string wrongClassName)
			: base(innerException, 31)
		{
			KeyList.Add("class", idClass);
			KeyList.Add("*wrongclass", wrongClassName);
			mMessage = ONErrorText.OIDProcessWrongClass;
			mMessage = mMessage.Replace("${class}", className);
			mMessage = mMessage.Replace("${wrongclass}", wrongClassName);
		}
	}
}

