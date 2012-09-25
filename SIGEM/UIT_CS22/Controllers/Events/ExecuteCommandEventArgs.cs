// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SIGEM.Client.Controllers
{
   
	/// <summary>
	/// Base class, event arguments for ExecuteCommand event.
	/// </summary>
	public class ExecuteCommandEventArgs: ExecuteCommandBaseEventArgs 
	{
        #region Members
        /// <summary>
        /// Key pressed
        /// </summary>
        private Keys mKey = Keys.None;
        /// <summary>
        /// Indicate if the Key has been processed or not
        /// </summary>
        private bool mHandled = false;
        #endregion Members

        #region Properties
        public Keys Key
        {
            get
            {
                return mKey;
            }
        }
        public bool Handled
        {
            get
            {
                return mHandled;
            }
            set
            {
                mHandled = value;
            }
        }
        #endregion Properties

        #region Constructors
        /// <summary>
		/// Initializes a new instance of the 'CommandKeyEventArgs' class.
		/// </summary>
		public ExecuteCommandEventArgs(ExecuteCommandType executeCommandType)
		{
			ExecuteCommandType = executeCommandType;
		}
        /// <summary>
        /// Initializes a new instance of the class for a specific key
        /// </summary>
        public ExecuteCommandEventArgs(Keys key)
        {
            ExecuteCommandType = ExecuteCommandType.None;
            mKey = key;
        }
		#endregion Constructors
	}
}



