// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Interface with the profile of action item events.
	/// </summary>
    public interface IActionItemEvents : INavigationItemEvents
	{
		/// <summary>
		/// Event that indicates the service response.
		/// </summary>
		event EventHandler<ServiceResultEventArgs> ServiceResponse;
		/// <summary>
		/// Event that request the selection of the next instance in the population.
		/// </summary>
		event EventHandler<SelectNextPreviousInstanceEventArgs> SelectNextInstance;
		/// <summary>
		/// Event that request the selection of the previous instance in the population
		/// </summary>
		event EventHandler<SelectNextPreviousInstanceEventArgs> SelectPreviousInstance;
	}
}



