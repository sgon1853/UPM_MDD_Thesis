// v3.8.4.5.b
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

using SIGEM.Client.Presentation;
using SIGEM.Client.Presentation.Forms;
using SIGEM.Client.Controllers;
using SIGEM.Client.Oids;

namespace SIGEM.Client.InteractionToolkit
{
	public partial class LoginForm : Form
	{
		// Subscribe to the authentication events.
		public event EventHandler SuccessAthenticate;

		public event EventHandler CancelAthenticate;

		// Array of controls for the login agents OIDs.
		Control[] mEditors = new Control[1];
		// Used to validate the arguments that not allow nulls.
		private ErrorProvider lErrorProvider = new ErrorProvider();

		public LoginForm()
		{
			InitializeComponent();

			// Icon assignament.
			this.Icon = UtilFunctions.BitmapToIcon(Properties.Resources.loginMenu);

			// Apply MultiLanguage.
			this.mlblLogin.Text = CultureManager.TranslateString(LanguageConstantKeys.L_LOGIN, LanguageConstantValues.L_LOGIN, this.mlblLogin.Text);
			this.mlblProfile.Text = CultureManager.TranslateString(LanguageConstantKeys.L_LOGIN_PROFILE, LanguageConstantValues.L_LOGIN_PROFILE, this.mlblProfile.Text);
			this.mlblPassword.Text = CultureManager.TranslateString(LanguageConstantKeys.L_LOGIN_PASSWORD, LanguageConstantValues.L_LOGIN_PASSWORD, this.mlblPassword.Text);
			this.mlblLanguage.Text = CultureManager.TranslateString(LanguageConstantKeys.L_LOGIN_LANGUAGE, LanguageConstantValues.L_LOGIN_LANGUAGE, this.mlblLanguage.Text);
			this.mbCancel.Text = CultureManager.TranslateString(LanguageConstantKeys.L_CANCEL, LanguageConstantValues.L_CANCEL, this.mbCancel.Text);
			this.mbOK.Text = CultureManager.TranslateString(LanguageConstantKeys.L_OK, LanguageConstantValues.L_OK, this.mbOK.Text);

			mEditors[0] = mTextBoxoid_1;
		}

		private void mAgent_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Hide all the login OIDs.
			foreach (Control item in mEditors)
			{
				item.Visible = false;
			}

			// Create the selected agent.
			string lAgentClassName = (mAgent.SelectedItem as Logics.LogInAgent).Name;
			Oids.Oid lagent = Oids.Oid.Create(lAgentClassName);

			// Check if the selected agent is an anonymous agent
			// to hide the Login and Password controls.
			AgentInfo lSelectedAgent = lagent as AgentInfo;
			bool lIsAgentConnectedAnonymous = (lSelectedAgent is AnonymousAgentInfo);
			this.mlblLogin.Visible = !lIsAgentConnectedAnonymous;
			this.mlblPassword.Visible = !lIsAgentConnectedAnonymous;
			this.mTextBoxPassword.Visible = !lIsAgentConnectedAnonymous;

			if (!lIsAgentConnectedAnonymous)
			{
				// Show the login OIDs depending of the selected agent.
				Logics.LogInAgent lLogInAgent = Logics.Agents.GetLogInAgentByName(lAgentClassName);
				if (lLogInAgent.AlternateKeyName != string.Empty)
				{
					lagent = (Oid)lagent.GetAlternateKey(lLogInAgent.AlternateKeyName);
				}

				for (int i = 0; i < lagent.Fields.Count; i++)
				{
					mEditors[i].Visible = true;
				}

				// Modify the window size depending on the number of arguments in the OID.
				if (lagent.Fields.Count == 1)
				{
					SetClientSizeCore(450, 185);
				}
				else
				{
					SetClientSizeCore(300 + (lagent.Fields.Count * 100), 185);
				}
			}
			else
			{
				// Resize windows size for anonymous agents.
				SetClientSizeCore(450, 185);
			}
		}

