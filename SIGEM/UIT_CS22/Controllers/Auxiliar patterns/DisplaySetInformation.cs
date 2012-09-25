// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SIGEM.Client.Logics;

namespace SIGEM.Client.Controllers
{
	public class DisplaySetInformation: Object
	{
		#region Members
		/// <summary>
		/// Display Set Name
		/// </summary>
		private string mName = string.Empty;
		/// <summary>
		/// DisplaySet Item list
		/// </summary>
		private List<DisplaySetItem> mDisplaySetItems = new List<DisplaySetItem>();
		/// <summary>
		/// List of agents of the DisplaySet.
		/// </summary>
		private List<string> mAgents;
		/// <summary>
		/// Custom or Modeled DisplaySet
		/// </summary>
		private bool mCustom = false;
		/// <summary>
		/// Related service information
		/// </summary>
		private DisplaySetServiceInfo mServiceInfo;
		/// <summary>
		/// Returns True if the DisplaySet is editable
		/// </summary>
		private bool mEditable = false;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets the name of the service.
		/// </summary>
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
		/// <summary>
		/// List of pairs,  displayset element (attribute) and service inbound argument
		/// </summary>
		public List<DisplaySetItem> DisplaySetItems
		{
			get
			{
				if (mDisplaySetItems == null)
					mDisplaySetItems = new List<DisplaySetItem>();

				return mDisplaySetItems;
			}
			set
			{
				mDisplaySetItems = value;
			}
		}
		/// <summary>
		/// Gets or sets the list of agents that can acces to the DisplaySet.
		/// </summary>
		public List<string> Agents
		{
			get
			{
				return mAgents;
			}
			set
			{
				mAgents = value;
			}
		}
		/// <summary>
		/// Custom or Modeled DisplaySet
		/// </summary>
		public bool Custom
		{
			get
			{
				return mCustom;
			}
			set
			{
				mCustom = value;
			}
		}
		/// <summary>
		/// Related service information
		/// </summary>
		public DisplaySetServiceInfo ServiceInfo
		{
			get
			{
				return mServiceInfo;
			}
			set
			{
				mServiceInfo = value;
			}
		}
		/// <summary>
		/// Returns True if the DisplaySet is editable.
		/// It will be calculated based on the Service and DisplaySet Items properties
		/// </summary>
		public bool Editable
		{
			get
			{
				return mEditable;
			}
			set
			{
				mEditable = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="displaySetName"></param>
		public DisplaySetInformation(string displaySetName)
		{
			mName = displaySetName;

		}
		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="displaySetName"></param>
		public DisplaySetInformation(DisplaySetInformation displaySetToBeCopied)
			:this(displaySetToBeCopied.Name)
		{
			Custom = displaySetToBeCopied.Custom;
			foreach (DisplaySetItem item in displaySetToBeCopied.DisplaySetItems)
			{
				DisplaySetItem newItem = new DisplaySetItem(item);
				DisplaySetItems.Add(newItem);
			}
			if (displaySetToBeCopied.ServiceInfo != null)
			{
				ServiceInfo = new DisplaySetServiceInfo(displaySetToBeCopied.ServiceInfo);
			}
		}
		#endregion Constructors

		#region Methods
		/// <summary>
		/// Adds a DisplaySet item to the DisplaySet.
		/// </summary>
		/// <param name="name">Name of the DisplaySet item.</param>
		/// <param name="alias">Alias of the DisplaySet item.</param>
		/// <param name="idXML">IdXML of the DisplaySet item.</param>
		/// <param name="modelType">ModelType of the DisplaySet item.</param>
		/// <param name="agents">List of agents of the DisplaySet item.</param>
		/// <param name="width">Width column of the DisplaySet item.</param>
		public void Add(string name, string alias, string idXML, ModelType modelType, string[] agents, int width)
		{
			List<string> lAgents = new List<string>(agents);
			DisplaySetItem lDisplaySetItem = new DisplaySetItem(name, alias, idXML, modelType, lAgents, width, true);
			mDisplaySetItems.Add(lDisplaySetItem);
		}
		/// <summary>
		/// Adds a DisplaySet item to the DisplaySet.
		/// </summary>
		/// <param name="name">Name of the DisplaySet item.</param>
		/// <param name="alias">Alias of the DisplaySet item.</param>
		/// <param name="idXML">IdXML of the DisplaySet item.</param>
		/// <param name="modelType">ModelType of the DisplaySet item.</param>
		/// <param name="nullAllowed">If the DisplaySet item allows null values.</param>
		/// <param name="definedSelectionOptions">List of defined selection options.</param>
		/// <param name="agents">List of agents of the DisplaySet item.</param>
		public void Add(string name, string alias, string idXML, ModelType modelType, bool nullAllowed, List<KeyValuePair<object, string>> definedSelectionOptions, string[] agents, int width)
		{
			List<string> lAgents = new List<string>(agents);
			DisplaySetItem lDisplaySetItem = new DisplaySetItem(name, alias, idXML, modelType, nullAllowed, definedSelectionOptions, lAgents, width, true);
			mDisplaySetItems.Add(lDisplaySetItem);
		}

		/// <summary>
		/// Adds an object-valued item to the DisplaySet.
		/// </summary>
		/// <param name="name">Name of the DisplaySet item.</param>
		/// <param name="alias">Alias of the DisplaySet item.</param>
		/// <param name="className">Definition class.</param>
		/// <param name="idXML">IdXML of the DisplaySet item.</param>
		/// <param name="agents">List of agents of the DisplaySet item.</param>
		/// <param name="width">Width column of the DisplaySet item.</param>
		public void Add(string name, string alias, string className, string idXML, string[] agents, int width, string[] oidFields, DisplaySetInformation supplementaryInfo)
		{
			List<string> lAgents = new List<string>(agents);
			DisplaySetItem lDisplaySetItem = new DisplaySetItem(name, alias, className, idXML, lAgents, width, true,oidFields, supplementaryInfo);
			mDisplaySetItems.Add(lDisplaySetItem);
		}


		/// <summary>
		/// Initialize the DisplaySet elements
		/// Checks the visibility and assign the proper alias
		/// </summary>
		public void Initialize()
		{
			// Configure or eliminate from the list the elements with no visibility
			List<DisplaySetItem> itemsToBeDeleted = new List<DisplaySetItem>();
			foreach (DisplaySetItem lItem in DisplaySetItems)
			{
				if (!Logic.Agent.IsActiveFacet(lItem.Agents))
				{
					itemsToBeDeleted.Add(lItem);
				}
				else
				{
					lItem.Alias = CultureManager.TranslateString(lItem.IdXML, lItem.Alias);
				}
			}
			foreach (DisplaySetItem lItem in itemsToBeDeleted)
			{
				DisplaySetItems.Remove(lItem);
			}

			CheckEditableItems();
		}

		/// <summary>
		/// Checks and marks the editable items in the DisplaySet
		/// </summary>
		public void CheckEditableItems()
		{
			Editable = true;

			// Service and Agent check
			if (ServiceInfo == null || !Logic.Agent.IsActiveFacet(ServiceInfo.Agents))
			{
				Editable = false;
				return;
			}

			// Check every DisplaySet Item
			foreach (DisplaySetServiceArgumentInfo lArgInfo in ServiceInfo.ArgumentDisplaySetPairs.Values)
			{
				DisplaySetItem lItem = GetDisplaySetItemByName(lArgInfo.DSElementName);

				if ((lItem == null) || (!lItem.Visible))
				{
					Editable = false;
					return;
				}
				else
				{
					lItem.Editable = true;
				}
			}
		}
		/// <summary>
		/// Return the DisplaySetItem by name.
		/// </summary>
		/// <param name="name">Name of the displayset item.</param>
		/// <returns></returns>
		protected DisplaySetItem GetDisplaySetItemByName(string name)
		{
			foreach (DisplaySetItem lItem in DisplaySetItems)
			{
				if (string.Compare(name, lItem.Name, true) == 0)
				{
					return lItem;
				}
			}
			return null;
		}
		/// <summary>
		/// Gets a list of Display Set items that the agent passed as parameter can see.
		/// </summary>
		/// <param name="agentName">Name of the agent.</param>
		/// <returns>List of DisplaySetItem that the agent can see.</returns>
		public List<DisplaySetItem> GetVisibleDisplaySetItems(string agentName)
		{
			List<DisplaySetItem> resultList = new List<DisplaySetItem>();
			foreach (DisplaySetItem item in mDisplaySetItems)
			{
				List<string> lAgents = new List<string>(item.Agents.ToArray());
				if (lAgents.Contains(agentName))
				{
					resultList.Add(item);
				}
			}

			return resultList;
		}
		/// <summary>
		/// Converts the list of DisplaySet items in a string list, separated by comma.
		/// </summary>
		/// <returns>String list of the DisplaySet items separated by comma.</returns>
		public string GetDisplaySetItemsAsString()
		{
			string resultList = string.Empty;
			string separator = ",";
			
			foreach (DisplaySetItem item in mDisplaySetItems)
			{
				if (!resultList.Equals(string.Empty)) resultList += separator;
				resultList += item.Name;
			}

			return resultList;
		}

		/// <summary>
		/// Returns the display set items as an string separated by commas.
		/// </summary>
		/// <returns>String separed by commas representing the displayset items.</returns>
		public string DisplaySetItemsAsString()
		{
			string lResult = "";

			foreach (DisplaySetItem lItem in DisplaySetItems)
			{
				if (!lResult.Equals(string.Empty))
					lResult += ",";

				lResult += lItem.Name;
			}

			return lResult;
		}

		#region Serialize-Deserialize
		/// <summary>
		/// Serialize the information in XML format
		/// </summary>
		/// <param name="writer"></param>
		public void Serialize(XmlWriter writer)
		{
			writer.WriteStartElement("DisplaySet");
			writer.WriteAttributeString("Name", Name);

			foreach (DisplaySetItem item in DisplaySetItems)
			{
				item.Serialize(writer);
			}

			// Service information
			if (ServiceInfo != null)
			{
				ServiceInfo.Serialize(writer);
			}

			writer.WriteEndElement();
		}
        /// <summary>
        /// Deserialize the infotmation from XML node
        /// </summary>
        /// <param name="displaySetNode"></param>
        /// <param name="version"></param>
		public void Deserialize(XmlNode displaySetNode, string version)
		{
            Name = displaySetNode.Attributes["Name"].Value;
			// Is a custom DisplaySet
			Custom = true;
             
            // Get all DisplaySet items
            XmlNodeList lItemNodes = displaySetNode.SelectNodes("DisplaySetItem");
            foreach (XmlNode lItemNode in lItemNodes)
            {
                DisplaySetItem item = new DisplaySetItem();
                item.Deserialize(lItemNode, version);
                DisplaySetItems.Add(item);
            }


            // Get Service info node
            XmlNodeList lServiceNodes = displaySetNode.SelectNodes("ServiceInfo");
            if (lServiceNodes.Count == 1)
            {
                ServiceInfo = new DisplaySetServiceInfo();
                ServiceInfo.Deserialize(lServiceNodes[0], version);
            }
		}
		#endregion Serialize-Deserialize
		#endregion Methods
	}
}
