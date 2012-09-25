// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Xml;
using SIGEM.Client;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the DisplaySet items.
	/// </summary>
	public class DisplaySetItem: Object
	{
		#region Members
		/// <summary>
		/// Name of the DisplaySet item.
		/// </summary>
		private string mName;
		/// <summary>
		/// Alias of the DisplaySet item.
		/// </summary>
		private string mAlias;
		/// <summary>
		/// XML DisplaySet item identifier.
		/// </summary>
		private string mIdXML;
		/// <summary>
		/// ModelType of the DisplaySet item.
		/// </summary>
		private ModelType mModelType;
		/// <summary>
		/// If the DisplaySet item allows null values.
		/// </summary>
		private bool mNullAllowed;
		/// <summary>
		/// List of defined selection options.
		/// </summary>
		private List<KeyValuePair<object, string>> mDefinedSelectionOptions;
		/// <summary>
		/// List of agents of the DisplaySet item.
		/// </summary>
		private List<string> mAgents;
		/// <summary>
		/// Width column of the DisplaySet item.
		/// </summary>
		private int mWidth = 0;
		/// <summary>
		/// User Preferences. Indicates if the Item is visible or not.
		/// </summary>
		private bool mVisible = true;
		/// <summary>
		/// Indicates if the Item is editable or not, due to the DisplaySet associated service
		/// </summary>
		private bool mEditable = false;
		/// <summary>
		/// Definition Class for object-valued elements.
		/// </summary>
		private string mClassName = "";
		/// <summary>
		/// OID fields list.
		/// </summary>
		private List<string> mOIDFields;
		/// <summary>
		/// Supplementary information to be shown instead of OID field values.
		/// </summary>
		private DisplaySetInformation mSupplementaryInfo;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets the DisplaySet item name.
		/// </summary>
		public string Name
		{
			get
			{
				return mName;
			}
			set
			{
				mName = value;
			}
		}
		/// <summary>
		/// Gets the DisplaySet item alias.
		/// </summary>
		public string Alias
		{
			get
			{
				return mAlias;
			}
			set
			{
				mAlias = value;
			}
		}
		/// <summary>
		/// Gets the XML DisplaySet item identifier.
		/// </summary>
		public string IdXML
		{
			get
			{
				return mIdXML;
			}
		}
		/// <summary>
		/// Gets the DisplaySet item ModelType .
		/// </summary>
		public ModelType ModelType
		{
			get
			{
				return mModelType;
			}
			set
			{
				mModelType = value;
			}
		}
		/// <summary>
		/// Gets a boolean value indicating whether the DisplaySet item allows null values.
		/// </summary>
		public bool NullAllowed
		{
			get
			{
				return mNullAllowed;
			}
		}
		/// <summary>
		/// Gets the list of defined selection options.
		/// </summary>
		public List<KeyValuePair<object, string>> DefinedSelectionOptions
		{
			get
			{
				return mDefinedSelectionOptions;
			}
		}
		/// <summary>
		/// Gets or sets the list of agents that can acces to the DisplaySet item.
		/// </summary>
		public List<string> Agents
		{
			get
			{
				return mAgents;
			}
			set
			{
				mAgents = value;
			}
		}
		/// <summary>
		/// Gets or sets the width column value to keep of the DisplaySet item.
		/// </summary>
		public int Width
		{
			get
			{
				return mWidth;
			}
			set
			{
				mWidth = value;
			}
		}

		/// <summary>
		/// Indicates if the Item is visible or not
		/// </summary>
		public bool Visible
		{
			get
			{
				return mVisible;
			}
			set
			{
				mVisible = value;
			}
		}
		/// <summary>
		/// Indicates if the Item is editable or not, due to the DisplaySet associated service
		/// </summary>
		public bool Editable
		{
			get
			{
				return mEditable;
			}
			set
			{
				mEditable = value;
			}
		}
		/// <summary>
		/// Definition Class for object-valued elements.
		/// </summary>
		public string ClassName
		{
			get
			{
				return mClassName;
			}
			set
			{
				mClassName = value;
			}
		}
		/// <summary>
		/// OID fields list.
		/// </summary>
		public List<string> OIDFields
		{
			get
			{
				return mOIDFields;
			}
			set
			{
				mOIDFields = value;
			}
		}
		/// <summary>
		/// Supplementary information to be shown instead of OID field values.
		/// </summary>
		public DisplaySetInformation SupplementaryInfo
		{
			get
			{
				return mSupplementaryInfo;
			}
			set
			{
				mSupplementaryInfo = value;
			}
		}
		#endregion Properties

		#region Constructors
		// Default constructor
		public DisplaySetItem()
		{
		}
		/// <summary>
		/// Initializes a new instance of the 'DisplaySetItem' class.
		/// </summary>
		/// <param name="name">Name of the DisplaySet item.</param>
		/// <param name="alias">Alias of the DisplaySet item.</param>
		/// <param name="idXML">IdXML of the DisplaySet item.</param>
		/// <param name="modelType">ModelType of the DisplaySet item.</param>
		/// <param name="agents">List of agents of the DisplaySet item.</param>
		/// <param name="width">Width column of the DisplaySet item.</param>
		/// <param name="visible">True if the item is visible.</param>
		public DisplaySetItem(string name, string alias, string idXML, ModelType modelType, List<string> agents, int width, bool visible)
		{
			mName = name;
			mAlias = alias;
			mIdXML = idXML;
			mModelType = modelType;
			mAgents = agents;
			mWidth = width;
			mVisible = visible;
		}
		/// <summary>
		/// Initializes a new instance of the 'DisplaySetItem' class.
		/// </summary>
		/// <param name="name">Name of the DisplaySet item.</param>
		/// <param name="alias">Alias of the DisplaySet item.</param>
		/// <param name="idXML">IdXML of the DisplaySet item.</param>
		/// <param name="modelType">ModelType of the DisplaySet item.</param>
		/// <param name="nullAllowed">If the DisplaySet item allows null values.</param>
		/// <param name="definedSelectionOptions">List of defined selection options.</param>
		/// <param name="agents">List of agents of the DisplaySet item.</param>
		/// <param name="width">Width column of the DisplaySet item.</param>
		/// <param name="visible">True if the item is visible.</param>
		public DisplaySetItem(string name, string alias, string idXML, ModelType modelType, bool nullAllowed, List<KeyValuePair<object, string>> definedSelectionOptions, List<string> agents, int width, bool visible)
		{
			mName = name;
			mAlias = alias;
			mIdXML = idXML;
			mModelType = modelType;
			mNullAllowed = nullAllowed;
			if (mNullAllowed)
			{
				KeyValuePair<object, string> lNullKeyValue = new KeyValuePair<object, string>(System.DBNull.Value , " ");
				if (!definedSelectionOptions.Contains(lNullKeyValue))
				{
					definedSelectionOptions.Insert(0, lNullKeyValue);
				}
			}
			mDefinedSelectionOptions = definedSelectionOptions;
			mAgents = agents;
			mWidth = width;
			mVisible = visible;
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="item">Item to be copied</param>
		public DisplaySetItem(DisplaySetItem item)
		{
			mName = item.Name;
			mAlias = item.Alias;
			mIdXML = item.IdXML;
			mModelType = item.ModelType;
			mNullAllowed = item.NullAllowed;
			mDefinedSelectionOptions = item.DefinedSelectionOptions;
			mAgents = item.Agents;
			mVisible = item.Visible;
			mWidth = item.Width;
			mClassName = item.ClassName;
			mOIDFields = item.OIDFields;
			mSupplementaryInfo = item.SupplementaryInfo;
		}
		/// <summary>
		/// Constructor for object-valued elements
		/// </summary>
		/// <param name="name">Name of the DisplaySet item.</param>
		/// <param name="alias">Alias of the DisplaySet item.</param>
		/// <param name="idXML">IdXML of the DisplaySet item.</param>
		/// <param name="className">Definition class.</param>
		/// <param name="agents">List of agents of the DisplaySet item.</param>
		/// <param name="width">Width column of the DisplaySet item.</param>
		/// <param name="visible">True if the item is visible.</param>
		public DisplaySetItem(string name, string alias, string className, string idXML, List<string> agents, int width, bool visible, string[] oidFields, DisplaySetInformation supplementaryInfo)
		{
			mName = name;
			mAlias = alias;
			mClassName = className;
			mIdXML = idXML;
			mModelType = ModelType.Oid;
			mAgents = agents;
			mWidth = width;
			mVisible = visible;
			mOIDFields = new List<string>(oidFields);
			mSupplementaryInfo = supplementaryInfo;
		}
		#endregion Constructors

		#region Methods

		#region Serialize-Deserialize
		/// <summary>
		/// Serialize the information in XML format
		/// </summary>
		/// <param name="writer"></param>
		public void Serialize(XmlWriter writer)
		{
			writer.WriteStartElement("DisplaySetItem");

			writer.WriteAttributeString("Name", Name);
			writer.WriteAttributeString("Alias", Alias);
			writer.WriteAttributeString("DataType", ModelType.ToString());
			writer.WriteAttributeString("Width", Width.ToString());
			if (Visible)
				writer.WriteAttributeString("Visible", "1");
			else
				writer.WriteAttributeString("Visible", "0");
            // Defined selection
            if (DefinedSelectionOptions != null && DefinedSelectionOptions.Count > 0)
            {
                SerializeDefinedSelection(writer);
            }

			writer.WriteEndElement();
		}
        /// <summary>
        /// Serialize to XML the Defined Selection values for this display set item
        /// </summary>
        /// <param name="writer"></param>
        private void SerializeDefinedSelection(XmlWriter writer)
        {
            // DSValues element
            writer.WriteStartElement("DSValues");

            foreach (KeyValuePair<object, string> pair in DefinedSelectionOptions)
            {
                writer.WriteStartElement("DSValue");
                string key = "";
                if (!pair.Key.Equals(System.DBNull.Value))
                {
                    key = Adaptor.DataFormats.Convert.TypeToXml(ModelType, pair.Key);
                }
                writer.WriteAttributeString("Value", key);
                writer.WriteAttributeString("Label", pair.Value);
                writer.WriteEndElement();
            }

            // DSValues element
            writer.WriteEndElement();
        }
        /// <summary>
        /// Deserialize the information from XML node.
        /// </summary>
        /// <param name="itemNode"></param>
        /// <param name="version"></param>
        public void Deserialize(XmlNode itemNode, string version)
        {
            Name = itemNode.Attributes["Name"].Value;
            Alias = itemNode.Attributes["Alias"].Value;
            ModelType = Adaptor.DataFormats.Convert.StringTypeToMODELType(itemNode.Attributes["DataType"].Value);
            Width = int.Parse(itemNode.Attributes["Width"].Value);
            string aux = itemNode.Attributes["Visible"].Value;
            if (aux != "0")
                Visible = true;
            else
                Visible = false;

            // Get Defined selection values
            if (itemNode.ChildNodes.Count == 1 && itemNode.ChildNodes[0].Name.Equals("DSValues"))
            {

                XmlNodeList lDSNodes = itemNode.ChildNodes[0].SelectNodes("DSValue");
                if (lDSNodes.Count > 0)
                {
                    KeyValuePair<object, string> pair;
                    string key;
                    string label;
                    object objectKey;
                    if (DefinedSelectionOptions == null)
                        mDefinedSelectionOptions = new List<KeyValuePair<object, string>>();

                    foreach (XmlNode lDSNode in lDSNodes)
                    {
                        key = lDSNode.Attributes["Value"].Value;
                        label = lDSNode.Attributes["Label"].Value;
                        if (key.Equals(""))
                        {
                            objectKey = System.DBNull.Value;
                        }
                        else
                        {
                            objectKey = Adaptor.DataFormats.Convert.XmlToType(ModelType, key);
                        }
                        pair = new KeyValuePair<object, string>(objectKey, label);
                        DefinedSelectionOptions.Add(pair);
                    }
                }
            }
            // Set as Agents the connected one
            Agents = new List<string>();
            Agents.Add(Logics.Logic.Agent.ClassName);
        }
		#endregion Serialize-Deserialize

		#endregion Methods
	}
}
