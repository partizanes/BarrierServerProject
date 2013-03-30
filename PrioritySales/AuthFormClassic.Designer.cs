namespace PrioritySales
{
    partial class AuthFormClassic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthFormClassic));
            this.PassTextBox = new System.Windows.Forms.TextBox();
            this.FormLoadTimer = new System.Windows.Forms.Timer(this.components);
            this.LabelAction = new System.Windows.Forms.Label();
            this.BackPanelUserText = new System.Windows.Forms.Panel();
            this.LabelUserText = new System.Windows.Forms.TextBox();
            this.ButtonSend = new System.Windows.Forms.Button();
            this.labelB = new System.Windows.Forms.Label();
            this.labelC = new System.Windows.Forms.Label();
            this.labelD = new System.Windows.Forms.Label();
            this.labelE = new System.Windows.Forms.Label();
            this.labelF = new System.Windows.Forms.Label();
            this.AuthPanelMain = new System.Windows.Forms.Panel();
            this.LabelExit = new System.Windows.Forms.Label();
            this.BackPanelPassText = new System.Windows.Forms.Panel();
            this.labelG = new System.Windows.Forms.Label();
            this.labelI = new System.Windows.Forms.Label();
            this.labelJ = new System.Windows.Forms.Label();
            this.labelK = new System.Windows.Forms.Label();
            this.labelL = new System.Windows.Forms.Label();
            this.BackPanelUserText.SuspendLayout();
            this.AuthPanelMain.SuspendLayout();
            this.BackPanelPassText.SuspendLayout();
            this.SuspendLayout();
            // 
            // PassTextBox
            // 
            this.PassTextBox.BackColor = System.Drawing.Color.Black;
            this.PassTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PassTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PassTextBox.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassTextBox.ForeColor = System.Drawing.Color.Gray;
            this.PassTextBox.Location = new System.Drawing.Point(1, 1);
            this.PassTextBox.Name = "PassTextBox";
            this.PassTextBox.PasswordChar = '*';
            this.PassTextBox.ShortcutsEnabled = false;
            this.PassTextBox.Size = new System.Drawing.Size(222, 23);
            this.PassTextBox.TabIndex = 0;
            this.PassTextBox.TabStop = false;
            this.PassTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PassTextBox.UseSystemPasswordChar = true;
            this.PassTextBox.WordWrap = false;
            this.PassTextBox.Enter += new System.EventHandler(this.PassTextBox_Enter);
            this.PassTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PassTextBox_KeyDown);
            this.PassTextBox.Leave += new System.EventHandler(this.PassTextBox_Leave);
            // 
            // FormLoadTimer
            // 
            this.FormLoadTimer.Enabled = true;
            this.FormLoadTimer.Interval = 3333;
            // 
            // LabelAction
            // 
            this.LabelAction.AutoSize = true;
            this.LabelAction.BackColor = System.Drawing.Color.Black;
            this.LabelAction.Font = new System.Drawing.Font("Mistral", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelAction.ForeColor = System.Drawing.Color.LightYellow;
            this.LabelAction.Location = new System.Drawing.Point(30, 7);
            this.LabelAction.Name = "LabelAction";
            this.LabelAction.Size = new System.Drawing.Size(246, 33);
            this.LabelAction.TabIndex = 0;
            this.LabelAction.Text = "ОЧЕРЕДНОСТЬ ПРОДАЖ";
            this.LabelAction.Visible = false;
            this.LabelAction.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LabelAction_MouseDown);
            this.LabelAction.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LabelAction_MouseMove);
            this.LabelAction.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LabelAction_MouseUp);
            // 
            // BackPanelUserText
            // 
            this.BackPanelUserText.BackColor = System.Drawing.Color.DodgerBlue;
            this.BackPanelUserText.Controls.Add(this.LabelUserText);
            this.BackPanelUserText.ForeColor = System.Drawing.Color.Coral;
            this.BackPanelUserText.Location = new System.Drawing.Point(35, 46);
            this.BackPanelUserText.Name = "BackPanelUserText";
            this.BackPanelUserText.Size = new System.Drawing.Size(224, 25);
            this.BackPanelUserText.TabIndex = 3;
            // 
            // LabelUserText
            // 
            this.LabelUserText.BackColor = System.Drawing.Color.Black;
            this.LabelUserText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LabelUserText.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelUserText.ForeColor = System.Drawing.Color.Gray;
            this.LabelUserText.Location = new System.Drawing.Point(1, 1);
            this.LabelUserText.Name = "LabelUserText";
            this.LabelUserText.Size = new System.Drawing.Size(222, 23);
            this.LabelUserText.TabIndex = 0;
            this.LabelUserText.TabStop = false;
            this.LabelUserText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.LabelUserText.Enter += new System.EventHandler(this.LabelUserText_Enter);
            this.LabelUserText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LabelUserText_KeyDown);
            this.LabelUserText.Leave += new System.EventHandler(this.LabelUserText_Leave);
            // 
            // ButtonSend
            // 
            this.ButtonSend.BackColor = System.Drawing.Color.Transparent;
            this.ButtonSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonSend.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonSend.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonSend.Location = new System.Drawing.Point(69, 128);
            this.ButtonSend.Name = "ButtonSend";
            this.ButtonSend.Size = new System.Drawing.Size(161, 29);
            this.ButtonSend.TabIndex = 0;
            this.ButtonSend.TabStop = false;
            this.ButtonSend.Text = "Авторизация";
            this.ButtonSend.UseVisualStyleBackColor = false;
            this.ButtonSend.Enter += new System.EventHandler(this.ButtonSend_Enter);
            this.ButtonSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButtonSend_KeyDown);
            this.ButtonSend.Leave += new System.EventHandler(this.ButtonSend_Leave);
            this.ButtonSend.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ButtonSend_PreviewKeyDown);
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelB.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelB.Location = new System.Drawing.Point(25, 146);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(23, 31);
            this.labelB.TabIndex = 1;
            this.labelB.Text = ".";
            this.labelB.Visible = false;
            // 
            // labelC
            // 
            this.labelC.AutoSize = true;
            this.labelC.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelC.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelC.Location = new System.Drawing.Point(50, 146);
            this.labelC.Name = "labelC";
            this.labelC.Size = new System.Drawing.Size(29, 31);
            this.labelC.TabIndex = 11;
            this.labelC.Text = ". ";
            this.labelC.Visible = false;
            // 
            // labelD
            // 
            this.labelD.AutoSize = true;
            this.labelD.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelD.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelD.Location = new System.Drawing.Point(77, 146);
            this.labelD.Name = "labelD";
            this.labelD.Size = new System.Drawing.Size(29, 31);
            this.labelD.TabIndex = 11;
            this.labelD.Text = ". ";
            this.labelD.Visible = false;
            // 
            // labelE
            // 
            this.labelE.AutoSize = true;
            this.labelE.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelE.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelE.Location = new System.Drawing.Point(102, 146);
            this.labelE.Name = "labelE";
            this.labelE.Size = new System.Drawing.Size(29, 31);
            this.labelE.TabIndex = 11;
            this.labelE.Text = ". ";
            this.labelE.Visible = false;
            // 
            // labelF
            // 
            this.labelF.AutoSize = true;
            this.labelF.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelF.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelF.Location = new System.Drawing.Point(126, 146);
            this.labelF.Name = "labelF";
            this.labelF.Size = new System.Drawing.Size(29, 31);
            this.labelF.TabIndex = 14;
            this.labelF.Text = ". ";
            this.labelF.Visible = false;
            // 
            // AuthPanelMain
            // 
            this.AuthPanelMain.BackColor = System.Drawing.Color.Black;
            this.AuthPanelMain.Controls.Add(this.LabelExit);
            this.AuthPanelMain.Controls.Add(this.ButtonSend);
            this.AuthPanelMain.Controls.Add(this.BackPanelPassText);
            this.AuthPanelMain.Controls.Add(this.BackPanelUserText);
            this.AuthPanelMain.Controls.Add(this.LabelAction);
            this.AuthPanelMain.Controls.Add(this.labelC);
            this.AuthPanelMain.Controls.Add(this.labelD);
            this.AuthPanelMain.Controls.Add(this.labelE);
            this.AuthPanelMain.Controls.Add(this.labelF);
            this.AuthPanelMain.Controls.Add(this.labelG);
            this.AuthPanelMain.Controls.Add(this.labelI);
            this.AuthPanelMain.Controls.Add(this.labelJ);
            this.AuthPanelMain.Controls.Add(this.labelK);
            this.AuthPanelMain.Controls.Add(this.labelL);
            this.AuthPanelMain.Controls.Add(this.labelB);
            this.AuthPanelMain.Location = new System.Drawing.Point(1, 1);
            this.AuthPanelMain.Name = "AuthPanelMain";
            this.AuthPanelMain.Size = new System.Drawing.Size(303, 182);
            this.AuthPanelMain.TabIndex = 0;
            this.AuthPanelMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AuthPanelMain_MouseDown);
            this.AuthPanelMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AuthPanelMain_MouseMove);
            this.AuthPanelMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AuthPanelMain_MouseUp);
            // 
            // LabelExit
            // 
            this.LabelExit.AutoSize = true;
            this.LabelExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelExit.ForeColor = System.Drawing.Color.SlateGray;
            this.LabelExit.Location = new System.Drawing.Point(281, 8);
            this.LabelExit.Name = "LabelExit";
            this.LabelExit.Size = new System.Drawing.Size(17, 13);
            this.LabelExit.TabIndex = 9;
            this.LabelExit.Text = "X ";
            this.LabelExit.Click += new System.EventHandler(this.LabelExit_Click);
            // 
            // BackPanelPassText
            // 
            this.BackPanelPassText.BackColor = System.Drawing.Color.DodgerBlue;
            this.BackPanelPassText.Controls.Add(this.PassTextBox);
            this.BackPanelPassText.ForeColor = System.Drawing.Color.Coral;
            this.BackPanelPassText.Location = new System.Drawing.Point(34, 86);
            this.BackPanelPassText.Name = "BackPanelPassText";
            this.BackPanelPassText.Size = new System.Drawing.Size(224, 25);
            this.BackPanelPassText.TabIndex = 1;
            // 
            // labelG
            // 
            this.labelG.AutoSize = true;
            this.labelG.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelG.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelG.Location = new System.Drawing.Point(150, 146);
            this.labelG.Name = "labelG";
            this.labelG.Size = new System.Drawing.Size(29, 31);
            this.labelG.TabIndex = 2;
            this.labelG.Text = ". ";
            this.labelG.Visible = false;
            // 
            // labelI
            // 
            this.labelI.AutoSize = true;
            this.labelI.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelI.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelI.Location = new System.Drawing.Point(177, 146);
            this.labelI.Name = "labelI";
            this.labelI.Size = new System.Drawing.Size(29, 31);
            this.labelI.TabIndex = 4;
            this.labelI.Text = ". ";
            this.labelI.Visible = false;
            // 
            // labelJ
            // 
            this.labelJ.AutoSize = true;
            this.labelJ.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelJ.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelJ.Location = new System.Drawing.Point(202, 146);
            this.labelJ.Name = "labelJ";
            this.labelJ.Size = new System.Drawing.Size(29, 31);
            this.labelJ.TabIndex = 1;
            this.labelJ.Text = ". ";
            this.labelJ.Visible = false;
            // 
            // labelK
            // 
            this.labelK.AutoSize = true;
            this.labelK.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelK.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelK.Location = new System.Drawing.Point(229, 146);
            this.labelK.Name = "labelK";
            this.labelK.Size = new System.Drawing.Size(29, 31);
            this.labelK.TabIndex = 2;
            this.labelK.Text = ". ";
            this.labelK.Visible = false;
            // 
            // labelL
            // 
            this.labelL.AutoSize = true;
            this.labelL.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelL.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelL.Location = new System.Drawing.Point(254, 146);
            this.labelL.Name = "labelL";
            this.labelL.Size = new System.Drawing.Size(29, 31);
            this.labelL.TabIndex = 6;
            this.labelL.Text = ". ";
            this.labelL.Visible = false;
            // 
            // AuthFormClassic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(306, 184);
            this.Controls.Add(this.AuthPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AuthFormClassic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Очередность Продаж";
            this.Shown += new System.EventHandler(this.AuthFormClassic_Shown);
            this.BackPanelUserText.ResumeLayout(false);
            this.BackPanelUserText.PerformLayout();
            this.AuthPanelMain.ResumeLayout(false);
            this.AuthPanelMain.PerformLayout();
            this.BackPanelPassText.ResumeLayout(false);
            this.BackPanelPassText.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer FormLoadTimer;
        private System.Windows.Forms.Label LabelAction;
        private System.Windows.Forms.Panel BackPanelUserText;
        private System.Windows.Forms.TextBox LabelUserText;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.Label labelC;
        private System.Windows.Forms.Label labelD;
        private System.Windows.Forms.Label labelE;
        private System.Windows.Forms.Label labelF;
        private System.Windows.Forms.Panel AuthPanelMain;
        private System.Windows.Forms.Label labelG;
        private System.Windows.Forms.Label labelI;
        private System.Windows.Forms.Label labelJ;
        private System.Windows.Forms.Label labelK;
        private System.Windows.Forms.Label labelL;
        private System.Windows.Forms.Label LabelExit;
        private System.Windows.Forms.Panel BackPanelPassText;
        public System.Windows.Forms.Button ButtonSend;
        public System.Windows.Forms.TextBox PassTextBox;
    }
}