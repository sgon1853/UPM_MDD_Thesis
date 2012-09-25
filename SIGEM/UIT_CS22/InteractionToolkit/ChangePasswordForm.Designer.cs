// v3.8.4.5.b
using System.Windows.Forms;

namespace SIGEM.Client.InteractionToolkit
{
	public partial class ChangePasswordForm
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
		this.mbOK = new System.Windows.Forms.Button();
		this.mbCancel = new System.Windows.Forms.Button();
		this.mTextBoxRetypeNewPass = new System.Windows.Forms.TextBox();
		this.mlblRetypeNewPass = new System.Windows.Forms.Label();
		this.mTextBoxNewPass = new System.Windows.Forms.TextBox();
		this.mlblNewPass = new System.Windows.Forms.Label();
		this.mlblCurrentPass = new System.Windows.Forms.Label();
		this.mTextBoxCurrenPas = new System.Windows.Forms.TextBox();
		this.SuspendLayout();
		//
		// mbOK
		//
		this.mbOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.mbOK.AutoEllipsis = true;
		this.mbOK.Location = new System.Drawing.Point(114, 108);
		this.mbOK.Name = "mbOK";
		this.mbOK.Size = new System.Drawing.Size(66, 24);
		this.mbOK.TabIndex = 3;
		this.mbOK.Text = "OK";
		this.mbOK.UseVisualStyleBackColor = true;
		this.mbOK.Click += new System.EventHandler(this.mbOK_Click);
		//
		// mbCancel
		//
		this.mbCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.mbCancel.AutoEllipsis = true;
		this.mbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.mbCancel.Location = new System.Drawing.Point(186, 108);
		this.mbCancel.Name = "mbCancel";
		this.mbCancel.Size = new System.Drawing.Size(66, 24);
		this.mbCancel.TabIndex = 4;
		this.mbCancel.Text = "Cancel";
		this.mbCancel.UseVisualStyleBackColor = true;
		this.mbCancel.Click += new System.EventHandler(this.mbCancel_Click);
		//
		// mTextBoxRetypeNewPass
		//
		this.mTextBoxRetypeNewPass.BackColor = System.Drawing.SystemColors.Info;
		this.mTextBoxRetypeNewPass.Location = new System.Drawing.Point(137, 70);
		this.mTextBoxRetypeNewPass.MaxLength = 30;
		this.mTextBoxRetypeNewPass.Name = "mTextBoxRetypeNewPass";
		this.mTextBoxRetypeNewPass.PasswordChar = '*';
		this.mTextBoxRetypeNewPass.Size = new System.Drawing.Size(100, 20);
		this.mTextBoxRetypeNewPass.TabIndex = 2;
		this.mTextBoxRetypeNewPass.UseSystemPasswordChar = true;
		//
		// mlblRetypeNewPass
		//
		this.mlblRetypeNewPass.AutoEllipsis = true;
		this.mlblRetypeNewPass.Location = new System.Drawing.Point(13, 72);
		this.mlblRetypeNewPass.Name = "mlblRetypeNewPass";
		this.mlblRetypeNewPass.Size = new System.Drawing.Size(118, 17);
		this.mlblRetypeNewPass.TabIndex = 10;
		this.mlblRetypeNewPass.Text = "Retype new password";
		//
		// mTextBoxNewPass
		//
		this.mTextBoxNewPass.BackColor = System.Drawing.SystemColors.Info;
		this.mTextBoxNewPass.Location = new System.Drawing.Point(137, 47);
		this.mTextBoxNewPass.MaxLength = 30;
		this.mTextBoxNewPass.Name = "mTextBoxNewPass";
		this.mTextBoxNewPass.PasswordChar = '*';
		this.mTextBoxNewPass.Size = new System.Drawing.Size(100, 20);
		this.mTextBoxNewPass.TabIndex = 1;
		this.mTextBoxNewPass.UseSystemPasswordChar = true;
		//
		// mlblNewPass
		//
		this.mlblNewPass.AutoEllipsis = true;
		this.mlblNewPass.Location = new System.Drawing.Point(13, 51);
		this.mlblNewPass.Name = "mlblNewPass";
		this.mlblNewPass.Size = new System.Drawing.Size(118, 13);
		this.mlblNewPass.TabIndex = 8;
		this.mlblNewPass.Text = "New Password";
		//
		// mlblCurrentPass
		//
		this.mlblCurrentPass.AutoEllipsis = true;
		this.mlblCurrentPass.Location = new System.Drawing.Point(13, 24);
		this.mlblCurrentPass.Name = "mlblCurrentPass";
		this.mlblCurrentPass.Size = new System.Drawing.Size(118, 13);
		this.mlblCurrentPass.TabIndex = 10;
		this.mlblCurrentPass.Text = "Old Password";
		//
		// mTextBoxCurrenPas
		//
		this.mTextBoxCurrenPas.BackColor = System.Drawing.SystemColors.Info;
		this.mTextBoxCurrenPas.Location = new System.Drawing.Point(137, 20);
		this.mTextBoxCurrenPas.MaxLength = 30;
		this.mTextBoxCurrenPas.Name = "mTextBoxCurrenPas";
		this.mTextBoxCurrenPas.PasswordChar = '*';
		this.mTextBoxCurrenPas.Size = new System.Drawing.Size(100, 20);
		this.mTextBoxCurrenPas.TabIndex = 0;
		this.mTextBoxCurrenPas.UseSystemPasswordChar = true;
		//
		// ChangePasswordForm
		//
		this.AcceptButton = this.mbOK;
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.CancelButton = this.mbCancel;
		this.ClientSize = new System.Drawing.Size(263, 137);
		this.Controls.Add(this.mbOK);
		this.Controls.Add(this.mbCancel);
		this.Controls.Add(this.mlblRetypeNewPass);
		this.Controls.Add(this.mTextBoxRetypeNewPass);
		this.Controls.Add(this.mTextBoxCurrenPas);
		this.Controls.Add(this.mlblCurrentPass);
		this.Controls.Add(this.mTextBoxNewPass);
		this.Controls.Add(this.mlblNewPass);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "ChangePasswordForm";
		this.ShowInTaskbar = false;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Change Password";
		this.ResumeLayout(false);
		this.PerformLayout();
	}
	#endregion

	// Fields Panel.
	private TextBox mTextBoxCurrenPas;
	private Label mlblNewPass;

	// Buttons Panel
	#if(!SECURE)

	#endif

	private Button mbOK;
	private Button mbCancel;
	private Label mlblCurrentPass;
	private Label mlblRetypeNewPass;
	private TextBox mTextBoxNewPass;
	private TextBox mTextBoxRetypeNewPass;
	}
}

