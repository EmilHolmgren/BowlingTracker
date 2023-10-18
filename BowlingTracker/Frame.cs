using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingTracker
{
    public class Frame
    {
        private static bool isLastFrame;
        private int roll1;
        private int roll2;
        private int roll3;

        public Frame(bool lastFrame)
        {
            roll1 = -1;
            roll2 = -1;
            roll3 = -1;
            isLastFrame = lastFrame;
        }

        internal int GetRoll(int rollNumber)
        {
            return rollNumber switch
            {
                1 => roll1,
                2 => roll2,
                3 => roll3,
                _ => throw new Exception(),
            };
        }

        internal void SetRoll(int v)
        {
            throw new NotImplementedException();
        }
    }
}