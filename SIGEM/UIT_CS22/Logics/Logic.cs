// v3.8.4.5.b
using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Reflection;
using SIGEM.Client.Oids;
using SIGEM.Client.Adaptor;
using SIGEM.Client.Logics.Preferences;
using SIGEM.Client.PrintingDriver;

namespace SIGEM.Client.Logics
{
	/// <summary>
	/// Class that dispatch the logic methods
	/// </summary>
	public static class Logic
	{
		#region Members
		/// <summary>
		/// Connected agent information
		/// </summary>
		private static AgentInfo mAgent;
		/// <summary>
		/// User preferences information
		/// </summary>
		private static PreferencesMng mUserPreferences;
		/// <summary>
		/// Refresh by row flag. Default value for all the scenarios
		/// </summary>
		public const bool RefreshByRowAfterServiceExecution = false;
		/// <summary>
		/// Refresh Master flag. Default value for all the scenarios
		/// </summary>
		public const bool RefreshMasterAfterServiceExecution = true;
		/// <summary>
		/// Gets the list of report that allow to print information from an instance.
		/// </summary>
		private static InstanceReports mInstanceReportsList;
		#endregion Members

		#region Properties
		/// <summary>
		/// Agent connected in the application.
		/// </summary>
		public static AgentInfo Agent
		{
			get
			{
				return mAgent;
			}
			set
			{
				// If it is not the same agent, reset User Preferences.
				if (mAgent != value)
				{
					mUserPreferences = null;
				}
				mAgent = value;
				if (mAgent != null)
				{
					SetAgentActiveFacets(mAgent);
				}
			}
		}
		/// <summary>
		/// Gets the Preferences Manager.
		/// </summary>
		public static PreferencesMng UserPreferences
		{
			get
			{
				if (mUserPreferences == null)
				{
					mUserPreferences = new PreferencesMng();
				}
				return mUserPreferences;
			}
		}

		/// <summary>
		/// Gets the list of report that allow to print information from an instance.
		/// </summary>
		public static InstanceReports InstanceReportsList
		{
			get
			{
				if (mInstanceReportsList == null)
				{
					mInstanceReportsList = new InstanceReports();
				}
				return mInstanceReportsList;
			}
		}
		#endregion Properties

		#region Adaptor
		public static string ConnectionString = Properties.Settings.Default.ConnectionString;
		private static ServerConnection mAdaptor = new ServerConnection();
		/// <summary>
		/// Represents a global reference to the Adaptor layer.
		/// </summary>
		public static ServerConnection Adaptor
		{
			get
			{
				if (mAdaptor == null)
				{
					mAdaptor = new ServerConnection(ConnectionString);
				}
				else if (mAdaptor.ConnectionString != ConnectionString)
				{
					mAdaptor.Close();
				}

				if (!mAdaptor.IsOpen())
				{
					mAdaptor.Open(ConnectionString);
				}
				return mAdaptor;
			}
		}
		#endregion Adaptor

		#region General
		private static Type GetType(string component)
		{
			try
			{
				return Type.GetType(component, true, true);
			}
			catch (Exception e)
			{
				throw new Exception("Can't get C# class '" + component + "'", e);
			}
		}
		private static string GetTypeName(string name)
		{
			return "SIGEM.Client." + name;
		}
		private static string GetLogicTypeName(string component)
		{
			return (typeof(Logic).Namespace + "." + component);
		}
		private static MethodInfo GetMethod(Type type, string methodName, Type[] types, object[] parameters)
		{
			try
			{
				if (types == null)
				{
					return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
				}
				else
				{
					return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, null, types, null);
				}
			}
			catch (AmbiguousMatchException)
			{
				Type[] lTypes = new Type[parameters.Length];
				for (int i = 0; i < parameters.Length; i++)
				{
					lTypes[i] = parameters[i].GetType();
				}
				return GetMethod(type, methodName, lTypes, parameters);
			}
			catch (Exception e)
			{
				throw new Exception("Can't get C# method '" + methodName + "' of class '" + type.Name + "'", e);
			}
		}
		private static object ExecuteMethod(string component, string methodName)
		{
			return ExecuteMethod(component, methodName, null, null);
		}
		private static object ExecuteMethod(string component, string methodName, object[] parameters)
		{
			return ExecuteMethod(component, methodName, null, parameters);
		}
		private static object ExecuteMethod(string component, string methodName, Type[] types, object[] parameters)
		{
			// Get class
			Type lType = null;
			try
			{
				lType = GetType(component);
			}
			catch (Exception e)
			{
				throw new Exception("Can't get C# class '" + component + "'", e);
			}
			if (lType == null)
				throw new Exception("Can't get C# class '" + component + "'");

