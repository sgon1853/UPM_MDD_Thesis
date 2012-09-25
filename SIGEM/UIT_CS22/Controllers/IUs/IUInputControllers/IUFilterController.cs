// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

using SIGEM.Client.Presentation;
using SIGEM.Client.Logics;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the IUFilterController.
	/// </summary>
	public class IUFilterController : IUInputFieldsController
	{
		#region Members
		/// <summary>
		/// Default order criteria.
		/// </summary>
		private string mDefaultOrderCriteria;
		/// <summary>
		/// Presentation Label member.
		/// </summary>
		private ILabelPresentation mLabel;
		#endregion Members

		#region Constructors
		/// <summary>
		/// nitializes a new instace of the 'IUFilterController' class.
		/// </summary>
		/// <param name="name">Interaction unit name.</param>
		/// <param name="alias">Alias of the filter.</param>
		/// <param name="idXml">IdXML of the filter.</param>
		/// <param name="defaultOrderCriteria">Default order criteria.</param>
		/// <param name="context">Context.</param>
		/// <param name="parent">Parent controller.</param>
		public IUFilterController(
			string name,
			string alias,
			string idXml,
			string defaultOrderCriteria,
			IUFilterContext context,
			IUController parent)
			: base()
		{
			Name = name;
			Alias = alias;
			IdXML = idXml;
			DefaultOrderCriteria = defaultOrderCriteria;
			Context = context;
			Parent = parent;

			InputFields = new ArgumentControllerList();
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets the class name.
		/// </summary>
		public override string ClassName
		{
			get
			{
				if ((Parent != null) & (Parent.Context != null))
				{
					// Return the parent class name (population IU).
					return ((IUPopulationContext)Parent.Context).ClassName;
				}
				return string.Empty;
			}
			protected set{/* not valuable */}
		} 
		/// <summary>
		/// Gets the default filter order criteria.
		/// </summary>
		public virtual string DefaultOrderCriteria
		{
			get
			{
				return mDefaultOrderCriteria;
			}
			protected set
			{
				mDefaultOrderCriteria = value;
			}
		} 
		/// <summary>
		/// Presentation Label.
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
			}
		} 
		/// <summary>
		/// Hide the Context Property in base class.
		/// </summary>
		public new IUFilterContext Context
		{
			get
			{
				return base.Context as IUFilterContext;
			}
			set
			{
				base.Context = value;
			}

		} 
		#endregion Properties

		#region Events
		/// <summary>
		/// Is raised when the execute metod is called.
		/// </summary>
		public event EventHandler<ExecuteFilterEventArgs> ExecuteFilter;
		#endregion Events

		#region Event Raisers
		/// <summary>
		/// Raise ExecuteFilter Event.
		/// </summary>
		/// <param name="executeFilterEventArgs">ExecuteFilterEventArgs.</param>
		protected virtual void OnExecuteFilter(ExecuteFilterEventArgs eventArgs)
		{
			EventHandler<ExecuteFilterEventArgs> handler = ExecuteFilter;
			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
		#endregion Event Raisers

		#region Methods
		/// <summary>
		/// Initializes the input fields properties.
		/// </summary>
		public override void Initialize()
		{

			#region Initialize Arguments Controller

			InternalInputFields.Initialize();

			#endregion Initialize Arguments Controller

			#region Initial Context Arguments Configuration
			// Initial Context Arguments Configuration
			ConfigureInitialContext();
			#endregion Initial Context Arguments Configuration

			try
			{
				#region Exectute Logic Default Values
				// Load default values.

				// Call Logic API ExecuteDefaultValues().
				ExecuteDefaultValues(Context);

				#endregion Exectute Logic Default Values

				#region Execue Load From Context
				// Load valued from context.
				ExecuteLoadFromContext(Context);
				#endregion xecue Load From Context
			}
			catch (Exception logicException)
			{
				ScenarioManager.LaunchErrorScenario(logicException);
			}

			base.Initialize();
		}
		/// <summary>
		/// Execute Filter Controller
		/// </summary>
		/// <returns></returns>
		public override bool Execute()
		{
			if (!CheckNullAndFormatFilterVariablesValues())
			{
				return false;
			}
			// Update Filter context from controller argumens
			UpdateContext();

			// Raise the Execute Filter Event.
			ExecuteFilterEventArgs lExecuteFilterEventArgs = new ExecuteFilterEventArgs(this);

			OnExecuteFilter(lExecuteFilterEventArgs);

			return lExecuteFilterEventArgs.Success;
		}
		/// <summary>
		/// Cancel - Close.
		/// </summary>
		/// <returns></returns>
		public override bool Cancel()
		{
			return true;
		} 
		/// <summary>
		/// Set argument Controller values from argument Context values.
		/// </summary>
		/// <param name="context"></param>
		public override void ConfigureByContext(IUContext context)
		{
			ConfigureInputFieldsByContext(Context);
			
			base.ConfigureByContext(context);
		}
		/// <summary>
		/// Apply Multi Language.
		/// </summary>
		public override void ApplyMultilanguage()
		{
			base.ApplyMultilanguage();
			
			// Translate the filter label.
            if (Label != null)
            {
			Label.Value = CultureManager.TranslateString(IdXML, Alias, Label.Value.ToString());
            }

			OkTrigger.Value = CultureManager.TranslateString(LanguageConstantKeys.L_SEARCH, LanguageConstantValues.L_SEARCH, OkTrigger.Value.ToString());
		}
		/// <summary>
		/// Actions related with the Execute command event from any filter variables 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void ProcessExecuteCommand(object sender, ExecuteCommandEventArgs e)
		{
			// Refresh means execute the filter again
			if (e.ExecuteCommandType == ExecuteCommandType.ExecuteRefresh)
			{
				Execute();
			}
		}

		/// <summary>
		/// Validates the format and null allowed of the filter variables
		/// </summary>
		/// <returns></returns>
		protected bool CheckNullAndFormatFilterVariablesValues()
		{
			bool lResult = true;
			object[] lArgs = new object[1];

			// Control the null - not null allowed for all the filter variables arguments.
			foreach (ArgumentController lFilterVariable in InputFields)
			{
				// Argument data-valued validation.
				ArgumentDVController lFilterVariableDV = lFilterVariable as ArgumentDVController;
				if ((lFilterVariableDV != null) && (lFilterVariableDV.Editor != null))
				{
					lArgs[0] = lFilterVariable.Alias;
					lResult = lResult & lFilterVariableDV.Editor.Validate(CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_NECESARY, LanguageConstantValues.L_VALIDATION_NECESARY, lArgs));
				}
				// Argument object-valued validation.
				else
				{
					ArgumentOVController lFilterVariableOV = lFilterVariable as ArgumentOVController;
					if (lFilterVariableOV != null)
					{
						List<Object> lEditorFields = new List<object>();
						foreach (IEditorPresentation lEditor in lFilterVariableOV.Editors)
						{
							if (lEditor != null)
							{
								lArgs[0] = lFilterVariable.Alias;
								// Shows the validation error only for the last editor field.
								lResult = lResult & lEditor.Validate(CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_VALIDATION_NECESARY, LanguageConstantValues.L_VALIDATION_NECESARY, lArgs));
								if (lEditor.Value != null)
								{
									// Fill the auxiliar list of values, in order to check if the Alternate Key is valid. 
									lEditorFields.Add(lEditor.Value);
								}
							}
						}

						// If the OV filter variable has to work with an Alternate Key, check that the values specified
						// in the editors are valid (It exist an instance that mach with this Alternate Key).
						if (lFilterVariableOV.AlternateKeyName != string.Empty && lFilterVariableOV.Editors.Count == lEditorFields.Count)
						{
							Oid lOid = Oid.Create(lFilterVariableOV.Domain);
							AlternateKey lAlternateKey = (AlternateKey)lOid.GetAlternateKey(lFilterVariableOV.AlternateKeyName);
							lAlternateKey.SetValues(lEditorFields);

							// Check if the Alternate Key is a valid one.
							Oid lResultOid = Logic.GetOidFromAlternateKey(lAlternateKey, lFilterVariableOV.AlternateKeyName);
							// If the Oid is not found, it is because the Alternate Key is not a valid one.
							if (lResultOid == null)
							{
								ScenarioManager.LaunchErrorScenario(new Exception(CultureManager.TranslateString(LanguageConstantKeys.L_ERROR_NO_EXIST_INSTANCE, LanguageConstantValues.L_ERROR_NO_EXIST_INSTANCE)));
								return false;
							}
						}
					}
				}
			}

			return lResult;
		}

		#endregion Methods
	}
}



