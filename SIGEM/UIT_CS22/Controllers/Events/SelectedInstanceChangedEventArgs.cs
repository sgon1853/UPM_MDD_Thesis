// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Stores information related to Selected Instance Changed event.
	/// </summary>
	public class SelectedInstanceChangedEventArgs: EventArgs
	{
		#region Members
		/// <summary>
		/// List containing the selected Oids.
		/// </summary>
		private List<Oid> mSelectedInstances = null;
		/// <summary>
		/// List containing the actions keys to enable or disable the offered actions.
		/// </summary>
		private List<string> mEnabledActionsKeys = null;
		/// <summary>
		/// List containing the navigations keys to enable or disable the offered navigations.
		/// </summary>
		private List<string> mEnabledNavigationsKeys = null;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'SelectedInstanceChangedEventArgs'.
		/// </summary>
		/// <param name="selectedInstances">List containing the selected Oids.</param>
		public SelectedInstanceChangedEventArgs(List<Oid> selectedInstances)
		{
			if (selectedInstances != null && selectedInstances.Count > 0)
			{
				SelectedInstances = new List<Oid>();
				foreach (Oid lOid in selectedInstances)
				{
					SelectedInstances.Add(lOid);
				}
			}
			else
			{
				SelectedInstances = null;
			}
		}
		/// <summary>
		/// Initializes a new instance of 'SelectedInstanceChangedEventArgs'.
		/// </summary>
		/// <param name="selectedInstances">List containing the selected Oids.</param>
		/// <param name="enabledActions">List containing the formula to enable or disable the actions.</param>
		/// <param name="enabledNavigations">List containing the formula to enable or disable the navigations.</param>
		public SelectedInstanceChangedEventArgs(List<Oid> selectedInstances, List<string> enabledActions, List<string> enabledNavigations)
			: this(selectedInstances)
		{
			EnabledActionsKeys = enabledActions;
			EnabledNavigationsKeys = enabledNavigations;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Selected Oids.
		/// </summary>
		public List<Oid> SelectedInstances
		{
			get
			{
				return mSelectedInstances;
			}
			set
			{
				mSelectedInstances = value;
			}
		}
		/// <summary>
		/// Actions keys to enable or disable the offered actions.
		/// </summary>
		public List<string> EnabledActionsKeys
		{
			get
			{
				return mEnabledActionsKeys;
			}
			set
			{
				mEnabledActionsKeys = value;
			}
		}
		/// <summary>
		/// Navigations keys to enable or disable the offered navigations
		/// </summary>
		public List<string> EnabledNavigationsKeys
		{
			get
			{
				return mEnabledNavigationsKeys;
			}
			set
			{
				mEnabledNavigationsKeys = value;
			}
		}
		#endregion Properties
	}
}
