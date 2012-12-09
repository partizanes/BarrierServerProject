namespace BalanceModule
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
            this.cas = new AxForCasLib.AxForCas();
            ((System.ComponentModel.ISupportInitialize)(this.cas)).BeginInit();
            this.SuspendLayout();
            // 
            // cas
            // 
            this.cas.Enabled = true;
            this.cas.Location = new System.Drawing.Point(790, 507);
            this.cas.Name = "cas";
            this.cas.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cas.OcxState")));
            this.cas.Size = new System.Drawing.Size(40, 22);
            this.cas.TabIndex = 0;
            this.cas.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(622, 305);
            this.Controls.Add(this.cas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "BalanceModule";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.cas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxForCasLib.AxForCas cas;
    }
}

