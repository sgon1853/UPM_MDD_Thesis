// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Specialized;
using SIGEM.Business.Types;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when a precondition is not satisfied.
	/// </summary>
	public class ONPreconditionException : ONException
	{	
		public ONPreconditionException(Exception innerException, string idPrecondition, string preconditionMessage, ListDictionary parameters)
			: base(innerException, 4)
		{
			KeyList.Add("precondition", idPrecondition);
			mMessage = ONErrorText.PreconditionFailure;
			mMessage = mMessage.Replace("${precondition}", preconditionMessage);

            foreach (DictionaryEntry lParameterElem in parameters)
            {
                ONString lParameter = lParameterElem.Value as ONString;

                KeyList.Add("*" + lParameterElem.Key, lParameter.TypedValue);
            }
		}
		
	}
}

