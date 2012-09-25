// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

using SIGEM.Client.Oids;

namespace SIGEM.Client.Adaptor
{
	#region ArgumentsList
	/// <summary>
	/// Stores and manages arguments in an ordered list .
	/// </summary>
	public partial class ArgumentsList :
		KeyedCollection<string, ArgumentInfo>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new empty instance of ArgumentsList.
		/// </summary>
		public ArgumentsList()
			: base(StringComparer.CurrentCultureIgnoreCase) { }
		/// <summary>
		/// Initializes a new instance of ArgumentsList. Types and Values are indicated as ordered lists.
		/// </summary>
		/// <param name="types">Types of arguments.</param>
		/// <param name="values">Values of arguments.</param>
		public ArgumentsList(
			Dictionary<string, ModelType> types,
			Dictionary<string, object> values,
			Dictionary<string, string> domains)
		{
			foreach(string lname in values.Keys)
			{
				this.Add(lname, types[lname], values[lname], domains[lname]);
			}
		}
		#endregion Constructors

		#region Methods
		/// <summary>
		/// Obtains the key of an argument in the list.
		/// </summary>
		/// <param name="item">Argument the key is obtained.</param>
		/// <returns>Argument key in the list.</returns>
		protected override string GetKeyForItem(ArgumentInfo item)
		{
			return item.Name;
		}
		/// <summary>
		/// Adds a new argument into the list. The type of the argument is given as ModelType.
		/// </summary>
		/// <param name="name">Name of the argument.</param>
		/// <param name="type">Type of the argument(ModelType).</param>
		/// <param name="value">Value of the argument.</param>
		/// <returns>Returns the ArgumentInfo added.</returns>
		public virtual ArgumentInfo Add(string name, ModelType type, object value, string domain)
		{
            if (this.Contains(name))
            {
                return this[name];
            }
			ArgumentInfo lResult = new ArgumentInfo(name, type, value, domain);
			this.Add(lResult);
			return lResult;
		}
		/// <summary>
		/// Adds a new argument into the list. The type of the argument is given as string.
		/// </summary>
		/// <param name="name">Name of the argument.</param>
		/// <param name="type">Type of the argument(string).</param>
		/// <param name="value">Value of the argument.</param>
		/// <param name="domain">Domain of the argument.</param>
		/// <returns>Returns the ArgumentInfo added.</returns>
		public virtual ArgumentInfo Add(string name, string type, object value, string domain)
		{
            if (this.Contains(name))
            {
                return this[name];
            }
			ArgumentInfo lResult = new ArgumentInfo(name, type, value, domain);
			this.Add(lResult);
			return lResult;
		}
		#endregion Methods
	}
	#endregion ArgumentsList

	#region ArgumentInfo
	/// <summary>
	/// Stores the information related to an argument.
	/// </summary>
	public class ArgumentInfo
	{
		#region Members
		private string mName = string.Empty;
		private ModelType mType = ModelType.String;
		private string mClassName = string.Empty;
		private object mValue = null;
		#endregion Members

		#region Properties
		/// <summary>
		/// Name of the argument.
		/// </summary>
		public virtual string Name
		{
			get
			{
				return mName;
			}
			protected set
			{
				mName = value;
			}
		}
		/// <summary>
		/// Type of the argument.
		/// </summary>
		public virtual ModelType Type
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
		/// <summary>
		/// Class name of the object valued argument.
		/// </summary>
		public virtual string ClassName
		{
			get
			{
				return mClassName;
			}
			protected set
			{
				mClassName = value;
			}
		}
		/// <summary>
		/// Value of the argument.
		/// </summary>
		public virtual object Value
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
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Creates a new instance of ArgumentInfo. Type is indicated as ModelType. Value is not set.
		/// </summary>
		/// <param name="name">Name of the argument.</param>
		/// <param name="type">Type of the argument.</param>
		public ArgumentInfo(string name, ModelType type, string className)
			: this(name, type, null, className) { }
		/// <summary>
		/// Creates a new instance of ArgumentInfo. Type is indicated as string. Value is not set.
		/// </summary>
		/// <param name="name">Name of the argument.</param>
		/// <param name="type">Type of the argument.</param>
		public ArgumentInfo(string name, string type, string className)
			: this(name, type, null, className) { }
		/// <summary>
		/// Creates a new instance of ArgumentInfo. Type is indicated as string.
		/// </summary>
		/// <param name="name">Name of the argument.</param>
		/// <param name="type">Type of the argument.</param>
		/// <param name="value">Value of the argument.</param>
		public ArgumentInfo(string name, string type, object value, string className)
			: this(name, ModelType.String, value, className)
		{
			Type = Adaptor.DataFormats.Convert.StringTypeToMODELType(type);
		}
		/// <summary>
		/// Creates a new instance of ArgumentInfo. Type is indicated as ModelType.
		/// </summary>
		/// <param name="name">Name of the argument.</param>
		/// <param name="type">Type of the argument.</param>
		/// <param name="value">Value of the argument.</param>
		public ArgumentInfo(string name, ModelType type, object value, string className)
		{
			Name = name;
			Type = type;
			Value = value;
			ClassName = className;
		}
		#endregion Constructors
	}
	#endregion ArgumentInfo
}