			// Get method
			MethodInfo lMethod = null;
			try
			{
				lMethod = GetMethod(lType, methodName, types, parameters);
			}
			catch (Exception e)
			{
				throw new Exception("Can't get C# method '" + methodName + "' of class '" + component + "'", e);
			}
			if (lMethod == null)
				throw new Exception("Can't get C# method '" + methodName + "' of class '" + component + "'");

			// Invoke method
			try
			{
				return lMethod.Invoke(null, parameters);
			}
			catch (TargetInvocationException e)
			{
				throw e.InnerException;
			}
		}
		#endregion General

		#region Class
		#region Get class type name
		public static string GetClassTypeName(string className)
		{
			return GetLogicTypeName(className + ".ClassLogic");
		}
		#endregion Get class type name

		#region Execute query instance
		/// <summary>
		/// Execute a query to retrieve an instance.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <returns>A DataTable with the instance searched.</returns>
		public static DataTable ExecuteQueryInstance(IUQueryContext context)
		{
			// Parameter values
			object[] lParameters = new object[1];
			lParameters[0] = context;

			// Parameter types
			Type[] lTypes = new Type[1];
			lTypes[0] = typeof(IUQueryContext);

			return ExecuteMethod(GetClassTypeName(context.ClassName), "ExecuteQueryInstance", lTypes, lParameters) as DataTable;
		}
		/// <summary>
		/// Execute a query to retrieve an instance.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="oidInstance">Oid of the instance to be searched.</param>
		/// <param name="displaySet">Display set that will be retrieved.</param>
		/// <returns>A DataTable with the instance searched.</returns>
		public static DataTable ExecuteQueryInstance(Oid agent, Oid oidInstance, string displaySet)
		{
			if (oidInstance != null)
			{
				return ExecuteQueryInstance(agent, oidInstance.ClassName, oidInstance, displaySet);
			}
			else
			{
				return null;
			}
		}
		
		/// <summary>
		/// Execute a query to retrieve an instance.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="className">Name of the class over which is done the instance query.</param>
		/// <param name="oidInstance">Oid of the instance to be searched.</param>
		/// <param name="displaySet">Display set that will be retrieved.</param>
		/// <returns>A DataTable with the instance searched.</returns>
		public static DataTable ExecuteQueryInstance(Oid agent, string className, Oid oidInstance, string displaySet)
		{
			if (oidInstance == null)
			{
				return null;
			}

			// Parameter values
			object[] lParameters = new object[3];
			lParameters[0] = agent;
			lParameters[1] = oidInstance;
			lParameters[2] = displaySet;

			// Parameter types
			Type[] lTypes = new Type[3];
			lTypes[0] = typeof(Oid);
			lTypes[1] = typeof(Oid);
			lTypes[2] = typeof(string);

			return ExecuteMethod(GetClassTypeName(className), "ExecuteQueryInstance", lTypes, lParameters) as DataTable;
		}
		/// <summary>
		/// Execute a query to retrieve an instance.
		/// </summary>
		/// <param name="agent">Application agent.</param>
		/// <param name="className">Name of the class over which is done the instance query.</param>
		/// <param name="alternateKeyName">AlternateKey name.</param>
		/// <param name="oid">Oid of the instance to be searched.</param>
		/// <param name="displaySet">Display set that will be retrieved.</param>
		/// <returns>DataTable object with the instance information.</returns>
		public static DataTable ExecuteQueryInstance(
			Oid agent,
			string className,
			string alternateKeyName,
			Oid oid,
			string displaySet)
		{
			return Logic.Adaptor.ExecuteQueryInstance(agent, className, alternateKeyName, oid, displaySet);
		}
		#endregion Execute query instance

		#region Execute query population
		/// <summary>
		/// Execute a query to retrieve a set of instances, without any condition.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <returns>A DataTable with the instances searched.</returns>
		public static DataTable ExecuteQueryPopulation(IUPopulationContext context)
		{
			// Parameter values
			object[] lParameters = new object[1];
			lParameters[0] = context;

			// Parameter types
			Type[] lTypes = new Type[1];
			lTypes[0] = typeof(IUPopulationContext);

			return ExecuteMethod(GetClassTypeName(context.ClassName), "ExecuteQueryPopulation", lTypes, lParameters) as DataTable;
		}
		#endregion Execute query population

