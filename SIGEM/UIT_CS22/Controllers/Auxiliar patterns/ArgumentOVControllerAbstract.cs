// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SIGEM.Client.Presentation;
using SIGEM.Client.Logics;
using SIGEM.Client.Oids;
using SIGEM.Client;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the object-valued Argument controller.
	/// </summary>
	public abstract class ArgumentOVControllerAbstract : ArgumentController
	{
		#region Members
		/// <summary>
		/// Domain or class name of the object-valued Argument.
		/// </summary>
		private string mDomain;
		/// <summary>
		/// Required Attributes for State Change Detection.
		/// </summary>
		private DisplaySetInformation mSCDAttributes = null;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ArgumentOVControllerAbstract' class.
		/// </summary>
		/// <param name="name">Name of the object-valued Argument.</param>
		/// <param name="alias">Alias of the object-valued Argument.</param>
		/// <param name="idxml">IdXML of the object-valued Argument.</param>
		/// <param name="domain">Domain or class name of the object-valued Argument.</param>
		/// <param name="nullAllowed">Indicates whether the object-valued Argument allows null values.</param>
		/// <param name="multipleSelectionAllowed">Indicates whether the object-valued Argument allows multiple values.</param>
		/// <param name="parent">Parent controller.</param>
		public ArgumentOVControllerAbstract(
			string name,
			string alias,
			string idxml,
			string domain,
			bool nullAllowed,
			bool multiSelectionAllowed,
			IUController parent)
			: base(name, parent, alias, idxml, nullAllowed, multiSelectionAllowed)
		{
			mDomain = domain;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets the domain or class name of the object-valued Argument.
		/// </summary>
		public string Domain
		{
			get
			{
				return mDomain;
			}
		}
		/// <summary>
		/// Gets or sets the last oid values of the object-valued Argument.
		/// </summary>
		protected List<Oid> LastValueListOids
		{
			get
			{
				List<Oid> lOids = base.LastValue as List<Oid>;
				return lOids;
			}
			set
			{
				base.LastValue = value;

				// Get attribute values for State Change Detection
				GetValuesForSCD(value);
			}
		}
		/// <summary>
		/// Required Attributes for 'State Change Detection'.
		/// </summary>
		public DisplaySetInformation SCDAttributes
		{
			get
			{
				if (mSCDAttributes == null)
				{
					mSCDAttributes = new DisplaySetInformation(string.Empty);
				}

				return mSCDAttributes;
			}
			set
			{
				mSCDAttributes = value;
			}
		}
		#endregion Properties

		#region Methods
		/// <summary>
		/// Query the attribute values used in the State Change Detection.
		/// </summary>
		/// <param name="oids">Oid list.</param>
		protected void GetValuesForSCD(List<Oid> oids)
		{
			// No Oids, do nothing.
			if (oids == null || (oids != null && oids.Count == 0))
			{
				return;
			}

			// Check that all the elements of the list are valid Oids.
			foreach (Oid lOid in oids)
			{
				if (!Oid.IsNotNullAndValid(lOid))
				{
					return;
				}
			}

			// More than one object select means no SCD.
			if (oids.Count > 1)
			{
				foreach (Oid oid in oids)
				{
					// Empty the lists.
					oid.SCDAttributesValues.Clear();
					oid.SCDAttributesTypes.Clear();
					oid.SCDAttributesDomains.Clear();
				}
				return;
			}

			// No SCD declared for this argument.
			if (SCDAttributes == null || SCDAttributes.DisplaySetItems == null || SCDAttributes.DisplaySetItems.Count == 0)
			{
				return;
			}

			// Empty the list.
			oids[0].SCDAttributesValues.Clear();
			oids[0].SCDAttributesTypes.Clear();
			oids[0].SCDAttributesDomains.Clear();

			// Query for all Data Valued attributes.
			string lAttributes = "";
			foreach (DisplaySetItem lItem in SCDAttributes.DisplaySetItems)
			{
				if (lItem.ModelType != ModelType.Oid)
				{
					if (!lAttributes.Equals(string.Empty))
						lAttributes += ",";

					lAttributes += lItem.Name;
				}
				else
				{
					// Add the OID fields
					foreach (string oidField in lItem.OIDFields)
					{
						if (!lAttributes.Equals(string.Empty))
							lAttributes += ",";

						lAttributes += lItem.Name + "." + oidField;
					}
				}
			}

			lAttributes = UtilFunctions.ReturnMissingAttributes(oids[0].ExtraInfo, lAttributes);
			if (lAttributes != "")
			{
				DataTable dataTable = null;
				try
				{
					dataTable = Logic.ExecuteQueryInstance(Logic.Agent, Domain, oids[0], lAttributes);
				}
				catch
				{
					return;
				}
				if (dataTable == null || dataTable.Rows.Count == 0)
				{
					return;
				}
				oids[0].ExtraInfo.Merge(dataTable);
			}

			// Pass the values to the Oid list.
			foreach (DisplaySetItem lItem in SCDAttributes.DisplaySetItems)
			{
				string lName = Name + "." + lItem.Name;
				if (lItem.ModelType != ModelType.Oid)
				{
					object attValue = oids[0].ExtraInfo.Rows[0][lItem.Name];
					attValue = (attValue == DBNull.Value) ? null : attValue;
					oids[0].SCDAttributesValues.Add(lName, attValue);
					oids[0].SCDAttributesTypes.Add(lName, lItem.ModelType);
					oids[0].SCDAttributesDomains.Add(lName, lItem.ClassName);
				}
				else
				{
					// Create a new Oid using the field values.
					Oid lOid = null;
					List<Object> lOIDFieldValues = new List<object>();
					bool lNUllValue = false;
					foreach (string oidField in lItem.OIDFields)
					{
						object lValue = oids[0].ExtraInfo.Rows[0][lItem.Name + "." + oidField];
						lValue = (lValue == DBNull.Value) ? null : lValue;
						if (lValue == null)
						{
							lNUllValue = true;
						}

						lOIDFieldValues.Add(lValue);
					}

					if (!lNUllValue)
					{
						lOid = Oid.Create(lItem.ClassName, lOIDFieldValues);
					}
					oids[0].SCDAttributesValues.Add(lName, lOid);
					oids[0].SCDAttributesTypes.Add(lName, lItem.ModelType);
					oids[0].SCDAttributesDomains.Add(lName, lItem.ClassName);
				}
			}
		}
		/// <summary>
		/// Update the value for one attribute involved in the State Change Detection.
		/// </summary>
		/// <param name="attName">Attribute name.</param>
		/// <param name="newValue">New value.</param>
		public void UpdateSCDValueAttribute(string attName, object newValue)
		{
			// Just for one instance.
			if (LastValueListOids == null || LastValueListOids.Count != 1)
			{
				return;
			}
			LastValueListOids[0].SCDAttributesValues[attName] = newValue;
		}
		/// <summary>
		/// Update the type for one attribute involved in the State Change Detection.
		/// </summary>
		/// <param name="attName">Attribute name.</param>
		/// <param name="type">Attribute type.</param>
		public void UpdateSCDTypeAttribute(string attName, ModelType type)
		{
			// Just for one instance.
			if (LastValueListOids == null || LastValueListOids.Count != 1)
			{
				return;
			}
			LastValueListOids[0].SCDAttributesTypes[attName] = type;
		}
		/// <summary>
		/// Update the domain for one attribute involved in the State Change Detection.
		/// </summary>
		/// <param name="attName">Attribute name.</param>
		/// <param name="domain">Attribute domain.</param>
		public void UpdateSCDDomainAttribute(string attName, string domain)
		{
			// Just for one instance.
			if (LastValueListOids == null || LastValueListOids.Count != 1)
			{
				return;
			}
			LastValueListOids[0].SCDAttributesDomains[attName] = domain;
		}
		/// <summary>
		/// Gets or sets the supplementary information text showed for the object-valued Argument.
		///   - Specific implemetation done in child controllers.
		/// </summary>
		public abstract string GetSupplementaryInfoText();
		
		#endregion Methods
	}
}

