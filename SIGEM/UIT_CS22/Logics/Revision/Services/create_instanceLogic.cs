// v3.8.4.5.b
using System;
using System.Collections;
using System.Data;
using System.Collections.Specialized;
using System.Collections.Generic;

using SIGEM.Client.Adaptor;
using SIGEM.Client.Oids;
using SIGEM.Client.Presentation;

namespace SIGEM.Client.Logics.Revision.Services
{
	/// <summary>
	/// Class that solves logic of 'create_instance' service.
	/// </summary>
	public static class create_instanceLogic
	{
		#region Auxiliar methods 
		/// <summary>
		/// Gets a list with the arguments values.
		/// </summary>
		/// <param name="p_atrid_RevisarAeronaveArg">Value of the inbound argument 'p_atrid_RevisarAeronave'.</param>
		/// <param name="p_atrFechaRevisionArg">Value of the inbound argument 'p_atrFechaRevision'.</param>
		/// <param name="p_atrNombreRevisorArg">Value of the inbound argument 'p_atrNombreRevisor'.</param>
		/// <param name="p_atrId_AeronaveArg">Value of the inbound argument 'p_atrId_Aeronave'.</param>
		/// <returns>List of inbound arguments values.</returns>
		private static Dictionary<string, object> GetInputFieldValues(int? p_atrid_RevisarAeronaveArg, DateTime? p_atrFechaRevisionArg, string p_atrNombreRevisorArg, string p_atrId_AeronaveArg)
		{
			// Fill values dictionary.
			Dictionary<string, object> lValues = new Dictionary<string, object>();
			lValues.Add("p_atrid_RevisarAeronave", p_atrid_RevisarAeronaveArg);
			lValues.Add("p_atrFechaRevision", p_atrFechaRevisionArg);
			lValues.Add("p_atrNombreRevisor", p_atrNombreRevisorArg);
			lValues.Add("p_atrId_Aeronave", p_atrId_AeronaveArg);

			return lValues;
		}
		/// <summary>
		/// Gets a list with the arguments types.
		/// </summary>
		/// <returns>List of inbound arguments types.</returns>
		public static Dictionary<string, ModelType> GetInboundArgumentTypes()
		{
			// Fill types dictionary.
			Dictionary<string, ModelType> lTypes = new Dictionary<string, ModelType>();
			lTypes.Add("p_atrid_RevisarAeronave", ModelType.Autonumeric);
			lTypes.Add("p_atrFechaRevision", ModelType.Date);
			lTypes.Add("p_atrNombreRevisor", ModelType.String);
			lTypes.Add("p_atrId_Aeronave", ModelType.String);

			return lTypes;
		}
		/// <summary>
		/// Gets a list with the arguments domains.
		/// </summary>
		/// <returns>List of inbound arguments domains.</returns>
		public static Dictionary<string, string> GetInboundArgumentDomains()
		{
			// Fill types dictionary.
			Dictionary<string, string> lDomains = new Dictionary<string, string>();

			return lDomains;
		}
		/// <summary>
		/// Gets a list with the arguments types.
		/// </summary>
		/// <returns>List of outbound arguments types.</returns>
		public static Dictionary<string, ModelType> GetOutboundArgumentTypes()
		{
			// Fill types dictionary.
			Dictionary<string, ModelType> lTypes = new Dictionary<string, ModelType>();

			return lTypes;
		}
		/// <summary>
		/// Gets a list with the arguments domains.
		/// </summary>
		/// <returns>List of inbound arguments domains.</returns>
		public static Dictionary<string, string> GetOutboundArgumentDomains()
		{
			// Fill types dictionary.
			Dictionary<string, string> lDomains = new Dictionary<string, string>();

			return lDomains;
		}
		#endregion Auxiliar methods
		
		#region Execute Default Values
		/// <summary>
		/// Method that solves the inbound arguments default values.
		/// </summary>
		/// <param name="context">Current context.</param>
		public static void ExecuteDefaultValues(IUServiceContext context)
		{


		}
		#endregion Execute Default Values
		
