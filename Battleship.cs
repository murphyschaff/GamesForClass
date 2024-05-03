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
        BattleshipPlayer player;
        BattleshipPlayer CPU;
        private int dimention = 9;
        private Button[,] cpuButtons;
        private Button[,] plrButtons;
        public Battleship()
        {
            InitializeComponent();
            initBattleship();
            createButtons();
        }
        public void initBattleship()
        {
            player = new BattleshipPlayer(normalRadio.Checked);
            CPU = new BattleshipPlayer(true);
        }
        //adds buttons to board on initilization
        private void createButtons()
        {
            cpuButtons = new Button[dimention, dimention];
            plrButtons = new Button[dimention, dimention];
            int buttonSize = 40;
            int textSize = 20;
            int buttonTextSize = 12;
            int[] CPUstart = { 63, 103 };
            int[] PLRstart = { 611, 103 };
            for (int i = 0; i < dimention; i++)
            {
                //adds x lables
                Label cpuLabel = new Label();
                cpuLabel.Name = i.ToString() + "cpulabelx";
                cpuLabel.Font = new Font("Microsoft Sans Sarif", textSize);
                cpuLabel.TextAlign = ContentAlignment.MiddleCenter;
                cpuLabel.Size = new Size(buttonSize, buttonSize);
                cpuLabel.Text = (i + 1).ToString();
                cpuLabel.Location = new Point(CPUstart[0] + (buttonSize * i), CPUstart[1] - buttonSize);
                this.Controls.Add(cpuLabel);

                Label plrLabel = new Label();
                plrLabel.Name = i.ToString() + "plrlabelx";
                plrLabel.Font = new Font("Microsoft Sans Sarif", textSize);
                plrLabel.TextAlign = ContentAlignment.MiddleCenter;
                plrLabel.Size = new Size(buttonSize, buttonSize);
                plrLabel.Text = (i + 1).ToString();
                plrLabel.Location = new Point(PLRstart[0] + (buttonSize * i), PLRstart[1] - buttonSize);
                this.Controls.Add(plrLabel);
                //adds y labels
                Label cpuylabel = new Label();
                cpuylabel.Name = i.ToString() + "cpulabely";
                cpuylabel.Font = new Font("Microsoft Sans Sarif", textSize);
                cpuylabel.TextAlign = ContentAlignment.MiddleCenter;
                cpuylabel.Size = new Size(buttonSize, buttonSize);
                cpuylabel.Text = (i + 1).ToString();
                cpuylabel.Location = new Point(CPUstart[0] - buttonSize, CPUstart[1] + (buttonSize * i));
                this.Controls.Add(cpuylabel);

                Label plryLabel = new Label();
                plryLabel.Name = i.ToString() + "plrlabely";
                plryLabel.Font = new Font("Microsoft Sans Sarif", textSize);
                plryLabel.TextAlign = ContentAlignment.MiddleCenter;
                plryLabel.Size = new Size(buttonSize, buttonSize);
                plryLabel.Text = (i + 1).ToString();
                plryLabel.Location = new Point(PLRstart[0] - buttonSize, PLRstart[1] + (buttonSize * i));
                this.Controls.Add(plryLabel);

                for (int j = 0; j < dimention; j++)
                {
                    //CPU buttons
                    Button cpuButton = new Button();
                    String name = "cpu" + i.ToString() + "-" + j.ToString();
                    cpuButton.Name = name;
                    cpuButton.Font = new Font("Microsoft Sans Sarif", buttonTextSize);
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
                    plrButton.Font = new Font("Microsoft Sans Sarif", buttonTextSize);
                    plrButton.Size = new Size(buttonSize, buttonSize);
                    plrButton.Location = new Point(PLRstart[0] + (buttonSize * i), PLRstart[1] + (buttonSize * j));
                    plrButton.MouseDown += PLRButtonClick;
                    plrButton.BackColor = Color.DeepSkyBlue;
                    plrButton.BringToFront();
                    this.Controls.Add(plrButton);
                    plrButtons[i, j] = plrButton;
                }
            }
            userShipInfo.Text = "Aircraft Carriers: 0 Battleships: 0\nDestroyers: 0 Submarines: 0";
            cpuShipInfo.Text = "Aircraft Carriers: 0 Battleships: 0\nDestroyers: 0 Submarines: 0";
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
                    if (normalRadio.Checked)
                    {
                        playerFeedback.Text = "You have already placed the maximum number of Aircraft Carriers (1)";
                    }
                    else
                    {
                        playerFeedback.Text = "You cannot place Aircraft Carriers in this mode";
                    }
                    return;
                }
                size = 4;
                playerFeedback.Text = "";
            }
            else if (selectedShip == "Battleship (Size: 3)")
            {
                if (numShips[1] > 1)
                {
                    if (normalRadio.Checked)
                    {
                        playerFeedback.Text = "You have already placed the maximum number of Battleships (2)";
                    }
                    else
                    {
                        playerFeedback.Text = "You cannot place Battleships in this mode";
                    }
                    return;
                }
                size = 3;
                playerFeedback.Text = "";
            }
            else if (selectedShip == "Destroyer (Size: 2)")
            {
                if (numShips[2] > 1)
                {
                    if (normalRadio.Checked)
                    {
                        playerFeedback.Text = "You have already placed the maximum number of Destroyers (2)";
                    }
                    else
                    {
                        playerFeedback.Text = "You cannot place Destroyers in this mode";
                    }
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
                    if (size == 1)
                    {
                        lookAtSub(button, xVal, yVal, true);
                    }
                    else
                    {
                        changeButtonSurround(button, xVal, yVal, size, true);
                    }
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
                    if (size == 1)
                    {
                        lookAtSub(button, xVal, yVal, true);
                    }
                    else
                    {
                        changeButtonSurround(button, xVal, yVal, size, true);
                    }
                    //ship is to be placed
                    autoPlaceShips.Visible = false;
                    player.addShipNew(xVal, yVal, size, direction);
                    updateBoard(true);
                }
            }
            else
            {
                changeButtonEnable(plrButtons, false);
                if (size == 1)
                {
                    lookAtSub(button, xVal, yVal, false);
                }
                else
                {
                    changeButtonSurround(button, xVal, yVal, size, false);
                }
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
        /* Specifically allows for placement of submarine */
        public void lookAtSub(Button center, int xVal, int yVal, bool remove)
        {
            //if the values are supposed to be removed
            if (remove)
            {
                //the player did not want to place the submarine
                if (center.Text == "C")
                {
                    if (yVal == 1 && plrButtons[xVal, 0].Text == "P")
                    {
                        plrButtons[xVal, 0].Text = "";
                    }
                    else
                    {
                        if (yVal == dimention -1)
                        {
                            plrButtons[xVal, yVal - 1].Text = "";
                        }
                        else
                        {
                            plrButtons[xVal, yVal + 1].Text = "";
                        }
                    } 
                }
                //The user wants to place the submarine
                else
                {
                    if (yVal == 0)
                    {
                        plrButtons[xVal, yVal + 1].Text = "";
                    }
                    else
                    {
                        plrButtons[xVal, yVal - 1].Text = "";
                    }
                }
                center.Text = "";
            }
            //if the values are supposed to be added
            else
            {
                center.Text = "P";
                center.Enabled = true;
                if (yVal == 0)
                {
                    plrButtons[xVal, yVal + 1].Text = "C";
                    plrButtons[xVal, yVal + 1].Enabled = true;
                }
                else
                {
                    plrButtons[xVal, yVal - 1].Text = "C";
                    plrButtons[xVal, yVal - 1].Enabled = true;
                }
            }
            
        }
        /* changes button surround to specified type */
        public void changeButtonSurround(Button center, int xVal, int yVal, int shipSize, bool remove)
        {
            //sets corresponding buttons
            //middle
            if (!remove) { center.Text = "C"; } else { center.Text = ""; }
            center.Enabled = true;
            //top
            if (Math.Abs(xVal - shipSize) >= 0)
            {
                //top left
                if (Math.Abs(yVal - shipSize) >= 0)
                {
                    if (player.checkForShip(xVal, yVal, shipSize, 1))
                    {
                        if (!remove) { plrButtons[xVal - 1, yVal - 1].Text = "UL"; } else { plrButtons[xVal - 1, yVal - 1].Text = ""; }
                        plrButtons[xVal - 1, yVal - 1].Enabled = true;
                    }
                }
                //top right
                if (shipSize + yVal <= dimention)
                {
                    if (player.checkForShip(xVal, yVal, shipSize, 6))
                    {
                        if (!remove) { plrButtons[xVal - 1, yVal + 1].Text = "DL"; } else { plrButtons[xVal - 1, yVal + 1].Text = ""; }
                        plrButtons[xVal - 1, yVal + 1].Enabled = true;
                    }

                }
                //top
                if (player.checkForShip(xVal, yVal, shipSize, 4))
                {
                    if (!remove) { plrButtons[xVal - 1, yVal].Text = "L"; } else { plrButtons[xVal - 1, yVal].Text = ""; }
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
                        if (!remove) { plrButtons[xVal + 1, yVal - 1].Text = "UR"; } else { plrButtons[xVal + 1, yVal - 1].Text = ""; }
                        plrButtons[xVal + 1, yVal - 1].Enabled = true;
                    }
                }
                //bottom right
                if (shipSize + yVal <= dimention)
                {
                    if (player.checkForShip(xVal, yVal, shipSize, 8))
                    {
                        if (!remove) { plrButtons[xVal + 1, yVal + 1].Text = "DR"; } else { plrButtons[xVal + 1, yVal + 1].Text = ""; }
                        plrButtons[xVal + 1, yVal + 1].Enabled = true;
                    }
                }
                //right
                if (player.checkForShip(xVal, yVal, shipSize, 5))
                {
                    if (!remove) { plrButtons[xVal + 1, yVal].Text = "R"; } else { plrButtons[xVal + 1, yVal].Text = ""; }
                    plrButtons[xVal + 1, yVal].Enabled = true;
                }
            }
            //up
            if (Math.Abs(yVal - shipSize) >= 0)
            {
                if (player.checkForShip(xVal, yVal, shipSize, 2))
                {
                    if (!remove) { plrButtons[xVal, yVal - 1].Text = "U"; } else { plrButtons[xVal, yVal - 1].Text = ""; }
                    plrButtons[xVal, yVal - 1].Enabled = true;
                }
            }
            //down
            if (shipSize + yVal <= dimention)
            {
                if (player.checkForShip(xVal, yVal, shipSize, 7))
                {
                    if (!remove) { plrButtons[xVal, yVal + 1].Text = "D"; } else { plrButtons[xVal, yVal + 1].Text = ""; }
                    plrButtons[xVal, yVal + 1].Enabled = true;
                }
            }   
        }
        public void changeButtonBackColor(Button[,] buttons, Color color)
        {
            for (int i = 0; i < dimention; i++)
            {
                for (int j = 0; j < dimention; j++)
                {
                    buttons[i,j].BackColor = color;
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
        private void removeButtonText(Button[,] array, Button[,] array2)
        {
            for (int i = 0; i < dimention; i++)
            {
                for (int j = 0; j < dimention; j++)
                {
                    array[i, j].Text = "";
                    array2[i, j].Text = "";
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
                                plrButtons[i, j].BackColor = Color.Gray;                               
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
                                if (fleet[i,j].isSunk() && board[i,j] != "O")
                                {
                                    plrButtons[i,j].BackColor = Color.Red;
                                }
                                else if (board[i,j] == "H")
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
            //Updates both player and CPU ship status
            }
            int[] plrShips = player.getNumShips();
            int[] cpuShips = CPU.getNumShips();
            //checks which mode this is
            if (normalRadio.Checked == true)
            {
                userShipInfo.Text = "Aircraft Carriers: " + plrShips[0].ToString() + " Battleships: " + plrShips[1].ToString() + "\nDestroyers: " + plrShips[2].ToString() + " Submarines: " + plrShips[3].ToString();
            }
            else
            {
                userShipInfo.Text = "Aircraft Carriers: 0 Battleships: 0\nDestroyers: 0 Submarines: " + plrShips[3].ToString();
            }
            cpuShipInfo.Text = "Aircraft Carriers: " + cpuShips[0].ToString() + " Battleships: " + cpuShips[1].ToString() + "\nDestroyers: " + cpuShips[2].ToString() + " Submarines: " + cpuShips[3].ToString();
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
                    CPU.changeNumShips(CPU, ship);
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
            int[] plrShips = player.getNumShips();
            int[] cpuShips = CPU.getNumShips();
            if (cpuShips[0] == 0 && cpuShips[1] == 0 && cpuShips[2] == 0 && cpuShips[3] == 0)
            {
                guessFeedback.Text = "You Win!";
                changeButtonEnable(plrButtons, false);
                changeButtonEnable(cpuButtons, false);
            }
            else if (plrShips[0] == 0 && plrShips[1] == 0 && plrShips[2] == 0 && plrShips[3] == 0)
            {
                guessFeedback.Text = "You Lose.";
                changeButtonEnable(plrButtons, false);
                changeButtonEnable(cpuButtons, false);
            }

        }
        //resets game board
        private void resetGame()
        {
            initBattleship();
            guessFeedback.Text = "";
            removeButtonText(plrButtons, cpuButtons);
            changeButtonEnable(plrButtons, true);
            changeButtonBackColor(plrButtons, Color.DeepSkyBlue);
            changeButtonBackColor(cpuButtons, Color.DeepSkyBlue);
            startButton.Visible = false;
            autoPlaceShips.Visible = true;
            shipSelection.Visible = true;
            userShipInfo.Text = "Aircraft Carriers: 0 Battleships: 0\nDestroyers: 0 Submarines: 0";
            cpuShipInfo.Text = "Aircraft Carriers: 0 Battleships: 0\nDestroyers: 0 Submarines: 0";
        }
        /* BUTTON FUNCTIONS */
        #region button functions
        private void button4_Click(object sender, EventArgs e)
        {
            resetGame();
        }
        //automatically places player ships
        private void autoPlaceShips_Click(object sender, EventArgs e)
        {
            int[] numShips = player.getNumShips();
            if (numShips[0] >0 || numShips[1] > 0 || numShips[2] > 0 || numShips[3] > 0)
            {
                playerFeedback.Text = "You have already placed some ships. Please place rest or reset";
                autoPlaceShips.Visible = false;
            }
            else
            {
                player.placeShips(normalRadio.Checked);
                //clears all text on screen
                removeButtonText(plrButtons, cpuButtons);
                startButton.Visible = true;
                autoPlaceShips.Visible = false;
                shipSelection.Visible = false;
                updateBoard(true);
                changeButtonEnable(plrButtons, false);
            }
        }
        //places CPU ships, starts game
        private void startButton_Click(object sender, EventArgs e)
        {
            autoPlaceShips.Visible = false;
            startButton.Visible = false;
            playerFeedback.Text = "";
            CPU.placeShips(true);
            changeButtonEnable(cpuButtons, true);
            changeButtonEnable(plrButtons, false);
            //if the mode is submarine mode, set all other ships to 'sunk'
            if (normalRadio.Checked == false)
            {
                int[] numShips = player.getNumShips();
                numShips[0] = 0;
                numShips[1] = 0;
                numShips[2] = 0;
                player.setNumShips(numShips);
            }
            updateBoard(false);
        }
        #endregion
        //activates cheat mode (shhh dont tell anyone)
        private void label3_Click(object sender, EventArgs e)
        {
            //shows location of CPU ships on board
            Ship[,] fleet = CPU.getFleet();
            for (int i = 0; i < dimention; i++)
            {
                for (int j = 0; j < dimention; j++)
                {
                    if (fleet[i,j].getName() != "None")
                    {
                        cpuButtons[i, j].Text = fleet[i, j].getLetter();
                    }
                }
            }
        }
        /* Mode Radio button changes */
        private void normalRadio_CheckedChanged(object sender, EventArgs e) { if (normalRadio.Checked == true) { submarineRadio.Checked = false; } else { submarineRadio.Checked = true; } resetGame(); }

        private void submarineRadio_CheckedChanged(object sender, EventArgs e) { if (submarineRadio.Checked == true) { normalRadio.Checked = false; } else {  normalRadio.Checked = true; } resetGame(); }
    }
    /* BattleshipPlayer class. Represents the players, holds board data */
    public class BattleshipPlayer
    {
        private String[,] board = new String[9,9];
        Ship[,] fleet = new Ship[9, 9];
        //keeps track of previous guess, 0&1: X,y coords of first guess, 2: direction type, 3&4: current guess coordinates, 5: original guess direction
        private int[] guesses = { -1, -1, -1, -1, -1, -1, -1 };
        //0: Aircraft carriers, 1: battleships, 2: destroyers, 3: submarines
        private int[] numShips = { 0, 0, 0, 0 };
        private int sunkShips = 0;
        private int guessStatus = 0;
        public BattleshipPlayer(bool normalMode)
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
            //if it is submarine mode, acts like all other ships are on board
            if (!normalMode)
            {
                numShips[0] = 1;
                numShips[1] = 2;
                numShips[2] = 2;
            }
        }
        //Getters and setters
        public String[,] getBoard() { return board; }
        public void setBoard(String[,] board) { this.board = board; }
        public Ship[,] getFleet() { return fleet; }
        public void setFleet(Ship[,] ships) {  this.fleet = ships; }
        public int[] getNumShips() { return numShips; }
        public void setNumShips(int[] numShips) { this.numShips =  numShips; }
        public int getSunkShips() { return sunkShips; }
        public void setSunkShips(int sunkShips) { this.sunkShips = sunkShips; }
        /* AI places ships onto board */
        #region ship placement
        //Mode: true, normal mode; false, single submarine mode
        public void placeShips(bool mode)
        {
            int x = 0;
            int y = 0;
            int type;
            int count = 0;
            Random rnd = new Random();
            //places single submarine for submarine only mode (player only)
            if (!mode)
            {
                x = rnd.Next(0, 8);
                y = rnd.Next(0, 8);
                if (checkForShip(x, y, 1, 1))
                {
                    addShipNew(x, y, 1, 1);
                }
                return;
            }
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
                    type = rnd.Next(1, 9);
                    switch (type)
                    {
                        case 1:
                            x = rnd.Next(3, 9);
                            y = rnd.Next(3, 9);
                            break;
                        case 2:
                            x = rnd.Next(0, 9);
                            y = rnd.Next(3, 9); 
                            break;
                        case 3:
                            x = rnd.Next(0, 6);
                            y = rnd.Next(3, 9);    
                            break;
                        case 4:
                            x = rnd.Next(3, 9);
                            y = rnd.Next(0, 9);
                            break;
                        case 5:
                            x = rnd.Next(0, 6);
                            y = rnd.Next(0, 9);
                            break;
                        case 6:
                            x = rnd.Next(3, 9);
                            y = rnd.Next(0, 6);
                            break;
                        case 7:
                            x = rnd.Next(0, 9);
                            y = rnd.Next(0, 6);
                            break;
                        case 8:
                            x = rnd.Next(0, 6);
                            y = rnd.Next(0, 6);
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
        #endregion
        #region guess algorithm
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
                //Checks to see if there is an unsunk, but hit ship on the board
                case -1:
                    //looks through board, if a unsunk ship is found changes to find direction and makes guess
                    for (int i = 0; i < usrBoard.GetLength(0); i++)
                    {
                        for (int j = 0; j < usrBoard.GetLength(1); j++)
                        {
                            if (usrBoard[i,j] == "H" && usrFleet[i,j].isSunk() == false)
                            {
                                guessStatus = 1;
                                guesses[0] = i;
                                guesses[1] = j;
                            }
                        }
                    }
                    //if one is not found, changes to random and makes guess
                    if (guessStatus != 1)
                    {
                        guessStatus = 0;
                    }
                    return newGuess(user);
                //finding random coordinate, as no previous guess was correct
                case 0:
                    bool find = true;
                    //runs until valid coordinates found
                    while (find)
                    {
                        x = rnd.Next(0, 9);
                        y = rnd.Next(0, 9);
                        if (usrBoard[x, y] != "H" && usrBoard[x, y] != "O")
                        {
                            find = false;
                        }
                    }
                    //the ship was hit, change guessStatus to 1 to find direction and register as a hit
                    if (usrFleet[x, y].getName() != "None")
                    {
                        usrBoard[x, y] = "H";
                        usrFleet[x, y].hit();
                        //0 & 1: X,y coords of first guess, 2: direction type, 3 & 4: current guess coordinates
                        guesses[0] = x;
                        guesses[1] = y;
                        //checks to see if the ship was sunk
                        if (usrFleet[x, y].isSunk() == true)
                        {
                            changeNumShips(user, usrFleet[x, y]);
                            guessStatus = -1;
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
                        guessStatus = 0;
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
                            guesses[5] = results[0];
                            guesses[3] = x;
                            guesses[4] = y;
                            usrFleet[x, y].hit();
                            //checks to see if the ship was sunk
                            if (usrFleet[x, y].isSunk() == true)
                            {
                                changeNumShips(user, usrFleet[x, y]);
                                guessStatus = -1;
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
                                    changeNumShips(user, usrFleet[x,y]);
                                    guessStatus = -1;
                                }
                                return true;
                            }
                            else
                            {
                                //miss, flip direction and return false
                                usrBoard[x, y] = "O";
                                guesses[2] = flipDirection(guesses[2]);
                                guesses[3] = guesses[0];
                                guesses[4] = guesses[1];
                                return false;
                            }
                        }
                        else
                        {
                            if (guesses[5] == guesses[2])
                            {
                                //both directions tried, return to random guessing
                                guessStatus = -1;
                                return newGuess(user);
                            }
                            else
                            {
                                //jumps over current coordinate guess and checks again
                                newCoords = getCoords(guesses[2], x, y);
                                if (newCoords[0] != -1)
                                {
                                    guesses[3] = newCoords[0];
                                    guesses[4] += newCoords[1];                                     
                                }
                                else
                                {
                                    //check to see if there is a unsunk ship that was found
                                    guessStatus = -1;
                                }
                                return newGuess(user);
                            }
                        }
                    }
                    else
                    {
                        //flips direction and trys again
                        guesses[2] = flipDirection(guesses[2]);
                        newCoords = getCoords(guesses[2], guesses[0], guesses[1]);
                        if (newCoords[0] != -1)
                        {
                            x = newCoords[0];
                            y = newCoords[1];
                            if (usrBoard[x, y] != "H" && usrBoard[x, y] != "O")
                            {
                                if (usrFleet[x, y].getName() != "None")
                                {
                                    //ship was hit, continue on current path
                                    usrBoard[x, y] = "H";
                                    guesses[3] = x;
                                    guesses[4] = y;
                                    usrFleet[x, y].hit();
                                    //check if ship was sunk
                                    if (usrFleet[x, y].isSunk() == true)
                                    {
                                        changeNumShips(user, usrFleet[x, y]);
                                        guessStatus = 0;
                                    }
                                    return true;
                                }
                                else
                                {
                                    //miss, flip direction and return false
                                    guesses[2] = flipDirection(guesses[2]);
                                    usrBoard[x,y] = "O";
                                    return false;
                                }
                            }
                            else
                            {
                                if (guesses[5] == guesses[2])
                                {
                                    //both directions tried, return to random guessing
                                    guessStatus = -1;
                                    return newGuess(user);
                                }
                                else
                                {
                                    //jumps over current coordinate guess and checks again
                                    newCoords = getCoords(guesses[2], x, y);
                                    if (newCoords[0] != -1)
                                    {
                                        guesses[3] = newCoords[0];
                                        guesses[4] += newCoords[1];
                                    }
                                    else
                                    {
                                        //check to see if there is a unsunk ship that was found
                                        guessStatus = -1;
                                    }
                                    return newGuess(user);
                                }
                            }
                        }
                        else
                        {
                            //resets status if both directions are out of bounds
                            guessStatus = 0;
                            return newGuess(user);
                        }
                    }
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
            if (x >= 0 && x < 9 && y >= 0 && y < 9)
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
        private int[] getDirectionCoords(String[,] usrBoard, int initx, int inity)
        {
            int[] directions = { 2, 7, 4, 5, 1, 3, 6, 8 };
            //0: direction number, 1: x coord, 2: y coord
            int[] ret = { 0,0,0 };
            int x, y;
            for (int i = 0; i < directions.Length; i++)
            {
                x = initx;
                y = inity;
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
                if (x >= 0 && x < 9 && y >= 0 && y < 9)
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
        //updates the number of ships that have not been sunk yet for a given user
        public void changeNumShips(BattleshipPlayer user, Ship ship)
        {
            int[] usrNumShips = user.getNumShips();
            String letter = ship.getLetter();
            switch (letter)
            {
                case "A":
                    usrNumShips[0]--; break;
                case "B":
                    usrNumShips[1]--; break;
                case "D":
                    usrNumShips[2]--; break;
                case "S":
                    usrNumShips[3]--; break;
            }
            user.setNumShips(usrNumShips);
        }
        #endregion
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
