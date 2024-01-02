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
            for (int i = 0; i < 6; i++)
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
                    case 5:
                        dice5.Text = val.ToString();
                        dice5.BackColor = clr;
                        break;
                }
            }
        }
        //Rolls all dice that are labeled 'free'
        public void rollDice(YahtzeeBoard board)
        {
            Random rnd = new Random();
            int[] freeDice = board.getFreeDice();

            for (int i  = 0; i < 6; i++)
            {
                if (freeDice[i] != -1)
                {
                    freeDice[i] = rnd.Next(1, 6);
                }
            }
            
            board.setFreeDice(freeDice);
            
        }
        //roll button action
        private void rollButton_Click(object sender, EventArgs e)
        {
            rollDice(player.getBoard());
            updateBoard(player.getBoard());
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
        private int[] freeDice = new int[6];
        private int[] holdDice = new int[6];
        public YahtzeeBoard()
        {
            for (int i = 0; i < freeDice.Length;i++) { freeDice[i] = 0; holdDice[i] = -1; }
        }

        //getters and setters
        public int[] getFreeDice() {  return freeDice; }
        public void setFreeDice(int[] freeDice) { this.freeDice = freeDice; }
        public int[] getHoldDice() { return holdDice;}
        public void setHoldDice(int[] holdDice) { this.holdDice = holdDice; }

    }
}
