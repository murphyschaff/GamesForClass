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
using System.Text.RegularExpressions;
using static System.Windows.Forms.AxHost;

namespace GamesForClass
{
    public partial class Battleship : Form
    {
        BattleshipPlayer player = new BattleshipPlayer();
        BattleshipPlayer CPU = new BattleshipPlayer();
        public Battleship()
        {
            InitializeComponent();
        }
        public void initBattleship()
        {
            //creates and adds blank boards to both player and CPU player
            String[][] blankBoard = new String[9][];
            for (int i = 0; i < blankBoard.Length; i++)
            {
                for (int j = 0; j < blankBoard[i].Length; j++)
                {
                    blankBoard[i][j] = "_";
                }
            }
            player.setBoard(blankBoard);
            CPU.setBoard(blankBoard);
        }
        //Updates graphics on board
        public void updateBoard(bool isplayer)
        {
            if (isplayer) {
                if (placePlayerShip())
                {
                    //places down the new board results into label (NEEDS MORE SPACES AND NEW LINES)
                    String[][] board = player.getBoard();
                    String output ="";
                    for (int i = 0;i < board.Length; i++)
                    {
                        output += board[i][0] + "__" + board[i][1] + "__" + board[i][2] + "__" + board[i][3] + "__" + board[i][4] + "__" + board[i][5] + "__" + board[i][6] + "__" + board[i][7] + "__" + board[i][8] + "\n";
                    }
                    label2.Text = output;
                }
            }
            else
            {
                CPU.placeShips();
                String[][] board = CPU.getBoard();
                String output = "";
                for (int i = 0; i < board.Length; i++)
                {
                    output += board[i][0] + "__" + board[i][1] + "__" + board[i][2] + "__" + board[i][3] + "__" + board[i][4] + "__" + board[i][5] + "__" + board[i][6] + "__" + board[i][7] + "__" + board[i][8] + "\n";
                }
                label1.Text = output;
            }
        }
        /* places player ship on board */
        public bool placePlayerShip()
        {
            String shipType = domainUpDown1.Text;
            String expr = @"[0-9]";
            MatchCollection mc = Regex.Matches(textBox1.Text, expr);
            int[] startCoords = new int[2];
            int[] endCoords = new int[2];
            int i = 0;
            int counter = 0;
            //gets coordinates from UI
            foreach (Match m in mc)
            {
                GroupCollection group = m.Groups;
                if (i < 2)
                {
                    startCoords[i] = Convert.ToInt32(group[0].Value);

                }
                else
                {
                    endCoords[i-2] = Convert.ToInt32(group[0].Value);
                }
                i++;
                counter++;
            }

            //makes sure correct number of coordinates was entered
            if (counter == 4)
            {
                //checks validity of coordinates based on ship type
                if (validatePlaceCoords(shipType, startCoords, endCoords))
                {
                    player.addShipToBoard(startCoords, endCoords);
                    label6.Text = "Ship Placed!";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                label6.Text = "Please enter correct number of coordinates (4 total numbers)";
                return false;
            }
        }

        //makes sure placement of ship coordinates are within boundaries, and if the placement is diagonal that the slope is 1
        public bool validatePlaceCoords(String shipType, int[] startCoord, int[] endCoord)
        {
            bool output = true;
            int startX = startCoord[0];
            int endX = endCoord[0];
            int startY = startCoord[1];
            int endY = endCoord[1];

            
            //not outside of board
            if (startX > 9 || endX > 9 || startY > 9 || endY > 9)
            {
                output = false;
                label6.Text = "Ship outside of boundaries";
            }
            else if (startX < 1 || endX < 1 || startY < 1 || endY < 1)
            {
                output = false;
                label6.Text = "Ship outside of boundaries";
            }
            //if the slope is not flat or vertical
            else if (Math.Abs(startX - endX) != 0 && Math.Abs(startY - endY) != 0)
            {
                if (Math.Abs(startX - endX) / Math.Abs(startY - endY) != 1)
                {
                    output = false; 
                    label6.Text = "Slope is too steep to place ship";
                }
            }
            else if (shipType == "Select Ship")
            {
                output = false;
                label6.Text = "Please select a ship";
            }
            else
            {
                //makes sure length matches type of ship requested
                //calculates length
                int length;
                if (Math.Abs(startX - endX) == 0)
                {
                    length = Math.Abs(startY - endY) + 1;
                }
                else 
                {
                    length = Math.Abs(startX - endX) + 1;
                }

                //does checks
                if (shipType == "Aircraft Carrier (Size: 4)" && length != 4)
                {
                    output = false;
                    label6.Text = "Wrong length for selected ship";
                }
                else if (shipType == "Battleship (Size: 3)" && length != 3)
                {
                    output = false;
                    label6.Text = "Wrong length for selected ship";
                }
                else if (shipType == "Destroyer (Size: 2)" && length != 2)
                {
                    output = false;
                    label6.Text = "Wrong length for selected ship";
                }
                else if (shipType == "Submarine (Size: 1)" && length != 1)
                {
                    output = false;
                    label6.Text = "Wrong length for selected ship";
                }
            }

            return output;
        }
        /* BUTTON FUNCTIONS */
        // Place ship function. Allows the player to place a ship
        private void button1_Click(object sender, EventArgs e)
        {
            updateBoard(true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            updateBoard(false);
        }
    }
    /* BattleshipPlayer class. Represents the players, holds board data */
    public class BattleshipPlayer
    {
        private String[][] board = new String[9][];
        public BattleshipPlayer()
        {
            board[0] = new String[9];
            board[1] = new String[9];
            board[2] = new String[9];
            board[3] = new String[9];
            board[4] = new String[9];
            board[5] = new String[9];
            board[6] = new String[9];
            board[7] = new String[9];
            board[8] = new String[9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i][j] = "_";
                }
            }
        }

