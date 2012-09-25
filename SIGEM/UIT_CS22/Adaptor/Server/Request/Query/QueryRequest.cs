// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

using SIGEM.Client.Adaptor.DataFormats;

namespace SIGEM.Client.Adaptor
{
	#region Query.Request.
	internal class QueryRequest
	{
		private string mAlternateKeyName = string.Empty;
		public string AlternateKeyName
		{
			get
			{
				return mAlternateKeyName;
			}
			set
			{
				mAlternateKeyName = value;
			}
		}

		public QueryRequest() { }

		public QueryRequest(
				string className,
				List<string> displaySet,
				QueryInstance queryInstance,
				string orderCriteria,
				NavigationalFiltering navigationalFiltering)
				: this(className, string.Empty, queryInstance, orderCriteria, navigationalFiltering)
		{
			this.DisplaySet.AddRange(displaySet);
		}
		public QueryRequest(
			string className,
			string displaySet,
			QueryInstance queryInstance,
			string orderCriteria,
			NavigationalFiltering navigationalFiltering)
		{
			this.Class = className;
			this.DisplayItems = displaySet;
			this.QueryInstance = queryInstance;
			this.OrderCriteria = orderCriteria;
			this.NavigationalFiltering = navigationalFiltering;
		}

		private string mClass = string.Empty;
		public string Class
		{
			get
			{
				return mClass;
			}
			set
			{
				mClass = (value != null ? value : string.Empty);
			}
		}

		public string DisplayItems
		{
			get
			{
				StringBuilder lStringDisplaySet = new StringBuilder();
				string lSeparador = string.Empty;
				foreach(string i in this.DisplaySet)
				{
					lStringDisplaySet.Append(lSeparador);
					lStringDisplaySet.Append(i);
					lSeparador = ", ";
				}

				if (this.HasAlternateKeyName())
				{
					string alternateKeyDisplaySet = this.GetAlternateKeyDisplaySet(Class, AlternateKeyName);
					lSeparador = ", ";
					if (alternateKeyDisplaySet != string.Empty)
					{
						if (lStringDisplaySet.Length > 0) lStringDisplaySet.Append(lSeparador);
						lStringDisplaySet.Append(alternateKeyDisplaySet);
					}
				}
				return lStringDisplaySet.ToString();
			}
			set
			{
				this.DisplaySet.Clear();

				if ((value != null) && (value.Length > 0))
				{
				this.DisplaySet.AddRange(value.Split(','));
				}
			}
		}

		public List<string> mDisplaySet = new List<string>();
		public List<string> DisplaySet
		{
			get
			{
				return mDisplaySet;
			}
		}

		/// <summary>
		/// Indicates if an alternate key name has been specified to the query.
		/// </summary>
		/// <returns>A boolean value.</returns>
		public bool HasAlternateKeyName()
		{
			return (AlternateKeyName != null && AlternateKeyName != string.Empty);
		}

		/// <summary>
		/// Gets the display set items needed for an Alternate Key.
		/// </summary>
		/// <param name="className">Name of the class.</param>
		/// <param name="alternateKeyName">Name of the Alternate Key name.</param>
		/// <returns>String containing the display set items separated by commas.</returns>
		private string GetAlternateKeyDisplaySet(string className, string alternateKeyName)
		{
			string alternateKeyDisplaySet = string.Empty;
			try
			{
				Oids.Oid oidRoot = Oids.Oid.Create(className);
				Oids.Oid alternateKey = (Oids.Oid)oidRoot.GetAlternateKey(AlternateKeyName);

				if (alternateKey != null)
				{
					// Get the alternate keys attribute names and add to display set.
					StringBuilder alternateKeyNames = new StringBuilder();
					string lSeparador = string.Empty;
					foreach (Oids.IOidField fieldItem in alternateKey.Fields)
					{
						alternateKeyNames.Append(lSeparador);
						alternateKeyNames.Append(fieldItem.Name);
						lSeparador = ", ";
					}
					alternateKeyDisplaySet = alternateKeyNames.ToString();
				}
			}
			catch
			{
				return string.Empty;
			}

			return alternateKeyDisplaySet;
		}
		protected void SetAlternateKeyFromQueryInstance()
		{
			if (IsQueryInstance)
			{
				QueryInstance query = this.QueryInstance;
				if (!query.IsAlternateKey())
				{
					string alternateKeyName = string.Empty;
					if (HasAlternateKeyName())
					{
						alternateKeyName = AlternateKeyName;
					}
					else
					{
						alternateKeyName = query.Oid.AlternateKeyName;
					}
					AlternateKeyName = alternateKeyName;
				}
			}
		}

		private string mOrderCriteria = string.Empty;
		public string OrderCriteria
		{
			get
			{
				return mOrderCriteria;
			}
			set
			{
				mOrderCriteria = (value != null ? value : string.Empty);
			}
		}

		private NavigationalFiltering mNavigationalFiltering = null;
		public NavigationalFiltering NavigationalFiltering
		{
			get
			{
				return mNavigationalFiltering;
			}
			set
			{
				mNavigationalFiltering = value;
			}
		}

		private QueryInstance mQueryInstance = new QueryInstance();
		public QueryInstance QueryInstance
		{
			get
			{
				return mQueryInstance;
			}
			set
			{
				mQueryInstance = value;
				SetAlternateKeyFromQueryInstance();
			}
		}
		public QueryRelated QueryRelated
		{
			get
			{
				return this.QueryInstance as QueryRelated;
			}
		}
		public QueryFilter QueryFilter
		{
			get
			{
				return QueryRelated as QueryFilter;
			}
		}

