// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Threading;
using SIGEM.Client.Presentation;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the IUMainController.
	/// </summary>
	public class IUMainController : IUController
	{
		#region Members
		/// <summary>
		/// HAT elements collection of the IU
		/// </summary>
		private HatList  mHatElementNodes = null;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'IUMainController' class.
		/// </summary>
		/// <param name="context">Context.</param>
		public IUMainController(IUMainContext context)
			: base()
		{
			Context = context;

			HatElementNodes = new HatList(this);
		}
		#endregion Constructors
		
		#region Properties
		/// <summary>
		///	Gets or sets the HAT nodes collection of the IU.
		/// </summary>
		public IListKeyed<HatElementController> HatElementNodes
		{
			get
			{
				return mHatElementNodes;
			}
			protected set
			{
				 mHatElementNodes = value as HatList;
			}
		}
		#endregion Properties

		#region Methods
		/// <summary>
		/// Apply multilanguage to the Main scenario.
		/// </summary>
		public override void ApplyMultilanguage()
		{
			// Applies the suitable text to all the HAT elements according the selected language.
			mHatElementNodes.ApplyMultilanguage();
			
			base.ApplyMultilanguage();
		}
		
		/// <summary>
		/// Show or hide the HAT elements according the connected agent visibility.
		/// </summary>
		public void ApplyConnectedAgentVisibility()
		{
			mHatElementNodes.ApplyAgentsVisibility();
		}
		/// <summary>
		/// Hides all the elements contained in the HAT.
		/// </summary>
		public void HideAllHATEntries()
		{
			mHatElementNodes.HideAll();
		}
		#endregion Methods
		#region Keyed HAT List
		/// <summary>
		/// Class that manages the HAT Collection node to the IU.
		/// HatNodeController -> without Trigger (only label).
		/// HatLeafController -> with Trigger.
		/// </summary>
		private class HatList : ListKeyed<HatElementController, IUMainController>
		{
			#region Constructors
			/// <summary>
			/// Initializes a new instance of the 'IUMainController' class.
			/// </summary>
			/// <param name="parent">Parent controller</param>
			public HatList(IUMainController parent)
				: base(parent)
			{
			}
			#endregion Constructors

			#region Delete
			/// <summary>
			/// Deletes a hat element of the controller.
			/// </summary>
			/// <param name="item">The item to delete</param>
			protected override void Delete(HatElementController item){}
			#endregion Delete

			#region Insert
			/// <summary>
			/// Inserts a new hat element to the controller.
			/// </summary>
			/// <param name="item">The HAT item.</param>
			/// <param name="index">the item index.</param>
			protected override void Insert(HatElementController item, int index){}
			#endregion Insert

			#region GetKeyForItem
			/// <summary>
			/// Gets the key index for the item.
			/// </summary>
			/// <param name="item">the item</param>
			/// <returns></returns>
			protected override string GetKeyForItem(HatElementController item)
			{
				return item.Name;
			}
			#endregion GetKeyForItem
			#region Methods
			/// <summary>
			/// Apply multilanguage to the HAT elements.
			/// </summary>
			public void ApplyMultilanguage()
			{
				// Applies the suitable text to all the HAT elements according the selected language.
				foreach (HatElementController item in this)
				{
					item.ApplyMultilanguage();
				}
			}

			/// <summary>
			/// Hides all the elements contained in the HAT.
			/// </summary>
			public void HideAll()
			{
				foreach (HatElementController item in this)
				{
					if (item is HatLeafController)
					{
						// If it is a Leaf it has a Trigger.
						(item as HatLeafController).Trigger.Visible = false;
					}
					else if (item is HatNodeController && (item as HatNodeController).Label != null)
					{
						// If it is a Node it has a Label.
						(item as HatNodeController).Label.Visible = false;
					}
				}
			}

			/// <summary>
			/// Configures (show or hide) the elements contained in the HAT, depending on the connected agent and its actived facets.
			/// </summary>
			public void ApplyAgentsVisibility()
			{
				// Configure the elements contained in the HAT, depending on the connected agent.
				foreach (HatElementController item in this)
				{
					item.ApplyAgentsVisibility();
				}
			}
			#endregion Methods
		}
		#endregion Keyed HAT List
	}
}


