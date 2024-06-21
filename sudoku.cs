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
        private int[,] puzzle;
        private int[,] userPuzzle;
        private String[,] userNotes;
        private int size = 9;
        private int sqrtSize = 3;
        private int difficulty = 0;
        public sudoku()
        {
            InitializeComponent();
        }
        /*
         * Puzzle setup functions
         */
        #region setup
        private void initSudoku()
        {
            puzzle = new int[9, 9];
            userPuzzle = new int[9, 9];
            userNotes = new string[9, 9];
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
            }
            initSudoku();
        }
        private void easyRadio_CheckedChanged(object sender, EventArgs e) { if (easyRadio.Checked) { hardRadio.Checked = false; mediumRadio.Checked = false; difficulty = 0; } }
        private void mediumRadio_CheckedChanged(object sender, EventArgs e) { if (mediumRadio.Checked) { easyRadio.Checked = false; hardRadio.Checked = false; difficulty = 1; } }
        private void hardRadio_CheckedChanged(object sender, EventArgs e) { if (hardRadio.Checked) { easyRadio.Checked = false; mediumRadio.Checked = false; difficulty = 2; } }

        #endregion
    }
}
