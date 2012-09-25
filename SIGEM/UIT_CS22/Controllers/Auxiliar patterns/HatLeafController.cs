// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Presentation;
using SIGEM.Client.Logics;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the HAT leafs.
	/// </summary>
	public class HatLeafController : HatElementController
	{
		#region Members
		/// <summary>
		/// Class name of the HAT leaf.
		/// </summary>
		private string mClassIUName;
		/// <summary>
		/// Interaction unit name of the HAT leaf.
		/// </summary>
		private string mIUName;
		/// <summary>
		/// Trigger presentation.
		/// </summary>
		private ITriggerPresentation mTrigger;
		/// <summary>
		/// Navigational filtering identifier.
		/// </summary>
		private string mNavigationalFilteringIdentity = string.Empty;
		#endregion Members


		#region Properties
		/// <summary>
		/// Gets or sets the class name of the HAT leaf.
		/// </summary>
		public string ClassIUName
		{
			get
			{
				return mClassIUName;
			}
			set
			{
				mClassIUName = value;
			}
		}
		/// <summary>
		/// Gets or sets the interaction unit name of the HAT leaf.
		/// </summary>
		public string IUName
		{
			get
			{
				return mIUName;
			}
			set
			{
				mIUName = value;
			}
		}
		/// <summary>
		/// Gets or sets the trigger presentation of the HAT leaf.
		/// </summary>
		public ITriggerPresentation Trigger
		{
			get
			{
				return mTrigger;
			}
			set
			{
				if (mTrigger != null)
				{
					mTrigger.Triggered -= new EventHandler<TriggerEventArgs>(Execute);
				}

				mTrigger = value;

				if (mTrigger != null)
				{
					mTrigger.Triggered += new EventHandler<TriggerEventArgs>(Execute);
					mTrigger.Value = Alias;
				}
			}
		}
		/// <summary>
		/// Gets or sets the navigational filtering identifier.
		/// </summary>
		public string NavigationalFilteringIdentity
		{
			get
			{
				return mNavigationalFilteringIdentity;
			}
			protected set
			{
				mNavigationalFilteringIdentity = (value == null ? string.Empty : value);
			}
		}
		/// <summary>
		/// Gets or Sets the alias of the HAT node and apply it to the Label.
		/// </summary>
		public override string Alias
		{
			get
			{
				return base.Alias;
			}
			set
			{
				base.Alias = value;

				if (Trigger != null)
				{
					Trigger.Value = value;
				}
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'HatLeafController'.
		/// </summary>
		/// <param name="name">Name of the HAT leaf.</param>
		/// <param name="alias">Alias of the HAT leaf.</param>
		/// <param name="idXML">IdXML of the HAT leaf.</param>
		/// <param name="classIUName">Class name of the HAT leaf.</param>
		/// <param name="iuName">Interaction unit name of the HAT leaf.</param>
		/// <param name="agents">List of agents of the HAT leaf.</param>
		[Obsolete("Since version 3.3.4.1")]
		public HatLeafController(string name, string alias, string idXML, string classIUName, string iuName, string[] agents)
			: base (name, alias, idXML, agents)
		{
			ClassIUName = classIUName;
			IUName = iuName;
		}
		/// <summary>
		/// Initializes a new instance of the 'HatLeafController'.
		/// </summary>
		/// <param name="name">Name of the HAT leaf.</param>
		/// <param name="alias">Alias of the HAT leaf.</param>
		/// <param name="idXML">IdXML of the HAT leaf.</param>
		/// <param name="classIUName">Class name of the HAT leaf.</param>
		/// <param name="iuName">Interaction unit name of the HAT leaf.</param>
		/// <param name="agents">List of agents of the HAT leaf.</param>
		/// <param name="navigationalFilteringIdentity">Navigational filtering identifier.</param>
		public HatLeafController(string name, string alias, string idXML, string classIUName, string iuName, string[] agents, string navigationalFilteringIdentity)
			: base(name, alias, idXML, agents)
		{
			ClassIUName = classIUName;
			IUName = iuName;
			NavigationalFilteringIdentity = navigationalFilteringIdentity;
		}
		#endregion Constructors

		#region Execute
		/// <summary>
		/// Executes the HAT leaf associated method.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">TriggerEventArgs.</param>
		public void Execute(object sender, TriggerEventArgs e)
		{
			try
			{
				// Create exchange information.
				ExchangeInfoAction lExchangeInfo = new ExchangeInfoAction(this.ClassIUName, this.IUName,NavigationalFilteringIdentity,  null, null);
				// Launch scenario.
				ScenarioManager.LaunchActionScenario(lExchangeInfo, null);
			}
			catch (Exception err)
			{
				ScenarioManager.LaunchErrorScenario(err);
			}
		}
		#endregion Execute
		#region Methods
		/// <summary>
		/// Apply Agents visibility.
		/// </summary>
		public override void ApplyAgentsVisibility()
		{
			if (Trigger == null)
			{
				return;
			}

			Trigger.Visible = Logics.Logic.Agent.IsActiveFacet(Agents);
		}
		#endregion Methods
	}
}
