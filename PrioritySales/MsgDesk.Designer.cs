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
            this.PanelEditMsgBack = new System.Windows.Forms.Panel();
            this.PanelEditMsgMain = new System.Windows.Forms.Panel();
            this.TextboxMsgEdited = new System.Windows.Forms.TextBox();
            this.LabelMsgId = new System.Windows.Forms.Label();
            this.LabelPriority = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LabelMsgIdEdited = new System.Windows.Forms.Label();
            this.ButtonAcceptChanges = new System.Windows.Forms.Button();
            this.ButtonCancelChanges = new System.Windows.Forms.Button();
            this.TextBoxEditedPriority = new System.Windows.Forms.TextBox();
            this.MenuMsgBoard.SuspendLayout();
            this.PanelEditMsgBack.SuspendLayout();
            this.PanelEditMsgMain.SuspendLayout();
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
            this.MenuMsgBoard.Size = new System.Drawing.Size(130, 48);
            // 
            // РедактироватьToolStripMenuItem
            // 
            this.РедактироватьToolStripMenuItem.ForeColor = System.Drawing.Color.DodgerBlue;
            this.РедактироватьToolStripMenuItem.Name = "РедактироватьToolStripMenuItem";
            this.РедактироватьToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.РедактироватьToolStripMenuItem.Text = "Редактировать";
            this.РедактироватьToolStripMenuItem.Click += new System.EventHandler(this.РедактироватьToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.ForeColor = System.Drawing.Color.DodgerBlue;
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // PanelEditMsgBack
            // 
            this.PanelEditMsgBack.Controls.Add(this.PanelEditMsgMain);
            this.PanelEditMsgBack.Location = new System.Drawing.Point(75, 25);
            this.PanelEditMsgBack.Name = "PanelEditMsgBack";
            this.PanelEditMsgBack.Size = new System.Drawing.Size(486, 287);
            this.PanelEditMsgBack.TabIndex = 0;
            this.PanelEditMsgBack.Visible = false;
            // 
            // PanelEditMsgMain
            // 
            this.PanelEditMsgMain.BackColor = System.Drawing.Color.Black;
            this.PanelEditMsgMain.Controls.Add(this.TextBoxEditedPriority);
            this.PanelEditMsgMain.Controls.Add(this.ButtonCancelChanges);
            this.PanelEditMsgMain.Controls.Add(this.ButtonAcceptChanges);
            this.PanelEditMsgMain.Controls.Add(this.LabelMsgIdEdited);
            this.PanelEditMsgMain.Controls.Add(this.label2);
            this.PanelEditMsgMain.Controls.Add(this.LabelPriority);
            this.PanelEditMsgMain.Controls.Add(this.LabelMsgId);
            this.PanelEditMsgMain.Controls.Add(this.TextboxMsgEdited);
            this.PanelEditMsgMain.Location = new System.Drawing.Point(1, 1);
            this.PanelEditMsgMain.Name = "PanelEditMsgMain";
            this.PanelEditMsgMain.Size = new System.Drawing.Size(483, 284);
            this.PanelEditMsgMain.TabIndex = 0;
            // 
            // TextboxMsgEdited
            // 
            this.TextboxMsgEdited.BackColor = System.Drawing.Color.Black;
            this.TextboxMsgEdited.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextboxMsgEdited.ForeColor = System.Drawing.Color.DodgerBlue;
            this.TextboxMsgEdited.Location = new System.Drawing.Point(21, 95);
            this.TextboxMsgEdited.Multiline = true;
            this.TextboxMsgEdited.Name = "TextboxMsgEdited";
            this.TextboxMsgEdited.Size = new System.Drawing.Size(447, 148);
            this.TextboxMsgEdited.TabIndex = 1;
            this.TextboxMsgEdited.Text = "Загрузка...";
            this.TextboxMsgEdited.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextboxMsgEdited_KeyDown);
            // 
            // LabelMsgId
            // 
            this.LabelMsgId.AutoSize = true;
            this.LabelMsgId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelMsgId.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LabelMsgId.Location = new System.Drawing.Point(18, 20);
            this.LabelMsgId.Name = "LabelMsgId";
            this.LabelMsgId.Size = new System.Drawing.Size(125, 16);
            this.LabelMsgId.TabIndex = 6;
            this.LabelMsgId.Text = "Номер cообщения";
            // 
            // LabelPriority
            // 
            this.LabelPriority.AutoSize = true;
            this.LabelPriority.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelPriority.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LabelPriority.Location = new System.Drawing.Point(18, 76);
            this.LabelPriority.Name = "LabelPriority";
            this.LabelPriority.Size = new System.Drawing.Size(82, 16);
            this.LabelPriority.TabIndex = 9;
            this.LabelPriority.Text = "Сообщение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(277, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Приоритет";
            // 
            // LabelMsgIdEdited
            // 
            this.LabelMsgIdEdited.AutoSize = true;
            this.LabelMsgIdEdited.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelMsgIdEdited.ForeColor = System.Drawing.Color.ForestGreen;
            this.LabelMsgIdEdited.Location = new System.Drawing.Point(149, 20);
            this.LabelMsgIdEdited.Name = "LabelMsgIdEdited";
            this.LabelMsgIdEdited.Size = new System.Drawing.Size(79, 16);
            this.LabelMsgIdEdited.TabIndex = 12;
            this.LabelMsgIdEdited.Text = "Загрузка...";
            // 
            // ButtonAcceptChanges
            // 
            this.ButtonAcceptChanges.BackColor = System.Drawing.Color.Transparent;
            this.ButtonAcceptChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonAcceptChanges.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonAcceptChanges.Location = new System.Drawing.Point(102, 249);
            this.ButtonAcceptChanges.Name = "ButtonAcceptChanges";
            this.ButtonAcceptChanges.Size = new System.Drawing.Size(130, 23);
            this.ButtonAcceptChanges.TabIndex = 2;
            this.ButtonAcceptChanges.Text = "Подтвердить";
            this.ButtonAcceptChanges.UseVisualStyleBackColor = false;
            this.ButtonAcceptChanges.Click += new System.EventHandler(this.ButtonAcceptChanges_Click);
            this.ButtonAcceptChanges.Enter += new System.EventHandler(this.ButtonAcceptChanges_Enter);
            this.ButtonAcceptChanges.Leave += new System.EventHandler(this.ButtonAcceptChanges_Leave);
            // 
            // ButtonCancelChanges
            // 
            this.ButtonCancelChanges.BackColor = System.Drawing.Color.Transparent;
            this.ButtonCancelChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonCancelChanges.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonCancelChanges.Location = new System.Drawing.Point(249, 249);
            this.ButtonCancelChanges.Name = "ButtonCancelChanges";
            this.ButtonCancelChanges.Size = new System.Drawing.Size(130, 23);
            this.ButtonCancelChanges.TabIndex = 3;
            this.ButtonCancelChanges.Text = "Отмена";
            this.ButtonCancelChanges.UseVisualStyleBackColor = false;
            this.ButtonCancelChanges.Click += new System.EventHandler(this.ButtonCancelChanges_Click);
            this.ButtonCancelChanges.Enter += new System.EventHandler(this.ButtonCancelChanges_Enter);
            this.ButtonCancelChanges.Leave += new System.EventHandler(this.ButtonCancelChanges_Leave);
            // 
            // TextBoxEditedPriority
            // 
            this.TextBoxEditedPriority.BackColor = System.Drawing.Color.Black;
            this.TextBoxEditedPriority.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBoxEditedPriority.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxEditedPriority.ForeColor = System.Drawing.Color.ForestGreen;
            this.TextBoxEditedPriority.Location = new System.Drawing.Point(363, 20);
            this.TextBoxEditedPriority.MaxLength = 1;
            this.TextBoxEditedPriority.Name = "TextBoxEditedPriority";
            this.TextBoxEditedPriority.ShortcutsEnabled = false;
            this.TextBoxEditedPriority.Size = new System.Drawing.Size(32, 15);
            this.TextBoxEditedPriority.TabIndex = 0;
            this.TextBoxEditedPriority.TabStop = false;
            this.TextBoxEditedPriority.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBoxEditedPriority.WordWrap = false;
            this.TextBoxEditedPriority.TextChanged += new System.EventHandler(this.TextBoxEditedPriority_TextChanged);
            this.TextBoxEditedPriority.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxEditedPriority_KeyDown);
            // 
            // MsgDesk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Controls.Add(this.PanelEditMsgBack);
            this.Controls.Add(this.ListViewMsg);
            this.Controls.Add(this.TextBoxMessage);
            this.DoubleBuffered = true;
            this.Name = "MsgDesk";
            this.Size = new System.Drawing.Size(650, 381);
            this.Load += new System.EventHandler(this.MsgDesk_Load);
            this.MenuMsgBoard.ResumeLayout(false);
            this.PanelEditMsgBack.ResumeLayout(false);
            this.PanelEditMsgMain.ResumeLayout(false);
            this.PanelEditMsgMain.PerformLayout();
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
        private System.Windows.Forms.Panel PanelEditMsgBack;
        private System.Windows.Forms.Panel PanelEditMsgMain;
        private System.Windows.Forms.Label LabelMsgIdEdited;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LabelPriority;
        private System.Windows.Forms.Label LabelMsgId;
        private System.Windows.Forms.TextBox TextboxMsgEdited;
        private System.Windows.Forms.Button ButtonCancelChanges;
        private System.Windows.Forms.Button ButtonAcceptChanges;
        private System.Windows.Forms.TextBox TextBoxEditedPriority;
    }
}
