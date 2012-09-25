// v3.8.4.5.b
using System;

namespace SIGEM.Client.Presentation
{
	/// <summary>
	/// Abstracts the behaviour of an element in the Interaction Toolkit layer.
	/// </summary>
	public interface IPresentation
	{
		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether the control is displayed.
		/// </summary>
		bool Visible { get; set; }
		#endregion Properties
	}
}
