// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Data;
using SIGEM.Client.Presentation;
using SIGEM.Client.Logics;
using SIGEM.Client.Oids;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Class that manages the object-valued Argument controller with preload.
	/// </summary>
	public class ArgumentOVPreloadController : ArgumentOVControllerAbstract
	{
		#region Members
		/// <summary>
		/// Editor presentation.
		/// </summary>
		private DisplaySetController mEditor;
		/// <summary>
		/// Order criteria defined for the argument preload.
		/// </summary>
		private OrderCriteriaController mOrderCriteria = null;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ArgumentOVPreloadController' class.
		/// </summary>
		/// <param name="name">Name of the object-valued Argument.</param>
		/// <param name="alias">Alias of the object-valued Argument.</param>
		/// <param name="idxml">IdXML of the object-valued Argument.</param>
		/// <param name="domain">Domain or class name of the object-valued Argument.</param>
		/// <param name="nullAllowed">Indicates whether the object-valued Argument allows null values.</param>
		/// <param name="multipleSelectionAllowed">Indicates whether the object-valued Argument allows multiple values.</param>
		/// <param name="editor">DisplaySet.</param>
		/// <param name="orderCriteria">Order criteria.</param>
		/// <param name="parent">Parent controller.</param>
		public ArgumentOVPreloadController(string name, string alias, string idxml, string domain, bool nullAllowed, bool multipleSelectionAllowed, DisplaySetController editor, OrderCriteriaController orderCriteria, IUController parent)
			: base(name, alias, idxml, domain, nullAllowed, multipleSelectionAllowed, parent)
		{
			Editor = editor;
			mOrderCriteria = orderCriteria;
		}
		/// <summary>
		/// Initializes a new instance of the 'ArgumentOVPreloadController' class.
		/// </summary>
		/// <param name="name">Name of the object-valued Argument.</param>
		/// <param name="alias">Alias of the object-valued Argument.</param>
		/// <param name="idxml">IdXML of the object-valued Argument.</param>
		/// <param name="domain">Domain or class name of the object-valued Argument.</param>
		/// <param name="nullAllowed">Indicates whether the object-valued Argument allows null values.</param>
		/// <param name="multipleSelectionAllowed">Indicates whether the object-valued Argument allows multiple values.</param>
		/// <param name="parent">Parent controller.</param>
		public ArgumentOVPreloadController(string name, string alias, string idxml, string domain, bool nullAllowed, bool multipleSelectionAllowed, IUController parent)
			: base(name, alias, idxml, domain, nullAllowed, multipleSelectionAllowed, parent)
		{
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets the editor presentation of the object-valued Argument.
		/// </summary>
		public DisplaySetController Editor
		{
			get
			{
				return mEditor;
			}
			set
			{
				if (mEditor != null)
				{
					mEditor.SelectedInstanceChanged -= new EventHandler<SelectedInstanceChangedEventArgs>(HandleEditorsSelectedInstanceChanged);
					mEditor.ExecuteCommand -= new EventHandler<ExecuteCommandEventArgs>(HandleEditorExecuteCommand);
				}
				mEditor = value;
				if (mEditor != null)
				{
					mEditor.SelectedInstanceChanged += new EventHandler<SelectedInstanceChangedEventArgs>(HandleEditorsSelectedInstanceChanged);
					mEditor.ExecuteCommand += new EventHandler<ExecuteCommandEventArgs>(HandleEditorExecuteCommand);
					mEditor.Parent = this;
				}
			}
		}
		/// <summary>
		/// Gets or sets the value of the object-valued Argument.
		/// </summary>
		public override object Value
		{
			get
			{
				List<Oid> lValue = mEditor.Values;
				if (UtilFunctions.OidListEquals(LastValueListOids, lValue))
				{
					return LastValueListOids;
				}

				// Get SCD attribute values
				GetValuesForSCD(lValue);
				return lValue;
			}
			set
			{
				// Set flag.
				IgnoreEditorsValueChangeEvent = true;
				// Set value.
				SetValue(value);
				// Remove flag.
				IgnoreEditorsValueChangeEvent = false;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the object-valued Argument is enabled or not.
		/// </summary>
		public override bool Enabled
		{
			get
			{
				return mEditor.Enabled;
			}
			set
			{
				// Set flag.
				IgnoreEditorsEnabledChangeEvent = true;
				// Set value.
				mEditor.Enabled = value;
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
				if ((Editor != null) && (Editor.Viewer != null))
				{
					return !(Editor.Viewer as IEditorPresentation).NullAllowed;
				}
				return false;
			}
			set
			{
				if ((Editor != null) && (Editor.Viewer != null))
				{
					(Editor.Viewer as IEditorPresentation).NullAllowed = !value;
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
				if ((Editor != null) && (Editor.Viewer != null))
				{
					// It is only visible if the Editor and the Label are both visibles.
					return Editor.Viewer.Visible && base.Visible;
				}
				return false;
			}
			set
			{
				if ((Editor != null) && (Editor.Viewer != null))
				{
					Editor.Viewer.Visible = value;
				}
				base.Visible = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the object-valued Argument is focused or not.
		/// </summary>
		public override bool Focused
		{
			get
			{
				if ((Editor != null) && (Editor.Viewer != null))
				{
					return (Editor.Viewer as IEditorPresentation).Focused;
				}
				return false;
			}
			set
			{
				if ((Editor != null) && (Editor.Viewer != null))
				{
					(Editor.Viewer as IEditorPresentation).Focused = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the object-valued Argument is selected or not.
		/// </summary>
		public override bool IsSelected
		{
			get
			{
				return false;
			}
			set
			{
			}
		}
		/// <summary>
		/// Gets or sets the order criteria associated to the argument.
		/// </summary>
		public OrderCriteriaController OrderCriteria
		{
			get
			{
				return mOrderCriteria;
			}
			set
			{
				mOrderCriteria = value;
			}
		}
		#endregion Properties

		#region Event Handlers
		/// <summary>
		/// Handles the Selected Instance Changed event from Editors
		/// </summary>
		/// <param name="sender">Sender object.</param>
		/// <param name="e">SelectedInstanceChangedEventArgs.</param>
		private void HandleEditorsSelectedInstanceChanged(object sender, SelectedInstanceChangedEventArgs e)
		{
			// If flag is set, do nothing.
			if (IgnoreEditorsValueChangeEvent)
			{
				return;
			}

			// Check if the value has changed.
			List<Oid> lLastValue = LastValueListOids;
			List<Oid> lValue = mEditor.Values;
			bool lEquals = UtilFunctions.OidListEquals(lLastValue, lValue);

			// If there is no change, do nothing.
			if (lEquals)
			{
				return;
			}

			// Assign the current value as last value
			LastValueListOids = lValue;

			// If the values are the different, raise the change event.
			OnValueChanged(new ValueChangedEventArgs(this, lLastValue, lValue, DependencyRulesAgentLogic.User));
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
		#endregion Event Handlers

		#region Methods
		public override void Initialize()
		{
			// Initialize the mandatory property for the Editor.
			Mandatory = !NullAllowed;

			if (Editor != null)
			{
				Editor.Initialize();
			}
			base.Initialize();
		}
		/// <summary>
		/// Sets the value of the object-valued Argument.
		/// </summary>
		/// <param name="value">Value of the object-valued Argument.</param>
		private void SetValue(object value)
		{
			List<Oid> lOids = value as List<Oid>;

			// Use only the first one if there are any.
			if (lOids != null && lOids.Count > 1)
			{
				lOids.RemoveRange(1, lOids.Count - 1);
			}
			// Store the values.
			mEditor.Values = lOids;
			LastValueListOids = lOids;
		}
		/// <summary>
		/// Sets population with the data.
		/// </summary>
		/// <param name="data">Data to populate.</param>
		public void SetPopulation(DataTable data)
		{
			if (data != null)
			{
			// If there is no editor presentation, do nothing.
			if (mEditor == null)
			{
				return;
			}
			if (data.Rows.Count > 0)
			{
				// Add a row with null value if the Argument allows null values.
				if (NullAllowed)
				{
					foreach (DataColumn column in data.Columns)
					{
						column.AllowDBNull = true;
					}
					data.Rows.InsertAt(data.NewRow(), 0);
				}
			}
			else
			{
				data = null;
			}
		}

			// Set flag.
			IgnoreEditorsValueChangeEvent = true;
			// Populates the data.
			mEditor.SetPopulation(data, true, null);
			// Remove flag.
			IgnoreEditorsValueChangeEvent = false;
		}

		/// <summary>
		/// Gets or sets the supplementary information text showed for the preload object-valued Argument.
		/// </summary>
		public override string GetSupplementaryInfoText()
		{
			Presentation.Forms.LabelDisplaySetPresentation lSupInfoLabelPresentation = (mEditor.Viewer as Presentation.Forms.LabelDisplaySetPresentation);
			if (lSupInfoLabelPresentation != null)
			{
				return lSupInfoLabelPresentation.DisplayedText;
			}
			else
			{
				return string.Empty;
			}
		}
		#endregion Methods
	}
}
