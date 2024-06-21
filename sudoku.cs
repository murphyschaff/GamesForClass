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
        private Button[,] buttons;
        private Label[,] labels;
        private Button[] numberButtons;
        private int difficulty = 0;
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
            buttons = new Button[size, size];
            labels = new Label[size, size];

            for (int i = 0; i < size; i++)
            {
                if (i % 3 == 0 && i != 0) y += 4;
                for (int j = 0; j < size; j++)
                {
                    if (j == 0) x = startx; else x += buttonSize + offset;
                    if (j % 3 == 0 && j != 0) x += 4;
                    
                    Button button = new Button();
                    button.Size = new Size(buttonSize, buttonSize);
                    button.Location = new Point(x, y);
                    button.Font = new Font("Microsoft Sans Sarif", 30);
                    button.TextAlign = ContentAlignment.MiddleCenter;
                    button.MouseDown += boardButtonClick;
                    this.Controls.Add(button);
                    button.BringToFront();
                    buttons[j, i] = button;

                    //lables for notes
                    Label label = new Label();
                    label.Size = new Size(buttonSize, 24);
                    label.Location = new Point(x, y);
                    label.Font = new Font("Microsoft Sans Sarif", 30);
                    label.TextAlign = ContentAlignment.MiddleRight;
                    //this.Controls.Add(label);
                    label.BringToFront();
                    labels[j,i] = label;
                    
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
            numberButtons = new Button[9];

            //fill button
            Button fill = new Button();
            fill.Location = new Point(startx, starty);
            fill.Size = new Size(buttonSize + offset + (buttonSize / 2), buttonSize);
            fill.Text = "Fill";
            fill.Font = new Font("Microsoft Sans Sarif", 15);
            fill.TextAlign = ContentAlignment.MiddleCenter;
            fill.MouseDown += setFillNumber;
            this.Controls.Add(fill);
            fill.BringToFront();

            //note button
            Button note = new Button();
            note.Location = new Point(startx + buttonSize + offset + (buttonSize / 2), starty);
            note.Size = new Size(buttonSize + offset + (buttonSize / 2), buttonSize);
            note.Text = "Note";
            note.Font = new Font("Microsoft Sans Sarif", 15);
            note.TextAlign = ContentAlignment.MiddleCenter;
            note.MouseDown += setNoteNumber;
            this.Controls.Add(note);
            note.BringToFront();

            //Places number buttons
            for (int i = 0; i < size; i++)
            {
                if (i % 3 == 0) { x = startx; y += buttonSize + offset; } else x += buttonSize + offset;

                Button button = new Button();
                button.Location = new Point(x, y);
                button.Size = new Size(buttonSize, buttonSize);
                button.Font = new Font("Microsoft Sans Sarif", 30);
                button.TextAlign = ContentAlignment.MiddleCenter;
                button.Text = (i + 1).ToString();
                button.MouseDown += numberButtonClick;
                this.Controls.Add(button);
                button.BringToFront();
                numberButtons[i] = button;

            }

            //remove button
            x = startx;
            y += buttonSize + offset;
            Button remove = new Button();
            remove.Location = new Point(x, y);
            remove.Size = new Size((buttonSize + offset) * 3, buttonSize);
            remove.Text = "Remove";
            remove.Font = new Font("Microsoft Sans Sarif", 15);
            remove.TextAlign = ContentAlignment.MiddleCenter;
            remove.MouseDown += setRemoveNumber;
            this.Controls.Add(remove);
            remove.BringToFront();
        }
        //function for when the board button is clicked
        private void boardButtonClick(object sender, MouseEventArgs e)
        {

        }
        //function for when a number button is clicked
        private void numberButtonClick(object sender, MouseEventArgs e)
        {

        }
        //sets number to be placed as a final value
        private void setFillNumber(object sender, MouseEventArgs e)
        {

        }
        //sets number to be placed as note
        private void setNoteNumber(object sender, MouseEventArgs e)
        {

        }
        //sets to remove a number from the board
        private void setRemoveNumber(object sender, MouseEventArgs e)
        {

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
                    index++;
                }
            }
        }
        #endregion
        #region button functions
        private void startReset_Click(object sender, EventArgs e)
        {
            if (startReset.Text == "Start")
            {
                startReset.Text = "Reset";
                genBoard();
                genValueButtons();
            }
            initSudoku();
        }
        private void easyRadio_CheckedChanged(object sender, EventArgs e) { if (easyRadio.Checked) { hardRadio.Checked = false; mediumRadio.Checked = false; difficulty = 0; } }
        private void mediumRadio_CheckedChanged(object sender, EventArgs e) { if (mediumRadio.Checked) { easyRadio.Checked = false; hardRadio.Checked = false; difficulty = 1; } }
        private void hardRadio_CheckedChanged(object sender, EventArgs e) { if (hardRadio.Checked) { easyRadio.Checked = false; mediumRadio.Checked = false; difficulty = 2; } }

        #endregion
    }
}
