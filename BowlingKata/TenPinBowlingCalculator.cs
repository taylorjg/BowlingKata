using System.Collections.Generic;

namespace BowlingKata
{
    // http://codingdojo.org/cgi-bin/wiki.pl?KataBowling
    // http://en.wikipedia.org/wiki/Ten-pin_bowling

    public class TenPinBowlingCalculator
    {
        private const string StrikeSymbol = "X";
        private const string SpareSymbol = "/";
        private const string GutterSymbol = "-";

        private static string RollToString(int roll)
        {
            switch (roll)
            {
                case 0:
                    return GutterSymbol;

                case 10:
                    return StrikeSymbol;

                default:
                    return System.Convert.ToString(roll);
            }
        }

        public string RollsToString(params int[] rolls)
        {
            var frameNumber = 1;
            var placeHolders = new List<PlaceHolder>();
            int? previousRoll = null;
            var result = string.Empty;

            foreach (var roll in rolls)
            {
                var rollString = RollToString(roll);

                var index = FindEmptyPlaceHolder(placeHolders);

                if (index >= 0 && frameNumber > 10)
                {
                    var frameNumberOfFirstEmptyPlaceHolder = placeHolders[index].FrameNumber;
                    FillInEmptyPlaceHolder(placeHolders, index, roll);
                    result += rollString;

                    index = FindEmptyPlaceHolder(placeHolders);

                    if (index >= 0 && frameNumber > 11)
                    {
                        var frameNumberOfSecondEmptyPlaceHolder = placeHolders[index].FrameNumber;
                        if (frameNumberOfSecondEmptyPlaceHolder > frameNumberOfFirstEmptyPlaceHolder)
                        {
                            FillInEmptyPlaceHolder(placeHolders, index, roll);
                            result += rollString;
                        }
                    }
                }

                if (frameNumber <= 10)
                {
                    if (roll == 10)
                    {
                        result += rollString;
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
                                result += SpareSymbol;
                                placeHolders.Add(new PlaceHolder(frameNumber));
                            }
                            else
                            {
                                result += rollString;
                            }
                            previousRoll = null;
                            frameNumber++;
                        }
                        else
                        {
                            result += rollString;
                            previousRoll = roll;
                        }
                    }
                }
            }

            return result;
        }

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
