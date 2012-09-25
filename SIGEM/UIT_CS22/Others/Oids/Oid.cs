// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

using SIGEM.Client.Adaptor;

namespace SIGEM.Client.Oids
{

	#region GenericOID [class]
	[Serializable]
	public class GenericOID : Oid
	{
		#region Constructors
		public GenericOID(string className)
			: base(className, string.Empty, string.Empty)
		{}

		public GenericOID(string className, object[] values, ModelType[] types)
			: base(className, string.Empty, string.Empty, values, types)
		{}

		public GenericOID(string className, IList<object> values, IList<ModelType> types)
			: base(className, string.Empty, string.Empty, values, types)
		{ }
		#endregion Constructors

	}
	#endregion GenericOID [class]

	#region Oid [class]
	[Serializable]
	public abstract class Oid : IOid, IAlternateKey
	{
		#region Members
		/// <summary>
		/// Class Name.
		/// </summary>
		private string mClassName;
		/// <summary>
		/// Alias of the Oid.
		/// </summary>
		private string mAlias;
		/// <summary>
		/// IdXML of the Oid.
		/// </summary>
		private string mIdXML;
		/// <summary>
		/// Oid Fields.
		/// </summary>
		private FieldList mOidFields;
		/// <summary>
		/// Instance extra information
		/// </summary>
		private DataTable mExtraInfo = null;
		/// <summary>
		/// Attribute value list for State Change Detection.
		/// </summary>
		private Dictionary<string, object> mSCDAttributesValues;
		/// <summary>
		/// Attribute types list for State Change Detection.
		/// </summary>
		private Dictionary<string, ModelType> mSCDAttributesTypes;
		/// <summary>
		/// Attribute domains list for State Change Detection.
		/// </summary>
		private Dictionary<string, string> mSCDAttributesDomains;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets the class name.
		/// </summary>
		public string ClassName
		{
			get { return mClassName; }
			protected set { mClassName = value; }
		}
		/// <summary>
		/// Gets or sets the alias.
		/// </summary>
		public string Alias
		{
			get { return mAlias; }
			set { mAlias = value; }
		}
		/// <summary>
		/// Gets or sets the idXML.
		/// </summary>
		public string IdXML
		{
			get { return mIdXML; }
			protected set { mIdXML = value; }
		}
		/// <summary>
		/// Gets a list of the oid fields.
		/// </summary>
		protected FieldList FieldList
		{
			get { return mOidFields as FieldList; }
		}
		/// <summary>
		/// Gets or sets a generic list of the oid fields.
		/// </summary>
		public IList<IOidField> Fields
		{
			get{return mOidFields; }
			protected set{ mOidFields = value as FieldList;}
		}
		/// <summary>
		/// Instance extra information
		/// </summary>
		public DataTable ExtraInfo
		{
			get
			{
				if (mExtraInfo == null)
					mExtraInfo = new DataTable();

				return mExtraInfo;
			}
			set
			{
				mExtraInfo = value;
			}
		}

		/// <summary>
		/// Attribute value list for State Change Detection.
		/// </summary>
		public Dictionary<string, object> SCDAttributesValues
		{
			get
			{
				if (mSCDAttributesValues == null)
				{
					mSCDAttributesValues = new Dictionary<string, object>();
				}
				return mSCDAttributesValues;
			}
			set
			{
				mSCDAttributesValues = value;
			}
		}
		/// <summary>
		/// Attribute types list for State Change Detection.
		/// </summary>
		public Dictionary<string, ModelType> SCDAttributesTypes
		{
			get
			{
				if (mSCDAttributesTypes == null)
				{
					mSCDAttributesTypes = new Dictionary<string, ModelType>();
				}
				return mSCDAttributesTypes;
			}
			set
			{
				mSCDAttributesTypes = value;
			}
		}
		/// <summary>
		/// Attribute domains list for State Change Detection.
		/// </summary>
		public Dictionary<string, string> SCDAttributesDomains
		{
			get
			{
				if (mSCDAttributesDomains == null)
				{
					mSCDAttributesDomains = new Dictionary<string, string>();
				}
				return mSCDAttributesDomains;
			}
			set
			{
				mSCDAttributesDomains = value;
			}
		}
		/// <summary>
		/// Gets or sets the name of the current 'AlternateKey'.
		/// </summary>
		public virtual string AlternateKeyName
		{
			// Only the classes that have alternate key have to override this property.
			get { return string.Empty; }
			set { }
		}
		#endregion Properties

