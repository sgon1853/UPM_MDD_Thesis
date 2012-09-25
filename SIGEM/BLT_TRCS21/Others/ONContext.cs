// 3.4.4.5
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using SIGEM.Business.OID;
using SIGEM.Business.Instance;
using SIGEM.Business.Collection;
using SIGEM.Business.XML;
using SIGEM.Business.Query;
using SIGEM.Business.Executive;
using SIGEM.Business.Data;
using SIGEM.Business.Attributes;
using SIGEM.Business.Action;
using SIGEM.Business.Server;
using SIGEM.Business.Types;
using SIGEM.Business.Exceptions;


namespace SIGEM.Business
{
	/// <summary>
	/// This component has the information about all the context of the execution of a service or a query.
	/// </summary>

	internal delegate void ONIntegrityConstraintsHandler(ONContext onContext);
	internal delegate void ONTriggersHandler(ONContext onContext);
	internal delegate void ONAcceptedTriggersHandler(ONContext onContext);

	internal class ONContext: IDisposable
	{
		#region Members
		// Agent
		private ONOid mOidAgent;
		// Agent active facets
		public StringCollection LeafActiveAgentFacets = new StringCollection();
		// BD Connection
		private object mSqlConnection;
		// Calculate query instances number
		public bool CalculateQueryInstancesNumber;
		#endregion

