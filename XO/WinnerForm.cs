using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XO
{
    public partial class WinnerForm : Form
    {
        private string _formText;
        Form1 _opener;

        public WinnerForm(Form1 opener, string formText)
        {
            _formText = formText;
            _opener = opener;

            InitializeComponent();
        }

        private void WinnerForm_Load(object sender, EventArgs e)
        {
            label1.Text = _formText;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
            _opener.newGame();
        }

        private void WinnerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_opener.HaveWinner)
            {
                if (MessageBox.Show("Вы уверены, что хотите выйти?", "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Dispose();
                    _opener.Dispose();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
