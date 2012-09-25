// v3.8.4.5.b
using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using SIGEM.Client.Oids;
using SIGEM.Client.Controllers;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of the .NET TreeView control.
	/// </summary>
	public class TreeViewPresentation
	{
		#region Members
		/// <summary>
		/// Flag to avoid the raising events.
		/// </summary>
		private bool mRaiseEventsFlag;
		/// <summary>
		/// .Net TreeView instance reference.
		/// </summary>
		protected TreeView mTreeViewIT;
		/// <summary>
		/// Intermedia Tree nodes collection.
		/// </summary>
		private Dictionary<string, NodeInfo> mIntermediaNodes = new Dictionary<string, NodeInfo>();
		/// <summary>
		/// Recursive Tree nodes list.
		/// </summary>
		private List<RecursiveNodeInfo> mRecursiveNodes = new List<RecursiveNodeInfo>();
		/// <summary>
		/// NextBlock image reference.
		/// </summary>
		private string mNextBlockImageKey;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'TreeViewPresentation'.
		/// </summary>
		/// <param name="treeView">TreeView instance.</param>
		public TreeViewPresentation(TreeView treeView)
		{
			mRaiseEventsFlag = true;
			mTreeViewIT = treeView;
			if (mTreeViewIT != null)
			{
				mTreeViewIT.AfterSelect += new TreeViewEventHandler(HandleTreeViewITAfterSelect);
				mTreeViewIT.AfterExpand += new TreeViewEventHandler(HandleTreeViewITAfterExpand);
				mTreeViewIT.MouseDown += new MouseEventHandler(HandleTreeViewITMouseDown);
				mTreeViewIT.KeyDown += new KeyEventHandler(HandleTreeViewITKeyDown);
				mTreeViewIT.ContextMenuStripChanged += new EventHandler(HandleTreeViewITContextMenuStripChanged);
			}
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets Enabled property.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return mTreeViewIT.Enabled;
			}
			set
			{
				mTreeViewIT.Enabled = value;
			}
		}
		/// <summary>
		/// Gets or sets the selected Oids.
		/// </summary>
		public List<Oid> Values
		{
			get
			{
				return GetSelectedOIDs();
			}
			set
			{
			}
		}
		/// <summary>
		/// Gets or sets Visible property.
		/// </summary>
		public bool Visible
		{
			get
			{
				return mTreeViewIT.Visible;
			}
			set
			{
				mTreeViewIT.Visible = value;
			}
		}
		/// <summary>
		/// Gets or sets NextBlockImage property.
		/// </summary>
		public string NextBlockImageKey
		{
			get
			{
				return mNextBlockImageKey;
			}
			set
			{
				mNextBlockImageKey = value;
			}
		}
		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when an InstanceNode is selected.
		/// </summary>
		public event EventHandler<InstanceNodeSelectedEventArgs> InstanceNodeSelected;
		/// <summary>
		/// Occurs when an IntermediaNode is selected.
		/// </summary>
		public event EventHandler<IntermediaNodeSelectedEventArgs> IntermediaNodeSelected;
		/// <summary>
		/// Occurs when the IntermediaNode information is required.
		/// </summary>
		public event EventHandler<SearchIntermediaNodeDataEventArgs> SearchIntermediaNodeData;
		/// <summary>
		/// Occurs when a FinalNode is selected.
		/// </summary>
		public event EventHandler<FinalNodeSelectedEventArgs> FinalNodeSelected;
		/// <summary>
		/// Occurs when Next data block is returned.
		/// </summary>
		public event EventHandler<GetNextNodeDataBlockEventArgs> GetNextDataBlock;
		/// <summary>
		/// Execute Command event Implementation.
		/// </summary>
		public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Executes actions related to after node selection.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleTreeViewITAfterSelect(object sender, TreeViewEventArgs e)
		{
			ProcessTreeViewITAfterSelect(sender, e);
		}
		/// <summary>
		/// Executes actions related to after node expansion.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void HandleTreeViewITAfterExpand(object sender, TreeViewEventArgs e)
		{
			ProcessTreeViewITAfterExpand(sender, e);
		}
		/// <summary>
		/// Executes actions related to MouseDown event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleTreeViewITMouseDown(object sender, MouseEventArgs e)
		{
			ProcessTreeViewITMouseDown(sender, e);
		}
		/// <summary>
		/// Executes actions related to KeyDown event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleTreeViewITKeyDown(object sender, KeyEventArgs e)
		{
			ProcessTreeViewITKeyDown(sender, e);
		}
		/// <summary>
		/// When Context menu changes, suscribe to Opening event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleTreeViewITContextMenuStripChanged(object sender, EventArgs e)
		{
			if (mTreeViewIT.ContextMenuStrip != null)
			{
				mTreeViewIT.ContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(HandleTreeViewITContextMenuStripOpening);
			}
		}
		/// <summary>
		/// If no root node selected, cancel the Open action
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleTreeViewITContextMenuStripOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (mTreeViewIT.Nodes.Count == 0)
				return;

			if (mTreeViewIT.SelectedNode != null )
				return;

			e.Cancel = true;
		}
		#endregion Event Handlers

		#region Process Events
		/// <summary>
		/// Process the action related to after node selection event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProcessTreeViewITAfterSelect(object sender, TreeViewEventArgs e)
		{
			ProcessNodeSelected(e.Node);
		}
		/// <summary>
		/// Process the action related to after node expansion event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProcessTreeViewITAfterExpand(object sender, TreeViewEventArgs e)
		{
			mTreeViewIT.SelectedNode = e.Node;
		}
		/// <summary>
		/// Process the action related to MouseDown event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProcessTreeViewITMouseDown(object sender, MouseEventArgs e)
		{
			// Select node with the Mouse right button
			if (e.Button == MouseButtons.Right)
			{
				TreeNode treeNode = mTreeViewIT.GetNodeAt(e.X, e.Y);
				if (treeNode != null && treeNode != mTreeViewIT.SelectedNode)
				{
					mTreeViewIT.SelectedNode = treeNode;
				}
			}
		}
		/// <summary>
		/// Process the action related to KeyDown event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProcessTreeViewITKeyDown(object sender, KeyEventArgs e)
		{
			// Press Refresh HotKey over a node
			if (e.KeyCode == Keys.F5)
			{
				if (mTreeViewIT.SelectedNode != null)
				{
					// Propagate the event
					OnExecuteCommand(new ExecuteCommandEventArgs(ExecuteCommandType.ExecuteRefresh));
				}
			}
		}

		#endregion Process Events

		#region Event Raisers
		/// <summary>
		/// Raises the IntermediaNodeSelected event
		/// </summary>
		/// <param name="args"></param>
		private void OnIntermediaNodeSelected(IntermediaNodeSelectedEventArgs args)
		{
			if (!mRaiseEventsFlag)
			{
				return;
			}

			EventHandler<IntermediaNodeSelectedEventArgs> handler = IntermediaNodeSelected;

			if (handler != null)
			{
				handler(this, args);
			}
		}
		/// <summary>
		/// Raises the InstanceNodeSelected event
		/// </summary>
		/// <param name="args"></param>
		private void OnInstanceNodeSelected(InstanceNodeSelectedEventArgs args)
		{
			if (!mRaiseEventsFlag)
			{
				return;
			}

			EventHandler<InstanceNodeSelectedEventArgs> handler = InstanceNodeSelected;

			if (handler != null)
			{
				handler(this, args);
			}
		}
		/// <summary>
		/// Raises the FinalNodeSelected event
		/// </summary>
		/// <param name="args"></param>
		private void OnFinalNodeSelected(FinalNodeSelectedEventArgs args)
		{
			if (!mRaiseEventsFlag)
			{
				return;
			}

			EventHandler<FinalNodeSelectedEventArgs> handler = FinalNodeSelected;

			if (handler != null)
			{
				handler(this, args);
			}
		}
		/// <summary>
		/// Raises the SearchIntermediaNodeData event
		/// </summary>
		/// <param name="args"></param>
		private void OnSearchIntermediaNodeData(SearchIntermediaNodeDataEventArgs args)
		{
			if (!mRaiseEventsFlag)
			{
				return;
			}

			EventHandler<SearchIntermediaNodeDataEventArgs> handler = SearchIntermediaNodeData;

			if (handler != null)
			{
				handler(this, args);
			}
		}
		/// <summary>
		/// Raises the GetNextDataBlock event
		/// </summary>
		/// <param name="args"></param>
		private void OnGetNextDataBlock(GetNextNodeDataBlockEventArgs args)
		{
			if (!mRaiseEventsFlag)
			{
				return;
			}

			EventHandler<GetNextNodeDataBlockEventArgs> handler = GetNextDataBlock;

			if (handler != null)
			{
				handler(this, args);
			}
		}
		/// <summary>
		/// Raises the ExecuteCommand event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnExecuteCommand(ExecuteCommandEventArgs eventArgs)
		{
			if (!mRaiseEventsFlag)
			{
				return;
			}

			EventHandler<ExecuteCommandEventArgs> handler = ExecuteCommand;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		#endregion Event Raisers

		#region Methods
		/// <summary>
		/// Executes the selected node processing.
		/// </summary>
		/// <param name="treeNode">TreeView node.</param>
		private void ProcessNodeSelected(TreeNode treeNode)
		{
			InstanceNodeInfo lInstanceInfo = null;
			List<KeyValuePair<string, Oid>> lCompleteOidPath = GetCompleteOidPath(treeNode);

			// Next block of instances
			if (treeNode.Text.Equals("..."))
			{
				// Get the last Oid
				Oid lLastOid = null;
				if (treeNode.PrevNode != null && treeNode.PrevNode.Tag != null)
				{
					lInstanceInfo = treeNode.PrevNode.Tag as InstanceNodeInfo;
					if (lInstanceInfo != null)
					{
						lLastOid = lInstanceInfo.InstanceOid;
					}
				}

				GetNextNodeDataBlockEventArgs getNextBlockArgs = new GetNextNodeDataBlockEventArgs(treeNode.Name, lLastOid, lCompleteOidPath);
				OnGetNextDataBlock(getNextBlockArgs);
				TreeNode parentNode = treeNode.Parent;
				// Delete the special node
				mRaiseEventsFlag = false;
				mTreeViewIT.Nodes.Remove(treeNode);
				mRaiseEventsFlag = true;
				// Add the received data to the parent node
				AddDataToNode(parentNode,treeNode.Name, getNextBlockArgs.Data, getNextBlockArgs.DisplaySetInfo, getNextBlockArgs.LastBlock, false);
				return;
			}

			// Instance or intermadia node ?

			// Raise the proper selected event
			if (treeNode.Tag != null && (treeNode.Tag as InstanceNodeInfo) != null)
			{
				ProcessInstanceNodeSelected(treeNode);
			}
			else
			{
				ProcessNoInstanceNodeSelected(treeNode);
			}
			mTreeViewIT.Focus();
		}

		private void ProcessNoInstanceNodeSelected(TreeNode treeNode)
		{
			// It is not an instance node. It can be a final or an intermedia one
			List<KeyValuePair<string, Oid>> lCompleteOidPath = GetCompleteOidPath(treeNode);
			InstanceNodeInfo lInstanceInfo = null;

			// Check if it is a final one
			if (IsFinalNode(treeNode))
			{
				Oid parentNodeOid = null;
				if (treeNode.Parent != null)
				{
					lInstanceInfo = treeNode.Parent.Tag as InstanceNodeInfo;
				}
				if (lInstanceInfo != null)
				{
					parentNodeOid = lInstanceInfo.InstanceOid;
				}
				OnFinalNodeSelected(new FinalNodeSelectedEventArgs(treeNode.Name, parentNodeOid, lCompleteOidPath));
				return;
			}

			// It is not a final node, it is a intermedia
			OnIntermediaNodeSelected(new IntermediaNodeSelectedEventArgs(treeNode.Name, lCompleteOidPath));

			// Check if more data are required
			if (treeNode.Nodes.Count == 1 &&
				treeNode.Nodes[0].Text.Equals("..."))
			{
				// Get the related data
				Oid lOid = null;
				string lNodeName = "";
				if (treeNode.Parent != null && treeNode.Parent.Tag != null)
				{
					lInstanceInfo = treeNode.Parent.Tag as InstanceNodeInfo;
					if (lInstanceInfo != null)
					{
						lOid = lInstanceInfo.InstanceOid;
					}
				}
				lNodeName = treeNode.Name;
				// Search more data for the IntermediaNode
				SearchIntermediaNodeDataEventArgs args = new SearchIntermediaNodeDataEventArgs(lOid, lNodeName, lCompleteOidPath);
				OnSearchIntermediaNodeData(args);
				// Add the received data to node
				AddDataToNode(treeNode, lNodeName, args.Data, args.DisplaySetInfo, args.LastBlock, true);
			}
		}

		private void ProcessInstanceNodeSelected(TreeNode treeNode)
		{
			InstanceNodeInfo lInstanceInfo = treeNode.Tag as InstanceNodeInfo;
			List<KeyValuePair<string, Oid>> lCompleteOidPath = GetCompleteOidPath(treeNode);
			List<Oid> lOidList = new List<Oid>();
			lOidList.Add(lInstanceInfo.InstanceOid);

			// Check if more data are required
			if (treeNode.Nodes.Count == 1 &&
				treeNode.Nodes[0].Text.Equals("..."))
			{
				// Search more data for the IntermediaNode
				lCompleteOidPath.Insert(0, new KeyValuePair<string, Oid>(treeNode.Nodes[0].Name, null));
				SearchIntermediaNodeDataEventArgs args = new SearchIntermediaNodeDataEventArgs(lInstanceInfo.InstanceOid, treeNode.Nodes[0].Name, lCompleteOidPath);
				OnSearchIntermediaNodeData(args);
				// Add the received data to node
				AddDataToNode(treeNode, treeNode.Nodes[0].Name, args.Data, args.DisplaySetInfo, args.LastBlock, true);
			}

			OnInstanceNodeSelected(new InstanceNodeSelectedEventArgs(lOidList, treeNode.Name, lCompleteOidPath));
		}
		/// <summary>
		/// Returns the list with all the Oid and its Id for the complete path of the received node
		/// </summary>
		/// <returns></returns>
		private List<KeyValuePair<string, Oid>> GetCompleteOidPath(TreeNode treeNode)
		{
			List<KeyValuePair<string, Oid>> lCompleteOidPath = new List<KeyValuePair<string,Oid>>();
			if (treeNode == null)
			{
				return lCompleteOidPath;
			}

			InstanceNodeInfo lInstanceNodeInfo = treeNode.Tag as InstanceNodeInfo;
			if (lInstanceNodeInfo != null)
			{
				// Add the Oid
				lCompleteOidPath.Add(new KeyValuePair<string, Oid>(treeNode.Name, lInstanceNodeInfo.InstanceOid));
			}
			else
			{
				// Add the nodeId of the intermedia node
				lCompleteOidPath.Add(new KeyValuePair<string, Oid>(treeNode.Name, null));
			}

			lCompleteOidPath.AddRange(GetCompleteOidPath(treeNode.Parent));
			return lCompleteOidPath;
		}
		/// <summary>
		/// Returns the list with all the Oid and its Id for the complete path of the selected node
		/// </summary>
		/// <returns></returns>
		public List<KeyValuePair<string, Oid>> GetCompleteSelectedOidPath()
		{
			return GetCompleteOidPath(mTreeViewIT.SelectedNode);
		}
		/// <summary>
		/// Returns true or false if exists the list for the complete path of the selected node.
		/// </summary>
		/// <returns></returns>
		public bool ExistCompleteOidPath(List<KeyValuePair<string, Oid>> oidPathList)
		{
			return GetNodeWithCompleteOidPath(oidPathList) != null;
		}
		/// <summary>
		/// Gets the complete list with Oid and its Id for the complete path of a node.
		/// </summary>
		/// <returns></returns>
		private TreeNode GetNodeWithCompleteOidPath(List<KeyValuePair<string, Oid>> oidPathList)
		{
			if (oidPathList == null || oidPathList.Count == 0)
			{
				return null;
			}

			// Copy the list
			List<KeyValuePair<string, Oid>> lOidPathList = new List<KeyValuePair<string, Oid>>(oidPathList);

			// Search the Oid in the nodes with the NodeId
			KeyValuePair<string, Oid> lPair = lOidPathList[0];
			TreeNode[] lTreeNodes = mTreeViewIT.Nodes.Find(lPair.Key, false);
			foreach (TreeNode lNode in lTreeNodes)
			{
				Oid lOid = GetOidNode(lNode);
				if (Oid.Equals(lOid, lPair.Value))
				{
					if (oidPathList.Count == 1)
					{
						return lNode;
					}
					// Go to the next level
					lOidPathList.RemoveAt(0);
					return GetSubNodeWithCompleteOidPath(lNode, lOidPathList);
				}
			}
			// Return null if the complete path has not been returned
			return null;
		}
		/// <summary>
		/// Gets the complete list with Oid and its Id for the complete path of a Subnode.
		/// </summary>
		/// <returns></returns>
		private TreeNode GetSubNodeWithCompleteOidPath(TreeNode node, List<KeyValuePair<string, Oid>> oidPathList)
		{
			// Search the Oid in the nodes with the NodeId
			KeyValuePair<string, Oid> lPair = oidPathList[0];
			TreeNode[] lTreeNodes = node.Nodes.Find(lPair.Key, false);
			foreach (TreeNode lNode in lTreeNodes)
			{
				Oid lOid = GetOidNode(lNode);
				if (Oid.Equals(lOid, lPair.Value))
				{
					if (oidPathList.Count == 1)
					{
						return lNode;
					}
					// Go to the next level
					oidPathList.RemoveAt(0);
					return GetSubNodeWithCompleteOidPath(lNode, oidPathList);
				}
			}
			// Return null if Subnode information has not been returned
			return null;
		}
		/// <summary>
		/// Adds the data corresponding to a node.
		/// </summary>
		/// <param name="treeNode">Tree node where it is going to be added the data.</param>
		/// <param name="data">DataTable containing the data to be added.</param>
		/// <param name="displaySetInfo">Display set information of the suitable node.</param>
		/// <param name="lastBlock">Boolean indicating if there is more data to show.</param>
		/// <param name="deleteExistingData">Boolean indicating if the loaded data will be deleted or not.</param>
		private void AddDataToNode(TreeNode treeNode, string lNodeName, DataTable data, DisplaySetInformation displaySetInfo, bool lastBlock, bool deleteExistingData)
		{
			mRaiseEventsFlag = false;

			// Will be true if is an IntermediaNode
			if (deleteExistingData)
			{
				if (treeNode == null)
				{
					mTreeViewIT.Nodes.Clear();
				}
				else
				{
					treeNode.Nodes.Clear();
				}
			}

			// No data for adding
			if (data == null || data.Rows.Count == 0)
			{
				mRaiseEventsFlag = true;
				return;
			}

			// To add data
			try
			{
				List<DataColumn> displaySetColumns = Adaptor.ServerConnection.GetDisplaySetColumns(data);

				if (displaySetColumns == null)
					return;

				// Search in the data table node information.
				foreach (System.Data.DataRow lRow in data.Rows)
				{

					string lTextForNode = GetTextForNode(displaySetInfo, lRow);

					// Sets the value
					TreeNode lNode = null;
					if (treeNode == null)
					{
						lNode = mTreeViewIT.Nodes.Add(lTextForNode);
						lNode.Name = "0";
					}
					else
					{
						lNode = treeNode.Nodes.Add(lTextForNode);
						lNode.Name = lNodeName;
					}

					Oid lOid = Adaptor.ServerConnection.GetOid(data, lRow);
					InstanceNodeInfo instanceInfo = new InstanceNodeInfo(lOid);
					lNode.Tag = instanceInfo;

					// Sets the node image
					string imageKey = GetImageKeyForNode(lNode.Name, false);
					if (!imageKey.Equals(""))
					{
						lNode.ImageKey = imageKey;
						lNode.SelectedImageKey = imageKey;
					}

					// Sets the node contextual menu
					ContextMenuStrip menu = GetMenuForNode(lNode);
					lNode.ContextMenuStrip = menu;

					// Search in Subnodes
					AddDetailsNodes(lNode);
				}

				// If it is not the last block, add the final special node
				if (!lastBlock)
				{
					if (treeNode == null)
					{
						TreeNode lNextBlockNode = mTreeViewIT.Nodes.Add("0", "...");
						lNextBlockNode.ImageKey = NextBlockImageKey;
					}
					else
					{
						TreeNode lNextBlockNode = treeNode.Nodes.Add(treeNode.Name, "...");
						lNextBlockNode.ImageKey = NextBlockImageKey;
					}
				}

			}
			catch
			{
			}

			mRaiseEventsFlag = true;
		}
		/// <summary>
		/// Gets the image for nodes.
		/// </summary>
		/// /// <returns></returns>
		private string GetImageKeyForNode(string nodeId, bool forGroup)
		{
			if (nodeId.Equals(""))
			{
				foreach (NodeInfo lNodeInfo in mIntermediaNodes.Values)
				{
					if (lNodeInfo.ParentNodeId.Equals(""))
					{
						if (forGroup)
						{
							return lNodeInfo.GroupImageKey;
						}
						else
						{
							return lNodeInfo.ImageKey;
						}
					}
				}

				return "";
			}

			NodeInfo lNodeInformation = mIntermediaNodes[nodeId];
			if (lNodeInformation != null)
			{
				if (forGroup)
				{
					return lNodeInformation.GroupImageKey;
				}
				else
				{
					return lNodeInformation.ImageKey;
				}
			}

			return "";
		}

		/// <summary>
		/// Returns true if the node is a final one.
		/// </summary>
		/// <param name="treeNode"></param>
		/// <returns></returns>
		private bool IsFinalNode(TreeNode treeNode)
		{
			bool finalNode = false;

			if (treeNode.Name != "")
			{
				finalNode = mIntermediaNodes[treeNode.Name].FinalNode;
			}

			return finalNode;
		}

		/// <summary>
		/// Gets the text to show in a node.
		/// </summary>
		/// <param name="displaySetInfo">Contains the Display Set information to format the data.</param>
		/// <param name="row">Contains the data to be formatted and then showed in a node.</param>
		/// <returns>A string formatted according the data type or the suitable label in case of Defined Selection.</returns>
		private string GetTextForNode(DisplaySetInformation displaySetInfo, DataRow row)
		{
			StringBuilder lAux = new StringBuilder();

			// Retrieves the information from the DisplaySetInfo.
			foreach (DisplaySetItem displaySetItem in displaySetInfo.DisplaySetItems)
			{
				if (displaySetItem.Visible)
				{
					if (lAux.Length != 0)
					{
						lAux.Append(" ");
					}

					// In case of containing a null element nothing is done,
					// Otherwise the info of the current column is append to the final column
					if (row[displaySetItem.Name].GetType() != typeof(System.DBNull))
					{
						if ((displaySetItem.DefinedSelectionOptions != null) && (displaySetItem.DefinedSelectionOptions.Count > 0))
						{
							// The Display Set item has a Defined Selection pattern associated.
							string dsItemValue = string.Empty;
							foreach (KeyValuePair<object, string> dsOption in displaySetItem.DefinedSelectionOptions)
							{
								if (row[displaySetItem.Name].Equals(dsOption.Key))
								{
									dsItemValue = dsOption.Value;
									break;
								}
							}
							lAux.Append(dsItemValue);
						}
						else
						{
							// A normal Display Set item.
							lAux.Append(DefaultFormats.ApplyDisplayFormat(row[displaySetItem.Name], displaySetItem.ModelType));
						}
					}
				}
			}

			return lAux.ToString();
		}
		/// <summary>
		/// Gets the contextual menu for the node.
		/// </summary>
		/// /// <returns></returns>
		private ContextMenuStrip GetMenuForNode(TreeNode node)
		{
			string lNodeId = "0";
			if (node != null)
			{
				lNodeId = node.Name;
			}
			NodeInfo nodeInfo = mIntermediaNodes[lNodeId];
			if (nodeInfo != null)
			{
				return nodeInfo.Menu;
			}
			return null;
		}
		/// <summary>
		/// Adds the data corresponding to a Subnode.
		/// </summary>
		private void AddDetailsNodes(TreeNode parentNode)
		{
			// Intermedia nodes
			foreach (NodeInfo lNodeInfo in mIntermediaNodes.Values)
			{
				if (parentNode.Name == lNodeInfo.ParentNodeId)
				{
					// Intermedia grouping node is mandatory
					if (lNodeInfo.HasIntermediaGroupingNode)
					{
						TreeNode lNode = parentNode.Nodes.Add(lNodeInfo.Alias);
						lNode.Name = lNodeInfo.NodeId;
						if (!lNodeInfo.FinalNode)
						{
							lNode.Nodes.Add("...");
						}
						// Sets the contextual menu
						lNode.ContextMenuStrip = lNodeInfo.Menu;

						// Sets the image key
						lNode.ImageKey = GetImageKeyForNode(lNodeInfo.NodeId, true);
						lNode.SelectedImageKey = GetImageKeyForNode(lNodeInfo.NodeId, true);
					}
					else
					{
						// Only one chid node. No intermedia node is required
						TreeNode lNode = parentNode.Nodes.Add("...");
						lNode.Name = lNodeInfo.NodeId;
						// Sets the contextual menu
						lNode.ContextMenuStrip = lNodeInfo.Menu;

						// Sets the image key
						lNode.ImageKey = GetImageKeyForNode(lNodeInfo.NodeId, true);
						lNode.SelectedImageKey = GetImageKeyForNode(lNodeInfo.NodeId, true);
					}
				}
			}

			// Recursive nodes
			foreach (RecursiveNodeInfo lRecursiveNodeInfo in mRecursiveNodes)
			{
				if (parentNode.Name == lRecursiveNodeInfo.ParentNodeId)
				{
					NodeInfo lNodeInfo = mIntermediaNodes[lRecursiveNodeInfo.NodeId];
					TreeNode lNode = parentNode.Nodes.Add(lRecursiveNodeInfo.Alias);
					lNode.Name = lNodeInfo.NodeId;
					if (!lNodeInfo.FinalNode)
					{
						lNode.Nodes.Add("...");
					}
					// Sets the contextual menu
					lNode.ContextMenuStrip = lNodeInfo.Menu;

					// Set the image key
					lNode.ImageKey = GetImageKeyForNode(lNodeInfo.NodeId, true);
					lNode.SelectedImageKey = GetImageKeyForNode(lNodeInfo.NodeId, true);
				}
			}
		}

		/// <summary>
		/// Returns the Oid list from the selected rows
		/// </summary>
		/// <returns>List of Oids.</returns>
		private List<Oid> GetSelectedOIDs()
		{
			try
			{
				if (mTreeViewIT.SelectedNode == null)
				{
					return null;
				}

				Oid lOid = null;
				if (mTreeViewIT.SelectedNode.Tag != null)
				{
					InstanceNodeInfo instanceInfo = mTreeViewIT.SelectedNode.Tag as InstanceNodeInfo;
					if (instanceInfo != null)
					{
						lOid = instanceInfo.InstanceOid;
					}
				}

				if (lOid == null)
				{
					return null;
				}

				List<Oid> oidList = new List<Oid>();
				oidList.Add(lOid);
				return oidList;
			}
			catch
			{
			}

			return null;
		}

		/// <summary>
		/// Adds the data corresponding to a IntermediaNode.
		/// </summary>
		/// <param name="parentId"></param>
		/// <param name="nodeId"></param>
		/// <param name="alias"></param>
		/// <param name="menu"></param>
		/// <param name="finalNode"></param>
		/// <param name="imageKey"></param>
		/// <param name="groupImageKey"></param>
		/// <param name="hasIntermediaGroupingNode"></param>
		public void AddIntermediaNode(string parentId, string nodeId, string alias, ContextMenuStrip menu, bool finalNode, string imageKey, string groupImageKey, bool hasIntermediaGroupingNode)
		{
			NodeInfo node = new NodeInfo(parentId, nodeId, alias, menu, finalNode, imageKey, groupImageKey, hasIntermediaGroupingNode);
			mIntermediaNodes.Add(nodeId, node);
		}

		/// <summary>
		/// Adds the data corresponding to a RecursiveNode.
		/// </summary>
		/// <param name="parentId"></param>
		/// <param name="nodeId"></param>
		/// <param name="alias"></param>
		public void AddRecursiveNode(string parentId, string nodeId, string alias)
		{
			RecursiveNodeInfo lRecursiveNodeInfo = new RecursiveNodeInfo(alias, parentId, nodeId);
			mRecursiveNodes.Add(lRecursiveNodeInfo);
		}

		/// <summary>
		/// Shows data for grouping root node.
		/// </summary>
		/// <param name="nodeId">Node identifier.</param>
		/// <param name="data">DataTable object with the data to be showed.</param>
		/// <param name="displaySetInfo">Display Set information to format the data in the node.</param>
		/// <param name="lastBlock">Boolean indicating if there is more data to be showed.</param>
		/// <param name="showGroupingNode">Boolean indicating if the root node has to be collapsed (deleting its subnodes).</param>
		public void ShowDataInRootNode(string nodeId, DataTable data, DisplaySetInformation displaySetInfo, bool lastBlock, bool showGroupingNode)
		{
			NodeInfo nodeInfo = mIntermediaNodes[nodeId];

			// If group node must be shown, find it and delete its subnodes
			TreeNode lParentNode = null;
			if (showGroupingNode)
			{
				TreeNode[] lGroupNodes = mTreeViewIT.Nodes.Find(nodeId, false);
				if (lGroupNodes.Length == 0)
				{
					lParentNode = mTreeViewIT.Nodes.Add(nodeId, nodeInfo.Alias);
				}
				else
				{
					// Get the first one
					lParentNode = lGroupNodes[0];
				}
				lParentNode.ImageKey = nodeInfo.GroupImageKey;
				lParentNode.SelectedImageKey = nodeInfo.GroupImageKey;

				// Set contextual menu for grouping root node
				ContextMenuStrip menu = GetMenuForNode(lParentNode);
				lParentNode.ContextMenuStrip = menu;
			}

			// Add the data node
			AddDataToNode(lParentNode, nodeId, data, displaySetInfo, lastBlock, true);

			// If no data and Grouping node, add the ... node
			if (!nodeInfo.FinalNode && lParentNode != null && data == null)
			{
				TreeNode lNextBlockNode = lParentNode.Nodes.Add(nodeId, "...");
				lNextBlockNode.ImageKey = NextBlockImageKey;
			}
		}

		/// <summary>
		/// Updates the information for one instance in the tree.
		/// </summary>
		/// <param name="nodeId">Node identifier.</param>
		/// <param name="oid">Oid of the instance to be updated.</param>
		/// <param name="data">New data.</param>
		/// <param name="displaySetInfo">Display Set information used to represent the data in the node.</param>
		public void UpdateNodeValues(string nodeId, Oid oid, DataTable data, DisplaySetInformation displaySetInfo)
		{
			// Get the nodes with the same Id
			TreeNode[] lNodes = mTreeViewIT.Nodes.Find(nodeId, true);
			bool lSelectionChange = false;

			// Locate the node with the same Oid
			foreach (TreeNode lNode in lNodes)
			{
				if (lNode.Tag != null)
				{
					InstanceNodeInfo instanceNodeInfo = lNode.Tag as InstanceNodeInfo;
					if (instanceNodeInfo != null)
					{
						if (instanceNodeInfo.InstanceOid.Equals(oid))
						{
							if (data == null || data.Rows.Count == 0)
							{
								// If the instance doesn't exist, delete the node
								mRaiseEventsFlag = false;
								TreeNode lPreviousSelected = mTreeViewIT.SelectedNode;
								mTreeViewIT.Nodes.Remove(lNode);
								mRaiseEventsFlag = true;
								TreeNode lCurrentSelected = mTreeViewIT.SelectedNode;
								if (!UtilFunctions.ObjectsEquals(lCurrentSelected, lPreviousSelected))
								{
									lSelectionChange = true;
								}
							}
							else
							{
								// Update the node values.
								lNode.Text = GetTextForNode(displaySetInfo, data.Rows[0]);
								instanceNodeInfo.InstanceOid = Adaptor.ServerConnection.GetOid(data, data.Rows[0]);
								// Clear subnodes
								lNode.Nodes.Clear();
								// Add the Deatils nodes
								AddDetailsNodes(lNode);
							}
						}
					}
				}
			}

			// If selected node has been modified, raise the event with the node selected
			if (lSelectionChange && mTreeViewIT.SelectedNode != null)
			{
				ProcessNodeSelected(mTreeViewIT.SelectedNode);
			}
		}
		/// <summary>
		/// Refreshes data for a root node.
		/// </summary>
		/// <param name="oidPathList">List of Oids of the suitable branch.</param>
		/// <param name="data">Data to be showed.</param>
		/// <param name="displaySetInfo">DisplaySet information to format the data.</param>
		/// <param name="lastBlock">Boolean indicating if there are more data to be retrieved.</param>
		public void RefreshBranch(List<KeyValuePair<string, Oid>> oidPathList, DataTable data, DisplaySetInformation displaySetInfo, bool lastBlock)
		{
			// Locate the parent node
			TreeNode lNode = GetNodeWithCompleteOidPath(oidPathList);
			if (lNode == null)
			{
				return;
			}

			// Add the new data
			AddDataToNode(lNode, lNode.Name, data, displaySetInfo, lastBlock, true);
		}
		/// <summary>
		/// Returns the Parent node Oid of the specifies node
		/// </summary>
		/// <param name="nodeId"></param>
		/// <param name="oid"></param>
		/// <returns></returns>
		public List<Oid> GetOidParentNodes(string nodeId, Oid oid)
		{
			List<Oid> lOidList = new List<Oid>();
			TreeNode[] lNodes = mTreeViewIT.Nodes.Find(nodeId, true);

			// Locate the node with the same Oid
			foreach (TreeNode node in lNodes)
			{
				if (node.Tag == null)
				{
					// It is a grouping node
					Oid parentOid = GetOidParentNode(node);
					if (oid.Equals(parentOid))
					{
						lOidList.Add(parentOid);
					}
				}
				else
				{
					InstanceNodeInfo instanceNodeInfo = node.Tag as InstanceNodeInfo;
					if (instanceNodeInfo != null)
					{
						if (instanceNodeInfo.InstanceOid.Equals(oid))
						{
							// Get the Oid of the parent node
							Oid parentOid = GetOidParentNode(node);
							if (parentOid != null)
							{
								lOidList.Add(parentOid);
							}
						}
					}
				}
			}

			return lOidList;
		}

		/// <summary>
		/// Clear all nodes in the Tree
		/// </summary>
		public void Clear()
		{
			mTreeViewIT.Nodes.Clear();
		}

		/// <summary>
		/// Returns the Oid of the parent node
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		private Oid GetOidParentNode(TreeNode node)
		{
			if (node == null || node.Parent == null)
			{
				return null;
			}

			InstanceNodeInfo instanceNodeInfo = node.Parent.Tag as InstanceNodeInfo;
			if (instanceNodeInfo == null)
			{
				return GetOidParentNode(node.Parent);
			}

			return instanceNodeInfo.InstanceOid;
		}
		/// <summary>
		/// Returns the Oid of a node
		/// </summary>
		/// <param name="node"></param>
		/// <returns>Oid</returns>
		private Oid GetOidNode(TreeNode node)
		{
			if (node == null)
			{
				return null;
			}

			InstanceNodeInfo instanceNodeInfo = node.Tag as InstanceNodeInfo;
			if (instanceNodeInfo == null)
			{
				return null;
			}

			return instanceNodeInfo.InstanceOid;
		}
		#endregion Methods
	}

	#region NodeInfo Class
	/// <summary>
	/// Node information.
	/// </summary>
	class NodeInfo
	{
		#region Members
		/// <summary>
		/// Parent node identifier.
		/// </summary>
		private string mParentNodeId;
		/// <summary>
		/// Node identifier.
		/// </summary>
		private string mNodeId;
		/// <summary>
		/// Node alias.
		/// </summary>
		private string mAlias;
		/// <summary>
		/// Contextual menu reference.
		/// </summary>
		private ContextMenuStrip mMenu;
		/// <summary>
		/// Flag indicating if is final node.
		/// </summary>
		private bool mFinalNode;
		/// <summary>
		/// Node image Key.
		/// </summary>
		private string mImageKey;
		/// <summary>
		/// Grouping node image Key.
		/// </summary>
		private string mGroupImageKey;
		/// <summary>
		/// Indicates if instances must be grouped under an intermedia node
		/// </summary>
		private bool mHasIntermediaGroupingNode = true;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'NodeInfo' with the information of a node.
		/// </summary>
		/// <param name="parentId">Parent node identifer.</param>
		/// <param name="nodeId">Node identifer.</param>
		/// <param name="alias">Alias node.</param>
		/// <param name="alias">Alias node.</param>
		/// <param name="menu">Contextual menu of the node.</param>
		/// <param name="imageKey">Image Key node.</param>
		/// <param name="groupImageKey">Image Key of grouping node.</param>
		/// <param name="hasIntermediaGroupingNode">Requires a grouping node.</param>
		public NodeInfo(string parentId, string nodeId, string alias, ContextMenuStrip menu, bool finalNode, string imageKey, string groupImageKey, bool hasIntermediaGroupingNode)
		{
			ParentNodeId = parentId;
			NodeId = nodeId;
			Alias = alias;
			Menu = menu;
			FinalNode = finalNode;
			ImageKey = imageKey;
			GroupImageKey = groupImageKey;
			HasIntermediaGroupingNode = hasIntermediaGroupingNode;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or Sets the parent node identifier.
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
		/// Gets or Sets the node identifier.
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
		/// Gets or Sets the alias node.
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
		/// Gets or Sets the contextual menu for a node.
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
		/// Gets or Sets the flag FinalNode value.
		/// </summary>
		public bool FinalNode
		{
			get
			{
				return mFinalNode;
			}
			set
			{
				mFinalNode = value;
			}
		}
		/// <summary>
		/// Gets or Sets the image Key node.
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
		/// Gets or Sets the image Key grouping node.
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
		/// Indicates if instances must be grouped under an intermedia node
		/// </summary>
		public bool HasIntermediaGroupingNode
		{
			get
			{
				return mHasIntermediaGroupingNode;
			}
			set
			{
				mHasIntermediaGroupingNode = value;
			}
		}
		#endregion Properties
	}
	#endregion NodeInfo Class

	#region InstanceNodeInfo Class
	/// <summary>
	/// InstanceNode information class.
	/// </summary>
	class InstanceNodeInfo
	{
		#region Members
		/// <summary>
		/// Instance Oid.
		/// </summary>
		private Oid mInstanceOid;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'InstanceNodeInfo'.
		/// </summary>
		/// <param name="instanceOid">Instance Oid.</param>
		public InstanceNodeInfo(Oid instanceOid)
		{
			mInstanceOid = instanceOid;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or Sets the Instance Oid.
		/// </summary>
		public Oid InstanceOid
		{
			get
			{
				return mInstanceOid;
			}
			set
			{
				mInstanceOid = value;
			}
		}
		#endregion Properties
	}
	#endregion InstanceNodeInfo Class

	#region RecursiveNodeInfo Class
	/// <summary>
	/// RecursiveNode information class.
	/// </summary>
	class RecursiveNodeInfo
	{
		#region Members
		/// <summary>
		/// Node alias.
		/// </summary>
		private string mAlias;
		/// <summary>
		/// Parent node identifier.
		/// </summary>
		private string mParentNodeId;
		/// <summary>
		/// Node identifier.
		/// </summary>
		private string mNodeId;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'RecursiveNodeInfo'.
		/// </summary>
		/// <param name="alias">Node alias.</param>
		/// <param name="parentNodeId">Parent node identifier.</param>
		/// <param name="nodeId">Node identifier.</param>
		public RecursiveNodeInfo(string alias, string parentNodeId, string nodeId)
		{
			mAlias = alias;
			mParentNodeId = parentNodeId;
			mNodeId = nodeId;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets the alias node.
		/// </summary>
		internal string Alias
		{
			get
			{
				return mAlias;
			}
		}
		/// <summary>
		/// Gets the parent node identifier.
		/// </summary>
		internal string ParentNodeId
		{
			get
			{
				return mParentNodeId;
			}
		}
		/// <summary>
		/// Gets the node identifier.
		/// </summary>
		internal string NodeId
		{
			get
			{
				return mNodeId;
			}
		}
		#endregion Properties
	}
	#endregion RecursiveNodeInfo Class

}
