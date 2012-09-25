// v3.8.4.5.b
using System;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.Specialized;
using SIGEM.Client.InteractionToolkit;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of an scenario.
	/// </summary>
	public class ScenarioPresentation: IScenarioPresentation
	{
		#region Members
		/// <summary>
		/// Form control
		/// </summary>
		private Form mForm;
		/// <summary>
		/// Scenario type.
		/// </summary>
		private ScenarioType mScenarioType = ScenarioType.None;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ScenarioPresentation' class.
		/// </summary>
		/// <param name="form">Form.</param>
		public ScenarioPresentation(Form form, ScenarioType scenarioType)
		{
			mForm = form;
			if (mForm != null)
			{
				// for force KeyPreview = true if ScenarioType is "Service".
				ScenarioType = scenarioType;

				// Suscriber key events. Only for suscribers to ExecuteCommand.
				mForm.KeyDown += new KeyEventHandler(HandleFormKeyDown);
			}
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Scenario type.
		/// </summary>
		public ScenarioType ScenarioType
		{
			get
			{
				return mScenarioType;
			}
			set
			{
				mScenarioType = value;

				if (mForm != null)
				{
					if (ScenarioType == ScenarioType.Service)
					{
						mForm.KeyPreview = true;
					}
					else
					{
						mForm.KeyPreview = false;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the form.
		/// </summary>
		public Form Form
		{
			get
			{
				return mForm;
			}
		}
		/// <summary>
		/// Gets or sets the scenario title.
		/// </summary>
		public string Title
		{
			get
			{
				return Form.Text;
			}
			set
			{
				Form.Text = value;
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether the control is displayed.
		/// </summary>
		public bool Visible
		{
			get
			{
				return Visible;
			}
			set
			{
				Visible = value;
			}
		}
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when the Ok control is triggered on the scenario.
		/// </summary>
		public event EventHandler<TriggerEventArgs> Ok;
		/// <summary>
		/// Occurs when the Cancel control is triggered on the scenario.
		/// </summary>
		public event EventHandler<TriggerEventArgs> Cancel;
		/// <summary>
		/// Execute Command event.
		/// </summary>
		public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Handles the Form KeyDown event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleFormKeyDown(object sender, KeyEventArgs e)
		{
				Keys lKeyPressed = (Keys)e.KeyCode;
				switch (lKeyPressed)
				{
					case Keys.PageDown:
						e.Handled = true;
						if (e.Control)
						{
							OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteServiceAndGoNextInstance));
						}
						else
						{
							OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteGoNextInstance));
						}
						break;
					case Keys.PageUp:
						e.Handled = true;
						if (e.Control)
						{
							OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteServiceAndGoPreviousInstance));

						}
						else
						{
							OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteGoPreviousInstance));
						}
						break;
					case Keys.Enter:
						if (e.Control)
						{
							e.Handled = true;
							OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteService));
						}
						break;
					default:
						break;
				}
		}
		#endregion Event Handlers

		#region Event Raisers
		/// <summary>
		/// Raise ExecuteCommand Event.
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnExecuteCommand(ExecuteCommandEventArgs eventArgs)
		{
			EventHandler<ExecuteCommandEventArgs> handler = ExecuteCommand;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raise Ok Event.
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnOk(TriggerEventArgs eventArgs)
		{
			EventHandler<TriggerEventArgs> handler = Ok;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raise Ok Event.
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnCancel(TriggerEventArgs eventArgs)
		{
			EventHandler<TriggerEventArgs> handler = Cancel;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		#endregion Event Raisers

		#region Methods
		/// <summary>
		/// Closes the scenario.
		/// </summary>
		public void Close()
		{
			ScenarioManager.IsClosing = true;
			Form.Close();
			ScenarioManager.IsClosing = false;
		}

		#region Set Position
		/// <summary>
		/// Assigns the position, the size and custom properties to the form.
		/// </summary>
		/// <param name="x">The new x position coordinate of the form.</param>
		/// <param name="y">The new y position coordinate of the form.</param>
		/// <param name="width">The new width of the form.</param>
		/// <param name="heigth">The new heigth of the form.</param>
		/// <param name="properties">List of custom properties to be assigned to the form.</param>
		public void SetPositionInfo(int x, int y, int width, int heigth, StringDictionary properties)
		{
			if (Form == null)
				return;

			try
			{
				Form.SuspendLayout();
				if (x == -1 && y == -1 && width == -1 && heigth == -1)
				{
					Form.WindowState = FormWindowState.Maximized;
				}
				else
				{
					if (x != 0 && y != 0)
					{
						Form.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
						Form.Top = y;
						Form.Left = x;
					}
					if (width != 0 && heigth != 0)
					{
						Form.Width = width;
						Form.Height = heigth;
					}
				}
				Form.Refresh();
				Form.PerformLayout();
				Form.ResumeLayout(true);
			}
			catch
			{
			}
		}
		/// <summary>
		/// Assigns the splitters properties to the form.
		/// </summary>
		/// <param name="properties">List of properties where the splitters information is contained.</param>
		public void SetSplittersInfo(StringDictionary properties)
		{
			if (properties != null)
			{
				foreach (Control control in Form.Controls)
				{
					SetSpliterInfo(control, properties);
				}
			}
		}

		/// <summary>
		/// Assing the position to the control if it is a SplitContainer.
		/// Search in the Contained Controls to process all the existing ones in the form.
		/// </summary>
		/// <param name="control"></param>
		/// <param name="properties"></param>
		private void SetSpliterInfo(Control control, StringDictionary properties)
		{
			SplitContainer split = control as SplitContainer;
			if (split != null)
			{
				string value = properties[split.Name];
				if (value != null)
				{
					split.SuspendLayout();
					split.SplitterDistance = int.Parse(value);
					split.Refresh();
					split.ResumeLayout(true);
				}
			}

			foreach (Control subControl in control.Controls)
			{
				SetSpliterInfo(subControl, properties);
			}
		}
		#endregion Set Position

		#region Get Position
		/// <summary>
		/// Obtains the form position and size
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="width"></param>
		/// <param name="heigth"></param>
		/// <param name="properties"></param>
		public void GetPositionInfo(ref int x, ref int y, ref int width, ref int heigth, StringDictionary properties)
		{
			if (Form == null)
				return;

			try
			{
				// Form position
				if (Form.WindowState == FormWindowState.Maximized)
				{
					x = -1;
					y = -1;
					width = -1;
					heigth = -1;
				}
				else
				{
					x = Form.Left;
					y = Form.Top;
					width = Form.Width;
					heigth = Form.Height;
				}
				// Gets the Distance of every split container in the Form
				foreach (Control control in Form.Controls)
				{
					GetSpliterInfo(control, properties);
				}
			}
			catch
			{
			}
		}
		/// <summary>
		/// Gets the information about the spliter position
		/// </summary>
		/// <param name="control">In this control</param>
		/// <param name="properties">To added in</param>
		private void GetSpliterInfo(Control control, StringDictionary properties)
		{
			SplitContainer split = control as SplitContainer;
			if (split != null)
			{
				properties.Add(split.Name, split.SplitterDistance.ToString());
			}

			foreach (Control subControl in control.Controls)
			{
				GetSpliterInfo(subControl, properties);
			}
		}
		#endregion Get Position

		#endregion Methods
	}
}
