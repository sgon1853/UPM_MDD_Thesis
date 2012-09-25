// 3.4.4.5

using System.Runtime.Remoting.Messaging;
using System;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// IONContextClassAttribute.
	/// </summary>
	public interface IONContextClassAttribute
	{
		void Preprocess(MarshalByRefObject inst, IMessage msg);
		void Postprocess(MarshalByRefObject inst, IMessage msg, ref IMessage msgReturn);
		void Exceptionprocess(MarshalByRefObject inst, IMessage msg, Exception exception);
	}
}

