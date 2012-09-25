// v3.8.4.5.b
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of the .NET TabPage control.
	/// </summary>
	public class TabControlPresentation : IGroupContainerPresentation
	{
		#region Members
		/// <summary>
		/// TabPage control
		/// </summary>
		private TabControl mTabControlIT;
		/// <summary>
		/// Internal list of pages
		/// </summary>
        private System.Collections.Generic.List<TabPage> mTabPages = new System.Collections.Generic.List<TabPage>();
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'TabControlPresentation'.
		/// </summary>
		/// <param name="tabPage">TabControl tabControl</param>
		public TabControlPresentation(TabControl tabControl)
		{
			mTabControlIT = tabControl;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets Visible property.
		/// </summary>
		public bool Visible
		{
			get
			{
				return mTabControlIT.Visible;
			}
			set
			{
				mTabControlIT.Visible = value;
			}
		}
		#endregion Properties

		#region Methods

		public void Initialize()
		{
			Visible = false;
            mTabControlIT.SizeMode = TabSizeMode.Normal;
			foreach (TabPage page in mTabControlIT.TabPages)
			{
                mTabPages.Add(page);
			}
		}

		public void SetGroupVisible(string groupId)
		{
            mTabControlIT.TabPages.Clear();
            
			foreach (TabPage page in mTabPages)
			{
				if (page.Tag.Equals(groupId))
				{
                    mTabControlIT.TabPages.Add(page);
					mTabControlIT.SelectedTab = page;
					mTabControlIT.Visible = true;
					break;
				}
			}
		}

		public void HideAllGroups()
		{
			Visible = false;
		}

		/// <summary>
		/// Assign the proper Id to the group in the specified order.
		/// Following the pre-order in the Master-Detail definition
		/// </summary>
		/// <param name="groupNumber">Group order</param>
		/// <param name="groupId">Group Id</param>
		public void AssignGroupId(int groupNumber, string groupId)
		{
			if (mTabControlIT.TabPages.Count < groupNumber)
			{
				return;
			}

			mTabControlIT.TabPages[groupNumber].Tag = groupId;
		}

		#endregion Methods

	}
}
