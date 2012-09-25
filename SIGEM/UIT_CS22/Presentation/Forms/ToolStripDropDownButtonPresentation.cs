// v3.8.4.5.b
using System;
using System.Drawing;
using System.Windows.Forms;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation
{
	/// <summary>
	/// Presentation abstaction of the .NET ToolStripDropDownButton.
	/// </summary>
	public class ToolStripDropDownButtonPresentation : ITriggerPresentation
	{
		#region Members
		/// <summary>
		/// ToolStripDropDownButton control.
		/// </summary>
		private ToolStripDropDownButton mToolStripDropDownButton;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ToolStripDropDownButtonPresentation' class.
		/// </summary>
		/// <param name="toolStripDropDownButton">ToolStripDropDownButton control.</param>
		public ToolStripDropDownButtonPresentation(ToolStripDropDownButton toolStripDropDownButton)
		{
			mToolStripDropDownButton = toolStripDropDownButton;
			if (mToolStripDropDownButton != null)
			{
				mToolStripDropDownButton.Click += new System.EventHandler(HandleDropDownButtonClick);
			}
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets Text to the control.
		/// </summary>
		public object Value
		{
			get
			{
				return mToolStripDropDownButton.Text;
			}
			set
			{
				mToolStripDropDownButton.Text = UtilFunctions.ProtectAmpersandChars(value.ToString());
				mToolStripDropDownButton.ToolTipText = value.ToString();
			}
		}
		/// <summary>
		/// Gets or sets Enabled property.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return mToolStripDropDownButton.Enabled;
			}
			set
			{
				mToolStripDropDownButton.Enabled = value;
			}
		}
		/// <summary>
		/// Gets or sets Selected.
		/// </summary>
		public bool Focused
		{
			get
			{
				return mToolStripDropDownButton.Selected;
			}
			set
			{
				mToolStripDropDownButton.Select();
			}
		}
		/// <summary>
		/// Gets or sets Visible property.
		/// </summary>
		public bool Visible
		{
			get
			{
				return mToolStripDropDownButton.Visible;
			}
			set
			{
				mToolStripDropDownButton.Visible = value;
			}
		}
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when ToolStripDropDownButton gets focus.
		/// </summary>
		public event EventHandler<GotFocusEventArgs> GotFocus;
		/// <summary>
		/// Occurs when ToolStripDropDownButton loses focus.
		/// </summary>
		public event EventHandler<LostFocusEventArgs> LostFocus;
		/// <summary>
		/// Occurs when ToolStripDropDownButton is triggered.
		/// </summary>
		public event EventHandler<TriggerEventArgs> Triggered;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Executes the actions related to Triggered event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void HandleDropDownButtonClick(object sender, System.EventArgs e)
		{
			OnTriggered(new TriggerEventArgs());
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