		#region Constructors
		protected Oid(string className, string alias, string idXML, FieldList fields)
		{
			this.ClassName = className;
			this.Alias = alias;
			this.IdXML = idXML;
			this.Fields = (fields == null ? new FieldList() : fields);
		}
		protected Oid(string className, string alias, string idXML)
			: this(className, alias, idXML, null as FieldList)
		{}
		protected Oid(string className, string alias, string idXML, IList<IOidField> fields)
			: this(className, alias, idXML, null as FieldList)
		{
			if (fields != null)
			{
				foreach (IOidField lfield in fields)
				{
					FieldList.Add(lfield);
				}
			}
		}
		public Oid(string className, string alias, string idXML, object[] values, ModelType[] types)
			: this(className, alias, idXML, null as FieldList)
		{
			CreateTypes(types);
			SetValues(values);
		}
		public Oid(string className, string alias, string idXML, IList<object> values, IList<ModelType> types)
			: this(className, alias, idXML, null as FieldList)
		{
			if((types != null) && (types.Count > 0))
			{
				CreateTypes(types);
				if((values != null) && (values.Count > 0))
				{
					SetValues(values);
				}
			}
		}
		#endregion Constructors

		#region Operators
		public override bool Equals(object obj)
		{
			// One of them is null.
			Oid lOid = obj as Oid;
			if (lOid == null || !lOid.IsValid())
			{
				return false;
			}

			if (string.Compare(ClassName, lOid.ClassName, true) == 0)
			{
				// Check Count.
				if (Fields.Count == lOid.Fields.Count)
				{
					// Check Values.
					for (int i = 0; i < Fields.Count; i++)
					{
						IOidField lOidField = Fields[i];

						if (lOidField.Value == null)
						{
							return false;
						}

						if ((lOidField.Type == ModelType.String) || (lOidField.Type == ModelType.Text))
						{
							// String comparation (No Case Sensitive).
							if (!string.Equals(lOidField.Value.ToString(), lOid.Fields[i].Value.ToString(), StringComparison.OrdinalIgnoreCase))
							{
								return false;
							}
						}
						else
						{
							// Object comparation.
							if (!lOidField.Value.Equals(lOid.Fields[i].Value))
							{
								return false;
							}
						}
					}
					return true;
				}
			}
			return false;
		}
		public override int GetHashCode()
		{
			int lHashCode = ClassName.GetHashCode();

			foreach (IOidField loidField in Fields)
			{
				lHashCode += loidField.Value.GetHashCode();
			}

			return base.GetHashCode();
		}
		public static bool operator ==(Oid oid1, Oid oid2)
		{
			// Both of them are the same reference or null
			if ((object) oid1 == (object) oid2)
			{
				return true;
			}

			// One of them is null
			if (((object) oid1 == null) || ((object) oid2 == null))
			{
				return false;
			}

			// Check ClassName
			return oid1.Equals(oid2);
		}
		public static bool operator !=(Oid oid1, Oid oid2)
		{
			return (!(oid1 == oid2));
		}
		#endregion Operators

