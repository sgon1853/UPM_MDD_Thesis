// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Data;
using System.Data.Common;

using SIGEM.Client.Oids;
using SIGEM.Client.Adaptor.DataFormats;
using SIGEM.Client.Adaptor.Exceptions;

namespace SIGEM.Client.Adaptor
{
	#region ServerConnection
	public partial class ServerConnection
	{
		#region Service Methods

		#region Service Methods [Private]
		/// <summary>
		/// Executes a service with the arguments required by the service packed in a ServiceRequest.
		/// </summary>
		/// <param name="agent">Agent who execute the service.</param>
		/// <param name="serviceRequest">Service to execute.</param>
		/// <returns>Execution response.</returns>
		private Response ExecuteService(Oid agent, ServiceRequest serviceRequest)
		{
			return this.Send(new Request(serviceRequest, agent));
		}
		/// <summary>
		/// Executes a service with the arguments indicated as Arguments.
		/// </summary>
		/// <param name="agent">Agent who execute the service.</param>
		/// <param name="className">Class involved in the service.</param>
		/// <param name="service">Service to execute.</param>
		/// <param name="arguments">Arguments.</param>
		/// <returns>Arguments of the service.</returns>
		private Arguments ExecuteService(Oid agent, string className, string service, Arguments arguments, ChangeDetectionItems changeDetectinItems)
		{

			ServiceRequest lServiceRequest = new ServiceRequest(className, service, arguments);

			lServiceRequest.ChangeDetectionItems = changeDetectinItems;

			Response lResponse = ExecuteService(agent, lServiceRequest);
			if (agent == null)
			{
				agent = lResponse.Service.Oid;
			}
			return lResponse.Service.Arguments;
		}
		#endregion Service Methods [Private]

		#region ExecuteService
		/// <summary>
		/// Executes a service with the arguments indicated as two collections (types and values).
		/// </summary>
		/// <param name="agent">Agent who execute the service.</param>
		/// <param name="className">Class involved in the service.</param>
		/// <param name="serviceName">Service executed.</param>
		/// <param name="argumentTypes">Arguments types.</param>
		/// <param name="argumentsValues">Arguments values.</param>
		/// <returns>Arguments as a collection.</returns>
		public Dictionary<string, object> ExecuteService(
			Oid agent,
			string className,
			string serviceName,
			Dictionary<string, ModelType> argumentTypes,
			Dictionary<string, object> argumentsValues,
			Dictionary<string, string> argumentDomains,
			Dictionary<string, ModelType> changedItemsTypes,
			Dictionary<string, object> changedItemsValues,
			Dictionary<string, string> changedItemsDomains
			)
		{
			Dictionary<string, object> lResult = null;
			Arguments arguments = new Arguments(argumentTypes, argumentsValues, argumentDomains );

			ChangeDetectionItems changedItems = null;
			if ( (changedItemsTypes!= null) && (changedItemsValues != null) && (changedItemsDomains != null) )
			{
				changedItems = new ChangeDetectionItems(changedItemsTypes, changedItemsValues, changedItemsDomains);
			}

			Arguments lOutPutArguments = ExecuteService(agent, className, serviceName, arguments, changedItems);

			if (lOutPutArguments != null)
			{
				lResult = lOutPutArguments.GetValues();
			}
			return lResult;
		}
		#region Overload method
		public Dictionary<string, object> ExecuteService(
		   Oid agent,
		   string className,
		   string serviceName,
		   Dictionary<string, ModelType> argumentTypes,
		   Dictionary<string, object> argumentsValues,
		   Dictionary<string, string> argumentDomains
		   )
		{
			return ExecuteService(agent, className, serviceName, argumentTypes, argumentsValues, argumentDomains, null, null, null);
		} 
		#endregion Overload method
		#endregion ExecuteService

		#region Global Service
		/// <summary>
		/// Executes a service with no class involved.
		/// </summary>
		/// <param name="agent">Agent who execute the service.</param>
		/// <param name="service">Service executed.</param>
		/// <param name="argumentTypes">Arguments types.</param>
		/// <param name="argumentsValues">Arguments values.</param>
		/// <returns>Arguments as a collection.</returns>
		public Dictionary<string, object> ExecuteService(
			Oid agent,
			string service,
			Dictionary<string, ModelType> argumentTypes,
			Dictionary<string, object> argumentsValues,
			Dictionary<string, string> argumentsDomains, 
			Dictionary<string, ModelType> changedItemsTypes, 
			Dictionary<string, object> changedItemsValues, 
			Dictionary<string, string> changedItemsDomains 
			)
		{
			return ExecuteService(agent, string.Empty, service, argumentTypes, argumentsValues, argumentsDomains, changedItemsTypes, changedItemsValues, changedItemsDomains);
		}