		#region Properties
		/// <summary>
		/// Represents the OID of the Agent connected to the system
		/// </summary>
		public ONOid OidAgent
		{
			get
			{
				return mOidAgent;
			}
			set
			{
				if (new ONBool((object) mOidAgent == null) || (mOidAgent != value))
				{
					mOidAgent = value;
					if ((object) mOidAgent != null)
					{
						LeafActiveAgentFacets = GetLeafActiveAgentFacets();
						AgentAssign(mOidAgent);
					}
				}
			}
		}
		/// <summary>
		/// Represents the connection to the DataBase
		/// </summary>
		public object SqlConnection
		{
			get
			{
				if (mSqlConnection == null)
					mSqlConnection = ONDBData.GetConnection();
				return mSqlConnection;
			}
			set
			{
				if ((value == null) && (mSqlConnection != null))
					ONDBData.CloseConnection(mSqlConnection);

				mSqlConnection = value;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public ONContext()
		{
			mOidAgent = null;
			mSqlConnection = null;
			CalculateQueryInstancesNumber = false;
		}
		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="onContext">Context to copy from</param>
		public ONContext(ONContext onContext)
		{
			mOidAgent = onContext.OidAgent;
			mSqlConnection = null;
			CalculateQueryInstancesNumber = false;
			LeafActiveAgentFacets  = onContext.LeafActiveAgentFacets;
		}
		#endregion

		#region Ticket Control
		public ONOid SetTicket(double dtdVersion, string clientName, string ticket)
		{
			try
			{
				ONOid lAgent = ONSecureControl.ValidateTicket(dtdVersion, ticket, clientName);
				OidAgent = lAgent;
			}
			catch (ONAgentValidationException)
			{
				if(!ONSecureControl.SecureServer)
					return null;
				else
					throw;
			}

			return mOidAgent;
		}
		public string GetTicket(double dtdVersion, string clientName)
		{
			return ONSecureControl.GetNextTicket(dtdVersion, clientName, OidAgent);
		}
		#endregion

		#region Agent Control
		/// <summary>
		/// Assign the agent connected as a principal of the system
		/// </summary>
		/// <param name="OidAgent">OID of the agent connected to the system</param>
		public IPrincipal AgentAssign(ONOid OidAgent)
		{
			// Retrieve old principal
			IPrincipal lPrincipalOld = Thread.CurrentPrincipal;

			// Create Identity
			GenericIdentity lIdentity = new GenericIdentity(OidAgent.ToString());

			// Create Principal
			string[] lRoles = new string[LeafActiveAgentFacets.Count];

			for (int i = 0; i < LeafActiveAgentFacets.Count; i++)
				lRoles[i] = LeafActiveAgentFacets[i];
				
			GenericPrincipal lPrincipal = new GenericPrincipal(lIdentity, lRoles);

			// Assign to the current thread
			Thread.CurrentPrincipal = lPrincipal;

			return lPrincipalOld;
		}
		/// <summary>
		/// Assign the "INTERN" agent as a principal of the system
		/// </summary>
		/// <param name="OidAgent">OID of the agent connected to the system</param>
		public static IPrincipal AgentInternAssign()
		{
			// Retrieve old principal
			IPrincipal lPrincipalOld = Thread.CurrentPrincipal;

			// Create Identity
			IIdentity lIdentity = lPrincipalOld.Identity;

			// Create Principal
			string[] lRoles = { "INTERN" };
			GenericPrincipal lPrincipal = new GenericPrincipal(lIdentity, lRoles);

			// Assign to the current thread
			Thread.CurrentPrincipal = lPrincipal;

			return lPrincipalOld;
		}
		/// <summary>
		/// Assign the "INTERN" agent as a principal of the system
		/// </summary>
		/// <param name="OidAgent">OID of the agent connected to the system</param>
		public static IPrincipal PrincipalAssign(IPrincipal principal)
		{
			// Retrieve old principal
			IPrincipal lPrincipalOld = Thread.CurrentPrincipal;

			// Assign to the current thread
			Thread.CurrentPrincipal = principal;

			return lPrincipalOld;
		}
		/// <summary>
		/// Get the instance of the conected agent in an specified class. If the agent has not 
		/// an active facet of the given class it returns an empty Instance of the class.
		/// </summary>
		/// <param name="className">Name of the class in which the agent instance is returned</param>
		public ONInstance GetConnectedAgentInstance(string className)
		{
			return OidAgent.GetInstance(this).GetFacet(className);
		}
		
		/// <summary>
		/// If the agent is part of an inheritance net it returns the deeper active facets of the net
		/// </summary>
		private StringCollection GetLeafActiveAgentFacets()
		{
			return OidAgent.GetInstance(this).Root().LeafActiveFacets();
		}
		#endregion

		#region Reflection (Component)
		#region GetComponent
		/// <summary>
		/// Create a component of the system 
		/// </summary>
		/// <param name="component">Name of the component to be created</param>
		private static object GetComponent(string component)
		{
			// Create Component
			Type lComponentType = Type.GetType(component, true, true);

			if (!lComponentType.IsClass)
				throw new ONXMLException(null,"Class");

			return Activator.CreateInstance(lComponentType);
		}
		/// <summary>
		/// Create a component of the system
		/// </summary>
		/// <param name="component">Name of the component to be created</param>
		/// <param name="parameters">Parameters neede to create the instance of the component</param>
		private static object GetComponent(string component, object[] parameters)
		{
			// Create Component
			Type lComponentType = Type.GetType(component, true, true);

			if (!lComponentType.IsClass)
				throw new ONXMLException(null,"Class");

			return Activator.CreateInstance(lComponentType, parameters);
		}
		/// <summary>
		/// Create an instance of the OID component
		/// </summary>
		/// <param name="className">Name of the class necessary to create the component</param>
		public static ONOid GetComponent_OID(string className)
		{
			return GetComponent("SIGEM.Business.OID." + className + "OID") as ONOid;
		}
		/// <summary>
		/// Create an instance of the Collection component
		/// </summary>
		/// <param name="className">Name of the class necessary to create the component</param>
		/// <param name="onContext">Object of type onContext needed to create an object of this type</param>
		public static ONCollection GetComponent_Collection(string className, ONContext onContext)
		{
			object[] lParams = {onContext};

			return GetComponent("SIGEM.Business.Collection." + className + "Collection", lParams) as ONCollection;
		}
		/// <summary>
		/// Create an instance of the Instance component
		/// </summary>
		/// <param name="className">Name of the class necessary to create the component</param>
		/// <param name="onContext">Object of type onContext needed to create an object of this type</param>
		public static ONInstance GetComponent_Instance(string className, ONContext onContext)
		{
			object[] lParams = {onContext};

			if (className == "")
				return GetComponent("SIGEM.Business.Instance.GlobalTransactionInstance", lParams) as ONInstance;
			else
				return GetComponent("SIGEM.Business.Instance." + className + "Instance", lParams) as ONInstance;
		}
		/// <summary>
		/// Create an instance of the Server component
		/// </summary>
		/// <param name="className">Name of the class necessary to create the component</param>
		/// <param name="onContext">Object of type onContext needed to create an object of this type</param>
		/// <param name="instance">Object of type instance needed to create an object of this type</param>
		public static ONServer GetComponent_Server(string className, ONContext onContext, ONInstance instance)
		{
			object[] lParams = {onContext, instance};

			//if (className == "")
			//	return GetComponent("SIGEM.Business.Server.GlobalTransactionServer", lParams) as ONServer;
			  if (className == "" || className == "GlobalTransaction")
                		return new GlobalTransactionServer(onContext as ONServiceContext) as ONServer;
			else
				return GetComponent("SIGEM.Business.Server." + className + "Server", lParams) as ONServer;
		}
		/// <summary>
		/// Create an instance of the Executive component
		/// </summary>
		/// <param name="className">Name of the class necessary to create the component</param>
		public static ONExecutive GetComponent_Executive(string className)
		{
			if (className == "")
				return GetComponent("SIGEM.Business.Executive.GlobalTransactionExecutive") as ONExecutive;
			else
				return GetComponent("SIGEM.Business.Executive." + className + "Executive") as ONExecutive;
		}
		/// <summary>
		/// Create an instance of the Data component
		/// </summary>
		/// <param name="className">Name of the class necessary to create the component</param>
		/// <param name="onContext">Object of type onContext needed to create an object of this type</param>
		public static ONData GetComponent_Data(string className, ONContext onContext)
		{
			object[] lParams = {onContext};

			if (className == "")
				return GetComponent("SIGEM.Business.Data.GlobalTransactionData", lParams) as ONData;
			else
				return GetComponent("SIGEM.Business.Data." + className + "Data", lParams) as ONData;
		}
		/// <summary>
		/// Create an instance of the Query component
		/// </summary>
		/// <param name="className">Name of the class necessary to create the component</param>
		/// <param name="onContext">Object of type onContext needed to create an object of this type</param>
		public static ONQuery GetComponent_Query(string className, ONContext onContext)
		{
			object[] lParams = {onContext};

			return GetComponent("SIGEM.Business.Query." + className + "Query", lParams) as ONQuery;
		}
		/// <summary>
		/// Create an instance of the Action component
		/// </summary>
		/// <param name="className">Name of the class necessary to create the component</param>
		/// <param name="onContext">Object of type onContext needed to create an object of this type</param>
		public static ONAction GetComponent_Action(string className, ONContext onContext)
		{
			object[] lParams = {onContext};

			if (className == "")
				return GetComponent("SIGEM.Business.Action.GlobalTransactionAction", lParams) as ONAction;
			else
				return GetComponent("SIGEM.Business.Action." + className + "Action", lParams) as ONAction;
		}
		/// <summary>
		/// Create an instance of the OrderCriteria component
		/// </summary>
		/// <param name="className">Name of the class necessary to create the component</param>
		/// <param name="orderCriteria">Name of the ordercriteria defined in the model object</param>
		public static ONOrderCriteria GetComponent_OrderCriteria(string className, string orderCriteria)
		{
			if (orderCriteria == "")
				return null;

			try
			{
				return GetComponent("SIGEM.Business.Query." + className + orderCriteria + "OrderCriteria") as ONOrderCriteria;
			}
			catch
			{
				return null;
			}
		}
		/// <summary>
		/// Create an instance of the XML component
		/// </summary>
		/// <param name="className">Name of the class necessary to create the component</param>
		public static ONXml GetComponent_XML(string className)
		{
			return GetComponent("SIGEM.Business.XML." + className + "XML") as ONXml;
		}
		/// <summary>
		/// Create an instance of the ONType component
		/// </summary>
		/// <param name="type">Name of the type to create the component</param>
		public static ONSimpleType GetComponent_ONType(string type)
		{
			return GetComponent("SIGEM.Business.Types.ON" + type) as ONSimpleType;
		}
		public static ONFilter GetComponent_Filter(string filterName, object[] arguments)
		{
			return GetComponent("SIGEM.Business.Query." + filterName, arguments) as ONFilter;
		}
        public static ONFilter GetComponent_NavigationalFilter(string filterId, object[] arguments)
        {
            return Activator.CreateInstance(GetType_NavigationalFilter(filterId), arguments) as ONFilter;
        }
        #endregion

		#region GetType
		/// <summary>
		/// Returns the type of the Instance component
		/// </summary>
		/// <param name="className">Name of the class necessary to get the type</param>
		public static Type GetType_Instance(string className)
		{
			if (className == "")
				return Type.GetType("SIGEM.Business.Instance.GlobalTransactionInstance", true, true);
			else
				return Type.GetType("SIGEM.Business.Instance." + className + "Instance", true, true);
		}
		/// <summary>
		/// Returns the type of the XML component
		/// </summary>
		/// <param name="className">The name of the class necessary to get the type</param>
		public static Type GetType_XML(string className)
		{
			if (className == "")
				return Type.GetType("SIGEM.Business.XML.GlobalTransactionXML", true, true);
			else
				return Type.GetType("SIGEM.Business.XML." + className + "XML", true, true);
		}
		/// <summary>
		/// Returns the type of the Query component
		/// </summary>
		/// <param name="className">The name of the class necessary to get the type</param>
		public static Type GetType_Query(string className)
		{
			return Type.GetType("SIGEM.Business.Query." + className + "Query", true, true);
		}
		/// <summary>
		/// Returns the type of the Collection component
		/// </summary>
		/// <param name="className">The name of the class necessary to get the type</param>
		public static Type GetType_Collection(string className)
		{
			return Type.GetType("SIGEM.Business.Collection." + className + "Collection", true, true);
		}
		/// <summary>
		/// Returns the type of the Data component
		/// </summary>
		/// <param name="className">Name of the class necessary to get the type</param>
		public static Type GetType_Data(string className)
		{
			if (className == "")
				return Type.GetType("SIGEM.Business.Data.GlobalTransactionData", true, true);
			else
				return Type.GetType("SIGEM.Business.Data." + className + "Data", true, true);
		}
		/// <summary>
		/// Returns the type of the Action component
		/// </summary>
		/// <param name="className">Name of the class necessary to get the type</param>
		public static Type GetType_Action(string className)
		{
			if (className == "")
				return Type.GetType("SIGEM.Business.Action.GlobalTransactionAction", true, true);
			else
				return Type.GetType("SIGEM.Business.Action." + className + "Action", true, true);
		}
		/// <summary>
		/// Returns the type of the LV component
		/// </summary>
		/// <param name="className">The name of the class necessary to get the type</param>
		public static Type GetType_LV(string className)
		{
			return Type.GetType("SIGEM.Business.LV." + className + "LV", true, true);
		}
        /// <summary>
        /// Returns the navigational filter class type corresponding to the navigationalFilterId
        /// </summary>
        /// <param name="navigationalFilterId">Navigational filter identifier</param>
        /// <returns>Navigational filter class type</returns>
        public static Type GetType_NavigationalFilter(string navigationalFilterId)
        {
            foreach (Type lType in Assembly.GetExecutingAssembly().GetTypes())
                foreach (ONFilterAttribute lAttribute in lType.GetCustomAttributes(typeof(ONFilterAttribute), true))
                    if (string.Compare(lAttribute.FilterName, navigationalFilterId, true) == 0)
                        return lType;
            return null;
        }
		/// <summary>
		/// Returns the type of the a component
		/// </summary>
		/// <param name="componentType">Type of the component in a string to return the type</param>
		/// <param name="className">Name of the class necessary to get the type</param>
		public static Type GetType(string componentType, string className)
		{
			if (className == "")
				return Type.GetType("SIGEM.Business." + componentType + ".GlobalTransaction" + componentType, true, true);
			else
				return Type.GetType("SIGEM.Business." + componentType + "." + className + componentType, true, true);
		}
		#endregion
		#endregion

		#region Reflection (Methods)
		#region Execute function
		/// <summary>
		/// Execute the User Functions defined in the system
		/// </summary>
		/// <param name="function">Name of the user function to be executed</param>
		/// <param name="parameters">Parameters needed to execute the user function</param>
		public static object ExecuteFunction(string function, object[] parameters)
		{
			Type lFunction = typeof(ONUserFunctions);
			foreach (MethodInfo lMethod in lFunction.GetMethods())
				if (string.Compare(lMethod.Name, function, true) == 0)
					return ONContext.InvoqueMethod(null, lMethod as MethodBase, parameters);

			lFunction = typeof(ONStdFunctions);
			foreach (MethodInfo lMethod in lFunction.GetMethods())
				if (string.Compare(lMethod.Name, function, true) == 0)
					return ONContext.InvoqueMethod(null, lMethod as MethodBase, parameters);

			return null;
		}
		#endregion

		#region Invoque Method
		/// <summary>
		/// Check if the method to be executed has attributes
		/// </summary>
		/// <param name="mInfo">Variable that has all the information about the method</param>
		/// <param name="attribute">The attribute to be checked</param>
		private static bool CheckMethodWithAttribute(MemberInfo mInfo, object attribute)
		{
			object[] lAttributes = mInfo.GetCustomAttributes(attribute.GetType(), true);
			
			foreach (object lAttribute in lAttributes)
			{
				if (lAttribute.Equals(attribute))
					return true;
			}

			return false;
		}
		/// <summary>
		/// Execute the method via reflection mode
		/// </summary>
		/// <param name="component">Component that the method is defined</param>
		/// <param name="method">Method to be executed</param>
		/// <param name="parameters">Parameters needed to execute the method</param>
		public static object InvoqueMethod(object component, MethodBase method, object[] parameters)
		{
			try
			{
				return method.Invoke(component, parameters);
			}
			catch (Exception e)
			{
				throw e.InnerException;
			}
		}

        /// <summary>
        /// Execute the method via reflection mode
        /// </summary>
        /// <param name="type">Type of the component where is the method to be executed</param>
        /// <param name="methodName">Name of the method to be executed</param>
        /// <param name="parameters">Parameters needed to execute the method</param>
        public static object InvoqueMethod(object component, string methodName, object[] parameters)
        {
            MethodInfo lService = component.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            if (lService == null)
            throw new Exception("Method not found");
            return ONContext.InvoqueMethod(component, lService as MethodBase, parameters);
        }

		/// <summary>
		/// Checks if the method exists
		/// </summary>
		/// <param name="type">Type of the object to retrieve all their methods</param>
		/// <param name="attribute">Attribute to be looked for in the type</param>
		/// <param name="xmlAttribute">Name of the Attribute in string format</param>
		public static bool ExistMethods(Type type, Type attribute, string xmlAttribute)
		{
			foreach (MethodInfo lMethodInfo in type.GetMethods())
			{
				foreach (IONAttribute lAttribute in lMethodInfo.GetCustomAttributes(typeof(IONAttribute), true))
				{
					Type lAttributeType = lAttribute.GetType();
					if ((lAttributeType == attribute || lAttributeType.IsSubclassOf(attribute)) && (string.Compare(lAttribute.ToString(), xmlAttribute, true) == 0))
						return true;
				}
			}

			return false;
		}
		/// <summary>
		/// Retrieve all the methods of a particular type
		/// </summary>
		/// <param name="type">Type of the object to retrieve all their methods</param>
		/// <param name="attributeType">Attribute to be looked for in the type</param>
		/// <param name="xmlAttribute">Name of the Attribute in string format</param>
		public static List<MethodInfo> GetMethods(Type type, Type attributeType, string xmlAttribute)
		{
			List<MethodInfo> lReturn = new List<MethodInfo>();

			foreach (MethodInfo lMethodInfo in type.GetMethods())
			{
				foreach (IONAttribute lAttribute in lMethodInfo.GetCustomAttributes(typeof(IONAttribute), true))
				{
					Type lAttributeType = lAttribute.GetType();
					if ((lAttributeType == attributeType || lAttributeType.IsSubclassOf(attributeType)) && (string.Compare(lAttribute.ToString(), xmlAttribute, true) == 0))
						lReturn.Add(lMethodInfo);
				}
			}

			return lReturn;
		}

        /// <summary>
		/// Retrieve all the methods of a particular type
		/// </summary>
		/// <param name="type">Type of the object to retrieve all their methods</param>
		/// <param name="name">Name of the service</param>
        public static MethodInfo GetMethods(Type type, string name)
        {
            MethodInfo lReturn = null;

            foreach (MethodInfo lMethodInfo in type.GetMethods())
            {
      	        if ((string.Compare(lMethodInfo.Name, name, true) == 0))
            	    lReturn = lMethodInfo;
            }
            return lReturn;
        }
		/// <summary>
		/// Execute the method via reflection mode
		/// </summary>
		/// <param name="component">Component that the method is defined</param>
		/// <param name="attribute">Attribute to be looked for in the type</param>
		/// <param name="xmlAttribute">Name of the Attribute in string format</param>
		/// <param name="parameters">Parameters needed to execute the method</param>
		public static object InvoqueMethod(object component, Type attribute, string xmlAttribute, object[] parameters)
		{
			object lReturn = null;

			foreach (MethodInfo lMethodInfo in component.GetType().GetMethods())
			{
				foreach (IONAttribute lAttribute in lMethodInfo.GetCustomAttributes(typeof(IONAttribute), true))
				{
					Type lAttributeType = lAttribute.GetType();
					if ((lAttributeType == attribute || lAttributeType.IsSubclassOf(attribute)) && (string.Compare(lAttribute.ToString(), xmlAttribute, true) == 0))
						return ONContext.InvoqueMethod(component, lMethodInfo, parameters);
				}
			}

			return lReturn;
		}
		/// <summary>
		/// Execute the method via reflection mode
		/// </summary>
		/// <param name="type">Type of the component where is the method to be executed</param>
		/// <param name="attribute">Attribute to be looked for in the type</param>
		/// <param name="parameters">Parameters needed to execute the method</param>
		public static object InvoqueMethod(Type type, Attribute attribute, object[] parameters)
		{
			MemberInfo[] lServices = type.FindMembers(MemberTypes.Method, BindingFlags.Public | BindingFlags.Instance, new MemberFilter(ONContext.CheckMethodWithAttribute), attribute);

			if (lServices.Length <= 0)
				throw new Exception("Method not found");

			if (lServices.Length == 1)
				return ONContext.InvoqueMethod(null, lServices[0] as MethodBase, parameters);

			foreach (object lService in lServices)
				ONContext.InvoqueMethod(null, lService as MethodBase, parameters);
			return null;
		}
		/// <summary>
		/// Execute the method via reflection mode
		/// </summary>
		/// <param name="type">Type of the component where is the method to be executed</param>
		/// <param name="methodName">Name of the method to be executed</param>
		/// <param name="parameters">Parameters needed to execute the method</param>
		public static object InvoqueMethod(Type type, string methodName, object[] parameters)
		{
			foreach (MethodInfo lMethodInfo in type.GetMethods())
			{
				if ((string.Compare(lMethodInfo.Name, methodName, true) == 0) && (lMethodInfo.GetParameters().Length == parameters.Length))
					return ONContext.InvoqueMethod(null, lMethodInfo, parameters);
			}

			throw new Exception("Method not found");
		}
		#endregion
		#endregion

		#region Reflection (Properties)
		public static PropertyInfo GetPropertyInfoWithAttribute(Type type, Type attributeType, string attributeParameter)
		{
			object[] lPropertyInfos = type.GetProperties();

			foreach (PropertyInfo lPropertyInfo in lPropertyInfos)
			{
				object[] lAttributes = lPropertyInfo.GetCustomAttributes(attributeType, false);

				foreach (Attribute lAttribute in lAttributes)
				{
					if ((attributeParameter == null) || (string.Compare(lAttribute.ToString(), attributeParameter, true) == 0))
						// Create Component
						return (lPropertyInfo);
				}
			}

			return null;
		}
		public static List<PropertyInfo> GetPropertiesInfoWithAttribute(Type type, Type attributeType, string attributeParameter)
		{
			object[] lPropertyInfos = type.GetProperties();
			List<PropertyInfo> lReturnValues = new List<PropertyInfo>();

			foreach (PropertyInfo lPropertyInfo in lPropertyInfos)
			{
				object[] lAttributes = lPropertyInfo.GetCustomAttributes(attributeType, false);

				foreach (Attribute lAttribute in lAttributes)
				{
					if ((attributeParameter == null) || (string.Compare(lAttribute.ToString(), attributeParameter, true) == 0))
						// Create Component
						lReturnValues.Add(lPropertyInfo);
				}
			}

			return lReturnValues;
		}
        public static List<PropertyInfo> GetPropertiesInfoWithAttribute(Type type, Type attributeType)
		{
			List<PropertyInfo
                > lReturnValues = new List<PropertyInfo>();

			foreach (PropertyInfo lPropertyInfo in type.GetProperties())
			{
				object[] lAttributes = lPropertyInfo.GetCustomAttributes(attributeType, false);

				if (lAttributes.Length > 0)
					lReturnValues.Add(lPropertyInfo);
			}

			return lReturnValues;
		}
		public static object GetPropertyWithAttribute(object component, Type attributeType, string attributeParameter)
		{
			PropertyInfo lPropertyInfo = GetPropertyInfoWithAttribute(component.GetType(), attributeType, attributeParameter);
			if (lPropertyInfo != null)
				return lPropertyInfo.GetValue(component, null);

			return null;
		}
        
        public static ArrayList GetPropertiesWithAttribute(object component, Type attributeType)
		{
			ArrayList lProperties = new ArrayList();

			List<PropertyInfo> lPropertiesInfo = GetPropertiesInfoWithAttribute(component.GetType(), attributeType);
			foreach (PropertyInfo lPropertyInfo in lPropertiesInfo)
				lProperties.Add(lPropertyInfo.GetValue(component, null));

			return lProperties;
		}

		public static Attribute GetAttributeInProperty(Type type, Type attributeType, string attributeParameter)
		{
			object[] lPropertyInfos = type.GetProperties();

			foreach (PropertyInfo lPropertyInfo in lPropertyInfos)
			{
				object[] lAttributes = lPropertyInfo.GetCustomAttributes(attributeType, false);

				foreach (Attribute lAttribute in lAttributes)
				{
					if ((attributeParameter == null) || (string.Compare(lAttribute.ToString(), attributeParameter, true) == 0))
						return lAttribute;
				}
			}

			return null;
		}
		#endregion

		#region IDisposable Methods
		/// <summary>
		/// Method to free resources. In this case removes the connection with the Data Base.
		/// </summary>
		public void Dispose()
		{
			if (mSqlConnection != null)
			{
				ONDBData.CloseConnection(mSqlConnection);
				mSqlConnection = null;
			}
		}
		#endregion

		#region Write file
		public static void WriteLog(string file, string text)
		{
			System.IO.StreamWriter lSW = System.IO.File.AppendText(file);
			lSW.WriteLine(text);
			lSW.Close();
		}
		#endregion Write file

 

	}
}

