// v3.8.4.5.b
using System.Windows.Forms;

namespace SIGEM.Client.InteractionToolkit
{
	public partial class LoginForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
		}

		#region Code generated for Windows Forms

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mbCancel = new System.Windows.Forms.Button();
			this.mbOK = new System.Windows.Forms.Button();
			this.mTextBoxoid_1 = new System.Windows.Forms.TextBox();
			this.mlblLanguage = new System.Windows.Forms.Label();
			this.mLanguage = new System.Windows.Forms.ComboBox();
			this.mTextBoxPassword = new System.Windows.Forms.TextBox();
			this.mlblPassword = new System.Windows.Forms.Label();
			this.mlblLogin = new System.Windows.Forms.Label();
			this.mlblProfile = new System.Windows.Forms.Label();
			this.mAgent = new System.Windows.Forms.ComboBox();
			this.mPictureBox = new System.Windows.Forms.PictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.mPictureBox)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			//
			// mlblProfile
			//
			this.mlblProfile.AutoEllipsis = true;
			this.mlblProfile.Location = new System.Drawing.Point(192, 26);
			this.mlblProfile.Name = "mlblProfile";
			this.mlblProfile.Size = new System.Drawing.Size(67, 14);
			this.mlblProfile.TabIndex = 5;
			this.mlblProfile.Text = "Profile";
			//
			// mAgent
			//
			this.mAgent.BackColor = System.Drawing.SystemColors.Info;
			this.mAgent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mAgent.FormattingEnabled = true;
			this.mAgent.Location = new System.Drawing.Point(263, 22);
			this.mAgent.Name = "mAgent";
			this.mAgent.Size = new System.Drawing.Size(150, 21);
			this.mAgent.TabIndex = 0;
			this.mAgent.SelectedIndexChanged += new System.EventHandler(this.mAgent_SelectedIndexChanged);
			//
			// mlblLogin
			//
			this.mlblLogin.AutoEllipsis = true;
			this.mlblLogin.Location = new System.Drawing.Point(192, 56);
			this.mlblLogin.Name = "mlblLogin";
			this.mlblLogin.Size = new System.Drawing.Size(67, 14);
			this.mlblLogin.TabIndex = 10;
			this.mlblLogin.Text = "Login";
			//
			// mTextBoxoid_1
			//
			this.mTextBoxoid_1.BackColor = System.Drawing.SystemColors.Info;
			this.mTextBoxoid_1.Location = new System.Drawing.Point(263, 55);
			this.mTextBoxoid_1.Name = "mTextBoxoid_1";
			this.mTextBoxoid_1.Size = new System.Drawing.Size(100, 20);
			this.mTextBoxoid_1.TabIndex = 1;
			//
			// mlblPassword
			//
			this.mlblPassword.AutoEllipsis = true;
			this.mlblPassword.Location = new System.Drawing.Point(192, 85);
			this.mlblPassword.Name = "mlblPassword";
			this.mlblPassword.Size = new System.Drawing.Size(67, 14);
			this.mlblPassword.TabIndex = 8;
			this.mlblPassword.Text = "Password";
			//
			// mTextBoxPassword
			//
			this.mTextBoxPassword.BackColor = System.Drawing.SystemColors.Info;
			this.mTextBoxPassword.Location = new System.Drawing.Point(263, 81);
			this.mTextBoxPassword.MaxLength = 30;
			this.mTextBoxPassword.Name = "mTextBoxPassword";
			this.mTextBoxPassword.PasswordChar = '*';
			this.mTextBoxPassword.Size = new System.Drawing.Size(100, 20);
			this.mTextBoxPassword.TabIndex = 100;
			this.mTextBoxPassword.UseSystemPasswordChar = true;
			//
			// mlblLanguage
			//
			this.mlblLanguage.AutoEllipsis = true;
			this.mlblLanguage.Enabled = false;
			this.mlblLanguage.Location = new System.Drawing.Point(192, 115);
			this.mlblLanguage.Name = "mlblLanguage";
			this.mlblLanguage.Size = new System.Drawing.Size(67, 14);
			this.mlblLanguage.TabIndex = 10;
			this.mlblLanguage.Text = "Language";
			//
			// mLanguage
			//
			this.mLanguage.BackColor = System.Drawing.SystemColors.Info;
			this.mLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mLanguage.Enabled = false;
			this.mLanguage.FormattingEnabled = true;
			this.mLanguage.Items.AddRange(new object[] {
			"Non Available"});
			this.mLanguage.Location = new System.Drawing.Point(263, 111);
			this.mLanguage.Name = "mLanguage";
			this.mLanguage.Size = new System.Drawing.Size(150, 21);
			this.mLanguage.TabIndex = 101;
			this.mLanguage.SelectedIndexChanged += new System.EventHandler(this.MultilanguageFixedString);
			//
			// mbCancel
			//
			this.mbCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.mbCancel.AutoEllipsis = true;
			this.mbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.mbCancel.Location = new System.Drawing.Point(365, 141);
			this.mbCancel.Name = "mbCancel";
			this.mbCancel.Size = new System.Drawing.Size(75, 25);
			this.mbCancel.TabIndex = 103;
			this.mbCancel.Text = "Cancel";
			this.mbCancel.UseVisualStyleBackColor = true;
			this.mbCancel.Click += new System.EventHandler(this.mbCancel_Click);
			//
			// mbOK
			//
			this.mbOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.mbOK.AutoEllipsis = true;
			this.mbOK.Location = new System.Drawing.Point(286, 141);
			this.mbOK.Name = "mbOK";
			this.mbOK.Size = new System.Drawing.Size(75, 25);
			this.mbOK.TabIndex = 102;
			this.mbOK.Text = "OK";
			this.mbOK.UseVisualStyleBackColor = true;
			this.mbOK.Click += new System.EventHandler(this.mbOK_Click);
			//
			// mPictureBox
			//
			this.mPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mPictureBox.Image = global::SIGEM.Client.Properties.Resources.login;
			this.mPictureBox.Location = new System.Drawing.Point(0, 0);
			this.mPictureBox.Name = "mPictureBox";
			this.mPictureBox.Size = new System.Drawing.Size(186, 181);
			this.mPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mPictureBox.TabIndex = 2;
			this.mPictureBox.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.mPictureBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(186, 181);
			this.panel1.TabIndex = 11;
			//
			// LoginForm
			//
			this.AcceptButton = this.mbOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.mbCancel;
			this.ClientSize = new System.Drawing.Size(450, 172);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.mAgent);
			this.Controls.Add(this.mbOK);
			this.Controls.Add(this.mLanguage);
			this.Controls.Add(this.mbCancel);
			this.Controls.Add(this.mlblLanguage);
			this.Controls.Add(this.mlblProfile);
			this.Controls.Add(this.mTextBoxoid_1);
			this.Controls.Add(this.mlblLogin);
			this.Controls.Add(this.mlblPassword);
			this.Controls.Add(this.mTextBoxPassword);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoginForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Load += new System.EventHandler(this.LoginForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.mPictureBox)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion

		// Picture Panel.
		private Button mbOK;
		private Button mbCancel;
		private ComboBox mLanguage;
		private Label mlblLanguage;
		private Label mlblPassword;
		private TextBox mTextBoxPassword;
		private Label mlblLogin;
		private ComboBox mAgent;
		private Label mlblProfile;
		private PictureBox mPictureBox;
		private TextBox mTextBoxoid_1;
		private Panel panel1;
	}
}

