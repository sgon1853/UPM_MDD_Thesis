// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using SIGEM.Client.Logics;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the ExecuteFilterEventArgs.
	/// </summary>
	public class ExecuteFilterEventArgs : EventArgs
	{
        private bool mSuccess = true;
        private IUFilterController mArguments = null;
        public ExecuteFilterEventArgs(IUFilterController arguments)
		{
            mArguments = arguments;
        }

        public bool Success
        {
            get
            {
                return mSuccess;
            }
            set
            {
                mSuccess = value;
            }
        }

		public IUFilterController Arguments
		{
			get 
            { 
                return mArguments;
            }
			protected set 
            { 
                mArguments = value; 
            }
		}
    
    }

}



