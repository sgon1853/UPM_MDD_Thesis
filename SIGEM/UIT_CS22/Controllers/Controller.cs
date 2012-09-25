// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Controller base class.
	/// </summary>
	public abstract class Controller
	{
		#region Members
		private Controller mParent;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets the parent controller.
		/// </summary>
		public Controller Parent
		{
			get
			{
				return mParent;
			}
			set
			{
				mParent= value;
			}
		}
		#endregion Properties

		#region Contructors
		/// <summary>
		/// Initializes a new instance of Controller.
		/// </summary>
		/// <param name="parent">The parent of this instance.</param>
		public Controller(Controller parent)
		{
			mParent = parent;
		}
		/// <summary>
		/// Initializes a new instance of Controller from derived Controller.
		/// </summary>
		protected Controller()
			: this(null) { } 
		#endregion Contructors
	}
}


