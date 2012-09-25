// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when trying to create an instance and it's been been relating using static relations
	/// </summary>
	public class ONStaticCreationException : ONException
	{
		public ONStaticCreationException(Exception innerException, string aliasClass, string idClass, string aliasRol, string idRol)
			: base(innerException, 47)
		{
			KeyList.Add("class", idClass);
			KeyList.Add("role", idRol);
			mMessage = ONErrorText.StaticCreation;
			mMessage = mMessage.Replace("${class}", aliasClass);
			mMessage = mMessage.Replace("${role}", aliasRol);
		}
	}
}

