// v3.8.4.5.b

using System;
using System.Data;
using System.Collections.Generic;
using SIGEM.Client.Presentation;
using SIGEM.Client.Logics;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the DisplaySetByBlocks controller.
	/// </summary>
	public class DisplaySetByBlocksController : DisplaySetController
	{
		#region Members
		/// <summary>
		/// Population presentation.
		/// </summary>
		private IDisplaySetByBlocksPresentation mPopulation = null;
		/// <summary>
		/// First block of data.
		/// </summary>
		private ITriggerPresentation mFirst;
		/// <summary>
		/// Previous block of data.
		/// </summary>
		private ITriggerPresentation mPrevious;
		/// <summary>
		/// Refresh block of data.
		/// </summary>
		private ITriggerPresentation mRefresh;
		/// <summary>
		/// Next block of data.
		/// </summary>
		private ITriggerPresentation mNext;
		/// <summary>
		/// datatable.
		/// </summary>
		private DataTable mDataTable = null;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'DisplaySetByBlocksController' class.
		/// </summary>
		/// <param name="displaySet">DisplaySet controller</param>
		public DisplaySetByBlocksController(DisplaySetController displaySet)
			: base(displaySet.Parent)
		{
			DisplaySetList = displaySet.DisplaySetList;
			CurrentDisplaySet = displaySet.CurrentDisplaySet;
		}
		/// <summary>
		/// Initializes a new instance of the 'DisplaySetByBlocksController' class.
		/// </summary>
		/// <param name="name">Name of the DisplaySet.</param>
		/// <param name="agents">List of agents of the DisplaySet.</param>
		/// <param name="parent">Parent controller.</param>
		public DisplaySetByBlocksController(Controller parent)
			: base(parent)
		{
		}

		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets the population of the DisplaySet.
		/// </summary>
		public IDisplaySetByBlocksPresentation Population
		{
			get
			{
				return mPopulation;
			}
			set
			{
				if (mPopulation != null)
				{
					mPopulation.MoreBlocks -= new EventHandler<TriggerEventArgs>(HandlePopulationNextPage);
				}
				mPopulation = value;
				Viewer = value;
				if (mPopulation != null)
				{
					mPopulation.MoreBlocks += new EventHandler<TriggerEventArgs>(HandlePopulationNextPage);
				}
			}
		}
		/// <summary>
		/// Gets the parent controller.
		/// </summary>
		public new IUQueryController Parent
		{
			get
			{
				return base.Parent as IUQueryController;
			}
		}
		/// <summary>
		/// Gets the parent context.
		/// </summary>
		public IUQueryContext Context
		{
			get
			{
				return Parent.Context;
			}
		}
		/// <summary>
		/// Gets the first visible row of the DisplaySet presentation.
		/// </summary>
		public int FirstVisibleRow
		{
			get
			{
				return mPopulation.FirstVisibleRow;
			}
			set
			{
				mPopulation.FirstVisibleRow = value;
			}
		}
		/// <summary>
		/// Gets the list of instances selected.
		/// </summary>
		public List<Oid> InstancesSelected
		{
			get
			{
				return null;
			}
		}
		/// <summary>
		/// Gets the last instance.
		/// </summary>
		public Oid LastInstance
		{
			get
			{
				return null;
			}
		}
		/// <summary>
		/// Gets or sets the trigger presentation used for showing the first block of instances in the Population.
		/// </summary>
		public ITriggerPresentation First
		{
			get
			{
				return mFirst;
			}
			set
			{
				if (mFirst != null)
				{
					mFirst.Triggered -= new EventHandler<TriggerEventArgs>(HandlePopulationFirstPage);
				}
				mFirst = value;
				if (mFirst != null)
				{
					mFirst.Triggered += new EventHandler<TriggerEventArgs>(HandlePopulationFirstPage);
				}
			}
		}
		/// <summary>
		/// Gets or sets the trigger presentation used for showing the previous block of instances in the Population.
		/// </summary>
		public ITriggerPresentation Previous
		{
			get
			{
				return mPrevious;
			}
			set
			{
				if (mPrevious != null)
				{
					mPrevious.Triggered -= new EventHandler<TriggerEventArgs>(HandlePopulationPreviousPage);
				}
				mPrevious = value;
				if (mPrevious != null)
				{
					mPrevious.Triggered += new EventHandler<TriggerEventArgs>(HandlePopulationPreviousPage);
				}
			}
		}
		/// <summary>
		/// Gets or sets the trigger presentation used for refreshing the current block of instances in the Population.
		/// </summary>
		public ITriggerPresentation Refresh
		{
			get
			{
				return mRefresh;
			}
			set
			{
				if (mRefresh != null)
				{
					mRefresh.Triggered -= new EventHandler<TriggerEventArgs>(HandlePopulationRefreshPage);
				}
				mRefresh = value;
				if (mRefresh != null)
				{
					mRefresh.Triggered += new EventHandler<TriggerEventArgs>(HandlePopulationRefreshPage);
				}
			}
		}
		/// <summary>
		/// Gets or sets the trigger presentation used for showing the next block of instances in the Population.
		/// </summary>
		public ITriggerPresentation Next
		{
			get
			{
				return mNext;
			}
			set
			{
				if (mNext != null)
				{
					mNext.Triggered -= new EventHandler<TriggerEventArgs>(HandlePopulationNextPage);
				}
				mNext = value;
				if (mNext != null)
				{
					mNext.Triggered += new EventHandler<TriggerEventArgs>(HandlePopulationNextPage);
				}
			}
		}
		/// <summary>
		/// Gets the datatable containing the data.
		/// </summary>
		public virtual DataTable DataTable
		{
			get
			{
				return mDataTable;
			}
		}
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when the first page of the population is required.
		/// </summary>
		public event EventHandler<FirstPageEventArgs> FirstPage;
		/// <summary>
		/// Occurs when the previous page of the population is required.
		/// </summary>
		public event EventHandler<PreviousPageEventArgs> PreviousPage;
		/// <summary>
		/// Occurs when a refresh of the population is required.
		/// </summary>
		public event EventHandler<RefreshPageEventArgs> RefreshPage;
		/// <summary>
		/// Occurs when the next page of the population is required.
		/// </summary>
		public event EventHandler<NextPageEventArgs> NextPage;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Handles the event when the first page of the DisplaySet presentation is required.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">TriggerEventArgs.</param>
		private void HandlePopulationFirstPage(Object sender, TriggerEventArgs e)
		{
			OnFirstPage(new FirstPageEventArgs());
		}
		/// <summary>
		/// Handles the event when the previous page of the DisplaySet presentation is required.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">TriggerEventArgs.</param>
		private void HandlePopulationPreviousPage(Object sender, TriggerEventArgs e)
		{
			OnPreviousPage(new PreviousPageEventArgs());
		}
		/// <summary>
		/// Handles the event when a refreshing in the DisplaySet presentation is required.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">TriggerEventArgs.</param>
		private void HandlePopulationRefreshPage(Object sender, TriggerEventArgs e)
		{
			OnRefreshPage(new RefreshPageEventArgs());
		}
		/// <summary>
		/// Handles the event when the next page of the DisplaySet presentation is required.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">TriggerEventArgs.</param>
		private void HandlePopulationNextPage(object sender, TriggerEventArgs e)
		{
			if ((mDataTable == null) || (mDataTable.Rows.Count == 0))
			{
				// Raise event.
				OnNextPage(new NextPageEventArgs());
			}
			else // DataTable has rows.
			{
				// Get first visible row
				int lVisibleRow = 0;
				if (Population != null)
				{
					lVisibleRow = Population.FirstVisibleRow;
				}

				// Raise event.
				OnNextPage(new NextPageEventArgs());

				// Set first visible row
				if (Population != null)
				{
					Population.FirstVisibleRow = lVisibleRow;
				}
			}
		}
		#endregion Event Handlers

		#region Event Raisers
		/// <summary>
		/// Raises the First Page event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnFirstPage(FirstPageEventArgs eventArgs)
		{
			EventHandler<FirstPageEventArgs> handler = FirstPage;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the Next Page event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnNextPage(NextPageEventArgs eventArgs)
		{
			EventHandler<NextPageEventArgs> handler = NextPage;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the Previous Page event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnPreviousPage(PreviousPageEventArgs eventArgs)
		{
			EventHandler<PreviousPageEventArgs> handler = PreviousPage;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the Refresh Page event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnRefreshPage(RefreshPageEventArgs eventArgs)
		{
			EventHandler<RefreshPageEventArgs> handler = RefreshPage;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		#endregion Event Raisers

		#region Methods

		#region Initialize
		/// <summary>
		/// Initializes and applies format to the DisplaySet.
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();
		}
		#endregion Initialize

		#region Set population
		/// <summary>
		/// Sets the population data to the DisplaySet.
		/// </summary>
		/// <param name="data">DataTable.</param>
		public override void SetPopulation(DataTable data, bool discardExistingData, List<Oid> selectedOids)
		{
			if (discardExistingData || mDataTable == null || data == null)
			{
				mDataTable = data;
				// Accept changes in all rows. Required for editable Grid.
				foreach (DataRow lRow in mDataTable.Rows)
				{
					lRow.AcceptChanges();
				}
			}
			else
			{
				// Join tables
				foreach (DataRow lRow in data.Rows)
				{
					object[] lData = lRow.ItemArray;
					DataRow lNewRow = mDataTable.Rows.Add(lData);
					// Accept the changes in the row. Required for editable Grid.
					lNewRow.AcceptChanges();
				}
			}

			// Show population
			if (mPopulation != null)
			{
				mPopulation.ShowData(mDataTable, selectedOids);
			}

			SetNumberOfInstances(); //Set the number of instances
		}
		#endregion Set population
		/// <summary>
		/// Stablish the label Number of elements
		/// </summary>
		private void SetNumberOfInstances()
		{
			if (this.NumberOfInstances == null)
			{
				return;
			}

			IUPopulationContext lContext = Context as IUPopulationContext;
			int? totalNumberOfInstances = (mDataTable.ExtendedProperties["TotalRows"] as int?);
			string strTotalNumberOfInstances = "..."; // By default, if no information about the total number of instances.
			if (totalNumberOfInstances != null && totalNumberOfInstances > -1)
			{
				strTotalNumberOfInstances = totalNumberOfInstances.ToString();
			}

			if (lContext != null)
			{
				if (lContext.LastBlock)
				{
					this.NumberOfInstances.Value = DataTable.Rows.Count.ToString() + "/" + mDataTable.Rows.Count.ToString();
				}
				else
				{
					this.NumberOfInstances.Value = DataTable.Rows.Count.ToString() + "/" + strTotalNumberOfInstances;
				}
			}
			else
			{
				this.NumberOfInstances.Value = DataTable.Rows.Count.ToString() + "/" + strTotalNumberOfInstances;
			}
		}
		/// <summary>
		/// Selects a row by index.
		/// </summary>
		/// <param name="index">Index of the row to be selected.</param>
		public void SelectRow(int index)
		{
			if (mPopulation != null)
			{
				mPopulation.SelectRow(index);
			}
		}
		/// <summary>
		/// Gets the position of the Oid in the population.
		/// </summary>
		/// <param name="oid">Oid object to be searched.</param>
		/// <returns>Integer indicating the position of the Oid.</returns>
		public int GetRowFromOid(Oid oid)
		{
			if (mPopulation != null)
			{
				return mPopulation.GetRowFromOid(oid);
			}
			return -1;
		}
		/// <summary>
		/// Save the information in user preferences
		/// </summary>
		protected override void SaveScenarioInfoInPrefecences()
		{
			// For population interaction unit
			IUQueryController queryController = Parent as IUQueryController;

			if (queryController == null)
				return;

			string nameInPreferences = Context.ClassName + ":" + queryController.Name;
			IUPopulationContext popContext = Context as IUPopulationContext;

			Logic.UserPreferences.SavePopulationInfo(nameInPreferences, popContext.BlockSize, CurrentDisplaySet.Name, DisplaySetList);

		}
		#endregion Methods
	}
}
