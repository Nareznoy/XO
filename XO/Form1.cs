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
    public partial class Form1 : Form
    {
        public int[,] mass = new int[3, 3];
        private bool haveWinner = false;
        private bool player = true;
        public Form1()
        {
            InitializeComponent();
        }
        //private void button_Click(object sender, EventArgs e)
        //{
        //    Button b = (Button)sender;
        //    int i = int.Parse(b.Name[6].ToString());
        //    int j = int.Parse(b.Name[8].ToString());

        //    if (mass[i, j] == 0)
        //    {
        //        changeButton(b, i, j, player);
        //        if ((checkBox1.Checked) && (!haveWinner))
        //        {
        //            getPoint(ref i, ref j);
        //            changeButton(b, i, j, false);
        //        }
        //        else
        //        {
        //            player = !player;
        //        }
        //    }
        //}

        private void changeButton(Button b, int i, int j, bool player)
        {
            b.Text = (player ? "X" : "O");
            //либо так
            //b.Image = (player ? img : img2);
            b.Update();
            mass[i, j] = (player ? 1 : 2);
            checkWinner(player);
        }

        private void getPoint(ref int i, ref int j)
        {
            Random rand = new Random();
            while (mass[i, j] != 0)
            {
                i = rand.Next(0, 3);
                j = rand.Next(0, 3);
            }
        }

        private void checkWinner(bool player)
        {
            int val = (player ? 1 : 2);
            bool[] done = new bool[9];
            for (int i = 0; i < done.Length; i++) done[i] = true;
            for (int i = 0; i < 3; i++)
            {
                if (mass[i, i] != val) done[6] = false;
                if (mass[i, 2 - i] != val) done[7] = false;
                for (int j = 0; j < 3; j++)
                {
                    if (mass[i, j] != val)
                    {
                        done[i] = false;
                    }
                    if (mass[j, i] != val) done[i + 3] = false;
                    if (mass[i, j] == 0) done[8] = false;
                }
            }
            for (int i = 0; i < done.Length; i++)
            {
                if (done[i] == true)
                {
                    haveWinner = true;
                    for (int k = 0; k < 3; k++)
                        for (int j = 0; j < 3; j++)
                        {
                            (tableLayoutPanel1.Controls[string.Format("button{0}_{1}", k, j)] as Button).Enabled = false;
                        }
                    if (i == 8)
                    {
                        MessageBox.Show("Ничья!!!");
                    }
                    if (player)
                    {
                        if (checkBox1.Checked)
                            MessageBox.Show("Вы выиграли!");
                        else MessageBox.Show("Игрок X выиграл!");
                    }
                    else
                    {
                        if (checkBox1.Checked)
                            MessageBox.Show("Вы проиграли!");
                        else MessageBox.Show("Игрок O выиграл!");
                    }
                    break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //for (int i = 0; i < 3; i++)
            //    for (int j = 0; j < 3; j++)
            //    {
            //        Button bt = new Button();
            //        bt.Location = new System.Drawing.Point(30 * i + 10 * (i + 1), 30 * j + 10 * (j + 1));
            //        bt.Name = String.Format("button{0}_{1}", i, j);
            //        bt.Size = new System.Drawing.Size(30, 30);
            //        bt.Click += new System.EventHandler(this.button_Click);
            //        this.Controls.Add(bt);
            //    }

        }

        private Button getButton(int i, int j)
        {
            if (i == 0 && j == 0)
                return button0_0;
            else if (i == 0 && j == 1)
                return button0_1;
            else if (i == 0 && j == 2)
                return button0_2;
            else if (i == 1 && j == 0)
                return button1_0;
            else if (i == 1 && j == 1)
                return button1_1;
            else if (i == 1 && j == 2)
                return button1_2;
            else if (i == 2 && j == 0)
                return button2_0;
            else if (i == 2 && j == 1)
                return button2_1;
            else if (i == 2 && j == 2)
                return button2_2;
            return null;
        }

        private void clickedButton(Button button, int i, int j)
        {
            if (mass[i, j] == 0)
            {
                changeButton(button, i, j, player);
                if ((checkBox1.Checked) && (!haveWinner))
                {
                    getPoint(ref i, ref j);
                    button = getButton(i, j);
                    changeButton(button, i, j, false);
                }
                else
                {
                    player = !player;
                }
            }
        }

        private void button0_0_Click(object sender, EventArgs e)
        {
            clickedButton((Button)sender, 0, 0);
        }

        private void button0_1_Click(object sender, EventArgs e)
        {
            clickedButton((Button)sender, 0, 1);
        }

        private void button0_2_Click(object sender, EventArgs e)
        {
            clickedButton((Button)sender, 0, 2);
        }

        private void button1_0_Click(object sender, EventArgs e)
        {
            clickedButton((Button)sender, 1, 0);
        }

        private void button1_1_Click(object sender, EventArgs e)
        {
            clickedButton((Button)sender, 1, 1);
        }

        private void button1_2_Click(object sender, EventArgs e)
        {
            clickedButton((Button)sender, 1, 2);
        }

        private void button2_0_Click(object sender, EventArgs e)
        {
            clickedButton((Button)sender, 2, 0);
        }

        private void button2_1_Click(object sender, EventArgs e)
        {
            clickedButton((Button)sender, 2, 1);
        }

        private void button2_2_Click(object sender, EventArgs e)
        {
            clickedButton((Button)sender, 2, 2);
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    player = true;
        //    haveWinner = false;
        //    for (int i = 0; i < 3; i++)
        //        for (int j = 0; j < 3; j++)
        //        {
        //            mass[i, j] = 0;
        //            (this.Controls[string.Format("button{0}_{1}", i, j)] as Button).Enabled = true;
        //            (this.Controls[string.Format("button{0}_{1}", i, j)] as Button).Image = null;
        //        }
        //}
    }
}