		public bool IsQueryInstance { get { return ((!this.IsQueryFilter) && (!this.IsQueryRelated)); } }
		public bool IsQueryFilter { get { return (this.QueryFilter != null); } }
		public bool IsQueryRelated { get { return (this.QueryRelated != null); } }
	}
	#endregion Query.Request.

	#region Query.Instance.
	internal class QueryInstance
	{
		public QueryInstance() { }
		public QueryInstance(Oids.Oid oid)
		{
			this.Oid = oid;
		}
		private Oids.Oid mOid = null;
		public Oids.Oid Oid
		{
			get
			{
				return mOid;
			}
			set
			{
				mOid = value;
			}
		}

		public static QueryInstance Create(Oids.Oid oid)
		{
			return new QueryInstance(oid);
		}

		/// <summary>
		/// Gets the name of the alternate key name of the Oid object used in the current query.
		/// </summary>
		/// <returns>String representing the name of the alternate key.</returns>
		public string GetAlternateKeyName()
		{
			return Oid.AlternateKeyName;
		}
		/// <summary>
		/// Indicates if the Oid object used in the current query represents an alternate key.
		/// </summary>
		/// <returns>Boolean indicating if the Oid represents an alternate key.</returns>
		public bool IsAlternateKey()
		{
			return (Oid is Oids.AlternateKey);
		}
		/// <summary>
		/// Gets AlternateKey object corresponding with the Oid of the current query (if it has alternate key).
		/// </summary>
		/// <returns>Boolean indicating if the Oid represents an alternate key.</returns>
		public Oids.AlternateKey GetAlternateKey()
		{
			try
			{
				return (Oid as Oids.AlternateKey);
			}
			catch
			{
				return null;
			}
		}
	}
	#endregion Query.Instance.

	#region Query.InstanceFromAlternateKey.
	internal class QueryInstanceFromAlternateKey : QueryInstance
	{
		#region Constructors.
		public QueryInstanceFromAlternateKey(Oids.Oid oid)
			: base(oid) { }

		#endregion Constructors.
	}
	#endregion Query.InstanceFromAlternateKey.

	#region Query.Related.
	internal class QueryRelated : QueryInstance
	{
		#region Constructors.
		public QueryRelated()
			: this((Dictionary<string,Oids.Oid>)null, (Oids.Oid)null, 0) { }

		public QueryRelated(Oids.Oid lastOid)
			: this((Dictionary<string, Oids.Oid>)null, lastOid, 0) { }

		public QueryRelated(int blockSize)
			: this((Dictionary<string, Oids.Oid>)null, (Oids.Oid)null, blockSize) { }

		public QueryRelated(Oids.Oid lastOid, int blockSize)
			: this((Dictionary<string, Oids.Oid>)null, lastOid, blockSize) { }

		public QueryRelated(Dictionary<string, Oids.Oid> linkedTo, Oids.Oid lastOid, int blockSize)
		{
			this.BlockSize = blockSize;
			this.Oid =lastOid;
			this.LinkedTo = linkedTo;
		}
		#endregion Constructors.

		#region BlockSize.
		private int mBlockSize = 0;
		public int BlockSize
		{
			get
			{
				return mBlockSize;
			}
			set
			{
				mBlockSize = value;
			}
		}
		#endregion BlockSize.

		#region LinkedTo
		private Dictionary<string, Oids.Oid> mLinkedTo = null;
		public Dictionary<string, Oids.Oid> LinkedTo
		{
			get
			{
				return mLinkedTo;
			}
			set
			{
				mLinkedTo = value;
			}
		}
		#endregion LinkedTo
	}
	#endregion Query.Related.

	#region Query.Filter
	internal class QueryFilter : QueryRelated
	{
		public QueryFilter()
			:base(){ }

		public QueryFilter(string name, Oids.Oid lastOid)
			: this(name, (Arguments)null, (Dictionary<string, Oids.Oid>)null, lastOid, 0) { }

		public QueryFilter(string name, Oids.Oid lastOid, int blockSize)
			: this(name, (Arguments)null, (Dictionary<string, Oids.Oid>)null, lastOid, blockSize) { }

		public QueryFilter(string name, Arguments arguments, int blockSize)
			:this(name,arguments,(Dictionary<string,Oids.Oid>)null,(Oids.Oid)null,blockSize){}

		public QueryFilter(string name, Arguments arguments, Oids.Oid lastOid, int blockSize)
			:this(name,arguments,(Dictionary<string,Oids.Oid>)null,lastOid,blockSize){}

		public QueryFilter(string name, Dictionary<string, Oids.Oid> linkedTo,int blockSize)
			: this(name, (Arguments)null, linkedTo, (Oids.Oid)null, blockSize) { }

		public QueryFilter(string name, Dictionary<string, Oids.Oid> linkedTo, Oids.Oid lastOid, int blockSize)
			: this(name, (Arguments)null, linkedTo, lastOid, blockSize) { }

		public QueryFilter(string name, Arguments arguments, Dictionary<string, Oids.Oid> linkedTo, Oids.Oid lastOid, int blockSize)
			:base(linkedTo,lastOid,blockSize)
		{
			this.Name = name;
			this.Variables = new FilterVariables(arguments);
		}

		#region Filter Name.
		private string mName = string.Empty;
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
		#endregion Filter Name.

		#region Filter Variables.
		public FilterVariables mVariables = null;
		public FilterVariables Variables
		{
			get
			{
				return mVariables;
			}
			set
			{
				mVariables = value;
			}
		}
		#endregion Filter Variables.
	}
	#endregion Query.Filter
}

