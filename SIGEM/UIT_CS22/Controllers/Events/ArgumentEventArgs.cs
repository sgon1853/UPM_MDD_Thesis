// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using SIGEM.Client.Logics;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the ArgumentEventArgs.
	/// </summary>
	public abstract class ArgumentEventArgs: EventArgs
	{
		#region Members
		/// <summary>
		/// Argument Reference
		/// </summary>
		private ArgumentController margument = null;
		/// <summary>
		/// Indicates the agent involved in this event.
		/// </summary>
		private DependencyRulesAgentLogic mAgent = DependencyRulesAgentLogic.Internal;
		#endregion Members

		#region Argument
		/// <summary>
		/// Argument Reference.
		/// </summary>
		public virtual ArgumentController Argument
		{
			get
			{
				return margument;
			}
			protected set
			{
				margument = value;
			}
		}
		#endregion Argument

		#region Name
		/// <summary>
		/// Argument Name.
		/// </summary>
		public string Name
		{
			get
			{
				return Argument.Name;
			}
		}
		#endregion Name

		#region Type of Dependence Rule Agent
		public DependencyRulesAgentLogic Agent
		{
			get
			{
				return mAgent;
			}
			set
			{
				mAgent = value;
			}
		}
		#endregion Type of Dependence Rule Agent

		#region Constructor
		protected ArgumentEventArgs(ArgumentController argument, DependencyRulesAgentLogic agentRule)
		{
			margument = argument;
			Agent = agentRule;
		}
		#endregion Constructor
	}
}



