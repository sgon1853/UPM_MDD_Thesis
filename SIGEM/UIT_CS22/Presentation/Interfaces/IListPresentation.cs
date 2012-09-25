// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Presentation
{
	/// <summary>
	/// Abstracts the behaviour for showing data in the
	/// InteractionToolkit layer with a list structure.
	/// </summary>
	public interface IListPresentation : IPresentation
	{
		#region Properties
		/// <summary>
		/// Gets the number of elements contained in the control.
		/// </summary>
		int Count { get; }
		/// <summary>
		/// Gets the text value of the array element depending on the name.
		/// </summary>
		/// <param name="name">Name of the element.</param>
		/// <returns>Returns the text value of the element.</returns>
		string this[string name] { get; }
		#endregion Properties

		#region Methods
		/// <summary>
		/// Shows the control with the data filled into it.
		/// </summary>
		/// <param name="data">Data.</param>
		void ShowData(DataTable data);
		#endregion Methods
	}
}
