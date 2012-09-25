// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using SIGEM.Client.Logics;

namespace SIGEM.Client.Controllers
{
    /// <summary>
	/// Class that manages the ChangeEnableEventArgs.
	/// </summary>
	public class EnabledChangedEventArgs : ArgumentEventArgs
    {
        #region Members
        /// <summary>
        /// Previous value of Enabled
        /// </summary>
        private bool mOldEnabled;
        #endregion Members

        #region Enabled
        /// <summary>
        /// Current value of Enabled
        /// </summary>
        public bool Enabled
		{
			get
			{
				return Argument.Enabled;
			}
		}
        /// <summary>
        /// Previous value of Enabled
        /// </summary>
        public bool OldEnabled
        {
            get
            {
                return OldEnabled;
            }
            set
            {
                mOldEnabled = value;
            }
        }
		#endregion Enabled

		#region Constructor
        public EnabledChangedEventArgs()
            : base(null,DependencyRulesAgentLogic.User)
        { }
        public EnabledChangedEventArgs(ArgumentController argument, DependencyRulesAgentLogic agentRule)
			: base(argument,agentRule)
		{ }
        public EnabledChangedEventArgs(ArgumentController argument, DependencyRulesAgentLogic agentRule, bool oldEnabledValue)
            : base(argument, agentRule)
        {
            mOldEnabled = oldEnabledValue;
        }
        #endregion Constructor
    }
}



