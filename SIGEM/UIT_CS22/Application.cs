// v3.8.4.5.b
using System;
using System.Collections.Generic;

using SIGEM.Client.Controllers;
using SIGEM.Client.Oids;
using SIGEM.Client.Presentation;
using SIGEM.Client.InteractionToolkit;

namespace SIGEM.Client
{
	/// <summary>
	/// Main class of the application.
	/// </summary>
	class Application
	{
		[STAThread]
		static void Main(string[] args)
		{
			//Enable Visual Styles in application forms.
			System.Windows.Forms.Application.EnableVisualStyles();
			System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

			// Show Splash Screen.
			MainSplashForm.ShowSplashScreen();

			System.Windows.Forms.Application.Run(ScenarioManager.MainForm);
		}
	}
}

