using GamesForClass.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesForClass
{
    public partial class WordGuess : Form
    {
        public String[] words;
        public Label[,] lables = new Label[5, 5];
        public WordGuess()
        {
            InitializeComponent();
            String file = Resources.wordle_answers_alphabetical;
            words = file.Split('\n');
            placeLables();
            chooseWord();
        }
        #region initial setup
        //places all lables on the board
        private void placeLables()
        {
            int startX = 140;
            int startY = 75;
            int size = 5;
            int labelSize = 70;
            int offset = 5;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Label label = new Label();
                    label.Location = new Point(startX + ((labelSize + offset) * j), startY + ((labelSize + offset) * i));
                    label.Size = new Size(labelSize, labelSize);
                    label.BackColor = Color.Gray;
                    label.Font = new Font("Microsoft Sans Sarif", 50);
                    label.BringToFront();

                    lables[i,j] = label;
                    this.Controls.Add(label);
                }
            }
        }
        #endregion
        //Function that chooses a word from the list and begins the game as intended
        private void chooseWord()
        {
            Random rnd = new Random();
            String word = words[rnd.Next(words.Length)];
            test.Text = word;
        }
    }
}
