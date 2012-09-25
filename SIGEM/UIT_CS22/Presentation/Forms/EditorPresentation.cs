// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Presentation.Forms
{
	/// <summary>
	/// Presentation abstract class of common methods list for Presentations
	/// </summary>
	public class EditorPresentation
	{
		#region Members
		/// <summary>
		/// Previous value for control.
		/// </summary>
		object mPreviousValue;
		#endregion Memebers

		#region Constructors
		protected EditorPresentation()
		{
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets PreviousValue property.
		/// </summary>
		protected object PreviousValue
		{
			get
			{
				return mPreviousValue;
			}
			set
			{
				mPreviousValue = value;
			}
		}
		#endregion Properties

		#region Methods
		/// <summary>
		/// Checks if control value has changed and return true or false
		/// </summary>
		/// <param name="currentValue"></param>
		protected bool CheckValueChange(object currentValue)
		{
			try
			{
				//Return false when value is null
				if (PreviousValue == null && currentValue == null)
					return false;

				if (PreviousValue == null || currentValue == null)
					return false;

				//Return true or false result of comparison
				return !PreviousValue.Equals(currentValue);
			}
			catch
			{
			}

			return true;
		}

        /// <summary>
        /// If control is inside a TabPage, activate it in the TabControl
        /// </summary>
        /// <param name="control"></param>
        protected void ActivateParentTabPage(System.Windows.Forms.Control control)
        {
            System.Windows.Forms.TabPage lTabPage = control as System.Windows.Forms.TabPage;

            if (lTabPage != null)
            {
                if (lTabPage.Parent != null)
                {
                    System.Windows.Forms.TabControl lTabControl = lTabPage.Parent as System.Windows.Forms.TabControl;
                    if (lTabControl != null)
                    {
                        lTabControl.SelectedTab = lTabPage;
                    }
                }
            }
            else
            {
                if (control.Parent != null)
                {
                    ActivateParentTabPage(control.Parent);
                }
            }
        }
		#endregion Methods
	}
}
