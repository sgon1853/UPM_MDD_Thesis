// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when the execution of a service is not valid in the current state of the instance.
	/// </summary>
	public class ONSTDException : ONException
	{

		public ONSTDException(Exception innerException, string idClass, string className, string idService, string serviceName)
			: base(innerException, 3)
		{
			KeyList.Add("class", idClass);
			KeyList.Add("service", idService);
			mMessage = ONErrorText.STDError;
			mMessage = mMessage.Replace("${class}", className);
			mMessage = mMessage.Replace("${service}", serviceName);
		}
	}
}