		#region Execute Preload

		#endregion Execute Preload
		
		#region Execute Load From Context
		/// <summary>
		/// Initialize the inbound arguments based in the context information.
		/// </summary>
		/// <param name="context">Current service context.</param>
		public static void ExecuteLoadFromContext(IUServiceContext context)
		{
			int depRulesCounter  = Properties.Settings.Default.DependencyRulesCounter;
			DependencyRulesCache depRulesCache = new DependencyRulesCache();

			// If Exchange information does not exist, none initialization can be done.
			if (context == null || context.ExchangeInformation == null)
			{
				return;
			}

			// Argument previous value. Common for all of them.
			object previousValue;


			#region Aggregation relationships initializations
			// Obtain data from the last navigation, in order to initialize object-valued arguments that represent aggregation relationships.
			string lLastNavigationRole = context.ExchangeInformation.GetLastNavigationRole();

			if (lLastNavigationRole != "")
			{

			}
			#endregion Aggregation relationships initializations

			#region Manual initializations
			// Search in context for initializations done by programmers, in order to achieve special behaviours.
			foreach(KeyValuePair<string,object> argument in context.ExchangeInformation.CustomData)
			{
				previousValue = context.GetInputFieldValue(argument.Key);

				object lCustomArgumentValue = argument.Value;


				context.SetInputFieldValue(argument.Key, lCustomArgumentValue);
				// Check SetValue dependency rules
				context.SelectedInputField = argument.Key;
				ExecuteDependencyRules(context, previousValue, DependencyRulesEventLogic.SetValue, DependencyRulesAgentLogic.Internal, ref depRulesCounter, depRulesCache);
				context.SelectedInputField = string.Empty;
			}
			#endregion Manual initializations

			#region Arguments initializations taking into account context information
			#endregion Arguments initializations taking into account context information
		}
		#endregion Execute Load From Context
		
		#region Conditional Navigation
		/// <summary>
		/// Solve the conditional navigation of 'create_instance' service.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <returns>A ExchangeInfoConditionalNavigation object indicating the target scenario and its initializations.</returns>
		public static ExchangeInfoConditionalNavigation ExecuteConditionalNavigation(IUServiceContext context)
		{
			return null; // No conditional navigations are defined for this service IU.
		}
		#endregion Conditional Navigation
		#region Execute Dependency Rules
		/// <summary>
		/// Solves the dependency rules of 'create_instance' inbound arguments, updating the context received as parameter.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <param name="oldValue">Old value of the modified argument.</param>
		/// <param name="dependencyRulesEvent">Event that has been thrown (SetValue, SetEnabled).</param>
		/// <param name="dependencyRulesAgent">Agent that has thrown the event (User, Internal).</param>
		public static void ExecuteDependencyRules(IUServiceContext context, object oldValue, DependencyRulesEventLogic dependencyRulesEvent, DependencyRulesAgentLogic dependencyRulesAgent)
		{

			int lDependencyRulesCounter = Properties.Settings.Default.DependencyRulesCounter;
			DependencyRulesCache depRulesCache = new DependencyRulesCache();
			ExecuteDependencyRules(context, oldValue, dependencyRulesEvent, dependencyRulesAgent, ref lDependencyRulesCounter, depRulesCache);
		}
		/// <summary>
		/// Solves the dependency rules of 'create_instance' inbound arguments, updating the context received as parameter.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <param name="oldValue">Old value of the modified argument.</param>
		/// <param name="dependencyRulesEvent">Event that has been thrown (SetValue, SetEnabled).</param>
		/// <param name="dependencyRulesAgent">Agent that has thrown the event (User, Internal).</param>
		/// <param name="dependencyRulesCounter">Counter that controls the number of dependency rules executed in order to avoid infinite loops.</param>
		/// <param name="depRulesCache">Cache in this dependency rule.</param>
		private static void ExecuteDependencyRules(IUServiceContext context, object oldValue, DependencyRulesEventLogic dependencyRulesEvent, DependencyRulesAgentLogic dependencyRulesAgent, ref int dependencyRulesCounter, DependencyRulesCache depRulesCache)
		{

			// Check if the receiver object-valued argument has more than one Oid.
			if (context.InputFields[context.SelectedInputField].Value is List<Oid>)
			{
				if (((List<Oid>)context.InputFields[context.SelectedInputField].Value).Count > 1)
				{
					// If the receiver object-valued argument has more than one Oid,
					// do not execute dependency rules.
					return;
				}
			}

			switch (context.SelectedInputField)
			{
				default:
					break;
			}
		}
		#endregion Execute Dependency Rules
		