		#region Methods
		#region Static methods
		/// <summary>
		/// Checks whether an Oid object is not null and is a valid Oid.
		/// </summary>
		/// <param name="oid">Oid object.</param>
		/// <returns>Returns true if the Oid is not null and valid. Otherwise, returns false.</returns>
		public static bool IsNotNullAndValid(Oid oid)
		{
			return ((oid != null) && oid.IsValid());
		}
		/// <summary>
		/// Create Type for especific OID.
		/// </summary>
		/// <param name="className">Name of Oid.</param>
		/// <returns>Type</returns>
		public static Type GetOidType(string className)
		{
			// Specific Oid Type
			Type lType = null;
			try
			{
				// Get Type
				lType = Type.GetType(typeof(Oid).Namespace + "." + className + "Oid", true, true);
			}
			catch
			{}
			return lType;
		}
		/// <summary>
		/// Returns the specific Oid passed as a parameter only if
		/// it is a valid Oid; Otherwise, returns null.
		/// </summary>
		/// <param name="oid">Oid object.</param>
		/// <returns>Specific Oid object or null.</returns>
		public static T CreateSecure<T>(Oid oid) where T : Oid
		{
			if (IsNotNullAndValid(oid))
			{
				return (T)oid;
			}
			return null;
		}
		/// <summary>
		/// Create Oid object specific. If className Oid object don't exist, return GenericOid object.
		/// </summary>
		/// <param name="className">Class Name.</param>
		/// <returns>Oid Object.</returns>
		private static Oid Create(string className, bool specific)
		{
			Oid lResult = null;
			Type lType = GetOidType(className);
			if (lType != null)
			{
				lResult = Activator.CreateInstance(lType) as Oid;
			}
			else
			{
				if (!specific)
				{
					lResult = new GenericOID(className);
				}
			}
			return lResult;
		}
		/// <summary>
		/// Creates an Oid object with the class name.
		/// </summary>
		/// <param name="className">Class name.</param>
		/// <returns>Oid object.</returns>
		public static Oid Create(string className)
		{
			return Create(className, true);
		}
		/// <summary>
		/// Create specific OId and set values.
		/// </summary>
		/// <param name="className">Name of domain for OID</param>
		/// <param name="values"></param>
		/// <returns>Specific OID.</returns>
		public static Oid Create(string className, IList<object> values)
		{
			// Specific Oid.
			Oid lOid = Create(className);
			if (lOid != null)
			{
				// Set values.
				int length = (values.Count > lOid.Fields.Count ? lOid.Fields.Count : values.Count);
				for (int lint = 0; lint < length; lint++)
				{
					lOid.Fields[lint].Value = values[lint];
				}
			}
			return lOid;
		}
		/// <summary>
		/// Create specific or generic Oid.
		/// </summary>
		/// <param name="className"></param>
		/// <param name="values"></param>
		/// <param name="types"></param>
		/// <returns></returns>
		public static Oid Create(string className, IList<object> values, IList<ModelType> types)
		{
			// Specific Oid.
			Oid lOid = Create(className);
			if (lOid != null)
			{
				if ((types != null) && (types.Count > 0))
				{
					ModelType[] ltypes = new ModelType[types.Count];
					types.CopyTo(ltypes, 0);
					lOid.CreateTypes(ltypes);

					if ((values != null) && (values.Count > 0))
					{
						object[] lvalues = new object[values.Count];
						values.CopyTo(lvalues, 0);
						lOid.SetValues(lvalues);
					}
				}
			}
			else
			{
				// Generic Oid.
				lOid = new GenericOID(className, values, types);
			}
			return lOid;
		}
		/// <summary>
		/// Create specific or generic Oid.
		/// </summary>
		/// <param name="className"></param>
		/// <param name="fields"></param>
		/// <returns></returns>
		public static Oid Create(string className, IList<KeyValuePair<ModelType, object>> fields)
		{
			// Specific Oid.
			Oid lOid = Create(className);

			// Generic Oid.
			if (lOid == null)
			{
				lOid = new GenericOID(className);
			}

			if (lOid != null)
			{
				if ((fields != null) && (fields.Count > 0))
				{
					lOid.Fields.Clear();
					foreach (KeyValuePair<ModelType, object> lField in fields)
					{
						IOidField loidfield = FieldList.CreateField(string.Empty, lField.Key);
						loidfield.Value = lField.Value;
						lOid.Fields.Add(loidfield);
					}
				}
			}
			return lOid;
		}