		#region Overload method
		public Dictionary<string, object> ExecuteService(
		   Oid agent,
		   string service,
		   Dictionary<string, ModelType> argumentTypes,
		   Dictionary<string, object> argumentsValues,
		   Dictionary<string, string> argumentsDomains
		   )
		{
			return ExecuteService(agent, string.Empty, service, argumentTypes, argumentsValues, argumentsDomains, null, null, null);
		} 
		#endregion Overload method
		#endregion Global Service

		#endregion Service Methods

		#region User Service Method
		/// <summary>
		/// Executes an specific function, indicated as an argument.
		/// </summary>
		/// <param name="agent">Agent who execute the function.</param>
		/// <param name="functionName">Function executed.</param>
		/// <param name="argumentTypes">Argument types.</param>
		/// <param name="argumentsValues">Argument values.</param>
		/// <param name="argumentsDomains">Argument domains.</param>
		/// <returns>Arguments as a collection.</returns>
		public Dictionary<string, object> ExecuteFunction(
			Oid agent,
			string functionName,
			Dictionary<string, ModelType> argumentTypes,
			Dictionary<string, object> argumentsValues,
			Dictionary<string, string> argumentsDomains)
		{
			return ExecuteService(agent, "FUNCTION", functionName, argumentTypes, argumentsValues, argumentsDomains, null, null, null);
		}
		/// <summary>
		/// Executes an specific function, indicated as an argument.
		/// </summary>
		/// <typeparam name="T">Output type.</typeparam>
		/// <param name="agent">Agent who execute the function.</param>
		/// <param name="functionName">Function executed.</param>
		/// <param name="argumentTypes">Argument types.</param>
		/// <param name="argumentsValues">Argument values.</param>
		/// <returns>Returns an object of type T.</returns>
		public T ExecuteFunction<T>(
			Oid agent,
			string functionName,
			Dictionary<string, ModelType> argumentTypes,
			Dictionary<string, object> argumentsValues,
			Dictionary<string, string> argumentDomains
			)
		{
			Dictionary<string, object> lOutBoundArgument =  ExecuteFunction(agent, functionName, argumentTypes, argumentsValues, argumentDomains);
			T lResult = (T)lOutBoundArgument[functionName];
			return  lResult;
		}
		#endregion User Service Method

		#region Authenticate
		/// <summary>
		/// Authenticates the agent with his password.
		/// </summary>
		/// <param name="agent">Agent.</param>
		/// <param name="password">Password.</param>
		/// <returns>True or false, if accepts or not.</returns>
		public bool Authenticate(Oid agent, string password)
		{
			Arguments lArguments = new Arguments();
			lArguments.Add("Agent", ModelType.Oid, agent, agent.GetOid().ClassName);
			lArguments.Add("password", ModelType.String, password, string.Empty);
			ServiceRequest serviceAutenticateRequest = new ServiceRequest(agent.GetOid().ClassName, VALIDATION_SERVICE_NAME, lArguments);
			// Call Service.
			// Exception: 'MVAgentValidation' service does not need Agent.
			Response resultResponse = this.ExecuteService((Oid)agent.GetOid(), serviceAutenticateRequest);
			// Once the agent is valid, set the agent Oid.
			agent.GetOid().SetValues(resultResponse.Service.Oid.GetValues());

			// If didn't throw exception, the Authentication is OK.
			return true;
		}
		#endregion Authenticate

		#region Change PassWord
		/// <summary>
		/// Changes the password of the current agent.
		/// </summary>
		/// <param name="agent">Agent.</param>
		/// <param name="oldPassword">Old password.</param>
		/// <param name="newPassword">New password.</param>
		/// <returns>True or false, if success or not.</returns>
		public bool ChangePassWord(Oid agent, string oldPassword, string newPassword)
		{
			Arguments lArguments = new Arguments();
			lArguments.Add("OLDPASSWORD", ModelType.Password, oldPassword, string.Empty);
			lArguments.Add("NEWPASSWORD", ModelType.Password, newPassword, string.Empty);
			Arguments lResult = ExecuteService(agent, agent.ClassName, CHANGE_PWD_SERVICE_NAME, lArguments, null);

			//If don't throw exception, the Authentication is OK.
			return true;
		}
		#endregion Change PassWord
	}
	#endregion ServerConnection
}

