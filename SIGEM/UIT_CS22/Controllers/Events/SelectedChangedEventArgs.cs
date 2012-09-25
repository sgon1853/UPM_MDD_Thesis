// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// SelectedChangedEventArgs class.
	/// </summary>
	public class SelectedChangedEventArgs: EventArgs
	{
		#region Members
		/// <summary>
		/// Selected Oids.
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
		/// Initializes a new instance of the 'SelectedChangedEventArgs' class.
		/// </summary>
		/// <param name="selectedInstances">List of selected instances.</param>
		public SelectedChangedEventArgs(List<Oid> selectedInstances)
		{
			mSelectedInstances = selectedInstances;
		}
		/// <summary>
		/// Initializes a new instance of 'SelectedInstanceChangedEventArgs'.
		/// </summary>
		/// <param name="selectedInstances">List of the selected instances.</param>
		/// <param name="enabledActions">List containing the formula to enable or disable the actions.</param>
		/// <param name="enabledNavigations">List containing the formula to enable or disable the navigations.</param>
		public SelectedChangedEventArgs(List<Oid> selectedInstances, List<string> enabledActions, List<string> enabledNavigations)
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
