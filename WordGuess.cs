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
        public Label[,] labels = new Label[5, 6];

        private int guess = 0;
        private int index = 0;
        private String word;
        private bool play = true;
        private int wordLength = 5;
        private int currentLoadedWords;
        public WordGuess()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyUp += new KeyEventHandler(WordGuess_KeyUp);
            String file = Resources._4words;
            words = file.Split('\n');
            currentLoadedWords = 4;
            placeLabels();
        }
        #region labels
        //places all lables on the board
        private void placeLabels()
        {
            int labelSize = 70;

            for (int i = 0; i < labels.GetLength(0); i++)
            {
                for (int j = 0; j < labels.GetLength(1); j++)
                {
                    Label label = new Label();
                    label.Size = new Size(labelSize, labelSize);
                    label.BackColor = Color.Gray;
                    label.Font = new Font("Microsoft Sans Sarif", 50);
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.BringToFront();
                    label.Visible = false;

                    labels[i,j] = label;
                    this.Controls.Add(label);
                }
            }
        }
        //changes label location and visibility depending on word length
        private void changeBoard()
        {
            int middleX = 325;
            int labelSize = 70;
            int offset = 5;
            int startX;
            int startY = 75;
            //calculates starting location
            if (wordLength == 4)
            {
                startX = middleX - (labelSize * 2) - 3;
            }
            else if (wordLength == 5)
            {
                startX = middleX - (labelSize * 2) - 45;
            }
            //wordLength == 6
            else
            {
                startX = middleX - (labelSize * 3) - 3;
            }
            //puts lables in starting location, making superfulous lables invisible and needed ones visible
            for (int i = 0; i < labels.GetLength(0); i++)
            {
                for (int j = 0; j < labels.GetLength(1); j++)
                {
                    labels[i,j].Location = new Point(startX + ((labelSize + offset) * j), startY + ((labelSize + offset) * i));

                    if (j < wordLength)
                    {
                        labels[i, j].Visible = true;
                    }
                    else
                    {
                        labels[i, j].Visible = false;
                    }
                }
            }

        }
        #endregion
        //Function that chooses a word from the list and begins the game as intended
        private void chooseWord()
        {
            //opens and adds words if check changed
            String file;
            Random rnd = new Random();
            if (fourLetter.Checked && currentLoadedWords != 4)
            {
                file = Resources._4words;
                words = file.Split('\n');
                currentLoadedWords = 4;
            }
            else if (fiveLetter.Checked && currentLoadedWords != 5)
            {
                file = Resources._5words;
                words = file.Split('\n');
                currentLoadedWords = 5;
            }
            else if (sixLetter.Checked && currentLoadedWords != 6)
            {
                file = Resources._6words;
                words = file.Split('\n');
                currentLoadedWords = 6;
            }
            //selects word
            word = words[rnd.Next(words.Length)].ToUpper();
            if (word.Length > currentLoadedWords)
            {
                word = word.Substring(0, word.Length - 1);
            }
            wordLength = word.Length;
            //updates board based on word size
            changeBoard();
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
                        label = labels[guess, index];
                        label.Text = "";
                    }
                }
                //user wants to enter guess
                else if (entry == "Return")
                {
                    if (index == wordLength)
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
                                feedback.Text = "You ran out of guesses. The word was \"" + word + "\"";
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
                    if (index < wordLength)
                    {
                        label = labels[guess, index];
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
            for (int i = 0; i < wordLength; i++)
            {
                label = labels[guess, i];
                currentLetter = (char)label.Text[0];
                if (currentLetter == word[i])
                {
                    label.BackColor = Color.Green;
                    label.ForeColor = Color.White;
                    correct++;
                }
            }

            if (correct == wordLength) return true; else checkInWord(); return false;
        }
        public void checkInWord()
        {
            Label label;
            char currentletter;
            //looks through each letter that is not correct, checks to see if it is in word (and spot is not currently correct)
            for (int i = 0; i < wordLength; i++)
            {
                label = labels[guess, i];
                if (label.BackColor != Color.Green)
                {
                    currentletter = (char)label.Text[0];
                    for (int j = 0; j < wordLength; j++)
                    {
                        //if the letter is in the word, and the letter has not been correctly guessed yet, then mark the space as yellow
                        if (currentletter == word[j] && labels[guess, j].BackColor != Color.Green)
                        {
                            label.BackColor = Color.Yellow;
                        }
                    }
                    //if the label was not changed color, change it to show something was done
                    if (label.BackColor != Color.Yellow)
                    {
                        label.BackColor = Color.DarkGray;
                    }
                }
            }
        }
        #endregion
        #region clear function
        //clears all lable back colors, and resets forecolor
        public void resetLabels()
        {
            for (int i = 0; i < labels.GetLength(0); i++)
            {
                for (int j =0; j < labels.GetLength(1); j++)
                {
                    labels[i, j].BackColor = Color.Gray;
                    labels[i, j].ForeColor = Color.Black;
                    labels[i, j].Text = "";
                }
            }
        }
        #endregion

        #region buttons
        //resets the game, at any time
        private void resetButton_Click(object sender, EventArgs e)
        {
            //changes button text/resets lables based on text
            if (resetButton.Text == "Start") resetButton.Text = "Reset";
            else resetLabels();
           
            play = true;
            guess = 0;
            index = 0;
            feedback.Text = "";
            test.Text = "";
            chooseWord();
        }
        private void title_Click(object sender, EventArgs e)
        {
            test.Text = word;
        }
        //radio button changes
        private void fourLetter_CheckedChanged(object sender, EventArgs e){ if (fourLetter.Checked == true) { fiveLetter.Checked = false; sixLetter.Checked = false; } }
        private void fiveLetter_CheckedChanged(object sender, EventArgs e){ if (fiveLetter.Checked == true) { fourLetter.Checked = false; sixLetter.Checked = false; } }
        private void sixLetter_CheckedChanged(object sender, EventArgs e){ if (sixLetter.Checked == true) { fourLetter.Checked = false; fiveLetter.Checked = false; } }
        #endregion
    }
}
