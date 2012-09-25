// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using SIGEM.Client.Oids;
using SIGEM.Client.Logics;
using SIGEM.Client.Presentation;
using SIGEM.Client.Presentation.Forms;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Manages the inforation about one node in the Tree
	/// </summary>
	public class TreeNodeController
	{
		#region Members
		/// <summary>
		/// Node identifier
		/// </summary>
		private string mNodeId;
		/// <summary>
		/// Parent node identifier
		/// </summary>
		private string mParentNodeId;
		/// <summary>
		/// Sub node list
		/// </summary>
		private List<TreeNodeController> mSubNodes = new List<TreeNodeController>();
		/// <summary>
		/// Final controller. Its information won't be shown inthe Tree.
		/// </summary>
		private IDetailController mFinalNodeQueryController = null;
		/// <summary>
		/// Final controller node Id.
		/// </summary>
		private string mFinalNodeID = "";
		/// <summary>
		/// Query context information. Used to find the instances to be shown in this tree node level
		/// </summary>
		private IUQueryContext mQueryContext = null;
		/// <summary>
		/// Filter controller. Only for first level nodes
		/// </summary>
		private FilterControllerList mFilterList = null;
		/// <summary>
		/// Contextual menu for this node
		/// </summary>
		private ContextMenuStrip mMenu = null;
		/// <summary>
		/// Action controller for this node
		/// </summary>
		private ActionController mAction = null;
		/// <summary>
		/// Navigation controller for this node
		/// </summary>
		private NavigationController mNavigation = null;
		/// <summary>
		/// Alias. Will be used as label in the grouping node
		/// </summary>
		private string mAlias;
		/// <summary>
		/// Indicates if this node represents the main Master-Detail Interaction Unit.
		/// </summary>
		private bool mIsMainMaster;
		/// <summary>
		/// Image to be shown in the instance
		/// </summary>
		private string mImageKey = "";
		/// <summary>
		/// Image for the Grouping node
		/// </summary>
		private string mGroupImageKey = "";
		/// <summary>
		/// Indicates if the grouping node must be shown
		/// </summary>
		private bool mShowGroupingNode;
		/// <summary>
		/// Tree presentation
		/// </summary>
		private TreeViewPresentation mTree;
		/// <summary>
		/// List of recursive nodes
		/// </summary>
		private List<TreeNodeController> mRecursiveNodes;
		/// <summary>
		/// Original node if this one it is a recursive node
		/// </summary>
		private TreeNodeController mOriginalNode;
		/// <summary>
		/// Options menu item
		/// </summary>
		private ITriggerPresentation mOptionsMenuItem;
		/// <summary>
		/// Navigations menu item
		/// </summary>
		private ITriggerPresentation mNavigationsMenuItem;
		/// <summary>
		/// Represents the information of the Display Set that is showed in the current Node.
		/// </summary>
		private DisplaySetInformation mDisplaySetInfo;
		/// <summary>
		/// Indicates if node will be shown in the tree
		/// </summary>
		private bool mShowInTree = true;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new Tree node controller
		/// </summary>
		/// <param name="parentNodeId">Parent node identifier</param>
		/// <param name="id">Id</param>
		/// <param name="queryController">Detail Interface</param>
		/// <param name="instanceMasterController">Main Master Instance IU</param>
		/// <param name="isMainMaster">True if it is the Main Master</param>
		/// <param name="showGroupingNode">Boolean if grouping node must be shown or not</param>
		/// <param name="showInTree">Boolean if data must be shown in the tree</param>
		public TreeNodeController(string parentNodeId, string id, IDetailController queryController, IUInstanceController instanceMasterController, bool isMainMaster, bool showGroupingNode, bool showInTree)
		{
			mRecursiveNodes = new List<TreeNodeController>();
			Filters = new FilterControllerList();
			mOriginalNode = null;
			ParentNodeId = parentNodeId;
			NodeId = id;
			mIsMainMaster = isMainMaster;
			ShowGroupingNode = showGroupingNode;
			ShowInTree = showInTree;
			FinalNodeID = NodeId;
			InitializeNode(queryController);
		}

		/// <summary>
		/// Initializes a new Tree node controller
		/// </summary>
		/// <param name="parentNodeId">Parent node identifier</param>
		/// <param name="id">Id</param>
		/// <param name="queryController">Detail Interface</param>
		/// <param name="instanceMasterController">Main Master Instance IU</param>
		/// <param name="isMainMaster">True if it is the Main Master</param>
		/// <param name="showGroupingNode">True if Grouping nodes must be shown</param>
		public TreeNodeController(string parentNodeId, string id, IDetailController queryController, IUInstanceController instanceMasterController, bool isMainMaster, bool showGroupingNode)
		{
			mRecursiveNodes = new List<TreeNodeController>();
			Filters = new FilterControllerList();
			mOriginalNode = null;
			ParentNodeId = parentNodeId;
			NodeId = id;
			mIsMainMaster = isMainMaster;
			ShowGroupingNode = showGroupingNode;
			ShowInTree = CalculateShowInTree(queryController);
			FinalNodeID = NodeId;
			InitializeNode(queryController);
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="nodeToBeCopied"></param>
		/// <param name="newExchangeInfo"></param>
		public TreeNodeController(TreeNodeController nodeToBeCopied, ExchangeInfoNavigation newExchangeInfo)
		{
			mRecursiveNodes = new List<TreeNodeController>();
			mParentNodeId = nodeToBeCopied.ParentNodeId;
			mNodeId = nodeToBeCopied.NodeId;
			mIsMainMaster = false;
			mShowGroupingNode = nodeToBeCopied.ShowGroupingNode;
			Filters = new FilterControllerList();
			mQueryContext = new IUPopulationContext(newExchangeInfo, nodeToBeCopied.QueryContext.ClassName, "");
			mQueryContext.DisplaySetAttributes = nodeToBeCopied.QueryContext.DisplaySetAttributes;
			mOriginalNode = nodeToBeCopied;
			IUPopulationContext lPopContext = nodeToBeCopied.QueryContext as IUPopulationContext;
			if (lPopContext != null)
			{
				((IUPopulationContext)QueryContext).BlockSize = lPopContext.BlockSize;
			}
			mShowInTree = nodeToBeCopied.ShowInTree;
			mFinalNodeID = nodeToBeCopied.FinalNodeID;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// indicates is the node will be shown in the Tree
		/// </summary>
		public bool ShowInTree
		{
			get
			{
				return mShowInTree;
			}
			protected set
			{
				mShowInTree = value;
			}
		}
		/// <summary>
		/// Gets if the grouping node must be shown
		/// </summary>
		public bool ShowGroupingNode
		{
			get
			{
				return mShowGroupingNode;
			}
			set
			{
				mShowGroupingNode = value;
			}
		}
		/// <summary>
		/// Controller tree node
		/// </summary>
		public TreeNodeController OriginalNode
		{
			get
			{
				return mOriginalNode;
			}
		}
		/// <summary>
		/// Gets list of recursive nodes
		/// </summary>
		public List<TreeNodeController> RecursiveNodes
		{
			get
			{
				return mRecursiveNodes;
			}
		}
		/// <summary>
		/// Gets last block
		/// </summary>
		public bool LastBlock
		{
			get
			{
				if (PopulationContext == null)
				{
					return true;
				}
				return PopulationContext.LastBlock;
			}
		}
		/// <summary>
		/// Image Key for grouping node
		/// </summary>
		public string GroupImageKey
		{
			get
			{
				return mGroupImageKey;
			}
			set
			{
				mGroupImageKey = value;
			}
		}
		/// <summary>
		/// Image Key for instance
		/// </summary>
		public string ImageKey
		{
			get
			{
				return mImageKey;
			}
			set
			{
				mImageKey = value;
			}
		}
		/// <summary>
		/// Tree presentation
		/// </summary>
		public TreeViewPresentation Tree
		{
			get
			{
				return mTree;
			}
			set
			{
				mTree = value;
			}
		}

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
					InternalFilters.Parent = null;
					InternalFilters.ExecuteFilter += new EventHandler<ExecuteFilterEventArgs>(HandleFilterExecute);
				}
			}
		}
		#endregion Filters
		/// <summary>
		/// Gets or Sets the Actions.
		/// </summary>
		public ActionController Action
		{
			get
			{
				return mAction;
			}
			set
			{
				if (mAction != null)
				{
					mAction.SelectedInstancesRequired -= new EventHandler<SelectedInstancesRequiredEventArgs>(HandleActionNavigationSelectedInstancesRequired);
					mAction.RefreshRequired -= new EventHandler<RefreshRequiredEventArgs>(HandleActionRefreshRequired);
					mAction.ContextRequired -= new EventHandler<ContextRequiredEventArgs>(HandleActionNavigationContextRequired);
					mAction.LaunchingScenario -= new EventHandler<LaunchingScenarioEventArgs>(HandleActionLaunchingScenario);
				}
				mAction = value;
				if (mAction != null)
				{
					mAction.SelectedInstancesRequired += new EventHandler<SelectedInstancesRequiredEventArgs>(HandleActionNavigationSelectedInstancesRequired);
					mAction.RefreshRequired += new EventHandler<RefreshRequiredEventArgs>(HandleActionRefreshRequired);
					mAction.ContextRequired += new EventHandler<ContextRequiredEventArgs>(HandleActionNavigationContextRequired);
					mAction.LaunchingScenario += new EventHandler<LaunchingScenarioEventArgs>(HandleActionLaunchingScenario);
					mAction.RefreshByRow = true;
				}
			}
		}
		/// <summary>
		/// Gets or Sets the Navigations.
		/// </summary>
		public NavigationController Navigation
		{
			get
			{
				return mNavigation;
			}
			set
			{
				if (mNavigation != null)
				{
					mNavigation.SelectedInstancesRequired -= new EventHandler<SelectedInstancesRequiredEventArgs>(HandleActionNavigationSelectedInstancesRequired);
					mNavigation.ContextRequired -= new EventHandler<ContextRequiredEventArgs>(HandleActionNavigationContextRequired);
				}
				mNavigation = value;
				if (mNavigation != null)
				{
					mNavigation.SelectedInstancesRequired += new EventHandler<SelectedInstancesRequiredEventArgs>(HandleActionNavigationSelectedInstancesRequired);
					mNavigation.ContextRequired += new EventHandler<ContextRequiredEventArgs>(HandleActionNavigationContextRequired);
				}
			}
		}
		/// <summary>
		/// Gets or Sets the context.
		/// </summary>
		public IUQueryContext QueryContext
		{
			get
			{
				return mQueryContext;
			}
			set
			{
				mQueryContext = value;
			}
		}
		/// <summary>
		/// Gets the Population context.
		/// </summary>
		public IUPopulationContext PopulationContext
		{
			get
			{
				return QueryContext as IUPopulationContext;
			}
		}
		/// <summary>
		/// Alias for gouping node.
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
		/// Gets or sets the final node.
		/// </summary>
		public IDetailController FinalNodeQueryController
		{
			get
			{
				return mFinalNodeQueryController;
			}
			protected set
			{
				mFinalNodeQueryController = value;
			}
		}
		/// <summary>
		/// Final controller node Id.
		/// </summary>
		public string FinalNodeID
		{
			get
			{
				return mFinalNodeID;
			}
			set
			{
				mFinalNodeID = value;
			}
		}
		/// <summary>
		/// Gets or sets the contextual menu for node.
		/// </summary>
		public ContextMenuStrip Menu
		{
			get
			{
				return mMenu;
			}
			set
			{
				mMenu = value;
			}
		}
		/// <summary>
		/// Gets or sets the node identifier.
		/// </summary>
		public string NodeId
		{
			get
			{
				return mNodeId;
			}
			set
			{
				mNodeId = value;
			}
		}
		/// <summary>
		/// Gets or sets the parent node identifier.
		/// </summary>
		public string ParentNodeId
		{
			get
			{
				return mParentNodeId;
			}
			set
			{
				mParentNodeId = value;
			}
		}
		/// <summary>
		/// Gets the SubNode tree list.
		/// </summary>
		public List<TreeNodeController> SubNodes
		{
			get
			{
				return mSubNodes;
			}
		}
		/// <summary>
		/// Options menu item
		/// </summary>
		public ITriggerPresentation OptionsMenuItem
		{
			get
			{
				return mOptionsMenuItem;
			}
			set
			{
				mOptionsMenuItem = value;
			}
		}
		/// <summary>
		/// Navigations menu item
		/// </summary>
		public ITriggerPresentation NavigationsMenuItem
		{
			get
			{
				return mNavigationsMenuItem;
			}
			set
			{
				mNavigationsMenuItem = value;
			}
		}
		/// <summary>
		/// Represents the information of the Display Set that is showed in the current Node.
		/// </summary>
		public DisplaySetInformation DisplaySetInfo
		{
			get
			{
				return mDisplaySetInfo;
			}
		}
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when the node must be refreshed.
		/// </summary>
		public event EventHandler<RefreshNodeRequiredEventArgs> RefreshRequired;
		/// <summary>
		/// Occurs when the context information must be managed
		/// </summary>
		public event EventHandler<ContextRequiredEventArgs> ContextRequired;
		/// <summary>
		/// Occurs when the filter node must executed.
		/// </summary>
		public event EventHandler<ExecuteFilterEventArgs> ExecuteFilter;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Executes actions related to context.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleSubNodeContextRequired(object sender, ContextRequiredEventArgs e)
		{
			// Propagate the event
			OnContextRequired(e);
		}
		/// <summary>
		/// Executes actions related to node refreshment.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleSubNodeRefreshRequired(object sender, RefreshNodeRequiredEventArgs e)
		{
			// Propagate the event
			OnRefreshRequired(e);
		}
		/// <summary>
		/// Executes actions when scenario is launched.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleActionLaunchingScenario(object sender, LaunchingScenarioEventArgs e)
		{
			ProcessActionLaunchingScenario(sender, e);
		}
		/// <summary>
		/// Executes actions related to Actions and Navigations context.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleActionNavigationContextRequired(object sender, ContextRequiredEventArgs e)
		{
			ProcessActionNavigationContextRequired(sender, e);
		}
		/// <summary>
		/// Executes actions related to Actions and Navigations of selected instances.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleActionNavigationSelectedInstancesRequired(object sender, SelectedInstancesRequiredEventArgs e)
		{
			ProcessActionNavigationSelectedInstancesRequired(sender, e);
		}
		/// <summary>
		/// Executes actions related to refreshment of Actions execution.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleActionRefreshRequired(object sender, RefreshRequiredEventArgs e)
		{
			ProcessActionRefreshRequired(sender, e);
		}
		/// <summary>
		/// Executes actions related to filter execution.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleFilterExecute(object sender, ExecuteFilterEventArgs e)
		{
			ProcessFilterExecute(sender, e);
		}
		#endregion Event Handlers

		#region Process Events
		/// <summary>
		/// Process the action related to scenario launching.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ProcessActionLaunchingScenario(object sender, LaunchingScenarioEventArgs e)
		{
			// If no Tree then return
			if (Tree == null)
			{
				return;
			}

			//Gets selected node info
			List<KeyValuePair<string, Oid>> lCompleteOidPath = Tree.GetCompleteSelectedOidPath();
			lCompleteOidPath.Reverse();

			// Add the selected node info path to the custom data
			e.ExchangeInformation.CustomData.Add("_NODEPATH", lCompleteOidPath);
		}
		/// <summary>
		/// Process the action related to Actions and Navigations context.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ProcessActionNavigationContextRequired(object sender, ContextRequiredEventArgs e)
		{
			// Propagate the event
			OnContextRequired(e);
		}
		/// <summary>
		/// Process the action related to Actions and Navigations of selected instances.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ProcessActionNavigationSelectedInstancesRequired(object sender, SelectedInstancesRequiredEventArgs e)
		{
			if (Tree != null)
			{
				e.SelectedInstances = Tree.Values;
			}
		}
		/// <summary>
		/// Process the action related with the refreshment of Actions execution
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ProcessActionRefreshRequired(object sender, RefreshRequiredEventArgs e)
		{
			// Propagate the event
			OnRefreshRequired(new RefreshNodeRequiredEventArgs(NodeId, e));
		}
		/// <summary>
		/// Process the action related to filter execution
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProcessFilterExecute(object sender, ExecuteFilterEventArgs e)
		{
			// Propagate the event
			OnExecuteFilter(e);
		}
		#endregion Process Events

		#region Event Raisers
		/// <summary>
		/// Raises the RefreshNodeRequired event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnRefreshRequired(RefreshNodeRequiredEventArgs eventArgs)
		{
			EventHandler<RefreshNodeRequiredEventArgs> handler = RefreshRequired;
			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the ContextRequired event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnContextRequired(ContextRequiredEventArgs eventArgs)
		{
			EventHandler<ContextRequiredEventArgs> handler = ContextRequired;
			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the ExecuteFilter event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnExecuteFilter(ExecuteFilterEventArgs eventArgs)
		{
			EventHandler<ExecuteFilterEventArgs> handler = ExecuteFilter;
			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}

		#endregion Event Raisers

		#region Methods
		/// <summary>
		///  Initializes the nodes controller.
		/// </summary>
		private void InitializeNode(IDetailController queryController)
		{
			if (ShowInTree)
			{
				IUMasterDetailController masterDetailController = queryController as IUMasterDetailController;
				if (masterDetailController != null)
					InitializeMasterDetailNode(masterDetailController, CalculateDetailsGroupingNode(masterDetailController));
			}
			else
			{
				// It is a final node, will not be shown in the Tree.
				FinalNodeQueryController = queryController;
				Alias = CultureManager.TranslateString(FinalNodeQueryController.IdXMLDetailAlias, FinalNodeQueryController.DetailAlias);
			}
		}

		/// <summary>
		/// Calculate if the node representing the Master will require Intermedia nodes
		/// </summary>
		/// <param name="masterDetailController"></param>
		/// <returns></returns>
		private bool CalculateDetailsGroupingNode(IUMasterDetailController masterDetailController)
		{
			// If there is just one detail
			if (masterDetailController.Details.Count == 1)
				return false;

			// If there is more than 2
			if (masterDetailController.Details.Count > 2)
				return true;

			// There is only 2
			int lNumNoPath = 0;
			int lNumNoInTree = 0;
			foreach (IDetailController lDetail in masterDetailController.Details)
			{
				ExchangeInfoNavigation lNavInfo = lDetail.ExchangeInformation as ExchangeInfoNavigation;
				if (lNavInfo != null && lNavInfo.RolePath.Equals(""))
				{
					lNumNoPath++;
				}
				else
				{
					IUMasterDetailController lMasterDetail = lDetail as IUMasterDetailController;
					if (lMasterDetail != null)
					{
						// This detail won't de represented in the Tree if the Master is a population with filters
						IUPopulationController lPopController = lMasterDetail.Master as IUPopulationController;
						if (lPopController != null && lPopController.Filters.Count > 0)
							lNumNoInTree++;
					}
					else
					{
						lNumNoInTree++;
					}
				}
			}

			// Just one can be shown when this node is selected or both must appear in the tree
			if (lNumNoPath + lNumNoInTree != 1)
				return true;
			
			return false;
		}

		/// <summary>
		/// ANalize the Master Detail controller and creates the suitable nodes
		/// </summary>
		/// <param name="masterDetailController"></param>
		/// <param name="showDetailsGroupingNode"></param>
		private void InitializeMasterDetailNode(IUMasterDetailController masterDetailController, bool showDetailsGroupingNode)
		{
			// Keep the Master information.
			QueryContext = masterDetailController.Master.Context;
			// Initialize the DisplaySet information.
			mDisplaySetInfo = new DisplaySetInformation(masterDetailController.Master.DisplaySet.DisplaySetList[0].Name);
			// It has to be considered only the items that the agent is able to access.
			mDisplaySetInfo.DisplaySetItems = masterDetailController.Master.DisplaySet.DisplaySetList[0].GetVisibleDisplaySetItems(Logic.Agent.ClassName);
			QueryContext.DisplaySetAttributes = mDisplaySetInfo.GetDisplaySetItemsAsString();
			Alias = CultureManager.TranslateString(masterDetailController.IdXMLDetailAlias, masterDetailController.DetailAlias);
			Action = masterDetailController.Master.Action;
			Navigation = masterDetailController.Master.Navigation;
			masterDetailController.Master.Action = null;
			masterDetailController.Master.Navigation = null;
			IUPopulationController lPopController = masterDetailController.Master as IUPopulationController;
			if (lPopController != null)
			{
				foreach (IUFilterController lFilter in lPopController.Filters)
				{
					Filters.Add(lFilter);
				}
				lPopController.Filters.Clear();
			}

			// Analize every detail properties
			#region Details sub-nodes creation
			int nodeCounter = 0;
			TreeNodeController lSubNode = null;

			// Process all the Master-Detail details, in order to create the suitable sub-nodes.
			foreach (IDetailController lDetail in masterDetailController.Details)
			{

				bool lShowInTree = CalculateShowInTree(lDetail);
				lSubNode = new TreeNodeController(NodeId, NodeId + "_" + nodeCounter.ToString(), lDetail, null, false, showDetailsGroupingNode, lShowInTree);
				mSubNodes.Add(lSubNode);
				// Suscribe subnode events
				lSubNode.RefreshRequired += new EventHandler<RefreshNodeRequiredEventArgs>(HandleSubNodeRefreshRequired);
				lSubNode.ContextRequired += new EventHandler<ContextRequiredEventArgs>(HandleSubNodeContextRequired);
				nodeCounter++;

				// Special situation. If this Detail won't be shown in the Tree
				//  assign the FinalNodeController. Only for the first one
				if (!lSubNode.ShowInTree && FinalNodeQueryController == null) 
				{
					FinalNodeID = lSubNode.NodeId;
					FinalNodeQueryController = lDetail;
					lSubNode.ShowGroupingNode = false;
				}
			}
			#endregion Details sub-nodes creation

			// Empty the detail list
			masterDetailController.Details.Clear();
		}

		/// <summary>
		/// Returns true if the Detail will be shown in the Tree
		/// </summary>
		/// <param name="lDetail"></param>
		/// <returns></returns>
		private bool CalculateShowInTree(IDetailController detail)
		{
			// If it has no role path, won't be shown in the Tree
			ExchangeInfoNavigation lNavInfo = detail.ExchangeInformation as ExchangeInfoNavigation;
			if (lNavInfo != null && lNavInfo.RolePath.Equals(""))
			{
				return false;
			}

			// If it is a Master Detail and its Master is a Population with Filters, won't be shown in the Tree
			IUMasterDetailController lMasterDetail = detail as IUMasterDetailController;
			if (lMasterDetail != null)
			{
				IUPopulationController lPopulationController = lMasterDetail.Master as IUPopulationController;
				if (lPopulationController != null && lPopulationController.Filters.Count > 0)
				{
					return false;
				}
				else
				{
					return true;
				}
			}

			// It is not a Master Detail and its role path is not empty 
			return false;
		}

		#region Initialize
		/// <summary>
		///  Initializes the IU instance controller.
		/// </summary>
		public void Initialize()
		{
			if (FinalNodeQueryController != null)
			{
				FinalNodeQueryController.Initialize();
			}

			if (Action != null)
			{
				Action.Initialize();
			}

			if (Navigation != null)
			{
				Navigation.Initialize();
			}

			InternalFilters.Initialize();

			foreach (TreeNodeController subNode in SubNodes)
			{
                if (!subNode.NodeId.Equals(FinalNodeID))
                {
                    subNode.Tree = Tree;
                    subNode.Initialize();
                }
			}
		}
		#endregion Initialize
		/// <summary>
		/// Gets the tree node from a node identifier
		/// </summary>
		/// <param name="nodeId">Node identifier</param>
		/// <returns>A Tree node</returns>
		public TreeNodeController GetNodeById(string nodeId)
		{
			if (NodeId.Equals(nodeId))
			{
				return this;
			}

			foreach (TreeNodeController node in SubNodes)
			{
				TreeNodeController lNode = node.GetNodeById(nodeId);
				if (lNode != null)
					return lNode;
			}

			return null;
		}
		/// <summary>
		/// Updates the final controller data, the related instances with the received Oid
		/// </summary>
		/// <param name="parentOid"></param>
		/// <returns></returns>
		internal bool UpdateFinalControllerData(Oid parentOid)
		{
			if (FinalNodeQueryController == null)
			{
				return false;
			}

			// Update the exchange information
			List<Oid> lOids = new List<Oid>();
			ExchangeInfo lExchgInfo = null;
			ExchangeInfoNavigation lInfoNav = null;
			if (parentOid != null)
			{
				lOids.Add(parentOid);
			}
			lExchgInfo = FinalNodeQueryController.ExchangeInformation;
			lInfoNav = new ExchangeInfoNavigation(lExchgInfo as ExchangeInfoNavigation);
			lInfoNav.SelectedOids = lOids;
			FinalNodeQueryController.ExchangeInformation = lInfoNav;

			// Clear the last Oid in the population controller
			IUPopulationController lPopulationController = FinalNodeQueryController as IUPopulationController;
			if (lPopulationController != null)
			{
				lPopulationController.Context.LastOid = null;
				lPopulationController.Context.LastOids.Clear();
			}

			// Refresh data in the controller
			FinalNodeQueryController.UpdateData(true);

			if (lOids.Count == 0)
				return false;

			return true;
		}
		/// <summary>
		/// Configure Actions and Navigations according to the recevied Oid
		/// </summary>
		/// <param name="selectedInstance">Selected instance oid</param>
		/// <returns></returns>
		public void ConfigureMenu(Oid selectedInstance)
		{
			ConfigureActions(selectedInstance);
			ConfigureNavigations(selectedInstance);
		}
		/// <summary>
		/// Enable or Disable the actions according to the recevied Oid
		/// </summary>
		/// <param name="selectedInstance">Selected instance oid</param>
		/// <returns></returns>
		private void ConfigureActions(Oid selectedInstance)
		{
			if (Action == null || Menu == null)
			{
				return;
			}

			// If no instance selected disable all the class services, except the creation ones
			if (selectedInstance == null)
			{
				foreach (ActionItemController actionItem in Action.ActionItems.Values)
				{
					if (actionItem.ActionItemType != ActionItemType.Creation && actionItem.ClassIUName.Equals(QueryContext.ClassName))
					{
						actionItem.Enabled = false;
					}
				}
			}
			else
			{
				Action.Enabled = true;
			}
		}
		/// <summary>
		/// Enable or Disable the navigations according to the recevied Oid
		/// </summary>
		/// <param name="selectedInstance">Selected instance oid</param>
		/// <returns></returns>
		private void ConfigureNavigations(Oid selectedInstance)
		{
			if (Navigation == null || Menu == null)
			{
				return;
			}

			// If no instance selected disable all navigatons
			if (selectedInstance == null)
			{
				Navigation.Enabled = false;
			}
			else
			{
				Navigation.Enabled = true;
			}
		}
		/// <summary>
		/// Apply multilanguage to nodes.
		/// </summary>
		internal void ApplyMultilanguage()
		{
			if (FinalNodeQueryController != null)
			{
				FinalNodeQueryController.ApplyMultilanguage();
			}
			foreach (TreeNodeController lNode in SubNodes)
			{
				lNode.ApplyMultilanguage();
			}

			if (OptionsMenuItem != null)
			{
				OptionsMenuItem.Value = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_OPTIONS, LanguageConstantValues.L_POP_UP_MENU_OPTIONS);
			}
			if (NavigationsMenuItem != null)
			{
				NavigationsMenuItem.Value = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_NAVIGATIONS, LanguageConstantValues.L_POP_UP_MENU_NAVIGATIONS);
			}
		}

		/// <summary>
		/// Returns True if it is a Final Node
		/// </summary>
		/// <returns></returns>
		public bool IsFinalNode()
		{
			return SubNodes.Count == 0;
		}
		#endregion Methods

	}
}

