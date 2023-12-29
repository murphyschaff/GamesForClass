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
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Contracts;
using System.Diagnostics;

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
            player = new BattleshipPlayer();
            CPU = new BattleshipPlayer();

        }
        //Updates graphics on board, depends on if board is being updated when player placing ships
        public void updateBoard(bool isplayer)
        {
            if (isplayer) {
                if (placePlayerShip())
                {
                    //places down the new board results into label (NEEDS MORE SPACES AND NEW LINES)
                    String[,] board = player.getBoard();
                    String output = "";
                    for (int i = 0;i < 9; i++)
                    {
                        output += board[i, 0] + "__" + board[i, 1] + "__" + board[i, 2] + "__" + board[i, 3] + "__" + board[i, 4] + "__" + board[i, 5] + "__" + board[i, 6] + "__" + board[i, 7] + "__" + board[i, 8] + "\n";
                    }
                    label2.Text = output;
                }
            }
            else
            {
                //Player board
                String[,] board = player.getBoard();
                String output = "";
                for (int i = 0; i < 9; i++)
                {
                    output += board[i, 0] + "__" + board[i,1] + "__" + board[i, 2] + "__" + board[i, 3] + "__" + board[i, 4] + "__" + board[i, 5] + "__" + board[i, 6] + "__" + board[i, 7] + "__" + board[i, 8] + "\n";
                }
                label2.Text = output;
                
                //CPU board
                board = CPU.getBoard();
                output = "";
                for (int i = 0; i < 9; i++)
                {
                    output += board[i, 0] + "__" + board[i, 1] + "__" + board[i, 2] + "__" + board[i, 3] + "__" + board[i, 4] + "__" + board[i, 5] + "__" + board[i, 6] + "__" + board[i, 7] + "__" + board[i, 8] + "\n";
                }
                label1.Text = output;
            }
        }
        //Places guess on board, tells player if it was a hit or not (Assumes valid coordinates)
        public void makeGuess(BattleshipPlayer user, int[] coords)
        {
            String[,] board = user.getBoard();
            Ship[,] ships = user.getFleet();
            coords[0] = coords[0] - 1;
            coords[1] = coords[1] - 1;

            //checks if the space has been guessed already
            if (board[coords[0], coords[1]] == "O" || board[coords[0], coords[1]] == "H")
            {
                label11.Text = "You have already selected this space";
                return;
            }
            //Is a hit
            else if (board[coords[0],coords[1]] != "_")
            {
                board[coords[0],coords[1]] = "H";
                Ship ship = ships[coords[0],coords[1]];
                ship.hit();
                if (ship.isSunk())
                {
                    label11.Text = String.Format("You sunk a {0}!", ship.getName());
                    CPU.setSunkShips(CPU.getSunkShips() + 1);
                }
                else
                {
                    label11.Text = "You hit a ship!";
                }
            }
            else
            {
                board[coords[0], coords[1]] = "O";
                label11.Text = "You missed.";
            }

            //AI makes guess
            if (CPU.guess(player))
            {
                label11.Text = label11.Text + "\nCPU hit a ship!";
            }
            else
            {
                label11.Text = label11.Text + "\nCPU missed.";
            }

            updateBoard(false);
            //check for win condition
            int playersunk = player.getSunkShips();
            int cpusunk = CPU.getSunkShips();
            if (playersunk > 5)
            {
                label11.Text = label11.Text + "\nThe CPU has sunk all your ships. You loose";
                button2.Visible = false;
                textBox2.Visible = false;
                label5.Visible = false;
            }
            else if (cpusunk > 5)
            {
                label11.Text = label11.Text + "\nYou have sunk all CPU ships. You Win!!!!";
                button2.Visible = false;
                textBox2.Visible = false;
                label5.Visible = false;
            }

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
            int type;
            //finding type
            if (startCoords[0] - endCoords[0] == 0) { type = 1; }
            else if (startCoords[1] - endCoords[1] == 0) { type = 2; }
            else { type = 3; }

            //makes sure correct number of coordinates was entered
            if (counter == 4)
            {
                //checks validity of coordinates based on ship type
                if (validatePlaceCoords(shipType, startCoords, endCoords))
                {
                    if (shipType == "Aircraft Carrier (Size: 4)")
                    {
                        player.addShip(startCoords, endCoords, type, new Ship(4, "Aircraft Carrier"), "A", true);
                        label6.Text = "Ship Placed!";
                        return true;
                    }
                    else if (shipType == "Battleship (Size: 3)")
                    {
                        player.addShip(startCoords, endCoords, type, new Ship(3, "Battleship"), "B", true);
                        label6.Text = "Ship Placed!";
                        return true;
                    }
                    else if (shipType == "Destroyer (Size: 2)")
                    {
                        player.addShip(startCoords, endCoords, type, new Ship(2, "Destroyer"), "D", true);
                        label6.Text = "Ship Placed!";
                        return true;
                    }
                    else
                    {
                        player.addShip(startCoords, endCoords, type, new Ship(1, "Submarine"), "S", true);
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
            int type = 0;
            if (startX - endX == 0)
            {
                type = 1;
            }
            else if (startY - endY == 0)
            {
                type = 2;
            }
            else
            {
                type = 3;
            }
            
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
                String shipName = "";
                String letter = "";
                //does checks
                if (shipType == "Aircraft Carrier (Size: 4)")
                {
                    if (length != 4)
                    {
                    output = false;
                    label6.Text = "Wrong length for selected ship";
                    }
                    shipName = "Aircraft Carrier";
                    letter = "A";
                }
                else if (shipType == "Battleship (Size: 3)")
                {
                    if (length != 3)
                    {
                    output = false;
                    label6.Text = "Wrong length for selected ship";
                    }
                    shipName = "Battleship";
                    letter = "B";
                }
                else if (shipType == "Destroyer (Size: 2)")
                {
                    if (length != 2)
                    {
                    output = false;
                    label6.Text = "Wrong length for selected ship";
                    }
                    shipName = "Destroyer";
                    letter = "D";
                }
                else if (shipType == "Submarine (Size: 1)" && length != 1)
                {
                    if (length != 1)
                    {
                    output = false;
                    label6.Text = "Wrong length for selected ship";
                    }
                    shipName = "Submarine";
                    letter = "S";
                }
                Ship ship = new Ship(length, shipName);
                //makes sure a ship is not already there
                if (!player.addShip(startCoord, endCoord, type, ship, letter, true))
                {
                    output = false;
                    label6.Text = "A ship already exists in this space.";
                }
            }

            return output;
        }
        /* BUTTON FUNCTIONS */
        // Place ship function. Allows the player to place a ship
        private void button1_Click(object sender, EventArgs e)
        {
            //need to stop player from overwriting ships
            button5.Visible = false;
            int numShips = player.getNumShips();
            if (numShips > 5)
            {
                button3.Visible = true;
                button1.Visible = false;
            }
            updateBoard(true);
        }
        //Start game function
        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            //for testing purposes, user can see CPU placed ships
            CPU.placeShips(true);
            button2.Visible = true;
            textBox2.Visible = true;
            label5.Visible = true;
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
        //auto place ships
        private void button5_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button3.Visible = true;
            button5.Visible = false;
            label4.Visible = false;
            domainUpDown1.Visible = false;
            label6.Visible = false;
            textBox1.Visible = false;
            player.placeShips(true);
            updateBoard(false);
        }
        //reset button sequence
        private void button4_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button5.Visible = true;
            label4.Visible = true;
            domainUpDown1.Visible = true;
            label6.Visible = true;
            textBox1.Visible = true;

            button2.Visible = false;
            textBox2.Visible = false;
            label5.Visible = false;

            label1.Text = "";
            label2.Text = "";
            label11.Text = "";

            initBattleship();
        }
    }
    /* BattleshipPlayer class. Represents the players, holds board data */
    public class BattleshipPlayer
    {
        private String[,] board = new String[9,9];
        Ship[,] fleet = new Ship[9, 9];
        //keeps track of previous guess, 0: last guess was mark, 1&2: X,y coords of previous guess, 3: direction type, 4&5: first found local, 6: ship direction
        private int[] guesses = { -1, -1, -1, -1, -1, -1, -1 };
        private int numShips = 0;
        private int sunkShips = 0;
        public BattleshipPlayer()
        {
            Ship blankShip = new Ship(0, "None");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i,j] = "_";
                    fleet[i, j] = blankShip;
                }
            }
        }
        //Getters and setters
        public String[,] getBoard() { return board; }
        public void setBoard(String[,] board) { this.board = board; }
        public Ship[,] getFleet() { return fleet; }
        public void setFleet(Ship[,] ships) {  this.fleet = ships; }
        public int getNumShips() { return numShips; }
        public void setNumShips(int numShips) { this.numShips = numShips; }
        public int getSunkShips() { return sunkShips; }
        public void setSunkShips(int sunkShips) { this.sunkShips = sunkShips; }
        /* AI places ships onto board */
        public void placeShips(bool visual)
        {
            int x, y, type;
            int count = 0;
            Random rnd = new Random();
            int[] start = new int[2];
            int[] end = new int[2];
            //places ships onto board
            while (count < 6)
            {
                //aircraft carrier
                if (count == 0)
                {
                    //gets placement type, 1-3 (vertical, horizontal, diagonal)
                    type = rnd.Next(1, 3);
                    switch (type)
                    {
                        //x remains the same
                        case 1:
                            x = rnd.Next(0, 8);
                            y = rnd.Next(0, 5);
                            start[0] = x;
                            start[1] = y;
                            end[0] = x;
                            end[1] = y + 3;
                            break;
                        //y remains the same
                        case 2:
                            x = rnd.Next(0, 5);
                            y = rnd.Next(0, 8);
                            start[0] = x;
                            start[1] = y;
                            end[0] = x + 3;
                            end[1] = y;
                            break;
                        //diagonal
                        case 3:
                            x = rnd.Next(0, 5);
                            y = rnd.Next(0, 5);
                            start[0] = x;
                            start[1] = y;
                            end[0] = x + 3;
                            end[1] = y + 3;
                            break;
                    }
                    Ship ac = new Ship(4, "Aircraft Carrier");
                    //checks if the ship can be placed, places it if it can
                    if (addShip(start, end, type, ac, "A", visual))
                    {
                        count++;
                    }
                }
                //Battleship
                else if (count < 3)
                {
                    //gets placement type, 1-3 (vertical, horizontal, diagonal)
                    type = rnd.Next(1, 3);
                    switch (type)
                    {
                        //x remains the same
                        case 1:
                            x = rnd.Next(0, 8);
                            y = rnd.Next(0, 6);
                            start[0] = x;
                            start[1] = y;
                            end[0] = x;
                            end[1] = y + 2;
                            break;
                        //y remains the same
                        case 2:
                            x = rnd.Next(0, 6);
                            y = rnd.Next(0, 8);
                            start[0] = x;
                            start[1] = y;
                            end[0] = x + 2;
                            end[1] = y;
                            break;
                        //diagonal
                        case 3:
                            x = rnd.Next(0, 6);
                            y = rnd.Next(0, 6);
                            start[0] = x;
                            start[1] = y;
                            end[0] = x + 2;
                            end[1] = y + 2;
                            break;
                    }
                    Ship ac = new Ship(3, "Battleship");
                    //checks if the ship can be placed, places it if it can
                    if (addShip(start, end, type, ac, "B", visual))
                    {
                        count++;
                    }
                }
                //Destroyer
                else if (count < 5)
                {
                    //gets placement type, 1-3 (vertical, horizontal, diagonal)
                    type = rnd.Next(1, 3);
                    switch (type)
                    {
                        //x remains the same
                        case 1:
                            x = rnd.Next(0, 8);
                            y = rnd.Next(0, 7);
                            start[0] = x;
                            start[1] = y;
                            end[0] = x;
                            end[1] = y + 1;
                            break;
                        //y remains the same
                        case 2:
                            x = rnd.Next(0, 7);
                            y = rnd.Next(0, 8);
                            start[0] = x;
                            start[1] = y;
                            end[0] = x + 1;
                            end[1] = y;
                            break;
                        //diagonal
                        case 3:
                            x = rnd.Next(0, 7);
                            y = rnd.Next(0, 7);
                            start[0] = x;
                            start[1] = y;
                            end[0] = x + 1;
                            end[1] = y + 1;
                            break;
                    }
                    Ship ac = new Ship(2, "Destroyer");
                    //checks if the ship can be placed, places it if it can
                    if (addShip(start, end, type, ac, "D", visual))
                    {
                        count++;
                    }
                }
                //Submarine
                else
                {
                    x = rnd.Next(0, 8);
                    y = rnd.Next(0, 8);
                    start[0] = x;
                    end[0] = x;
                    start[1] = y;
                    end[1] = y;
                    Ship ac = new Ship(1, "Submarine");
                    if (addShip(start, end, 1, ac, "S", visual))
                    {
                        count++;
                    }
                }
            }
        }
        //checks current ship placement, places ship if it is able to (false, unable to place)
        //visual: if the ship is actually placed onto the visual board
        public bool addShip(int[] start, int[] end, int type, Ship ship, String letter, bool visual)
        {
            int x, y;
            switch (type)
            {
                //no change in x
                case 1:
                    x = start[0];
                    //check for already placed ship
                    for (int i = start[1]; i <= end[1]; i++)
                    {
                        if (fleet[x, i].getName() != "None")
                        {
                            return false;
                        }
                    }
                    //place ship if there was not one there already
                    for (int i = start[1]; i <= end[1]; i++)
                    {
                        if (visual) { board[x, i] = letter; }
                        fleet[x, i] = ship;

                    }
                    return true;
                //no change in y
                case 2:
                    y = start[1];
                    for (int i = start[0]; i <= end[0]; i++)
                    {
                        if (fleet[i, y].getName() != "None")
                        {
                            return false;
                        }
                    }
                    for (int i = start[0]; i <= end[0]; i++)
                    {
                        if (visual) { board[i, y] = letter; }
                        fleet[i, y] = ship;
                    }
                    return true;
                //diagonal
                case 3:
                    int j = start[1];
                    for (int i = start[0]; i <= end[0]; i++)
                    {
                        if (fleet[i, j].getName() != "None")
                        {
                            return false;
                        }
                        j++;
                    }
                    j = start[1];
                    for (int i = start[0]; i <= end[0]; i++)
                    {
                        if (visual) { board[i, j] = letter; }
                        fleet[i, j] = ship;
                        j++;
                    }
                    return true;
            }
            return false;
        }
        
        //Algorithm to make a guess on the board
        public bool guess(BattleshipPlayer user)
        {
            String[,] opboard = user.getBoard();
            Ship[,] opfleet = user.getFleet();
            int sunkShips = user.getSunkShips();
            int x, y;
            bool correct = false;
            //previous guess was correct
            if (guesses[0] == 1)
            {
                //only made one correct guess so far
                if (guesses[6] == 0)
                {
                    Random rnd = new Random();
                    bool run = true;
                    x = 0;
                    y = 0;
                    while (run)
                    {
                        //gets random direction
                        int dir = rnd.Next(1, 8);
                        switch (dir)
                        {
                            case 1:
                                x = guesses[1] - 1;
                                y = guesses[2] - 1;
                                break;
                            case 2:
                                x = guesses[1] - 1;
                                y = guesses[2];
                                break;
                            case 3:
                                x = guesses[1] - 1;
                                y = guesses[2] + 1;
                                break;
                            case 4:
                                x = guesses[1];
                                y = guesses[2] - 1;
                                break;
                            case 5:
                                x = guesses[1];
                                y = guesses[2] + 1;
                                break;
                            case 6:
                                x = guesses[1] + 1;
                                y = guesses[2] - 1;
                                break;
                            case 7:
                                x = guesses[1] + 1;
                                y = guesses[2];
                                break;
                            case 8:
                                x = guesses[1] + 1;
                                y = guesses[2] + 1;
                                break;
                        }
                        int[] status = checkGuessBounds(dir, x, y, opboard, false);
                        //within bounds
                        if (status[0] == 1)
                        {
                            if (hitOrMiss(user, status[1], status[2]))
                            {
                                guesses[0] = 1;
                                correct = true;
                                //sets correct direction
                                guesses[3] = dir;
                                //sets og direction
                                guesses[6] = dir;
                                //checking if the ship was sunk
                                if (opfleet[status[1], status[2]].isSunk())
                                {
                                    for (int i = 0; i < guesses.Length; i++)
                                    {
                                        guesses[i] = -1;
                                    }
                                    sunkShips++;
                                }
                            }
                            else
                            {
                                guesses[0] = 0;
                            }
                            run = false;
                        }
                    }
                }
                else
                {
                    int[] status = checkGuessBounds(guesses[3], guesses[1], guesses[2], opboard, true);
                    //guess is in bounds
                    if (status[0] == 1)
                    {
                        if (hitOrMiss(user, status[1], status[2]))
                        {
                            guesses[0] = 1;
                            correct = true;
                            //checking if the ship was sunk
                            if (opfleet[status[1], status[2]].isSunk())
                            {
                                for (int i = 0; i < guesses.Length; i++)
                                {
                                    guesses[i] = -1;
                                }
                                sunkShips++;
                            }
                        }
                        else
                        {
                            guesses[0] = 0;
                        }
                    }
                    //guess is not in bounds
                    else
                    {
                        //flips direction and uses og guess
                        status = checkGuessBounds(flipDirection(guesses[6]), guesses[4], guesses[5], opboard, true);
                        if (status[0] == 1)
                        { 
                            if (hitOrMiss(user, status[1], status[2]))
                            {
                                guesses[0] = 1;
                                correct = true;
                                //checking if the ship was sunk
                                if (opfleet[status[1], status[2]].isSunk())
                                {
                                    for (int i = 0; i < guesses.Length; i++)
                                    {
                                        guesses[i] = -1;
                                    }
                                    sunkShips++;
                                }
                            }
                            else
                            {
                                guesses[0] = 0;
                            }
                        }
                        //both directions are invalid, find new location
                        else
                        {
                            for (int i = 0; i < guesses.Length; i++)
                            {
                                guesses[i] = -1;
                            }
                        }
                    }
                }
            }
            //The previous guess was not correct
            else
            {
                //There is no og correct direction, i.e only one guess made so far
                if (guesses[6] == 0)
                {
                    Random rnd = new Random();
                    bool run = true;
                    x = 0;
                    y = 0;
                    while (run)
                    {
                        //gets random direction
                        int dir = rnd.Next(1, 8);
                        switch (dir)
                        {
                            case 1:
                                x = guesses[1] - 1;
                                y = guesses[2] - 1;
                                break;
                            case 2:
                                x = guesses[1] - 1;
                                y = guesses[2];
                                break;
                            case 3:
                                x = guesses[1] - 1;
                                y = guesses[2] + 1;
                                break;
                            case 4:
                                x = guesses[1];
                                y = guesses[2] - 1;
                                break;
                            case 5:
                                x = guesses[1];
                                y = guesses[2] + 1;
                                break;
                            case 6:
                                x = guesses[1] + 1;
                                y = guesses[2] - 1;
                                break;
                            case 7:
                                x = guesses[1] + 1;
                                y = guesses[2];
                                break;
                            case 8:
                                x = guesses[1] + 1;
                                y = guesses[2] + 1;
                                break;
                        }
                        int[] status = checkGuessBounds(dir, x, y, opboard, false);
                        //within bounds
                        if (status[0] == 1)
                        {
                            if (hitOrMiss(user, status[1], status[2]))
                            {
                                guesses[0] = 1;
                                correct = true;
                                //sets correct direction
                                guesses[3] = dir;
                                //sets og direction
                                guesses[6] = dir;
                                //checking if the ship was sunk
                                if (opfleet[status[1], status[2]].isSunk())
                                {
                                    for (int i = 0; i < guesses.Length; i++)
                                    {
                                        guesses[i] = -1;
                                    }
                                    sunkShips++;
                                }
                            }
                            else
                            {
                                guesses[0] = 0;
                            }
                            run = false;
                        }
                    }
                }
                else
                {
                    //not currently working on a ship
                    if (guesses[6] == -1)
                    {
                        Random rnd = new Random();
                        bool run = true;
                        while (run)
                        {
                            x = rnd.Next(0, 9);
                            y = rnd.Next(0, 9);
                            //found a ship to sink
                            if (opfleet[x, y].getName() != "None")
                            {
                                opboard[x, y] = "H";
                                opfleet[x, y].hit();
                                guesses[0] = 1;
                                correct = true;
                                if (!opfleet[x, y].isSunk())
                                {
                                    guesses[1] = x;
                                    guesses[2] = y;
                                    guesses[4] = x;
                                    guesses[5] = y;
                                    guesses[6] = 0;
                                    sunkShips++;
                                }
                                run = false;
                            }
                            //already guessed
                            else if (opboard[x,y] == "O" || opboard[x,y] == "H")
                            {
                                continue;
                            }
                            //nothing guessed
                            else
                            {
                                opboard[x, y] = "O";
                                guesses[0] = 0;
                                run = false;
                            }
                        }
                        user.setBoard(opboard);
                        user.setFleet(opfleet);
                    }
                    //currently working on a ship
                    else
                    {

                        //there is a og correct direction, go in opposite direction.
                        int newDir = flipDirection(guesses[6]);
                        int[] status = checkGuessBounds(newDir, guesses[4], guesses[5], opboard, true);
                        //working status
                        if (status[0] == 1)
                        {
                            if (hitOrMiss(user, status[1], status[2]))
                            {
                                guesses[0] = 1;
                                correct = true;
                                //sets correct direction
                                guesses[3] = newDir;
                                //checking if the ship was sunk
                                if (opfleet[status[1], status[2]].isSunk())
                                {
                                    for (int i = 0; i < guesses.Length; i++)
                                    {
                                        guesses[i] = -1;
                                    }
                                    sunkShips++;
                                }
                            }
                            else
                            {
                                guesses[0] = 0;
                            }

                        }

                    }
                }
            }
            user.setSunkShips(sunkShips);
            return correct;
        } 
        //returns true if guess was correct, false if not
        public bool hitOrMiss(BattleshipPlayer user, int cX, int cY)
        {
            String[,] usrboard = user.getBoard();
            Ship[,] usrfleet = user.getFleet();
            //checks if the spot has a ship on it
            if (usrfleet[cX, cY].getName() != "None")
            {
                usrboard[cX, cY] = "H";
                usrfleet[cX, cY].hit();
                guesses[1] = cX;
                guesses[2] = cY;
                user.setBoard(usrboard);
                user.setFleet(usrfleet);
                return true;
            }
            //was a miss
            else
            {
                usrboard[cX,cY] = "O";
                guesses[0] = 0;
                user.setBoard(usrboard);
                return false;
            }
        }
        //checks the bounds of coordinates, makes sure spot was not already taken
        public int[] checkGuessBounds(int direction, int x, int y, String[,] usrboard, bool mov)
        {
            int[] output = new int[3];
            int cX = 0;
            int cY = 0;
            switch (direction)
            {
                case 1:
                    cX = x - 1;
                    cY = y - 1;
                    break;
                case 2:
                    cX = x - 1;
                    cY = y;
                    break;
                case 3:
                    cX = x - 1;
                    cY = y + 1;
                    break;
                case 4:
                    cX = x;
                    cY = y - 1;
                    break;
                case 5:
                    cX = x;
                    cY = y + 1;
                    break;
                case 6:
                    cX = x + 1;
                    cY = y - 1;
                    break;
                case 7:
                    cX = x + 1;
                    cY = y;
                    break;
                case 8:
                    cX = x + 1;
                    cY = y + 1;
                    break;
            }
            //checks if the coordinates need to move
            if (mov)
            {
                output[1] = cX;
                output[2] = cY;
            }
            else
            {
                output[1] = x;
                output[2] = y;
                cX = x;
                cY = y;
            }
            if (cX >= 0 && cX < 9 && cY >= 0 && cY < 9)
            {
                //guess is within bounds, check if used already
                if (usrboard[cX, cY] == "O" || usrboard[cX,cY] == "H") {
                    output[0] = 0;
                    return output;
                }
                //guess is valid
                else
                {
                    output[0] = 1;
                    return output;
                }
            }
            else
            {
                output[0] = 0;
                return output;
            }
        }
        //flips the direction of a given value
        public int flipDirection(int direction)
        {
            switch(direction)
            {
                case 1:
                    return 8;
                case 2:
                    return 7;
                case 3:
                    return 6;
                case 4:
                    return 5;
                case 5:
                    return 4;
                case 6:
                    return 3;
                case 7:
                    return 2;
                case 8:
                    return 1;
            }
            return 0;
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
