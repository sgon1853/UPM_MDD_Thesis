// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when an argument in the XML of the request is null and 
	/// it does not allow null values.
	/// </summary>
	public class ONNotNullArgumentException : ONException
	{
		public ONNotNullArgumentException(Exception innerException, string idService, string idClass, string idArgument, string serviceAlias, string classAlias, string argumentAlias)
			: base(innerException, 26)
		{
			KeyList.Add("service", idService);
			KeyList.Add("class", idClass);
			KeyList.Add("argument", idArgument);
			mMessage = ONErrorText.NotNullArgument;
			mMessage = mMessage.Replace("${argument}", argumentAlias);
			mMessage = mMessage.Replace("${service}", serviceAlias);
			mMessage = mMessage.Replace("${class}", classAlias);
		}
	}
}

