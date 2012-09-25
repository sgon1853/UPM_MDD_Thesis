// v3.8.4.5.b
using System;
using System.Drawing;
using System.Windows.Forms;

using SIGEM.Client.Controllers;
namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of the .NET TabPage control.
	/// </summary>
	public class TabPagePresentation : ITriggerPresentation
	{
		#region Members
		/// <summary>
		/// TabPage control
		/// </summary>
		private TabPage mTabPageIT;
		/// <summary>
		/// TabControl parent.
		/// </summary>
		private TabControl mTabControlIT;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'TabPagePresentation'.
		/// </summary>
		/// <param name="tabPage">TabPage instance.</param>
		public TabPagePresentation(TabPage tabPage)
		{
			mTabPageIT = tabPage;

			mTabPageIT.LostFocus += new EventHandler(HandleTabPageLostFocus);
			mTabPageIT.GotFocus += new EventHandler(HandleTabPageGotFocus);

			// Suscribe to the change tab selection event
			mTabControlIT = mTabPageIT.Parent as TabControl;
			if (mTabControlIT != null)
			{
				mTabControlIT.SelectedIndexChanged += new EventHandler(HandleTabControlSelectedIndexChanged);
			}
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets TabPage text.
		/// </summary>
		public object Value
		{
			get
			{
				return mTabPageIT.Text;
			}
			set
			{
				mTabPageIT.Text = value.ToString();
			}
		}
		/// <summary>
		/// Gets or sets Visible property.
		/// </summary>
		public bool Visible
		{
			get
			{
				if (mTabControlIT != null)
				{
					if (mTabControlIT.SelectedTab == mTabPageIT)
						return true;
					else
						return false;
				}
				
				return true;
			}
			set
			{
			}
		}
		/// <summary>
		/// Gets or sets TabPage enabled.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return true;
			}
			set
			{
			}
		}
		/// <summary>
		/// Gets or sets TabPage focus.
		/// </summary>
		public bool Focused
		{
			get
			{
				return true;
			}
			set
			{
			}
		}
		#endregion Properties
		#region Events
		public event EventHandler<GotFocusEventArgs> GotFocus;
		public event EventHandler<LostFocusEventArgs> LostFocus;
		public event EventHandler<TriggerEventArgs> Triggered;
		#endregion Events

		#region Event Handlers
		private void HandleTabControlSelectedIndexChanged(object sender, EventArgs e)
		{
			if (mTabControlIT != null && mTabControlIT.SelectedTab == mTabPageIT)
			{
				OnTriggered(new TriggerEventArgs());
			}
		}

		/// <summary>
		/// Lost focus tabpage handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleTabPageLostFocus(object sender, EventArgs e)
		{
			OnLostFocus(new LostFocusEventArgs());
		}

		/// <summary>
		/// Got focus tabpage handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleTabPageGotFocus(object sender, EventArgs e)
		{
			OnGotFocus(new GotFocusEventArgs());
		}
		#endregion Event Handlers

		#region Event Raisers
		/// <summary>
		/// Raise Triggered Event.
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnTriggered(TriggerEventArgs eventArgs)
		{
			EventHandler<TriggerEventArgs> handler = Triggered;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}

		/// <summary>
		/// Raise LostFocus Event.
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnLostFocus(LostFocusEventArgs eventArgs)
		{
			EventHandler<LostFocusEventArgs> handler = LostFocus;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}

		/// <summary>
		/// Raise GotFocus Event.
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnGotFocus(GotFocusEventArgs eventArgs)
		{
			EventHandler<GotFocusEventArgs> handler = GotFocus;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}

		#endregion Event Raisers
	}
}
