using System.Collections.Generic;

namespace BowlingKata
{
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
                if (frameNumber > 1)
                {
                    total += FillInPlaceHolder(placeHolders, roll);
                }

                if (frameNumber > 2)
                {
                    total += FillInPlaceHolder(placeHolders, roll);
                }

                if (frameNumber <= 10)
                {
                    total += roll;

                    if (RollIsAStrike(roll))
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
                            if (PreviousRollAndThisRollMakeASpare(previousRoll, roll))
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

        private static bool RollIsAStrike(int roll)
        {
            return roll == 10;
        }

        private static bool PreviousRollAndThisRollMakeASpare(int? previousRoll, int roll)
        {
            return previousRoll + roll == 10;
        }

        private static int FillInPlaceHolder(List<PlaceHolder> placeHolders, int roll)
        {
            var index = placeHolders.FindIndex(PlaceHolderExtensions.IsEmpty);

            if (index < 0)
            {
                return 0;
            }

            placeHolders[index] = placeHolders[index].With(roll);
            return roll;
        }
    }
}
