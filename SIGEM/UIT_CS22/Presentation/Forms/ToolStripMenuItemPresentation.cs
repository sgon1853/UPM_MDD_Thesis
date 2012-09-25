// v3.8.4.5.b
using System;
using System.Drawing;
using System.Windows.Forms;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstaction of the .NET ToolStripMenuItem.
	/// </summary>
	public class ToolStripMenuItemPresentation : ILabelPresentation, ITriggerPresentation
	{
		#region Members
		/// <summary>
		/// ToolStripMenuItem instance.
		/// </summary>
		private ToolStripMenuItem mToolStripMenuItem;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'ToolStripMenuItemPresentation'.
		/// </summary>
		/// <param name="toolStripMenuItem">ToolStripMenuItem instance.</param>
		public ToolStripMenuItemPresentation(ToolStripMenuItem toolStripMenuItem)
			: this(toolStripMenuItem, true)
		{
		}
		/// <summary>
		/// Initializes a new instance of 'ToolStripMenuItemPresentation', specifying if it's leaf element.
		/// </summary>
		/// <param name="toolStripMenuItem">ToolStripMenuItem.</param>
		/// <param name="isLeafElement">Specifies if it's leaf element.</param>
		public ToolStripMenuItemPresentation(ToolStripMenuItem toolStripMenuItem, bool isLeafElement)
		{
			mToolStripMenuItem = toolStripMenuItem;
			if ((mToolStripMenuItem != null) && (isLeafElement))
			{
				mToolStripMenuItem.Click += new System.EventHandler(HandleMenuItemClick);
			}
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets Value.
		/// </summary>
		public object Value
		{
			get
			{
				return mToolStripMenuItem.Text;
			}
			set
			{
				mToolStripMenuItem.Text = value.ToString();
			}
		}
		/// <summary>
		/// Gets or sets Enabled property.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return mToolStripMenuItem.Enabled;
			}
			set
			{
				mToolStripMenuItem.Enabled = value;
			}
		}
		/// <summary>
		/// Gets or sets Focused property.
		/// </summary>
		public bool Focused
		{
			get
			{
				return mToolStripMenuItem.Selected;
			}
			set
			{
				mToolStripMenuItem.Select();
			}
		}
		/// <summary>
		/// Gets or sets Visible property.
		/// </summary>
		public bool Visible
		{
			get
			{
				return mToolStripMenuItem.Visible;
			}
			set
			{
				mToolStripMenuItem.Visible = value;
			}
		}
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when ToolStripButton gets focus.
		/// </summary>
		public event EventHandler<GotFocusEventArgs> GotFocus;
		/// <summary>
		/// Occurs when ToolStripButton gets focus.
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
