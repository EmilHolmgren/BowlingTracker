namespace BowlingTracker
{
    public class Frame
    {
        public enum Status {
        NONE, SPARE, STRIKE
    }

        private readonly bool isLastFrame;
        private int roll1;
        private int roll2;
        private int roll3;
        private int rollsDone;
        private Status status;
        private bool isCompleted;

        public Frame(bool isLastFrame)
        {
            roll1 = 0;
            roll2 = 0;
            roll3 = 0;
            rollsDone = 0;
            status = Status.NONE;
            isCompleted = false;
            this.isLastFrame = isLastFrame;
        }

        public Status GetStatus()
        {
            return status;
        }

        public bool IsLastFrame()
        {
            return isLastFrame;
        }

        public bool IsSpecial()
        {
            return status == Status.SPARE || status == Status.STRIKE;
        }

        public void SetRoll(int rollValue)
        {
            if (rollsDone == 0)
            {
                ValidateRoll(rollValue, 0, 10);
                roll1 = rollValue;
                CheckStrike();
            }
            else
            {
                ValidateRoll(rollValue, 0, 10 - roll1);
                roll2 = rollValue;
                CheckSpare();
            }

            rollsDone++;
            CheckFrameComplete();
        }

        public int GetRollsDone()
        {
            return rollsDone;
        }

        public void SetRollLastFrame(int rollValue)
        {
            if (rollsDone == 0)
            {
                SetRoll(rollValue);
            }
            else if (rollsDone == 1)
            {
                if (status != Status.STRIKE) {
                    SetRoll(rollValue);
                } else {
                    ValidateRoll(rollValue, 0, 10);
                    roll2 = rollValue;
                    CheckStrike();
                    rollsDone++;
                }
            }
            else 
            {
                int upperBoundRemainingPins = status != Status.NONE ? 10 : 10 - roll2;
                ValidateRoll(rollValue, 0, upperBoundRemainingPins);
                roll3 = rollValue;
                rollsDone++;
                CheckFrameComplete();
            }
        }

        public bool HasBeenCompleted()
        {
            return isCompleted;
        }

        public int GetRoll(int rollNo)
        {
            return rollNo switch
            {
                1 => roll1,
                2 => roll2,
                3 => roll3,
                _ => throw new ArgumentException("Ivalid roll number."),
            };
        }

        private static void ValidateRoll(int rollNo, int lowerBound, int upperBound)
        {
            bool isValid = lowerBound <= rollNo && rollNo <= upperBound;
            if (!isValid)
            {
                throw new ArgumentException("Invalid input. Value is out of bounds.");
            }
        }
        private void CheckSpare()
        {
            if (roll1 + roll2 == 10)
            {
                status = Status.SPARE;
            }
        }

        private void CheckStrike()
        {
            if (roll1 == 10)
            {
                status = Status.STRIKE;
            }
            //This should only apply to last frame
            else
            {
                status = Status.NONE;
            }
        }
        
        private void CheckFrameComplete()
        {
            if (!isLastFrame) {
                isCompleted = IsSpecial() || rollsDone == 2;
            }
            else
            {
                bool special = IsSpecial() && rollsDone == 3;
                bool notSpecial = !IsSpecial() && rollsDone == 2;
                isCompleted = special || notSpecial;
            }
        }
    }
}