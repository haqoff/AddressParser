using FiasParserLib;
using System;
using System.Windows.Forms;

namespace FiasParserGUI
{
    public partial class SendToDbForm : Form
    {
        private FiasClassesDataContext dataContext;
        private DataGridView dgv;

        public SendToDbForm(FiasClassesDataContext dataContext, DataGridView dgv)
        {
            InitializeComponent();
            this.dataContext = dataContext;
            this.dgv = dgv;

            foreach (DataGridViewColumn c in dgv.Columns)
            {
                cbChainNameColumn.Items.Add(c.Name);
                cbShopCodeColumn.Items.Add(c.Name);
            }
        }

        private void btnSend_Click(object sender, System.EventArgs e)
        {
            cbChainNameColumn.Enabled = false;
            cbShopCodeColumn.Enabled = false;
            btnSend.Enabled = false;
            lblLoading.Visible = true;

            try
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (string.IsNullOrWhiteSpace(dgv[cbChainNameColumn.Text, i].Value?.ToString())
                        || string.IsNullOrWhiteSpace(dgv[cbShopCodeColumn.Text, i].Value?.ToString())) continue;

                    var dim = new DimShops()
                    {
                        ChainName = dgv[cbChainNameColumn.Text, i].Value?.ToString(),
                        ShopCode = dgv[cbShopCodeColumn.Text, i].Value?.ToString(),
                        District = dgv[MainForm.DISTRICT_FIELD, i].Value?.ToString(),
                        Region = dgv[MainForm.REGION_FIELD, i].Value?.ToString(),
                        City = dgv[MainForm.CITY_FIELD, i].Value?.ToString(),
                        Street = dgv[MainForm.STREET_FIELD, i].Value?.ToString(),
                        House = dgv[MainForm.HOUSE_FIELD, i].Value?.ToString()
                    };

                    dataContext.DimShops.InsertOnSubmit(dim);
                }
                dataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cbChainNameColumn.Enabled = true;
                cbShopCodeColumn.Enabled = true;
                btnSend.Enabled = true;
                lblLoading.Visible = false;
            }
        }
    }
}
