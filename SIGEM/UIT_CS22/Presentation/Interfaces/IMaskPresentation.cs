// v3.8.4.5.b
using System;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation
{
	/// <summary>
	/// Abstracts the behaviour of a common masked text editor in
	/// the InteractionToolkit layer.
	/// </summary>
    public interface IMaskPresentation
	{
		#region Properties
        string Mask { get; set; }
        string IPValidationMessage { get; set; }
        #endregion Properties
    }
}




