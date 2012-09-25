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
	public class SelectNextPreviousInstanceEventArgs : SelectInstanceEventArgs
    {
        #region Members
        /// <summary>
        /// Select next instance flag
        /// </summary>
        private bool mSelectNext;
        /// <summary>
        /// New selected instance
        /// </summary>
        private List<Oid> mNewSelectedInstance;

        #endregion Members

        #region Constructors
        /// <summary>
        /// Initialices a new instance of SelectNextPreviousInstanceEventArgs
        /// </summary>
        /// <param name="currentOid">Initial instance</param>
        /// <param name="refreshInstance">Indicates if the current instance must be refreshed</param>
        /// <param name="selectNext">Indicates what is the new selection. True indicates select next, False indicates select previous.</param>
        public SelectNextPreviousInstanceEventArgs(Oid currentOid, bool refreshInstance, bool selectNext)
			: base(currentOid, refreshInstance)
		{
            mSelectNext = selectNext;
		}
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Select next instance flag
        /// </summary>
        public bool SelectNext
        {
            get
            {
                return mSelectNext;
            }
            set
            {
                mSelectNext = value;
            }
        }
        /// <summary>
        /// New selected instance
        /// </summary>
        public List<Oid> NewSelectedInstance
        {
            get
            {
                return mNewSelectedInstance;
            }
            set
            {
                mNewSelectedInstance = value;
            }
        }
        #endregion Properties
    }
}



