using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        }
        //creates a new instance of yhatzee
        public void initYahtzee()
        {
            player = new YahtzeePlayer();
            CPU = new YahtzeePlayer();
        }

    }

    public class YahtzeePlayer
    {
        private bool[] sections = new bool[13];
        private YahtzeeBoard board;
        public YahtzeePlayer()
        {
            for (int i = 0; i < sections.Length; i++)
            {
                sections[i] = false;
            }
        }

        //getters and setters
        public bool[] getSections() { return sections; }
        public void setSections(bool[] sections) { this.sections = sections; }
        public YahtzeeBoard getBoard() { return board; }
        public void setBoard(YahtzeeBoard board) { this.board = board; }

    }

    public class YahtzeeBoard
    {
        private int[] freeDice = new int[6];
        private int[] holdDice = new int[6];
        public YahtzeeBoard()
        {
            for (int i = 0; i < freeDice.Length;i++) { freeDice[i] = -1; holdDice[i] = -1; }
        }

        //getters and setters
        public int[] getFreeDice() {  return freeDice; }
        public void setFreeDice(int[] freeDice) { this.freeDice = freeDice; }
        public int[] getHoldDice() { return holdDice;}
        public void setHoldDice(int[] holdDice) { this.holdDice = holdDice; }
    }
}
