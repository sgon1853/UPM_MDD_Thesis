// 3.4.4.5

using System;

namespace SIGEM.Business.Exceptions
{
	/// <summary>
	/// Exception thrown when a value in the XML request does not follow de proper format.
	/// </summary>
	public class ONXMLFormatException : ONException
	{

		public override string Message
		{
			get
			{
				if (!(InnerException is ONException))
					return OwnMessage;

				return base.Message;
			}
		}

		public override int Code
		{
			get
			{
				if (!(InnerException is ONException))
					return OwnCode;

				return base.Code;
			}
		}

		public override System.Collections.Specialized.HybridDictionary Params
		{
			get
			{
				if (!(InnerException is ONException))
					return OwnParams;

				return base.Params;
			}
		}

		public ONXMLFormatException(Exception innerException, string pDataType)
				: base(innerException, 29)
		{
			KeyList.Add("*type", pDataType);
			mMessage = ONErrorText.XMLFormat;
			mMessage = mMessage.Replace("${type}", pDataType);
		}
	}
}

