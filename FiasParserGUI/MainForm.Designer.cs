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
            this.btnParseFile = new System.Windows.Forms.Button();
            this.dgvContent = new System.Windows.Forms.DataGridView();
            this.tbSheetName = new System.Windows.Forms.TextBox();
            this.pParsingProgress = new System.Windows.Forms.Panel();
            this.lblCountUknown = new System.Windows.Forms.Label();
            this.lblRemaingTime = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pbParse = new System.Windows.Forms.ProgressBar();
            this.pCustomAddress = new System.Windows.Forms.Panel();
            this.lblLastWritedRow = new System.Windows.Forms.Label();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnNextEmpty = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblLoading = new System.Windows.Forms.Label();
            this.pMenu = new System.Windows.Forms.Panel();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnLoadFromFile = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).BeginInit();
            this.pParsingProgress.SuspendLayout();
            this.pCustomAddress.SuspendLayout();
            this.pMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnParseFile
            // 
            this.btnParseFile.Location = new System.Drawing.Point(3, 7);
            this.btnParseFile.Name = "btnParseFile";
            this.btnParseFile.Size = new System.Drawing.Size(115, 23);
            this.btnParseFile.TabIndex = 0;
            this.btnParseFile.Text = "Распарсить файл";
            this.btnParseFile.UseVisualStyleBackColor = true;
            this.btnParseFile.Click += new System.EventHandler(this.btnParseFile_Click);
            // 
            // dgvContent
            // 
            this.dgvContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContent.Location = new System.Drawing.Point(12, 41);
            this.dgvContent.MultiSelect = false;
            this.dgvContent.Name = "dgvContent";
            this.dgvContent.Size = new System.Drawing.Size(995, 373);
            this.dgvContent.TabIndex = 1;
            // 
            // tbSheetName
            // 
            this.tbSheetName.Location = new System.Drawing.Point(281, 9);
            this.tbSheetName.Name = "tbSheetName";
            this.tbSheetName.Size = new System.Drawing.Size(106, 20);
            this.tbSheetName.TabIndex = 2;
            this.tbSheetName.Text = "Введите имя листа";
            // 
            // pParsingProgress
            // 
            this.pParsingProgress.Controls.Add(this.lblCountUknown);
            this.pParsingProgress.Controls.Add(this.lblRemaingTime);
            this.pParsingProgress.Controls.Add(this.lblProgress);
            this.pParsingProgress.Controls.Add(this.pbParse);
            this.pParsingProgress.Location = new System.Drawing.Point(12, 420);
            this.pParsingProgress.Name = "pParsingProgress";
            this.pParsingProgress.Size = new System.Drawing.Size(995, 65);
            this.pParsingProgress.TabIndex = 4;
            // 
            // lblCountUknown
            // 
            this.lblCountUknown.AutoSize = true;
            this.lblCountUknown.Location = new System.Drawing.Point(577, 41);
            this.lblCountUknown.Name = "lblCountUknown";
            this.lblCountUknown.Size = new System.Drawing.Size(113, 13);
            this.lblCountUknown.TabIndex = 3;
            this.lblCountUknown.Text = "Кол-во неизвестных:";
            // 
            // lblRemaingTime
            // 
            this.lblRemaingTime.AutoSize = true;
            this.lblRemaingTime.Location = new System.Drawing.Point(427, 41);
            this.lblRemaingTime.Name = "lblRemaingTime";
            this.lblRemaingTime.Size = new System.Drawing.Size(56, 13);
            this.lblRemaingTime.TabIndex = 2;
            this.lblRemaingTime.Text = "Осталось";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(227, 41);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(56, 13);
            this.lblProgress.TabIndex = 1;
            this.lblProgress.Text = "Прогресс";
            // 
            // pbParse
            // 
            this.pbParse.Location = new System.Drawing.Point(12, 15);
            this.pbParse.Name = "pbParse";
            this.pbParse.Size = new System.Drawing.Size(980, 23);
            this.pbParse.TabIndex = 0;
            // 
            // pCustomAddress
            // 
            this.pCustomAddress.Controls.Add(this.lblLastWritedRow);
            this.pCustomAddress.Controls.Add(this.btnWrite);
            this.pCustomAddress.Controls.Add(this.btnNextEmpty);
            this.pCustomAddress.Controls.Add(this.textBox1);
            this.pCustomAddress.Controls.Add(this.comboBox6);
            this.pCustomAddress.Controls.Add(this.comboBox5);
            this.pCustomAddress.Controls.Add(this.comboBox4);
            this.pCustomAddress.Controls.Add(this.comboBox3);
            this.pCustomAddress.Controls.Add(this.comboBox2);
            this.pCustomAddress.Controls.Add(this.comboBox1);
            this.pCustomAddress.Location = new System.Drawing.Point(1013, 41);
            this.pCustomAddress.Name = "pCustomAddress";
            this.pCustomAddress.Size = new System.Drawing.Size(222, 444);
            this.pCustomAddress.TabIndex = 5;
            // 
            // lblLastWritedRow
            // 
            this.lblLastWritedRow.AutoSize = true;
            this.lblLastWritedRow.Location = new System.Drawing.Point(30, 420);
            this.lblLastWritedRow.Name = "lblLastWritedRow";
            this.lblLastWritedRow.Size = new System.Drawing.Size(161, 13);
            this.lblLastWritedRow.TabIndex = 10;
            this.lblLastWritedRow.Text = "Последняя записаная строка:";
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(113, 388);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(106, 23);
            this.btnWrite.TabIndex = 9;
            this.btnWrite.Text = "Записать";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnNextEmpty
            // 
            this.btnNextEmpty.Location = new System.Drawing.Point(3, 389);
            this.btnNextEmpty.Name = "btnNextEmpty";
            this.btnNextEmpty.Size = new System.Drawing.Size(104, 23);
            this.btnNextEmpty.TabIndex = 8;
            this.btnNextEmpty.Text = "Перейти к след.";
            this.btnNextEmpty.UseVisualStyleBackColor = true;
            this.btnNextEmpty.Click += new System.EventHandler(this.btnNextEmpty_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 343);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(222, 20);
            this.textBox1.TabIndex = 7;
            // 
            // comboBox6
            // 
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new System.Drawing.Point(0, 214);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(220, 21);
            this.comboBox6.TabIndex = 5;
            this.comboBox6.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new System.Drawing.Point(0, 170);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(220, 21);
            this.comboBox5.TabIndex = 4;
            this.comboBox5.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(0, 122);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(220, 21);
            this.comboBox4.TabIndex = 3;
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(0, 81);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(220, 21);
            this.comboBox3.TabIndex = 2;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(0, 42);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(220, 21);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(-1, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(220, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(1110, 24);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(10, 13);
            this.lblLoading.TabIndex = 6;
            this.lblLoading.Text = "-";
            // 
            // pMenu
            // 
            this.pMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pMenu.Controls.Add(this.btnSave);
            this.pMenu.Controls.Add(this.btnSaveAs);
            this.pMenu.Controls.Add(this.btnLoadFromFile);
            this.pMenu.Controls.Add(this.btnParseFile);
            this.pMenu.Controls.Add(this.tbSheetName);
            this.pMenu.Location = new System.Drawing.Point(12, 1);
            this.pMenu.Name = "pMenu";
            this.pMenu.Size = new System.Drawing.Size(992, 36);
            this.pMenu.TabIndex = 7;
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(892, 6);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(95, 23);
            this.btnSaveAs.TabIndex = 9;
            this.btnSaveAs.Text = "Сохранить как";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnLoadFromFile
            // 
            this.btnLoadFromFile.Location = new System.Drawing.Point(136, 7);
            this.btnLoadFromFile.Name = "btnLoadFromFile";
            this.btnLoadFromFile.Size = new System.Drawing.Size(113, 23);
            this.btnLoadFromFile.TabIndex = 8;
            this.btnLoadFromFile.Text = "Загрузить с файла";
            this.btnLoadFromFile.UseVisualStyleBackColor = true;
            this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(804, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 488);
            this.Controls.Add(this.pMenu);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.pCustomAddress);
            this.Controls.Add(this.pParsingProgress);
            this.Controls.Add(this.dgvContent);
            this.Name = "MainForm";
            this.Text = "Парсер адресов";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).EndInit();
            this.pParsingProgress.ResumeLayout(false);
            this.pParsingProgress.PerformLayout();
            this.pCustomAddress.ResumeLayout(false);
            this.pCustomAddress.PerformLayout();
            this.pMenu.ResumeLayout(false);
            this.pMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnParseFile;
        private System.Windows.Forms.DataGridView dgvContent;
        private System.Windows.Forms.TextBox tbSheetName;
        private System.Windows.Forms.Panel pParsingProgress;
        private System.Windows.Forms.ProgressBar pbParse;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblRemaingTime;
        private System.Windows.Forms.Label lblCountUknown;
        private System.Windows.Forms.Panel pCustomAddress;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnNextEmpty;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Panel pMenu;
        private System.Windows.Forms.Button btnLoadFromFile;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Label lblLastWritedRow;
        private System.Windows.Forms.Button btnSave;
    }
}

