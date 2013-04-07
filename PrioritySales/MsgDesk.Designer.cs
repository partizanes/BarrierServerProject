namespace PrioritySales
{
    partial class MsgDesk
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TextBoxMessage = new System.Windows.Forms.TextBox();
            this.ListViewMsg = new System.Windows.Forms.ListView();
            this.x = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MenuMsgBoard = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.РедактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMsgBoard.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBoxMessage
            // 
            this.TextBoxMessage.BackColor = System.Drawing.Color.Black;
            this.TextBoxMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxMessage.ForeColor = System.Drawing.Color.Silver;
            this.TextBoxMessage.Location = new System.Drawing.Point(1, 354);
            this.TextBoxMessage.Name = "TextBoxMessage";
            this.TextBoxMessage.Size = new System.Drawing.Size(648, 26);
            this.TextBoxMessage.TabIndex = 2;
            this.TextBoxMessage.TabStop = false;
            this.TextBoxMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxMessage_KeyDown);
            // 
            // ListViewMsg
            // 
            this.ListViewMsg.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.ListViewMsg.AutoArrange = false;
            this.ListViewMsg.BackColor = System.Drawing.Color.Black;
            this.ListViewMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListViewMsg.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.x});
            this.ListViewMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ListViewMsg.FullRowSelect = true;
            this.ListViewMsg.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ListViewMsg.LabelWrap = false;
            this.ListViewMsg.Location = new System.Drawing.Point(1, 1);
            this.ListViewMsg.MultiSelect = false;
            this.ListViewMsg.Name = "ListViewMsg";
            this.ListViewMsg.ShowGroups = false;
            this.ListViewMsg.ShowItemToolTips = true;
            this.ListViewMsg.Size = new System.Drawing.Size(648, 352);
            this.ListViewMsg.TabIndex = 3;
            this.ListViewMsg.TabStop = false;
            this.ListViewMsg.UseCompatibleStateImageBehavior = false;
            this.ListViewMsg.View = System.Windows.Forms.View.Details;
            this.ListViewMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewMsg_KeyDown);
            // 
            // x
            // 
            this.x.Text = "";
            this.x.Width = 640;
            // 
            // MenuMsgBoard
            // 
            this.MenuMsgBoard.BackColor = System.Drawing.Color.Black;
            this.MenuMsgBoard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.РедактироватьToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.MenuMsgBoard.Name = "MenuMsgBoard";
            this.MenuMsgBoard.ShowImageMargin = false;
            this.MenuMsgBoard.ShowItemToolTips = false;
            this.MenuMsgBoard.Size = new System.Drawing.Size(130, 70);
            // 
            // РедактироватьToolStripMenuItem
            // 
            this.РедактироватьToolStripMenuItem.ForeColor = System.Drawing.Color.DodgerBlue;
            this.РедактироватьToolStripMenuItem.Name = "РедактироватьToolStripMenuItem";
            this.РедактироватьToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.РедактироватьToolStripMenuItem.Text = "Редактировать";
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.ForeColor = System.Drawing.Color.DodgerBlue;
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // MsgDesk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Controls.Add(this.ListViewMsg);
            this.Controls.Add(this.TextBoxMessage);
            this.DoubleBuffered = true;
            this.Name = "MsgDesk";
            this.Size = new System.Drawing.Size(650, 381);
            this.Load += new System.EventHandler(this.MsgDesk_Load);
            this.MenuMsgBoard.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox TextBoxMessage;
        public System.Windows.Forms.ListView ListViewMsg;
        private System.Windows.Forms.ColumnHeader x;
        private System.Windows.Forms.ContextMenuStrip MenuMsgBoard;
        private System.Windows.Forms.ToolStripMenuItem РедактироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
    }
}
