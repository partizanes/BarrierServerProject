namespace PrioritySales
{
    partial class AuthForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthForm));
            this.labelTimerMsg = new System.Windows.Forms.Timer(this.components);
            this.TaskbarIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.login_panel = new System.Windows.Forms.Panel();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.LabelSetting = new System.Windows.Forms.Label();
            this.LabelExit = new System.Windows.Forms.Label();
            this.LabelHide = new System.Windows.Forms.Label();
            this.LabelMsg = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TextboxLogin = new System.Windows.Forms.TextBox();
            this.textboxPass = new System.Windows.Forms.TextBox();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelPass = new System.Windows.Forms.Label();
            this.check_save_login = new System.Windows.Forms.CheckBox();
            this.login_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTimerMsg
            // 
            this.labelTimerMsg.Tick += new System.EventHandler(this.labelTimerMsg_Tick);
            // 
            // TaskbarIcon
            // 
            this.TaskbarIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TaskbarIcon.Icon")));
            this.TaskbarIcon.Text = "Action";
            this.TaskbarIcon.DoubleClick += new System.EventHandler(this.TaskbarIcon_DoubleClick);
            // 
            // login_panel
            // 
            this.login_panel.BackColor = System.Drawing.Color.Transparent;
            this.login_panel.BackgroundImage = global::PrioritySales.Properties.Resources.form33;
            this.login_panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.login_panel.Controls.Add(this.LabelVersion);
            this.login_panel.Controls.Add(this.LabelSetting);
            this.login_panel.Controls.Add(this.LabelExit);
            this.login_panel.Controls.Add(this.LabelHide);
            this.login_panel.Controls.Add(this.LabelMsg);
            this.login_panel.Controls.Add(this.buttonLogin);
            this.login_panel.Controls.Add(this.label1);
            this.login_panel.Controls.Add(this.TextboxLogin);
            this.login_panel.Controls.Add(this.textboxPass);
            this.login_panel.Controls.Add(this.labelLogin);
            this.login_panel.Controls.Add(this.labelPass);
            this.login_panel.Controls.Add(this.check_save_login);
            this.login_panel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.login_panel.Location = new System.Drawing.Point(12, 12);
            this.login_panel.Name = "login_panel";
            this.login_panel.Size = new System.Drawing.Size(359, 245);
            this.login_panel.TabIndex = 5;
            this.login_panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.login_panel_MouseDown);
            this.login_panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.login_panel_MouseMove);
            this.login_panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.login_panel_MouseUp);
            // 
            // LabelVersion
            // 
            this.LabelVersion.AutoSize = true;
            this.LabelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelVersion.ForeColor = System.Drawing.Color.Yellow;
            this.LabelVersion.Location = new System.Drawing.Point(18, 217);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(42, 13);
            this.LabelVersion.TabIndex = 7;
            this.LabelVersion.Text = "Version";
            // 
            // LabelSetting
            // 
            this.LabelSetting.AutoSize = true;
            this.LabelSetting.ForeColor = System.Drawing.Color.Yellow;
            this.LabelSetting.Location = new System.Drawing.Point(295, 11);
            this.LabelSetting.Name = "LabelSetting";
            this.LabelSetting.Size = new System.Drawing.Size(13, 13);
            this.LabelSetting.TabIndex = 6;
            this.LabelSetting.Text = "н";
            this.LabelSetting.DoubleClick += new System.EventHandler(this.LabelSetting_DoubleClick);
            // 
            // LabelExit
            // 
            this.LabelExit.AutoSize = true;
            this.LabelExit.ForeColor = System.Drawing.Color.Yellow;
            this.LabelExit.Location = new System.Drawing.Point(335, 11);
            this.LabelExit.Name = "LabelExit";
            this.LabelExit.Size = new System.Drawing.Size(12, 13);
            this.LabelExit.TabIndex = 0;
            this.LabelExit.Text = "x";
            this.LabelExit.DoubleClick += new System.EventHandler(this.LabelExit_DoubleClick);
            // 
            // LabelHide
            // 
            this.LabelHide.AutoSize = true;
            this.LabelHide.ForeColor = System.Drawing.Color.Yellow;
            this.LabelHide.Location = new System.Drawing.Point(316, 11);
            this.LabelHide.Name = "LabelHide";
            this.LabelHide.Size = new System.Drawing.Size(13, 13);
            this.LabelHide.TabIndex = 5;
            this.LabelHide.Text = "с";
            this.LabelHide.DoubleClick += new System.EventHandler(this.LabelHide_DoubleClick);
            // 
            // LabelMsg
            // 
            this.LabelMsg.AutoSize = true;
            this.LabelMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelMsg.ForeColor = System.Drawing.Color.LimeGreen;
            this.LabelMsg.Location = new System.Drawing.Point(65, 210);
            this.LabelMsg.Name = "LabelMsg";
            this.LabelMsg.Size = new System.Drawing.Size(0, 20);
            this.LabelMsg.TabIndex = 4;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLogin.ForeColor = System.Drawing.Color.DodgerBlue;
            this.buttonLogin.Location = new System.Drawing.Point(109, 187);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(153, 23);
            this.buttonLogin.TabIndex = 3;
            this.buttonLogin.Text = "Войти";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            this.buttonLogin.Enter += new System.EventHandler(this.login_button_Enter);
            this.buttonLogin.Leave += new System.EventHandler(this.login_button_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Mistral", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(132, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "ACTION";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label1_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1_MouseUp);
            // 
            // TextboxLogin
            // 
            this.TextboxLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextboxLogin.Location = new System.Drawing.Point(91, 73);
            this.TextboxLogin.MaxLength = 10;
            this.TextboxLogin.Name = "TextboxLogin";
            this.TextboxLogin.Size = new System.Drawing.Size(196, 22);
            this.TextboxLogin.TabIndex = 0;
            this.TextboxLogin.Enter += new System.EventHandler(this.login_textbox_Enter);
            this.TextboxLogin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.login_textbox_KeyDown);
            this.TextboxLogin.Leave += new System.EventHandler(this.login_textbox_Leave);
            // 
            // textboxPass
            // 
            this.textboxPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textboxPass.Location = new System.Drawing.Point(91, 113);
            this.textboxPass.MaxLength = 15;
            this.textboxPass.Name = "textboxPass";
            this.textboxPass.PasswordChar = '*';
            this.textboxPass.Size = new System.Drawing.Size(196, 22);
            this.textboxPass.TabIndex = 1;
            this.textboxPass.Enter += new System.EventHandler(this.pass_textbox_Enter);
            this.textboxPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pass_textbox_KeyDown);
            this.textboxPass.Leave += new System.EventHandler(this.pass_textbox_Leave);
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.BackColor = System.Drawing.Color.Transparent;
            this.labelLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLogin.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelLogin.Location = new System.Drawing.Point(30, 77);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(55, 18);
            this.labelLogin.TabIndex = 0;
            this.labelLogin.Text = "Логин";
            // 
            // labelPass
            // 
            this.labelPass.AutoSize = true;
            this.labelPass.BackColor = System.Drawing.Color.Transparent;
            this.labelPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPass.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelPass.Location = new System.Drawing.Point(18, 113);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(67, 18);
            this.labelPass.TabIndex = 0;
            this.labelPass.Text = "Пароль";
            // 
            // check_save_login
            // 
            this.check_save_login.AutoSize = true;
            this.check_save_login.BackColor = System.Drawing.Color.Transparent;
            this.check_save_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.check_save_login.ForeColor = System.Drawing.Color.DodgerBlue;
            this.check_save_login.Location = new System.Drawing.Point(146, 155);
            this.check_save_login.Name = "check_save_login";
            this.check_save_login.Size = new System.Drawing.Size(92, 19);
            this.check_save_login.TabIndex = 2;
            this.check_save_login.Text = "Запомнить";
            this.check_save_login.UseVisualStyleBackColor = false;
            this.check_save_login.Visible = false;
            this.check_save_login.Enter += new System.EventHandler(this.check_save_login_Enter);
            this.check_save_login.KeyDown += new System.Windows.Forms.KeyEventHandler(this.check_save_login_KeyDown);
            this.check_save_login.Leave += new System.EventHandler(this.check_save_login_Leave);
            // 
            // AuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.login_panel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuthForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.Color.Navy;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AuthForm_FormClosed);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AuthForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AuthForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AuthForm_MouseUp);
            this.login_panel.ResumeLayout(false);
            this.login_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel login_panel;
        private System.Windows.Forms.TextBox TextboxLogin;
        public System.Windows.Forms.TextBox textboxPass;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.Timer labelTimerMsg;
        public System.Windows.Forms.Label LabelMsg;
        private System.Windows.Forms.CheckBox check_save_login;
        private System.Windows.Forms.Label LabelHide;
        private System.Windows.Forms.Label LabelExit;
        private System.Windows.Forms.Label LabelSetting;
        private System.Windows.Forms.Label LabelVersion;
        public System.Windows.Forms.Button buttonLogin;
        public System.Windows.Forms.NotifyIcon TaskbarIcon;
    }
}