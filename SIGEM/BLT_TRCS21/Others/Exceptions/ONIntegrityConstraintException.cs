// 3.4.4.5

using System;
using System.Collections;
using System.Collections.Specialized;
using SIGEM.Business.Types;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when an integrity constraint is not satisfied.
	/// </summary>
	public class ONIntegrityConstraintException : ONException
	{
		public ONIntegrityConstraintException(Exception innerException, string idConstraint, string constraintMessage, ListDictionary parameters)
			: base(innerException, 5)
		{
			KeyList.Add("constraint", idConstraint);
			mMessage = ONErrorText.StaticConstraintFailure;
			mMessage = mMessage.Replace("${constraint}", constraintMessage);

            foreach (DictionaryEntry lParameterElem in parameters)
            {
                ONString lParameter = lParameterElem.Value as ONString;

                KeyList.Add("*" + lParameterElem.Key, lParameter.TypedValue);
            }


		}
	}
}

