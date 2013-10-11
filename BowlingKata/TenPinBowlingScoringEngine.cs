using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingKata
{
    public class TenPinBowlingScoringEngine
    {
        // TODO: this code is horrid - major refactoring required.
        public void ProcessRolls(Action<Frame> handleFrame, params int[] rolls)
        {
            var frameNumber = 1;
            var placeHolders = new List<PlaceHolder>();
            int? previousRoll = null;
            int? firstRollOfMostRecentSpare = null;
            var runningTotal = 0;

            foreach (var roll in rolls)
            {
                var index = FindEmptyPlaceHolder(placeHolders);

                if (index >= 0)
                {
                    var frameNumberOfFirstEmptyPlaceHolder = placeHolders[index].FrameNumber;
                    FillInEmptyPlaceHolder(placeHolders, index, roll);

                    if (CountOfEmptyPlaceHoldersForFrameNumber(placeHolders, frameNumberOfFirstEmptyPlaceHolder) == 0)
                    {
                        Frame frame;
                        if (CountOfPlaceHoldersForFrameNumber(placeHolders, frameNumberOfFirstEmptyPlaceHolder) == 2)
                        {
                            var firstBonusBall = placeHolders[index - 1].Score.Value;
                            var secondBonusBall = placeHolders[index].Score.Value;
                            var score = 10 + firstBonusBall + secondBonusBall;
                            runningTotal += score;
                            if (frameNumberOfFirstEmptyPlaceHolder == 10)
                            {
                                frame = Frame.StrikeFrameLast(score, runningTotal, RollToString(firstBonusBall), RollToString(secondBonusBall));
                            }
                            else
                            {
                                frame = Frame.StrikeFrameNotLast(frameNumberOfFirstEmptyPlaceHolder, score, runningTotal);
                            }
                        }
                        else
                        {
                            var bonusBall = placeHolders[index].Score.Value;
                            var score = 10 + bonusBall;
                            runningTotal += score;
                            if (frameNumberOfFirstEmptyPlaceHolder == 10)
                            {
                                frame = Frame.SpareFrameLast(score, runningTotal, RollToString(firstRollOfMostRecentSpare.Value), RollToString(bonusBall));
                            }
                            else
                            {
                                frame = Frame.SpareFrameNotLast(frameNumberOfFirstEmptyPlaceHolder, score, runningTotal, RollToString(firstRollOfMostRecentSpare.Value));
                            }
                        }
                        handleFrame(frame);
                    }

                    index = FindEmptyPlaceHolder(placeHolders);

                    if (index >= 0)
                    {
                        var frameNumberOfSecondEmptyPlaceHolder = placeHolders[index].FrameNumber;
                        if (frameNumberOfSecondEmptyPlaceHolder > frameNumberOfFirstEmptyPlaceHolder)
                        {
                            FillInEmptyPlaceHolder(placeHolders, index, roll);

                            if (CountOfEmptyPlaceHoldersForFrameNumber(placeHolders, frameNumberOfSecondEmptyPlaceHolder) == 0)
                            {
                                Frame frame;
                                if (CountOfPlaceHoldersForFrameNumber(placeHolders, frameNumberOfSecondEmptyPlaceHolder) == 2)
                                {
                                    var firstBonusBall = placeHolders[index - 1].Score.Value;
                                    var secondBonusBall = placeHolders[index].Score.Value;
                                    var score = 10 + firstBonusBall + secondBonusBall;
                                    runningTotal += score;
                                    if (frameNumberOfSecondEmptyPlaceHolder == 10)
                                    {
                                        frame = Frame.StrikeFrameLast(score, runningTotal, RollToString(firstBonusBall), RollToString(secondBonusBall));
                                    }
                                    else
                                    {
                                        frame = Frame.StrikeFrameNotLast(frameNumberOfSecondEmptyPlaceHolder, score, runningTotal);
                                    }
                                }
                                else
                                {
                                    var bonusBall = placeHolders[index].Score.Value;
                                    var score = 10 + bonusBall;
                                    runningTotal += score;
                                    if (frameNumberOfSecondEmptyPlaceHolder == 10)
                                    {
                                        frame = Frame.SpareFrameLast(score, runningTotal, RollToString(firstRollOfMostRecentSpare.Value), RollToString(bonusBall));
                                    }
                                    else
                                    {
                                        frame = Frame.SpareFrameNotLast(frameNumberOfSecondEmptyPlaceHolder, score, runningTotal, RollToString(firstRollOfMostRecentSpare.Value));
                                    }
                                }
                                handleFrame(frame);
                            }
                        }
                    }
                }

                if (frameNumber <= 10)
                {
                    if (roll == 10)
                    {
                        placeHolders.Add(new PlaceHolder(frameNumber));
                        placeHolders.Add(new PlaceHolder(frameNumber));
                        previousRoll = null;
                        firstRollOfMostRecentSpare = null;
                        frameNumber++;
                    }
                    else
                    {
                        if (previousRoll.HasValue)
                        {
                            var score = previousRoll.Value + roll;
                            if (score == 10)
                            {
                                placeHolders.Add(new PlaceHolder(frameNumber));
                                firstRollOfMostRecentSpare = previousRoll;
                            }
                            else
                            {
                                firstRollOfMostRecentSpare = null;
                                runningTotal += score;
                                var frame = Frame.UninterestingFrame(
                                    frameNumber,
                                    score,
                                    runningTotal,
                                    RollToString(previousRoll.Value),
                                    RollToString(roll));
                                handleFrame(frame);
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

            // TODO: handle partial frames
            // e.g.
            // 4, 4, 5  - frame is missing its second roll
            // 10       - strike where both the next rolls are missing
            // 10, 1    - strike where the next roll is present but the next next roll is missing
            // 2, 8     - spare where the next roll is missing
        }

        public int CalculateTotal(params int[] rolls)
        {
            var total = 0;
            ProcessRolls(frame => total += frame.Score, rolls);
            return total;
        }

        public string RollsToString(params int[] rolls)
        {
            var sb = new StringBuilder();

            ProcessRolls(
                frame =>
                {
                    sb.Append(frame.FirstRoll);
                    sb.Append(frame.SecondRoll);
                    sb.Append(frame.ThirdRoll);
                },
                rolls);

            return sb.ToString();
        }

        private static string RollToString(int roll)
        {
            switch (roll)
            {
                case 0:
                    return RollSymbols.GutterSymbol;

                case 10:
                    return RollSymbols.StrikeSymbol;

                default:
                    return Convert.ToString(roll);
            }
        }

        // TODO: create a PlaceHolderCollection class and move these methods into it.

        private static int FindEmptyPlaceHolder(List<PlaceHolder> placeHolders)
        {
            return placeHolders.FindIndex(PlaceHolderExtensions.IsEmpty);
        }

        private static int CountOfEmptyPlaceHoldersForFrameNumber(IEnumerable<PlaceHolder> placeHolders, int frameNumber)
        {
            return placeHolders.Count(ph => ph.IsEmpty() && ph.FrameNumber == frameNumber);
        }

        private static int CountOfPlaceHoldersForFrameNumber(IEnumerable<PlaceHolder> placeHolders, int frameNumber)
        {
            return placeHolders.Count(ph => ph.FrameNumber == frameNumber);
        }

        private static void FillInEmptyPlaceHolder(IList<PlaceHolder> placeHolders, int index, int roll)
        {
            placeHolders[index] = placeHolders[index].With(roll);
        }
    }
}
