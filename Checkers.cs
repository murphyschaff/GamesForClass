﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesForClass
{
    public partial class Checkers : Form
    {
        private CheckerPlayer player;
        private CheckerPlayer CPU;
        private Button[] buttons;
        private int dimention = 8;
        private Checker[] checkers;

        private Button holdButton;
        private Color buttonDefaultColor = Color.Gray;
        public Checkers()
        {
            player = new CheckerPlayer();
            CPU = new CheckerPlayer();
            buttons = new Button[32];
            checkers = new Checker[24];
            InitializeComponent();
            createBoard();
        }
        #region board functions
        //creates board
        public void createBoard()
        {      
            int startX = 240;
            int startY = 65;
            int size = 80;
            int counter = 0;
            int x, y;
            for (int i = 0; i < dimention; i++)
            {
                for (int j = 0; j < dimention; j++)
                {
                    x = startX + (j * size);
                    y = startY + (i * size);
                    //when buttons are on odd values
                    if (i % 2 == 0)
                    {
                        //label
                        if (j % 2 == 0)
                        {
                            createLabel(j, i, x, y, size);                           
                        }
                        //button
                        else
                        {
                            Button button = createButton(j, i, x, y, size);
                            buttons[counter] = button;
                            counter++;
                        }
                    }
                    //when buttons are on even values
                    else
                    {
                        //button
                        if (j % 2 == 0)
                        {
                            Button button = createButton(j, i, x, y, size);
                            buttons[counter] = button;
                            counter++;
                        }
                        //label
                        else
                        {
                            createLabel(j, i, x, y, size);                            
                        }
                    }
                }
            }
            boardBackground.SendToBack();
            title.SendToBack();
        }
        //creates button for board
        public Button createButton(int i, int j, int x, int y, int size)
        {
            Button button = new Button();
            button.Name = i.ToString() + j.ToString();
            button.BackColor = buttonDefaultColor;
            button.ForeColor = Color.White;
            button.Location = new Point(x, y);
            button.Font = new Font("Microsoft Sans Sarif", 20);
            button.Size = new Size(size, size);
            button.MouseDown += boardButtonClick;
            button.Enabled = true;
            button.BringToFront();
            this.Controls.Add(button);

            return button;
        }
        //creates label for board
        public void createLabel(int i, int j, int x, int y, int size)
        {
            Label label = new Label();
            label.Name = i.ToString() + j.ToString();
            label.BackColor = Color.White;
            label.Location = new Point(x, y);
            label.Size = new Size(size, size);
            label.Enabled = false;
            label.BringToFront();
            this.Controls.Add(label);         
        }
       
        #endregion
        //function that runs when the button boards are clicked
        //places initial checkers onto board
        public void placePieces()
        {
            Checker[] whiteCheckers = new Checker[12];
            Checker[] blackCheckers = new Checker[12];
            int checkerCounter = 0;
            //places white pieces
            for(int i = 0; i < 12; i++)
            {
                Button button = buttons[i];
                String xVal = "";
                String yVal = "";
                xVal += button.Name[0];
                yVal += button.Name[1];
                int[] location = { Convert.ToInt32(xVal), Convert.ToInt32(yVal) };
                Color color = Color.White;
                Checker checker = new Checker(location, color);

                button.Text = checker.getText();
                button.ForeColor = checker.getColor();
                whiteCheckers[i] = checker;
                checkers[checkerCounter] = checker;
                checkerCounter++;
            }
            int counter = 0;
            //places black checkers
            for (int i = 31; i > 19; i--)
            {
                Button button = buttons[i];
                String xVal = "";
                String yVal = "";
                xVal += button.Name[0];
                yVal += button.Name[1];
                int[] location = { Convert.ToInt32(xVal), Convert.ToInt32(yVal) };
                Color color = Color.Black;
                Checker checker = new Checker(location, color);

                button.Text = checker.getText();
                button.ForeColor = checker.getColor();
                blackCheckers[counter] = checker;
                checkers[checkerCounter] = checker;
                checkerCounter++;
                counter++;
            }
            player.setCheckers(blackCheckers);
            CPU.setCheckers(whiteCheckers);
        }
        #region player movement functions
        //
        //Finds all possible moves for given checker
        //references column, row
        private void findMoves(Button button)
        {
            String xVal = "";
            String yVal = "";
            xVal += button.Name[0];
            yVal += button.Name[1];
            int x = Convert.ToInt32(xVal);
            int y = Convert.ToInt32(yVal);

            Checker checker = player.findCheckerAtCoordinates(x, y);
            //makes sure checker is not null
            if (checker != null)
            {
                //If the checker is not a king
                if (checker.getText() == "O")
                {
                    //check directions 1 & 2
                    if (checkBounds(x-1,y-1))
                    {
                        checkDirection(1, x - 1, y - 1);
                    }
                    if (checkBounds(x+1, y - 1))
                    {
                        checkDirection(2, x + 1, y - 1);
                    }
                }
                else
                {
                    //check all directions
                    if (checkBounds(x - 1, y - 1))
                    {
                        checkDirection(1, x - 1, y - 1);
                    }
                    if (checkBounds(x + 1, y - 1))
                    {
                        checkDirection(2, x + 1, y - 1);
                    }
                    if (checkBounds(x-1, y + 1))
                    {
                        checkDirection(3, x - 1, y + 1);
                    }
                    if (checkBounds(x+1, y + 1))
                    {
                        checkDirection(4, x + 1, y + 1);
                    }
                }
            }
            else
            {
                test.Text = "No Player checker found at location";
            }
        }
        //checks given coordinates if can jump or not
        /* Directions
         * 1   2
         *   O
         * 3   4
         */
        private void checkDirection(int direction, int x, int y)
        {
            //look to see if take is possible in this direction
            if (findCheckerAtCoordinates(x, y) != null)
            {

                switch (direction)
                {
                    case 1:
                        x--;
                        y--;
                        break;
                    case 2:
                        x++;
                        y--;
                        break;
                    case 3:
                        y++;
                        x--;
                        break;
                    case 4:
                        y++;
                        x--;
                        break;
                }
                //checks to see if the space would be in bounds, and there is no other piece in the space to make jump, and makes sure piece is enemy
                Checker checker = findCheckerAtCoordinates(x, y);
                if (checkBounds(x,y) && checker != null)
                {
                    Button button = findButtonWithCoordinates(x, y);
                    if (button != null && checker.getColor() != Color.Black)
                    {
                        button.BackColor = Color.Yellow;
                    }
                    else
                    {
                        test.Text = "jump button not found";
                    }
                }
            }
            //normal move
            else
            {
                Button button = findButtonWithCoordinates(x, y);
                if (button != null)
                {
                    button.BackColor= Color.Yellow;
                }
                else
                {
                    test.Text = "Button not found";
                }
            }
        }
        #endregion
        #region find functions
        //finds a specific button at given coordinates, returns null if none found
        private Button findButtonWithCoordinates(int x, int y)
        {
            Button button = null;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].Name == x.ToString() + y.ToString())
                {
                    button = buttons[i];
                    break;
                }
            }
            return button;
        }
        //finds a checker at given coordinates, returns null if none found
        public Checker findCheckerAtCoordinates(int x, int y)
        {
            Checker checker = null;
            for (int i = 0; i < checkers.Length; i++)
            {
                int[] coords = checkers[i].getLocation();
                //checks coordinates, returns checker with matching coordinates if found
                if (coords[0] == x && coords[1] == y)
                {
                    checker = checkers[i];
                    break;
                }
            }
            return checker;
        }
        #endregion
        #region button change functions
        //removes all background color on buttons for specified color
        private void removeButtonColor(Color color)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].BackColor == color)
                {
                    buttons[i].BackColor = buttonDefaultColor;
                }
            }
        }
        //removes all checkers from board, resets text
        private void resetButtons()
        {
            for (int i =0; i < buttons.Length;i++)
            {
                buttons[i].Text = "";
                buttons[i].BackColor = buttonDefaultColor;
            }
        }
        //switches all enablement of buttons
        private void changeButtonEnable(bool enabled)
        {
            for (int j = 0; j < buttons.Length; j++)
            {
                buttons[j].Enabled = enabled;
            }

        }
        #endregion
        #region extraneous functions
        //checks x, y coordinates to make sure they are in bounds of board
        public bool checkBounds(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < dimention && y < dimention)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region button functions
        /* 
         * Button Functions
         */
        private void startReset_Click(object sender, EventArgs e)
        {
            if (startReset.Text == "Start")
            {
                changeButtonEnable(true);
                placePieces();
                startReset.Text = "Reset";
            }
            else
            {
                resetButtons();
                changeButtonEnable(false);
                startReset.Text = "Start";
            }
        }
        //When a button on the board is clicked, this function runs
        public void boardButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            //Look for possible pieces to click on
            if (button.BackColor != Color.Yellow)
            {
                //checks to see if a button was placed on hold
                if (holdButton == null)
                {
                    holdButton = button;
                    findMoves(button);
                }
                else
                {
                    //clear previous buttons finds, highlight new ones
                    removeButtonColor(Color.Yellow);
                    holdButton = button;
                    findMoves(button);
                }
            }
        }
        #endregion
    }
    /*
     * Class that represents a checker player
     */
    public class CheckerPlayer
    {
        private Checker[] checkers;

        public CheckerPlayer()
        {
            checkers = new Checker[12];
        }
        //getters and setters
        public void setCheckers(Checker[] checkers) { this.checkers = checkers; }
        //Finds a checker at given coordinates, if no checker is at coordinates returns null
        public Checker findCheckerAtCoordinates(int x, int y)
        {
            Checker checker = null;
            for (int i = 0; i < checkers.Length; i++)
            {
                int[] coords = checkers[i].getLocation();
                //checks coordinates, returns checker with matching coordinates if found
                if (coords[0] == x && coords[1] == y)
                {
                    checker = checkers[i];
                    break;
                }
            }
            return checker;
        }
    }
    /*
     * Class that represents an individual checker on the board
     */
    public class Checker
    {
        private String text;
        private int[] location;
        private Color color;
        private int health;

        public Checker(int[] location, Color color)
        {
            text = "O";
            this.location = location;
            this.color = color;
            health = 1;
        }

        //getters and setters
        public String getText() { return text; }
        public int[] getLocation() { return location; }
        public Color getColor() { return color; }
        public int getHealth() { return health; }

        public void take() { health = 0; }
        public bool isAlive() { return health > 0;}
        public void makeKing() { text = "K"; }
        public void setLocation(int[] location) { this.location = location; }

    }
}
