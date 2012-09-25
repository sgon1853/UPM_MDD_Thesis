// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when the agent is not valid or does not exist.
	/// </summary>
	public class ONAgentValidationException : ONException
	{	
		public ONAgentValidationException(Exception innerException)
			: base(innerException, 41)
		{
			mMessage = ONErrorText.AgentValidationFailure;
		}
		
	}
}

