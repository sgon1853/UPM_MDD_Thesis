// 3.4.4.5

using System.Runtime.Remoting.Messaging;
using System;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// IONContextArgumentAttribute.
	/// </summary>
	public interface IONContextArgumentAttribute
	{
		int IndexArgument
		{
			get; set;
		}

		void Preprocess(MarshalByRefObject inst, IMessage msg);
		void Postprocess(MarshalByRefObject inst, IMessage msg, ref IMessage msgReturn);
		void Exceptionprocess(MarshalByRefObject inst, IMessage msg, Exception exception);
	}
}

