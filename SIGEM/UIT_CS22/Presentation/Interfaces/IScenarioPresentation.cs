// v3.8.4.5.b
using System;
using System.Windows.Forms;
using System.Collections.Specialized;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation
{
	public enum ScenarioType
	{
		None,
		Population,
		Instance,
		MasterDetail,
		Service,
		GlobalService,
		OutboundArgs,
		Other
	}

	/// <summary>
	/// Abstracts the scenario for showing the complete data
	/// to the user.
	/// </summary>
	public interface IScenarioPresentation : IPresentation
	{
		#region Properties
		/// <summary>
		/// Gets or sets the title of the scenario.
		/// </summary>
		string Title{ get; set; }
		/// <summary>
		/// Gets the form object.
		/// </summary>
		Form Form { get; }

		ScenarioType ScenarioType { get; set; }

		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when the control raise a command.
		/// </summary>
		event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		/// <summary>
		/// Occurs when the Ok control is triggered on the scenario.
		/// </summary>
		event EventHandler<TriggerEventArgs> Ok;
		/// <summary>
		/// Occurs when the Cancel control is triggered on the scenario.
		/// </summary>
		event EventHandler<TriggerEventArgs> Cancel;
		#endregion Events

		#region Methods
		/// <summary>
		/// Closes the scenario.
		/// </summary>
		void Close();
		/// <summary>
		/// Set graphical positions.
		/// </summary>
		void SetPositionInfo(int x, int y, int width, int heigth, StringDictionary properties);
		/// <summary>
		/// Get graphical positions.
		/// </summary>
		void GetPositionInfo(ref int x, ref int y, ref int width, ref int heigth, StringDictionary properties);
		/// <summary>
		/// Set splitters positions.
		/// </summary>
		void SetSplittersInfo(StringDictionary properties);
		#endregion Methods
	}
}
