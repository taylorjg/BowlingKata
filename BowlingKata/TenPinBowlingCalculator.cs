namespace BowlingKata
{
    // http://codingdojo.org/cgi-bin/wiki.pl?KataBowling
    // http://en.wikipedia.org/wiki/Ten-pin_bowling

    public class TenPinBowlingCalculator
    {
        public int Calculate(params int[] rolls)
        {
            var frameNumber = 1;
            var placeHolderCollection = new PlaceHolderCollection();
            int? previousRoll = null;
            var total = 0;

            foreach (var roll in rolls)
            {
                var index = placeHolderCollection.FindFirstEmptyPlaceHolder();

                if (index >= 0)
                {
                    var frameNumberOfFirstEmptyPlaceHolder = placeHolderCollection[index].FrameNumber;
                    placeHolderCollection.UpdatePlaceHolder(index, roll);
                    total += roll;

                    index = placeHolderCollection.FindFirstEmptyPlaceHolder();

                    if (index >= 0)
                    {
                        var frameNumberOfSecondEmptyPlaceHolder = placeHolderCollection[index].FrameNumber;
                        if (frameNumberOfSecondEmptyPlaceHolder > frameNumberOfFirstEmptyPlaceHolder)
                        {
                            placeHolderCollection.UpdatePlaceHolder(index, roll);
                            total += roll;
                        }
                    }
                }

                if (frameNumber <= 10)
                {
                    total += roll;

                    if (roll == 10)
                    {
                        placeHolderCollection.AddPlaceHolder(frameNumber);
                        placeHolderCollection.AddPlaceHolder(frameNumber);
                        previousRoll = null;
                        frameNumber++;
                    }
                    else
                    {
                        if (previousRoll.HasValue)
                        {
                            if (previousRoll + roll == 10)
                            {
                                placeHolderCollection.AddPlaceHolder(frameNumber);
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
    }
}
