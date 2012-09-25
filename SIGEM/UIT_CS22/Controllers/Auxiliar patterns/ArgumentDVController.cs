// v3.8.4.5.b
using System;
using System.Collections.Generic;
using SIGEM.Client.Presentation;
using SIGEM.Client.Logics;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the data-valued Argument controller.
	/// </summary>
	public class ArgumentDVController : ArgumentController
	{
		#region Members
		/// <summary>
		/// ModelType of the data-valued Argument.
		/// </summary>
		private ModelType mModelType;
		/// <summary>
		/// Editor presentation.
		/// </summary>
		private IEditorPresentation mEditor;
		/// <summary>
		/// List of values of the defined selection.
		/// </summary>
		private List<KeyValuePair<object, string>> mOptions;
		private object mOptionsDefaultValue = null;
		/// <summary>
		/// The max length of the control values.
		/// </summary>
		private int mMaxLength = 0;
		/// <summary>
		/// Introduction pattern associated to the data-valued argument.
		/// </summary>
		private IntroductionPattern mIntroductionPattern = null;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ArgumentDVController' class.
		/// </summary>
		/// <param name="name">Name of the data-valued Argument.</param>
		/// <param name="alias">Alias of the data-valued Argument.</param>
		/// <param name="idXML">IdXML of the data-valued Argument.</param>
		/// <param name="modelType">ModelType of the data-valued Argument.</param>
		/// <param name="nullAllowed">Indicates whether the data-valued Argument allows null values.</param>
		/// <param name="options">List of options of the defined selection.</param>
		/// <param name="optionsDefaultValue">Default selected option for the defined seelction.</param>
		/// <param name="parent">Parent controller.</param>
		public ArgumentDVController(
			string name,
			string alias,
			string idXML,
			ModelType modelType,
			bool nullAllowed,
			List<KeyValuePair<object, string>> options,
			object optionsDefaultValue,
			IUController parent)
			: base(name, parent, alias, idXML, nullAllowed, false )
		{
			mModelType = modelType;
			mOptions = options;
			// Add the 'null' value to the options list.
			if (nullAllowed)
			{
				mOptions.Insert(0, new KeyValuePair<object, string>(null, string.Empty));
			}
			mOptionsDefaultValue = optionsDefaultValue;
		}
		/// <summary>
		/// Initializes a new instance of the 'ArgumentDVController' class.
		/// </summary>
		/// <param name="name">Name of the data-valued Argument.</param>
		/// <param name="alias">Alias of the data-valued Argument.</param>
		/// <param name="idxml">IdXML of the data-valued Argument.</param>
		/// <param name="modelType">ModelType of the data-valued Argument.</param>
		/// <param name="nullAllowed">Indicates whether the data-valued Argument allows null values.</param>
		/// <param name="parent">Parent controller.</param>
		[Obsolete("Since version 3.1.3.9.")]
		public ArgumentDVController(
			string name,
			string alias,
			string idxml,
			ModelType modelType,
			int maxLength,
			bool nullAllowed,
			IUController parent)
			: base(name, parent, alias, idxml, nullAllowed, false)
		{
			mModelType = modelType;
			mMaxLength = maxLength;
			mOptions = null;
		}
		/// <summary>
		/// Initializes a new instance of the 'ArgumentDVController' class.
		/// </summary>
		/// <param name="name">Name of the data-valued argument.</param>
		/// <param name="alias">Alias of the data-valued argument.</param>
		/// <param name="idxml">IdXML of the data-valued argument.</param>
		/// <param name="modelType">ModelType of the data-valued argument.</param>
		/// <param name="maxLength">Maximum length of the data-valued argument.</param>
		/// <param name="nullAllowed">Indicates whether the data-valued argument allows null values.</param>
		/// <param name="introductionPattern">Introduction pattern associated to the data-valued argument.</param>
		/// <param name="parent">Parent controller.</param>
		public ArgumentDVController(
			string name,
			string alias,
			string idxml,
			ModelType modelType,
			int maxLength,
			bool nullAllowed,
			IntroductionPattern introductionPattern,
			IUController parent)
			: base(name, parent, alias, idxml, nullAllowed, false)
		{
			mModelType = modelType;
			mMaxLength = maxLength;
			mOptions = null;
			IntroductionPattern = introductionPattern;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets the ModelType of the data-valued Argument.
		/// </summary>
		public ModelType ModelType
		{
			get
			{
				return mModelType;
			}
		}
		/// <summary>
		/// Gets a value indicating the max legth of the control value.
		/// </summary>
		public int MaxLength
		{
			get
			{
				return mMaxLength;
			}
		}
		/// <summary>
		/// Gets or sets the editor presentation of the data-valued Argument.
		/// </summary>
		public IEditorPresentation Editor
		{
			get
			{
				return mEditor;
			}
			set
			{
				if (mEditor != null)
				{
					mEditor.ValueChanged -= new EventHandler<ValueChangedEventArgs>(HandleEditorValueChanged);
					mEditor.EnableChanged -= new EventHandler<EnabledChangedEventArgs>(HandleEditorEnabledChanged);
					mEditor.ExecuteCommand -= new EventHandler<ExecuteCommandEventArgs>(HandleEditorExecuteCommand);
				}

				mEditor = value;

				// If not null, suscribe to the events and assign properties
				if (mEditor != null)
				{
					mEditor.DataType = mModelType;
					ISelectorPresentation selector = mEditor as ISelectorPresentation;
					if (selector != null && mOptions != null)
					{
						// Set flags.
						IgnoreEditorsValueChangeEvent = true;
						IgnoreEditorsEnabledChangeEvent = true;

						// Fill the presentation with the list of options.
						selector.Items = mOptions;
						// Set the default selected value for the options.
						if (mOptionsDefaultValue != null)
						{
							selector.Value = mOptionsDefaultValue;
						}
						LastValue = Value;

						// Remove flags.
						IgnoreEditorsValueChangeEvent = false;
						IgnoreEditorsEnabledChangeEvent = false;
					}

					// Assign numeric presentation properties
					INumericPresentation numericPresentation = mEditor as INumericPresentation;
					if (numericPresentation != null && IntroductionPattern != null)
					{
						numericPresentation.MaxIntegerDigits = IntroductionPattern.MaxIntegerDigits;
						numericPresentation.MaxDecimalDigits = IntroductionPattern.MaxDecimalDigits;
						numericPresentation.MinDecimalDigits = IntroductionPattern.MinDecimalDigits;
						numericPresentation.MinValue = IntroductionPattern.MinValue;
						numericPresentation.MaxValue = IntroductionPattern.MaxValue;
						numericPresentation.IPValidationMessage = IntroductionPattern.IPValidationMessage;
					}

					// Assign string, dates, time and datetime presentation properties
					IMaskPresentation maskPresentation = mEditor as IMaskPresentation;
					if (maskPresentation != null && IntroductionPattern != null)
					{
						maskPresentation.Mask = IntroductionPattern.Mask;
						maskPresentation.IPValidationMessage =  IntroductionPattern.IPValidationMessage;
					}

					// Editor events
					mEditor.ValueChanged += new EventHandler<ValueChangedEventArgs>(HandleEditorValueChanged);
					mEditor.EnableChanged += new EventHandler<EnabledChangedEventArgs>(HandleEditorEnabledChanged);
					mEditor.ExecuteCommand += new EventHandler<ExecuteCommandEventArgs>(HandleEditorExecuteCommand);
					mEditor.MaxLength = this.MaxLength;
				}
			}
		}
		/// <summary>
		/// Gets or sets the value of the data-valued Argument.
		/// </summary>
		public override object Value
		{
			get
			{
				if (mEditor != null)
				{
					return mEditor.Value;
				}
				return null;
			}
			set
			{
				IgnoreEditorsValueChangeEvent = true;
				mEditor.Value = value;
				LastValue = value;
				IgnoreEditorsValueChangeEvent = false;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the data-valued Argument is enabled or not.
		/// </summary>
		public override bool Enabled
		{
			get
			{
				if (mEditor != null)
				{
					return mEditor.Enabled;
				}
				return false;
			}
			set
			{
				// Set flag.
				IgnoreEditorsEnabledChangeEvent = true;
				bool lChange = (mEditor.Enabled != value);
				mEditor.Enabled = value;
				// Raise the event if the value changed.
				if (lChange)
				{
					OnEnableChanged(new EnabledChangedEventArgs(this, DependencyRulesAgentLogic.Internal, !value));
				}
				// Remove flag.
				IgnoreEditorsEnabledChangeEvent = false;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the data-valued Argument is mandatory or not.
		/// </summary>
		public override bool Mandatory
		{
			get
			{
				if (Editor != null)
				{
					return !Editor.NullAllowed;
				}
				return false;
			}
			set
			{
				if (Editor != null)
				{
					Editor.NullAllowed = !value;
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the data-valued Argument is visible or not.
		/// </summary>
		public override bool Visible
		{
			get
			{
				if (Editor != null)
				{
					// It is only visible if the Editor and the Label are both visibles.
					return Editor.Visible && base.Visible;
				}
				return base.Visible;
			}
			set
			{
				if (Editor != null)
				{
					Editor.Visible = value;
				}
				base.Visible = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the data-valued Argument is focused or not.
		/// </summary>
		public override bool Focused
		{
			get
			{
				if (Editor != null)
				{
					return Editor.Focused;
				}
				return false;
			}
			set
			{
				if (Editor != null)
				{
					Editor.Focused = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the data-valued Argument is selected or not.
		/// </summary>
		public override bool IsSelected
		{
			get
			{
				if (mEditor != null)
				{
					return mEditor.Focused;
				}
				return false;
			}
			set
			{
				mEditor.Focused = true;
			}
		}
		/// <summary>
		/// Gets the introduction pattern associated to the data-valued argument.
		/// </summary>
		public IntroductionPattern IntroductionPattern
		{
			get
			{
				return mIntroductionPattern;
			}
			protected set
			{
				mIntroductionPattern = value;
			}
		}
		#endregion Properties

		#region Events Handlers
		/// <summary>
		/// Occurs when the value of the data-valued Argument changes.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">ValueChangedEventArgs</param>
		private void HandleEditorValueChanged(object sender, ValueChangedEventArgs e)
		{
			// Do nothing if the flag is set.
			if (IgnoreEditorsValueChangeEvent)
			{
				return;
			}
			object lValue = Value;
			object lLastValue = LastValue;
			// Raise the value changed event.
			if (!UtilFunctions.ObjectsEquals(lLastValue, lValue))
			{
				OnValueChanged(new ValueChangedEventArgs(this, lLastValue, lValue, DependencyRulesAgentLogic.User));
			}
		}
		/// <summary>
		/// Occurs when the Enable property of the data-valued Argument changes.
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">EnableChangedEventArgs.</param>
		private void HandleEditorEnabledChanged(object sender, EnabledChangedEventArgs e)
		{
			// Do nothing if the flag is set.
			if (IgnoreEditorsEnabledChangeEvent)
			{
				return;
			}
			// Raise the enable changed event.
			OnEnableChanged(new EnabledChangedEventArgs(this, DependencyRulesAgentLogic.Internal, !Enabled));
		}
		/// <summary>
		/// Handles the Editor Execute command event. Just propagate it.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleEditorExecuteCommand(object sender, ExecuteCommandEventArgs e)
		{
			OnExecuteCommand(e);
		}
		#endregion Events Handlers

		#region Methods
		/// <summary>
		/// Initializes the Argument.
		/// </summary>
		public override void Initialize()
		{
			IUServiceController lServiceController = Parent as IUServiceController;

			// It is an Outbound Argument.
			if ((lServiceController != null) && (lServiceController.IsOutboundArgumentController))
			{
			}
			else
			{
				Mandatory = !NullAllowed;
			}

			base.Initialize();
		}
		#endregion Methods
	}
}