		#region Execute query related
		/// <summary>
		/// Execute a query related with other instance.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <returns>A DataTable with the instances searched.</returns>
		public static DataTable ExecuteQueryRelated(IUQueryContext context)
		{
			// Parameter values
			object[] lParameters = new object[1];
			lParameters[0] = context;

			// Parameter types
			Type[] lTypes = new Type[1];
			lTypes[0] = typeof(IUQueryContext);

			return ExecuteMethod(GetClassTypeName(context.ClassName), "ExecuteQueryRelated", lTypes, lParameters) as DataTable;
		}
		#endregion Execute query related

		#region Get identification function fields
		/// <summary>
		/// Returns a list with the name of the identification function attributes.
		/// </summary>
		/// <param name="className">Name of the class.</param>
		/// <returns>List with the name of the identification function attributes.</returns>
		public static List<string> GetIdentificationFunctionFields(string className)
		{
			return ExecuteMethod(GetClassTypeName(className), "GetIdentificationFunctionFields") as List<string>;
		}
		#endregion Get identification function fields

		#region IsInheritanceHierarchy
		/// <summary>
		/// Determine if 'classToBeChecked' is in 'className' Inheritace Hierarchy net.
		/// </summary>
		/// <param name="className">Name of the current class.</param>
		/// <param name="classToBeChecked">Name of the class to be checked in the 'className' Inheritace Hierarchy net.</param>
		/// <returns>A boolena value indicating if 'classToBeChecked' is in 'className' Inheritace Hierarchy net or not.</returns>
		public static bool IsClassInheritanceHierarchy(string className, string classToBeChecked)
		{
			try
			{
				// Parameter values
				object[] lParameters = new object[1];
				lParameters[0] = classToBeChecked;

				// Parameter types
				Type[] lTypes = new Type[1];
				lTypes[0] = typeof(string);

				return (bool)ExecuteMethod(GetClassTypeName(className), "IsInheritanceHierarchy", lTypes, lParameters);
			}
			catch
			{
				return false;
			}
		}
		#endregion IsInheritanceHierarchy

		#region Get agents
		/// <summary>
		/// Gets a list with the name of agent classes defined in the application (in the View).
		/// </summary>
		/// <returns>List of agents names.</returns>
		public static List<string> GetAgents()
		{
			List<string> lResult = new List<string>();
			lResult.Add("Administrador");
			return lResult;
		}
		#endregion Get agents

		#region Set Active Facets
		private static void SetAgentActiveFacets(AgentInfo agent)
		{
			List<string> lActiveFacets = new List<string>();
			try
			{
				object[] lParameters = new object[1];
				lParameters[0] = agent;
				lActiveFacets = ExecuteMethod(GetClassTypeName(agent.ClassName), "GetActiveFacets", lParameters) as List<string>;
			}
			catch { }
			lActiveFacets.Add(agent.ClassName);

			agent.AgentFacets = lActiveFacets;
		}
		#endregion Set Active Facets

		#endregion Class

		#region Service
		#region Get service type name
		/// <summary>
		/// Gets the type of a service.
		/// </summary>
		/// <param name="className">Name of the class.</param>
		/// <param name="serviceName">Name of the service.</param>
		/// <returns>Type of the service (enumerate value Event, Transaction, Operation).</returns>
		public static string GetServiceTypeName(string className, string serviceName)
		{
			return GetLogicTypeName(className + ".Services." + serviceName + "Logic");
		}
		#endregion Get service type name
		#region Get Inbound Argument Types.
		public static Dictionary<string, ModelType> GetInboundArgumentTypes(string className, string serviceName)
		{
			return ExecuteMethod(GetServiceTypeName(className, serviceName), "GetInboundArgumentTypes", null, null) as Dictionary<string, ModelType>;
		}
		#endregion Get Inbound Argument Types.
		#region Get Outbound Argument Types.
		public static Dictionary<string, ModelType> GetOutboundArgumentTypes(string className, string serviceName)
		{
			return ExecuteMethod(GetServiceTypeName(className, serviceName), "GetOutboundArgumentTypes", null, null) as Dictionary<string, ModelType>;
		}
		#endregion Get Outbound Argument Types.

		#region Execute service
		/// <summary>
		/// Solves the execution of a service.
		/// </summary>
		/// <param name="context">Current context.</param>
		public static void ExecuteService(IUServiceContext context)
		{
			// Parameter values
			object[] lParameters = new object[1];
			lParameters[0] = context;

			// Parameter types
			Type[] lTypes = new Type[1];
			lTypes[0] = typeof(IUServiceContext);

			ExecuteMethod(GetServiceTypeName(context.ClassName, context.ServiceName), "ExecuteService", lTypes, lParameters);
		}
		#endregion Execute service

