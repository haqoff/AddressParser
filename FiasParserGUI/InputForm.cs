using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiasParserGUI
{
    public partial class InputForm : Form
    {
        public string InputText { get; private set; }
        public InputForm(string caption)
        {
            InitializeComponent();
            Text = caption; 
        }


        private void btnOK_Click(object sender, EventArgs e) => Confirm();

        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Confirm();
        }

        private void Confirm()
        {
            InputText = tbInput.Text;
            Close();
        }

    }
}
