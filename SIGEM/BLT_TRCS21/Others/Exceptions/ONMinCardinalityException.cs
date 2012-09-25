// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when a minimum cardinality constraint is not satisfied.
	/// </summary>
	public class ONMinCardinalityException : ONException
	{
		public ONMinCardinalityException(Exception innerException, string aliasClass, string idClass, string aliasRol, string idRol, int cardinality) 
			: base(innerException, 6)
		{
			KeyList.Add("class", idClass);
			KeyList.Add("role", idRol);
			KeyList.Add("*cardinality", cardinality);
			mMessage = ONErrorText.MinCardinality;
			mMessage = mMessage.Replace("${class}", aliasClass);
			mMessage = mMessage.Replace("${role}", aliasRol);
			mMessage = mMessage.Replace("${cardinality}", cardinality.ToString());
		}
	}
}

