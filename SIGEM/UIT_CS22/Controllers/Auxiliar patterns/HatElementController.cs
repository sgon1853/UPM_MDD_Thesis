// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using SIGEM.Client.Presentation;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the HAT elements.
	/// </summary>
	public abstract class HatElementController : Controller
	{
		#region Members
		/// <summary>
		/// Number of the HAT element.
		/// </summary>
		private string mName;
		/// <summary>
		/// Alias of the HAT element.
		/// </summary>
		private string mAlias;
		/// <summary>
		/// IdXML of the HAT element.
		/// </summary>
		private string mIdXML;
		/// <summary>
		/// List of agents of the HAT element.
		/// </summary>
		private List<string> mAgents;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets the name of the HAT element.
		/// </summary>
		public string Name
		{
			get
			{
				return mName;
			}
		}
		/// <summary>
		/// Gets the alias of the HAT element.
		/// </summary>
		public virtual string Alias
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
		/// Gets the IdXML of the HAT element.
		/// </summary>
		public string IdXML
		{
			get
			{
				return mIdXML;
			}
		}
		/// <summary>
		/// Gets the parent controller of the HAT element.
		/// </summary>
		public new IUMainController Parent
		{
			get
			{
				return base.Parent as IUMainController;
			}
		}
		/// <summary>
		/// Gets or sets the list of agents that can acces to the Action item.
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
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'HatElementController' class.
		/// </summary>
		/// <param name="name">Name of the HAT element.</param>
		/// <param name="alias">Alias of the HAT element.</param>
		/// <param name="idXML">IdXML of the HAT element.</param>
		/// <param name="agents">List of agents of the HAT element.</param>
		public HatElementController(string name, string alias, string idXML, string[] agents)
			: base (null)
		{
			List<string> lAgents = new List<string>(agents);
			mName = name;
			mAlias = alias;
			mIdXML = idXML;
			Agents = lAgents;
		}
		#endregion Constructors

		#region Methods
		/// <summary>
		/// Initialize controller
		/// </summary>
		public virtual void Initialize()
		{
		}

		/// <summary>
		/// Apply multilanguage
		/// </summary>
		public virtual void ApplyMultilanguage()
		{
			HatLeafController leaf = this as HatLeafController;
			if (leaf != null)
			{
				leaf.Trigger.Value = CultureManager.TranslateString(this.IdXML, this.Alias);
			}
			else
			{
				HatNodeController node = this as HatNodeController;
				if (node != null && node.Label != null)
				{
					node.Label.Value = CultureManager.TranslateString(this.IdXML, this.Alias);
				}
			}
		}

		/// <summary>
		/// Apply Agents visibility
		/// </summary>
		public virtual void ApplyAgentsVisibility()
		{
		}
		#endregion Methods
	}
}
