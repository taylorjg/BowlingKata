namespace BowlingKata
{
    internal static class PlaceHolderExtensions
    {
        public static PlaceHolder With(this PlaceHolder placeHolder, int roll)
        {
            return new PlaceHolder(roll, placeHolder.FrameNumber);
        }

        public static bool IsEmpty(this PlaceHolder placeHolder)
        {
            return !placeHolder.Score.HasValue;
        }
    }
}
