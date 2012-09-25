// v3.8.4.5.b
using System;

namespace SIGEM.Client.Presentation
{
	/// <summary>
	/// Abstracts the behaviour of a common label control
	/// in the InteractionToolkit layer.
	/// </summary>
	public interface ILabelPresentation : IPresentation
	{
		#region Properties
		/// <summary>
		/// Gets or sets the text value of the control.
		/// </summary>
		object Value { get; set; }
		#endregion Properties
	}
}
