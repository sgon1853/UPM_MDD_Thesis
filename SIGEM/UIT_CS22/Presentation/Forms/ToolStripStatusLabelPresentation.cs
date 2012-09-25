// v3.8.4.5.b
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIGEM.Client.Presentation.Forms

{
	/// <summary>
	/// Presentation abstraction of the .NET Label control.
	/// </summary>
	class ToolStripStatusLabelPresentation : ILabelPresentation
	{
		#region Members
		/// <summary>
		/// Tool strip status control
		/// </summary>
		protected ToolStripStatusLabel mToolStripStatusLabelIT;
		#endregion Members

		#region Constructors

		/// <summary>
		/// Initializes a new instance of 'ToolStripStatusLabel'
		/// </summary>
		/// <param name="toolStripStatusLabelIT">ToolStripStatusLabel</param>
		public ToolStripStatusLabelPresentation(ToolStripStatusLabel toolStripStatusLabelIT)
		{
			mToolStripStatusLabelIT = toolStripStatusLabelIT;
		}
		/// <summary>
		/// Initializes a new instance of 'ToolStripStatusLabel'
		/// </summary>
		/// <param name="toolStripStatusLabelIT">ToolStripStatusLabel</param>
		/// <param name="toolTip">ToolTipText</param>
		public ToolStripStatusLabelPresentation(ToolStripStatusLabel toolStripStatusLabelIT, string toolTip)
		{
			mToolStripStatusLabelIT = toolStripStatusLabelIT;
			mToolStripStatusLabelIT.ToolTipText = toolTip;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets label text.
		/// </summary>
		public object Value
		{
			get
			{
				return mToolStripStatusLabelIT.Text;
			}
			set
			{
				mToolStripStatusLabelIT.Text = value.ToString();
			}
		}
		/// <summary>
		/// Gets or sets Visible property.
		/// </summary>
		public bool Visible
		{
			get
			{
				return mToolStripStatusLabelIT.Visible;
			}
			set
			{
				mToolStripStatusLabelIT.Visible = value;
			}
		}
		#endregion ILabelPresentation
	}
}
