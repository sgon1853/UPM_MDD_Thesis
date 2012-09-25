// v3.8.4.5.b
using System;
using System.Windows.Forms;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of the .NET LinkLabel control.
	/// </summary>
	public class LinkLabelPresentation: ITriggerPresentation
	{
		#region Members
		private LinkLabel mLinkLabelIT;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'LinkLabelPresentation'.
		/// </summary>
		/// <param name="linkLabel">LinkLabel instance.</param>
		public LinkLabelPresentation(LinkLabel linkLabel)
		{
			mLinkLabelIT = linkLabel;
			if (mLinkLabelIT != null)
			{
				mLinkLabelIT.GotFocus += new EventHandler(Enter);
				mLinkLabelIT.LostFocus += new EventHandler(Leave);
				mLinkLabelIT.Click += new System.EventHandler(OnTriggered);
			}
		}
		#endregion Constructors

		#region ITriggerPresentation
		/// <summary>
		/// Occurs when the control gets focus.
		/// </summary>
		public event EventHandler<GotFocusEventArgs> GotFocus;
		/// <summary>
		/// Occurs when the control loses focus.
		/// </summary>
		public event EventHandler<LostFocusEventArgs> LostFocus;
		/// <summary>
		/// Occurs when the control is triggered.
		/// </summary>
		public event EventHandler<TriggerEventArgs> Triggered;
		/// <summary>
		/// Executes actions related to Triggered event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnTriggered(object sender, System.EventArgs e)
		{
			if (Triggered != null)
			{
				Triggered(this, new TriggerEventArgs());
			}

		}
		/// <summary>
		/// Executes actions related to GotFocus event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Enter(object sender, EventArgs e)
		{
			// Throw got focus event
			if (GotFocus != null)
			{
				GotFocus(this, new GotFocusEventArgs());
			}
		}
		/// <summary>
		/// Executes actions related to LostFocus event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Leave(object sender, EventArgs e)
		{
			// Throw lost focus event
			if (LostFocus != null)
			{
				LostFocus(this, new LostFocusEventArgs());
			}
		}
		/// <summary>
		/// Gets or sets Enabled property.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return mLinkLabelIT.Enabled;
			}
			set
			{
				mLinkLabelIT.Enabled = value;
			}
		}
		/// <summary>
		/// Gets or sets Focused property.
		/// </summary>
		public bool Focused
		{
			get
			{
				return mLinkLabelIT.Focused;
			}
			set
			{
				if (value == true)
				{
					mLinkLabelIT.Focus();
				}
				else if (mLinkLabelIT.Parent != null)
				{
					mLinkLabelIT.Parent.Focus();
				}
			}
		}
		/// <summary>
		/// Gets or sets label text.
		/// </summary>
		public object Value
		{
			get
			{
				return mLinkLabelIT.Text;
			}
			set
			{
				mLinkLabelIT.Text = value.ToString();
			}
		}
		/// <summary>
		/// Gets or sets Visible property.
		/// </summary>
		public bool Visible
		{
			get
			{
				return mLinkLabelIT.Visible;
			}
			set
			{
				mLinkLabelIT.Visible = value;
			}
		}
		#endregion ITriggerPresentation
	}
}
