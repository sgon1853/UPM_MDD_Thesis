// v3.8.4.5.b
using System;
using System.Collections;
using System.Data;
using System.Collections.Specialized;
using System.Collections.Generic;

using SIGEM.Client.Adaptor;
using SIGEM.Client.Oids;
using SIGEM.Client.Presentation;

namespace SIGEM.Client.Logics.RevisionPasajero.Services
{
	/// <summary>
	/// Class that solves logic of 'delete_instance' service.
	/// </summary>
	public static class delete_instanceLogic
	{
		#region Auxiliar methods 
		/// <summary>
		/// Gets a list with the arguments values.
		/// </summary>
		/// <param name="p_thisRevisionPasajeroArg">Value of the inbound argument 'p_thisRevisionPasajero'.</param>
		/// <returns>List of inbound arguments values.</returns>
		private static Dictionary<string, object> GetInputFieldValues(RevisionPasajeroOid p_thisRevisionPasajeroArg)
		{
			// Fill values dictionary.
			Dictionary<string, object> lValues = new Dictionary<string, object>();
			lValues.Add("p_thisRevisionPasajero", p_thisRevisionPasajeroArg);

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
			lTypes.Add("p_thisRevisionPasajero", ModelType.Oid);

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
			lDomains.Add("p_thisRevisionPasajero", "RevisionPasajero");

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
			lDomains.Add("p_thisRevisionPasajero", "RevisionPasajero");

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


			#region 'this' initialization
			// Argument 'this' initialization: 'p_thisRevisionPasajero'.
			List<Oid> lSelectedOids = null;
			ExchangeInfoAction lInfoAction = context.ExchangeInformation as ExchangeInfoAction;
			if (lInfoAction != null)
			{
				lSelectedOids = lInfoAction.SelectedOids;
			}

			// Check if the selected Oid is an Oid of the 'this' argument class.
			if (UtilFunctions.OidsBelongToClass(lSelectedOids, "RevisionPasajero"))
			{
				if (!context.GetInputFieldMultiSelectionAllowed("p_thisRevisionPasajero") && lSelectedOids.Count > 1)
				{
					lSelectedOids.RemoveRange(1, lSelectedOids.Count - 1);
				}
				previousValue = context.GetInputFieldValue("p_thisRevisionPasajero");
				context.SetInputFieldValue("p_thisRevisionPasajero", lSelectedOids);
				// Check SetValue dependency rules.
				context.SelectedInputField = "p_thisRevisionPasajero";
				ExecuteDependencyRules(context, previousValue, DependencyRulesEventLogic.SetValue, DependencyRulesAgentLogic.Internal, ref depRulesCounter, depRulesCache);
				// Check SetEnabled dependency rules.
				context.SetInputFieldEnabled("p_thisRevisionPasajero", false);
				context.SelectedInputField = "p_thisRevisionPasajero";
				ExecuteDependencyRules(context, true, DependencyRulesEventLogic.SetActive, DependencyRulesAgentLogic.Internal, ref depRulesCounter, depRulesCache);
				context.SelectedInputField = string.Empty;
			}
			#endregion 'this' initialization

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
			// Initialize object-valued arguments using information in the context stack, only if the argument has not value.
			List<Oid> lArgumentValue = null;
			// 'p_thisRevisionPasajero' argument.
			previousValue = context.GetInputFieldValue("p_thisRevisionPasajero");
			if (previousValue == null)
			{
				// Search an Oid of the argument class: 'RevisionPasajero'.
				lArgumentValue = context.ExchangeInformation.GetOidsOfClass("RevisionPasajero");
				if (lArgumentValue != null)
				{
					context.SetInputFieldValue("p_thisRevisionPasajero", lArgumentValue);
					// Check SetValue dependency rules.
					context.SelectedInputField = "p_thisRevisionPasajero";
					ExecuteDependencyRules(context, null, DependencyRulesEventLogic.SetValue, DependencyRulesAgentLogic.Internal, ref depRulesCounter, depRulesCache);
					context.SelectedInputField = string.Empty;
				}
			}
			
			#endregion Arguments initializations taking into account context information
		}
		#endregion Execute Load From Context
		
