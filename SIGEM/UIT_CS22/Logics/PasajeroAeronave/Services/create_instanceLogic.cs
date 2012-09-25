// v3.8.4.5.b
using System;
using System.Collections;
using System.Data;
using System.Collections.Specialized;
using System.Collections.Generic;

using SIGEM.Client.Adaptor;
using SIGEM.Client.Oids;
using SIGEM.Client.Presentation;

namespace SIGEM.Client.Logics.PasajeroAeronave.Services
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
		/// <param name="p_agrAeronaveArg">Value of the inbound argument 'p_agrAeronave'.</param>
		/// <param name="p_agrPasajeroArg">Value of the inbound argument 'p_agrPasajero'.</param>
		/// <param name="p_atrid_PasajeroAeronaveArg">Value of the inbound argument 'p_atrid_PasajeroAeronave'.</param>
		/// <param name="p_atrNombreAeronaveArg">Value of the inbound argument 'p_atrNombreAeronave'.</param>
		/// <param name="p_atrNombrePasajeroArg">Value of the inbound argument 'p_atrNombrePasajero'.</param>
		/// <returns>List of inbound arguments values.</returns>
		private static Dictionary<string, object> GetInputFieldValues(AeronaveOid p_agrAeronaveArg, PasajeroOid p_agrPasajeroArg, int? p_atrid_PasajeroAeronaveArg, string p_atrNombreAeronaveArg, string p_atrNombrePasajeroArg)
		{
			// Fill values dictionary.
			Dictionary<string, object> lValues = new Dictionary<string, object>();
			lValues.Add("p_agrAeronave", p_agrAeronaveArg);
			lValues.Add("p_agrPasajero", p_agrPasajeroArg);
			lValues.Add("p_atrid_PasajeroAeronave", p_atrid_PasajeroAeronaveArg);
			lValues.Add("p_atrNombreAeronave", p_atrNombreAeronaveArg);
			lValues.Add("p_atrNombrePasajero", p_atrNombrePasajeroArg);

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
			lTypes.Add("p_agrAeronave", ModelType.Oid);
			lTypes.Add("p_agrPasajero", ModelType.Oid);
			lTypes.Add("p_atrid_PasajeroAeronave", ModelType.Autonumeric);
			lTypes.Add("p_atrNombreAeronave", ModelType.String);
			lTypes.Add("p_atrNombrePasajero", ModelType.String);

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
			lDomains.Add("p_agrAeronave", "Aeronave");
			lDomains.Add("p_agrPasajero", "Pasajero");

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
			lDomains.Add("p_agrAeronave", "Aeronave");
			lDomains.Add("p_agrPasajero", "Pasajero");

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
				if (lLastNavigationRole.Equals("Aeronave", StringComparison.CurrentCultureIgnoreCase) &&
					UtilFunctions.OidsBelongToClass(lLastNavigationOids, "Aeronave"))
				{
					if (!context.GetInputFieldMultiSelectionAllowed("p_agrAeronave") && lLastNavigationOids.Count > 1)
					{
						lLastNavigationOids.RemoveRange(1, lLastNavigationOids.Count - 1);
					}
					previousValue = context.GetInputFieldValue("p_agrAeronave");
					context.SetInputFieldValue("p_agrAeronave", lLastNavigationOids);
					// Check SetValue dependency rules.
					context.SelectedInputField = "p_agrAeronave";
					ExecuteDependencyRules(context, previousValue, DependencyRulesEventLogic.SetValue, DependencyRulesAgentLogic.Internal, ref depRulesCounter, depRulesCache);
					// Check SetEnabled dependency rules.
					context.SetInputFieldEnabled("p_agrAeronave", false);
					context.SelectedInputField = "p_agrAeronave";
					ExecuteDependencyRules(context, true, DependencyRulesEventLogic.SetActive, DependencyRulesAgentLogic.Internal, ref depRulesCounter, depRulesCache);
					context.SelectedInputField = string.Empty;
				}
				// Compare the argument role with the role of the navigation obtained from the context.
				if (lLastNavigationRole.Equals("Pasajero", StringComparison.CurrentCultureIgnoreCase) &&
					UtilFunctions.OidsBelongToClass(lLastNavigationOids, "Pasajero"))
				{
					if (!context.GetInputFieldMultiSelectionAllowed("p_agrPasajero") && lLastNavigationOids.Count > 1)
					{
						lLastNavigationOids.RemoveRange(1, lLastNavigationOids.Count - 1);
					}
					previousValue = context.GetInputFieldValue("p_agrPasajero");
					context.SetInputFieldValue("p_agrPasajero", lLastNavigationOids);
					// Check SetValue dependency rules.
					context.SelectedInputField = "p_agrPasajero";
					ExecuteDependencyRules(context, previousValue, DependencyRulesEventLogic.SetValue, DependencyRulesAgentLogic.Internal, ref depRulesCounter, depRulesCache);
					// Check SetEnabled dependency rules.
					context.SetInputFieldEnabled("p_agrPasajero", false);
					context.SelectedInputField = "p_agrPasajero";
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
			// 'p_agrAeronave' argument.
			previousValue = context.GetInputFieldValue("p_agrAeronave");
			if (previousValue == null)
			{
				// Search an Oid of the argument class: 'Aeronave'.
				lArgumentValue = context.ExchangeInformation.GetOidsOfClass("Aeronave");
				if (lArgumentValue != null)
				{
					context.SetInputFieldValue("p_agrAeronave", lArgumentValue);
					// Check SetValue dependency rules.
					context.SelectedInputField = "p_agrAeronave";
					ExecuteDependencyRules(context, null, DependencyRulesEventLogic.SetValue, DependencyRulesAgentLogic.Internal, ref depRulesCounter, depRulesCache);
					context.SelectedInputField = string.Empty;
				}
			}
			
			// 'p_agrPasajero' argument.
			previousValue = context.GetInputFieldValue("p_agrPasajero");
			if (previousValue == null)
			{
				// Search an Oid of the argument class: 'Pasajero'.
				lArgumentValue = context.ExchangeInformation.GetOidsOfClass("Pasajero");
				if (lArgumentValue != null)
				{
					context.SetInputFieldValue("p_agrPasajero", lArgumentValue);
					// Check SetValue dependency rules.
					context.SelectedInputField = "p_agrPasajero";
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
			context.OutputFields = ExecuteService(context.Agent, context.GetInputFieldValue("p_agrAeronave") as List<Oid>, context.GetInputFieldValue("p_agrPasajero") as List<Oid>, context.GetInputFieldValue("p_atrid_PasajeroAeronave") as int?, context.GetInputFieldValue("p_atrNombreAeronave") as string, context.GetInputFieldValue("p_atrNombrePasajero") as string);
		}
		/// <summary>
		/// Method that solves the execution of 'create_instance' service.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="argumentValues">Inbound argument values.</param>
		/// <returns>Outbound argument values.</returns>
		public static Dictionary<string, object> ExecuteService(Oid agent,  Dictionary<string, object> argumentValues)
		{
			return ExecuteService(agent, argumentValues["p_agrAeronave"] as List<Oid>, argumentValues["p_agrPasajero"] as List<Oid>, argumentValues["p_atrid_PasajeroAeronave"] as int?, argumentValues["p_atrNombreAeronave"] as string, argumentValues["p_atrNombrePasajero"] as string);
		}
		/// <summary>
		/// Method that solves the execution of 'create_instance' service.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="p_agrAeronaveArg">Value of the inbound argument 'p_agrAeronave'.</param>
		/// <param name="p_agrPasajeroArg">Value of the inbound argument 'p_agrPasajero'.</param>
		/// <param name="p_atrid_PasajeroAeronaveArg">Value of the inbound argument 'p_atrid_PasajeroAeronave'.</param>
		/// <param name="p_atrNombreAeronaveArg">Value of the inbound argument 'p_atrNombreAeronave'.</param>
		/// <param name="p_atrNombrePasajeroArg">Value of the inbound argument 'p_atrNombrePasajero'.</param>
		/// <returns>Outbound argument values.</returns>
		public static Dictionary<string, object> ExecuteService(Oid agent, List<Oid> p_agrAeronaveArg, List<Oid> p_agrPasajeroArg, int? p_atrid_PasajeroAeronaveArg, string p_atrNombreAeronaveArg, string p_atrNombrePasajeroArg)
		{
			AeronaveOid lp_agrAeronave = null;
			if ((p_agrAeronaveArg != null) && (p_agrAeronaveArg.Count > 0) && (p_agrAeronaveArg[0] != null))
			{
				lp_agrAeronave = (p_agrAeronaveArg[0] as AeronaveOid);
			}
			PasajeroOid lp_agrPasajero = null;
			if ((p_agrPasajeroArg != null) && (p_agrPasajeroArg.Count > 0) && (p_agrPasajeroArg[0] != null))
			{
				lp_agrPasajero = (p_agrPasajeroArg[0] as PasajeroOid);
			}
			Dictionary<string, object> lValues = GetInputFieldValues(lp_agrAeronave, lp_agrPasajero, p_atrid_PasajeroAeronaveArg, p_atrNombreAeronaveArg, p_atrNombrePasajeroArg);
			Dictionary<string, ModelType> lTypes = GetInboundArgumentTypes();
			Dictionary<string, string> lDomains = GetInboundArgumentDomains();

			Dictionary<string, object> lOutboundArguments = Logic.Adaptor.ExecuteService(agent, "PasajeroAeronave", "create_instance", lTypes, lValues, lDomains);
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
				case "p_agrAeronave":
					ExecuteValidateValuep_agrAeronave(context.Agent, context.GetInputFieldValue("p_agrAeronave") as List<Oid>);
					break;
				case "p_agrPasajero":
					ExecuteValidateValuep_agrPasajero(context.Agent, context.GetInputFieldValue("p_agrPasajero") as List<Oid>);
					break;
				case "p_atrid_PasajeroAeronave":
					ExecuteValidateValuep_atrid_PasajeroAeronave(context.Agent, context.GetInputFieldValue("p_atrid_PasajeroAeronave") as int?);
					break;
				case "p_atrNombreAeronave":
					ExecuteValidateValuep_atrNombreAeronave(context.Agent, context.GetInputFieldValue("p_atrNombreAeronave") as string);
					break;
				case "p_atrNombrePasajero":
					ExecuteValidateValuep_atrNombrePasajero(context.Agent, context.GetInputFieldValue("p_atrNombrePasajero") as string);
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
				case "p_agrAeronave":
					ExecuteValidateValuep_agrAeronave(agent, argumentValue as List<Oid>);
					break;
				case "p_agrPasajero":
					ExecuteValidateValuep_agrPasajero(agent, argumentValue as List<Oid>);
					break;
				case "p_atrid_PasajeroAeronave":
					ExecuteValidateValuep_atrid_PasajeroAeronave(agent, argumentValue as int?);
					break;
				case "p_atrNombreAeronave":
					ExecuteValidateValuep_atrNombreAeronave(agent, argumentValue as string);
					break;
				case "p_atrNombrePasajero":
					ExecuteValidateValuep_atrNombrePasajero(agent, argumentValue as string);
					break;
				default:
					break;
			}
		}
		/// <summary>
		/// Solves the validation of the 'p_agrAeronave' inbound argument.
		/// </summary>
		/// <param name="agent">Application agent</param>
		/// <param name="argumentValue">Value of the inbound argument to validate</param>
		public static void ExecuteValidateValuep_agrAeronave(Oid agent, List<Oid> argumentValue)
		{
			// TODO: Validate value
		}
		/// <summary>
		/// Solves the validation of the 'p_agrPasajero' inbound argument.
		/// </summary>
		/// <param name="agent">Application agent</param>
		/// <param name="argumentValue">Value of the inbound argument to validate</param>
		public static void ExecuteValidateValuep_agrPasajero(Oid agent, List<Oid> argumentValue)
		{
			// TODO: Validate value
		}
		/// <summary>
		/// Solves the validation of the 'p_atrid_PasajeroAeronave' inbound argument.
		/// </summary>
		/// <param name="agent">Application agent</param>
		/// <param name="argumentValue">Value of the inbound argument to validate</param>
		public static void ExecuteValidateValuep_atrid_PasajeroAeronave(Oid agent, int? argumentValue)
		{
			// TODO: Validate value
		}
		/// <summary>
		/// Solves the validation of the 'p_atrNombreAeronave' inbound argument.
		/// </summary>
		/// <param name="agent">Application agent</param>
		/// <param name="argumentValue">Value of the inbound argument to validate</param>
		public static void ExecuteValidateValuep_atrNombreAeronave(Oid agent, string argumentValue)
		{
			// TODO: Validate value
		}
		/// <summary>
		/// Solves the validation of the 'p_atrNombrePasajero' inbound argument.
		/// </summary>
		/// <param name="agent">Application agent</param>
		/// <param name="argumentValue">Value of the inbound argument to validate</param>
		public static void ExecuteValidateValuep_atrNombrePasajero(Oid agent, string argumentValue)
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
