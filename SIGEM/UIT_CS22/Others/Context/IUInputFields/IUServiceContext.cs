// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;

using SIGEM.Client.Oids;

namespace SIGEM.Client
{
	/// <summary>
	/// Class 'IUServiceContext'.
	/// </summary>
	[Serializable]
	public class IUServiceContext : IUInputFieldsContext
	{
		#region Members
		/// <summary>
		/// Output fields dictionary.
		/// </summary>
		private Dictionary<string, object> mOutputFields = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
		/// <summary>
		/// Service executed?.
		/// </summary>
		public bool ExecutedService = false;
		/// <summary>
		/// Instance of Selected Oids.
		/// </summary>
		protected List<Oid> mSelectedOids = new List<Oid>();
		/// <summary>
		/// Instance of Related Oids.
		/// </summary>
		protected List<Oid> mRelatedOids;
		/// <summary>
		/// Instance of Related Path.
		/// </summary>
		protected string mRelatedPath = string.Empty;
		/// <summary>
		/// Next & Previous Oids.
		/// </summary>
		private List<Oid> mNextPreviousOids = new List<Oid>();
		/// <summary>
		/// Index of the Next & Previous Oids.
		/// </summary>
		private int mIndexNextPreviousOids = 0;
		/// <summary>
		/// Boolean value indicating whether the scenario is an output fields scenario.
		/// </summary>
		private bool mIsOutputFieldsScenario = false;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets the service name.
		/// </summary>
		public string ServiceName
		{
			get
			{
				return ContainerName;
			}
			protected set
			{
				ContainerName = value;
			}
		}
		/// <summary>
		/// Gets the output fields dictionary.
		/// </summary>
		public Dictionary<string, object> OutputFields
		{
			get
			{
				return mOutputFields;
			}
			set
			{
				mOutputFields = value;
			}
		}
		/// <summary>
		/// Gets or sets selected Oids.
		/// </summary>
		public override List<Oid> SelectedOids
		{
			get
			{
				return mSelectedOids;
			}
			set
			{
				mSelectedOids = value;
			}
		}
		/// <summary>
		/// Gets or sets related Oids.
		/// </summary>
		public override List<Oid> RelatedOids
		{
			get
			{
				return mRelatedOids;
			}
			set
			{
				mRelatedOids = value;
			}
		}
		/// <summary>
		/// Gets related path.
		/// </summary>
		public override string RelatedPath
		{
			get
			{
				return mRelatedPath;
			}
		}
		/// <summary>
		/// Gets the Next & Previous Oids.
		/// </summary>
		public List<Oid> NextPreviousOids
		{
			get
			{
				return mNextPreviousOids;
			}
		}
		/// <summary>
		/// Gets or sets the index of th Next & Previous Oids.
		/// </summary>
		public int IndexNextPreviousOids
		{
			get
			{
				return mIndexNextPreviousOids;
			}
			set
			{
				mIndexNextPreviousOids = value;
			}
		}
		/// <summary>
		/// Gets a boolean value indicating whether the scenario is an output fields scenario.
		/// </summary>
		public bool IsOutputFieldsScenario
		{
			get
			{
				return mIsOutputFieldsScenario;
			}
			protected set
			{
				mIsOutputFieldsScenario = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initalizes a new instance of the 'IUServiceContext' class.
		/// </summary>
		/// <param name="exchangeInfo">Exchange information.</param>
		/// <param name="className">Class name.</param>
		/// <param name="serviceName">Sevice name.</param>
		/// <param name="iuName">IU name.</param>
		public IUServiceContext(ExchangeInfo exchangeInfo, string className, string serviceName, string iuName)
			: base(exchangeInfo, className, serviceName, iuName)
		{
			ContextType = ContextType.Service;
		}
		/// <summary>
		/// Initalizes a new instance of the 'IUServiceContext' class.
		/// </summary>
		/// <param name="exchangeInfo">Exchange information.</param>
		/// <param name="className">Class name.</param>
		/// <param name="serviceName">Sevice name.</param>
		/// <param name="iuName">Interaction unit name.</param>
		/// <param name="isOutputFieldsScenario">Boolean value indicating whether the scenario is an output fields scenario.</param>
		public IUServiceContext(ExchangeInfo exchangeInfo, string className, string serviceName, string iuName, bool isOutputFieldsScenario)
			: this(exchangeInfo, className, serviceName, iuName)
		{
			IsOutputFieldsScenario = isOutputFieldsScenario;
		}
		#endregion Constructors

		#region Methods
		#region Clear output fields
		/// <summary>
		/// Clear the output fields.
		/// </summary>
		public void ClearOutputFields()
		{
			// Clear all the output fields.
			foreach (KeyValuePair<string,object> lOutputField in OutputFields)
			{
				OutputFields[lOutputField.Key] = null;
			}
		}
		#endregion Clear output fields

		#region Get output field
		/// <summary>
		/// Get the output field.
		/// </summary>
		/// <param name="outputFieldName">Output field name.</param>
		/// <returns>Output field.</returns>
		public object GetOutputField(string outputFieldName)
		{
			if (OutputFields.ContainsKey(outputFieldName))
			{
				return OutputFields[outputFieldName];
			}

			return null;
		}
		#endregion Get output field

		#region Get/Set output field value
		/// <summary>
		/// Get the output field value.
		/// </summary>
		/// <param name="outputFieldName">Output field name.</param>
		/// <returns>Output field value.</returns>
		public object GetOutputFieldValue(string outputFieldName)
		{
			return GetOutputField(outputFieldName);
		}
		/// <summary>
		/// Set the output field value.
		/// </summary>
		/// <param name="outputFieldName">Output field name.</param>
		/// <param name="value">Value.</param>
		public void SetOutputFieldValue(string outputFieldName, object value)
		{
			try
			{
				OutputFields[outputFieldName] = value;
			}
			catch
			{
			}
		}
		#endregion Get/Set output field value

		/// <summary>
		/// Clear ExtraInfo of Oids stored in the full context.
		/// </summary>
		public void ClearOVExtraInfo()
		{
			// Clear the Object-valued inbound arguments ExtraInfo.
			this.ClearOVInputExtraInfo();

			// Clears the Extra information stored in the navigated Oids.
			if (this.ExchangeInformation != null)
			{
				this.ExchangeInformation.ClearExtraInfo();
			}
		}

		/// <summary>
		/// Clear the Object-valued inbound arguments ExtraInfo. 
		/// </summary>
		private void ClearOVInputExtraInfo()
		{
			foreach (IUContextArgumentInfo lInputField in InputFields.Values)
			{
				List<Oid> lOVInputFieldValue;
				if ((lInputField.Value != null) &&
					(lInputField.Value is List<Oid>))
				{
					// If it is an Object-valued argument, reset its ExtraInfo.
					lOVInputFieldValue = (lInputField.Value as List<Oid>);
					foreach (Oid lOid in lOVInputFieldValue)
					{
						lOid.ExtraInfo = null;
					}
				}
			}
		}
		#endregion Methods
	}
}


