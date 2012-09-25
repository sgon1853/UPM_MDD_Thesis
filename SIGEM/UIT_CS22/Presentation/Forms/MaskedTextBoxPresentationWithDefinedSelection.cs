// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of the .NET MaskedTextBox control for attributes with defined selection.
	/// This Presentation is used only to show (not to edit) values for attributes with defined selection.
	/// </summary>
	public class MaskedTextBoxPresentationWithDefinedSelection : MaskedTextBoxPresentation, ISelectorPresentation
	{
		#region Members
		/// <summary>
		/// List of Defined Selections.
		/// </summary>
		private IList<KeyValuePair<object, string>> mDefinedSelectionOptions = new List<KeyValuePair<object, string>>();
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets value.
		/// </summary>
		public override object Value
		{
			get
			{
				if (mMaskedTextBoxIT.Text == string.Empty)
				{
					return null;
				}

				foreach (KeyValuePair<object, string> definedSelection in mDefinedSelectionOptions)
				{
					if (string.Compare(mMaskedTextBoxIT.Text, definedSelection.Value, true) == 0)
					{
						return definedSelection.Key;
					}
				}

				return null;
			}
			set
			{
				if (value == null)
				{
					mMaskedTextBoxIT.Text = string.Empty;
				}
				else
				{
					foreach (KeyValuePair<object, string> definedSelection in mDefinedSelectionOptions)
					{
						if (object.Equals(value, definedSelection.Key))
						{
							mMaskedTextBoxIT.Text = definedSelection.Value;
						}
					}
				}
			}
		}
		/// <summary>
		/// Sets Defined Selections items.
		/// </summary>
		public IList<KeyValuePair<object, string>> Items
		{
			get
			{
				return mDefinedSelectionOptions;
			}
			set
			{
				mDefinedSelectionOptions = value;
			}
		}
		/// <summary>
		/// Gets or sets the Defined Selection selected index.
		/// </summary>
		public int SelectedItem
		{
			get
			{
				int i = 0;
				foreach (KeyValuePair<object, string> definedSelection in mDefinedSelectionOptions)
				{
					if (object.Equals(this.Value, definedSelection.Key))
					{
						return i;
					}
					i++;
				}
				return -1;
			}
			set
			{
				try
				{
					mMaskedTextBoxIT.Text = mDefinedSelectionOptions[value].Value;
				}
				catch
				{
					mMaskedTextBoxIT.Text = string.Empty;
				}
			}
		}
		/// <summary>
		/// Gets or sets the Read Only property
		/// </summary>
		new public bool ReadOnly
		{
			get
			{
				return mMaskedTextBoxIT.ReadOnly;
			}
			set
			{
				mMaskedTextBoxIT.ReadOnly = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'MaskedTextBoxPresentationWithDefinedSelection' class,
		///  specifying that the control does not perform the validation.
		/// </summary>
		/// <param name="maskedTextBox">.NET MaskedTextBox reference.</param>
		public MaskedTextBoxPresentationWithDefinedSelection(MaskedTextBox maskedTextBox)
			:base(maskedTextBox, false)
		{
		}
		#endregion Constructors

		#region Methods
		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <param name="index">Index where item is inserted.</param>
		/// <param name="optionValue">Item to insert</param>
		public void InsertItem(int index, string optionValue)
		{
			// It is not needed to provide code in this function because this Presentation is used only to show values.
		}
		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <param name="optionValue"></param>
		public void InsertItem(string optionValue)
		{
			// It is not needed to provide code in this function because this Presentation is used only to show values.
		}
		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <param name="index">Index where item will be removed.</param>
		public void RemoveItem (int index)
		{
			// It is not needed to provide code in this function because this Presentation is used only to show values.
		}
		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <param name="optionValue">Item to remove.</param>
		public void RemoveItem (string optionValue)
		{
			// It is not needed to provide code in this function because this Presentation is used only to show values.
		}
		#endregion Methods
	}
}
