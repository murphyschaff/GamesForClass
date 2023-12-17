using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesForClass {  
        public partial class TicTacToe : Form
        {
        int[] choices = new int[] {0, 0, 0, 0};
        bool complete = false;
            public TicTacToe()
            {
                InitializeComponent();
            }
        /* Button Clicks */
        /* Top Right */
        private void button1_Click(object sender, EventArgs e)
        {
            if (!complete)
            {
                if (fillX(((Button)sender)))
                {
                    checkWinner();
                    if (!complete)
                    {
                        runAI(); 
                        checkWinner();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!complete)
            {
                if (fillX(((Button)sender)))
                {
                    checkWinner();
                    if (!complete)
                    {
                        runAI();
                        checkWinner();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!complete)
            {
                if (fillX(((Button)sender)))
                {
                    checkWinner();
                    if (!complete)
                    {
                        runAI();
                        checkWinner();
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!complete)
            {
                if (fillX(((Button)sender)))
                {
                    checkWinner();
                    if (!complete)
                    {
                        runAI();
                        checkWinner();
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!complete)
            {
                if (fillX(((Button)sender)))
                {
                    checkWinner();
                    if (!complete)
                    {
                        runAI();
                        checkWinner();
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!complete)
            {
                if (fillX(((Button)sender)))
                {
                    checkWinner();
                    if (!complete)
                    {
                        runAI();
                        checkWinner();
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!complete)
            {
                if (fillX(((Button)sender)))
                {
                    checkWinner();
                    if (!complete)
                    {
                        runAI();
                        checkWinner();
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!complete)
            {
                if (fillX(((Button)sender)))
                {
                    checkWinner();
                    if (!complete)
                    {
                        runAI();
                        checkWinner();
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!complete)
            {
                if (fillX(((Button)sender)))
                {
                    checkWinner();
                    if (!complete)
                    {
                        runAI();
                        checkWinner();
                    }
                }
            }
        }

        private bool fillX(Button b)
        {
            bool placed = false;
            if (b.Text == "")
            {
                b.Text = "X";
                placed = true;
            }
            return placed;
        }
        /* Fills square with computer's choice */
        private bool fillO(int num)
        {
            bool placed = false;
            switch (num)
            {
                case 1:
                    if (button1.Text == "")
                    {
                        button1.Text = "O";
                        placed = true;
                    }
                    break;
                case 2:
                    if (button2.Text == "")
                    {
                        button2.Text = "O";
                        placed = true;
                    }
                    break;
                case 3:
                    if (button3.Text == "")
                    {
                        button3.Text = "O";
                        placed = true;
                    }
                    break;
                case 4:
                    if (button4.Text == "")
                    {
                        button4.Text = "O";
                        placed = true;
                    }
                    break;
                case 5:
                    if (button5.Text == "")
                    {
                        button5.Text = "O";
                        placed = true;
                    }
                    break;
                case 6:
                    if (button6.Text == "")
                    {
                        button6.Text = "O";
                        placed = true;
                    }
                    break;
                case 7:
                    if (button7.Text == "")
                    {
                        button7.Text = "O";
                        placed = true;
                    }
                    break;
                case 8:
                    if (button8.Text == "")
                    {
                        button8.Text = "O";
                        placed = true;
                    }
                    break;
                case 9:
                    if (button9.Text == "")
                    {
                        button9.Text = "O";
                        placed = true;
                    }
                    break;
            }
            return placed;
        }
        /* Runs AI for Tic-Tac-Toe */
        private void runAI()
        {
            bool run = true;
            while (run)
            {
                //If the maximum number of choices has not been made yet
                if (choices[3] == 0)
                {
                    //make a new choice
                    Random rnd = new Random();
                    if (fillO(rnd.Next(1, 9)))
                    {
                        run = false;
                    }
                } else
                {
                    run = false;
                }
            }

        }
        private void checkWinner()
        {
            if (button1.Text == button2.Text && button1.Text == button3.Text && button1.Text != "")
            {
                if (button1.Text == "X")
                {
                    label7.Text = "You Win!";
                    int val = Convert.ToInt32(label5.Text);
                    label5.Text = Convert.ToString(1 + val);
                } else
                {
                    label7.Text = "Sorry, the computer won.";
                    int val = Convert.ToInt32(label6.Text);
                    label6.Text = Convert.ToString(1 + val);
                }
                complete = true;
            } else if (button4.Text == button5.Text && button4.Text == button6.Text && button4.Text != "")
            {
                if (button4.Text == "X")
                {
                    label7.Text = "You Win!";
                    int val = Convert.ToInt32(label5.Text);
                    label5.Text = Convert.ToString(1 + val);
                }
                else
                {
                    label7.Text = "Sorry, the computer won.";
                    int val = Convert.ToInt32(label6.Text);
                    label6.Text = Convert.ToString(1 + val);
                }
                complete = true;
            } else if (button7.Text == button8.Text && button7.Text == button9.Text && button7.Text != "")
            {
                if (button7.Text == "X")
                {
                    label7.Text = "You Win!";
                    int val = Convert.ToInt32(label5.Text);
                    label5.Text = Convert.ToString(1 + val);
                }
                else
                {
                    label7.Text = "Sorry, the computer won.";
                    int val = Convert.ToInt32(label6.Text);
                    label6.Text = Convert.ToString(1 + val);
                }
                complete = true;
            } else if (button1.Text == button4.Text && button1.Text == button7.Text && button1.Text != "")
            {
                if (button1.Text == "X")
                {
                    label7.Text = "You Win!";
                    int val = Convert.ToInt32(label5.Text);
                    label5.Text = Convert.ToString(1 + val);
                }
                else
                {
                    label7.Text = "Sorry, the computer won.";
                    int val = Convert.ToInt32(label6.Text);
                    label6.Text = Convert.ToString(1 + val);
                }
                complete = true;
            } else if (button2.Text == button5.Text && button2.Text == button8.Text && button2.Text != "")
            {
                if (button2.Text == "X")
                {
                    label7.Text = "You Win!";
                    int val = Convert.ToInt32(label5.Text);
                    label5.Text = Convert.ToString(1 + val);
                }
                else
                {
                    label7.Text = "Sorry, the computer won.";
                    int val = Convert.ToInt32(label6.Text);
                    label6.Text = Convert.ToString(1 + val);
                }
                complete = true;
            } else if (button3.Text == button6.Text && button3.Text == button9.Text && button3.Text != "")
            {
                if (button3.Text == "X")
                {
                    label7.Text = "You Win!";
                    int val = Convert.ToInt32(label5.Text);
                    label5.Text = Convert.ToString(1 + val);
                }
                else
                {
                    label7.Text = "Sorry, the computer won.";
                    int val = Convert.ToInt32(label6.Text);
                    label6.Text = Convert.ToString(1 + val);
                }
                complete = true;
            } else if (button1.Text == button5.Text && button1.Text == button9.Text && button1.Text != "")
            {
                if (button1.Text == "X")
                {
                    label7.Text = "You Win!";
                    int val = Convert.ToInt32(label5.Text);
                    label5.Text = Convert.ToString(1 + val);
                }
                else
                {
                    label7.Text = "Sorry, the computer won.";
                    int val = Convert.ToInt32(label6.Text);
                    label6.Text = Convert.ToString(1 + val);
                }
                complete = true;
            } else if (button3.Text == button5.Text && button3.Text == button7.Text && button3.Text != "")
            {
                if (button3.Text == "X")
                {
                    label7.Text = "You Win!";
                    int val = Convert.ToInt32(label5.Text);
                    label5.Text = Convert.ToString(1 + val);
                }
                else
                {
                    label7.Text = "Sorry, the computer won.";
                    int val = Convert.ToInt32(label6.Text);
                    label6.Text = Convert.ToString(1 + val);
                }
                complete = true;
            } else if (button1.Text != "" && button2.Text != "" && button3.Text != "" && button4.Text != "" && button5.Text != "" && button6.Text != "" && button7.Text != "" && button8.Text != "" && button9.Text != "")
            {
                label7.Text = "You Tied!";
                complete = true;
            }
        }
        /* button that resets the score */
        private void button10_Click(object sender, EventArgs e)
        {
            label5.Text = "0";
            label6.Text = "0";
        }
        /*starts a new game */
        private void button11_Click(object sender, EventArgs e)
        {
            button1.Text = "";
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";
            button5.Text = "";
            button6.Text = "";
            button7.Text = "";
            button8.Text = "";
            button9.Text = "";
            complete = false;
            label7.Text = "";
        }
    }
    
}
