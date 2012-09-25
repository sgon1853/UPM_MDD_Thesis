// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace SIGEM.Client.Controllers
{	
		#region FilterList -> Collection.
		public class FilterControllerList :
			KeyedCollection<string, IUFilterController>,
			IFilters
		{
			#region Members
			/// <summary>
			/// Input Parent member.
			/// </summary>
			private IUPopulationController mParent = null;

			/// <summary>
			/// The last filter Selected.
			/// </summary>
			private IUFilterController mFilterSelected = null;

			/// <summary>
			/// Name of the last filter Executed.
			/// </summary>
			private string mExecutedFilterName = string.Empty;

			#endregion Members

			#region Properties

			#region Parent
			/// <summary>
			/// Input Parent property.
			/// </summary>
			public virtual IUPopulationController Parent
			{
				get
				{
					return mParent;
				}
				set
				{
					mParent = value;
				}
			}
			#endregion Parent

			#region FilterSelected
			/// <summary>
			/// Filter Selected.
			/// </summary>
			public virtual IUFilterController FilterSelected
			{
				get
				{
					foreach (IUFilterController lFilter in this)
					{
						if (lFilter.InputFields.ArgumentSelected != null)
						{
							mFilterSelected = lFilter;
							return mFilterSelected;
						}
					}
					if (mFilterSelected != null)
					{
						return mFilterSelected;
					}
					return null;
				}
				set
				{
					mFilterSelected = value;
				}
			}
			#endregion FilterSelected

			#region Executed Filter Name.
			/// <summary>
			/// Last Filter Name Executed.
			/// </summary>
			public string ExecutedFilterName
			{
				get { return mExecutedFilterName; }
				set { mExecutedFilterName = value; }
			}			
			#endregion Executed Filter Name.

			#endregion Properties

			#region Constructors
			/// <summary>
			/// Initializes a new instance of the 'FilterList' class.
			/// </summary>
			public FilterControllerList() { }
			#endregion Constructors

			#region KeyedCollection
			protected override string GetKeyForItem(IUFilterController item)
			{
				return item.Name;
			}

			#region Collection<IUInputFilterController> Implementation
			protected override void ClearItems()
			{
				int lCount = 0;
				foreach (IUFilterController litem in this)
				{
					Delete(litem);
					lCount++;
				}
				base.ClearItems();
			}

			protected override void InsertItem(int index, IUFilterController item)
			{
				Insert(item, index);
				base.InsertItem(index, item);
			}

			protected override void RemoveItem(int index)
			{
				Delete(this[index]);
				base.RemoveItem(index);
			}

			protected override void SetItem(int index, IUFilterController item)
			{
				Insert(item, index);
				base.SetItem(index, item);
			}
			#endregion Collection<IUInputFilterController> Implementation

			#endregion KeyedCollection

			#region IFilterList implementation

			public bool Exist(string name)
			{
				if(this.Dictionary != null)
				{
					return this.Dictionary.ContainsKey(name);
				}
				return false;
			}

			#endregion IFilterList implementation

			#region Insert IUFilterController and suscribe to parent to its events.
			/// <summary>
			/// Suscribe to ExecuteFilter event.
			/// </summary>
			/// <param name="filterController">Filter controller to insert.</param>
			protected virtual void Insert(IUFilterController filterController, int index)
			{
				filterController.ExecuteFilter += new EventHandler<ExecuteFilterEventArgs>(HandleFilterQueryExecute);
			}
			#endregion Insert IUFilterController and suscribe to parent to its events.

			#region Remove IUFilterController and reset suscribe events.
			/// <summary>
			/// Unsuscribe to ExecuteFilter event.
			/// </summary>
			/// <param name="filterController">Filter controller to delete.</param>
			protected virtual void Delete(IUFilterController filterController)
			{
				filterController.ExecuteFilter -= new EventHandler<ExecuteFilterEventArgs>(HandleFilterQueryExecute);
			}
			#endregion Remove IUFilterController and reset suscribe events.

			#region Events
			public event EventHandler<ExecuteFilterEventArgs> ExecuteFilter;

			private void HandleFilterQueryExecute(object sender, ExecuteFilterEventArgs e)
			{
				OnExecuteFilter(e);
			}
			protected virtual void OnExecuteFilter(ExecuteFilterEventArgs executeFilterEventArgs)
			{
				if (ExecuteFilter != null)
				{
					ExecuteFilter(this, executeFilterEventArgs);
				}
			}
			#endregion Events

			#region Initialize
			/// <summary>
			/// Initialize Filter Variables.
			/// </summary>
			public virtual void Initialize()
			{
				foreach (IUFilterController lFilter in this)
				{
					lFilter.Initialize();
				}
                if (Parent != null)
				    ConfigureByContext(Parent.Context.Filters);
			}
			#endregion Initialize

			#region ConfigureByContext
			public virtual void ConfigureByContext(IUPopulationContext.FilterContextList filterContextList)
			{
				foreach (IUFilterContext lFilter in filterContextList)
				{
					this[lFilter.FilterName].ConfigureByContext(lFilter);
				}

				ExecutedFilterName = Parent.Context.ExecutedFilter;
			}
			#endregion ConfigureByContext

			#region Upadate All filter Contextes
			/// <summary>
			/// Update the context.
			/// </summary>
			public virtual void UpdateContext()
			{
				// Set Filter and variable Selected.
				if (FilterSelected != null)
				{
					IUFilterController lFilterSelected = FilterSelected;

					Parent.Context.FilterNameSelected = lFilterSelected.Name;

					ArgumentController lArgumentSelected = lFilterSelected.InputFields.ArgumentSelected;

					if (lArgumentSelected != null)
					{
						Parent.Context.FilterVariableNameSelected = lArgumentSelected.Name;
					}
				}

				// Update Context for all filters.
				foreach (IUFilterController lFilter in this)
				{
					lFilter.UpdateContext();
				}
				// Set the Executed Filter.
				Parent.Context.ExecutedFilter = ExecutedFilterName;
			}
			#endregion Upadate All filter Contextes

			#region ApplyMultilanguage
			public virtual void ApplyMultilanguage()
			{
				// Filter Search buttons.
				foreach (IUFilterController lFilter in this)
				{
					lFilter.ApplyMultilanguage();
				}
			}
			#endregion ApplyMultilanguage
		}
		#endregion Arguments

		#region IFilters
		/// <summary>
		/// Argument list interface.
		/// </summary>
		public interface IFilters :
			IList<IUFilterController>
		{
			/// <summary>
			/// Gets & Sets the filter Selected.
			/// </summary>
			IUFilterController FilterSelected { get; set;}

			/// <summary>
			/// Gets the specified Filter from the list.
			/// </summary>
			/// <param name="name"></param>
			/// <returns></returns>
			IUFilterController this[string name] { get; }
			/// <summary>
			/// Checks if the item specified exist in the argument list.
			/// </summary>
			/// <param name="name">Item to search.</param>
			/// <returns></returns>
			bool Exist(string name);
		}
		#endregion IFilters
}


