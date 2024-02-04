using System;
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
            plr.getBoard().setNoPts(false);
            //checks which ones are available
            for (int i = 0; i < sections.Length;i++)
            {
                switch(i)
                {
                    //Ones
                    case 0:
                        //if it has not been chosen, and there are three or more, make stuff available to choose
                        if (sections[i] == false && vals[0] >= 1)
                        {
                            user1s.Visible = true;
                            changeMade = true;
                        }
                        break;
                    //twos
                    case 1:
                        if (sections[i] == false && vals[1] >= 1)
                        {
                            user2s.Visible = true;
                            changeMade = true;
                        }
                        break;
                    //threes
                    case 2:
                        if (sections[i] == false && vals[2] >= 1)
                        {
                            user3s.Visible = true;
                            changeMade = true;
                        }
                        break;
                    //fours
                    case 3:
                        if (sections[i] == false && vals[3] >= 1)
                        {
                            user4s.Visible = true;
                            changeMade = true;
                        }
                        break;
                    //fives
                    case 4:
                        if (sections[i] == false && vals[4] >= 1)
                        {
                            user5s.Visible = true;
                            changeMade = true;
                        }
                        break;
                    //sixes
                    case 5:
                        if (sections[i] == false && vals[5] >= 1)
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
                        if (sections[i] == false && (vals[0] != 0 || vals[1] != 0 || vals[2] != 0 || vals[3] != 0 || vals[4] != 0))
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
                if (user1s.Checked == false) { user1s.Visible = true; }
                if (user2s.Checked == false) { user2s.Visible = true; }
                if (user3s.Checked == false) { user3s.Visible = true; }
                if (user4s.Checked == false) { user4s.Visible = true; }
                if (user5s.Checked == false) { user5s.Visible = true; }
                if (user6s.Checked == false) { user6s.Visible = true; }
                if (user3k.Checked == false) { user3k.Visible = true; }
                if (user4k.Checked == false) { user4k.Visible = true; }
                if (userfh.Checked == false) { userfh.Visible = true; }
                if (userss.Checked == false) { userss.Visible = true; }
                if (userls.Checked == false) { userls.Visible = true; }
                if (userya.Checked == false) { userya.Visible = true; }
            }

        }
        //calculates the points from a given round
        //isZero adds zero points to specified selection
        public void calculatePoints(YahtzeePlayer plr, bool isZero)
        {
            int[] points = plr.getPoints();
            bool[] sections = plr.getSections();
            YahtzeeBoard board = plr.getBoard();
            bool[] hold = board.getHold();
            bool[] locked = board.getLocked();
            int[] vals = board.calculateVals(false);
            int[] diceVals = board.getDiceVals();

            //finds appropriate sections and marks them complete if done so
            if (user1s.Checked == true && user1s.Visible == true)
            {
                if (isZero) { points[0] = 0; } else { points[0] = vals[0]; }
                sections[0] = true;
            }
            if (user2s.Checked == true && user2s.Visible == true)
            {
                if (isZero) { points[1] = 0; } else { points[1] = vals[1] * 2; }
                sections[1] = true;
            }
            if (user3s.Checked == true && user3s.Visible == true)
            {
                if (isZero) { points[2] = 0; } else { points[2] = vals[2] * 3; }
                sections[2] = true;
            }
            if (user4s.Checked == true && user4s.Visible == true)
            {
                if (isZero) { points[3] = 0; } else { points[3] = vals[3] * 4; }
                sections[3] = true;
            }
            if (user5s.Checked == true && user5s.Visible == true)
            {
                if (isZero) { points[4] = 0; } else { points[4] = vals[4] * 5; }
                sections[4] = true;
            }
            if (user6s.Checked == true && user6s.Visible == true)
            {
                if (isZero) { points[5] = 0; } else { points[5] = vals[5] * 6; }
                sections[5] = true;
            }
            if (user3k.Checked == true && user3k.Visible == true)
            {
                int pts = 0;
                for (int i = 0; i < diceVals.Length; i++)
                {
                    pts += diceVals[i];
                }
                sections[6] = true;
                if (isZero) { points[6] = 0; } else { points[6] = pts; }
            }
            if (user4k.Checked == true && user4k.Visible == true)
            {
                int pts = 0;
                for (int i = 0; i < diceVals.Length; i++)
                {
                    pts += diceVals[i];
                }
                sections[7] = true;
                if (isZero) { points[7] = 0; } else { points[7] = pts; }
            }
            if (userfh.Checked == true && userfh.Visible == true)
            {
                sections[8] = true;
                if (isZero) { points[8] = 0; } else { points[8] = 25; }
            }
            if (userss.Checked == true && userss.Visible == true)
            {
                sections[9] = true;
                if (isZero) { points[9] = 0; } else { points[9] = 30; }
            }
            if (userls.Checked == true && userls.Visible == true)
            {
                sections[10] = true;
                if (isZero) { points[10] = 0; } else { points[10] = 40; }
            }
            if (userya.Checked == true && userya.Visible == true)
            {
                sections[11] = true;
                if (isZero) { points[11] = 0; } else { points[11] = 50; }
            }
            if (userch.Checked == true && userch.Visible == true)
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
                if (points[i] != -1)
                {
                    output += points[i].ToString() + "\n";
                }
                else { output += "\n"; }
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
        
        public void addCPUPoints()
        {
            int[] points = CPU.getPoints();
            String output = "";
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i] != -1)
                {
                    output += points[i].ToString() + "\n";
                }
                else
                {
                    output += "\n";
                }
                if (i == 5)
                {
                    output += "\n";
                }
            }
            cpuPoints.Text = output;
        }
        //runs win condition check
        public bool checkWin()
        {
            int[] cpuPoints = CPU.getPoints();
            int[] plrPoints = player.getPoints();
            bool[] cpuSections = CPU.getSections();
            bool[] plrSections = player.getSections();

            //calculates total points
            int cpuPts = 0;
            int plrPts = 0;
            for (int i = 0; i < cpuPoints.Length;i++)
            {
                if (cpuPoints[i] != -1) { cpuPts += cpuPoints[i]; }
                if (plrPoints[i] != -1) { plrPts += plrPoints[i]; }
                //adds bonus points
                if (i == 5)
                {
                    if (cpuPts >= 63)
                    {
                        cpuPts += 32;
                        cpuBonus.Text = "Top Bonus: 32";
                    }
                    if (plrPts >= 63)
                    {
                        plrPts += 32;
                        userBonus.Text = "Top Bonus: 32";
                    }
                }
            }

            cpuTotal.Text = "Total: " + cpuPts.ToString();
            usrTotal.Text = "Total: " + plrPts.ToString();
            //check for winner
            bool complete = true;
            for (int i = 0; i < plrSections.Length;i++)
            {
                if (plrSections[i] != true || cpuSections[i] != true )
                {
                    complete = false;
                    break;
                }
            }
            if (complete)
            {
                rollButton.Text = "Reset";
                if (cpuPts > plrPts)
                {
                    winLabel.Text = "You loose!";
                }
                else if (cpuPts < plrPts)
                {
                    winLabel.Text = "You Win!";
                }
                else
                {
                    winLabel.Text = "You Tie!";
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void resetBoard()
        {
            player = new YahtzeePlayer();
            CPU = new YahtzeePlayer();
            YahtzeeBoard blank = new YahtzeeBoard();
            updateBoard(blank);

            cpuPoints.Text = "";
            userPoints.Text = "";
            rollButton.Text = "Roll";
            usrTotal.Text = "Total:";
            cpuTotal.Text = "Total:";
            cpuBonus.Text = "Top Bonus:";
            userBonus.Text = "Top Bonus:";
            winLabel.Text = "";
            user1s.Checked = false;
            user2s.Checked = false;
            user3s.Checked = false;
            user4s.Checked = false;
            user5s.Checked = false;
            user6s.Checked = false;
            user3k.Checked = false;
            user4k.Checked = false;
            userss.Checked = false;
            userls.Checked = false;
            userfh.Checked = false;
            userya.Checked = false;
            userch.Checked = false;
            dice.Enabled = true;
            dice1.Enabled = true;
            dice2.Enabled = true;
            dice3.Enabled = true;
            dice4.Enabled = true;
        }
        //roll button action
        private void rollButton_Click(object sender, EventArgs e)
        {
            //runs the player roll sequence
            if (rollButton.Text == "Roll" || rollButton.Text == "Your Turn")
            {
                if (rollButton.Text == "Your Turn")
                {
                    rollButton.Text = "Roll";
                    dice.Enabled = true;
                    dice1.Enabled = true;
                    dice2.Enabled = true;
                    dice3.Enabled = true;
                    dice4.Enabled = true;
                    player.getBoard().setNoPts(false);
                }
                YahtzeeBoard board = player.getBoard();
                int rolls = board.getRolls();
                board.incrRolls();
                board.rollDice();
                updateBoard(board);
                if (rolls >= 2)
                {
                    rollButton.Visible = false;
                }
            }
            //CPU turn
            else if (rollButton.Text == "CPU Turn" || rollButton.Text == "Next")
            {
                //starts CPU roll sequence
                rollButton.Text = "Next";
                dice.Enabled = false;
                dice1.Enabled = false;
                dice2.Enabled = false;
                dice3.Enabled = false;
                dice4.Enabled = false;
                if (CPU.rollAndDecide())
                {
                    rollButton.Text = "Your Turn";
                    addCPUPoints();
                    updateBoard(CPU.getBoard());
                    CPU.resetBoard();
                    if (checkWin())
                    {
                        rollButton.Text = "Reset";
                    }
                }
                else
                {
                updateBoard(CPU.getBoard());
                }
            }
            //reset board
            else
            {
                resetBoard();
            }
        }
        //confirm button action
        private void confirmButton_Click(object sender, EventArgs e)
        {
            confirmButton.Visible = false;
            YahtzeeBoard board = player.getBoard();
            if (board.getNoPts() == false)
            {
                calculatePoints(player, false);
                player.setBoard(new YahtzeeBoard());
                hideChecks(-1);
                updateBoard(player.getBoard());
            }
            else
            {
                calculatePoints(player, true);
                player.setBoard(new YahtzeeBoard());
                hideChecks(-1);
                updateBoard(player.getBoard());
            }
            rollButton.Text = "CPU Turn";
            rollButton.Visible = true;
            if (checkWin())
            {
                rollButton.Text = "Reset";
            }
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
        private void user1s_CheckedChanged(object sender, EventArgs e) { if (user1s.Checked == true) { hideChecks(0); confirmButton.Visible = true; } else { findOptions(player); confirmButton.Visible = false; } }
        private void user2s_CheckedChanged(object sender, EventArgs e) { if (user2s.Checked == true) { hideChecks(1); confirmButton.Visible = true; } else { findOptions(player); confirmButton.Visible = false; } }
        private void user3s_CheckedChanged(object sender, EventArgs e) { if (user3s.Checked == true) { hideChecks(2); confirmButton.Visible = true; } else { findOptions(player); confirmButton.Visible = false; } }
        private void user4s_CheckedChanged(object sender, EventArgs e) { if (user4s.Checked == true) { hideChecks(3); confirmButton.Visible = true; } else { findOptions(player); confirmButton.Visible = false; } }
        private void user5s_CheckedChanged(object sender, EventArgs e) { if (user5s.Checked == true) { hideChecks(4); confirmButton.Visible = true; } else { findOptions(player); confirmButton.Visible = false; } }
        private void user6s_CheckedChanged(object sender, EventArgs e) { if (user6s.Checked == true) { hideChecks(5); confirmButton.Visible = true; } else { findOptions(player); confirmButton.Visible = false; } }
        private void user3k_CheckedChanged(object sender, EventArgs e) { if (user3k.Checked == true) { hideChecks(6); confirmButton.Visible = true; } else { findOptions(player); confirmButton.Visible = false; } }
        private void user4k_CheckedChanged(object sender, EventArgs e) { if (user4k.Checked == true) { hideChecks(7); confirmButton.Visible = true; } else { findOptions(player); confirmButton.Visible = false; } }
        private void userfh_CheckedChanged(object sender, EventArgs e) { if (userfh.Checked == true) { hideChecks(8); confirmButton.Visible = true; } else { findOptions(player); confirmButton.Visible = false; } }
        private void userss_CheckedChanged(object sender, EventArgs e) { if (userss.Checked == true) { hideChecks(9); confirmButton.Visible = true; } else { findOptions(player); confirmButton.Visible = false; } }
        private void userls_CheckedChanged(object sender, EventArgs e) { if (userls.Checked == true) { hideChecks(10); confirmButton.Visible = true; } else { findOptions(player); confirmButton.Visible = false; } }
        private void userya_CheckedChanged(object sender, EventArgs e) { if (userya.Checked == true) { hideChecks(11); confirmButton.Visible = true; } else { findOptions(player); confirmButton.Visible = false; } }
        private void userch_CheckedChanged(object sender, EventArgs e) { if (userch.Checked == true) { hideChecks(12); confirmButton.Visible = true; } else { findOptions(player); confirmButton.Visible = false; } }

        private void testButton_Click(object sender, EventArgs e)
        {
            YahtzeePlayer test = new YahtzeePlayer();
            int[] board = { 4,4,4,1,1 };
            test.getBoard().setDiceVals(board);
            test.getBoard().incrRolls();
            test.getBoard().incrRolls();
            test.getBoard().incrRolls();
            bool[] sections = { true, false, true, true, true, true, true, true, true, false, true, true, true };
            test.setSections(sections);
            test.rollAndDecide();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            resetBoard();
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
                points[i] = -1;
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
        public void resetBoard() { board = new YahtzeeBoard(); }

        /* Automatic Player Decision Functions */
        //rolls dice, makes decision based on what the roll is
        //returns true if decision was made, false if not
        public bool rollAndDecide()
        {
            board.rollDice();
            board.incrRolls();
            int[] diceVals = board.getDiceVals();
            bool[] hold = board.getHold();
            int[] vals = board.calculateVals(true); //gets number of values currently on the board

            //look at dice values
            //straight check
            int strVals = 0;
            for (int i = 0; i < vals.Length; i++)
            {
                if (vals[i] >= 1)
                {
                    strVals++;
                }
            }
            if (strVals > 3 && (sections[10] == false || sections[9] == false))
            {
                int[] used = { 0, 0, 0, 0, 0, 0 };
                int[] usedIndex = { -1, -1, -1, -1, -1, -1 };
                bool checkValid = true;
                //finds the values that make up the straight
                for (int i = 0; i < diceVals.Length; i++)
                {
                    if (vals[diceVals[i] - 1] >= 1 && used[diceVals[i] - 1] == 0)
                    {
                        used[diceVals[i] - 1] = 1;
                        usedIndex[diceVals[i] - 1] = i;
                    }
                }
                //deicdes which ones to hold, looks for either 1,6 or neither
                if (used[5] == 1 && ((used[4] == 1 && used[3] == 1) || (used[3] == 1 && used[2] == 1) || (used[4] == 1 && used[2] == 1)))
                {
                    clearHold(hold);
                    for (int i = 1; i < usedIndex.Length; i++)
                    {
                        if (usedIndex[i] != -1) { hold[usedIndex[i]] = true; }
                    }
                }
                else if (used[0] == 1 && ((used[1] == 1 && used[2] == 1) || (used[2] == 1 && used[3] ==1) || (used[1] == 1 && used[3] == 1)))
                {
                    clearHold(hold);
                    for (int i = 0; i < usedIndex.Length -1; i++)
                    { 
                        if (usedIndex[i] != -1) { hold[usedIndex[i]] = true; }
                    }
                }
                else
                {
                    clearHold(hold);
                    int gap = 0;
                    //checks to see if there is a close straight in the middle of the range
                    //gap of 2, not a possible straight
                    for (int i = 0; i < used.Length; i++)
                    {
                        if (used[i] == 1 && gap < 2)
                        {
                            gap = 0;
                        }
                        else if (used[i] == 0)
                        {
                            gap++;
                        }
                        if (gap == 2)
                        {
                            break;
                        }
                    }
                    if (gap != 2)
                    {
                        //places into hold if there is a close straight
                        for (int i = 1; i < usedIndex.Length - 1; i++)
                        {
                            if (usedIndex[i] != -1) { hold[usedIndex[i]] = true; }
                        }
                    }
                    else
                    {
                        //cannot be a straight, does not run checks
                        checkValid = false;
                    }
                }
                //checks to see if straight already exists
                if (sections[10] == false && checkValid == true)
                {
                    if (vals[0] == 1 && vals[1] == 1 && vals[2] == 1 && vals[3] == 1 && vals[4] == 1)
                    {
                        sections[10] = true;
                        points[10] = 40;
                        return true;
                    }
                    else if (vals[1] == 1 && vals[2] == 1 && vals[3] == 1 && vals[4] == 1 && vals[5] == 1)
                    {
                        sections[10] = true;
                        points[10] = 40;
                        return true;
                    }
                }
                //small straight
                if (sections[9] == false && board.getRolls() == 3 && checkValid == true)
                {
                    if (vals[0] >= 1 && vals[1] >= 1 && vals[2] >= 1 && vals[3] >= 1)
                    {
                        sections[9] = true;
                        points[9] = 30;
                        return true;
                    }
                    else if (vals[1] >= 1 & vals[2] >= 1 && vals[3] >= 1 && vals[4] >= 1)
                    {
                        sections[9] = true;
                        points[9] = 30;
                        return true;
                    }
                    else if (vals[2] >= 1 && vals[3] >= 1 && vals[4] >= 1 && vals[5] >= 1)
                    {
                        sections[9] = true;
                        points[9] = 30;
                        return true;
                    }
                }
                //straight could not be decided on this turn
                if (board.getRolls() != 3)
                {
                    return false;
                }
                else
                {
                    //nothing else could be selected, try chance
                    if (sections[12] == false)
                    {
                        allHold(hold);
                        sections[12] = true;
                        int tot = 0;
                        for (int i = 0; i < diceVals.Length; i++)
                        {
                            tot += diceVals[i];
                            hold[i] = true;
                        }
                        points[12] = tot;
                        return true;
                    }
                    else
                    {
                        //attempts to fill in a number
                        if ((sections[0] == false && vals[0] > 0) || (sections[1] == false && vals[1] > 0) || (sections[2] == false && vals[2] > 0)
                            || (sections[3] == false && vals[3] > 0) || (sections[4] == false && vals[4] > 0) || (sections[5] == false && vals[5] > 0))
                        {
                            //find highest value that can still be used
                            int val = 0;
                            int num = 0;
                            for (int i = 0; i < vals.Length; i++)
                            {
                                if (vals[i] > val && sections[i] == false)
                                {
                                    val = vals[i];
                                    num = i;
                                }
                            }
                            setHold(hold, num + 1, diceVals);
                            sections[num] = true;
                            points[num] = val * (num + 1);
                            return true;
                        }
                        else
                        {
                            //fills in next value with 0, starting at ones
                            setHold(hold, 0, diceVals);
                            for (int i = 0; i < sections.Length; i++)
                            {
                                if (sections[i] == false)
                                {
                                    sections[i] = true;
                                    points[i] = 0;
                                    break;
                                }
                            }
                            return true;

                        }
                    }
                }
            }
            else
            {
                //value type, either yahtzee, 4k, 3k, or values
                int two = 0;
                int three = 0;
                int four = 0;
                int yahtzee = 0;
                for (int i = 0; i < vals.Length; i++)
                {
                    switch (vals[i])
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            if (two < i + 1) { two = i + 1; }
                            break;
                        case 3:
                            three = i + 1;
                            break;
                        case 4:
                            four = i + 1;
                            break;
                        case 5:
                            yahtzee = i + 1;
                            break;
                    }
                }
                //yahtzee or full house, take immediately
                //yahtzee
                if (sections[11] == false && yahtzee != 0)
                {
                    sections[11] = true;
                    setHold(hold, yahtzee, diceVals);
                    points[11] = 50;
                    return true;
                }
                //full house
                else if (sections[8] == false && three != 0 && two != 0)
                {
                    allHold(hold);
                    sections[8] = true;
                    for (int i = 0; i < hold.Length; i++)
                    {
                        hold[i] = true;
                    }
                    points[8] = 25;
                    return true;
                }
                else
                {
                    //otherwise look to get as close to yahtzee as possible, sets values to hold
                    if (board.getRolls() != 3)
                    {
                        //if there are still ones in the bottom section to check (except straights)
                        if (sections[6] == false || sections[7] == false || sections[8] == false || sections[11] == false || sections[12] == false)
                        {
                            //places four, three, or highest two value into hold
                            if (four != 0)
                            {
                                setHold(hold, four, diceVals);
                            }
                            else if (three != 0)
                            {
                                setHold(hold, three, diceVals);
                            }
                            else if (two != 0)
                            {
                                setHold(hold, two, diceVals);
                            }
                            return false;
                        }
                        else
                        {
                            //now needs to fill in specific numbers, regardless of how many there are
                            int lrgst = 0;
                            for (int i = 0; i < vals.Length; i++)
                            {
                                if (vals[i] != 0 && lrgst < i + 1 && sections[i] == false)
                                {
                                    lrgst = i + 1;
                                }
                            }
                            setHold(hold, lrgst, diceVals);
                            return false;
                        }
                    }
                    else
                    {
                        //four of a kind
                        if (four != 0 && (sections[7] == false || sections[four - 1] == false || sections[6] == false))
                        {
                            setHold(hold, four, diceVals);
                            if (sections[7] == true || (four < 3 && sections[four -1] == false))
                            {
                                sections[four - 1] = true;
                                points[four - 1] = four * 4;
                            }
                            else if (sections[7] == false)
                            {
                                sections[7] = true;
                                int pts = 0;
                                for (int i =0; i < diceVals.Length; i++)
                                {
                                    pts += diceVals[i];
                                }
                                points[7] = pts;
                            }
                            //puts in 3k if cannot be put elsewhere
                            else 
                            {
                                sections[6] = true;
                                int pts = 0;
                                for (int i = 0; i < diceVals.Length; i++)
                                {
                                    pts += diceVals[i];
                                }
                                points[6] = pts;
                            }
                            return true;
                        }
                        //three of a kind
                        else if (three != 0 && (sections[6] == false || sections[three-1] == false))
                        {
                            if (sections[6] == true || (three < 3 && sections[three-1] == false))
                            {
                                setHold(hold, three, diceVals);
                                sections[three - 1] = true;
                                points[three - 1] = three * 3;
                            }
                            else
                            {
                                setHold(hold, three, diceVals);
                                sections[6] = true;
                                int pts = 0;
                                for (int i = 0; i < diceVals.Length; i++)
                                {
                                    pts += diceVals[i];
                                }
                                points[6] = pts;
                            }
                            return true;
                        }
                        //top section (numbers)
                        else if ((sections[0] == false && vals[0] > 0) || (sections[1] == false && vals[1] > 0) || (sections[2] == false && vals[2] > 0)
                            || (sections[3] == false && vals[3] > 0) || (sections[4] == false && vals[4] > 0) || (sections[5] == false && vals[5] > 0))
                        {
                            //find highest value that can still be used
                            int val = 0;
                            int num = 0;
                            for (int i = 0; i < vals.Length; i++)
                            {
                                if (vals[i] > val && sections[i] == false)
                                {
                                    val = vals[i];
                                    num = i;
                                }
                            }
                            setHold(hold, num + 1, diceVals);
                            sections[num] = true;
                            points[num] = val * (num + 1);
                            return true;
                        }
                        else
                        {
                            //chance
                            if (sections[12] == false)
                            {
                                int tot = 0;
                                for (int i = 0; i < diceVals.Length; i++)
                                {
                                    tot += diceVals[i];
                                    hold[i] = true;
                                }
                                allHold(hold);
                                sections[12] = true;
                                points[12] = tot;
                                return true;
                            }
                            //fills in any remaining section with 0 if nothing else can be done
                            else
                            {
                                setHold(hold, 0, diceVals);
                                for (int i = 0; i < sections.Length; i++)
                                {
                                    if (sections[i] == false)
                                    {
                                        sections[i] = true;
                                        points[i] = 0;
                                        break;
                                    }
                                }
                                return true;
                            }
                        }
                    }                  
                }
            }
        }
        //sets the hold for the autogenerated sections, for visual clarity
        private void setHold(bool[] hold, int val, int[] diceVals)
        {

            for (int i =0; i < diceVals.Length; i++)
            {
                if (val == diceVals[i])
                {
                    hold[i] = true;
                }
                else
                {
                    hold[i] = false;
                }
            }
        }
        //adds every dice to the hold
        private void allHold(bool[] hold)
        {
            for (int i = 0; i < hold.Length; i++)
            {
                hold[i] = true;
            }
        }
        //sets all hold values to false
        private void clearHold(bool[] hold)
        {
            for (int i = 0; i < hold.Length; i++)
            {
                hold[i] = false;
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
                    diceVals[i] = rnd.Next(1, 7);
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
