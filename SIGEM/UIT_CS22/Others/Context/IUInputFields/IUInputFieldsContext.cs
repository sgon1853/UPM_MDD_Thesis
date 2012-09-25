// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;

using SIGEM.Client.Oids;

namespace SIGEM.Client
{
	/// <summary>
	/// Class 'IUInputFieldsContext'.
	/// </summary>
	[Serializable]
	public abstract class IUInputFieldsContext : IUClassContext
	{
		#region Members
		/// <summary>
		/// Name of the input fields container.
		/// </summary>
		private string mContainerName;
		/// <summary>
		/// Input fields dictionary.
		/// </summary>
		private Dictionary<string, IUContextArgumentInfo> mInputFields = new Dictionary<string, IUContextArgumentInfo>(StringComparer.InvariantCultureIgnoreCase);
		/// <summary>
		/// Dictionary containing the input fields which allow multiple selection.
		/// </summary>
		private Dictionary<string, IUContextArgumentInfo> mMultiSelectionInputFields = new Dictionary<string, IUContextArgumentInfo>(StringComparer.InvariantCultureIgnoreCase);
		/// <summary>
		/// Name of the selected input field.
		/// </summary>
		private string mSelectedInputField = string.Empty;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets the name of the input fields container.
		/// </summary>
		public string ContainerName
		{
			get
			{
				return mContainerName;
			}
			protected set
			{
				mContainerName = value;
			}
		}
		/// <summary>
		/// Gets the input fields dictionary.
		/// </summary>
		public Dictionary<string, IUContextArgumentInfo> InputFields
		{
			get
			{
				return mInputFields;
			}
		}
		/// <summary>
		/// Gets a dictionary containing the input fields which allow multiple selection.
		/// </summary>
		public Dictionary<string, IUContextArgumentInfo> MultiSelectionInputFields
		{
			get
			{
				return mMultiSelectionInputFields;
			}
		}
		/// <summary>
		/// Gets or sets the name of the selected input field.
		/// </summary>
		public string SelectedInputField
		{
			get
			{
				return mSelectedInputField;
			}
			set
			{
				mSelectedInputField = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'IUInputFieldsContext' class.
		/// </summary>
		/// <param name="exchangeInfo">Exchange information.</param>
		/// <param name="className">Class name.</param>
		/// <param name="containerName">Container name.</param>
		/// <param name="iuPatternName">Interaction unit name.</param>
		public IUInputFieldsContext(ExchangeInfo exchangeInfo, string className, string containerName, string iuPatternName)
			: base(exchangeInfo, ContextType.InputFields, className, iuPatternName)
		{
			ContainerName = containerName;
		}
		#endregion Constructors

		#region Methods
		#region Clear input fields
		/// <summary>
		/// Clear the input fields.
		/// </summary>
		public virtual void ClearInputFields()
		{
			// Configure all the input fields to its default value.
			foreach (IUContextArgumentInfo lInputField in InputFields.Values)
			{
				lInputField.Value = null;
				lInputField.Enabled = true;
				lInputField.MultiSelectionAllowed = false;
				lInputField.SupplementaryInfo = string.Empty;
				lInputField.PreLoadPopulation = null;
				lInputField.PreLoadPopulationInitialized = false;
				lInputField.OrderCriteria = string.Empty;
			}
		}
		#endregion Clear input fields
		
		#region Clear input fields focus
		/// <summary>
		/// Clear the input fields focus.
		/// </summary>
		public virtual void ClearInputFieldsFocus()
		{
			// Clear the focus for all the Input Fields.
			foreach (IUContextArgumentInfo lInputField in InputFields.Values)
			{
				lInputField.Focused = false;
			}
		}
		#endregion Clear input fields focus

		#region Get input field
		/// <summary>
		/// Get the input field.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <returns>Input field.</returns>
		public virtual IUContextArgumentInfo GetInputField(string inputFieldName)
		{
			IUContextArgumentInfo lInputField = null;

			if (InputFields.ContainsKey(inputFieldName))
			{
				return InputFields[inputFieldName];
			}
			else
			{
				lInputField = new IUContextArgumentInfo(inputFieldName);
				InputFields[inputFieldName] = lInputField;
			}

			return lInputField;
		}
		#endregion Get input field

		#region Get/Set input field value
		/// <summary>
		/// Get the input field value.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <returns>Input field value.</returns>
		public virtual object GetInputFieldValue(string inputFieldName)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				return lInputField.Value;
			}

			return null;
		}
		/// <summary>
		/// Set the input field value.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <param name="value">Input field value.</param>
		public virtual void SetInputFieldValue(string inputFieldName, object value)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				lInputField.Value = value;
			}
		}
		#endregion Get/Set input field value

		#region Get/Set input field enabled
		/// <summary>
		/// Get the input field enabled.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <returns>Boolean value indicating whether the input field is enabled or not.</returns>
		public virtual bool GetInputFieldEnabled(string inputFieldName)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				return lInputField.Enabled;
			}

			return true;
		}
		/// <summary>
		/// Set the input field enabled.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <param name="enabled">Boolean value.</param>
		public virtual void SetInputFieldEnabled(string inputFieldName, bool enabled)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				lInputField.Enabled = enabled;
			}
		}
		#endregion Get/Set input field enabled

		#region Get/Set input field Mandatory
		/// <summary>
		/// Get the input field Mandatory.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <returns>Boolean value indicating whether the input field is Mandatory or not.</returns>
		public virtual bool GetInputFieldMandatory(string inputFieldName)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				return lInputField.Mandatory;
			}

			return true;
		}
		/// <summary>
		/// Set the input field Mandatory.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <param name="mandatory">Boolean value.</param>
		public virtual void SetInputFieldMandatory(string inputFieldName, bool mandatory)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				lInputField.Mandatory = mandatory;
			}
		}
		#endregion Get/Set input field Mandatory

		#region Get/Set input field Visible
		/// <summary>
		/// Get the input field Visible.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <returns>Boolean value indicating whether the input field is visible or not.</returns>
		public virtual bool GetInputFieldVisible(string inputFieldName)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				return lInputField.Visible;
			}

			return true;
		}
		/// <summary>
		/// Set the input field Visible.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <param name="visible">Boolean value.</param>
		public virtual void SetInputFieldVisible(string inputFieldName, bool visible)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				lInputField.Visible = visible;
			}
		}
		#endregion Get/Set input field Visible

		#region Get/Set input field Focused
		/// <summary>
		/// Get the input field Focused.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <returns>Boolean value indicating whether the input field has the focus or not.</returns>
		public virtual bool GetInputFieldFocused(string inputFieldName)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				return lInputField.Focused;
			}

			return true;
		}
		/// <summary>
		/// Set the input field Focused.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <param name="focused">Boolean value.</param>
		public virtual void SetInputFieldFocused(string inputFieldName)
		{
			ClearInputFieldsFocus();

			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				lInputField.Focused = true;
			}
		}
		#endregion Get/Set input field Focused
		#region Get/Set input field multiselection allowed
		/// <summary>
		/// Get the input field multiselection allowed.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <returns>Boolean value indicating whether the input field allows multilsection or not.</returns>
		public virtual bool GetInputFieldMultiSelectionAllowed(string inputFieldName)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				return lInputField.MultiSelectionAllowed;
			}

			return false;
		}
		/// <summary>
		/// Set the input field multiselection allowed.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <param name="multiSelectionAllowed">Boolean value.</param>
		public virtual void SetInputFieldMultiSelectionAllowed(string inputFieldName, bool multiSelectionAllowed)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				lInputField.MultiSelectionAllowed = multiSelectionAllowed;
			}
		}
		#endregion Get/Set input field multiselection allowed

		#region Get/Set input field supplemantary information
		/// <summary>
		/// Get the input field supplementary information.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <returns>String containig the input field supplementary information.</returns>
		public virtual string GetInputFieldSupplementaryInfo(string inputFieldName)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				return lInputField.SupplementaryInfo;
			}

			return string.Empty;
		}
		/// <summary>
		/// Set the input field supplementary information.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <param name="supplementaryInfo">String containing the input field supplementary information.</param>
		public virtual void SetInputFieldSupplementaryInfo(string inputFieldName, string supplementaryInfo)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				lInputField.SupplementaryInfo = supplementaryInfo;
			}
		}
		#endregion Get/Set input field supplemantary information

		#region Get/Set input field preload population
		/// <summary>
		/// Get the input field preload population.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <returns>Datatable containig the preload population.</returns>
		public virtual DataTable GetInputFieldPreloadPopulation(string inputFieldName)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				return lInputField.PreLoadPopulation;
			}

			return null;
		}
		/// <summary>
		/// Set the input field preload population.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <param name="preloadPopulation">Input field preload population.</param>
		public virtual void SetInputFieldPreloadPopulation(string inputFieldName, DataTable preloadPopulation)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);
			if (lInputField != null)
			{
				// Get the input field value.
				List<Oid> lInputFieldValue = lInputField.Value as List<Oid>;

				// Get the preload Oids and check whether contains the input field value.
				if ((lInputFieldValue != null) && (lInputFieldValue.Count > 0) && (preloadPopulation != null))
				{
					List<Oid> lPreloadOids = new List<Oid>();
					foreach (DataRow lRow in preloadPopulation.Rows)
					{
						Oid lOid = Adaptor.ServerConnection.GetOid(preloadPopulation, lRow);
						lPreloadOids.Add(lOid);
					}

					// Set the null to the input field value if the Oid is not contained into the preload.
					if (!(lPreloadOids.Contains(lInputFieldValue[0])))
					{
						lInputField.Value = null;
					}
				}

				// Set the input field preload population.
				lInputField.PreLoadPopulation = preloadPopulation;

				// Set the input field preload population initialized.
				lInputField.PreLoadPopulationInitialized = true;
			}
		}
		#endregion Get/Set input field preload population

		#region Get/Set input field preload population initialized
		/// <summary>
		/// Get the input field preload population initialized.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <returns>Boolean value indicating whether the input field is initialized or not.</returns>
		public virtual bool GetInputFieldPreloadPopulationInitialized(string inputFieldName)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				return lInputField.PreLoadPopulationInitialized;
			}

			return false;
		}
		/// <summary>
		/// Get the input field preload population initialized.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <param name="initialized">Boolean value.</param>
		public virtual void SetInputFieldPreloadPopulationInitialized(string inputFieldName, bool initialized)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				lInputField.PreLoadPopulationInitialized = initialized;
			}
		}
		#endregion Get/Set input field preload population initialized

		#region Get/Set input field order criteria
		/// <summary>
		/// Get the input field order criteria.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <returns>String containig the name of the input field order criteria.</returns>
		public virtual string GetInputFieldOrderCriteria(string inputFieldName)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				return lInputField.OrderCriteria;
			}

			return string.Empty;
		}
		/// <summary>
		/// Set the input field order criteria.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <param name="orderCriteria">Order criteria name.</param>
		public virtual void SetInputFieldOrderCriteria(string inputFieldName, string orderCriteria)
		{
			IUContextArgumentInfo lInputField = GetInputField(inputFieldName);

			if (lInputField != null)
			{
				lInputField.OrderCriteria = orderCriteria;
			}
		}
		#endregion Get/Set input field order criteria

		#region Clear multiselection input fields
		/// <summary>
		/// Clear the multiselection input fields.
		/// </summary>
		public void ClearMultiSelectionInputFields()
		{
			// Configure all the multiselection input fields to its default value.
			foreach (IUContextArgumentInfo lMultiSelectionInputField in MultiSelectionInputFields.Values)
			{
				lMultiSelectionInputField.Name = string.Empty;
				lMultiSelectionInputField.Value = null;
				lMultiSelectionInputField.Enabled = true;
				lMultiSelectionInputField.MultiSelectionAllowed = false;
				lMultiSelectionInputField.SupplementaryInfo = string.Empty;
				lMultiSelectionInputField.PreLoadPopulation = null;
				lMultiSelectionInputField.PreLoadPopulationInitialized = false;
				lMultiSelectionInputField.OrderCriteria = string.Empty;
			}
		}
		#endregion Clear multiselection input fields

		#region Get multiselection input field
		/// <summary>
		/// Get the multiselection input field.
		/// </summary>
		/// <param name="inputFieldName">Multiselection input field name.</param>
		/// <returns>Multiselection input field.</returns>
		public virtual IUContextArgumentInfo GetMultiSelectionInputField(string inputFieldName)
		{
			IUContextArgumentInfo lMultiSelectionInputField = null;

			if (MultiSelectionInputFields.ContainsKey(inputFieldName))
			{
				return MultiSelectionInputFields[inputFieldName];
			}
			else
			{
				lMultiSelectionInputField = new IUContextArgumentInfo(inputFieldName);
				MultiSelectionInputFields[inputFieldName] = lMultiSelectionInputField;
			}

			return lMultiSelectionInputField;
		}
		#endregion Get multiselection input field

		#region Get/Set multiselection input fields value
		/// <summary>
		/// Get the multiselection input field value.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <returns>Multiselection input field value.</returns>
		public object GetMultiSelectionInputFieldValue(string inputFieldName)
		{
			IUContextArgumentInfo lMultiSelectionInputField = GetMultiSelectionInputField(inputFieldName);

			if (lMultiSelectionInputField != null)
			{
				return lMultiSelectionInputField.Value;
			}

			return null;
		}
		/// <summary>
		/// Set the multiselection input field value.
		/// </summary>
		/// <param name="inputFieldName">Input field name.</param>
		/// <param name="value">Multiselection input field value.</param>
		public void SetMultiSelectionInputFieldValue(string inputFieldName, object value)
		{
			IUContextArgumentInfo lMultiSelectionInputField = GetMultiSelectionInputField(inputFieldName);

			if (lMultiSelectionInputField != null)
			{
				lMultiSelectionInputField.Value = value;
			}
		}
		#endregion Multiselection inbound argument methods
		#endregion Methods
	}
}


