// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections.Specialized;

namespace SIGEM.Client.Controllers
{
	public class DisplaySetServiceInfo: Object
	{
		#region Members
		/// <summary>
		/// Class service name
		/// </summary>
		private string mClassServiceName = string.Empty;
		/// <summary>
		/// Service name
		/// </summary>
		private string mServiceName = string.Empty;
		/// <summary>
		/// Service Alias
		/// </summary>
		private string mServiceAlias = string.Empty;
		/// <summary>
		/// Argument name representing the selected instance
		/// </summary>
		private string mSelectedInstanceArgumentName = string.Empty;
		/// <summary>
		/// Argument Alias representing the selected instance
		/// </summary>
		private string mSelectedInstanceArgumentAlias = string.Empty;
		/// <summary>
		/// Alternate key name used in the argument that represents the selected instance.
		/// </summary>
		private string mSelectedInstanceArgumentAlternateKeyName = string.Empty;
		/// <summary>
		/// List of pairs,  Inbound Argument and displayset element
		/// </summary>
		private Dictionary<string, DisplaySetServiceArgumentInfo> mArgumentDisplaySetPairs = new Dictionary<string, DisplaySetServiceArgumentInfo>();
		/// <summary>
		/// List of agents of the service.
		/// </summary>
		private List<string> mAgents;
		#endregion Members

		#region Properties
		/// <summary>
		/// List of pairs,  displayset element (attribute) and service inbound argument
		/// </summary>
		public Dictionary<string, DisplaySetServiceArgumentInfo> ArgumentDisplaySetPairs
		{
			get
			{
				return mArgumentDisplaySetPairs;
			}
			set
			{
				mArgumentDisplaySetPairs = value;
			}
		}
		/// <summary>
		/// Gets or sets the name of the service.
		/// </summary>
		public string ServiceName
		{
			get
			{
				return mServiceName;
			}
			set
			{
				mServiceName = value;
			}
		}
		/// <summary>
		/// Gets or sets the class name of the service.
		/// </summary>
		public string ClassServiceName
		{
			get
			{
				return mClassServiceName;
			}
			set
			{
				mClassServiceName = value;
			}
		}
		/// <summary>
		/// Service Alias
		/// </summary>
		public string ServiceAlias
		{
			get
			{
				return mServiceAlias;
			}
			set
			{
				mServiceAlias = value;
			}
		}
		/// <summary>
		/// Gets or sets the list of agents that can access to the service.
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
		/// Argument name representing the selected instance
		/// </summary>
		public string SelectedInstanceArgumentName
		{
			get
			{
				return mSelectedInstanceArgumentName;
			}
			set
			{
				mSelectedInstanceArgumentName = value;
			}
		}
		/// <summary>
		/// Argument Alias representing the selected instance
		/// </summary>
		public string SelectedInstanceArgumentAlias
		{
			get
			{
				return mSelectedInstanceArgumentAlias;
			}
			set
			{
				mSelectedInstanceArgumentAlias = value;
			}
		}

