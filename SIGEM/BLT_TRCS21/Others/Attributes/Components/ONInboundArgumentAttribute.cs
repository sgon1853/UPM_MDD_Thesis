// 3.4.4.5

using SIGEM.Business.Types;
using SIGEM.Business.Exceptions;
using SIGEM.Business.Action;
using System;
using System.Runtime.Remoting.Messaging;

namespace SIGEM.Business.Attributes
{
	/// <summary>
	/// Attribute related with the inbounds arguments defined in the model object.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter)]
	internal class ONInboundArgumentAttribute: Attribute, IONContextArgumentAttribute
	{
		#region Members
		public string IdArgument;
		public string Name;
		public string Type;
		public bool AllowsNull;
		public int Length;
		public string IdService;
		public string ServiceName;
		public string ClassName;
		public string IdClass;
		private int mIndexArgument;
		#endregion

		#region	Properties
		public int IndexArgument
		{
			get
			{
				return mIndexArgument;
			}
			set
			{
				mIndexArgument = value;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="idArgument">Identification of the argument</param>
		/// <param name="argumentName">Name of the argument</param>
		/// <param name="type">Type of the argument</param>
		/// <param name="idService">Identification of the service that has the argument</param>
		/// <param name="serviceName">Name of the service</param>
		/// <param name="idClass">Identification of the class that the service is defined</param>
		/// <param name="className">Name of the class</param>
		public ONInboundArgumentAttribute(string idArgument, string argumentName, string type, string idService, string serviceName, string idClass, string className)
		{
			IdArgument = idArgument;
			Name = argumentName;
			Type = type;
			IdService = idService;
			ServiceName = serviceName;
			AllowsNull = true;
			Length = 0;
			IdClass = idClass;
			ClassName = className;
		}
		#endregion

		#region Interface IONContextServiceAttribute Methods
		/// <summary>
		/// Method executed before the code related to the attribute. The action is to check if the value is null
		/// </summary>
		public void Preprocess(MarshalByRefObject inst, IMessage msg)
		{
			// Extract Action
			ONAction lAction = inst as ONAction;

			// Shared Event Control
			if ((lAction == null) || (lAction.OnContext.InSharedContext == true)) // No-Principal Shared Event
				return;

			// Null Check
			IMethodCallMessage lMsg = msg as IMethodCallMessage;
			IONType lArgument = lMsg.Args[IndexArgument] as IONType;
			if (lArgument == null || lArgument.Value == null)
			{
				if (AllowsNull == false)
					throw new ONNotNullArgumentException(null, IdService, IdClass, IdArgument, ServiceName, ClassName, Name);
			}
			else if ((Length > 0) && (lArgument.Value.ToString().Length > Length))
				throw new ONMaxLenghtArgumentException(null, IdArgument, Name, Length.ToString());
		}
		public void Postprocess(MarshalByRefObject inst, IMessage msg, ref IMessage msgReturn)
		{	
		}
		public void Exceptionprocess(MarshalByRefObject inst, IMessage msg, Exception exception)
		{
		}
		#endregion
	}
}

