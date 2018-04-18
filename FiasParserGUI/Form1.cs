using FiasParserLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace FiasParserGUI
{
    public partial class MainForm : Form
    {
        private const string GUID_FIELD = "GUID";
        private const string PARSEDNAME_FIELD = "PARSEDNAME";
        private const string IDTYPE_FIELD = "ID TYPE";
        private const string LAST_FINDED_OBJECT_FIELED = "Last Finded Name";



        private string path;
        private DataTable table;
        private List<ComboBox> comboBoxes;
        private Dictionary<ComboBox, Result> AOGUID_dic;
        private FiasParser parser;

        private string saveText;
        private string saveParentGuid;
        private int saveAOLEVEL;
        private ComboBox saveCB;


        public MainForm()
        {
            InitializeComponent();
            AOGUID_dic = new Dictionary<ComboBox, Result>();
            comboBoxes = new List<ComboBox>();
            try
            {
                parser = new FiasParser();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось присоединиться к БД.", "Ошибка");
            }

        }

        private void btnParseFile_Click(object sender, EventArgs e)
        {
            if (LoadFromFile())
            {
                table.Columns.Add(PARSEDNAME_FIELD, typeof(System.String));
                table.Columns.Add(IDTYPE_FIELD, typeof(System.String));
                table.Columns.Add(GUID_FIELD, typeof(System.String));
                table.Columns.Add(LAST_FINDED_OBJECT_FIELED, typeof(System.String));
                Thread th = new Thread(DoParse);
                th.Start();
            }
        }

        private bool LoadToGridView()
        {
            if (tbSheetName.Text == "") return false;

            var connectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties=\"Excel 8.0;HDR=Yes;\";", path);
            var queryString = String.Format("Select * from [{0}$]", tbSheetName.Text);

            try
            {
                var connection = new OleDbConnection(connectionString);
                var dataAdapter = new OleDbDataAdapter(queryString, connection);
                table = new DataTable();
                dataAdapter.Fill(table);
                dgvContent.DataSource = table;

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dgvContent.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }

                dgvContent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                Text = "Парсер адресов, загружено: " + path;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void DoParse()
        {
            if (table == null) return;

            const int columnIndex = 0;

            int countUknown = 0;

            DateTime startTime = DateTime.Now;

            pbParse.Invoke(new Action(() => { pbParse.Maximum = table.Rows.Count; }));

            for (int i = 0; i < table.Rows.Count; i++)
            {
                try
                {
                    string value = table.Rows[i][columnIndex].ToString();
                    ParseResult r = parser.Parse(value);

                    table.Rows[i][PARSEDNAME_FIELD] = r.address;
                    table.Rows[i][IDTYPE_FIELD] = r.type.ToString();
                    table.Rows[i][GUID_FIELD] = r.id;
                    table.Rows[i][LAST_FINDED_OBJECT_FIELED] = r.lastFindedName;
                }
                catch (Exception ex)
                {
                    table.Rows[i][PARSEDNAME_FIELD] = ex.Message;
                    countUknown++;
                }

                //progress call
                TimeSpan timeRemaining = TimeSpan.FromTicks((long)(DateTime.Now.Subtract(startTime).Ticks * (table.Rows.Count - (i + 1)) / (float)(i + 1)));

                pbParse.Invoke(new Action(() => { lblRemaingTime.Text = timeRemaining.ToString(); }));
                pbParse.Invoke(new Action(() => { pbParse.Increment(1); }));
                pbParse.Invoke(new Action(() => { lblProgress.Text = (i + 1).ToString() + "/" + table.Rows.Count; }));

            }
            dgvContent.Invoke(new Action(() => { dgvContent.DataSource = table; }));

            pbParse.Invoke(new Action(() => { lblCountUknown.Text = "Кол-во неизвестных: " + countUknown; }));
            MessageBox.Show("всё");
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;

            var index = comboBoxes.IndexOf(cb);

            if (cb.SelectedIndex > -1 && cb.SelectedIndex < AOGUID_dic[cb].objects.Count)
            {
                textBox1.Text = AOGUID_dic[cb].objects[cb.SelectedIndex].AOGUID;
            }
            else if (cb.SelectedIndex > AOGUID_dic[cb].objects.Count - 1 
                && cb.SelectedIndex < AOGUID_dic[cb].objects.Count + AOGUID_dic[cb].houses.Count)
            {
                textBox1.Text = AOGUID_dic[cb].houses[cb.SelectedIndex - AOGUID_dic[cb].objects.Count].HOUSEGUID;
            }
            else if (cb.SelectedIndex > AOGUID_dic[cb].objects.Count + AOGUID_dic[cb].houses.Count - 1 
                && cb.SelectedIndex < AOGUID_dic[cb].objects.Count + AOGUID_dic[cb].houses.Count + AOGUID_dic[cb].rooms.Count)
            {
                textBox1.Text = AOGUID_dic[cb].rooms[cb.SelectedIndex - AOGUID_dic[cb].objects.Count 
                    - AOGUID_dic[cb].houses.Count].ROOMGUID;
            }


        }
        private void Find()
        {
            var guids = new Result();
            foreach (var item in parser.SelectAllObject(saveText, saveParentGuid, saveAOLEVEL))
            {
                bool alreadyFinded = false;

                foreach (var okm in AOGUID_dic[saveCB].objects)
                {
                    if (okm.AOLEVEL == item.AOLEVEL) alreadyFinded = true;
                }
                if (!alreadyFinded)
                {
                    guids.objects.Add(item);
                    saveCB.Invoke(new Action(() => { saveCB.Items.Add(item.FORMALNAME + " " + item.SHORTNAME); }));
                }
            }
            if (saveAOLEVEL > 4 && saveAOLEVEL != 8)
                foreach (var house in parser.SelectAllHouses(saveParentGuid, saveText))
                {
                    guids.houses.Add(house);
                    saveCB.Invoke(new Action(() => { saveCB.Items.Add(house.ToString()); }));
                }

            if (saveAOLEVEL == 8)
                foreach (var room in parser.SelectAllRoom(saveParentGuid, saveText))
                {
                    guids.rooms.Add(room);
                    saveCB.Invoke(new Action(() => { saveCB.Items.Add("комната: " + room.FLATNUMBER); }));
                }


            AOGUID_dic[saveCB] = guids;

            //todo
            if (guids.objects?.Count == 0 && guids.houses?.Count == 0 && guids.rooms?.Count ==0)
                lblLoading.Invoke(new Action(() => { lblLoading.Text = "-"; }));
            else lblLoading.Invoke(new Action(() => { lblLoading.Text = "Найдено: " + saveCB.Items.Count; }));

            saveCB.Invoke(new Action(() =>
            {
                if (saveCB.Items.Count > 0) saveCB.SelectedIndex = 0;
            }));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            comboBoxes.Add(comboBox1);
            comboBoxes.Add(comboBox2);
            comboBoxes.Add(comboBox3);
            comboBoxes.Add(comboBox4);
            comboBoxes.Add(comboBox5);
            comboBoxes.Add(comboBox6);

            foreach (var item in comboBoxes)
            {
                AOGUID_dic.Add(item, new Result());
            }
        }

        private void comboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var cb = sender as ComboBox;

                if (string.IsNullOrEmpty(cb.Text)) return;

                saveCB = cb;
                saveText = cb.Text;
                int index = comboBoxes.IndexOf(cb);

                saveParentGuid = null;
                saveAOLEVEL = 0;
                if (index > 0)
                {
                    var prev = comboBoxes[index - 1];
                    if (prev.SelectedIndex == -1) return;
                    if (prev.SelectedIndex > -1 && prev.SelectedIndex < AOGUID_dic[prev].objects.Count)
                    {
                        saveAOLEVEL = AOGUID_dic[prev].objects[prev.SelectedIndex].AOLEVEL;
                        saveParentGuid = AOGUID_dic[prev].objects[prev.SelectedIndex].AOGUID;
                    }

                    else if (prev.SelectedIndex > AOGUID_dic[prev].objects.Count 
                        && prev.SelectedIndex < AOGUID_dic[prev].objects.Count+ AOGUID_dic[prev].houses.Count)
                    {
                        saveParentGuid = AOGUID_dic[prev].houses[prev.SelectedIndex - AOGUID_dic[prev].objects.Count].HOUSEGUID;
                    }
                    else
                    {
                        int ind = prev.SelectedIndex - AOGUID_dic[prev].houses.Count - 1 - AOGUID_dic[prev].objects.Count - 1;
                        saveParentGuid = AOGUID_dic[prev].rooms[ind].ROOMGUID;
                    }
                }

                lblLoading.Text = "Загрузка...";
                cb.Items.Clear();
                for (int i = index + 1; i < comboBoxes.Count; i++)
                {
                    comboBoxes[i].Items.Clear();
                    comboBoxes[i].Text = "";
                }
                Thread th = new Thread(Find);
                th.Start();

            }
        }

        private void btnNextEmpty_Click(object sender, EventArgs e)
        {
            if (table == null) return;
            if (!string.IsNullOrEmpty(comboBox2.Text))
            {
                DialogResult dialogResult = MessageBox.Show("Вы точно хотите перейти к следующему?", "Предупреждение.", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No) return;
            }
            dgvContent.Focus();

            for (int i = dgvContent.FirstDisplayedScrollingRowIndex + 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                var cell = row[IDTYPE_FIELD];
                if ((string.IsNullOrEmpty(cell.ToString()) || "Object" == cell.ToString()))
                {
                    dgvContent.ClearSelection();
                    dgvContent.FirstDisplayedScrollingRowIndex = i;
                    dgvContent.Rows[i].Selected = true;

                    foreach (var c in comboBoxes)
                    {
                        c.Items.Clear();
                        c.Text = "";
                    }

                    if (row[LAST_FINDED_OBJECT_FIELED].ToString() != "")
                    {
                        comboBox1.Text = row[LAST_FINDED_OBJECT_FIELED].ToString();
                        comboBox1.Items.Add(row[LAST_FINDED_OBJECT_FIELED].ToString());
                        comboBox1.SelectedIndex = 0;
                        AOGUID_dic[comboBox1].objects.Clear();
                        AOGUID_dic[comboBox1].objects.Add(new ObjectKnownMargins()
                        {
                            AOGUID = row[GUID_FIELD].ToString(),
                            FORMALNAME = row[LAST_FINDED_OBJECT_FIELED].ToString()
                        });
                        comboBox2.Focus();
                    }

                    break;
                }

            }

        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            int lastId = 0;
            for (int i = 0; i < comboBoxes.Count; i++)
            {
                if (comboBoxes[i].SelectedIndex > -1) lastId = i;
            }
            var cb = comboBoxes[lastId];

            if (cb.SelectedIndex > -1 && cb.SelectedIndex < AOGUID_dic[cb].objects.Count)
            {
                //object
                table.Rows[dgvContent.FirstDisplayedScrollingRowIndex][GUID_FIELD] = AOGUID_dic[cb].objects[cb.SelectedIndex].AOGUID;
                table.Rows[dgvContent.FirstDisplayedScrollingRowIndex][IDTYPE_FIELD] = IdType.Object;
            }

           else if (cb.SelectedIndex >= AOGUID_dic[cb].objects.Count
                && cb.SelectedIndex < AOGUID_dic[cb].objects.Count + AOGUID_dic[cb].houses.Count)
            {
                //house
                table.Rows[dgvContent.FirstDisplayedScrollingRowIndex][GUID_FIELD] = AOGUID_dic[cb].houses[cb.SelectedIndex - AOGUID_dic[cb].objects.Count].HOUSEGUID;
                table.Rows[dgvContent.FirstDisplayedScrollingRowIndex][IDTYPE_FIELD] = IdType.House;
            }
            else
            {
                //room
                int ind = cb.SelectedIndex - AOGUID_dic[cb].houses.Count - 1 - AOGUID_dic[cb].objects.Count - 1;

                table.Rows[dgvContent.FirstDisplayedScrollingRowIndex][GUID_FIELD] = AOGUID_dic[cb].rooms[ind].ROOMGUID;
                table.Rows[dgvContent.FirstDisplayedScrollingRowIndex][IDTYPE_FIELD] = IdType.Room;
            }

            foreach (var c in comboBoxes)
            {
                c.Items.Clear();
                c.Text = "";
            }

        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            LoadFromFile();
        }

        private bool LoadFromFile()
        {
            var odf = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx"
            };


            if (odf.ShowDialog() == DialogResult.OK)
            {
                path = odf.FileName;
                if (!LoadToGridView()) return false;

                foreach (DataGridViewColumn column in dgvContent.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                return true;
            }
            return false;
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Documents (*.xls)|*.xls",
                FileName = "Inventory_Adjustment_Export.xls"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Copy DataGridView results to clipboard
                CopyAlltoClipboard();

                object misValue = System.Reflection.Missing.Value;
                Excel.Application xlexcel = new Excel.Application();

                xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // Paste clipboard results to worksheet range
                Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                // For some reason column A is always blank in the worksheet. ¯\_(ツ)_/¯
                // Delete blank column A and select cell A1
                Excel.Range delRng = xlWorkSheet.get_Range("A:A").Cells;
                delRng.Delete(Type.Missing);
                xlWorkSheet.get_Range("A1").Select();

                // Save the excel file under the captured location from the SaveFileDialog
                xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlexcel.DisplayAlerts = true;
                xlWorkBook.Close(true, misValue, misValue);
                xlexcel.Quit();

                ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlexcel);

                // Clear Clipboard and DataGridView selection
                Clipboard.Clear();
                dgvContent.ClearSelection();
            }

        }

        private void CopyAlltoClipboard()
        {
            dgvContent.SelectAll();
            DataObject dataObj = dgvContent.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}


