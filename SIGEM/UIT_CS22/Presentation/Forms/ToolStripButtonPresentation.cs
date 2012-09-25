// v3.8.4.5.b
using System;
using System.Drawing;
using System.Windows.Forms;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation
{
	/// <summary>
	/// Presentation abstaction of the .NET ToolStripButton.
	/// </summary>
	public class ToolStripButtonPresentation: ITriggerPresentation
	{
		#region Members
		/// <summary>
		/// ToolStrip control.
		/// </summary>
		private ToolStripButton mToolStripButton;
		/// <summary>
		/// Context menu item control.
		/// </summary>
		private ToolStripMenuItem mContextMenuToolStripMenuItem;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ToolStripButtonPresentation' class.
		/// </summary>
		/// <param name="toolStripButton">ToolStripButton</param>
		/// <param name="contextMenuToolStripMenuItem">ToolStripMenuItem</param>
		public ToolStripButtonPresentation(ToolStripButton toolStripButton, ToolStripMenuItem contextMenuToolStripMenuItem)
			: this(toolStripButton)
		{
			if ((contextMenuToolStripMenuItem != null))
			{
				mContextMenuToolStripMenuItem = contextMenuToolStripMenuItem;
				contextMenuToolStripMenuItem.Click += new System.EventHandler(HandleMenuItemClick);
			}
		}

		/// <summary>
		/// Initializes a new instance of the 'ToolStripButtonPresentation' class.
		/// </summary>
		/// <param name="toolStripButton">ToolStripButton.</param>
		public ToolStripButtonPresentation(ToolStripButton toolStripButton)
		{
			mToolStripButton = toolStripButton;
			if (mToolStripButton != null)
			{
				mToolStripButton.Click += new System.EventHandler(HandleMenuItemClick);
			}
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets the text on the ToolStripButton.
		/// </summary>
		public object Value
		{
			get
			{
				return mToolStripButton.Text;
			}
			set
			{
				mToolStripButton.Text = UtilFunctions.ProtectAmpersandChars(value.ToString());
				mToolStripButton.ToolTipText = value.ToString();
				if (mContextMenuToolStripMenuItem != null)
				{
					mContextMenuToolStripMenuItem.Text = mToolStripButton.Text;
				}
			}
		}
		/// <summary>
		/// Gets or sets Enabled.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return mToolStripButton.Enabled;
			}
			set
			{
				mToolStripButton.Enabled = value;
				if (mContextMenuToolStripMenuItem != null)
					mContextMenuToolStripMenuItem.Enabled = value;
			}
		}
		/// <summary>
		/// Gets or sets Selected.
		/// </summary>
		public bool Focused
		{
			get
			{
				return mToolStripButton.Selected;
			}
			set
			{
				mToolStripButton.Select();
			}
		}
		/// <summary>
		/// Gets or sets Visible.
		/// </summary>
		public bool Visible
		{
			get
			{
				return mToolStripButton.Visible;
			}
			set
			{
				mToolStripButton.Visible = value;
				if(mContextMenuToolStripMenuItem != null)
					mContextMenuToolStripMenuItem.Visible = value;
			}
		}
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when ToolStripButton gets focus.
		/// </summary>
		public event EventHandler<GotFocusEventArgs> GotFocus;
		/// <summary>
		/// Occurs when ToolStripButton loses focus.
		/// </summary>
		public event EventHandler<LostFocusEventArgs> LostFocus;
		/// <summary>
		/// Occurs when ToolStripButton is triggered.
		/// </summary>
		public event EventHandler<TriggerEventArgs> Triggered;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Executes the actions related to Triggered event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void HandleMenuItemClick(object sender, System.EventArgs e)
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

