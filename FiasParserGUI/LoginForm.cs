using FiasParserLib;
using System;
using System.Windows.Forms;

namespace FiasParserGUI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbServer.Text)
                || string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbPassword.Text)) return;

            string connectionString = string.Format("Data Source={0};Initial Catalog=fias;User ID={1};Password={2};", tbServer.Text, tbName.Text, tbPassword.Text);
            try
            {
                btnLogin.Enabled = false;
                var parser = new FiasParser(connectionString);
                var main = new MainForm(parser);
                Hide();
                if (main.ShowDialog() != DialogResult.OK)
                    Close();
            }
            catch
            {
                MessageBox.Show("Не удалось подключиться к БД. Проверьте данные для подключения.");
                btnLogin.Enabled = true;
            }
        }
    }
}
