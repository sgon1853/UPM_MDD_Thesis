// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using SIGEM.Client.Logics;
using SIGEM.Client.Presentation;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages IUInputFieldsController.
	/// </summary>
	public abstract class IUInputFieldsController: IUController
	{
		#region Members
		/// <summary>
		/// Input fields list.
		/// </summary>
		private ArgumentControllerList mInputFields = null; 
		/// <summary>
		/// True if the application has to catch the change value and enabled events.
		/// </summary>
		protected bool mEnabledChangeArgument = true;
		/// <summary>
		/// Indicates that exist values modified by the user
			/// </summary>
		private bool mPendingChanges = false;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets the class name.
		/// </summary>
		public abstract string ClassName { get; protected set; } 
		/// <summary>
		/// Gets mInputFields reference to private ArgumentControllerList.
		/// </summary>
		protected ArgumentControllerList InternalInputFields
		{
			get { return mInputFields; }
			set { mInputFields = value; }
		} 
		/// <summary>
		/// Gets or sets the input fields list.
		/// </summary>
		public virtual IArguments InputFields
		{
			get
			{
				return InternalInputFields;
			}
			protected set
			{
				if (InternalInputFields != null)
				{
					InternalInputFields.EnabledChanged -= new EventHandler<EnabledChangedEventArgs>(HandleInputFieldChangeEnabled);
					InternalInputFields.ValueChanged -= new EventHandler<ValueChangedEventArgs>(HandleInputFieldChangeValue);
					InternalInputFields.ExecuteCommand -= new EventHandler<ExecuteCommandEventArgs>(OnExecuteCommand);
				}
				InternalInputFields = value as ArgumentControllerList;

				if (InternalInputFields != null)
				{
					InternalInputFields.EnabledChanged += new EventHandler<EnabledChangedEventArgs>(HandleInputFieldChangeEnabled);
					InternalInputFields.ValueChanged += new EventHandler<ValueChangedEventArgs>(HandleInputFieldChangeValue);
					InternalInputFields.ExecuteCommand += new EventHandler<ExecuteCommandEventArgs>(OnExecuteCommand);
				}
			}
		}
		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		public new IUInputFieldsContext Context
		{
			get
			{
				return base.Context as IUInputFieldsContext;
			}
			set
			{
				base.Context = value;
			}
		}
		/// <summary>
		/// Gets or sets the parent controller.
		/// </summary>
		public new IUController Parent
		{
			get
			{
				return base.Parent as IUController;
			}
			set
			{
				base.Parent = value;
			}
        }
		/// <summary>
		/// Indicates that exist values modified by the user
		/// </summary>
		public bool PendingChanges
		{
			get
			{
				return mPendingChanges;
			}
			set
			{
				mPendingChanges = value;
			}
		}
        #endregion Properties

        #region Event Handlers
        /// <summary>
        /// Handles the Inut field ChangeEnabled event.
        /// </summary>
        /// <param name="sender">Argument reference.</param>
        /// <param name="changeValue">Event parameters.</param>
        private void HandleInputFieldChangeEnabled(object sender, EnabledChangedEventArgs changeEnable)
        {
            ProcessInputFieldChangeEnabled(changeEnable);
        }
        /// <summary>
        /// Methods suscribed to ChangeValue event. see set Arguments property.
        /// </summary>
        /// <param name="sender">Argument reference.</param>
        /// <param name="changeValue">Event parameters.</param>
        private void HandleInputFieldChangeValue(object sender, ValueChangedEventArgs changeValue)
        {
            OnChangeValue(changeValue);
        }
        /// <summary>
        /// Executes actions related to commands. Empty implementation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            ProcessExecuteCommand(sender, e);
        }
        #endregion Event Handlers

        #region Methods
        /// <summary>
        /// Actions related with the Execute Command events from the editors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ProcessExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
        }
        /// <summary>
		/// Clear Input Fields values.
		/// </summary>
		protected virtual void ClearInputFields()
		{
			// Clear input files context values.
			Context.ClearInputFields();

			// Clear controller values.
			foreach (ArgumentController argument in InputFields)
			{
				argument.Enabled = true;
				argument.Value = null;
			}
		}
		/// <summary>
		/// Sets the context data and configurations to input fields controller.
		/// </summary>
		/// <param name="context">IUInputFieldsContext reference.</param>
		protected virtual void ConfigureInputFieldsByContext(IUInputFieldsContext context)
		{
			mEnabledChangeArgument = false;

			if (context != null)
			{				
				// Set the input fields values from context values.
				foreach (ArgumentController lArgument in InputFields)
				{
					// Preload population associated to the argument.
					if (context.GetInputFieldPreloadPopulationInitialized(lArgument.Name))
					{
						ArgumentOVPreloadController lOVPreloadArgument = lArgument as ArgumentOVPreloadController;
						if (lOVPreloadArgument != null)
						{
							lOVPreloadArgument.SetPopulation(context.GetInputFieldPreloadPopulation(lArgument.Name));
							context.SetInputFieldPreloadPopulationInitialized(lArgument.Name, false);
						}
					}

					// Set Value.
					lArgument.Value = context.GetInputFieldValue(lArgument.Name);

					// Set Enable.
					lArgument.Enabled = context.GetInputFieldEnabled(lArgument.Name);

					// Set Mandatory.
					lArgument.Mandatory = context.GetInputFieldMandatory(lArgument.Name);
					// Set Visible.
					lArgument.Visible = context.GetInputFieldVisible(lArgument.Name);
					ArgumentOVController lOVArgument = lArgument as ArgumentOVController;
					// When the argument is OV, it is possible to exits multiselection
					if (lOVArgument != null)
					{
						if ((lOVArgument.Enabled) && (lOVArgument.MultiSelectionAllowed))
						{
							List<Oid> lInstancesSelected = lOVArgument.Value as List<Oid>;
							if ((lInstancesSelected != null) && (lInstancesSelected.Count > 1))
							{
								// Is there is multiselection and more than one selected,
								// the oid editors must be disable.
								lOVArgument.EnabledEditors = false;
							}
						}
					}
				}
				foreach (ArgumentController lArgument in InputFields)
				{
					// Set the focus to the editor.
					if (context.GetInputFieldFocused(lArgument.Name))
					{
						lArgument.Focused = true;
						// After setting the focus to the editor, 
						// the focus of the input fields is cleared.
						context.ClearInputFieldsFocus();
						break;
					}
				}
			}

			mEnabledChangeArgument = true;
		}
		/// <summary>
		/// Create and build context structure to this controller.
		/// </summary>
		protected virtual void ConfigureInitialContext()
		{
			// Only the first time is called, before create new arguments context.
			Context.InputFields.Clear();
			
			// Initialize the context with the information from the arguments
			foreach (ArgumentController lArgument in InputFields)
			{
				// if argument does not exist it is created (Gets or Create).
				IUContextArgumentInfo lContextArgumentInfo = Context.GetInputField(lArgument.Name);

				// Set Value and Enabled to the context
				lContextArgumentInfo.Value = lArgument.Value;
				lContextArgumentInfo.Enabled = true;

				lContextArgumentInfo.Visible = true;
				lContextArgumentInfo.NullAllowed = lArgument.NullAllowed;
				lContextArgumentInfo.Mandatory = lArgument.Mandatory;

				lContextArgumentInfo.MultiSelectionAllowed = lArgument.MultiSelectionAllowed;

				// If is Preload set displayset attributes & OrderCriteria.
				ArgumentOVPreloadController lArgumentOVPreload = lArgument as ArgumentOVPreloadController;
				if (lArgumentOVPreload != null)
				{
					lContextArgumentInfo.SupplementaryInfo = lArgumentOVPreload.Editor.DisplaySetAttributes;

					if (lArgumentOVPreload.OrderCriteria != null)
					{
						lContextArgumentInfo.OrderCriteria = lArgumentOVPreload.OrderCriteria.Name;
					}
				}
			}

			base.UpdateContext();
		}		
		/// <summary>
		/// Set arguments Context values from arguments Controllers values.
		/// </summary>
		public override void UpdateContext()
		{
			foreach (ArgumentController lArgument in InputFields)
			{
				// Argument value.
				Context.SetInputFieldValue(lArgument.Name, lArgument.Value);

				// Argument enable.
				Context.SetInputFieldEnabled(lArgument.Name, lArgument.Enabled);

				ArgumentOVPreloadController lArgumentOVPreload = lArgument as ArgumentOVPreloadController;
				if (lArgumentOVPreload != null)
				{
					Context.SetInputFieldSupplementaryInfo(lArgument.Name, lArgumentOVPreload.Editor.DisplaySetAttributes);

					if (lArgumentOVPreload.OrderCriteria != null)
					{
						Context.SetInputFieldOrderCriteria(lArgument.Name,lArgumentOVPreload.OrderCriteria.Name);
					}
				}
			
				// Set Multi-Selection.
				Context.SetMultiSelectionInputFieldValue(lArgument.Name, lArgument.MultiSelectionAllowed);
			}

			base.UpdateContext();
		}
			

		/// <summary>
		/// Implemented in derived classes to call Logic Execute Default Values.
		/// </summary>
		/// <param name="context">Context structure.</param>
		protected virtual void ExecuteDefaultValues(IUInputFieldsContext context)
		{
			Logic.ExecuteDefaultValues(context);
		}
		/// <summary>
		/// Implemented in derived classes to call Logic Load from Context Values. Sets context values in controller values.
		/// </summary>
		/// <param name="context">Context structure.</param>
		protected virtual void ExecuteLoadFromContext(IUInputFieldsContext context)
		{
			Logic.ExecuteLoadFromContext(context);
		}		
		/// <summary>
		/// Execute Dependence Rules.
		/// </summary>
		public virtual void ExecuteDependenceRules(ArgumentEventArgs argumentEventArgs)
		{

			if (!mEnabledChangeArgument)
			{
				return;
			}

			UpdateContext();

			Context.SelectedInputField = argumentEventArgs.Name;

			// Execute Dependency Rules
			try
			{
				// if the change is from value.
				ValueChangedEventArgs lChangeValueEventArgs = argumentEventArgs as ValueChangedEventArgs;
				if (lChangeValueEventArgs != null)
				{
					PendingChanges = true;
					Logic.ExecuteDependencyRules(
						Context, 
						lChangeValueEventArgs.OldValue, 
						DependencyRulesEventLogic.SetValue, 
						argumentEventArgs.Agent);
				}

				// if the change is from enable.
				EnabledChangedEventArgs lChangeEnableEventArgs = argumentEventArgs as EnabledChangedEventArgs;
				if (lChangeEnableEventArgs != null)
				{
					Logic.ExecuteDependencyRules(
							Context,
							lChangeEnableEventArgs.Enabled,
							DependencyRulesEventLogic.SetActive,
							argumentEventArgs.Agent);
				}
			}
			catch (Exception logicException)
			{
				ScenarioManager.LaunchErrorScenario(logicException);
			}

			// Delete argument selection
			Context.SelectedInputField  = string.Empty;

			// Set context
			ConfigureByContext(Context);
		}
		/// <summary>
		/// Perform action Execute in derived classes.
		/// </summary>
		/// <returns></returns>
		public abstract bool Execute();
		/// <summary>
		/// Perform action Cancel in derived classes.
		/// </summary>
		/// <returns></returns>
		public abstract bool Cancel();
		/// <summary>
		/// Abstract method tha must be implemented in derived class to capture change Enable values.
		/// </summary>
		/// <param name="changeEnable">Event parameters.</param>
		protected virtual void ProcessInputFieldChangeEnabled(EnabledChangedEventArgs changeEnable)
		{
			ExecuteDependenceRules(changeEnable);
		}
		/// <summary>
		/// Abstract method tha must be implemented in derived class to capture change Value values.
		/// </summary>
		/// <param name="changeEnable">Event parameters.</param>
		protected virtual void OnChangeValue(ValueChangedEventArgs changeValue)
		{
			ExecuteDependenceRules(changeValue);
		}
		/// <summary>
		/// Executes actions related to Ok trigger event.
		/// </summary>
        protected override void ProcessExecuteOk()
		{
			// Call Execute Method in derived Classes.
			if (Execute())
			{
				base.ProcessExecuteOk();
			}
		}
        #endregion Methods
    }
}


