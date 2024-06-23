using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesForClass
{
    public partial class sudoku : Form
    {
        private int size = 9;
        private int sqrtSize = 3;
        private int[,] puzzle;
        private int[,] userPuzzle;
        private String[,] userNotes;
        private CheckBox[,] buttons;
        private Label[,] labels;
        private CheckBox[] numberButtons;
        private int difficulty = 0;
        private bool hintActive = false;
        private int emptyValues = 81;

        private RadioButton fill;
        private RadioButton note;
        private RadioButton remove;

        private CheckBox boardHold = null;
        private CheckBox valueHold = null;
        public sudoku()
        {
            InitializeComponent();
        }
        #region board
        //creates board buttons and lables
        //287
        private void genBoard()
        {
            int buttonSize = 50;
            int offset = 2;
            int startx = 75;
            int starty = 75;
            int x = startx;
            int y = starty;
            buttons = new CheckBox[size, size];
            labels = new Label[size, size];

            for (int i = 0; i < size; i++)
            {
                if (i % 3 == 0 && i != 0) y += 4;
                for (int j = 0; j < size; j++)
                {
                    if (j == 0) x = startx; else x += buttonSize + offset;
                    if (j % 3 == 0 && j != 0) x += 4;
                    
                    CheckBox button = new CheckBox();
                    button.Appearance = Appearance.Button;
                    button.Name = i.ToString() + j.ToString();
                    button.Size = new Size(buttonSize, buttonSize);
                    button.Location = new Point(x, y);
                    button.Font = new Font("Microsoft Sans Sarif", 30);
                    button.TextAlign = ContentAlignment.MiddleCenter;
                    button.CheckedChanged += boardButtonClick;
                    button.BackColor = Color.LightGray;
                    this.Controls.Add(button);
                    button.BringToFront();
                    buttons[i, j] = button;

                    //lables for notes
                    Label label = new Label();
                    label.Size = new Size(buttonSize, 24);
                    label.Location = new Point(x, y);
                    label.Font = new Font("Microsoft Sans Sarif", 10);
                    label.TextAlign = ContentAlignment.MiddleRight;
                    label.BackColor = Color.Transparent;
                    this.Controls.Add(label);
                    labels[i,j] = label;
                    
                }
                y += buttonSize + offset;
            }
        }
        //creates the value buttons to place a value into the board
        private void genValueButtons()
        {
            int buttonSize = 50;
            int offset = 2;
            int startx = 580;
            int starty = 183;
            int x = startx;
            int y = starty;
            numberButtons = new CheckBox[9];

            //fill button
            fill = new RadioButton();
            fill.Appearance = Appearance.Button;
            fill.Location = new Point(startx, starty);
            fill.Size = new Size(buttonSize + offset + (buttonSize / 2), buttonSize);
            fill.Text = "Fill";
            fill.Font = new Font("Microsoft Sans Sarif", 15);
            fill.TextAlign = ContentAlignment.MiddleCenter;
            fill.CheckedChanged += setFillNumber;
            this.Controls.Add(fill);
            fill.BringToFront();

            //note button
            note = new RadioButton();
            note.Appearance = Appearance.Button;
            note.Location = new Point(startx + buttonSize + offset + (buttonSize / 2), starty);
            note.Size = new Size(buttonSize + offset + (buttonSize / 2), buttonSize);
            note.Text = "Note";
            note.Font = new Font("Microsoft Sans Sarif", 15);
            note.TextAlign = ContentAlignment.MiddleCenter;
            note.CheckedChanged += setNoteNumber;
            this.Controls.Add(note);
            note.BringToFront();

            //Places number buttons
            for (int i = 0; i < size; i++)
            {
                if (i % 3 == 0) { x = startx; y += buttonSize + offset; } else x += buttonSize + offset;

                CheckBox button = new CheckBox();
                button.Appearance = Appearance.Button;
                button.Location = new Point(x, y);
                button.Size = new Size(buttonSize, buttonSize);
                button.Font = new Font("Microsoft Sans Sarif", 30);
                button.TextAlign = ContentAlignment.MiddleCenter;
                button.Text = (i + 1).ToString();
                button.CheckedChanged += numberButtonClick;
                this.Controls.Add(button);
                button.BringToFront();
                numberButtons[i] = button;

            }

            //remove button
            x = startx;
            y += buttonSize + offset;
            remove = new RadioButton();
            remove.Appearance = Appearance.Button;
            remove.Location = new Point(x, y);
            remove.Size = new Size((buttonSize + offset) * 3, buttonSize);
            remove.Text = "Remove";
            remove.Font = new Font("Microsoft Sans Sarif", 15);
            remove.TextAlign = ContentAlignment.MiddleCenter;
            remove.CheckedChanged += setRemoveNumber;
            this.Controls.Add(remove);
            remove.BringToFront();

            fill.Checked = true;
        }
        //function for when the board button is clicked
        private void boardButtonClick(object sender, EventArgs e)
        {
            CheckBox button = (CheckBox)sender;
            //checks if the value needs to be removed first
            if (remove.Checked && button.Text != "")
            {
                removeValue(button);
                clearColor();
                return;
            }
            //checks to see if there is a board button in hold already, switches if so
            if (boardHold != null)
            {
                boardHold.Checked = false;
                boardHold = button;
            }
            
            if (valueHold != null)
            {
                //places value onto board
                placeValue(Convert.ToInt32(valueHold.Text), button);
                if (emptyValues == 0)
                {
                    win();
                }
            }
            else
            {
                boardHold = button;
            }
        }
        //function for when a number button is clicked
        private void numberButtonClick(object sender, EventArgs e)
        {
            CheckBox button = (CheckBox)sender;
            int value = Convert.ToInt32(button.Text);
            //removes other checks from the other numbers
            removeNumberChecks(value - 1);

            if (boardHold != null)
            {
                //places value onto board
                placeValue(value, boardHold);
                boardHold = null;
                if (emptyValues == 0)
                {
                    win();
                }
            }
            else
            {
                //places number into hold, or removes from hold
                if (button.Checked) valueHold = button; else valueHold = null;
            }
        }
        //sets number to be placed as a final value
        private void setFillNumber(object sender, EventArgs e)
        {
            if (fill.Checked) { note.Checked = false; remove.Checked = false; }
        }
        //sets number to be placed as note
        private void setNoteNumber(object sender, EventArgs e)
        {
            if (note.Checked) { fill.Checked = false; remove.Checked = false; }
        }
        //sets to remove a number from the board
        private void setRemoveNumber(object sender, EventArgs e)
        {
            if (remove.Checked) { fill.Checked = false; note.Checked = false; }

            if (valueHold != null)
            {
                valueHold.Checked = false;
                valueHold = null;
            }
        }

        #region board update functions
        //updates the board to the current user board, resets all coloring if needed
        private void startupBoard()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (userPuzzle[i, j] != 0)
                    {
                        buttons[i, j].Text = userPuzzle[i, j].ToString();
                    }
                    else
                    {
                        buttons[i, j].Text = "";
                    }
                    //test.Text += puzzle[i, j].ToString() + " ";
                }
                //test.Text += "\n";
            }
        }
        //clears all colors from the board, resets to default. Ignores index at ignore
        private void clearColor()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (buttons[i,j].Enabled)
                    {
                        buttons[i, j].BackColor = Color.LightGray;
                    }
                }
            }
        }

        //unchecks number buttons except for ignoe
        private void removeNumberChecks(int ignore)
        {
            for (int i = 0; i < numberButtons.Length; i++)
            {
                if (i != ignore)
                {
                    numberButtons[i].Checked = false;
                }
            }
        }
        #endregion
        //places a value either into the notes section, or onto the board
        private void placeValue(int value, CheckBox button)
        {
            char[] name = button.Name.ToCharArray();

            int x = name[0] - '0';
            int y = name[1] - '0';

            if (hintActive) { clearColor(); hintActive = false; }

            if (fill.Checked)
            {
                //fill number into main board
                if (userPuzzle[x,y] == 0) emptyValues--;
                userPuzzle[x,y] = value;
                button.Text = value.ToString();
                if (userNotes[x,y] != "")
                {
                    labels[x, y].Text = "";
                    labels[x,y].SendToBack();
                    userNotes[x, y] = "";
                }
            }
            else
            {
                //fill number into notes
                String currentNote = userNotes[x,y];
                
                if (currentNote == "")
                {
                    currentNote = value.ToString();
                }
                else
                {
                    currentNote += ", " + value.ToString();
                }

                userNotes[x,y] = currentNote;
                labels[x, y].Text = currentNote;
                labels[x,y].BringToFront();
            }
        }

        //removes a value from the board, or removes notes
        private void removeValue(CheckBox button)
        {
            char[] name = button.Name.ToCharArray();

            int x = name[0] - '0';
            int y = name[1] - '0';
            
            if (userPuzzle[x,y] != 0)
            {
                //removes value from the board
                userPuzzle[x, y] = 0;
                buttons[x, y].Text = "";
                emptyValues++;
            }
            else
            {
                //removes all notes
                userNotes[x, y] = "";
                labels[x, y].Text = "";
                labels[x, y].SendToBack();
            }

        }

        //checks if the user has won
        private void win()
        {
            if (checkCorrectness())
            {
                feedback.Text = "You win!";
                changeButtonEnable(false, buttons);
            }
        }

        //turns all incorrect values red, sees if the user won
        private bool checkCorrectness()
        {
            bool win = true;
            hintActive = true;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (userPuzzle[i, j] != 0 && userPuzzle[i, j] != puzzle[i, j])
                    {
                        buttons[i, j].BackColor = Color.Red;
                        win = false;
                    }
                }
            }
            if (win) hintActive = false;
            return win;
        }

        //gets user input for board from keyboard
        private void keyboardInput(Object sender, KeyEventArgs e)
        {
            String word = e.KeyCode.ToString();
            char[] entry = e.KeyCode.ToString().ToCharArray();

            if (Char.IsDigit(entry[entry.Length - 1]) && (fill.Checked || note.Checked) && boardHold != null)
            {
                int value = entry[entry.Length - 1] - '0';
                if (value != 0)
                {
                    placeValue(value, boardHold);
                    if (emptyValues == 0)
                    {
                        win();
                    }
                }
            }
            else if (word == "Back" && boardHold != null)
            {
                removeValue(boardHold);
            }
        }
        #endregion
        #region puzzle generation
        /*
         * Puzzle setup functions
         */
        private void initSudoku()
        {
            puzzle = new int[size, size];
            userPuzzle = new int[size, size];
            userNotes = new string[size, size];
            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    puzzle[i, j] = 0;
                    userPuzzle[i, j] = 0;
                    userNotes[i, j] = "";
                }
            }
            genPuzzle();
            showValues();
        }
        //creates a new puzzle
        private void genPuzzle()
        {
            Random rand = new Random();
            int val;
            //fills in the diagonal squares
            for (int i = 0; i < 3; i++)
            {
                List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                int x, y;
                int count = 0;
                int row = 0;
                while (numbers.Count > 0)
                {
                    //finds random value, calculates matrix position
                    val = rand.Next(0, numbers.Count);
                    x = (3 * i) + row;
                    y = (3 * i) + (count % 3);
                    
                    //adds value to matrix, removes it from chance
                    puzzle[x, y] = numbers[val];
                    numbers.RemoveAt(val);
                    
                    count++;
                    if (count % 3 == 0)
                    {
                        row++;
                    }
                }
            }

            int startx = 0;
            int starty = 3;
            //starts recursion for the remaining resources
            fillRemaining(startx, starty);
        }
        //fills the reminaing spaces in the puzzle
        private bool fillRemaining(int i, int j)
        {
            //Adapted from https://www.geeksforgeeks.org/program-sudoku-generator/
            if (j >= size && i < size - 1)
            {
                i = i + 1;
                j = 0;
            }
            if (i >= size && j >= size)
                return true;

            if (i < sqrtSize)
            {
                if (j < sqrtSize)
                    j = sqrtSize;
            }
            else if (i < size - sqrtSize)
            {
                if (j == (int)(i / sqrtSize) * sqrtSize)
                    j = j + sqrtSize;
            }
            else
            {
                if (j == size - sqrtSize)
                {
                    i = i + 1;
                    j = 0;
                    if (i >= size)
                        return true;
                }
            }

            for (int num = 1; num <= size; num++)
            {
                if (checkRules(num, i, j))
                {
                    puzzle[i, j] = num;
                    if (fillRemaining(i, j + 1))
                        return true;

                    puzzle[i, j] = 0;
                }
            }
            return false;
        }
        //checks if a current value can go in a place based on sudoku rules
        private bool checkRules(int value, int x, int y)
        {
            //checks the row and column for the same value already exisiting, if so returns false
            for (int i = 0; i < 9; i++)
            {
                if (puzzle[x, i] == value || puzzle[i, y] == value)
                {
                    return false;
                }
            }
            //none hit check, returns true
            return checkSquare(value, x - x % sqrtSize, y - y % sqrtSize);
        }
        //checks the current box to see if the number already exits
        private bool checkSquare(int num, int rowStart, int colStart)
        {
            for (int i = 0; i < sqrtSize; i++)
            {
                for (int j = 0; j < sqrtSize; j++)
                {
                    if (puzzle[rowStart + i, colStart + j] == num)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //hides values based on the difficulty level selected
        //0:Easy, 1: Medium, 2: Hard
        public void showValues()
        {
            int hide = 0;
            Random rnd = new Random();
            int x, y;
            //selects the number of values to be shown to the user
            switch (difficulty)
            {
                case 0:
                    hide = 20;
                    break;
                case 1:
                    hide = 15;
                    break;
                case 2:
                    hide = 12;
                    break;
            }
            int index = 0;
            while (index < hide)
            {
                x = rnd.Next(0, size);
                y = rnd.Next(0, size);

                if (userPuzzle[x,y] == 0)
                {
                    userPuzzle[x, y] = puzzle[x, y];
                    buttons[x, y].BackColor = Color.Gray;
                    buttons[x,y].Enabled = false;
                    index++;
                }
            }
            emptyValues -= hide;
        }
        #endregion
        #region button functions
        private void startReset_Click(object sender, EventArgs e)
        {
            if (startReset.Text == "Start")
            {
                startReset.Text = "Reset";
                checkButton.Visible = true;
                this.KeyPreview = true;
                this.KeyUp += new KeyEventHandler(keyboardInput);
                genBoard();
                genValueButtons();
            }
            else
            {
                changeButtonEnable(true, buttons);
                changeButtonColor(Color.LightGray, buttons);
            }
            initSudoku();
            startupBoard();
        }
        private void easyRadio_CheckedChanged(object sender, EventArgs e) { if (easyRadio.Checked) { hardRadio.Checked = false; mediumRadio.Checked = false; difficulty = 0; } }
        private void mediumRadio_CheckedChanged(object sender, EventArgs e) { if (mediumRadio.Checked) { easyRadio.Checked = false; hardRadio.Checked = false; difficulty = 1; } }
        private void hardRadio_CheckedChanged(object sender, EventArgs e) { if (hardRadio.Checked) { easyRadio.Checked = false; mediumRadio.Checked = false; difficulty = 2; } }
        //checks for correctness between user board and correct answers, changes backgrounds to red if incorrect
        private void checkButton_Click(object sender, EventArgs e)
        {
            checkCorrectness();
        }

        #region button change
        private void changeButtonEnable(bool enable, CheckBox[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j].Enabled = enable;
                }
            }
        }

        private void changeButtonColor(Color color, CheckBox[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j].BackColor = color;
                }
            }
        }

        #endregion
        private void title_Click(object sender, EventArgs e)
        {
            
            for (int i =0; i < size; i++)
            {
                for (int j = 0;j < size; j++)
                {
                    if (userPuzzle[i,j] == 0)
                    {
                        placeValue(puzzle[i,j], buttons[i,j]);
                        if (emptyValues == 0)
                        {
                            win();
                        }
                        return;
                    }
                }
            }
        }
        #endregion

    }
}