		#region Execute default values
		/// <summary>
		/// Solves the default values of the inbound arguments of a service.
		/// </summary>
		/// <param name="context">Current context.</param>
		public static void ExecuteDefaultValues(IUServiceContext context)
		{
			// Parameter values
			object[] lParameters = new object[1];
			lParameters[0] = context;

			// Parameter types
			Type[] lTypes = new Type[1];
			lTypes[0] = typeof(IUServiceContext);

			ExecuteMethod(GetServiceTypeName(context.ClassName, context.ServiceName), "ExecuteDefaultValues", lTypes, lParameters);
		}
		#endregion Execute default values

		#region Execute validate value
		/// <summary>
		/// Solves the validation of the inbound arguments values of a service.
		/// </summary>
		/// <param name="context">Current context.</param>
		public static void ExecuteValidateValue(IUServiceContext context)
		{
			// Parameter values
			object[] lParameters = new object[1];
			lParameters[0] = context;

			// Parameter types
			Type[] lTypes = new Type[1];
			lTypes[0] = typeof(IUServiceContext);

			ExecuteMethod(GetServiceTypeName(context.ClassName, context.ServiceName), "ExecuteValidateValue", lTypes, lParameters);
		}
		#endregion Execute validate value

		#region Execute Load From Context
		/// <summary>
		/// Initialize the inbound arguments of a service, taking into account the context information.
		/// </summary>
		/// <param name="context">Current service context.</param>
		public static void ExecuteLoadFromContext(IUServiceContext context)
		{
			try
			{
			// Parameter values
			object[] lParameters = new object[1];
			lParameters[0] = context;

			// Parameter types
			Type[] lTypes = new Type[1];
			lTypes[0] = typeof(IUServiceContext);

			ExecuteMethod(GetServiceTypeName(context.ClassName, context.ServiceName), "ExecuteLoadFromContext", lTypes, lParameters);
			}
			catch
			{
			}
		}
		#endregion Execute Load From Context

		#region Get effect type
		/// <summary>
		/// Gets the effect type of a service.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <returns>Effect type of the service (Creation, Destruction, Modification).</returns>
		public static ServiceEffectType GetServiceEffectType(IUServiceContext context)
		{
			// Parameter values
			object[] lParameters = new object[1];
			lParameters[0] = context;

			// Parameter types
			Type[] lTypes = new Type[1];
			lTypes[0] = typeof(IUServiceContext);

			return (ServiceEffectType) ExecuteMethod(GetServiceTypeName(context.ClassName, context.ServiceName), "GetServiceEffectType", lTypes, lParameters);
		}
		#endregion Get effect type

		#region Execute Conditional Navigation
		/// <summary>
		/// Solve the conditional navigation of a service.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <returns>A ExchangeInfo object indicating the target scenario and its initializations.</returns>
		public static ExchangeInfoConditionalNavigation ExecuteConditionalNavigation(IUServiceContext context)
		{
			// Parameter values.
			object[] lParameters = new object[1];
			lParameters[0] = context;

			// Parameter types.
			Type[] lTypes = new Type[1];
			lTypes[0] = typeof(IUServiceContext);

			return ExecuteMethod(GetServiceTypeName(context.ClassName, context.ServiceName), "ExecuteConditionalNavigation", lTypes, lParameters) as ExchangeInfoConditionalNavigation;
		}
		#endregion Execute Conditional Navigation

		#region Execute dependency rules
		/// <summary>
		/// Solves the dependency rules of a service.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <param name="lastValue">Old value of the modified argument.</param>
		/// <param name="dependencyRulesEvent">Event that has been thrown (SetValue, SetEnabled).</param>
		/// <param name="dependencyRulesAgent">Agent that has thrown the event (User, Internal).</param>
		public static void ExecuteDependencyRules(IUServiceContext context, object lastValue, DependencyRulesEventLogic dependencyRulesEvent, DependencyRulesAgentLogic dependencyRulesAgent)
		{
			// Parameter values.
			object[] lParameters = new object[4];
			lParameters[0] = context;
			lParameters[1] = lastValue;
			lParameters[2] = dependencyRulesEvent;
			lParameters[3] = dependencyRulesAgent;

			// Parameter types.
			Type[] lTypes = new Type[4];
			lTypes[0] = typeof(IUServiceContext);
			lTypes[1] = typeof(object);
			lTypes[2] = typeof(DependencyRulesEventLogic);
			lTypes[3] = typeof(DependencyRulesAgentLogic);

			ExecuteMethod(GetServiceTypeName(context.ClassName, context.ServiceName), "ExecuteDependencyRules", lTypes, lParameters);
		}
		#endregion Execute dependency rules

