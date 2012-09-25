// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when trying to delete an already related instance.
	/// </summary>
	public class ONStaticException : ONException
	{
		public ONStaticException(Exception innerException, string aliasClass, string idClass, string aliasRol, string idRol)
			: base(innerException, 11)
		{
			KeyList.Add("class", idClass);
			KeyList.Add("role", idRol);
			mMessage = ONErrorText.Static;
			mMessage = mMessage.Replace("${class}", aliasClass);
			mMessage = mMessage.Replace("${role}", aliasRol);
		}
	}
}

