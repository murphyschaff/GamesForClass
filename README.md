# GamesForClass
## By Murphy Schaff
## Version 0.6: Battleship Rework

# Use Instructions
Release Executable is located under Releases, GamesForClass_v0_6

# Game Instructions
## Tic Tac Toe
The Tic-Tac-Toe board is represented by 9 buttons in the center of the screen. To play, select your AI difficulty on the right, then choose one of the middle buttons to make your first move.
Once the game is complete, the score will be tallied on the right side. Select "New Game" to start another game against the AI, or select "Reset Score" to reset the score.
## War
On the left side of the screen are the player's and computer's decks of cards. The number represents how many cards are currently in each deck. To play a round, click "Play!". The player with the higher card wins the round, along with both of the cards. If there is a tie, on the right side of the window each player will place down 4 cards and a 5th card will be drawn to break the tie. The winner of that round gets all the cards on the table. Once the game is complete, you can select "New Game" to play again. You can also select "Simulate", to simulate a full round.
NOTE: Upon opening War and selecting "New Game", the program is randomly dealing out all cards. This takes a few seconds.
## Battleship
Upon opening Battleship, you are greeted with two battleship boards. On the left is the CPU's board. Here you will attempt to make guesses as to where the CPU's ships are located. On the right is your board, here you will place your ships and the CPU will try to guess where they are. First, you must place your ships. You can either press "Auto Ship Place" to have the ships placed for you, or you can place each one individually. You must place 1 Aircraft Carrier, 2 Battleships, 2 Destroyers, and 1 Submarine. To place your ships, first select the ship type from the dropdown menu. Then, select the tile you want to start your ship at. A few options will show up around the tile depending on the ship's size, then choose the direction you would like to place your ship in. If you want to change ship location, select the center button (marked as C). Once all your ships are placed, you can select "Start Game!" to start the game. Upon starting the game, you can choose any box on the CPU board to make a guess. If you hit a ship, a "H" with a yellow background will appear. If you sink a ship, all the boxes that contain the ship will turn red. Labels keep track of how many ships you have sunk, and how many ships the CPU has sunk. Clicking "Reset" at any time will reset the game.
## Yahtzee
Clicking "Roll" when it is your turn will roll each dice on the board. You can click any individual dice to place it into a "hold" state, where once you roll the dice again it will not be rolled. On the right side are the player's point sections. When you place dice into the hold, each possible combination that you can use will have a check box next to it. To place the dice into that section and get points, click the box and click the "Confirm" button. This will give the turn to the CPU. Click "CPU Turn" and "Next" to have the CPU run through its options. Click "Your Turn" to run your next turn. Once the game is complete, you can click either "Reset" button to reset the game. You can also click the "Reset" button at the bottom at any time to reset the game.
## Minesweeper
To start a game of minesweeper, select your difficulty on the bottom left. Click "Start Game" once you have selected the difficulty you want to play. Easy is a 8x8 board with 10 mines, Medium is a 16x16 board with 40 mines, and Hard is a 30x16 board with 99 mines. Select "Start Game" to spawn the board, and click a individual button to start. Right clicking a button will mark the space as a mine, where left clicking will reveal the value of the space. Left clicking on a bomb tile will cause the mine to explode, ending the game. To reset the game, click the "Reset" button. 

# Release Notes
## Release v0.6: Battleship Rework
+ Features
    + Battleship
        + Updated user interface to be more user friendly
        + Changed guess algorithm to be more readable, and more intelligent
        + Added more game information for user comprehension
        + Added Secret Addition

## Release v0.5.1
+ Features
    + Minesweeper
        + Secret Addition
+ Bug Fixes
    + Minesweeper
        + Changed win condition to make it hard to cheat
        + Fixed out-of-bounds error when winning
    + Yahtzee
        + Fixed bug where 0 points would be added to category even if valid points were present.

## Release v0.5: Minesweeper Release
+ Added Game Minesweeper
    + Added game functionality
    + Added 3 difficulty levels, easy, medium, hard

## Release v0.4: Yahtzee Release
+ Added Game Yahtzee
    + Added Game functonality
    + CPU player and algorithm
+ Bug Fixes for Battleship

## Release v0.3: Battleship Release
+ Added Game Battleship
    + Added Game functionality
    + CPU Player
    + Automatic Ship generation
    + CPU Guess algorithm