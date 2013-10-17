using System;
using System.Collections.Generic;
using System.Linq;
using BowlingKata;

namespace BowlingKataApp
{
    internal class Program
    {
        private static void Main()
        {
            var rolls = ChooseRolls();

            var tenPinBowlingScoringEngine = new TenPinBowlingScoringEngine();
            var frames = tenPinBowlingScoringEngine.ProcessRolls(rolls);
            var lines = MakeBeginningsOfLines();

            foreach (var frame in frames)
            {
                FormatFrame(frame, lines);
            }

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

        private static readonly int[][] MenuOfPredefinedRolls =
            {
                new[] { 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0 },
                new[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                new[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 7 },
                new[] { 10, 10, 1, 2 },
                new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 1, 2 },
                new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 3, 1 },
                new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 },
                new[] { 10, 7, 3, 7, 2, 9, 1, 10, 10, 10, 2, 3, 6, 4, 7, 3, 3 }
            };

        private static IEnumerable<int> ChooseRolls()
        {
            var optionChar = 'a';
            foreach (var predefinedRolls in MenuOfPredefinedRolls)
            {
                Console.WriteLine("{0}) {1}", optionChar, PredefinedRollsToString(predefinedRolls));
                optionChar++;
            }
            Console.WriteLine("{0}) I want to enter my own list of rolls", optionChar);
            Console.Write("Please make a selection (a - {0}): ", optionChar);

            var line = Console.ReadLine();

            if (!string.IsNullOrEmpty(line))
            {
                var choiceIndex = line[0] - 'a';

                if (choiceIndex >= 0 && choiceIndex < MenuOfPredefinedRolls.Length)
                {
                    return MenuOfPredefinedRolls[choiceIndex];
                }
                if (choiceIndex == MenuOfPredefinedRolls.Length)
                {
                    return ReadRollsFromConsole();
                }
            }

            Console.WriteLine("Unknown selection - using option a)");
            return MenuOfPredefinedRolls[0];
        }

        private static string PredefinedRollsToString(IEnumerable<int> predefinedRolls)
        {
            return string.Join(", ", predefinedRolls.Select(roll => Convert.ToString(roll)));
        }

        private static IEnumerable<int> ReadRollsFromConsole()
        {
            Console.Write("Enter your rolls (separated by commas): ");
            var line = Console.ReadLine();
            return line == null ? Enumerable.Empty<int>() : line.Split(',').Select(int.Parse);
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

        private static string[] MakeBeginningsOfLines()
        {
            return new[]
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
        }

        private static void FormatFrame(Frame frame, IList<string> lines)
        {
            if (!frame.IsLastFrame)
            {
                lines[0] += "-----+";
                lines[1] += string.Format("  {0,-3}|", frame.FrameNumber);
                lines[2] += "-----+";
                lines[3] += string.Format(" |{0,-1}|{1,-1}|", RollSymbol(frame.FirstRoll), RollSymbol(frame.SecondRoll));
                lines[4] += " +-+-|";
                lines[5] += "     |";
                lines[6] += string.Format(" {0,-4}|", FormatRunningTotal(frame));
                lines[7] += "     |";
                lines[8] += "-----+";
            }
            else
            {
                lines[0] += "-------+";
                lines[1] += string.Format("  {0,-5}|", frame.FrameNumber);
                lines[2] += "-------+";
                lines[3] += string.Format(" |{0,-1}|{1,-1}|{2,-1}|", RollSymbol(frame.FirstRoll), RollSymbol(frame.SecondRoll), RollSymbol(frame.ThirdRoll));
                lines[4] += " +-+-+-|";
                lines[5] += "       |";
                lines[6] += string.Format("  {0,-5}|", FormatRunningTotal(frame));
                lines[7] += "       |";
                lines[8] += "-------+";
            }
        }

        private static string RollSymbol(Roll roll)
        {
            return (roll != null) ? roll.Symbol : string.Empty;
        }

        private static string FormatRunningTotal(Frame frame)
        {
            return frame.IsComplete ? string.Format("{0}", frame.RunningTotal) : string.Format("{0}", string.Empty);
        }
    }
}
