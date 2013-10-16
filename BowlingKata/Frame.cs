namespace BowlingKata
{
    public class Frame
    {
        public int FrameNumber { get; set; }
        public int Score { get; private set; }
        public int RunningTotal { get; private set; }
        public Roll FirstRoll { get; private set; }
        public Roll SecondRoll { get; private set; }
        public Roll ThirdRoll { get; private set; }

        private enum FrameState
        {
            Empty,
            Regular,
            SpareNeedOneMore,
            StrikeNeedTwoMore,
            StrikeNeedOneMore,
            Complete
        }

        private FrameState _frameState = FrameState.Empty;

        public Frame(int frameNumber)
        {
            FrameNumber = frameNumber;
        }

        public bool IsLastFrame
        {
            get { return FrameNumber == 10; }
        }

        public void SetRunningTotal(int runningTotal)
        {
            RunningTotal = runningTotal;
        }

        public bool ProcessRoll(int roll)
        {
            var consumedRoll = false;

            switch (_frameState)
            {
                case FrameState.Empty:
                    FirstRoll = new Roll(roll);
                    Score += roll;
                    _frameState = (roll == 10) ? FrameState.StrikeNeedTwoMore : FrameState.Regular;
                    consumedRoll = true;
                    break;

                case FrameState.Regular:
                    var isSpare = (FirstRoll.Value + roll == 10);
                    SecondRoll = new Roll(roll, isSpare);
                    Score += roll;
                    _frameState = (isSpare) ? FrameState.SpareNeedOneMore : FrameState.Complete;
                    consumedRoll = true;
                    break;

                case FrameState.SpareNeedOneMore:
                    if (IsLastFrame)
                    {
                        ThirdRoll = new Roll(roll);
                    }
                    Score += roll;
                    _frameState = FrameState.Complete;
                    break;

                case FrameState.StrikeNeedTwoMore:
                    if (IsLastFrame)
                    {
                        SecondRoll = new Roll(roll);
                    }
                    Score += roll;
                    _frameState = FrameState.StrikeNeedOneMore;
                    break;

                case FrameState.StrikeNeedOneMore:
                    if (IsLastFrame)
                    {
                        ThirdRoll = new Roll(roll);
                    }
                    Score += roll;
                    _frameState = FrameState.Complete;
                    break;

                case FrameState.Complete:
                    break;
            }

            return consumedRoll;
        }
    }
}
