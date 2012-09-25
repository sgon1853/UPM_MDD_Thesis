// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using SIGEM.Client.Adaptor;

namespace SIGEM.Client.Oids
{
	/// <summary>
	/// This class mantains information related to 
	/// the communication with the business logic.
	/// </summary>
	[Serializable]
	public abstract class AgentInfo : Oid
	{
		#region Members
		/// <summary>
		/// Communication ticket.
		/// </summary>
		private string mTicket = string.Empty;
		/// <summary>
		/// Communication message sequence.
		/// </summary>
		private int mSequence = 0;
		/// <summary>
		/// List of active facets of the connected Agent
		/// </summary>
		private List<string> mAgentFacets = new List<string>();
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets the communication ticket.
		/// </summary>
		public string Ticket
		{
			get 
			{
				return mTicket;
			}
			set 
			{
				mTicket = value;
			}
		}
		/// <summary>
		/// Gets or sets the communication message sequence.
		/// </summary>
		public int Sequence
		{
			get
			{
				return mSequence;
			}
			set
			{
				mSequence = value;
			}
		}

		/// <summary>
		/// Gets or sets the List of active facets of the connected Agent.
		/// </summary>
		public List<string> AgentFacets
		{
			get
			{
				return mAgentFacets;
			}
			set
			{
				mAgentFacets = value;
			}
		}
		#endregion Properties
		
		#region Constructors
		protected AgentInfo(string className, string alias, string idXML, FieldList fields)
			: base(className, alias, idXML, fields){}

		protected AgentInfo(string className, string alias, string idXML)
			: base(className, alias, idXML){}

		protected AgentInfo(string className, string alias, string idXML, IList<IOidField> fields)
			: base(className, alias, idXML, fields) { }

		public AgentInfo(string className, string alias, string idXML, object[] values, ModelType[] types)
			: base(className, alias, idXML, values, types){}

		public AgentInfo(string className, string alias, string idXML, IList<object> values, IList<ModelType> types)
			: base(className, alias, idXML, values, types){}
		#endregion Constructors

		#region Methods
		/// <summary>
		/// Checks if the class name passed as parameter is an active facet for the current agent.
		/// </summary>
		/// <param name="className">Name of the class "facet" to be checked.</param>
		/// <returns>True if the facet is activated.</returns>
		public bool IsActiveFacet(string className)
		{
			return mAgentFacets.Contains(className);
		}
		/// <summary>
		/// Checks if the list of class names passed as parameter are active facets for the current agent.
		/// </summary>
		/// <param name="classNames">List of class names or "facets" to be checked.</param>
		/// <returns>True if the facets are activated.</returns>
		public bool IsActiveFacet(List<string> classNames)
		{
			foreach (string agentFacet in mAgentFacets)
			{
				if (classNames.Contains(agentFacet))
					return true;
			}

			return false;
		}
		/// <summary>
		/// Checks if the classes passed as parameter in the string list are active facets for the current agent.
		/// </summary>
		/// <param name="classNames">String list of class names or "facets" to be checked.</param>
		/// <returns>True if the facets are activated.</returns>
		public bool IsActiveFacet(string[] classNames)
		{
			foreach (string agent in classNames)
			{
				if (mAgentFacets.Contains(agent))
					return true;
			}

			return false;
		}
		#endregion Methods

	}
}

