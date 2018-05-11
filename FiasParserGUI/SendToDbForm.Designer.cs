namespace FiasParserGUI
{
    partial class SendToDbForm
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
            this.cbChainNameColumn = new System.Windows.Forms.ComboBox();
            this.lblChainNameColumn = new System.Windows.Forms.Label();
            this.lblShopCodeColumn = new System.Windows.Forms.Label();
            this.cbShopCodeColumn = new System.Windows.Forms.ComboBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblLoading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbChainNameColumn
            // 
            this.cbChainNameColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChainNameColumn.FormattingEnabled = true;
            this.cbChainNameColumn.Location = new System.Drawing.Point(83, 20);
            this.cbChainNameColumn.Name = "cbChainNameColumn";
            this.cbChainNameColumn.Size = new System.Drawing.Size(289, 21);
            this.cbChainNameColumn.TabIndex = 0;
            // 
            // lblChainNameColumn
            // 
            this.lblChainNameColumn.AutoSize = true;
            this.lblChainNameColumn.Location = new System.Drawing.Point(12, 23);
            this.lblChainNameColumn.Name = "lblChainNameColumn";
            this.lblChainNameColumn.Size = new System.Drawing.Size(65, 13);
            this.lblChainNameColumn.TabIndex = 1;
            this.lblChainNameColumn.Text = "ChainName:";
            // 
            // lblShopCodeColumn
            // 
            this.lblShopCodeColumn.AutoSize = true;
            this.lblShopCodeColumn.Location = new System.Drawing.Point(12, 50);
            this.lblShopCodeColumn.Name = "lblShopCodeColumn";
            this.lblShopCodeColumn.Size = new System.Drawing.Size(60, 13);
            this.lblShopCodeColumn.TabIndex = 3;
            this.lblShopCodeColumn.Text = "ShopCode:";
            // 
            // cbShopCodeColumn
            // 
            this.cbShopCodeColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShopCodeColumn.FormattingEnabled = true;
            this.cbShopCodeColumn.Location = new System.Drawing.Point(83, 47);
            this.cbShopCodeColumn.Name = "cbShopCodeColumn";
            this.cbShopCodeColumn.Size = new System.Drawing.Size(289, 21);
            this.cbShopCodeColumn.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(161, 74);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Отправить";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(158, 4);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(90, 13);
            this.lblLoading.TabIndex = 5;
            this.lblLoading.Text = "Идёт загрузка...";
            this.lblLoading.Visible = false;
            // 
            // SendToDbForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 105);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lblShopCodeColumn);
            this.Controls.Add(this.cbShopCodeColumn);
            this.Controls.Add(this.lblChainNameColumn);
            this.Controls.Add(this.cbChainNameColumn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SendToDbForm";
            this.Text = "Отправить в БД";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbChainNameColumn;
        private System.Windows.Forms.Label lblChainNameColumn;
        private System.Windows.Forms.Label lblShopCodeColumn;
        private System.Windows.Forms.ComboBox cbShopCodeColumn;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblLoading;
    }
}