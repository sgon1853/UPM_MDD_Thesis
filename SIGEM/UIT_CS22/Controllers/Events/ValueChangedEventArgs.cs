// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using SIGEM.Client.Logics;

namespace SIGEM.Client.Controllers
{
    /// <summary>
	/// Class that manages the ChangeValueEventArgs.
	/// </summary>
	public class ValueChangedEventArgs: ArgumentEventArgs
	{
		#region Members
		/// <summary>
		/// Indicates the previous value of the argument.
		/// </summary>
		private object mOldValue;

		/// <summary>
		/// Indicates the new value of the argument.
		/// </summary>		
		private object mNewValue;
		#endregion Members

		#region Properties
		/// <summary>
		/// Indicates the new value of the argument.
		/// </summary>
		public object NewValue
		{
			get { return mNewValue; }
			set { mNewValue = value; }
		}

		/// <summary>
		/// Indicates the previous value of the argument.
		/// </summary>	
		public object OldValue
		{
			get { return mOldValue; }
			set { mOldValue = value; }
		}

		#endregion Properties

		#region Value
		public object Value
		{
			get
			{
				return Argument.Value;
			}
		}
		#endregion Value

		#region Constructor
        public ValueChangedEventArgs()
            : base(null,DependencyRulesAgentLogic.User)
        {
        }
		public ValueChangedEventArgs(ArgumentController argument, DependencyRulesAgentLogic agentRule)
			: base(argument,agentRule)
		{
        }
        public ValueChangedEventArgs(ArgumentController argument, object oldValue, object newValue, DependencyRulesAgentLogic agentRule)
            : base(argument, agentRule)
        {
            mOldValue = oldValue;
            mNewValue = newValue;
        }
        #endregion Constructor
    }
}



