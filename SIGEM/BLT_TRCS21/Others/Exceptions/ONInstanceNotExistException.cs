// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when a instance not exists.
	/// </summary>
	public class ONInstanceNotExistException : ONException
	{
		
		public ONInstanceNotExistException(Exception innerException, string idClass, string className)
			: base(innerException, 1)
		{
			KeyList.Add("class", idClass);
			mMessage = ONErrorText.InstanceNotExists;
			mMessage = mMessage.Replace("${class}", className);
		}
	}
}

