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

        public WinnerForm(string formText)
        {
            _formText = formText;
            InitializeComponent();
        }

        private void WinnerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
