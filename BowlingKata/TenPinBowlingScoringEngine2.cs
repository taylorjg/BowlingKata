using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingKata
{
    public class TenPinBowlingScoringEngine2
    {
        public Frame2[] ProcessRolls(IEnumerable<int> rolls)
        {
            var frames = MakeTenEmptyFrames();

            foreach (var roll in rolls)
            {
                foreach (var index in Enumerable.Range(0, 10))
                {
                    if (frames[index].ProcessRoll(roll))
                    {
                        break;
                    }
                }
            }

            var runningTotal = 0;
            foreach (var index in Enumerable.Range(0, 10))
            {
                var frame = frames[index];
                runningTotal += frame.Score;
                frame.SetRunningTotal(runningTotal);
            }

            return frames.ToArray();
        }

        public int CalculateTotalScore(IEnumerable<int> rolls)
        {
            var frames = ProcessRolls(rolls);
            return frames.Sum(frame => frame.Score);
        }

        public string RollsToString(IEnumerable<int> rolls)
        {
            var sb = new StringBuilder();

            foreach (var frame in ProcessRolls(rolls))
            {
                AppendRollSymbol(sb, frame.FirstRoll);
                AppendRollSymbol(sb, frame.SecondRoll);
                AppendRollSymbol(sb, frame.ThirdRoll);
            }

            return sb.ToString();
        }

        private static void AppendRollSymbol(StringBuilder sb, Roll roll)
        {
            if (roll != null)
            {
                sb.Append(roll.Symbol);
            }
        }

        private static List<Frame2> MakeTenEmptyFrames()
        {
            var frames = new List<Frame2>();

            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (var index in Enumerable.Range(0, 10))
            {
                var frameNumber = index + 1;
                frames.Add(new Frame2(frameNumber));
            }
            // ReSharper restore LoopCanBeConvertedToQuery
            return frames;
        }
    }
}
