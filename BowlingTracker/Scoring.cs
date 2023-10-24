using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingTracker
{
    public class Scoring
    {
        private readonly Frame[] frames;

        public Scoring(Frame[] frames)
        {
            this.frames = frames;
        }

        internal int GetCurrentScore()
        {            
            List<Frame> completedFrames = GetCompletedFrames();
            Queue<int> allRolls = GetAllRollsConsecutive();
            int totalScore = 0;

            foreach (Frame f in completedFrames)
            {
                totalScore += TotalFrameScoreIfPossible(allRolls, f.GetStatus(), f.IsLastFrame());
            }

            return totalScore;
        }

        private static int TotalFrameScoreIfPossible(Queue<int> allRolls, Frame.Status status, bool isLastFrame)
        {
            int frameScore = 0;
            
            if (isLastFrame) {
                frameScore += allRolls.Dequeue() + allRolls.Dequeue();
                allRolls.TryDequeue(out int bonusRoll3);
                return frameScore += bonusRoll3;
            }

            switch (status)
            {
                case Frame.Status.NONE:
                    frameScore += allRolls.Dequeue() + allRolls.Dequeue();
                    break;
                case Frame.Status.SPARE:
                    if (allRolls.Count < 3)
                    {
                        break;
                    }
                    int firstRoll = allRolls.Dequeue();
                    int secondRoll = allRolls.Dequeue();
                    allRolls.TryPeek(out int bonusValue);

                    frameScore += firstRoll + secondRoll + bonusValue;
                    break;
                case Frame.Status.STRIKE:
                    if (allRolls.Count < 3) {
                    break;
                    }
                    int strikScore = allRolls.Dequeue();
                    int bonusValue1 = allRolls.ElementAt(0);
                    int bonusValue2 = allRolls.ElementAt(1);

                    frameScore += strikScore + bonusValue1 + bonusValue2;
                    break;
            }
            return frameScore;
        }

        private List<Frame> GetCompletedFrames()
        {
            List<Frame> completedFrames = new();
            for (int i = 0; i < frames.Length; i++)
            {
                if (frames[i].HasBeenCompleted())
                {
                    completedFrames.Add(frames[i]);
                }
            }
            return completedFrames;
        }

        private Queue<int> GetAllRollsConsecutive()
        {
            Queue<int> allRolls = new();

            for (int i = 0; i < frames.Length; i++)
            {
                for (int j = 0; j < frames[i].GetRollsDone(); j++)
                {
                    allRolls.Enqueue(frames[i].GetRoll(j+1));
                }
            }
            return allRolls;
        }
    }
}