		/// <summary>
		/// Get or sets the alternate key name used in the argument that represents the selected instance.
		/// </summary>
		public string SelectedInstanceArgumentAlternateKeyName
		{
			get
			{
				return mSelectedInstanceArgumentAlternateKeyName;
			}
			set
			{
				mSelectedInstanceArgumentAlternateKeyName = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public DisplaySetServiceInfo()
		{
		}
		/// <summary>
		/// Constructor from controller factory
		/// </summary>
		/// <param name="serviceController">Associated Service controller</param>
		/// <param name="argumentDisplaySetPairs">Pairs Argument name - Attribute in DIsplaySet name</param>
		public DisplaySetServiceInfo(IUServiceController serviceController, Dictionary<string, string> argumentDisplaySetPairs)
		{
			mServiceName = serviceController.Context.ServiceName;
			mClassServiceName = serviceController.Context.ClassName;
			mAgents = serviceController.Agents;
			mServiceAlias = CultureManager.TranslateString(serviceController.IdXML, serviceController.Alias);
			foreach (ArgumentController argument in serviceController.InputFields)
			{
				ArgumentDVController argumentDV = argument as ArgumentDVController;

				if (argumentDV != null)
				{
					// Multilanguage for the Alias argument
					string alias = CultureManager.TranslateString(argument.IdXML, argument.Alias);
					string dsElementName = argumentDisplaySetPairs[argumentDV.Name];
					// Add the new pair to the list
					DisplaySetServiceArgumentInfo argumentInfo = new DisplaySetServiceArgumentInfo(argumentDV.Name, dsElementName, argumentDV.ModelType, alias, argumentDV.NullAllowed);
					this.ArgumentDisplaySetPairs.Add(argumentDV.Name, argumentInfo);
				}
				else
				{
					ArgumentOVController lOVArgument = argument as ArgumentOVController;
					if (lOVArgument != null && lOVArgument.AlternateKeyName != string.Empty)
					{
						SelectedInstanceArgumentAlternateKeyName = lOVArgument.AlternateKeyName;
					}

					SelectedInstanceArgumentName = argument.Name;
					// Multilanguage for the Alias argument
					SelectedInstanceArgumentAlias = CultureManager.TranslateString(argument.IdXML, argument.Alias);
				}
			}
		}
		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="serviceInfoTOBeCopied"></param>
		public DisplaySetServiceInfo(DisplaySetServiceInfo serviceInfoToBeCopied)
		{
			Agents = serviceInfoToBeCopied.Agents;
			ArgumentDisplaySetPairs = serviceInfoToBeCopied.ArgumentDisplaySetPairs;
			ClassServiceName = serviceInfoToBeCopied.ClassServiceName;
			SelectedInstanceArgumentAlias = serviceInfoToBeCopied.SelectedInstanceArgumentAlias;
			SelectedInstanceArgumentName = serviceInfoToBeCopied.SelectedInstanceArgumentName;
			ServiceName = serviceInfoToBeCopied.ServiceName;
			ServiceAlias = serviceInfoToBeCopied.ServiceAlias;
		}

		#endregion Constructors

		#region Methods
		/// <summary>
		/// Returns true if the Display Set element must allows null
		/// </summary>
		/// <param name="displaySetElement"></param>
		/// <returns></returns>
		public bool DisplaySetElementAllowsNull(string displaySetElement)
		{
			// If any argument related allows null, the display set element too
			foreach (KeyValuePair<string, DisplaySetServiceArgumentInfo> pair in ArgumentDisplaySetPairs)
			{
				if (pair.Value.DSElementName.Equals(displaySetElement, StringComparison.OrdinalIgnoreCase))
				{
					return pair.Value.AllowsNull;
				}
			}
			return false;
		}

		#region Serialize-Deserialize
		/// <summary>
		/// Serialize the information in XML format
		/// </summary>
		/// <param name="writer"></param>
		internal void Serialize(XmlWriter writer)
		{
			writer.WriteStartElement("ServiceInfo");
			writer.WriteAttributeString("ClassName", ClassServiceName);
			writer.WriteAttributeString("Name", ServiceName);
			writer.WriteAttributeString("Alias", ServiceAlias);
			writer.WriteAttributeString("SelectedInstanceArgumentName", SelectedInstanceArgumentName);
			writer.WriteAttributeString("SelectedInstanceArgumentAlias", SelectedInstanceArgumentAlias);
			string agents = UtilFunctions.StringList2String(Agents, ",");
			writer.WriteAttributeString("Agents", agents);

			writer.WriteStartElement("Arguments");
			foreach (KeyValuePair<string, DisplaySetServiceArgumentInfo> pair in ArgumentDisplaySetPairs)
			{
				pair.Value.Serialize(writer);
			}
			// Arguments
			writer.WriteEndElement();

			// Serive Info
			writer.WriteEndElement();
		}
        /// <summary>
        /// Deserialize from XML node
        /// </summary>
        /// <param name="serviceNode"></param>
        /// <param name="version"></param>
		internal void Deserialize(XmlNode serviceNode, string version)
		{
            ClassServiceName = serviceNode.Attributes["ClassName"].Value;
			ServiceName = serviceNode.Attributes["Name"].Value;
			ServiceAlias = serviceNode.Attributes["Alias"].Value;
			SelectedInstanceArgumentName = serviceNode.Attributes["SelectedInstanceArgumentName"].Value;
			SelectedInstanceArgumentAlias = serviceNode.Attributes["SelectedInstanceArgumentAlias"].Value;
            string agents = serviceNode.Attributes["Agents"].Value;
			Agents = new List<string>();
			Agents.AddRange(agents.Split(','));

            // Get all Arguments info
            if (serviceNode.ChildNodes.Count != 1)
                return;

            XmlNodeList lArgumentNodes = serviceNode.ChildNodes[0].SelectNodes("Argument");
            foreach (XmlNode lArgumentNode in lArgumentNodes)
            {
                DisplaySetServiceArgumentInfo argumentInfo = new DisplaySetServiceArgumentInfo();
                argumentInfo.Deserialize(lArgumentNode, version);
                ArgumentDisplaySetPairs.Add(argumentInfo.Name, argumentInfo);
            }
		}
		#endregion Serialize-Deserialize

		#endregion Methods

	}
}
