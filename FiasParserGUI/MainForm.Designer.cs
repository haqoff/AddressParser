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
            this.cbHouse = new System.Windows.Forms.ComboBox();
            this.cbStreet = new System.Windows.Forms.ComboBox();
            this.cbCity = new System.Windows.Forms.ComboBox();
            this.cbRegion = new System.Windows.Forms.ComboBox();
            this.tbDistrict = new System.Windows.Forms.TextBox();
            this.lblRowIndex = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblHouse = new System.Windows.Forms.Label();
            this.lblStreet = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.lblDistrict = new System.Windows.Forms.Label();
            this.pSide = new System.Windows.Forms.Panel();
            this.btnForwardNext = new System.Windows.Forms.Button();
            this.lblLoading = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).BeginInit();
            this.msBar.SuspendLayout();
            this.gbAddress.SuspendLayout();
            this.pSide.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvContent
            // 
            this.dgvContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContent.Location = new System.Drawing.Point(12, 26);
            this.dgvContent.MultiSelect = false;
            this.dgvContent.Name = "dgvContent";
            this.dgvContent.Size = new System.Drawing.Size(995, 458);
            this.dgvContent.TabIndex = 1;
            this.dgvContent.SelectionChanged += new System.EventHandler(this.dgvContent_SelectionChanged);
            this.dgvContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
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
            this.msBar.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.msBar.Size = new System.Drawing.Size(1247, 24);
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
            this.miFile.Size = new System.Drawing.Size(48, 20);
            this.miFile.Text = "Файл";
            // 
            // miOpen
            // 
            this.miOpen.Name = "miOpen";
            this.miOpen.Size = new System.Drawing.Size(153, 22);
            this.miOpen.Text = "Открыть";
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // miClose
            // 
            this.miClose.Enabled = false;
            this.miClose.Name = "miClose";
            this.miClose.Size = new System.Drawing.Size(153, 22);
            this.miClose.Text = "Закрыть";
            this.miClose.Click += new System.EventHandler(this.miClose_Click);
            // 
            // miSave
            // 
            this.miSave.Enabled = false;
            this.miSave.Name = "miSave";
            this.miSave.Size = new System.Drawing.Size(153, 22);
            this.miSave.Text = "Сохранить";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miSaveAs
            // 
            this.miSaveAs.Enabled = false;
            this.miSaveAs.Name = "miSaveAs";
            this.miSaveAs.Size = new System.Drawing.Size(153, 22);
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
            this.miProccessing.Size = new System.Drawing.Size(79, 20);
            this.miProccessing.Text = "Обработка";
            // 
            // miFiasParse
            // 
            this.miFiasParse.Name = "miFiasParse";
            this.miFiasParse.Size = new System.Drawing.Size(180, 22);
            this.miFiasParse.Text = "Распарсить по FIAS";
            this.miFiasParse.Click += new System.EventHandler(this.miFiasParse_Click);
            // 
            // miSendToDB
            // 
            this.miSendToDB.Name = "miSendToDB";
            this.miSendToDB.Size = new System.Drawing.Size(180, 22);
            this.miSendToDB.Text = "Отправить в БД";
            this.miSendToDB.Click += new System.EventHandler(this.miSendToDB_Click);
            // 
            // miSettings
            // 
            this.miSettings.Name = "miSettings";
            this.miSettings.Size = new System.Drawing.Size(79, 20);
            this.miSettings.Text = "Настройки";
            // 
            // gbAddress
            // 
            this.gbAddress.Controls.Add(this.lblLoading);
            this.gbAddress.Controls.Add(this.cbHouse);
            this.gbAddress.Controls.Add(this.cbStreet);
            this.gbAddress.Controls.Add(this.cbCity);
            this.gbAddress.Controls.Add(this.cbRegion);
            this.gbAddress.Controls.Add(this.tbDistrict);
            this.gbAddress.Controls.Add(this.lblRowIndex);
            this.gbAddress.Controls.Add(this.lblStatus);
            this.gbAddress.Controls.Add(this.lblHouse);
            this.gbAddress.Controls.Add(this.lblStreet);
            this.gbAddress.Controls.Add(this.lblCity);
            this.gbAddress.Controls.Add(this.lblRegion);
            this.gbAddress.Controls.Add(this.lblDistrict);
            this.gbAddress.Location = new System.Drawing.Point(1015, 25);
            this.gbAddress.Margin = new System.Windows.Forms.Padding(2);
            this.gbAddress.Name = "gbAddress";
            this.gbAddress.Padding = new System.Windows.Forms.Padding(2);
            this.gbAddress.Size = new System.Drawing.Size(226, 288);
            this.gbAddress.TabIndex = 11;
            this.gbAddress.TabStop = false;
            this.gbAddress.Text = "Адрес";
            // 
            // cbHouse
            // 
            this.cbHouse.FormattingEnabled = true;
            this.cbHouse.Location = new System.Drawing.Point(1, 261);
            this.cbHouse.Name = "cbHouse";
            this.cbHouse.Size = new System.Drawing.Size(220, 21);
            this.cbHouse.Sorted = true;
            this.cbHouse.TabIndex = 17;
            this.cbHouse.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.cbHouse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // cbStreet
            // 
            this.cbStreet.FormattingEnabled = true;
            this.cbStreet.Location = new System.Drawing.Point(0, 220);
            this.cbStreet.Name = "cbStreet";
            this.cbStreet.Size = new System.Drawing.Size(221, 21);
            this.cbStreet.Sorted = true;
            this.cbStreet.TabIndex = 16;
            this.cbStreet.SelectedIndexChanged += new System.EventHandler(this.cbStreet_SelectedIndexChanged);
            this.cbStreet.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.cbStreet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // cbCity
            // 
            this.cbCity.FormattingEnabled = true;
            this.cbCity.Location = new System.Drawing.Point(0, 180);
            this.cbCity.Name = "cbCity";
            this.cbCity.Size = new System.Drawing.Size(221, 21);
            this.cbCity.Sorted = true;
            this.cbCity.TabIndex = 15;
            this.cbCity.SelectedIndexChanged += new System.EventHandler(this.cbCity_SelectedIndexChanged);
            this.cbCity.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.cbCity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // cbRegion
            // 
            this.cbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegion.FormattingEnabled = true;
            this.cbRegion.Location = new System.Drawing.Point(0, 137);
            this.cbRegion.Name = "cbRegion";
            this.cbRegion.Size = new System.Drawing.Size(221, 21);
            this.cbRegion.Sorted = true;
            this.cbRegion.TabIndex = 14;
            this.cbRegion.SelectedIndexChanged += new System.EventHandler(this.cbRegion_SelectedIndexChanged);
            this.cbRegion.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.cbRegion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // tbDistrict
            // 
            this.tbDistrict.Location = new System.Drawing.Point(0, 98);
            this.tbDistrict.Name = "tbDistrict";
            this.tbDistrict.ReadOnly = true;
            this.tbDistrict.Size = new System.Drawing.Size(220, 20);
            this.tbDistrict.TabIndex = 13;
            this.tbDistrict.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // lblRowIndex
            // 
            this.lblRowIndex.AutoSize = true;
            this.lblRowIndex.Location = new System.Drawing.Point(0, 35);
            this.lblRowIndex.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRowIndex.Name = "lblRowIndex";
            this.lblRowIndex.Size = new System.Drawing.Size(82, 13);
            this.lblRowIndex.TabIndex = 11;
            this.lblRowIndex.Text = "Номер строки:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(0, 57);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 13);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Статус:";
            // 
            // lblHouse
            // 
            this.lblHouse.AutoSize = true;
            this.lblHouse.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblHouse.Location = new System.Drawing.Point(0, 245);
            this.lblHouse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHouse.Name = "lblHouse";
            this.lblHouse.Size = new System.Drawing.Size(33, 13);
            this.lblHouse.TabIndex = 8;
            this.lblHouse.Text = "Дом:";
            // 
            // lblStreet
            // 
            this.lblStreet.AutoSize = true;
            this.lblStreet.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblStreet.Location = new System.Drawing.Point(0, 204);
            this.lblStreet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStreet.Name = "lblStreet";
            this.lblStreet.Size = new System.Drawing.Size(42, 13);
            this.lblStreet.TabIndex = 6;
            this.lblStreet.Text = "Улица:";
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCity.Location = new System.Drawing.Point(0, 163);
            this.lblCity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(105, 13);
            this.lblCity.TabIndex = 4;
            this.lblCity.Text = "Населённый пункт:";
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblRegion.Location = new System.Drawing.Point(0, 121);
            this.lblRegion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(53, 13);
            this.lblRegion.TabIndex = 2;
            this.lblRegion.Text = "Область:";
            // 
            // lblDistrict
            // 
            this.lblDistrict.AutoSize = true;
            this.lblDistrict.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblDistrict.Location = new System.Drawing.Point(0, 79);
            this.lblDistrict.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDistrict.Name = "lblDistrict";
            this.lblDistrict.Size = new System.Drawing.Size(40, 13);
            this.lblDistrict.TabIndex = 0;
            this.lblDistrict.Text = "Округ:";
            // 
            // pSide
            // 
            this.pSide.Controls.Add(this.btnForwardNext);
            this.pSide.Enabled = false;
            this.pSide.Location = new System.Drawing.Point(1012, 26);
            this.pSide.Margin = new System.Windows.Forms.Padding(2);
            this.pSide.Name = "pSide";
            this.pSide.Size = new System.Drawing.Size(236, 458);
            this.pSide.TabIndex = 13;
            // 
            // btnForwardNext
            // 
            this.btnForwardNext.Location = new System.Drawing.Point(64, 430);
            this.btnForwardNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnForwardNext.Name = "btnForwardNext";
            this.btnForwardNext.Size = new System.Drawing.Size(106, 23);
            this.btnForwardNext.TabIndex = 0;
            this.btnForwardNext.Text = "Перейти к след.";
            this.btnForwardNext.UseVisualStyleBackColor = true;
            this.btnForwardNext.Click += new System.EventHandler(this.btnForwardNext_Click);
            // 
            // lblLoading
            // 
            this.lblLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLoading.Location = new System.Drawing.Point(5, 15);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(215, 20);
            this.lblLoading.TabIndex = 18;
            this.lblLoading.Text = "---";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1247, 488);
            this.Controls.Add(this.gbAddress);
            this.Controls.Add(this.pSide);
            this.Controls.Add(this.dgvContent);
            this.Controls.Add(this.msBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.msBar;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.Label lblDistrict;
        private System.Windows.Forms.Label lblHouse;
        private System.Windows.Forms.Label lblStreet;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pSide;
        private System.Windows.Forms.Button btnForwardNext;
        private System.Windows.Forms.ToolStripMenuItem miClose;
        private System.Windows.Forms.Label lblRowIndex;
        private System.Windows.Forms.TextBox tbDistrict;
        private System.Windows.Forms.ComboBox cbRegion;
        private System.Windows.Forms.ComboBox cbHouse;
        private System.Windows.Forms.ComboBox cbStreet;
        private System.Windows.Forms.ComboBox cbCity;
        private System.Windows.Forms.Label lblLoading;
    }
}

