using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesForClass
{
    public partial class Checkers : Form
    {
        private CheckerPlayer player;
        private CheckerPlayer CPU;
        private Button[,] buttons;
        private int dimention = 8;
        public Checkers()
        {
            buttons = new Button[8, 8];
            InitializeComponent();
            createBoard();
        }
        #region board creation
        //creates board
        public void createBoard()
        {      
            int startX = 240;
            int startY = 65;
            int size = 80;
            int x, y;
            for (int i = 0; i < dimention; i++)
            {
                for (int j = 0; j < dimention; j++)
                {
                    x = startX + (i * size);
                    y = startY + (j * size);
                    //when buttons are on odd values
                    if (i % 2 == 0)
                    {
                        //label
                        if (j % 2 == 0)
                        {
                            createLabel(i, j, x, y, size);                           
                        }
                        //button
                        else
                        {
                            Button button = createButton(i, j, x, y, size);
                            buttons[i,j] = button;
                        }
                    }
                    //when buttons are on even values
                    else
                    {
                        //button
                        if (j % 2 == 0)
                        {
                            Button button = createButton(i, j, x, y, size);
                            buttons[i, j] = button;
                        }
                        //label
                        else
                        {
                            createLabel(i, j, x, y, size);                            
                        }
                    }
                }
            }
            boardBackground.SendToBack();
        }
        //creates button for board
        public Button createButton(int i, int j, int x, int y, int size)
        {
            Button button = new Button();
            button.Name = i.ToString() + j.ToString();
            button.BackColor = Color.Black;
            button.ForeColor = Color.White;
            button.Location = new Point(x, y);
            button.Font = new Font("Microsoft Sans Sarif", 20);
            button.Size = new Size(size, size);
            button.MouseDown += boardButtonClick;
            button.Enabled = false;
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
        public void boardButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            test.Text = button.Name;
        }

        private void startReset_Click(object sender, EventArgs e)
        {
            if (startReset.Text == "Start")
            {
                test.Text = "Start";
            }
        }
    }

    public class CheckerPlayer
    {
        private Checker[] checkers;

        public CheckerPlayer()
        {
            checkers = new Checker[12];
        }
    }

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
