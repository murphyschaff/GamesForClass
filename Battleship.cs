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
        private int dimention = 9;
        private Button[,] cpuButtons;
        private Button[,] plrButtons;
        public Battleship()
        {
            InitializeComponent();
            createButtons();
        }
        public void initBattleship()
        {
            player = new BattleshipPlayer();
            CPU = new BattleshipPlayer();
        }
        //adds buttons to board on initilization
        private void createButtons()
        {
            cpuButtons = new Button[dimention, dimention];
            plrButtons = new Button[dimention, dimention];
            int buttonSize = 40;
            int[] CPUstart = { 63, 103 };
            int[] PLRstart = { 611, 103 };
            for (int i = 0; i < dimention; i++)
            {
                for (int j = 0; j < dimention; j++)
                {
                    //CPU buttons
                    Button cpuButton = new Button();
                    String name = "cpu" + i.ToString() + "-" + j.ToString();
                    cpuButton.Name = name;
                    cpuButton.Size = new Size(buttonSize, buttonSize);
                    cpuButton.Location = new Point(CPUstart[0] + (buttonSize * i), CPUstart[1] + (buttonSize * j));
                    cpuButton.MouseDown += CPUButtonClick;
                    cpuButton.BackColor = Color.DeepSkyBlue;
                    cpuButton.BringToFront();
                    this.Controls.Add(cpuButton);
                    cpuButton.Enabled = false;
                    cpuButtons[i, j] = cpuButton;

                    //player buttons
                    Button plrButton = new Button();
                    name = "plr" + i.ToString() + "-" + j.ToString();
                    plrButton.Name = name;
                    plrButton.Size = new Size(buttonSize, buttonSize);
                    plrButton.Location = new Point(PLRstart[0] + (buttonSize * i), PLRstart[1] + (buttonSize * j));
                    plrButton.MouseDown += PLRButtonClick;
                    plrButton.BackColor = Color.DeepSkyBlue;
                    plrButton.BringToFront();
                    this.Controls.Add(plrButton);
                    plrButtons[i, j] = plrButton;
                }
            }
        }
        //when the player selects a button on the CPU board
        private void CPUButtonClick(object sender, MouseEventArgs e)
        {
            playerFeedback.Text = "test";
        }
        //when the player selects a button
        private void PLRButtonClick(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            String selectedShip = shipSelection.Text;
            int size;
            if (selectedShip == "Select Ship")
            {
                playerFeedback.Text = "Please select a ship to place";
                return;
            }
            else if (selectedShip == "Aircraft Carrier (Size: 4)")
            {
                size = 4;
                playerFeedback.Text = "";
            }
            else if (selectedShip == "Battleship (Size: 3)")
            {
                size = 3;
                playerFeedback.Text = "";
            }
            else if (selectedShip == "Destroyer (Size: 2)")
            {
                size = 2;
                playerFeedback.Text = "";
            }
            //submarine
            else
            {
                size = 1;
                playerFeedback.Text = "";
            }

            if (button.Text != "")
            {
                if (button.Text == "S")
                {
                    changeButtonEnable(plrButtons, true);
                    changeButtonSurround(button, size, 1);
                }
            }
            else
            {
                changeButtonEnable(plrButtons, false);
                changeButtonSurround(button, size, 0);
            }
        }
        /* changes button surround to specified type */
        //shipType: 0: Aircraft carrier, 1: battleship, 2: destroyer, 3: submarine
        //placeType: 0: adds directions and 's' for starting place, 1: removes all options
        public void changeButtonSurround(Button center, int shipSize, int placeType)
        {
            String buttonName = center.Name;
            String output = "";
            int xVal = 0;
            int yVal;
            for (int i = 3; i < buttonName.Length; i++)
            {
                if (buttonName[i] == '-')
                {
                    xVal = (Convert.ToInt32(output));
                    output = "";
                }
                else
                {
                    output += buttonName[i];
                }
            }
            yVal = Convert.ToInt32(output);
            //sets corresponding buttons
            //middle
            if (placeType == 0) { center.Text = "S"; center.Enabled = true; } else { center.Text = ""; }
            //top
            if (xVal - shipSize >= 0)
            {
                //top left
                if (yVal - shipSize >= 0)
                {
                    if (placeType == 0)
                    {
                        plrButtons[xVal - 1, yVal - 1].Text = "UL";
                        plrButtons[xVal - 1, yVal - 1].Enabled = true;
                    }
                    else
                    {
                        plrButtons[xVal - 1, yVal - 1].Text = "";
                    }
                }
                //top right
                if (shipSize + yVal <= dimention)
                {
                    if (placeType == 0)
                    {
                        plrButtons[xVal - 1, yVal + 1].Text = "DL";
                        plrButtons[xVal - 1, yVal + 1].Enabled = true;
                    }
                    else
                    {
                        plrButtons[xVal - 1, yVal + 1].Text = "";
                    }
                }
                //top
                if (placeType == 0)
                {
                    plrButtons[xVal - 1, yVal].Text = "L";
                    plrButtons[xVal - 1, yVal].Enabled = true;
                }
                else
                {
                    plrButtons[xVal - 1, yVal].Text = "";
                }
            }
            //right
            if (shipSize + xVal <= dimention)
            {
                //top right
                if (yVal - shipSize >= 0)
                {
                    if (placeType == 0)
                    {
                        plrButtons[xVal + 1, yVal - 1].Text = "UR";
                        plrButtons[xVal + 1, yVal - 1].Enabled = true;
                    }
                    else
                    {
                        plrButtons[xVal + 1, yVal - 1].Text = "";
                    }
                }
                //bottom right
                if (shipSize + yVal <= dimention)
                {
                    if (placeType == 0)
                    {
                        plrButtons[xVal + 1, yVal + 1].Text = "BR";
                        plrButtons[xVal + 1, yVal + 1].Enabled = true;
                    }
                    else
                    {
                        plrButtons[xVal + 1, yVal + 1].Text = "";
                    }
                }
                //right
                if (placeType == 0)
                {
                    plrButtons[xVal + 1, yVal].Text = "R";
                    plrButtons[xVal + 1, yVal].Enabled = true;
                }
                else
                {
                    plrButtons[xVal + 1, yVal].Text = "";
                }
            }
            //up
            if (yVal - shipSize >= 0)
            {
                if (placeType == 0)
                {
                    plrButtons[xVal, yVal -1].Text = "U";
                    plrButtons[xVal, yVal - 1].Enabled = true;
                }
                else
                {
                    plrButtons[xVal, yVal - 1].Text = "";
                }
            }
            //down
            if (shipSize + yVal <= dimention)
            {
                if (placeType == 0)
                {
                    plrButtons[xVal, yVal + 1].Text = "B";
                    plrButtons[xVal, yVal + 1].Enabled = true;
                }
                else
                {
                    plrButtons[xVal, yVal + 1].Text = "";
                }
            }   
        }
        //Marks all buttons in given array as enabled or disabled
        private void changeButtonEnable(Button[,] array, bool enabled)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0;  j < array.GetLength(1); j++)
                {
                    array[i, j].Enabled = enabled;
                }
            }
        }
        /* places player ship on board */
        public bool placePlayerShip(int type, int direction)
        {
            int shipSize;
            String shipLetter;
            switch (type)
            {
                case 0: shipSize = 4; shipLetter = "A"; break;
                case 1: shipSize = 3; shipLetter = "B"; break;
                case 2: shipSize = 2; shipLetter = "D"; break;
                case 3: shipSize = 1; shipLetter = "S"; break;
            }
            //find current ship direction
            return false;
            
        }
        //Updates graphics on board, depends on if board is being updated when player placing ships
        public void updateBoard(bool isplayer)
        {
            if (isplayer) 
            {
                //shows everything on board for player, places ships
                String[,] board = player.getBoard();
                for (int i =0; i < dimention; i++)
                {
                    for (int j =0; j < dimention; j++)
                    {
                        plrButtons[i, j].Text = board[i, j];
                    }
                }
            }
            else
            {
                
            }
        }
        //Places guess on board, tells player if it was a hit or not (Assumes valid coordinates)
        public void makeGuess(BattleshipPlayer user, int[] coords)
        {
            String[,] board = user.getBoard();
            Ship[,] fleet = user.getFleet();
            coords[0] = coords[0] - 1;
            coords[1] = coords[1] - 1;

            //checks if the space has been guessed already
            if (board[coords[0], coords[1]] == "O" || board[coords[0], coords[1]] == "H")
            {
                label11.Text = "You have already selected this space";
                return;
            }
            //Is a hit
            else if (fleet[coords[0],coords[1]].getName() != "None")
            {
                board[coords[0],coords[1]] = "H";
                Ship ship = fleet[coords[0],coords[1]];
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
            }
            else if (cpusunk > 5)
            {
                label11.Text = label11.Text + "\nYou have sunk all CPU ships. You Win!!!!";
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

        //makes sure placement of ship coordinates are within boundaries, and if the placement is diagonal that the slope is 1
        public bool validatePlaceCoords(String shipType, int[] startCoord, int[] endCoord)
        {
            bool output = true;
            int startX = startCoord[0];
            int endX = endCoord[0];
            int startY = startCoord[1];
            int endY = endCoord[1];
            int type;
            int index = 0;
            int[] numShips = player.getNumShips();

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
                playerFeedback.Text = "Ship outside of boundaries";
            }
            else if (startX < 1 || endX < 1 || startY < 1 || endY < 1)
            {
                output = false;
                playerFeedback.Text = "Ship outside of boundaries";
            }
            //if the slope is not flat or vertical
            else if (Math.Abs(startX - endX) != 0 && Math.Abs(startY - endY) != 0)
            {
                if (Math.Abs(startX - endX) / Math.Abs(startY - endY) != 1)
                {
                    output = false; 
                    playerFeedback.Text = "Slope is too steep to place ship";
                }
            }
            else if (shipType == "Select Ship")
            {
                output = false;
                playerFeedback.Text = "Please select a ship";
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
                    playerFeedback.Text = "Wrong length for selected ship";
                    }
                    if (numShips[0] == 1)
                    {
                        playerFeedback.Text = "You have placed the max number of ships of this type";
                        output = false;
                    }
                    shipName = "Aircraft Carrier";
                    letter = "A";
                    index = 0;
                }
                else if (shipType == "Battleship (Size: 3)")
                {
                    if (length != 3)
                    {
                        output = false;
                        playerFeedback.Text = "Wrong length for selected ship";
                    }
                    if (numShips[1] == 2)
                    {
                        playerFeedback.Text = "You have placed the max number of ships of this type";
                        output = false;
                    }
                    shipName = "Battleship";
                    letter = "B";
                    index = 1;
                }
                else if (shipType == "Destroyer (Size: 2)")
                {
                    if (length != 2)
                    {
                        output = false;
                        playerFeedback.Text = "Wrong length for selected ship";
                    }
                    if (numShips[2] == 2)
                    {
                        playerFeedback.Text = "You have placed the max number of ships of this type";
                        output = false;
                    }
                    shipName = "Destroyer";
                    letter = "D";
                    index = 2;
                }
                else if (shipType == "Submarine (Size: 1)")
                {
                    if (length != 1)
                    {
                        output = false;
                        playerFeedback.Text = "Wrong length for selected ship";
                    }
                    if (numShips[3] == 1)
                    {
                        playerFeedback.Text = "You have placed the max number of ships of this type";
                        output = false;
                    }
                    shipName = "Submarine";
                    letter = "S";
                    index = 3;
                }
                Ship ship = new Ship(length, shipName);
                //makes sure a ship is not already there
                startCoord[0] = startCoord[0] - 1;
                startCoord[1] = startCoord[1] - 1;
                endCoord[0] = endCoord[0] - 1;
                endCoord[1] = endCoord[1] - 1;
                if (output)
                {
                    if (!player.addShip(startCoord, endCoord, type, ship, letter, true))
                    {
                        output = false;
                        playerFeedback.Text = "A ship already exists in this space.";
                    }
                    else
                    {
                        numShips[index] = numShips[index] + 1;
                        player.setNumShips(numShips);
                    }
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
            int[] numShips = player.getNumShips();
            updateBoard(true);
            if (numShips[0] == 1 && numShips[1] == 2 && numShips[2] == 2 && numShips[3] == 1)
            {
                button3.Visible = true;
                shipSelection.Visible = false;
                playerFeedback.Visible = false;
            }
        }
        //Start game function
        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            //for testing purposes, user can see CPU placed ships
            CPU.placeShips(false);
            updateBoard(false);
        }
        //button that runs the make guess sequence
        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        //auto place ships
        private void button5_Click(object sender, EventArgs e)
        {
            
            player.placeShips(true);
            updateBoard(false);
        }
        //reset button sequence
        private void button4_Click(object sender, EventArgs e)
        {
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
        private int[] numShips = { 0, 0, 0, 0 };
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
        public int[] getNumShips() { return numShips; }
        public void setNumShips(int[] numShips) { this.numShips = numShips; }
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
