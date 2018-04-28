namespace FiasParserGUI
{
    partial class LoginForm
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
            this.tbServer = new System.Windows.Forms.TextBox();
            this.lblDataForLogin = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(55, 33);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(217, 20);
            this.tbServer.TabIndex = 0;
            // 
            // lblDataForLogin
            // 
            this.lblDataForLogin.AutoSize = true;
            this.lblDataForLogin.Location = new System.Drawing.Point(72, 9);
            this.lblDataForLogin.Name = "lblDataForLogin";
            this.lblDataForLogin.Size = new System.Drawing.Size(146, 13);
            this.lblDataForLogin.TabIndex = 1;
            this.lblDataForLogin.Text = "Введите данные для входа.";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(2, 36);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(47, 13);
            this.lblServer.TabIndex = 2;
            this.lblServer.Text = "Сервер:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(2, 72);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Имя: ";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(55, 69);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(217, 20);
            this.tbName.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(2, 98);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(48, 13);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Пароль:";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(55, 95);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(217, 20);
            this.tbPassword.TabIndex = 5;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(103, 137);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "Войти";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 174);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.lblDataForLogin);
            this.Controls.Add(this.tbServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.Text = "Вход";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label lblDataForLogin;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnLogin;
    }
}