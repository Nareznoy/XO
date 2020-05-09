using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XO
{
    public partial class Form1 : Form
    {
        private int[,] _fieldArray = new int[3, 3];
        private bool _haveWinner = false;
        private bool _player = true;
        private bool[] _done = new bool[9];

        public bool HaveWinner { get { return _haveWinner; } }



        public Form1()
        {
            InitializeComponent();
        }


        private void changeButton(Button b, int i, int j)
        {
            b.Text = (_player ? "X" : "O");
            b.Update();
            _fieldArray[i, j] = (_player ? 1 : 2);
            checkWinner();
        }


        private void getRandomPoint(ref int i, ref int j)
        {
            Random rand = new Random();
            while (_fieldArray[i, j] != 0)
            {
                i = rand.Next(0, 3);
                j = rand.Next(0, 3);
            }
        }


        private void checkWinner()
        {
            int val = (_player ? 1 : 2);
            for (int i = 0; i < _done.Length; i++) 
                _done[i] = true;
            for (int i = 0; i < 3; i++)
            {
                if (_fieldArray[i, i] != val) _done[6] = false;
                if (_fieldArray[i, 2 - i] != val) _done[7] = false;
                for (int j = 0; j < 3; j++)
                {
                    if (_fieldArray[i, j] != val) _done[i] = false;
                    if (_fieldArray[j, i] != val) _done[i + 3] = false;
                    if (_fieldArray[i, j] == 0) _done[8] = false;
                }
            }
            for (int i = 0; i < _done.Length; i++)
            {
                if (_done[i] == true)
                {
                    _haveWinner = true;

                    changeButtonsStatus(false);
                }
            }
        }


        private void showWinnerWindow()
        {
            for (int i = 0; i < _done.Length; i++)
            {
                if (i == 8)
                {
                    WinnerForm winnerForm = new WinnerForm(this, "Ничья!");
                    winnerForm.ShowDialog();
                    return;
                }
                if (!_player)
                {
                    if (checkBox1.Checked)
                    {
                        WinnerForm winnerForm = new WinnerForm(this, "Вы выиграли!");
                        winnerForm.ShowDialog();
                        return;
                    }
                    else
                    {
                        WinnerForm winnerForm = new WinnerForm(this, "Игрок X выиграл!");
                        winnerForm.ShowDialog();
                        return;
                    }
                }
                else
                {
                    if (checkBox1.Checked)
                    {
                        WinnerForm winnerForm = new WinnerForm(this, "Вы проиграли!");
                        winnerForm.ShowDialog();
                        return;
                    }
                    else
                    {
                        WinnerForm winnerForm = new WinnerForm(this, "Игрок O выиграл!");
                        winnerForm.ShowDialog();
                        return;
                    }
                }
            }
        }


        public void newGame()
        {
            clearField();
            changeButtonsStatus(true);
            _haveWinner = false;
            _player = true;
        }


        private void clickedButton(Button button, int i, int j)
        {
            if (_fieldArray[i, j] == 0)
            {
                changeButton(button, i, j);
                if ((checkBox1.Checked) && (!_haveWinner))
                {
                    getRandomPoint(ref i, ref j);
                    button = getButton(i, j);

                    _player = false;
                    changeButton(button, i, j);
                    _player = true;
                }
                else
                {
                    _player = !_player;
                }
                if (_haveWinner)
                {
                    showWinnerWindow();
                }
            }
        }


        private void button0_0_Click(object sender, EventArgs e) { clickedButton((Button)sender, 0, 0); }
        private void button0_1_Click(object sender, EventArgs e) { clickedButton((Button)sender, 0, 1); }
        private void button0_2_Click(object sender, EventArgs e) { clickedButton((Button)sender, 0, 2); }
        private void button1_0_Click(object sender, EventArgs e) { clickedButton((Button)sender, 1, 0); }
        private void button1_1_Click(object sender, EventArgs e) { clickedButton((Button)sender, 1, 1); }
        private void button1_2_Click(object sender, EventArgs e) { clickedButton((Button)sender, 1, 2); }
        private void button2_0_Click(object sender, EventArgs e) { clickedButton((Button)sender, 2, 0); }
        private void button2_1_Click(object sender, EventArgs e) { clickedButton((Button)sender, 2, 1); }
        private void button2_2_Click(object sender, EventArgs e) { clickedButton((Button)sender, 2, 2); }


        private void changeButtonsStatus(bool status)
        {
            for (int k = 0; k < 3; k++)
            {
                for (int j = 0; j < 3; j++)
                {
                    (tableLayoutPanel1.Controls[string.Format("button{0}_{1}", k, j)] as Button).Enabled = status;
                }
            }
        }


        private void clearField()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _fieldArray[i, j] = 0;
                }
            }

            for (int k = 0; k < 3; k++)
            {
                for (int j = 0; j < 3; j++)
                {
                    (tableLayoutPanel1.Controls[string.Format("button{0}_{1}", k, j)] as Button).Text = "";
                }
            }
        }


        private Button getButton(int i, int j) => 
            tableLayoutPanel1.Controls[string.Format("button{0}_{1}", i, j)] as Button;
    }
}