		#endregion Service

		#region Filter

		#region Get filter type name
		public static string GetFilterTypeName(string className, string filterName)
		{
			return GetLogicTypeName(className + ".Filters." + filterName + "Logic");
		}
		#endregion Get filter type name

		#region Execute query filter
		/// <summary>
		/// Solves the filter execution.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <returns>A DataTable with the instances searched.</returns>
		public static DataTable ExecuteQueryFilter(IUPopulationContext context)
		{
			// Parameter values
			object[] lParameters = new object[1];
			lParameters[0] = context;

			// Parameter types
			Type[] lTypes = new Type[1];
			lTypes[0] = typeof(IUPopulationContext);

			return ExecuteMethod(GetFilterTypeName(context.ClassName, context.ExecutedFilter), "ExecuteQueryFilter", lTypes, lParameters) as DataTable;
		}
		#endregion Execute query filter

		#region Execute default values
		/// <summary>
		/// Solves the filter variables default values of a filter.
		/// </summary>
		/// <param name="context">Current context.</param>
		public static void ExecuteDefaultValues(IUPopulationContext context, string filterName)
		{
			// Parameter values
			object[] lParameters = new object[1];
			lParameters[0] = context;

			// Parameter types
			Type[] lTypes = new Type[1];
			lTypes[0] = typeof(IUPopulationContext);

			ExecuteMethod(GetFilterTypeName(context.ClassName, filterName), "ExecuteDefaultValues", lTypes, lParameters);
		}
		#endregion Execute default values

		#region Execute validate value
		/// <summary>
		/// Solves the validation of filter variables values of a filter.
		/// </summary>
		/// <param name="context">Current context.</param>
		public static void ExecuteValidateValue(IUPopulationContext context)
		{
			// Parameter values
			object[] lParameters = new object[1];
			lParameters[0] = context;

			// Parameter types
			Type[] lTypes = new Type[1];
			lTypes[0] = typeof(IUPopulationContext);

			ExecuteMethod(GetFilterTypeName(context.ClassName, context.FilterNameSelected), "ExecuteValidateValue", lTypes, lParameters);
		}
		#endregion Execute validate value

		#endregion Filter

		#region Get actions/navigations enabled state formulas
		/// <summary>
		/// Gets the state formulas (enabled or disabled) for action and navigation items (preconditions).
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="data">Datatable.</param>
		/// <param name="actionList">Action list.</param>
		/// <param name="navigationList">Navigation list.</param>
		public static void GetActionsNavigationsEnabledStateFormulas(IUQueryContext context, DataTable data, List<string> actionList, List<string> navigationList)
		{
			try
			{
				// Parameter values.
				object[] lParameters = new object[4];
				lParameters[0] = context;
				lParameters[1] = data;
				lParameters[2] = actionList;
				lParameters[3] = navigationList;

				// Parameter types.
				Type[] lTypes = new Type[4];
				lTypes[0] = typeof(IUQueryContext);
				lTypes[1] = typeof(DataTable);
				lTypes[2] = typeof(List<string>);
				lTypes[3] = typeof(List<string>);

				ExecuteMethod(GetClassTypeName(context.ClassName), "GetActionsNavigationsEnabledState", lTypes, lParameters);
			}
			catch
			{
			}
		}
		#endregion Get actions/navigations enabled state formulas

		#region Input Fields {Filter || Service}

		#region Execute {Filter || Service}
		/// <summary>
		/// Solves the execution of a service or filter.
		/// </summary>
		/// <param name="context">Current context.</param>
		public static void Execute(IUInputFieldsContext context)
		{
			// Parameter values
			object[] lParameters = new object[1];
			lParameters[0] = context;
			// Parameter types
			Type[] lTypes = new Type[1];


			switch (context.ContextType)
			{
				case ContextType.Service:
					lTypes[0] = typeof(IUServiceContext);
					ExecuteMethod(GetServiceTypeName(context.ClassName, context.ContainerName), "ExecuteService", lTypes, lParameters);
					break;
				case ContextType.Filter:
					lTypes[0] = typeof(IUFilterContext);
					ExecuteMethod(GetFilterTypeName(context.ClassName, context.ContainerName), "ExecuteQueryFilter", lTypes, lParameters);
					break;
				default:
					break;
			}
		}
		#endregion Execute {Filter || Service}

