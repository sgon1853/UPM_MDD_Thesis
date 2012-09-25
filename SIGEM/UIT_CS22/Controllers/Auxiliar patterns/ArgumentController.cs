// v3.8.4.5.b
using System;
using SIGEM.Client.Presentation;
using SIGEM.Client.Logics;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the Argument controller.
	/// </summary>
	public abstract class ArgumentController : Controller
	{
		#region Members
		/// <summary>
		/// Label presentation.
		/// </summary>
		private ILabelPresentation mLabel;
		/// <summary>
		/// Argument name.
		/// </summary>
		private string mName;
		/// <summary>
		/// Argument alias.
		/// </summary>
		private string mAlias;
		/// <summary>
		/// XML argument identifier.
		/// </summary>
		private string mIdXML;
		/// <summary>
		/// Indicates whether the Argument allows null values.
		/// </summary>
		private bool mNullAllowed = false;
		/// <summary>
		/// Last argument value.
		/// </summary>
		private object mLastValue = null;
		/// <summary>
		/// Indicates whether the Argument must ignore change events or not.
		/// </summary>
		private bool mIgnoreEditorsValueChangeEvent = false;
		/// <summary>
		/// Indicates whether the Argument must ignore enable events or not.
		/// </summary>
		private bool mIgnoreEditorsEnabledChangeEvent = false;
		/// <summary>
		/// Indicates whether the Argument allows multiple values.
		/// </summary>
		private bool mMultiSelectionAllowed = false;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ArgumentController' class.
		/// </summary>
		/// <param name="name">Name of the Argument.</param>
		/// <param name="parent">Parent controller.</param>
		/// <param name="alias">Alias of the Argument.</param>
		/// <param name="idXML">IdXML ofthe Argument.</param>
		/// <param name="nullAllowed">Indicates whether the Argument allows null values.</param>
		/// <param name="multiSelectionAllowed">Indicates whether the Argument allows multiple values.</param>
		public ArgumentController(string name, IUController parent, string alias, string idXML, bool nullAllowed, bool multiSelectionAllowed)
			: base(parent)
		{
			mName = name;
			mAlias = alias;
			mIdXML = idXML;
			mNullAllowed = nullAllowed;
			mMultiSelectionAllowed = multiSelectionAllowed;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets label presentation of the Argument.
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
		/// Gets the argument name.
		/// </summary>
		public string Name
		{
			get
			{
				return mName;
			}
		}
		/// <summary>
		/// Gets the argument alias.
		/// </summary>
		public string Alias
		{
			get
			{
				return mAlias;
			}
		}
		/// <summary>
		/// Gets the XML argument identifier.
		/// </summary>
		public string IdXML
		{
			get
			{
				return mIdXML;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the Argument is visible or not.
		/// </summary>
		public virtual bool Visible
		{
			get
			{
				if (Label != null)
				{
					return Label.Visible;
				}
				return false;
			}
			set
			{
				if (Label != null)
				{
					Label.Visible = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the Argument allows null values.
		/// </summary>
		public bool NullAllowed
		{
			get
			{
				return mNullAllowed;
			}
			set
			{
				mNullAllowed = value;
			}
		}
		/// <summary>
		/// Gets or sets the last argument value.
		/// </summary>
		protected object LastValue
		{
			get
			{
				return mLastValue;
			}
			set
			{
				mLastValue = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the Argument must ignore change events.
		/// </summary>
		protected bool IgnoreEditorsValueChangeEvent
		{
			get
			{
				return mIgnoreEditorsValueChangeEvent;
			}
			set
			{
				mIgnoreEditorsValueChangeEvent = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the Argument must ignore enable events.
		/// </summary>
		protected bool IgnoreEditorsEnabledChangeEvent
		{
			get
			{
				return mIgnoreEditorsEnabledChangeEvent;
			}
			set
			{
				mIgnoreEditorsEnabledChangeEvent = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the Argument allows multiple values.
		/// </summary>
		public bool MultiSelectionAllowed
		{
			get
			{
				return mMultiSelectionAllowed;
			}
			set
			{
				mMultiSelectionAllowed = value;
			}
		}
		/// <summary>
		/// Gets the parent controller reference.
		/// </summary>
		public new IUController Parent
		{
			get
			{
				return base.Parent as IUController;
			}
		}
		/// <summary>
		/// Gets or sets the argument value.
		/// </summary>
		public abstract object Value { get; set; }
		/// <summary>
		/// Gets or sets a boolean value indicating whether the Argument is enabled or not.
		/// </summary>
		public abstract bool Enabled { get; set; }
		/// <summary>
		/// Gets or sets a boolean value indicating whether the Argument is mandatory or not.
		/// </summary>
		public abstract bool Mandatory { get; set; }
		/// <summary>
		/// Gets or sets a boolean value indicating whether the Argument is focused or not.
		/// </summary>
		public abstract bool Focused { get; set; }
		/// <summary>
		/// Gets or sets a boolean value indicating whether the Argumet is selected or not.
		/// </summary>
		public abstract bool IsSelected { get; set; }

		#endregion Properties

		#region Events
		/// <summary>
		/// Occurs when argument value is changed.
		/// </summary>
		public event EventHandler<ValueChangedEventArgs> ValueChanged;
		/// <summary>
		/// Occurs when the argument enable property is changed.
		/// </summary>
		public event EventHandler<EnabledChangedEventArgs> EnableChanged;
		/// <summary>
		/// Occurs when from presentation execute commands.
		/// </summary>
		public event EventHandler<ExecuteCommandEventArgs> ExecuteCommand;
		#endregion Events

		#region Event Raisers
		/// <summary>
		/// Raises the Value changed event
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void OnValueChanged(ValueChangedEventArgs eventArgs)
		{
			EventHandler<ValueChangedEventArgs> handler = ValueChanged;

			if (handler!= null)
			{
				handler(this,eventArgs);
			}
		}
		protected void OnEnableChanged(EnabledChangedEventArgs eventArgs)
		{
			EventHandler<EnabledChangedEventArgs> handler = EnableChanged;

			if (handler != null)
			{
				handler(this, eventArgs);
			}
	   }
		protected void OnExecuteCommand(ExecuteCommandEventArgs eventArgs)
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
		/// Initializes the Argument.
		/// </summary>
		public virtual void Initialize()
		{
			if(this.Label != null)
			{
				// Apply multilanguage to the argument label.
				this.Label.Value = CultureManager.TranslateString(this.IdXML, this.Alias, this.Label.Value.ToString());
			}
		}
		#endregion Methods
	}
}
