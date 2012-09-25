// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using SIGEM.Client.Controllers;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of the .NET Label control in a DiplaySet.
	/// </summary>
	public class LabelDisplaySetPresentation : IDisplaySetPresentation
	{
		#region Members
		/// <summary>
		/// Label instance reference.
		/// </summary>
		protected Label mLabelIT;
		/// <summary>
		/// Showed Oid.
		/// </summary>
		private Oid mShowedOID = null;
		/// <summary>
		/// Label type list.
		/// </summary>
		private List<ModelType> mTypeList = new List<ModelType>();
		/// <summary>
		/// Defined selection values
		/// </summary>
		private List<List<KeyValuePair<object, string>>> mDefinedSelectionOptions = new List<List<KeyValuePair<object,string>>>();
		/// <summary>
		/// Texts to be shown for boolean values.
		/// </summary>
		private List<KeyValuePair<object, string>> mBooleanValues = new List<KeyValuePair<object, string>>();
		/// <summary>
		/// Attribute names to be used as suplementary information
		/// </summary>
		private List<string> mAttributeNames = new List<string>();
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'LabelDisplaySetPresentation'.
		/// </summary>
		/// <param name="label">Label.</param>
		public LabelDisplaySetPresentation(Label label)
		{
			mLabelIT = label;
			AssignDefaultTextForBooleans();
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets Visible property.
		/// </summary>
		public bool Visible
		{
			get
			{
				return mLabelIT.Visible;
			}
			set
			{
				mLabelIT.Visible = value;
			}
		}
		/// <summary>
		/// Gets or sets Enabled property.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return mLabelIT.Enabled;
			}
			set
			{
				mLabelIT.Enabled = value;
			}
		}
		/// <summary>
		/// Display Texts for Boolean values
		/// </summary>
		public string DisplayTextBoolNull
		{
			get
			{
				if (mBooleanValues.Count > 0)
				{
					return mBooleanValues[0].Value;
				}
				return "";
			}
			set
			{
				if (mBooleanValues.Count > 0)
				{
					mBooleanValues.RemoveAt(0);
				}
				mBooleanValues.Insert(0, new KeyValuePair<object, string>(null, value));
			}
		}
		public string DisplayTextBoolTrue
		{
			get
			{
				if (mBooleanValues.Count > 1)
				{
					return mBooleanValues[1].Value;
				}
				return "";
			}
			set
			{
				if (mBooleanValues.Count > 1)
				{
					mBooleanValues.RemoveAt(1);
				}
				mBooleanValues.Insert(1, new KeyValuePair<object, string>(true, value));
			}
		}
		public string DisplayTextBoolFalse
		{
			get
			{
				if (mBooleanValues.Count > 2)
				{
					return mBooleanValues[2].Value;
				}
				return "";
			}
			set
			{
				if (mBooleanValues.Count > 2)
				{
					mBooleanValues.RemoveAt(2);
				}
				mBooleanValues.Insert(2, new KeyValuePair<object, string>(false, value));
			}
		}
		/// <summary>
		/// Returns the text displayed of the instance loaded.
		/// </summary>
		public string DisplayedText
		{
			get
			{
				return mLabelIT.Text;
			}
		}
		#endregion Properties

		#region Events
		/// <summary>
		/// No commands associated.
		/// </summary>
		public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		/// <summary>
		/// Occurs when selected cell is changed (not used, defined due to compatibility of the interface).
		/// </summary>
		public event EventHandler<SelectedChangedEventArgs> SelectionChanged;
		#endregion Events

		#region Event Raisers
		/// <summary>
		/// Raises the ExecuteCommand event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnExecuteCommand(ExecuteCommandEventArgs eventArgs)
		{

			EventHandler<ExecuteCommandEventArgs> handler = ExecuteCommand;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the SelectionChanged event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnSelectionChanged(SelectedChangedEventArgs eventArgs)
		{
			EventHandler<SelectedChangedEventArgs> handler = SelectionChanged;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		#endregion Event Raisers

		#region Methods
		/// <summary>
		/// Three texts are added to the list. Given order is important, related properties will use that order
		/// </summary>
		private void AssignDefaultTextForBooleans()
		{
			string boolNull = CultureManager.TranslateString(LanguageConstantKeys.L_BOOL_NULL, LanguageConstantValues.L_BOOL_NULL);
			mBooleanValues.Add(new KeyValuePair<object, string>(null, boolNull));
			string boolTrue = CultureManager.TranslateString(LanguageConstantKeys.L_BOOL_TRUE, LanguageConstantValues.L_BOOL_TRUE);
			mBooleanValues.Add(new KeyValuePair<object, string>(true, boolTrue));
			string boolFalse = CultureManager.TranslateString(LanguageConstantKeys.L_BOOL_FALSE, LanguageConstantValues.L_BOOL_FALSE);
			mBooleanValues.Add(new KeyValuePair<object, string>(false, boolFalse));
		}
		/// <summary>
		/// Sets label text from a message string.
		/// </summary>
		/// <param name="message">Label text.</param>
		public void SetMessage(string message)
		{
			// Shows the message.
			mLabelIT.Text = message;
		}
		/// <summary>
		/// Shows data (empty function due to compatibility of the interface).
		/// </summary>
		/// <param name="data">Data.</param>
		public void ShowData(DataTable data, List<Oid> selectedOids)
		{
			if (data == null || data.Rows.Count == 0)
			{
				mLabelIT.Text = "";
				return;
			}

			DataRow row = data.Rows[0];

			string lText = GetRowAsText(row);
			mLabelIT.Text = lText;
		}
		/// <summary>
		/// Converts the received row in a string.
		/// </summary>
		/// <param name="row">Data Row</param>
		private string GetRowAsText(DataRow row)
		{
			string lText="";
			string lAux = "";
			int i = 0;
			// For all the columns in the Row
			foreach (string attribute in mAttributeNames)
			{
				// Defined selection case
				if (mDefinedSelectionOptions[i] != null)
				{
					lAux = "";
					for (int j = 0; j < mDefinedSelectionOptions[i].Count; j++)
					{
						if (row[attribute].GetType() == typeof(System.DBNull))
						{
							if (mDefinedSelectionOptions[i][j].Key == null)
							{
								lAux += mDefinedSelectionOptions[i][j].Value;
								break;
							}
						}
						else
						{
							if (mDefinedSelectionOptions[i][j].Key != null &&
								mDefinedSelectionOptions[i][j].Key.ToString() == row[attribute].ToString())
							{
								lAux += mDefinedSelectionOptions[i][j].Value;
								break;
							}
						}
					}

					// Value not found in the defined selection set. Apply default
					if (lAux == "" && row[attribute].GetType() != typeof(System.DBNull))
					{
						lAux = DefaultFormats.ApplyDisplayFormat(row[attribute], mTypeList[i]);
					}
				}
				else
				{
					lAux = DefaultFormats.ApplyDisplayFormat(row[attribute], mTypeList[i]);
				}
				i++;

				// Separates values with blank space
				if (lText != "")
					lText += " ";
				lText += lAux;
			}

			return lText;
		}
		/// <summary>
		/// Gets related Oid to the label.
		/// </summary>
		/// <returns>Oid list containing related Oid.</returns>
		public List<Oid> GetSelectedOIDs()
		{
			List<Oid> oids = new List<Oid>();
			oids.Add(mShowedOID);
			return oids;
		}
		/// <summary>
		/// Selected Index Changed (empty function due to compatibility of the interface).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectedIndexChanged(object sender, EventArgs e)
		{
		}
		/// <summary>
		/// Gets or sets values (empty due to compatibility of the interface).
		/// </summary>
		public List<Oid> Values
		{
			// Keep the compatibility of the interface.
			get
			{
				return null;
			}
			set
			{
			}
		}
		/// <summary>
		/// Adds a new attribute to the display list
		/// </summary>
		/// <param name="name"></param>
		/// <param name="alias"></param>
		/// <param name="idxml"></param>
		/// <param name="modelType"></param>
		/// <param name="definedSelectionOptions"></param>
		/// <param name="Agents"></param>
		/// <param name="width"></param>
		public void AddDisplaySetItem(string name, string alias, string idxml, ModelType modelType, List<KeyValuePair<object, string>> definedSelectionOptions, string[] Agents, int width)
		{
			mAttributeNames.Add(name);
			mTypeList.Add(modelType);
			if (modelType == ModelType.Bool)
			{
				mDefinedSelectionOptions.Add(mBooleanValues);
			}
			else
			{
				mDefinedSelectionOptions.Add(definedSelectionOptions);
			}
		}
		/// <summary>
		/// Empty implementation. Compatibility with the IDisplaySetPresentation
		/// </summary>
		/// <param name="name"></param>
		/// <param name="alias"></param>
		/// <param name="modelType"></param>
		/// <param name="definedSelectionOptions"></param>
		/// <param name="width"></param>
		public void SetFormatDisplaySetItem(string name, string alias, ModelType modelType, List<KeyValuePair<object, string>> definedSelectionOptions, int width, bool editable, bool allowsNullInEditMode)
		{
		}
		/// <summary>
		/// Removes all the DisplaySet items
		/// </summary>
		public void RemoveAllDisplaySetItems()
		{
		}
		/// <summary>
		/// Returns in a list tha width of every visible column
		/// </summary>
		/// <returns></returns>
		public List<int> GetColumnsWidth()
		{
			return null;
		}
		/// <summary>
		/// Returns the modified rows.
		/// Empty implementation
		/// </summary>
		/// <returns></returns>
		public DataTable GetModifiedRows()
		{
			return null;
		}
		#endregion Methods
	}
}