		#region Execute Service
		/// <summary>
		/// Method that solves the execution of 'create_instance' service.
		/// </summary>
		/// <param name="context">Current context.</param>
		public static void ExecuteService(IUServiceContext context)
		{
			context.OutputFields = ExecuteService(context.Agent, context.GetInputFieldValue("p_atrid_RevisarAeronave") as int?, context.GetInputFieldValue("p_atrFechaRevision") as DateTime?, context.GetInputFieldValue("p_atrNombreRevisor") as string, context.GetInputFieldValue("p_atrId_Aeronave") as string);
		}
		/// <summary>
		/// Method that solves the execution of 'create_instance' service.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="argumentValues">Inbound argument values.</param>
		/// <returns>Outbound argument values.</returns>
		public static Dictionary<string, object> ExecuteService(Oid agent,  Dictionary<string, object> argumentValues)
		{
			return ExecuteService(agent, argumentValues["p_atrid_RevisarAeronave"] as int?, argumentValues["p_atrFechaRevision"] as DateTime?, argumentValues["p_atrNombreRevisor"] as string, argumentValues["p_atrId_Aeronave"] as string);
		}
		/// <summary>
		/// Method that solves the execution of 'create_instance' service.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="p_atrid_RevisarAeronaveArg">Value of the inbound argument 'p_atrid_RevisarAeronave'.</param>
		/// <param name="p_atrFechaRevisionArg">Value of the inbound argument 'p_atrFechaRevision'.</param>
		/// <param name="p_atrNombreRevisorArg">Value of the inbound argument 'p_atrNombreRevisor'.</param>
		/// <param name="p_atrId_AeronaveArg">Value of the inbound argument 'p_atrId_Aeronave'.</param>
		/// <returns>Outbound argument values.</returns>
		public static Dictionary<string, object> ExecuteService(Oid agent, int? p_atrid_RevisarAeronaveArg, DateTime? p_atrFechaRevisionArg, string p_atrNombreRevisorArg, string p_atrId_AeronaveArg)
		{
			Dictionary<string, object> lValues = GetInputFieldValues(p_atrid_RevisarAeronaveArg, p_atrFechaRevisionArg, p_atrNombreRevisorArg, p_atrId_AeronaveArg);
			Dictionary<string, ModelType> lTypes = GetInboundArgumentTypes();
			Dictionary<string, string> lDomains = GetInboundArgumentDomains();

			Dictionary<string, object> lOutboundArguments = Logic.Adaptor.ExecuteService(agent, "Revision", "create_instance", lTypes, lValues, lDomains);
			return UtilFunctions.ProcessOutboundArgsList(lOutboundArguments);
		}
		#endregion Execute Service
		