		#region Execute {Filter || Service} Default Values
		/// <summary>
		/// Solves the default values of the inbound arguments of a service or filter variables.
		/// </summary>
		/// <param name="context">Current context.</param>
		public static void ExecuteDefaultValues(IUInputFieldsContext context)
		{
			// Parameter values
			object[] lParameters = new object[1];
			lParameters[0] = context;

			// Parameter types
			Type[] lTypes = new Type[1];

			switch (context.ContextType)
			{
				case ContextType.Service:
					lTypes[0] = typeof(IUServiceContext);
					ExecuteMethod(GetServiceTypeName(context.ClassName, context.ContainerName), "ExecuteDefaultValues", lTypes, lParameters);
					break;
				case ContextType.Filter:
					lTypes[0] = typeof(IUFilterContext);
					ExecuteMethod(GetFilterTypeName(context.ClassName, context.ContainerName), "ExecuteDefaultValues", lTypes, lParameters);
					break;
				default:
					break;
			}
		}
		#endregion Execute {Filter || Service} Default Values

		#region Execute {Filter || Service} Validate Value
		/// <summary>
		/// Solves the validation of the inbound arguments values of a service.
		/// </summary>
		/// <param name="context">Current context.</param>
		public static void ExecuteValidateValue(IUInputFieldsContext context)
		{
			// Parameter values
			object[] lParameters = new object[1];
			lParameters[0] = context;

			// Parameter types
			Type[] lTypes = new Type[1];

			switch (context.ContextType)
			{
				case ContextType.Service:
					lTypes[0] = typeof(IUServiceContext);
					ExecuteMethod(GetServiceTypeName(context.ClassName, context.ContainerName), "ExecuteValidateValue", lTypes, lParameters);
					break;
				case ContextType.Filter:
					lTypes[0] = typeof(IUFilterContext);
					ExecuteMethod(GetFilterTypeName(context.ClassName, context.ContainerName), "ExecuteValidateValue", lTypes, lParameters);
					break;
				default:
					break;
			}
		}
		#endregion Execute {Filter || Service} Validate Value

		#region Execute {Filter || Service}  Load From Context
		/// <summary>
		/// Initialize the inbound arguments of a service, taking into account the context information.
		/// </summary>
		/// <param name="context">Current service context.</param>
		public static void ExecuteLoadFromContext(IUInputFieldsContext context)
		{
			try
			{
				// Parameter values
				object[] lParameters = new object[1];
				lParameters[0] = context;

				// Parameter types
				Type[] lTypes = new Type[1];

				switch (context.ContextType)
				{
					case ContextType.Service:
						lTypes[0] = typeof(IUServiceContext);
						ExecuteMethod(GetServiceTypeName(context.ClassName, context.ContainerName), "ExecuteLoadFromContext", lTypes, lParameters);
						break;
					case ContextType.Filter:
						lTypes[0] = typeof(IUFilterContext);
						ExecuteMethod(GetFilterTypeName(context.ClassName, context.ContainerName), "ExecuteLoadFromContext", lTypes, lParameters);
						break;
					default:
						break;
				}
			}
			catch
			{
			}
		}
		#endregion Execute {Filter || Service} Load From Context

		#region Execute {Filter || Service}  Dependency rules
		/// <summary>
		/// Solves the dependency rules of a service.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <param name="lastValue">Old value of the modified argument.</param>
		/// <param name="dependencyRulesEvent">Event that has been thrown (SetValue, SetEnabled).</param>
		/// <param name="dependencyRulesAgent">Agent that has thrown the event (User, Internal).</param>
		public static void ExecuteDependencyRules(IUInputFieldsContext context, object lastValue, DependencyRulesEventLogic dependencyRulesEvent, DependencyRulesAgentLogic dependencyRulesAgent)
		{
			// Parameter values.
			object[] lParameters = new object[4];
			lParameters[0] = context;
			lParameters[1] = lastValue;
			lParameters[2] = dependencyRulesEvent;
			lParameters[3] = dependencyRulesAgent;

			// Parameter types.
			Type[] lTypes = new Type[4];
			lTypes[1] = typeof(object);
			lTypes[2] = typeof(DependencyRulesEventLogic);
			lTypes[3] = typeof(DependencyRulesAgentLogic);

			switch (context.ContextType)
			{
				case ContextType.Service:
					lTypes[0] = typeof(IUServiceContext);
					ExecuteMethod(GetServiceTypeName(context.ClassName, context.ContainerName), "ExecuteDependencyRules", lTypes, lParameters);
					break;
				case ContextType.Filter:
					lTypes[0] = typeof(IUFilterContext);
					ExecuteMethod(GetFilterTypeName(context.ClassName, context.ContainerName), "ExecuteDependencyRules", lTypes, lParameters);
					break;
				default:
					break;
			}
		}
		#endregion Execute {Filter || Service}  Dependency rules

