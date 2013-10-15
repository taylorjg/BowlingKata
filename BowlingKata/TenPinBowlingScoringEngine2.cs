using System.Collections.Generic;
using System.Linq;

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

            return frames.ToArray();
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