		private void mbOK_Click(object sender, EventArgs e)
		{
			#region Actualize current language
			// Get the current language from the combobox language selector.
			CultureManager.Culture = new System.Globalization.CultureInfo(((KeyValuePair<string, string>)this.mLanguage.SelectedItem).Key);
			#endregion Actualize current language

			string lAgentClassName = (mAgent.SelectedItem as Logics.LogInAgent).Name;
			Oids.Oid lagent = Oids.Oid.Create(lAgentClassName);
			lagent.ClearValues();
			AgentInfo lAgentInfo = lagent as AgentInfo;
			Logics.LogInAgent lLogInAgent = Logics.Agents.GetLogInAgentByName(lAgentClassName);
			// Check if the connected agent has alternate key.
			if (lLogInAgent.AlternateKeyName != string.Empty)
			{
				// Obtain the Alternate Key used by the connected agent class, 
				// in order to be able of filling in the field values from the editors.
				lagent = (Oid)lagent.GetAlternateKey(lLogInAgent.AlternateKeyName);
				lagent.AlternateKeyName = lLogInAgent.AlternateKeyName;
			}

			if (!(lAgentInfo is AnonymousAgentInfo))
			{
				// To validate that all the login values are introduced.
				bool lbIsNull = false;
				// Error provider properties
				lErrorProvider.Clear();
				lErrorProvider.BlinkRate = 500;
				lErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

				#region Validate that the login and password are introduced
				// Validate the password field.
				if (mTextBoxPassword.Text == string.Empty)
				{
					// Set the focus and error
					mTextBoxPassword.Focus();
					lErrorProvider.SetError(mTextBoxPassword, CultureManager.TranslateString(LanguageConstantKeys.L_PASSWORD_NOT_NULL, LanguageConstantValues.L_PASSWORD_NOT_NULL));
					lErrorProvider.SetIconPadding(mTextBoxPassword, -20);
					lbIsNull = true;
				}

				// Validate the login fields.
				for (int i = mEditors.Length - 1; i >= 0; i--)
				{
					if (mEditors[i].Visible)
					{
						// Null validation.
						if (mEditors[i].Text == string.Empty)
						{
							// Set the focus and error
							mEditors[i].Focus();
							lErrorProvider.SetError(mEditors[i], CultureManager.TranslateString(LanguageConstantKeys.L_LOGIN_NOT_NULL, LanguageConstantValues.L_LOGIN_NOT_NULL));
							lErrorProvider.SetIconPadding(mEditors[i], -20);
							lbIsNull = true;
						}
					}
				}

				// If there are any empty argument.
				if (lbIsNull == true)
				{
					// Do not continue
					return;
				}
				#endregion Validate that the login and password are introduced
			}

			#region Create the agent
			int lOidField = 0;
			try
			{
				if (!(lAgentInfo is AnonymousAgentInfo))
				{
					// Set the OID type to the proper control.
					foreach (Control item in mEditors)
					{
						if (item.Visible == true)
						{
							try
							{
								if (!DefaultFormats.CheckDataType(item.Text, lagent.Fields[lOidField].Type, false))
								{
									string lText = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR_BAD_IDENTITY, LanguageConstantValues.L_ERROR_BAD_IDENTITY);
									string lMessage = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR, LanguageConstantValues.L_ERROR);
									MessageBox.Show(lText, lMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
									return;
								}
								object value = Logics.Logic.StringToModel(lagent.Fields[lOidField].Type, item.Text);
								lagent.SetValue(lOidField, value);
								lOidField++;
							}
							catch
							{
								string lText = CultureManager.TranslateString(LanguageConstantKeys.L_LOGIN_INCORRECT, LanguageConstantValues.L_LOGIN_INCORRECT);
								string lMessage = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR, LanguageConstantValues.L_ERROR);
								MessageBox.Show(lText, lMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return;
							}
						}
					}
				}

				#region Agent authentication
				if (lagent is AlternateKey)
				{
					AuthenticateAlternateKey(lagent, mTextBoxPassword.Text);
				}
				else
				{
					Authenticate(lagent as AgentInfo, mTextBoxPassword.Text);
				}
				#endregion Agent authentication
			}
			catch
			{
				string lMessage = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR, LanguageConstantValues.L_ERROR);
				string lExcepMsg = CultureManager.TranslateString(LanguageConstantKeys.L_ERROR_BAD_IDENTITY, LanguageConstantValues.L_ERROR_BAD_IDENTITY);
				MessageBox.Show(lExcepMsg,lMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			#endregion Create the agent.
		}
		private void mbCancel_Click(object sender, EventArgs e)
		{
			// Set that the agent authentication fails.
			if (CancelAthenticate != null)
			{
				CancelAthenticate(this, EventArgs.Empty);
			}
			this.Close();
		}

		private void LoginForm_Load(object sender, EventArgs e)
		{
			this.Text = "Desktop";

			// Get all the agents.
			List<Logics.LogInAgent> listLogInAgents = Logics.Agents.GetLogInAgents();
			mAgent.DataSource = listLogInAgents;
			mAgent.ValueMember = "Name";
			mAgent.DisplayMember = "Alias";

			if (Logics.Logic.Agent != null)
			{
				mAgent.SelectedItem = Logics.Logic.Agent;
			}

			#region Load supported languages
			List<KeyValuePair<string, string>> lSupportedLanguages = new List<KeyValuePair<string, string>>();

			// Load supported languages from the LanguageManager class.
			foreach (System.Globalization.CultureInfo litem in CultureManager.SupportedLanguages.Values)
			{
				lSupportedLanguages.Add(new KeyValuePair<string, string>(litem.Name, litem.NativeName));
			}

			// Load supported languages from the LanguageManager class into the combobox.
			if (lSupportedLanguages.Count > 0)
			{
				KeyValuePair<string, string> lSelectedLanguage = new KeyValuePair<string, string>(CultureManager.Culture.Name, CultureManager.Culture.NativeName);
				this.mLanguage.SelectedIndexChanged -= new System.EventHandler(this.MultilanguageFixedString);
				this.mLanguage.DataSource = lSupportedLanguages;
				this.mLanguage.ValueMember = "Key";
				this.mLanguage.DisplayMember = "Value";
				this.mLanguage.SelectedIndexChanged += new System.EventHandler(this.MultilanguageFixedString);
				this.mLanguage.SelectedItem = lSelectedLanguage;
				this.mLanguage.Enabled = true;
				this.mlblLanguage.Enabled = true;

			}
			/* Not avaliable languages for the application.
			 * IMPORNTANT: To enable any additional language, go to the
			 * 'CultureManager' class and add the needed language
			 * in 'CultureManager.SupportedLanguages' property.
			*/
			else
			{
				this.mLanguage.Items.Add("None");
				this.mLanguage.SelectedIndex = 0;
				this.mLanguage.Enabled = false;
				this.mlblLanguage.Enabled = false;

				// Check the anonymous agent.
				if (this.mAgent.Items.Count == 1)
				{
					string lAgentClassName = (mAgent.SelectedItem as Logics.LogInAgent).Name;
					AgentInfo lAgent = Oid.Create(lAgentClassName) as AgentInfo;
					if (lAgent is AnonymousAgentInfo)
					{
						LogIn(lAgent);
					}
				}
			}
			#endregion Load supported languages
		}

