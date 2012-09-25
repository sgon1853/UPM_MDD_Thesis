// 3.4.4.5

using System;
using System.Collections.Specialized;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when an argument in the XML of the request is not valid.
	/// </summary>
	public class ONArgumentException : ONException
	{
		public ONArgumentException(Exception innerException, string argName)
			: base(innerException, 37)
		{
			KeyList.Add("*argument", argName);
			mMessage = ONErrorText.ServiceArgument;
			mMessage = mMessage.Replace("${argument}", argName);
		}
	}
}

