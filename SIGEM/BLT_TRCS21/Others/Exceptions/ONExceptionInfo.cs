// 3.4.4.5

using System;
using System.Collections.Specialized;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Keeps the information of an exception.
	/// </summary>
	public class ONExceptionInfo
	{
		public int Code;
		public string Message;
		public HybridDictionary Items;
		public ONExceptionInfo(int code, string message, HybridDictionary items)
		{
			Code = code;
			Message = message;
			Items = items;
		}
	}
}

