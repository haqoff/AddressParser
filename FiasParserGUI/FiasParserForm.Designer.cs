namespace FiasParserGUI
{
    partial class FiasParserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FiasParserForm));
            this.lblSourceColumn = new System.Windows.Forms.Label();
            this.cbSourceColumn = new System.Windows.Forms.ComboBox();
            this.btnParse = new System.Windows.Forms.Button();
            this.pProgress = new System.Windows.Forms.Panel();
            this.lblCurrentAddress = new System.Windows.Forms.Label();
            this.lblCountToFix = new System.Windows.Forms.Label();
            this.lblRemained = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.lbReplace = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pSettings = new System.Windows.Forms.Panel();
            this.pProgress.SuspendLayout();
            this.pSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSourceColumn
            // 
            this.lblSourceColumn.AutoSize = true;
            this.lblSourceColumn.Location = new System.Drawing.Point(0, 5);
            this.lblSourceColumn.Name = "lblSourceColumn";
            this.lblSourceColumn.Size = new System.Drawing.Size(135, 17);
            this.lblSourceColumn.TabIndex = 0;
            this.lblSourceColumn.Text = "Исходный столбец:";
            // 
            // cbSourceColumn
            // 
            this.cbSourceColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSourceColumn.FormattingEnabled = true;
            this.cbSourceColumn.Location = new System.Drawing.Point(144, 2);
            this.cbSourceColumn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbSourceColumn.Name = "cbSourceColumn";
            this.cbSourceColumn.Size = new System.Drawing.Size(223, 24);
            this.cbSourceColumn.TabIndex = 1;
            // 
            // btnParse
            // 
            this.btnParse.Location = new System.Drawing.Point(129, 274);
            this.btnParse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(109, 28);
            this.btnParse.TabIndex = 3;
            this.btnParse.Text = "Обработать";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // pProgress
            // 
            this.pProgress.Controls.Add(this.lblCurrentAddress);
            this.pProgress.Controls.Add(this.lblCountToFix);
            this.pProgress.Controls.Add(this.lblRemained);
            this.pProgress.Controls.Add(this.lblProgress);
            this.pProgress.Controls.Add(this.pbProgress);
            this.pProgress.Location = new System.Drawing.Point(12, 161);
            this.pProgress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pProgress.Name = "pProgress";
            this.pProgress.Size = new System.Drawing.Size(369, 107);
            this.pProgress.TabIndex = 4;
            // 
            // lblCurrentAddress
            // 
            this.lblCurrentAddress.Location = new System.Drawing.Point(0, 10);
            this.lblCurrentAddress.Name = "lblCurrentAddress";
            this.lblCurrentAddress.Size = new System.Drawing.Size(368, 17);
            this.lblCurrentAddress.TabIndex = 4;
            this.lblCurrentAddress.Text = "Текущий адрес:";
            this.lblCurrentAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCountToFix
            // 
            this.lblCountToFix.AutoSize = true;
            this.lblCountToFix.Location = new System.Drawing.Point(3, 36);
            this.lblCountToFix.Name = "lblCountToFix";
            this.lblCountToFix.Size = new System.Drawing.Size(214, 17);
            this.lblCountToFix.TabIndex = 3;
            this.lblCountToFix.Text = "Cтрок со статусом PROBLEMS:";
            // 
            // lblRemained
            // 
            this.lblRemained.AutoSize = true;
            this.lblRemained.Location = new System.Drawing.Point(208, 60);
            this.lblRemained.Name = "lblRemained";
            this.lblRemained.Size = new System.Drawing.Size(75, 17);
            this.lblRemained.TabIndex = 2;
            this.lblRemained.Text = "Осталось:";
            this.lblRemained.Visible = false;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(43, 60);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(28, 17);
            this.lblProgress.TabIndex = 1;
            this.lblProgress.Text = "0/0";
            this.lblProgress.Visible = false;
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(3, 80);
            this.pbProgress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(363, 23);
            this.pbProgress.TabIndex = 0;
            // 
            // lbReplace
            // 
            this.lbReplace.FormattingEnabled = true;
            this.lbReplace.ItemHeight = 16;
            this.lbReplace.Location = new System.Drawing.Point(3, 55);
            this.lbReplace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbReplace.Name = "lbReplace";
            this.lbReplace.Size = new System.Drawing.Size(313, 84);
            this.lbReplace.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(323, 55);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(44, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(323, 116);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(44, 23);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Строки для замены:";
            // 
            // pSettings
            // 
            this.pSettings.Controls.Add(this.lblSourceColumn);
            this.pSettings.Controls.Add(this.lbReplace);
            this.pSettings.Controls.Add(this.cbSourceColumn);
            this.pSettings.Controls.Add(this.label3);
            this.pSettings.Controls.Add(this.btnAdd);
            this.pSettings.Controls.Add(this.btnRemove);
            this.pSettings.Location = new System.Drawing.Point(12, 12);
            this.pSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pSettings.Name = "pSettings";
            this.pSettings.Size = new System.Drawing.Size(369, 143);
            this.pSettings.TabIndex = 9;
            // 
            // FiasParserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 308);
            this.Controls.Add(this.pSettings);
            this.Controls.Add(this.pProgress);
            this.Controls.Add(this.btnParse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FiasParserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Распарсить по FIAS";
            this.pProgress.ResumeLayout(false);
            this.pProgress.PerformLayout();
            this.pSettings.ResumeLayout(false);
            this.pSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSourceColumn;
        private System.Windows.Forms.ComboBox cbSourceColumn;
        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.Panel pProgress;
        private System.Windows.Forms.Label lblCurrentAddress;
        private System.Windows.Forms.Label lblCountToFix;
        private System.Windows.Forms.Label lblRemained;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.ListBox lbReplace;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pSettings;
    }
}