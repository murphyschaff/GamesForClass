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
using System.Drawing.Imaging;

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
            //gets coordinates of button
            Button button = (Button)sender;
            String buttonName = button.Name;
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
            int[] coords = { xVal, yVal };
            makeGuess(CPU, coords);
        }
        //when the player selects a button
        private void PLRButtonClick(object sender, MouseEventArgs e)
        {
            //gets name of button and finds the corresponding coordinates
            Button button = (Button)sender;
            String buttonName = button.Name;
            String output = "";
            int xVal = 0;
            int yVal;
            int[] numShips = player.getNumShips();
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
            //gets user ship selection
            String selectedShip = shipSelection.Text;
            int size;
            if (selectedShip == "Select Ship")
            {
                playerFeedback.Text = "Please select a ship to place";
                return;
            }
            else if (selectedShip == "Aircraft Carrier (Size: 4)")
            {
                if (numShips[0] > 0)
                {
                    playerFeedback.Text = "You have already placed the maximum number of Aircraft Carriers (1)";
                    return;
                }
                size = 4;
                playerFeedback.Text = "";
            }
            else if (selectedShip == "Battleship (Size: 3)")
            {
                if (numShips[1] > 1)
                {
                    playerFeedback.Text = "You have already placed the maximum number of Battleships (2)";
                    return;
                }
                size = 3;
                playerFeedback.Text = "";
            }
            else if (selectedShip == "Destroyer (Size: 2)")
            {
                if (numShips[2] > 1)
                {
                    playerFeedback.Text = "You have already placed the maximum number of Destroyers (2)";
                    return;
                }
                size = 2;
                playerFeedback.Text = "";
            }
            //submarine
            else
            {
                if (numShips[3] > 0)
                {
                    playerFeedback.Text = "You have already placed the maximum number of Submarines (1)";
                    return;
                }
                size = 1;
                playerFeedback.Text = "";
            }
            //sets button text based on what is there
            if (button.Text != "")
            {
                if (button.Text == "C")
                {
                    updateBoard(true);
                }
                else
                {
                    //find corresponding direction
                    int direction = 0;
                    switch(button.Text)
                    {
                        case "UL": direction = 1; xVal++; yVal++; break;
                        case "U": direction = 2; yVal++; break;
                        case "UR": direction = 3; xVal--; yVal++; break;
                        case "L": direction = 4; xVal++; break;
                        case "R": direction = 5; xVal--; break;
                        case "DL": direction= 6; xVal++; yVal--; break;
                        case "D": direction = 7; yVal--; break;
                        case "DR": direction = 8; xVal--; yVal--; break;
                        case "P": direction = 8; break;
                    }
                    changeButtonSurround(button, xVal, yVal, size);
                    //ship is to be placed
                    player.addShipNew(xVal, yVal, size, direction);
                    updateBoard(true);
                }
            }
            else
            {
                changeButtonEnable(plrButtons, false);
                changeButtonSurround(button, xVal, yVal, size);
            }

            //if all ships are placed, enables start game
            if (numShips[0] == 1 && numShips[1] == 2 && numShips[2] == 2 & numShips[3] == 1)
            {
                startButton.Visible = true;
                autoPlaceShips.Visible = false;
                shipSelection.Visible = false;
                playerFeedback.Text = "Ready to start!";
            }
        }
        /* changes button surround to specified type */
        //shipType: 0: Aircraft carrier, 1: battleship, 2: destroyer, 3: submarine
        //placeType: 0: adds directions and 's' for starting place, 1: removes all options
        public void changeButtonSurround(Button center, int xVal, int yVal, int shipSize)
        {
            //sets corresponding buttons
            //middle
            //checks to see if the ship is a submarine
            if (shipSize == 1)
            {
                center.Text = "P";
                center.Enabled = true;
                return;
            }
            center.Text = "C";
            center.Enabled = true;
            //top
            if (Math.Abs(xVal - shipSize) >= 0)
            {
                //top left
                if (Math.Abs(yVal - shipSize) >= 0)
                {
                    if (player.checkForShip(xVal, yVal, shipSize, 1))
                    {
                        plrButtons[xVal - 1, yVal - 1].Text = "UL";
                        plrButtons[xVal - 1, yVal - 1].Enabled = true;
                    }
                }
                //top right
                if (shipSize + yVal <= dimention)
                {
                    if (player.checkForShip(xVal, yVal, shipSize, 6))
                    {
                        plrButtons[xVal - 1, yVal + 1].Text = "DL";
                        plrButtons[xVal - 1, yVal + 1].Enabled = true;
                    }

                }
                //top
                if (player.checkForShip(xVal, yVal, shipSize, 4))
                {
                    plrButtons[xVal - 1, yVal].Text = "L";
                    plrButtons[xVal - 1, yVal].Enabled = true;
                }
            }
            //right
            if (shipSize + xVal <= dimention)
            {
                //top right
                if (Math.Abs(yVal - shipSize) >= 0)
                {
                    if (player.checkForShip(xVal, yVal, shipSize, 3))
                    {
                        plrButtons[xVal + 1, yVal - 1].Text = "UR";
                        plrButtons[xVal + 1, yVal - 1].Enabled = true;
                    }
                }
                //bottom right
                if (shipSize + yVal <= dimention)
                {
                    if (player.checkForShip(xVal, yVal, shipSize, 8))
                    {
                        plrButtons[xVal + 1, yVal + 1].Text = "DR";
                        plrButtons[xVal + 1, yVal + 1].Enabled = true;
                    }
                }
                //right
                if (player.checkForShip(xVal, yVal, shipSize, 5))
                {
                    plrButtons[xVal + 1, yVal].Text = "R";
                    plrButtons[xVal + 1, yVal].Enabled = true;
                }
            }
            //up
            if (Math.Abs(yVal - shipSize) >= 0)
            {
                if (player.checkForShip(xVal, yVal, shipSize, 2))
                {
                    plrButtons[xVal, yVal -1].Text = "U";
                    plrButtons[xVal, yVal - 1].Enabled = true;
                }
            }
            //down
            if (shipSize + yVal <= dimention)
            {
                if (player.checkForShip(xVal, yVal, shipSize, 7))
                {
                    plrButtons[xVal, yVal + 1].Text = "D";
                    plrButtons[xVal, yVal + 1].Enabled = true;
                }
            }   
        }
        //Marks all not ship buttons in given array as enabled or disabled
        private void changeButtonEnable(Button[,] array, bool enabled)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0;  j < array.GetLength(1); j++)
                {
                    //if (array[i,j].Text != "A" || array[i,j].Text != "B" || array[i,j].Text != "D" || array[i,j].Text != "S")
                    array[i, j].Enabled = enabled;
                }
            }
        }
        //Updates graphics on board, depends on if board is being updated when player placing ships
        public void updateBoard(bool isplayer)
        {
            if (isplayer) 
            {
                //shows everything on board for player, places ships
                String[,] board = player.getBoard();
                Ship[,] fleet = player.getFleet();
                int sunkShips = player.getSunkShips();
                test.Text = sunkShips.ToString();
                for (int i =0; i < dimention; i++)
                {
                    for (int j =0; j < dimention; j++)
                    {
                        if (board[i, j] == "")
                        {
                            if (fleet[i, j].getName() != "None")
                            {
                                plrButtons[i, j].Text = fleet[i, j].getLetter();
                                plrButtons[i, j].Enabled = false;
                                if (fleet[i, j].isSunk())
                                {
                                    plrButtons[i, j].BackColor = Color.Red;
                                }
                                else
                                {
                                    plrButtons[i, j].BackColor = Color.Gray;
                                }
                            }
                            else if (plrButtons[i, j].Text != "" && guessFeedback.Text != "")
                            {
                                plrButtons[i, j].Enabled = true;
                                plrButtons[i, j].Text = "";
                            }
                            else
                            {
                                if (guessFeedback.Text == "") { plrButtons[i, j].Enabled = true; }
                            }
                        }
                        else
                        {
                            if (board[i,j] == "H" || board[i,j] == "O")
                            {
                                if (board[i,j] == "H")
                                {
                                    plrButtons[i, j].BackColor = Color.Yellow;
                                }
                            }
                            else
                            {
                                plrButtons[i, j].Enabled = true;

                            }
                            plrButtons[i, j].Text = board[i, j];
                        }
                    }
                }
            }
            else
            {
                String[,] board = CPU.getBoard();
                Ship[,] fleet = CPU.getFleet();
                for (int i = 0; i < dimention; i++)
                {
                    for (int j = 0; j < dimention; j++)
                    {
                        cpuButtons[i,j].Text = board[i,j];
                        if (board[i,j] == "H")
                        {
                            if (fleet[i,j].isSunk())
                            {
                                cpuButtons[i, j].BackColor = Color.Red;
                            }
                            else
                            {
                                cpuButtons[i, j].BackColor = Color.Yellow;
                            }
                            cpuButtons[i, j].Enabled = false;
                        }
                        else if (board[i,j] == "O")
                        {
                            cpuButtons[i, j].Enabled = false;
                        }
                    }
                }
            }
        }
        //Places guess on board, tells player if it was a hit or not (Assumes valid coordinates)
        public void makeGuess(BattleshipPlayer user, int[] coords)
        {
            String[,] board = user.getBoard();
            Ship[,] fleet = user.getFleet();

            //checks if the space has been guessed already
            if (board[coords[0], coords[1]] == "O" || board[coords[0], coords[1]] == "H")
            {
                guessFeedback.Text = "You have already selected this space";
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
                    guessFeedback.Text = String.Format("You sunk a {0}!", ship.getName());
                    CPU.setSunkShips(CPU.getSunkShips() + 1);
                }
                else
                {
                    guessFeedback.Text = "You hit a ship!";
                }
            }
            else
            {
                board[coords[0], coords[1]] = "O";
                guessFeedback.Text = "You missed.";
            }

            //AI makes guess
            if (CPU.newGuess(player))
            {
                guessFeedback.Text = guessFeedback.Text + "\nCPU hit a ship!";
            }
            else
            {
                guessFeedback.Text = guessFeedback.Text + "\nCPU missed.";
            }

            updateBoard(false);
            updateBoard(true);
            //check for win condition
            int playersunk = player.getSunkShips();
            int cpusunk = CPU.getSunkShips();
            if (playersunk > 5)
            {
                guessFeedback.Text = guessFeedback.Text + "\nThe CPU has sunk all your ships. You loose";
            }
            else if (cpusunk > 5)
            {
                guessFeedback.Text = guessFeedback.Text + "\nYou have sunk all CPU ships. You Win!!!!";
            }

        }
        /* BUTTON FUNCTIONS */
        #region button functions
        private void button4_Click(object sender, EventArgs e)
        {
            initBattleship();
            autoPlaceShips.Visible = true;
            shipSelection.Visible = true;
        }
        //automatically places player ships
        private void autoPlaceShips_Click(object sender, EventArgs e)
        {
            player.placeShips();
            startButton.Visible = true;
            autoPlaceShips.Visible = false;
            shipSelection.Visible = false;
            updateBoard(true);
        }
        //places CPU ships, starts game
        private void startButton_Click(object sender, EventArgs e)
        {
            autoPlaceShips.Visible = false;
            startButton.Visible = false;
            CPU.placeShips();
            changeButtonEnable(cpuButtons, true);
            changeButtonEnable(plrButtons, false);
            updateBoard(false);
        }
        #endregion
    }
    /* BattleshipPlayer class. Represents the players, holds board data */
    public class BattleshipPlayer
    {
        private String[,] board = new String[9,9];
        Ship[,] fleet = new Ship[9, 9];
        //keeps track of previous guess, 0&1: X,y coords of first guess, 2: direction type, 3&4: current guess coordinates
        private int[] guesses = { -1, -1, -1, -1, -1, -1 };
        //0: Aircraft carriers, 1: battleships, 2: destroyers, 3: submarines
        private int[] numShips = { 0, 0, 0, 0 };
        private int sunkShips = 0;
        private int guessStatus = 0;
        public BattleshipPlayer()
        {
            Ship blankShip = new Ship(0, "None", "N");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i,j] = "";
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
        public void placeShips()
        {
            int x = 0;
            int y = 0;
            int type;
            int count = 0;
            Random rnd = new Random();
            //places ships onto board
            while (count < 6)
            {
                //aircraft carrier
                if (count == 0)
                {
                    //gets placement type, 1-8 for each direction mentioned:
                    /* 1 2 3
                     * 4   5
                     * 6 7 8 */
                    type = rnd.Next(1, 8);
                    switch (type)
                    {
                        case 1:
                            x = rnd.Next(3, 8);
                            y = rnd.Next(3, 8);
                            break;
                        case 2:
                            x = rnd.Next(0, 8);
                            y = rnd.Next(3, 8); 
                            break;
                        case 3:
                            x = rnd.Next(0, 5);
                            y = rnd.Next(3, 8);    
                            break;
                        case 4:
                            x = rnd.Next(3, 8);
                            y = rnd.Next(0, 8);
                            break;
                        case 5:
                            x = rnd.Next(0, 5);
                            y = rnd.Next(0, 8);
                            break;
                        case 6:
                            x = rnd.Next(3, 8);
                            y = rnd.Next(0, 5);
                            break;
                        case 7:
                            x = rnd.Next(0, 8);
                            y = rnd.Next(0, 5);
                            break;
                        case 8:
                            x = rnd.Next(0, 5);
                            y = rnd.Next(0, 5);
                            break;
                    }
                    //checks if the ship can be placed, places it if it can
                    if (checkForShip(x, y, 4, type))
                    {
                        addShipNew(x, y, 4, type);
                        count++;
                    }
                }
                //Battleship
                else if (count < 3)
                {
                    //gets placement type, 1-8 for each direction mentioned:
                    /* 1 2 3
                     * 4   5
                     * 6 7 8 */
                    type = rnd.Next(1, 8);
                    switch (type)
                    {
                        case 1:
                            x = rnd.Next(2, 8);
                            y = rnd.Next(2, 8);
                            break;
                        case 2:
                            x = rnd.Next(0, 8);
                            y = rnd.Next(2, 8);
                            break;
                        case 3:
                            x = rnd.Next(0, 6);
                            y = rnd.Next(2, 8);
                            break;
                        case 4:
                            x = rnd.Next(2, 8);
                            y = rnd.Next(0, 8);
                            break;
                        case 5:
                            x = rnd.Next(0, 6);
                            y = rnd.Next(0, 8);
                            break;
                        case 6:
                            x = rnd.Next(2, 8);
                            y = rnd.Next(0, 6);
                            break;
                        case 7:
                            x = rnd.Next(0, 8);
                            y = rnd.Next(0, 6);
                            break;
                        case 8:
                            x = rnd.Next(0, 6);
                            y = rnd.Next(0, 6);
                            break;
                    }
                    //checks if the ship can be placed, places it if it can
                    if (checkForShip(x, y, 3, type))
                    {
                        addShipNew(x, y, 3, type);
                        count++;
                    }
                }
                //Destroyer
                else if (count < 5)
                {
                    //gets placement type, 1-8 for each direction mentioned:
                    /* 1 2 3
                     * 4   5
                     * 6 7 8 */
                    type = rnd.Next(1, 8);
                    switch (type)
                    {
                        case 1:
                            x = rnd.Next(1, 8);
                            y = rnd.Next(1, 8);
                            break;
                        case 2:
                            x = rnd.Next(0, 8);
                            y = rnd.Next(1, 8);
                            break;
                        case 3:
                            x = rnd.Next(0, 7);
                            y = rnd.Next(1, 8);
                            break;
                        case 4:
                            x = rnd.Next(1, 8);
                            y = rnd.Next(0, 8);
                            break;
                        case 5:
                            x = rnd.Next(0, 7);
                            y = rnd.Next(0, 8);
                            break;
                        case 6:
                            x = rnd.Next(1, 8);
                            y = rnd.Next(0, 7);
                            break;
                        case 7:
                            x = rnd.Next(0, 8);
                            y = rnd.Next(0, 7);
                            break;
                        case 8:
                            x = rnd.Next(0, 7);
                            y = rnd.Next(0, 7);
                            break;
                    }
                    //checks if the ship can be placed, places it if it can
                    if (checkForShip(x, y, 2, type))
                    {
                        addShipNew(x, y, 2, type);
                        count++;
                    }
                }
                //Submarine
                else
                {
                    x = rnd.Next(0, 8);
                    y = rnd.Next(0, 8);
                    if (checkForShip(x,y,1,1))
                    {
                        addShipNew(x, y, 1, 1);
                        count++;
                    }
                }
            }
        }
        //looks to see if there is already a ship in a desired location, and makes sure potential ship is within bounds
        //direction example
        /* 1 2 3
         * 4   5
         * 6 7 8 */
        public bool checkForShip(int startX, int startY, int size, int direction)
        {
            //both x and y change
            int dimention = fleet.GetLength(0);
            switch (direction)
            {
                case 1:
                    for (int i = 0; i < size; i++)
                    {
                        if (startX - i < 0 || startY - i < 0)
                        {
                            return false;
                        }
                        if (fleet[startX - i, startY - i].getName() != "None")
                        {
                            return false;
                        }
                    }
                    return true;
                case 2:
                    for (int i = 0; i < size; i++)
                    {
                        if (startY - i < 0)
                        {
                            return false;
                        }
                        if (fleet[startX, startY - i].getName() != "None")
                        {
                            return false;
                        }
                    }
                    return true;
                case 3:
                    for (int i = 0; i < size; i++)
                    {
                        if (startX + i >= dimention || startY - i < 0)
                        {
                            return false;
                        }
                        if (fleet[startX + i, startY - i].getName() != "None")
                        {
                            return false;
                        }
                    }
                    return true;
                case 4:
                    for (int i = 0; i < size; i++)
                    {
                        if (startX - i < 0)
                        {
                            return false;
                        }
                        if (fleet[startX - i, startY].getName() != "None")
                        {
                            return false;
                        }
                    }
                    return true;
                case 5:
                    for (int i = 0; i < size; i++)
                    {
                        if (startX + i >= dimention)
                        {
                            return false;
                        }
                        if (fleet[startX + i, startY].getName() != "None")
                        {
                            return false;
                        }
                    }
                    return true;
                case 6:
                    for (int i = 0; i < size; i++)
                    {
                        if (startX - i < 0 || startY + i >= dimention)
                        {
                            return false;
                        }
                        if (fleet[startX - i, startY + i].getName() != "None")
                        {
                            return false;
                        }
                    }
                    return true;
                case 7:
                    for (int i = 0; i < size; i++)
                    {
                        if (startY + i >= dimention)
                        {
                            return false;
                        }
                        if (fleet[startX, startY + i].getName() != "None")
                        {
                            return false;
                        }
                    }
                    return true;
                case 8:
                    for (int i = 0; i < size; i++)
                    {
                        if (startX + i >= dimention || startY + i >= dimention)
                        {
                            return false;
                        }
                        if (fleet[startX + i, startY + i].getName() != "None")
                        {
                            return false;
                        }
                    }
                    return true;
            }
            return false;
        }
        //adds ships to fleet, marks said ship as complete
        public void addShipNew(int startX, int startY, int size, int direction)
        {
            //creates new ship object to be added to fleet
            String name = "";
            String letter = "";
            //sets name, letter, and adds 1 to numShips
            switch (size)
            {
                case 1: name = "Submarine"; letter = "S"; numShips[3]++; break;
                case 2: name = "Destroyer"; letter = "D"; numShips[2]++;  break;
                case 3: name = "Battleship"; letter = "B"; numShips[1]++; break;
                case 4: name = "Aircraft Carrier"; letter = "A"; numShips[0]++ ; break;
            }
            Ship newShip = new Ship(size, name, letter);
            //adds the ship in the corresponding direction
            switch (direction)
            {
                case 1:
                    for (int i = 0; i < size; i++)
                    {
                        fleet[startX - i, startY - i] = newShip;
                    }
                    break;
                case 2:
                    for (int i = 0; i < size; i++)
                    {
                        fleet[startX, startY - i] = newShip;
                    }
                    break;
                case 3:
                    for (int i = 0; i < size; i++)
                    {
                        fleet[startX + i, startY - i] = newShip;
                    }
                    break;
                case 4:
                    for (int i = 0; i < size; i++)
                    {
                        fleet[startX - i, startY] = newShip;
                    }
                    break;
                case 5:
                    for (int i = 0; i < size; i++)
                    {
                        fleet[startX + i, startY] = newShip;
                    }
                    break;
                case 6:
                    for (int i = 0; i < size; i++)
                    {
                        fleet[startX - i, startY + i] = newShip;
                    }
                    break;
                case 7:
                    for (int i = 0; i < size; i++)
                    {
                        fleet[startX, startY + i] = newShip;
                    }
                    break;
                case 8:
                    for (int i = 0; i < size; i++)
                    {
                        fleet[startX + i, startY + i] = newShip;
                    }
                    break;
            }
        }
        //new guess algorithm
        //user: player whose board is being guessed on
        public bool newGuess(BattleshipPlayer user)
        {
            String[,] usrBoard = user.getBoard();
            Ship[,] usrFleet = user.getFleet();
            Random rnd = new Random();
            int x = 0;
            int y = 0;
            switch (guessStatus)
            {
                //finding random coordinate, as no previous guess was correct
                case 0:
                    bool find = true;
                    //runs until valid coordinates found
                    while (find)
                    {
                        x = rnd.Next(0, 8);
                        y = rnd.Next(0, 8);
                        if (usrBoard[x, y] != "H" && usrBoard[x, y] != "O")
                        {
                            find = false;
                        }
                    }
                    //the ship was hit, change guessStatus to 1 to find direction and register as a hit
                    if (usrFleet[x, y].getName() != "None")
                    {
                        usrBoard[x, y] = "H";
                        //0 & 1: X,y coords of first guess, 2: direction type, 3 & 4: current guess coordinates
                        guesses[0] = x;
                        guesses[1] = y;
                        //checks to see if the ship was sunk
                        if (usrFleet[x, y].isSunk() == true)
                        {
                            changeNumShips(usrFleet[x, y]);
                        }
                        else
                        {
                            guessStatus = 1;
                        }
                        return true;
                    }
                    else
                    {
                        usrBoard[x, y] = "O";
                        return false;
                    }
                //Finding a direction to try and sink the ship
                case 1:
                    x = guesses[0];
                    y = guesses[1];
                    int[] results = getDirectionCoords(usrBoard, x, y);
                    x = results[1];
                    y = results[2];
                    if (results[0] != -1)
                    {
                        //means the ship was hit
                        if (usrFleet[results[1], results[2]].getName() != "None")
                        {
                            //sets direction, changes status to 2
                            usrBoard[x,y] = "H";
                            guesses[2] = results[0];
                            guesses[3] = x;
                            guesses[4] = y;
                            usrFleet[x, y].hit();
                            //checks to see if the ship was sunk
                            if (usrFleet[x, y].isSunk() == true)
                            {
                                changeNumShips(usrFleet[x, y]);
                                guessStatus = 0;
                            }
                            else
                            {
                                guessStatus = 2;
                            }
                            return true;
                        }
                        else
                        {
                            //direction was wrong, mark as miss
                            usrBoard[x, y] = "O";
                            return false;
                        }
                    }
                    else
                    {
                        //all surrounds are unavailable, try random direction again
                        guessStatus = 0;
                        return false;
                    }
                //'hunts' ship until it is sunk
                case 2:
                    int[] newCoords = getCoords(guesses[2], guesses[3], guesses[4]);
                    if (newCoords[0] != -1)
                    {
                        x = newCoords[0];
                        y = newCoords[1];
                        if (usrBoard[x,y] != "H" && usrBoard[x,y] != "O")
                        {
                            if (usrFleet[x,y].getName() != "None")
                            {
                                //ship was hit, continue on current path
                                usrBoard[x, y] = "H";
                                guesses[3] = x;
                                guesses[4] = y;
                                usrFleet[x, y].hit();
                                //check if ship was sunk
                                if (usrFleet[x, y].isSunk() == true)
                                {
                                    changeNumShips(usrFleet[x,y]);
                                    guessStatus = 0;
                                }
                                return true;
                            }
                            else
                            {
                                //miss, flip direction and return false
                                guesses[2] = flipDirection(guesses[2]);
                                return false;
                            }
                        }
                    }
                    else
                    {
                        //try random direction again
                        guessStatus = 0;
                        return newGuess(user);
                    }
                    break;
            }
            return true;
        }
        //gets new coordinates to be tested by the algorithm
        //0: new x coord, 1: new y coord
        private int[] getCoords(int dir, int x, int y)
        {
            switch (dir)
            {
                case 1:
                    x--;
                    y--;
                    break;
                case 2:
                    y--;
                    break;
                case 3:
                    x++;
                    y--;
                    break;
                case 4:
                    x--;
                    break;
                case 5:
                    x++;
                    break;
                case 6:
                    x--;
                    y++;
                    break;
                case 7:
                    y++;
                    break;
                case 8:
                    x++;
                    y++;
                    break;
            }
            int[] ret = { 0, 0 };
            if (x >= 0 && x <= 9 && y >= 0 && y <= 9)
            {
                ret[0] = x;
                ret[1] = y;
                return ret;
            }
            else
            {
                ret[0] = -1;
                return ret;
            }
        }
        //gets 'random' direciton based on x,y and makes sure in bounds and not already used
        private int[] getDirectionCoords(String[,] usrBoard, int x, int y)
        {
            int[] directions = { 2, 7, 4, 5, 1, 3, 6, 8 };
            //0: direction number, 1: x coord, 2: y coord
            int[] ret = { 0,0,0 };
            for (int i = 0; i < directions.Length; i++)
            {
                switch (directions[i])
                {
                    case 1:
                        x--;
                        y--;
                        break;
                    case 2:
                        y--;
                        break;
                    case 3:
                        x++;
                        y--;
                        break;
                    case 4:
                        x--;
                        break;
                    case 5:
                        x++;
                        break;
                    case 6:
                        x--;
                        y++;
                        break;
                    case 7:
                        y++;
                        break;
                    case 8:
                        x++;
                        y++;
                        break;
                }
                //makes sure the guess is in bounds
                if (x >= 0 && x <= 9 && y >= 0 && y <= 9)
                {
                    //means that the spot was not already used
                    if (usrBoard[x, y] != "H" && usrBoard[x,y] != "O")
                    {
                        ret[0] = directions[i];
                        ret[1] = x;
                        ret[2] = y;
                        return ret;
                    }
                }
            }
            //no direction could be found, try a random spot on board again
            ret[0] = -1;
            return ret;
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
                                    changeNumShips(opfleet[status[1], status[2]]);
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
                                changeNumShips(opfleet[status[1], status[2]]);
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
                                    changeNumShips(opfleet[status[1], status[2]]);
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
                                    changeNumShips(opfleet[status[1], status[2]]);
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
        //index 0: 0 if used already, 1 if free. 1: x coord, 2: y coord
        /* 1 2 3
         * 4   5
         * 6 7 8 */
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
                    cX = x;
                    cY = y - 1;
                    break;
                case 3:
                    cX = x + 1;
                    cY = y - 1;
                    break;
                case 4:
                    cX = x - 1;
                    cY = y;
                    break;
                case 5:
                    cX = x + 1;
                    cY = y;
                    break;
                case 6:
                    cX = x - 1;
                    cY = y - 1;
                    break;
                case 7:
                    cX = x;
                    cY = y + 1;
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
        //changes number of ships that have not been sunk
        private void changeNumShips(Ship ship)
        {
            String letter = ship.getLetter();
            switch (letter)
            {
                case "A":
                    numShips[0]--; break;
                case "B":
                    numShips[1]--; break;
                case "D":
                    numShips[2]--; break;
                case "S":
                    numShips[3]--; break;
            }
        }
    }
    //Class that represents the individual ship
    public class Ship
    {
        private int size;
        private int health;
        private String name;
        private String letter;
        public Ship(int size, String name, string letter)
        {
            this.size = size;
            health = size;
            this.name = name;
            this.letter = letter;
        }

        //Getters and setters
        public int getSize() { return size; }
        public void setSize(int size) {  this.size = size; }
        public String getName() { return name; }
        public String getLetter() { return letter; }
        //checks if the ship is sunk. If there is zero health remaining, then it is sunk
        public bool isSunk()
        {
            if (health == 0)
            {
                return true;
            }
            return false;
        }
        //'hits' the ship
        public void hit()
        {
            health--;
        }
    }
}
