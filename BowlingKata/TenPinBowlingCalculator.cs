using System.Collections.Generic;

namespace BowlingKata
{
    // http://codingdojo.org/cgi-bin/wiki.pl?KataBowling
    // http://en.wikipedia.org/wiki/Ten-pin_bowling

    public class TenPinBowlingCalculator
    {
        public int Calculate(params int[] rolls)
        {
            var frameNumber = 1;
            var placeHolders = new List<PlaceHolder>();
            int? previousRoll = null;
            var total = 0;

            foreach (var roll in rolls)
            {
                var index = FindEmptyPlaceHolder(placeHolders);

                if (index >= 0)
                {
                    var frameNumberOfFirstEmptyPlaceHolder = placeHolders[index].FrameNumber;
                    FillInEmptyPlaceHolder(placeHolders, index, roll);
                    total += roll;

                    index = FindEmptyPlaceHolder(placeHolders);

                    if (index >= 0)
                    {
                        var frameNumberOfSecondEmptyPlaceHolder = placeHolders[index].FrameNumber;
                        if (frameNumberOfSecondEmptyPlaceHolder > frameNumberOfFirstEmptyPlaceHolder)
                        {
                            FillInEmptyPlaceHolder(placeHolders, index, roll);
                            total += roll;
                        }
                    }
                }

                if (frameNumber <= 10)
                {
                    total += roll;

                    if (roll == 10)
                    {
                        placeHolders.Add(new PlaceHolder(frameNumber));
                        placeHolders.Add(new PlaceHolder(frameNumber));
                        previousRoll = null;
                        frameNumber++;
                    }
                    else
                    {
                        if (previousRoll.HasValue)
                        {
                            if (previousRoll + roll == 10)
                            {
                                placeHolders.Add(new PlaceHolder(frameNumber));
                            }
                            previousRoll = null;
                            frameNumber++;
                        }
                        else
                        {
                            previousRoll = roll;
                        }
                    }
                }
            }

            return total;
        }

        private static int FindEmptyPlaceHolder(List<PlaceHolder> placeHolders)
        {
            return placeHolders.FindIndex(PlaceHolderExtensions.IsEmpty);
        }

        private static void FillInEmptyPlaceHolder(IList<PlaceHolder> placeHolders, int index, int roll)
        {
            placeHolders[index] = placeHolders[index].With(roll);
        }
    }
}
