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

namespace GamesForClass
{
    public partial class Minesweeper : Form
    {
        private int[,] values;
        private int[,] marks;
        private Button[,] buttons;
        private int difficulty;
        public Minesweeper()
        {
            InitializeComponent();
            createBoard(2);
            //printVals(16, 30);
        }
        public void createBoard(int difficulty)
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
        }
        //plants bombs and calculates distance to each bomb
        public void plantBombs(int mines, int x, int y)
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
        public void placeButtons(int x, int y)
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
                    newButton.Click += Button_Click;
                    this.Controls.Add(newButton);
                    buttons[i,j] = newButton;
                }
            }

        }
        //button click function, for all board button clicks
        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            test.Text = button.Name;
        }
        public void printVals(int x, int y)
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

    }
    

}
