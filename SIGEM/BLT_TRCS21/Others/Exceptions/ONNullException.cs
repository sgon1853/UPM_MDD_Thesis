// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when an element has a null value.
	/// </summary>
	public class ONNullException : ONException
	{
		public ONNullException(Exception innerException)
			: base(innerException, 101) 
		{
			mMessage = ONErrorText.NullNotAllowed;
		}
	}
}