		/// <summary>
		/// Application agent authentication.
		/// </summary>
		/// <param name="agent">Application agent to authenticate.</param>
		/// <param name="password">Password.</param>
		private void Authenticate(AgentInfo agent, string password)
		{
			// Anonimous agent.
			if (agent is AnonymousAgentInfo)
			{
				LogIn(agent);
			}
			// If authentication is ok (wrapper authentication function).
			else if (Logics.Logic.Adaptor.Authenticate(agent, password))
			{
				LogIn(agent);
			}
		}
		/// <summary>
		/// Application agent authentication.
		/// </summary>
		/// <param name="agent">Application agent to authenticate.</param>
		/// <param name="password">Password.</param>
		private void AuthenticateAlternateKey(Oid agent, string password)
		{
			// If authentication is ok (wrapper authentication function).
			if (Logics.Logic.Adaptor.Authenticate(agent, password))
			{
				LogIn((agent.GetOid() as AgentInfo));
			}
		}

		/// <summary>
		/// Grant access to the application (LogIn).
		/// </summary>
		/// <param name="agent">Application agent.</param>
		private void LogIn(AgentInfo agent)
		{
			if (agent != null)
			{
				// Set the agent state.
				Logics.Logic.Agent = agent as AgentInfo;

				// Set the agent alias.
				Logics.Logic.Agent.Alias = mAgent.Text;

				// Set that the authentication is correct.
				if (SuccessAthenticate != null)
				{
					SuccessAthenticate(this, EventArgs.Empty);
				}
				this.Close();
			}
		}

		protected override void OnClosed(EventArgs e)
		{
			lErrorProvider.Dispose();
			base.OnClosed(e);
			mbCancel_Click(this,e);
		}
		private void MultilanguageFixedString(object sender, EventArgs e)
		{
			#region Actualize current language
			// Get the current language from the combobox language selector.
			CultureManager.Culture = new System.Globalization.CultureInfo(((KeyValuePair<string, string>)this.mLanguage.SelectedItem).Key);
			#endregion Actualize current language

			#region Get all the agents alias
			int lPreviousSelectedIndex = mAgent.SelectedIndex;
			mAgent.SelectedIndexChanged -= new System.EventHandler(this.mAgent_SelectedIndexChanged);
			mAgent.DataSource = Logics.Agents.GetLogInAgents();
			mAgent.ValueMember = "Name";
			mAgent.DisplayMember = "Alias";
			// Assign the previous selected Agent.
			mAgent.SelectedIndex = lPreviousSelectedIndex;
			mAgent.SelectedIndexChanged += new System.EventHandler(this.mAgent_SelectedIndexChanged);
			#endregion Get all the agents alias

			#region Actualize Fixed String
			//
			//Profile
			//
			this.mlblProfile.Text = CultureManager.TranslateString(LanguageConstantKeys.L_LOGIN_PROFILE, LanguageConstantValues.L_LOGIN_PROFILE);
			//
			// mAgent
			//
			this.mlblLogin.Text = CultureManager.TranslateString(LanguageConstantKeys.L_LOGIN, LanguageConstantValues.L_LOGIN);
			//
			// mlblPassword
			//
			this.mlblPassword.Text = CultureManager.TranslateString(LanguageConstantKeys.L_LOGIN_PASSWORD, LanguageConstantValues.L_LOGIN_PASSWORD);
			//
			// mlblLanguage
			//
			this.mlblLanguage.Text = CultureManager.TranslateString(LanguageConstantKeys.L_LOGIN_LANGUAGE, LanguageConstantValues.L_LOGIN_LANGUAGE);
			//
			// mbCancel
			//
			this.mbCancel.Text = CultureManager.TranslateString(LanguageConstantKeys.L_CANCEL, LanguageConstantValues.L_CANCEL);
			//
			// mbOK
			//
			this.mbOK.Text = CultureManager.TranslateString(LanguageConstantKeys.L_OK, LanguageConstantValues.L_OK);
			#endregion Actualize Fixed String
		}
	}
}

