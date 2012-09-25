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


namespace SIGEM.Client.Adaptor.Exceptions
{
	using SIGEM.Client.Adaptor.DataFormats;

	#region ChangedItems
	/// <summary>
	/// Stores information of a set of change detection Items.
	/// </summary>
	public class ChangedItems : 
		Dictionary<string, ChangedItem>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new empty instance of ChangedItems.
		/// </summary>
		public ChangedItems()
			:base(StringComparer.CurrentCultureIgnoreCase){}

		/// <summary>
		/// Initializes a new instance of ChangedItems.
		/// </summary>
		/// <param name="ChangedItems">ChangedItems to be set.</param>
		public ChangedItems(ChangedItems items)
			: base(items) { }
		#endregion Constructors

		#region GetValues
		/// <summary>
		/// Obtains the ChangedItems stored in the attribute Values.
		/// </summary>
		/// <returns>Dictionary<string,object></returns>
		public Dictionary<string,object> GetOldValues()
		{
			Dictionary<string, object> lResult = new Dictionary<string, object>();
			foreach (ChangedItem i in this.Values)
			{
				lResult.Add(i.Name, i.OldValue);
			}
			return lResult;
		}
		public Dictionary<string, object> GetNewValues()
		{
			Dictionary<string, object> lResult = new Dictionary<string, object>();
			foreach (ChangedItem i in this.Values)
			{
				lResult.Add(i.Name, i.NewValue);
			}
			return lResult;
		}
		#endregion GetValues

		/// <summary>
		/// Adds a new ChangedItem.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		/// <param name="domain">Domain.</param>
		/// <returns>ChangedItem.</returns>
		public ChangedItem Add(string name, ModelType type, object oldvalue, object newvalue, string domain)
		{
			ChangedItem lResult = Create(name, type, oldvalue, newvalue, domain);
			this.Add(lResult.Name,lResult);
			return lResult;
		}
		/// <summary>
		/// Adds a new change detection item.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		/// <returns>ChangedItem.</returns>
		public ChangedItem Add(string name, string type, object oldvalue, object newvalue, string domain)
		{
			ChangedItem lResult = Create(name, type, oldvalue, newvalue, domain);
			this.Add(lResult.Name,lResult);
			return lResult;
		}
		/// <summary>
		/// Adds a new change detection item.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <returns>ChangedItem.</returns>
		public ChangedItem Add(ChangedItem value)
		{
			if (value != null)
			{
				this.Add(value.Name, value);
			}
			else
			{
				throw new ArgumentNullException("ChangedItem value");
			}
			return value;
		}


		#region Create a new instance of ChangedItem
		/// <summary>
		/// Creates a new instance of ChangedItem.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		/// <returns>ChangedItem.</returns>
		public virtual ChangedItem Create(string name, string type, object oldvalue, object newvalue, string domain)
		{
			return new ChangedItem(name, type, oldvalue, newvalue, domain);
		}
		#region Overload methods
		public virtual ChangedItem Create(string name, ModelType type, object oldvalue, object newvalue, string domain)
		{
			return Create(name, type.ToString(), oldvalue, newvalue, domain);
		}
		public virtual ChangedItem Create(string name, string type, object oldvalue, object newvalue)
		{
			return Create(name, type, oldvalue, newvalue, string.Empty);
		}
		public virtual ChangedItem Create(string name, ModelType type, object oldvalue, object newvalue)
		{
			return Create(name, type.ToString(), oldvalue, newvalue, string.Empty);
		}
		#endregion Overload methods 
		#endregion Create a new instance of ChangedItem

		/// <summary>
		/// Redefines the property 'this'.
		/// </summary>
		/// <param name="index">Index.</param>
		/// <returns>ChangedItem.</returns>
		public virtual ChangedItem this[int index]
		{
			get
			{
				ChangedItem lResult = null;
				if(index < this.Count)
				{
					ChangedItem[] Values = null;
					this.Values.CopyTo(Values, index);
					lResult = Values[0];
				}
				return lResult;
			}
		}
	}
	#endregion ChangedItems



	#region Changed Item
	/// <summary>
	/// 
	/// </summary>
	public class ChangedItem
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of ChangedItem.
		/// </summary>
		public ChangedItem()
			: this(string.Empty, string.Empty, null, null, string.Empty) { }

		public ChangedItem(string name, string type)
			: this(name, type, null, null, string.Empty) { }

		/// <summary>
		/// Initializes a new instance of ChangedItem.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		public ChangedItem(string name, ModelType type, object oldvalue, object newvalue)
			: this(name, type.ToString(), oldvalue, newvalue, string.Empty) { }

		/// <summary>
		/// Initializes a new instance of ChangedItem.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		public ChangedItem(string name, string type, object oldvalue, object newvalue, string domain)
		{
			this.Name = name;
			this.Type = type;
			this.NewValue = newvalue;
			this.OldValue = oldvalue;
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
		private object mOldValue = null;
		/// <summary>
		/// Gets or sets Value.
		/// </summary>
		public object OldValue
		{
			get
			{
				return mOldValue;
			}
			set
			{
				mOldValue = value;
			}
		}
		#endregion Value

		#region Value
		private object mnewValue = null;
		/// <summary>
		/// Gets or sets Value.
		/// </summary>
		public object NewValue
		{
			get
			{
				return mnewValue;
			}
			set
			{
				mnewValue = value;
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
	#endregion Changed Item
}


