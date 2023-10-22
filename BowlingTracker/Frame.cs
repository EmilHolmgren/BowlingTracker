using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingTracker
{
    public class Frame
    {
        private int roll1;
        private int roll2;
        private int roll3;
        private readonly bool isLastFrame;
        private string specialFrame;

        public Frame(bool lastFrame)
        {
            //Why -1
            roll1 = -1;
            roll2 = -1;
            roll3 = -1;
            isLastFrame = lastFrame;
            specialFrame = "no";
        }

        public int GetRoll(int rollNumber)
        {
            return rollNumber switch
            {
                1 => roll1,
                2 => roll2,
                3 => roll3,
                _ => throw new ArgumentException("Input needs to be between 1-3."),
            };
        }

        public void SetRoll(int rollNo, int rollValue)
        {
            switch(rollNo)
            {
                case 1: 
                    roll1 = rollValue;
                    specialFrame = rollValue == 10 ? "strike" : "no";
                    break;
                case 2: 
                    roll2 = rollValue;
                    specialFrame = (rollValue + GetRoll(1))==10 ? "spare" : "no"; 
                    if (isLastFrame)
                    {
                        specialFrame = (rollValue + GetRoll(1)) >= 10 ? "special" : "no";
                    }                    
                    break;
                case 3: 
                    roll3 = rollValue;
                    break;
                default: throw new ArgumentException("Input needs to be between 1-3.");
            }
            
        }

        public bool IsLastFrame()
        {
            return isLastFrame;
        }

        internal string GetSpecial()
        {
            return specialFrame;
        }
    }
}