namespace PrioritySales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormClassic));
            this.PanelMainClassic = new System.Windows.Forms.Panel();
            this.PanelBackButton = new System.Windows.Forms.Panel();
            this.PanelButton = new System.Windows.Forms.Panel();
            this.ButtonUnk2 = new System.Windows.Forms.Button();
            this.ButtonExit = new System.Windows.Forms.Button();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ButtonSetting = new System.Windows.Forms.Button();
            this.ButtonList = new System.Windows.Forms.Button();
            this.ButtonHide = new System.Windows.Forms.Button();
            this.ButtonMsg = new System.Windows.Forms.Button();
            this.ButtonUnk = new System.Windows.Forms.Button();
            this.PanelBackInfoBar = new System.Windows.Forms.Panel();
            this.PanelInfoBar = new System.Windows.Forms.Panel();
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
            this.TimerClearMsg = new System.Windows.Forms.Timer(this.components);
            this.PanelMainClassic.SuspendLayout();
            this.PanelBackButton.SuspendLayout();
            this.PanelButton.SuspendLayout();
            this.PanelBackInfoBar.SuspendLayout();
            this.PanelInfoBar.SuspendLayout();
            this.PanelBackMain.SuspendLayout();
            this.PanelMainBlock.SuspendLayout();
            this.PanelAddBg.SuspendLayout();
            this.PanelAddTask.SuspendLayout();
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
            this.PanelButton.Controls.Add(this.ButtonUnk2);
            this.PanelButton.Controls.Add(this.ButtonExit);
            this.PanelButton.Controls.Add(this.ButtonAdd);
            this.PanelButton.Controls.Add(this.ButtonSetting);
            this.PanelButton.Controls.Add(this.ButtonList);
            this.PanelButton.Controls.Add(this.ButtonHide);
            this.PanelButton.Controls.Add(this.ButtonMsg);
            this.PanelButton.Controls.Add(this.ButtonUnk);
            this.PanelButton.Location = new System.Drawing.Point(1, 1);
            this.PanelButton.Name = "PanelButton";
            this.PanelButton.Size = new System.Drawing.Size(655, 50);
            this.PanelButton.TabIndex = 0;
            this.PanelButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelButton_MouseDown);
            this.PanelButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelButton_MouseMove);
            this.PanelButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelButton_MouseUp);
            // 
            // ButtonUnk2
            // 
            this.ButtonUnk2.BackColor = System.Drawing.Color.Transparent;
            this.ButtonUnk2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonUnk2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonUnk2.Location = new System.Drawing.Point(331, 13);
            this.ButtonUnk2.Name = "ButtonUnk2";
            this.ButtonUnk2.Size = new System.Drawing.Size(75, 23);
            this.ButtonUnk2.TabIndex = 5;
            this.ButtonUnk2.Text = "Unk2";
            this.ButtonUnk2.UseVisualStyleBackColor = false;
            this.ButtonUnk2.Enter += new System.EventHandler(this.button1_Enter);
            this.ButtonUnk2.Leave += new System.EventHandler(this.button1_Leave);
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
            this.ButtonAdd.Leave += new System.EventHandler(this.ButtonAdd_Leave);
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
            this.ButtonList.Enter += new System.EventHandler(this.ButtonList_Enter);
            this.ButtonList.Leave += new System.EventHandler(this.ButtonList_Leave);
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
            this.ButtonHide.Enter += new System.EventHandler(this.ButtonHide_Enter);
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
            this.ButtonMsg.Enter += new System.EventHandler(this.ButtonMsg_Enter);
            this.ButtonMsg.Leave += new System.EventHandler(this.ButtonMsg_Leave);
            // 
            // ButtonUnk
            // 
            this.ButtonUnk.BackColor = System.Drawing.Color.Transparent;
            this.ButtonUnk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonUnk.ForeColor = System.Drawing.Color.DodgerBlue;
            this.ButtonUnk.Location = new System.Drawing.Point(250, 13);
            this.ButtonUnk.Name = "ButtonUnk";
            this.ButtonUnk.Size = new System.Drawing.Size(75, 23);
            this.ButtonUnk.TabIndex = 4;
            this.ButtonUnk.Text = "Unk";
            this.ButtonUnk.UseVisualStyleBackColor = false;
            this.ButtonUnk.Enter += new System.EventHandler(this.ButtonUnk_Enter);
            this.ButtonUnk.Leave += new System.EventHandler(this.ButtonUnk_Leave);
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
            this.PanelInfoBar.Controls.Add(this.LabelUserName);
            this.PanelInfoBar.Location = new System.Drawing.Point(1, 1);
            this.PanelInfoBar.Name = "PanelInfoBar";
            this.PanelInfoBar.Size = new System.Drawing.Size(656, 35);
            this.PanelInfoBar.TabIndex = 0;
            this.PanelInfoBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelInfoBar_MouseDown);
            this.PanelInfoBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelInfoBar_MouseMove);
            this.PanelInfoBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelInfoBar_MouseUp);
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
            this.PanelAddBg.Location = new System.Drawing.Point(77, 40);
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
            // TimerClearMsg
            // 
            this.TimerClearMsg.Interval = 3000;
            this.TimerClearMsg.Tick += new System.EventHandler(this.TimerClearMsg_Tick);
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
        private System.Windows.Forms.Button ButtonList;
        private System.Windows.Forms.Button ButtonHide;
        private System.Windows.Forms.Button ButtonMsg;
        private System.Windows.Forms.Button ButtonUnk;
        private System.Windows.Forms.Panel PanelInfoBar;
        public System.Windows.Forms.Label LabelUserName;
        private System.Windows.Forms.Button ButtonUnk2;
        public System.Windows.Forms.Panel PanelAddBg;
        private System.Windows.Forms.Panel PanelAddTask;
        private System.Windows.Forms.Label msg_label;
        private System.Windows.Forms.Label LabelNameItem;
        private System.Windows.Forms.Label LabelCountAdd;
        private System.Windows.Forms.Label DataTimeLabel;
        private System.Windows.Forms.Label LabelButtonItem;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.Button ButtonTurn;
        private System.Windows.Forms.Label LabelPrice;
        public System.Windows.Forms.Label LabelInfo;
        public System.Windows.Forms.TextBox TextboxNameItem;
        public System.Windows.Forms.Timer TimerClearMsg;
        public System.Windows.Forms.TextBox TextboxCountAdd;
        public System.Windows.Forms.TextBox TextboxAddBar;
        public System.Windows.Forms.TextBox TextboxPrice;



    }
}