// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when it is impossible to comunicate with the legacy system.
	/// </summary>
	internal class ONErrorWithLegacySystem : ONException
	{	
		public ONErrorWithLegacySystem(Exception innerException)
			: base(innerException, 15)
		{
			mMessage = ONErrorText.ErrorWithLegacySystem;
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

