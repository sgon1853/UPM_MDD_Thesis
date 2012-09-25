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
	#region Arguments
	/// <summary>
	/// Stores information of a set of arguments.
	/// </summary>
	public class Arguments : Dictionary<string, Argument>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new empty instance of Arguments.
		/// </summary>
		public Arguments()
		:base(StringComparer.CurrentCultureIgnoreCase){}
		/// <summary>
		/// Initializes a new instance of Arguments.
		/// </summary>
		/// <param name="arguments">Arguments to be set.</param>
		public Arguments(Arguments arguments)
		:base(arguments){}
		/// <summary>
		/// Initializes a new instance of Arguments.
		/// </summary>
		/// <param name="types">Argument types.</param>
		/// <param name="values">Argument values.</param>
		public Arguments(Dictionary<string, ModelType> types, Dictionary<string, object> values, Dictionary<string, string> domains)
		{
			foreach(string i in values.Keys)
			{
				if(domains.ContainsKey(i))
				{
					this.Add(new Argument(i, types[i], values[i], domains[i]));
				}
				else
				{
					this.Add(new Argument(i, types[i], values[i], string.Empty));
				}
			}
		}
		#endregion Constructors

		#region GetValues
		/// <summary>
		/// Obtains the arguments stored in the attribute Values.
		/// </summary>
		/// <returns>Dictionary<string,object></returns>
		public Dictionary<string,object> GetValues()
		{
			Dictionary<string, object> lResult = new Dictionary<string, object>();
			foreach (Argument i in this.Values)
			{
				lResult.Add(i.Name, i.Value);
			}
			return lResult;
		}
		#endregion GetValues

		/// <summary>
		/// Adds a new argument.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		/// <param name="domain">Domain.</param>
		/// <returns>Argument.</returns>
		public Argument Add(string name, ModelType type, object value, string domain)
		{
			Argument lResult = Create(name, type, value, domain);
			this.Add(lResult.Name,lResult);
			return lResult;
		}
		/// <summary>
		/// Adds a new argument.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		/// <returns>Argument.</returns>
		public Argument Add(string name, string type, object value, string domain)
		{
			Argument lResult = Create(name, type, value, domain);
			this.Add(lResult.Name,lResult);
			return lResult;
		}
		/// <summary>
		/// Adds a new argument.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <returns>Argument.</returns>
		public Argument Add(Argument value)
		{
			if (value != null)
			{
				this.Add(value.Name, value);
			}
			else
			{
				throw new ArgumentNullException("Argument value");
			}
			return value;
		}
		/// <summary>
		/// Creates a new instance of Argument.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		/// <returns>Argument.</returns>
		public virtual Argument Create(string name, ModelType type, object value, string className)
		{
			return new Argument(name, type, value, className);
		}
		/// <summary>
		/// Creates a new instance of Argument.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		/// <returns>Argument.</returns>
		public virtual Argument Create(string name, string type, object value, string className)
		{
			return new Argument(name, type, value, className);
		}
		/// <summary>
		/// Redefines the property 'this'.
		/// </summary>
		/// <param name="index">Index.</param>
		/// <returns>Argument.</returns>
		public virtual Argument this[int index]
		{
			get
			{
				Argument lResult = null;
				if(index < this.Count)
				{
					Argument[] Values = null;
					this.Values.CopyTo(Values, index);
					lResult = Values[0];
				}
				return lResult;
			}
		}
	}
	#endregion Arguments

	#region Argument
	/// <summary>
	/// Defines the argument stored information.
	/// </summary>
	public class Argument
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of Argument.
		/// </summary>
		public Argument() {}
		/// <summary>
		/// Initializes a new instance of Argument.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		public Argument(string name, ModelType type, string className)
			: this(name, type.ToString(),null, className){ }
		/// <summary>
		/// Initializes a new instance of Argument.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		public Argument(string name, string type, string className)
			:this(name,type,null, className){}
		/// <summary>
		/// Initializes a new instance of Argument.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		public Argument(string name, ModelType type, object value, string className)
			: this(name, type.ToString(), value, className) { }
		/// <summary>
		/// Initializes a new instance of Argument.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		public Argument(string name, string type, object value, string className)
		{
			this.Name = name;
			this.Type = type;
			this.Value = value;
			this.ClassName = className;
		}
		#endregion Constructors


		public string Name = string.Empty;
		public string Type = string.Empty;
		public string ClassName = string.Empty;

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
		/// <summary>
		/// Indicates if the value is an Oid (main key).
		/// </summary>
		public bool IsOidValue()
		{
			return (Value is Oids.Oid);
		}
		/// <summary>
		/// Indicates if the value is an Alternate key.
		/// </summary>
		public bool IsAlternateKeyValue()
		{
			return (Value is Oids.AlternateKey);
		}
	}
	#endregion Argument

	#region FilterVariables
	/// <summary>
	/// Defines the filter variables stored information.
	/// </summary>
	public class FilterVariables : Arguments
	{
		/// <summary>
		/// Initializes a new instance of FilterVariables.
		/// </summary>
		public FilterVariables()
			:base(){}
		/// <summary>
		/// Initializes a new instance of FilterVariables.
		/// </summary>
		/// <param name="arguments">Arguments of the filter.</param>
		public FilterVariables(Arguments arguments)
		{
			if (arguments != null)
			{
				foreach (Argument i in arguments.Values)
				{
					this.Add(new FilterVariable(i));
				}
			}
		}
		/// <summary>
		/// Initializes a new instance of FilterVariables.
		/// </summary>
		/// <param name="variables">Variables of the filter.</param>
		public FilterVariables(FilterVariables variables)
			: base(variables) { }
		/// <summary>
		/// Initializes a new instance of FilterVariables.
		/// </summary>
		/// <param name="types">Types of the arguments.</param>
		/// <param name="values">Values of the arguments.</param>
		/// <param name="domains">Domains of the arguments.</param>
		public FilterVariables(Dictionary<string, ModelType> types, Dictionary<string, object> values, Dictionary<string, string> domains)
		{
			foreach(string i in values.Keys)
			{
				if(domains.ContainsKey(i))
				{
					this.Add(new FilterVariable(i, types[i], values[i], domains[i]));
				}
				else
				{
					this.Add(new FilterVariable(i, types[i], values[i], string.Empty));
				}
			}
		}
		/// <summary>
		/// Initializes a new filter variable.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		/// <param name="domain">Domain.</param>
		/// <returns>Argument.</returns>
		public override Argument Create(string name, ModelType type, object value, string domain)
		{
			return new FilterVariable(name, type, value, domain);
		}
		/// <summary>
		/// Initializes a new filter variable.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		/// <param name="domain">Domain.</param>
		/// <returns>Argument.</returns>
		public override Argument Create(string name, string type, object value, string domain)
		{
			return new FilterVariable(name, type, value, domain);
		}
	}
	#endregion FilterVariables

	#region FilterVariable
	/// <summary>
	/// Defines a filter variable stored information.
	/// </summary>
	public class FilterVariable : Argument
	{
		#region Constructors
		/// <summary>
		/// Initializes a new FilterVariable.
		/// </summary>
		public FilterVariable()
			:base() {}
		/// <summary>
		/// Initializes a new FilterVariable.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="type">Type of the variable.</param>
		public FilterVariable(string name, ModelType type, string domain)
			:base(name,type, domain){}
		/// <summary>
		/// Initializes a new FilterVariable.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="type">Type of the variable.</param>
		public FilterVariable(string name, string type, string domain)
			:base(name,type, domain){}
		/// <summary>
		/// Initializes a new FilterVariable.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="type">Type of the variable.</param>
		/// <param name="value">Value of the variable.</param>
		public FilterVariable(string name, ModelType type, object value, string domain)
			:base(name,type,value, domain){}
		/// <summary>
		/// Initializes a new FilterVariable.
		/// </summary>
		/// <param name="name">Name of the variable.</param>
		/// <param name="type">Type of the variable.</param>
		/// <param name="value">Value of the variable.</param>
		public FilterVariable(string name, string type, object value, string domain)
			:base(name,type,value, domain){}
		/// <summary>
		/// Initializes a new FilterVariable.
		/// </summary>
		/// <param name="argument">Argument(name, type, value).</param>
		public FilterVariable(Argument argument)
			:base(argument.Name,argument.Type,argument.Value, argument.ClassName){}
		#endregion Constructors
	}
	#endregion FilterVariable
}

