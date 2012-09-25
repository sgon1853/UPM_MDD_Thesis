// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when a new related instance already exists.
	/// </summary>
	public class ONRelatedExistException : ONException
	{
		
		public ONRelatedExistException(Exception innerException, string idRole, string role)
			: base(innerException, 8)
		{
			KeyList.Add("role", idRole);
			mMessage = ONErrorText.RelatedExists;
			mMessage = mMessage.Replace("${role}", role);
		}
	}
}

