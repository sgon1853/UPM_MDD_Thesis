// v3.8.4.5.b
using System;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation
{
	/// <summary>
	/// Abstracts the behaviour of a common trigger generator
	/// in the InteractionToolkit layer.
	/// </summary>
	public interface ITriggerPresentation: ILabelPresentation
	{
		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether the control can respond to user interaction.
		/// </summary>
		bool Enabled { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the control has input focus.
		/// </summary>
		bool Focused{ get; set; }
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when the control gets the input focus.
		/// </summary>
		event EventHandler<GotFocusEventArgs> GotFocus;
		/// <summary>
		/// Occurs when the control loses the input focus.
		/// </summary>
		event EventHandler<LostFocusEventArgs> LostFocus;
		/// <summary>
		/// Occurs when the control raises a trigger.
		/// </summary>
		event EventHandler<TriggerEventArgs> Triggered;
		#endregion Events
	}
}
