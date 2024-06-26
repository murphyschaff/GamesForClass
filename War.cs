﻿using System;
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
    public partial class War : Form
    {
        Queue<int> CPUDeck = new Queue<int>();
        Queue<int> playerDeck = new Queue<int>();
        Queue<int> playerDownCards = new Queue<int>();
        Queue<int> CPUDownCards = new Queue<int>();
        bool complete = false;
        bool tie = false;
        bool plrallIn = false;
        bool cpuallin = false;
        public War()
        {
            InitializeComponent();
            initWar();
        }
        /* creates the initial decks for the game */
        public void initWar()
        {
            List<int> initDeck = new List<int>();
            int count = 0;
            Random choice = new Random();
            //creates the initial list
            for (int i = 0; i < 52;  i++)
            {
                initDeck.Add((i % 13) + 1);
            }

            //runs untill the cards are given out
            while (initDeck.Count > 0)
            {
                int val = choice.Next(0, initDeck.Count);
                //if the random value chose one that has not been chosen yet
                if (initDeck[val] != 0)
                {
                    //gives a card to player every other chosen card, starting with player
                    if (count % 2 == 0)
                    {
                        playerDeck.Enqueue(initDeck[val]);
                        initDeck[val] = 0;
                        count++;
                    }
                    else
                    {
                        CPUDeck.Enqueue(initDeck[val]);
                        initDeck[val] = 0;
                        count++;
                    }
                    initDeck.RemoveAt(val);
                }
            }
            label3.Text = "";
            label4.Text = Convert.ToString(CPUDeck.Count);
            label5.Text = Convert.ToString(playerDeck.Count);
        }
        private void play()
        {
            if (!complete)
            {
                //draws cards from deck
                int playerCard = playerDeck.Dequeue();
                int CPUCard = CPUDeck.Dequeue();

                //displays cards
                label1.Text = Convert.ToString(CPUCard);
                label2.Text = Convert.ToString(playerCard);

                //CPU wins match
                if (CPUCard > playerCard)
                {
                    if (tie)
                    {
                        tie = false;
                        //adds all cards on the ground to CPU deck
                        while (CPUDownCards.Count > 0)
                        {
                            CPUDeck.Enqueue(CPUDownCards.Dequeue());
                        }
                        while (playerDownCards.Count > 0)
                        {
                            CPUDeck.Enqueue(playerDownCards.Dequeue());
                        }
                        //removes tie lables
                        label17.Visible = false;
                        label16.Visible = false;
                        label15.Visible = false;
                        label14.Visible = false;
                        label13.Visible = false;
                        label12.Visible = false;
                        label11.Visible = false;
                        label10.Visible = false;
                    }
                    CPUDeck.Enqueue(CPUCard);
                    CPUDeck.Enqueue(playerCard);
                    label3.Text = "CPU Wins the Round";
                }
                //player wins match
                else if (CPUCard < playerCard)
                {
                    if (tie)
                    {
                        tie = false;
                        //adds all cards on the table to the players deck
                        while (playerDownCards.Count > 0)
                        {
                            playerDeck.Enqueue(playerDownCards.Dequeue());
                        }
                        while (CPUDownCards.Count > 0)
                        {
                            playerDeck.Enqueue(CPUDownCards.Dequeue());
                        }
                        //removes tie labels
                        label17.Visible = false;
                        label16.Visible = false;
                        label15.Visible = false;
                        label14.Visible = false;
                        label13.Visible = false;
                        label12.Visible = false;
                        label11.Visible = false;
                        label10.Visible = false;
                    }
                    playerDeck.Enqueue(playerCard);
                    playerDeck.Enqueue(CPUCard);
                    label3.Text = "You Win the round!";
                }
                //Match is a tie
                else
                {
                    tie = true;
                    //places down 3 cards on top of the other card, draws again
                    int i = 0;
                    while (playerDeck.Count > 1 && i < 3)
                    {
                        playerDownCards.Enqueue(playerDeck.Dequeue());
                        i++;
                    }
                    playerDownCards.Enqueue(playerCard);

                    i = 0;
                    while (CPUDeck.Count > 1 && i < 3)
                    {
                        CPUDownCards.Enqueue(CPUDeck.Dequeue());
                        i++;
                    }
                    CPUDownCards.Enqueue(CPUCard);
                    //adds the graphics for the tie
                    label3.Text = "Round Tie!";
                    label10.Visible = true;
                    label11.Visible = true;
                    label12.Visible = true;
                    label13.Visible = true;
                    label14.Visible = true;
                    label15.Visible = true;
                    label17.Visible = true;
                    label16.Visible = true;
                    label10.Text = Convert.ToString(CPUDownCards.Count);
                    label13.Text = Convert.ToString(playerDownCards.Count);

                }
                label4.Text = Convert.ToString(CPUDeck.Count);
                label5.Text = Convert.ToString(playerDeck.Count);
            }
        }
        private void checkWinner()
        {
            if (playerDeck.Count == 0)
            {
                if (tie)
                {
                    plrallIn = true;
                }
                else
                {
                    label3.Text = "CPU is the winner.";
                    button1.Enabled = false;
                    complete = true;
                }
            }
            else if (CPUDeck.Count == 0)
            {
                if (tie)
                {
                    cpuallin = true;
                }
                else
                {
                    label3.Text = "You are the winner!!!!";
                    button1.Enabled = false;
                    complete = true;
                }
            }
        }
        /* simulates when a player is 'all in' */
        private void allIn()
        {
            int playerCard, CPUcard;
            //when the player is all in
            if (plrallIn)
            {
                playerCard = playerDownCards.Dequeue();
                CPUcard = CPUDeck.Dequeue();
                label1.Text = Convert.ToString(CPUcard);
                label2.Text = Convert.ToString(playerCard);
                //player wins
                if (playerCard > CPUcard)
                {
                    tie = false;
                    //adds all cards on the table to the players deck
                    while (playerDownCards.Count > 0)
                    {
                        playerDeck.Enqueue(playerDownCards.Dequeue());
                    }
                    while (CPUDownCards.Count > 0)
                    {
                        playerDeck.Enqueue(CPUDownCards.Dequeue());
                    }
                    //removes tie labels
                    label17.Visible = false;
                    label16.Visible = false;
                    label15.Visible = false;
                    label14.Visible = false;
                    label13.Visible = false;
                    label12.Visible = false;
                    label11.Visible = false;
                    label10.Visible = false;
                    playerDeck.Enqueue(playerCard);
                    playerDeck.Enqueue(CPUcard);
                    label3.Text = "You Win the round!";
                    plrallIn = false;
                }
                //CPU wins, and thus wins the whole match
                else if (playerCard < CPUcard)
                {
                    tie = false;
                    //adds all cards on the ground to CPU deck
                    while (CPUDownCards.Count > 0)
                    {
                        CPUDeck.Enqueue(CPUDownCards.Dequeue());
                    }
                    while (playerDownCards.Count > 0)
                    {
                        CPUDeck.Enqueue(playerDownCards.Dequeue());
                    }
                    //removes tie lables
                    label17.Visible = false;
                    label16.Visible = false;
                    label15.Visible = false;
                    label14.Visible = false;
                    label13.Visible = false;
                    label12.Visible = false;
                    label11.Visible = false;
                    label10.Visible = false; 
                    CPUDeck.Enqueue(CPUcard);
                    CPUDeck.Enqueue(playerCard);
                    label3.Text = "CPU Wins the Round";
                    plrallIn = false;
                }
            }
            //CPU is all in
            else
            {
                playerCard = playerDeck.Dequeue();
                CPUcard = CPUDownCards.Dequeue();
                label1.Text = Convert.ToString(CPUcard);
                label2.Text = Convert.ToString(playerCard);
                //player wins, and thus wins whole game
                if (playerCard > CPUcard)
                {
                    tie = false;
                    //adds all cards on the table to the players deck
                    while (playerDownCards.Count > 0)
                    {
                        playerDeck.Enqueue(playerDownCards.Dequeue());
                    }
                    while (CPUDownCards.Count > 0)
                    {
                        playerDeck.Enqueue(CPUDownCards.Dequeue());
                    }
                    //removes tie labels
                    label17.Visible = false;
                    label16.Visible = false;
                    label15.Visible = false;
                    label14.Visible = false;
                    label13.Visible = false;
                    label12.Visible = false;
                    label11.Visible = false;
                    label10.Visible = false;
                    playerDeck.Enqueue(playerCard);
                    playerDeck.Enqueue(CPUcard);
                    label3.Text = "You Win the round!";
                    cpuallin = false;
                }
                //CPU wins
                else if (playerCard < CPUcard)
                {
                    tie = false;
                    //adds all cards on the ground to CPU deck
                    while (CPUDownCards.Count > 0)
                    {
                        CPUDeck.Enqueue(CPUDownCards.Dequeue());
                    }
                    while (playerDownCards.Count > 0)
                    {
                        CPUDeck.Enqueue(playerDownCards.Dequeue());
                    }
                    //removes tie lables
                    label17.Visible = false;
                    label16.Visible = false;
                    label15.Visible = false;
                    label14.Visible = false;
                    label13.Visible = false;
                    label12.Visible = false;
                    label11.Visible = false;
                    label10.Visible = false;
                    CPUDeck.Enqueue(CPUcard);
                    CPUDeck.Enqueue(playerCard);
                    label3.Text = "CPU Wins the Round";
                    cpuallin = false;
                }
            }
            label4.Text = Convert.ToString(CPUDeck.Count);
            label5.Text = Convert.ToString(playerDeck.Count);
        }
        /* Simulates the game of war until there is a winner */
        private void simulate()
        {
            int counter = 1;
            label23.Visible = true;
            label24.Visible = true;
            label25.Visible = true;
            while (!complete)
            {
                if (cpuallin || plrallIn)
                {
                    allIn();
                }
                else
                {
                    play();
                }
                checkWinner();
                label25.Text = Convert.ToString(counter);
                counter++;
            }
        }
        /* Play button */
        private void button1_Click(object sender, EventArgs e)
        {
            if (cpuallin || plrallIn)
            {
                allIn();
            }
            else
            {
                play();
            }
            checkWinner();
        }
        /* New Game button */
        private void button2_Click(object sender, EventArgs e)
        {
            playerDeck.Clear();
            CPUDeck.Clear();
            playerDownCards.Clear();
            CPUDownCards.Clear();
            label1.Text = "";
            label2.Text = "";
            label4.Text = "";
            label5.Text = "";
            label23.Visible = false; 
            label24.Visible = false;
            label25.Visible = false;
            label17.Visible = false;
            label16.Visible = false;
            label15.Visible = false;
            label14.Visible = false;
            label13.Visible = false;
            label12.Visible = false;
            label11.Visible = false;
            label10.Visible = false;
            complete = false;
            button1.Enabled = true;
            button3.Enabled = true;
            initWar();
        }
        /* Simulate button */
        private void button3_Click(object sender, EventArgs e)
        {
            simulate();
            button3.Enabled = false;
        }
    }
}
