// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown reading the XML request when a a service argument is not valid.
	/// </summary>
	public class ONServiceArgumentsException : ONException
	{
		public ONServiceArgumentsException(System.Exception innerException, string idClass, string classAlias, string idService, string serviceAlias)
			: base(innerException, 38)
		{
			KeyList.Add("class", idClass);
			KeyList.Add("service", idService);
			mMessage = ONErrorText.ServiceArguments;
			mMessage = mMessage.Replace("${class}", classAlias);
			mMessage = mMessage.Replace("${service}", serviceAlias);
		}
	}
}

