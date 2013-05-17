namespace PrioritySales
{
    partial class PriorityDetails
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewMainForm = new System.Windows.Forms.DataGridView();
            this.doc_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Izg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMainForm)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.dataGridViewMainForm);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(621, 354);
            this.panel1.TabIndex = 0;
            // 
            // dataGridViewMainForm
            // 
            this.dataGridViewMainForm.AllowUserToAddRows = false;
            this.dataGridViewMainForm.AllowUserToDeleteRows = false;
            this.dataGridViewMainForm.AllowUserToResizeColumns = false;
            this.dataGridViewMainForm.AllowUserToResizeRows = false;
            this.dataGridViewMainForm.BackgroundColor = System.Drawing.Color.Black;
            this.dataGridViewMainForm.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMainForm.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewMainForm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMainForm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.doc_id,
            this.date,
            this.kod,
            this.count,
            this.Rprice,
            this.ost,
            this.Izg,
            this.Nds,
            this.tn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewMainForm.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewMainForm.EnableHeadersVisualStyles = false;
            this.dataGridViewMainForm.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.dataGridViewMainForm.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewMainForm.MultiSelect = false;
            this.dataGridViewMainForm.Name = "dataGridViewMainForm";
            this.dataGridViewMainForm.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Green;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMainForm.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewMainForm.RowHeadersVisible = false;
            this.dataGridViewMainForm.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewMainForm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewMainForm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMainForm.Size = new System.Drawing.Size(615, 348);
            this.dataGridViewMainForm.TabIndex = 1;
            this.dataGridViewMainForm.TabStop = false;
            this.dataGridViewMainForm.Enter += new System.EventHandler(this.dataGridViewMainForm_Enter);
            this.dataGridViewMainForm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewMainForm_KeyDown);
            // 
            // doc_id
            // 
            this.doc_id.HeaderText = "Документ";
            this.doc_id.Name = "doc_id";
            this.doc_id.ReadOnly = true;
            this.doc_id.Width = 115;
            // 
            // date
            // 
            this.date.HeaderText = "Дата";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 95;
            // 
            // kod
            // 
            this.kod.HeaderText = "Код";
            this.kod.Name = "kod";
            this.kod.ReadOnly = true;
            this.kod.Width = 41;
            // 
            // count
            // 
            this.count.HeaderText = "К-во";
            this.count.Name = "count";
            this.count.ReadOnly = true;
            this.count.Width = 70;
            // 
            // Rprice
            // 
            this.Rprice.HeaderText = "Р.Цена";
            this.Rprice.Name = "Rprice";
            this.Rprice.ReadOnly = true;
            this.Rprice.Width = 86;
            // 
            // ost
            // 
            this.ost.HeaderText = "Ост.";
            this.ost.Name = "ost";
            this.ost.ReadOnly = true;
            this.ost.Width = 50;
            // 
            // Izg
            // 
            this.Izg.HeaderText = "Ц.Изг";
            this.Izg.Name = "Izg";
            this.Izg.ReadOnly = true;
            this.Izg.Width = 75;
            // 
            // Nds
            // 
            this.Nds.HeaderText = "Ндс";
            this.Nds.Name = "Nds";
            this.Nds.ReadOnly = true;
            this.Nds.Width = 40;
            // 
            // tn
            // 
            this.tn.HeaderText = "Тн";
            this.tn.Name = "tn";
            this.tn.ReadOnly = true;
            this.tn.Width = 40;
            // 
            // PriorityDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Controls.Add(this.panel1);
            this.Name = "PriorityDetails";
            this.Size = new System.Drawing.Size(623, 356);
            this.Load += new System.EventHandler(this.PriorityDetails_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PriorityDetails_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMainForm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.DataGridView dataGridViewMainForm;
        private System.Windows.Forms.DataGridViewTextBoxColumn doc_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn kod;
        private System.Windows.Forms.DataGridViewTextBoxColumn count;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rprice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Izg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nds;
        private System.Windows.Forms.DataGridViewTextBoxColumn tn;
    }
}
