using FiasParserLib;
using System;
using System.Data;
using System.Data.OleDb;
using System.Threading;
using System.Windows.Forms;

namespace FiasParserGUI
{
    public partial class MainForm : Form
    {
        private string path;
        private DataTable table;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            var odf = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx"
            };

            if (odf.ShowDialog() == DialogResult.OK)
            {
                path = odf.FileName;
                LoadToGridView();
            }
        }

        private void LoadToGridView()
        {
            if (tbSheetName.Text == "") return;

            var connectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties=\"Excel 8.0;HDR=Yes;\";", path);
            var queryString = String.Format("Select * from [{0}$]", tbSheetName.Text);

            try
            {
                var connection = new OleDbConnection(connectionString);
                var dataAdapter = new OleDbDataAdapter(queryString, connection);
                table = new DataTable();
                dataAdapter.Fill(table);
                Text = "Парсер адресов, загружено: " + path;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(DoParse);
            th.Start();
        }

        private void DoParse()
        {
            if (table == null) return;

            const int columnIndex = 0;
            var result = new string[table.Rows.Count, 3];
            //var parser = new FiasParser();
            DateTime startTime = DateTime.Now;

            pbParse.Invoke(new Action(() => { pbParse.Maximum = table.Rows.Count; }));

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string value = table.Rows[i][columnIndex].ToString();
                // ParseResult r = parser.Parse(value);

                var r = new ParseResult()
                {
                    address = "Такой то адрес",
                    id = "такой то d",
                    type = IdType.House                   
                };

                result[i, 0] = r.address;
                result[i, 1] = r.type.ToString();
                result[i, 2] = r.id;

                //progress call
                TimeSpan timeRemaining = TimeSpan.FromTicks((long)(DateTime.Now.Subtract(startTime).Ticks * (table.Rows.Count - (i + 1)) /(float) (i + 1)));

                pbParse.Invoke(new Action(() => { lblRemaingTime.Text = timeRemaining.ToString(); }));
                pbParse.Invoke(new Action(() => { pbParse.Increment(1); }));
                pbParse.Invoke(new Action(() => { lblProgress.Text = (i + 1).ToString() + "/" + table.Rows.Count; }));
                
            }
            MessageBox.Show("всё");
        }
    }
}
