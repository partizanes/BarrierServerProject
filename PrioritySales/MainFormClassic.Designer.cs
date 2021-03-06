﻿namespace PrioritySales
{
    partial class MainFormClassic
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormClassic));
            this.PanelMainClassic = new System.Windows.Forms.Panel();
            this.PanelBackButton = new System.Windows.Forms.Panel();
            this.PanelButton = new System.Windows.Forms.Panel();
            this.ButtonLog = new System.Windows.Forms.Button();
            this.ButtonExit = new System.Windows.Forms.Button();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ButtonSetting = new System.Windows.Forms.Button();
            this.ButtonList = new System.Windows.Forms.Button();
            this.ButtonHide = new System.Windows.Forms.Button();
            this.ButtonMsg = new System.Windows.Forms.Button();
            this.ButtonTasks = new System.Windows.Forms.Button();
            this.PanelBackInfoBar = new System.Windows.Forms.Panel();
            this.PanelInfoBar = new System.Windows.Forms.Panel();
            this.LabelVersionBd = new System.Windows.Forms.Label();
            this.LabelUserName = new System.Windows.Forms.Label();
            this.PanelBackMain = new System.Windows.Forms.Panel();
            this.PanelMainBlock = new System.Windows.Forms.Panel();
            this.PanelAddBg = new System.Windows.Forms.Panel();
            this.PanelAddTask = new System.Windows.Forms.Panel();
            this.LabelPrice = new System.Windows.Forms.Label();
            this.TextboxPrice = new System.Windows.Forms.TextBox();
            this.LabelInfo = new System.Windows.Forms.Label();
            this.ButtonTurn = new System.Windows.Forms.Button();
            this.msg_label = new System.Windows.Forms.Label();
            this.LabelNameItem = new System.Windows.Forms.Label();
            this.LabelCountAdd = new System.Windows.Forms.Label();
            this.DataTimeLabel = new System.Windows.Forms.Label();
            this.LabelButtonItem = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.TextboxCountAdd = new System.Windows.Forms.TextBox();
            this.TextboxNameItem = new System.Windows.Forms.TextBox();
            this.TextboxAddBar = new System.Windows.Forms.TextBox();
            this.dataGridViewMainForm = new System.Windows.Forms.DataGridView();
            this.u_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kassa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimerClearMsg = new System.Windows.Forms.Timer(this.components);
            this.PrioritySalesIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TimerIconChange = new System.Windows.Forms.Timer(this.components);
            this.MenuStripDataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.подробноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PanelMainClassic.SuspendLayout();
            this.PanelBackButton.SuspendLayout();
            this.PanelButton.SuspendLayout();
            this.PanelBackInfoBar.SuspendLayout();
            this.PanelInfoBar.SuspendLayout();
            this.PanelBackMain.SuspendLayout();
            this.PanelMainBlock.SuspendLayout();
            this.PanelAddBg.SuspendLayout();
            this.PanelAddTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMainForm)).BeginInit();
            this.MenuStripDataGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelMainClassic
            // 
            this.PanelMainClassic.BackColor = System.Drawing.Color.Black;
            this.PanelMainClassic.Controls.Add(this.PanelBackButton);
            this.PanelMainClassic.Controls.Add(this.PanelBackInfoBar);
            this.PanelMainClassic.Controls.Add(this.PanelBackMain);
            this.PanelMainClassic.Location = new System.Drawing.Point(1, 1);
            this.PanelMainClassic.Name = "PanelMainClassic";
            this.PanelMainClassic.Size = new System.Drawing.Size(681, 528);
            this.PanelMainClassic.TabIndex = 0;
            this.PanelMainClassic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelMainClassic_MouseDown);
            this.PanelMainClassic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelMainClassic_MouseMove);
            this.PanelMainClassic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelMainClassic_MouseUp);
            // 
            // PanelBackButton
            // 
            this.PanelBackButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.PanelBackButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelBackButton.Controls.Add(this.PanelButton);
            this.PanelBackButton.Location = new System.Drawing.Point(11, 11);
            this.PanelBackButton.Name = "PanelBackButton";
            this.PanelBackButton.Size = new System.Drawing.Size(659, 54);
            this.PanelBackButton.TabIndex = 26;
            // 
            // PanelButton
            // 
            this.PanelButton.BackColor = System.Drawing.Color.Black;
            this.PanelButton.Controls.Add(this.ButtonLog);
            this.PanelButton.Controls.Add(this.ButtonExit);
            this.PanelButton.Controls.Add(this.ButtonAdd);
            this.PanelButton.Controls.Add(this.ButtonSetting);
            this.PanelButton.Controls.Add(this.ButtonList);
            this.PanelButton.Controls.Add(this.ButtonHide);
            this.PanelButton.Controls.Add(this.ButtonMsg);
            this.PanelButton.Controls.Add(this.ButtonTasks);
            this.PanelButton.Location = new System.Drawing.Point(1, 1);
            this.PanelButton.Name = "PanelButton";
            this.PanelButton.Size = new System.Drawing.Size(655, 50);
            this.PanelButton.TabIndex = 0;
            this.PanelButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelButton_MouseDown);
            this.PanelButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelButton_MouseMove);
            this.PanelButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelButton_MouseUp);
            // 
            // ButtonLog
            // 
            this.ButtonLog.BackColor = System.Drawing.Color.Transparent;
            this.ButtonLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonLog.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonLog.Location = new System.Drawing.Point(331, 13);
            this.ButtonLog.Name = "ButtonLog";
            this.ButtonLog.Size = new System.Drawing.Size(75, 23);
            this.ButtonLog.TabIndex = 5;
            this.ButtonLog.Text = "Лог";
            this.ButtonLog.UseVisualStyleBackColor = false;
            this.ButtonLog.Click += new System.EventHandler(this.ButtonLog_Click);
            this.ButtonLog.Enter += new System.EventHandler(this.ButtonUnk2_Enter);
            this.ButtonLog.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButtonLog_KeyDown);
            this.ButtonLog.Leave += new System.EventHandler(this.button1_Leave);
            // 
            // ButtonExit
            // 
            this.ButtonExit.BackColor = System.Drawing.Color.Transparent;
            this.ButtonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonExit.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonExit.Location = new System.Drawing.Point(574, 13);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(75, 23);
            this.ButtonExit.TabIndex = 8;
            this.ButtonExit.Text = "Выход";
            this.ButtonExit.UseVisualStyleBackColor = false;
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            this.ButtonExit.Enter += new System.EventHandler(this.ButtonExit_Enter);
            this.ButtonExit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButtonExit_KeyDown);
            this.ButtonExit.Leave += new System.EventHandler(this.ButtonExit_Leave);
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.BackColor = System.Drawing.Color.Transparent;
            this.ButtonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonAdd.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonAdd.Location = new System.Drawing.Point(7, 13);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(75, 23);
            this.ButtonAdd.TabIndex = 1;
            this.ButtonAdd.Text = "Добавить";
            this.ButtonAdd.UseVisualStyleBackColor = false;
            this.ButtonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            this.ButtonAdd.Enter += new System.EventHandler(this.ButtonAdd_Enter);
            this.ButtonAdd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButtonAdd_KeyDown);
            this.ButtonAdd.Leave += new System.EventHandler(this.ButtonAdd_Leave);
            this.ButtonAdd.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ButtonAdd_PreviewKeyDown);
            // 
            // ButtonSetting
            // 
            this.ButtonSetting.BackColor = System.Drawing.Color.Transparent;
            this.ButtonSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonSetting.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonSetting.Location = new System.Drawing.Point(412, 13);
            this.ButtonSetting.Name = "ButtonSetting";
            this.ButtonSetting.Size = new System.Drawing.Size(75, 23);
            this.ButtonSetting.TabIndex = 6;
            this.ButtonSetting.Text = "Настройки";
            this.ButtonSetting.UseVisualStyleBackColor = false;
            this.ButtonSetting.Enter += new System.EventHandler(this.ButtonSetting_Enter);
            this.ButtonSetting.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButtonSetting_KeyDown);
            this.ButtonSetting.Leave += new System.EventHandler(this.ButtonSetting_Leave);
            // 
            // ButtonList
            // 
            this.ButtonList.BackColor = System.Drawing.Color.Transparent;
            this.ButtonList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonList.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonList.Location = new System.Drawing.Point(88, 13);
            this.ButtonList.Name = "ButtonList";
            this.ButtonList.Size = new System.Drawing.Size(75, 23);
            this.ButtonList.TabIndex = 2;
            this.ButtonList.Text = "Список";
            this.ButtonList.UseVisualStyleBackColor = false;
            this.ButtonList.Click += new System.EventHandler(this.ButtonList_Click);
            this.ButtonList.Enter += new System.EventHandler(this.ButtonList_Enter);
            this.ButtonList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButtonList_KeyDown);
            this.ButtonList.Leave += new System.EventHandler(this.ButtonList_Leave);
            this.ButtonList.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ButtonList_PreviewKeyDown);
            // 
            // ButtonHide
            // 
            this.ButtonHide.BackColor = System.Drawing.Color.Transparent;
            this.ButtonHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonHide.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonHide.Location = new System.Drawing.Point(493, 13);
            this.ButtonHide.Name = "ButtonHide";
            this.ButtonHide.Size = new System.Drawing.Size(75, 23);
            this.ButtonHide.TabIndex = 7;
            this.ButtonHide.Text = "Свернуть";
            this.ButtonHide.UseVisualStyleBackColor = false;
            this.ButtonHide.Click += new System.EventHandler(this.ButtonHide_Click);
            this.ButtonHide.Enter += new System.EventHandler(this.ButtonHide_Enter);
            this.ButtonHide.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButtonHide_KeyDown);
            this.ButtonHide.Leave += new System.EventHandler(this.ButtonHide_Leave);
            // 
            // ButtonMsg
            // 
            this.ButtonMsg.BackColor = System.Drawing.Color.Transparent;
            this.ButtonMsg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonMsg.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonMsg.Location = new System.Drawing.Point(169, 13);
            this.ButtonMsg.Name = "ButtonMsg";
            this.ButtonMsg.Size = new System.Drawing.Size(75, 23);
            this.ButtonMsg.TabIndex = 3;
            this.ButtonMsg.Text = "Сообщения";
            this.ButtonMsg.UseVisualStyleBackColor = false;
            this.ButtonMsg.Click += new System.EventHandler(this.ButtonMsg_Click);
            this.ButtonMsg.Enter += new System.EventHandler(this.ButtonMsg_Enter);
            this.ButtonMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButtonMsg_KeyDown);
            this.ButtonMsg.Leave += new System.EventHandler(this.ButtonMsg_Leave);
            this.ButtonMsg.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ButtonMsg_PreviewKeyDown);
            // 
            // ButtonTasks
            // 
            this.ButtonTasks.BackColor = System.Drawing.Color.Transparent;
            this.ButtonTasks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonTasks.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonTasks.Location = new System.Drawing.Point(250, 13);
            this.ButtonTasks.Name = "ButtonTasks";
            this.ButtonTasks.Size = new System.Drawing.Size(75, 23);
            this.ButtonTasks.TabIndex = 4;
            this.ButtonTasks.Text = "Задачи";
            this.ButtonTasks.UseVisualStyleBackColor = false;
            this.ButtonTasks.Click += new System.EventHandler(this.ButtonTasks_Click);
            this.ButtonTasks.Enter += new System.EventHandler(this.ButtonTasks_Enter);
            this.ButtonTasks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButtonTasks_KeyDown);
            this.ButtonTasks.Leave += new System.EventHandler(this.ButtonTasks_Leave);
            this.ButtonTasks.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ButtonTasks_PreviewKeyDown);
            // 
            // PanelBackInfoBar
            // 
            this.PanelBackInfoBar.BackColor = System.Drawing.Color.DodgerBlue;
            this.PanelBackInfoBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelBackInfoBar.Controls.Add(this.PanelInfoBar);
            this.PanelBackInfoBar.Location = new System.Drawing.Point(11, 478);
            this.PanelBackInfoBar.Name = "PanelBackInfoBar";
            this.PanelBackInfoBar.Size = new System.Drawing.Size(660, 39);
            this.PanelBackInfoBar.TabIndex = 25;
            // 
            // PanelInfoBar
            // 
            this.PanelInfoBar.BackColor = System.Drawing.Color.Black;
            this.PanelInfoBar.Controls.Add(this.LabelVersionBd);
            this.PanelInfoBar.Controls.Add(this.LabelUserName);
            this.PanelInfoBar.Location = new System.Drawing.Point(1, 1);
            this.PanelInfoBar.Name = "PanelInfoBar";
            this.PanelInfoBar.Size = new System.Drawing.Size(656, 35);
            this.PanelInfoBar.TabIndex = 0;
            this.PanelInfoBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelInfoBar_MouseDown);
            this.PanelInfoBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelInfoBar_MouseMove);
            this.PanelInfoBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelInfoBar_MouseUp);
            // 
            // LabelVersionBd
            // 
            this.LabelVersionBd.AutoSize = true;
            this.LabelVersionBd.BackColor = System.Drawing.Color.Transparent;
            this.LabelVersionBd.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelVersionBd.ForeColor = System.Drawing.Color.Green;
            this.LabelVersionBd.Location = new System.Drawing.Point(489, 6);
            this.LabelVersionBd.Name = "LabelVersionBd";
            this.LabelVersionBd.Size = new System.Drawing.Size(29, 19);
            this.LabelVersionBd.TabIndex = 0;
            this.LabelVersionBd.Text = "Бд:";
            this.LabelVersionBd.Visible = false;
            // 
            // LabelUserName
            // 
            this.LabelUserName.AutoSize = true;
            this.LabelUserName.BackColor = System.Drawing.Color.Transparent;
            this.LabelUserName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelUserName.ForeColor = System.Drawing.Color.Green;
            this.LabelUserName.Location = new System.Drawing.Point(6, 6);
            this.LabelUserName.Name = "LabelUserName";
            this.LabelUserName.Size = new System.Drawing.Size(112, 19);
            this.LabelUserName.TabIndex = 0;
            this.LabelUserName.Text = "Пользователь:  ";
            this.LabelUserName.TextChanged += new System.EventHandler(this.LabelUserName_TextChanged);
            this.LabelUserName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LabelUserName_MouseDown);
            this.LabelUserName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LabelUserName_MouseMove);
            this.LabelUserName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LabelUserName_MouseUp);
            // 
            // PanelBackMain
            // 
            this.PanelBackMain.BackColor = System.Drawing.Color.DodgerBlue;
            this.PanelBackMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelBackMain.Controls.Add(this.PanelMainBlock);
            this.PanelBackMain.Location = new System.Drawing.Point(11, 73);
            this.PanelBackMain.Name = "PanelBackMain";
            this.PanelBackMain.Size = new System.Drawing.Size(660, 392);
            this.PanelBackMain.TabIndex = 24;
            // 
            // PanelMainBlock
            // 
            this.PanelMainBlock.BackColor = System.Drawing.Color.Black;
            this.PanelMainBlock.Controls.Add(this.PanelAddBg);
            this.PanelMainBlock.Controls.Add(this.dataGridViewMainForm);
            this.PanelMainBlock.Location = new System.Drawing.Point(1, 1);
            this.PanelMainBlock.Name = "PanelMainBlock";
            this.PanelMainBlock.Size = new System.Drawing.Size(656, 388);
            this.PanelMainBlock.TabIndex = 0;
            this.PanelMainBlock.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelMainBlock_MouseDown);
            this.PanelMainBlock.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelMainBlock_MouseMove);
            this.PanelMainBlock.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelMainBlock_MouseUp);
            // 
            // PanelAddBg
            // 
            this.PanelAddBg.BackColor = System.Drawing.Color.DodgerBlue;
            this.PanelAddBg.Controls.Add(this.PanelAddTask);
            this.PanelAddBg.Location = new System.Drawing.Point(653, 44);
            this.PanelAddBg.Name = "PanelAddBg";
            this.PanelAddBg.Size = new System.Drawing.Size(517, 311);
            this.PanelAddBg.TabIndex = 6;
            this.PanelAddBg.Visible = false;
            // 
            // PanelAddTask
            // 
            this.PanelAddTask.BackColor = System.Drawing.Color.Black;
            this.PanelAddTask.Controls.Add(this.LabelPrice);
            this.PanelAddTask.Controls.Add(this.TextboxPrice);
            this.PanelAddTask.Controls.Add(this.LabelInfo);
            this.PanelAddTask.Controls.Add(this.ButtonTurn);
            this.PanelAddTask.Controls.Add(this.msg_label);
            this.PanelAddTask.Controls.Add(this.LabelNameItem);
            this.PanelAddTask.Controls.Add(this.LabelCountAdd);
            this.PanelAddTask.Controls.Add(this.DataTimeLabel);
            this.PanelAddTask.Controls.Add(this.LabelButtonItem);
            this.PanelAddTask.Controls.Add(this.dateTimePicker1);
            this.PanelAddTask.Controls.Add(this.TextboxCountAdd);
            this.PanelAddTask.Controls.Add(this.TextboxNameItem);
            this.PanelAddTask.Controls.Add(this.TextboxAddBar);
            this.PanelAddTask.Location = new System.Drawing.Point(2, 2);
            this.PanelAddTask.Name = "PanelAddTask";
            this.PanelAddTask.Size = new System.Drawing.Size(513, 307);
            this.PanelAddTask.TabIndex = 0;
            this.PanelAddTask.Visible = false;
            // 
            // LabelPrice
            // 
            this.LabelPrice.AutoSize = true;
            this.LabelPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelPrice.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LabelPrice.Location = new System.Drawing.Point(411, 105);
            this.LabelPrice.Name = "LabelPrice";
            this.LabelPrice.Size = new System.Drawing.Size(41, 16);
            this.LabelPrice.TabIndex = 0;
            this.LabelPrice.Text = "Цена";
            // 
            // TextboxPrice
            // 
            this.TextboxPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextboxPrice.Location = new System.Drawing.Point(379, 124);
            this.TextboxPrice.Name = "TextboxPrice";
            this.TextboxPrice.Size = new System.Drawing.Size(105, 35);
            this.TextboxPrice.TabIndex = 0;
            this.TextboxPrice.TabStop = false;
            this.TextboxPrice.Enter += new System.EventHandler(this.TextboxPrice_Enter);
            this.TextboxPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextboxPrice_KeyDown);
            this.TextboxPrice.Leave += new System.EventHandler(this.TextboxPrice_Leave);
            // 
            // LabelInfo
            // 
            this.LabelInfo.AutoSize = true;
            this.LabelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.LabelInfo.ForeColor = System.Drawing.Color.Red;
            this.LabelInfo.Location = new System.Drawing.Point(20, 18);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(0, 16);
            this.LabelInfo.TabIndex = 0;
            // 
            // ButtonTurn
            // 
            this.ButtonTurn.BackColor = System.Drawing.Color.Transparent;
            this.ButtonTurn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonTurn.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonTurn.Location = new System.Drawing.Point(164, 262);
            this.ButtonTurn.Name = "ButtonTurn";
            this.ButtonTurn.Size = new System.Drawing.Size(200, 23);
            this.ButtonTurn.TabIndex = 0;
            this.ButtonTurn.TabStop = false;
            this.ButtonTurn.Text = "Поставить в очередь";
            this.ButtonTurn.UseVisualStyleBackColor = false;
            this.ButtonTurn.Click += new System.EventHandler(this.ButtonTurn_Click);
            this.ButtonTurn.Enter += new System.EventHandler(this.ButtonTurn_Enter);
            this.ButtonTurn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButtonTurn_KeyDown);
            this.ButtonTurn.Leave += new System.EventHandler(this.ButtonTurn_Leave);
            // 
            // msg_label
            // 
            this.msg_label.AutoSize = true;
            this.msg_label.ForeColor = System.Drawing.Color.Red;
            this.msg_label.Location = new System.Drawing.Point(186, 20);
            this.msg_label.Name = "msg_label";
            this.msg_label.Size = new System.Drawing.Size(0, 13);
            this.msg_label.TabIndex = 8;
            // 
            // LabelNameItem
            // 
            this.LabelNameItem.AutoSize = true;
            this.LabelNameItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelNameItem.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LabelNameItem.Location = new System.Drawing.Point(219, 182);
            this.LabelNameItem.Name = "LabelNameItem";
            this.LabelNameItem.Size = new System.Drawing.Size(107, 16);
            this.LabelNameItem.TabIndex = 0;
            this.LabelNameItem.Text = "Наименование";
            // 
            // LabelCountAdd
            // 
            this.LabelCountAdd.AutoSize = true;
            this.LabelCountAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelCountAdd.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LabelCountAdd.Location = new System.Drawing.Point(278, 105);
            this.LabelCountAdd.Name = "LabelCountAdd";
            this.LabelCountAdd.Size = new System.Drawing.Size(86, 16);
            this.LabelCountAdd.TabIndex = 0;
            this.LabelCountAdd.Text = "Количество";
            // 
            // DataTimeLabel
            // 
            this.DataTimeLabel.AutoSize = true;
            this.DataTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DataTimeLabel.ForeColor = System.Drawing.Color.DodgerBlue;
            this.DataTimeLabel.Location = new System.Drawing.Point(239, 44);
            this.DataTimeLabel.Name = "DataTimeLabel";
            this.DataTimeLabel.Size = new System.Drawing.Size(40, 16);
            this.DataTimeLabel.TabIndex = 0;
            this.DataTimeLabel.Text = "Дата";
            // 
            // LabelButtonItem
            // 
            this.LabelButtonItem.AutoSize = true;
            this.LabelButtonItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelButtonItem.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LabelButtonItem.Location = new System.Drawing.Point(137, 103);
            this.LabelButtonItem.Name = "LabelButtonItem";
            this.LabelButtonItem.Size = new System.Drawing.Size(75, 16);
            this.LabelButtonItem.TabIndex = 0;
            this.LabelButtonItem.Text = "Штрих-код";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(164, 63);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.TabStop = false;
            this.dateTimePicker1.Enter += new System.EventHandler(this.dateTimePicker1_Enter);
            this.dateTimePicker1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker1_KeyDown);
            this.dateTimePicker1.Leave += new System.EventHandler(this.dateTimePicker1_Leave);
            // 
            // TextboxCountAdd
            // 
            this.TextboxCountAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextboxCountAdd.Location = new System.Drawing.Point(276, 124);
            this.TextboxCountAdd.Name = "TextboxCountAdd";
            this.TextboxCountAdd.Size = new System.Drawing.Size(88, 35);
            this.TextboxCountAdd.TabIndex = 0;
            this.TextboxCountAdd.TabStop = false;
            this.TextboxCountAdd.Enter += new System.EventHandler(this.TextboxCountAdd_Enter);
            this.TextboxCountAdd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextboxCountAdd_KeyDown);
            this.TextboxCountAdd.Leave += new System.EventHandler(this.TextboxCountAdd_Leave);
            // 
            // TextboxNameItem
            // 
            this.TextboxNameItem.Enabled = false;
            this.TextboxNameItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextboxNameItem.Location = new System.Drawing.Point(23, 201);
            this.TextboxNameItem.MaxLength = 50;
            this.TextboxNameItem.Name = "TextboxNameItem";
            this.TextboxNameItem.ReadOnly = true;
            this.TextboxNameItem.Size = new System.Drawing.Size(461, 26);
            this.TextboxNameItem.TabIndex = 0;
            this.TextboxNameItem.TabStop = false;
            // 
            // TextboxAddBar
            // 
            this.TextboxAddBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextboxAddBar.Location = new System.Drawing.Point(83, 124);
            this.TextboxAddBar.MaxLength = 12;
            this.TextboxAddBar.Name = "TextboxAddBar";
            this.TextboxAddBar.Size = new System.Drawing.Size(178, 35);
            this.TextboxAddBar.TabIndex = 0;
            this.TextboxAddBar.TabStop = false;
            this.TextboxAddBar.Enter += new System.EventHandler(this.TextboxAddBar_Enter);
            this.TextboxAddBar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextboxAddBar_KeyDown);
            this.TextboxAddBar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxAddBar_KeyPress);
            this.TextboxAddBar.Leave += new System.EventHandler(this.TextboxAddBar_Leave);
            // 
            // dataGridViewMainForm
            // 
            this.dataGridViewMainForm.AllowUserToAddRows = false;
            this.dataGridViewMainForm.AllowUserToDeleteRows = false;
            this.dataGridViewMainForm.AllowUserToResizeColumns = false;
            this.dataGridViewMainForm.AllowUserToResizeRows = false;
            this.dataGridViewMainForm.BackgroundColor = System.Drawing.Color.Black;
            this.dataGridViewMainForm.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMainForm.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewMainForm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMainForm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.u_id,
            this.Barcode,
            this.NameItem,
            this.Price,
            this.CountStart,
            this.Count,
            this.Status,
            this.Kassa,
            this.DateCol});
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle19.Format = "N0";
            dataGridViewCellStyle19.NullValue = null;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewMainForm.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridViewMainForm.EnableHeadersVisualStyles = false;
            this.dataGridViewMainForm.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.dataGridViewMainForm.Location = new System.Drawing.Point(2, 1);
            this.dataGridViewMainForm.MultiSelect = false;
            this.dataGridViewMainForm.Name = "dataGridViewMainForm";
            this.dataGridViewMainForm.ReadOnly = true;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.Green;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMainForm.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dataGridViewMainForm.RowHeadersVisible = false;
            this.dataGridViewMainForm.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewMainForm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewMainForm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMainForm.Size = new System.Drawing.Size(653, 386);
            this.dataGridViewMainForm.TabIndex = 0;
            this.dataGridViewMainForm.TabStop = false;
            this.dataGridViewMainForm.Visible = false;
            this.dataGridViewMainForm.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMainForm_CellEnter);
            this.dataGridViewMainForm.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMainForm_CellLeave);
            this.dataGridViewMainForm.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMainForm_RowEnter);
            this.dataGridViewMainForm.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewMainForm_RowsAdded);
            this.dataGridViewMainForm.Enter += new System.EventHandler(this.dataGridViewMainForm_Enter);
            this.dataGridViewMainForm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // u_id
            // 
            this.u_id.Frozen = true;
            this.u_id.HeaderText = "ID";
            this.u_id.Name = "u_id";
            this.u_id.ReadOnly = true;
            this.u_id.Width = 25;
            // 
            // Barcode
            // 
            this.Barcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Barcode.Frozen = true;
            this.Barcode.HeaderText = "Штрихкод";
            this.Barcode.MaxInputLength = 13;
            this.Barcode.Name = "Barcode";
            this.Barcode.ReadOnly = true;
            this.Barcode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Barcode.Width = 85;
            // 
            // NameItem
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.NameItem.DefaultCellStyle = dataGridViewCellStyle17;
            this.NameItem.Frozen = true;
            this.NameItem.HeaderText = "Наименование";
            this.NameItem.Name = "NameItem";
            this.NameItem.ReadOnly = true;
            this.NameItem.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.NameItem.Width = 190;
            // 
            // Price
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Price.DefaultCellStyle = dataGridViewCellStyle18;
            this.Price.Frozen = true;
            this.Price.HeaderText = "Цена";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            this.Price.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Price.Width = 50;
            // 
            // CountStart
            // 
            this.CountStart.Frozen = true;
            this.CountStart.HeaderText = "К-во";
            this.CountStart.Name = "CountStart";
            this.CountStart.ReadOnly = true;
            this.CountStart.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CountStart.Width = 60;
            // 
            // Count
            // 
            this.Count.Frozen = true;
            this.Count.HeaderText = "Продано";
            this.Count.Name = "Count";
            this.Count.ReadOnly = true;
            this.Count.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Count.Width = 55;
            // 
            // Status
            // 
            this.Status.Frozen = true;
            this.Status.HeaderText = "Статус";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Status.Width = 50;
            // 
            // Kassa
            // 
            this.Kassa.Frozen = true;
            this.Kassa.HeaderText = "Касса";
            this.Kassa.Name = "Kassa";
            this.Kassa.ReadOnly = true;
            this.Kassa.Width = 45;
            // 
            // DateCol
            // 
            this.DateCol.HeaderText = "Дата";
            this.DateCol.Name = "DateCol";
            this.DateCol.ReadOnly = true;
            this.DateCol.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DateCol.Width = 89;
            // 
            // TimerClearMsg
            // 
            this.TimerClearMsg.Interval = 3000;
            this.TimerClearMsg.Tick += new System.EventHandler(this.TimerClearMsg_Tick);
            // 
            // PrioritySalesIcon
            // 
            this.PrioritySalesIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("PrioritySalesIcon.Icon")));
            this.PrioritySalesIcon.Text = "Двойной клик для активации приложения.";
            this.PrioritySalesIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PrioritySales_MouseDoubleClick);
            // 
            // TimerIconChange
            // 
            this.TimerIconChange.Interval = 1000;
            this.TimerIconChange.Tick += new System.EventHandler(this.TimerIconChange_Tick);
            // 
            // MenuStripDataGrid
            // 
            this.MenuStripDataGrid.AllowMerge = false;
            this.MenuStripDataGrid.BackColor = System.Drawing.Color.Black;
            this.MenuStripDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.подробноToolStripMenuItem,
            this.копироватьToolStripMenuItem,
            this.редактироватьToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.MenuStripDataGrid.Name = "MenuStripDataGrid";
            this.MenuStripDataGrid.ShowImageMargin = false;
            this.MenuStripDataGrid.ShowItemToolTips = false;
            this.MenuStripDataGrid.Size = new System.Drawing.Size(130, 92);
            // 
            // подробноToolStripMenuItem
            // 
            this.подробноToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.подробноToolStripMenuItem.ForeColor = System.Drawing.Color.DodgerBlue;
            this.подробноToolStripMenuItem.Name = "подробноToolStripMenuItem";
            this.подробноToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.подробноToolStripMenuItem.Text = "Подробно";
            this.подробноToolStripMenuItem.Click += new System.EventHandler(this.подробноToolStripMenuItem_Click);
            // 
            // копироватьToolStripMenuItem
            // 
            this.копироватьToolStripMenuItem.ForeColor = System.Drawing.Color.DodgerBlue;
            this.копироватьToolStripMenuItem.Name = "копироватьToolStripMenuItem";
            this.копироватьToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.копироватьToolStripMenuItem.Text = "Копировать";
            this.копироватьToolStripMenuItem.Click += new System.EventHandler(this.копироватьToolStripMenuItem_Click);
            // 
            // редактироватьToolStripMenuItem
            // 
            this.редактироватьToolStripMenuItem.ForeColor = System.Drawing.Color.DodgerBlue;
            this.редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
            this.редактироватьToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.редактироватьToolStripMenuItem.Text = "Редактировать";
            this.редактироватьToolStripMenuItem.Click += new System.EventHandler(this.редактироватьToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.ForeColor = System.Drawing.Color.DodgerBlue;
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // MainFormClassic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(684, 530);
            this.Controls.Add(this.PanelMainClassic);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFormClassic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrioritySales";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormClassic_FormClosing);
            this.Load += new System.EventHandler(this.MainFormClassic_Load);
            this.Shown += new System.EventHandler(this.MainFormClassic_Shown);
            this.PanelMainClassic.ResumeLayout(false);
            this.PanelBackButton.ResumeLayout(false);
            this.PanelButton.ResumeLayout(false);
            this.PanelBackInfoBar.ResumeLayout(false);
            this.PanelInfoBar.ResumeLayout(false);
            this.PanelInfoBar.PerformLayout();
            this.PanelBackMain.ResumeLayout(false);
            this.PanelMainBlock.ResumeLayout(false);
            this.PanelAddBg.ResumeLayout(false);
            this.PanelAddTask.ResumeLayout(false);
            this.PanelAddTask.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMainForm)).EndInit();
            this.MenuStripDataGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelMainClassic;
        private System.Windows.Forms.Panel PanelBackButton;
        private System.Windows.Forms.Panel PanelBackInfoBar;
        private System.Windows.Forms.Panel PanelBackMain;
        private System.Windows.Forms.Panel PanelMainBlock;
        private System.Windows.Forms.Panel PanelButton;
        private System.Windows.Forms.Button ButtonExit;
        private System.Windows.Forms.Button ButtonAdd;
        private System.Windows.Forms.Button ButtonSetting;
        private System.Windows.Forms.Button ButtonHide;
        public System.Windows.Forms.Button ButtonMsg;
        private System.Windows.Forms.Panel PanelInfoBar;
        public System.Windows.Forms.Label LabelUserName;
        public System.Windows.Forms.Panel PanelAddBg;
        private System.Windows.Forms.Panel PanelAddTask;
        private System.Windows.Forms.Label msg_label;
        private System.Windows.Forms.Label LabelNameItem;
        private System.Windows.Forms.Label LabelCountAdd;
        private System.Windows.Forms.Label DataTimeLabel;
        private System.Windows.Forms.Label LabelButtonItem;
        public System.Windows.Forms.Button ButtonTurn;
        private System.Windows.Forms.Label LabelPrice;
        public System.Windows.Forms.Label LabelInfo;
        public System.Windows.Forms.TextBox TextboxNameItem;
        public System.Windows.Forms.Timer TimerClearMsg;
        public System.Windows.Forms.TextBox TextboxCountAdd;
        public System.Windows.Forms.TextBox TextboxAddBar;
        public System.Windows.Forms.TextBox TextboxPrice;
        public System.Windows.Forms.DataGridView dataGridViewMainForm;
        public System.Windows.Forms.Label LabelVersionBd;
        public System.Windows.Forms.Button ButtonList;
        public System.Windows.Forms.NotifyIcon PrioritySalesIcon;
        public System.Windows.Forms.Timer TimerIconChange;
        public System.Windows.Forms.Button ButtonTasks;
        private System.Windows.Forms.DataGridViewTextBoxColumn u_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kassa;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCol;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ContextMenuStrip MenuStripDataGrid;
        private System.Windows.Forms.ToolStripMenuItem подробноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItem;
        public System.Windows.Forms.Button ButtonLog;



    }
}