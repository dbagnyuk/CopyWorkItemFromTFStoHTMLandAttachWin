namespace CopyWorkItemFromTFStoHTMLandAttachWin
{
    partial class configForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(configForm));
            this.groupBoxConfig = new System.Windows.Forms.GroupBox();
            this.buttonDirChoose = new System.Windows.Forms.Button();
            this.buttonConfigSave = new System.Windows.Forms.Button();
            this.labelPath = new System.Windows.Forms.Label();
            this.labelPass = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBoxConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxConfig
            // 
            this.groupBoxConfig.Controls.Add(this.buttonDirChoose);
            this.groupBoxConfig.Controls.Add(this.buttonConfigSave);
            this.groupBoxConfig.Controls.Add(this.labelPath);
            this.groupBoxConfig.Controls.Add(this.labelPass);
            this.groupBoxConfig.Controls.Add(this.labelLogin);
            this.groupBoxConfig.Controls.Add(this.textBoxPath);
            this.groupBoxConfig.Controls.Add(this.textBoxPass);
            this.groupBoxConfig.Controls.Add(this.textBoxLogin);
            this.groupBoxConfig.Location = new System.Drawing.Point(10, 10);
            this.groupBoxConfig.Name = "groupBoxConfig";
            this.groupBoxConfig.Size = new System.Drawing.Size(220, 220);
            this.groupBoxConfig.TabIndex = 0;
            this.groupBoxConfig.TabStop = false;
            this.groupBoxConfig.Text = "Create/Edit Config File";
            this.groupBoxConfig.Enter += new System.EventHandler(this.groupBoxConfig_Enter);
            // 
            // buttonDirChoose
            // 
            this.buttonDirChoose.Location = new System.Drawing.Point(185, 146);
            this.buttonDirChoose.Name = "buttonDirChoose";
            this.buttonDirChoose.Size = new System.Drawing.Size(25, 25);
            this.buttonDirChoose.TabIndex = 7;
            this.buttonDirChoose.Text = "...";
            this.buttonDirChoose.UseVisualStyleBackColor = true;
            this.buttonDirChoose.Click += new System.EventHandler(this.buttonDirChoose_Click);
            // 
            // buttonConfigSave
            // 
            this.buttonConfigSave.Location = new System.Drawing.Point(135, 190);
            this.buttonConfigSave.Name = "buttonConfigSave";
            this.buttonConfigSave.Size = new System.Drawing.Size(75, 23);
            this.buttonConfigSave.TabIndex = 6;
            this.buttonConfigSave.Text = "Save";
            this.buttonConfigSave.UseVisualStyleBackColor = true;
            this.buttonConfigSave.Click += new System.EventHandler(this.buttonConfigSave_Click);
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(10, 131);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(94, 13);
            this.labelPath.TabIndex = 5;
            this.labelPath.Text = "Path to Attach Dir:";
            // 
            // labelPass
            // 
            this.labelPass.AutoSize = true;
            this.labelPass.Location = new System.Drawing.Point(10, 78);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(56, 13);
            this.labelPass.TabIndex = 4;
            this.labelPass.Text = "Password:";
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(10, 25);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(77, 13);
            this.labelLogin.TabIndex = 3;
            this.labelLogin.Text = "Domain\\Login:";
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(10, 149);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(165, 20);
            this.textBoxPath.TabIndex = 2;
            this.textBoxPath.TextChanged += new System.EventHandler(this.textBoxPath_TextChanged);
            this.textBoxPath.Click += new System.EventHandler(this.textBoxPath_Click);
            // 
            // textBoxPass
            // 
            this.textBoxPass.Location = new System.Drawing.Point(10, 96);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.PasswordChar = '*';
            this.textBoxPass.Size = new System.Drawing.Size(200, 20);
            this.textBoxPass.TabIndex = 1;
            this.textBoxPass.UseSystemPasswordChar = true;
            this.textBoxPass.Click += new System.EventHandler(this.textBoxPass_Click);
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.AccessibleName = "";
            this.textBoxLogin.Location = new System.Drawing.Point(10, 43);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(200, 20);
            this.textBoxLogin.TabIndex = 0;
            this.textBoxLogin.Click += new System.EventHandler(this.textBoxLogin_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.UserProfile;
            this.folderBrowserDialog.HelpRequest += new System.EventHandler(this.folderBrowserDialog_HelpRequest);
            // 
            // configForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 237);
            this.Controls.Add(this.groupBoxConfig);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(690, 530);
            this.MaximumSize = new System.Drawing.Size(255, 275);
            this.MinimumSize = new System.Drawing.Size(255, 275);
            this.Name = "configForm";
            this.Text = "Config";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.configForm_FormClosing);
            this.Load += new System.EventHandler(this.configForm_Load);
            this.groupBoxConfig.ResumeLayout(false);
            this.groupBoxConfig.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxConfig;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.TextBox textBoxPass;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Button buttonConfigSave;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button buttonDirChoose;
    }
}