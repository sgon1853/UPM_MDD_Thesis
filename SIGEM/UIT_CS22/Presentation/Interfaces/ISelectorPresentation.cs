// v3.8.4.5.b
using System;
using System.Collections.Generic;

namespace SIGEM.Client.Presentation
{
	/// <summary>
	/// Abstracts de behaviour of a common selector in the
	/// InteractionToolkit layer.
	/// </summary>
	public interface ISelectorPresentation : IEditorPresentation
	{
		#region Properties
		/// <summary>
		/// Gets or sets a list representing the collection of the items contained in the control.
		/// </summary>
		IList<KeyValuePair<object, string>> Items { get; set;}
		/// <summary>
		/// Gets or sets the index of the selected item.
		/// </summary>
		int SelectedItem { get; set;}
		#endregion Properties

		#region Methods
		/// <summary>
		/// Inserts a new item into the control list of items indicating its index.
		/// </summary>
		/// <param name="index">Index of the item.</param>
		/// <param name="itemValue">Value of the item.</param>
		void InsertItem(int index, string itemValue);
		/// <summary>
		/// Inserts a new item into the control list of items at the end of the list.
		/// </summary>
		/// <param name="itemValue">Value of the item.</param>
		void InsertItem(string itemValue);
		/// <summary>
		/// Removes an item of the control's list of items specifying its index.
		/// </summary>
		/// <param name="index">Index of the item.</param>
		void RemoveItem(int index);
		/// <summary>
		/// Removes an item of the control's list of items specifying its value.
		/// </summary>
		/// <param name="optionValue">Value of the item.</param>
		void RemoveItem(string itemValue);
		#endregion Methods
	}
}
