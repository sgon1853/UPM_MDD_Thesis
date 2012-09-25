// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when an element of the XML request is not valid.
	/// </summary>
	public class ONXMLException : ONException
	{

		public ONXMLException(Exception innerException, string xml) 
			: base(innerException, 36)
		{
			KeyList.Add("xml", xml);
			mMessage = ONErrorText.XMLError;
			mMessage = mMessage.Replace("${xml}", xml);
		}
	}
}

