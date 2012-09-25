// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;
using SIGEM.Client.Oids;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation
{
	/// <summary>
	/// Abstracts the behaviour of the blocks of a common or standard
	/// data grid in the InteractionToolkit layer.
	/// </summary>
	public interface IDisplaySetByBlocksPresentation : IDisplaySetPresentation
	{
		#region Properties
		/// <summary>
		/// Gets the number of elements (rows) contained in the control.
		/// </summary>
		int Count { get; }
		/// <summary>
		/// Gets or sets the first visible row of the control.
		/// </summary>
		int FirstVisibleRow { get; set; }
		/// <summary>
		/// Gets the number of selected elements (rows) of the control.
		/// </summary>
		int SelectedCount { get; }

		/// <summary>
		/// Gets a cell text of the grid depending on the row index and column name.
		/// </summary>
		/// <param name="rowIndex">Row index.</param>
		/// <param name="columnName">Column name.</param>
		/// <returns>Returns the text of the required cell.</returns>
		object this[int rowIndex, string columnName] { get; }
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when more blocks of data are required.
		/// </summary>
		event EventHandler<TriggerEventArgs> MoreBlocks;
		#endregion Events

		#region Methods
		/// <summary>
		/// Returns the text value of the selected cell depending on the row index and column name.
		/// </summary>
		/// <param name="index">Row index.</param>
		/// <param name="columnName">Column name.</param>
		/// <returns>Returns the text of the selected cell.</returns>
		object GetSelectedCell(int index, string columnName);
		/// <summary>
		/// Selects a row by index.
		/// </summary>
		/// <param name="index">Row index.</param>
		void SelectRow(int index);
		/// <summary>
		/// Gets the index of a row by its Oid.
		/// </summary>
		/// <param name="oid">Oid of the row to be searched.</param>
		/// <returns>Returns the index of a row by its Oid.</returns>
		int GetRowFromOid(Oid oid);
		#endregion Methods
	}
}
