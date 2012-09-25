// v3.8.4.5.b
using System;
using System.Windows.Forms;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of the .NET Button control.
	/// </summary>
	public class ButtonPresentation: ITriggerPresentation
	{
		#region Members
		/// <summary>
		/// .NET Button reference.
		/// </summary>
		private Button mButtonIT;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ButtonPresentation' class.
		/// </summary>
		/// <param name="button">.NET Button reference.</param>
		public ButtonPresentation(Button button)
		{
			mButtonIT = button;
			if (mButtonIT != null)
			{
				mButtonIT.GotFocus += new EventHandler(HandleButtonITEnter);
				mButtonIT.LostFocus += new EventHandler(HandleButtonITLeave);
				mButtonIT.Click += new System.EventHandler(HandleButtonITClick);
			}
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets a boolean value indicating whether the control is enabled or not.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return mButtonIT.Enabled;
			}
			set
			{
				mButtonIT.Enabled = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the control has the focus.
		/// </summary>
		public bool Focused
		{
			get
			{
				return mButtonIT.Focused;
			}
			set
			{
				if (value == true)
				{
					mButtonIT.Focus();
				}
				else if (mButtonIT.Parent != null)
				{
					mButtonIT.Parent.Focus();
				}
			}
		}
		/// <summary>
		/// Gets or sets the value of the control.
		/// </summary>
		public object Value
		{
			get
			{
				return mButtonIT.Text;
			}
			set
			{
				mButtonIT.Text = value.ToString();
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the control is visible or not.
		/// </summary>
		public bool Visible
		{
			get
			{
				return mButtonIT.Visible;
			}
			set
			{
				mButtonIT.Visible = value;
			}
		}
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when the control gets ocus.
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
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Executes the actions related to GotFocus event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleButtonITEnter(object sender, EventArgs e)
		{
			OnGotFocus(new GotFocusEventArgs());
		}
		/// <summary>
		/// Executes the actions related to LostFocus event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleButtonITLeave(object sender, EventArgs e)
		{
			OnLostFocus(new LostFocusEventArgs());
		}
		/// <summary>
		/// Executes the actions related to Triggered event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleButtonITClick(object sender, System.EventArgs e)
		{
			OnTriggered( new TriggerEventArgs());
		}
		#endregion Event Handlers

		#region Event Raisers
		/// <summary>
		/// Raise the LostFocus event
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
		/// Raise the GotFocus event
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
		#endregion Event Raisers
	}
}