		#region Execute Validate Value
		/// <summary>
		/// Solves the validation of inbound arguments of 'create_instance' service.
		/// </summary>
		/// <param name="context">Current context.</param>
		public static void ExecuteValidateValue(IUServiceContext context)
		{
			switch (context.SelectedInputField)
			{	
				case "p_atrid_RevisarAeronave":
					ExecuteValidateValuep_atrid_RevisarAeronave(context.Agent, context.GetInputFieldValue("p_atrid_RevisarAeronave") as int?);
					break;
				case "p_atrFechaRevision":
					ExecuteValidateValuep_atrFechaRevision(context.Agent, context.GetInputFieldValue("p_atrFechaRevision") as DateTime?);
					break;
				case "p_atrNombreRevisor":
					ExecuteValidateValuep_atrNombreRevisor(context.Agent, context.GetInputFieldValue("p_atrNombreRevisor") as string);
					break;
				case "p_atrId_Aeronave":
					ExecuteValidateValuep_atrId_Aeronave(context.Agent, context.GetInputFieldValue("p_atrId_Aeronave") as string);
					break;
				default:
					break;
			}
		}
		/// <summary>
		/// Solves the validation of the inbound arguments of 'create_instance' service specified as parameter.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="argumentName">Inbound argument name to validate.</param>
		/// <param name="argumentValue">Inbound argument value to validate.</param>
		public static void ExecuteValidateValue(Oid agent, string argumentName, object argumentValue)
		{
			switch (argumentName)
			{	
				case "p_atrid_RevisarAeronave":
					ExecuteValidateValuep_atrid_RevisarAeronave(agent, argumentValue as int?);
					break;
				case "p_atrFechaRevision":
					ExecuteValidateValuep_atrFechaRevision(agent, argumentValue as DateTime?);
					break;
				case "p_atrNombreRevisor":
					ExecuteValidateValuep_atrNombreRevisor(agent, argumentValue as string);
					break;
				case "p_atrId_Aeronave":
					ExecuteValidateValuep_atrId_Aeronave(agent, argumentValue as string);
					break;
				default:
					break;
			}
		}
		/// <summary>
		/// Solves the validation of the 'p_atrid_RevisarAeronave' inbound argument.
		/// </summary>
		/// <param name="agent">Application agent</param>
		/// <param name="argumentValue">Value of the inbound argument to validate</param>
		public static void ExecuteValidateValuep_atrid_RevisarAeronave(Oid agent, int? argumentValue)
		{
			// TODO: Validate value
		}
		/// <summary>
		/// Solves the validation of the 'p_atrFechaRevision' inbound argument.
		/// </summary>
		/// <param name="agent">Application agent</param>
		/// <param name="argumentValue">Value of the inbound argument to validate</param>
		public static void ExecuteValidateValuep_atrFechaRevision(Oid agent, DateTime? argumentValue)
		{
			// TODO: Validate value
		}
		/// <summary>
		/// Solves the validation of the 'p_atrNombreRevisor' inbound argument.
		/// </summary>
		/// <param name="agent">Application agent</param>
		/// <param name="argumentValue">Value of the inbound argument to validate</param>
		public static void ExecuteValidateValuep_atrNombreRevisor(Oid agent, string argumentValue)
		{
			// TODO: Validate value
		}
		/// <summary>
		/// Solves the validation of the 'p_atrId_Aeronave' inbound argument.
		/// </summary>
		/// <param name="agent">Application agent</param>
		/// <param name="argumentValue">Value of the inbound argument to validate</param>
		public static void ExecuteValidateValuep_atrId_Aeronave(Oid agent, string argumentValue)
		{
			// TODO: Validate value
		}
		#endregion Execute Validate Value
		
		#region Get type
		/// <summary>
		/// Method that returns the type of 'create_instance' service.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <returns>Type of the service (enumerate value Event, Transaction, Operation).</returns>
		public static ServiceType GetServiceType(IUServiceContext context)
		{
			// Type = E.
			return ServiceType.Event;
		}
		#endregion Get type
		
		#region Get effect type
		/// <summary>
		/// Gets the effect type of 'create_instance' service.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <returns>Effect type of the service (Creation, Destruction, Modification).</returns>
		public static ServiceEffectType GetServiceEffectType(IUServiceContext context)
		{
			// ServiceType = C.
			return ServiceEffectType.Creation;
		}
		#endregion Get effect type
	}
}
