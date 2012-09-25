// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when an argument in the XML of the request exceeds the maximum length.
	/// </summary>
	public class ONMaxLenghtArgumentException : ONException
	{
		public ONMaxLenghtArgumentException(Exception innerException, string idArgument, string argument, string maxlenght)
			: base(innerException, 35)
		{
			KeyList.Add("argument", idArgument);
			KeyList.Add("*length", maxlenght);
	
			mMessage = ONErrorText.MaxLenghtArgument;
			mMessage = mMessage.Replace("${argument}", argument);
			mMessage = mMessage.Replace("${length}", maxlenght);
		}
	}
}

