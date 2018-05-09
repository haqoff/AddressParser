namespace FiasParserGUI
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvContent = new System.Windows.Forms.DataGridView();
            this.msBar = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.miClose = new System.Windows.Forms.ToolStripMenuItem();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.miProccessing = new System.Windows.Forms.ToolStripMenuItem();
            this.miFiasParse = new System.Windows.Forms.ToolStripMenuItem();
            this.miSendToDB = new System.Windows.Forms.ToolStripMenuItem();
            this.miSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.gbAddress = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tbHouse = new System.Windows.Forms.TextBox();
            this.lblHouse = new System.Windows.Forms.Label();
            this.tbStreet = new System.Windows.Forms.TextBox();
            this.lblStreet = new System.Windows.Forms.Label();
            this.tbCity = new System.Windows.Forms.TextBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.tbRegion = new System.Windows.Forms.TextBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.tbDistrict = new System.Windows.Forms.TextBox();
            this.lblDistrict = new System.Windows.Forms.Label();
            this.pSide = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.lblRowIndex = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).BeginInit();
            this.msBar.SuspendLayout();
            this.gbAddress.SuspendLayout();
            this.pSide.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvContent
            // 
            this.dgvContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContent.Location = new System.Drawing.Point(16, 32);
            this.dgvContent.Margin = new System.Windows.Forms.Padding(4);
            this.dgvContent.MultiSelect = false;
            this.dgvContent.Name = "dgvContent";
            this.dgvContent.Size = new System.Drawing.Size(1327, 564);
            this.dgvContent.TabIndex = 1;
            this.dgvContent.SelectionChanged += new System.EventHandler(this.dgvContent_SelectionChanged);
            // 
            // msBar
            // 
            this.msBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miProccessing,
            this.miSettings});
            this.msBar.Location = new System.Drawing.Point(0, 0);
            this.msBar.Name = "msBar";
            this.msBar.Size = new System.Drawing.Size(1658, 28);
            this.msBar.TabIndex = 7;
            this.msBar.Text = "menuStrip1";
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpen,
            this.miClose,
            this.miSave,
            this.miSaveAs});
            this.miFile.Name = "miFile";
            this.miFile.Size = new System.Drawing.Size(57, 24);
            this.miFile.Text = "Файл";
            // 
            // miOpen
            // 
            this.miOpen.Name = "miOpen";
            this.miOpen.Size = new System.Drawing.Size(216, 26);
            this.miOpen.Text = "Открыть";
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // miClose
            // 
            this.miClose.Enabled = false;
            this.miClose.Name = "miClose";
            this.miClose.Size = new System.Drawing.Size(216, 26);
            this.miClose.Text = "Закрыть";
            this.miClose.Click += new System.EventHandler(this.miClose_Click);
            // 
            // miSave
            // 
            this.miSave.Enabled = false;
            this.miSave.Name = "miSave";
            this.miSave.Size = new System.Drawing.Size(216, 26);
            this.miSave.Text = "Сохранить";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miSaveAs
            // 
            this.miSaveAs.Enabled = false;
            this.miSaveAs.Name = "miSaveAs";
            this.miSaveAs.Size = new System.Drawing.Size(216, 26);
            this.miSaveAs.Text = "Сохранить как";
            this.miSaveAs.Click += new System.EventHandler(this.miSaveAs_Click);
            // 
            // miProccessing
            // 
            this.miProccessing.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFiasParse,
            this.miSendToDB});
            this.miProccessing.Enabled = false;
            this.miProccessing.Name = "miProccessing";
            this.miProccessing.Size = new System.Drawing.Size(97, 24);
            this.miProccessing.Text = "Обработка";
            // 
            // miFiasParse
            // 
            this.miFiasParse.Name = "miFiasParse";
            this.miFiasParse.Size = new System.Drawing.Size(218, 26);
            this.miFiasParse.Text = "Распарсить по FIAS";
            this.miFiasParse.Click += new System.EventHandler(this.miFiasParse_Click);
            // 
            // miSendToDB
            // 
            this.miSendToDB.Name = "miSendToDB";
            this.miSendToDB.Size = new System.Drawing.Size(218, 26);
            this.miSendToDB.Text = "Отправить в БД";
            // 
            // miSettings
            // 
            this.miSettings.Name = "miSettings";
            this.miSettings.Size = new System.Drawing.Size(96, 24);
            this.miSettings.Text = "Настройки";
            // 
            // gbAddress
            // 
            this.gbAddress.Controls.Add(this.lblRowIndex);
            this.gbAddress.Controls.Add(this.lblStatus);
            this.gbAddress.Controls.Add(this.tbHouse);
            this.gbAddress.Controls.Add(this.lblHouse);
            this.gbAddress.Controls.Add(this.tbStreet);
            this.gbAddress.Controls.Add(this.lblStreet);
            this.gbAddress.Controls.Add(this.tbCity);
            this.gbAddress.Controls.Add(this.lblCity);
            this.gbAddress.Controls.Add(this.tbRegion);
            this.gbAddress.Controls.Add(this.lblRegion);
            this.gbAddress.Controls.Add(this.tbDistrict);
            this.gbAddress.Controls.Add(this.lblDistrict);
            this.gbAddress.Location = new System.Drawing.Point(1353, 31);
            this.gbAddress.Name = "gbAddress";
            this.gbAddress.Size = new System.Drawing.Size(301, 354);
            this.gbAddress.TabIndex = 11;
            this.gbAddress.TabStop = false;
            this.gbAddress.Text = "Адрес";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(0, 70);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(57, 17);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Статус:";
            // 
            // tbHouse
            // 
            this.tbHouse.Location = new System.Drawing.Point(3, 321);
            this.tbHouse.Name = "tbHouse";
            this.tbHouse.Size = new System.Drawing.Size(292, 22);
            this.tbHouse.TabIndex = 9;
            // 
            // lblHouse
            // 
            this.lblHouse.AutoSize = true;
            this.lblHouse.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblHouse.Location = new System.Drawing.Point(0, 301);
            this.lblHouse.Name = "lblHouse";
            this.lblHouse.Size = new System.Drawing.Size(40, 17);
            this.lblHouse.TabIndex = 8;
            this.lblHouse.Text = "Дом:";
            // 
            // tbStreet
            // 
            this.tbStreet.Location = new System.Drawing.Point(3, 271);
            this.tbStreet.Name = "tbStreet";
            this.tbStreet.Size = new System.Drawing.Size(292, 22);
            this.tbStreet.TabIndex = 7;
            // 
            // lblStreet
            // 
            this.lblStreet.AutoSize = true;
            this.lblStreet.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblStreet.Location = new System.Drawing.Point(0, 251);
            this.lblStreet.Name = "lblStreet";
            this.lblStreet.Size = new System.Drawing.Size(53, 17);
            this.lblStreet.TabIndex = 6;
            this.lblStreet.Text = "Улица:";
            // 
            // tbCity
            // 
            this.tbCity.Location = new System.Drawing.Point(3, 221);
            this.tbCity.Name = "tbCity";
            this.tbCity.Size = new System.Drawing.Size(292, 22);
            this.tbCity.TabIndex = 5;
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCity.Location = new System.Drawing.Point(0, 201);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(136, 17);
            this.lblCity.TabIndex = 4;
            this.lblCity.Text = "Населённый пункт:";
            // 
            // tbRegion
            // 
            this.tbRegion.Location = new System.Drawing.Point(3, 169);
            this.tbRegion.Name = "tbRegion";
            this.tbRegion.Size = new System.Drawing.Size(292, 22);
            this.tbRegion.TabIndex = 3;
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblRegion.Location = new System.Drawing.Point(0, 149);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(68, 17);
            this.lblRegion.TabIndex = 2;
            this.lblRegion.Text = "Область:";
            // 
            // tbDistrict
            // 
            this.tbDistrict.Location = new System.Drawing.Point(3, 117);
            this.tbDistrict.Name = "tbDistrict";
            this.tbDistrict.Size = new System.Drawing.Size(292, 22);
            this.tbDistrict.TabIndex = 1;
            // 
            // lblDistrict
            // 
            this.lblDistrict.AutoSize = true;
            this.lblDistrict.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblDistrict.Location = new System.Drawing.Point(0, 97);
            this.lblDistrict.Name = "lblDistrict";
            this.lblDistrict.Size = new System.Drawing.Size(50, 17);
            this.lblDistrict.TabIndex = 0;
            this.lblDistrict.Text = "Округ:";
            // 
            // pSide
            // 
            this.pSide.Controls.Add(this.button1);
            this.pSide.Enabled = false;
            this.pSide.Location = new System.Drawing.Point(1350, 32);
            this.pSide.Name = "pSide";
            this.pSide.Size = new System.Drawing.Size(314, 564);
            this.pSide.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(85, 529);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Перейти к след.";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lblRowIndex
            // 
            this.lblRowIndex.AutoSize = true;
            this.lblRowIndex.Location = new System.Drawing.Point(0, 43);
            this.lblRowIndex.Name = "lblRowIndex";
            this.lblRowIndex.Size = new System.Drawing.Size(104, 17);
            this.lblRowIndex.TabIndex = 11;
            this.lblRowIndex.Text = "Номер строки:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1658, 601);
            this.Controls.Add(this.gbAddress);
            this.Controls.Add(this.pSide);
            this.Controls.Add(this.dgvContent);
            this.Controls.Add(this.msBar);
            this.MainMenuStrip = this.msBar;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Парсер адресов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).EndInit();
            this.msBar.ResumeLayout(false);
            this.msBar.PerformLayout();
            this.gbAddress.ResumeLayout(false);
            this.gbAddress.PerformLayout();
            this.pSide.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvContent;
        private System.Windows.Forms.MenuStrip msBar;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miOpen;
        private System.Windows.Forms.ToolStripMenuItem miSave;
        private System.Windows.Forms.ToolStripMenuItem miSaveAs;
        private System.Windows.Forms.ToolStripMenuItem miProccessing;
        private System.Windows.Forms.ToolStripMenuItem miFiasParse;
        private System.Windows.Forms.ToolStripMenuItem miSendToDB;
        private System.Windows.Forms.ToolStripMenuItem miSettings;
        private System.Windows.Forms.GroupBox gbAddress;
        private System.Windows.Forms.TextBox tbRegion;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.TextBox tbDistrict;
        private System.Windows.Forms.Label lblDistrict;
        private System.Windows.Forms.TextBox tbHouse;
        private System.Windows.Forms.Label lblHouse;
        private System.Windows.Forms.TextBox tbStreet;
        private System.Windows.Forms.Label lblStreet;
        private System.Windows.Forms.TextBox tbCity;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pSide;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem miClose;
        private System.Windows.Forms.Label lblRowIndex;
    }
}

