// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;
using SIGEM.Client.Adaptor;
using SIGEM.Client.Oids;
using SIGEM.Client.Logics;
using SIGEM.Client.Presentation;
using SIGEM.Client.Presentation.Forms;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the IUTreeController
	/// </summary>
	public class IUTreeController : IUController
	{
		#region Members
		/// <summary>
		/// List of root nodes
		/// </summary>
		private List<TreeNodeController> mRootNodes = new List<TreeNodeController>();
		/// <summary>
		/// Controller for the main Instance Master, if axists
		/// </summary>
		private IUInstanceController mInstanceMasterController;
		/// <summary>
		/// Tree presentation
		/// </summary>
		private TreeViewPresentation mTree;
		/// <summary>
		/// Goup container presentation. Allows side information management
		/// </summary>
		private IGroupContainerPresentation mGroupContainer;
		/// <summary>
		/// List of pop-up refresh menu items
		/// </summary>
		private List<ITriggerPresentation> mRefreshMenuItems;
		/// <summary>
		/// Exchange information member for Master Detail Controller
		/// </summary>
		private ExchangeInfo mMasterDetailExchangeInfo;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Load the internal infomration based on the Master-Detail controller
		/// </summary>
		/// <param name="masterDetailController"></param>
		public IUTreeController(IUMasterDetailController masterDetailController)
			: base()
		{
			mRefreshMenuItems = new List<ITriggerPresentation>();
			// Get the information from the MasterDetail received and convert it 
			ConvertMasterDetailInfo(masterDetailController);
			// Load the Exchange Information
			mMasterDetailExchangeInfo = masterDetailController.ExchangeInformation;
			// Copy Name and Context
			Name = masterDetailController.Name;
			Context = masterDetailController.Context;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets the ExchangeInformation.
		/// </summary>
		public override ExchangeInfo ExchangeInformation
		{
			get
			{
				if (mMasterDetailExchangeInfo != null)
				{
					return mMasterDetailExchangeInfo;
				}
                if (mRootNodes.Count == 1 && mRootNodes[0].QueryContext != null)
                {
                    return mRootNodes[0].QueryContext.ExchangeInformation;
                }
				return null;
			}
			set
			{
				if (mMasterDetailExchangeInfo != null)
				{
					mMasterDetailExchangeInfo = value;
				}
			}
		}
		/// <summary>
		/// List of root nodes
		/// </summary>
		public List<TreeNodeController> RootNodes
		{
			get
			{
				return mRootNodes;
			}
		}
		/// <summary>
		/// Controller for the main Instance Master, if axists
		/// </summary>
		public IUInstanceController InstanceMasterController
		{
			get
			{
				return mInstanceMasterController;
			}
			set
			{
				mInstanceMasterController = value;
				if (mInstanceMasterController != null)
				{
					mInstanceMasterController.SelectedInstanceChanged += new EventHandler<SelectedInstanceChangedEventArgs>(HandleInstanceMasterControllerSelectedInstanceChanged);
				}
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
				if (mTree != null)
				{
					mTree.SearchIntermediaNodeData -= new EventHandler<SearchIntermediaNodeDataEventArgs>(HandleTreeSearchIntermediaNodeData);
					mTree.FinalNodeSelected -= new EventHandler<FinalNodeSelectedEventArgs>(HandleTreeFinalNodeSelected);
					mTree.GetNextDataBlock -= new EventHandler<GetNextNodeDataBlockEventArgs>(HandleTreeGetNextDataBlock);
					mTree.ExecuteCommand -= new EventHandler<ExecuteCommandEventArgs>(HandleTreeExecuteCommand);
				}
				mTree = value;
				if (mTree != null)
				{
					mTree.SearchIntermediaNodeData += new EventHandler<SearchIntermediaNodeDataEventArgs>(HandleTreeSearchIntermediaNodeData);
					mTree.FinalNodeSelected += new EventHandler<FinalNodeSelectedEventArgs>(HandleTreeFinalNodeSelected);
					mTree.GetNextDataBlock += new EventHandler<GetNextNodeDataBlockEventArgs>(HandleTreeGetNextDataBlock);
					mTree.ExecuteCommand += new EventHandler<ExecuteCommandEventArgs>(HandleTreeExecuteCommand);
					mTree.InstanceNodeSelected += new EventHandler<InstanceNodeSelectedEventArgs>(HandleTreeInstanceNodeSelected);
					mTree.IntermediaNodeSelected += new EventHandler<IntermediaNodeSelectedEventArgs>(HandleTreeIntermediaNodeSelected);
				}
			}
		}
		/// <summary>
		/// Goup container presentation. Allows side information management
		/// </summary>
		public IGroupContainerPresentation GroupContainer
		{
			get
			{
				return mGroupContainer;
			}
			set
			{
				mGroupContainer = value;
			}
		}
		/// <summary>
		/// List of pop-up refresh menu items
		/// </summary>
		public TreeNodeController GetNodeById(string nodeId)
		{
			foreach (TreeNodeController node in RootNodes)
			{
				TreeNodeController lNode = node.GetNodeById(nodeId);
				if (lNode != null)
					return lNode;
			}
			return null;
		}
		#endregion Properties

		#region Event Handlers
		/// <summary>
		/// Executes actions related with the IntermediaNode data researchment.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleTreeSearchIntermediaNodeData(object sender, SearchIntermediaNodeDataEventArgs e)
		{
			ProcessTreeSearchIntermediaNodeData(sender, e);
		}
		/// <summary>
		/// Executes actions related to InstanceNode selection.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleTreeInstanceNodeSelected(object sender, InstanceNodeSelectedEventArgs e)
		{
			ProcessTreeInstanceNodeSelected(sender, e);
		}
		/// <summary>
		/// Executes actions related to IntermediaNode selection.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleTreeIntermediaNodeSelected(object sender, IntermediaNodeSelectedEventArgs e)
		{
			ProcessTreeIntermediaNodeSelected(sender, e);
		}
		/// <summary>
		/// Executes actions related to getting next DataBlock.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleTreeGetNextDataBlock(object sender, GetNextNodeDataBlockEventArgs e)
		{
			ProcessTreeGetNextDataBlock(sender, e);
		}
		/// <summary>
		/// Executes actions related to FinalNode selection.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleTreeFinalNodeSelected(object sender, FinalNodeSelectedEventArgs e)
		{
			ProcessTreeFinalNodeSelected(sender, e);
		}
		/// <summary>
		/// Executes actions related with Tree ExecuteCommand event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleTreeExecuteCommand(object sender, ExecuteCommandEventArgs e)
		{
			ProcessTreeExecuteCommand(sender, e);
		}
		/// <summary>
		/// Executes actions related to changing selection in the Master region.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleInstanceMasterControllerSelectedInstanceChanged(object sender, SelectedInstanceChangedEventArgs e)
		{
			ProcessInstanceMasterControllerSelectedInstanceChanged(sender, e);
		}
		/// <summary>
		/// Executes actions related with refreshing option menu.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleRefreshMenuItemTriggered(object sender, TriggerEventArgs e)
		{
			ProcessRefreshMenuItemTriggered(sender, e);
		}
		/// <summary>
		/// Executes actions related to ContextRequired event for root nodes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleRootNodeContextRequired(object sender, ContextRequiredEventArgs e)
		{
			ProcessRootNodeContextRequired(sender, e);
		}
		/// <summary>
		/// Executes actions related to RefreshRequired event for root nodes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleRootNodeRefreshRequired(object sender, RefreshNodeRequiredEventArgs e)
		{
			ProcessRootNodeRefreshRequired(sender, e);
		}
		/// <summary>
		/// Executes actions related to ExecuteFilter event for root nodes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleRootNodeExecuteFilter(object sender, ExecuteFilterEventArgs e)
		{
			ProcessRootNodeExecuteFilter(sender, e);
		}
		#endregion Event Handlers

		#region Process Events
		/// <summary>
		/// Process the Instance Node Selected from the Tree. Configures the contextual menu and show the side information if it is neccesary
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessTreeInstanceNodeSelected(object sender, InstanceNodeSelectedEventArgs e)
		{
			// Hide all panels
			HideAllGroups();

			// Check if the selected node must show a final controller
			TreeNodeController lNodeController = GetNodeById(e.NodeId);
			if (lNodeController == null || lNodeController.FinalNodeID.Equals(""))
			{
				return;
			}

			if (e.SelectedInstances != null && e.SelectedInstances.Count > 0)
			{
				lNodeController.ConfigureMenu(e.SelectedInstances[0]);
				ShowFinalController(lNodeController.FinalNodeID, e.SelectedInstances[0], e.CompleteOidPath);
			}
		}
		/// <summary>
		/// Process the Intermedia Node Selected event from the Tree. Configures the contextual menu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessTreeIntermediaNodeSelected(object sender, IntermediaNodeSelectedEventArgs e)
		{
			// Configure the node menu
			TreeNodeController lNodeController = GetNodeById(e.NodeId);
			HideAllGroups();
			if (lNodeController != null)
			{
				// It is an intermedia node, no instance selected
				lNodeController.ConfigureMenu(null);
			}
		}
		/// <summary>
		/// Process the selection instance changes in the main Instance controller. Updates the information in the tree
		/// </summary>
		/// <param name="sender">The control that have raised the event.</param>
		/// <param name="e">Contains the necessary parameters involved in SelectedInstanceChanged event.</param>
		protected virtual void ProcessInstanceMasterControllerSelectedInstanceChanged(object sender, SelectedInstanceChangedEventArgs e)
		{
			// Selected instance in the master has been modified, refresh the first level nodes
			HideAllGroups();

			// If no instance selected in the Master, clear all nodes
			if (e.SelectedInstances == null || e.SelectedInstances.Count == 0)
			{
				Tree.Clear();
			}
			else
			{
				foreach (TreeNodeController treeNodeController in RootNodes)
				{
					// If this node has to show instances
					if (treeNodeController.QueryContext != null)
					{
						treeNodeController.QueryContext.ExchangeInformation.SelectedOids = e.SelectedInstances;
						DataTable lData = GetNextDataBlock(treeNodeController.QueryContext, null);
						Tree.ShowDataInRootNode(treeNodeController.NodeId, lData, treeNodeController.DisplaySetInfo, treeNodeController.LastBlock, treeNodeController.ShowGroupingNode);
					}
					else
					{
						Tree.ShowDataInRootNode(treeNodeController.NodeId, null, null, true, treeNodeController.ShowGroupingNode);
					}
				}
			}

			// If no instance node selected, configure root context menus
			if (Tree.Values == null || Tree.Values.Count == 0)
			{
				foreach (TreeNodeController treeNodeController in RootNodes)
				{
					treeNodeController.ConfigureMenu(null);
				}
			}
		}
		/// <summary>
		/// Process the SearchIntermediaNodeData event from the Tree. Finds and returns the proper related data
		/// </summary>
		/// <param name="sender">The control that have raised the event.</param>
		/// <param name="e">Contains the necessary parameters involved in SearchIntermediaNodeData event.</param>
		protected virtual void ProcessTreeSearchIntermediaNodeData(object sender, SearchIntermediaNodeDataEventArgs e)
		{
			// Hide all the panels
			HideAllGroups();

			// If no Oid parent or Id, do nothing
			if (e.NodeId == "" || e.ParentNodeOid == null)
			{
				return;
			}

			List<KeyValuePair<string, Oid>> lOidPathList = new List<KeyValuePair<string, Oid>>(e.CompleteOidPath);
			lOidPathList.Reverse();

			// Find the corresponfing ExchangeInfo with the Complete Oid Path
			// Add the last NodeId to the list
			IUQueryContext queryContext = GetQueryContextFromPath(lOidPathList);

			if (queryContext == null)
			{
				return;
			}

			TreeNodeController treeNodeController = this.GetNodeById(e.NodeId);
			DisplaySetInformation lDisplaySetInfo = treeNodeController.DisplaySetInfo;
			// Get the data
			DataTable lData = null;
			lData = GetPopulationRelatedWith(e.ParentNodeOid, queryContext);

			e.DisplaySetInfo = lDisplaySetInfo;

			e.Data = lData;
			e.LastBlock = IsLastBlock(queryContext);
		}
		/// <summary>
		/// Process the Final Node Selected event from the Tree. Shows the side information
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessTreeFinalNodeSelected(object sender, FinalNodeSelectedEventArgs e)
		{
			// One final node has been selected, show the related side info
			ShowFinalController(e.ParentNodeId, e.ParentNodeOid, e.CompleteOidPath);
		}
		/// <summary>
		/// Process the GetNextDataBlock event from the Tree. Finds the corresponding next data block
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessTreeGetNextDataBlock(object sender, GetNextNodeDataBlockEventArgs e)
		{
			List<KeyValuePair<string, Oid>> lOidPathList = new List<KeyValuePair<string, Oid>>(e.CompleteOidPath);

			IUQueryContext queryContext = null;
			if (IsRootNode(lOidPathList))
			{
				TreeNodeController lNodeController = GetNodeById(lOidPathList[0].Key);
				if (lNodeController == null)
				{
					return;
				}
				queryContext = lNodeController.QueryContext;
			}
			else
			{
				lOidPathList.Reverse();
				queryContext = UpdateAllNodeContextInformation(lOidPathList);
			}

			if (queryContext == null)
			{
				return;
			}

			// Get the DisplaySetInformation from the node controller.
			TreeNodeController treeNodeController = this.GetNodeById(e.NodeId);
			DisplaySetInformation lDisplaySetInfo = treeNodeController.DisplaySetInfo;

			DataTable lData = null;
			lData = GetNextDataBlock(queryContext, e.LastOid);

			// Send the DisplaySetInformation in the event arguments.
			e.DisplaySetInfo = lDisplaySetInfo;
			e.Data = lData;
			e.LastBlock = IsLastBlock(queryContext);
		}
		/// <summary>
		/// Process the Refresh Menu popup event. Refresh the information in the Tree, based on the selected node
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessRefreshMenuItemTriggered(object sender, TriggerEventArgs e)
		{
			Refresh();
		}
		/// <summary>
		/// Process the Execute Command event from the Tree
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessTreeExecuteCommand(object sender, ExecuteCommandEventArgs e)
		{
			// Only for Refesh command
			if (e.ExecuteCommandType == ExecuteCommandType.ExecuteRefresh)
			{
				Refresh();
			}

		}
		/// <summary>
		/// Process the ExecuteFilter event from a root node. Finds the data and shows them in the Tree
		/// </summary>
		/// <param name="sender">The control that have raised the event.</param>
		/// <param name="e">Contains the necessary parameters involved in ExecuteFilter event.</param>
		protected virtual void ProcessRootNodeExecuteFilter(object sender, ExecuteFilterEventArgs e)
		{
			// Only for one Root Node
			if (RootNodes.Count != 1)
			{
				return;
			}

			IUPopulationContext lPopContext = RootNodes[0].QueryContext as IUPopulationContext;
			if (lPopContext == null)
			{
				return;
			}

			// Assign the filter name
			lPopContext.ExecutedFilter = e.Arguments.Name;

			// Get the data
			DataTable lData = GetNextDataBlock(lPopContext, null);
			Tree.ShowDataInRootNode(RootNodes[0].NodeId, lData, RootNodes[0].DisplaySetInfo, IsLastBlock(lPopContext), RootNodes[0].ShowGroupingNode);
			lPopContext.ExecutedFilter = e.Arguments.Name;

			if (Tree.Values == null || Tree.Values.Count == 0)
			{
				foreach (TreeNodeController treeNodeController in RootNodes)
				{
					treeNodeController.ConfigureMenu(null);
				}
			}
		}
		/// <summary>
		/// Process the ContextRequired event from one root node. Updates all the context tree and returns the last one
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessRootNodeContextRequired(object sender, ContextRequiredEventArgs e)
		{
			// Get the completa oid path list and update the all the context
			List<KeyValuePair<string, Oid>> lOidsPath = Tree.GetCompleteSelectedOidPath();
			lOidsPath.Reverse();
			// Return the last one
			e.Context = UpdateAllNodeContextInformation(lOidsPath);

			// If the context is null, assign the context of the first level root node.
			if (e.Context == null)
			{
				e.Context = RootNodes[0].QueryContext;
			}
		}
		/// <summary>
		/// Process the Refresh Required event from one root node. Based on the received information, refresh one instance or a complete tree branch
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessRootNodeRefreshRequired(object sender, RefreshNodeRequiredEventArgs e)
		{
			// Refresh instance
			if (e.RefreshType == RefreshRequiredType.RefreshInstances)
			{
				RefreshOidList(e.NodeId, ((RefreshRequiredInstancesEventArgs)e.RefreshRequiredArgs).Instances);
				return;
			}

			// It is not an instance refresh, it is a complete branch refresh
			if (e.ReceivedExchangeInfo == null)
			{
				return;
			}

			List<KeyValuePair<string, Oid>> lOidsPath = e.ReceivedExchangeInfo.CustomData["_NODEPATH"] as List<KeyValuePair<string, Oid>>;
			if (lOidsPath == null)
			{
				return;
			}

			TreeNodeController lNode = GetNodeById(e.NodeId);
			if (lNode == null)
			{
				return;
			}

			if (lOidsPath != null && lOidsPath.Count <= 1)
			{
				// If there are not Oids loaded, load the data by means of refreshing the Tree.
				if (this.InstanceMasterController != null)
				{
					this.InstanceMasterController.Refresh();
					return;
				}
				Refresh();
			}
			else
			{
				// If there are Oids loaded, update them.
				UpdateBranch(lOidsPath, lNode);
			}
		}
		#endregion Process Events

		#region Methods
		/// <summary>
		/// Converts the Master-Detail structure to Tree-Node structure
		/// </summary>
		/// <param name="masterDetailController"></param>
		private void ConvertMasterDetailInfo(IUMasterDetailController masterDetailController)
		{
			// Assign the Alias and the IdXML properties from the IUMasterDetailController controller.
			this.Alias = masterDetailController.Alias;
			this.IdXML = masterDetailController.IdXML;

			// Main Master controller
			TreeNodeController lRootNode = null;
			// If the Main Master is a Instance
			if (masterDetailController.Master.GetType().Name.Equals("IUInstanceController"))
			{
				int nodeCounter = 0;
				// If there is more than one detail, show the grouping node
				bool showGroupingNode = masterDetailController.Details.Count > 1;
				foreach (IDetailController detail in masterDetailController.Details)
				{
					lRootNode = new TreeNodeController("", nodeCounter.ToString(), detail, (masterDetailController.Master as IUInstanceController), false, showGroupingNode);
					RootNodes.Add(lRootNode);
					lRootNode.RefreshRequired += new EventHandler<RefreshNodeRequiredEventArgs>(HandleRootNodeRefreshRequired);
					lRootNode.ContextRequired += new EventHandler<ContextRequiredEventArgs>(HandleRootNodeContextRequired);
					lRootNode.ExecuteFilter += new EventHandler<ExecuteFilterEventArgs>(HandleRootNodeExecuteFilter);
					nodeCounter++;
				}

				// Assign the Instance Main Master
				InstanceMasterController = masterDetailController.Master as IUInstanceController;
			}
			else
			{
				// Main master is a population
				lRootNode = new TreeNodeController("", "0", masterDetailController, null, true, false,true);
				RootNodes.Add(lRootNode);
				masterDetailController.Details.Clear();
				lRootNode.RefreshRequired += new EventHandler<RefreshNodeRequiredEventArgs>(HandleRootNodeRefreshRequired);
				lRootNode.ContextRequired += new EventHandler<ContextRequiredEventArgs>(HandleRootNodeContextRequired);
				lRootNode.ExecuteFilter += new EventHandler<ExecuteFilterEventArgs>(HandleRootNodeExecuteFilter);
			}
			// Remove the existing details
			masterDetailController.Details.Clear();
		}
		/// <summary>
		/// Load the initial data in the Tree
		/// </summary>
		private void LoadInitialData()
		{
			if (InstanceMasterController == null)
			{
				foreach (TreeNodeController lNode in RootNodes)
				{
					DataTable lData = GetNextDataBlock(lNode.QueryContext, null);
					Tree.ShowDataInRootNode(lNode.NodeId, lData, lNode.DisplaySetInfo, IsLastBlock(lNode.QueryContext), lNode.ShowGroupingNode);
				}

				if (Tree.Values == null || Tree.Values.Count == 0)
				{
					foreach (TreeNodeController treeNodeController in RootNodes)
					{
						treeNodeController.ConfigureMenu(null);
					}
				}
			}
		}
		/// <summary>
		/// Configures the Tree presentation for the root nodes
		/// </summary>
		private void ConfigureTree()
		{
			foreach (TreeNodeController node in RootNodes)
			{
				ConfigureTreeForNode("", node);
			}
		}
		/// <summary>
		/// Configures the Tree presentation for the received node and its subnodes
		/// </summary>
		private void ConfigureTreeForNode(string parentNodeId, TreeNodeController treeNode)
		{
			// Configures the Tree using the received Node and locating the TreeNodes contained in it
			mTree.AddIntermediaNode(parentNodeId, treeNode.NodeId, treeNode.Alias, treeNode.Menu, treeNode.SubNodes.Count == 0, treeNode.ImageKey, treeNode.GroupImageKey, treeNode.ShowGroupingNode);
			foreach (TreeNodeController subNode in treeNode.SubNodes)
			{
				if (subNode.ShowInTree || subNode.ShowGroupingNode)
					ConfigureTreeForNode(treeNode.NodeId, subNode);
			}
		}
		/// <summary>
		/// Initializes the Group Container for the root nodes
		/// </summary>
		private void InitializeGroupContainer()
		{
			int nodeCounter = 0;
			foreach (TreeNodeController node in mRootNodes)
			{
				if (node.IsFinalNode())
				{
					GroupContainer.AssignGroupId(nodeCounter, node.NodeId);
					nodeCounter++;
				}
				else
				{
					InitializeGroupContainer(node, ref nodeCounter);
				}
			}
		}
		/// <summary>
		/// Initializes the Group Container for the received node and its subnodes
		/// </summary>
		private void InitializeGroupContainer(TreeNodeController node, ref int nodeCounter)
		{
			foreach (TreeNodeController subNode in node.SubNodes)
			{
				if (subNode.IsFinalNode())
				{
					GroupContainer.AssignGroupId(nodeCounter, subNode.NodeId);
					nodeCounter++;
				}
				else
				{
					InitializeGroupContainer(subNode, ref nodeCounter);
				}
			}
		}
		/// <summary>
		/// Updates a complete existing branch in the tree
		/// </summary>
		/// <param name="originalOidPath"></param>
		/// <param name="nodeController"></param>
		private void UpdateBranch(List<KeyValuePair<string, Oid>> originalOidPath, TreeNodeController nodeController)
		{
			// Check if the branch still exists in the Tree
			if (!Tree.ExistCompleteOidPath(originalOidPath))
			{
				return;
			}

			// If there is only one element in the list, it is a root node
			if (originalOidPath.Count == 1)
			{
				// The root node it is an instance, refresh it
				if (originalOidPath[0].Value != null)
				{
					RefreshInstance(nodeController.NodeId, originalOidPath[0].Value as Oid);
					return;
				}
				else
				{
					// The root node it is a grouping node, refresh all the branch
				}
			}

			// Keep the original list
			List<KeyValuePair<string, Oid>> oidPathList = new List<KeyValuePair<string, Oid>>(originalOidPath);

			Oid lParentOid = null;
			lParentOid = GetLastOidInPath(oidPathList);

			// Find the corresponfing ExchangeInfo with the Complete Oid Path
			// Add the last NodeId to the list
			IUQueryContext queryContext = GetQueryContextFromPath(oidPathList);

			// Get the relaed data
			DataTable lData = null;
			if (lParentOid == null)
			{
				// If parent oid is null, use the last selected instance in the context
				if (queryContext.ExchangeInformation.SelectedOids != null &&
					queryContext.ExchangeInformation.SelectedOids.Count > 0)
				{
					lData = GetPopulationRelatedWith(queryContext.ExchangeInformation.SelectedOids[0], queryContext);
				}
			}
			else
			{
				lData = GetPopulationRelatedWith(lParentOid, queryContext);
			}

			// Replace the existing data in the Tree
			HideAllGroups();
			Tree.RefreshBranch(originalOidPath, lData, nodeController.DisplaySetInfo, IsLastBlock(queryContext));
		}
		/// <summary>
		/// Returns the last oid in the oid path
		/// </summary>
		/// <param name="oidPathList"></param>
		/// <returns></returns>
		private Oid GetLastOidInPath(List<KeyValuePair<string, Oid>> oidPathList)
		{
			for (int i = oidPathList.Count - 1; i >= 0; i--)
			{
				if (oidPathList[i].Value != null)
				{
					return oidPathList[i].Value;
				}
			}

			return null;
		}
		/// <summary>
		/// Returns true if the context indicates that the last block has been received
		/// </summary>
		/// <param name="queryContext"></param>
		/// <returns></returns>
		private bool IsLastBlock(IUQueryContext queryContext)
		{
			IUPopulationContext popContext = queryContext as IUPopulationContext;
			if (popContext == null)
			{
				return true;
			}

			return popContext.LastBlock;
		}
		/// <summary>
		/// Updates the query context tree information following the oid path list
		/// </summary>
		/// <param name="completeOidPathList"></param>
		/// <returns>The last query context</returns>
		private IUQueryContext UpdateAllNodeContextInformation(List<KeyValuePair<string, Oid>> oidPathList)
		{
			if (oidPathList == null || oidPathList.Count == 0)
			{
				return null;
			}
			// Copy the list and remove the null oids
			List<KeyValuePair<string, Oid>> lCompleteOidPathList = new List<KeyValuePair<string, Oid>>(oidPathList);

			// The selected oid in one level of the tree is the selected instance for the next level context info
			IUQueryContext queryContext = null;
			List<KeyValuePair<string, Oid>> lPartialOidPathList = new List<KeyValuePair<string, Oid>>();

			// The first oid
			Oid lPreviousOid = lCompleteOidPathList[0].Value;
			// Add the first node to the partial list
			lPartialOidPathList.Add(lCompleteOidPathList[0]);

			for (int i = 1; i < lCompleteOidPathList.Count; i++)
			{
				KeyValuePair<string, Oid> lPair = lCompleteOidPathList[i];
				lPartialOidPathList.Add(lPair);

				queryContext = GetQueryContextFromPath(lPartialOidPathList);
				if (lPreviousOid != null && queryContext != null)
				{
					List<Oid> lOids = new List<Oid>();
					lOids.Add(lPreviousOid);
					queryContext.ExchangeInformation.SelectedOids = lOids;
				}

				// Selected instance for the next level. If it is null, keep the previous one
				if (lPair.Value != null)
				{
					lPreviousOid = lPair.Value;
				}
			}

			return queryContext;
		}
		/// <summary>
		/// Retruns true if the node is a sub node at any level
		/// </summary>
		/// <param name="node">Node</param>
		/// <param name="nodeToBeFound">Sub node</param>
		/// <returns></returns>
		private bool ContainsNodeController(TreeNodeController node, TreeNodeController nodeToBeFound)
		{
			if (node == nodeToBeFound)
				return true;

			foreach (TreeNodeController lNodeController in node.SubNodes)
			{
				if (ContainsNodeController(lNodeController, nodeToBeFound))
				{
					return true;
				}
			}

			return false;
		}
		/// <summary>
		/// Retunrs the instances related with the received Oid using the received context
		/// </summary>
		/// <param name="relatedOid"></param>
		/// <param name="queryContext"></param>
		/// <returns></returns>
		private DataTable GetPopulationRelatedWith(Oid relatedOid, IUQueryContext queryContext)
		{
			if (relatedOid == null || queryContext == null)
				return null;

			List<Oid> lOids = new List<Oid>();
			lOids.Add(relatedOid);

			// Assign the exchange info to the context
			queryContext.ExchangeInformation.SelectedOids = lOids;
			queryContext.SelectedOids = lOids;

			// Clear the last oid
			IUPopulationContext populationContext = queryContext as IUPopulationContext;
			if (populationContext != null)
			{
				populationContext.LastOids.Clear();
			}

			DataTable lData = null;
			try
			{
				lData = Logic.ExecuteQueryRelated(queryContext);
			}
			catch (Exception exc)
			{
				ScenarioManager.LaunchErrorScenario(exc);
				return null;
			}

			return lData;
		}
		/// <summary>
		/// Returns the next data block using the context and the last Oid received as arguments
		/// </summary>
		/// <param name="queryContext"></param>
		/// <param name="lastOid"></param>
		/// <returns></returns>
		private DataTable GetNextDataBlock(IUQueryContext queryContext, Oid lastOid)
		{
			if (queryContext == null)
			{
				return null;
			}

			DataTable lData = null;

			try
			{
				IUPopulationContext populationContext = queryContext as IUPopulationContext;
				if (populationContext != null)
				{

					populationContext.LastOids.Clear();
					populationContext.LastOid = lastOid;

					// Depending on the ExchangeInfo Type
					switch (populationContext.ExchangeInformation.ExchangeType)
					{
						case ExchangeType.Navigation:
							if (populationContext.ExecutedFilter.Equals(""))
							{
								// If this Population doesn't contain filters, search all related population
								lData = Logic.ExecuteQueryRelated(populationContext);
							}
							else
							{
								// Search using the executed filter.
								lData = Logic.ExecuteQueryFilter(populationContext);
							}
							break;
						case ExchangeType.Action:
						case ExchangeType.SelectionForward:
							// If this Population doesn't contain filters, search all population
							if (populationContext.Filters.Count == 0)
							{
								lData = Logic.ExecuteQueryPopulation(populationContext);
							}
							else
							{
								// Search using the executed filter.
								if (!populationContext.ExecutedFilter.Equals(""))
								{
									lData = Logic.ExecuteQueryFilter(populationContext);
								}
							}
							break;
						default:
							lData = null;
							break;
					}

					// Assign the LastOid to the context
					populationContext.LastOid = Adaptor.ServerConnection.GetLastOid(lData);

				}
				else
				{
					// Instance
					lData = Logic.ExecuteQueryRelated(queryContext);
				}
			}
			catch (Exception logicException)
			{
				ScenarioManager.LaunchErrorScenario(logicException);
			}
			return lData;
		}
		/// <summary>
		/// Refresh the information in the Tree of the received Oid list
		/// </summary>
		/// <param name="nodeId">Node identifier.</param>
		/// <param name="oidList">List of Oid to be refreshed.</param>
		private void RefreshOidList(string nodeId, List<Oid> oidList)
		{
			if (oidList == null || oidList.Count == 0)
			{
				return;
			}
			// Get the proper controller and context.
			TreeNodeController treeNodeController = GetNodeById(nodeId);
			IUQueryContext queryContext = treeNodeController.QueryContext;
			DisplaySetInformation displaySetInfo = treeNodeController.DisplaySetInfo;

			foreach (Oid lOid in oidList)
			{
				RefreshInstance(queryContext, nodeId, lOid, displaySetInfo);
			}
		}
		/// <summary>
		/// Refresh the information in the Tree for one instance of the specified node id
		/// </summary>
		/// <param name="nodeId"></param>
		/// <param name="oid"></param>
		private void RefreshInstance(string nodeId, Oid oid)
		{
			if (oid == null)
			{
				return;
			}

			List<Oid> lOids = new List<Oid>();
			lOids.Add(oid);

			RefreshOidList(nodeId, lOids);
		}
		/// <summary>
		/// Refresh one instance using the context and Oid received as arguments.
		/// </summary>
		/// <param name="queryContext">Contexto del nodo.</param>
		/// <param name="nodeId">List of Oid to be refreshed.</param>
		/// <param name="oid">Oid of the instance to be refreshed.</param>
		/// <param name="displaySetInfo">DisplaySet information used to represent the instance data.</param>
		private void RefreshInstance(IUQueryContext queryContext, string nodeId, Oid oid, DisplaySetInformation displaySetInfo)
		{
			try
			{
				DataTable lInstance = Logic.ExecuteQueryInstance(Logic.Agent, oid, queryContext.DisplaySetAttributes);
				Tree.UpdateNodeValues(nodeId, oid, lInstance, displaySetInfo);
			}
			catch
			{
			}
		}
		/// <summary>
		/// Shows the side information related with the specified node id
		/// </summary>
		/// <param name="nodeId"></param>
		/// <param name="parentNodeOid"></param>
		/// <param name="oidPathList"></param>
		private void ShowFinalController(string nodeId, Oid parentNodeOid,  List<KeyValuePair<string, Oid>> oidPathList)
		{
			// One final node has been selected, show the related info

			// Hide all panels
			HideAllGroups();

			// Update all the context information
			if (oidPathList != null && oidPathList.Count > 0)
			{
				List<KeyValuePair<string, Oid>> lOidPathList = new List<KeyValuePair<string, Oid>>(oidPathList);
				lOidPathList.Reverse();
				UpdateAllNodeContextInformation(lOidPathList);
			}

			// Get the Node
			TreeNodeController lNodeController = GetNodeById(nodeId);
			if (lNodeController == null)
			{
				return;
			}

			// By default it is the parent node
			Oid lParentNodeOid = parentNodeOid;
			
			// If the node is root and final node at once, then the main master is an Instance IU.
			if (lNodeController.IsFinalNode() && lNodeController.ParentNodeId.Equals(""))
			{
				if (this.InstanceMasterController != null)
				{
					// Assign the oid selected in the Instance IU.
					if ((this.InstanceMasterController.InstancesSelected != null) && 
						(this.InstanceMasterController.InstancesSelected.Count > 0))
					{
						lParentNodeOid = this.InstanceMasterController.InstancesSelected[0];
					}
				}
			}
			if (lNodeController.UpdateFinalControllerData(lParentNodeOid))
			{
				// Show the group element
				GroupContainer.SetGroupVisible(lNodeController.NodeId);
			}
		}
		/// <summary>
		/// Return True if the received oid path corresponding with a root node
		/// </summary>
		/// <param name="lOidPathList"></param>
		/// <returns></returns>
		private bool IsRootNode(List<KeyValuePair<string, Oid>> oidPathList)
		{
			// No elements in the list
			if (oidPathList == null || oidPathList.Count == 0)
			{
				return false;
			}

			// If there is only one
			if (oidPathList.Count == 1)
			{
				return true;
			}

			// If the oid for all the elements is null
			foreach (KeyValuePair<string, Oid> lPair in oidPathList)
			{
				if (lPair.Value != null)
				{
					return false;
				}
			}

			return true;
		}
		/// <summary>
		/// Removes the intermendia elements with null Oid
		/// </summary>
		/// <param name="oidPathList"></param>
		/// <returns></returns>
		private List<KeyValuePair<string, Oid>> RemoveIntermediaPairsWithNullOid(List<KeyValuePair<string, Oid>> oidPathList)
		{
			List<KeyValuePair<string, Oid>> lPathList = new List<KeyValuePair<string, Oid>>();
			int lCounter = 0;
			foreach (KeyValuePair<string, Oid> lPair in oidPathList)
			{
				if (lPair.Value != null || lCounter == oidPathList.Count - 1)
				{
					lPathList.Add(lPair);
				}
				lCounter++;
			}

			return lPathList;
		}
		/// <summary>
		/// Returns the context corresponding with the received path
		/// </summary>
		/// <param name="completeOidPathList"></param>
		/// <returns></returns>
		private IUQueryContext GetQueryContextFromPath(List<KeyValuePair<string, Oid>> completeOidPathList)
		{
			if (completeOidPathList == null || completeOidPathList.Count == 0)
			{
				return null;
			}

			// Copy the original list
			List<KeyValuePair<string, Oid>> lOidPathList = new List<KeyValuePair<string, Oid>>(completeOidPathList);
			// Eliminate the intermedia elements with null oid
			lOidPathList = RemoveIntermediaPairsWithNullOid(lOidPathList);

			try
			{
				// Get the first node and 
				KeyValuePair<string, Oid> lPair = lOidPathList[0];
				foreach (TreeNodeController lTreeNodeController in RootNodes)
				{
					if (lTreeNodeController.NodeId.Equals(lPair.Key))
					{
						if (lOidPathList.Count > 1)
						{
							lOidPathList.RemoveAt(0);
							return GetQueryContextFromPath(lTreeNodeController, lOidPathList);
						}
						else
						{
							return lTreeNodeController.QueryContext;
						}
					}
				}
			}
			catch
			{
			}
			// Return null if has not been returned any value
			return null;
		}

		/// <summary>
		/// Returns the context corresponding with the received path, staring in the received Node Controller
		/// </summary>
		/// <param name="completeOidPathList"></param>
		/// <returns></returns>
		private IUQueryContext GetQueryContextFromPath(TreeNodeController treeNodeController, List<KeyValuePair<string, Oid>> oidPathList)
		{
			// The first element in the list must correspond with the NodeId of one subnode or recursive node
			KeyValuePair<string, Oid> lPair = oidPathList[0];

			// Recursive node
			foreach (TreeNodeController lTreeNodeController in treeNodeController.RecursiveNodes)
			{
				if (lTreeNodeController.NodeId.Equals(lPair.Key))
				{
					if (oidPathList.Count > 1)
					{
						oidPathList.RemoveAt(0);
						return GetQueryContextFromPath(lTreeNodeController.OriginalNode, oidPathList);
					}
					else
					{
						return lTreeNodeController.QueryContext;
					}
				}
			}

			// Standard node
			foreach (TreeNodeController lTreeNodeController in treeNodeController.SubNodes)
			{
				if (lTreeNodeController.NodeId.Equals(lPair.Key))
				{
					if (oidPathList.Count > 1)
					{
						oidPathList.RemoveAt(0);
						return GetQueryContextFromPath(lTreeNodeController, oidPathList);
					}
					else
					{
						return lTreeNodeController.QueryContext;
					}
				}
			}
			// Return null if has not been returned any value
			return null;
		}
		/// <summary>
		/// Hides all the groups in the group container
		/// </summary>
		private void HideAllGroups()
		{
			GroupContainer.HideAllGroups();
		}
		/// <summary>
		/// Initializes the IUTreeController
		/// </summary>
		public override void Initialize()
		{
			// Initializes the Group container
			if (GroupContainer != null)
			{
				GroupContainer.Initialize();
				InitializeGroupContainer();
			}

			// Hides all the elements in the group
			HideAllGroups();

			// Configures the Tree
			if (Tree != null)
			{
				ConfigureTree();
			}

			// Initilizes the root nodes
			foreach (TreeNodeController node in RootNodes)
			{
				node.Tree = Tree;
				node.Initialize();
			}

			// Initializes the Main Instance Master, is it exists
			if (InstanceMasterController != null)
			{
				InstanceMasterController.Initialize();
			}

			base.Initialize();

			// Load first data level
			LoadInitialData();
		}
		/// <summary>
		/// Refresh the selected node information 
		/// </summary>
		public void Refresh()
		{
			// Get the complete Oid path
			List<KeyValuePair<string, Oid>> lCompleteOidPath = Tree.GetCompleteSelectedOidPath();
			if (lCompleteOidPath == null || lCompleteOidPath.Count == 0 ||
				(lCompleteOidPath.Count == 1 && lCompleteOidPath[0].Value == null))
			{
				// If there are no Oids, load the data.
				LoadInitialData();
				return;
			}

            // Find the node controller of the last node in the list
            TreeNodeController lNodeController = GetNodeById(lCompleteOidPath[0].Key);
            if (lNodeController == null)
                return;

            // If no Oid in the first element, it is a intermedia node ...
            if (lCompleteOidPath[0].Value == null)
            {
                lCompleteOidPath.Reverse();
                UpdateBranch(lCompleteOidPath, lNodeController);
                return;
            }

            // There is an Oid in the first element, it is an instance node.
            // Refresh the instance and its final node (if it exists)
            if (lNodeController.FinalNodeQueryController != null)
            {
                lNodeController.FinalNodeQueryController.Refresh();
            }
            RefreshInstance(lCompleteOidPath[0].Key, lCompleteOidPath[0].Value);
		}
		/// <summary>
		/// This method allows the creation of recursive relations in the Tree
		/// Adds a new child node to an existing one that must be one of the parent nodes at any level
		/// The new child node will show related instances using the role path 
		/// </summary>
		/// <param name="parentNode">Parent node</param>
		/// <param name="newChildNode">New child node</param>
		/// <param name="rolePath">Role path</param>
		/// <param name="navigationalFilterId">Navigational filter id</param>
		/// <param name="alias">Alias to be shown in the grouping node</param>
		public void AddRecursiveNode(TreeNodeController parentNode, TreeNodeController newChildNode, string rolePath, string navigationalFilterId, string alias)
		{
			// Check if the new child node contains the parent node or it is the same
			// If not, it is not a recursive node
			bool lFound = parentNode == newChildNode;
			if (!lFound)
			{
				foreach (TreeNodeController lNodeController in newChildNode.SubNodes)
				{
					lFound = ContainsNodeController(lNodeController, parentNode);
					if (lFound)
					{
						break;
					}
				}
			}

			if (!lFound)
			{
				return;
			}

			// Parent node has a new subnode. All existing subnodes requires a grouping intermedia node
			foreach (TreeNodeController lSubNode in parentNode.SubNodes)
			{
				lSubNode.ShowGroupingNode = true;
			}

			// Add to the list
			ExchangeInfoNavigation lNavInfo = new ExchangeInfoNavigation(newChildNode.QueryContext.ClassName, "", rolePath, navigationalFilterId, null, null, "");
			TreeNodeController lNewNode = new TreeNodeController(newChildNode, lNavInfo);
			parentNode.RecursiveNodes.Add(lNewNode);

			// Notify to the Tree the new recursive node
			Tree.AddRecursiveNode(parentNode.NodeId, newChildNode.NodeId, alias);
		}
		/// <summary>
		/// Adds a new refesh popup menu item to the controller
		/// </summary>
		/// <param name="refreshMenuItem"></param>
		public void AddRefreshMenuItem(ITriggerPresentation refreshMenuItem)
		{
			mRefreshMenuItems.Add(refreshMenuItem);
			// Suscribe to refresh event
			refreshMenuItem.Triggered += new EventHandler<TriggerEventArgs>(HandleRefreshMenuItemTriggered);
		}
		/// <summary>
		/// Assing the text in the proper language to the contained elements
		/// </summary>
		public override void ApplyMultilanguage()
		{
			// Main Instance Controller
			if (InstanceMasterController != null)
			{
				InstanceMasterController.ApplyMultilanguage();
			}

			// For each node
			foreach (TreeNodeController lNode in RootNodes)
			{
				lNode.ApplyMultilanguage();
			}

			// Refresh menu items
			foreach (ITriggerPresentation refreshMenuItem in mRefreshMenuItems)
			{
				refreshMenuItem.Value = CultureManager.TranslateString(LanguageConstantKeys.L_POP_UP_MENU_REFRESH, LanguageConstantValues.L_POP_UP_MENU_REFRESH);
			}

			base.ApplyMultilanguage();
		}
		#endregion Methods
	}
}
