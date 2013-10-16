namespace BowlingKata
{
    public class Frame
    {
        public int FrameNumber { get; private set; }
        public int Score { get; private set; }
        public int RunningTotal { get; private set; }
        public Roll FirstRoll { get; private set; }
        public Roll SecondRoll { get; private set; }
        public Roll ThirdRoll { get; private set; }

        private enum FrameState
        {
            ReadyForFirstRoll,
            ReadyForSecondRoll,
            SpareNeedOneMore,
            StrikeNeedTwoMore,
            StrikeNeedOneMore,
            Complete
        }

        private FrameState _frameState = FrameState.ReadyForFirstRoll;

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
            var rollBelongsToThisFrame = false;

            switch (_frameState)
            {
                case FrameState.ReadyForFirstRoll:
                    FirstRoll = new Roll(roll);
                    Score += roll;
                    _frameState = (roll == 10) ? FrameState.StrikeNeedTwoMore : FrameState.ReadyForSecondRoll;
                    rollBelongsToThisFrame = true;
                    break;

                case FrameState.ReadyForSecondRoll:
                    var isSpare = (FirstRoll.Value + roll == 10);
                    SecondRoll = new Roll(roll, isSpare);
                    Score += roll;
                    _frameState = (isSpare) ? FrameState.SpareNeedOneMore : FrameState.Complete;
                    rollBelongsToThisFrame = true;
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

            return rollBelongsToThisFrame;
        }
    }
}
