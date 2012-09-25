// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Presentation;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the HAT nodes.
	/// </summary>
	public class HatNodeController : HatElementController
	{
		#region Members
		/// <summary>
		/// Label presentation.
		/// </summary>
		private ILabelPresentation mLabel;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets the label presentation of the HAT node.
		/// </summary>
		public ILabelPresentation Label
		{
			get
			{
				return mLabel;
			}
			set
			{
				mLabel = value;
				if (mLabel != null)
				{
					mLabel.Value = Alias;
				}
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

				if (Label != null)
				{
					Label.Value = value;
				}
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'HatNodeController' class.
		/// </summary>
		/// <param name="name">Name of the HAT node.</param>
		/// <param name="alias">Alias of the HAT node.</param>
		/// <param name="idXML">IdXML of the HAT node.</param>
		/// <param name="agents">List of agents of the HAT node.</param>
		public HatNodeController(string name, string alias, string idXML, string[] agents)
			: base(name, alias, idXML, agents)
		{
		}
		#endregion Constructors

		#region Methods
		/// <summary>
		/// Apply Agents visibility
		/// </summary>
		public override void ApplyAgentsVisibility()
		{
			if (Label == null)
			{
				return;
			}

			Label.Visible = Logics.Logic.Agent.IsActiveFacet(Agents);
		}
		#endregion Methods
	}
}
