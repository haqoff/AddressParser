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
            this.btnLoadFromFile = new System.Windows.Forms.Button();
            this.dgvContent = new System.Windows.Forms.DataGridView();
            this.tbSheetName = new System.Windows.Forms.TextBox();
            this.btnParse = new System.Windows.Forms.Button();
            this.pParsingProgress = new System.Windows.Forms.Panel();
            this.pbParse = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblRemaingTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).BeginInit();
            this.pParsingProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadFromFile
            // 
            this.btnLoadFromFile.Location = new System.Drawing.Point(48, 12);
            this.btnLoadFromFile.Name = "btnLoadFromFile";
            this.btnLoadFromFile.Size = new System.Drawing.Size(99, 23);
            this.btnLoadFromFile.TabIndex = 0;
            this.btnLoadFromFile.Text = "Загрузить Файл";
            this.btnLoadFromFile.UseVisualStyleBackColor = true;
            this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);
            // 
            // dgvContent
            // 
            this.dgvContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContent.Location = new System.Drawing.Point(12, 41);
            this.dgvContent.Name = "dgvContent";
            this.dgvContent.Size = new System.Drawing.Size(387, 253);
            this.dgvContent.TabIndex = 1;
            // 
            // tbSheetName
            // 
            this.tbSheetName.Location = new System.Drawing.Point(270, 14);
            this.tbSheetName.Name = "tbSheetName";
            this.tbSheetName.Size = new System.Drawing.Size(100, 20);
            this.tbSheetName.TabIndex = 2;
            // 
            // btnParse
            // 
            this.btnParse.Location = new System.Drawing.Point(183, 300);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(75, 23);
            this.btnParse.TabIndex = 3;
            this.btnParse.Text = "Парсить";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // pParsingProgress
            // 
            this.pParsingProgress.Controls.Add(this.lblRemaingTime);
            this.pParsingProgress.Controls.Add(this.lblProgress);
            this.pParsingProgress.Controls.Add(this.pbParse);
            this.pParsingProgress.Location = new System.Drawing.Point(12, 329);
            this.pParsingProgress.Name = "pParsingProgress";
            this.pParsingProgress.Size = new System.Drawing.Size(492, 80);
            this.pParsingProgress.TabIndex = 4;
            // 
            // pbParse
            // 
            this.pbParse.Location = new System.Drawing.Point(12, 15);
            this.pbParse.Name = "pbParse";
            this.pbParse.Size = new System.Drawing.Size(467, 23);
            this.pbParse.TabIndex = 0;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(211, 41);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(35, 13);
            this.lblProgress.TabIndex = 1;
            this.lblProgress.Text = "label1";
            // 
            // lblRemaingTime
            // 
            this.lblRemaingTime.AutoSize = true;
            this.lblRemaingTime.Location = new System.Drawing.Point(395, 41);
            this.lblRemaingTime.Name = "lblRemaingTime";
            this.lblRemaingTime.Size = new System.Drawing.Size(56, 13);
            this.lblRemaingTime.TabIndex = 2;
            this.lblRemaingTime.Text = "Осталось";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 415);
            this.Controls.Add(this.pParsingProgress);
            this.Controls.Add(this.btnParse);
            this.Controls.Add(this.tbSheetName);
            this.Controls.Add(this.dgvContent);
            this.Controls.Add(this.btnLoadFromFile);
            this.Name = "MainForm";
            this.Text = "Парсер адресов";
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).EndInit();
            this.pParsingProgress.ResumeLayout(false);
            this.pParsingProgress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadFromFile;
        private System.Windows.Forms.DataGridView dgvContent;
        private System.Windows.Forms.TextBox tbSheetName;
        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.Panel pParsingProgress;
        private System.Windows.Forms.ProgressBar pbParse;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblRemaingTime;
    }
}

