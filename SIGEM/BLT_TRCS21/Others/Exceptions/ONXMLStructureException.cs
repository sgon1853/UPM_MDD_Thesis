// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when the structure of the XML request does not follow the DTD or an element is not awaited.
	/// </summary>
	public class ONXMLStructureException : ONException
	{

		public ONXMLStructureException(Exception innerException, string pXMLTag)
			: base(innerException, 30) 
		{
			KeyList.Add("*xmltag", pXMLTag);
			mMessage = ONErrorText.XMLStructure;
			mMessage = mMessage.Replace("${tag}", pXMLTag);
		}
	}
}

