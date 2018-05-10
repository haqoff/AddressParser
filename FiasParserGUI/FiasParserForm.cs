using FiasParserLib;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FiasParserGUI
{
    public partial class FiasParserForm : Form
    {
        private DataGridView dgv;
        private FiasParser parser;
        public bool Parsed { get; private set; }


        public FiasParserForm(FiasParser parser, DataGridView dgv)
        {
            InitializeComponent();

            this.parser = parser;
            this.dgv = dgv;

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                cbSourceColumn.Items.Add(column);
            }

            SwitchGUI(true);
        }

        private void SwitchGUI(bool settingsEnable)
        {
            pSettings.Enabled = settingsEnable;
            pProgress.Enabled = !settingsEnable;

            lblProgress.Visible = !settingsEnable;
            lblRemained.Visible = !settingsEnable;

            btnParse.Enabled = settingsEnable;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var input = new InputForm("Введите исходную строку.");
            var replace = new InputForm("Введите строку для замены.");

            input.ShowDialog();
            replace.ShowDialog();

            var replacemant = new Replacement()
            {
                source = input.InputText,
                replace = replace.InputText
            };

            lbReplace.Items.Add(replacemant);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbReplace.SelectedIndex > -1) lbReplace.Items.RemoveAt(lbReplace.SelectedIndex);
        }

        private void RecreateColumn(string name)
        {
            if (dgv.Columns.Contains(name)) dgv.Columns.Remove(name);
            dgv.Columns.Add(name, name);
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            SwitchGUI(false);

            RecreateColumn(MainForm.STATUS_FIELD);
            RecreateColumn(MainForm.DISTRICT_FIELD);
            RecreateColumn(MainForm.REGION_FIELD);
            RecreateColumn(MainForm.CITY_FIELD);
            RecreateColumn(MainForm.STREET_FIELD);
            RecreateColumn(MainForm.HOUSE_FIELD);

            var th = new Thread(Parse);
            th.Start();
        }


        private void Parse()
        {
            var sb = new StringBuilder();
            int countToFix = 0;
            var column = cbSourceColumn.SelectedItem as DataGridViewColumn;

            pbProgress.Invoke((MethodInvoker)(() => pbProgress.Maximum = dgv.Rows.Count));

            DateTime startTime = DateTime.Now;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                var cell = dgv[column.Index, i];
                var value = cell.Value.ToString();

                lblCurrentAddress.Invoke((MethodInvoker)(() => lblCurrentAddress.Text = value));

                foreach (Replacement r in lbReplace.Items)
                {
                    value = value.Replace(r.source, r.replace);
                }

                var parsed = parser.Parse(value);

                if (parsed.Count > 0)
                {
                    var lastNode = parsed[0];

                    while (lastNode != null)
                    {
                        sb.Clear();
                        if (lastNode.Type == TableType.Object)
                        {
                            sb.Append(lastNode.ShortNameType);
                            sb.Append(" ");
                        }
                        sb.Append(lastNode.Name);
                        var name = sb.ToString();

                        //регион
                        if (lastNode.AOLevel > 0 && lastNode.AOLevel < 3) dgv[MainForm.REGION_FIELD, i].Value = name;
                        //город
                        else if (lastNode.AOLevel == 4 || lastNode.AOLevel == 6) dgv[MainForm.CITY_FIELD, i].Value = name;
                        //улица
                        else if (lastNode.AOLevel == 7 || lastNode.AOLevel == 91) dgv[MainForm.STREET_FIELD, i].Value = name;
                        //дом
                        else if (lastNode.Type == TableType.House) dgv[MainForm.HOUSE_FIELD, i].Value = name;

                        lastNode = lastNode.Parent;
                    }

                    if (!string.IsNullOrEmpty(dgv[MainForm.REGION_FIELD, i].Value.ToString()))
                    {
                        //TODO сделаем ищем округ.
                    }
                }

                //Progress
                ///lblProgress
                sb.Clear();
                sb.Append(i + 1);
                sb.Append("/");
                sb.Append(dgv.Rows.Count);

                lblProgress.Invoke((MethodInvoker)(() => lblProgress.Text = sb.ToString()));

                ///lblRemained
                TimeSpan timeRemained = TimeSpan.FromTicks(DateTime.Now.Subtract(startTime).Ticks *
                    (dgv.Rows.Count - (i + 1)) / (i + 1));
                lblRemained.Invoke((MethodInvoker)(() => lblRemained.Text = "Осталось: " + timeRemained.ToString()));

                ///pbProgress
                pbProgress.Invoke((MethodInvoker)(() => pbProgress.Increment(1)));

                ///StatusField, lblCountToFix
                if (MainForm.IsHasEmptyField(dgv, i))
                {
                    dgv[MainForm.STATUS_FIELD, i].Value = "PROBLEMS";
                    countToFix++;
                    lblCountToFix.Invoke((MethodInvoker)(() => lblCountToFix.Text = "Необходимо исправлять: " + countToFix.ToString()));
                }
                else
                {
                    dgv[MainForm.STATUS_FIELD, i].Value = "GOOD";
                }

            }
            Parsed = true;
            MessageBox.Show("Обработка завершена.");
            SwitchGUI(true);
        }

    }

    internal struct Replacement
    {
        public string source;
        public string replace;

        public override string ToString()
        {
            return String.Format("'{0}' => '{1}'", source, replace);
        }
    }
}
