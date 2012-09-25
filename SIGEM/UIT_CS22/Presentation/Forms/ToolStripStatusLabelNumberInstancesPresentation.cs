// v3.8.4.5.b
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIGEM.Client.Presentation.Forms
{
	class ToolStripStatusLabelNumberInstancesPresentation : ToolStripStatusLabelPresentation
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of 'ToolStripStatusLabel'
		/// </summary>
		/// <param name="toolStripStatusLabel">ToolStripStatusLabel</param>
		public ToolStripStatusLabelNumberInstancesPresentation(ToolStripStatusLabel toolStripStatusLabel)
			: this(toolStripStatusLabel, string.Empty)
		{
			this.mToolStripStatusLabelIT.ToolTipText = CultureManager.TranslateString(LanguageConstantKeys.L_NUMBER_OF_ELEMENTS, LanguageConstantValues.L_NUMBER_OF_ELEMENTS, this.mToolStripStatusLabelIT.ToolTipText);
		}
		/// <summary>
		/// Initializes a new instance of 'ToolStripStatusLabel'
		/// </summary>
		/// <param name="toolStripStatusLabel">ToolStripStatusLabel</param>
		/// <param name="toolTip">ToolTipText</param>
		public ToolStripStatusLabelNumberInstancesPresentation(ToolStripStatusLabel toolStripStatusLabel, string toolTip)
			: base(toolStripStatusLabel,toolTip)
		{
		}
		#endregion Constructors
	}
}

