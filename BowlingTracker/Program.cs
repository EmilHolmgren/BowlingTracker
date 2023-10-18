using BowlingTracker;

internal class Program
{
    private static bool gameEnded;
    private static Frame[] scores;
    private static int currentScore;

    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, space!");
        Frame[] scores = new Frame[10];
        Frame frame1 = new(false);
        frame1.SetRoll(4);
        GetCurrentScore();
        gameEnded = false;
        SetNextRoll(3);
        while (!gameEnded)
        {
            gameEnded = DidGameEnd();
        }
        ResetGame();
    }

    private static bool DidGameEnd()
    {
        return scores[9].GetRoll(3)!=-1;
    }

    private static void SetNextRoll(int pinsKnocked)
    {
        throw new NotImplementedException();
    }

    private static void GetCurrentScore()
    {
        Console.WriteLine(currentScore);
    }

    private static void ResetGame()
    {
        scores = new Frame[10];
        currentScore = 0;
    }
}