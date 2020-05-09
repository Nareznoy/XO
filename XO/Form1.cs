using System;
using System.Windows.Forms;

namespace XO
{
    public partial class Form1 : Form
    {
        private readonly bool[] _done = new bool[9];
        private readonly int[,] _fieldArray = new int[3, 3];
        private bool _player = true;


        public Form1()
        {
            InitializeComponent();
        }

        public bool HaveWinner { get; private set; }


        private void ClickedButton(Button button, int i, int j)
        {
            if (_fieldArray[i, j] != 0) return;

            ChangeButton(button, i, j);
            if (checkBox1.Checked && !HaveWinner)
            {
                GetRandomPoint(ref i, ref j);
                button = GetButton(i, j);

                _player = false;
                ChangeButton(button, i, j);
                _player = true;
            }
            else
            {
                _player = !_player;
            }

            if (HaveWinner) ShowWinnerWindow();
        }


        private void ChangeButton(Button b, int i, int j)
        {
            b.Text = _player ? "X" : "O";
            b.Update();
            _fieldArray[i, j] = _player ? 1 : 2;
            CheckWinner();
        }


        private void GetRandomPoint(ref int i, ref int j)
        {
            var rand = new Random();
            while (_fieldArray[i, j] != 0)
            {
                i = rand.Next(0, 3);
                j = rand.Next(0, 3);
            }
        }


        private Button GetButton(int i, int j)
        {
            return (Button)tableLayoutPanel1.Controls[$"button{i}_{j}"];
        }


        private void CheckWinner()
        {
            var val = _player ? 1 : 2;
            for (var i = 0; i < _done.Length; i++)
                _done[i] = true;
            for (var i = 0; i < 3; i++)
            {
                if (_fieldArray[i, i] != val) _done[6] = false;
                if (_fieldArray[i, 2 - i] != val) _done[7] = false;
                for (var j = 0; j < 3; j++)
                {
                    if (_fieldArray[i, j] != val) _done[i] = false;
                    if (_fieldArray[j, i] != val) _done[i + 3] = false;
                    if (_fieldArray[i, j] == 0) _done[8] = false;
                }
            }

            foreach (var win in _done)
            {
                if (!win) continue;

                HaveWinner = true;
                ChangeButtonsStatus(false);
            }
        }


        private void ShowWinnerWindow()
        {
            for (var i = 0; i < _done.Length; i++)
            {
                if (_done[i] == true)
                {
                    if (i == 8)
                    {
                        var winnerForm = new WinnerForm(this, "Ничья!");
                        winnerForm.ShowDialog();
                        return;
                    }

                    if (!_player)
                    {
                        if (checkBox1.Checked)
                        {
                            var winnerForm = new WinnerForm(this, "Вы выиграли!");
                            winnerForm.ShowDialog();
                            return;
                        }
                        else
                        {
                            var winnerForm = new WinnerForm(this, "Игрок X выиграл!");
                            winnerForm.ShowDialog();
                            return;
                        }
                    }

                    if (checkBox1.Checked)
                    {
                        var winnerForm = new WinnerForm(this, "Вы проиграли!");
                        winnerForm.ShowDialog();
                        return;
                    }
                    else
                    {
                        var winnerForm = new WinnerForm(this, "Игрок O выиграл!");
                        winnerForm.ShowDialog();
                        return;
                    }
                }
            }
        }


        private void ChangeButtonsStatus(bool status)
        {
            for (var k = 0; k < 3; k++)
                for (var j = 0; j < 3; j++)
                    ((Button)tableLayoutPanel1.Controls[$"button{k}_{j}"]).Enabled = status;
        }


        private void button0_0_Click(object sender, EventArgs e) { ClickedButton((Button)sender, 0, 0); }
        private void button0_1_Click(object sender, EventArgs e) { ClickedButton((Button)sender, 0, 1); }
        private void button0_2_Click(object sender, EventArgs e) { ClickedButton((Button)sender, 0, 2); }
        private void button1_0_Click(object sender, EventArgs e) { ClickedButton((Button)sender, 1, 0); }
        private void button1_1_Click(object sender, EventArgs e) { ClickedButton((Button)sender, 1, 1); }
        private void button1_2_Click(object sender, EventArgs e) { ClickedButton((Button)sender, 1, 2); }
        private void button2_0_Click(object sender, EventArgs e) { ClickedButton((Button)sender, 2, 0); }
        private void button2_1_Click(object sender, EventArgs e) { ClickedButton((Button)sender, 2, 1); }
        private void button2_2_Click(object sender, EventArgs e) { ClickedButton((Button)sender, 2, 2); }


        public void NewGame()
        {
            ClearField();
            ChangeButtonsStatus(true);
            HaveWinner = false;
            _player = true;
        }


        private void ClearField()
        {
            for (var i = 0; i < 3; i++) 
                for (var j = 0; j < 3; j++)
                    _fieldArray[i, j] = 0;

            for (var k = 0; k < 3; k++)
                for (var j = 0; j < 3; j++)
                    ((Button) tableLayoutPanel1.Controls[$"button{k}_{j}"]).Text = "";
        }
    }
}