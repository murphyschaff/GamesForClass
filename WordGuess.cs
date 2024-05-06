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

        private int guess = 0;
        private int index = 0;
        private String word;
        private bool play = true;
        public WordGuess()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyUp += new KeyEventHandler(WordGuess_KeyUp);
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
                    label.TextAlign = ContentAlignment.MiddleCenter;
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
            word = words[rnd.Next(words.Length)].ToUpper();
            word = word.Substring(0, word.Length - 1);
        }
        //keeps track of user input
        private void WordGuess_KeyUp(object sender, KeyEventArgs e)
        {
            String entry = e.KeyCode.ToString();
            Label label;
            //makes sure the game is currently going
            if (play)
            {
                feedback.Text = "";
                //user wants to remove character
                if (entry == "Back")
                {
                    if (index != 0)
                    {
                        index--; 
                        label = lables[guess, index];
                        label.Text = "";
                    }
                }
                //user wants to enter guess
                else if (entry == "Return")
                {
                    if (index == 5)
                    {
                        //checks guess validity
                        if (checkGuess())
                        {
                            guess++;
                            feedback.Text = "You guessed \"" + word + "\" in " + guess.ToString() + " guesses";
                            play = false;
                        }
                        else
                        {
                            guess++;
                            index = 0;
                            //out of guesses, game over
                            if (guess == 5)
                            {
                                feedback.Text = "You ran out of guesses.\nThe correct word was \"" + word + "\"";
                                play = false;
                            }
                        }
                    }
                    else
                    {
                        feedback.Text = "Please enter a full word";
                    }
                }
                //user wants to enter character
                else if (entry.Length == 1)
                {
                    if (index < 5)
                    {
                        label = lables[guess, index];
                        label.Text = entry;
                        index++;
                    }
                }
                else
                {
                    //invalid input
                    feedback.Text = "Invalid input. Please try again";
                }
            }
        }
        #region guess checking
        //checks to see if the guess made by the user is correct
        //returns true if so, false if not
        public bool checkGuess()
        {
            Label label;
            char currentLetter;
            int correct = 0;
            //checks if a letter is correct. If correct, marks space green
            for (int i = 0; i < lables.GetLength(0); i++)
            {
                label = lables[guess, i];
                currentLetter = (char)label.Text[0];
                if (currentLetter == word[i])
                {
                    label.BackColor = Color.Green;
                    label.ForeColor = Color.White;
                    correct++;
                }
            }

            if (correct == 5) return true; else checkInWord(); return false;
        }
        public void checkInWord()
        {
            Label label;
            char currentletter;
            //looks through each letter that is not correct, checks to see if it is in word (and spot is not currently correct)
            for (int i = 0; i < lables.GetLength(0); i++)
            {
                label = lables[guess, i];
                if (label.BackColor != Color.Green)
                {
                    currentletter = (char)label.Text[0];
                    for (int j = 0; j < lables.GetLength(0);j++)
                    {
                        //if the letter is in the word, and the letter has not been correctly guessed yet, then mark the space as yellow
                        if (currentletter == word[j] && lables[guess, j].BackColor != Color.Green)
                        {
                            label.BackColor = Color.Yellow;
                        }
                    }
                }
            }
        }
        #endregion
        #region clear functions
        //clears all lable back colors, and resets forecolor
        public void resetLabels()
        {
            for (int i = 0; i < lables.GetLength(0); i++)
            {
                for (int j =0; j < lables.GetLength(0); j++)
                {
                    lables[i, j].BackColor = Color.Gray;
                    lables[i, j].ForeColor = Color.Black;
                    lables[i, j].Text = "";
                }
            }
        }
        #endregion

        #region buttons
        //resets the game, at any time
        private void resetButton_Click(object sender, EventArgs e)
        {
            resetLabels();
            chooseWord();
            play = true;
            guess = 0;
            index = 0;
            feedback.Text = "";
            test.Text = "";
        }
        private void title_Click(object sender, EventArgs e)
        {
            test.Text = word;
        }
        #endregion

    }
}
