// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Presentation;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the OrderCriteria controller.
	/// The Order Criteria indicates the order that is displayed the items in a population.
	/// </summary>
	public class OrderCriteriaController : Controller
	{
		#region Members
		/// <summary>
		/// Name of the OrderCriteria.
		/// </summary>
		private string mName;
		/// <summary>
		/// Alias of the OrderCriteria.
		/// </summary>
		private string mAlias;
		/// <summary>
		/// IdXML of the OrderCriteria.
		/// </summary>
		private string mIdXML;
		/// <summary>
		/// List of agents of the OrderCriteria.
		/// </summary>
		private List<string> mAgents;
		/// <summary>
		/// Selector presentation.
		/// </summary>
		private IEditorPresentation mSelector;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets the OrderCriteria name.
		/// </summary>
		public string Name
		{
			get
			{
				return mName;
			}
		}
		/// <summary>
		/// Gets the OrderCriteria alias.
		/// </summary>
		public string Alias
		{
			get
			{
				return mAlias;
			}
		}
		/// <summary>
		/// Gets the OrderCriteria XML identifier.
		/// </summary>
		public string IdXML
		{
			get
			{
				return mIdXML;
			}
		}
		/// <summary>
		/// Gets or sets the OrderCriteria selector.
		/// This selector indicates which item parameter is the order pattern.
		/// </summary>
		public IEditorPresentation Selector
		{
			get
			{
				return mSelector;
			}
			set
			{
				if (mSelector != null)
				{
					mSelector.ValueChanged -= new EventHandler<ValueChangedEventArgs>(HandleOrderSelectorValueChanged);
				}
				mSelector = value;
				if (mSelector != null)
				{
					mSelector.ValueChanged += new EventHandler<ValueChangedEventArgs>(HandleOrderSelectorValueChanged);
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the OrderCriteria is selected or not.
		/// </summary>
		public bool IsSelected
		{
			get
			{
				if (mSelector != null)
				{
					object selectorValue = mSelector.Value;
					if (selectorValue != null)
					{
						return (string.Compare(selectorValue.ToString(), Name, true) == 0);
					}
				}
				return false;
			}
			set
			{
				if (value)
				{
					mSelector.Value = Name;
				}
				else if (mSelector.Value.ToString() == Name)
				{
					mSelector.Value = string.Empty;
				}
			}
		}
		/// <summary>
		/// Gets a reference to the parent controller.
		/// </summary>
		public new IUController Parent
		{
			get
			{
				return base.Parent as IUController;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'OrderCriteriaController' class.
		/// </summary>
		/// <param name="name">Name of the OrderCriteria.</param>
		/// <param name="alias">Alias of the OrderCriteria.</param>
		/// <param name="idXML">IdXML of the OrderCriteria.</param>
		/// <param name="agents">List of agents of the OrderCriteria.</param>
		/// <param name="parent">Parent controller.</param>
		public OrderCriteriaController(string name, string alias, string idXML, string[] agents, Controller parent)
			: base(parent)
		{
			mName = name;
			mAlias = alias;
			mIdXML = idXML;
			mAgents = new List<string>(agents);
		}
		#endregion Constructors

		#region Events
		/// <summary>
		/// Occurs when the OrderCriteria selector has changed its value.
		/// </summary>
		public event EventHandler<OrderCriteriaChangedEventArgs> ValueChanged;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Handles the Order selector value changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleOrderSelectorValueChanged(Object sender, ValueChangedEventArgs e)
		{
			if (!this.IsSelected)
			{
				return;
			}

			OnValueChanged(new OrderCriteriaChangedEventArgs());
		}
		#endregion Event Handlers

		#region Event Raisers
		/// <summary>
		/// Raises the Value changed Event
		/// </summary>
		/// <param name="eventArgs"></param>
		private void OnValueChanged(OrderCriteriaChangedEventArgs eventArgs)
		{
			EventHandler<OrderCriteriaChangedEventArgs> handler = ValueChanged;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		#endregion Event Raisers
	}
}
