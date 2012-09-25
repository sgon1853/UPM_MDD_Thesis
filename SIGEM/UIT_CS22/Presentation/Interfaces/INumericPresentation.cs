// v3.8.4.5.b
using System;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation
{
	/// <summary>
	/// Abstracts the behaviour of a common editor for numeric values in
	/// the InteractionToolkit layer.
	/// </summary>
    public interface INumericPresentation: IEditorPresentation
	{
		#region Properties
        decimal? MinValue { get; set; }
        decimal? MaxValue { get; set; }
        string IPValidationMessage { get; set; }
        int? MaxIntegerDigits { get; set; }
        int MaxDecimalDigits { get; set; }
        int MinDecimalDigits { get; set; }
        #endregion Properties
    }
}




