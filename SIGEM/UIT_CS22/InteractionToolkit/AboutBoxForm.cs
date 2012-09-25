// v3.8.4.5.b
using System;
using System.Windows.Forms;

namespace SIGEM.Client.InteractionToolkit
{
	public partial class AboutBoxForm : Form
	{
		#region Members
		#region Fade In/ Fade Out
		private double mOpacityIncrement = 0;
		private double mOpacityDecrement = 0;
		#endregion Fade In/ Fade Out

		private Timer mTimerText;
		private Timer mTimerToStart;
		private Timer mTimerToClose;
		private int mNumberSecondsToClose = 2;
		private double mTimerToCloseElapsedIntervals = 0;
		private double mTimerToPaintElapsedIntervals = 0;
		private long mElapsedTime = 0;
		#endregion Members

		#region Constructors
		public AboutBoxForm()
		{
			InitializeComponent();

			// Apply MultiLanguage.
			this.lblAplicationTitle.Text = CultureManager.TranslateString(LanguageConstantKeys.L_ABOUT_FORM_TITLE, LanguageConstantValues.L_ABOUT_FORM_TITLE, this.lblAplicationTitle.Text);
			this.lblVersionNumber.Text = CultureManager.TranslateString(LanguageConstantKeys.L_ABOUT_VERSION, LanguageConstantValues.L_ABOUT_VERSION, this.lblVersionNumber.Text);
			this.lblText.Text = CultureManager.TranslateString(LanguageConstantKeys.L_ABOUT_COPYRIGHT, LanguageConstantValues.L_ABOUT_COPYRIGHT, this.lblText.Text);


			#region Init Fade In /Fade Out
			mOpacityIncrement = .05;
			mOpacityDecrement = .08;
			Opacity = .00;
			mTimerToStart.Start();
			#endregion Init Fade In /Fade Out

			ClientSize = this.BackgroundImage.Size;

			// Timer for the information text
			mTimerText.Start();
		}
		#endregion Constructors

		#region Methods [Private]

		#region Fade In / Fade Out  & Close Form & Info Text Movement
		private void FadeInFadeOutOrClose()
		{
			// Fade In/Out
			if (mOpacityIncrement > 0)
			{
				if (Opacity < 1)
				{
					Opacity += mOpacityIncrement;
				}
			}
			else
			{
				if (Opacity > 0)
				{
					Opacity += mOpacityIncrement;
				}
				else
				{
					base.Close();
				}
			}

			// Info text movement
			mElapsedTime += mTimerText.Interval;
		}
		#endregion  Fade In / Fade Out  & Close Form & Info Text Movement

		#region Seconds & Milliseconds operations
		private double CountIntervals(double timerInterval)
		{
			return (MillisecondsToClose() / timerInterval);
		}
		private double MillisecondsToClose()
		{
			return (mNumberSecondsToClose * 1000);
		}
		private int ElapsedSecondsToClose(double timerInterval, double countIntervals)
		{
			return (int)(mNumberSecondsToClose - ElapsedSeconds(timerInterval, countIntervals));
		}
		private int ElapsedSeconds(double timerInterval, double countIntervals)
		{
			return (int)((timerInterval * countIntervals) / 1000);
		}
		#endregion Seconds & Milliseconds operations

		#endregion Methods [Private]

		#region Controls Events

		#region Close the Form
		// Double Click on the form close it
		private void Double_Click(object sender, MouseEventArgs e)
		{
			// This Close the form.
			mOpacityIncrement = (-mOpacityDecrement);
		}

		private void pictureBoxClose_Click(object sender, EventArgs e)
		{
			// This Close the form.
			mOpacityIncrement = (-mOpacityDecrement);
		}
		#endregion Close the Form

		#region Timers

		#region Timer calculate the elapsed time for close the form
		private void TimerToClose_Tick(object sender, EventArgs e)
		{
			int iSecondsLeft = ElapsedSecondsToClose(mTimerToClose.Interval, mTimerToCloseElapsedIntervals);
			mTimerToCloseElapsedIntervals++;

			#region This condition Close the Form
			if (iSecondsLeft <= 0)
			{
				// This Close the form
				mOpacityIncrement = (-mOpacityDecrement);
			}
			#endregion This condition Close the Form
		}
		#endregion Timer calculate the elapsed time for close the form

		#region Timer Paint Event
		private void TimerToPaint_Tick(object sender, EventArgs e)
		{
			// Fade In/Out or Close
			FadeInFadeOutOrClose();

			double lCount = 1 / CountIntervals(mTimerToStart.Interval);

			// count of intervals timer
			mTimerToPaintElapsedIntervals++;
		}
		#endregion Timer Paint Event

		#endregion Timers

		#region Modify the pictureBoxClose border style
		private void pictureBoxClose_MouseHover(object sender, EventArgs e)
		{
			pictureBoxClose.BorderStyle = BorderStyle.FixedSingle;
		}

		private void pictureBoxClose_MouseLeave(object sender, EventArgs e)
		{
			pictureBoxClose.BorderStyle = BorderStyle.None;
		}
		#endregion Modify the pictureBoxClose border style

		#endregion Controls Events
	}
}

