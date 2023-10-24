namespace BowlingTracker
{
    public class Game
    {
        private readonly int totalNoOfFrames;
        private readonly Frame[] frames;
        private readonly Scoring scoringStrat;
        private int currentFrameNum;
        private bool gameEnded;
 
        public Game()
        {
            totalNoOfFrames = 10;

            frames = new Frame[totalNoOfFrames];
            for (int i = 0; i < totalNoOfFrames - 1; i++)
            {
                frames[i] = new Frame(false);
            }
            frames[totalNoOfFrames-1] = new Frame(true);

            scoringStrat = new Scoring(frames);
            currentFrameNum = 1;
            gameEnded = false;
        }
       
        public int GetCurrentScore()
        {
            return scoringStrat.GetCurrentScore();
        }

        public int GetCurrentFrameNum()
        {
            return currentFrameNum;
        }

        public bool DidGameEnd()
        {
            return gameEnded;
        }
        
        public void SetNextRoll(int pinsKnocked)
        {
            if (currentFrameNum < totalNoOfFrames)
            {
                SetRegularRoll(pinsKnocked);
            }
            else 
            {
                SetLastFrameRolls(pinsKnocked);
            }
        }

        private void SetRegularRoll(int pinsKnocked)
        {
            Frame currentFrame = frames[currentFrameNum - 1];
            currentFrame.SetRoll(pinsKnocked);
            if (currentFrame.HasBeenCompleted())
            {
                currentFrameNum++;
            }
        }

        private void SetLastFrameRolls(int pinsKnocked)
        {
            Frame currentFrame = frames[currentFrameNum - 1];
            currentFrame.SetRollLastFrame(pinsKnocked);
            if (currentFrame.HasBeenCompleted()) 
            {
                gameEnded = true;
            }
        }

        /* Possible way to print scoreboard using a print strategy
        public void PrintScoreBoard()
        {
            scoringStrat.printScoreboard(printStrat);
        }
        */
    }
}