		#region Conditional Navigation
		/// <summary>
		/// Solve the conditional navigation of 'delete_instance' service.
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
		/// Solves the dependency rules of 'delete_instance' inbound arguments, updating the context received as parameter.
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
		/// Solves the dependency rules of 'delete_instance' inbound arguments, updating the context received as parameter.
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
		/// Method that solves the execution of 'delete_instance' service.
		/// </summary>
		/// <param name="context">Current context.</param>
		public static void ExecuteService(IUServiceContext context)
		{
			context.OutputFields = ExecuteService(context.Agent, context.GetInputFieldValue("p_thisRevisionPasajero") as List<Oid>);
		}
		/// <summary>
		/// Method that solves the execution of 'delete_instance' service.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="argumentValues">Inbound argument values.</param>
		/// <returns>Outbound argument values.</returns>
		public static Dictionary<string, object> ExecuteService(Oid agent,  Dictionary<string, object> argumentValues)
		{
			return ExecuteService(agent, argumentValues["p_thisRevisionPasajero"] as List<Oid>);
		}
		/// <summary>
		/// Method that solves the execution of 'delete_instance' service.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="p_thisRevisionPasajeroArg">Value of the inbound argument 'p_thisRevisionPasajero'.</param>
		/// <returns>Outbound argument values.</returns>
		public static Dictionary<string, object> ExecuteService(Oid agent, List<Oid> p_thisRevisionPasajeroArg)
		{
			RevisionPasajeroOid lp_thisRevisionPasajero = null;
			if ((p_thisRevisionPasajeroArg != null) && (p_thisRevisionPasajeroArg.Count > 0) && (p_thisRevisionPasajeroArg[0] != null))
			{
				lp_thisRevisionPasajero = (p_thisRevisionPasajeroArg[0] as RevisionPasajeroOid);
			}
			Dictionary<string, object> lValues = GetInputFieldValues(lp_thisRevisionPasajero);
			Dictionary<string, ModelType> lTypes = GetInboundArgumentTypes();
			Dictionary<string, string> lDomains = GetInboundArgumentDomains();

			Dictionary<string, object> lOutboundArguments = Logic.Adaptor.ExecuteService(agent, "RevisionPasajero", "delete_instance", lTypes, lValues, lDomains);
			return UtilFunctions.ProcessOutboundArgsList(lOutboundArguments);
		}
		#endregion Execute Service
		
		#region Execute Validate Value
		/// <summary>
		/// Solves the validation of inbound arguments of 'delete_instance' service.
		/// </summary>
		/// <param name="context">Current context.</param>
		public static void ExecuteValidateValue(IUServiceContext context)
		{
			switch (context.SelectedInputField)
			{	
				case "p_thisRevisionPasajero":
					ExecuteValidateValuep_thisRevisionPasajero(context.Agent, context.GetInputFieldValue("p_thisRevisionPasajero") as List<Oid>);
					break;
				default:
					break;
			}
		}
		/// <summary>
		/// Solves the validation of the inbound arguments of 'delete_instance' service specified as parameter.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="argumentName">Inbound argument name to validate.</param>
		/// <param name="argumentValue">Inbound argument value to validate.</param>
		public static void ExecuteValidateValue(Oid agent, string argumentName, object argumentValue)
		{
			switch (argumentName)
			{	
				case "p_thisRevisionPasajero":
					ExecuteValidateValuep_thisRevisionPasajero(agent, argumentValue as List<Oid>);
					break;
				default:
					break;
			}
		}
		/// <summary>
		/// Solves the validation of the 'p_thisRevisionPasajero' inbound argument.
		/// </summary>
		/// <param name="agent">Application agent</param>
		/// <param name="argumentValue">Value of the inbound argument to validate</param>
		public static void ExecuteValidateValuep_thisRevisionPasajero(Oid agent, List<Oid> argumentValue)
		{
			// TODO: Validate value
		}
		#endregion Execute Validate Value
		
		#region Get type
		/// <summary>
		/// Method that returns the type of 'delete_instance' service.
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
		/// Gets the effect type of 'delete_instance' service.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <returns>Effect type of the service (Creation, Destruction, Modification).</returns>
		public static ServiceEffectType GetServiceEffectType(IUServiceContext context)
		{
			// ServiceType = D.
			return ServiceEffectType.Destruction;
		}
		#endregion Get effect type
	}
}
