using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FiasParserLib;

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
                cbSourceColumn.Items.Add(column.Name);
            }

            SwitchGUI(true);
        }

        private void SwitchGUI(bool settingsEnable)
        {
            pSettings.InvokeIfRequired(() => { pSettings.Enabled = settingsEnable; });
            pProgress.InvokeIfRequired(() => { pProgress.Enabled = !settingsEnable; });

            lblProgress.InvokeIfRequired(() => { lblProgress.Enabled = !settingsEnable; });
            lblRemained.InvokeIfRequired(() => { lblRemained.Enabled = !settingsEnable; });

            lblProgress.InvokeIfRequired(() => { lblProgress.Visible = !settingsEnable; });
            lblRemained.InvokeIfRequired(() => { lblRemained.Visible = !settingsEnable; });

            btnParse.InvokeIfRequired(() => { btnParse.Enabled = settingsEnable; });
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
            DataGridViewColumn column = null;

            try
            {
                dgv.Invoke((MethodInvoker)(() => column = dgv.Columns[cbSourceColumn.SelectedItem.ToString()]));

                pbProgress.Invoke((MethodInvoker)(() => pbProgress.Maximum = dgv.Rows.Count));

                DateTime startTime = DateTime.Now;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    var cell = dgv[column.Index, i];
                    string value = cell.Value?.ToString();

                    if (string.IsNullOrWhiteSpace(value)) continue;

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

                        if (!string.IsNullOrEmpty(dgv[MainForm.REGION_FIELD, i].Value?.ToString()))
                        {
                            dgv[MainForm.DISTRICT_FIELD, i].Value = parser.GetDistrictByRegion(dgv[MainForm.REGION_FIELD, i].Value.ToString());
                        }

                        if (!string.IsNullOrEmpty(dgv[MainForm.REGION_FIELD, i].Value?.ToString())
                            && !string.IsNullOrEmpty(dgv[MainForm.STREET_FIELD, i].Value?.ToString())
                            && string.IsNullOrEmpty(dgv[MainForm.CITY_FIELD, i].Value?.ToString()))
                        {
                            dgv[MainForm.CITY_FIELD, i].Value = dgv[MainForm.REGION_FIELD, i].Value;
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
                    }
                    else
                    {
                        dgv[MainForm.STATUS_FIELD, i].Value = "GOOD";
                    }

                    sb.Clear();
                    sb.Append("Необходимо исправлять: ");
                    sb.Append(countToFix);
                    sb.Append('/');
                    sb.Append(i + 1);
                    lblCountToFix.Invoke((MethodInvoker)(() => lblCountToFix.Text = sb.ToString()));

                }
                Parsed = true;
                MessageBox.Show("Обработка завершена.");
                SwitchGUI(true);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
