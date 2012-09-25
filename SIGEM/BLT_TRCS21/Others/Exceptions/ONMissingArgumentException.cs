// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when an argument in the XML of the request is missing.
	/// </summary>
	public class ONMissingArgumentException : ONException
	{
		public ONMissingArgumentException(Exception innerException, string idArgument, string argument)
			: base(innerException, 33)
		{
			KeyList.Add("argument", idArgument);
			mMessage = ONErrorText.MissingArgument;
			mMessage = mMessage.Replace("${argument}", argument);
		}
	}
}

