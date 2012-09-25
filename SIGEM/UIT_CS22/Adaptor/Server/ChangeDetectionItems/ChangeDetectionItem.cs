// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Data;
using System.Data.Common;


namespace SIGEM.Client.Adaptor
{
	using SIGEM.Client.Adaptor.DataFormats;

	#region ChangeDectectionItems
	/// <summary>
	/// Stores information of a set of change detection Items.
	/// </summary>
	public class ChangeDetectionItems : 
		Dictionary<string, ChangeDetectionItem>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new empty instance of ChangeDectectionItems.
		/// </summary>
		public ChangeDetectionItems()
			:base(StringComparer.CurrentCultureIgnoreCase){}

		/// <summary>
		/// Initializes a new instance of ChangeDectectionItems.
		/// </summary>
		/// <param name="ChangeDectectionItems">ChangeDectectionItems to be set.</param>
		public ChangeDetectionItems(ChangeDetectionItems items)
			: base(items) { }

		public ChangeDetectionItems(Dictionary<string, ModelType> types, Dictionary<string, object> values, Dictionary<string, string> domains)
		{
			foreach(string i in values.Keys)
			{
				if(domains.ContainsKey(i))
				{
					
					this.Add( this.Create(i,types[i],values[i], domains[i]) );
				}
				else
				{
					this.Add(this.Create(i, types[i], values[i], string.Empty));
				}
			}
		}
		#endregion Constructors

		#region GetValues
		/// <summary>
		/// Obtains the ChangeDetectionItems stored in the attribute Values.
		/// </summary>
		/// <returns>Dictionary<string,object></returns>
		public Dictionary<string,object> GetValues()
		{
			Dictionary<string, object> lResult = new Dictionary<string, object>();
			foreach (ChangeDetectionItem i in this.Values)
			{
				lResult.Add(i.Name, i.Value);
			}
			return lResult;
		}
		#endregion GetValues

		/// <summary>
		/// Adds a new ChangeDectectionItem.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		/// <param name="domain">Domain.</param>
		/// <returns>ChangeDectectionItem.</returns>
		public ChangeDetectionItem Add(string name, ModelType type, object value, string domain)
		{
			ChangeDetectionItem lResult = Create(name, type, value, domain);
			this.Add(lResult.Name,lResult);
			return lResult;
		}
		/// <summary>
		/// Adds a new change detection item.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		/// <returns>ChangeDetectionItem.</returns>
		public ChangeDetectionItem Add(string name, string type, object value, string domain)
		{
			ChangeDetectionItem lResult = Create(name, type, value, domain);
			this.Add(lResult.Name,lResult);
			return lResult;
		}
		/// <summary>
		/// Adds a new change detection item.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <returns>ChangeDetectionItem.</returns>
		public ChangeDetectionItem Add(ChangeDetectionItem value)
		{
			if (value != null)
			{
				this.Add(value.Name, value);
			}
			else
			{
				throw new ArgumentNullException("ChangeDetectionItem value");
			}
			return value;
		}


		#region Create a new instance of ChangeDectectionItem
		/// <summary>
		/// Creates a new instance of ChangeDectectionItem.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		/// <returns>ChangeDetectionItem.</returns>
		public virtual ChangeDetectionItem Create(string name, string type, object value, string domain)
		{
			return new ChangeDetectionItem(name, type, value, domain);
		}
		#region Overload methods
		public virtual ChangeDetectionItem Create(string name, ModelType type, object value, string domain)
		{
			return Create(name, type.ToString(), value, domain);
		}
		public virtual ChangeDetectionItem Create(string name, string type, object value)
		{
			return Create(name, type, value, string.Empty);
		}
		public virtual ChangeDetectionItem Create(string name, ModelType type, object value)
		{
			return Create(name, type.ToString(), value, string.Empty);
		}
		#endregion Overload methods 
		#endregion Create a new instance of ChangeDectectionItem

		/// <summary>
		/// Redefines the property 'this'.
		/// </summary>
		/// <param name="index">Index.</param>
		/// <returns>ChangeDectectionItem.</returns>
		public virtual ChangeDetectionItem this[int index]
		{
			get
			{
				ChangeDetectionItem lResult = null;
				if(index < this.Count)
				{
					ChangeDetectionItem[] Values = null;
					this.Values.CopyTo(Values, index);
					lResult = Values[0];
				}
				return lResult;
			}
		}
	}
	#endregion ChangeDectectionItems


	#region Change Detection Item
	/// <summary>
	/// 
	/// </summary>
	public class ChangeDetectionItem
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of ChangeDectectionItem.
		/// </summary>
		public ChangeDetectionItem()
			: this(string.Empty, string.Empty, null, string.Empty) { }

		public ChangeDetectionItem(string name, string type)
			: this(name, type, null,string.Empty) { }

		/// <summary>
		/// Initializes a new instance of ChangeDectectionItem.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		public ChangeDetectionItem(string name, ModelType type, object value)
			: this(name, type.ToString(), value,string.Empty) { }

		/// <summary>
		/// Initializes a new instance of ChangeDectectionItem.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		public ChangeDetectionItem(string name, string type, object value, string domain)
		{
			this.Name = name;
			this.Type = type;
			this.Value = value;
			this.ClassName = domain;
		}
		#endregion Constructors

		#region Model Type
		public string mType = string.Empty;
		public string Type
		{
			get { return mType; }
			set { mType = value; }
		}
		#endregion Model Type

		#region Name
		private string mName = string.Empty;
		public string Name
		{
			get { return mName; }
			set { mName = value; }
		}
		#endregion Name

		#region Value
		private object mValue = null;
		/// <summary>
		/// Gets or sets Value.
		/// </summary>
		public object Value
		{
			get
			{
				return mValue;
			}
			set
			{
				mValue = value;
			}
		}
		#endregion Value

		#region Class Name (OID domain)
		private string mClassName = string.Empty;
		/// <summary>
		/// Gets or sets the name of the class.
		/// </summary>
		/// <value>The name of the class.</value>
		public string ClassName
		{
			get { return mClassName; }
			set { mClassName = value; }
		} 
		#endregion Class Name (OID domain)

	}
	#endregion Change Detection Item
}


