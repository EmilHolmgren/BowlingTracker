To run BowlingTracker go to the directory where Run.cs is located.

type: dotnet run Run.cs

Follow instruction in console to complete scoring your bowling game.

To run xUnit tests, go to the directory .\Tests\.

Type: dotnet test

Ideas behind the implementation


Comments:
- A framescore should only be updated after it has been concluded, i.e. no update after the first roll.
- One larger 'Game' class handles everything (except w.r.t. Frame). 
	Reason: Small and simple project.
	Idea: 	Have other classes take up responsibility.
		E.g. create a Score-class that would be responsible for updating score.
		Could be used in strategy pattern if other ways of scoring was desired for different game types.
- Length of a game could be setup as a parameter when creating new game, if variations were desired.
	
- Need to test the impl. only allows a max knocked down pins of 10 per frame (30 in last frame).
- Seperate tests into smaller test using AAA.

