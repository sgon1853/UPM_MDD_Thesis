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
	/// Class that solves logic of 'create_instance' service.
	/// </summary>
	public static class create_instanceLogic
	{
		#region Auxiliar methods 
		/// <summary>
		/// Gets a list with the arguments values.
		/// </summary>
		/// <param name="p_agrPasajeroAeronaveArg">Value of the inbound argument 'p_agrPasajeroAeronave'.</param>
		/// <param name="p_agrRevisionArg">Value of the inbound argument 'p_agrRevision'.</param>
		/// <param name="p_atrid_RevisionPasajeroArg">Value of the inbound argument 'p_atrid_RevisionPasajero'.</param>
		/// <returns>List of inbound arguments values.</returns>
		private static Dictionary<string, object> GetInputFieldValues(PasajeroAeronaveOid p_agrPasajeroAeronaveArg, RevisionOid p_agrRevisionArg, int? p_atrid_RevisionPasajeroArg)
		{
			// Fill values dictionary.
			Dictionary<string, object> lValues = new Dictionary<string, object>();
			lValues.Add("p_agrPasajeroAeronave", p_agrPasajeroAeronaveArg);
			lValues.Add("p_agrRevision", p_agrRevisionArg);
			lValues.Add("p_atrid_RevisionPasajero", p_atrid_RevisionPasajeroArg);

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
			lTypes.Add("p_agrPasajeroAeronave", ModelType.Oid);
			lTypes.Add("p_agrRevision", ModelType.Oid);
			lTypes.Add("p_atrid_RevisionPasajero", ModelType.Autonumeric);

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
			lDomains.Add("p_agrPasajeroAeronave", "PasajeroAeronave");
			lDomains.Add("p_agrRevision", "Revision");

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
			lDomains.Add("p_agrPasajeroAeronave", "PasajeroAeronave");
			lDomains.Add("p_agrRevision", "Revision");

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
				List<Oid> lLastNavigationOids = context.ExchangeInformation.GetLastNavigationRoleOids();
				// Compare the argument role with the role of the navigation obtained from the context.
				if (lLastNavigationRole.Equals("PasajeroAeronave", StringComparison.CurrentCultureIgnoreCase) &&
					UtilFunctions.OidsBelongToClass(lLastNavigationOids, "PasajeroAeronave"))
				{
					if (!context.GetInputFieldMultiSelectionAllowed("p_agrPasajeroAeronave") && lLastNavigationOids.Count > 1)
					{
						lLastNavigationOids.RemoveRange(1, lLastNavigationOids.Count - 1);
					}
					previousValue = context.GetInputFieldValue("p_agrPasajeroAeronave");
					context.SetInputFieldValue("p_agrPasajeroAeronave", lLastNavigationOids);
					// Check SetValue dependency rules.
					context.SelectedInputField = "p_agrPasajeroAeronave";
					ExecuteDependencyRules(context, previousValue, DependencyRulesEventLogic.SetValue, DependencyRulesAgentLogic.Internal, ref depRulesCounter, depRulesCache);
					// Check SetEnabled dependency rules.
					context.SetInputFieldEnabled("p_agrPasajeroAeronave", false);
					context.SelectedInputField = "p_agrPasajeroAeronave";
					ExecuteDependencyRules(context, true, DependencyRulesEventLogic.SetActive, DependencyRulesAgentLogic.Internal, ref depRulesCounter, depRulesCache);
					context.SelectedInputField = string.Empty;
				}
				// Compare the argument role with the role of the navigation obtained from the context.
				if (lLastNavigationRole.Equals("Revision", StringComparison.CurrentCultureIgnoreCase) &&
					UtilFunctions.OidsBelongToClass(lLastNavigationOids, "Revision"))
				{
					if (!context.GetInputFieldMultiSelectionAllowed("p_agrRevision") && lLastNavigationOids.Count > 1)
					{
						lLastNavigationOids.RemoveRange(1, lLastNavigationOids.Count - 1);
					}
					previousValue = context.GetInputFieldValue("p_agrRevision");
					context.SetInputFieldValue("p_agrRevision", lLastNavigationOids);
					// Check SetValue dependency rules.
					context.SelectedInputField = "p_agrRevision";
					ExecuteDependencyRules(context, previousValue, DependencyRulesEventLogic.SetValue, DependencyRulesAgentLogic.Internal, ref depRulesCounter, depRulesCache);
					// Check SetEnabled dependency rules.
					context.SetInputFieldEnabled("p_agrRevision", false);
					context.SelectedInputField = "p_agrRevision";
					ExecuteDependencyRules(context, true, DependencyRulesEventLogic.SetActive, DependencyRulesAgentLogic.Internal, ref depRulesCounter, depRulesCache);
					context.SelectedInputField = string.Empty;
				}

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
			// 'p_agrPasajeroAeronave' argument.
			previousValue = context.GetInputFieldValue("p_agrPasajeroAeronave");
			if (previousValue == null)
			{
				// Search an Oid of the argument class: 'PasajeroAeronave'.
				lArgumentValue = context.ExchangeInformation.GetOidsOfClass("PasajeroAeronave");
				if (lArgumentValue != null)
				{
					context.SetInputFieldValue("p_agrPasajeroAeronave", lArgumentValue);
					// Check SetValue dependency rules.
					context.SelectedInputField = "p_agrPasajeroAeronave";
					ExecuteDependencyRules(context, null, DependencyRulesEventLogic.SetValue, DependencyRulesAgentLogic.Internal, ref depRulesCounter, depRulesCache);
					context.SelectedInputField = string.Empty;
				}
			}
			
			// 'p_agrRevision' argument.
			previousValue = context.GetInputFieldValue("p_agrRevision");
			if (previousValue == null)
			{
				// Search an Oid of the argument class: 'Revision'.
				lArgumentValue = context.ExchangeInformation.GetOidsOfClass("Revision");
				if (lArgumentValue != null)
				{
					context.SetInputFieldValue("p_agrRevision", lArgumentValue);
					// Check SetValue dependency rules.
					context.SelectedInputField = "p_agrRevision";
					ExecuteDependencyRules(context, null, DependencyRulesEventLogic.SetValue, DependencyRulesAgentLogic.Internal, ref depRulesCounter, depRulesCache);
					context.SelectedInputField = string.Empty;
				}
			}
			
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
			context.OutputFields = ExecuteService(context.Agent, context.GetInputFieldValue("p_agrPasajeroAeronave") as List<Oid>, context.GetInputFieldValue("p_agrRevision") as List<Oid>, context.GetInputFieldValue("p_atrid_RevisionPasajero") as int?);
		}
		/// <summary>
		/// Method that solves the execution of 'create_instance' service.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="argumentValues">Inbound argument values.</param>
		/// <returns>Outbound argument values.</returns>
		public static Dictionary<string, object> ExecuteService(Oid agent,  Dictionary<string, object> argumentValues)
		{
			return ExecuteService(agent, argumentValues["p_agrPasajeroAeronave"] as List<Oid>, argumentValues["p_agrRevision"] as List<Oid>, argumentValues["p_atrid_RevisionPasajero"] as int?);
		}
		/// <summary>
		/// Method that solves the execution of 'create_instance' service.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="p_agrPasajeroAeronaveArg">Value of the inbound argument 'p_agrPasajeroAeronave'.</param>
		/// <param name="p_agrRevisionArg">Value of the inbound argument 'p_agrRevision'.</param>
		/// <param name="p_atrid_RevisionPasajeroArg">Value of the inbound argument 'p_atrid_RevisionPasajero'.</param>
		/// <returns>Outbound argument values.</returns>
		public static Dictionary<string, object> ExecuteService(Oid agent, List<Oid> p_agrPasajeroAeronaveArg, List<Oid> p_agrRevisionArg, int? p_atrid_RevisionPasajeroArg)
		{
			PasajeroAeronaveOid lp_agrPasajeroAeronave = null;
			if ((p_agrPasajeroAeronaveArg != null) && (p_agrPasajeroAeronaveArg.Count > 0) && (p_agrPasajeroAeronaveArg[0] != null))
			{
				lp_agrPasajeroAeronave = (p_agrPasajeroAeronaveArg[0] as PasajeroAeronaveOid);
			}
			RevisionOid lp_agrRevision = null;
			if ((p_agrRevisionArg != null) && (p_agrRevisionArg.Count > 0) && (p_agrRevisionArg[0] != null))
			{
				lp_agrRevision = (p_agrRevisionArg[0] as RevisionOid);
			}
			Dictionary<string, object> lValues = GetInputFieldValues(lp_agrPasajeroAeronave, lp_agrRevision, p_atrid_RevisionPasajeroArg);
			Dictionary<string, ModelType> lTypes = GetInboundArgumentTypes();
			Dictionary<string, string> lDomains = GetInboundArgumentDomains();

			Dictionary<string, object> lOutboundArguments = Logic.Adaptor.ExecuteService(agent, "RevisionPasajero", "create_instance", lTypes, lValues, lDomains);
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
				case "p_agrPasajeroAeronave":
					ExecuteValidateValuep_agrPasajeroAeronave(context.Agent, context.GetInputFieldValue("p_agrPasajeroAeronave") as List<Oid>);
					break;
				case "p_agrRevision":
					ExecuteValidateValuep_agrRevision(context.Agent, context.GetInputFieldValue("p_agrRevision") as List<Oid>);
					break;
				case "p_atrid_RevisionPasajero":
					ExecuteValidateValuep_atrid_RevisionPasajero(context.Agent, context.GetInputFieldValue("p_atrid_RevisionPasajero") as int?);
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
				case "p_agrPasajeroAeronave":
					ExecuteValidateValuep_agrPasajeroAeronave(agent, argumentValue as List<Oid>);
					break;
				case "p_agrRevision":
					ExecuteValidateValuep_agrRevision(agent, argumentValue as List<Oid>);
					break;
				case "p_atrid_RevisionPasajero":
					ExecuteValidateValuep_atrid_RevisionPasajero(agent, argumentValue as int?);
					break;
				default:
					break;
			}
		}
		/// <summary>
		/// Solves the validation of the 'p_agrPasajeroAeronave' inbound argument.
		/// </summary>
		/// <param name="agent">Application agent</param>
		/// <param name="argumentValue">Value of the inbound argument to validate</param>
		public static void ExecuteValidateValuep_agrPasajeroAeronave(Oid agent, List<Oid> argumentValue)
		{
			// TODO: Validate value
		}
		/// <summary>
		/// Solves the validation of the 'p_agrRevision' inbound argument.
		/// </summary>
		/// <param name="agent">Application agent</param>
		/// <param name="argumentValue">Value of the inbound argument to validate</param>
		public static void ExecuteValidateValuep_agrRevision(Oid agent, List<Oid> argumentValue)
		{
			// TODO: Validate value
		}
		/// <summary>
		/// Solves the validation of the 'p_atrid_RevisionPasajero' inbound argument.
		/// </summary>
		/// <param name="agent">Application agent</param>
		/// <param name="argumentValue">Value of the inbound argument to validate</param>
		public static void ExecuteValidateValuep_atrid_RevisionPasajero(Oid agent, int? argumentValue)
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
