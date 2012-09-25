// 3.4.4.5
using System;
using System.Collections;
using System.Collections.Specialized;

namespace SIGEM.Business
{
	/// <summary>
	/// Class that represents the triggers of the model
	/// </summary>
	public class ONTrigger
	{
		#region Members
		public string ClassName;
		public string ServiceName;
		public string ArgumentThisName;
		public ArrayList InputParameters;
		public ArrayList OutputParameters;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="className">Name of the class where is defined the trigger</param>
		/// <param name="serviceName">Service that makes to throw the trigger</param>
		/// <param name="argumentThisName">Name of the argument that is THIS in the trigger</param>
		public ONTrigger(string className, string serviceName, string argumentThisName)
		{
			ClassName = className;
			ServiceName = serviceName;
			ArgumentThisName = argumentThisName;

			InputParameters = new ArrayList();
			OutputParameters = new ArrayList();

		}
		#endregion
	}
}

