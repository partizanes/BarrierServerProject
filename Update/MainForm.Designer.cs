namespace Update
{
    partial class Update
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Update));
            this.ProgressBarMainForm = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.PanelMainForm = new System.Windows.Forms.Panel();
            this.PanelListBox = new System.Windows.Forms.Panel();
            this.MainListBox = new System.Windows.Forms.ListBox();
            this.PanelMainForm.SuspendLayout();
            this.PanelListBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgressBarMainForm
            // 
            this.ProgressBarMainForm.Location = new System.Drawing.Point(12, 216);
            this.ProgressBarMainForm.Name = "ProgressBarMainForm";
            this.ProgressBarMainForm.Size = new System.Drawing.Size(483, 18);
            this.ProgressBarMainForm.TabIndex = 1;
            // 
            // PanelMainForm
            // 
            this.PanelMainForm.BackColor = System.Drawing.Color.Black;
            this.PanelMainForm.Controls.Add(this.PanelListBox);
            this.PanelMainForm.Controls.Add(this.ProgressBarMainForm);
            this.PanelMainForm.Location = new System.Drawing.Point(1, 1);
            this.PanelMainForm.Name = "PanelMainForm";
            this.PanelMainForm.Size = new System.Drawing.Size(508, 245);
            this.PanelMainForm.TabIndex = 2;
            // 
            // PanelListBox
            // 
            this.PanelListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.PanelListBox.Controls.Add(this.MainListBox);
            this.PanelListBox.Location = new System.Drawing.Point(11, 11);
            this.PanelListBox.Name = "PanelListBox";
            this.PanelListBox.Size = new System.Drawing.Size(484, 197);
            this.PanelListBox.TabIndex = 4;
            // 
            // MainListBox
            // 
            this.MainListBox.BackColor = System.Drawing.Color.Black;
            this.MainListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MainListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainListBox.ForeColor = System.Drawing.Color.DodgerBlue;
            this.MainListBox.FormattingEnabled = true;
            this.MainListBox.ItemHeight = 15;
            this.MainListBox.Location = new System.Drawing.Point(1, 1);
            this.MainListBox.Name = "MainListBox";
            this.MainListBox.Size = new System.Drawing.Size(482, 195);
            this.MainListBox.TabIndex = 0;
            // 
            // Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(510, 247);
            this.ControlBox = false;
            this.Controls.Add(this.PanelMainForm);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Update";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Утилита обновления";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Update_Shown);
            this.PanelMainForm.ResumeLayout(false);
            this.PanelListBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressBarMainForm;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel PanelMainForm;
        private System.Windows.Forms.Panel PanelListBox;
        public System.Windows.Forms.ListBox MainListBox;
    }
}

