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
        private int openMove = 0;
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
                    label.Font = new Font("Microsoft Sans Sarif", 100);
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
            background.SendToBack();
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
        private bool checkWinner()
        {
            String winner = "";
            bool open = false;
            //checks game conditions
            if (mainGame[2].Text != "" && mainGame[0].Text == mainGame[1].Text && mainGame[1].Text == mainGame[2].Text)
            {
                winner = mainGame[2].Text;
            }
            else if (mainGame[5].Text != "" && mainGame[3].Text == mainGame[4].Text && mainGame[4].Text == mainGame[5].Text)
            {
                winner = mainGame[5].Text;
            }
            else if (mainGame[8].Text != "" && mainGame[6].Text == mainGame[7].Text && mainGame[7].Text == mainGame[8].Text)
            {
                winner = mainGame[8].Text;
            }
            else if (mainGame[6].Text != "" && mainGame[0].Text == mainGame[3].Text && mainGame[3].Text == mainGame[6].Text)
            {
                winner = mainGame[6].Text;
            }
            else if (mainGame[1].Text != "" && mainGame[1].Text == mainGame[4].Text && mainGame[4].Text == mainGame[7].Text)
            {
                winner = mainGame[1].Text;
            }
            else if (mainGame[2].Text != "" && mainGame[2].Text == mainGame[5].Text && mainGame[5].Text == mainGame[8].Text)
            {
                winner = mainGame[2].Text;
            }
            else if (mainGame[8].Text != "" && mainGame[0].Text == mainGame[4].Text && mainGame[4].Text == mainGame[8].Text)
            {
                winner = mainGame[8].Text;
            }
            else if (mainGame[6].Text != "" && mainGame[2].Text == mainGame[4].Text && mainGame[4].Text == mainGame[6].Text)
            {
                winner = mainGame[6].Text;
            }
            else
            {
                //looks for a tie
                for (int i = 0; i < mainGame.Length; i++)
                {
                    if (mainGame[i].Text == "")
                    {
                        open = true;
                        break;
                    }
                }
                if (!open)
                {
                    winner = "T";
                    feedback.Text = "Tie!";
                }
            }

            //checks for winner
            if (winner == "X" || winner == "O" || winner == "T")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //updates board when someone wins small game, returns true if game is won
        public bool updateBoard(int index, String let)
        {
            mainGame[index].BringToFront();
            mainGame[index].Text = let;
            bool winner = checkWinner();
            if (winner)
            {
                //close out board
                for (int i = 0; i < smallGames.Length; i++)
                {
                    smallGames[i].changeButtonEnable(false);
                }
            }
            return winner;
        }
        //makes sure the game that is to be switched to is not won
        //returns: index of next game to open up
        private int changeGame(int current, int proposed)
        {
            //looks at next game, checks to see if there is a winner
            if (smallGames[proposed].getWinner() == "")
            {
                //changes board enablement to proposed board
                smallGames[current].changeButtonEnable(false);
                smallGames[proposed].changeButtonEnable(true);
                return proposed;
            }
            else
            {
                //opens all free spots
                enableAllOpenGames();
                openMove = 0;
                return -1;
            }
        }
        //runs sequence that ends the game, with winner
        private void endGame(String winner)
        {
            changeAllButtonEnables(false, -1);
            feedback.Text = winner + " the winner!";
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

            //if this is the first move of the game, disable all other buttons except for this index
            if (openMove == 0)
            {
                openMove = 1;
                changeAllButtonEnables(false, currentGameIndex);
            }

            smallGames[currentGameIndex].changeBoard(newIndex, "X");
            //checks for winner
            String winner = smallGames[currentGameIndex].getWinner();
            if (winner != "")
            {
                gameOver = updateBoard(currentGameIndex, winner);
            }

            //makes sure the game is not over when passing control to CPU
            if (!gameOver)
            {
                newIndex = changeGame(currentGameIndex, newIndex);

                //change to AI player
                if (newIndex != -1)
                {
                    retIndex = CPU.makeMove(smallGames, newIndex);
                }
                else
                {
                    //makes move on any board if they are all open
                    int[] ret = CPU.makeOpenMove(smallGames);
                    newIndex = ret[0];
                    retIndex = ret[1];
                    changeAllButtonEnables(false, retIndex);
                    openMove = 1;
                }
                //checks for winner of this game
                winner = smallGames[newIndex].getWinner();
                if (winner != "")
                {
                    gameOver = updateBoard(newIndex, winner);
                }
                //makes sure game is not over when passing control back to player
                if (!gameOver)
                {
                    changeGame(newIndex, retIndex);

                }
                else
                {
                    //game is over
                    if (feedback.Text != "Tie!")
                    {
                        endGame("CPU is");
                    }
                    else
                    {
                        changeAllButtonEnables(false, -1);
                    }
                }
            }
            else
            {
                //game is over
                if (feedback.Text != "Tie!")
                {
                    endGame("You are");
                } 
                else
                {
                    changeAllButtonEnables(false, -1);
                }
            }
        }
        //changes the enablement of all buttons on board. ignores index passed
        private void changeAllButtonEnables(bool enable, int index)
        {
            for (int i = 0; i < smallGames.Length; i++)
            {
                if (i != index)
                {
                    smallGames[i].changeButtonEnable(enable);
                }
            }
        }
        //enables buttons of all incomplete games
        private void enableAllOpenGames()
        {
            for (int i = 0;i < smallGames.Length; i++)
            {
                if (smallGames[i].getWinner() == "")
                {
                    smallGames[i].changeButtonEnable(true);
                }
            }
           
        }
        /* Button Clicks */
        private void resetButton_Click(object sender, EventArgs e)
        {
            openMove = 0;
            feedback.Text = "";
            changeAllButtonEnables(true, -1);
            for (int i = 0; i < smallGames.Length; i++)
            {
                mainGame[i].Text = "";
                mainGame[i].SendToBack();
                smallGames[i].reset();
            }
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
        public String getText(int index) { return board[index].Text; }

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
            bool open = false;
            if (board[2].Text != "" && board[0].Text == board[1].Text && board[1].Text == board[2].Text)
            {
                winner = board[0].Text;
            }
            else if (board[5].Text != "" && board[3].Text == board[4].Text && board[4].Text == board[5].Text)
            {
                winner = board[3].Text;
            }
            else if (board[8].Text != "" && board[6].Text == board[7].Text && board[7].Text == board[8].Text)
            {
                winner = board[6].Text;
            }
            else if (board[6].Text != "" && board[0].Text == board[3].Text && board[3].Text == board[6].Text)
            {
                winner = board[6].Text;
            }
            else if (board[1].Text != "" && board[1].Text == board[4].Text && board[4].Text == board[7].Text)
            {
                winner = board[1].Text;
            }
            else if (board[2].Text != "" && board[2].Text == board[5].Text && board[5].Text == board[8].Text)
            {
                winner = board[8].Text;
            }
            else if (board[8].Text != "" && board[0].Text == board[4].Text && board[4].Text == board[8].Text)
            {
                winner = board[0].Text;
            }
            else if (board[6].Text != "" && board[2].Text == board[4].Text && board[4].Text == board[6].Text)
            {
                winner = board[2].Text;
            }
            else
            {
                //looks for a tie
                for (int i =0; i < board.Length; i++)
                {
                    if (board[i].Text == "")
                    {
                        open = true;
                        break;
                    }
                }
                if (!open)
                {
                    winner = "T";
                }
            }
        }
        //checks if there is a spot that could win
        //returns -1 if cannot, index if it can
        public int checkCloseWin(String user)
        {
            //adapted from Tic Tac Toe game
            String[] top = { board[0].Text, board[3].Text, board[6].Text };
            String[] middleR = { board[1].Text, board[4].Text, board[7].Text };
            String[] bottom = { board[2].Text, board[5].Text, board[8].Text };

            String[] left = { board[0].Text, board[1].Text, board[2].Text };
            String[] middleC = { board[3].Text, board[4].Text, board[5].Text };
            String[] right = { board[6].Text, board[7].Text, board[8].Text };

            String[] Ldiag = { board[0].Text, board[4].Text, board[8].Text };
            String[] Rdiag = { board[2].Text, board[4].Text, board[6].Text };

            int topX = 0;
            int middleRX = 0;
            int bottomX = 0;
            int leftX = 0;
            int rightX = 0;
            int middleCX = 0;
            int LdiagX = 0;
            int RdiagX = 0;
            //finds all the possible X values for each possible win chance
            for (int i = 0; i < top.Length; i++)
            {
                if (top[i] == user) topX++;
                if (middleR[i] == user) middleRX++;
                if (bottom[i] == user) bottomX++;
                if (left[i] == user) leftX++;
                if (middleC[i] == user) middleCX++;
                if (right[i] == user) rightX++;
                if (Ldiag[i] == user) LdiagX++;
                if (Rdiag[i] == user) RdiagX++;
            }
            //if there is a chance that they win, the int values are added to the queue
            if (topX > 1)
            {
                if (top[0] == "") return 0;
                if (top[1] == "") return 3;
                if (top[2] == "") return 6;
            }
            if (middleRX > 1)
            {
                if (middleR[0] == "") return 1;
                if (middleR[1] == "") return 4;
                if (middleR[2] == "") return 7;
            }
            if (bottomX > 1)
            {
                if (bottom[0] == "") return 2;
                if (bottom[1] == "") return 5;
                if (bottom[2] == "") return 8;
            }
            if (leftX > 1)
            {
                if (left[0] == "") return 0;
                if (left[1] == "") return 1;
                if (left[2] == "") return 2;
            }
            if (middleCX > 1)
            {
                if (middleC[0] == "") return 3;
                if (middleC[1] == "") return 4;
                if (middleC[2] == "") return 5;
            }
            if (rightX > 1)
            {
                if (right[0] == "") return 6;
                if (right[1] == "") return 7;
                if (right[2] == "") return 8;
            }
            if (LdiagX > 1)
            {
                if (Ldiag[0] == "") return 0;
                if (Ldiag[1] == "") return 4;
                if (Ldiag[2] == "") return 8;
            }
            if (RdiagX > 1)
            {
                if (Rdiag[0] == "") return 2;
                if (Rdiag[1] == "") return 4;
                if (Rdiag[2] == "") return 6;
            }
            return -1;
        }
        //changes the enablement of the buttons
        public void changeButtonEnable(bool enable)
        {
            for (int i =0; i < board.Length; i++)
            {
                board[i].Enabled = enable;
            }
        }
        //resets all button text, and winner value
        public void reset()
        {
            winner = "";
            for (int i = 0; i < board.Length; i++)
            {
                board[i].Text = "";
                
            }
        }
    }
    /*
     * Represents the AI playing against player
     */
    public class STTTAI
    {
        String letter;
        String otherLetter;
        public STTTAI(string letter)
        {
            this.letter = letter;
            if (letter == "O")
            {
                otherLetter = "X";
            } 
            else
            {
                otherLetter = "O";
            }
        }

        //makes move for AI
        #region movement functions
        //makes move for AI
        //ret: location of new board to be made available
        public int makeMove(TTTGame[] largeBoard, int currentIndex)
        {
            int ret;
            TTTGame currentGame = largeBoard[currentIndex];
            //checks to see if AI can make move to win
            ret = lookToWin(currentGame, letter);
            if (ret == -1)
            {
                //look to see if player can win
                ret = lookToWin(currentGame, "X");
                if (ret == -1)
                {
                    //try to make move that does not allow player to make easy win in their next move
                    ret = intelligentMove(largeBoard, currentGame, otherLetter);
                    if (ret == -1)
                    {
                        ret = randomMove(currentGame);
                    }
                }
            }
            return ret;
        }
        //checks to see if AI can make move to win small board
        private int lookToWin(TTTGame game, String usr)
        {
            int index = game.checkCloseWin(usr);
            if (index != -1)
            {
                //make move to win/block
                game.changeBoard(index, letter);
                
            }
            return index;
        }
        //attempts to make an intelligent move, one that does not set up other player for easy move
        private int intelligentMove(TTTGame[] largeBoard, TTTGame currentGame, String usr)
        {
            int[] attemptIndex = { 8, 6, 0, 2, 4, 1, 3, 5, 7 };

            for (int i =0; i < attemptIndex.Length; i++)
            {
                //makes move at index if it is available, and it would not give other player easy move
                if (currentGame.getText(attemptIndex[i]) == "")
                {
                    //checks to see if the other player has a close win at the game at this index
                    if (largeBoard[attemptIndex[i]].checkCloseWin(usr) == -1)
                    {
                        currentGame.changeBoard(attemptIndex[i], letter);
                        return attemptIndex[i];
                    }
                }
            }
            //no easy, intelligent move found
            return -1;
        }
        //makes a 'random' move
        private int randomMove(TTTGame currentGame)
        {
            //move must then be 'random'
            int ret = 0;
            int[] attemptIndex = { 8, 6, 0, 2, 4, 1, 3, 5, 7 };
            attemptIndex = shuffle(attemptIndex);
            for (int i = 0; i < attemptIndex.Length; i++)
            {
                if (currentGame.getText(attemptIndex[i]) == "")
                {
                    currentGame.changeBoard(attemptIndex[i], letter);
                    ret = attemptIndex[i];
                }
            }
            return ret;
        }
        //randomly shuffles array contents
        private int[] shuffle(int[] arr)
        {
            int n = arr.Length;
            int start, end, tmp;
            Random rnd = new Random();
            while (n > 0)
            {
                start = rnd.Next(0, arr.Length);
                end = rnd.Next(0, arr.Length);
                //swaps if the indexes are different
                if (start != end)
                {
                    tmp = arr[start];
                    arr[start] = arr[end];
                    arr[end] = tmp;
                    n--;
                }
            }
            return arr;
        }
        //makes a move, if the movement is open to all not finished boards
        //returns: 0: index of game played in, 1: index of next game
        public int[] makeOpenMove(TTTGame[] games)
        {
            int closeWin = -1;
            int[] ret = { -1, -1 };
            int[] attemptIndex = { 8, 6, 0, 2, 4, 1, 3, 5, 7 };
            //look to win for a given game
            for (int i = 0; i < games.Length; i++)
            {
                //only looks at games that were won
                if (games[i].getWinner() == "")
                {
                    closeWin = games[i].checkCloseWin(letter);
                    if (closeWin != -1)
                    {
                        games[i].changeBoard(closeWin, letter);
                        ret[0] = i;
                        ret[1] = closeWin;
                        return ret;
                    }
                }
            }
            //look to block for a given game
            for (int i = 0; i < games.Length; i++)
            {
                if (games[i].getWinner() == "")
                {
                    closeWin = games[i].checkCloseWin(otherLetter);
                    if (closeWin != -1)
                    {
                        games[i].changeBoard(closeWin, letter);
                        ret[0] = i;
                        ret[1] = closeWin;
                        return ret;
                    }
                }
            }
            //make random move otherwise
            //intelligent choice made first, if fails makes random move
            for (int i = 0; i < attemptIndex.Length; i++)
            {
                if (games[attemptIndex[i]].getWinner() == "")
                {
                    closeWin = intelligentMove(games, games[attemptIndex[i]], otherLetter);
                    if (closeWin == -1)
                    {
                        closeWin = randomMove(games[attemptIndex[i]]);
                        ret[0] = i;
                        ret[1] = closeWin;
                        return ret;
                    }
                    else
                    {
                        ret[0] = i;
                        ret[1] = closeWin;
                        return ret;
                    }
                }
            }
            //should not reach here
            return ret;
        }
        #endregion
    }
}
