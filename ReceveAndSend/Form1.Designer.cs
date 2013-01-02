namespace ReceveAndSend
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.LabelWhere = new System.Windows.Forms.Label();
            this.LabelFrom = new System.Windows.Forms.Label();
            this.TextBoxWhere = new System.Windows.Forms.TextBox();
            this.TextBoxFrom = new System.Windows.Forms.TextBox();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.cas = new AxForCasLib.AxForCas();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ButtonHide = new System.Windows.Forms.Button();
            this.LabelInfo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.cas)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.progressBar1.Location = new System.Drawing.Point(15, 146);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(190, 24);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 11;
            // 
            // LabelWhere
            // 
            this.LabelWhere.AutoSize = true;
            this.LabelWhere.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LabelWhere.Location = new System.Drawing.Point(15, 51);
            this.LabelWhere.Name = "LabelWhere";
            this.LabelWhere.Size = new System.Drawing.Size(31, 13);
            this.LabelWhere.TabIndex = 10;
            this.LabelWhere.Text = "Куда";
            // 
            // LabelFrom
            // 
            this.LabelFrom.AutoSize = true;
            this.LabelFrom.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LabelFrom.Location = new System.Drawing.Point(12, 9);
            this.LabelFrom.Name = "LabelFrom";
            this.LabelFrom.Size = new System.Drawing.Size(43, 13);
            this.LabelFrom.TabIndex = 9;
            this.LabelFrom.Text = "Откуда";
            // 
            // TextBoxWhere
            // 
            this.TextBoxWhere.BackColor = System.Drawing.Color.Black;
            this.TextBoxWhere.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBoxWhere.ForeColor = System.Drawing.Color.DodgerBlue;
            this.TextBoxWhere.Location = new System.Drawing.Point(15, 67);
            this.TextBoxWhere.Name = "TextBoxWhere";
            this.TextBoxWhere.Size = new System.Drawing.Size(190, 13);
            this.TextBoxWhere.TabIndex = 8;
            this.TextBoxWhere.Text = "192.168.1.178";
            // 
            // TextBoxFrom
            // 
            this.TextBoxFrom.BackColor = System.Drawing.Color.Black;
            this.TextBoxFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBoxFrom.ForeColor = System.Drawing.Color.DodgerBlue;
            this.TextBoxFrom.Location = new System.Drawing.Point(15, 25);
            this.TextBoxFrom.Name = "TextBoxFrom";
            this.TextBoxFrom.Size = new System.Drawing.Size(190, 13);
            this.TextBoxFrom.TabIndex = 7;
            this.TextBoxFrom.Text = "192.168.1.191";
            // 
            // ButtonStart
            // 
            this.ButtonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonStart.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonStart.Location = new System.Drawing.Point(15, 93);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(190, 23);
            this.ButtonStart.TabIndex = 6;
            this.ButtonStart.Text = "Начать";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.button1_Click);
            this.ButtonStart.MouseEnter += new System.EventHandler(this.ButtonStart_MouseEnter);
            this.ButtonStart.MouseLeave += new System.EventHandler(this.ButtonStart_MouseLeave);
            // 
            // cas
            // 
            this.cas.Enabled = true;
            this.cas.Location = new System.Drawing.Point(-2, 185);
            this.cas.Name = "cas";
            this.cas.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cas.OcxState")));
            this.cas.Size = new System.Drawing.Size(10, 10);
            this.cas.TabIndex = 12;
            this.cas.Visible = false;
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.Black;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(1, 1);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(443, 156);
            this.listBox1.TabIndex = 14;
            // 
            // ButtonHide
            // 
            this.ButtonHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonHide.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonHide.Location = new System.Drawing.Point(210, 11);
            this.ButtonHide.Name = "ButtonHide";
            this.ButtonHide.Size = new System.Drawing.Size(17, 159);
            this.ButtonHide.TabIndex = 15;
            this.ButtonHide.Text = ">";
            this.ButtonHide.UseVisualStyleBackColor = true;
            this.ButtonHide.Click += new System.EventHandler(this.ButtonHide_Click);
            this.ButtonHide.MouseEnter += new System.EventHandler(this.ButtonHide_MouseEnter);
            this.ButtonHide.MouseLeave += new System.EventHandler(this.ButtonHide_MouseLeave);
            // 
            // LabelInfo
            // 
            this.LabelInfo.AutoSize = true;
            this.LabelInfo.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LabelInfo.Location = new System.Drawing.Point(25, 125);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(0, 13);
            this.LabelInfo.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Location = new System.Drawing.Point(234, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(446, 159);
            this.panel1.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(686, 174);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.LabelInfo);
            this.Controls.Add(this.ButtonHide);
            this.Controls.Add(this.cas);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.LabelWhere);
            this.Controls.Add(this.LabelFrom);
            this.Controls.Add(this.TextBoxWhere);
            this.Controls.Add(this.TextBoxFrom);
            this.Controls.Add(this.ButtonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReceveAndSend";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.cas)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label LabelWhere;
        private System.Windows.Forms.Label LabelFrom;
        private System.Windows.Forms.TextBox TextBoxWhere;
        private System.Windows.Forms.TextBox TextBoxFrom;
        private System.Windows.Forms.Button ButtonStart;
        private AxForCasLib.AxForCas cas;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button ButtonHide;
        private System.Windows.Forms.Label LabelInfo;
        private System.Windows.Forms.Panel panel1;
    }
}

