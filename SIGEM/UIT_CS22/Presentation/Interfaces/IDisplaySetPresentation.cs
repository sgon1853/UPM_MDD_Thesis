// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;
using SIGEM.Client.Oids;
using SIGEM.Client.Presentation;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation
{
	/// <summary>
	/// Abstracts the behaviour of a common or standard
	/// data grid in the InteractionToolkit layer.
	/// </summary>
	public interface IDisplaySetPresentation : IPresentation
	{
		#region Properties
		/// <summary>
		/// Gets or sets the enabled property.
		/// </summary>
		bool Enabled { get; set; }
		/// <summary>
		/// Gets or sets the list of oid values.
		/// </summary>
		List<Oid> Values { get; set; }
		/// <summary>
		/// Texts to be shown for boolean values
		/// </summary>
		string DisplayTextBoolNull { get; set;}
		string DisplayTextBoolTrue { get; set;}
		string DisplayTextBoolFalse { get; set;}
		#endregion Properties

		#region Methods
		/// <summary>
		/// Shows the control with the data filled into it.
		/// </summary>
		/// <param name="data">Data to be shown</param>
		/// <param name="selectedOids">OIDs to be selected</param>
		void ShowData(DataTable data, List<Oid> selectedOids);
		/// <summary>
		/// Sets the message of the suplementary information.
		/// </summary>
		/// <param name="message"></param>
		void SetMessage(string message);
		/// <summary>
		/// Adds the display set item to the population.
		/// </summary>
		/// <param name="name">The name of the display set item.</param>
		/// <param name="alias">the alias of the display set item.</param>
		/// <param name="idxml">the xml identification of the display set item.</param>
		/// <param name="modelType">the model type of the display set item.</param>
		/// <param name="agents">the agent for the display set item.</param>
		/// <param name="width">width for the display set item.</param>
		void AddDisplaySetItem(string name, string alias, string idxml, ModelType modelType, List<KeyValuePair<object, string>> definedSelectionOptions, string[] agents, int width);
		/// <summary>
		/// Sets a format for the display set item.
		/// </summary>
		/// <param name="name">the name of the display set item.</param>
		/// <param name="alias">the alias of the display set item</param>
		/// <param name="modelType">the model type of the display set item</param>
		/// <param name="width">width for the display set item.</param>
		void SetFormatDisplaySetItem(string name, string alias, ModelType modelType, List<KeyValuePair<object, string>> definedSelectionOptions, int width, bool editable, bool allowsNullInEditMode);
		/// <summary>
		/// Removes all the DisplaySet items
		/// </summary>
		void RemoveAllDisplaySetItems();
		/// <summary>
		/// Get columns width
		/// </summary>
		List<int> GetColumnsWidth();
		/// <summary>
		/// Returns the modified rows
		/// </summary>
		/// <returns></returns>
		DataTable GetModifiedRows();
		#endregion Methods

		#region Events
		/// <summary>
		/// Occurs when a selection is changed.
		/// </summary>
		event EventHandler<SelectedChangedEventArgs> SelectionChanged;
		/// <summary>
		/// Occurs when the control raise a command.
		/// </summary>
		event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		#endregion Events
	}
}
