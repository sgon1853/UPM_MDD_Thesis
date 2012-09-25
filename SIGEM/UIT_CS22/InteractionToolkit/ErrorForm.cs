// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using SIGEM.Client.Adaptor.Exceptions;

namespace SIGEM.Client.InteractionToolkit
{
	/// <summary>
	/// Application Error form.
	/// </summary>
	public partial class ErrorForm : Form
	{
		#region Members
		private const int expandedHeigh = 330;
		private const int hiddedHeigh = 140;
		private bool mShowDetails = false;
		#endregion Members
		#region Constructors
		public ErrorForm()
		{
			InitializeComponent();

			// Icon assignament.
			this.Icon = UtilFunctions.BitmapToIcon(Properties.Resources.warning);
			
			SetTexts();

			ShowErrorDetails();
		}
		#endregion Constructors

		#region Intialize
		/// <summary>
		/// Initializes the 'ErrorForm' of the application.
		/// </summary>
		/// <param name="exception">Bussiness logic exception.</param>
		public void Initialize(ResponseException exception)
		{
			// Show the business logic exception.
			ShowBusinessLogicError(exception);			
		}
		/// <summary>
		/// Initializes the 'ErrorForm' of the application.
		/// </summary>
		/// <param name="exception">Exception thrown.</param>
		public void Initialize(Exception exception)
		{
			// Show the client exception.
			ShowClientError(exception);
		}
		#endregion Initialize

		#region Methods

		/// <summary>
		/// Assigns the button texts and caption depending on the language.
		/// </summary>
		private void SetTexts()
		{
			this.Text = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR, LanguageConstantValues.L_ERROR, this.Text);
			this.buttonReport.Text = CultureManager.TranslateString(LanguageConstantKeys.L_REPORT, LanguageConstantValues.L_REPORT, this.buttonReport.Text);
			this.buttonCopy.Text = CultureManager.TranslateString(LanguageConstantKeys.L_COPY, LanguageConstantValues.L_COPY, this.buttonCopy.Text);
			this.buttonClose.Text = CultureManager.TranslateString(LanguageConstantKeys.L_CLOSE, LanguageConstantValues.L_CLOSE, this.buttonClose.Text);
		}

		/// <summary>
		/// Show the business logic errors and exceptions.
		/// </summary>
		/// <param name="exception">ResponseException.</param>
		private void ShowBusinessLogicError(ResponseException exception)
		{
			// Exception message translated depending on the culture.
			this.labelErrorDesciption.Text = CultureManager.TranslateBusinessLogicException(exception);				

			// Show all the exception traces.
			if ((exception != null) && (exception.Traces != null))
			{
				this.richTextBoxDetails.Clear();
				this.richTextBoxDetails.AppendText("Bussines Logic Traces:");
				this.richTextBoxDetails.AppendText("\r");

				this.richTextBoxDetails.SelectionBullet = true;

				// Show traces as a LIFO (Last In First Out) stack.
				for (int i = exception.Traces.Count - 1; i >= 0; i--)
				{
					this.richTextBoxDetails.AppendText("Error Number " + exception.Traces[i].Number + ": " + CultureManager.TranslateBusinessLogicException(exception.Traces[i]) + ".");
					this.richTextBoxDetails.AppendText("\r");
				}

				this.richTextBoxDetails.SelectionBullet = false;
			}
		}
		/// <summary>
		/// Show the client errors and exceptions.
		/// </summary>
		/// <param name="exception">ResponseException.</param>
		private void ShowClientError(Exception exception)
		{
			if (exception == null)
			{
				return;
			}

			// Error description.
			this.labelErrorDesciption.Text = exception.Message;

			// Show all the details.
			this.richTextBoxDetails.Clear();
			while (exception.InnerException != null)
			{
				this.richTextBoxDetails.AppendText(exception.InnerException.Message);
				this.richTextBoxDetails.AppendText("\r");
				exception = exception.InnerException;
			}
			this.richTextBoxDetails.SelectionBullet = false;
		}
		#endregion Methods		

		#region Event handlers
		private void buttonHide_Click(object sender, EventArgs e)
		{
			ShowErrorDetails();
		}

		/// <summary>
		/// Shows or hides the error details.
		/// </summary>
		private void ShowErrorDetails()
		{
			buttonCopy.Visible = mShowDetails;
			buttonReport.Visible = false;
			richTextBoxDetails.Visible = mShowDetails;

			if (mShowDetails)
			{
				buttonHide.Text = CultureManager.TranslateString(LanguageConstantKeys.L_HIDE, LanguageConstantValues.L_HIDE);
				this.Height = expandedHeigh;
				richTextBoxDetails.SelectionHangingIndent = 10;
				richTextBoxDetails.SelectionIndent = 5;
			}
			else
			{
				buttonHide.Text = CultureManager.TranslateString(LanguageConstantKeys.L_DETAILS, LanguageConstantValues.L_DETAILS);
				this.Height = hiddedHeigh;
			}

			mShowDetails = !mShowDetails;
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void buttonCopy_Click(object sender, EventArgs e)
		{
			try
			{
				// Copy error message detail text to clipboard
				Clipboard.SetText(richTextBoxDetails.Text);
			}
			catch
			{
			}
		}

		#region Report
		private void buttonReport_Click(object sender, EventArgs e)
		{
			try
			{
				// Set mail server
				SmtpClient client = new SmtpClient("ServerName");
				// Set sender
				MailAddress from = new MailAddress("user@MailAddress");
				// Set destination
				MailAddress to = new MailAddress("admin@MailAddress");
				// Set message content
				MailMessage message = new MailMessage(from, to);
				message.Body = "Body";
				message.BodyEncoding = System.Text.Encoding.UTF8;
				message.Subject = "Subject";
				message.SubjectEncoding = System.Text.Encoding.UTF8;

				// Set the method that is called back when the send operation ends
				client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

				// Set the userState, which allows to use callback method to identify the send
				string userState = from.Address;

				// Send
				client.SendAsync(message, userState);

				// Clear
				message.Dispose();
			}
			catch (SmtpException err)
			{
				MessageBox.Show(err.Message);
			}
		}
		public static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
		{
			// Get the userState identifier
			String token = (string)e.UserState;

			if (e.Cancelled)
			{
				MessageBox.Show("Send canceled", token);
			}

			if (e.Error != null)
			{
				MessageBox.Show(e.Error.ToString());
			}
			else
			{
				MessageBox.Show("Message sent");
			}
		}
		#endregion Report

		#endregion Event handlers

	}
}

