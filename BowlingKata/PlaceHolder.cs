namespace BowlingKata
{
    public class PlaceHolder
    {
        public PlaceHolder(int frameNumber)
        {
            FrameNumber = frameNumber;
        }

        public PlaceHolder(int score, int frameNumber)
        {
            Score = score;
            FrameNumber = frameNumber;
        }

        public int? Score { get; private set; }
        public int FrameNumber { get; private set; }
    }
}
