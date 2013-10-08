using System.Collections.Generic;
using System.Linq;

namespace BowlingKata
{
    public class TenPinBowlingCalculator
    {
        public int Calculate(params int[] rolls)
        {
            var total = 0;
            var frameCount = 1;
            int? previousRoll = null;
            var placeHolders = new List<PlaceHolder>();

            foreach (var roll in rolls)
            {
                if (frameCount > 1)
                {
                    var placeHolder = placeHolders.FirstOrDefault(ph => ph.Score == -1);
                    if (placeHolder != null)
                    {
                        placeHolder.Score = roll;
                    }
                }

                if (frameCount > 2)
                {
                    var placeHolder = placeHolders.FirstOrDefault(ph => ph.Score == -1);
                    if (placeHolder != null)
                    {
                        placeHolder.Score = roll;
                    }
                }

                if (frameCount <= 10)
                {
                    total += roll;

                    if (roll == 10)
                    {
                        placeHolders.Add(new PlaceHolder(frameCount));
                        placeHolders.Add(new PlaceHolder(frameCount));
                        previousRoll = null;
                        frameCount++;
                    }
                    else
                    {
                        if (previousRoll.HasValue)
                        {
                            if (previousRoll + roll == 10)
                            {
                                placeHolders.Add(new PlaceHolder(frameCount));
                            }
                            previousRoll = null;
                            frameCount++;
                        }
                        else
                        {
                            previousRoll = roll;
                        }
                    }
                }
            }

            total += placeHolders.Sum(ph => ph.Score);

            return total;
        }

        private class PlaceHolder
        {
            public int Score { get; set; }
            public int FrameNumber { get; private set; }

            public PlaceHolder(int frameNumber)
            {
                Score = -1;
                FrameNumber = frameNumber;
            }
        }
    }
}
