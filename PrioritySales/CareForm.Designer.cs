﻿namespace PrioritySales
{
    partial class CareForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.LabelCareForm = new System.Windows.Forms.Label();
            this.TimerChangeColor = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.LabelCareForm);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 34);
            this.panel1.TabIndex = 0;
            this.panel1.DoubleClick += new System.EventHandler(this.panel1_DoubleClick);
            // 
            // LabelCareForm
            // 
            this.LabelCareForm.AutoSize = true;
            this.LabelCareForm.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LabelCareForm.Location = new System.Drawing.Point(31, 9);
            this.LabelCareForm.Name = "LabelCareForm";
            this.LabelCareForm.Size = new System.Drawing.Size(144, 13);
            this.LabelCareForm.TabIndex = 0;
            this.LabelCareForm.Text = "<Необработанные задачи>";
            this.LabelCareForm.DoubleClick += new System.EventHandler(this.LabelCareForm_DoubleClick);
            // 
            // TimerChangeColor
            // 
            this.TimerChangeColor.Enabled = true;
            this.TimerChangeColor.Interval = 1000;
            this.TimerChangeColor.Tick += new System.EventHandler(this.TimerChangeColor_Tick);
            // 
            // CareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(212, 37);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CareForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CareForm";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer TimerChangeColor;
        public System.Windows.Forms.Label LabelCareForm;
    }
}