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
    public partial class Form1 : Form
    {
        private TicTacToe ticTac;
        private War war;
        private Battleship ship;
        private Yahtzee yahtzee;
        private Minesweeper masweeper;
        private Checkers checkers;
        public Form1()
        {
            InitializeComponent();
        }
        /* Tic Tac Toe game */
        private void button1_Click(object sender, EventArgs e)
        {
            ticTac = new TicTacToe();
            ticTac.Show();
        }
        /* War */
        private void button2_Click(object sender, EventArgs e)
        {
            war = new War();
            war.Show();
        }
        /* Battleship */
        private void button3_Click(object sender, EventArgs e)
        {
            ship = new Battleship();
            ship.Show();
        }
        /* Yhatzee */
        private void button4_Click(object sender, EventArgs e)
        {
            yahtzee = new Yahtzee();
            yahtzee.Show();
        }
        /* Minesweeper */
        private void minesweeper_Click(object sender, EventArgs e)
        {
            masweeper = new Minesweeper();
            masweeper.Show();
        }
        /* Checkers */
        private void checkersButton_Click(object sender, EventArgs e)
        {
            checkers = new Checkers();
            checkers.Show();
        }
    }
}