		#endregion Input Fields {Filter || Service}

		#region Oid
		#region Get class type name
		public static string GetOidTypeName(string className)
		{
			return GetTypeName("Oids." + className + "Oid");
		}
		#endregion Get class type name

		#region CreateOidFromOidFields
		/// <summary>
		/// Creates an Oid object with the list of values for its fields, passed as parameters.
		/// </summary>
		/// <param name="className">Name of the class of the new Oid.</param>
		/// <param name="oidFields">List of fields values.</param>
		/// <param name="alternatieKeyName">Indicates the name of the alternate key.</param>
		/// <param name="executeQuery">Indicates wheter a quey must be executed to retrieve the primary Oid.</param>
		/// <returns>An Oid object.</returns>
		public static Oid CreateOidFromOidFields(
			string className,
			List<object> oidFields,
			string alternateKeyName,
			bool executeQuery)
		{
			Oid lOid = null;

			// Null if some field is null.
			if (oidFields != null)
			{
				foreach (object lOidField in oidFields)
				{
					if (lOidField == null)
					{
						return null;
					}
				}

				if (alternateKeyName != string.Empty)
				{
					// Create the Oid to know its structure and be able to retrieve the alternate key.
					lOid = Oid.Create(className);

					// Fill the alternate key with the field values from the editors.
					AlternateKey alternateKey = (AlternateKey)lOid.GetAlternateKey(alternateKeyName);
					alternateKey.SetValues(oidFields);

					if (executeQuery)
					{
						// Retrieve the suitable Oid.
						lOid = GetOidFromAlternateKey(alternateKey, alternateKeyName);
					}

					// Null if number of alternate fields does not match.
					if ((oidFields.Count != alternateKey.Fields.Count))
					{
						return null;
					}
				}
				else
				{
					// Create the Oid and set its fields values from the editors.
					lOid = Oid.Create(className, oidFields);

					// Null if number of fields in oidField and Oid is not the same.
					if ((oidFields.Count != lOid.Fields.Count))
					{
						return null;
					}
				}
			}
			return lOid;
		}
		#endregion CreateOidFromOidFields
		#region AlternateKeyFromOid / OidFromAlternateKey
		/// <summary>
		/// Returns the alternate key corresponding with the Oid and the name specified as parameters.
		/// </summary>
		/// <param name="oid">Oid used to retrieve the alternate key.</param>
		/// <param name="alternateKeyName">Alternate key name.</param>
		/// <returns>AlternateKey object.</returns>
		public static AlternateKey GetAlternateKeyFromOid(Oid oid, string alternateKeyName)
		{
			DataTable dataTable = Adaptor.ExecuteQueryInstance(
									Agent,
									oid.ClassName,
									alternateKeyName,
									oid,
									string.Empty);

			if ((dataTable != null) && (dataTable.Rows.Count == 1))
			{
				Oid auxOid = ServerConnection.GetOid(dataTable, dataTable.Rows[0], alternateKeyName);
				AlternateKey alternateKey = (AlternateKey)auxOid.GetAlternateKey(alternateKeyName);
				return alternateKey;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Gets the Oid of the instance whose alternate key is specified as parameter.
		/// </summary>
		/// <param name="alternateKey">Alternate key.</param>
		/// <param name="alternateKeyName">Alternate key name.</param>
		/// <returns>The Oid corresponding with the alternate key specified.</returns>
		public static Oid GetOidFromAlternateKey(AlternateKey alternateKey, string alternateKeyName)
		{
			
			DataTable dataTable = Adaptor.ExecuteQueryInstance(
												Agent,
												alternateKey.GetOid().ClassName,
												alternateKeyName,
												alternateKey,
												string.Empty);
			// Process the result.
			if ((dataTable != null) && (dataTable.Rows.Count == 1))
			{
				// Extract the Oid from the DataTable.
				Oid auxOid = ServerConnection.GetOid(dataTable, dataTable.Rows[0], alternateKeyName);
				return auxOid;
			}
			else
			{
				return null;
			}
		}
		#endregion AlternateKeyFromOid / OidFromAlternateKey
		#endregion Oid

		#region Types
		/// <summary>
		/// Converts an object value into a string value, according a ModelType.
		/// </summary>
		/// <param name="modelType">Type used to convert.</param>
		/// <param name="value">Value to convert.</param>
		/// <returns>A string value.</returns>
		public static string ModelToString(ModelType modelType, object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			if (modelType == ModelType.Time)
			{
				return ((TimeSpan)value).ToString();
			}
			else if (modelType == ModelType.Date)
			{
				return ((DateTime)value).Date.ToString();
			}
			else
			{
				return value.ToString();
			}
		}
		/// <summary>
		/// Converts a string value into the suitable object value, according a ModelType.
		/// </summary>
		/// <param name="modelType">Type used to convert.</param>
		/// <param name="value">String value to convert.</param>
		/// <returns>An object value.</returns>
		public static object StringToModel(ModelType modelType, string value)
		{
			if (value == "")
			{
				return null;
			}
			if (modelType == ModelType.Autonumeric)
			{
				return Int32.Parse(value, System.Globalization.NumberStyles.Number, CultureManager.Culture);
			}
			if (modelType == ModelType.String)
			{
				return value;
			}
			if (modelType == ModelType.Int)
			{
				return Int32.Parse(value, System.Globalization.NumberStyles.Number, CultureManager.Culture);
			}
			if (modelType == ModelType.Nat)
			{
				return Int32.Parse(value, System.Globalization.NumberStyles.Number, CultureManager.Culture);
			}
			if (modelType == ModelType.Text)
			{
				return value;
			}
			if (modelType == ModelType.Time)
			{
				return Convert.ToDateTime(value).TimeOfDay;
			}
			if (modelType == ModelType.Date)
			{
				return Convert.ToDateTime(value);
			}
			if (modelType == ModelType.DateTime)
			{
				return Convert.ToDateTime(value);
			}
			if (modelType == ModelType.Real)
			{
				return decimal.Parse(value, CultureManager.Culture);
			}
			if (modelType == ModelType.Bool)
			{
				return Convert.ToBoolean(value);
			}
			if (modelType == ModelType.Password)
			{
				return value;
			}
			if (modelType == ModelType.Blob)
			{
				return value;
			}
			return null;
		}
		/// <summary>
		/// Returns the DateTime corresponding with the string value if the received value follows the specified DateTime mask
		/// </summary>
		/// <param name="value"></param>
		/// <param name="format"></param>
		/// <returns></returns>
		public static DateTime? StringToDateTime(string value, string format)
		{
			try
			{
				if (format == null || format.Equals(string.Empty))
				{
					return DateTime.Parse(value, CultureManager.Culture);
				}
				else
				{
					return DateTime.ParseExact(value, format, CultureManager.Culture);
				}
			}
			catch
			{
				return null;
			}
		}
		/// <summary>
		/// Converts a string type name into the suitable ModelType type.
		/// </summary>
		/// <param name="modelType">Type used to convert.</param>
		/// <returns>A ModelType enumerated value.</returns>
		public static ModelType StringToModelType(string modelType)
		{
			if ((string.Compare(modelType, "autonumeric", true) == 0) || (string.Compare(modelType, "autonumerico", true) == 0))
			{
				return ModelType.Autonumeric;
			}
			if (string.Compare(modelType, "string", true) == 0)
			{
				return ModelType.String;
			}
			if (string.Compare(modelType, "int", true) == 0)
			{
				return ModelType.Int;
			}
			if (string.Compare(modelType, "nat", true) == 0)
			{
				return ModelType.Nat;
			}
			if (string.Compare(modelType, "text", true) == 0)
			{
				return ModelType.Text;
			}
			if (string.Compare(modelType, "time", true) == 0)
			{
				return ModelType.Time;
			}
			if (string.Compare(modelType, "date", true) == 0)
			{
				return ModelType.Date;
			}
			if (string.Compare(modelType, "datetime", true) == 0)
			{
				return ModelType.DateTime;
			}
			if (string.Compare(modelType, "real", true) == 0)
			{
				return ModelType.Real;
			}
			if (string.Compare(modelType, "bool", true) == 0)
			{
				return ModelType.Bool;
			}
			if (string.Compare(modelType, "password", true) == 0)
			{
				return ModelType.Password;
			}
			if (string.Compare(modelType, "blob", true) == 0)
			{
				return ModelType.Blob;
			}
			return ModelType.Oid;
		}
		#endregion Types

		#region Get variable Types.
		/// <summary>
		/// Gets a collection with type variables of filter.
		/// </summary>
		/// <param name="className">Class Name.</param>
		/// <param name="filterName">Filter Name.</param>
		/// <returns></returns>
		public static Dictionary<string, ModelType> GetVariablesTypes(string className, string filterName)
		{
			return ExecuteMethod(GetFilterTypeName(className, filterName), "GetVariablesTypes", null, null) as Dictionary<string, ModelType>;
		}
		#endregion Get variable Types.
	}
}
