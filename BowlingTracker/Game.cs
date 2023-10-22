using System.Globalization;
using System.Runtime.CompilerServices;

namespace BowlingTracker
{
    public class Game
    {
        private int[] scoreCumulative;
        private Frame currentFrame;
        private int currentFrameNum;
        private int currentRollNum;
        private Frame[] frames;
        private int carryStrike;
        private bool carrySpare;
 
        public Game()
        {
            SetupGame();
        }
        
        private void SetupGame()
        {
            currentRollNum = 1;
            scoreCumulative = new int[10];
            carryStrike = 0;
            carrySpare = false;
            
            frames = new Frame[10];

            for (int i = 0; i < 9; i++)
            {
                frames[i] = new Frame(false);
            }

            frames[9] = new Frame(true);
            currentFrame = frames[0];
            currentFrameNum = 1;
        }

        public void SetNextRoll(int pinsKnocked)
        {
            //Check for correct input
            if (pinsKnocked < 0 || 10 < pinsKnocked) {
                throw new ArgumentException("You can only knock down 0-10 pins.");
            }

            currentFrame.SetRoll(currentRollNum, pinsKnocked);
            
            UpdateScoreCheck();
                        
            //Update current frame and current roll and set special-flags
            if (currentRollNum==1) 
            {
                if (currentFrame.IsLastFrame())
                {
                    currentRollNum =2;
                    bool isStrike = currentFrame.GetSpecial()=="strike";
                    carryStrike += isStrike ? 1 : 0;
                }
                else 
                {
                    bool isStrike = currentFrame.GetSpecial()=="strike";
                    carryStrike += isStrike ? 1 : 0;
                    currentRollNum = isStrike ? 1 : 2;
                    currentFrameNum += currentRollNum==2 ? 0 : 1;
                    currentFrame = frames[currentFrameNum-1];
                }
            }
            else if (currentRollNum==2) 
            {
                if (currentFrame.IsLastFrame())
                {
                    currentRollNum = 3;
                }
                else 
                {
                    carrySpare = currentFrame.GetSpecial()=="spare";
                    currentRollNum = 1; 
                    currentFrameNum +=1;
                    currentFrame = frames[currentFrameNum-1];
                }
            }
        }

        //Checks if scores should be updated, and calls update if needed.
        private void UpdateScoreCheck()
        {
            string special = currentFrame.GetSpecial();
                        
            //1st frame
            if (currentFrameNum == 1)
            {
                if (currentRollNum == 2 && special=="no") 
                {
                    UpdateFrameScore(currentFrameNum);
                }
            }
            //2-9th frames
            else if (!currentFrame.IsLastFrame())
            {
                //1st roll
                if (currentRollNum == 1) 
                {
                    if (carrySpare) 
                    {
                        UpdateFrameScore(currentFrameNum-1);
                        carrySpare = false;
                    }
                    else if (carryStrike == 2)
                    {
                        UpdateFrameScore(currentFrameNum-2);
                        carryStrike = 1;
                    }
                }
                //2nd roll
                else if (currentRollNum == 2) 
                {
                    if (carryStrike == 1) 
                    {
                        UpdateFrameScore(currentFrameNum-1);
                        carryStrike = 0;
                    }
                    if (special == "no") 
                    {
                        UpdateFrameScore(currentFrameNum);
                    }
                }
            }
            //last frame
            else 
            {
                if (currentRollNum == 1) 
                {
                    if (carryStrike == 2)
                    {
                        UpdateFrameScore(currentFrameNum-2);
                        carryStrike = 1;
                    }
                    if (carrySpare) 
                    {
                        UpdateFrameScore(currentFrameNum-1);
                        carrySpare = false;
                    }
                }
                else if (currentRollNum == 2)
                {
                    if (carryStrike > 0) 
                    {
                        UpdateFrameScore(currentFrameNum-1);
                        carryStrike--;
                    }
                    if (special == "no") {
                        UpdateFrameScore(currentFrameNum);
                    }
                }
                else 
                {
                    UpdateFrameScore(currentFrameNum);
                }
            }
        }

