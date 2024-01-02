using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesForClass
{
    public partial class Yahtzee : Form
    {
        private YahtzeePlayer player;
        private YahtzeePlayer CPU;
        public Yahtzee()
        {
            InitializeComponent();
            initYahtzee();
        }
        //creates a new instance of yhatzee
        public void initYahtzee()
        {
            player = new YahtzeePlayer();
            CPU = new YahtzeePlayer();
        }
        //updates yahtzee board
        public void updateBoard(YahtzeeBoard board)
        {
            int[] free = board.getFreeDice();
            int[] hold = board.getHoldDice();
            int val;
            Color clr;
            bool isHeld = false;
            for (int i = 0; i < free.Length; i++)
            {
                //dice is being held
                if (free[i] == -1)
                {
                    isHeld = true;
                    val = hold[i];
                }
                else
                {
                    isHeld = false;
                    val = free[i];
                }
                //changes background color
                if (isHeld) { clr = Color.DarkGray; } else { clr = Color.LightGray; }
                //updates respective label
                switch (i)
                {
                    case 0:
                        dice.Text = val.ToString();
                        dice.BackColor = clr;
                        break;
                    case 1:
                        dice1.Text = val.ToString();
                        dice1.BackColor = clr;
                        break;
                    case 2:
                        dice2.Text = val.ToString();
                        dice2.BackColor = clr;
                        break;
                    case 3:
                        dice3.Text = val.ToString();
                        dice3.BackColor = clr;
                        break;
                    case 4:
                        dice4.Text = val.ToString();
                        dice4.BackColor = clr;
                        break;
                }
            }
        }
        //hides all check boxes on screen
        public void hideChecks()
        {
            user1s.Visible = false;
            user2s.Visible = false;
            user3s.Visible = false;
            user4s.Visible = false;
            user5s.Visible = false;
            user6s.Visible = false;
            user3k.Visible = false;
            user4k.Visible = false;
            userss.Visible = false;
            userls.Visible = false;
            userfh.Visible = false;
            userya.Visible = false;
            userch.Visible = false;
        }
        //Rolls all dice that are labeled 'free'
        public void rollDice(YahtzeeBoard board)
        {
            Random rnd = new Random();
            int[] freeDice = board.getFreeDice();

            for (int i  = 0; i < freeDice.Length; i++)
            {
                if (freeDice[i] != -1)
                {
                    freeDice[i] = rnd.Next(1, 6);
                }
            }
            
            board.setFreeDice(freeDice);
            
        }
        //finds options for user based on currently selected dice
        public void findOptions(YahtzeePlayer plr)
        {
            int[] hold = plr.getBoard().getHoldDice();
            int[] vals = new int[6];
            hideChecks();
            //finds the number of dice that have the same value from the hold dice
            for (int i = 0; i < hold.Length;i++)
            {
                switch(hold[i])
                {
                    case 0:
                        //base case
                        break;
                    case 1:
                        vals[0]++; break;
                    case 2:
                        vals[1]++; break;
                    case 3:
                        vals[2]++; break;
                    case 4:
                        vals[3]++; break;
                    case 5:
                        vals[4]++; break;
                    case 6:
                        vals[5]++; break;
                }
            }
            //checks which ones are available
            bool[] sections = plr.getSections();
            for (int i = 0; i < sections.Length;i++)
            {
                switch(i)
                {
                    //Ones
                    case 0:
                        //if it has not been chosen, and there are three or more, make stuff available to choose
                        if (sections[i] == false && vals[0] >= 3)
                        {
                            user1s.Visible = true;
                        }
                        break;
                    //twos
                    case 1:
                        if (sections[i] == false && vals[1] >= 3)
                        {
                            user2s.Visible = true;
                        }
                        break;
                    //threes
                    case 2:
                        if (sections[i] == false && vals[2] >= 3)
                        {
                            user3s.Visible = true;
                        }
                        break;
                    //fours
                    case 3:
                        if (sections[i] == false && vals[3] >= 3)
                        {
                            user4s.Visible = true;
                        }
                        break;
                    //fives
                    case 4:
                        if (sections[i] == false && vals[4] >= 3)
                        {
                            user5s.Visible = true;
                        }
                        break;
                    //sixes
                    case 5:
                        if (sections[i] == false && vals[5] >= 3)
                        {
                            user6s.Visible = true;
                        }
                        break;
                    //three of a kind
                    case 6:
                        if (sections[i] == false)
                        {
                            for (int j = 0; j < vals.Length; j++)
                            {
                                if (vals[j] >= 3)
                                {
                                    user3k.Visible = true;
                                }
                            }
                        }
                        break;
                    //four of a kind
                    case 7:
                        if (sections[i] == false)
                        {
                            for (int j = 0; j < vals.Length; j++)
                            {
                                if (vals[j] >= 4)
                                {
                                    user4k.Visible = true;
                                }
                            }
                        }
                        break;
                    //full house
                    case 8:
                        if (sections[i] == false)
                        {
                            //checks if there is a value with 3 instances
                            for (int j = 0; j < vals.Length;j++)
                            {
                                if (vals[j] == 3)
                                {
                                    //checks if there is another value that has 2
                                    for (int k = 0; k < vals.Length; k++)
                                    {
                                        if (vals[k] == 2)
                                        {
                                            userfh.Visible = true;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    //Small Straight
                    case 9:
                        if (sections[i] == false)
                        {
                            if (vals[0] == 1 && vals[1] == 1 && vals[2] == 1 && vals[3] == 1)
                            {
                                userss.Visible = true;
                            }
                            else if (vals[1] == 1 & vals[2] == 1 && vals[3] == 1 && vals[4] == 1)
                            {
                                userss.Visible = true;
                            }
                            else if (vals[2] == 1 && vals[3] == 1 && vals[4] == 1 && vals[5] == 1)
                            {
                                userss.Visible = true;
                            }
                        }
                        break;
                    //Large Straight
                    case 10:
                        if (sections[i] == false)
                        {
                            if (vals[0] == 1 && vals[1] == 1 && vals[2] == 1 && vals[3] == 1 && vals[4] == 1 && vals[5] == 1)
                            {
                                userls.Visible = true;
                            }
                        }
                        break;
                    //Yahtzee
                    case 11:
                        if (sections[i] == false)
                        {
                            for (int j = 0; j < vals.Length; j++)
                            {
                                if (vals[j] == 5)
                                {
                                    userya.Visible = true;
                                    break;
                                }
                            }
                        }
                        break;
                    //Chance
                    case 12:
                        if (sections[i] == false && vals[0] != 0 || vals[1] != 0 || vals[2] != 0 || vals[3] != 0 || vals[4] != 0)
                        {
                            userch.Visible = true;
                        }
                        break;
                }
            }

        }
        //roll button action
        private void rollButton_Click(object sender, EventArgs e)
        {
            rollDice(player.getBoard());
            updateBoard(player.getBoard());
        }
        //confirm button action
        private void confirmButton_Click(object sender, EventArgs e)
        {

        }

        /* LABELS */
        private void dice_Click(object sender, EventArgs e)
        {
            Color clr = Color.DarkGray;
            YahtzeeBoard board = player.getBoard();
            //dice is currently set to hold
            if (dice.BackColor == clr)
            {
                board.markFree(0);
            }
            else
            {
                board.markHold(0);
            }
            player.setBoard(board);
            updateBoard(player.getBoard());
            findOptions(player);
        }

        private void dice1_Click(object sender, EventArgs e)
        {
            Color clr = Color.DarkGray;
            YahtzeeBoard board = player.getBoard();
            //dice is currently set to hold
            if (dice1.BackColor == clr)
            {
                board.markFree(1);
            }
            else
            {
                board.markHold(1);
            }
            player.setBoard(board);
            updateBoard(player.getBoard());
            findOptions(player);
        }

        private void dice2_Click(object sender, EventArgs e)
        {
            Color clr = Color.DarkGray;
            YahtzeeBoard board = player.getBoard();
            //dice is currently set to hold
            if (dice2.BackColor == clr)
            {
                board.markFree(2);
            }
            else
            {
                board.markHold(2);
            }
            player.setBoard(board);
            updateBoard(player.getBoard());
            findOptions(player);
        }

        private void dice3_Click(object sender, EventArgs e)
        {
            Color clr = Color.DarkGray;
            YahtzeeBoard board = player.getBoard();
            //dice is currently set to hold
            if (dice3.BackColor == clr)
            {
                board.markFree(3);
            }
            else
            {
                board.markHold(3);
            }
            player.setBoard(board);
            updateBoard(player.getBoard());
            findOptions(player);
        }

        private void dice4_Click(object sender, EventArgs e)
        {
            Color clr = Color.DarkGray;
            YahtzeeBoard board = player.getBoard();
            //dice is currently set to hold
            if (dice4.BackColor == clr)
            {
                board.markFree(4);
            }
            else
            {
                board.markHold(4);
            }
            player.setBoard(board);
            updateBoard(player.getBoard());
            findOptions(player);
        }
    }

    public class YahtzeePlayer
    {
        private bool[] sections = new bool[13];
        private int[] points = new int[13];
        private YahtzeeBoard board;
        public YahtzeePlayer()
        {
            for (int i = 0; i < sections.Length; i++)
            {
                sections[i] = false;
                points[i] = 0;
            }
            board = new YahtzeeBoard();
        }

        //getters and setters
        public bool[] getSections() { return sections; }
        public void setSections(bool[] sections) { this.sections = sections; }
        public YahtzeeBoard getBoard() { return board; }
        public void setBoard(YahtzeeBoard board) { this.board = board; }
        public int[] getPoints() { return points;}
        public void setPoints(int[] points) {  this.points = points; }

    }

    public class YahtzeeBoard
    {
        private int[] freeDice = new int[5];
        private int[] holdDice = new int[5];
        public YahtzeeBoard()
        {
            for (int i = 0; i < freeDice.Length;i++) { freeDice[i] = 0; holdDice[i] = -1; }
        }

        //getters and setters
        public int[] getFreeDice() {  return freeDice; }
        public void setFreeDice(int[] freeDice) { this.freeDice = freeDice; }
        public int[] getHoldDice() { return holdDice;}
        public void setHoldDice(int[] holdDice) { this.holdDice = holdDice; }

        //marks a dice as 'free' or 'hold'
        public void markFree(int index) { freeDice[index] = holdDice[index]; holdDice[index] = -1; }
        public void markHold(int index) { holdDice[index] = freeDice[index]; freeDice[index] = -1; }

    }
}
