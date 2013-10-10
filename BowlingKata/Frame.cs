namespace BowlingKata
{
    // http://www.apollostemplates.com/pdf-templates/bowling-templates/Bowling_Score_Sheet_3x6_black.pdf

    public class Frame
    {
        private const int LastFrameNumber = 10;

        private Frame(
            int frameNumber,
            int score,
            int runningTotal,
            string firstRoll,
            string secondRoll = RollSymbols.BlankSymbol,
            string thirdRoll = RollSymbols.BlankSymbol)
        {
            FrameNumber = frameNumber;
            Score = score;
            RunningTotal = runningTotal;
            FirstRoll = firstRoll;
            SecondRoll = secondRoll;
            ThirdRoll = thirdRoll;
        }

        public int FrameNumber { get; private set; }
        public int Score { get; set; }
        public int RunningTotal { get; set; }
        public string FirstRoll { get; set; }
        public string SecondRoll { get; set; }
        public string ThirdRoll { get; set; }

        public bool IsLast {
            get
            {
                return FrameNumber == LastFrameNumber;
            }
        }

        public static Frame UninterestingFrame(int frameNumber, int score, int runningTotal, string firstRoll, string secondRoll)
        {
            return new Frame(frameNumber, score, runningTotal, firstRoll, secondRoll);
        }

        public static Frame SpareFrameNotLast(int frameNumber, int score, int runningTotal, string firstRoll)
        {
            return new Frame(frameNumber, score, runningTotal, firstRoll, RollSymbols.SpareSymbol);
        }

        public static Frame StrikeFrameNotLast(int frameNumber, int score, int runningTotal)
        {
            return new Frame(frameNumber, score, runningTotal, RollSymbols.StrikeSymbol);
        }

        public static Frame SpareFrameLast(int score, int runningTotal, string firstRoll, string bonusBall)
        {
            return new Frame(LastFrameNumber, score, runningTotal, firstRoll, RollSymbols.SpareSymbol, bonusBall);
        }

        public static Frame StrikeFrameLast(int score, int runningTotal, string firstBonusBall, string secondBonusBall)
        {
            return new Frame(LastFrameNumber, score, runningTotal, RollSymbols.StrikeSymbol, firstBonusBall, secondBonusBall);
        }
    }
}
