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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

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
        //updates the board after moves are made
        public void updateBoard()
        {
            removeButtonColor(buttonDefaultColor);
            for (int i = 0; i < buttons.Length; i++)
            {
                int[] coords = getCoords(buttons[i]);
                Checker checker = findCheckerAtCoordinates(coords[0], coords[1]);
                if (checker != null)
                {
                    if (checker.getHealth() > 0)
                    {
                        buttons[i].Text = checker.getText();
                        buttons[i].ForeColor = checker.getColor();
                    }
                    else
                    {
                        buttons[i].Text = "";
                    }
                }
                else
                {
                    buttons[i].Text = "";
                }
            }
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
            for (int i = 0; i < 12; i++)
            {
                Button button = buttons[i];
                int[] location = getCoords(button);
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
                int[] location = getCoords(button);
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
        //Finds all possible moves for given checker
        //references column, row
        private bool findMoves(Button button)
        {
            bool option = false;
            int[] coords = getCoords(button);
            int x = coords[0];
            int y = coords[1];

            Checker checker = player.findCheckerAtCoordinates(x, y);
            //makes sure checker is not null
            if (checker != null)
            {
                //If the checker is not a king
                if (checker.getText() == "O")
                {
                    //check directions 1 & 2
                    if (checkBounds(x - 1, y - 1))
                    {
                        if (checkDirection(1, x - 1, y - 1))
                        {
                            option = true;
                        }
                    }
                    if (checkBounds(x + 1, y - 1))
                    {
                        if (checkDirection(2, x + 1, y - 1))
                        {
                            option = true;
                        }
                    }
                }
                else
                {
                    //check all directions
                    if (checkBounds(x - 1, y - 1))
                    {
                        if (checkDirection(1, x - 1, y - 1))
                        {
                            option = true;
                        }
                    }
                    if (checkBounds(x + 1, y - 1))
                    {
                        if (checkDirection(2, x + 1, y - 1))
                        {
                            option = true;
                        }
                    }
                    if (checkBounds(x - 1, y + 1))
                    {
                        if (checkDirection(3, x - 1, y + 1))
                        {
                            option = true;
                        }
                    }
                    if (checkBounds(x + 1, y + 1))
                    {
                        if (checkDirection(4, x + 1, y + 1))
                        {
                            option = true;
                        }
                    }
                }
            }
            else
            {
                //checking for health
                Checker testchecker = findCheckerAtCoordinates(x, y);

                if (testchecker != null) { test.Text = testchecker.getHealth().ToString(); } else { test.Text = x.ToString() + y.ToString(); }
            }
            return option;
        }
        //finds another jump specifically
        private bool findJump(Checker checker, int[] coordinates)
        {
            bool jump = false;
            int x = coordinates[0];
            int y = coordinates[1];
            Checker checkChecker;
            Button button;
            //If the checker is not a king
            if (checker.getText() == "O")
            {
                //check directions 1 & 2
                if (checkBounds(x - 2, y - 2))
                {
                    //grabs checker inbetween
                    checkChecker = findCheckerAtCoordinates(x - 1, y - 1);
                    //makes sure the checker exists and the colors are different
                    if (checkChecker != null && checkChecker.getColor() != checker.getColor())
                    {
                        //makes sure the jump space does not have a checker in it
                        if (findCheckerAtCoordinates(x-2, y-2) == null)
                        {
                            button = findButtonWithCoordinates(x - 2, y - 2);
                            button.BackColor = Color.Yellow;
                            jump = true;
                        }
                    }
                }
                if (checkBounds(x + 2, y - 2))
                {
                    //grabs checker inbetween
                    checkChecker = findCheckerAtCoordinates(x + 1, y - 1);
                    //makes sure the checker exists and the colors are different
                    if (checkChecker != null && checkChecker.getColor() != checker.getColor())
                    {
                        //makes sure the jump space does not have a checker in it
                        if (findCheckerAtCoordinates(x + 2, y - 2) == null)
                        {
                            button = findButtonWithCoordinates(x + 2, y - 2);
                            button.BackColor = Color.Yellow;
                            jump = true;
                        }
                    }
                }
            }
            else
            {
                //check all directions
                if (checkBounds(x - 2, y - 2))
                {
                    //grabs checker inbetween
                    checkChecker = findCheckerAtCoordinates(x - 1, y - 1);
                    //makes sure the checker exists and the colors are different
                    if (checkChecker != null && checkChecker.getColor() != checker.getColor())
                    {
                        //makes sure the jump space does not have a checker in it
                        if (findCheckerAtCoordinates(x - 2, y - 2) == null)
                        {
                            button = findButtonWithCoordinates(x - 2, y - 2);
                            button.BackColor = Color.Yellow;
                            jump = true;
                        }
                    }
                }
                if (checkBounds(x + 2, y - 2))
                {
                    //grabs checker inbetween
                    checkChecker = findCheckerAtCoordinates(x + 1, y - 1);
                    //makes sure the checker exists and the colors are different
                    if (checkChecker != null && checkChecker.getColor() != checker.getColor())
                    {
                        //makes sure the jump space does not have a checker in it
                        if (findCheckerAtCoordinates(x + 2, y - 2) == null)
                        {
                            button = findButtonWithCoordinates(x + 2, y - 2);
                            button.BackColor = Color.Yellow;
                            jump = true;
                        }
                    }
                }
                if (checkBounds(x - 2, y + 2))
                {
                    //grabs checker inbetween
                    checkChecker = findCheckerAtCoordinates(x - 1, y + 1);
                    //makes sure the checker exists and the colors are different
                    if (checkChecker != null && checkChecker.getColor() != checker.getColor())
                    {
                        //makes sure the jump space does not have a checker in it
                        if (findCheckerAtCoordinates(x - 2, y + 2) == null)
                        {
                            button = findButtonWithCoordinates(x - 2, y + 2);
                            button.BackColor = Color.Yellow;
                            jump = true;
                        }
                    }
                }
                if (checkBounds(x + 2, y + 2))
                {
                    //grabs checker inbetween
                    checkChecker = findCheckerAtCoordinates(x + 1, y + 1);
                    //makes sure the checker exists and the colors are different
                    if (checkChecker != null && checkChecker.getColor() != checker.getColor())
                    {
                        //makes sure the jump space does not have a checker in it
                        if (findCheckerAtCoordinates(x + 2, y + 2) == null)
                        {
                            button = findButtonWithCoordinates(x + 2, y -+2);
                            button.BackColor = Color.Yellow;
                            jump = true;
                        }
                    }
                }
            }
            return jump;
        }
        //checks given coordinates if can jump or not
        /* Directions
         * 1   2
         *   O
         * 3   4
         */
        private bool checkDirection(int direction, int x, int y)
        {
            //look to see if take is possible in this direction
            Checker checker = findCheckerAtCoordinates(x, y);
            if (checker != null)
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
                        x--;
                        y++;
                        break;
                    case 4:
                        x++;
                        y++;
                        break;
                }
                //checks to see if the space would be in bounds, and there is no other piece in the space to make jump, and makes sure piece is enemy
                Checker jumpchecker = findCheckerAtCoordinates(x, y);
                if (checkBounds(x, y) && jumpchecker == null)
                {
                    Button button = findButtonWithCoordinates(x, y);
                    if (button != null && checker.getColor() != Color.Black)
                    {
                        button.BackColor = Color.Yellow;
                        return true;
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
                    button.BackColor = Color.Yellow;
                }
                else
                {
                    test.Text = "Button not found";
                }
            }
            return false;
        }
        //Makes move to specified button
        private void makeMove(Button button)
        {
            //finds current and hold button's coordinates
            int[] currentCoords = getCoords(button);
            int[] holdCoords = getCoords(holdButton);

            //finds given piece to move
            Checker checker = findCheckerAtCoordinates(holdCoords[0], holdCoords[1]);
            //move checker to location and updates board
            if (checker != null)
            {
                holdButton.Text = "";   
                removeButtonColor(Color.Yellow);
                checker.setLocation(currentCoords);
                //makes checker a king if it made to the top
                if (currentCoords[1] == 0)
                {
                    checker.makeKing();
                }
                //checks to see if a jump was made
                if (currentCoords[0] > holdCoords[0] + 1 || currentCoords[0] < holdCoords[0] - 1)
                {
                    //takes piece that was jumped
                    takePiece(currentCoords, holdCoords);
                    //jump was made, stage to find another option
                    holdButton = button;
                    //if there are no moves found, CPU turn
                    if (!findJump(checker, currentCoords))
                    {
                        CPU.makeMove(this);
                    }                    
                }
                else
                {
                    //makes CPU move
                    CPU.makeMove(this);
                }
                updateBoard();
            }
        }
        //takes piece on board
        private void takePiece(int[] currentCoords, int[] holdCoords)
        {
            int[] removeCoords = new int[2];
            //finds direction moved
            if (currentCoords[0] < holdCoords[0] - 1)
            {
                if (currentCoords[1] < holdCoords[1] -1)
                {
                    //direction 1
                    removeCoords[0] = currentCoords[0] + 1;
                    removeCoords[1] = currentCoords[1] + 1;
                }
                else
                {
                    //direction 3
                    removeCoords[0] = currentCoords[0] + 1;
                    removeCoords[1] = currentCoords[1] - 1;
                }
            }
            else
            {
                if (currentCoords[1] < holdCoords[1] -1)
                {
                    //direction 2
                    removeCoords[0] = currentCoords[0] - 1;
                    removeCoords[1] = currentCoords[1] + 1;
                }
                else
                {
                    //direction 4
                    removeCoords[0] = currentCoords[0] - 1;
                    removeCoords[1] = currentCoords[1] - 1;
                }
            }
            //removes checker from board
            Checker removeChecker = findCheckerAtCoordinates(removeCoords[0], removeCoords[1]);
            if (removeChecker != null)
            {
                removeChecker.take();
            }
            else
            {
                test.Text = "Checker not found, error";
            }
        }
        #endregion
        #region find functions
        //finds a specific button at given coordinates, returns null if none found
        public Button findButtonWithCoordinates(int x, int y)
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
            for (int i = 0; i < buttons.Length; i++)
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
        //returns the coordinates of a given button
        public int[] getCoords(Button button)
        {
            int[] coords = new int[2];
            String x = "";
            String y = "";
            x += button.Name[0];
            y += button.Name[1];
            coords[0] = Convert.ToInt32(x);
            coords[1] = Convert.ToInt32(y);
            return coords;
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
            else
            {
                //calls make move function
                makeMove(button);
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
        private const int normalCheckerDirections = 2;
        private const int kingCheckerDirections = 4;
        private int dimention = 9;
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
        #region automatic movements
        //makes a movement automatically
        public void makeMove(Checkers instance)
        {
            //checks to see if any checker is in danger, makes move if so
            for (int i = 0; i < checkers.Length; i++)
            {
                //makes sure the checker is playable
                if (checkers[i].getHealth() > 0)
                {
                    if (lookForDanger(checkers[i], instance))
                    {
                        //checker found to be in danger, check to see if you can take first
                        if (attemptTake(checkers[i], instance) == false)
                        {
                            //attempt to move piece out of danger
                            if (attemptMove(checkers[i], instance))
                            {
                                //piece was moved
                                return;
                            }
                        }
                        else
                        {
                            //piece was taken
                            return;
                        }
                    }
                }
            }

            //move 'random' piece if one cannot be taken or moved already
            bool find = true;
            int loopCounter = 0;
            int index = 0;
            while (find)
            {
                //looks to see if the player has a king to move
                if (loopCounter < checkers.Length && checkers[index].getHealth() > 0)
                {
                    if (checkers[index].getText() == "K")
                    {
                        if (attemptMove(checkers[index], instance))
                        {
                            return;
                        }
                    }
                    index++;
                }
                else
                {
                    if (index >= checkers.Length)
                    {
                        index = 0;
                    }
                    //move first piece that can move
                    if (checkers[index].getHealth() > 0 && attemptMove(checkers[index], instance))
                    {
                        return;
                    }
                    index++;
                }
                loopCounter++;
                //stops infinite loop, SHOULD NOT HIT
                if (loopCounter > 100)
                {
                    return;
                }
            }
        }
        //looks for danger around a given checker
        /* Locations
         * 1   2
         *   O
         * 3   4
         */
        private bool lookForDanger(Checker checker, Checkers instance)
        {
            int[] location = checker.getLocation();
            int x = 0;
            int y = 0;
            int x1 = 0;
            int y1 = 0;
            //looks in 4 directions, checks to see if there is danger
            for (int i = 1; i <= normalCheckerDirections; i++)
            {
                switch (i)
                {
                    case 1:
                        x = location[0] - 1;
                        y = location[1] - 1;
                        x1 = location[0] + 1;
                        y1 = location[1] + 1;
                        break;
                    case 2:
                        x = location[0] + 1;
                        y = location[1] - 1;
                        x1 = location[0] - 1;
                        y1 = location[1] + 1;
                        break;
                }
                Checker pc1 = null;
                Checker pc2 = null;
                //checks to see if a given position would be in bounds
                if (instance.checkBounds(x, y))
                {
                    pc1 = instance.findCheckerAtCoordinates(x, y);
                }
                if (instance.checkBounds(x1, y1))
                {
                    pc2 = instance.findCheckerAtCoordinates(x1, y1);
                }
                //checks to see if there are checkers at specified locations
                if (pc1 != null || pc2 != null)
                {
                    //checks if enemy color is in area, if so there is danger
                    if (pc1 != null && pc1.getColor() != checker.getColor())
                    {
                        return true;
                    }
                    if (pc2 != null && pc2.getColor() != checker.getColor())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //function that attempts to make random move on checker, not for attempting to take a piece
        //returns true if move made, false if none made
        private bool attemptMove(Checker checker, Checkers instance)
        {
            int[] coords = checker.getLocation();
            int x, y;
            //standard peice
            if (checker.getText() == "O")
            {
                for (int i = 1; i <=normalCheckerDirections; i++)
                {
                    x = coords[0];
                    y = coords[1];
                    switch (i)
                    {
                        case 1:
                            //black pieces move up, white move down
                            if (checker.getColor() == Color.Black)
                            {
                                //direction 1
                                x--;
                                y--;
                            }
                            else
                            {
                                //direction 3
                                x--;
                                y++;
                            }
                            break;
                        case 2:
                            if (checker.getColor() == Color.Black)
                            {
                                //direction 2
                                x++;
                                y--;
                            }
                            else
                            {
                                //direction 4
                                x++;
                                y++;
                            }
                            break;
                    }
                    //checks coordinate bounds
                    if (instance.checkBounds(x, y))
                    {
                        //checks to see if there is a checker already at this coordinate, preventing move
                        if (instance.findCheckerAtCoordinates(x, y) == null)
                        {
                            //looks to see if the checker needs to be kinged
                            if (checker.getColor() == Color.Black && y == 0)
                            {
                                checker.makeKing();
                            }
                            if (checker.getColor() == Color.White && y == dimention)
                            {
                                checker.makeKing();
                            }
                            //moves checker to this location
                            int[] newLocation = {x, y};
                            checker.setLocation(newLocation);
                            return true;
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i <= kingCheckerDirections; i++)
                {
                    x = coords[0];
                    y = coords[1];
                    switch (i)
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
                            x--;
                            y++;
                            break;
                        case 4:
                            x++;
                            y++;
                            break;
                    }
                    //checks coordinate bounds
                    if (instance.checkBounds(x, y))
                    {
                        //checks to see if there is a checker already at this coordinate, preventing move
                        if (instance.findCheckerAtCoordinates(x, y) == null)
                        {
                            //looks to see if the checker needs to be kinged
                            if (checker.getColor() == Color.Black && y == 0)
                            {
                                checker.makeKing();
                            }
                            if (checker.getColor() == Color.White && y == dimention)
                            {
                                checker.makeKing();
                            }
                            //moves checker to this location
                            int[] newLocation = { x, y };
                            checker.setLocation(newLocation);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        //attempts to take a piece
        //returns true if piece was taken, false if not
        private bool attemptTake(Checker checker, Checkers instance)
        {
            int[] location = checker.getLocation();
            int x = 0;
            int y = 0;
            int openx = 0;
            int openy = 0;

            if (checker.getText() == "O")
            {
                for (int i = 1; i <= normalCheckerDirections; i++)
                {
                    switch (i)
                    {
                        case 1:
                            if (checker.getColor() == Color.Black)
                            {
                                //direction 1
                                x = location[0] - 1;
                                y = location[1] - 1;
                                openx = location[0] - 2;
                                openy = location[1] - 2;
                            }
                            else
                            {
                                //direction 3
                                x = location[0] - 1;
                                y = location[1] + 1;
                                openx = location[0] - 2;
                                openy = location[1] + 2;
                            }
                            break;
                        case 2:
                            if (checker.getColor() == Color.Black)
                            {
                                //direction 2
                                x = location[0] + 1;
                                y = location[1] - 1;
                                openx = location[0] + 2;
                                openy = location[1] - 2;
                            }
                            else
                            {
                                //direction 4
                                x = location[0] + 1;
                                y = location[1] + 1;
                                openx = location[0] + 2;
                                openy = location[1] + 2;
                            }
                            break;
                    }
                    //checks if coordinates are in bounds
                    if (instance.checkBounds(x, y))
                    {
                        Checker checkChecker = instance.findCheckerAtCoordinates(x,y);
                        //makes sure checker exists, and checks if it is enemy color
                        if (checkChecker != null && checkChecker.getColor() != checker.getColor())
                        {
                            //checks if the space behind said checker is open, and in bounds
                            if (instance.checkBounds(openx, openy) && instance.findCheckerAtCoordinates(openx, openy) == null)
                            {
                                //checks if checker needs to be kinged
                                if (checker.getColor() == Color.Black && y == 0)
                                {
                                    checker.makeKing();
                                }
                                if (checker.getColor() == Color.White && y == dimention)
                                {
                                    checker.makeKing();
                                }
                                //sets new location for button
                                int[] newcoords = { openx, openy };
                                checker.setLocation(newcoords);

                                //takes other player's checker
                                checkChecker.take();

                                return true;
                            }
                        }
                    }
                }
            }
            //king piece
            else
            {
                for (int i =0; i <= kingCheckerDirections; i++)
                {
                    //gets directions
                    switch (i)
                    {
                        case 1:
                            x = location[0] - 1;
                            y = location[1] - 1;
                            openx = location[0] - 2;
                            openy = location[1] - 2;
                            break;
                        case 2:
                            x = location[0] + 1;
                            y = location[1] - 1;
                            openx = location[0] + 2;
                            openy = location[1] - 2;
                            break;
                        case 3:
                            x = location[0] - 1;
                            y = location[1] + 1;
                            openx = location[0] - 2;
                            openy = location[1] + 2;
                            break;
                        case 4:
                            x = location[0] + 1;
                            y = location[1] + 1;
                            openx = location[0] + 2;
                            openy = location[1] + 2;
                            break;
                    }
                    //checks if coordinates are in bounds
                    if (instance.checkBounds(x, y))
                    {
                        Checker checkChecker = instance.findCheckerAtCoordinates(x, y);
                        //makes sure checker exists, and checks if it is enemy color
                        if (checkChecker != null && checkChecker.getColor() != checker.getColor())
                        {
                            //checks if the space behind said checker is open, and in bounds
                            if (instance.checkBounds(openx, openy) && instance.findCheckerAtCoordinates(openx, openy) == null)
                            {
                                //checks if checker needs to be kinged
                                if (checker.getColor() == Color.Black && y == 0)
                                {
                                    checker.makeKing();
                                }
                                if (checker.getColor() == Color.White && y == dimention)
                                {
                                    checker.makeKing();
                                }
                                //sets new location for button
                                int[] newcoords = { openx, openy };
                                checker.setLocation(newcoords);

                                //takes other player's checker
                                checkChecker.take();

                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        #endregion
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
        //health is set to 0, location moved off board
        public void take() { health = 0; location[0] = -1; location[1] = -1; }
        public bool isAlive() { return health > 0;}
        public void makeKing() { text = "K"; }
        public void setLocation(int[] location) { this.location = location; }

    }
}
