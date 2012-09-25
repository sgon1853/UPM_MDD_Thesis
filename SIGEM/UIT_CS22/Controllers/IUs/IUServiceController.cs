// v3.8.4.5.b
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using SIGEM.Client.Presentation;
using SIGEM.Client.Adaptor;
using SIGEM.Client.Logics;
using SIGEM.Client.Oids;
using SIGEM.Client.Adaptor.Exceptions;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the IUServiceController.
	/// </summary>
	public class IUServiceController: IUInputFieldsController, IActionItemEvents
	{
		#region Members

		#region This Argument
		/// <summary>
		/// Argument This [private]
		/// </summary>
		private ArgumentController mArgumentThis;
		#endregion This Argument

		#region ClassName
		/// <summary>
		/// Class name.
		/// </summary>
		private string mClassName;
		#endregion ClassName

		#region Interaction Unit Service Name
		/// <summary>
		/// Interaction Unit Service name.
		/// </summary>
		private string mInteractionUnitServiceName;
		#endregion Interaction Unit Service Name

		#region Outbound Arguments Members

		#region Outbound Arguments
		/// <summary>
		/// Outbound argument list [private]
		/// </summary>
		private ArgumentControllerList mOutputFields;
		#endregion Outbound Arguments

		#region Outbound Target Scenario
		/// <summary>
		/// Target scenario to show outbound arguments.
		/// </summary>
		private string mOutboundArgumentsScenario;
		#endregion Outbound Target Scenario

		#region Is Outbound Service Controller
		/// <summary>
		/// If the controller is a controller for outbound arguments scenario.
		/// </summary>
		private bool mIsOutboundArgumentController = false;
		#endregion Is Outbound Service Controller

		#endregion Outbound Arguments Members

		#region Next & Previous Presentation Members
		/// <summary>
		/// Indicates if the service includes the Next-Previous management.
		/// </summary>
		private bool mNextPreviousFeature;
		/// <summary>
		/// Presentation of the Previous trigger.
		/// </summary>
		private ITriggerPresentation mPreviousTrigger;
		/// <summary>
		/// Presentation of the Next trigger.
		/// </summary>
		private ITriggerPresentation mNextTrigger;
		/// <summary>
		/// Presentation of the Apply trigger.
		/// </summary>
		private ITriggerPresentation mApplyTrigger;
		/// <summary>
		/// Presentation of the Apply & Previous trigger.
		/// </summary>
		private ITriggerPresentation mApplyPreviousTrigger;
		/// <summary>
		/// Presentation of the Apply & Next trigger.
		/// </summary>
		private ITriggerPresentation mApplyNextTrigger;

		#endregion Next & Previous Presentation Members

		#region Multi Execution Members

		#region Cancel Window
		/// <summary>
		/// Service cancel window.
		/// </summary>
		private CancelWindow mCancelWindow = null; // For cancel the multiple execution
		#endregion Cancel Window

		#endregion Multi Execution Members

		/// <summary>
		/// List of agents of the DisplaySet item.
		/// </summary>
		private List<string> mAgents;

		#region State Change Detection flags
		/// <summary>
		/// Show previous and current values in case of an error by State Change Detection.
		/// </summary>
		private bool mShowValuesInSCDError = false;
		/// <summary>
		/// Allow to the final user to retry service execution in case of an error by State Change Detection.
		/// </summary>
		private bool mRetryInSCDError = false;
		#endregion State Change Detection flags
        /// <summary>
        /// Show service form
        /// </summary>
        private bool mShowScenario = true;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'IUServiceController', class name and service name are indicated.
		/// </summary>
		/// <param name="name">Name of the Interaction Unit.</param>
		/// <param name="alias">Alias of the Interaction Unit.</param>
		/// <param name="idXML">IdXML of the Interaction Unit.</param>
		/// <param name="agents">List of agents.</param>
		/// <param name="className">Class name.</param>
		/// <param name="serviceName">Service name.</param>
		/// <param name="context">Context.</param>
		/// <param name="parent">Parent controller.</param>
		public IUServiceController(string name, string alias, string idXML, string[] agents, string className, string interactionUnitServiceName, IUContext context, IUController parent)
			: this(name, alias, idXML, agents, className, interactionUnitServiceName, context, parent, false) { }
		/// <summary>
		/// Initializes a new instance of 'IUServiceController', class name and service name are indicated.
		/// </summary>
		/// <param name="name">Name of the Interaction Unit.</param>
		/// <param name="alias">Alias of the Interaction Unit.</param>
		/// <param name="idXML">IdXML of the Interaction Unit.</param>
		/// <param name="agents">List of agents.</param>
		/// <param name="className">Class name.</param>
		/// <param name="serviceName">Service name.</param>
		/// <param name="context">Context.</param>
		/// <param name="parent">Parent controller.</param>
		/// <param name="nextPreviousFeature">Indicates whether the Next & Previous feature is enabled or not.</param>
		public IUServiceController(
			string name,
			string alias,
			string idXML,
			string[] agents,
			string className,
			string interactionUnitServiceName,
			IUContext context,
			IUController parent,
			bool nextPreviousFeature)
			: base()
		{
			// Service Name.
			Name = name;

			// Interaction Unit Alias.
			Alias = alias;

			// Interaction Unit Identify.
			IdXML = idXML;

			List<string> lAgents = new List<string>(agents);
			Agents = lAgents;

			// Interaction Unit Class Name.
			ClassName = className;

			// Interaction Unit Service Name.
			InteractionUnitServiceName = interactionUnitServiceName;

			// Interaction Unit Context.
			Context = (IUServiceContext)context;

			// Interaction Unit Parent.
			Parent = parent;

			// Allow/Disable next previous feature
			NextPreviousFeature = nextPreviousFeature;

			// Allow Input Argument List
			InputFields = new ArgumentControllerList();

			// Allow Output Argument List
			OutputFields = new ArgumentControllerList();

		}
		#endregion Constructors
		
		#region Properties
		/// <summary>
		/// Gets or sets the Argument This (OID of Class).
		/// </summary>
		public ArgumentController ArgumentThis
		{
			get
			{
				return mArgumentThis;
			}
			set
			{
				mArgumentThis = value;
			}
		}
		/// <summary>
		/// Gets the class name where is defined this service.
		/// </summary>
		public override string ClassName
		{
			get
			{
				return mClassName;
			}
			protected set
			{
				mClassName = value;
			}
		}
		/// <summary>
		/// Gets the service name.
		/// </summary>
		public virtual string InteractionUnitServiceName
		{
			get
			{
				return mInteractionUnitServiceName;
			}
			protected set
			{
				mInteractionUnitServiceName = value;
			}
		}
		/// <summary>
		/// Overrided Scenario for suscrib Execute Command event
		/// </summary>
		public override IScenarioPresentation Scenario
		{
			get
			{
				return base.Scenario;
			}
			set
			{
				if (base.Scenario != null)
				{
					base.Scenario.ExecuteCommand -= new EventHandler<ExecuteCommandEventArgs>(HandleScenarioExecuteCommand);
				}

				base.Scenario = value;

				if (base.Scenario != null)
				{
					base.Scenario.ExecuteCommand += new EventHandler<ExecuteCommandEventArgs>(HandleScenarioExecuteCommand);
				}
			}
		}
		/// <summary>
		/// Gets or sets Service Context.
		/// </summary>
		public new IUServiceContext Context
		{
			get
			{
				return mContext as IUServiceContext;
			}
			set
			{
				mContext = value;
			}
		}
		/// <summary>
		/// Gets the selected instances of the IU.
		/// </summary>
		public override List<Oid> InstancesSelected
		{
			get
			{
				// Return This
				if (ArgumentThis != null)
				{
					return ArgumentThis.Value as List<Oid>;
				}

				// Default value
				return new List<Oid>();
			}
		}
		/// <summary>
		/// Gets & Sets interna Output Fields.
		/// </summary>
		protected ArgumentControllerList InternalOutputFields
		{
			get { return mOutputFields; }
			set { mOutputFields = value; }
		}

		/// <summary>
		/// Gets the Outbound argument list
		/// </summary>
		public virtual IArguments OutputFields
		{
			get
			{
				return InternalOutputFields;
			}
			protected set
			{
				InternalOutputFields = value as ArgumentControllerList;
			}
		}
		/// <summary>
		/// Gets or sets the target scenario for showing the outbound arguments of the service.
		/// </summary>
		public string OutboundArgumentsScenario
		{
			get
			{
				return mOutboundArgumentsScenario;
			}
			set
			{
				mOutboundArgumentsScenario = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the controller is a controller for outbound or inbound arguments.
		/// </summary>
		public bool IsOutboundArgumentController
		{
			get
			{
				return mIsOutboundArgumentController;
			}
			set
			{
				mIsOutboundArgumentController = value;
			}
		}

		#region Next & Previous Presentation Properties
		/// <summary>
		/// Gets if the service includes the Next-Previous management.
		/// </summary>
		public virtual bool NextPreviousFeature
		{
			get { return mNextPreviousFeature; }
			protected set { mNextPreviousFeature = value; }
		}
		/// <summary>
		/// Gets or sets the Previous trigger.
		/// </summary>
		public ITriggerPresentation PreviousTrigger
		{
			get
			{
				return mPreviousTrigger;
			}
			set
			{
				if (mPreviousTrigger != null)
				{
					mPreviousTrigger.Triggered -= new EventHandler<TriggerEventArgs>(HandleExecutePrevious);
				}
				mPreviousTrigger = value;
				if (mPreviousTrigger != null)
				{
					mPreviousTrigger.Triggered += new EventHandler<TriggerEventArgs>(HandleExecutePrevious);
				}
			}
		}
		/// <summary>
		/// Gets or sets the Next trigger.
		/// </summary>
		public ITriggerPresentation NextTrigger
		{
			get
			{
				return mNextTrigger;
			}
			set
			{
				if (mNextTrigger != null)
				{
					mNextTrigger.Triggered -= new EventHandler<TriggerEventArgs>(HandleExecuteNext);
				}
				mNextTrigger = value;
				if (mNextTrigger != null)
				{
					mNextTrigger.Triggered += new EventHandler<TriggerEventArgs>(HandleExecuteNext);
				}
			}
		}
		/// <summary>
		/// Gets or sets the Apply trigger.
		/// </summary>
		public ITriggerPresentation ApplyTrigger
		{
			get
			{
				return mApplyTrigger;
			}
			set
			{
				if (mApplyTrigger != null)
				{
					mApplyTrigger.Triggered -= new EventHandler<TriggerEventArgs>(HandleExecuteApply);
				}
				mApplyTrigger = value;
				if (mApplyTrigger != null)
				{
					mApplyTrigger.Triggered += new EventHandler<TriggerEventArgs>(HandleExecuteApply);
				}
			}
		}
		/// <summary>
		/// Gets or sets the Apply & Previous trigger.
		/// </summary>
		public ITriggerPresentation ApplyPreviousTrigger
		{
			get
			{
				return mApplyPreviousTrigger;
			}
			set
			{
				if (mApplyPreviousTrigger != null)
				{
					mApplyPreviousTrigger.Triggered -= new EventHandler<TriggerEventArgs>(HandleExecuteApplyPrevious);
				}
				mApplyPreviousTrigger = value;
				if (mApplyPreviousTrigger != null)
				{
					mApplyPreviousTrigger.Triggered += new EventHandler<TriggerEventArgs>(HandleExecuteApplyPrevious);
				}
			}
		}
		/// <summary>
		/// Gets or sets the Apply & Next trigger.
		/// </summary>
		public ITriggerPresentation ApplyNextTrigger
		{
			get
			{
				return mApplyNextTrigger;
			}
			set
			{
				if (mApplyNextTrigger != null)
				{
					mApplyNextTrigger.Triggered -= new EventHandler<TriggerEventArgs>(HandleExecuteApplyNext);
				}
				mApplyNextTrigger = value;
				if (mApplyNextTrigger != null)
				{
					mApplyNextTrigger.Triggered += new EventHandler<TriggerEventArgs>(HandleExecuteApplyNext);
				}
			}
		}
		#endregion Next & Previous Presentation Properties

		/// <summary>
		/// Check if it is a service multiexecution.
		/// </summary>
		/// <returns></returns>
		public virtual bool IsMultiexecution
		{
			get
			{
				// Control the multiexecution for all the inbound arguments.
				foreach (ArgumentController lInboundArgument in InputFields)
				{
					// Control the multi selection allowed for the inbound argument.
					if (lInboundArgument.MultiSelectionAllowed)
					{
						List<Oid> lOids = lInboundArgument.Value as List<Oid>;
						// If there are more than one oid there is multiexecution.
						if (lOids != null && lOids.Count > 1)
						{
							return true;
						}
					}
				}
				return false;
			}
		}
		/// <summary>
		/// Gets or sets the list of agents that can acces to the Service.
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
		/// Show previous and current values in case of an error by State Change Detection.
		/// </summary>
		public bool ShowValuesInSCDError
		{
			get
			{
				return mShowValuesInSCDError;
			}
			set
			{
				mShowValuesInSCDError = value;
			}
		}
		/// <summary>
		/// Allow final user to retry service execution in case of an error by State Change Detection.
		/// </summary>
		public bool RetryInSCDError
		{
			get
			{
				return mRetryInSCDError;
			}
			set
			{
				mRetryInSCDError = value;
			}
		}
        /// <summary>
        /// Show Service form
        /// </summary>
        public bool ShowScenario
        {
            get
            {
                // Check if conditions are satisfied
                if (!mShowScenario)
                {
                    if (InputFields.Count != 1)
                        return true;

                    ArgumentOVControllerAbstract lArgumentOV = InputFields[0] as ArgumentOVControllerAbstract;
                    if (lArgumentOV == null || lArgumentOV.Value == null)
                        return true;
                }

                return mShowScenario;
            }
            set
            {
                mShowScenario = value;
            }
        }
		#endregion Properties

		#region Events
		/// <summary>
		/// Before Execute event.
		/// </summary>
		public event EventHandler<CheckForPendingChangesEventArgs> BeforeExecute;
		#endregion Events

		#region Event Handlers
		/// <summary>
		/// Private implementatino for Execute Commads.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleScenarioExecuteCommand(object sender, ExecuteCommandEventArgs e)
		{
			ProcessScenarioExecuteCommand(sender, e);
		}
		/// <summary>
		/// This event occurs when the Previous is triggered.
		/// </summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">TriggerEventArgs.</param>
		private void HandleExecutePrevious(Object sender, TriggerEventArgs e)
		{
			ProcessExecutePrevious(sender, e);
		}

		/// <summary>
		/// This event occurs when the Next is triggered.
		/// </summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">TriggerEventArgs.</param>
		private void HandleExecuteNext(Object sender, TriggerEventArgs e)
		{
			ProcessExecuteNext(sender, e);
		}
		/// <summary>
		/// This event occurs when the Apply is triggered. It also executes the service and
		/// if the result is sucesfull, it disables the Apply and Ok buttons.
		/// </summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">TriggerEventArgs.</param>
		private void HandleExecuteApply(Object sender, TriggerEventArgs e)
		{
			ProcessExecuteApply(sender, e);
		}
		/// <summary>
		/// This event occurs when the Apply & Previous is triggered. It also executes
		/// the service and moves to the previous instance.
		/// </summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">TriggerEventArgs.</param>
		private void HandleExecuteApplyPrevious(Object sender, TriggerEventArgs e)
		{
			ProcessExecuteApplyPrevious(sender, e);
		}
		/// <summary>
		/// This event occurs when the Apply & Next is triggered. It alsp executes
		/// the service and moves to the next instance.
		/// </summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">TriggerEventArgs.</param>
		private void HandleExecuteApplyNext(Object sender, TriggerEventArgs e)
		{
			ProcessExecuteApplyNext(sender, e);
		}
		#endregion Event Handlers

		#region Process events
		/// <summary>
		/// Related actions with the Scenario Execute Command event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessScenarioExecuteCommand(object sender, ExecuteCommandEventArgs e)
		{
			switch (e.ExecuteCommandType)
			{
				case ExecuteCommandType.ExecuteGoNextInstance:
					ProcessExecuteNext(sender, new TriggerEventArgs());
					break;
				case ExecuteCommandType.ExecuteGoPreviousInstance:
					ProcessExecutePrevious(sender, new TriggerEventArgs());
					break;
				case ExecuteCommandType.ExecuteServiceAndGoNextInstance:
					ProcessExecuteApplyNext(sender, new TriggerEventArgs());
					break;
				case ExecuteCommandType.ExecuteServiceAndGoPreviousInstance:
					ProcessExecuteApplyPrevious(sender, new TriggerEventArgs());
					break;
				case ExecuteCommandType.ExecuteService:
					ProcessExecuteApply(sender, new TriggerEventArgs()); 
					break;
				case ExecuteCommandType.ExecuteClose:
					ProcessExecuteCancel();
					break;
				default:
					break;
			}
		}
		/// <summary>
		/// Related actions with the Execute Next event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessExecuteNext(object sender, TriggerEventArgs e)
		{
			if (NextTrigger != null && NextTrigger.Enabled && NextTrigger.Visible)
			{
				Execute_GoNextPrevious(true);
			}
		}
		/// <summary>
		/// Related actions with the Execute Previous event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessExecutePrevious(object sender, TriggerEventArgs e)
		{
			if (PreviousTrigger != null && PreviousTrigger.Enabled && PreviousTrigger.Visible)
			{
				Execute_GoNextPrevious(false);
			}
		}
		/// <summary>
		/// Related actions with the Execute Apply event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessExecuteApply(object sender, TriggerEventArgs e)
		{
			if (ApplyTrigger != null && ApplyTrigger.Enabled && ApplyTrigger.Visible)
			{
				// Execute the service.
				if (!ExecuteService(true))
				{
					return;
				}

				// Refresh the current instance.
				RefreshRequiredInstancesEventArgs arguments = new RefreshRequiredInstancesEventArgs(Context.ExchangeInformation.SelectedOids, Context.ExchangeInformation);
                OnRefreshRequired(arguments);

				// Disable the Apply and Ok buttons.
				ApplyTrigger.Enabled = false;
				OkTrigger.Enabled = false;
			}
		}
		/// <summary>
		/// Related actions with the Execute Apply Next event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessExecuteApplyNext(object sender, TriggerEventArgs e)
		{
			if (this.ApplyNextTrigger != null && ApplyNextTrigger.Enabled && ApplyNextTrigger.Visible)
			{

				// Execute the service.
				if (!ExecuteService(true))
				{
					return;
				}

				// Refresh the current instance
                RefreshRequiredInstancesEventArgs arguments = new RefreshRequiredInstancesEventArgs(Context.ExchangeInformation.SelectedOids, Context.ExchangeInformation);
                OnRefreshRequired(arguments);

				// Move to the next intance.
				Execute_GoNextPrevious(true);
			}
		}
		/// <summary>
		/// Related actions with the Execute Apply Previous event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ProcessExecuteApplyPrevious(object sender, TriggerEventArgs e)
		{
			if (ApplyPreviousTrigger != null && ApplyPreviousTrigger.Enabled && ApplyPreviousTrigger.Visible)
			{
				// Execute the service.
				if (!ExecuteService(true))
				{
					return;
				}

				// Refresh the current instance.
                RefreshRequiredInstancesEventArgs arguments = new RefreshRequiredInstancesEventArgs(Context.ExchangeInformation.SelectedOids, Context.ExchangeInformation);
                OnRefreshRequired(arguments);

				// Move to the previous intance.
				Execute_GoNextPrevious(false);
			}
		}
		/// <summary>
		/// Process ExecuteCommand event from the Input fields
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void ProcessExecuteCommand(object sender, ExecuteCommandEventArgs e)
		{
			base.ProcessExecuteCommand(sender, e);

			// If it is associated (no scenario), then execute the service
			if (e.ExecuteCommandType == ExecuteCommandType.ExecuteRefresh && Scenario == null)
			{
				// Avoiding dependency rules execution as consequence of changing any inbound argument.
				this.mEnabledChangeArgument = false;
				Execute();
				this.mEnabledChangeArgument = true;
			}
		}
		#endregion Process events

		#region Event Raisers
		/// <summary>
		/// Raises the SelectNextInstance event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnSelectNextInstance(SelectNextPreviousInstanceEventArgs eventArgs)
		{
			EventHandler<SelectNextPreviousInstanceEventArgs> handler = SelectNextInstance;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the SelectPreviousInstance event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnSelectPreviousInstance(SelectNextPreviousInstanceEventArgs eventArgs)
		{
			EventHandler<SelectNextPreviousInstanceEventArgs> handler = SelectPreviousInstance;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the ServiceResponse event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnServiceResponse(ServiceResultEventArgs eventArgs)
		{
			EventHandler<ServiceResultEventArgs> handler = ServiceResponse;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the RefreshInstance event
		/// </summary>
		/// <param name="eventArgs"></param>
        protected virtual void OnRefreshRequired(RefreshRequiredEventArgs eventArgs)
		{
			EventHandler<RefreshRequiredEventArgs> handler = RefreshRequired;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		/// <summary>
		/// Raises the BeforeExecute event.
		/// </summary>
		/// <param name="eventArgs"></param>
		protected virtual void OnBeforeExecute(CheckForPendingChangesEventArgs eventArgs)
		{
			EventHandler<CheckForPendingChangesEventArgs> handler = BeforeExecute;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		#endregion Event Raisers

		#region Methods
		/// <summary>
		/// Enables the Scenario
		/// </summary>
		/// <param name="enabled"></param>
		public void Enable(bool enabled)
		{
			mEnabledChangeArgument = false;
			foreach (ArgumentController lArgument in InputFields)
			{
				lArgument.Enabled = enabled;
			}
			mEnabledChangeArgument = true;

			if (OkTrigger != null)
			{
				OkTrigger.Enabled = enabled;
			}

			if (CancelTrigger != null)
			{
				CancelTrigger.Enabled = enabled;
			}
		}
		/// <summary>
		/// Initializes the service properties.
		/// </summary>
		public override void Initialize()
		{
            PendingChanges = false;

			// If the controller is a controller for Outbound arguments.
			if (IsOutboundArgumentController)
			{
				IUServiceContext lPreviuosServiceContext = Context.ExchangeInformation.Previous as IUServiceContext;
				if (lPreviuosServiceContext != null)
				{
					foreach (ArgumentController lOutboundArgument in OutputFields)
					{
						// Get the output field value.
						object lOutputFieldValue = lPreviuosServiceContext.GetOutputFieldValue(lOutboundArgument.Name);
						// If the output field is data-valued and boolean, apply multilanguage.
						ArgumentDVController lOutboundArgumentDV = lOutboundArgument as ArgumentDVController;
						if ((lOutboundArgumentDV != null) && (lOutboundArgumentDV.ModelType == ModelType.Bool) && (lOutputFieldValue != null))
						{
							//Text protection for boolean values when these are represented as CheckBox control 
							if (!(lOutboundArgumentDV.Editor is Presentation.Forms.CheckBoxPresentation))
							{
								// Check the boolean value.
								bool? lBoolValue;
								try
								{
									lBoolValue = Convert.ToBoolean(lOutputFieldValue);
								}
								catch
								{
									lBoolValue = null;
								}

								// Apply multilanguage to the boolean value.
								switch (lBoolValue)
								{
									case true:
										lOutputFieldValue = CultureManager.TranslateString(LanguageConstantKeys.L_BOOL_TRUE, LanguageConstantValues.L_BOOL_TRUE);
										break;
									case false:
										lOutputFieldValue = CultureManager.TranslateString(LanguageConstantKeys.L_BOOL_FALSE, LanguageConstantValues.L_BOOL_FALSE);
										break;
									default:
										lOutputFieldValue = CultureManager.TranslateString(LanguageConstantKeys.L_BOOL_NULL, LanguageConstantValues.L_BOOL_NULL);
										break;
								}
							}
						}

						// Actualize context.
						Context.SetOutputFieldValue(lOutboundArgument.Name, lOutputFieldValue);

						// SERVICE MULTILANGUAGE: Apply multilanguage to the argument label.
						lOutboundArgument.Initialize();
						// END MULTILANGUAGE
					}
				}
			}
			else
			{
				// Initialize Input Fields
				// Enable or disable the Next-Previous feature.
				SetNextPreviousFeature();

				// SERVICE MULTILANGUAGE: Apply multilanguage to the Inbound and Outbound arguments of the service.
				foreach (ArgumentController lInboundArgument in InputFields)
				{
					// Apply multilanguage to the argument label.
					lInboundArgument.Initialize();
				}
				// END MULTILANGUAGE
				ClearInputFields();

				// Configure initial context
				ConfigureInitialContext();

				// Load defaul values.
				try
				{
					// Logic API call.
					Logic.ExecuteDefaultValues(Context);
				}
				catch (Exception logicException)
				{
					ScenarioManager.LaunchErrorScenario(logicException);
				}

				// Load valued from context.
				try
				{
					// Logic API call.
					Logic.ExecuteLoadFromContext(Context);
				}
				catch (Exception logicException)
				{
					ScenarioManager.LaunchErrorScenario(logicException);
				}
			}
			base.Initialize();
		}

		#region Next & Previous Methods

		/// <summary>
		/// Apply Next Previous Feature if we have changed the value of the parameter in the Service Controller Factory.
		/// </summary>
		private void SetNextPreviousFeature()
		{
			bool hideButtons = false;

			// Conditions to activate the Next-Previous feature:
			// 1. Desired option (flag = true);
			// 2. Previous context must be a Population context, it must contain a list of instances;
			// 3. Only one instance must be selected;
			IUPopulationContext prevContext = Context.ExchangeInformation.Previous as IUPopulationContext;
			if (mNextPreviousFeature == false ||
				prevContext == null ||
				Context.ExchangeInformation == null ||
				Context.ExchangeInformation.SelectedOids == null ||
				Context.ExchangeInformation.SelectedOids.Count != 1)
			{
				hideButtons = true;
			}

			if (hideButtons)
			{
				if (PreviousTrigger != null)
				{
					PreviousTrigger.Visible = false;
				}
				if (NextTrigger != null)
				{
					NextTrigger.Visible = false;
				}
				if (ApplyTrigger != null)
				{
					ApplyTrigger.Visible = false;
				}
				if (ApplyPreviousTrigger != null)
				{
					ApplyPreviousTrigger.Visible = false;
				}
				if (ApplyNextTrigger != null)
				{
					ApplyNextTrigger.Visible = false;
				}
			}
			else
			{
				if (PreviousTrigger != null)
				{
					PreviousTrigger.Visible = true;
				}
				if (NextTrigger != null)
				{
					NextTrigger.Visible = true;
				}
				if (ApplyTrigger != null)
				{
					ApplyTrigger.Visible = true;
				}
				if (ApplyPreviousTrigger != null)
				{
					ApplyPreviousTrigger.Visible = true;
				}
				if (ApplyNextTrigger != null)
				{
					ApplyNextTrigger.Visible = true;
				}
			}
		}
		/// <summary>
		/// Select the next or previous intance in the population.
		/// </summary>
		/// <param name="goToNext"></param>
		private void Execute_GoNextPrevious(bool goToNext)
		{
			// Move next or previous instance.
			SelectNextPreviousInstanceEventArgs arguments = new SelectNextPreviousInstanceEventArgs(Context.ExchangeInformation.SelectedOids[0], false, goToNext);
			if (goToNext)
			{
				OnSelectNextInstance(arguments);
			}
			else
			{
				OnSelectPreviousInstance(arguments);
			}

			// If no previous instace, close scenario and exit.
			if (arguments.NewSelectedInstance == null)
			{
				// Disable-Enable Next-Previous buttons.
				PreviousTrigger.Enabled = goToNext;
				NextTrigger.Enabled = !goToNext;
			}
			else
			{
				// Enable Next-Previous buttons.
				if (!PreviousTrigger.Enabled)
				{
					PreviousTrigger.Enabled = true;
				}

				if (!NextTrigger.Enabled)
				{
					NextTrigger.Enabled = true;
				}

				// Assign the new received instance to the Exchange info.
				Context.ExchangeInformation.SelectedOids = arguments.NewSelectedInstance;

				// Reinitialize the controller. Starts again.
				Initialize();
			}

			// Enable the Apply and Ok Buttons.
			if (ApplyTrigger != null)
			{
				ApplyTrigger.Enabled = true;
			}
			OkTrigger.Enabled = true;
		}
		#endregion Next & Previous Methods

		/// <summary>
		/// Sets the context data and configurations to arguments controller.
		/// </summary>
		/// <param name="context">Reference to Service Context.</param>
		public override void ConfigureByContext(IUContext context)
		{
			IUServiceContext lContext = context as IUServiceContext;

			//---------------------------
			// InBound Arguments.
			//---------------------------
			// If the controller is a controller for inbound arguments.
			if(!IsOutboundArgumentController)
			{
				ConfigureInputFieldsByContext(context as IUInputFieldsContext);
			}
			//---------------------------
			// OutBound Arguments.
			//---------------------------
			else
			{
				// Argument values
				foreach (ArgumentController lArgument in OutputFields)
				{
					// Sets Argument values.
					lArgument.Value = lContext.GetOutputFieldValue(lArgument.Name);
				}
			}
			// Default
			base.ConfigureByContext(context);
		}
		/// <summary>
		/// Updates the service context.
		/// </summary>
		public override void UpdateContext()
		{
			// Clean the existing data
			Context.ClearInputFields();

			// Argument selected
			ArgumentController lArgumentSelected = InputFields.ArgumentSelected;
			if (lArgumentSelected != null)
			{
				Context.SelectedInputField = lArgumentSelected.Name;
			}
			else
			{
				Context.SelectedInputField = string.Empty;
			}

			// Default
			base.UpdateContext();
		}
		/// <summary>
		/// Executes the service, including multiexecution and conditional navigation if defined.
		/// </summary>
		/// <returns>True if the service has been successfully executed, False in the other cases.</returns>
		public override bool Execute()
		{
			// Check if the inbound argument values are correct.
			if (!this.CheckNullAndFormatArgumentValues())
			{
				return false;
			}
			// Execute Service.
			bool lResult = ExecuteService(false);
			PendingChanges = !lResult;
			bool lIsMultiExecution = IsMultiexecution;

			#region Execute ConditionalNavigations
			ExchangeInfoConditionalNavigation lExchangeInfoConditionalNavigation = null;
			// Execute the conditional navigational when there is no multiexecution.
			if (!lIsMultiExecution)
			{
				// Logic API call.
				lExchangeInfoConditionalNavigation = Logic.ExecuteConditionalNavigation(Context);
			}
			#endregion Execute ConditionalNavigations

			// If don't have Conditional Navigation and the Service was not success
			if ((lExchangeInfoConditionalNavigation == null) && (!lResult))
			{
				return false;
			}

			#region Lauch Service Response Event
			// Everything is fine.
			if (ServiceResponse != null)
			{
				ServiceResponse(this, new ServiceResultEventArgs(lResult, Context.ExchangeInformation));
			}
			#endregion Lauch Service Response event
			
			#region Outbound Arguments. Before Conditional navigation question
			// Launch outbound arguments scenario only when there is no multiexecution
			if (lResult && lExchangeInfoConditionalNavigation != null && lExchangeInfoConditionalNavigation.ConditionalNavigationInfo.Count > 1 &&
				!lIsMultiExecution && OutboundArgumentsScenario != null && OutboundArgumentsScenario.Length > 0)
			{
				ExchangeInfo lGenericExchangeInfo = new ExchangeInfo(ExchangeType.Generic, Context.ClassName, mOutboundArgumentsScenario, Context);
				ScenarioManager.LaunchOutbountArgumentsScenario(lGenericExchangeInfo);
			}
			#endregion Outbound Arguments. Before Conditional navigation question

			#region Launch ConditionalNavigations
			try
			{
				if (lExchangeInfoConditionalNavigation != null)
				{
					// Clean Extra information in all the Oids in order to force new queries.
					foreach (DestinationInfo destinationInfo in lExchangeInfoConditionalNavigation.ConditionalNavigationInfo)
					{
						if (destinationInfo.ExchangeInfo.SelectedOids != null)
						{
							foreach (Oid lOid in destinationInfo.ExchangeInfo.SelectedOids)
							{
								lOid.ExtraInfo = null;
							}
						}
						if (destinationInfo.ExchangeInfo.CustomData != null)
						{
							foreach (object lValue in destinationInfo.ExchangeInfo.CustomData)
							{
								List<Oid> lOids = lValue as List<Oid>;
								if (lOids != null)
								{
									foreach (Oid lOid in lOids)
									{
										lOid.ExtraInfo = null;
									}
								}

							}
						}
					}

					// Show conditional navigation form, to allow the user to choose a response.
					IActionItemSuscriber actionItem = null;
					if (ServiceResponse != null)
					{
						actionItem = ServiceResponse.Target as IActionItemSuscriber;
					}
					ScenarioManager.LaunchConditionalNavigationScenario(lExchangeInfoConditionalNavigation, actionItem);
				}
			}
			catch (Exception logicException)
			{
				ScenarioManager.LaunchErrorScenario(logicException);
				return lResult;
			}
			#endregion Launch ConditionalNavigations

			#region Outbound Arguments
			// Launch outbound arguments scenario only when there is no multiexecution
			if (lResult && (lExchangeInfoConditionalNavigation == null || lExchangeInfoConditionalNavigation.ConditionalNavigationInfo.Count == 1) &&
				!lIsMultiExecution && OutboundArgumentsScenario != null && OutboundArgumentsScenario.Length > 0)
			{
				ExchangeInfo lGenericExchangeInfo = new ExchangeInfo(ExchangeType.Generic, Context.ClassName, mOutboundArgumentsScenario, Context);
				ScenarioManager.LaunchOutbountArgumentsScenario(lGenericExchangeInfo);
			}
			#endregion Outbound Arguments

			CloseScenario();

			return lResult;
		}
		/// <summary>
		///
		/// </summary>
		/// <returns></returns>
		public override bool Cancel()
		{
			bool lResult = true;

			CloseScenario();

			return lResult;
		}
		/// <summary>
		/// Checks the null argument values.
		/// </summary>
		/// <returns></returns>
		private bool CheckNullAndFormatArgumentValues()
		{
			bool lResult = true;
			object[] lArgs = new object[1];

            // First invalid inbound argument, it will get the focus
            ArgumentController lFirstInvalidArgument = null;

			// Control the null - not null allowed for all the inbound arguments
			foreach (ArgumentController lArgument in InputFields)
			{
				// Argument data-valued validation.
				ArgumentDVController lArgumentDV = lArgument as ArgumentDVController;
				if ((lArgumentDV != null) && (lArgumentDV.Editor != null))
				{
					lArgs[0] = lArgument.Alias;
					lResult = lResult & lArgumentDV.Editor.Validate(CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_NECESARY, LanguageConstantValues.L_VALIDATION_NECESARY, lArgs));
				}
				// Argument object-valued validation.
				else
				{
					ArgumentOVController lArgumentOV = lArgument as ArgumentOVController;
					if (lArgumentOV != null)
					{
						if (!((lArgumentOV.MultiSelectionAllowed) && ((lArgumentOV.Value as List<Oid>) != null) && ((lArgumentOV.Value as List<Oid>).Count > 1)))
						{
							List<Object> lEditorFields = new List<object>();
							foreach (IEditorPresentation lEditor in lArgumentOV.Editors)
							{
								if (lEditor != null)
								{
									// Shows the validation error only for the last editor field.
									lArgs[0] = lArgument.Alias;
									lResult = lResult & lEditor.Validate(CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_NECESARY, LanguageConstantValues.L_VALIDATION_NECESARY, lArgs));
									if (lEditor.Value != null)
									{
										// Fill the auxiliar list of values, in order to check if the Alternate Key is valid. 
										lEditorFields.Add(lEditor.Value);
									}
								}
							}

							// If the OV Argument has to work with an Alternate Key, check that the values specified
							// in the editors are valid (It exist an instance that mach with this Alternate Key).
							if (lArgumentOV.AlternateKeyName != string.Empty && lArgumentOV.Editors.Count == lEditorFields.Count)
							{
								Oid lOid = Oid.Create(lArgumentOV.Domain);
								AlternateKey lAlternateKey = (AlternateKey)lOid.GetAlternateKey(lArgumentOV.AlternateKeyName);
								lAlternateKey.SetValues(lEditorFields);

								// Check if the Alternate Key is a valid one.
								Oid lResultOid = Logic.GetOidFromAlternateKey(lAlternateKey, lArgumentOV.AlternateKeyName);
								// If the Oid is not found, it is because the Alternate Key is not a valid one.
								if (lResultOid == null)
								{
									ScenarioManager.LaunchErrorScenario(new Exception(CultureManager.TranslateString(LanguageConstantKeys.L_ERROR_NO_EXIST_INSTANCE, LanguageConstantValues.L_ERROR_NO_EXIST_INSTANCE)));
									return false;
								}
							}
						}
					}
					else
					{
						ArgumentOVPreloadController lArgumentOVPreload = lArgument as ArgumentOVPreloadController;
						if ((lArgumentOVPreload != null) && (lArgumentOVPreload.Editor != null))
						{
							lArgs[0] = lArgumentOVPreload.Alias;
							IEditorPresentation lEditorPresentation = lArgumentOVPreload.Editor.Viewer as IEditorPresentation;
							if (lEditorPresentation != null)
							{
								lResult = lResult & lEditorPresentation.Validate(CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_NECESARY, LanguageConstantValues.L_VALIDATION_NECESARY, lArgs));
							}
						}
					}
				}

                // This is the first invalid inbound argument
                if (!lResult && lFirstInvalidArgument == null)
                {
                    lFirstInvalidArgument = lArgument;
                }
			}

            // Set focus to the first invalid argument
            if (lFirstInvalidArgument != null)
            {
                lFirstInvalidArgument.Focused = true;
            }

			return lResult;
		}
		/// <summary>
		/// Executes the service, including multiexecution.
		/// </summary>
		/// <param name="isInboundArgumentsValidationRequired">Indicates when is required to validate the inbound arguments.</param>
		/// <returns>True if the service has been executed successfully or False in the other cases.</returns>
		private bool ExecuteService(bool isInboundArgumentsValidationRequired)
		{
			if (isInboundArgumentsValidationRequired)
			{
				// Not Null inbound argument checking.
				if (!CheckNullAndFormatArgumentValues())
				{
					return false;
				}
			}

			UpdateContext();

			// When it is an associated service, validate if there are pending changes in the
			// interaction unit which contains the service.
			CheckForPendingChangesEventArgs eventArg = new CheckForPendingChangesEventArgs();
			OnBeforeExecute(eventArg);
			if (eventArg.Cancel)
			{
				return false;
			}

			Context.ExecutedService = true;
			int lNumOidsTotal = 0;
			DataTable report = new DataTable();
			List<IUContextArgumentInfo> lMultiSelectionList = new List<IUContextArgumentInfo>();
			Dictionary<string, IUContextArgumentInfo> lNoMultiSelectionList = new Dictionary<string, IUContextArgumentInfo>(StringComparer.CurrentCultureIgnoreCase);
			Dictionary<string, KeyValuePair<string, string>> lInboundArgumentsAlias = new Dictionary<string, KeyValuePair<string, string>>();
			Dictionary<string, KeyValuePair<string, string>> lOutboundArgumentsAlias = new Dictionary<string, KeyValuePair<string, string>>();
			// Separate in two list the Multi Selection and non Multi Selection
			foreach (ArgumentController lArgument in InputFields)
			{
				ArgumentOVController lInboundArgumentOV = lArgument as ArgumentOVController;
				if (lInboundArgumentOV != null)
				{
					// For multiexecution, in order to store the Alias of the inbound arguments.
					lInboundArgumentsAlias.Add(lInboundArgumentOV.Name, new KeyValuePair<string, string>(lInboundArgumentOV.IdXML, lInboundArgumentOV.Alias));
				}
				
				List<Oid> listOidsArgumentValue = lArgument.Value as List<Oid>;
				if (listOidsArgumentValue != null && listOidsArgumentValue.Count > 1)
				{
					if (lNumOidsTotal == 0)
					{
						lNumOidsTotal = listOidsArgumentValue.Count;
					}
					else
					{
						lNumOidsTotal = lNumOidsTotal * listOidsArgumentValue.Count;
					}
					
					if (lInboundArgumentOV != null && lInboundArgumentOV.AlternateKeyName != string.Empty)
					{
						foreach (Oid lOid in listOidsArgumentValue)
						{
							// Indicate that each Oid of the list is going to use the Alternative Key of the inbound argument.
							lOid.AlternateKeyName = lInboundArgumentOV.AlternateKeyName;
						}
					}
					lMultiSelectionList.Add(new IUContextArgumentInfo(lArgument.Name, listOidsArgumentValue, false, null));
				}
				else
				{
					lNoMultiSelectionList[lArgument.Name] = new IUContextArgumentInfo(lArgument.Name, lArgument.Value, false, null);
				}
			}

			// No Multiexecution has to be processed. Execute the service only one time.
			if (lNumOidsTotal == 0)
			{

				Exception logicException = ExecuteServiceChekingSCDRetry();
				// Show error scenario, only if no State Change Detection error.
				if (logicException != null)
				{
					ResponseException lResponseException = logicException as ResponseException;
					if (lResponseException == null || !lResponseException.Number.Equals(48))
					{
						ScenarioManager.LaunchErrorScenario(logicException);
					}
					return false;
				}

				// Every time a service is executed successfully, reset Agent connected ExtraInfo.
				Logic.Agent.ExtraInfo = null;

				this.Context.ClearOVExtraInfo(); // Clear ExtraInfo of Oids selected previously.

				return true;
			}

			// Multiple instances selected.

			// Store the Alias and the IdXML of the outbound arguments for multilanguage feature.
			foreach (ArgumentController lOutboundArgument in OutputFields)
			{
				lOutboundArgumentsAlias.Add(lOutboundArgument.Name, new KeyValuePair<string, string>(lOutboundArgument.IdXML, lOutboundArgument.Alias));
			}

			// Show the Cancel Window
			ShowCancelWindow();

			// Number of times that the service has been executed.
			int lNumProcessed = 0;
			// Flags for the Cancel or Ignore Errors. Only for multiselection
			bool lCancel = false;
			bool lIgnoreError = false;
			bool someErrors = ExecuteServiceForMultiSelection(lMultiSelectionList, lNoMultiSelectionList,
				ref lNumOidsTotal, ref lNumProcessed, ref lCancel,
				ref lIgnoreError, ref report, lInboundArgumentsAlias);

			HideCancelWindow();

			if (someErrors)
			{
				// Show error message to the user
				ScenarioManager.LaunchMultiExecutionReportScenario(report, Alias, lInboundArgumentsAlias, lOutboundArgumentsAlias);
				Context.ExecutedService = false;
				return false;
			}

			// Check if the report must be shown to the final user
			if (OutputFields.Count > 0)
			{
				ScenarioManager.LaunchMultiExecutionReportScenario(report, Alias, lInboundArgumentsAlias, lOutboundArgumentsAlias);
			}

			// Every time a service is executed successfully, reset Agent connected ExtraInfo.
			Logic.Agent.ExtraInfo = null;

			// Clear ExtraInfo of Oids selected previously.
			this.Context.ClearOVExtraInfo();

			return true;
		}
		/// <summary>
		/// Auxiliar function for exectuting a service.
		/// </summary>
		/// <param name="multiSelectionList">List of multiselection.</param>
		/// <param name="noMultiSelectionList"></param>
		/// <param name="numOidsTotal">Total number of Oids.</param>
		/// <param name="numProcessed">Number of precess.</param>
		/// <param name="cancel">Cancel condition.</param>
		/// <param name="ignoreErrors">Ignore errors.</param>
		/// <param name="errorList">Error list.</param>
		private bool ExecuteServiceForMultiSelection(List<IUContextArgumentInfo> multiSelectionList, Dictionary<string, IUContextArgumentInfo> noMultiSelectionList, ref int numOidsTotal, ref int numProcessed, ref bool cancel, ref bool ignoreErrors, ref DataTable report, Dictionary<string, KeyValuePair<string, string>> argumentAlias)
		{
			// First process every element of the the multi selecction list.
			bool someError = false;
			if (multiSelectionList.Count > 0)
			{
				// Remove the first list in the multi seleccion list and process the Oids in that list
				IUContextArgumentInfo lMultiValueArgument = multiSelectionList[0];
				// Remove the first element in the global list
				multiSelectionList.RemoveAt(0);
				List<Oid> lOidList = lMultiValueArgument.Value as List<Oid>;

				foreach (Oid lOid in lOidList)
				{
					List<Oid> lLocalList = new List<Oid>(1);
					lLocalList.Add(lOid);
					noMultiSelectionList[lMultiValueArgument.Name] = new IUContextArgumentInfo(lMultiValueArgument.Name, lLocalList, false, null);
					if (ExecuteServiceForMultiSelection(multiSelectionList, noMultiSelectionList, ref numOidsTotal, ref numProcessed, ref cancel, ref ignoreErrors, ref report, argumentAlias))
					{
						someError = true;
					}

					if (cancel)
					{
						return true;
					}
				}
				// Add the list at the begining, in order to processed again for the
				// next item in the previous nested list.
				multiSelectionList.Insert(0, lMultiValueArgument);
			}
			else
			{
				// The OID string.
				Dictionary<string, string> lArgumentsOids = new Dictionary<string, string>();
				// For each execution of the service a new row is created.
				DataRow newReportRow = report.NewRow();
				// Execute the service with the argument values in the noMultiSelectionList list
				// Update the context with the values.
				foreach (IUContextArgumentInfo lArgument in noMultiSelectionList.Values)
				{
					Context.SetInputFieldValue(lArgument.Name, lArgument.Value);
				}

				//If it is multi execution, the inbound arguments and the execution column are made.
				PopulateReportInboundArgs(argumentAlias, ref report, ref newReportRow, numProcessed);

				// Execute service
				Exception logicException = ExecuteServiceChekingSCDRetry();
				if (logicException == null)
				{
					// Success service execution
					string lReportMessage = CultureManager.TranslateString(LanguageConstantKeys.L_MULTIEXE_EXECUTION, LanguageConstantValues.L_MULTIEXE_EXECUTION);
					newReportRow[lReportMessage] = CultureManager.TranslateString(LanguageConstantKeys.L_MULTIEXE_SUCESS, LanguageConstantValues.L_MULTIEXE_SUCESS);

					PopulateReportOutboundArgs(ref report, ref newReportRow, numProcessed);
					numProcessed++;
					ShowProgressValue(numProcessed, numOidsTotal, ref cancel);
					report.Rows.Add(newReportRow);
				}
				else
				{
					// If SCD error and no retry, cancel execution
					ResponseException lResponseException = logicException as ResponseException;
					if (lResponseException != null && lResponseException.Message.Equals("SCD"))
					{
						cancel=true;
						someError = true;
						return true;
					}

					// Inform to the user about the error
					if (!ignoreErrors)
					{
						InformUser(logicException, ref cancel, ref ignoreErrors);
					}
					// Add the error message to the error list
					string lReportMessage = CultureManager.TranslateString(LanguageConstantKeys.L_MULTIEXE_EXECUTION, LanguageConstantValues.L_MULTIEXE_EXECUTION);
					newReportRow[lReportMessage] = logicException.Message;

					report.Rows.Add(newReportRow);
					numProcessed++;
					if (cancel)
					{
						return true;
					}
					someError = true;
				}
			}
			return someError;
		}

		/// <summary>
		/// Execute the service and manages the SCD error.
		/// </summary>
		/// <returns>Returns the obtained error.</returns>
		private Exception ExecuteServiceChekingSCDRetry()
		{
			Dictionary<string, object> lNewValues = new Dictionary<string, object>();

			try
			{
				Logic.ExecuteService(Context);
				return null;
			}
			catch (Exception logicException)
			{
				// Manage the 'State Change Detection' exception.
				ResponseException lResponseException = logicException as ResponseException;
				if (lResponseException != null && lResponseException.Number.Equals(48))
				{
					Dictionary<string, KeyValuePair<Oid, DisplaySetInformation>> lSCDInfo = new Dictionary<string, KeyValuePair<Oid, DisplaySetInformation>>();
					foreach (ArgumentController lArg in InputFields)
					{
						ArgumentOVControllerAbstract lOVArg = lArg as ArgumentOVControllerAbstract;
						if (lOVArg != null)
						{
							List<Oid> lOidList = Context.GetInputFieldValue(lOVArg.Name) as List<Oid>;
							Oid lOid = (lOidList != null && lOidList.Count > 0) ? lOidList[0] : null;

							lSCDInfo.Add(lOVArg.Name, new KeyValuePair<Oid, DisplaySetInformation>(lOid, lOVArg.SCDAttributes));
						}
					}

					// Get the new values from the exception.
					foreach (ChangedItem lChangedItem in lResponseException.ChangedItems.Values)
					{
						lNewValues.Add(lChangedItem.Name, lChangedItem.NewValue);
					}

					// If 'State Change Detection' error and user selects no retry.
					if (!ScenarioManager.LaunchSCDErrorScenario(lSCDInfo, lNewValues, ShowValuesInSCDError, RetryInSCDError))
					{
						return logicException;
					}
				}
				else
				{
					// Other logic errors.
					Context.ExecutedService = false;
					return logicException;
				}
			}

			// 'State Change Detection' Retry has been selected. Update previous values using the current ones.
			foreach (string lKeyField in lNewValues.Keys)
			{
				// Get argument name from the key.
				string lOVArgName = lKeyField.Substring(0, lKeyField.IndexOf('.'));

				if (InputFields.Exist(lOVArgName))
				{
					// Get the argument in order to update its 'State Change Detection' values.
					ArgumentOVController lArgController = InputFields[lOVArgName] as ArgumentOVController;
					if (lArgController != null)
					{
						lArgController.UpdateSCDValueAttribute(lKeyField, lNewValues[lKeyField]);
					}
				}
			}

			// Retry service execution, after updating arguments values.
			return ExecuteServiceChekingSCDRetry();

		}

		/// <summary>
		/// Populates and creates the columns of the inbound arguments for the multiexecution result report.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <param name="inboundOVArguments">Object valuated arguments of the service.</param>
		/// <param name="report">Report.</param>
		/// <param name="row">New row of the report.</param>
		/// <param name="numProcessed">Number of arguments processed.</param>
		private void PopulateReportInboundArgs(Dictionary<string, KeyValuePair<string, string>> inboundOVArguments, ref DataTable report, ref DataRow row, int numProcessed)
		{
			if (numProcessed == 0)
			{
				// For the firt time, the inbound columns for the multiexecution report must to be create.
				foreach (string inboundNames in inboundOVArguments.Keys)
				{
					report.Columns.Add(inboundNames);
				}
				// The execution column.
				report.Columns.Add(CultureManager.TranslateString(LanguageConstantKeys.L_MULTIEXE_EXECUTION, LanguageConstantValues.L_MULTIEXE_EXECUTION));
			}
			foreach (string argumentName in inboundOVArguments.Keys)
			{
				IUContextArgumentInfo lArgument = Context.InputFields[argumentName];
				ArgumentOVController lArgumentOVController = (this.InputFields[lArgument.Name] as ArgumentOVController);
				List<Oid> lOidList = lArgument.Value as List<Oid>;
				// The Inbound argument column is populate with the OIDs of the inbound Object Valued argument.
				if (lOidList != null)
				{
					if (lOidList[0] != null &&
						(lArgumentOVController != null && 
							lArgumentOVController.AlternateKeyName != string.Empty))
					{
						// Obtain the alternate key from the Oid.
						AlternateKey alternateKey = Logic.GetAlternateKeyFromOid(lOidList[0], lOidList[0].AlternateKeyName);
						// Format the alternative IF and add it to the DataRow.
						row[lArgument.Name] = UtilFunctions.OidFieldsToString(alternateKey, ' ');
					}
					else
					{
						// Format the Oid and add it to the DataRow.
						row[lArgument.Name] = UtilFunctions.OidFieldsToString(lOidList[0], ' ');
					}
				}
			}
		}
		/// <summary>
		/// Populates and creates the columns of the outbound arguments for the multiexecution result report.
		/// </summary>
		/// <param name="context">Current context.</param>
		/// <param name="report">Report.</param>
		/// <param name="row">New row of the report.</param>
		/// <param name="numProcessed">Number of arguments processed.</param>
		private void PopulateReportOutboundArgs(ref DataTable report, ref DataRow row, int numProcessed)
		{

			// If no outbound arguments, do nothing
			if (OutputFields.Count == 0)
			{
				return;
			}

			// Gets the Outbound Arguments of the service to populate the report.
			Dictionary<string, Object> lOutboundArguments = Context.OutputFields;
			// The outbound columns for the multiexecution report must be created.
			foreach (ArgumentController argument in OutputFields)
			{
				// If the outbound column does not exist create it.
				if (!report.Columns.Contains(argument.Name))
				{
					report.Columns.Add(argument.Name);
				}
			}
			// The execution column is populate.
			{
				string lMultiExecutionMessage = CultureManager.TranslateString(LanguageConstantKeys.L_MULTIEXE_EXECUTION, LanguageConstantValues.L_MULTIEXE_EXECUTION);
				row[lMultiExecutionMessage] = CultureManager.TranslateString(LanguageConstantKeys.L_MULTIEXE_SUCESS, LanguageConstantValues.L_MULTIEXE_SUCESS);
			}
			// The Outbound Arguments columns are populate.
			foreach (ArgumentController argument in OutputFields)
			{
				List<Oid> outboundOVargumentValue = lOutboundArguments[argument.Name] as List<Oid>;
				if (outboundOVargumentValue != null) // Object-valued outboud argument.
				{
					ArgumentOVController outboundArgOV = (argument as ArgumentOVController);
					if (outboundArgOV != null && outboundArgOV.AlternateKeyName != string.Empty)
					{
						Oid lOid = (outboundOVargumentValue[0] as Oid);
						// Obtain the alternative Identification Function from the Oid.
						AlternateKey alternateKey = Logic.GetAlternateKeyFromOid(lOid, outboundArgOV.AlternateKeyName);
						// Format the alternative IF and add it to the DataRow.
						row[argument.Name] = UtilFunctions.OidFieldsToString(alternateKey, ' ');
					}
					else
					{
						// Format the Oid and add it to the DataRow.
						row[argument.Name] = UtilFunctions.OidFieldsToString(outboundOVargumentValue[0], ' ');
					}
				}
				else
				{
					if (lOutboundArguments[argument.Name] != null)
					{
						row[argument.Name] = lOutboundArguments[argument.Name].ToString();
					}
					else
					{
						row[argument.Name] = string.Empty;
					}
				}
			}
		}
		/// <summary>
		/// User information.
		/// </summary>
		/// <param name="e">Exception.</param>
		/// <param name="cancel">Cancelation.</param>
		/// <param name="ignoreErrors">Ignore errors.</param>
		private void InformUser(Exception e, ref bool cancel, ref bool ignoreErrors)
		{
			string lText = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR_OCCURS, LanguageConstantValues.L_ERROR_OCCURS);
			string lCaption = CultureManager.TranslateString(LanguageConstantKeys.L_WARNING, LanguageConstantValues.L_WARNING);
			DialogResult lAwnser = MessageBox.Show(lText, lCaption, MessageBoxButtons.AbortRetryIgnore);
			if (lAwnser.Equals(DialogResult.Abort))
			{
				cancel = true;
			}

			if (lAwnser.Equals(DialogResult.Ignore))
			{
				ignoreErrors = true;
			}
		}
		/// <summary>
		/// Hides the service cancel window.
		/// </summary>
		private void HideCancelWindow()
		{
			// Hides the cancel window
			try
			{
				mCancelWindow.Close();
				mCancelWindow = null;
			}
			catch
			{
			}
		}
		/// <summary>
		/// Show the service execution progress.
		/// </summary>
		/// <param name="numProcessed">Number process.</param>
		/// <param name="numOidsTotal">Total number of Oids.</param>
		/// <param name="cancel">Cancel condition.</param>
		private void ShowProgressValue(int numProcessed, int numOidsTotal, ref bool cancel)
		{
			// Sets the Value to the CancelWindow
			if (mCancelWindow == null)
			{
				return;
			}

			cancel = mCancelWindow.Cancel;
			if (cancel)
			{
				{
					string lText = CultureManager.TranslateString(LanguageConstantKeys.L_MULTIEXE_CANCEL_ACK, LanguageConstantValues.L_MULTIEXE_CANCEL_ACK);
					string lCaption = CultureManager.TranslateString(LanguageConstantKeys.L_WARNING, LanguageConstantValues.L_WARNING);
					if (MessageBox.Show(lText, lCaption, MessageBoxButtons.YesNo) == DialogResult.No)
					{
						cancel = false;
						mCancelWindow.Cancel = false;
					}
				}
			}

			try
			{
				decimal lValue = (numProcessed * 100 / numOidsTotal);
				mCancelWindow.Value = (int)lValue;
			}
			catch
			{
			}
		}
		/// <summary>
		/// Shows the cancel window of the service.
		/// </summary>
		private void ShowCancelWindow()
		{
			// Shows the cancel window
			try
			{
				mCancelWindow = new CancelWindow();
				mCancelWindow.MdiParent = base.Scenario.Form.MdiParent;
				mCancelWindow.Show();
			}
			catch
			{
				mCancelWindow = null;
			}
		}
		/// <summary>
		/// Exectue the Command Type.
		/// </summary>
		/// <param name="executeCommandType">Command Type.</param>
		/// <param name="args">Command Arguments</param>
		public virtual void ExecuteCommand(ExecuteCommandType executeCommandType, params object[] args)
		{
			ProcessExecuteCommand(this,new ExecuteCommandEventArgs(executeCommandType));
			//HandleScenarioExecuteCommand(null, new ExecuteCommandEventArgs(executeCommandType));
		}
		/// <summary>
		/// Apply multilanguage to the scenario.
		/// </summary>
		public override void ApplyMultilanguage()
		{
			base.ApplyMultilanguage();

			if (IsOutboundArgumentController)
			{
				// Ok button.
				if (this.OkTrigger != null)
				{
					this.OkTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_CLOSE, LanguageConstantValues.L_CLOSE, this.OkTrigger.Value.ToString());
				}

				// Cancel button.
				if (this.CancelTrigger != null)
				{
					this.CancelTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_CLOSE, LanguageConstantValues.L_CLOSE, this.CancelTrigger.Value.ToString());
				}
			}
			else
			{
				// Ok button.
				if (this.OkTrigger != null)
				{
					this.OkTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_OK, LanguageConstantValues.L_OK, this.OkTrigger.Value.ToString());
				}

				// Cancel button.
				if (this.CancelTrigger != null)
				{
					this.CancelTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_CANCEL, LanguageConstantValues.L_CANCEL, this.CancelTrigger.Value.ToString());
				}
			}
		}
		/// <summary>
		/// Clear values.
		/// </summary>
		public void ClearValues()
		{
			// Clear context values.
			Context.ClearInputFields();

			// Clear controller values.
			foreach (ArgumentController argument in InputFields)
			{
				argument.Enabled = true;
				argument.Value = null;
			}
		}
		#endregion Methods
        #region IActionItemEvents implementation
        /// <summary>
        /// Service response.
        /// </summary>
        public event EventHandler<ServiceResultEventArgs> ServiceResponse;
        /// <summary>
        /// Select next instance event.
        /// </summary>
        public event EventHandler<SelectNextPreviousInstanceEventArgs> SelectNextInstance;
        /// <summary>
        /// Select previous instance event.
        /// </summary>
        public event EventHandler<SelectNextPreviousInstanceEventArgs> SelectPreviousInstance;
        /// <summary>
        /// Refresh instance event.
        /// </summary>
        public event EventHandler<RefreshRequiredEventArgs> RefreshRequired;

        #endregion IActionItemEvents implementation
	}
}

