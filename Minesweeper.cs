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
        private int[,] marks;
        private Button[,] buttons;
        private int xLen;
        private int yLen;
        public Minesweeper()
        {
            InitializeComponent();
        }
        private void createBoard(int difficulty)
        {
            int mines = 0;
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

            plantBombs(mines, x, y);
            placeButtons(x, y);
            remainingBombs.Text = mines.ToString();
        }
        //plants bombs and calculates distance to each bomb
        //VALUES: -1: BOMB, 0: Not next to any bombs, other numbers: how many adjacent bombs
        private void plantBombs(int mines, int x, int y)
        {
            Random rnd = new Random();
            //planting bombs randomly
            int index = 0;
            while (index < mines)
            {
                int xVal = rnd.Next(x);
                int yVal = rnd.Next(y);
                if (values[xVal, yVal] != -1)
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
            for (int i = 1; i < y -1; i++)
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
            if (values[0, y-1] != -1)
            {
                if (values[0, y-2] == -1)
                {
                    adjMine++;
                }
                if (values[1, y-2] == -1)
                {
                    adjMine++;
                }
                if (values[1, y-1] == -1)
                {
                    adjMine++;
                }
                values[0, y-1] = adjMine;
            }

            //left side
            for (int i = 1; i < x-1; i++) 
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
            if (values[x-1,0] != -1)
            {
                if (values[x-2, 0] == -1)
                {
                    adjMine++;
                }
                if (values[x-2, 1] == -1)
                {
                    adjMine++;
                }
                if (values[x-1,1] == -1)
                {
                    adjMine++;
                }
                values[x-1, 0] = adjMine;
            }
            //right side
            for (int i = 1; i < x - 1; i++)
            {
                adjMine = 0;
                if (values[i, y-1] != -1)
                {
                    if (values[i - 1, y-1] == -1)
                    {
                        adjMine++;
                    }
                    if (values[i - 1, y-2] == -1)
                    {
                        adjMine++;
                    }
                    if (values[i, y-2] == -1)
                    {
                        adjMine++;
                    }
                    if (values[i + 1, y-2] == -1)
                    {
                        adjMine++;
                    }
                    if (values[i + 1, y-1] == -1)
                    {
                        adjMine++;
                    }
                    values[i, y-1] = adjMine;
                }
            }
            //bottom right
            adjMine = 0;
            if (values[x - 1, y-1] != -1)
            {
                if (values[x - 2, y - 1] == -1)
                {
                    adjMine++;
                }
                if (values[x - 2, y-2] == -1)
                {
                    adjMine++;
                }
                if (values[x - 1, y-2] == -1)
                {
                    adjMine++;
                }
                values[x - 1, y-1] = adjMine;
            }
            //bottom
            for (int i = 1; i < y-1; i++)
            {
                adjMine = 0;
                if (values[x-1, i] != -1)
                {
                    if (values[x-1, i-1] == -1)
                    {
                        adjMine++;
                    }
                    if (values[x-2, i-1] == -1)
                    {
                        adjMine++;
                    }
                    if (values[x-2, i] == -1)
                    {
                        adjMine++;
                    }
                    if (values[x-2, i+1] == -1)
                    {
                        adjMine++;
                    }
                    if (values[x-1, i+1] == -1)
                    {
                        adjMine++;
                    }
                    values[x-1, i] = adjMine;
                }
            }
            //middle
            for (int i = 1; i < x-1; i++)
            {
                for (int t = 1; t < y-1; t++)
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
                int rmBombs = Convert.ToInt32(remainingBombs.Text);
                if (rmBombs > 0)
                {
                    remainingBombs.Text = (rmBombs - 1).ToString();
                }
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
                    button.BackColor = Color.Gray;
                    button.Enabled = false;
                    clearZeros(xVal, yVal);
                    button.Enabled = false;
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
            checkWin();
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
                        if (values[i, j] == -1 && buttons[i, j].Text != "B")
                        {
                            //user has not won yet
                            mrkdBombs = false;
                            break;
                        }
                    }
                }
                if (!mrkdBombs)
                {
                    resultsLabel.Text = "Mistake Made";
                }
                else
                {
                    resultsLabel.Text = "You Win!";
                    for (int i = 0; i < xLen; i++)
                    {
                        for (int j = 0; i < yLen; j++)
                        {
                            buttons[i,j].Enabled = false;
                        }
                    }
                }
            }
        }
        //recursive function that clears a large swath of board to start
        private void clearZeros(int xVal, int yVal)
        {
            if (yVal < yLen -1)
            {
                checkRight(xVal, yVal + 1);
            }
            if (yVal > 0)
            {
                checkLeft(xVal, yVal - 1);
            }
            if (xVal < xLen -1)
            {
                checkDown(xVal + 1, yVal);
            }
            if (xVal > 0)
            {
                checkUp(xVal -1, yVal);
            }
        }
        //checks the button to the right for 0 or value that is not bomb
        private void checkRight(int xVal, int yVal)
        {
            int val = values[xVal, yVal];
            Button button = buttons[xVal, yVal];
            button.Enabled = false;
            button.BackColor = Color.Gray;
            if (val == 0 && yVal < yLen - 1)
            {
                checkRight(xVal, yVal + 1);
            }
            else
            {
                button.Text = val.ToString();
            }
        }
        //checks the button to the left for 0 or value that is not bomb
        private void checkLeft(int xVal, int yVal)
        {
            int val = values[xVal, yVal];
            Button button = buttons[xVal, yVal];
            button.Enabled = false;
            button.BackColor = Color.Gray;
            if (val == 0 && yVal > 0)
            {
                checkLeft(xVal, yVal - 1);
            }
            else
            {
                button.Text = val.ToString();
            }
        }
        //checks the button beneath for 0 or value that is not bomb
        private void checkDown(int xVal, int yVal)
        {
            int val = values[xVal, yVal];
            Button button = buttons[xVal, yVal];
            button.Enabled = false;
            button.BackColor = Color.Gray;
            if (val == 0)
            {
                if (xVal < xLen - 1)
                {
                    checkDown(xVal +1, yVal);
                }
                if (yVal < yLen -1)
                {
                    checkRight(xVal, yVal + 1);
                }
                if (yVal > 0)
                {
                    checkLeft(xVal, yVal - 1);
                }
            }
            else
            {
                button.Text = val.ToString();
            }
        }
        //checks the button above for 0 or value that is not bomb
        private void checkUp(int xVal, int yVal)
        {
            int val = values[xVal, yVal];
            Button button = buttons[xVal, yVal];
            button.Enabled = false;
            button.BackColor = Color.Gray;
            if (val == 0)
            {
                if (xVal > 0)
                {
                    checkUp(xVal - 1, yVal);
                }
                if (yVal < yLen - 1)
                {
                    checkRight(xVal, yVal + 1);
                }
                if (yVal > 0)
                {
                    checkLeft(xVal, yVal - 1);
                }
            }
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
                        buttons[i, j].Text = "B";
                        buttons[i, j].ForeColor = Color.Red;
                    }
                }
            }
        }
        //button click function, for all board button clicks
        private void Button_Click(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            bool isBomb = false;
            if (e.Button == MouseButtons.Right)
            {
                isBomb = true;
                test.Text = button.Name;
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
    }
    

}
