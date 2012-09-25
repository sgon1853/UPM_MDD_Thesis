// v3.8.4.5.b
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of the .NET Label control.
	/// </summary>
	public class LabelPasswordPresentation : LabelPresentation
	{
		#region Members
		/// <summary>
		/// Label control
		/// </summary>
		private Label mConfirmLabelIT = null;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'LabelPasswordPresentation'.
		/// </summary>
		/// <param name="label">Password label</param>
		/// <param name="confirmLabel">Confirm Label</param>
		public LabelPasswordPresentation(Label label, Label confirmLabel)
			:base(label)
		{
			mConfirmLabelIT  = confirmLabel;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or Sets the label texts.
		/// </summary>
		public override object Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;

				// Apply Multilanguage
				mConfirmLabelIT.Text = CultureManager.TranslateString(LanguageConstantKeys.L_CONFIRM, LanguageConstantValues.L_CONFIRM);
			}
		}

		/// <summary>
		/// Gets or Sets the Visible of password label.
		/// </summary>
		public override bool Visible
		{
			get
			{
				return (base.Visible & mConfirmLabelIT.Visible);
			}
			set
			{
				base.Visible = value;
				mConfirmLabelIT.Visible = value;
			}
		}
		#endregion Properties
	}
}
