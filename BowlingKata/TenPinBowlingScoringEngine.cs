using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingKata
{
    public class TenPinBowlingScoringEngine
    {
        public Frame[] ProcessRolls(IEnumerable<int> rolls)
        {
            var frames = MakeTenEmptyFrames();

            foreach (var roll in rolls)
            {
                foreach (var index in Enumerable.Range(0, 10))
                {
                    var rollBelongsToThisFrame = frames[index].ProcessRoll(roll);
                    if (rollBelongsToThisFrame)
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

            return frames;
        }

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

        private static void AppendRollSymbol(StringBuilder sb, Roll roll)
        {
            if (roll != null)
            {
                sb.Append(roll.Symbol);
            }
        }

        private static Frame[] MakeTenEmptyFrames()
        {
            return Enumerable.Range(1, 10).Select(frameNumber => new Frame(frameNumber)).ToArray();
        }
    }
}
