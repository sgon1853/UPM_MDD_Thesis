// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Interface with the profile of an navigation item suscriber.
	/// </summary>
    public interface INavigationItemSuscriber
	{
        /// <summary>
        /// Request a data refresh as a result of some specific action
        /// </summary>
        event EventHandler<RefreshRequiredEventArgs> RefreshRequired;
        /// <summary>
		/// Suscribes an action to an event.
		/// </summary>
		/// <param name="actionServiceEvents"></param>
		void SuscribeNavigationEvents(INavigationItemEvents navigationItemEvents);
	}
}



