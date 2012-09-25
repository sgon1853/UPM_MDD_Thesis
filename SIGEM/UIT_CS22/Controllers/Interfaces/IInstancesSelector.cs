// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Interface with the profile of an instance selector.
	/// </summary>
	public interface IInstancesSelector
	{
		/// <summary>
		/// Event that indicates an instance has been selected.
		/// </summary>
		event EventHandler<InstancesSelectedEventArgs> InstancesHasBeenSelected;
	}
}

