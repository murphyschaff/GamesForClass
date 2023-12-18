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
        int choices = 0;
        bool complete = false;
        int difficulty = 0;
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

            if (difficulty == 0)
            {
                easyAI();
            }
            else if (difficulty == 1)
            {
                mediumAI();
            }
            else
            {
                hardAI();
            }      
        }
        /*
         * checkWinner()
         * Checks if the game has been won
         */
        private void checkWinner()
        {
            if (button1.Text == button2.Text && button1.Text == button3.Text && button1.Text != "")
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
            }
            else if (button4.Text == button5.Text && button4.Text == button6.Text && button4.Text != "")
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
            }
            else if (button7.Text == button8.Text && button7.Text == button9.Text && button7.Text != "")
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
            }
            else if (button1.Text == button4.Text && button1.Text == button7.Text && button1.Text != "")
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
            }
            else if (button2.Text == button5.Text && button2.Text == button8.Text && button2.Text != "")
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
            }
            else if (button3.Text == button6.Text && button3.Text == button9.Text && button3.Text != "")
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
            }
            else if (button1.Text == button5.Text && button1.Text == button9.Text && button1.Text != "")
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
            }
            else if (button3.Text == button5.Text && button3.Text == button7.Text && button3.Text != "")
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
            }
            else if (button1.Text != "" && button2.Text != "" && button3.Text != "" && button4.Text != "" && button5.Text != "" && button6.Text != "" && button7.Text != "" && button8.Text != "" && button9.Text != "")
            {
                label7.Text = "You Tied!";
                complete = true;
            }
        }
        /*
         * AI TYPES
         */
        /* EASY */
        private void easyAI()
        {
            Random rnd = new Random();
            int val = rnd.Next(1,9);
            while (!fillO(val))
            {
                val = rnd.Next(1,9);
            }
        }
        /*Medium */
        private void mediumAI()
        {
            //Runs the corner strategy if middle is not taken
            if (button5.Text == "" || button5.Text == "O")
            {
                if (button5.Text == "")
                {
                    button5.Text = "O";
                }
                else
                {
                    bool choiceMade = false;
                    //checks if a corner has already been chosen, and if it can fill the opposite corner
                    if (button1.Text == "O")
                    {
                        choiceMade = fillO(9);
                    }
                    else if (button3.Text == "O")
                    {
                        choiceMade = fillO(7);
                    }
                    else if (button7.Text == "O")
                    {
                        choiceMade = fillO(6);
                    }
                    else if (button9.Text == "O")
                    {
                        choiceMade = fillO(1);
                    }
                    //If the opposite corner has been taken, or no corners taken already, runs random chooser
                    if (!choiceMade)
                    {
                        //If all corners have been taken already, lowest value space
                        if (button1.Text != "" && button3.Text != "" && button7.Text != "" && button9.Text != "")
                        {
                            int[] index = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                            int i = 0;
                            while (!fillO(index[i]))
                            {
                                i++;
                            }
                        }
                        else if (button1.Text == "X" || button9.Text == "X")
                        {
                            if (!fillO(7))
                            {
                                fillO(3);
                            }
                        }
                        else
                        {
                            if (!fillO(1))
                            {
                                fillO(9);
                            }
                        }
                    }
                }
            }
            else
            {
                //if the middle is not available
                Random checkRandom = new Random();
                int val = checkRandom.Next(1, 50);
                //finds all possible win chances if random number chooses correctly
                if (val < 40)
                {
                    Queue<int> possibleWins = findWinChances("X");
                    bool done = false;
                    while (possibleWins.Count > 0)
                    {
                        //blocks win chance for this round
                        if (fillO(possibleWins.Dequeue()))
                        {
                            done = true;
                            break;
                        }
                    }
                    if (!done)
                    {
                        easyAI();
                    }
                } else
                {
                    easyAI();
                }
            }
        }
        /* Hard AI, will always try to win, then block, then set up a win */
        private void hardAI()
        {
            Queue<int> AIChances = findWinChances("O");
            if (AIChances.Count > 0)
            {
                bool done = false;
                while (AIChances.Count > 0)
                {
                    if (fillO(AIChances.Dequeue()))
                    {
                        done = true;
                        break;
                    }
                }
                if (!done)
                {
                    easyAI();
                }
            }
            else
            {
                Queue<int> playerChances = findWinChances("X");
                if (playerChances.Count > 0)
                {
                    bool done = false;
                    while (playerChances.Count > 0)
                    {
                        if (fillO(playerChances.Dequeue()))
                        {
                            done = true;
                            break;
                        }
                    }
                    if (!done)
                    {
                        easyAI();
                    }
                }
                else
                {
                    //attempt to set up auto win through filling middle and two corners, if it cannot fills randomly
                    if (button5.Text == "" || button5.Text == "O")
                    {
                        if (button5.Text == "")
                        {
                            fillO(5);
                        }
                        else
                        {
                            if (!fillO(7))
                            {
                                if (!fillO(9))
                                {
                                    if (!fillO(1))
                                    {
                                        if (!fillO(3))
                                        {
                                            easyAI();
                                        }
                                    }
                                }
                            }
                        }
                    } 
                    else
                    {
                        easyAI();
                    }
                }
            }

        }


        /* Discovers all possible win chances that the player has */
        private Queue<int> findWinChances(String user)
        {
            //gets the actual board values
            String[] top = { button1.Text, button2.Text, button3.Text };
            String[] middleR = { button4.Text, button5.Text, button6.Text };
            String[] bottom = { button7.Text, button8.Text, button9.Text};

            String[] left = { button1.Text, button4.Text, button7.Text };
            String[] middleC = { button2.Text, button5.Text, button8.Text };
            String[] right = { button3.Text, button6.Text, button9.Text };

            String[] Ldiag = { button1.Text, button5.Text, button9.Text };
            String[] Rdiag = { button3.Text, button5.Text, button7.Text};

            int topX = 0;
            int middleRX = 0;
            int bottomX = 0;
            int leftX = 0;
            int rightX = 0;
            int middleCX = 0;
            int LdiagX = 0;
            int RdiagX = 0;
            //finds all the possible X values for each possible win chance
            for (int i = 0; i < top.Length; i++)
            {
                if (top[i] == user) topX++;
                if (middleR[i] == user) middleRX++;
                if (bottom[i] == user) bottomX++;
                if (left[i] == user) leftX++;
                if (middleC[i] == user) middleCX++;
                if (right[i] == user) rightX++;
                if (Ldiag[i] == user) LdiagX++;
                if (Rdiag[i] == user) RdiagX++;
            }
            Queue<int> values = new Queue<int>();
            //if there is a chance that they win, the int values are added to the queue
            if (topX > 1)
            {
                if (top[0] == "") values.Enqueue(1);
                if (top[1] == "") values.Enqueue(2);
                if (top[2] == "") values.Enqueue(3);
            }
            if (middleRX > 1)
            {
                if (middleR[0] == "") values.Enqueue(4);
                if (middleR[1] == "") values.Enqueue(5);
                if (middleR[2] == "") values.Enqueue(6);
            }
            if (bottomX > 1)
            {
                if (bottom[0] == "") values.Enqueue(7);
                if (bottom[1] == "") values.Enqueue(8);
                if (bottom[2] == "") values.Enqueue(9);
            }
            if (leftX > 1)
            {
                if (left[0] == "") values.Enqueue(1);
                if (left[1] == "") values.Enqueue(4);
                if (left[2] == "") values.Enqueue(7);
            }
            if (middleCX > 1)
            {
                if (middleC[0] == "") values.Enqueue(2);
                if (middleC[1] == "") values.Enqueue(5);
                if (middleC[2] == "") values.Enqueue(8);
            }
            if (rightX > 1)
            {
                if (right[0] == "") values.Enqueue(3);
                if (right[1] == "") values.Enqueue(6);
                if (right[2] == "") values.Enqueue(9);
            }
            if (LdiagX > 1)
            {
                if (Ldiag[0] == "") values.Enqueue(1);
                if (Ldiag[1] == "") values.Enqueue(5);
                if (Ldiag[2] == "") values.Enqueue(9);
            }
            if (RdiagX > 1)
            {
                if (Rdiag[0] == "") values.Enqueue(3);
                if (Rdiag[1] == "") values.Enqueue(5);
                if (Rdiag[2] == "") values.Enqueue(7);
            }

            return values;
           
        }
        /* Changes difficulty value */
        //Easy
        private void button12_Click(object sender, EventArgs e)
        {
            difficulty = 0;
            label10.Text = "Easy"
;        }
        //Medium
        private void button13_Click(object sender, EventArgs e)
        {
            difficulty = 1;
            label10.Text = "Medium";
        }
        //Hard
        private void button14_Click(object sender, EventArgs e)
        {
            difficulty = 2;
            label10.Text = "Hard";
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
            choices = 0;
        }
    }
    
}
