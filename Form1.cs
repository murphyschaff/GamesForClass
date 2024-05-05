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
        private STTT sttt;
        private WordGuess wordGuess;
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
        /* Super Tic-Tac-Toe */
        private void STTTbutton_Click(object sender, EventArgs e)
        {
            loadingLabel.Text = "Loading...";
            sttt = new STTT();
            sttt.Show();
            loadingLabel.Text = "";
        }
        /* WordGuess */
        private void wordGuessButton_Click(object sender, EventArgs e)
        {
            loadingLabel.Text = "Loading...";
            wordGuess = new WordGuess();
            wordGuess.Show();
            loadingLabel.Text = "";
        }
        /* Random button */
        private void supriseButton_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int val = rnd.Next(0,7);
            switch(val)
            {
                case 0:
                    button1_Click(sender, e);
                    break;
                case 1:
                    button2_Click(sender, e);
                    break;
                case 2:
                    button3_Click(sender, e);
                    break;
                case 3:
                    button4_Click(sender, e);
                    break;
                case 4:
                    minesweeper_Click(sender, e);
                    break;
                case 5:
                    checkersButton_Click(sender, e);
                    break;
                case 6:
                    STTTbutton_Click(sender, e);
                    break;
                case 7:
                    wordGuessButton_Click(sender, e);
                    break;
            }
        }

    }
}
