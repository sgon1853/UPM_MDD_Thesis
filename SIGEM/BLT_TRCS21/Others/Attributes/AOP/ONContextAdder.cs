// 3.4.4.5

using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// ONContextAdder.
	/// </summary>
	public class ONContextAdder : IContextProperty, IContributeObjectSink 
	{
		#region Attributes
		private IONContextClassAttribute mOnContextClass;
		#endregion

		#region Constructors
		public ONContextAdder(IONContextClassAttribute onContextClass)
		{
			mOnContextClass = onContextClass;
		}
		#endregion

		#region IContextProperty Methods
		public string Name 
		{
			get 
			{
				return "ONServiceProperty";
			}
		}
		public bool IsNewContextOK( Context newCtx ) 
		{
			return true ;
		}
		public void Freeze(Context newContext)
		{
		}
		#endregion

		#region IContributeObjectSink Methods
		public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
		{
			if (obj.GetType().GetCustomAttributes(typeof(ONContextAttribute), true).Length > 0)
				nextSink = new ONContextInterceptor(obj, nextSink, mOnContextClass);

			return nextSink;
		}
		#endregion
	}
}

