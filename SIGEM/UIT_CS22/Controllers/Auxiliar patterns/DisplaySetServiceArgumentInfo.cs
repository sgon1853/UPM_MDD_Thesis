// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections.Specialized;

namespace SIGEM.Client.Controllers
{
	public class DisplaySetServiceArgumentInfo: Object
	{
		#region Members
		/// <summary>
		/// Argument name
		/// </summary>
		private string mName = string.Empty;
		/// <summary>
		/// DisplaySet Element name (attribute name)
		/// </summary>
		private string mDSElementName = string.Empty;
		/// <summary>
		/// Argument Alias
		/// </summary>
		private string mAlias = string.Empty;
		/// <summary>
		/// Model Data Type
		/// </summary>
		private ModelType mDataType;
		/// <summary>
		/// Indicates if the argument allows null value
		/// </summary>
		private bool mAllowsNull;
		#endregion Members

		#region Properties
		/// <summary>
		/// Argument name
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
		/// DisplaySet Element name (attribute name)
		/// </summary>
		public string DSElementName
		{
			get
			{
				return mDSElementName;
			}
			set
			{
				mDSElementName = value;
			}
		}
		/// <summary>
		/// Argument Alias
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
		/// Argumnet Model data type
		/// </summary>
		public ModelType DataType
		{
			get
			{
				return mDataType;
			}
			set
			{
				mDataType = value;
			}
		}
		/// <summary>
		/// Indicates if the argument allows null value
		/// </summary>
		public bool AllowsNull
		{
			get
			{
				return mAllowsNull;
			}
			set
			{
				mAllowsNull = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public DisplaySetServiceArgumentInfo()
		{
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dsElementName"></param>
		/// <param name="dataType"></param>
		/// <param name="alias"></param>
		/// <param name="allowsNull"></param>
		public DisplaySetServiceArgumentInfo(string name, string dsElementName, ModelType dataType, string alias, bool allowsNull)
		{
			mName = name;
			mDSElementName = dsElementName;
			mDataType = dataType;
			mAlias = alias;
			mAllowsNull = allowsNull;
		}
		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="argumentInfo"></param>
		public DisplaySetServiceArgumentInfo(DisplaySetServiceArgumentInfo argumentInfo)
		{
			Name = argumentInfo.Name;
			DSElementName = argumentInfo.DSElementName;
			DataType = argumentInfo.DataType;
			Alias = argumentInfo.Alias;
			AllowsNull = argumentInfo.AllowsNull;
		}

		#endregion Constructors

		#region Methods

		#region Serialize-Deserialize
		/// <summary>
		/// Serialize the information in XML format
		/// </summary>
		/// <param name="writer"></param>
		internal void Serialize(XmlWriter writer)
		{
			writer.WriteStartElement("Argument");
			writer.WriteAttributeString("Name", Name);
			writer.WriteAttributeString("Alias", Alias);
			writer.WriteAttributeString("DataType", DataType.ToString());
			string allowsNull = "0";
			if (AllowsNull)
			{
				allowsNull = "1";
			}
			writer.WriteAttributeString("AllowsNull", allowsNull);
			writer.WriteAttributeString("DSItem", DSElementName);

			writer.WriteEndElement();
		}
        /// <summary>
        /// Deserialize from XML node
        /// </summary>
        /// <param name="argumentNode"></param>
        /// <param name="version"></param>
		internal void Deserialize(XmlNode argumentNode, string version)
		{
            Name = argumentNode.Attributes["Name"].Value;
			Alias = argumentNode.Attributes["Alias"].Value;
			DataType = Adaptor.DataFormats.Convert.StringTypeToMODELType(argumentNode.Attributes["DataType"].Value);
			DSElementName = argumentNode.Attributes["DSItem"].Value;
			string lAllowsNull = "0";
            lAllowsNull = argumentNode.Attributes["AllowsNull"].Value;
			AllowsNull = false;
			if (lAllowsNull != "0")
			{
				AllowsNull = true;
			}
		}
		#endregion Serialize-Deserialize

		#endregion Methods

	}
}
