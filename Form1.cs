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
            loadingLabel.Text = "Loading...";
            ticTac = new TicTacToe();
            ticTac.Show();
            loadingLabel.Text = "";
        }
        /* War */
        private void button2_Click(object sender, EventArgs e)
        {
            loadingLabel.Text = "Loading...";
            war = new War();
            war.Show();
            loadingLabel.Text = "";
        }
        /* Battleship */
        private void button3_Click(object sender, EventArgs e)
        {
            loadingLabel.Text = "Loading...";
            ship = new Battleship();
            ship.Show();
            loadingLabel.Text = "";
        }
        /* Yhatzee */
        private void button4_Click(object sender, EventArgs e)
        {
            loadingLabel.Text = "Loading...";
            yahtzee = new Yahtzee();
            yahtzee.Show();
            loadingLabel.Text = "";
        }
        /* Minesweeper */
        private void minesweeper_Click(object sender, EventArgs e)
        {
            loadingLabel.Text = "Loading...";
            masweeper = new Minesweeper();
            masweeper.Show();
            loadingLabel.Text = "";
        }
        /* Checkers */
        private void checkersButton_Click(object sender, EventArgs e)
        {
            loadingLabel.Text = "Loading...";
            checkers = new Checkers();
            checkers.Show();
            loadingLabel.Text = "";
        }
    }
}
