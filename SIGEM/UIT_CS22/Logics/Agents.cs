// v3.8.4.5.b
using System;
using System.Collections.Generic;

using SIGEM.Client.Oids;

namespace SIGEM.Client.Logics
{
	/// <summary>
	/// Class that manages the application agents defined in the model.
	/// </summary>
	public static class Agents
	{
		/// <summary>
		/// 'Administrador' application agent.
		/// </summary>
		public const string Administrador = "Administrador";

		/// <summary>
		/// All application agents.
		/// </summary>
		public static string[] All = { Administrador };

		#region Retrieve suitable Agent connected Oid
		/// <summary>
		/// Gets the agent connected Oid if it is 'Administrador'. In other case, it returns null.
		/// </summary>
		public static AdministradorOid AGENT_Administrador
		{
			get
			{
				AdministradorOid agentAdministradorOid = (Logic.Agent as AdministradorOid);
				return agentAdministradorOid;
			}
		}
		#endregion Retrieve suitable Agent connected Oid

		#region GetLogInAgents
		/// <summary>
		/// Get the list of application agents that must be loaded into the login scenario's combo.
		/// </summary>
		/// <returns>List of login agents.</returns>
		public static List<LogInAgent> GetLogInAgents()
		{
			List<LogInAgent> lLogInAgents = new List<LogInAgent>();

			// 'Administrador'.
			lLogInAgents.Add(new LogInAgent(
				"Administrador",	// Name.
				CultureManager.TranslateString("Clas_1348605050880238_Alias", "Administrador"),	// Alias.
				"Clas_1348605050880238_Alias",	// IdXML.
				string.Empty));	// Alternate Key Name.
			return lLogInAgents;
		}

		/// <summary>
		/// Get the information of an agent that can access to the application from its name.
		/// </summary>
		/// <param name="agentName">Indicates the name of the agent asked for.</param>
		/// <returns>LogInAgent object.</returns>
		public static LogInAgent GetLogInAgentByName(string agentName)
		{
			// Search the agent by its class name.
			foreach (LogInAgent loginAgent in GetLogInAgents())
			{
				if (string.Compare(loginAgent.Name, agentName, true) == 0)
				{
					return loginAgent;
				}
			}

			return null;
		}
		#endregion GetLogInAgents
	}

	#region LogInAgent
	/// <summary>
	/// LogInAgent class.
	/// </summary>
	public class LogInAgent
	{
		#region Members
		/// <summary>
		/// Agent's name.
		/// </summary>
		private string mName;
		/// <summary>
		/// Agent's alias.
		/// </summary>
		private string mAlias;
		/// <summary>
		/// Agent's IdXML.
		/// </summary>
		private string mIdXML;
		/// <summary>
		/// Name of the Agent's Alternate Key.
		/// </summary>
		private string mAlternateKeyName;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'LogInAgent' class.
		/// </summary>
		/// <param name="name">Agent's name.</param>
		/// <param name="alias">Agent's alias.</param>
		/// <param name="idXML">Agent's IdXML.</param>
		/// <param name="alternateKeyName">Name of the Agent's Alternate Key.</param>
		public LogInAgent(string name, string alias, string idXML, string alternateKeyName)
		{
			this.Name = name;
			this.Alias = alias;
			this.IdXML = idXML;
			this.AlternateKeyName = alternateKeyName;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets the agent's name.
		/// </summary>
		public string Name
		{
			get
			{
				return mName;
			}
			protected set
			{
				mName = value;
			}
		}
		/// <summary>
		/// Gets the agent's alias.
		/// </summary>
		public string Alias
		{
			get
			{
				return mAlias;
			}
			set
			{
				mAlias = value;
			}
		}
		/// <summary>
		/// Gets the agent's IdXML.
		/// </summary>
		public string IdXML
		{
			get
			{
				return mIdXML;
			}
			protected set
			{
				mIdXML = value;
			}
		}
		/// <summary>
		/// Gets the name of the Agent's Alternate Key.
		/// </summary>
		public string AlternateKeyName
		{
			get
			{
				return mAlternateKeyName;
			}
			protected set
			{
				mAlternateKeyName = value;
			}
		}
		#endregion Properties
	}
	#endregion LogInAgent
}
