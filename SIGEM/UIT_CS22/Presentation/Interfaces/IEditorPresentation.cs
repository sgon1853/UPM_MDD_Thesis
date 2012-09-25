// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation
{
	/// <summary>
	/// Abstracts the behaviour of a common text editor in
	/// the InteractionToolkit layer.
	/// </summary>
	public interface IEditorPresentation : ILabelPresentation
	{
		#region Properties
		/// <summary>
		/// Gets or sets the model type.
		/// </summary>
		ModelType DataType { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the control can respond to user interaction.
		/// </summary>
		bool Enabled { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the control has input focus.
		/// </summary>
		bool Focused { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the control allows null values.
		/// </summary>
		bool NullAllowed { get; set; }
		/// <summary>
		/// Gets or sets a value indicating the max legth of the control value.
		/// </summary>
		int MaxLength { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the control is Read Only
		/// </summary>
		bool ReadOnly { get; set; }
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when the control gets focus.
		/// </summary>
		event EventHandler<GotFocusEventArgs> GotFocus;
		/// <summary>
		/// Occurs when the control loses the focus.
		/// </summary>
		event EventHandler<LostFocusEventArgs> LostFocus;
		/// <summary>
		/// Occurs when the control value is changed.
		/// </summary>
		event EventHandler<ValueChangedEventArgs> ValueChanged;
		/// <summary>
		/// Occurs when enable property is changed.
		/// </summary>
		event EventHandler<EnabledChangedEventArgs> EnableChanged;
		/// <summary>
		/// Execute Command event Implementation of IEditorPresentation interface.
		/// </summary>
		event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		#endregion Events

		#region Methods
		/// <summary>
		/// Validates the control value.
		/// </summary>
		/// <param name="defaultErrorMessage">Default validation message.</param>
		/// <returns>Boolean value indicating if the validation was ok or not.</returns>
		bool Validate(string errorMessage);
		#endregion Methods
	}
}
