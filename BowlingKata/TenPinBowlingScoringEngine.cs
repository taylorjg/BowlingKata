using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingKata
{
    public class TenPinBowlingScoringEngine
    {
        public int CalculateTotalScore(IEnumerable<int> rolls)
        {
            return ProcessRolls(rolls).Sum(frame => frame.Score);
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

        public Frame[] ProcessRolls(IEnumerable<int> rolls)
        {
            var frames = MakeTenEmptyFrames();
            PopulateFrames(rolls, frames);
            SetRunningTotals(frames);
            return frames;
        }

        private static Frame[] MakeTenEmptyFrames()
        {
            return Enumerable.Range(1, 10).Select(frameNumber => new Frame(frameNumber)).ToArray();
        }

        private static void PopulateFrames(IEnumerable<int> rolls, IList<Frame> frames)
        {
            foreach (var roll in rolls)
            {
                foreach (var frame in frames)
                {
                    var rollBelongsToThisFrame = frame.ProcessRoll(roll);
                    if (rollBelongsToThisFrame)
                    {
                        break;
                    }
                }
            }
        }

        private static void SetRunningTotals(IEnumerable<Frame> frames)
        {
            var runningTotal = 0;
            foreach (var frame in frames)
            {
                runningTotal += frame.Score;
                frame.SetRunningTotal(runningTotal);
            }
        }

        private static void AppendRollSymbol(StringBuilder sb, Roll roll)
        {
            if (roll != null)
            {
                sb.Append(roll.Symbol);
            }
        }
    }
}
