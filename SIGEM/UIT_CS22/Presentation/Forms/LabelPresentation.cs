// v3.8.4.5.b
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstraction of the .NET Label control.
	/// </summary>
	public class LabelPresentation : ILabelPresentation
	{
		#region Members
		/// <summary>
		/// Label control
		/// </summary>
		private Label mLabelIT;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'LabelPresentation'.
		/// </summary>
		/// <param name="label">Label instance.</param>
		public LabelPresentation(Label label)
		{
			mLabelIT = label;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets label text.
		/// </summary>
		public virtual object Value
		{
			get
			{
				return mLabelIT.Text;
			}
			set
			{
				mLabelIT.Text = value.ToString();
			}
		}
		/// <summary>
		/// Gets or sets Visible property.
		/// </summary>
		public virtual bool Visible
		{
			get
			{
				return mLabelIT.Visible;
			}
			set
			{
				mLabelIT.Visible = value;
			}
		}
		#endregion Properties
	}
}
