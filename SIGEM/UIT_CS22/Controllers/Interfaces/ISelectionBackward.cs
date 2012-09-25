// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Interface with the profile of a selection backward.
	/// </summary>
	public interface ISelectionBackward
	{
		/// <summary>
		/// Suscribes an instance selector to selection backward.
		/// </summary>
		/// <param name="instancesSelector"></param>
		void SuscribeSelectionBackward(IInstancesSelector instancesSelector);
	}
}

