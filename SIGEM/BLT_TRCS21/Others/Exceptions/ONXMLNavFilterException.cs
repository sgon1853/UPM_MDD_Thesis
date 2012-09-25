// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when the structure of the navigational filter XML request does not follow the DTD or an element is not awaited.
	/// </summary>
	public class ONXMLNavFilterException : ONException
	{

		public ONXMLNavFilterException(Exception innerException, string pXMLTag, string pXMLArg, string pXMLVar)
			: base(innerException, 46)
		{
			string lXMLExpectedtags = pXMLArg + ", " + pXMLVar;
			KeyList.Add("*actualxmltag", pXMLTag);
			KeyList.Add("*expectedxmltags", lXMLExpectedtags);
			mMessage = ONErrorText.XMLNavFilter;
			mMessage = mMessage.Replace("${actualxmltag}", pXMLTag);
			mMessage = mMessage.Replace("${expectedxmltags}", lXMLExpectedtags);
		}
	}
}

