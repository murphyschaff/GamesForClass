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
        public sudoku()
        {
            InitializeComponent();
            initSudoku();
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

            //fills in remaining spaces, checks for correctness
            for (int i = 0; i < 6; i++)
            {
                //gets square starting locations
                List<int> squareValues = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                int startx = 0;
                int starty = 0;
                int count = 0;
                int row = 0;
                switch(i)
                {
                    case 0:
                        startx = 0;
                        starty = 3;
                        break;
                    case 1:
                        startx = 0;
                        starty = 6;
                        break;
                    case 2:
                        startx = 3;
                        starty = 0;
                        break;
                    case 3:
                        startx = 3;
                        starty = 6;
                        break;
                    case 4:
                        startx = 6;
                        starty = 0;
                        break;
                    case 5:
                        startx = 6;
                        starty = 3;
                        break;
                }

                //runs until entire square filled
                while (squareValues.Count > 0)
                {
                    //gets value from list, calculates coordinates
                    val = rand.Next(0, squareValues.Count);
                    int x = startx + row;
                    int y = starty + (count % 3);

                    int checkVal = squareValues[val];
                    if (checkRules(checkVal, x, y))
                    {
                        puzzle[x, y] = checkVal;
                        squareValues.RemoveAt(val);

                        count++;
                        if (count %3 == 0)
                        {
                            row++;
                        }
                    }
                }
                int hold = 0;
            }

        }
        //recursive function to make sure a square can be filled in
        private bool fillSquare(List<int> remainingValues, int x, int y)
        {
            Random rnd = new Random();
            int val = rnd.Next(0, remainingValues.Count);
            int number = remainingValues[val];
            if (checkRules(number, x, y))
            {
                remainingValues.RemoveAt(val);
                if (remainingValues.Count == 0)
                {
                    return true;
                } 
                else
                {
                    return fillSquare(remainingValues, x, y);
                }
            }
            else
            {
                return false;
            }
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
            return true;
        }

        #endregion
    }
}
