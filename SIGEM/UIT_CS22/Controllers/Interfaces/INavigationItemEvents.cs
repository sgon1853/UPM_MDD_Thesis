// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Interface with the profile of navigation item events.
	/// </summary>
    public interface INavigationItemEvents
	{
		/// <summary>
		/// Event that request the refreshing of the population
		/// </summary>
        event EventHandler<RefreshRequiredEventArgs> RefreshRequired;
	}
}



