// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using SIGEM.Client.Oids;

namespace SIGEM.Client
{
	[Serializable]
	public class IUPopulationContext : IUQueryContext
	{
		#region Members
		/// <summary>
		/// Instance of Order Criteria Name Selected.
		/// </summary>
		private string mOrderCriteriaNameSelected = string.Empty;
		/// <summary>
		/// Instance of Filter Name Selected.
		/// </summary>
		private string mFilterNameSelected = string.Empty;
		/// <summary>
		/// Filter contexts list.
		/// </summary>
		FilterContextList mFilters = null;
		/// <summary>
		/// Instance of Filter Variable Name Selected.
		/// </summary>
		private string mFilterVariableNameSelected = string.Empty;
		/// <summary>
		/// Instance of Block Size.
		/// </summary>
		private int mBlockSize = 40;
		/// <summary>
		/// Instance of Last Oids.
		/// </summary>
		private Stack<Oid> mLastOids = new Stack<Oid>();
		/// <summary>
		/// Instance of Last Block.
		/// </summary>
		private bool mLastBlock = false;
		/// <summary>
		/// Instance of MultiSelection Allowed.
		/// </summary>
		private bool mMultiSelectionAllowed = false;

		/// <summary>
		/// Executed Filter.
		/// </summary>
		private string mExecutedFilter = string.Empty;

		#endregion Members

		#region Properties

		#region OrderCriteriaNameSelected
		/// <summary>
		/// Gets or sets order criteria name selected.
		/// </summary>
		public string OrderCriteriaNameSelected
		{
			get
			{
				return mOrderCriteriaNameSelected;
			}
			set
			{
				mOrderCriteriaNameSelected = value;
			}
		}
		#endregion OrderCriteriaNameSelected

		#region FilterNameSelected
		/// <summary>
		/// Gets or sets filter name selected.
		/// </summary>
		public string FilterNameSelected
		{
			get
			{
				return mFilterNameSelected;
			}
			set
			{
				mFilterNameSelected = value;
			}
		}
		#endregion FilterNameSelected
		#region FilterVariableNameSelected
		/// <summary>
		/// Gets or sets filter variable name selected.
		/// </summary>
		public string FilterVariableNameSelected
		{
			get
			{
				return mFilterVariableNameSelected;
			}
			set
			{
				mFilterVariableNameSelected = value;
			}
		}
		#endregion FilterVariableNameSelected

		#region ExecutedFilter
		/// <summary>
		/// Filter Executed
		/// </summary>
		public string ExecutedFilter
		{
			get { return mExecutedFilter; }
			set { mExecutedFilter = (value == null ? string.Empty : value); }
		}

		#endregion ExecutedFilter
		#region Filters
		/// <summary>
		/// Gets the filter contexts list.
		/// </summary>
		public FilterContextList Filters
		{
			get
			{
				return mFilters;
			}
			protected set
			{
				mFilters = value;
			}
		}
		#endregion Filters
		#region BlockSize
		/// <summary>
		/// Gets or sets block size.
		/// </summary>
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
		#endregion BlockSize
		#region LastOid
		/// <summary>
		/// Gets or sets last Oid.
		/// </summary>
		public Oid LastOid
		{
			get
			{
				if (LastOids.Count > 0)
				{
					return LastOids.Peek();
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					LastOids.Push(value);
				}
			}
		}
		#endregion LastOid
		#region LastOids
		/// <summary>
		/// Gets or sets last Oids.
		/// </summary>
		public Stack<Oid> LastOids
		{
			get
			{
				return mLastOids;
			}
			set
			{
				mLastOids = value;
			}
		}
		#endregion LastOids
		#region LastBlock
		/// <summary>
		/// Gets or sets last block.
		/// </summary>
		public bool LastBlock
		{
			get
			{
				return mLastBlock;
			}
			set
			{
				mLastBlock = value;
			}
		}
		#endregion LastBlock
		#region MultiSelectionAllowed
		/// <summary>
		/// Gets or sets multi selection allowed.
		/// </summary>
		public bool MultiSelectionAllowed
		{
			get
			{
				return mMultiSelectionAllowed;
			}
			set
			{
				mMultiSelectionAllowed = value;
			}
		}
		#endregion MultiSelectionAllowed


		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'IUPopulationContext' class.
		/// </summary>
		/// <param name="exchangeInfo">Exchange information.</param>
		/// <param name="className">Class name.</param>
		/// <param name="iuName">IU name.</param>
		public IUPopulationContext(ExchangeInfo exchangeInfo, string className, string iuName)
			: base(exchangeInfo, ContextType.Population, className, iuName)
		{
			Filters = new FilterContextList();
		}
		#endregion Constructors

		#region FilterList -> Collection.
		/// <summary>
		/// Filter Contexts List.
		/// </summary>
		public class FilterContextList :
			KeyedCollection<string, IUFilterContext>,
			IFilters
		{
			#region Constructors
			/// <summary>
			/// Initializes a new instance of the 'FilterList' class.
			/// </summary>
			public FilterContextList() { }
			#endregion Constructors

			#region KeyedCollection
			protected override string GetKeyForItem(IUFilterContext filterContext)
			{
				return filterContext.FilterName;
			}

			#region Collection<IUInputFilterController> Implementation
			protected override void ClearItems()
			{
				int lCount = 0;
				foreach (IUFilterContext lFilterContext in this)
				{
					Delete(lFilterContext);
					lCount++;
				}
				base.ClearItems();
			}
			protected override void InsertItem(int index, IUFilterContext filterContext)
			{
				Insert(filterContext, index);
				base.InsertItem(index, filterContext);
			}
			protected override void RemoveItem(int index)
			{
				Delete(this[index]);
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, IUFilterContext filterContext)
			{
				Insert(filterContext, index);
				base.SetItem(index, filterContext);
			}
			#endregion Collection<IUInputFilterController> Implementation
			#endregion KeyedCollection

			#region IFilterList implementation
			public bool Exist(string filterContext)
			{
				return this.Dictionary.ContainsKey(filterContext);
			}
			#endregion IFilterList implementation

			#region Insert IUFilterContext
			protected virtual void Insert(IUFilterContext filterContext, int index)
			{
			}
			#endregion Insert IUFilterContext

			#region Remove IUFilterContext
			protected virtual void Delete(IUFilterContext filterContext)
			{
			}
			#endregion Remove IUFilterContext
		}
		#endregion FilterList -> Collection.

		#region IFilters
		/// <summary>
		/// Filter list interface.
		/// </summary>
		public interface IFilters : IList<IUFilterContext>
		{
			/// <summary>
			/// Gets the specified filter from the context list.
			/// </summary>
			/// <param name="name">Filter context name.</param>
			/// <returns></returns>
			IUFilterContext this[string name] { get; }
			/// <summary>
			/// Checks if the item specified exist in the filter context list.
			/// </summary>
			/// <param name="name">Item to search.</param>
			/// <returns></returns>
			bool Exist(string name);
		}
		#endregion IFilters
	}
}
