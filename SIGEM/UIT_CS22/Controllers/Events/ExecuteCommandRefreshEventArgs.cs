// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Base class, event arguments for ExecuteCommand event.
	/// </summary>
	public class ExecuteCommandRefreshEventArgs: ExecuteCommandEventArgs
    {
        #region Members
        /// <summary>
        /// Oids to be refreshed
        /// </summary>
        private List<Oid> mSelectedOids = null;
        #endregion Members

        #region Constructors
        /// <summary>
		/// Initializes a new instance of the 'CommandKeyEventArgs' class.
		/// </summary>
		public ExecuteCommandRefreshEventArgs(List<Oid> selectedOids)
			:base(ExecuteCommandType.ExecuteRefresh)
		{
			SelectedOids = (selectedOids == null ? new List<Oid>() : selectedOids);
		}
		#endregion Constructors

        #region Properties
        /// <summary>
        /// Oids to be refreshed
        /// </summary>
        public virtual List<Oid> SelectedOids
		{
			get 
            { 
                return mSelectedOids; 
            }
			protected set 
            { 
                mSelectedOids = value; 
            }
		}
        #endregion Properties
    }
}



