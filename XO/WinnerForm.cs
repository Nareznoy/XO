using System;
using System.Windows.Forms;

namespace XO
{
    public partial class WinnerForm : Form
    {
        private readonly string _formText;
        private readonly Form1 _opener;

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            Dispose();
            _opener.NewGame();
        }

        private void WinnerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_opener.HaveWinner) return;

            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Предупреждение", MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
            {
                Dispose();
                _opener.Dispose();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}