To run BowlingTracker go to the directory where App.cs is located.

Type: dotnet run

Follow instructions in console to complete scoring your bowling game.

To run xUnit tests, go to the directory .\Tests\.

Type: dotnet test

Comments on implementation:
A framescore should only be updated after it has been concluded, i.e. no update after the first roll.

The 'Game' class is responsible for keeping track of the flow of the game. It handles:
	- which frame currently is being played.
	- check for when the game has ended.

The 'Frame' class is representing one frame of the game. It is responsible for:
	- updating the different rolls of the frame.
	- updating the status of the frame if it becomes spare or strike.
	- knowing when a frame is completed.

The 'Scoring' class is a specific scoring strategy that has the responsibility to:
	- compute the current score of the game.
	- compute the score based on official rules.

The length of a game is fixed at 10, but could be setup as a parameter when creating a new game if variations were desired.
Console only UI. The 'Game' class could be changed to implement a print-strategy/UI for a better UI.