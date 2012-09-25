// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

using SIGEM.Client.Oids;

namespace SIGEM.Client
{
	/// <summary>
	/// Class 'IUContext'.
	/// </summary>
	[Serializable]
	public class IUErrorContext: IUContext
	{
		#region Members
		/// <summary>
		/// Instance of Exception.
		/// </summary>
		[NonSerialized]
		private Exception mException = null;

		#endregion Members

		#region Properties
		/// <summary>
		/// Gets message.
		/// </summary>
		public string Message
		{
			get
			{
				StringBuilder lText = new StringBuilder();
				Exception lException = Error;
				while (lException != null)
				{
					lText.Append(lException.Message);
					lText.Append("\n");
					lException = lException.InnerException;
				}
				return lText.ToString();
			}
		}
		/// <summary>
		/// Gets error.
		/// </summary>
		public Exception Error
		{
			get
			{
				return mException;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'IUErrorContext'.
		/// </summary>
		/// <param name="agent">Agent.</param>
		/// <param name="exception">Exception.</param>
		public IUErrorContext(Exception exception)
			: base(null, ContextType.Error, RelationType.Navigation, string.Empty)
		{
			mException = exception;
		}
		/// <summary>
		/// Initializes a new instance of 'IUErrorContext'.
		/// </summary>
		/// <param name="previous">Previous.</param>
		/// <param name="exception">Exception.</param>
		public IUErrorContext(IUContext previous, Exception exception)
			: base(previous, ContextType.Error,RelationType.Navigation,string.Empty)

		{
			mException = exception;
		}
		#endregion Constructors
	}
}
