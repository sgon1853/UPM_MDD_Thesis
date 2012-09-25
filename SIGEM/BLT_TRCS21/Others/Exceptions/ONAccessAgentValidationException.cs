// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when the agent does not have permissions.
	/// </summary>
	public class ONAccessAgentValidationException : ONException
	{	
		public ONAccessAgentValidationException(Exception innerException)
			: base(innerException, 43)
		{
			mMessage = ONErrorText.AccessAgentValidationFailure;
		}
		public override int Code
		{
			get
			{
				return mCode;
			}
		}
	}
}


