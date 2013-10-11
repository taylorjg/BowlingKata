using System;
using System.Collections.Generic;
using BowlingKata;

namespace BowlingKataApp
{
    internal class Program
    {
        private static void Main()
        {
            //var rolls = new[] {5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5};
            //var rolls = new[] {10, 2, 8, 1, 2};

            // http://bowling.about.com/od/rulesofthegame/a/bowlingscoring.htm
            var rolls = new[] {10, 7, 3, 7, 2, 9, 1, 10, 10, 10, 2, 3, 6, 4, 7, 3, 3};

            var frames = new List<Frame>();

            var tenPinBowlingScoringEngine = new TenPinBowlingScoringEngine();
            tenPinBowlingScoringEngine.ProcessRolls(frames.Add, rolls);

            var lines = new[]
                {
                    "+",
                    "|",
                    "+",
                    "|",
                    "|",
                    "|",
                    "|",
                    "|",
                    "+"
                };

            foreach (var frame in frames)
            {
                FormatFrame(frame, lines);
            }

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

        // +-----+-----+-----+-----+-----+-----+-----+-----+-----+-------+
        // |  1  |  2  |  3  |  4  |  5  |  6  |  7  |  8  |  9  |  10   |
        // +-----+-----+-----+-----+-----+-----+-----+-----+-----+-------+
        // | |X| |                                               | |X|X|X|
        // | +-+-|                                               | +-+-+-|
        // |     |                                               |       |
        // | 30  |                                               |  300  |
        // |     |                                               |       |
        // +-----+-----+-----+-----+-----+-----+-----+-----+-----+-------+

        private static void FormatFrame(Frame frame, IList<string> lines)
        {
            if (!frame.IsLast)
            {
                lines[0] += "-----+";
                lines[1] += string.Format("  {0,-3}|", frame.FrameNumber);
                lines[2] += "-----+";
                lines[3] += string.Format(" |{0,-1}|{1,-1}|", frame.FirstRoll, frame.SecondRoll);
                lines[4] += " +-+-|";
                lines[5] += "     |";
                lines[6] += string.Format(" {0,-4}|", frame.RunningTotal);
                lines[7] += "     |";
                lines[8] += "-----+";
            }
            else
            {
                lines[0] += "-------+";
                lines[1] += string.Format("  {0,-5}|", frame.FrameNumber);
                lines[2] += "-------+";
                lines[3] += string.Format(" |{0,-1}|{1,-1}|{2,-1}|", frame.FirstRoll, frame.SecondRoll, frame.ThirdRoll);
                lines[4] += " +-+-+-|";
                lines[5] += "       |";
                lines[6] += string.Format("  {0,-5}|", frame.RunningTotal);
                lines[7] += "       |";
                lines[8] += "-------+";
            }
        }
    }
}
