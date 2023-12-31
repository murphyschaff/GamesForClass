﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
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
            int[] diceVals = board.getDiceVals();
            bool[] held = board.getHold();
            int val;
            Color clr;
            for (int i = 0; i < diceVals.Length; i++)
            {
                val = diceVals[i];
                //changes background color
                if (held[i]) { clr = Color.DarkGray; } else { clr = Color.LightGray; }
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
        //hides all check boxes on screen, except one being ignored
        public void hideChecks(int keep)
        {
            for (int i = 0; i < 13; i++)
            {
                if (i == keep) { continue; }
                switch(i)
                {
                    case 0:
                        user1s.Visible = false;
                        break;
                    case 1:
                        user2s.Visible = false;
                        break;
                    case 2:
                        user3s.Visible = false;
                        break;
                    case 3:
                        user4s.Visible = false;
                        break;
                    case 4:
                        user5s.Visible = false;
                        break;
                    case 5:
                        user6s.Visible = false;
                        break;
                    case 6:
                        user3k.Visible = false;
                        break;
                    case 7:
                        user4k.Visible = false;
                        break;
                    case 8:
                        userfh.Visible = false;
                        break;
                    case 9:
                        userss.Visible = false;
                        break;
                    case 10:
                        userls.Visible = false;
                        break;
                    case 11:
                        userya.Visible = false;
                        break;
                    case 12:
                        userch.Visible = false;
                        break;
                }

            }
        }
        
        //finds options for user based on currently selected dice
        public void findOptions(YahtzeePlayer plr)
        {
            hideChecks(-1);
            int[] vals = plr.getBoard().calculateVals(false);
            bool[] sections = plr.getSections();
            bool changeMade = false;
            int rolls = plr.getBoard().getRolls();
            //checks which ones are available
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
                            changeMade = true;
                        }
                        break;
                    //twos
                    case 1:
                        if (sections[i] == false && vals[1] >= 3)
                        {
                            user2s.Visible = true;
                            changeMade = true;
                        }
                        break;
                    //threes
                    case 2:
                        if (sections[i] == false && vals[2] >= 3)
                        {
                            user3s.Visible = true;
                            changeMade = true;
                        }
                        break;
                    //fours
                    case 3:
                        if (sections[i] == false && vals[3] >= 3)
                        {
                            user4s.Visible = true;
                            changeMade = true;
                        }
                        break;
                    //fives
                    case 4:
                        if (sections[i] == false && vals[4] >= 3)
                        {
                            user5s.Visible = true;
                            changeMade = true;
                        }
                        break;
                    //sixes
                    case 5:
                        if (sections[i] == false && vals[5] >= 3)
                        {
                            user6s.Visible = true;
                            changeMade = true;
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
                                    changeMade = true;
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
                                    changeMade = true;
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
                                            changeMade = true;
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
                                changeMade = true;
                            }
                            else if (vals[1] == 1 & vals[2] == 1 && vals[3] == 1 && vals[4] == 1)
                            {
                                userss.Visible = true;
                                changeMade = true;
                            }
                            else if (vals[2] == 1 && vals[3] == 1 && vals[4] == 1 && vals[5] == 1)
                            {
                                userss.Visible = true;
                                changeMade = true;
                            }
                        }
                        break;
                    //Large Straight
                    case 10:
                        if (sections[i] == false)
                        {
                            if (vals[0] == 1 && vals[1] == 1 && vals[2] == 1 && vals[3] == 1 && vals[4] == 1)
                            {
                                userls.Visible = true;
                                changeMade = true;
                            }
                            else if (vals[1] == 1 && vals[2] == 1 && vals[3] == 1 && vals[4] == 1 && vals[5] == 1)
                            {
                                userls.Visible = true;
                                changeMade = true;
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
                                    changeMade = true;
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
                            changeMade = true;
                        }
                        break;
                }
            }
            //sees if there are no options left after the third roll
            if (rolls == 3 && changeMade == false)
            {
                plr.getBoard().setNoPts(true);
                lastRoll(plr, false);
            }

        }
        //calculates the points from a given round
        public void calculatePoints(YahtzeePlayer plr)
        {
            int[] points = plr.getPoints();
            bool[] sections = plr.getSections();
            YahtzeeBoard board = plr.getBoard();
            bool[] hold = board.getHold();
            bool[] locked = board.getLocked();
            int[] vals = board.calculateVals(false);

            //finds appropriate sections and marks them complete if done so
            if (user1s.Checked == true)
            {
                points[0] = vals[0];
                sections[0] = true;
            }
            if (user2s.Checked == true)
            {
                points[1] = vals[1] * 2;
                sections[1] = true;
            }
            if (user3s.Checked == true)
            {
                points[2] = vals[2] * 3;
                sections[2] = true;
            }
            if (user4s.Checked == true)
            {
                points[3] = vals[3] * 4;
                sections[3] = true;
            }
            if (user5s.Checked == true)
            {
                points[4] = vals[4] * 5;
                sections[4] = true;
            }
            if (user6s.Checked == true)
            {
                points[5] = vals[5] * 6;
                sections[5] = true;
            }
            if (user3k.Checked == true)
            {
                for (int i = 0; i < vals.Length; i++)
                {
                    if (vals[i] >= 3)
                    {
                        points[6] = vals[i] * (i + 1);
                        sections[6] = true;
                        break;
                    }
                }
            }
            if (user4k.Checked == true)
            {
                for (int i = 0; i < vals.Length; i++)
                {
                    if (vals[i] >= 4)
                    {
                        points[7] = vals[i] * (i + 1);
                        sections[7] = true;
                        break;
                    }
                }
            }
            if (userfh.Checked == true)
            {
                for (int j = 0; j < vals.Length; j++)
                {
                    if (vals[j] == 3)
                    {
                        //checks if there is another value that has 2
                        for (int k = 0; k < vals.Length; k++)
                        {
                            if (vals[k] == 2)
                            {
                                points[8] = vals[j] * 3 + vals[k] * 2;
                                sections[8] = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (userss.Checked == true)
            {
                sections[9] = true;
                if (vals[0] == 1 && vals[1] == 1 && vals[2] == 1 && vals[3] == 1)
                {
                    points[9] = 10;
                }
                else if (vals[1] == 1 & vals[2] == 1 && vals[3] == 1 && vals[4] == 1)
                {
                    points[9] = 14;
                }
                else
                {
                    points[9] = 18;
                }
            }
            if (userls.Checked == true)
            {
                sections[10] = true;
                if (vals[0] == 1 && vals[1] == 1 && vals[2] == 1 && vals[3] == 1 && vals[4] == 1)
                {
                    points[10] = 15;
                }
                else if (vals[1] == 1 && vals[2] == 1 && vals[3] == 1 && vals[4] == 1 && vals[5] == 1)
                {
                    points[10] = 20;
                }
            }
            if (userya.Checked == true)
            {
                sections[11] = true;
                points[11] = 30;
            }
            if (userch.Checked == true)
            {
                int tot = 0;
                for (int i = 0; i < vals.Length; i++)
                {
                    tot += (vals[i] * (i + 1));
                }
                sections[12] = true;
                points[12] = tot;
            }

            //set all point totals to board
            String output = "";
            for (int i = 0; i < sections.Length; i++)
            {
                output += points[i].ToString() + "\n";
                if (i == 5)
                {
                    output += "\n";
                }
            }
            userPoints.Text = output;
            //lock the used dice from being used again in that round
            for (int i = 0; i < hold.Length; i++)
            {
                if (hold[i] == true)
                {
                    locked[i] = true;
                }
            }
            //set all values back in code
            plr.setPoints(points);
            plr.setSections(sections);
            board.setHoldDice(hold);
            board.setLocked(locked);
            plr.setBoard(board);
        }
        //opens all options, adds 0 points to the column if selected
        public void lastRoll(YahtzeePlayer plr, bool final)
        {
            bool[] sections = plr.getSections();
            //runs the open sequence
            if (!final)
            {
                //makes visible all unused categories
                if (sections[0] == false) { user1s.Visible = false; }
                if (sections[1] == false) { user2s.Visible = false; }
                if (sections[2] == false) { user3s.Visible = false; }
                if (sections[3] == false) { user4s.Visible = false; }
                if (sections[4] == false) { user5s.Visible = false; }
                if (sections[5] == false) { user6s.Visible = false; }
                if (sections[6] == false) { user3k.Visible = false; }
                if (sections[7] == false) { user4k.Visible = false; }
                if (sections[8] == false) { userfh.Visible = false; }
                if (sections[9] == false) { userss.Visible = false; }
                if (sections[10] == false) { userls.Visible = false; }
                if (sections[11] == false) { userya.Visible = false; }
                if (sections[12] == false) { userch.Visible = false; }
            }
            //runs the actual selection sequence
            else
            {
                int[] points = plr.getPoints();
                if (user1s.Checked == true)
                {
                    points[0] = 0;
                    sections[0] = true;
                }
                if (user2s.Checked == true)
                {
                    points[1] = 0;
                    sections[1] = true;
                }
                if (user3s.Checked == true)
                {
                    points[2] = 0;
                    sections[2] = true;
                }
                if (user4s.Checked == true)
                {
                    points[3] = 0;
                    sections[3] = true;
                }
                if (user5s.Checked == true)
                {
                    points[4] = 0;
                    sections[4] = true;
                }
                if (user6s.Checked == true)
                {
                    points[5] = 0;
                    sections[5] = true;
                }
                if (user3k.Checked == true)
                {
                    points[6] = 0;
                    sections[6] = true;
                }
                if (user4k.Checked == true)
                {
                    points[7] = 0;
                    sections[7] = true;
                }
                if (userfh.Checked == true)
                {
                    points[8] = 0;
                    sections[8] = true;
                }
                if (userss.Checked == true)
                {
                    points[9] = 0;
                    sections[9] = true;
                }
                if (userls.Checked == true)
                {
                    points[10] = 0;
                    sections[10] = true;
                }
                if (userya.Checked == true)
                {
                    points[11] = 0;
                    sections[11] = true;
                }
                if (userch.Checked == true)
                {
                    points[12] = 0;
                    sections[12] = true;
                }
                plr.setPoints(points);
            }
        }
        //roll button action
        private void rollButton_Click(object sender, EventArgs e)
        {
            YahtzeeBoard board = player.getBoard();
            int rolls = board.getRolls();
            board.incrRolls();
            //runs the player roll sequence
            if (rollButton.Text == "Roll")
            {
                board.rollDice();
                updateBoard(board);
                if (rolls >= 2)
                {
                    rollButton.Visible = false;
                }
            }
            //Start CPU turn
            else if (rollButton.Text == "Next Turn")
            {
                //starts CPU roll sequence
                rollButton.Text = "Next";
            }
            //roll again for CPU
            else
            {

            }
        }
        //confirm button action
        private void confirmButton_Click(object sender, EventArgs e)
        {
            YahtzeeBoard board = player.getBoard();
            if (board.getNoPts() == false)
            {
                calculatePoints(player);
                player.setBoard(new YahtzeeBoard());
                hideChecks(-1);
                updateBoard(player.getBoard());
            }
            else
            {
                lastRoll(player, true);
                player.getBoard().setNoPts(true);
            }
            rollButton.Text = "Next Turn";
            rollButton.Visible = true;
        }

        /* LABELS */
        private void dice_Click(object sender, EventArgs e)
        {
            Color clr = Color.DarkGray;
            YahtzeeBoard board = player.getBoard();
            bool[] locked = board.getLocked();
            //dice is currently set to hold
            if (dice.BackColor == clr && locked[0] == false)
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
            bool[] locked = board.getLocked();
            //dice is currently set to hold
            if (dice1.BackColor == clr && locked[1] == false)
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
            bool[] locked = board.getLocked();
            //dice is currently set to hold
            if (dice2.BackColor == clr && locked[2] == false)
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
            bool[] locked = board.getLocked();
            //dice is currently set to hold
            if (dice3.BackColor == clr && locked[3] == false)
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
            bool[] locked = board.getLocked();
            //dice is currently set to hold
            if (dice4.BackColor == clr && locked[4] == false)
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
        /* Click Changes */
        private void user1s_CheckedChanged(object sender, EventArgs e) { if (user1s.Checked == true) { hideChecks(0); } else { findOptions(player); } }
        private void user2s_CheckedChanged(object sender, EventArgs e) { if (user2s.Checked == true) { hideChecks(1); } else { findOptions(player); } }
        private void user3s_CheckedChanged(object sender, EventArgs e) { if (user3s.Checked == true) { hideChecks(2); } else { findOptions(player); } }
        private void user4s_CheckedChanged(object sender, EventArgs e) { if (user4s.Checked == true) { hideChecks(3); } else { findOptions(player); } }
        private void user5s_CheckedChanged(object sender, EventArgs e) { if (user5s.Checked == true) { hideChecks(4); } else { findOptions(player); } }
        private void user6s_CheckedChanged(object sender, EventArgs e) { if (user6s.Checked == true) { hideChecks(5); } else { findOptions(player); } }
        private void user3k_CheckedChanged(object sender, EventArgs e) { if (user3k.Checked == true) { hideChecks(6); } else { findOptions(player); } }
        private void user4k_CheckedChanged(object sender, EventArgs e) { if (user4k.Checked == true) { hideChecks(7); } else { findOptions(player); } }
        private void userfh_CheckedChanged(object sender, EventArgs e) { if (userfh.Checked == true) { hideChecks(8); } else { findOptions(player); } }
        private void userss_CheckedChanged(object sender, EventArgs e) { if (userss.Checked == true) { hideChecks(9); } else { findOptions(player); } }
        private void userls_CheckedChanged(object sender, EventArgs e) { if (userls.Checked == true) { hideChecks(10); } else { findOptions(player); } }
        private void userya_CheckedChanged(object sender, EventArgs e) { if (userya.Checked == true) { hideChecks(11); } else { findOptions(player); } }
        private void userch_CheckedChanged(object sender, EventArgs e) { if (userch.Checked == true) { hideChecks(12); } else { findOptions(player); } }
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

        /* Automatic Player Decision Functions */
        //rolls dice, makes decision based on what the roll is
        //returns true if decision was made, false if not
        public bool rollAndDecide()
        {
            board.rollDice();
            board.incrRolls();
            int[] diceVals = board.getDiceVals();
            bool[] hold = board.getHold();
            int[] holdVals = { 0, 0, 0, 0, 0, 0 };
            int[] vals = board.calculateVals(true);

            //check for values being held
            int holdCtr = 0;
            for (int i = 0; i < hold.Length; i++)
            {
                if (hold[i] == true)
                {
                    holdVals[holdCtr] = diceVals[i];
                    holdCtr++;
                }
            }
            //if this is not the first roll
            if (board.getRolls() != 3)
            {
                //checks if there is a current value in the hold
                //aims for the second roll
                if (holdVals[0] != 0)
                {
                    //checks if this is a num type
                    if (vals[holdVals[0] - 1] > 1)
                    {
                        int val = holdVals[0];
                        for (int i = 0; i < diceVals.Length; i++)
                        {
                            if (hold[i] == false)
                            {
                                if (diceVals[i] == val)
                                {
                                    hold[i] = true;
                                }
                            }
                        }
                    }
                    //straight type
                    else
                    {
                        int[] str = { 0, 0, 0, 0, 0, 0 };
                        for (int i = 0; i < diceVals.Length; i++)
                        {
                            //put hold values in hold into str list, if there is one that is not there it is added to the hold
                            if (hold[i] == true)
                            {
                                str[diceVals[i] -1] = 1;
                            }
                            else if (str[diceVals[i]-1] == 0)
                            {
                                hold[i] = true;
                            }
                        }
                    }
                }
                //first roll should end up here
                else
                {
                    //checks to see which value is the one to keep
                    bool strType = true;
                    int val = 0;
                    int index = 0;
                    for (int i = 0; i < vals.Length; i++)
                    {           
                        if (vals[i] >= 2 && vals[i] > val)
                        {
                            strType = false;
                            val = vals[i];
                            index = i;
                        }
                    }
                    //AI will attempt to do straights first, as there are less of those types
                    //straight type
                    if (strType == true && sections[9] == false || sections[10] == false)
                    {
                        //holds a sequence of dice
                        int[] str = { 0, 0, 0, 0, 0, 0 };
                        for (int i = 0; i < diceVals.Length; i++)
                        {
                            if (str[diceVals[i] - 1] == 0)
                            {
                                str[diceVals[i] - 1] = 1;
                                hold[i] = true;
                            }
                        }
                    }
                    //num type
                    else
                    {
                        //puts all selected variables into hold
                        for (int i = 0; i < diceVals.Length; i++)
                        {
                            if (diceVals[i] == index + 1)
                            {
                                hold[i] = true;
                            }
                        }
                    }
                }
                return false;
            }
            //After last roll, make decision
            else
            {
                if (vals[holdVals[0] - 1] > 1)
                {
                    int val = holdVals[0];
                    //makes decision based on what the value is, either goes for the numbers section or the 3k, 4k type
                    //going for 3k, 4k, or yahtzee
                    if (val > 3 && sections[6] == false || sections[7] == false)
                    {
                        if (sections[7] == false && vals[val - 1] == 4)
                        {
                            sections[7] = true;
                            points[7] = vals[val - 1] * val;
                            return true;
                        }
                        else if (sections[6] == false && vals[val - 1] >= 3)
                        {
                            sections[6] = true;
                            points[6] = vals[val - 1] * val;
                            return true;
                        }
                    }
                    //full house check
                    if (sections[8] == false && vals[val - 1] == 3)
                    {
                        for (int i = 0; i < vals.Length; i++)
                        {
                            if (vals[i] == 2)
                            {
                                sections[8] = true;
                                points[8] = 3 * val + 2 * (i + 1);
                                return true;
                            }
                        }
                    }
                    //going for value only
                    else
                    {
                        //makes sure there are 3 or more
                        if (vals[val - 1] >= 3)
                        {
                            sections[val - 1] = true;
                            points[val - 1] = vals[val - 1] * val;
                            return true;
                        }
                    }
                    return false;
                }
                //going for a straight
                else
                {
                    //large straight check
                    if (sections[10] == false)
                    {
                        if (vals[0] == 1 && vals[1] == 1 && vals[2] == 1 && vals[3] == 1 && vals[4] == 1)
                        {
                            sections[10] = true;
                            points[10] = 1 + 2 + 3 + 4 + 5;
                            return true;
                        }
                        else if (vals[1] == 1 && vals[2] == 1 && vals[3] == 1 && vals[4] == 1 && vals[5] == 1)
                        {
                            sections[10] = true;
                            points[10] = 2 + 3 + 4 + 5 + 6;
                            return true;
                        }
                    }
                    //small straight
                    else if (sections[9] == false)
                    {
                        if (vals[0] == 1 && vals[1] == 1 && vals[2] == 1 && vals[3] == 1)
                        {
                            sections[9] = true;
                            points[9] = 1 + 2 + 3 + 4;
                            return true;
                        }
                        else if (vals[1] == 1 & vals[2] == 1 && vals[3] == 1 && vals[4] == 1)
                        {
                            sections[9] = true;
                            points[9] = 2 + 3 + 4 + 5;
                            return true;
                        }
                        else if (vals[2] == 1 && vals[3] == 1 && vals[4] == 1 && vals[5] == 1)
                        {
                            sections[9] = true;
                            points[9] = 3 + 4 + 5 + 6;
                            return true;
                        }
                    }
                }

                //if the code makes it here, a decision could not be made. Hense, either chance must be selected, or a random section must be entered as a 0
                if (sections[12] == false)
                {
                    //chance selected
                    sections[12] = true;
                    for (int i = 0; i < diceVals.Length; i++)
                    {
                        points[12] += diceVals[i];
                    }
                    return true;
                }
                else 
                {
                    for (int i = 0; i < sections.Length; i++)
                    {
                        if (sections[i] == false)
                        {
                            sections[i] = true;
                            points[i] = 0;
                            return true;
                        }
                    }
                }
                //if you still cannot return, something went wrong.
                return false;
            }
        }
    }

    public class YahtzeeBoard
    {
        private int[] diceVals = new int[5];
        private bool[] diceHold = new bool[5];
        private bool[] locked = new bool[5];
        private int[] vals = new int[6];
        private int rolls;
        private bool noPts;
        public YahtzeeBoard()
        {
            for (int i = 0; i < diceVals.Length;i++) { diceVals[i] = 0; diceHold[i] = false; locked[i] = false;}
            rolls = 0;
            noPts = false;
        }

        //getters and setters
        public int[] getDiceVals() {  return diceVals; }
        public void setDiceVals(int[] diceVals) { this.diceVals = diceVals; }
        public bool[] getHold() { return diceHold;}
        public void setHoldDice(bool[] hold) { this.diceHold = hold; }
        public bool[] getLocked() { return locked; }
        public void setLocked(bool[] locked) {  this.locked = locked; }
        public int getRolls() { return rolls; }
        public bool getNoPts() { return noPts; }
        public void setNoPts(bool noPts) { this.noPts = noPts; }


        //marks a dice as 'free' or 'hold'
        public void markFree(int index) { diceHold[index] = false; }
        public void markHold(int index) { diceHold[index] = true; }
        public void markLocked(int index) { locked[index] = true; }
        public void unlockDice() { for (int i = 0; i < locked.Length; i++) { locked[i] = false; } }

        //increments and resets roll variable
        public void incrRolls() { rolls++; }
        public void resetRolls() { rolls = 0; }

        //Rolls all dice that are labeled 'free'
        public void rollDice()
        {
            Random rnd = new Random();
            for (int i = 0; i < diceVals.Length; i++)
            {
                if (diceHold[i] == false)
                {
                    diceVals[i] = rnd.Next(1, 6);
                }
            }
        }
        //calculates the number of each value of dice currently in 'held' state
        //all:true when you want to calculate values of all dice, false if just ones being held
        public int[] calculateVals(bool all)
        {
            vals = new int[6];
            for (int i = 0; i < diceVals.Length; i++)
            {
                //only calculates the values being held
                if (all || diceHold[i] == true)
                {
                    switch (diceVals[i])
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
            }
            return vals;
        }

    }
}
