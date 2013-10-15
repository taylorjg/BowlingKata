namespace BowlingKata
{
    public class Frame2
    {
        public int FrameNumber { get; set; }
        public int Score { get; private set; }
        //public int RunningTotal { get; private set; }
        public Roll FirstRoll { get; private set; }
        public Roll SecondRoll { get; private set; }
        private int _placeHolderCount;

        public Frame2(int frameNumber)
        {
            FrameNumber = frameNumber;
        }

        public bool ProcessRoll(int roll)
        {
            if (FirstRoll == null)
            {
                FirstRoll = new Roll(roll);
                Score += roll;
            }
            else if (SecondRoll == null)
            {
                var isSpare = (FirstRoll.Value + roll == 10);
                if (isSpare)
                {
                    SecondRoll = new Roll(roll, true);
                    _placeHolderCount = 1;
                }
                else
                {
                    SecondRoll = new Roll(roll);
                }
                Score += roll;
            }
            else if (_placeHolderCount > 0)
            {
                Score += roll;
                _placeHolderCount--;
            }

            return FirstRoll == null || SecondRoll == null || _placeHolderCount > 0;
        }
    }
}
