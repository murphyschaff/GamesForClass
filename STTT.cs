using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesForClass
{
    public partial class STTT : Form
    {
        TTTGame[] smallGames = new TTTGame[9];
        String[] mainGame = new string[9];
        private int buttonSize = 50;
        private int buffer = 5;
        public STTT()
        {
            InitializeComponent();
            generate();
        }
        //generates STTT board on form
        #region generate functions
        public void generate()
        {
            int startX = 190;
            int startY = 90;
            int counter = 0;
            int arraySize = (buttonSize * 3) + (buffer * 2);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    TTTGame newGame = new TTTGame(generateSmallBoard(startX + (i * arraySize), startY + (j * arraySize)));
                    smallGames[counter] = newGame;
                    counter++;
                }
            }

        }
        private Button[] generateSmallBoard(int startX, int startY)
        {
            int counter = 0;
            Button[] buttons = new Button[9];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button button = new Button();
                    button.Name = i.ToString() + "-" + j.ToString();
                    button.Size = new Size(buttonSize, buttonSize);
                    button.Location = new Point(startX + buffer + (buttonSize * i), startY + buffer + (buttonSize * j));
                    button.Font = new Font("Microsoft Sans Sarif", 20);
                    buttons[counter] = button;
                    this.Controls.Add(button);
                    counter++;
                }
            }

            return buttons;
        }
        #endregion

        #region buttons
        /* Button Clicks */
        private void resetButton_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
    /*
     * Represents an individual Tic-Tac-Toe game
     */
    public class TTTGame
    {
        private String winner = "";
        private Button[] board;

        public TTTGame(Button[] board)
        {
            this.board = board;
        }
        //getters
        public String getWinner() { return winner; }
        public Button[] getBoard() { return board;}

        //Changes board, only if winner is not yet decided
        public void changeBoard(int index, String usr)
        {
            if (winner == "")
            {
                board[index].Text = usr;
                checkWinner();
            }
        }
        //checks for small board winner, changes winner variable if a player has won the game
        private void checkWinner()
        {
            if (board[0].Text == board[1].Text && board[1].Text == board[2].Text && board[2].Text != "")
            {
                winner = board[0].Text;
            }
            else if (board[3].Text == board[4].Text && board[4].Text == board[5].Text && board[5].Text != "")
            {
                winner = board[3].Text;
            }
            else if (board[6].Text == board[7].Text && board[7].Text == board[8].Text && board[8].Text != "")
            {
                winner = board[5].Text;
            }
            else if (board[0].Text == board[4].Text && board[4].Text == board[8].Text && board[8].Text != "")
            {
                winner = board[7].Text;
            }
            else if (board[2].Text == board[4].Text && board[4].Text == board[6].Text && board[6].Text != "")
            {
                winner = board[5].Text;
            } 
        }
    }
    /*
     * Represents the AI playing against player
     */
    public class STTCAI
    {
        String letter = "O";
        public STTCAI(string letter)
        {
            this.letter = letter;
        }

        //makes move for AI
        #region movement functions
        public void makeMove(TTTGame[] largeBoard, int currentIndex)
        {
            TTTGame currentGame = largeBoard[currentIndex];
            //checks to see if AI can make move to win
            //look to see if player can win
            //try to make move that does not allow player to make easy win in their next move
        }
        #endregion
    }
}
