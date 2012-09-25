// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Stores information related to Service Result event.
	/// </summary>
	public class SelectInstanceEventArgs : EventArgs
    {
        #region Members
        /// <summary>
        /// Indicates if the instance msut be refreshed
        /// </summary>
        private bool mRefreshInstance;
        /// <summary>
        /// OID instance
        /// </summary>
        private Oid mCurrentOid;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Initilices a new intance of SelectInstanceEventArgs
        /// </summary>
        /// <param name="currentOid">OID to be selected</param>
        /// <param name="refreshInstance">Insdicates in the instance must be refreshed or not</param>
        public SelectInstanceEventArgs(Oid currentOid, bool refreshInstance)
		{
			RefreshInstance = refreshInstance;
			CurrentOid = currentOid;
		}
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Indicates if the instance msut be refreshed
        /// </summary>
        public bool RefreshInstance
        {
            get
            {
                return mRefreshInstance;
            }
            set
            {
                mRefreshInstance = value;
            }
        }
        /// <summary>
        /// OID instance
        /// </summary>
        public Oid CurrentOid
        {
            get
            {
                return mCurrentOid;
            }
            set
            {
                mCurrentOid = value;
            }
        }
        #endregion Properties
    }
}



