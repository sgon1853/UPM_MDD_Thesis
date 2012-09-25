// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;

using SIGEM.Client.Controllers;
using SIGEM.Client.Presentation;

namespace SIGEM.Client.InteractionToolkit
{

	public partial class MainSplashForm : Form
	{
		#region Members
		#region Fade In/ Fade Out
		private double mOpacityIncrement = 0;
			private double mOpacityDecrement = 0;
		#endregion Fade In/ Fade Out

		#region Progress Bar
			private Rectangle mProgress;
		#endregion Progress Bar

		private int mNumberSecondsToClose = 2;
		private double mTimerToCloseElapsedIntervals = 0;
		private double mTimerToPaintElapsedIntervals = 0;
		private double mProgressBarCompletion = 0;

		#endregion Members

		#region Constructors
		public MainSplashForm()
		{
			InitializeComponent();

			#region Init Fade In /Fade Out
			mOpacityIncrement = .05;
			mOpacityDecrement = .08;
			Opacity = .00;
			mTimer.Start();
			#endregion Init Fade In /Fade Out

			ClientSize = this.BackgroundImage.Size;

			try
			{
				mNumberSecondsToClose = int.Parse(mTimerToClose.Tag.ToString());

				if(mNumberSecondsToClose < 0)
					mOpacityIncrement = (-mOpacityDecrement);
			}
			catch
			{
				mNumberSecondsToClose = 2; // Secods to close by default
			}
			finally
			{
				mTimerToClose.Start();
			}

		}
		#endregion Constructors

		#region Methods [Private]

		#region Fade In / Fade Out  & Close Form
		private void FadeInFadeOutOrClose()
		{
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
		}
		#endregion  Fade In / Fade Out  & Close Form

		#region Invalidate Panel -> throw panel paint
		private void InvalidatedPanelProgress()
		{
			if (mpnlStatus.RightToLeft == RightToLeft.Yes)
			{
				int width = (int)Math.Floor(mpnlStatus.ClientRectangle.Width * mProgressBarCompletion);
				int height = mpnlStatus.ClientRectangle.Height;
				int x = mpnlStatus.ClientRectangle.Width - width; 
				int y = mpnlStatus.ClientRectangle.Y; 
				if (width > 0 && height > 0)
				{
					width = mpnlStatus.ClientRectangle.Width;
					mProgress = new Rectangle(x, y, width, height);
					mpnlStatus.Invalidate(mProgress);
				}
			}
			else
			{
			int width = (int)Math.Floor(mpnlStatus.ClientRectangle.Width * mProgressBarCompletion);
			int height = mpnlStatus.ClientRectangle.Height;
			int x = mpnlStatus.ClientRectangle.X;
			int y = mpnlStatus.ClientRectangle.Y;
			if (width > 0 && height > 0)
			{
				mProgress = new Rectangle(x, y, width, height);
				mpnlStatus.Invalidate(mProgress);
				}
			}
		}
		#endregion Invalidate Panel

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

		#region Status Message
		private string mStatusMessage = string.Empty;
		private void SetStatusMessage()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(SetStatusMessage));
			}
			else
			{
				mlblStatus.Text = mStatusMessage;
			}
		}
		public string StatusMessage
		{
			set
			{
				mStatusMessage = value;
				SetStatusMessage();
			}
			get
			{
				return mlblStatus.Text;
			}
		}
		#endregion Status Message

		#region Paint Progress Bar
		private void PaintProgressBarInPanel(object sender, PaintEventArgs e)
		{
			if (mpnlStatus.RightToLeft == RightToLeft.Yes)
			{
				if (mProgressBarCompletion > 0)
				{
					if (e.ClipRectangle.Width > 0)
					{
						LinearGradientBrush brBackground =
							new LinearGradientBrush(mProgress,													
													Color.FromArgb(181, 237, 254),
													Color.FromArgb(58, 96, 151),
													LinearGradientMode.Horizontal);

						e.Graphics.FillRectangle(brBackground, mProgress);
					}
				}
			}
			else
			{
			if (mProgressBarCompletion > 0)
			{
				if (e.ClipRectangle.Width > 0)
				{
					LinearGradientBrush brBackground =
						new LinearGradientBrush(mProgress,
						Color.FromArgb(58, 96, 151),
						Color.FromArgb(181, 237, 254),
						LinearGradientMode.Horizontal);
					e.Graphics.FillRectangle(brBackground, mProgress);
					}
				}
			}
		}
		#endregion Paint Progress Bar

		#endregion Methods [Private]

		#region Controls Events

		#region Double Click Close the Form
		private void Double_Click(object sender, MouseEventArgs e)
		{
			// this Close the form.
			mOpacityIncrement = (-mOpacityDecrement);
		}
		#endregion Double Click Close the Form

		#region Timers

		#region Timer calculate the elapsed time for close the form
		private void TimerToClose_Tick(object sender, EventArgs e)
		{
			int iSecondsLeft = ElapsedSecondsToClose(mTimerToClose.Interval, mTimerToCloseElapsedIntervals);
			mTimerToCloseElapsedIntervals++;

			#region Time Remaining
			/*if (iSecondsLeft == 1)
			{
				mlblTimeRemaining.Text = string.Format("1 second remaining");
			}
			else
			{
				mlblTimeRemaining.Text = string.Format("{0} seconds remaining", iSecondsLeft);
			}*/
			#endregion Time Remaining

			#region This condition Close the Form
			if (iSecondsLeft <= 0)
			{
				mOpacityIncrement = (-mOpacityDecrement); // This Close the form
			}
			#endregion This condition Close the Form
		}
		#endregion Timer calculate the elapsed time for close the form

		#region Timer Paint ProgressBar Event
		private void TimerToPaint_Tick(object sender, EventArgs e)
		{
			// Fade In/Out or Close
			FadeInFadeOutOrClose();

			double lCount = 1 / CountIntervals(mTimer.Interval);

			if (mProgressBarCompletion <= 1)
			{
				mProgressBarCompletion += lCount;

				if (ElapsedSecondsToClose(mTimer.Interval, mTimerToPaintElapsedIntervals) > 0)
				{
					// Invalidate Panel
					InvalidatedPanelProgress();
				}
			}
			// count of intervals timer
			mTimerToPaintElapsedIntervals++;
		}
		#endregion Timer Paint ProgressBar Event

		public  new void Close()
		{
			mOpacityIncrement = (-mOpacityDecrement);
		}

		#endregion Timers

		#endregion Controls Events

		#region Statics

		// Threading
		static MainSplashForm ms_frmSplash = null;
		static Thread ms_oThread = null;

		public static Thread SplashThread
		{
			get { return ms_oThread; }
		}


		static public void ShowSplashScreen()
		{
			// Make sure it's only launched once.
			if (ms_frmSplash != null)
				return;
			ms_oThread = new Thread(new ThreadStart(MainSplashForm.ShowForm));
			ms_oThread.IsBackground = true;
			ms_oThread.SetApartmentState(ApartmentState.STA);
			ms_oThread.Start();
		}

		// A property returning the splash screen instance
		static public MainSplashForm SplashForm
		{
			get { return ms_frmSplash; }
		}

		// A private entry point for the thread.
		static private void ShowForm()
		{
			ms_frmSplash = new MainSplashForm();
			System.Windows.Forms.Application.Run(ms_frmSplash);
		}


		// A static method to set the status and update the reference.
		static public void SetStatus(string newStatus)
		{
			ms_frmSplash.StatusMessage = newStatus;
		}

		#endregion Statics
	}
}