        public String[][] getBoard()
        {
            return board;
        }
        public void setBoard(String[][] board)
        {
            this.board = board;
        }
        /* AI places ships onto board */
        public void placeShips()
        {
            //Get to place: 1 Aircraft Carrier, 2 Battleships, 2 Destroyers, 1 Submarine
            int numShips = 0;
            Random rnd = new Random();
            while (numShips < 6)
            {
                //Aircraft carrier
                if (numShips == 0)
                {
                    int type = rnd.Next(0, 3);
                    int x, y;
                    //horizontal
                    if (type == 0) {
                        x = rnd.Next(1, 7);
                        y = rnd.Next(1, 10);
                        int[] start = { x, y };
                        int[] end = { x + 3, y };
                        if (checkForPlacedShip(start, end))
                        {
                            addShipToBoard(start, end);
                            numShips++;
                        }
                    }
                    //vertical
                    else if (type == 1)
                    {
                        x = rnd.Next(1, 10);
                        y = rnd.Next(1, 7);
                        int[] start = { x, y };
                        int[] end = { x, y + 3};
                        if (checkForPlacedShip(start, end))
                        {
                            addShipToBoard(start, end);
                            numShips++;
                        }
                    }
                    //diagonal
                    else
                    {
                        x = rnd.Next(1, 7);
                        y = rnd.Next(1,7);
                        int[] start = { x, y };
                        int[] end = { x + 3, y + 3 };
                        if (checkForPlacedShip(start,end))
                        {
                            addShipToBoard(start, end);
                            numShips++;
                        }
                    }

                }
                //Battleship
                else if (numShips < 3)
                {
                    int type = rnd.Next(0, 3);
                    int x, y;
                    //horizontal
                    if (type == 0)
                    {
                        x = rnd.Next(1, 8);
                        y = rnd.Next(1, 10);
                        int[] start = { x, y };
                        int[] end = { x + 2, y };
                        if (checkForPlacedShip(start, end))
                        {
                            addShipToBoard(start, end);
                            numShips++;
                        }
                    }
                    //vertical
                    else if (type == 1) 
                    {
                        x = rnd.Next(1, 10);
                        y = rnd.Next(1, 8);
                        int[] start = { x, y };
                        int[] end = { x, y + 2};
                        if (checkForPlacedShip(start, end))
                        {
                            addShipToBoard(start, end);
                            numShips++;
                        }
                    }
                    //diagonal
                    else
                    {
                        x = rnd.Next(1, 8);
                        y = rnd.Next(1, 8);
                        int[] start = { x, y };
                        int[] end = { x + 2, y + 2 };
                        if (checkForPlacedShip(start, end))
                        {
                            addShipToBoard(start, end);
                            numShips++;
                        }
                    }
                }
                //Destroyer
                else if (numShips < 5)
                {
                    int type = rnd.Next(0, 3);
                    int x, y;
                    //horizontal
                    if (type == 0)
                    {
                        x = rnd.Next(1, 9);
                        y = rnd.Next(1, 10);
                        int[] start = { x, y };
                        int[] end = {x + 1, y };
                        if (checkForPlacedShip(start, end))
                        {
                            addShipToBoard(start, end);
                            numShips++;
                        }
                    }
                    //vertical
                    else if (type == 1)
                    {
                        x = rnd.Next(1, 10);
                        y = rnd.Next(1, 9);
                        int[] start = { x, y };
                        int[] end = {x, y + 1 };
                        if (checkForPlacedShip(start, end))
                        {
                            addShipToBoard(start, end);
                            numShips++;
                        }
                    }
                    //diagonal
                    else
                    {
                        x = rnd.Next(1, 9);
                        y = rnd.Next(1, 9);
                        int[] start = { x, y };
                        int[] end = { x + 1, y + 1 };
                        if (checkForPlacedShip(start, end))
                        {
                            addShipToBoard(start, end);
                            numShips++;
                        }
                    }
                }
                //Submarine
                else
                {
                    int x = rnd.Next(1, 10);
                    int y = rnd.Next(1, 10);
                    int[] start = { x, y };
                    int[] end = { x, y };
                    if (checkForPlacedShip(start, end))
                    {
                        addShipToBoard(start, end);
                        numShips++;
                    }
                }
                    
            }
        }
        //Makes sure there was not a ship placed in this spot already
        public bool checkForPlacedShip(int[] startCoord, int[] endCoord)
        {
            int startX = startCoord[0];
            int endX = endCoord[0];
            int startY = startCoord[1];
            int endY = endCoord[1];
            int x, y, xDif, yDif;

            if (startX > endX)
            {
                x = endX;
                xDif = startX - endX;
            }
            else
            {
                x = startX;
                xDif = endX - startX;
            }
            if (startY > endY)
            {
                y = endY;
                yDif = startY - endY;
            }
            else
            {
                y = startY;
                yDif = endY - startY;
            }
            //Placement Loops
            //if the placement is vertical
            if (xDif == 0)
            {
                for (int i = y - 1; i < y + yDif; i++)
                {
                    if (this.board[i][y - 1] == "X") 
                    {
                        return false;
                    }
                }
            }
            //if placement is horizontal
            else if (yDif == 0)
            {
                for (int i = x - 1; i < x + xDif; i++)
                {
                    if (this.board[x - 1][i] == "X")
                    {
                        return false;
                    }
                }
            }
            //if placement is diagonal
            else
            {
                int j = y - 1;
                for (int i = x - 1; i < x + xDif; i++)
                {
                    if (this.board[i][j] == "X")
                    {
                        return false;
                    }
                    j++;
                }
            }
            return true;
        }
        //places the ships onto the board
        public void addShipToBoard(int[] startCoord, int[] endCoord)
        {
            //places already validated ship into board space
            int startX = startCoord[0];
            int endX = endCoord[0];
            int startY = startCoord[1];
            int endY = endCoord[1];
            int x, y, xDif, yDif;

            if (startX > endX)
            {
                x = endX;
                xDif = startX - endX;
            }
            else
            {
                x = startX;
                xDif = endX - startX;
            }
            if (startY > endY)
            {
                y = endY;
                yDif = startY - endY;
            }
            else
            {
                y = startY;
                yDif = endY - startY;
            }

            //Placement Loops
            //if the placement is vertical
            if (xDif == 0)
            {
                for (int i = y - 1; i < y + yDif; i++)
                {
                    this.board[i][y - 1] = "X";
                }
            }
            //if placement is horizontal
            else if (yDif == 0)
            {
                for (int i = x - 1; i < x + xDif; i++)
                {
                    this.board[x - 1][i] = "X";
                }
            }
            //if placement is diagonal
            else
            {
                int j = y - 1;
                for (int i = x - 1; i < x + xDif; i++)
                {
                    this.board[i][j] = "X";
                    j++;
                }
            }
        }
    }
}
