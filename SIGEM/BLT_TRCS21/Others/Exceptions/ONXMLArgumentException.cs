// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when an argument of the XML request is not valid.
	/// </summary>
	public class ONXMLArgumentException : ONException
	{
		public ONXMLArgumentException(System.Exception innerException, string param)
			: base(innerException, 36)
		{
			KeyList.Add("param", param);
			mMessage = ONErrorText.XMLError;
			mMessage = mMessage.Replace("${param}", param);
		}

	}
}

