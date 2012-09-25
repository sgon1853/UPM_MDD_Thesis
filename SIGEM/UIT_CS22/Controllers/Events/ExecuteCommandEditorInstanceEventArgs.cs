// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Base class, event arguments for ExecuteCommandEditorInstance event.
	/// </summary>
	public class ExecuteCommandEditorInstanceEventArgs: ExecuteCommandEventArgs
	{
		#region Members
		/// <summary>
		/// Selected Oids.
		/// </summary>
		private List<Oid> mSelectedOids = null;
		#endregion Members

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the 'ExecuteCommandSelectionBackwardEventArgs' class.
        /// </summary>
        public ExecuteCommandEditorInstanceEventArgs(List<Oid> selectedOids)
            : base(ExecuteCommandType.EditorInstance)
        {
            SelectedOids = (selectedOids == null ? new List<Oid>() : selectedOids);
        }
        #endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets the selcted Oids.
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


