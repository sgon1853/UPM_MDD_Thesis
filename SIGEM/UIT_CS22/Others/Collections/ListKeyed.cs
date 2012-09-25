// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

using SIGEM.Client.Controllers;

namespace SIGEM.Client
{
	/// <summary>
	/// Class that manages the 'ListKeyed'.
	/// </summary>
	/// <typeparam name="T">Key.</typeparam>
	/// <typeparam name="TParentController">Parent controller.</typeparam>
	public abstract class ListKeyed<T,TParentController> :
		KeyedCollection<string, T>,
		IListKeyed<T>
		where T : SIGEM.Client.Controllers.Controller
		where TParentController: SIGEM.Client.Controllers.Controller
	{
		#region Members
		/// <summary>
		/// Instance Parent Controller.
		/// </summary>
		private TParentController mParentController = null;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets the parent controller.
		/// </summary>
		protected virtual TParentController Parent
		{
			get
			{
				return mParentController;
			}
			set
			{
				mParentController = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ArgumentList' class.
		/// </summary>
		/// <param name="parent">Parent controller.</param>
		public ListKeyed(TParentController parent)
			: base(StringComparer.CurrentCultureIgnoreCase)
		{
			Parent = parent;
		}
		#endregion Constructors

		#region Methods
		/// <summary>
		/// Delete item.
		/// </summary>
		/// <param name="item">Item.</param>
		protected abstract void Delete(T item);
		/// <summary>
		/// Insert item.
		/// </summary>
		/// <param name="item">Item.</param>
		/// <param name="index">Index.</param>
		protected abstract void Insert(T item, int index);
		/// <summary>
		/// Clear items.
		/// </summary>
		protected override void ClearItems()
		{
			int lCount = 0;
			foreach (T litem in this)
			{
				Delete(litem);
				lCount++;
			}
			base.ClearItems();
		}
		/// <summary>
		/// Insert item.
		/// </summary>
		/// <param name="index">Index.</param>
		/// <param name="item">Item.</param>
		protected override void InsertItem(int index, T item)
		{
			if(item != null)
			{
				Insert(item, index);
			}
			base.InsertItem(index, item);
		}
		/// <summary>
		/// Remove item.
		/// </summary>
		/// <param name="index">Index.</param>
		protected override void RemoveItem(int index)
		{
			Delete(this[index]);
			base.RemoveItem(index);
		}
		/// <summary>
		/// Set item.
		/// </summary>
		/// <param name="index">Index.</param>
		/// <param name="item">Item.</param>
		protected override void SetItem(int index, T item)
		{
			if(item != null)
			{
				Insert(item, index);
			}
			base.SetItem(index, item);
		}
		/// <summary>
		/// Exist item.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <returns>bool.</returns>
		public bool Exist(string name)
		{
			if (this.Dictionary != null)
			{
				return this.Dictionary.ContainsKey(name);
			}
			return false;
		}
		#endregion Methods
	}

}
