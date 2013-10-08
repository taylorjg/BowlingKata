using System;
using System.Collections.Generic;

namespace BowlingKata
{
    public class TenPinBowlingCalculator
    {
        private const string StrikeSymbol = "X";
        private const string SpareSymbol = "/";
        private const string GutterSymbol = "-";

        public string RollsToString(params int[] rolls)
        {
            var frameNumber = 1;
            var placeHolders = new List<PlaceHolder>();
            int? previousRoll = null;
            var result = string.Empty;

            foreach (var roll in rolls)
            {
                if (frameNumber > 1)
                {
                    var fillInScore = FillInPlaceHolder(placeHolders, roll);
                    if (fillInScore.HasValue && frameNumber > 10)
                    {
                        result += RollToString(roll);
                    }
                }

                if (frameNumber > 2)
                {
                    var fillInScore = FillInPlaceHolder(placeHolders, roll);
                    if (fillInScore.HasValue && frameNumber > 11)
                    {
                        result += RollToString(roll);
                    }
                }

                if (frameNumber <= 10)
                {
                    if (RollIsAStrike(roll))
                    {
                        result += RollToString(roll);
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
                                result += SpareSymbol;
                                placeHolders.Add(new PlaceHolder(frameNumber));
                            }
                            else
                            {
                                result += RollToString(roll);
                            }
                            previousRoll = null;
                            frameNumber++;
                        }
                        else
                        {
                            result += RollToString(roll);
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
                if (frameNumber > 1)
                {
                    var fillInScore = FillInPlaceHolder(placeHolders, roll);
                    total += (fillInScore.HasValue) ? fillInScore.Value : 0;
                }

                if (frameNumber > 2)
                {
                    var fillInScore = FillInPlaceHolder(placeHolders, roll);
                    total += (fillInScore.HasValue) ? fillInScore.Value : 0;
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

        private static string RollToString(int roll)
        {
            if (roll == 10)
            {
                return StrikeSymbol;
            }

            if (roll == 0)
            {
                return GutterSymbol;
            }

            return Convert.ToString(roll);
        }

        private static bool RollIsAStrike(int roll)
        {
            return roll == 10;
        }

        private static bool PreviousRollAndThisRollMakeASpare(int? previousRoll, int roll)
        {
            return previousRoll + roll == 10;
        }

        private static int? FillInPlaceHolder(List<PlaceHolder> placeHolders, int roll)
        {
            var index = placeHolders.FindIndex(PlaceHolderExtensions.IsEmpty);

            if (index < 0)
            {
                return null;
            }

            placeHolders[index] = placeHolders[index].With(roll);
            return roll;
        }
    }
}