        //Will only be called, when a frame score is certain to be updated.
        private void UpdateFrameScore(int frameNum)
        {
            //Add score from previous frames if not first frame.
            scoreCumulative[frameNum-1] += frameNum == 1 ? 0 : scoreCumulative[frameNum-2];
            
            Frame frame = frames[frameNum-1];
            string special = frame.GetSpecial();
            int firstRoll = frame.GetRoll(1);

            //Special case last frame
            if (frame.IsLastFrame())
            {
                scoreCumulative[frameNum-1] += firstRoll + frame.GetRoll(2);
                if (special!="no") 
                {
                    scoreCumulative[frameNum-1] += frame.GetRoll(3);
                }
            }
            else
            {
                switch (special) 
                {
                    case "no":
                        scoreCumulative[frameNum-1] += firstRoll + frame.GetRoll(2);
                        break;
                    case "spare": 
                        scoreCumulative[frameNum-1] += firstRoll + frame.GetRoll(2) + frames[frameNum].GetRoll(1);
                        break;
                    case "strike":
                        Frame nextFrame = frames[frameNum];
                        int firstRollNextFrame = nextFrame.GetRoll(1);
                        bool isNextFrameStrike = nextFrame.GetSpecial()=="strike";

                        //Special case for second last frame (9th) and if no strike in next frame
                        if (nextFrame.IsLastFrame() || !isNextFrameStrike) 
                        {
                            scoreCumulative[frameNum-1] += firstRoll + firstRollNextFrame + nextFrame.GetRoll(2);
                        }
                        else
                        {
                            scoreCumulative[frameNum-1] += firstRoll + firstRollNextFrame + frames[frameNum+1].GetRoll(1);
                        }
                        break;
                    default: 
                        throw new Exception("Unknown \'special\'.");
                }
            }
        }

        public int GetCurrentScore(int frameNum)
        {
            return scoreCumulative[frameNum-1];
        }

        public int GetCurrentFrameNum()
        {
            return currentFrameNum;
        }

        public int GetCurrentRoll()
        {
            return currentRollNum;
        }

        public bool DidGameEnd()
        {
            Frame frame = frames[currentFrameNum-1];
            if (frame.IsLastFrame()) 
            {
                int roll1 = frame.GetRoll(1);
                int roll2 = frame.GetRoll(2);

                if (frame.GetRoll(3)!=-1)
                {
                    return true;
                }
                else if (roll1 >= 0 && roll2 >= 0 && roll1+roll2< 10)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetLatestScore()
        {
            return scoreCumulative.Max();
        }

        internal void PrintScoreBoard()
        {
            string horizontalLine = "";
            string frameNumbers = "";
            string rollScores = "";
            string totalScore = "";
            for (int i=0; i<currentFrameNum; i++) {
                horizontalLine += "------------";
                frameNumbers += "| Frame: " + currentFrameNum + " ";
                bool shouldGetRoll1 = frames[i].GetRoll(1)!=-1;
                bool shouldGetRoll2 = frames[i].GetRoll(2)!=-1;
                string roll1 = shouldGetRoll1 ? frames[i].GetRoll(1).ToString() : " ";
                string roll2 = shouldGetRoll2 ? frames[i].GetRoll(2).ToString() : " ";
                rollScores += "| " + roll1 + " | " + roll2 + " ";
                string score = currentRollNum !=1 ? scoreCumulative[i].ToString() : " ";
                totalScore += "| " + score + "   ";
            }
            Console.WriteLine(horizontalLine);
            Console.WriteLine(frameNumbers);
            Console.WriteLine(horizontalLine);
            Console.WriteLine(rollScores);
            Console.WriteLine(horizontalLine);
            Console.WriteLine(totalScore);
            Console.WriteLine(horizontalLine);
        }
    }
}