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
    public partial class STTT : Form
    {
        TTTGame[] smallGames;
        String[] mainGame;
        public STTT()
        {
            InitializeComponent();
        }
        //generates STTT board on form
        #region generate functions
        public void generate()
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
        private String[] board;

        public TTTGame()
        {
            board = new String[6];
        }
        //getters
        public String getWinner() { return winner; }
        public String[] getBoard() { return board;}

        //Changes board, only if winner is not yet decided
        public void changeBoard(int index, String usr)
        {
            if (winner == "")
            {
                board[index] = usr;
                checkWinner();
            }
        }
        //checks for small board winner, changes winner variable if a player has won the game
        private void checkWinner()
        {
            if (board[0] == board[1] && board[1] == board[2] && board[2] != null)
            {
                winner = board[0];
            }
            else if (board[3] == board[4] && board[4] == board[5] && board[5] != null)
            {
                winner = board[3];
            }
            else if (board[6] == board[7] && board[7] == board[8] && board[8] != null)
            {
                winner = board[5];
            }
            else if (board[0] == board[4] && board[4] == board[8] && board[8] != null)
            {
                winner = board[7];
            }
            else if (board[2] == board[4] && board[4] == board[6] && board[6] != null)
            {
                winner = board[5];
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
