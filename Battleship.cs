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
using System.Runtime.CompilerServices;
using System.Diagnostics.Tracing;
using System.Security.Policy;
using System.Xml.Linq;

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
            String[][] guessBoard = new string[9][];
            //creates and adds blank boards to both player and CPU player
            for (int i = 0; i < guessBoard.Length; i++)
            {
                for (int j = 0; j < guessBoard[i].Length; j++)
                {
                    guessBoard[i][j] = "_";
                }
            }
            player.setBoard(guessBoard);
            CPU.setBoard(guessBoard);
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
        //Places guess on board, tells player if it was a hit or not (Assumes valid coordinates)
        public void makeGuess(BattleshipPlayer user, int[] coords)
        {
            String[][] board = user.getBoard();
            String[][] guessBoard = user.getGuessBoard();
            Ship[,] ships = user.getShips();

            
            //Is a hit
            if (board[coords[0]-1][coords[1]-1] == "X")
            {
                guessBoard[coords[0] - 1][coords[1] - 1] = "X";
                Ship ship = ships[coords[0]-1,coords[1]-1];
                ship.hit();
                if (ship.isSunk())
                {
                    label11.Text = String.Format("You sunk a {0}!", ship.getName());
                }
                else
                {
                    label11.Text = "You hit a ship!";
                }
            }
            else
            {
                guessBoard[coords[0] - 1][coords[1] - 1] = "O";
                label11.Text = "You missed.";
            }

            //run computer guess
 
        }
        //makes sure guess coordinates are within the board
        public bool valGuessCoords(int[] coords)
        {
            if (coords[0] < 1 || coords[1] < 1 || coords[0] > 9 || coords[1] > 9)
            {
                return false;
            }
            else
            {
                return true;
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
                    if (shipType == "Aircraft Carrier (Size: 4)")
                    {
                        player.addShipToBoard("Aircraft Carrier", 4, startCoords, endCoords);
                        label6.Text = "Ship Placed!";
                        return true;
                    }
                    else if (shipType == "Battleship (Size: 3)")
                    {
                        player.addShipToBoard("Battleship", 3, startCoords, endCoords);
                        label6.Text = "Ship Placed!";
                        return true;
                    }
                    else if (shipType == "Destroyer (Size: 2)")
                    {
                        player.addShipToBoard("Destroyer", 2, startCoords, endCoords);
                        label6.Text = "Ship Placed!";
                        return true;
                    }
                    else
                    {
                        player.addShipToBoard("Submarine", 1, startCoords, endCoords);
                        label6.Text = "Ship Placed!";
                        return true;
                    }
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
        //button that runs the make guess sequence
        private void button2_Click(object sender, EventArgs e)
        {
            String expr = @"[0-9]";
            MatchCollection mc = Regex.Matches(textBox2.Text, expr);
            int[] coords = new int[2];
            int i = 0;
            int counter = 0;
            //gets coordinates from UI
            foreach (Match m in mc)
            {
                GroupCollection group = m.Groups;
                if (counter < 2)
                {
                    coords[i] = Convert.ToInt32(group[0].Value);
                }
                i++;
                counter++;
            }
            if (counter == 2)
            {
                //player is making guess, send CPU
                if (valGuessCoords(coords))
                {
                    makeGuess(CPU, coords);
                }
                else
                {
                    label11.Text = "Please enter valid coordinates";
                }
            } 
            else
            {
                label11.Text = "Please enter valid coordinates";
            }
        }
    }
    /* BattleshipPlayer class. Represents the players, holds board data */
    public class BattleshipPlayer
    {
        private String[][] board = new String[9][];
        private String[][] guessBoard = new String[9][];
        Ship[,] ships = new Ship[9, 9];
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
            guessBoard[0] = new String[9];
            guessBoard[1] = new String[9];
            guessBoard[2] = new String[9];
            guessBoard[3] = new String[9];
            guessBoard[4] = new String[9];
            guessBoard[5] = new String[9];
            guessBoard[6] = new String[9];
            guessBoard[7] = new String[9];
            guessBoard[8] = new String[9];
            Ship blankShip = new Ship(0, "None");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i][j] = "_";
                    guessBoard[i][j] = "_";
                    ships[i, j] = blankShip;
                }
            }
        }
        //Getters and setters
        public String[][] getBoard()
        {
            return board;
        }
        public void setBoard(String[][] board)
        {
            this.board = board;
        }
        public String[][] getGuessBoard()
        {
            return guessBoard;
        }
        public void setGuessBoard(String[][] guessBoard)
        {
            this.guessBoard = guessBoard;
        }
        public Ship[,] getShips() { return ships; }
        public void setShips(Ship[,] ships) {  this.ships = ships; }
        /* AI places ships onto board */
        public void placeShips()
        {
            //Get to place: 1 Aircraft Carrier, 2 Battleships, 2 Destroyers, 1 Submarine
            int numShips = 0;
            Random rnd = new Random();
            String name;
            while (numShips < 6)
            {
                //Aircraft carrier
                if (numShips == 0)
                {
                    int type = rnd.Next(0, 3);
                    int x, y;
                    name = "Aircraft Carrier";
                    //horizontal
                    if (type == 0) {
                        x = rnd.Next(1, 7);
                        y = rnd.Next(1, 10);
                        int[] start = { x, y };
                        int[] end = { x + 3, y };
                        if (checkForPlacedShip(start, end))
                        {
                            addShipToBoard(name, 4, start, end);
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
                            addShipToBoard(name, 4, start, end);
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
                            addShipToBoard(name, 4, start, end);
                            numShips++;
                        }
                    }

                }
                //Battleship
                else if (numShips < 3)
                {
                    int type = rnd.Next(0, 3);
                    int x, y;
                    name = "Battleship";
                    //horizontal
                    if (type == 0)
                    {
                        x = rnd.Next(1, 8);
                        y = rnd.Next(1, 10);
                        int[] start = { x, y };
                        int[] end = { x + 2, y };
                        if (checkForPlacedShip(start, end))
                        {
                            addShipToBoard(name, 3, start, end);
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
                            addShipToBoard(name, 3, start, end);
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
                            addShipToBoard(name, 3, start, end);
                            numShips++;
                        }
                    }
                }
                //Destroyer
                else if (numShips < 5)
                {
                    int type = rnd.Next(0, 3);
                    int x, y;
                    name = "Destroyer";
                    //horizontal
                    if (type == 0)
                    {
                        x = rnd.Next(1, 9);
                        y = rnd.Next(1, 10);
                        int[] start = { x, y };
                        int[] end = {x + 1, y };
                        if (checkForPlacedShip(start, end))
                        {
                            addShipToBoard(name, 2, start, end);
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
                            addShipToBoard(name, 2, start, end);
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
                            addShipToBoard(name, 2, start, end);
                            numShips++;
                        }
                    }
                }
                //Submarine
                else
                {
                    name = "Submarine";
                    int x = rnd.Next(1, 10);
                    int y = rnd.Next(1, 10);
                    int[] start = { x, y };
                    int[] end = { x, y };
                    if (checkForPlacedShip(start, end))
                    {
                        addShipToBoard(name, 1, start, end);
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
        public void addShipToBoard(String name, int size, int[] startCoord, int[] endCoord)
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
            Ship newShip = new Ship(size, name);
            //Placement Loops
            //if the placement is vertical
            if (xDif == 0)
            {
                for (int i = y - 1; i < y + yDif; i++)
                {
                    this.board[i][y - 1] = "X";
                    this.ships[i,y - 1] = newShip;
                }
            }
            //if placement is horizontal
            else if (yDif == 0)
            {
                for (int i = x - 1; i < x + xDif; i++)
                {
                    this.board[x - 1][i] = "X";
                    this.ships[x-1, i] = newShip;
                }
            }
            //if placement is diagonal
            else
            {
                int j = y - 1;
                for (int i = x - 1; i < x + xDif; i++)
                {
                    this.board[i][j] = "X";
                    this.ships[i, j] = newShip;
                    j++;
                }
            }
        }
    }
    //Class that represents the individual ship
    public class Ship
    {
        private int size;
        private int[] health;
        private String name;
        public Ship(int size, String name)
        {
            this.size = size;
            health = new int[size];
            for (int i = 0; i < size; i++)
            {
                health[i] = 0;
            }

            this.name = name;
        }

        //Getters and setters
        public int getSize() { return size; }
        public void setSize(int size) {  this.size = size; }
        public String getName() { return name; }
        //checks if the ship is sunk. If each value of the ship is 1, then it is sunk
        public bool isSunk()
        {
            for (int i = 0; i < size; i++)
            {
                if (health[i] == 0)
                {
                    return false;
                }
            }
            return true;
        }
        //'hits' the ship
        public void hit()
        {
            int counter = 0;
            while (counter < size)
            {
                if (health[counter] == 0)
                {
                    health[counter] = 1;
                    break;
                }
                counter++;
            }
        }
    }
}
