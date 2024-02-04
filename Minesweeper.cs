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
using System.Xml;

namespace GamesForClass
{
    public partial class Minesweeper : Form
    {
        private int[,] values;
        private bool placed = false;
        private int mines;
        private Button[,] buttons;
        private int xLen;
        private int yLen;
        public Minesweeper()
        {
            InitializeComponent();
        }
        private void createBoard(int difficulty)
        {
            int x = 0;
            int y = 0;
            Random rnd = new Random();
            switch (difficulty)
            {
                //easy
                case 0:
                    mines = 10;
                    x = 8;
                    y = 8;
                    break;
                //medium
                case 1:
                    mines = 40;
                    x = 16;
                    y = 16;
                    break;
                //hard
                case 2:
                    mines = 99;
                    x = 16;
                    y = 30;
                    break;
            }
            values = new int[x, y];
            buttons = new Button[x, y];
            xLen = x;
            yLen = y;
            //creates initial values board
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    values[i, j] = -5;
                }
            }
            placeButtons(x, y);
            remainingBombs.Text = mines.ToString();
        }
        //plants bombs and calculates distance to each bomb
        //VALUES: -1: BOMB, 0: Not next to any bombs, other numbers: how many adjacent bombs
        private void plantBombs(int noX, int noY)
        {
            Random rnd = new Random();
            //planting bombs randomly
            int index = 0;
            while (index < mines)
            {
                int xVal = rnd.Next(xLen);
                int yVal = rnd.Next(yLen);
                if (values[xVal, yVal] != -1 && (xVal != noX && yVal != noY))
                {
                    values[xVal, yVal] = -1;
                    index++;
                }
            }

            //calculate distance to bomb
            int adjMine = 0;
            //top left
            if (values[0,0] != -1)
            {
                if (values[1,0] == -1)
                {
                    adjMine++;
                }
                if (values[1,1] == -1)
                {
                    adjMine++;
                }
                if (values[0,1] == -1)
                {
                    adjMine++;
                }
                values[0,0] = adjMine;
            }
            //top row
            adjMine = 0;
            for (int i = 1; i < yLen -1; i++)
            {
                adjMine = 0;
                if (values[0,i] != -1)
                {
                    if (values[0, i-1] == -1)
                    {
                        adjMine++;
                    }
                    if (values[1, i-1] == -1)
                    {
                        adjMine++;
                    }
                    if (values[1,i] == -1)
                    {
                        adjMine++;
                    }
                    if (values[1,i+1] == -1)
                    {
                        adjMine++;
                    }
                    if (values[0,i+1] == -1)
                    {
                        adjMine++;
                    }
                    values[0, i] = adjMine;
                }
            }
            //top right
            adjMine = 0;
            if (values[0, yLen-1] != -1)
            {
                if (values[0, yLen-2] == -1)
                {
                    adjMine++;
                }
                if (values[1, yLen -2] == -1)
                {
                    adjMine++;
                }
                if (values[1, yLen -1] == -1)
                {
                    adjMine++;
                }
                values[0, yLen -1] = adjMine;
            }

            //left side
            for (int i = 1; i < xLen-1; i++) 
            {
                adjMine = 0;
                if (values[i, 0] != -1)
                {
                    if (values[i -1, 0] == -1)
                    {
                        adjMine++;
                    }
                    if (values[i-1, 1] == -1)
                    {
                        adjMine++;
                    }
                    if (values[i, 1] == -1)
                    {
                        adjMine++;
                    }
                    if (values[i+1, 1] == -1)
                    {
                        adjMine++;
                    }
                    if (values[i +1,0] == -1)
                    {
                        adjMine++;
                    }
                    values[i,0] = adjMine;
                }
            }
            //bottom left
            adjMine = 0;
            if (values[xLen-1,0] != -1)
            {
                if (values[xLen-2, 0] == -1)
                {
                    adjMine++;
                }
                if (values[xLen-2, 1] == -1)
                {
                    adjMine++;
                }
                if (values[xLen-1,1] == -1)
                {
                    adjMine++;
                }
                values[xLen-1, 0] = adjMine;
            }
            //right side
            for (int i = 1; i < xLen - 1; i++)
            {
                adjMine = 0;
                if (values[i, yLen -1] != -1)
                {
                    if (values[i - 1, yLen -1] == -1)
                    {
                        adjMine++;
                    }
                    if (values[i - 1, yLen -2] == -1)
                    {
                        adjMine++;
                    }
                    if (values[i, yLen -2] == -1)
                    {
                        adjMine++;
                    }
                    if (values[i + 1, yLen -2] == -1)
                    {
                        adjMine++;
                    }
                    if (values[i + 1, yLen -1] == -1)
                    {
                        adjMine++;
                    }
                    values[i, yLen -1] = adjMine;
                }
            }
            //bottom right
            adjMine = 0;
            if (values[xLen - 1, yLen -1] != -1)
            {
                if (values[xLen - 2, yLen - 1] == -1)
                {
                    adjMine++;
                }
                if (values[xLen - 2, yLen -2] == -1)
                {
                    adjMine++;
                }
                if (values[xLen - 1, yLen -2] == -1)
                {
                    adjMine++;
                }
                values[xLen - 1, yLen -1] = adjMine;
            }
            //bottom
            for (int i = 1; i < yLen -1; i++)
            {
                adjMine = 0;
                if (values[xLen-1, i] != -1)
                {
                    if (values[xLen -1, i-1] == -1)
                    {
                        adjMine++;
                    }
                    if (values[xLen-2, i-1] == -1)
                    {
                        adjMine++;
                    }
                    if (values[xLen -2, i] == -1)
                    {
                        adjMine++;
                    }
                    if (values[xLen -2, i+1] == -1)
                    {
                        adjMine++;
                    }
                    if (values[xLen -1, i+1] == -1)
                    {
                        adjMine++;
                    }
                    values[xLen -1, i] = adjMine;
                }
            }
            //middle
            for (int i = 1; i < xLen -1; i++)
            {
                for (int t = 1; t < yLen -1; t++)
                {
                    adjMine = 0;
                    if (values[i,t] != -1)
                    {
                        if (values[i-1, t-1] == -1)
                        {
                            adjMine++;
                        }
                        if (values[i-1,t] == -1)
                        {
                            adjMine++;
                        }
                        if (values[i-1, t+1] == -1)
                        {
                            adjMine++;
                        }
                        if (values[i, t-1] == -1)
                        {
                            adjMine++;
                        }
                        if (values[i, t+1] == -1)
                        {
                            adjMine++;
                        }
                        if (values[i+1, t-1] == -1)
                        {
                            adjMine++;
                        }
                        if (values[i+1, t] == -1)
                        {
                            adjMine++;
                        }
                        if (values[i+1, t+1] == -1)
                        {
                            adjMine++;
                        }
                        values[i,t] = adjMine;
                    }
                }
            }
        }
        //places all buttons onto board
        private void placeButtons(int x, int y)
        {
            //middle is 575, 340
            int offsetX = 340 - (15 + ((x / 2) * 30));
            int offsetY = 575 - (15 + ((y / 2) * 30));
            for (int i = 0; i < x; i++)
            {
                for (int j =0; j < y; j++)
                {
                    Button newButton = new Button();
                    String name = i.ToString() + "-" + j.ToString();
                    newButton.Name = name;
                    newButton.Size = new Size(30, 30);
                    newButton.Location = new Point(offsetY + (j * 30) , offsetX + (i * 30));
                    newButton.MouseDown += Button_Click;
                    newButton.BackColor = Color.White;
                    newButton.BringToFront();
                    this.Controls.Add(newButton);
                    buttons[i,j] = newButton;
                }
            }
            background.SendToBack();

        }
        //checks user actions when clicking button
        private void placeFlag(Button button, bool isBomb)
        {
            String buttonName = button.Name;
            String output = "";
            int xVal = 0;
            int yVal;
            for (int i = 0; i < buttonName.Length; i++)
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
            //if the button was right-clicked, sets as bomb
            if (isBomb)
            {
                button.Text = "B";
                button.BackColor = Color.DarkGray;
            }
            else
            {               
                int val = values[xVal,yVal];
                //if user marked bomb as safe
                if (val == -1)
                {
                    //explode
                    explode();
                    resultsLabel.Text = "You Loose!";
                    return;
                }
                else if (val == 0)
                {
                    //run zero clear sequence
                    clearZeros(xVal, yVal);
                }
                else
                {
                    //mark button with value
                    button.Text = val.ToString();
                    button.BackColor = Color.Gray;
                    button.Enabled = false;
                }
            }
            //run check win sequence
            calcMinesRemaining();
            checkWin();
        }
        //calculates the number of bombs remaining on the board
        private void calcMinesRemaining()
        {
            int markedMines = 0;
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    if (buttons[i,j].Text == "B" && buttons[i,j].BackColor == Color.DarkGray)
                    {
                        markedMines++;
                    }
                }
            }
            if (mines - markedMines >= 0) 
            { 
                remainingBombs.Text = (mines - markedMines).ToString();
            }
            else
            {
                resultsLabel.Text = "Mistake Made";
            }
        }
        //checks to see if the user has won
        private void checkWin()
        {
            bool mrkdBombs = true;
            int rmBombs = Convert.ToInt32(remainingBombs.Text);
            if (rmBombs == 0)
            {
                for (int i = 0; i < xLen; i++)
                {
                    for (int j = 0; j < yLen; j++)
                    {
                        //checks to make sure all non bomb spaces have been selected
                        if (values[i, j] != -1 && buttons[i, j].Enabled == true)
                        {
                            //user has not won yet
                            mrkdBombs = false;
                            break;
                        }
                    }
                }
                if (mrkdBombs)
                {
                    resultsLabel.Text = "You Win!";
                    for (int i = 0; i < xLen; i++)
                    {
                        for (int j = 0; j < yLen; j++)
                        {
                            buttons[i, j].Enabled = false;
                        }
                    }
                }
            }
        }
        //recursive function that clears a large swath of board to start
        private void clearZeros(int xVal, int yVal)
        {
            Button button = buttons[xVal, yVal];
            int val = values[xVal, yVal];
            button.Enabled = false;
            button.BackColor = Color.Gray;
            button.Text = "";
            //if the value is 0, check above, below, right, and left if not already checked. does not check diagonals
            if (val == 0)
            {
                if (xVal < xLen -1 && buttons[xVal + 1, yVal].Enabled == true)
                {
                    clearZeros(xVal + 1, yVal);
                }
                if (xVal > 0 && buttons[xVal - 1, yVal].Enabled == true)
                {
                    clearZeros(xVal -1, yVal);
                }
                if (yVal < yLen -1 && buttons[xVal, yVal + 1].Enabled == true)
                {
                    clearZeros(xVal, yVal + 1);
                }
                if (yVal > 0 && buttons[xVal, yVal -1].Enabled == true)
                {
                    clearZeros(xVal, yVal - 1);
                }
            }
            //places number onto button, stops recursion
            else
            {
                button.Text = val.ToString();
            }
        }
        //causes loss, explodes and shows all bombs on board
        private void explode()
        {
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    if (values[i,j] == -1)
                    {
                        buttons[i, j].BackColor = Color.Red;
                        buttons[i, j].Text = "B";
                    }
                    buttons[i, j].Enabled = false;
                }
            }
        }
        //button click function, for all board button clicks
        private void Button_Click(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            if (!placed)
            {
                placed = true;
                //plants bomb if this is the first button clicked
                //prevents first button from being a bomb
                String buttonName = button.Name;
                String output = "";
                int xVal = 0;
                int yVal;
                for (int i = 0; i < buttonName.Length; i++)
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
                plantBombs(xVal, yVal);
            }
            bool isBomb = false;
            if (e.Button == MouseButtons.Right)
            {
                isBomb = true;
            }
            placeFlag(button, isBomb);
        }
        //prints all data in values to a test label (testing purposes only)
        private void printVals(int x, int y)
        {
            String output = "";
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    output += values[i, j].ToString() + " ";
                }
                output += "\n";
            }
            test.Text = output;
        }
        //unchecks other check boxes
        private void uncheckBox(int skip)
        {
            for (int i = 0; i < 3; i++)
            {
                if (skip != i)
                {
                    switch (i)
                    {
                        //easy
                        case 0:
                            easyCheck.Checked = false; break;
                        //medium
                        case 1:
                            mediumCheck.Checked = false; break;
                        //hard
                        case 2:
                            hardCheck.Checked = false; break;
                    }
                }
            }
        }
        //starts the game sequence
        private void startButton_Click(object sender, EventArgs e)
        {
            int difficulty;
            startButton.Enabled = false;
            easyCheck.Enabled = false;
            mediumCheck.Enabled = false;
            hardCheck.Enabled = false;
            label1.Enabled = false;
            //gets selected difficulty, easy, medium, hard
            if (easyCheck.Checked)
            {
                difficulty = 0;
            }
            else if (mediumCheck.Checked)
            {
                difficulty = 1;
            }
            else
            {
                difficulty = 2;
            }
            //runs creation sequence
            createBoard(difficulty);
        }
        //check box changes
        private void easyCheck_CheckedChanged(object sender, EventArgs e) { if (easyCheck.Checked) { uncheckBox(0); } }
        private void mediumCheck_CheckedChanged(object sender, EventArgs e) { if (mediumCheck.Checked) { uncheckBox(1); } }
        private void hardCheck_CheckedChanged(object sender, EventArgs e) { if (hardCheck.Checked) { uncheckBox(2); } }
        //reset button click
        private void resetButton_Click(object sender, EventArgs e)
        {
            //removes all generated buttons from board
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    this.Controls.Remove(buttons[i, j]);
                }
            }
            remainingBombs.Text = "";
            resultsLabel.Text = "";
            startButton.Enabled = true;
            easyCheck.Enabled = true;
            mediumCheck.Enabled = true;
            hardCheck.Enabled = true;
            label1.Enabled = true;
            placed = false;
        }
        //secret button, shows where all mines are located on board
        private void title_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    if (values[i,j] == -1)
                    {
                        buttons[i, j].Text = "B";
                    }
                }
            }
        }
    }
    

}