		/// <summary>
		/// Copy the received Oid list
		/// </summary>
		/// <param name="list2BeCopied"></param>
		/// <returns></returns>
        public static List<Oid> CopyOidList(List<Oid> list2BeCopied)
        {
            if (list2BeCopied == null)
                return null;

            List<Oid> lReturnList = new List<Oid>();
            foreach (Oid oid in list2BeCopied)
            {
                Oid lOid = Oid.Create(oid.ClassName, oid.GetFields());
                lOid.Alias = oid.Alias;
                lOid.AlternateKeyName = oid.AlternateKeyName;
                lOid.IdXML = oid.IdXML;
                lReturnList.Add(lOid);
            }

            return lReturnList;
        }
		#endregion Static methods

		#region Non-Static methods
		/// <summary>
		/// Clear values.
		/// </summary>
		public virtual void ClearValues()
		{
			ClearValues(null);
		}
		/// <summary>
		/// Clear values.
		/// </summary>
		/// <param name="defaultValue">Default value.</param>
		public virtual void ClearValues(object defaultValue)
		{
			foreach (IOidField loidField in Fields)
			{
				loidField.Value = defaultValue;
			}
		}
		/// <summary>
		/// Checks whether the Oid object is a valid Oid.
		/// </summary>
		/// <returns>Returns true if the Oid is valid; Otherwise, returns false.</returns>
		public virtual bool IsValid()
		{
			// Valid oid means:
			// 1. The Oid must have a class name,
			// 2. All the oid fields must be different to null,
			// 3. And the model type of the field (ModelType) must be equal to the field type (.NET type).
			if (!string.IsNullOrEmpty(ClassName) && Fields != null && Fields.Count > 0)
			{
				for (int i = 0; i < Fields.Count; i++)
				{
					// Get the field value and .NET type.
					object lFieldValue = Fields[i].Value;
					Type lFieldType = Adaptor.DataFormats.Convert.MODELTypeToNetType(Fields[i].Type);
					if (Type.Equals(lFieldType, typeof(string)))
					{
						// Remove white space characters. Null value is considered when the
						// string length is equals zero.
						string lFieldStringValue = lFieldValue as string;
						if ((lFieldStringValue == null) || (lFieldStringValue != null && lFieldStringValue.Trim().Length == 0))
						{
							lFieldValue = null;
						}
					}

					// Check null value and type.
					if ((lFieldValue == null) || ((lFieldValue != null) && !(Type.Equals(lFieldValue.GetType(), lFieldType))))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}
		/// <summary>
		/// Create Fields for OID.
		/// </summary>
		/// <param name="types">Oid types.</param>
		protected virtual void CreateTypes(ModelType[] types)
		{
			// Set Types
			if (types != null)
			{
				// Clear fields for this OID.
				FieldList.Clear();
				// Add New OID Types and fields for OID.
				foreach (ModelType ltype in types)
				{
					IOidField lfield = FieldList.Add(string.Empty, ltype);
				}
			}
		}
		/// <summary>
		/// Create Types for OID.
		/// </summary>
		/// <param name="types">Oid types.</param>
		protected virtual void CreateTypes(IList<ModelType> types)
		{
			ModelType[] ltypes = new ModelType[types.Count];
			types.CopyTo(ltypes, 0);
			CreateTypes(ltypes);
		}
		/// <summary>
		/// Sets values for the Oid fields.
		/// </summary>
		/// <param name="values">Values.</param>
		public virtual void SetValues(object[] values)
		{
			// Set Values.
			if (values != null)
			{
				for( int li = 0; ((li < this.Fields.Count) && (li < values.Length)); li++)
				{
					this.Fields[li].Value = values[li];
				}
			}
		}
		/// <summary>
		/// Sets values for the Oid fields.
		/// </summary>
		/// <param name="values">Values.</param>
		public virtual void SetValues(IList<object> values)
		{
			object[] lvalues = new object[values.Count];
			values.CopyTo(lvalues, 0);
			SetValues(lvalues);
		}
		/// <summary>
		/// Set the value of an Oid field.
		/// </summary>
		/// <param name="index">Field index.</param>
		/// <param name="value">Field value.</param>
		public virtual void SetValue(int index, object value)
		{
			if (index < this.Fields.Count)
			{
				this.Fields[index].Value = value;
			}
		}
		/// <summary>
		/// Gets the values of an Oid object.
		/// </summary>
		/// <returns>Oid field values.</returns>
		public virtual object[] GetValues()
		{
			object[] lValues = new object[Fields.Count];
			int litem = 0;
			foreach (IOidField loidField in Fields)
			{
				lValues[litem++] = loidField.Value;
			}
			return lValues;
		}
		/// <summary>
		/// Gets the fields of an Oid object.
		/// </summary>
		/// <returns>Oid fields.</returns>
		public virtual IList<KeyValuePair<ModelType, object>> GetFields()
		{
			List<KeyValuePair<ModelType, object>> lResult = new List<KeyValuePair<ModelType, object>>();
			foreach (IOidField loidField in Fields)
			{
				lResult.Add(new KeyValuePair<ModelType, object>(loidField.Type, loidField.Value));
			}
			return lResult;
		}
		
		/// <summary>
		/// Gets a reference to the 'PrimaryKey' (primary Oid).
		/// </summary>
		/// <returns>Primary Oid reference.</returns>
		public virtual IOid GetOid()
		{
			return (Oid)this;
		}

		/// <summary>
		/// Gets a reference to the 'AlternateKey' (alternate Oid).
		/// </summary>
		/// <param name="alternateKeyName">AlternateKey name.</param>
		/// <returns>Alternate Oid reference.</returns>
		public virtual IOid GetAlternateKey(string alternateKeyName)
		{
			// Only the classes that have alternate key have to override this function.
			return null;
		}
		#endregion Non-Static methods
		#endregion Methods
	}
	#endregion Oid [class]

	#region FieldList [Class]
	/// <summary>
	/// Editor presentations for attributes.
	/// </summary>
	[Serializable]
	public class FieldList :
		KeyedCollection<string, IOidField>
	{
		#region KeyForItem
		protected override string GetKeyForItem(IOidField item)
		{
			if((item.Name ==  null) || (item.Name.Length == 0))
			{
				item.Name = "Field" + this.Count.ToString();

				while (this.Contains(item.Name))
				{
					item.Name += "_";
				}
			}

			return item.Name;
		}
		#endregion KeyForItem

		#region Add Oid Field
		public IOidField Add(string name, ModelType type)
		{
			IOidField loidField = CreateField(name, type);
			Add(loidField);
			return loidField;
		}
		public IOidField Add(string name, ModelType type, object value)
		{
			IOidField loidField = Add(name, type);
			loidField.Value = value;
			return loidField;
		}
		public IOidField Add(string name, ModelType type, object value, int maxLength)
		{
			IOidField loidField = Add(name, type, value);
			loidField.MaxLength = maxLength;
			return loidField;
		}
		#endregion Add Oid Field

		#region Create Oid Field
		public static IOidField CreateField(string name, ModelType type)
		{
			return new OidField(name, type);
		}
		public static IOidField CreateField(string name, ModelType type, int maxLength)
		{
			return new OidField(name, type, maxLength);
		}
		#endregion Create Oid Field

		#region OidField [Class]
		[Serializable]
		private class OidField :
			IOidField
		{
			#region Members
			private ModelType mType;
			private object mValue;
			private string mName;
			private int mMaxLength = 0;
			#endregion Members

			public OidField(string name, ModelType type)
			{
				Name = name;
				Type = type;
			}
			public OidField(string name, ModelType type, int maxLength)
			{
				Name = name;
				Type = type;
				MaxLength= maxLength;
			}

			#region IOidField

			#region Type
			public ModelType Type
			{
				get
				{
					return mType;
				}
				protected set
				{
					mType = value;
				}
			}
			#endregion Type

			#region Value
			public object Value
			{
				get { return mValue; }
				set { mValue = value; }
			}
			#endregion Value

			#region Name
			public string Name
			{
				get { return mName; }
				set { mName = value; }
			}
			#endregion Name

			#region MaxLength
			public int MaxLength
			{
				get { return mMaxLength; }
				set { mMaxLength = value; }
			}
			#endregion MaxLength

			#endregion IOidField
		}
		#endregion OidField
	}
	#endregion FieldList
}

