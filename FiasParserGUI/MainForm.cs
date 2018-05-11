using FiasParserLib;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace FiasParserGUI
{
    public partial class MainForm : Form
    {
        private FiasParser parser;

        private bool fileOpened;
        private string openedFilePath;
        private bool hasUnsavedChanges;

        public const string DISTRICT_FIELD = "Округ";
        public const string REGION_FIELD = "Область";
        public const string CITY_FIELD = "Населённый пуsнкт";
        public const string STREET_FIELD = "Улица";
        public const string HOUSE_FIELD = "Дом";
        public const string STATUS_FIELD = "Статус";


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

            var districts = (from d in parser.DataContext.District
                             select d.District1).ToList().Distinct();
            foreach (var d in districts)
            {
                cbDistrict.Items.Add(d);
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
            cbDistrict.SelectedIndex = 0;
            tbRegion.Clear();
            tbCity.Clear();
            tbStreet.Clear();
            tbHouse.Clear();
            lblStatus.Text = "Статус: ";
            lblRowIndex.Text = "Номер строки: ";
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

                try
                {
                    cbDistrict.SelectedItem = dgvContent[DISTRICT_FIELD, rowIndex].Value?.ToString();
                }
                catch
                {
                    cbDistrict.SelectedIndex = 0;
                    dgvContent[DISTRICT_FIELD, rowIndex].Value = cbDistrict.Text;
                }
                tbRegion.Text = dgvContent[REGION_FIELD, rowIndex].Value?.ToString();
                tbCity.Text = dgvContent[CITY_FIELD, rowIndex].Value?.ToString();
                tbStreet.Text = dgvContent[STREET_FIELD, rowIndex].Value?.ToString();
                tbHouse.Text = dgvContent[HOUSE_FIELD, rowIndex].Value?.ToString();
                //когда мы мначинаем менять текст в tbRegion, остальные не успеваюь поменяться, поэтому баг



                pSide.Enabled = true;
                gbAddress.Enabled = true;
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
                dgvContent[DISTRICT_FIELD, dgvContent.SelectedCells[0].RowIndex].Value = cbDistrict.SelectedItem.ToString();
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
            if (dgvContent.SelectedCells.Count > 0)
            {
                int rowIndex = dgvContent.SelectedCells[0].RowIndex;

                SetIfEquals(sender, tbRegion, REGION_FIELD, rowIndex);
                SetIfEquals(sender, tbCity, CITY_FIELD, rowIndex);
                SetIfEquals(sender, tbStreet, STREET_FIELD, rowIndex);
                SetIfEquals(sender, tbHouse, HOUSE_FIELD, rowIndex);

                if (!string.IsNullOrWhiteSpace(cbDistrict.Text)
                    && !string.IsNullOrWhiteSpace(tbRegion.Text)
                    && !string.IsNullOrWhiteSpace(tbCity.Text)
                    && !string.IsNullOrWhiteSpace(tbStreet.Text)
                    && !string.IsNullOrWhiteSpace(tbHouse.Text)
                    && dgvContent[STATUS_FIELD, rowIndex].Value?.ToString() == "PROBLEMS")
                {
                    dgvContent[STATUS_FIELD, dgvContent.SelectedCells[0].RowIndex].Value = "FIXED";
                }

                hasUnsavedChanges = true;
            }
        }

        private void SetIfEquals(object sender, TextBox t, string columnName, int rowIndex)
        {
            if (sender == t)
            {
                dgvContent[columnName, rowIndex].Value = t.Text;
            }
        }

        private bool SetFixedIfNotEmptyCurrentSelected()
        {
            if (FocusIfEmpty(cbDistrict)) return false;
            if (FocusIfEmpty(tbRegion)) return false;
            if (FocusIfEmpty(tbCity)) return false;
            if (FocusIfEmpty(tbStreet)) return false;
            if (FocusIfEmpty(tbHouse)) return false;

            dgvContent[STATUS_FIELD, dgvContent.SelectedCells[0].RowIndex].Value = "FIXED";

            return true;
        }

        private bool FocusIfEmpty(Control c)
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
    }
}


