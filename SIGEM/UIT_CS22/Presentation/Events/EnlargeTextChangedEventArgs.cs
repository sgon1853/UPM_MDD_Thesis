// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Presentation
{
	/// <summary>
	/// EnlargeTextChangedEventArgs class.
	/// </summary>
	public class EnlargeTextChangedEventArgs: EventArgs
	{
		#region Members
		/// <summary>
		/// Text in enlarge Form
		/// </summary>
		public string mTextInEnlargeForm;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the EnlargeTextChangedEventArgs class.
		/// </summary>
		public EnlargeTextChangedEventArgs(string pText)
		{
			mTextInEnlargeForm = pText;
		}
		#endregion Constructors
	}
}
