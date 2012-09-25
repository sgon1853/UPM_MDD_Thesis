// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when a new instance already exists.
	/// </summary>
	public class ONInstanceExistException : ONException
	{

		public ONInstanceExistException(Exception innerException, string idClass, string className)
			: base(innerException, 2)
		{
			KeyList.Add("class", idClass);
			mMessage = ONErrorText.InstanceExists;
			mMessage = mMessage.Replace("${class}", className);
		}
	}
}

