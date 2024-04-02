using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesForClass
{
    public partial class STTT : Form
    {
        TTTGame[] smallGames = new TTTGame[9];
        Label[] mainGame = new Label[9];
        STTTAI CPU = new STTTAI("O");
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
                    //creates labels for large game
                    Label label = new Label();
                    label.Name = counter.ToString();
                    label.Size = new Size((buttonSize * 3), (buttonSize * 3));
                    label.Location = new Point(startX + buffer + (i * arraySize), startY + buffer + (j * arraySize));
                    label.Font = new Font("Microsoft Sans Sarif", 50);
                    label.BackColor = Color.Transparent;
                    this.Controls.Add(label);
                    mainGame[counter] = label;
                    
                    //creates buttons for smaller game
                    TTTGame newGame = new TTTGame(generateSmallBoard(startX + (i * arraySize), startY + (j * arraySize), counter));
                    smallGames[counter] = newGame;
                    counter++;
                }
            }
            //sends all labels to back
            for (int i = 0; i < mainGame.Length; i++)
            {
                mainGame[i].SendToBack();
            }

        }
        private Button[] generateSmallBoard(int startX, int startY, int name)
        {
            int counter = 0;
            Button[] buttons = new Button[9];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button button = new Button();
                    button.Name = name.ToString() + "-" + counter.ToString();
                    button.Size = new Size(buttonSize, buttonSize);
                    button.Location = new Point(startX + buffer + (buttonSize * i), startY + buffer + (buttonSize * j));
                    button.Font = new Font("Microsoft Sans Sarif", 20);
                    button.BringToFront();
                    button.MouseDown += boardButtonClick;
                    buttons[counter] = button;
                    this.Controls.Add(button);
                    counter++;
                }
            }

            return buttons;
        }
        #endregion
        #region main game functions
        //returns winner of main game, if there is one
        //0: is winner, 1: false if O, true if X
        private bool[] checkWinner()
        {
            String winner = "";
            bool[] ret = { false, false };

            //checks game conditions
            if (mainGame[0].Text == mainGame[1].Text && mainGame[1].Text == mainGame[2].Text && mainGame[2].Text != "")
            {
                winner = mainGame[0].Text;
            }
            else if (mainGame[3].Text == mainGame[4].Text && mainGame[4].Text == mainGame[5].Text && mainGame[5].Text != "")
            {
                winner = mainGame[3].Text;
            }
            else if (mainGame[6].Text == mainGame[7].Text && mainGame[7].Text == mainGame[8].Text && mainGame[8].Text != "")
            {
                winner = mainGame[5].Text;
            }
            else if (mainGame[0].Text == mainGame[4].Text && mainGame[4].Text == mainGame[8].Text && mainGame[8].Text != "")
            {
                winner = mainGame[7].Text;
            }
            else if (mainGame[2].Text == mainGame[4].Text && mainGame[4].Text == mainGame[6].Text && mainGame[6].Text != "")
            {
                winner = mainGame[5].Text;
            }

            //checks for winner
            if (winner == "X")
            {
                ret[0] = true;
                ret[1] = true;
                return ret;
            }
            else if (winner == "O")
            {
                ret[0] = true;
                ret[1] = false;
                return ret;
            }
            else
            {
                return ret;
            }
        }
        //updates board when someone wins small game, returns true if game is won
        public bool updateBoard(int index, String let)
        {
            mainGame[index].BringToFront();
            mainGame[index].Text = let;
            bool[] winner = checkWinner();
            if (winner[0])
            {
                //close out board
                for (int i = 0; i < smallGames.Length; i++)
                {
                    smallGames[i].changeButtonEnable(false);
                }

                if (winner[1])
                {
                    //player is winner
                }
                else
                {
                    //CPU is winner
                }
            }
            return winner[0];
        }
        #endregion

        #region buttons
        //main button function for all board buttons
        public void boardButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            //parses button name to get current index, and the location of the new index
            String name0 = button.Name[0].ToString();
            String name1 = button.Name[2].ToString();
            int currentGameIndex = Convert.ToInt32(name0);
            int newIndex = Convert.ToInt32(name1);
            int retIndex;
            bool gameOver = false;

            smallGames[currentGameIndex].changeBoard(newIndex, "X");
            smallGames[currentGameIndex].changeButtonEnable(false);
            //checks for winner
            String winner = smallGames[currentGameIndex].getWinner();
            if (winner != "")
            {
                gameOver = updateBoard(currentGameIndex, winner);
            }

            //makes sure the game is not over when passing control to CPU
            if (!gameOver)
            {
                //change to AI player
                smallGames[newIndex].changeButtonEnable(true);
                retIndex = CPU.makeMove(smallGames, newIndex);
                //checks for winner of this game
                winner = smallGames[newIndex].getWinner();
                if (winner != "")
                {
                    gameOver = updateBoard(newIndex, winner);
                }
                smallGames[newIndex].changeButtonEnable(false);
                //makes sure game is not over when passing control back to player
                if (!gameOver)
                {
                    smallGames[retIndex].changeButtonEnable(true);
                }
            }
        }
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
        //changes the enablement of the buttons
        public void changeButtonEnable(bool enable)
        {
            for (int i =0; i < board.Length; i++)
            {
                board[i].Enabled = enable;
            }
        }
    }
    /*
     * Represents the AI playing against player
     */
    public class STTTAI
    {
        String letter = "O";
        public STTTAI(string letter)
        {
            this.letter = letter;
        }

        //makes move for AI
        #region movement functions
        //makes move for AI
        //ret: location of new board to be made available
        public int makeMove(TTTGame[] largeBoard, int currentIndex)
        {
            int ret = 0;
            TTTGame currentGame = largeBoard[currentIndex];
            //checks to see if AI can make move to win
            //look to see if player can win
            //try to make move that does not allow player to make easy win in their next move
            return ret;
        }
        #endregion
    }
}
