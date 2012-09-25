// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when a related instance does not exist.
	/// </summary>
	public class ONRelatedNotExistException : ONException
	{
		
		public ONRelatedNotExistException(Exception innerException, string idRole, string role)
			: base(innerException, 9)
		{
			KeyList.Add("role", idRole);
			mMessage = ONErrorText.RelatedNotExists;
			mMessage = mMessage.Replace("${role}", role);
		}
	}
}

