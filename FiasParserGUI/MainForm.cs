using FiasParserLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace FiasParserGUI
{
    public partial class MainForm : Form
    {
        private FiasParser parser;
        private static object forLock = new object();

        private bool fileOpened;
        private string openedFilePath;
        private bool hasUnsavedChanges;


        public const string DISTRICT_FIELD = "Округ";
        public const string REGION_FIELD = "Область";
        public const string CITY_FIELD = "Населённый пункт";
        public const string STREET_FIELD = "Улица";
        public const string HOUSE_FIELD = "Дом";
        public const string STATUS_FIELD = "Статус";

        public readonly string[] CITIES_LIKE_REGION = { "г Москва", "г Санкт-Петербург", "г Барнаул","г Севастополь" };


        public MainForm(FiasParser parser)
        {
            InitializeComponent();
            this.parser = parser;

            fileOpened = false;
            hasUnsavedChanges = false;

            dgvContent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvContent.AllowUserToDeleteRows = false;
            dgvContent.AllowUserToAddRows = false;
            dgvContent.AllowUserToOrderColumns = false;
            dgvContent.RowHeadersWidth = 70;


            foreach (var o in GetObjectsByParent(null))
            {
                cbRegion.Items.Add(o);
            }

            SwitchGUI(false);
        }


        private void Save(DataGridView dgv, string path)
        {
            dgv.MultiSelect = true;
            // Copy DataGridView results to clipboard
            dgv.SelectAll();
            DataObject dataObj = dgvContent.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);

            object misValue = System.Reflection.Missing.Value;
            Excel.Application xlexcel = new Excel.Application
            {
                DisplayAlerts = false // Without this you will get two confirm overwrite prompts
            };
            Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
            Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            // Format column as text before pasting results, this was required for my data
            var rangeColumnsNames = new string[]
            {
                    "A:A",
                    "B:B",
                    "C:C",
                    "D:D",
                    "E:E",
                    "F:F",
                    "G:G",
                    "H:H",
                    "I:I",
                    "G:G",
                    "K:K",
                    "L:L",
                    "M:M",
                    "N:N",
                    "O:O",
                    "P:P",
                    "Q:Q",
                    "R:R",
                    "S:S",
                    "T:T",
                    "U:U",
                    "V:V",
                    "W:W",
                    "X:X",
                    "Y:Y",
                    "Z:Z"
            };
            Excel.Range rng = xlWorkSheet.get_Range(rangeColumnsNames[dgv.Columns.Count]).Cells;
            rng.NumberFormat = "@";

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                var column = dgv.Columns[i];

                xlWorkSheet.Cells[1, i + 2] = column.HeaderCell.Value.ToString();
            }

            // Paste clipboard results to worksheet range
            Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[2, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            // For some reason column A is always blank in the worksheet. ¯\_(ツ)_/¯
            // Delete blank column A and select cell A1
            Excel.Range delRng = xlWorkSheet.get_Range("A:A").Cells;
            delRng.Delete(Type.Missing);
            xlWorkSheet.get_Range("A1").Select();

            // Save the excel file under the captured location from the SaveFileDialog
            xlWorkBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlexcel.DisplayAlerts = true;
            xlWorkBook.Close(true, misValue, misValue);
            xlexcel.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlexcel);

            // Clear Clipboard and DataGridView selection
            Clipboard.Clear();
            dgv.ClearSelection();
            dgv.MultiSelect = false;
            hasUnsavedChanges = false;
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при попытке освободить обьект. " + ex.ToString());
            }
            finally
            {
                obj = null;
                GC.Collect();
            }
        }

        private void miOpen_Click(object sender, EventArgs e)
        {
            if (fileOpened && !TryCloseFile()) return;

            OpenFileDialog ofd = new OpenFileDialog
            {
                DefaultExt = "*.xls;*.xlsx",
                Filter = "Excel 2003(*.xls)|*.xls|Excel 2007(*.xlsx)|*.xlsx",
                Title = "Выберите документ для загрузки данных"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                    ofd.FileName +
                                    ";Extended Properties='Excel 12.0 XML;HDR=YES;IMEX=1';";

                    System.Data.OleDb.OleDbConnection con =
                        new System.Data.OleDb.OleDbConnection(constr);
                    con.Open();

                    var inputForm = new InputForm("Введите название листа.");
                    inputForm.ShowDialog();

                    string select = String.Format("SELECT * FROM [{0}$]", inputForm.InputText);

                    System.Data.OleDb.OleDbDataAdapter ad =
                        new System.Data.OleDb.OleDbDataAdapter(select, con);

                    DataTable dt = new DataTable();
                    ad.Fill(dt);

                    dgvContent.DataSource = dt;

                    foreach (DataGridViewColumn column in dgvContent.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    for (int i = 0; i < dgvContent.Rows.Count; i++)
                    {
                        dgvContent.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    con.Close();

                    fileOpened = true;
                    openedFilePath = ofd.FileName;

                    SwitchGUI(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Произошла ошибка при открытии файла.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public static bool IsHasEmptyField(DataGridView dgv, int rowIndex)
        {
            if (string.IsNullOrEmpty(dgv[DISTRICT_FIELD, rowIndex].Value?.ToString())
                || string.IsNullOrEmpty(dgv[REGION_FIELD, rowIndex].Value?.ToString())
                  || string.IsNullOrEmpty(dgv[CITY_FIELD, rowIndex].Value?.ToString())
                  || string.IsNullOrEmpty(dgv[STREET_FIELD, rowIndex].Value?.ToString())
                  || string.IsNullOrEmpty(dgv[HOUSE_FIELD, rowIndex].Value?.ToString())) return true;
            return false;
        }

        /// <summary>
        /// Закрывает открытый файл программы.
        /// </summary>
        /// <returns>Возвращает true, если файл был закрыт, иначе false.</returns>
        private bool TryCloseFile()
        {
            var cautionMessage = "Имеются несохранённые изменения!\nВы точно хотите выйти?";
            if (hasUnsavedChanges &&
                MessageBox.Show(cautionMessage, "Предупреждение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
                != DialogResult.Yes) return false;

            dgvContent.Columns.Clear();

            ClearSideBar();

            SwitchGUI(false);

            fileOpened = false;
            hasUnsavedChanges = false;

            return true;
        }

        private void ClearSideBar()
        {
            tbDistrict.Clear();
            cbRegion.SelectedIndex = 0;
            cbCity.Items.Clear();
            cbStreet.Items.Clear();
            cbHouse.Items.Clear();
            lblStatus.Text = "Статус: ";
            lblRowIndex.Text = "Номер строки: ";
        }

        private List<ObjectNode> GetObjectsByParent(string parentGuid)
        {
            List<ObjectNode> res = new List<ObjectNode>();

            lock (forLock)
            {
                if (parentGuid != null)
                {
                    res = (from o in parser.DataContext.Object
                           where o.PARENTGUID == parentGuid && o.ACTSTATUS == 1
                           select new ObjectNode(o.AOGUID, o.FORMALNAME, TableType.Object, parentGuid, null, o.SHORTNAME, o.AOLEVEL)).ToList();

                }
                else
                {
                    res = (from o in parser.DataContext.Object
                           where o.PARENTGUID == null && o.ACTSTATUS == 1
                           select new ObjectNode(o.AOGUID, o.FORMALNAME, TableType.Object, parentGuid, null, o.SHORTNAME, o.AOLEVEL)).ToList();
                }
            }
            return res;
        }


        private void SwitchGUI(bool fileOpened)
        {
            dgvContent.Enabled = fileOpened;

            if (fileOpened)
            {
                if (CheckTableForParsedData(dgvContent))
                {
                    miSendToDB.Enabled = true;
                }
                else
                {
                    miSendToDB.Enabled = false;
                }
            }
            else
            {
                pSide.Enabled = false;
                gbAddress.Enabled = false;
            }

            miProccessing.Enabled = fileOpened;
            miClose.Enabled = fileOpened;
            miSave.Enabled = fileOpened;
            miSaveAs.Enabled = fileOpened;
        }

        private bool CheckTableForParsedData(DataGridView dgv)
        {
            var c = dgv.Columns;
            if (c.Contains(DISTRICT_FIELD) && c.Contains(REGION_FIELD) && c.Contains(CITY_FIELD) && c.Contains(STREET_FIELD)
                && c.Contains(HOUSE_FIELD) && c.Contains(STATUS_FIELD)) return true;
            return false;
        }

        private void miClose_Click(object sender, EventArgs e) => TryCloseFile();

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!TryCloseFile()) e.Cancel = true;
            else
            {
                parser.Close();
            }
            try
            {
                Application.Exit();
            }
            catch
            {

            }
        }

        private void miSaveAs_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Excel 2003(*.xls)|*.xls|Excel 2007(*.xlsx)|*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Save(dgvContent, sfd.FileName);
                openedFilePath = sfd.FileName;
            }
        }

        private void miSave_Click(object sender, EventArgs e) => Save(dgvContent, openedFilePath);

        private void miFiasParse_Click(object sender, EventArgs e)
        {
            var parseForm = new FiasParserForm(parser, dgvContent);
            parseForm.ShowDialog();

            if (parseForm.Parsed)
            {
                hasUnsavedChanges = true;
                foreach (DataGridViewColumn c in dgvContent.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                SwitchGUI(true);
            }
            parseForm.Dispose();
        }

        private void dgvContent_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvContent.SelectedCells.Count > 0 && CheckTableForParsedData(dgvContent))
            {
                var rowIndex = dgvContent.SelectedCells[0].RowIndex;
                lblRowIndex.Text = "Номер строки: " + (rowIndex + 1).ToString();
                lblStatus.Text = "Статус: " + dgvContent[STATUS_FIELD, rowIndex].Value?.ToString();

                cbCity.Items.Clear();
                cbStreet.Items.Clear();
                cbHouse.Items.Clear();

                cbRegion.Text = dgvContent[REGION_FIELD, rowIndex].Value?.ToString(); 

                cbCity.Text = dgvContent[CITY_FIELD, rowIndex].Value?.ToString();
                cbStreet.Text = dgvContent[STREET_FIELD, rowIndex].Value?.ToString();
                cbHouse.Text = dgvContent[HOUSE_FIELD, rowIndex].Value?.ToString();
                //когда мы мначинаем менять текст в tbRegion, остальные не успеваюь поменяться, поэтому баг        pSide.Enabled = true;
                gbAddress.Enabled = true;
                pSide.Enabled = true;
            }
            else
            {
                ClearSideBar();
                gbAddress.Enabled = false;
                pSide.Enabled = false;
            }
        }


        private void MoveNextProblem(DataGridView dgv)
        {
            int startRowIndex = 0;
            if (dgv.SelectedCells.Count > 0) startRowIndex = dgv.SelectedCells[0].RowIndex + 1;
            else
            {
                startRowIndex = dgv.FirstDisplayedScrollingRowIndex;
            }

            if (startRowIndex > dgv.Rows.Count - 1) return;

            bool finded = false;
            for (int i = startRowIndex; i < dgv.Rows.Count; i++)
            {
                if (dgv[STATUS_FIELD, i].Value?.ToString() == "PROBLEMS")
                {
                    dgv.FirstDisplayedScrollingRowIndex = i;
                    dgv.Rows[i].Selected = true;
                    finded = true;
                    SetFixedIfNotEmptyCurrentSelected();
                    break;
                }
            }
            if (!finded) MessageBox.Show("Проблемных строк, начиная со строки " + startRowIndex.ToString() + ", больше не найдено.");
        }


        private void btnForwardNext_Click(object sender, EventArgs e) => MoveNextProblem(dgvContent);

        private void cbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvContent.SelectedCells.Count > 0)
            {
                dgvContent[DISTRICT_FIELD, dgvContent.SelectedCells[0].RowIndex].Value = tbDistrict.Text;
                hasUnsavedChanges = true;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            if (SetFixedIfNotEmptyCurrentSelected()) MoveNextProblem(dgvContent);
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (CheckTableForParsedData(dgvContent)&&dgvContent.SelectedCells.Count > 0)
            {
                int rowIndex = dgvContent.SelectedCells[0].RowIndex;

                SetIfEquals(sender, tbDistrict, DISTRICT_FIELD, rowIndex);
                SetIfEquals(sender, cbRegion, REGION_FIELD, rowIndex);
                SetIfEquals(sender, cbCity, CITY_FIELD, rowIndex);
                SetIfEquals(sender, cbStreet, STREET_FIELD, rowIndex);
                SetIfEquals(sender, cbHouse, HOUSE_FIELD, rowIndex);



                if (!string.IsNullOrWhiteSpace(dgvContent[DISTRICT_FIELD,rowIndex].Value?.ToString())
                    && !string.IsNullOrWhiteSpace(dgvContent[REGION_FIELD, rowIndex].Value?.ToString())
                    && !string.IsNullOrWhiteSpace(dgvContent[CITY_FIELD, rowIndex].Value?.ToString())
                    && !string.IsNullOrWhiteSpace(dgvContent[STREET_FIELD, rowIndex].Value?.ToString())
                    && !string.IsNullOrWhiteSpace(dgvContent[HOUSE_FIELD, rowIndex].Value?.ToString())
                    && dgvContent[STATUS_FIELD, rowIndex].Value?.ToString() == "PROBLEMS")
                {
                    dgvContent[STATUS_FIELD, dgvContent.SelectedCells[0].RowIndex].Value = "FIXED";
                }

                hasUnsavedChanges = true;
            }
        }

        private void SetIfEquals(object sender, Control t, string columnName, int rowIndex)
        {
            if (sender == t && t.Text!=null)
            {
                dgvContent[columnName, rowIndex].Value = t.Text;
            }
        }

        private bool SetFixedIfNotEmptyCurrentSelected()
        {
            if (FocusIfEmpty(cbRegion)) return false;
            if (FocusIfEmpty(cbCity)) return false;
            if (FocusIfEmpty(cbStreet)) return false;
            if (FocusIfEmpty(cbHouse)) return false;

            return true;
        }

        private bool FocusIfEmpty(ComboBox c)
        {
            if (string.IsNullOrWhiteSpace(c.Text))
            {
                c.Select();
                return true;
            }
            return false;
        }

        private void miSendToDB_Click(object sender, EventArgs e)
        {
            var sendForm = new SendToDbForm(parser.DataContext, dgvContent);
            sendForm.ShowDialog();
        }

        private void cbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            var region = cbRegion.SelectedItem as ObjectNode;
            if (cbRegion.SelectedIndex == -1 || region == null) return;

            cbCity.SelectedIndex = -1;

            string district = null;
            lock (forLock)
            {
                district = (from d in parser.DataContext.District
                            where d.Region == region.Name
                            select d.District1).FirstOrDefault();

            tbDistrict.Text = district;

            Thread th = new Thread(SetCityByRegion);
            th.Start();
        }
        }

        private void SetCityByRegion()
        {

            ObjectNode region = null;
            cbRegion.Invoke((MethodInvoker)(() => region = cbRegion.SelectedItem as ObjectNode));

            if (region == null) return;

            cbCity.Invoke((MethodInvoker)(() => cbCity.Items.Clear()));
            cbCity.Invoke((MethodInvoker)(() => cbCity.SelectedIndex = -1));

            lblLoading.Invoke((MethodInvoker)(() => lblLoading.Visible = true));
            lblLoading.Invoke((MethodInvoker)(() => lblLoading.Text = "Загрузка всех городов в " + region.ToString() + "."));


            var finded = new List<ObjectNode>();
            foreach (var f in GetObjectsByParent(region.Guid))
            {
                if (f.AOLevel == 3)
                {
                    foreach (var city in GetObjectsByParent(f.Guid))
                    {
                        finded.Add(city);
                    }
                }
                else
                {
                    finded.Add(f);
                }
            }

            if (CITIES_LIKE_REGION.Contains(region.ToString()))
            {
                for (int i = 0; i < finded.Count; i++)
                {
                    var cur = finded[i] as ObjectNode;

                    if (cur?.AOLevel == 4 || cur?.AOLevel == 3)
                    {
                        cbCity.Invoke((MethodInvoker)(() => cbCity.Items.Add(cur)));
                    }
                }

                cbCity.Invoke((MethodInvoker)(() => cbCity.Items.Add(region)));
            }
            else
            {
                foreach (var item in finded)
                {
                    cbCity.Invoke((MethodInvoker)(() => cbCity.Items.Add(item)));
                }
            }
            lblLoading.Invoke((MethodInvoker)(() => lblLoading.Visible = false));
        }

        private void SetStreetsByCity()
        {
            ObjectNode city = null;
            cbCity.Invoke((MethodInvoker)(() => city = cbCity.SelectedItem as ObjectNode));

            if (city == null) return;

            cbStreet.Invoke((MethodInvoker)(() => cbStreet.Items.Clear()));
            cbStreet.Invoke((MethodInvoker)(() => cbStreet.SelectedIndex = -1));

            lblLoading.Invoke((MethodInvoker)(() => lblLoading.Visible = true));
            lblLoading.Invoke((MethodInvoker)(() => lblLoading.Text = "Загрузка всех улиц в " + city.ToString() + "."));

            foreach (var f in GetObjectsByParent(city.Guid))
            {
                if (f.AOLevel > 6)
                    cbStreet.Invoke((MethodInvoker)(() => cbStreet.Items.Add(f)));
            }

            lblLoading.Invoke((MethodInvoker)(() => lblLoading.Visible = false));
        }

        private void SetHousesByStreet()
        {
            try
            {
                ObjectNode street = null;
                cbStreet.Invoke((MethodInvoker)(() => street = cbStreet.SelectedItem as ObjectNode));

                if (street == null) return;

                cbHouse.Invoke((MethodInvoker)(() => cbHouse.Items.Clear()));
                cbHouse.Invoke((MethodInvoker)(() => cbHouse.SelectedIndex = -1));

                lblLoading.Invoke((MethodInvoker)(() => lblLoading.Visible = true));
                lblLoading.Invoke((MethodInvoker)(() => lblLoading.Text = "Загрузка всех домов на " + street.ToString() + "."));

                lock (forLock)
                {
                    var houses = (from h in parser.DataContext.House
                                  where h.AOGUID == street.Guid
                                  select new ObjectNode(h.HOUSEGUID, h.HOUSENUM, TableType.House, street.Guid, street)
                             ).ToList().Distinct(new ObjectNode.ByNameComparer());

                    foreach (var h in houses)
                    {
                        cbHouse.Invoke((MethodInvoker)(() => cbHouse.Items.Add(h)));
                    }
                }

                lblLoading.Invoke((MethodInvoker)(() => lblLoading.Visible = false));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " [MainForm.cs, method: SetHousesByStreet]");
            }
        }

        private void cbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCity.SelectedIndex == -1) return;

            Thread th = new Thread(SetStreetsByCity);
            th.Start();
        }

        private void cbStreet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStreet.SelectedIndex == -1) return;
            Thread th = new Thread(SetHousesByStreet);
            th.Start();
        }
    }
}
