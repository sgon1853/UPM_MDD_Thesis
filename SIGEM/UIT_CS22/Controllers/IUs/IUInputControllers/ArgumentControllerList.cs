// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using SIGEM.Client.Presentation;

namespace SIGEM.Client.Controllers
{

		#region IArguments
		/// <summary>
		/// Argument list interface.
		/// </summary>
		public interface IArguments :
			IList<ArgumentController>
		{
			/// <summary>
			/// Gets & Sets Argument Selected.
			/// </summary>
			ArgumentController ArgumentSelected { get;set;}
			/// <summary>
			/// Gets the specified argument from the list.
			/// </summary>
			/// <param name="name"></param>
			/// <returns></returns>
			ArgumentController this[string name] { get; }
			/// <summary>
			/// Checks if the item specified exist in the argument list.
			/// </summary>
			/// <param name="name">Item to search.</param>
			/// <returns></returns>
			bool Exist(string name);
		}
		#endregion IArguments

	public class ArgumentControllerList :
		KeyedCollection<string, ArgumentController>,
		IArguments
	{
		#region Members
		/// <summary>
		/// Input Parent member.
		/// </summary>
		private IUInputFieldsController mParent = null;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ArgumentList' class.
		/// </summary>
		public ArgumentControllerList() { }
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Input Parent property.
		/// </summary>
		public virtual IUInputFieldsController Parent
		{
			get
			{
				return mParent;
			}
			set
			{
				mParent = value;
			}
		}
		/// <summary>
		/// Gets or Sets the argument selected.
		/// </summary>
		public virtual ArgumentController ArgumentSelected
		{
			get
			{
				// Search selected argument.
				foreach (ArgumentController lArgument in this)
				{
					if (lArgument.IsSelected)
					{
						return lArgument;
					}
				}

				// Default value.
				return null;
			}
			set
			{
				ArgumentController lArgument = ArgumentSelected;
				if (lArgument != null)
				{
					lArgument.IsSelected = false;
				}
				if (value != null)
				{
					value.IsSelected = true;
				}
			}
		}
		#endregion Properties

		#region Events
		/// <summary>
		/// Event raised when change value property in the argument controller.
		/// </summary>
		public event EventHandler<ValueChangedEventArgs> ValueChanged;
		/// <summary>
		/// Event raised when change Enable property in the argument controller.
		/// </summary>
		public event EventHandler<EnabledChangedEventArgs> EnabledChanged;
		/// <summary>
		/// Event raised when an argument executes a command
		/// </summary>
		public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Method suscribed to ArgumentValueChangedEventHandler
		/// </summary>
		/// <param name="sender">ArgumentController reference.</param>
		/// <param name="e"></param>
		private void HandleArgumentValueChanged(object sender, ValueChangedEventArgs e)
		{
			ValueChangedEventArgs lChangeValue = new ValueChangedEventArgs(sender as ArgumentController, e.Agent);
			lChangeValue.NewValue = e.NewValue;
			lChangeValue.OldValue = e.OldValue;

			OnValueChanged(lChangeValue);
		}
		/// <summary>
		/// Method suscribed to ArgumentValueChangedEventHandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleArgumentEnabledChanged(object sender, EnabledChangedEventArgs e)
		{
			EnabledChangedEventArgs lChangeValue = new EnabledChangedEventArgs(sender as ArgumentController, e.Agent);
			OnEnabledChanged(lChangeValue);
		}
		/// <summary>
		/// Handles the Argument Execute Command event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleArgumentExecuteCommand(object sender, ExecuteCommandEventArgs e)
		{
			OnExecuteCommand(e);
		}

		#endregion Event Handlers

		#region Event Raisers
		/// <summary>
		/// Raises the ValueChanged event.
		/// </summary>
		/// <param name="changeEnable">Event argumnts.</param>
		protected virtual void OnValueChanged(ValueChangedEventArgs eventArgs)
		{
			EventHandler<ValueChangedEventArgs> handler = ValueChanged;
			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the EnabledChanged event.
		/// </summary>
		/// <param name="changeEnable">Event argumnts.</param>
		protected virtual void OnEnabledChanged(EnabledChangedEventArgs eventArgs)
		{
			EventHandler<EnabledChangedEventArgs> handler = EnabledChanged;
			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the ExecuteCommand event.
		/// </summary>
		/// <param name="changeEnable">Event argumnts.</param>
		protected virtual void OnExecuteCommand(ExecuteCommandEventArgs eventArgs)
		{
			EventHandler<ExecuteCommandEventArgs> handler = ExecuteCommand;
			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		#endregion Event Raisers

		#region Methods

		/// <summary>
		/// Initialize Arguments.
		/// </summary>
		public virtual void Initialize()
		{
			// Initialize Arguments
			foreach (ArgumentController lArgument in this)
			{
				lArgument.Initialize();
			}
		}

		#region KeyedCollection

		protected virtual void Insert(ArgumentController argumentController, int index)
		{
			if (argumentController != null)
			{
				argumentController.ValueChanged += new EventHandler<ValueChangedEventArgs>(HandleArgumentValueChanged);
				argumentController.EnableChanged += new EventHandler<EnabledChangedEventArgs>(HandleArgumentEnabledChanged);
				argumentController.ExecuteCommand += new EventHandler<ExecuteCommandEventArgs>(HandleArgumentExecuteCommand);
			}
		}
		protected virtual void Delete(ArgumentController argumentController)
		{
			if (argumentController != null)
			{
				argumentController.ValueChanged -= new EventHandler<ValueChangedEventArgs>(HandleArgumentValueChanged);
				argumentController.EnableChanged -= new EventHandler<EnabledChangedEventArgs>(HandleArgumentEnabledChanged);
				argumentController.ExecuteCommand -= new EventHandler<ExecuteCommandEventArgs>(HandleArgumentExecuteCommand);
			}
		}

		public bool Exist(string name)
		{
			if (this.Dictionary != null)
			{
				return this.Dictionary.ContainsKey(name);
			}
			return false;
		}
		protected override string GetKeyForItem(ArgumentController item)
		{
			return item.Name;
		}

		protected override void ClearItems()
		{
			int lCount = 0;
			foreach (ArgumentController litem in this)
			{
				Delete(litem);
				lCount++;
			}
			base.ClearItems();
		}

		protected override void InsertItem(int index, ArgumentController item)
		{
			Insert(item, index);
			base.InsertItem(index, item);
		}

		protected override void RemoveItem(int index)
		{
			Delete(this[index]);
			base.RemoveItem(index);
		}

		protected override void SetItem(int index, ArgumentController item)
		{
			Insert(item, index);
			base.SetItem(index, item);
		}
		#endregion KeyedCollection

		#endregion Methods
	}
}



