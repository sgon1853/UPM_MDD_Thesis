// v3.8.4.5.b
using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Windows.Forms;
using SIGEM.Client.Presentation;
using SIGEM.Client.Adaptor;
using SIGEM.Client.Logics;
using SIGEM.Client.Oids;
using SIGEM.Client.Logics.Preferences;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the IUPopulationController.
	/// </summary>
	public class IUPopulationController : IUQueryController, IInstancesSelector
	{
		#region Members
		/// <summary>
		/// IUPopulation filters.
		/// </summary>
		private FilterControllerList mFilterList = null;
		/// <summary>
		/// IUPopulation OrderCriterias.
		/// </summary>
		private Dictionary<string, OrderCriteriaController> mOrderCriterias= null;
        /// <summary>
        /// Execute first filter when form is showed by first time
        /// </summary>
        private bool mAutoSearch = false;
		#endregion Members

		#region Properties

		#region ExchangeInformation
		/// <summary>
		/// Gets or sets the ExchangeInformation.
		/// </summary>
		public override ExchangeInfo ExchangeInformation
		{
			get
			{
				return Context.ExchangeInformation;
			}
			set
			{
				Context.ExchangeInformation = value;
			}
		}
		#endregion ExchangeInformation
		#region InternalFilters
		/// <summary>
		/// Access Internal filters type.
		/// </summary>
		protected FilterControllerList InternalFilters
		{
			get { return mFilterList; }
			set { mFilterList = value; }
		}

		#endregion InternalFilters

		#region Filters
		/// <summary>
		/// Gets the IUPopulation Filters list.
		/// </summary>
		public virtual IFilters Filters
		{
			get
			{
				return InternalFilters;
			}
			protected set
			{
				if (InternalFilters != null)
				{
					InternalFilters.Parent = null;
					InternalFilters.ExecuteFilter -= new EventHandler<ExecuteFilterEventArgs>(HandleFilterExecute);
				}

				InternalFilters = value as FilterControllerList;

				if (InternalFilters != null)
				{
					InternalFilters.Parent = this;
					InternalFilters.ExecuteFilter += new EventHandler<ExecuteFilterEventArgs>(HandleFilterExecute);
				}
			}
		}
		#endregion Filters

		#region OrderCriterias
		/// <summary>
		///	Gets the IUPopulation Order Criterias list.
		/// </summary>
		public virtual Dictionary<string, OrderCriteriaController> OrderCriterias
		{
			get
			{
				return mOrderCriterias;
			}
			protected set
			{
				mOrderCriterias = value;
			}
		}
		#endregion OrderCriterias

		#region OrderCriteriaSelected
		/// <summary>
		/// Gets or sets the selected IUPopulation Order Criteria.
		/// </summary>
		public OrderCriteriaController OrderCriteriaSelected
		{
			get
			{
				// Search selected order criteria
				foreach (OrderCriteriaController lOrderCriteria in mOrderCriterias.Values)
				{
					if (lOrderCriteria.IsSelected)
					{
						return lOrderCriteria;
					}
				}
				return null;
			}
			set
			{
				OrderCriteriaController lOrderCriteria = OrderCriteriaSelected;
				if (lOrderCriteria!= null)
				{
					lOrderCriteria.IsSelected = false;
				}

				if (value != null)
				{
				value.IsSelected = true;
				}
			}
		}
		#endregion OrderCriteriaSelected

		#region Context
		/// <summary>
		/// Gets or sets the IUPopulation context.
		/// </summary>
		public new IUPopulationContext Context
		{
			get
			{
				return mContext as IUPopulationContext;
			}
			set
			{
				mContext = value;
			}
		}
		#endregion Context

		#region DisplaySet
		/// <summary>
		/// Gets or sets the IUPopulation DisplaySet presentation.
		/// </summary>
		public new DisplaySetByBlocksController DisplaySet
		{
			get
			{
				return base.DisplaySet as DisplaySetByBlocksController;
			}
			set
			{
				if (DisplaySet != null)
				{
					DisplaySet.NextPage -= new EventHandler<NextPageEventArgs>(HandleDisplaySetNextPage);
					DisplaySet.PreviousPage -= new EventHandler<PreviousPageEventArgs>(HandleDisplaySetPreviousPage);
					DisplaySet.RefreshPage -= new EventHandler<RefreshPageEventArgs>(HandleDisplaySetRefreshPage);
					DisplaySet.FirstPage -= new EventHandler<FirstPageEventArgs>(HandleDisplaySetFirstPage);
				}
				base.DisplaySet = value;
				if (value != null)
				{
					DisplaySet.NextPage += new EventHandler<NextPageEventArgs>(HandleDisplaySetNextPage);
					DisplaySet.PreviousPage += new EventHandler<PreviousPageEventArgs>(HandleDisplaySetPreviousPage);
					DisplaySet.RefreshPage += new EventHandler<RefreshPageEventArgs>(HandleDisplaySetRefreshPage);
					DisplaySet.FirstPage += new EventHandler<FirstPageEventArgs>(HandleDisplaySetFirstPage);
				}
			}
		}
		#endregion DisplaySet

        /// <summary>
        /// Execute first filter when form is showed by first time
        /// </summary>
        public bool AutoSearch
        {
            get
            {
                return mAutoSearch;
            }
            set
            {
                mAutoSearch = value;
            }
        }
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'IUPopulationController' class.
		/// </summary>
		/// <param name="name">Name of the Interaction Unit.</param>
		/// <param name="alias">Alias of the Interaction Unit.</param>
		/// <param name="idXML">IdXML of the Interaction Unit.</param>
		/// <param name="context">Current context.</param>
		/// <param name="parent">Parent controller.</param>
		public IUPopulationController(string name, string alias, string idXML, IUPopulationContext context, IUController parent)
			: base()
		{
			Name = name;
			Alias = alias;
			IdXML = idXML;
			Context = context;
			Parent =  parent;

			Filters = new FilterControllerList();

			mOrderCriterias = new Dictionary<string, OrderCriteriaController>(StringComparer.CurrentCultureIgnoreCase);
		}
		#endregion Constructors

		#region Events
		/// <summary>
		/// Occurs when an instance has been selected in the IUPopulation.
		/// </summary>
		public event EventHandler<InstancesSelectedEventArgs> InstancesHasBeenSelected;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Executes the actions related to OnFiltered event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">FilterEventArgs.</param>
		private void HandleFilterExecute(object sender, ExecuteFilterEventArgs executeFilterEventArgs)
		{
			// Checking pending changes
			if (!CheckPendingChanges(true, true))
			{
				return;
			}

			try
			{
				// Set in the beginning.
				Context.LastOids.Clear();
				DisplaySet.FirstVisibleRow = 0;

				// Change Selected filter.
				if (executeFilterEventArgs.Arguments != null)
				{
					InternalFilters.ExecutedFilterName = executeFilterEventArgs.Arguments.Name;
				}

				// Update data.
				UpdateData(true);
			}
			catch (Exception e)
			{
				ScenarioManager.LaunchErrorScenario(e);
			}
		}
		/// <summary>
		/// Executes the actions related to FirstPage event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">FirstPageEventArgs.</param>
		protected void HandleDisplaySetFirstPage(object sender, FirstPageEventArgs eventArgs)
		{
			try
			{
				// Clear Oids stack
				Context.LastOids.Clear();

				// Update data
				UpdateData(true);
			}
			catch (Exception e)
			{
				ScenarioManager.LaunchErrorScenario(e);
			}
		}
		/// <summary>
		/// Executes the actions related to PreviousPage event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">PreviousPageEventArgs.</param>
		protected void HandleDisplaySetPreviousPage(object sender, PreviousPageEventArgs eventArgs)
		{
			try
			{
				// Pops the top item of the Oids stack
				if (Context.LastOids.Count > 0)
				{
					Context.LastOids.Pop();
				}

				// Update data
				UpdateData(false);
			}
			catch (Exception e)
			{
				ScenarioManager.LaunchErrorScenario(e);
			}
		}
		/// <summary>
		/// Executes the actions related to RefreshPage event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">RefreshPageEventArgs.</param>
		protected void HandleDisplaySetRefreshPage(object sender, RefreshPageEventArgs eventArgs)
		{
			Refresh();
		}
		/// <summary>
		/// Executes the actions related to NextPage event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">NextPageEventArgs.</param>
		protected void HandleDisplaySetNextPage(object sender, NextPageEventArgs eventArgs)
		{
			try
			{
				// Return if it is last block
				if (Context.LastBlock)
				{
					return;
				}

				// Update data.
				UpdateData(false);
			}
			catch (Exception e)
			{
				ScenarioManager.LaunchErrorScenario(e);
			}
		}
		/// <summary>
		/// Occurs when the Order Criteria changes in the Population Interaction Unit.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="eventArgs">OrderCriteriaChangedEventArgs.</param>
		private void HandleOrderCriteriaValueChanged(object sender, OrderCriteriaChangedEventArgs eventArgs)
		{
			if (Filters != null)
			{
				// Refresh the grid only if there are no filters in the Population Interaction Unit.
				if (Filters.Count <= 0)
				{
					// Checking pending changes.
					if (!CheckPendingChanges(true, true))
					{
						return;
					}
					// Update the current context.
					UpdateContext();

					// Refresh data grid of the PIU.
					UpdateData(true);
				}
			}
		}
		#endregion Event Handlers

		#region Event Raisers
		/// <summary>
		/// Raises the InstancesHasBeenSelected event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnInstancesHasBeenSelected(InstancesSelectedEventArgs eventArgs)
		{
			EventHandler<InstancesSelectedEventArgs> handler = InstancesHasBeenSelected;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		#endregion Event Raisers

		#region Methods
		/// <summary>
		/// Refreshs the data in the viewer
		/// </summary>
		public override void Refresh()
		{
			// Set in the beginning.
			DisplaySet.FirstVisibleRow = 0;
			try
			{
				// Update data
				UpdateData(true);
			}
			catch (Exception e)
			{
				ScenarioManager.LaunchErrorScenario(e);
			}
		}
		/// <summary>
		/// Adds an OrderCriteria to the IUPopulation.
		/// </summary>
		/// <param name="orderCriteriaController">OrderCriteria to add.</param>
		public void AddOrderCriteria(OrderCriteriaController orderCriteriaController)
		{
			if (orderCriteriaController != null)
			{
				OrderCriterias.Add(orderCriteriaController.Name, orderCriteriaController);
			}
			orderCriteriaController.ValueChanged += new EventHandler<OrderCriteriaChangedEventArgs>(HandleOrderCriteriaValueChanged);
		}
		/// <summary>
		/// Initializes the IUPopulation.
		/// </summary>
		public override void Initialize()
		{
			// Configure the  IUPopulation.
			if (OkTrigger != null)
			{
				if (Context.ExchangeInformation.ExchangeType == ExchangeType.SelectionForward)
				{
					OkTrigger.Visible = true;
				}
				else
				{
					OkTrigger.Visible = false;
				}
			}

			if (mOrderCriterias.Values.Count > 0)
			{
				IList<KeyValuePair<object, string>> lOrderCriterias = new List<KeyValuePair<object, string>>();

				// To add text value depending on there is preferential Order Criteria in the filter or not
				if (CheckPreferentialOrderCriteriaDefined())
				{
					lOrderCriterias.Add(new KeyValuePair<object, string>(string.Empty, CultureManager.TranslateString(LanguageConstantKeys.L_FILTERDEFINED, LanguageConstantValues.L_FILTERDEFINED)));
				}
				else
				{
					lOrderCriterias.Add(new KeyValuePair<object, string>(string.Empty, CultureManager.TranslateString(LanguageConstantKeys.L_NONE, LanguageConstantValues.L_NONE)));
				}
				OrderCriteriaController lOrderCriteriaControllerLast = null;
				foreach (OrderCriteriaController lOrderCriteriaController in mOrderCriterias.Values)
				{
					string lTranlatedString = CultureManager.TranslateString(lOrderCriteriaController.IdXML, lOrderCriteriaController.Alias, lOrderCriteriaController.Alias);
					lOrderCriterias.Add(new KeyValuePair<object, string>(lOrderCriteriaController.Name, lTranlatedString));
					if ((lOrderCriteriaController.Selector as ISelectorPresentation) != null)
					{
						lOrderCriteriaControllerLast = lOrderCriteriaController;
					}
				}
				if ((lOrderCriteriaControllerLast.Selector as ISelectorPresentation) != null)
				{
					(lOrderCriteriaControllerLast.Selector as ISelectorPresentation).Items = lOrderCriterias;
					(lOrderCriteriaControllerLast.Selector as ISelectorPresentation).SelectedItem = 0;
				}
			}
            
            // Set Default order criteria
            if (ExchangeInformation.DefaultOrderCriteria != "")
            {
                if (mOrderCriterias.Values.Count > 0)
                {
                    foreach (OrderCriteriaController lOrderCriteria in OrderCriterias.Values)
                    {
                        if (lOrderCriteria.Name.Equals(ExchangeInformation.DefaultOrderCriteria,StringComparison.InvariantCultureIgnoreCase))
                        {
                            OrderCriteriaSelected = lOrderCriteria;
                            break;
                        }
                    }

                }
                else
                {
                    Context.OrderCriteriaNameSelected = ExchangeInformation.DefaultOrderCriteria;
                }
            }

			// Initialize filters default values.
			InternalFilters.Initialize();


			// Enabled the associated clear trigger
			if (AssociatedServiceClearTrigger != null)
			{
				AssociatedServiceClearTrigger.Visible = true;
			}

			base.Initialize();
		}
		/// <summary>
		/// Loads the initial population based on the information received.
		/// </summary>
		protected override void LoadInitialData()
		{
			UpdateContext();

            // Execute query population or filter
            if (Filters.Count > 0 && AutoSearch)
            {
                // First filter
                InternalFilters.ExecutedFilterName = Filters[0].Name;
                Context.ExecutedFilter = InternalFilters.ExecutedFilterName;
            }
            UpdateData(true);
		}
		/// <summary>
		/// Updates the data of the IUPopulation.
		/// </summary>
		/// <param name="refresh">Boolean value indicating whether data is refreshed or not.</param>
		public override void UpdateData(bool refresh)
		{
			// Update context
			UpdateContext();

			int lLastOidsCount = 0;
			// Current selected oids
			List<Oid> lSelectedOids = Context.SelectedOids;

			// Forget the existing data
			if (refresh)
			{
				// Clear the selection in the context.
				Context.SelectedOids = null;

				if (Context.LastOids.Count != 0)
				{
					lLastOidsCount = Context.LastOids.Count - 1;
				}

				Context.LastOids.Clear();
				Context.LastBlock = false;
			}

			DataTable lData = GetNextData(Context);

			// Add new last Oid.
			AddLastOid(Context,lData);

			// Create and Configure Data Columns if the data table is null.
			lData = ConfigureDataTableByDisplaySet(lData, DisplaySet);

			// Show Population
			// If refesh is true, the data set is reloaded.
			if (refresh)
			{
				// Reload lLastOidsCount BlokSize Pages.
				if ((lLastOidsCount > 0) && (!Context.LastBlock) )
				{
					DataTable lDataBlocks = GetDataTableBlocks(Context, lLastOidsCount);
					if (lDataBlocks != null)
					{
						if (lData != null)
						{
							lData.Merge(lDataBlocks);
						}
					}
				}

				// Populate the First Block.
				SetPopulation(lData, true, lSelectedOids);
				Context.SelectedOids = lSelectedOids;
			}
			else
			{
				// Set in Population next data block
				SetPopulation(lData, false, lSelectedOids);
			}

			// Execute default update
			base.UpdateData(refresh);
		}
		/// <summary>
		/// Query a number of blocks form the server.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="numBlocks"></param>
		private DataTable GetDataTableBlocks(IUPopulationContext context, int numBlocks)
		{
			DataTable lDataResult = null;
			for (int iaux = 0; iaux < numBlocks; iaux++)
			{
				// Get Data Query.
				DataTable lDataAux = GetQuery(Context);
				AddLastOid(Context, lDataAux);

				if (lDataAux != null)
				{
					if (lDataResult == null)
					{
						lDataResult = lDataAux;
					}
					else
					{
						lDataResult.Merge(lDataAux);
					}
				}
			}

			return lDataResult;
		}
		/// <summary>
		///
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		private DataTable GetNextData(IUPopulationContext context)
		{
			DataTable lData = null;
			// When is navigation, must have Seletected, Oids if not showed populations must be nothing.
			if (ExchangeInfo.IsNavigation(context.ExchangeInformation))
			{
				if (ExchangeInfo.HasSelectedOids(context.ExchangeInformation))
				{
					lData = GetQuery(context);
				}
				else
				{
					this.Context.LastBlock = true;
				}
			}
			else
			{
				lData = GetQuery(context);
			}

			return lData;
		}
		/// <summary>
		/// Has the Oid Selected in this block page ?
		/// </summary>
		/// <param name="dataTable"></param>
		/// <param name="selectedOid">Selected Oid.</param>
		/// <returns>True if datatable has the selected Oid.</returns>
		private bool HasTheSelectedOID(DataTable dataTable, Oid selectedOid)
		{
			if (selectedOid != null)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					Oid lOid = Adaptor.ServerConnection.GetOid(dataTable, row);

					if (lOid.Equals(selectedOid))
					{
						return true;
					}
				}
			}
			return false;
		}
		/// <summary>
		/// Enable or disable navigations depending if dataTable have instances or not.
		/// </summary>
		/// <param name="navigation">Navigation Controllers</param>
		/// <param name="dataTable">DataTable with instances queried.</param>
		protected virtual void ConfigureNavigations(NavigationController navigation, DataTable dataTable)
		{
			// Enable or disable Navigation items depending on the data.
			if (navigation != null)
			{
				if ((dataTable == null) || (dataTable.Rows.Count == 0))
				{
					foreach (NavigationItemController lNavigationItem in navigation.NavigationItems.Values)
					{
						lNavigationItem.Trigger.Enabled = false;
					}
				}
				else
				{
					foreach (NavigationItemController lNavigationItem in navigation.NavigationItems.Values)
					{
						lNavigationItem.Trigger.Enabled = true;
					}
				}
			}
		}
		/// <summary>
		/// Configure null data table from display set items metada.
		/// </summary>
		/// <param name="dataTable">dataTable to check if is null.</param>
		/// <param name="displaySet">Display set controller</param>
		/// <returns></returns>
		protected virtual DataTable ConfigureDataTableByDisplaySet(DataTable dataTable, DisplaySetController displaySet)
		{
			if (dataTable == null)
			{
				// Create columns, only for visible elements
				dataTable = new DataTable();
				List<string> lDSElements = new List<string>();
				foreach (DisplaySetItem litem in displaySet.CurrentDisplaySet.DisplaySetItems)
				{
					if (litem.Visible)
					{
						if (!lDSElements.Contains(litem.Name))
						{
							lDSElements.Add(litem.Name);
							dataTable.Columns.Add(litem.Name);
						}
					}
				}
			}
			return dataTable;
		}
		/// <summary>
		/// Add to Stack of last Oid queried.
		/// </summary>
		/// <param name="context">Context to save the last oid.</param>
		/// <param name="dataTable">Data table queried.</param>
		/// <returns></returns>
		protected virtual Oid AddLastOid(IUPopulationContext context, DataTable dataTable)
		{
			if (dataTable != null)
			{
				Oid lLastOid = Adaptor.ServerConnection.GetLastOid(dataTable);
				return AddLastOid(context, lLastOid);
			}

			return null;
		}
		/// <summary>
		/// Add to Stack of last Oid queried.
		/// </summary>
		/// <param name="context">Context to save the last oid.</param>
		/// <param name="lastOid">last Oid.</param>
		/// <returns></returns>
		protected virtual Oid AddLastOid(IUPopulationContext context, Oid lastOid)
		{
			if (lastOid != null)
			{
				context.LastOids.Push(lastOid);
			}
			return lastOid;
		}
		/// <summary>
		/// Execute a Query Filter or Query Population depending if has <b>FilterNameSelected</b> or not.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		protected virtual DataTable GetQuery(IUPopulationContext context)
		{
			DataTable lData = null;

			// If one filters is selected. use the Filter search
			if (Context.ExecutedFilter != string.Empty)
			{
				try
				{
					// Logic API call.
					lData = Logic.ExecuteQueryFilter(Context);
				}
				catch (Exception logicException)
				{
					ScenarioManager.LaunchErrorScenario(logicException);
				}
			}
			else
			{
				lData = GetQueryPopulation(context);
			}

			return lData;
		}
		/// <summary>
		/// Execute a <b>Query Related</b> or <b>Query Populaton</b> depending of exchange type.
		/// <b>ExchangeType.Navigation</b> execute Query Related.
		/// <b>ExchangeType.Action or SelectinForward</b> execute Query Population.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		protected virtual DataTable GetQueryPopulation(IUPopulationContext context)
		{
			DataTable lData = null;

			// Depending on the ExchangeInfo Type
			switch (Context.ExchangeInformation.ExchangeType)
			{
				case ExchangeType.Navigation:
					try
					{
						// Logic API call.
						lData = Logic.ExecuteQueryRelated(context);
					}
					catch (Exception logicException)
					{
						ScenarioManager.LaunchErrorScenario(logicException);
					}
					break;
				case ExchangeType.Action:
				case ExchangeType.SelectionForward:
					// If this Population doesn't contain filters, search all population
					if (Filters.Count == 0)
					{
						try
						{
							// Logic API call.
							lData = Logic.ExecuteQueryPopulation(context);
						}
						catch (Exception logicException)
						{
							ScenarioManager.LaunchErrorScenario(logicException);
						}
					}
					else
					{
						// If there are no filters executed, set the last block true to do not
						//  retrieve all the instances
						this.Context.LastBlock = true;
					}
					break;
				default:
					lData = null;
					break;
			}

			return lData;
		}
		/// <summary>
		/// Actions related with the DisplaySet Execute Command event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void ProcessDisplaySetExecuteCommand(object sender, ExecuteCommandEventArgs e)
		{

            // Manage the defined shortcuts
            if (e.Key != Keys.None)
            {
                // Action items
                if (Action != null)
                {
                    foreach (ActionItemController lActionItem in Action.ActionItems.Values)
                    {
                        if (lActionItem.CheckShortcutKey(e.Key))
                        {
                            if (lActionItem.Enabled)
                            {

                                lActionItem.Execute(null, new TriggerEventArgs());
                                e.Handled = true;
                                return;
                            }
                        }
                    }
                }

                // Navigation items
                if (Navigation != null)
                {
                    foreach (NavigationItemController lNavItem in Navigation.NavigationItems.Values)
                    {
                        if (lNavItem.CheckShortcutKey(e.Key))
                        {
                            if (lNavItem.Enabled)
                            {
                                lNavItem.Execute(null, new TriggerEventArgs());
                                e.Handled = true;
                                return;
                            }
                        }
                    }
                }
            }

			base.ProcessDisplaySetExecuteCommand(sender, e);

			switch (e.ExecuteCommandType)
			{
				case ExecuteCommandType.ExecuteFirstDestroyActionService:
					ExecuteCommandFirstDestroyActionService();
					break;
				case ExecuteCommandType.ExecuteFirstCreateActionService:
					ExecuteCommandFirstCreateActionService();
					break;
				case ExecuteCommandType.ExecuteFirstNotDestroyNotCreateActionService:
					ExecuteCommandFirstNotDestroyNotCreateActionService();
					break;

				case ExecuteCommandType.ExecuteRefresh:
					Refresh();
					break;

				case ExecuteCommandType.ExecuteRetriveAll:
					ExecuteCommandRetriveAll(Context);
					break;
				case ExecuteCommandType.ExecuteSelectInstance:
					// If there is an associated service set the selected instance.
					IUQueryController lIUQueryController = this as IUQueryController;
					if (lIUQueryController != null && lIUQueryController.AssociatedServiceController != null)
					{
						ExecuteCommandFirstNotDestroyNotCreateActionService();
						break;
					}

					if (this.OkTrigger != null && this.OkTrigger.Enabled && this.OkTrigger.Visible)
					{
						ProcessExecuteOk();
					}
					else
					{
						ExecuteCommandFirstNotDestroyNotCreateActionService();
					}
					break;
				default:
					break;
			}
		}
		/// <summary>
		/// Retrieves all the existing instances
		/// </summary>
		/// <param name="context"></param>
		protected virtual void ExecuteCommandRetriveAll(IUPopulationContext context)
		{
			context.LastBlock = false;
			UpdateData(true);

			while (!context.LastBlock)
			{
				UpdateData(false);
			}
		}
		/// <summary>
		/// Configures the InteractionToolkit layer from the context values.
		/// </summary>
		/// <param name="context">IUPopulationContext used to configure the InteractionToolkit layer.</param>
		public void ConfigureByContext(IUPopulationContext context)
		{
			IUPopulationContext lContext = context as IUPopulationContext;

			// Order criteria selected
			if (lContext.OrderCriteriaNameSelected != string.Empty)
			{
				OrderCriteriaSelected = OrderCriterias[lContext.OrderCriteriaNameSelected];
			}

			// Default
			base.ConfigureByContext(context);
		}
		/// <summary>
		/// Updates the context values from the InteractionToolkit layer.
		/// </summary>
		public override void UpdateContext()
		{
			InternalFilters.UpdateContext();

			// Order criteria selected.
			OrderCriteriaController lOrderCriteriaSelected = OrderCriteriaSelected;
			if (lOrderCriteriaSelected != null)
			{
				Context.OrderCriteriaNameSelected = OrderCriteriaSelected.Name;
			}
			else
			{
				Context.OrderCriteriaNameSelected = string.Empty;
			}

			if (string.IsNullOrEmpty(Context.OrderCriteriaNameSelected) && (Filters.Exist(Context.ExecutedFilter)))
			{
				Context.OrderCriteriaNameSelected = Filters[Context.ExecutedFilter].DefaultOrderCriteria;
			}

			base.UpdateContext();
		}
		/// <summary>
		/// Executes the actions related to OnOk event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void ProcessExecuteOk()
		{
			OnInstancesHasBeenSelected(new InstancesSelectedEventArgs(DisplaySet.Values));
			base.ProcessExecuteOk();
		}
		/// <summary>
		/// Checks null values and format for the filter variables.
		/// </summary>
		/// <param name="filter">Filter to check.</param>
		/// <returns>If true, the cheking was OK, otherwise, return false.</returns>
		private bool CheckNullAndFormatFilterVariablesValues(IUFilterController filter)
		{
			bool lResult = true;
			object[] lArgs = new object[1];

			// Exit function if filter is null.
			if (filter == null)
			{
				return false;
			}

			// Control the null - not null allowed for all the filter variables arguments.
			foreach (ArgumentController lFilterVariable in filter.InputFields)
			{
				// Argument data-valued validation.
				ArgumentDVController lFilterVariableDV = lFilterVariable as ArgumentDVController;
				if ((lFilterVariableDV != null) && (lFilterVariableDV.Editor != null))
				{
					lArgs[0] = lFilterVariable.Alias;
					lResult = lResult & lFilterVariableDV.Editor.Validate(CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_NECESARY, LanguageConstantValues.L_VALIDATION_NECESARY, lArgs));
				}
				// Argument object-valued validation.
				else
				{
					ArgumentOVController lFilterVariableOV = lFilterVariable as ArgumentOVController;
					if (lFilterVariableOV != null)
					{
						foreach (IEditorPresentation lEditor in lFilterVariableOV.Editors)
						{
							if (lEditor != null)
							{
								lArgs[0] = lFilterVariable.Alias;
								// Shows the validation error only for the last editor field.
								lResult = lResult & lEditor.Validate(CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_NECESARY, LanguageConstantValues.L_VALIDATION_NECESARY, lArgs));
							}
						}
					}
				}
			}

			return lResult;
		}
		/// <summary>
		/// Apply multilanguage to the scenario.
		/// </summary>
		public override void ApplyMultilanguage()
		{
			base.ApplyMultilanguage();

			// Distinguish Selection mode.
			if (Context.ExchangeInformation.ExchangeType == ExchangeType.SelectionForward)
			{
				// Ok button.
				if (this.OkTrigger != null)
				{
					this.OkTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_SELECT, LanguageConstantValues.L_SELECT, this.OkTrigger.Value.ToString());
				}
			}

			// Cancel button.
			if (this.CancelTrigger != null)
			{
				this.CancelTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_CLOSE, LanguageConstantValues.L_CLOSE, this.CancelTrigger.Value.ToString());
			}
		}
		/// <summary>
		/// Process the user preferences for this scenario
		/// </summary>
		/// <param name="preferences"></param>
		public void SetPreferences(PopulationPrefs preferences)
		{
			// Gets the custom DIsplaySets
			if (preferences == null)
				return;

			// Add teh custom DisplaySets to the DisplaySet List
			foreach (DisplaySetInformation displaySet in preferences.DisplaySets)
			{
				DisplaySet.DisplaySetList.Add(displaySet);
			}

			// Assign the selected DisplaySet
			if (!preferences.SelectedDisplaySetName.Equals(""))
			{
				DisplaySetInformation displaySet = DisplaySet.GetDisplaySetByName(preferences.SelectedDisplaySetName);
				if (displaySet != null)
				{
					DisplaySet.CurrentDisplaySet = displaySet;
				}
			}

			Context.BlockSize = preferences.BlockSize;
		}
		/// <summary>
		/// Checks if there are filters defined or not to add the correct text value for Order Criteria.
		/// </summary>
		private bool CheckPreferentialOrderCriteriaDefined()
		{
			// There is any filter.
			if ((mFilterList != null) && (mFilterList.Count > 0))
			{
				// To be applied "Filter defined" text 
				return true;
			}

			// To be applied "None" text
			return false;
		}
		#endregion Methods
	}
}

