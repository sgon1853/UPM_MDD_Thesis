// v3.8.4.5.b
using System;
using System.Collections.Generic;

namespace SIGEM.Client.Controllers
{

	/// <summary>
	/// Execute commands types.
	/// </summary>
	public enum ExecuteCommandType
	{
		None = 0,

		ExecuteFirstDestroyActionService = 10,
		ExecuteFirstCreateActionService = 11,
		ExecuteFirstNotDestroyNotCreateActionService = 12,

		ExecuteRefresh =20,
		ExecuteRetriveAll = 21,
		ExecuteSelectInstance = 22,
        ExecuteRefreshNode = 23,


		// Next-Previous in Service.
		ExecuteGoNextInstance = 30,
		ExecuteGoPreviousInstance = 31,
		ExecuteServiceAndGoNextInstance = 32,
		ExecuteServiceAndGoPreviousInstance = 33,
		ExecuteService = 34,
		
        // For editable PIU and IIU
        ExecuteDisplaySetService = 40,
        ValuesHasBeenModified = 41,

		// For all scenarios.
		ExecuteClose = 50,

		// Editor Instance
		EditorInstance = 60 
	}

	/// <summary>
	/// Base class, event arguments for Commands event.
	/// </summary>
	public class ExecuteCommandBaseEventArgs: EventArgs
	{
        #region Members
        /// <summary>
        /// Command Type
        /// </summary>
        private ExecuteCommandType mExecuteCommandType;
        #endregion Members

        #region Constructors
        /// <summary>
		/// Initializes a new instance of the 'ExecuteCommandBaseEventArgs' class.
		/// </summary>
		public ExecuteCommandBaseEventArgs() 
        {
            mExecuteCommandType= ExecuteCommandType.None;
        }
		#endregion Constructors

		#region Properties
		public ExecuteCommandType ExecuteCommandType
		{
			get { return mExecuteCommandType; }
			set { mExecuteCommandType = value; }
        }
        #endregion Properties
    }
}



